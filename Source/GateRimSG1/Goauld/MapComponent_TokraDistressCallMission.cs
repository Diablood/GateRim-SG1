using System;
using System.Collections.Generic;
using System.Linq;
using GateRimSG1.Missions;
using RimWorld;
using Verse;
using Verse.AI;
using Verse.AI.Group;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Owns the local-map state of a Tok'ra distress-call mission. The
    /// manager keeps the global operation slot; this component observes
    /// combat, field treatment and the visible Tok'ra recovery team.
    /// </summary>
    public sealed class MapComponent_TokraDistressCallMission : MapComponent
    {
        private bool initialized;
        private bool operationResolved;
        private bool missionSucceeded;
        private TokraDistressCallVariant variant
            = TokraDistressCallVariant.None;
        private WorldObject_TokraDistressCallSite parentSite;
        private List<Pawn> survivors = new List<Pawn>();
        private Thing salvage;
        private List<string> careReceivedIds = new List<string>();
        private List<string> departureOrderedIds = new List<string>();
        private List<string> departedSurvivorIds = new List<string>();
        private Dictionary<string, int> stableSinceTicks
            = new Dictionary<string, int>();
        private List<Pawn> recoveryTeam = new List<Pawn>();
        private bool recoveryTeamRequested;
        private bool recoveryTeamSpawned;
        private bool recoveryTeamDepartureOrdered;
        private int recoveryTeamArrivalTick;
        private int nextRecoveryTeamRetryTick;
        private IntVec3 preferredEntryCell = IntVec3.Invalid;
        private int nextStateCheckTick;

        public MapComponent_TokraDistressCallMission(Map map) : base(map)
        {
        }

        public bool Initialized => initialized;
        public bool OperationResolved => operationResolved;
        public bool MissionSucceeded => missionSucceeded;
        public IntVec3 PreferredEntryCell => preferredEntryCell;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(
                ref initialized,
                "distressInitialized",
                false);
            Scribe_Values.Look(
                ref operationResolved,
                "distressOperationResolved",
                false);
            Scribe_Values.Look(
                ref missionSucceeded,
                "distressMissionSucceeded",
                false);
            Scribe_Values.Look(
                ref variant,
                "distressVariant",
                TokraDistressCallVariant.None);
            Scribe_References.Look(ref parentSite, "distressParentSite");
            Scribe_Collections.Look(
                ref survivors,
                "distressSurvivors",
                LookMode.Reference);
            Scribe_References.Look(ref salvage, "distressSalvage");
            Scribe_Collections.Look(
                ref careReceivedIds,
                "distressCareReceivedIds",
                LookMode.Value);
            Scribe_Collections.Look(
                ref departureOrderedIds,
                "distressDepartureOrderedIds",
                LookMode.Value);
            Scribe_Collections.Look(
                ref departedSurvivorIds,
                "distressDepartedSurvivorIds",
                LookMode.Value);
            Scribe_Collections.Look(
                ref stableSinceTicks,
                "distressStableSinceTicks",
                LookMode.Value,
                LookMode.Value);
            Scribe_Collections.Look(
                ref recoveryTeam,
                "distressRecoveryTeam",
                LookMode.Reference);
            Scribe_Values.Look(
                ref recoveryTeamRequested,
                "distressRecoveryTeamRequested",
                false);
            Scribe_Values.Look(
                ref recoveryTeamSpawned,
                "distressRecoveryTeamSpawned",
                false);
            Scribe_Values.Look(
                ref recoveryTeamDepartureOrdered,
                "distressRecoveryTeamDepartureOrdered",
                false);
            Scribe_Values.Look(
                ref recoveryTeamArrivalTick,
                "distressRecoveryTeamArrivalTick",
                0);
            Scribe_Values.Look(
                ref nextRecoveryTeamRetryTick,
                "distressNextRecoveryTeamRetryTick",
                0);
            Scribe_Values.Look(
                ref preferredEntryCell,
                "distressPreferredEntryCell",
                IntVec3.Invalid);
            Scribe_Values.Look(
                ref nextStateCheckTick,
                "distressNextStateCheckTick",
                0);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                survivors = survivors ?? new List<Pawn>();
                careReceivedIds = careReceivedIds ?? new List<string>();
                departureOrderedIds
                    = departureOrderedIds ?? new List<string>();
                departedSurvivorIds
                    = departedSurvivorIds ?? new List<string>();
                stableSinceTicks
                    = stableSinceTicks ?? new Dictionary<string, int>();
                recoveryTeam = recoveryTeam ?? new List<Pawn>();
            }
        }

        public override void MapComponentTick()
        {
            base.MapComponentTick();

            if (!initialized || operationResolved)
            {
                return;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;

            if (currentTick < nextStateCheckTick)
            {
                return;
            }

            nextStateCheckTick = currentTick
                + TokraDistressCallMissionUtility.CheckIntervalTicks;

            if (variant == TokraDistressCallVariant.GenuineRescue)
            {
                TickRescue(currentTick);
            }
            else
            {
                TickCombatOnly();
            }
        }

        public void Initialize(
            WorldObject_TokraDistressCallSite parent,
            TokraDistressCallVariant arrivalVariant,
            List<Pawn> spawnedSurvivors,
            List<Pawn> spawnedHostiles,
            Thing spawnedSalvage,
            IntVec3 entryCell)
        {
            parentSite = parent;
            variant = arrivalVariant;
            survivors = spawnedSurvivors ?? new List<Pawn>();
            salvage = spawnedSalvage;
            preferredEntryCell = entryCell;
            initialized = true;
            operationResolved = false;
            missionSucceeded = false;
            nextStateCheckTick = Find.TickManager?.TicksGame ?? 0;

            if (variant == TokraDistressCallVariant.GenuineRescue
                && survivors.Count == 0)
            {
                ResolveFailure("failureSurvivorsLost");
            }
        }

        public void MarkInitializationFailed(
            WorldObject_TokraDistressCallSite parent)
        {
            parentSite = parent;
            initialized = true;
            ResolveFailure("failureSiteLost");
        }

        public void NotifyManagerResolved(bool succeeded)
        {
            operationResolved = true;
            missionSucceeded = succeeded;
        }

        private void TickCombatOnly()
        {
            if (!TokraDistressCallMissionUtility.HasActiveHostiles(map))
            {
                ResolveSuccess();
            }
        }

        private void TickRescue(int currentTick)
        {
            TokraOrganicOperationDefinition definition
                = TokraOrganicOperationFramework.GetDefinition(
                    TokraOrganicOperationArchetype.DistressCall);
            GateRimMissionPawnCareDef careProfile = definition?.PawnCare;
            GateRimMissionDistressCallDef distressProfile
                = definition?.MissionDef?.distressCall;
            bool hostilesRemain
                = TokraDistressCallMissionUtility.HasActiveHostiles(map);
            List<Pawn> readyForRecovery = new List<Pawn>();

            foreach (Pawn survivor in survivors.Where(item => item != null))
            {
                string id = survivor.GetUniqueLoadID();

                if (departedSurvivorIds.Contains(id))
                {
                    continue;
                }

                if (survivor.Dead || survivor.IsPrisonerOfColony)
                {
                    continue;
                }

                if (survivor.Destroyed || survivor.MapHeld != map)
                {
                    if (departureOrderedIds.Contains(id))
                    {
                        MarkSurvivorDeparted(survivor, id);
                    }

                    continue;
                }

                if (!careReceivedIds.Contains(id))
                {
                    TokraOrganicWoundedAgentUtility
                        .EnsureSymbioteShock(survivor);

                    if (!IsSymbioteShockTended(survivor, careProfile))
                    {
                        continue;
                    }

                    careReceivedIds.Add(id);
                    TokraOrganicWoundedAgentUtility
                        .BeginPostShockRecovery(survivor);
                    Messages.Message(
                        "GR_TokraDistressCall_CareReceived"
                            .Translate(survivor.LabelShortCap),
                        survivor,
                        MessageTypeDefOf.PositiveEvent,
                        historical: true);
                }
                else
                {
                    TokraOrganicWoundedAgentUtility
                        .RemoveSymbioteShock(survivor);
                    TokraOrganicWoundedAgentUtility
                        .EnsurePostShockRecovery(survivor);
                }

                if (!hostilesRemain)
                {
                    readyForRecovery.Add(survivor);
                }
            }

            if (!hostilesRemain && readyForRecovery.Count > 0)
            {
                RequestOrMaintainRecoveryTeam(
                    currentTick,
                    distressProfile,
                    readyForRecovery);
            }

            if (departedSurvivorIds.Count > 0
                && !HasUnresolvedLivingSurvivor())
            {
                OrderRemainingRecoveryTeamDeparture();
                ResolveSuccess();
                return;
            }

            if (departedSurvivorIds.Count == 0
                && !HasPotentiallyRescuableSurvivor())
            {
                OrderRemainingRecoveryTeamDeparture();
                ResolveFailure("failureSurvivorsLost");
            }
        }

        private void RequestOrMaintainRecoveryTeam(
            int currentTick,
            GateRimMissionDistressCallDef profile,
            List<Pawn> readyForRecovery)
        {
            if (!recoveryTeamRequested)
            {
                recoveryTeamRequested = true;
                recoveryTeamArrivalTick = currentTick + Math.Max(
                    1,
                    profile?.recoveryTeamDelayTicks
                        ?? profile?.evacuationDelayTicks
                        ?? 600);
                Pawn target = readyForRecovery.First();
                Messages.Message(
                    "GR_TokraDistressCall_EvacuationInbound"
                        .Translate(target.LabelShortCap),
                    target,
                    MessageTypeDefOf.PositiveEvent,
                    historical: true);
                return;
            }

            if (!recoveryTeamSpawned)
            {
                if (currentTick < recoveryTeamArrivalTick
                    || currentTick < nextRecoveryTeamRetryTick)
                {
                    return;
                }

                if (!TrySpawnRecoveryTeam(profile, readyForRecovery))
                {
                    nextRecoveryTeamRetryTick = currentTick + Math.Max(
                        250,
                        profile?.recoveryTeamRetryTicks ?? 1200);
                    return;
                }
            }

            AssignRecoveryJobs(readyForRecovery);

            if (!HasActiveRecoveryTeamMember()
                && readyForRecovery.Any(IsSurvivorStillOnMap))
            {
                recoveryTeamSpawned = false;
                recoveryTeamDepartureOrdered = false;
                nextRecoveryTeamRetryTick = currentTick + Math.Max(
                    250,
                    profile?.recoveryTeamRetryTicks ?? 1200);
            }
        }

        private bool TrySpawnRecoveryTeam(
            GateRimMissionDistressCallDef profile,
            List<Pawn> recoveryTargets)
        {
            if (map == null || recoveryTargets.NullOrEmpty())
            {
                return false;
            }

            Faction tokraFaction
                = TokraFactionUtility.GetOrCreatePersistentFaction(
                    "distress-call recovery team");
            PawnKindDef recoveryKind = ResolveRecoveryPawnKind(profile);

            if (tokraFaction == null || recoveryKind == null)
            {
                return false;
            }

            PawnsArrivalModeDef arrivalMode = PawnsArrivalModeDefOf.EdgeWalkIn;

            if (arrivalMode?.Worker == null
                || !arrivalMode.Worker.CanUseOnMap(map))
            {
                return false;
            }

            IncidentParms parms = new IncidentParms
            {
                target = map,
                faction = tokraFaction,
                attackTargets = recoveryTargets
                    .Where(IsSurvivorStillOnMap)
                    .Cast<Thing>()
                    .ToList()
            };

            if (!arrivalMode.Worker.TryResolveRaidSpawnCenter(parms))
            {
                return false;
            }

            int minimum = Math.Max(1, profile?.recoveryTeamMinimumCount ?? 2);
            int maximum = Math.Max(
                minimum,
                profile?.recoveryTeamMaximumCount ?? 3);
            int count = Math.Max(
                minimum,
                Math.Min(maximum, recoveryTargets.Count));
            List<Pawn> generatedPawns = new List<Pawn>();

            for (int index = 0; index < count; index++)
            {
                Pawn pawn = PawnGenerator.GeneratePawn(
                    recoveryKind,
                    tokraFaction,
                    map.Tile);

                if (pawn != null)
                {
                    generatedPawns.Add(pawn);
                }
            }

            if (generatedPawns.Count == 0)
            {
                return false;
            }

            arrivalMode.Worker.Arrive(generatedPawns, parms);

            recoveryTeam = generatedPawns
                .Where(pawn => pawn != null
                    && !pawn.Destroyed
                    && pawn.Spawned
                    && pawn.Map == map)
                .ToList();

            if (recoveryTeam.Count == 0)
            {
                return false;
            }

            recoveryTeamSpawned = true;
            recoveryTeamDepartureOrdered = false;
            Messages.Message(
                "GR_TokraDistressCall_RecoveryTeamArrived".Translate(),
                recoveryTeam[0],
                MessageTypeDefOf.PositiveEvent,
                historical: true);
            return true;
        }

        private static PawnKindDef ResolveRecoveryPawnKind(
            GateRimMissionDistressCallDef profile)
        {
            string defName = string.IsNullOrWhiteSpace(
                    profile?.recoveryPawnKindDefName)
                ? profile?.survivorPawnKindDefName
                : profile.recoveryPawnKindDefName;

            return string.IsNullOrWhiteSpace(defName)
                ? null
                : DefDatabase<PawnKindDef>.GetNamedSilentFail(defName);
        }

        private void AssignRecoveryJobs(List<Pawn> readyForRecovery)
        {
            foreach (Pawn survivor in readyForRecovery
                .Where(IsSurvivorStillOnMap))
            {
                string survivorId = survivor.GetUniqueLoadID();

                if (!survivor.Downed)
                {
                    if (TryOrderWalkingDeparture(survivor))
                    {
                        AddDepartureOrder(survivorId);
                    }

                    continue;
                }

                if (IsRecoveryCarrierAssigned(survivor))
                {
                    AddDepartureOrder(survivorId);
                    continue;
                }

                Pawn carrier = FindAvailableRecoveryCarrier();

                if (carrier == null
                    || !RCellFinder.TryFindBestExitSpot(
                        carrier,
                        out IntVec3 exitCell))
                {
                    continue;
                }

                Job job = JobMaker.MakeJob(JobDefOf.Kidnap);
                job.targetA = survivor;
                job.targetB = exitCell;
                job.count = 1;

                if (carrier.jobs.TryTakeOrderedJob(job))
                {
                    AddDepartureOrder(survivorId);
                }
            }
        }

        private bool TryOrderWalkingDeparture(Pawn survivor)
        {
            if (TokraOrganicWoundedAgentUtility.TryOrderDeparture(survivor))
            {
                return true;
            }

            Faction faction = survivor.Faction;

            if (faction == null || survivor.GetLord() != null)
            {
                return false;
            }

            LordMaker.MakeNewLord(
                faction,
                new LordJob_ExitMapBest(
                    LocomotionUrgency.Jog,
                    canDig: false,
                    canDefendSelf: true),
                map,
                new List<Pawn> { survivor });
            return true;
        }

        private Pawn FindAvailableRecoveryCarrier()
        {
            foreach (Pawn pawn in recoveryTeam.Where(item => item != null))
            {
                if (pawn.Dead
                    || pawn.Destroyed
                    || !pawn.Spawned
                    || pawn.Map != map
                    || pawn.Downed)
                {
                    continue;
                }

                Job currentJob = pawn.jobs?.curJob;

                if (currentJob == null
                    || currentJob.def != JobDefOf.Kidnap)
                {
                    return pawn;
                }
            }

            return null;
        }

        private bool IsRecoveryCarrierAssigned(Pawn survivor)
        {
            foreach (Pawn pawn in recoveryTeam.Where(item => item != null))
            {
                Job currentJob = pawn.jobs?.curJob;

                if (currentJob?.def == JobDefOf.Kidnap
                    && currentJob.targetA.Thing == survivor)
                {
                    return true;
                }
            }

            return false;
        }

        private bool HasActiveRecoveryTeamMember()
        {
            return recoveryTeam.Any(pawn => pawn != null
                && !pawn.Dead
                && !pawn.Destroyed
                && pawn.Spawned
                && pawn.Map == map);
        }

        private void OrderRemainingRecoveryTeamDeparture()
        {
            if (recoveryTeamDepartureOrdered)
            {
                return;
            }

            List<Pawn> remaining = recoveryTeam
                .Where(pawn => pawn != null
                    && !pawn.Dead
                    && !pawn.Destroyed
                    && pawn.Spawned
                    && pawn.Map == map)
                .ToList();

            if (remaining.Count == 0)
            {
                recoveryTeamDepartureOrdered = true;
                return;
            }

            Faction faction = remaining[0].Faction;

            if (faction == null)
            {
                return;
            }

            foreach (Pawn pawn in remaining)
            {
                pawn.jobs?.EndCurrentJob(JobCondition.InterruptForced);
            }

            LordMaker.MakeNewLord(
                faction,
                new LordJob_ExitMapBest(
                    LocomotionUrgency.Jog,
                    canDig: false,
                    canDefendSelf: true),
                map,
                remaining);
            recoveryTeamDepartureOrdered = true;
        }

        private static bool IsSurvivorStillOnMap(Pawn survivor)
        {
            return survivor != null
                && !survivor.Dead
                && !survivor.Destroyed
                && survivor.Spawned
                && survivor.Map != null
                && !survivor.IsPrisonerOfColony;
        }

        private void AddDepartureOrder(string survivorId)
        {
            if (!departureOrderedIds.Contains(survivorId))
            {
                departureOrderedIds.Add(survivorId);
            }
        }

        private void MarkSurvivorDeparted(Pawn survivor, string survivorId)
        {
            if (departedSurvivorIds.Contains(survivorId))
            {
                return;
            }

            departedSurvivorIds.Add(survivorId);
            TokraOrganicWoundedAgentUtility.ClearOperationHealthConditions(
                survivor);
            Messages.Message(
                "GR_TokraDistressCall_SurvivorEvacuated"
                    .Translate(survivor.LabelShortCap),
                map.Parent,
                MessageTypeDefOf.PositiveEvent,
                historical: true);
        }

        private static bool IsSymbioteShockTended(
            Pawn survivor,
            GateRimMissionPawnCareDef careProfile)
        {
            if (survivor?.health?.hediffSet == null
                || string.IsNullOrWhiteSpace(
                    careProfile?.initialHediffDefName))
            {
                return false;
            }

            HediffDef shockDef = DefDatabase<HediffDef>.GetNamedSilentFail(
                careProfile.initialHediffDefName);
            HediffWithComps shock = shockDef == null
                ? null
                : survivor.health.hediffSet.GetFirstHediffOfDef(shockDef)
                    as HediffWithComps;
            HediffComp_TendDuration tendComp
                = shock?.TryGetComp<HediffComp_TendDuration>();

            return tendComp != null && tendComp.IsTended;
        }

        private bool HasUnresolvedLivingSurvivor()
        {
            foreach (Pawn survivor in survivors.Where(item => item != null))
            {
                string id = survivor.GetUniqueLoadID();

                if (departedSurvivorIds.Contains(id)
                    || survivor.Dead
                    || survivor.IsPrisonerOfColony)
                {
                    continue;
                }

                if (!survivor.Destroyed && survivor.MapHeld == map)
                {
                    return true;
                }
            }

            return false;
        }

        private bool HasPotentiallyRescuableSurvivor()
        {
            foreach (Pawn survivor in survivors.Where(item => item != null))
            {
                string id = survivor.GetUniqueLoadID();

                if (departedSurvivorIds.Contains(id))
                {
                    return true;
                }

                if (!survivor.Dead
                    && !survivor.Destroyed
                    && !survivor.IsPrisonerOfColony
                    && survivor.MapHeld == map)
                {
                    return true;
                }
            }

            return false;
        }

        private void ResolveSuccess()
        {
            if (operationResolved)
            {
                return;
            }

            if (!GameComponent_TokraOrganicOperationManager
                .NotifyDistressCallSiteResolved(
                    parentSite,
                    succeeded: true,
                    failureTextId: null))
            {
                NotifyManagerResolved(true);
            }
        }

        private void ResolveFailure(string failureTextId)
        {
            if (operationResolved)
            {
                return;
            }

            if (!GameComponent_TokraOrganicOperationManager
                .NotifyDistressCallSiteResolved(
                    parentSite,
                    succeeded: false,
                    failureTextId: failureTextId))
            {
                NotifyManagerResolved(false);
            }
        }
    }
}
