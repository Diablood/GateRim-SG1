using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using GateRimSG1.Storytelling;
using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Persistent scheduler for bounded Goa'uld territorial takeovers.
    ///
    /// One exact permanent settlement may change owner after every shared
    /// safeguard, cooldown and compatibility rule has been revalidated. The
    /// tracker never creates or destroys a settlement and never affects player
    /// or non-Goa'uld territory.
    /// </summary>
    public sealed class GameComponent_GoauldTerritorialStrategyTracker
        : GameComponent
    {
        private const int CurrentSchemaVersion = 2;
        private const int CheckIntervalTicks = 250;
        private const int DebugReservationDelayTicks = 5000;
        private const int MinimumReservationDelayTicks = 60000;
        private const int MaximumReservationDelayTicks = 120000;
        private const int NaturalAttemptMinimumTicks = 2700000;
        private const int NaturalAttemptMaximumTicks = 5400000;
        private const int GlobalCooldownTicks = 900000;
        private const int BaseDomainCooldownTicks = 1800000;
        private const int PairCooldownTicks = 3600000;
        private const int TerritorialReportVariantCount = 3;

        private static readonly FieldInfo SettlementCachedMaterialField =
            AccessTools.Field(typeof(Settlement), "cachedMat");

        private int schemaVersion = CurrentSchemaVersion;
        private GoauldTerritorialReservationState reservation =
            new GoauldTerritorialReservationState();
        private List<GoauldTerritorialDomainCooldownState> domainCooldowns =
            new List<GoauldTerritorialDomainCooldownState>();
        private List<GoauldTerritorialPairCooldownState> pairCooldowns =
            new List<GoauldTerritorialPairCooldownState>();
        private int configuredDomainCountAtInitialization = -1;
        private int nextCheckTick;
        private int nextGlobalEligibleTick;
        private int nextNaturalAttemptTick;
        private bool wasGateRimStorytellerActive;
        private int suspensionStartTick = -1;
        private string lastReconciliationSummary = "<not reconciled>";
        private string lastTerritorialReportKey;

        public GameComponent_GoauldTerritorialStrategyTracker(Game game)
        {
        }

        public static GameComponent_GoauldTerritorialStrategyTracker Current
            => Verse.Current.Game
                ?.GetComponent<
                    GameComponent_GoauldTerritorialStrategyTracker>();

        public bool SimulationAvailable
            => GateRimStorytellerUtility.IsGateRimStorytellerActive
                && GoauldTerritorialSafeguardUtility
                    .GetActiveTerritorialDomains().Count
                    >= GoauldTerritorialSafeguardUtility
                        .MinimumActiveDomainCount;

        public bool HasPendingReservation
            => reservation?.pending == true;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(
                ref schemaVersion,
                "goauldTerritorialStrategySchemaVersion",
                CurrentSchemaVersion);
            Scribe_Deep.Look(
                ref reservation,
                "goauldTerritorialReservation");
            Scribe_Collections.Look(
                ref domainCooldowns,
                "goauldTerritorialDomainCooldowns",
                LookMode.Deep);
            Scribe_Collections.Look(
                ref pairCooldowns,
                "goauldTerritorialPairCooldowns",
                LookMode.Deep);
            Scribe_Values.Look(
                ref configuredDomainCountAtInitialization,
                "goauldTerritorialConfiguredDomainCountAtInitialization",
                -1);
            Scribe_Values.Look(
                ref nextCheckTick,
                "goauldTerritorialNextCheckTick",
                0);
            Scribe_Values.Look(
                ref nextGlobalEligibleTick,
                "goauldTerritorialNextGlobalEligibleTick",
                0);
            Scribe_Values.Look(
                ref nextNaturalAttemptTick,
                "goauldTerritorialNextNaturalAttemptTick",
                0);
            Scribe_Values.Look(
                ref wasGateRimStorytellerActive,
                "goauldTerritorialStorytellerWasActive",
                false);
            Scribe_Values.Look(
                ref suspensionStartTick,
                "goauldTerritorialSuspensionStartTick",
                -1);
            Scribe_Values.Look(
                ref lastReconciliationSummary,
                "goauldTerritorialLastReconciliationSummary",
                "<not reconciled>");
            Scribe_Values.Look(
                ref lastTerritorialReportKey,
                "goauldTerritorialLastReportKey");

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                MigrateState();
                NormalizeState();
            }
        }

        public override void FinalizeInit()
        {
            base.FinalizeInit();
            InitializeNow();
        }

        public override void StartedNewGame()
        {
            base.StartedNewGame();
            InitializeNow();
        }

        public override void LoadedGame()
        {
            base.LoadedGame();
            InitializeNow();
        }

        public override void GameComponentTick()
        {
            base.GameComponentTick();

            int currentTick = CurrentTick();

            if (currentTick < nextCheckTick)
            {
                return;
            }

            nextCheckTick = currentTick + CheckIntervalTicks;
            ReconcileState(currentTick, cancelInvalidReservation: true);
            ObserveStorytellerBoundary(currentTick);

            if (!GateRimStorytellerUtility.IsGateRimStorytellerActive)
            {
                return;
            }

            if (reservation?.pending == true
                && currentTick >= reservation.resolutionTick)
            {
                ResolvePendingReservation(currentTick);
            }

            if (!HasPendingReservation
                && currentTick >= nextNaturalAttemptTick)
            {
                TryCreateNaturalReservation();
                ArmNextNaturalAttempt(currentTick);
            }
        }

        public bool TryCreateDebugReservation()
        {
            int currentTick = CurrentTick();
            ReconcileState(currentTick, cancelInvalidReservation: true);

            if (HasPendingReservation)
            {
                lastReconciliationSummary =
                    "One territorial takeover is already pending globally.";
                return false;
            }

            List<ReservationCandidate> candidates = BuildCandidates();
            ReservationCandidate selected = candidates
                .Where(candidate =>
                    candidate.evaluation.allowed
                    && CheckCooldowns(
                        candidate.gainingDomain,
                        candidate.losingDomain,
                        currentTick)
                        == GoauldTerritorialReservationOutcome.None)
                .OrderBy(candidate =>
                    candidate.evaluation.gainingSettlementCount)
                .ThenByDescending(candidate =>
                    candidate.evaluation.losingSettlementCount)
                .ThenBy(candidate => candidate.gainingDomain.loadID)
                .ThenBy(candidate => candidate.losingDomain.loadID)
                .ThenBy(candidate => candidate.settlement.ID)
                .FirstOrDefault();

            if (selected == null)
            {
                RecordNoCandidateSummary(
                    candidates,
                    "No debug territorial takeover is currently eligible",
                    currentTick);
                return false;
            }

            return TryScheduleReservation(
                selected.gainingDomain,
                selected.losingDomain,
                selected.settlement,
                GoauldInterDomainRelation.OpenConflict,
                debugShortDelay: true,
                naturalScheduler: false);
        }

        public bool TryRunNaturalAttemptDebug()
        {
            int currentTick = CurrentTick();
            bool created = TryCreateNaturalReservation();
            ArmNextNaturalAttempt(currentTick);
            return created;
        }

        public bool TryScheduleReservation(
            Faction gainingDomain,
            Faction losingDomain,
            Settlement settlement,
            GoauldInterDomainRelation requiredRelation,
            bool debugShortDelay = false,
            bool naturalScheduler = false)
        {
            int currentTick = CurrentTick();
            ReconcileState(currentTick, cancelInvalidReservation: true);

            if (HasPendingReservation)
            {
                return false;
            }

            GoauldTerritorialSafeguardEvaluation evaluation =
                GoauldTerritorialSafeguardUtility.EvaluateTransfer(
                    gainingDomain,
                    losingDomain,
                    settlement,
                    requiredRelation);

            GoauldTerritorialReservationOutcome cooldownFailure =
                CheckCooldowns(
                    gainingDomain,
                    losingDomain,
                    currentTick);

            if (!evaluation.allowed
                || cooldownFailure
                    != GoauldTerritorialReservationOutcome.None)
            {
                lastReconciliationSummary = !evaluation.allowed
                    ? evaluation.failure + " - " + evaluation.detail
                    : cooldownFailure.ToString();
                return false;
            }

            reservation = new GoauldTerritorialReservationState
            {
                gainingDomain = gainingDomain,
                losingDomain = losingDomain,
                settlementWorldObjectId = settlement.ID,
                createdTick = currentTick,
                resolutionTick = currentTick
                    + (debugShortDelay
                        ? DebugReservationDelayTicks
                        : Rand.RangeInclusive(
                            MinimumReservationDelayTicks,
                            MaximumReservationDelayTicks)),
                requiredRelation = requiredRelation,
                outcome = GoauldTerritorialReservationOutcome.Pending,
                pending = true,
                debugShortDelay = debugShortDelay,
                naturalScheduler = naturalScheduler,
                activeDomainCountAtCreation = evaluation.activeDomainCount,
                totalSettlementCountAtCreation =
                    evaluation.totalSettlementCount,
                gainingSettlementCountAtCreation =
                    evaluation.gainingSettlementCount,
                losingSettlementCountAtCreation =
                    evaluation.losingSettlementCount
            };
            lastReconciliationSummary =
                "Created one pending Goa'uld territorial takeover.";

            GR_Log.Message(
                "Reserved Goa'uld territorial takeover: gaining="
                + $"{DomainName(gainingDomain)} ({gainingDomain.loadID}), "
                + $"losing={DomainName(losingDomain)} "
                + $"({losingDomain.loadID}), settlement={settlement.ID}, "
                + $"resolutionTick={reservation.resolutionTick}, "
                + $"debugShortDelay={debugShortDelay}, "
                + $"naturalScheduler={naturalScheduler}.");
            return true;
        }

        public bool TriggerPendingReservationNow()
        {
            if (!HasPendingReservation
                || !GateRimStorytellerUtility.IsGateRimStorytellerActive)
            {
                return false;
            }

            int currentTick = CurrentTick();
            reservation.resolutionTick = currentTick;
            ResolvePendingReservation(currentTick);
            return reservation.outcome
                == GoauldTerritorialReservationOutcome.CompletedTransfer;
        }

        public bool CancelPendingReservationDebug()
        {
            if (!HasPendingReservation)
            {
                return false;
            }

            CompleteReservation(
                GoauldTerritorialReservationOutcome.CancelledDebug,
                CurrentTick(),
                "Cancelled manually through the developer menu.");
            return true;
        }

        public void ReconcileDebug()
        {
            bool wasPending = HasPendingReservation;
            ReconcileState(CurrentTick(), cancelInvalidReservation: true);

            if (!wasPending || HasPendingReservation)
            {
                SetReconciliationSummary();
            }
        }

        public void ResetDebug()
        {
            reservation = new GoauldTerritorialReservationState();
            domainCooldowns.Clear();
            pairCooldowns.Clear();
            nextCheckTick = 0;
            nextGlobalEligibleTick = 0;
            nextNaturalAttemptTick = 0;
            suspensionStartTick = -1;
            wasGateRimStorytellerActive =
                GateRimStorytellerUtility.IsGateRimStorytellerActive;
            lastTerritorialReportKey = null;
            ArmNextNaturalAttempt(CurrentTick());
            lastReconciliationSummary = "Territorial strategy state reset.";
        }

        public string BuildDebugReport()
        {
            int currentTick = CurrentTick();
            ReconcileState(currentTick, cancelInvalidReservation: true);
            ObserveStorytellerBoundary(currentTick);

            List<Faction> allDomains =
                GoauldSystemLordFactionUtility.GetAllFactions(
                    includeDefeated: true);
            List<Faction> activeTerritorialDomains =
                GoauldTerritorialSafeguardUtility
                    .GetActiveTerritorialDomains();
            List<Settlement> settlements =
                GoauldTerritorialSafeguardUtility
                    .GetPermanentGoauldSettlements();
            int minimumSettlements =
                GoauldTerritorialSafeguardUtility
                    .MinimumSettlementCountForTransfers(
                        activeTerritorialDomains.Count);

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Goa'uld territorial strategy report");
            builder.AppendLine();
            builder.AppendLine("schema: " + schemaVersion);
            builder.AppendLine(
                "SG-1 storyteller active: "
                + FormatBoolean(
                    GateRimStorytellerUtility
                        .IsGateRimStorytellerActive));
            builder.AppendLine(
                "territorial simulation available: "
                + FormatBoolean(SimulationAvailable));
            builder.AppendLine(
                "Goa'uld domains at tracker initialization: "
                + configuredDomainCountAtInitialization);
            builder.AppendLine(
                "currently existing Goa'uld domains: "
                + allDomains.Count);
            builder.AppendLine(
                "active territorial domains: "
                + activeTerritorialDomains.Count);
            builder.AppendLine(
                "minimum active domains: "
                + GoauldTerritorialSafeguardUtility
                    .MinimumActiveDomainCount);
            builder.AppendLine(
                "permanent Goa'uld settlements: "
                + settlements.Count);
            builder.AppendLine(
                "minimum settlements for transfers: "
                + minimumSettlements
                + " (active domains + "
                + GoauldTerritorialSafeguardUtility
                    .MinimumWorldSurplusSettlementCount
                + " surplus)");
            builder.AppendLine(
                "automatic territorial share ceiling: "
                + GoauldTerritorialSafeguardUtility
                    .MaximumAutomaticTerritorialShare(
                        activeTerritorialDomains.Count)
                    .ToString("P0")
                + " (75% with two domains; 50% with three or more)");
            builder.AppendLine(
                "natural attempt cadence: 45-90 days");
            builder.AppendLine(
                "next natural attempt: "
                + FormatRemaining(nextNaturalAttemptTick));
            builder.AppendLine(
                "global cooldown: "
                + FormatRemaining(nextGlobalEligibleTick));
            builder.AppendLine(
                "suspension started: "
                + FormatTick(suspensionStartTick));
            builder.AppendLine(
                "last reconciliation: "
                + lastReconciliationSummary);
            builder.AppendLine();

            builder.AppendLine("Domains");
            builder.AppendLine("-------");

            if (allDomains.Count == 0)
            {
                builder.AppendLine("<none>");
            }

            foreach (Faction domain in allDomains)
            {
                int settlementCount = settlements.Count(settlement =>
                    settlement.Faction == domain);
                GoauldTerritorialDomainCooldownState cooldown =
                    FindDomainCooldown(domain);

                builder.AppendLine(
                    DomainName(domain)
                    + " ("
                    + domain.loadID
                    + ")");
                builder.AppendLine(
                    "  defeated: " + FormatBoolean(domain.defeated));
                builder.AppendLine(
                    "  permanent settlements: " + settlementCount);
                builder.AppendLine(
                    "  territorial active: "
                    + FormatBoolean(
                        activeTerritorialDomains.Contains(domain)));
                builder.AppendLine(
                    "  final settlement protected: "
                    + FormatBoolean(settlementCount <= 1));
                builder.AppendLine(
                    "  expansion weight: x"
                    + GoauldTerritorialSafeguardUtility
                        .ExpansionWeight(settlementCount)
                        .ToString("0.00"));
                builder.AppendLine(
                    "  expansion delay multiplier: x"
                    + GoauldTerritorialSafeguardUtility
                        .ExpansionDelayMultiplier(settlementCount));
                builder.AppendLine(
                    "  domain cooldown: "
                    + FormatRemaining(cooldown?.nextEligibleTick ?? 0));
            }

            builder.AppendLine();
            builder.AppendLine("Diplomatic coherence");
            builder.AppendLine("---------------------");

            List<GoauldInterDomainRelationState> pairs =
                GameComponent_GoauldInterDomainRelationTracker.Current
                    ?.Snapshot()
                ?? new List<GoauldInterDomainRelationState>();

            if (pairs.Count == 0)
            {
                builder.AppendLine("<no stored pair>");
            }

            foreach (GoauldInterDomainRelationState pair in pairs)
            {
                FactionRelationKind expected =
                    GameComponent_GoauldInterDomainRelationTracker
                        .VanillaRelationFor(pair.relation);
                FactionRelationKind actual =
                    pair.firstDomain?.RelationKindWith(pair.secondDomain)
                        ?? FactionRelationKind.Hostile;
                GoauldTerritorialPairCooldownState cooldown =
                    FindPairCooldown(
                        pair.firstDomain,
                        pair.secondDomain);

                builder.AppendLine(
                    DomainName(pair.firstDomain)
                    + " <-> "
                    + DomainName(pair.secondDomain));
                builder.AppendLine(
                    "  GateRim relation: " + pair.relation);
                builder.AppendLine(
                    "  vanilla relation: " + actual);
                builder.AppendLine(
                    "  expected vanilla relation: " + expected);
                builder.AppendLine(
                    "  coherent: "
                    + FormatBoolean(actual == expected));
                builder.AppendLine(
                    "  pair cooldown: "
                    + FormatRemaining(cooldown?.nextEligibleTick ?? 0));
            }

            builder.AppendLine();
            builder.AppendLine("Pending reservation");
            builder.AppendLine("-------------------");
            AppendReservation(builder);
            builder.AppendLine();
            builder.AppendLine(
                "Bounded-transfer contract: this tracker may change exactly "
                + "one eligible Goa'uld settlement owner. It never creates "
                + "or destroys a settlement, targets player or outside "
                + "territory, changes doctrine, alters raid cadence or "
                + "modifies player goodwill.");

            return builder.ToString();
        }

        private void InitializeNow()
        {
            NormalizeState();
            int currentTick = CurrentTick();

            if (configuredDomainCountAtInitialization < 0)
            {
                configuredDomainCountAtInitialization =
                    GoauldSystemLordFactionUtility.GetAllFactions(
                        includeDefeated: true).Count;
            }

            if (nextNaturalAttemptTick <= 0)
            {
                ArmNextNaturalAttempt(currentTick);
            }

            ReconcileState(currentTick, cancelInvalidReservation: true);
            ObserveStorytellerBoundary(currentTick);
        }

        private bool TryCreateNaturalReservation()
        {
            int currentTick = CurrentTick();
            ReconcileState(currentTick, cancelInvalidReservation: true);

            if (HasPendingReservation)
            {
                return false;
            }

            List<ReservationCandidate> candidates = BuildCandidates();
            List<ReservationCandidate> eligible = candidates
                .Where(candidate =>
                    candidate.evaluation.allowed
                    && CheckCooldowns(
                        candidate.gainingDomain,
                        candidate.losingDomain,
                        currentTick)
                        == GoauldTerritorialReservationOutcome.None)
                .ToList();

            ReservationCandidate selected = SelectWeightedCandidate(eligible);

            if (selected == null)
            {
                RecordNoCandidateSummary(
                    candidates,
                    "Natural territorial attempt found no eligible takeover",
                    currentTick);
                return false;
            }

            return TryScheduleReservation(
                selected.gainingDomain,
                selected.losingDomain,
                selected.settlement,
                GoauldInterDomainRelation.OpenConflict,
                debugShortDelay: false,
                naturalScheduler: true);
        }

        private List<ReservationCandidate> BuildCandidates()
        {
            List<GoauldInterDomainRelationState> openConflictPairs =
                GameComponent_GoauldInterDomainRelationTracker.Current
                    ?.Snapshot(includeInactive: false)
                    .Where(state =>
                        state?.relation
                            == GoauldInterDomainRelation.OpenConflict)
                    .ToList()
                ?? new List<GoauldInterDomainRelationState>();
            List<ReservationCandidate> candidates =
                new List<ReservationCandidate>();

            foreach (GoauldInterDomainRelationState pair in openConflictPairs)
            {
                AddCandidates(
                    candidates,
                    pair.firstDomain,
                    pair.secondDomain);
                AddCandidates(
                    candidates,
                    pair.secondDomain,
                    pair.firstDomain);
            }

            return candidates;
        }

        private void AddCandidates(
            List<ReservationCandidate> candidates,
            Faction gainingDomain,
            Faction losingDomain)
        {
            List<Settlement> settlements =
                GoauldTerritorialSafeguardUtility
                    .GetPermanentSettlements(losingDomain);

            if (settlements.Count == 0)
            {
                settlements.Add(null);
            }

            foreach (Settlement settlement in settlements)
            {
                GoauldTerritorialSafeguardEvaluation evaluation =
                    GoauldTerritorialSafeguardUtility.EvaluateTransfer(
                        gainingDomain,
                        losingDomain,
                        settlement,
                        GoauldInterDomainRelation.OpenConflict);

                candidates.Add(new ReservationCandidate
                {
                    gainingDomain = gainingDomain,
                    losingDomain = losingDomain,
                    settlement = settlement,
                    evaluation = evaluation
                });
            }
        }

        private ReservationCandidate SelectWeightedCandidate(
            List<ReservationCandidate> candidates)
        {
            if (candidates == null || candidates.Count == 0)
            {
                return null;
            }

            float totalWeight = candidates.Sum(candidate =>
                GoauldTerritorialSafeguardUtility.ExpansionWeight(
                    candidate.evaluation.gainingSettlementCount));

            if (totalWeight <= 0f)
            {
                return null;
            }

            float selection = Rand.Value * totalWeight;

            foreach (ReservationCandidate candidate in candidates)
            {
                selection -= GoauldTerritorialSafeguardUtility
                    .ExpansionWeight(
                        candidate.evaluation.gainingSettlementCount);

                if (selection <= 0f)
                {
                    return candidate;
                }
            }

            return candidates[candidates.Count - 1];
        }

        private void RecordNoCandidateSummary(
            List<ReservationCandidate> candidates,
            string prefix,
            int currentTick)
        {
            ReservationCandidate diagnostic = candidates
                ?.OrderBy(candidate => candidate.evaluation.failure)
                .ThenBy(candidate => candidate.gainingDomain?.loadID ?? 0)
                .ThenBy(candidate => candidate.losingDomain?.loadID ?? 0)
                .FirstOrDefault();

            if (diagnostic == null)
            {
                lastReconciliationSummary =
                    prefix + ": no active open-conflict pair exists.";
                return;
            }

            if (!diagnostic.evaluation.allowed)
            {
                lastReconciliationSummary =
                    prefix
                    + ": "
                    + diagnostic.evaluation.failure
                    + " - "
                    + diagnostic.evaluation.detail;
                return;
            }

            GoauldTerritorialReservationOutcome cooldownFailure =
                CheckCooldowns(
                    diagnostic.gainingDomain,
                    diagnostic.losingDomain,
                    currentTick);
            lastReconciliationSummary = cooldownFailure
                == GoauldTerritorialReservationOutcome.None
                ? prefix + ": no weighted candidate was selected."
                : prefix + ": " + cooldownFailure;
        }

        private void ResolvePendingReservation(int currentTick)
        {
            if (reservation?.pending != true)
            {
                return;
            }

            Settlement settlement =
                GoauldTerritorialSafeguardUtility.FindSettlement(
                    reservation.settlementWorldObjectId);
            GoauldTerritorialSafeguardEvaluation evaluation =
                GoauldTerritorialSafeguardUtility.EvaluateTransfer(
                    reservation.gainingDomain,
                    reservation.losingDomain,
                    settlement,
                    reservation.requiredRelation);

            if (!evaluation.allowed)
            {
                CompleteReservation(
                    OutcomeFor(evaluation.failure),
                    currentTick,
                    evaluation.failure + " - " + evaluation.detail);
                return;
            }

            GoauldTerritorialReservationOutcome cooldownFailure =
                CheckCooldowns(
                    reservation.gainingDomain,
                    reservation.losingDomain,
                    currentTick,
                    ignoreCurrentReservation: true);

            if (cooldownFailure
                != GoauldTerritorialReservationOutcome.None)
            {
                CompleteReservation(
                    cooldownFailure,
                    currentTick,
                    cooldownFailure.ToString());
                return;
            }

            string settlementLabel = settlement.LabelCap;
            Faction gainingDomain = reservation.gainingDomain;
            Faction losingDomain = reservation.losingDomain;

            if (!ApplySettlementTransfer(settlement, gainingDomain))
            {
                CompleteReservation(
                    GoauldTerritorialReservationOutcome
                        .CancelledMutationFailed,
                    currentTick,
                    "The settlement owner could not be changed safely.");
                return;
            }

            ApplyCooldowns(
                gainingDomain,
                losingDomain,
                evaluation.projectedGainingSettlementCount,
                currentTick);
            CompleteReservation(
                GoauldTerritorialReservationOutcome.CompletedTransfer,
                currentTick,
                settlementLabel
                    + " changed owner from "
                    + DomainName(losingDomain)
                    + " to "
                    + DomainName(gainingDomain)
                    + ".");
            SendTerritorialTransferLetter(
                settlement,
                settlementLabel,
                gainingDomain,
                losingDomain);
        }

        private bool ApplySettlementTransfer(
            Settlement settlement,
            Faction gainingDomain)
        {
            if (settlement == null || gainingDomain == null)
            {
                return false;
            }

            if (SettlementCachedMaterialField == null)
            {
                GR_Log.ErrorOnce(
                    "Unable to locate RimWorld Settlement.cachedMat; "
                    + "the territorial takeover was cancelled before changing ownership.",
                    138843901);
                return false;
            }

            Faction previousOwner = settlement.Faction;

            try
            {
                SettlementCachedMaterialField.SetValue(settlement, null);
                settlement.SetFaction(gainingDomain);
            }
            catch (Exception exception)
            {
                if (settlement.Faction != gainingDomain)
                {
                    GR_Log.Error(
                        "Failed to apply bounded Goa'uld territorial "
                        + "takeover before ownership changed: "
                        + exception);
                    return false;
                }

                GR_Log.Warning(
                    "The Goa'uld settlement owner changed despite an "
                    + "exception reported by a patched faction setter; "
                    + "the transfer will be completed consistently: "
                    + exception);
            }

            if (settlement.Faction != gainingDomain)
            {
                GR_Log.Error(
                    "RimWorld returned from Settlement.SetFaction without "
                    + "applying the requested Goa'uld territorial owner.");
                return false;
            }

            try
            {
                settlement.trader?.TryDestroyStock();
                settlement.previouslyGeneratedInhabitants?.Clear();
                Find.World?.renderer?.Notify_StaticWorldObjectPosChanged();
            }
            catch (Exception exception)
            {
                GR_Log.Warning(
                    "The Goa'uld settlement changed owner from "
                    + DomainName(previousOwner)
                    + " to "
                    + DomainName(gainingDomain)
                    + ", but one cached settlement surface could not be "
                    + "fully reset: "
                    + exception);
            }

            return true;
        }

        private void SendTerritorialTransferLetter(
            Settlement settlement,
            string settlementLabel,
            Faction gainingDomain,
            Faction losingDomain)
        {
            string reportKey = SelectTerritorialReportKey();
            Find.LetterStack?.ReceiveLetter(
                "GR_GoauldTerritorialTakeover_Label".Translate(),
                reportKey.Translate(
                    settlementLabel,
                    DomainName(losingDomain),
                    DomainName(gainingDomain)),
                LetterDefOf.NeutralEvent,
                new GlobalTargetInfo(settlement.Tile));
        }

        private string SelectTerritorialReportKey()
        {
            int variant = Rand.Range(0, TerritorialReportVariantCount);
            string key = "GR_GoauldTerritorialTakeover_Text" + variant;

            if (key == lastTerritorialReportKey
                && TerritorialReportVariantCount > 1)
            {
                variant = (variant
                    + Rand.Range(1, TerritorialReportVariantCount))
                    % TerritorialReportVariantCount;
                key = "GR_GoauldTerritorialTakeover_Text" + variant;
            }

            lastTerritorialReportKey = key;
            return key;
        }

        private void CompleteReservation(
            GoauldTerritorialReservationOutcome outcome,
            int currentTick,
            string summary)
        {
            reservation.pending = false;
            reservation.outcome = outcome;
            reservation.completedTick = currentTick;
            lastReconciliationSummary = summary;

            GR_Log.Message(
                "Completed Goa'uld territorial reservation with "
                + $"outcome={outcome}; gaining="
                + $"{DomainName(reservation.gainingDomain)}, losing="
                + $"{DomainName(reservation.losingDomain)}, settlement="
                + $"{reservation.settlementWorldObjectId}.");
        }

        private void ReconcileState(
            int currentTick,
            bool cancelInvalidReservation)
        {
            NormalizeState();
            GameComponent_GoauldInterDomainRelationTracker.Current
                ?.ReconcileAllPairs();

            List<Faction> validDomains =
                GoauldSystemLordFactionUtility.GetAllFactions(
                    includeDefeated: true);

            domainCooldowns.RemoveAll(state =>
                state?.domain == null
                || !validDomains.Contains(state.domain));
            pairCooldowns.RemoveAll(state =>
                state?.firstDomain == null
                || state.secondDomain == null
                || state.firstDomain == state.secondDomain
                || !validDomains.Contains(state.firstDomain)
                || !validDomains.Contains(state.secondDomain));

            bool reservationCancelled = false;

            if (reservation?.pending == true
                && cancelInvalidReservation
                && GateRimStorytellerUtility.IsGateRimStorytellerActive)
            {
                Settlement settlement =
                    GoauldTerritorialSafeguardUtility.FindSettlement(
                        reservation.settlementWorldObjectId);
                GoauldTerritorialSafeguardEvaluation evaluation =
                    GoauldTerritorialSafeguardUtility.EvaluateTransfer(
                        reservation.gainingDomain,
                        reservation.losingDomain,
                        settlement,
                        reservation.requiredRelation);

                if (!evaluation.allowed
                    && evaluation.failure
                        != GoauldTerritorialSafeguardFailure
                            .StorytellerInactive)
                {
                    CompleteReservation(
                        OutcomeFor(evaluation.failure),
                        currentTick,
                        evaluation.failure + " - " + evaluation.detail);
                    reservationCancelled = true;
                }
            }

            if (!reservationCancelled
                && (lastReconciliationSummary.NullOrEmpty()
                    || lastReconciliationSummary == "<not reconciled>"))
            {
                SetReconciliationSummary();
            }
        }

        private void SetReconciliationSummary()
        {
            lastReconciliationSummary =
                "Reconciled "
                + GoauldTerritorialSafeguardUtility
                    .GetActiveTerritorialDomains().Count
                + " active territorial domain(s), "
                + GoauldTerritorialSafeguardUtility
                    .GetPermanentGoauldSettlements().Count
                + " permanent settlement(s) and diplomatic coherence.";
        }

        private GoauldTerritorialReservationOutcome CheckCooldowns(
            Faction gainingDomain,
            Faction losingDomain,
            int currentTick,
            bool ignoreCurrentReservation = false)
        {
            if (!ignoreCurrentReservation && HasPendingReservation)
            {
                return GoauldTerritorialReservationOutcome
                    .CancelledIncompatibleTransition;
            }

            if (currentTick < nextGlobalEligibleTick)
            {
                return GoauldTerritorialReservationOutcome
                    .CancelledGlobalCooldown;
            }

            if (currentTick
                < (FindDomainCooldown(gainingDomain)?.nextEligibleTick ?? 0)
                || currentTick
                    < (FindDomainCooldown(losingDomain)?.nextEligibleTick ?? 0))
            {
                return GoauldTerritorialReservationOutcome
                    .CancelledDomainCooldown;
            }

            if (currentTick
                < (FindPairCooldown(
                        gainingDomain,
                        losingDomain)
                    ?.nextEligibleTick ?? 0))
            {
                return GoauldTerritorialReservationOutcome
                    .CancelledPairCooldown;
            }

            return GoauldTerritorialReservationOutcome.None;
        }

        private void ApplyCooldowns(
            Faction gainingDomain,
            Faction losingDomain,
            int gainingSettlementCount,
            int currentTick)
        {
            nextGlobalEligibleTick = currentTick + GlobalCooldownTicks;
            int gainingDelay = BaseDomainCooldownTicks
                * GoauldTerritorialSafeguardUtility
                    .ExpansionDelayMultiplier(gainingSettlementCount);
            SetDomainCooldown(
                gainingDomain,
                currentTick + gainingDelay);
            SetDomainCooldown(
                losingDomain,
                currentTick + BaseDomainCooldownTicks);
            SetPairCooldown(
                gainingDomain,
                losingDomain,
                currentTick + PairCooldownTicks);
        }

        private GoauldTerritorialDomainCooldownState FindDomainCooldown(
            Faction domain)
        {
            return domainCooldowns.FirstOrDefault(state =>
                state?.domain == domain);
        }

        private void SetDomainCooldown(Faction domain, int tick)
        {
            GoauldTerritorialDomainCooldownState state =
                FindDomainCooldown(domain);

            if (state == null)
            {
                state = new GoauldTerritorialDomainCooldownState
                {
                    domain = domain
                };
                domainCooldowns.Add(state);
            }

            state.nextEligibleTick = Math.Max(
                state.nextEligibleTick,
                tick);
        }

        private GoauldTerritorialPairCooldownState FindPairCooldown(
            Faction first,
            Faction second)
        {
            Canonicalize(ref first, ref second);
            return pairCooldowns.FirstOrDefault(state =>
                state?.firstDomain == first
                && state.secondDomain == second);
        }

        private void SetPairCooldown(
            Faction first,
            Faction second,
            int tick)
        {
            Canonicalize(ref first, ref second);
            GoauldTerritorialPairCooldownState state =
                FindPairCooldown(first, second);

            if (state == null)
            {
                state = new GoauldTerritorialPairCooldownState
                {
                    firstDomain = first,
                    secondDomain = second
                };
                pairCooldowns.Add(state);
            }

            state.nextEligibleTick = Math.Max(
                state.nextEligibleTick,
                tick);
        }

        private void ArmNextNaturalAttempt(int currentTick)
        {
            nextNaturalAttemptTick = currentTick
                + Rand.RangeInclusive(
                    NaturalAttemptMinimumTicks,
                    NaturalAttemptMaximumTicks);
        }

        private void ObserveStorytellerBoundary(int currentTick)
        {
            bool active =
                GateRimStorytellerUtility.IsGateRimStorytellerActive;

            if (!active && wasGateRimStorytellerActive)
            {
                suspensionStartTick = currentTick;
            }
            else if (!active && suspensionStartTick < 0)
            {
                suspensionStartTick = currentTick;
            }
            else if (active && !wasGateRimStorytellerActive)
            {
                ResumeAfterSuspension(currentTick);
            }

            wasGateRimStorytellerActive = active;
        }

        private void ResumeAfterSuspension(int currentTick)
        {
            if (suspensionStartTick < 0
                || currentTick <= suspensionStartTick)
            {
                suspensionStartTick = -1;
                return;
            }

            int pausedTicks = currentTick - suspensionStartTick;

            if (reservation?.pending == true
                && reservation.resolutionTick > 0)
            {
                reservation.resolutionTick += pausedTicks;
            }

            if (nextGlobalEligibleTick > 0)
            {
                nextGlobalEligibleTick += pausedTicks;
            }

            if (nextNaturalAttemptTick > 0)
            {
                nextNaturalAttemptTick += pausedTicks;
            }

            foreach (GoauldTerritorialDomainCooldownState state
                in domainCooldowns)
            {
                if (state?.nextEligibleTick > 0)
                {
                    state.nextEligibleTick += pausedTicks;
                }
            }

            foreach (GoauldTerritorialPairCooldownState state
                in pairCooldowns)
            {
                if (state?.nextEligibleTick > 0)
                {
                    state.nextEligibleTick += pausedTicks;
                }
            }

            suspensionStartTick = -1;
        }

        private void MigrateState()
        {
            if (schemaVersion >= CurrentSchemaVersion)
            {
                return;
            }

            domainCooldowns =
                new List<GoauldTerritorialDomainCooldownState>();
            pairCooldowns =
                new List<GoauldTerritorialPairCooldownState>();
            nextGlobalEligibleTick = 0;
            nextNaturalAttemptTick = 0;

            if (reservation?.pending == true)
            {
                reservation.pending = false;
                reservation.outcome =
                    GoauldTerritorialReservationOutcome
                        .CancelledLegacyDryRun;
                reservation.completedTick = CurrentTick();
                lastReconciliationSummary =
                    "Cancelled one legacy 0.3.83 dry-run reservation and "
                    + "cleared dry-run cooldowns; no settlement owner was "
                    + "changed during migration.";
            }
            else
            {
                lastReconciliationSummary =
                    "Cleared legacy 0.3.83 dry-run cooldowns during "
                    + "territorial strategy migration.";
            }

            schemaVersion = CurrentSchemaVersion;
        }

        private void NormalizeState()
        {
            schemaVersion = CurrentSchemaVersion;
            reservation = reservation
                ?? new GoauldTerritorialReservationState();
            domainCooldowns = domainCooldowns
                ?.Where(state => state != null)
                .ToList()
                ?? new List<GoauldTerritorialDomainCooldownState>();
            pairCooldowns = pairCooldowns
                ?.Where(state => state != null)
                .ToList()
                ?? new List<GoauldTerritorialPairCooldownState>();
            configuredDomainCountAtInitialization = Math.Max(
                -1,
                configuredDomainCountAtInitialization);
            nextCheckTick = Math.Max(0, nextCheckTick);
            nextGlobalEligibleTick = Math.Max(
                0,
                nextGlobalEligibleTick);
            nextNaturalAttemptTick = Math.Max(
                0,
                nextNaturalAttemptTick);
            suspensionStartTick = Math.Max(-1, suspensionStartTick);
            reservation.settlementWorldObjectId = Math.Max(
                -1,
                reservation.settlementWorldObjectId);
            reservation.createdTick = Math.Max(0, reservation.createdTick);
            reservation.resolutionTick = Math.Max(
                0,
                reservation.resolutionTick);
            reservation.completedTick = Math.Max(
                0,
                reservation.completedTick);

            foreach (GoauldTerritorialPairCooldownState state
                in pairCooldowns)
            {
                Canonicalize(
                    ref state.firstDomain,
                    ref state.secondDomain);
                state.nextEligibleTick = Math.Max(
                    0,
                    state.nextEligibleTick);
            }

            foreach (GoauldTerritorialDomainCooldownState state
                in domainCooldowns)
            {
                state.nextEligibleTick = Math.Max(
                    0,
                    state.nextEligibleTick);
            }
        }

        private void AppendReservation(StringBuilder builder)
        {
            if (reservation == null
                || (reservation.outcome
                        == GoauldTerritorialReservationOutcome.None
                    && !reservation.pending))
            {
                builder.AppendLine("<none>");
                return;
            }

            Settlement settlement =
                GoauldTerritorialSafeguardUtility.FindSettlement(
                    reservation.settlementWorldObjectId);
            builder.AppendLine(
                "outcome: " + reservation.outcome);
            builder.AppendLine(
                "pending: " + FormatBoolean(reservation.pending));
            builder.AppendLine(
                "source: "
                + (reservation.naturalScheduler
                    ? "natural scheduler"
                    : "developer action"));
            builder.AppendLine(
                "gaining domain: "
                + DomainName(reservation.gainingDomain));
            builder.AppendLine(
                "losing domain: "
                + DomainName(reservation.losingDomain));
            builder.AppendLine(
                "settlement: "
                + (settlement == null
                    ? reservation.settlementWorldObjectId + " <missing>"
                    : settlement.LabelCap
                        + " ("
                        + settlement.ID
                        + ")"));
            builder.AppendLine(
                "current owner: "
                + DomainName(settlement?.Faction));

            List<Settlement> currentSettlements =
                GoauldTerritorialSafeguardUtility
                    .GetPermanentGoauldSettlements();
            int currentActiveDomainCount = currentSettlements
                .Select(item => item.Faction)
                .Where(faction => faction != null && !faction.defeated)
                .Distinct()
                .Count();
            int currentGainingCount = currentSettlements.Count(item =>
                item.Faction == reservation.gainingDomain);
            int currentLosingCount = currentSettlements.Count(item =>
                item.Faction == reservation.losingDomain);
            int projectedGainingCount = reservation.pending
                && settlement?.Faction == reservation.losingDomain
                    ? currentGainingCount + 1
                    : currentGainingCount;
            float projectedShare = currentSettlements.Count <= 0
                ? 0f
                : (float)projectedGainingCount
                    / currentSettlements.Count;

            builder.AppendLine(
                "current counts: total="
                + currentSettlements.Count
                + ", gaining="
                + currentGainingCount
                + ", losing="
                + currentLosingCount);
            builder.AppendLine(
                (reservation.pending
                    ? "projected gaining share: "
                    : "current gaining share: ")
                + projectedShare.ToString("P0")
                + " / ceiling "
                + GoauldTerritorialSafeguardUtility
                    .MaximumAutomaticTerritorialShare(
                        currentActiveDomainCount)
                    .ToString("P0"));
            builder.AppendLine(
                "map loaded: "
                + FormatBoolean(settlement?.HasMap == true));
            builder.AppendLine(
                "player present on tile: "
                + FormatBoolean(
                    GoauldTerritorialSafeguardUtility
                        .HasPlayerWorldObjectAtTile(settlement)));
            builder.AppendLine(
                "active quest target: "
                + FormatBoolean(
                    GoauldTerritorialSafeguardUtility
                        .IsActiveQuestTarget(settlement)));
            builder.AppendLine(
                "required relation: "
                + reservation.requiredRelation);
            builder.AppendLine(
                "created: " + FormatTick(reservation.createdTick));
            builder.AppendLine(
                "deadline: "
                + FormatRemaining(reservation.resolutionTick));
            builder.AppendLine(
                "completed: "
                + FormatTick(reservation.completedTick));
            builder.AppendLine(
                "creation snapshot: domains="
                + reservation.activeDomainCountAtCreation
                + ", settlements="
                + reservation.totalSettlementCountAtCreation
                + ", gaining="
                + reservation.gainingSettlementCountAtCreation
                + ", losing="
                + reservation.losingSettlementCountAtCreation);
        }

        private static GoauldTerritorialReservationOutcome OutcomeFor(
            GoauldTerritorialSafeguardFailure failure)
        {
            switch (failure)
            {
                case GoauldTerritorialSafeguardFailure.StorytellerInactive:
                    return GoauldTerritorialReservationOutcome
                        .CancelledStorytellerInactive;
                case GoauldTerritorialSafeguardFailure
                    .TooFewActiveDomains:
                    return GoauldTerritorialReservationOutcome
                        .CancelledTooFewActiveDomains;
                case GoauldTerritorialSafeguardFailure.SparseWorld:
                    return GoauldTerritorialReservationOutcome
                        .CancelledSparseWorld;
                case GoauldTerritorialSafeguardFailure.InactiveDomain:
                    return GoauldTerritorialReservationOutcome
                        .CancelledInactiveDomain;
                case GoauldTerritorialSafeguardFailure.MissingSettlement:
                    return GoauldTerritorialReservationOutcome
                        .CancelledMissingSettlement;
                case GoauldTerritorialSafeguardFailure.OwnershipChanged:
                    return GoauldTerritorialReservationOutcome
                        .CancelledOwnershipChanged;
                case GoauldTerritorialSafeguardFailure
                    .SettlementMapLoaded:
                    return GoauldTerritorialReservationOutcome
                        .CancelledSettlementMapLoaded;
                case GoauldTerritorialSafeguardFailure.PlayerPresent:
                    return GoauldTerritorialReservationOutcome
                        .CancelledPlayerPresent;
                case GoauldTerritorialSafeguardFailure
                    .QuestTargetProtected:
                    return GoauldTerritorialReservationOutcome
                        .CancelledQuestTargetProtected;
                case GoauldTerritorialSafeguardFailure.RelationChanged:
                    return GoauldTerritorialReservationOutcome
                        .CancelledRelationChanged;
                case GoauldTerritorialSafeguardFailure
                    .LastSettlementProtected:
                    return GoauldTerritorialReservationOutcome
                        .CancelledLastSettlementProtected;
                case GoauldTerritorialSafeguardFailure.HegemonyLimit:
                    return GoauldTerritorialReservationOutcome
                        .CancelledHegemonyLimit;
                case GoauldTerritorialSafeguardFailure
                    .IncompatibleTransition:
                    return GoauldTerritorialReservationOutcome
                        .CancelledIncompatibleTransition;
                default:
                    return GoauldTerritorialReservationOutcome
                        .CancelledIncompatibleTransition;
            }
        }

        private static void Canonicalize(
            ref Faction first,
            ref Faction second)
        {
            if (first != null
                && second != null
                && first.loadID > second.loadID)
            {
                Faction temporary = first;
                first = second;
                second = temporary;
            }
        }

        private static int CurrentTick()
        {
            return Find.TickManager?.TicksGame ?? 0;
        }

        private static string DomainName(Faction faction)
        {
            return faction?.Name ?? "<missing domain>";
        }

        private static string FormatBoolean(bool value)
        {
            return value ? "yes" : "no";
        }

        private static string FormatTick(int tick)
        {
            if (tick <= 0)
            {
                return "<never>";
            }

            return tick
                + " ("
                + ((float)tick / GenDate.TicksPerDay).ToString("0.00")
                + " days)";
        }

        private static string FormatRemaining(int tick)
        {
            if (tick <= 0)
            {
                return "<not scheduled>";
            }

            int remaining = Math.Max(0, tick - CurrentTick());
            return remaining
                + " ticks ("
                + ((float)remaining / GenDate.TicksPerDay)
                    .ToString("0.00")
                + " days)";
        }

        private sealed class ReservationCandidate
        {
            public Faction gainingDomain;
            public Faction losingDomain;
            public Settlement settlement;
            public GoauldTerritorialSafeguardEvaluation evaluation;
        }
    }
}
