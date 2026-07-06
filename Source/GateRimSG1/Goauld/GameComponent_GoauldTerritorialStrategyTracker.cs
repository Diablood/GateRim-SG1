using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GateRimSG1.Storytelling;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Persistent dry-run foundation for future Goa'uld territorial changes.
    ///
    /// This milestone may reserve and validate one exact transfer candidate,
    /// but never changes settlement ownership, creates a settlement or destroys
    /// a world object. Future consequences must pass through this tracker rather
    /// than duplicating their own strategic limits.
    /// </summary>
    public sealed class GameComponent_GoauldTerritorialStrategyTracker
        : GameComponent
    {
        private const int CurrentSchemaVersion = 1;
        private const int CheckIntervalTicks = 250;
        private const int DebugReservationDelayTicks = 5000;
        private const int MinimumReservationDelayTicks = 60000;
        private const int MaximumReservationDelayTicks = 120000;
        private const int GlobalCooldownTicks = 900000;
        private const int BaseDomainCooldownTicks = 1800000;
        private const int PairCooldownTicks = 3600000;

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
        private bool wasGateRimStorytellerActive;
        private int suspensionStartTick = -1;
        private string lastReconciliationSummary = "<not reconciled>";

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

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
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

            if (!GateRimStorytellerUtility.IsGateRimStorytellerActive
                || reservation?.pending != true
                || currentTick < reservation.resolutionTick)
            {
                return;
            }

            ResolvePendingReservation(currentTick);
        }

        public bool TryCreateDebugReservation()
        {
            ReconcileState(CurrentTick(), cancelInvalidReservation: true);

            if (HasPendingReservation)
            {
                return false;
            }

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
                AddCandidate(
                    candidates,
                    pair.firstDomain,
                    pair.secondDomain);
                AddCandidate(
                    candidates,
                    pair.secondDomain,
                    pair.firstDomain);
            }

            ReservationCandidate selected = candidates
                .Where(candidate => candidate.evaluation.allowed)
                .OrderBy(candidate =>
                    candidate.evaluation.gainingSettlementCount)
                .ThenByDescending(candidate =>
                    candidate.evaluation.losingSettlementCount)
                .ThenBy(candidate => candidate.gainingDomain.loadID)
                .ThenBy(candidate => candidate.losingDomain.loadID)
                .FirstOrDefault();

            if (selected == null)
            {
                ReservationCandidate diagnostic = candidates
                    .OrderBy(candidate => candidate.evaluation.failure)
                    .FirstOrDefault();
                lastReconciliationSummary = diagnostic == null
                    ? "No active open-conflict pair exists for a territorial dry run."
                    : "No dry-run transfer is currently eligible: "
                        + diagnostic.evaluation.failure
                        + " - "
                        + diagnostic.evaluation.detail;
                return false;
            }

            return TryScheduleReservation(
                selected.gainingDomain,
                selected.losingDomain,
                selected.settlement,
                GoauldInterDomainRelation.OpenConflict,
                debugShortDelay: true);
        }

        public bool TryScheduleReservation(
            Faction gainingDomain,
            Faction losingDomain,
            Settlement settlement,
            GoauldInterDomainRelation requiredRelation,
            bool debugShortDelay = false)
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
                activeDomainCountAtCreation = evaluation.activeDomainCount,
                totalSettlementCountAtCreation =
                    evaluation.totalSettlementCount,
                gainingSettlementCountAtCreation =
                    evaluation.gainingSettlementCount,
                losingSettlementCountAtCreation =
                    evaluation.losingSettlementCount
            };
            lastReconciliationSummary =
                "Created one dry-run territorial reservation; no world object was modified.";

            GR_Log.Message(
                "Reserved Goa'uld territorial dry run: gaining="
                + $"{DomainName(gainingDomain)} ({gainingDomain.loadID}), "
                + $"losing={DomainName(losingDomain)} "
                + $"({losingDomain.loadID}), settlement={settlement.ID}, "
                + $"resolutionTick={reservation.resolutionTick}, "
                + $"debugShortDelay={debugShortDelay}.");
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
                == GoauldTerritorialReservationOutcome.CompletedDryRun;
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
            ReconcileState(CurrentTick(), cancelInvalidReservation: true);
        }

        public void ResetDebug()
        {
            reservation = new GoauldTerritorialReservationState();
            domainCooldowns.Clear();
            pairCooldowns.Clear();
            nextCheckTick = 0;
            nextGlobalEligibleTick = 0;
            suspensionStartTick = -1;
            wasGateRimStorytellerActive =
                GateRimStorytellerUtility.IsGateRimStorytellerActive;
            lastReconciliationSummary = "Territorial safeguard state reset.";
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
            builder.AppendLine("Goa'uld territorial safeguard report");
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
                    .MaximumAutomaticTerritorialShare
                    .ToString("P0"));
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
                "Dry-run contract: this tracker never changes settlement "
                + "ownership, creates a settlement, destroys a world object, "
                + "changes doctrine, alters raid cadence or modifies player "
                + "goodwill.");

            return builder.ToString();
        }

        private void InitializeNow()
        {
            NormalizeState();

            if (configuredDomainCountAtInitialization < 0)
            {
                configuredDomainCountAtInitialization =
                    GoauldSystemLordFactionUtility.GetAllFactions(
                        includeDefeated: true).Count;
            }

            ReconcileState(CurrentTick(), cancelInvalidReservation: true);
            ObserveStorytellerBoundary(CurrentTick());
        }

        private void AddCandidate(
            List<ReservationCandidate> candidates,
            Faction gainingDomain,
            Faction losingDomain)
        {
            Settlement settlement =
                GoauldTerritorialSafeguardUtility
                    .GetPermanentSettlements(losingDomain)
                    .FirstOrDefault();
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

            ApplyCooldowns(
                reservation.gainingDomain,
                reservation.losingDomain,
                evaluation.gainingSettlementCount,
                currentTick);
            CompleteReservation(
                GoauldTerritorialReservationOutcome.CompletedDryRun,
                currentTick,
                "Dry run completed; the candidate remained eligible and no territorial mutation was applied.");
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
                "Completed Goa'uld territorial reservation dry run with "
                + $"outcome={outcome}; gaining="
                + $"{DomainName(reservation.gainingDomain)}, losing="
                + $"{DomainName(reservation.losingDomain)}, settlement="
                + $"{reservation.settlementWorldObjectId}. No territorial "
                + "mutation was applied.");
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

            if (!reservationCancelled)
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
