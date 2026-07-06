using System;
using System.Collections.Generic;
using System.Linq;
using GateRimSG1.Storytelling;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Persistent Goa'uld reactions to player-caused extraction outcomes,
    /// decisive raid defeats and major failures of shared alliance reprisals.
    /// The layers reuse one tracker without creating a parallel mission or
    /// grievance framework.
    /// </summary>
    public sealed class GameComponent_GoauldDomainReprisalTracker
        : GameComponent
    {
        private const string UltimatumLetterDefName =
            "SG1_GoauldExtractionUltimatum";
        private const int UltimatumDurationTicks = 60000;
        private const int MinimumDelayTicks = 60000;
        private const int MaximumDelayTicks = 180000;
        private const int DebugDelayTicks = 5000;
        private const int CooldownTicks = 900000;
        private const int CheckIntervalTicks = 250;

        public const int SharedMinimumInitialPawnCount = 5;
        public const float SharedDefeatRemainingFraction = 0.25f;
        public const float SharedReprisalChance = 0.25f;
        public const float SharedThreatBudgetFactor = 0.80f;
        public const int SharedMinimumDelayTicks = 120000;
        public const int SharedMaximumDelayTicks = 240000;
        public const int SharedPairCooldownTicks = 1800000;

        public const int AllianceRuptureMinimumInitialPawnCount = 6;
        public const float AllianceRuptureFailureRemainingFraction = 0.20f;
        public const int AllianceRuptureMinimumDelayTicks = 60000;
        public const int AllianceRuptureMaximumDelayTicks = 120000;

        private const int SharedDebugDelayTicks = 5000;
        private const int AllianceRuptureDebugDelayTicks = 5000;
        private const int AllianceRuptureVariantCount = 3;

        private List<GoauldDomainReprisalState> states =
            new List<GoauldDomainReprisalState>();
        private List<GoauldSharedAllianceRaidObservation>
            sharedAllianceObservations =
                new List<GoauldSharedAllianceRaidObservation>();
        private List<GoauldSharedAllianceReprisalState>
            sharedAllianceStates =
                new List<GoauldSharedAllianceReprisalState>();
        private List<GoauldAllianceMajorFailureObservation>
            allianceFailureObservations =
                new List<GoauldAllianceMajorFailureObservation>();
        private List<GoauldAllianceRuptureState> allianceRuptureStates =
            new List<GoauldAllianceRuptureState>();
        private int nextCheckTick;
        private bool sharedStorytellerWasActive;
        private int sharedSuspensionStartTick = -1;
        private string lastAllianceRuptureReportKey;

        public GameComponent_GoauldDomainReprisalTracker(Game game)
        {
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(
                ref states,
                "goauldDomainReprisalStates",
                LookMode.Deep);
            Scribe_Values.Look(
                ref nextCheckTick,
                "goauldDomainReprisalNextCheckTick",
                0);
            Scribe_Collections.Look(
                ref sharedAllianceObservations,
                "goauldSharedAllianceRaidObservations",
                LookMode.Deep);
            Scribe_Collections.Look(
                ref sharedAllianceStates,
                "goauldSharedAllianceReprisalStates",
                LookMode.Deep);
            Scribe_Collections.Look(
                ref allianceFailureObservations,
                "goauldAllianceMajorFailureObservations",
                LookMode.Deep);
            Scribe_Collections.Look(
                ref allianceRuptureStates,
                "goauldAllianceRuptureStates",
                LookMode.Deep);
            Scribe_Values.Look(
                ref lastAllianceRuptureReportKey,
                "goauldAllianceRuptureLastReportKey");
            Scribe_Values.Look(
                ref sharedStorytellerWasActive,
                "goauldSharedAllianceStorytellerWasActive",
                false);
            Scribe_Values.Look(
                ref sharedSuspensionStartTick,
                "goauldSharedAllianceSuspensionStartTick",
                -1);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                states = states
                    ?.Where(state => state != null)
                    .ToList()
                    ?? new List<GoauldDomainReprisalState>();
                sharedAllianceObservations = sharedAllianceObservations
                    ?.Where(IsValidSharedObservation)
                    .ToList()
                    ?? new List<GoauldSharedAllianceRaidObservation>();
                sharedAllianceStates = sharedAllianceStates
                    ?.Where(IsValidSharedState)
                    .ToList()
                    ?? new List<GoauldSharedAllianceReprisalState>();
                allianceFailureObservations = allianceFailureObservations
                    ?.Where(IsValidAllianceFailureObservation)
                    .ToList()
                    ?? new List<GoauldAllianceMajorFailureObservation>();
                allianceRuptureStates = allianceRuptureStates
                    ?.Where(IsValidAllianceRuptureState)
                    .ToList()
                    ?? new List<GoauldAllianceRuptureState>();
                sharedSuspensionStartTick = Math.Max(
                    -1,
                    sharedSuspensionStartTick);
            }
        }

        public override void FinalizeInit()
        {
            base.FinalizeInit();
            InitializeSharedAllianceState();
        }

        public override void StartedNewGame()
        {
            base.StartedNewGame();
            InitializeSharedAllianceState();
        }

        public override void LoadedGame()
        {
            base.LoadedGame();
            InitializeSharedAllianceState();
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

            for (int index = 0; index < states.Count; index++)
            {
                GoauldDomainReprisalState state = states[index];

                if (state?.ultimatumPending == true)
                {
                    if (DemandedSymbioteWasDestroyed(state))
                    {
                        ScheduleReprisal(
                            state,
                            currentTick,
                            UltimatumResolution.SymbioteDestroyed);
                    }
                    else if (currentTick >= state.ultimatumExpiryTick)
                    {
                        ScheduleReprisal(
                            state,
                            currentTick,
                            UltimatumResolution.Expired);
                    }
                    else
                    {
                        MaintainUltimatumSedation(state);
                    }
                }
                else if (state?.pending == true
                    && currentTick >= state.reprisalTick)
                {
                    TryResolveReprisal(state, currentTick);
                }
            }

            TickSharedAllianceReprisals(currentTick);
        }

        public static void NotifyActiveHostExtracted(
            Pawn formerHost,
            GoauldSymbioteData symbioteData,
            Pawn freeSymbiote)
        {
            Map map = formerHost?.Map;
            Faction domainFaction = symbioteData?.AllegianceFaction;

            if (map == null
                || !map.IsPlayerHome
                || freeSymbiote == null
                || !freeSymbiote.Spawned
                || symbioteData?.Origin != GoauldSymbioteOrigin.Goauld
                || !GoauldSystemLordFactionUtility.IsSystemLordFaction(
                    domainFaction))
            {
                return;
            }

            Current?.TryBeginUltimatum(
                map,
                domainFaction,
                formerHost.LabelShortCap,
                symbioteData.SymbioteName,
                freeSymbiote,
                debugShortDelay: false,
                debugGeneratedSymbiote: false);
        }

        public static void NotifyFatalActiveHostExtraction(
            Map map,
            string formerHostLabel,
            GoauldSymbioteData symbioteData)
        {
            Faction domainFaction = symbioteData?.AllegianceFaction;

            if (map == null
                || !map.IsPlayerHome
                || symbioteData?.Origin != GoauldSymbioteOrigin.Goauld
                || !GoauldSystemLordFactionUtility.IsSystemLordFaction(
                    domainFaction))
            {
                return;
            }

            Current?.TryScheduleFatalExtractionReprisal(
                map,
                domainFaction,
                formerHostLabel,
                symbioteData.SymbioteName,
                debugShortDelay: false);
        }

        public static GameComponent_GoauldDomainReprisalTracker Current
            => Verse.Current.Game
                ?.GetComponent<GameComponent_GoauldDomainReprisalTracker>();

        public static void NotifyNaturalStandardRaidStarted(
            Map map,
            Faction primaryDomain,
            List<Pawn> primaryPawns)
        {
            Current?.TryObserveNaturalStandardRaid(
                map,
                primaryDomain,
                primaryPawns);
        }

        public bool TryCreateDebugUltimatum(Map map)
        {
            Faction faction = Find.FactionManager?.FirstFactionOfDef(
                GR_DefOf.SG1_GoauldSystemLordPrototype);

            if (map == null || faction == null)
            {
                return false;
            }

            Pawn symbiote = PawnGenerator.GeneratePawn(
                GR_DefOf.SG1_GoauldSymbiote,
                faction,
                map.Tile);

            if (symbiote == null
                || !GenPlace.TryPlaceThing(
                    symbiote,
                    map.Center,
                    map,
                    ThingPlaceMode.Near))
            {
                symbiote?.Destroy(DestroyMode.Vanish);
                return false;
            }

            SedateDebugSymbiote(symbiote);

            bool started = TryBeginUltimatum(
                map,
                faction,
                "GR_GoauldDomainReprisal_DebugFormerHost"
                    .Translate()
                    .ToString(),
                symbiote.LabelShortCap,
                symbiote,
                debugShortDelay: true,
                debugGeneratedSymbiote: true);

            if (!started && !symbiote.Destroyed)
            {
                symbiote.Destroy(DestroyMode.Vanish);
            }

            return started;
        }

        public bool TryCreateDebugFatalExtractionReprisal(Map map)
        {
            Faction faction = Find.FactionManager?.FirstFactionOfDef(
                GR_DefOf.SG1_GoauldSystemLordPrototype);

            return TryScheduleFatalExtractionReprisal(
                map,
                faction,
                "GR_GoauldDomainReprisal_DebugFormerHost"
                    .Translate()
                    .ToString(),
                "GR_GoauldDomainFatalExtraction_DebugSymbiote"
                    .Translate()
                    .ToString(),
                debugShortDelay: true);
        }

        public static bool IsCurrentUltimatum(Faction domainFaction)
        {
            return Current?.FindState(domainFaction)?.ultimatumPending == true;
        }

        public static bool CanSurrenderDemandedSymbiote(
            Faction domainFaction,
            out string disabledReason)
        {
            disabledReason = string.Empty;
            GoauldDomainReprisalState state = Current?.FindState(domainFaction);

            if (state?.ultimatumPending != true)
            {
                disabledReason = "GR_GoauldDomainUltimatum_NoLongerActive"
                    .Translate();
                return false;
            }

            Pawn symbiote = state.demandedSymbiote;
            Map targetMap = Current?.FindTargetMap(state);

            if (symbiote == null
                || symbiote.Destroyed
                || !symbiote.Spawned
                || symbiote.Map != targetMap)
            {
                disabledReason =
                    "GR_GoauldDomainUltimatum_SymbioteUnavailable"
                    .Translate(state.symbioteName);
                return false;
            }

            return true;
        }

        public static void SurrenderFromLetter(Faction domainFaction)
        {
            Current?.TrySurrender(domainFaction);
        }

        public static void DefyFromLetter(Faction domainFaction)
        {
            Current?.TryDefy(domainFaction);
        }

        public bool TryCreateDebugSharedAllianceReprisal(Map map)
        {
            if (map == null
                || !map.IsPlayerHome
                || !GameComponent_GoauldAlliedReinforcementTracker
                    .TryGetFirstAlliancePair(
                        out Faction primaryDomain,
                        out Faction alliedDomain))
            {
                return false;
            }

            return TryScheduleSharedAllianceReprisal(
                map,
                primaryDomain,
                alliedDomain,
                debugShortDelay: true,
                bypassChanceAndCooldown: true);
        }

        public bool TriggerFirstPendingSharedAllianceReprisalNow()
        {
            GoauldSharedAllianceReprisalState state = sharedAllianceStates
                .FirstOrDefault(candidate => candidate?.pending == true);

            if (state == null)
            {
                return false;
            }

            int currentTick = CurrentTick();
            state.debugForcedExecution = true;
            state.reprisalTick = currentTick;
            return TryResolveSharedAllianceReprisal(state, currentTick);
        }

        public void ResetSharedAllianceReprisalsDebug()
        {
            sharedAllianceObservations.Clear();
            sharedAllianceStates.Clear();
            sharedSuspensionStartTick = -1;
            sharedStorytellerWasActive =
                GateRimStorytellerUtility.IsGateRimStorytellerActive;
        }

        public bool TryCreateDebugMajorAllianceFailure(Map map)
        {
            if (map == null
                || !map.IsPlayerHome
                || HasPendingAllianceRupture()
                || !GameComponent_GoauldAlliedReinforcementTracker
                    .TryGetFirstAlliancePair(
                        out Faction primaryDomain,
                        out Faction alliedDomain))
            {
                return false;
            }

            return TryScheduleAllianceRupture(
                map.uniqueID,
                primaryDomain,
                alliedDomain,
                AllianceRuptureMinimumInitialPawnCount,
                1,
                debugShortDelay: true);
        }

        public bool HasPendingAllianceRuptureForPair(
            Faction firstDomain,
            Faction secondDomain)
        {
            return allianceRuptureStates.Any(state =>
                state?.pending == true
                && ((state.primaryDomain == firstDomain
                        && state.alliedDomain == secondDomain)
                    || (state.primaryDomain == secondDomain
                        && state.alliedDomain == firstDomain)));
        }

        public bool TriggerFirstPendingAllianceRuptureNow()
        {
            GoauldAllianceRuptureState state = allianceRuptureStates
                .FirstOrDefault(candidate => candidate?.pending == true);

            if (state == null)
            {
                return false;
            }

            int currentTick = CurrentTick();
            state.ruptureTick = currentTick;
            return TryResolveAllianceRupture(state, currentTick);
        }

        public void ResetAllianceRupturesDebug()
        {
            allianceFailureObservations.Clear();
            allianceRuptureStates.Clear();
            lastAllianceRuptureReportKey = null;
        }

        public bool TriggerFirstPendingNow()
        {
            GoauldDomainReprisalState state = states.FirstOrDefault(
                candidate => candidate?.pending == true);

            if (state == null)
            {
                return false;
            }

            int currentTick = CurrentTick();
            state.reprisalTick = currentTick;
            return TryResolveReprisal(state, currentTick);
        }

        public bool ExpireFirstUltimatumNow()
        {
            GoauldDomainReprisalState state = states.FirstOrDefault(
                candidate => candidate?.ultimatumPending == true);

            if (state == null)
            {
                return false;
            }

            ScheduleReprisal(
                state,
                CurrentTick(),
                UltimatumResolution.Expired);
            return true;
        }

        public bool KillFirstDemandedSymbioteDebug()
        {
            GoauldDomainReprisalState state = states.FirstOrDefault(
                candidate => candidate?.ultimatumPending == true);
            Pawn symbiote = state?.demandedSymbiote;

            if (symbiote == null || symbiote.Destroyed || symbiote.Dead)
            {
                return false;
            }

            symbiote.Kill(null);

            if (symbiote.Dead || symbiote.Destroyed)
            {
                ScheduleReprisal(
                    state,
                    CurrentTick(),
                    UltimatumResolution.SymbioteDestroyed);
                return true;
            }

            return false;
        }

        public void ResetDebug()
        {
            foreach (GoauldDomainReprisalState state in states)
            {
                RemoveUltimatumLetters(state?.domainFaction);
                DestroyDebugSymbiote(state);
            }

            states.Clear();
            ResetSharedAllianceReprisalsDebug();
            ResetAllianceRupturesDebug();
            nextCheckTick = 0;
        }

        public string BuildDebugReport()
        {
            int currentTick = CurrentTick();
            List<string> lines = new List<string>();

            lines.Add("Goa'uld domain reaction report");
            lines.Add(string.Empty);
            lines.Add("Extraction reactions:");

            if (states.Count == 0)
            {
                lines.Add("  none");
            }
            else
            {
                lines.AddRange(states.Select(state =>
                    "  "
                    + $"{state.domainFaction?.Name ?? "<missing domain>"}: "
                    + (state.ultimatumPending
                        ? $"ultimatum expires in {Math.Max(0, state.ultimatumExpiryTick - currentTick)} ticks, "
                            + $"symbiote {state.demandedSymbiote?.ThingID ?? "<missing>"}, "
                            + $"map {state.targetMapUniqueId}, {state.raidPoints:0} points"
                        : state.pending
                        ? $"pending in {Math.Max(0, state.reprisalTick - currentTick)} ticks, "
                            + $"map {state.targetMapUniqueId}, {state.raidPoints:0} points"
                        : $"cooldown {Math.Max(0, state.nextEligibleTick - currentTick)} ticks")));
            }

            lines.Add(string.Empty);
            lines.Add("Shared alliance reprisals:");
            lines.Add(
                "  SG-1 Command active: "
                + (GateRimStorytellerUtility
                    .IsGateRimStorytellerActive ? "yes" : "no"));
            lines.Add(
                "  observed eligible standard raids: "
                + sharedAllianceObservations.Count);

            if (sharedAllianceStates.Count == 0)
            {
                lines.Add("  pending/cooldown pairs: none");
            }
            else
            {
                lines.AddRange(sharedAllianceStates.Select(state =>
                    "  "
                    + $"{state.primaryDomain?.Name ?? "<missing>"} + "
                    + $"{state.alliedDomain?.Name ?? "<missing>"}: "
                    + (state.pending
                        ? $"pending in {Math.Max(0, state.reprisalTick - currentTick)} ticks, "
                            + $"map {state.targetMapUniqueId}, {state.raidPoints:0} combined points"
                        : $"pair cooldown {Math.Max(0, state.nextEligibleTick - currentTick)} ticks")));
            }

            lines.Add(string.Empty);
            lines.Add("Major alliance failure observations:");

            if (allianceFailureObservations.Count == 0)
            {
                lines.Add("  none");
            }
            else
            {
                foreach (GoauldAllianceMajorFailureObservation observation
                    in allianceFailureObservations)
                {
                    Map observationMap = FindMap(
                        observation.targetMapUniqueId);
                    int activeCount = ActiveAllianceFailurePawnCount(
                        observation,
                        observationMap);
                    lines.Add(
                        "  "
                        + $"{observation.primaryDomain?.Name ?? "<missing>"} + "
                        + $"{observation.alliedDomain?.Name ?? "<missing>"}: "
                        + $"active {activeCount}/{observation.initialPawnCount}, "
                        + $"threshold <= {observation.initialPawnCount * AllianceRuptureFailureRemainingFraction:0.##}, "
                        + $"map {observation.targetMapUniqueId}");
                }
            }

            lines.Add(string.Empty);
            lines.Add("Alliance ruptures after major failure:");

            if (allianceRuptureStates.Count == 0)
            {
                lines.Add("  none");
            }
            else
            {
                lines.AddRange(allianceRuptureStates.Select(state =>
                    "  "
                    + $"{state.primaryDomain?.Name ?? "<missing>"} <-> "
                    + $"{state.alliedDomain?.Name ?? "<missing>"}: "
                    + (state.pending
                        ? $"pending in {Math.Max(0, state.ruptureTick - currentTick)} ticks, "
                            + $"failure {state.activePawnCountAtFailure}/{state.initialPawnCount}, "
                            + $"map {state.targetMapUniqueId}"
                        : $"{state.outcome}, resolved tick {state.resolutionTick}")));
            }

            lines.Add(
                "  rupture rule: natural shared reprisal only, at least 6 "
                + "combined Jaffa, evaluate once at <=20% active, "
                + "deterministic Alliance -> Rivalry after 1-2 days");
            lines.Add(
                "  one pending rupture globally; cancel if the exact pair "
                + "is no longer allied or either domain becomes inactive");

            lines.Add(string.Empty);
            lines.Add(
                "shared rule: standard natural raid only, at least 5 initial "
                + "Jaffa, evaluate once at <=25% active, 25% chance, one "
                + "global pending reprisal, 30-day pair cooldown");
            lines.Add(
                "shared attack: 80% of vanilla threat points, direct joint "
                + "raid split 60/40 between the exact allied pair");

            return string.Join("\n", lines);
        }

        private void InitializeSharedAllianceState()
        {
            sharedAllianceObservations = sharedAllianceObservations
                ?.Where(IsValidSharedObservation)
                .ToList()
                ?? new List<GoauldSharedAllianceRaidObservation>();
            sharedAllianceStates = sharedAllianceStates
                ?.Where(IsValidSharedState)
                .ToList()
                ?? new List<GoauldSharedAllianceReprisalState>();
            allianceFailureObservations = allianceFailureObservations
                ?.Where(IsValidAllianceFailureObservation)
                .ToList()
                ?? new List<GoauldAllianceMajorFailureObservation>();
            allianceRuptureStates = allianceRuptureStates
                ?.Where(IsValidAllianceRuptureState)
                .ToList()
                ?? new List<GoauldAllianceRuptureState>();
            sharedStorytellerWasActive =
                GateRimStorytellerUtility.IsGateRimStorytellerActive;

            if (sharedStorytellerWasActive)
            {
                sharedSuspensionStartTick = -1;
            }
        }

        private void TickSharedAllianceReprisals(int currentTick)
        {
            ObserveSharedStorytellerBoundary(currentTick);

            if (!GateRimStorytellerUtility.IsGateRimStorytellerActive)
            {
                return;
            }

            for (int index = sharedAllianceObservations.Count - 1;
                index >= 0;
                index--)
            {
                GoauldSharedAllianceRaidObservation observation =
                    sharedAllianceObservations[index];

                if (!IsValidSharedObservation(observation))
                {
                    sharedAllianceObservations.RemoveAt(index);
                    continue;
                }

                Map map = FindMap(observation.targetMapUniqueId);

                if (map == null || !map.IsPlayerHome)
                {
                    sharedAllianceObservations.RemoveAt(index);
                    continue;
                }

                int activeCount = ActiveSharedRaidPawnCount(
                    observation.primaryPawns,
                    map,
                    observation.primaryDomain);
                float threshold = observation.initialPawnCount
                    * SharedDefeatRemainingFraction;

                if (activeCount > threshold)
                {
                    continue;
                }

                sharedAllianceObservations.RemoveAt(index);

                if (!AreDomainsStillAllied(
                        observation.primaryDomain,
                        observation.alliedDomain)
                    || Rand.Value >= SharedReprisalChance)
                {
                    continue;
                }

                TryScheduleSharedAllianceReprisal(
                    map,
                    observation.primaryDomain,
                    observation.alliedDomain,
                    debugShortDelay: false,
                    bypassChanceAndCooldown: false);
            }

            foreach (GoauldSharedAllianceReprisalState state
                in sharedAllianceStates.Where(candidate =>
                    candidate?.pending == true
                    && currentTick >= candidate.reprisalTick).ToList())
            {
                TryResolveSharedAllianceReprisal(state, currentTick);
            }

            TickAllianceFailureObservations();
            TickAllianceRuptures(currentTick);
        }

        private bool TryObserveNaturalStandardRaid(
            Map map,
            Faction primaryDomain,
            List<Pawn> primaryPawns)
        {
            List<Pawn> validPawns = primaryPawns
                ?.Where(pawn =>
                    pawn != null
                    && pawn.Faction == primaryDomain)
                .Distinct()
                .ToList()
                ?? new List<Pawn>();

            if (!GateRimStorytellerUtility.IsGateRimStorytellerActive
                || map == null
                || !map.IsPlayerHome
                || !GoauldSystemLordFactionUtility.IsSystemLordFaction(
                    primaryDomain)
                || validPawns.Count < SharedMinimumInitialPawnCount
                || HasPendingSharedAllianceReprisal()
                || !TryResolveEligibleAlliedDomain(
                    primaryDomain,
                    out Faction alliedDomain)
                || PairIsCoolingDown(
                    primaryDomain,
                    alliedDomain,
                    CurrentTick()))
            {
                return false;
            }

            sharedAllianceObservations.RemoveAll(candidate =>
                candidate?.targetMapUniqueId == map.uniqueID
                && candidate.primaryDomain == primaryDomain);
            sharedAllianceObservations.Add(
                new GoauldSharedAllianceRaidObservation
                {
                    targetMapUniqueId = map.uniqueID,
                    primaryDomain = primaryDomain,
                    alliedDomain = alliedDomain,
                    primaryPawns = validPawns,
                    initialPawnCount = validPawns.Count
                });

            GR_Log.Message(
                "Observing eligible standard Goa'uld alliance-context raid "
                + $"for a possible shared reprisal: primary={primaryDomain.Name} "
                + $"({primaryDomain.loadID}), ally={alliedDomain.Name} "
                + $"({alliedDomain.loadID}), map={map.uniqueID}, "
                + $"initial pawns={validPawns.Count}.");
            return true;
        }

        private bool TryScheduleSharedAllianceReprisal(
            Map map,
            Faction primaryDomain,
            Faction alliedDomain,
            bool debugShortDelay,
            bool bypassChanceAndCooldown)
        {
            int currentTick = CurrentTick();

            if (!GateRimStorytellerUtility.IsGateRimStorytellerActive
                || map == null
                || !map.IsPlayerHome
                || primaryDomain == null
                || alliedDomain == null
                || primaryDomain == alliedDomain
                || !GoauldSystemLordFactionUtility.IsSystemLordFaction(
                    primaryDomain)
                || !GoauldSystemLordFactionUtility.IsSystemLordFaction(
                    alliedDomain)
                || HasPendingSharedAllianceReprisal()
                || (!bypassChanceAndCooldown
                    && (!AreDomainsStillAllied(primaryDomain, alliedDomain)
                        || PairIsCoolingDown(
                            primaryDomain,
                            alliedDomain,
                            currentTick))))
            {
                return false;
            }

            GoauldSharedAllianceReprisalState state =
                FindOrCreateSharedState(primaryDomain, alliedDomain);
            int delay = debugShortDelay
                ? SharedDebugDelayTicks
                : Rand.RangeInclusive(
                    SharedMinimumDelayTicks,
                    SharedMaximumDelayTicks);

            state.primaryDomain = primaryDomain;
            state.alliedDomain = alliedDomain;
            state.targetMapUniqueId = map.uniqueID;
            state.reprisalTick = currentTick + delay;
            state.raidPoints = Math.Max(
                1f,
                StorytellerUtility.DefaultThreatPointsNow(map)
                    * SharedThreatBudgetFactor);
            state.pending = true;
            state.debugShortDelay = debugShortDelay;
            state.debugForcedExecution = false;

            // This warning announces a future reaction only. It intentionally
            // has no LookTargets, so RimWorld does not offer a misleading
            // camera jump before any force or world site exists.
            Find.LetterStack?.ReceiveLetter(
                "GR_GoauldSharedAllianceReprisal_WarningLabel".Translate(),
                "GR_GoauldSharedAllianceReprisal_WarningText".Translate(
                    primaryDomain.Name,
                    alliedDomain.Name,
                    delay.ToStringTicksToPeriod()),
                LetterDefOf.ThreatBig,
                (LookTargets)null);

            GR_Log.Message(
                "Scheduled shared Goa'uld alliance reprisal: "
                + $"primary={primaryDomain.Name} ({primaryDomain.loadID}), "
                + $"ally={alliedDomain.Name} ({alliedDomain.loadID}), "
                + $"map={map.uniqueID}, delay={delay}, "
                + $"combined points={state.raidPoints:0}.");
            return true;
        }

        private bool TryResolveSharedAllianceReprisal(
            GoauldSharedAllianceReprisalState state,
            int currentTick)
        {
            if (state?.pending != true
                || !GateRimStorytellerUtility.IsGateRimStorytellerActive)
            {
                return false;
            }

            Map map = FindMap(state.targetMapUniqueId);
            bool excludeAllianceRuptureObservation =
                state.debugShortDelay || state.debugForcedExecution;

            if (map == null
                || !map.IsPlayerHome
                || !AreDomainsStillAllied(
                    state.primaryDomain,
                    state.alliedDomain))
            {
                CompleteSharedState(state, currentTick);
                return false;
            }

            if (GameComponent_GoauldAlliedReinforcementTracker
                .HasPendingOrActive(map))
            {
                state.reprisalTick = currentTick + CheckIntervalTicks;
                return false;
            }

            IncidentWorker_GoauldJaffaNaturalRaid worker =
                GR_DefOf.SG1_GoauldJaffaNaturalRaid?.Worker
                    as IncidentWorker_GoauldJaffaNaturalRaid;

            if (worker == null)
            {
                GR_Log.Error(
                    "Cannot start shared Goa'uld alliance reprisal: natural "
                    + "raid worker is unavailable.");
                CompleteSharedState(state, currentTick);
                return false;
            }

            HashSet<string> primaryBefore = PawnIdsForFaction(
                map,
                state.primaryDomain);
            HashSet<string> alliedBefore = PawnIdsForFaction(
                map,
                state.alliedDomain);
            IncidentParms parms = new IncidentParms
            {
                target = map,
                faction = state.primaryDomain,
                forced = true,
                points = Math.Max(1f, state.raidPoints),
                sendLetter = false
            };

            bool succeeded = worker.TryExecuteForcedSharedAllianceReprisal(
                parms,
                state.alliedDomain);

            if (succeeded)
            {
                List<Pawn> primaryPawns = NewPawnsForFaction(
                    map,
                    state.primaryDomain,
                    primaryBefore);
                List<Pawn> alliedPawns = NewPawnsForFaction(
                    map,
                    state.alliedDomain,
                    alliedBefore);
                List<Pawn> targets = new List<Pawn>();
                Pawn primaryTarget = primaryPawns.FirstOrDefault(
                    pawn => pawn?.Spawned == true);
                Pawn alliedTarget = alliedPawns.FirstOrDefault(
                    pawn => pawn?.Spawned == true);

                if (primaryTarget != null)
                {
                    targets.Add(primaryTarget);
                }

                if (alliedTarget != null)
                {
                    targets.Add(alliedTarget);
                }

                Find.LetterStack?.ReceiveLetter(
                    "GR_GoauldSharedAllianceReprisal_ArrivalLabel"
                        .Translate(),
                    "GR_GoauldSharedAllianceReprisal_ArrivalText"
                        .Translate(
                            state.primaryDomain.Name,
                            state.alliedDomain.Name),
                    LetterDefOf.ThreatBig,
                    targets.Count > 0
                        ? new LookTargets(targets)
                        : new LookTargets(map.Center, map));

                if (!excludeAllianceRuptureObservation)
                {
                    TryObserveMajorAllianceFailure(
                        map,
                        state.primaryDomain,
                        state.alliedDomain,
                        primaryPawns,
                        alliedPawns);
                }
            }
            else
            {
                Messages.Message(
                    "GR_GoauldSharedAllianceReprisal_AttackFailed"
                        .Translate(),
                    MessageTypeDefOf.NeutralEvent,
                    historical: true);
            }

            GR_Log.Message(
                "Resolved shared Goa'uld alliance reprisal: "
                + $"primary={state.primaryDomain?.Name ?? "<missing>"}, "
                + $"ally={state.alliedDomain?.Name ?? "<missing>"}, "
                + $"success={succeeded}, map={map.uniqueID}, "
                + $"combined points={state.raidPoints:0}.");

            CompleteSharedState(state, currentTick);
            return succeeded;
        }

        private void ObserveSharedStorytellerBoundary(int currentTick)
        {
            bool active =
                GateRimStorytellerUtility.IsGateRimStorytellerActive;

            if (!active && sharedStorytellerWasActive)
            {
                sharedSuspensionStartTick = currentTick;
            }
            else if (!active && sharedSuspensionStartTick < 0)
            {
                sharedSuspensionStartTick = currentTick;
            }
            else if (active
                && !sharedStorytellerWasActive
                && sharedSuspensionStartTick >= 0)
            {
                int pausedTicks = Math.Max(
                    0,
                    currentTick - sharedSuspensionStartTick);

                foreach (GoauldSharedAllianceReprisalState state
                    in sharedAllianceStates.Where(candidate =>
                        candidate?.pending == true))
                {
                    state.reprisalTick += pausedTicks;
                }

                foreach (GoauldAllianceRuptureState state
                    in allianceRuptureStates.Where(candidate =>
                        candidate?.pending == true))
                {
                    state.ruptureTick += pausedTicks;
                }

                sharedSuspensionStartTick = -1;
            }

            sharedStorytellerWasActive = active;
        }

        private bool TryResolveEligibleAlliedDomain(
            Faction primaryDomain,
            out Faction alliedDomain)
        {
            alliedDomain = null;

            if (Math.Abs(
                    GoauldOpenConflictPressureUtility
                        .ResolveNaturalRaidPressureFactor(primaryDomain)
                    - GoauldOpenConflictPressureUtility
                        .AllianceNaturalRaidFactor) >= 0.001f)
            {
                return false;
            }

            List<Faction> candidates =
                GameComponent_GoauldInterDomainRelationTracker.Current
                    ?.Snapshot(includeInactive: false)
                    .Where(pair =>
                        pair?.relation ==
                            GoauldInterDomainRelation.Alliance
                        && (pair.firstDomain == primaryDomain
                            || pair.secondDomain == primaryDomain))
                    .Select(pair => pair.firstDomain == primaryDomain
                        ? pair.secondDomain
                        : pair.firstDomain)
                    .Where(faction =>
                        GoauldSystemLordFactionUtility
                            .IsSystemLordFaction(faction)
                        && !faction.defeated)
                    .Distinct()
                    .ToList()
                ?? new List<Faction>();

            if (candidates.Count == 0)
            {
                return false;
            }

            alliedDomain = candidates.RandomElement();
            return true;
        }

        private static bool AreDomainsStillAllied(
            Faction first,
            Faction second)
        {
            return first != null
                && second != null
                && GameComponent_GoauldInterDomainRelationTracker.Current
                    ?.Snapshot(includeInactive: false)
                    .Any(pair =>
                        pair?.relation ==
                            GoauldInterDomainRelation.Alliance
                        && ((pair.firstDomain == first
                                && pair.secondDomain == second)
                            || (pair.firstDomain == second
                                && pair.secondDomain == first))) == true;
        }

        private bool HasPendingSharedAllianceReprisal()
        {
            return sharedAllianceStates.Any(state =>
                state?.pending == true);
        }

        private bool PairIsCoolingDown(
            Faction first,
            Faction second,
            int currentTick)
        {
            GoauldSharedAllianceReprisalState state =
                FindSharedState(first, second);
            return state != null
                && currentTick < state.nextEligibleTick;
        }

        private GoauldSharedAllianceReprisalState FindOrCreateSharedState(
            Faction first,
            Faction second)
        {
            GoauldSharedAllianceReprisalState state =
                FindSharedState(first, second);

            if (state != null)
            {
                return state;
            }

            state = new GoauldSharedAllianceReprisalState
            {
                primaryDomain = first,
                alliedDomain = second
            };
            sharedAllianceStates.Add(state);
            return state;
        }

        private GoauldSharedAllianceReprisalState FindSharedState(
            Faction first,
            Faction second)
        {
            return sharedAllianceStates.FirstOrDefault(state =>
                state != null
                && ((state.primaryDomain == first
                        && state.alliedDomain == second)
                    || (state.primaryDomain == second
                        && state.alliedDomain == first)));
        }

        private static void CompleteSharedState(
            GoauldSharedAllianceReprisalState state,
            int currentTick)
        {
            state.pending = false;
            state.targetMapUniqueId = -1;
            state.reprisalTick = 0;
            state.nextEligibleTick = currentTick
                + SharedPairCooldownTicks;
            state.raidPoints = 0f;
            state.debugShortDelay = false;
            state.debugForcedExecution = false;
        }

        private void TickAllianceFailureObservations()
        {
            for (int index = allianceFailureObservations.Count - 1;
                index >= 0;
                index--)
            {
                GoauldAllianceMajorFailureObservation observation =
                    allianceFailureObservations[index];

                if (!IsValidAllianceFailureObservation(observation))
                {
                    allianceFailureObservations.RemoveAt(index);
                    continue;
                }

                Map map = FindMap(observation.targetMapUniqueId);

                if (map == null
                    || !map.IsPlayerHome
                    || !AreDomainsStillAllied(
                        observation.primaryDomain,
                        observation.alliedDomain))
                {
                    allianceFailureObservations.RemoveAt(index);
                    continue;
                }

                int activeCount = ActiveAllianceFailurePawnCount(
                    observation,
                    map);
                float threshold = observation.initialPawnCount
                    * AllianceRuptureFailureRemainingFraction;

                if (activeCount > threshold)
                {
                    continue;
                }

                allianceFailureObservations.RemoveAt(index);

                if (!HasPendingAllianceRupture())
                {
                    TryScheduleAllianceRupture(
                        observation.targetMapUniqueId,
                        observation.primaryDomain,
                        observation.alliedDomain,
                        observation.initialPawnCount,
                        activeCount,
                        debugShortDelay: false);
                }
            }
        }

        private void TickAllianceRuptures(int currentTick)
        {
            foreach (GoauldAllianceRuptureState state
                in allianceRuptureStates.Where(candidate =>
                    candidate?.pending == true).ToList())
            {
                if (!AreDomainsActive(
                        state.primaryDomain,
                        state.alliedDomain))
                {
                    CancelAllianceRupture(
                        state,
                        currentTick,
                        GoauldAllianceRuptureOutcome
                            .CancelledInactiveDomain);
                    continue;
                }

                if (!AreDomainsStillAllied(
                        state.primaryDomain,
                        state.alliedDomain))
                {
                    CancelAllianceRupture(
                        state,
                        currentTick,
                        GoauldAllianceRuptureOutcome
                            .CancelledNoLongerAllied);
                    continue;
                }

                if (currentTick >= state.ruptureTick)
                {
                    TryResolveAllianceRupture(state, currentTick);
                }
            }
        }

        private bool TryObserveMajorAllianceFailure(
            Map map,
            Faction primaryDomain,
            Faction alliedDomain,
            List<Pawn> primaryPawns,
            List<Pawn> alliedPawns)
        {
            List<Pawn> validPrimaryPawns = FilterTrackedPawns(
                primaryPawns,
                map,
                primaryDomain);
            List<Pawn> validAlliedPawns = FilterTrackedPawns(
                alliedPawns,
                map,
                alliedDomain);
            int initialPawnCount = validPrimaryPawns.Count
                + validAlliedPawns.Count;

            if (!GateRimStorytellerUtility.IsGateRimStorytellerActive
                || map == null
                || !map.IsPlayerHome
                || initialPawnCount <
                    AllianceRuptureMinimumInitialPawnCount
                || HasPendingAllianceRupture()
                || !AreDomainsStillAllied(primaryDomain, alliedDomain))
            {
                return false;
            }

            allianceFailureObservations.RemoveAll(candidate =>
                candidate?.targetMapUniqueId == map.uniqueID
                && ((candidate.primaryDomain == primaryDomain
                        && candidate.alliedDomain == alliedDomain)
                    || (candidate.primaryDomain == alliedDomain
                        && candidate.alliedDomain == primaryDomain)));
            allianceFailureObservations.Add(
                new GoauldAllianceMajorFailureObservation
                {
                    targetMapUniqueId = map.uniqueID,
                    primaryDomain = primaryDomain,
                    alliedDomain = alliedDomain,
                    primaryPawns = validPrimaryPawns,
                    alliedPawns = validAlliedPawns,
                    initialPawnCount = initialPawnCount
                });

            GR_Log.Message(
                "Observing a natural shared Goa'uld reprisal for a major "
                + "alliance failure: "
                + $"primary={primaryDomain.Name} ({primaryDomain.loadID}), "
                + $"ally={alliedDomain.Name} ({alliedDomain.loadID}), "
                + $"map={map.uniqueID}, combined pawns={initialPawnCount}.");
            return true;
        }

        private bool TryScheduleAllianceRupture(
            int targetMapUniqueId,
            Faction primaryDomain,
            Faction alliedDomain,
            int initialPawnCount,
            int activePawnCountAtFailure,
            bool debugShortDelay)
        {
            int currentTick = CurrentTick();

            if (!GateRimStorytellerUtility.IsGateRimStorytellerActive
                || primaryDomain == null
                || alliedDomain == null
                || initialPawnCount <
                    AllianceRuptureMinimumInitialPawnCount
                || HasPendingAllianceRupture()
                || !AreDomainsStillAllied(primaryDomain, alliedDomain))
            {
                return false;
            }

            GoauldAllianceRuptureState state =
                FindOrCreateAllianceRuptureState(
                    primaryDomain,
                    alliedDomain);
            int delay = debugShortDelay
                ? AllianceRuptureDebugDelayTicks
                : Rand.RangeInclusive(
                    AllianceRuptureMinimumDelayTicks,
                    AllianceRuptureMaximumDelayTicks);

            state.primaryDomain = primaryDomain;
            state.alliedDomain = alliedDomain;
            state.targetMapUniqueId = targetMapUniqueId;
            state.failureTick = currentTick;
            state.ruptureTick = currentTick + delay;
            state.resolutionTick = 0;
            state.initialPawnCount = initialPawnCount;
            state.activePawnCountAtFailure = Math.Max(
                0,
                activePawnCountAtFailure);
            state.pending = true;
            state.debugShortDelay = debugShortDelay;
            state.outcome = GoauldAllianceRuptureOutcome.Pending;

            GR_Log.Message(
                "Scheduled Goa'uld alliance rupture after a major shared "
                + "reprisal failure: "
                + $"primary={primaryDomain.Name} ({primaryDomain.loadID}), "
                + $"ally={alliedDomain.Name} ({alliedDomain.loadID}), "
                + $"failure={state.activePawnCountAtFailure}/"
                + $"{state.initialPawnCount}, delay={delay}.");
            return true;
        }

        private bool TryResolveAllianceRupture(
            GoauldAllianceRuptureState state,
            int currentTick)
        {
            if (state?.pending != true
                || !GateRimStorytellerUtility.IsGateRimStorytellerActive)
            {
                return false;
            }

            if (!AreDomainsActive(
                    state.primaryDomain,
                    state.alliedDomain))
            {
                CancelAllianceRupture(
                    state,
                    currentTick,
                    GoauldAllianceRuptureOutcome.CancelledInactiveDomain);
                return false;
            }

            if (!AreDomainsStillAllied(
                    state.primaryDomain,
                    state.alliedDomain))
            {
                CancelAllianceRupture(
                    state,
                    currentTick,
                    GoauldAllianceRuptureOutcome
                        .CancelledNoLongerAllied);
                return false;
            }

            GameComponent_GoauldInterDomainRelationTracker relationTracker =
                GameComponent_GoauldInterDomainRelationTracker.Current;
            bool changed = relationTracker
                ?.TryBreakAllianceAfterMajorFailure(
                    state.primaryDomain,
                    state.alliedDomain,
                    state.debugShortDelay) == true;

            if (!changed)
            {
                CancelAllianceRupture(
                    state,
                    currentTick,
                    GoauldAllianceRuptureOutcome
                        .CancelledTransitionFailed);
                return false;
            }

            string reportKey = SelectAllianceRuptureReportKey();
            Find.LetterStack?.ReceiveLetter(
                "GR_GoauldAllianceRupture_Label".Translate(),
                reportKey.Translate(
                    state.primaryDomain.Name,
                    state.alliedDomain.Name),
                LetterDefOf.NeutralEvent,
                (LookTargets)null);

            state.pending = false;
            state.resolutionTick = currentTick;
            state.outcome = GoauldAllianceRuptureOutcome.Completed;

            GR_Log.Message(
                "Resolved Goa'uld alliance rupture after major failure: "
                + $"{state.primaryDomain.Name} ({state.primaryDomain.loadID}) "
                + $"and {state.alliedDomain.Name} "
                + $"({state.alliedDomain.loadID}) changed from Alliance "
                + "to Rivalry.");
            return true;
        }

        private void CancelAllianceRupture(
            GoauldAllianceRuptureState state,
            int currentTick,
            GoauldAllianceRuptureOutcome outcome)
        {
            if (state == null)
            {
                return;
            }

            state.pending = false;
            state.resolutionTick = currentTick;
            state.outcome = outcome;

            GR_Log.Message(
                "Cancelled pending Goa'uld alliance rupture: "
                + $"primary={state.primaryDomain?.Name ?? "<missing>"}, "
                + $"ally={state.alliedDomain?.Name ?? "<missing>"}, "
                + $"reason={outcome}.");
        }

        private string SelectAllianceRuptureReportKey()
        {
            int variant = Rand.Range(0, AllianceRuptureVariantCount);
            string key = "GR_GoauldAllianceRupture_Text" + variant;

            if (key == lastAllianceRuptureReportKey
                && AllianceRuptureVariantCount > 1)
            {
                variant = (variant
                    + Rand.Range(1, AllianceRuptureVariantCount))
                    % AllianceRuptureVariantCount;
                key = "GR_GoauldAllianceRupture_Text" + variant;
            }

            lastAllianceRuptureReportKey = key;
            return key;
        }

        private bool HasPendingAllianceRupture()
        {
            return allianceRuptureStates.Any(state =>
                state?.pending == true);
        }

        private GoauldAllianceRuptureState
            FindOrCreateAllianceRuptureState(
                Faction first,
                Faction second)
        {
            GoauldAllianceRuptureState state = allianceRuptureStates
                .FirstOrDefault(candidate =>
                    candidate != null
                    && ((candidate.primaryDomain == first
                            && candidate.alliedDomain == second)
                        || (candidate.primaryDomain == second
                            && candidate.alliedDomain == first)));

            if (state != null)
            {
                return state;
            }

            state = new GoauldAllianceRuptureState
            {
                primaryDomain = first,
                alliedDomain = second
            };
            allianceRuptureStates.Add(state);
            return state;
        }

        private static int ActiveAllianceFailurePawnCount(
            GoauldAllianceMajorFailureObservation observation,
            Map map)
        {
            if (observation == null || map == null)
            {
                return 0;
            }

            return ActiveSharedRaidPawnCount(
                    observation.primaryPawns,
                    map,
                    observation.primaryDomain)
                + ActiveSharedRaidPawnCount(
                    observation.alliedPawns,
                    map,
                    observation.alliedDomain);
        }

        private static List<Pawn> FilterTrackedPawns(
            IEnumerable<Pawn> pawns,
            Map map,
            Faction faction)
        {
            return pawns?.Where(pawn =>
                    pawn != null
                    && pawn.Spawned
                    && pawn.Map == map
                    && pawn.Faction == faction)
                .Distinct()
                .ToList()
                ?? new List<Pawn>();
        }

        private static bool AreDomainsActive(
            Faction first,
            Faction second)
        {
            return first != null
                && second != null
                && !first.defeated
                && !second.defeated
                && GoauldSystemLordFactionUtility
                    .IsSystemLordFaction(first)
                && GoauldSystemLordFactionUtility
                    .IsSystemLordFaction(second);
        }

        private static int ActiveSharedRaidPawnCount(
            IEnumerable<Pawn> pawns,
            Map map,
            Faction faction)
        {
            return pawns?.Count(pawn =>
                pawn != null
                && !pawn.Dead
                && !pawn.Downed
                && pawn.Spawned
                && pawn.Map == map
                && pawn.Faction == faction)
                ?? 0;
        }

        private static bool IsValidSharedObservation(
            GoauldSharedAllianceRaidObservation observation)
        {
            return observation != null
                && observation.targetMapUniqueId >= 0
                && observation.primaryDomain != null
                && observation.alliedDomain != null
                && observation.primaryDomain != observation.alliedDomain
                && observation.initialPawnCount >=
                    SharedMinimumInitialPawnCount;
        }

        private static bool IsValidSharedState(
            GoauldSharedAllianceReprisalState state)
        {
            return state != null
                && state.primaryDomain != null
                && state.alliedDomain != null
                && state.primaryDomain != state.alliedDomain;
        }

        private static bool IsValidAllianceFailureObservation(
            GoauldAllianceMajorFailureObservation observation)
        {
            return observation != null
                && observation.targetMapUniqueId >= 0
                && observation.primaryDomain != null
                && observation.alliedDomain != null
                && observation.primaryDomain != observation.alliedDomain
                && observation.initialPawnCount >=
                    AllianceRuptureMinimumInitialPawnCount;
        }

        private static bool IsValidAllianceRuptureState(
            GoauldAllianceRuptureState state)
        {
            return state != null
                && state.primaryDomain != null
                && state.alliedDomain != null
                && state.primaryDomain != state.alliedDomain;
        }

        private static Map FindMap(int uniqueId)
        {
            return Find.Maps?.FirstOrDefault(map =>
                map?.uniqueID == uniqueId);
        }

        private static HashSet<string> PawnIdsForFaction(
            Map map,
            Faction faction)
        {
            return new HashSet<string>(
                map?.mapPawns?.AllPawnsSpawned
                    ?.Where(pawn => pawn?.Faction == faction)
                    .Select(pawn => pawn.ThingID)
                ?? Enumerable.Empty<string>());
        }

        private static List<Pawn> NewPawnsForFaction(
            Map map,
            Faction faction,
            HashSet<string> existingPawnIds)
        {
            return map?.mapPawns?.AllPawnsSpawned
                ?.Where(pawn =>
                    pawn?.Faction == faction
                    && !existingPawnIds.Contains(pawn.ThingID))
                .ToList()
                ?? new List<Pawn>();
        }

        private bool TryBeginUltimatum(
            Map map,
            Faction domainFaction,
            string formerHostLabel,
            string symbioteName,
            Pawn demandedSymbiote,
            bool debugShortDelay,
            bool debugGeneratedSymbiote)
        {
            if (map == null
                || domainFaction == null
                || Find.TickManager == null
                || !GoauldSystemLordFactionUtility.IsSystemLordFaction(
                    domainFaction))
            {
                return false;
            }

            int currentTick = CurrentTick();
            GoauldDomainReprisalState state = FindOrCreateState(domainFaction);

            if (state.ultimatumPending
                || state.pending
                || currentTick < state.nextEligibleTick)
            {
                return false;
            }

            state.targetMapUniqueId = map.uniqueID;
            state.ultimatumExpiryTick = currentTick + UltimatumDurationTicks;
            state.raidPoints = Math.Max(
                1f,
                StorytellerUtility.DefaultThreatPointsNow(map));
            state.formerHostLabel = formerHostLabel ?? string.Empty;
            state.symbioteName = symbioteName ?? string.Empty;
            state.demandedSymbiote = demandedSymbiote;
            state.debugGeneratedSymbiote = debugGeneratedSymbiote;
            state.debugShortDelay = debugShortDelay;
            state.ultimatumPending = true;
            state.pending = false;
            state.reprisalTick = 0;

            MaintainUltimatumSedation(state);

            if (!CreateUltimatumLetter(state))
            {
                DestroyDebugSymbiote(state);
                ClearState(state, nextEligibleTick: 0);
                return false;
            }

            GR_Log.Message(
                $"Started Goa'uld extraction ultimatum from "
                + $"{domainFaction.Name} ({domainFaction.loadID}) against "
                + $"map {map.uniqueID}: symbiote="
                + $"{demandedSymbiote?.ThingID ?? "<missing>"}, "
                + $"points={state.raidPoints:0}.");

            return true;
        }

        private bool TryScheduleFatalExtractionReprisal(
            Map map,
            Faction domainFaction,
            string formerHostLabel,
            string symbioteName,
            bool debugShortDelay)
        {
            if (map == null
                || domainFaction == null
                || Find.TickManager == null
                || !GoauldSystemLordFactionUtility.IsSystemLordFaction(
                    domainFaction))
            {
                return false;
            }

            int currentTick = CurrentTick();
            GoauldDomainReprisalState state = FindOrCreateState(domainFaction);

            if (state.ultimatumPending
                || state.pending
                || currentTick < state.nextEligibleTick)
            {
                return false;
            }

            int delay = debugShortDelay
                ? DebugDelayTicks
                : Rand.RangeInclusive(MinimumDelayTicks, MaximumDelayTicks);

            state.targetMapUniqueId = map.uniqueID;
            state.reprisalTick = currentTick + delay;
            state.raidPoints = Math.Max(
                1f,
                StorytellerUtility.DefaultThreatPointsNow(map));
            state.formerHostLabel = formerHostLabel ?? string.Empty;
            state.symbioteName = symbioteName ?? string.Empty;
            state.pending = true;

            Find.LetterStack?.ReceiveLetter(
                "GR_GoauldDomainFatalExtraction_Label".Translate(),
                "GR_GoauldDomainFatalExtraction_Text".Translate(
                    domainFaction.Name,
                    state.symbioteName,
                    state.formerHostLabel,
                    delay.ToStringTicksToPeriod()),
                LetterDefOf.ThreatBig,
                new LookTargets(map.Center, map));

            GR_Log.Message(
                $"Scheduled fatal-extraction reprisal from "
                + $"{domainFaction.Name} ({domainFaction.loadID}) against "
                + $"map {map.uniqueID}: delay={delay}, "
                + $"points={state.raidPoints:0}.");

            return true;
        }

        private bool TrySurrender(Faction domainFaction)
        {
            if (!CanSurrenderDemandedSymbiote(
                    domainFaction,
                    out string disabledReason))
            {
                Messages.Message(
                    disabledReason,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            GoauldDomainReprisalState state = FindState(domainFaction);
            Pawn symbiote = state.demandedSymbiote;
            string symbioteName = state.symbioteName;

            RemoveUltimatumLetters(domainFaction);
            symbiote.Destroy(DestroyMode.Vanish);
            CompleteState(state, CurrentTick());

            Messages.Message(
                "GR_GoauldDomainUltimatum_Surrendered".Translate(
                    symbioteName,
                    domainFaction.Name),
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            GR_Log.Message(
                $"Surrendered extracted symbiote {symbiote.ThingID} to "
                + $"Goa'uld domain {domainFaction.Name} "
                + $"({domainFaction.loadID}); reprisal cancelled.");

            return true;
        }

        private bool TryDefy(Faction domainFaction)
        {
            GoauldDomainReprisalState state = FindState(domainFaction);

            if (state?.ultimatumPending != true)
            {
                return false;
            }

            ScheduleReprisal(
                state,
                CurrentTick(),
                UltimatumResolution.Defied);
            return true;
        }

        private void ScheduleReprisal(
            GoauldDomainReprisalState state,
            int currentTick,
            UltimatumResolution resolution)
        {
            int delay = state.debugShortDelay
                ? DebugDelayTicks
                : Rand.RangeInclusive(MinimumDelayTicks, MaximumDelayTicks);
            Map map = FindTargetMap(state);

            RemoveUltimatumLetters(state.domainFaction);
            DestroyDebugSymbiote(state);

            state.demandedSymbiote = null;
            state.debugGeneratedSymbiote = false;
            state.debugShortDelay = false;
            state.ultimatumPending = false;
            state.ultimatumExpiryTick = 0;
            state.pending = true;
            state.reprisalTick = currentTick + delay;

            string textKey;

            switch (resolution)
            {
                case UltimatumResolution.Expired:
                    textKey = "GR_GoauldDomainReprisal_ExpiredText";
                    break;
                case UltimatumResolution.SymbioteDestroyed:
                    textKey = "GR_GoauldDomainReprisal_SymbioteDestroyedText";
                    break;
                default:
                    textKey = "GR_GoauldDomainReprisal_DefiedText";
                    break;
            }

            Find.LetterStack?.ReceiveLetter(
                "GR_GoauldDomainReprisal_WarningLabel".Translate(),
                textKey.Translate(
                    state.domainFaction.Name,
                    state.symbioteName,
                    state.formerHostLabel,
                    delay.ToStringTicksToPeriod()),
                LetterDefOf.ThreatBig,
                map == null
                    ? null
                    : new LookTargets(map.Center, map));

            GR_Log.Message(
                $"Scheduled Goa'uld domain reprisal from "
                + $"{state.domainFaction.Name} ({state.domainFaction.loadID}) "
                + $"after extraction ultimatum: resolution={resolution}, "
                + $"delay={delay}, points={state.raidPoints:0}.");
        }

        private static bool DemandedSymbioteWasDestroyed(
            GoauldDomainReprisalState state)
        {
            Pawn symbiote = state?.demandedSymbiote;
            return symbiote == null || symbiote.Destroyed || symbiote.Dead;
        }

        private static void MaintainUltimatumSedation(
            GoauldDomainReprisalState state)
        {
            Pawn symbiote = state?.demandedSymbiote;

            if (symbiote?.health == null
                || symbiote.Destroyed
                || symbiote.Dead
                || !symbiote.Spawned)
            {
                return;
            }

            Hediff anesthetic = symbiote.health.hediffSet
                .GetFirstHediffOfDef(HediffDefOf.Anesthetic);

            if (anesthetic == null)
            {
                anesthetic = HediffMaker.MakeHediff(
                    HediffDefOf.Anesthetic,
                    symbiote);
                symbiote.health.AddHediff(anesthetic);
            }

            anesthetic.Severity = Math.Max(anesthetic.Severity, 1f);
            symbiote.jobs?.StopAll();
        }

        private bool CreateUltimatumLetter(
            GoauldDomainReprisalState state)
        {
            LetterDef letterDef = DefDatabase<LetterDef>.GetNamedSilentFail(
                UltimatumLetterDefName);

            if (letterDef == null
                || !typeof(ChoiceLetter_GoauldExtractionUltimatum)
                    .IsAssignableFrom(letterDef.letterClass))
            {
                GR_Log.Error(
                    "Cannot create Goa'uld extraction ultimatum: "
                    + UltimatumLetterDefName
                    + " is missing or uses the wrong letter class.");
                return false;
            }

            ChoiceLetter_GoauldExtractionUltimatum letter
                = LetterMaker.MakeLetter(
                    "GR_GoauldDomainUltimatum_Label".Translate(),
                    "GR_GoauldDomainUltimatum_Text".Translate(
                        state.domainFaction.Name,
                        state.symbioteName,
                        state.formerHostLabel),
                    letterDef)
                as ChoiceLetter_GoauldExtractionUltimatum;

            if (letter == null)
            {
                GR_Log.Error(
                    "Failed to instantiate the Goa'uld extraction "
                    + "ultimatum letter.");
                return false;
            }

            letter.Initialize(state.domainFaction);
            letter.StartTimeout(UltimatumDurationTicks);
            Find.LetterStack.ReceiveLetter(letter);
            return true;
        }

        private static void RemoveUltimatumLetters(Faction domainFaction)
        {
            if (Find.LetterStack == null || domainFaction == null)
            {
                return;
            }

            List<Letter> letters = Find.LetterStack.LettersListForReading
                .OfType<ChoiceLetter_GoauldExtractionUltimatum>()
                .Where(letter => letter.DomainFaction == domainFaction)
                .Cast<Letter>()
                .ToList();

            foreach (Letter letter in letters)
            {
                Find.LetterStack.RemoveLetter(letter);
            }
        }

        private GoauldDomainReprisalState FindState(Faction faction)
        {
            return states.FirstOrDefault(
                candidate => candidate?.domainFaction == faction);
        }

        private Map FindTargetMap(GoauldDomainReprisalState state)
        {
            return Find.Maps?.FirstOrDefault(
                candidate => candidate?.uniqueID == state.targetMapUniqueId);
        }

        private static void DestroyDebugSymbiote(
            GoauldDomainReprisalState state)
        {
            Pawn symbiote = state?.demandedSymbiote;

            if (state?.debugGeneratedSymbiote == true
                && symbiote != null
                && !symbiote.Destroyed)
            {
                symbiote.Destroy(DestroyMode.Vanish);
            }
        }

        private static void SedateDebugSymbiote(Pawn symbiote)
        {
            if (symbiote?.health == null)
            {
                return;
            }

            Hediff anesthetic = HediffMaker.MakeHediff(
                HediffDefOf.Anesthetic,
                symbiote);
            anesthetic.Severity = 1f;
            symbiote.health.AddHediff(anesthetic);
            symbiote.jobs?.StopAll();
        }

        private bool TryResolveReprisal(
            GoauldDomainReprisalState state,
            int currentTick)
        {
            Map map = Find.Maps?.FirstOrDefault(
                candidate => candidate?.uniqueID == state.targetMapUniqueId);

            if (map == null || !map.IsPlayerHome)
            {
                CompleteState(state, currentTick);
                return false;
            }

            if (!GoauldSystemLordFactionUtility.IsSystemLordFaction(
                    state.domainFaction))
            {
                CompleteState(state, currentTick);
                return false;
            }

            IncidentDef incident = GR_DefOf.SG1_GoauldJaffaNaturalRaid;

            if (incident?.Worker == null)
            {
                GR_Log.Error(
                    "Cannot start Goa'uld domain reprisal: natural raid "
                    + "incident is unavailable.");
                CompleteState(state, currentTick);
                return false;
            }

            IncidentParms parms = new IncidentParms
            {
                target = map,
                faction = state.domainFaction,
                forced = true,
                points = Math.Max(1f, state.raidPoints)
            };

            bool succeeded = incident.Worker.TryExecute(parms);

            if (!succeeded)
            {
                Messages.Message(
                    "GR_GoauldDomainReprisal_AttackFailed".Translate(),
                    MessageTypeDefOf.NeutralEvent,
                    historical: true);
            }

            GR_Log.Message(
                $"Resolved Goa'uld domain reprisal from "
                + $"{state.domainFaction?.Name ?? "<missing>"}: "
                + $"success={succeeded}, map={map.uniqueID}, "
                + $"points={state.raidPoints:0}.");

            CompleteState(state, currentTick);
            return succeeded;
        }

        private GoauldDomainReprisalState FindOrCreateState(Faction faction)
        {
            GoauldDomainReprisalState state = states.FirstOrDefault(
                candidate => candidate?.domainFaction == faction);

            if (state != null)
            {
                return state;
            }

            state = new GoauldDomainReprisalState
            {
                domainFaction = faction
            };
            states.Add(state);
            return state;
        }

        private static void CompleteState(
            GoauldDomainReprisalState state,
            int currentTick)
        {
            ClearState(state, currentTick + CooldownTicks);
        }

        private static void ClearState(
            GoauldDomainReprisalState state,
            int nextEligibleTick)
        {
            state.ultimatumPending = false;
            state.pending = false;
            state.targetMapUniqueId = -1;
            state.ultimatumExpiryTick = 0;
            state.reprisalTick = 0;
            state.nextEligibleTick = nextEligibleTick;
            state.raidPoints = 0f;
            state.formerHostLabel = string.Empty;
            state.symbioteName = string.Empty;
            state.demandedSymbiote = null;
            state.debugGeneratedSymbiote = false;
            state.debugShortDelay = false;
        }

        private static int CurrentTick()
        {
            return Find.TickManager?.TicksGame ?? 0;
        }
    }

    internal enum UltimatumResolution
    {
        Defied,
        Expired,
        SymbioteDestroyed
    }
}
