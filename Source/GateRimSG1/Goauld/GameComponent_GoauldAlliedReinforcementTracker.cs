using System;
using System.Collections.Generic;
using System.Linq;
using GateRimSG1.Storytelling;
using RimWorld;
using Verse;
using Verse.AI;
using Verse.AI.Group;

namespace GateRimSG1.Goauld
{
    public enum GoauldAllianceRaidOutcome
    {
        Standard,
        DelayedReinforcement,
        JointRaid
    }

    /// <summary>
    /// Stores the silent delay between an eligible natural raid and one
    /// allied-domain reinforcement wave. The wave consumes part of the raid's
    /// existing point budget and is announced only when it reaches the map.
    /// </summary>
    public sealed class GameComponent_GoauldAlliedReinforcementTracker
        : GameComponent
    {
        public const float AlliedBudgetFraction = 0.25f;
        public const float CooperativeRaidChance = 0.50f;
        public const float JointRaidChanceWithinCooperation = 0.50f;
        public const float JointPrimaryBudgetFraction = 0.60f;
        public const float JointAlliedBudgetFraction = 0.40f;
        public const float MinimumCombinedRaidPoints = 800f;
        public const int MinimumArrivalDelayTicks = 1800;
        public const int MaximumArrivalDelayTicks = 3600;
        public const int DebugArrivalDelayTicks = 600;

        private const int CheckIntervalTicks = 30;
        private const int RetryDelayTicks = 250;
        private const int MaximumSpawnAttempts = 3;
        private const int MaximumCooperationTicks = 120000;

        private List<GoauldAlliedReinforcementState> states =
            new List<GoauldAlliedReinforcementState>();
        private int nextCheckTick;

        public GameComponent_GoauldAlliedReinforcementTracker(Game game)
        {
        }

        public static GameComponent_GoauldAlliedReinforcementTracker Current
            => Verse.Current.Game
                ?.GetComponent<
                    GameComponent_GoauldAlliedReinforcementTracker>();

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(
                ref states,
                "goauldAlliedReinforcementStates",
                LookMode.Deep);
            Scribe_Values.Look(
                ref nextCheckTick,
                "goauldAlliedReinforcementNextCheckTick",
                0);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                states = states
                    ?.Where(IsValidState)
                    .ToList()
                    ?? new List<GoauldAlliedReinforcementState>();
            }
        }

        public override void FinalizeInit()
        {
            base.FinalizeInit();

            foreach (GoauldAlliedReinforcementState state in states)
            {
                if (state?.arrived == true)
                {
                    NotifyCooperationChanged(state);
                }
            }
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

            for (int index = states.Count - 1; index >= 0; index--)
            {
                GoauldAlliedReinforcementState state = states[index];

                if (!IsValidState(state))
                {
                    RemoveStateAt(index, state);
                    continue;
                }

                if (!state.arrived)
                {
                    if (currentTick >= state.arrivalTick)
                    {
                        TryResolveArrival(state, currentTick, index);
                    }

                    continue;
                }

                TickActiveCooperation(state, currentTick, index);
            }
        }

        public static bool TryResolvePlan(
            Map map,
            Faction primaryDomain,
            float combinedPoints,
            GoauldJaffaRaidDoctrine doctrine,
            GoauldAllianceRaidOutcome? forcedOutcome,
            out GoauldAllianceRaidOutcome outcome,
            out Faction alliedDomain,
            out float primaryPoints,
            out float alliedPoints)
        {
            outcome = GoauldAllianceRaidOutcome.Standard;
            alliedDomain = null;
            primaryPoints = combinedPoints;
            alliedPoints = 0f;

            if (Current == null
                || map == null
                || Current.states.Any(state =>
                    state?.targetMapUniqueId == map.uniqueID)
                || !GateRimStorytellerUtility.IsGateRimStorytellerActive
                || !GoauldSystemLordFactionUtility.IsSystemLordFaction(
                    primaryDomain)
                || combinedPoints < MinimumCombinedRaidPoints
                || Math.Abs(
                    GoauldOpenConflictPressureUtility
                        .ResolveNaturalRaidPressureFactor(primaryDomain)
                    - GoauldOpenConflictPressureUtility
                        .AllianceNaturalRaidFactor) > 0.001f)
            {
                return false;
            }

            List<Faction> candidates = GetAlliedDomains(primaryDomain);

            if (candidates.Count == 0)
            {
                return false;
            }

            outcome = forcedOutcome ?? SelectOutcome(doctrine);
            alliedDomain = candidates.RandomElement();

            if (outcome == GoauldAllianceRaidOutcome.Standard)
            {
                return true;
            }

            if (outcome == GoauldAllianceRaidOutcome.JointRaid
                && doctrine != GoauldJaffaRaidDoctrine.Direct)
            {
                outcome = GoauldAllianceRaidOutcome.DelayedReinforcement;
            }

            if (outcome == GoauldAllianceRaidOutcome.JointRaid)
            {
                primaryPoints = Math.Max(
                    1f,
                    combinedPoints * JointPrimaryBudgetFraction);
                alliedPoints = Math.Max(
                    1f,
                    combinedPoints * JointAlliedBudgetFraction);
            }
            else
            {
                alliedPoints = Math.Max(
                    1f,
                    combinedPoints * AlliedBudgetFraction);
                primaryPoints = Math.Max(
                    1f,
                    combinedPoints - alliedPoints);
            }

            return true;
        }

        private static GoauldAllianceRaidOutcome SelectOutcome(
            GoauldJaffaRaidDoctrine doctrine)
        {
            if (Rand.Value >= CooperativeRaidChance)
            {
                return GoauldAllianceRaidOutcome.Standard;
            }

            if (doctrine == GoauldJaffaRaidDoctrine.Direct
                && Rand.Value < JointRaidChanceWithinCooperation)
            {
                return GoauldAllianceRaidOutcome.JointRaid;
            }

            return GoauldAllianceRaidOutcome.DelayedReinforcement;
        }

        public static bool TryGetFirstAlliancePair(
            out Faction primaryDomain,
            out Faction alliedDomain)
        {
            primaryDomain = null;
            alliedDomain = null;
            GameComponent_GoauldInterDomainRelationTracker tracker =
                GameComponent_GoauldInterDomainRelationTracker.Current;
            GoauldInterDomainRelationState pair = tracker
                ?.Snapshot(includeInactive: false)
                .Where(state =>
                    state?.relation == GoauldInterDomainRelation.Alliance
                    && state.firstDomain != null
                    && state.secondDomain != null
                    && Math.Abs(
                        GoauldOpenConflictPressureUtility
                            .ResolveNaturalRaidPressureFactor(
                                state.firstDomain)
                        - GoauldOpenConflictPressureUtility
                            .AllianceNaturalRaidFactor) < 0.001f)
                .OrderBy(state => state.firstDomain.loadID)
                .ThenBy(state => state.secondDomain.loadID)
                .FirstOrDefault();

            if (pair == null)
            {
                return false;
            }

            primaryDomain = pair.firstDomain;
            alliedDomain = pair.secondDomain;
            return true;
        }

        public static bool HasPendingOrActive(Map map)
        {
            return map != null
                && Current?.states.Any(state =>
                    state?.targetMapUniqueId == map.uniqueID) == true;
        }

        public bool TrySchedule(
            Map map,
            Faction primaryDomain,
            Faction alliedDomain,
            float alliedPoints,
            List<Pawn> primaryPawns,
            bool debugShortDelay)
        {
            if (map == null
                || primaryDomain == null
                || alliedDomain == null
                || primaryDomain == alliedDomain
                || !(alliedPoints > 0f)
                || states.Any(state =>
                    state?.targetMapUniqueId == map.uniqueID))
            {
                return false;
            }

            int delay = debugShortDelay
                ? DebugArrivalDelayTicks
                : Rand.RangeInclusive(
                    MinimumArrivalDelayTicks,
                    MaximumArrivalDelayTicks);
            GoauldAlliedReinforcementState newState =
                new GoauldAlliedReinforcementState
                {
                    targetMapUniqueId = map.uniqueID,
                    primaryDomain = primaryDomain,
                    alliedDomain = alliedDomain,
                    alliedPoints = alliedPoints,
                    arrivalTick = CurrentTick() + delay,
                    manifestation = GoauldAlliedRaidManifestation
                        .DelayedReinforcement,
                    primaryPawns = primaryPawns
                        ?.Where(pawn => pawn != null)
                        .ToList()
                        ?? new List<Pawn>()
                };
            newState.initialPrimaryPawnCount =
                newState.primaryPawns.Count;
            states.Add(newState);

            GR_Log.Message(
                "Silently scheduled allied Goa'uld raid reinforcements: "
                + $"primary={primaryDomain.Name} ({primaryDomain.loadID}), "
                + $"ally={alliedDomain.Name} ({alliedDomain.loadID}), "
                + $"map={map.uniqueID}, points={alliedPoints:0}, "
                + $"delay={delay} ticks.");
            return true;
        }

        public bool TryStartJointRaid(
            Map map,
            Faction primaryDomain,
            Faction alliedDomain,
            float alliedPoints,
            List<Pawn> primaryPawns,
            IntVec3 alliedSpawnCenter)
        {
            if (map == null
                || primaryDomain == null
                || alliedDomain == null
                || primaryDomain == alliedDomain
                || !(alliedPoints > 0f)
                || states.Any(state =>
                    state?.targetMapUniqueId == map.uniqueID))
            {
                return false;
            }

            GoauldAlliedReinforcementState newState =
                new GoauldAlliedReinforcementState
                {
                    targetMapUniqueId = map.uniqueID,
                    primaryDomain = primaryDomain,
                    alliedDomain = alliedDomain,
                    alliedPoints = alliedPoints,
                    arrivalTick = CurrentTick(),
                    manifestation =
                        GoauldAlliedRaidManifestation.JointRaid,
                    alliedSpawnCenter = alliedSpawnCenter,
                    primaryPawns = primaryPawns
                        ?.Where(pawn => pawn != null)
                        .ToList()
                        ?? new List<Pawn>()
                };
            newState.initialPrimaryPawnCount =
                newState.primaryPawns.Count;
            states.Add(newState);
            int stateIndex = states.Count - 1;
            TryResolveArrival(newState, CurrentTick(), stateIndex);

            if (!newState.arrived)
            {
                GR_Log.Warning(
                    "The allied detachment of a joint Goa'uld raid could "
                    + "not enter simultaneously; a bounded retry remains "
                    + "scheduled.");
            }

            return states.Contains(newState);
        }

        public static bool AreTemporarilyCooperating(
            Faction first,
            Faction second)
        {
            if (first == null || second == null || first == second)
            {
                return false;
            }

            return Current?.states.Any(state =>
                state?.arrived == true
                && ((state.primaryDomain == first
                        && state.alliedDomain == second)
                    || (state.primaryDomain == second
                        && state.alliedDomain == first))) == true;
        }

        public string BuildDebugReport()
        {
            if (states.Count == 0)
            {
                return "No allied Goa'uld reinforcement is pending or active.";
            }

            int currentTick = CurrentTick();
            return string.Join(
                "\n",
                states.Select(state =>
                    $"{state.primaryDomain?.Name ?? "<missing>"} + "
                    + $"{state.alliedDomain?.Name ?? "<missing>"}: "
                    + $"mode={state.manifestation}, "
                    + (state.arrived
                        ? $"active, expires in {Math.Max(0, state.cooperationExpiryTick - currentTick)} ticks, "
                            + $"primary pawns={ActivePawnCount(state.primaryPawns)}, "
                            + $"allied pawns={ActivePawnCount(state.alliedPawns)}"
                        : $"arrives in {Math.Max(0, state.arrivalTick - currentTick)} ticks, "
                            + $"{state.alliedPoints:0} points")));
        }

        public bool OrderFirstJointPrimaryWithdrawalDebug()
        {
            GoauldAlliedReinforcementState state = states.FirstOrDefault(
                candidate => candidate?.arrived == true
                    && candidate.manifestation
                        == GoauldAlliedRaidManifestation.JointRaid
                    && HasSpawnedParticipant(candidate.primaryPawns)
                    && HasSpawnedParticipant(candidate.alliedPawns));
            Map map = ActivePawns(state?.primaryPawns)
                .FirstOrDefault()?.Map;

            if (state == null || map == null)
            {
                return false;
            }

            OrderFactionWithdrawal(
                state.primaryDomain,
                state.primaryPawns,
                map);
            return true;
        }

        private static List<Faction> GetAlliedDomains(
            Faction primaryDomain)
        {
            GameComponent_GoauldInterDomainRelationTracker tracker =
                GameComponent_GoauldInterDomainRelationTracker.Current;

            if (tracker == null)
            {
                return new List<Faction>();
            }

            return tracker.Snapshot(includeInactive: false)
                .Where(state =>
                    state?.relation == GoauldInterDomainRelation.Alliance
                    && (state.firstDomain == primaryDomain
                        || state.secondDomain == primaryDomain))
                .Select(state => state.firstDomain == primaryDomain
                    ? state.secondDomain
                    : state.firstDomain)
                .Where(faction =>
                    GoauldSystemLordFactionUtility
                        .IsSystemLordFaction(faction)
                    && !faction.defeated)
                .Distinct()
                .ToList();
        }

        private void TryResolveArrival(
            GoauldAlliedReinforcementState state,
            int currentTick,
            int stateIndex)
        {
            Map map = Find.Maps
                ?.FirstOrDefault(candidate =>
                    candidate?.uniqueID == state.targetMapUniqueId);

            if (map == null || !map.IsPlayerHome)
            {
                RemoveStateAt(stateIndex, state);
                return;
            }

            state.arrived = true;
            state.cooperationExpiryTick = currentTick
                + MaximumCooperationTicks;
            NotifyCooperationChanged(state);

            HashSet<string> existingPawnIds = PawnIdsForFaction(
                map,
                state.alliedDomain);
            IncidentDef incidentDef =
                GR_DefOf.SG1_GoauldJaffaControlledRaid;
            IncidentParms parms = StorytellerUtility.DefaultParmsNow(
                incidentDef.category,
                map);
            parms.faction = state.alliedDomain;
            parms.points = state.alliedPoints;
            parms.forced = true;
            parms.sendLetter = state.manifestation
                == GoauldAlliedRaidManifestation.DelayedReinforcement;

            if (parms.sendLetter)
            {
                parms.customLetterDef = LetterDefOf.ThreatBig;
                parms.customLetterLabel =
                    "GR_GoauldAlliedReinforcement_ArrivalLabel"
                        .Translate(state.alliedDomain.Name);
                parms.customLetterText =
                    "GR_GoauldAlliedReinforcement_ArrivalText"
                        .Translate(
                            state.primaryDomain.Name,
                            state.alliedDomain.Name);
            }

            if (state.alliedSpawnCenter.IsValid)
            {
                parms.spawnCenter = state.alliedSpawnCenter;
            }

            if (incidentDef?.Worker?.TryExecute(parms) == true)
            {
                state.alliedPawns = NewPawnsForFaction(
                    map,
                    state.alliedDomain,
                    existingPawnIds);
                state.initialAlliedPawnCount =
                    state.alliedPawns.Count;
                GR_Log.Message(
                    (state.manifestation
                            == GoauldAlliedRaidManifestation.JointRaid
                        ? "Joint Goa'uld allied detachment arrived: "
                        : "Allied Goa'uld raid reinforcements arrived: ")
                    + $"primary={state.primaryDomain.Name} "
                    + $"({state.primaryDomain.loadID}), "
                    + $"ally={state.alliedDomain.Name} "
                    + $"({state.alliedDomain.loadID}), "
                    + $"map={map.uniqueID}, points={state.alliedPoints:0}, "
                    + $"pawns={state.alliedPawns.Count}.");
                return;
            }

            state.arrived = false;
            state.cooperationExpiryTick = 0;
            NotifyCooperationChanged(state);
            state.spawnAttempts++;

            if (state.spawnAttempts >= MaximumSpawnAttempts)
            {
                GR_Log.Warning(
                    "Cancelled allied Goa'uld raid reinforcements after "
                    + "three failed arrival attempts.");
                RemoveStateAt(stateIndex, state);
                return;
            }

            state.arrivalTick = currentTick + RetryDelayTicks;
        }

        private void TickActiveCooperation(
            GoauldAlliedReinforcementState state,
            int currentTick,
            int stateIndex)
        {
            bool primaryPresent = HasSpawnedParticipant(
                state.primaryPawns);
            bool allyPresent = HasSpawnedParticipant(state.alliedPawns);

            if (!state.withdrawalOrdered)
            {
                if (state.manifestation
                    == GoauldAlliedRaidManifestation.JointRaid)
                {
                    bool eitherWithdrawing =
                        ForceIsWithdrawing(state.primaryPawns)
                        || ForceIsWithdrawing(state.alliedPawns);
                    bool eitherBroken = ForceIsBroken(
                            state.primaryPawns,
                            state.initialPrimaryPawnCount)
                        || ForceIsBroken(
                            state.alliedPawns,
                            state.initialAlliedPawnCount);

                    if (eitherWithdrawing
                        || eitherBroken
                        || !primaryPresent
                        || !allyPresent)
                    {
                        OrderJointWithdrawal(state);
                    }
                }
                else if (ForceIsWithdrawing(state.primaryPawns))
                {
                    OrderAlliedWithdrawal(state);
                }
            }

            if (!primaryPresent
                || !allyPresent
                || currentTick >= state.cooperationExpiryTick)
            {
                RemoveStateAt(stateIndex, state);
            }
        }

        private static bool ForceIsWithdrawing(
            List<Pawn> pawns)
        {
            List<Pawn> mobile = ActivePawns(pawns);

            return mobile.Count > 0
                && mobile.All(pawn =>
                    pawn.GetLord()?.LordJob is LordJob_ExitMapBest
                    || pawn.mindState?.duty?.def == DutyDefOf.ExitMapBest);
        }

        private static bool ForceIsBroken(
            List<Pawn> pawns,
            int initialCount)
        {
            if (initialCount <= 0)
            {
                return false;
            }

            int threshold = Math.Max(
                1,
                (int)Math.Floor(initialCount * 0.30f));
            int activeCount = ActivePawns(pawns).Count;
            return activeCount < initialCount
                && activeCount <= threshold;
        }

        private static void OrderJointWithdrawal(
            GoauldAlliedReinforcementState state)
        {
            Map map = ActivePawns(state.primaryPawns)
                    .FirstOrDefault()?.Map
                ?? ActivePawns(state.alliedPawns)
                    .FirstOrDefault()?.Map;

            if (map == null)
            {
                return;
            }

            OrderFactionWithdrawal(
                state.primaryDomain,
                state.primaryPawns,
                map);
            OrderFactionWithdrawal(
                state.alliedDomain,
                state.alliedPawns,
                map);
            state.withdrawalOrdered = true;
            GR_Log.Message(
                "Ordered both domains of a joint Goa'uld raid to "
                + $"withdraw from map {map.uniqueID}.");
        }

        private static void OrderAlliedWithdrawal(
            GoauldAlliedReinforcementState state)
        {
            List<Pawn> mobile = ActivePawns(state.alliedPawns);
            Map map = mobile.FirstOrDefault()?.Map;

            if (map == null || mobile.Count == 0)
            {
                return;
            }

            OrderFactionWithdrawal(
                state.alliedDomain,
                state.alliedPawns,
                map);
            state.withdrawalOrdered = true;
            GR_Log.Message(
                "Ordered allied Goa'uld reinforcements to withdraw with "
                + $"the primary raid on map {map.uniqueID}.");
        }

        private static void OrderFactionWithdrawal(
            Faction faction,
            List<Pawn> pawns,
            Map map)
        {
            List<Pawn> mobile = ActivePawns(pawns);

            if (faction == null || map == null || mobile.Count == 0)
            {
                return;
            }

            foreach (Pawn pawn in mobile)
            {
                pawn.GetLord()?.RemovePawn(pawn);
                pawn.jobs?.EndCurrentJob(JobCondition.InterruptForced);
            }

            LordMaker.MakeNewLord(
                faction,
                new LordJob_ExitMapBest(
                    LocomotionUrgency.Jog,
                    canDig: false,
                    canDefendSelf: true),
                map,
                mobile);
        }

        private void RemoveStateAt(
            int index,
            GoauldAlliedReinforcementState state)
        {
            bool cooperationWasActive = state?.arrived == true;

            if (index >= 0 && index < states.Count)
            {
                states.RemoveAt(index);
            }

            if (cooperationWasActive)
            {
                NotifyCooperationChanged(state);
            }
        }

        private static void NotifyCooperationChanged(
            GoauldAlliedReinforcementState state)
        {
            GoauldSystemLordFactionUtility
                .NotifyTemporaryCooperationChanged(
                    state?.primaryDomain,
                    state?.alliedDomain);
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

        private static List<Pawn> ActivePawns(List<Pawn> pawns)
        {
            return pawns
                ?.Where(pawn =>
                    pawn != null
                    && !pawn.Dead
                    && !pawn.Destroyed
                    && pawn.Spawned
                    && !pawn.Downed
                    && !pawn.IsPrisonerOfColony)
                .ToList()
                ?? new List<Pawn>();
        }

        private static bool HasSpawnedParticipant(List<Pawn> pawns)
        {
            return pawns?.Any(pawn =>
                pawn != null
                && !pawn.Dead
                && !pawn.Destroyed
                && pawn.Spawned
                && !pawn.IsPrisonerOfColony) == true;
        }

        private static int ActivePawnCount(List<Pawn> pawns)
        {
            return ActivePawns(pawns).Count;
        }

        private static bool IsValidState(
            GoauldAlliedReinforcementState state)
        {
            return state != null
                && state.targetMapUniqueId >= 0
                && GoauldSystemLordFactionUtility.IsSystemLordFaction(
                    state.primaryDomain)
                && GoauldSystemLordFactionUtility.IsSystemLordFaction(
                    state.alliedDomain)
                && state.primaryDomain != state.alliedDomain
                && state.alliedPoints > 0f;
        }

        private static int CurrentTick()
        {
            return Find.TickManager?.TicksGame ?? 0;
        }
    }
}
