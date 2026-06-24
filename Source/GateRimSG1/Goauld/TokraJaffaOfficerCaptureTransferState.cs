using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using RimWorld.Planet;
using Verse;
using Verse.AI;
using Verse.AI.Group;

namespace GateRimSG1.Goauld
{
    internal enum TokraJaffaOfficerCaptureTransferTickResult
    {
        None,
        Succeeded,
        FailedTargetKilled,
        FailedTargetLost
    }

    public sealed class TokraJaffaOfficerCaptureTransferState : IExposable
    {
        public Pawn targetOfficer;
        public bool fieldSiteCleared;
        public int targetMissingSinceTick;

        public bool extractionRequested;
        public int extractionMapId = -1;
        public int extractionArrivalTick;
        public int nextExtractionRetryTick;
        public List<Pawn> extractionTeam = new List<Pawn>();
        public Pawn extractionCarrier;
        public bool extractionTeamSpawned;
        public bool extractionPickupOrdered;
        public bool extractionCarrierHadTarget;
        public bool extractionTargetDeparted;
        public bool extractionTeamDepartureOrdered;
        public bool extractionWaitingMessageSent;

        public void ExposeData()
        {
            Scribe_References.Look(ref targetOfficer, "targetOfficer");
            Scribe_Values.Look(ref fieldSiteCleared, "fieldSiteCleared", false);
            Scribe_Values.Look(
                ref targetMissingSinceTick,
                "targetMissingSinceTick",
                0);
            Scribe_Values.Look(
                ref extractionRequested,
                "extractionRequested",
                false);
            Scribe_Values.Look(ref extractionMapId, "extractionMapId", -1);
            Scribe_Values.Look(
                ref extractionArrivalTick,
                "extractionArrivalTick",
                0);
            Scribe_Values.Look(
                ref nextExtractionRetryTick,
                "nextExtractionRetryTick",
                0);
            Scribe_Collections.Look(
                ref extractionTeam,
                "extractionTeam",
                LookMode.Reference);
            Scribe_References.Look(
                ref extractionCarrier,
                "extractionCarrier");
            Scribe_Values.Look(
                ref extractionTeamSpawned,
                "extractionTeamSpawned",
                false);
            Scribe_Values.Look(
                ref extractionPickupOrdered,
                "extractionPickupOrdered",
                false);
            Scribe_Values.Look(
                ref extractionCarrierHadTarget,
                "extractionCarrierHadTarget",
                false);
            Scribe_Values.Look(
                ref extractionTargetDeparted,
                "extractionTargetDeparted",
                false);
            Scribe_Values.Look(
                ref extractionTeamDepartureOrdered,
                "extractionTeamDepartureOrdered",
                false);
            Scribe_Values.Look(
                ref extractionWaitingMessageSent,
                "extractionWaitingMessageSent",
                false);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                extractionTeam = extractionTeam ?? new List<Pawn>();
            }
        }

        public void Reset()
        {
            targetOfficer = null;
            fieldSiteCleared = false;
            targetMissingSinceTick = 0;
            extractionRequested = false;
            extractionMapId = -1;
            extractionArrivalTick = 0;
            nextExtractionRetryTick = 0;
            extractionTeam = new List<Pawn>();
            extractionCarrier = null;
            extractionTeamSpawned = false;
            extractionPickupOrdered = false;
            extractionCarrierHadTarget = false;
            extractionTargetDeparted = false;
            extractionTeamDepartureOrdered = false;
            extractionWaitingMessageSent = false;
        }
    }

    internal static class TokraJaffaOfficerCaptureTransferController
    {
        public static string GetExtractionRequestDisabledReason(
            TokraJaffaOfficerCaptureTransferState state,
            Map map)
        {
            if (state == null)
            {
                return "GR_TokraJaffaOfficerCapture_TargetUnavailable"
                    .Translate()
                    .ToString();
            }

            if (state.extractionRequested)
            {
                return "GR_TokraJaffaOfficerCapture_ExtractionAlreadyRequested"
                    .Translate()
                    .ToString();
            }

            if (map?.IsPlayerHome != true)
            {
                return "GR_TokraJaffaOfficerCapture_ExtractionRequiresHomeMap"
                    .Translate()
                    .ToString();
            }

            Pawn target = state.targetOfficer;

            if (target == null || target.Dead || target.Destroyed)
            {
                return "GR_TokraJaffaOfficerCapture_TargetUnavailable"
                    .Translate()
                    .ToString();
            }

            if (!TokraJaffaOfficerCaptureMissionUtility
                .IsPawnPresentOnMapIncludingCarried(target, map))
            {
                return "GR_TokraJaffaOfficerCapture_ExtractionTargetNotPresent"
                    .Translate(target.LabelShortCap)
                    .ToString();
            }

            if (!target.IsPrisonerOfColony)
            {
                return "GR_TokraJaffaOfficerCapture_TargetNotPrisoner"
                    .Translate(target.LabelShortCap)
                    .ToString();
            }

            return null;
        }

        public static bool TryRequestHomeExtraction(
            TokraJaffaOfficerCaptureTransferState state,
            Map map,
            Pawn operatorPawn)
        {
            string disabledReason = GetExtractionRequestDisabledReason(
                state,
                map);

            if (!string.IsNullOrEmpty(disabledReason))
            {
                Thing rejectionTarget = operatorPawn != null
                    ? (Thing)operatorPawn
                    : state?.targetOfficer;
                Messages.Message(
                    disabledReason,
                    rejectionTarget,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            state.extractionRequested = true;
            state.extractionMapId = map.uniqueID;
            state.extractionArrivalTick = currentTick + Rand.RangeInclusive(
                TokraJaffaOfficerCaptureMissionUtility
                    .GetExtractionMinimumDelayTicks(),
                TokraJaffaOfficerCaptureMissionUtility
                    .GetExtractionMaximumDelayTicks());
            state.nextExtractionRetryTick = state.extractionArrivalTick;
            state.extractionWaitingMessageSent = false;

            Pawn target = state.targetOfficer;
            Thing messageTarget = operatorPawn != null
                ? (Thing)operatorPawn
                : target;
            Messages.Message(
                "GR_TokraJaffaOfficerCapture_ExtractionRequested".Translate(
                    target.LabelShortCap,
                    GetExtractionHoursString(state)),
                messageTarget,
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            GR_Log.Message(
                "Requested Tok'ra home-map extraction for Jaffa officer "
                + $"{target.LabelShortCap}; map {map.uniqueID}; "
                + $"arrival tick {state.extractionArrivalTick}.");
            return true;
        }

        public static TokraJaffaOfficerCaptureTransferTickResult Tick(
            TokraJaffaOfficerCaptureTransferState state,
            int currentTick)
        {
            Pawn target = state?.targetOfficer;

            if (target == null)
            {
                return state?.fieldSiteCleared == true
                    ? TokraJaffaOfficerCaptureTransferTickResult
                        .FailedTargetLost
                    : TokraJaffaOfficerCaptureTransferTickResult.None;
            }

            if (target.Dead)
            {
                return TokraJaffaOfficerCaptureTransferTickResult
                    .FailedTargetKilled;
            }

            if (state.extractionRequested)
            {
                TokraJaffaOfficerCaptureTransferTickResult extractionResult
                    = TickHomeExtraction(state, currentTick);

                if (extractionResult
                    != TokraJaffaOfficerCaptureTransferTickResult.None)
                {
                    return extractionResult;
                }
            }

            if (target.Destroyed && !state.extractionTargetDeparted)
            {
                return TokraJaffaOfficerCaptureTransferTickResult
                    .FailedTargetLost;
            }

            Map homeMap = TokraJaffaOfficerCaptureMissionUtility
                .FindPlayerHomeMapContaining(target);
            Caravan caravan = TokraJaffaOfficerCaptureMissionUtility
                .FindPlayerCaravanContaining(target);

            if (caravan != null && target.IsPrisonerOfColony)
            {
                TokraJaffaOfficerCaptureMissionUtility
                    .EnsureTargetTransferRestraint(target);
            }
            else if (homeMap != null && !state.extractionTeamSpawned)
            {
                TokraJaffaOfficerCaptureMissionUtility
                    .ClearTargetTransferRestraint(target);
            }

            if (homeMap != null
                || caravan != null
                || state.extractionTargetDeparted)
            {
                state.targetMissingSinceTick = 0;
                return TokraJaffaOfficerCaptureTransferTickResult.None;
            }

            if (!state.fieldSiteCleared)
            {
                state.targetMissingSinceTick = 0;
                return TokraJaffaOfficerCaptureTransferTickResult.None;
            }

            if (state.targetMissingSinceTick <= 0)
            {
                state.targetMissingSinceTick = currentTick;
                return TokraJaffaOfficerCaptureTransferTickResult.None;
            }

            if (currentTick - state.targetMissingSinceTick
                < TokraJaffaOfficerCaptureMissionUtility.TargetLossGraceTicks)
            {
                return TokraJaffaOfficerCaptureTransferTickResult.None;
            }

            return TokraJaffaOfficerCaptureTransferTickResult
                .FailedTargetLost;
        }

        public static void Cleanup(
            TokraJaffaOfficerCaptureTransferState state)
        {
            if (state == null)
            {
                return;
            }

            Map extractionMap = TokraJaffaOfficerCaptureMissionUtility
                .FindPlayerHomeMapById(state.extractionMapId);
            CancelExtractionPickupOrder(state);
            TokraJaffaOfficerCaptureMissionUtility
                .ClearTargetTransferRestraint(state.targetOfficer);

            if (extractionMap != null)
            {
                OrderRemainingExtractionTeamDeparture(state, extractionMap);
            }
        }

        public static string GetExtractionHoursString(
            TokraJaffaOfficerCaptureTransferState state)
        {
            int currentTick = Find.TickManager?.TicksGame ?? 0;
            int remainingTicks = Math.Max(
                0,
                (state?.extractionArrivalTick ?? 0) - currentTick);
            return Math.Max(1, Math.Ceiling(remainingTicks / 2500f))
                .ToString("0");
        }

        private static TokraJaffaOfficerCaptureTransferTickResult
            TickHomeExtraction(
                TokraJaffaOfficerCaptureTransferState state,
                int currentTick)
        {
            Map extractionMap = TokraJaffaOfficerCaptureMissionUtility
                .FindPlayerHomeMapById(state.extractionMapId);

            if (extractionMap == null)
            {
                return TokraJaffaOfficerCaptureTransferTickResult.None;
            }

            Pawn target = state.targetOfficer;
            bool targetPresent = TokraJaffaOfficerCaptureMissionUtility
                .IsPawnPresentOnMapIncludingCarried(target, extractionMap);
            Pawn currentCarrier = FindExtractionCarrierHoldingTarget(
                state,
                extractionMap);

            if (currentCarrier != null)
            {
                state.extractionCarrier = currentCarrier;
                state.extractionCarrierHadTarget = true;
            }

            if (state.extractionCarrierHadTarget && !targetPresent)
            {
                if (!state.extractionTargetDeparted)
                {
                    state.extractionTargetDeparted = true;
                    state.extractionPickupOrdered = false;
                    TokraJaffaOfficerCaptureMissionUtility
                        .ClearTargetTransferRestraint(target);
                    Messages.Message(
                        "GR_TokraJaffaOfficerCapture_ExtractionTargetDeparted"
                            .Translate(target.LabelShortCap),
                        extractionMap.Parent,
                        MessageTypeDefOf.PositiveEvent,
                        historical: true);
                }

                OrderRemainingExtractionTeamDeparture(state, extractionMap);

                return HasActiveExtractionTeamMember(state, extractionMap)
                    ? TokraJaffaOfficerCaptureTransferTickResult.None
                    : TokraJaffaOfficerCaptureTransferTickResult.Succeeded;
            }

            if (currentCarrier != null)
            {
                return TokraJaffaOfficerCaptureTransferTickResult.None;
            }

            if (!state.extractionTeamSpawned)
            {
                if (currentTick < Math.Max(
                        state.extractionArrivalTick,
                        state.nextExtractionRetryTick))
                {
                    return TokraJaffaOfficerCaptureTransferTickResult.None;
                }

                if (!targetPresent || !target.IsPrisonerOfColony)
                {
                    if (!state.extractionWaitingMessageSent)
                    {
                        Messages.Message(
                            "GR_TokraJaffaOfficerCapture_ExtractionWaiting"
                                .Translate(target.LabelShortCap),
                            extractionMap.Parent,
                            MessageTypeDefOf.NegativeEvent,
                            historical: false);
                        state.extractionWaitingMessageSent = true;
                    }

                    state.nextExtractionRetryTick = currentTick
                        + TokraJaffaOfficerCaptureMissionUtility
                            .GetExtractionRetryTicks();
                    return TokraJaffaOfficerCaptureTransferTickResult.None;
                }

                if (!TrySpawnExtractionTeam(state, extractionMap))
                {
                    state.nextExtractionRetryTick = currentTick
                        + TokraJaffaOfficerCaptureMissionUtility
                            .GetExtractionRetryTicks();
                    return TokraJaffaOfficerCaptureTransferTickResult.None;
                }

                state.extractionWaitingMessageSent = false;
            }

            if (!targetPresent)
            {
                return TokraJaffaOfficerCaptureTransferTickResult.None;
            }

            if (!target.IsPrisonerOfColony)
            {
                CancelExtractionPickupOrder(state);
                TokraJaffaOfficerCaptureMissionUtility
                    .ClearTargetTransferRestraint(target);
                return TokraJaffaOfficerCaptureTransferTickResult.None;
            }

            TokraJaffaOfficerCaptureMissionUtility
                .EnsureTargetTransferRestraint(target);

            if (!HasActiveExtractionTeamMember(state, extractionMap))
            {
                ResetExtractionTeamForRetry(state, currentTick);
                return TokraJaffaOfficerCaptureTransferTickResult.None;
            }

            TryAssignExtractionPickup(state, extractionMap);
            return TokraJaffaOfficerCaptureTransferTickResult.None;
        }

        private static bool TrySpawnExtractionTeam(
            TokraJaffaOfficerCaptureTransferState state,
            Map map)
        {
            Faction tokraFaction
                = TokraFactionUtility.GetOrCreatePersistentFaction(
                    "Jaffa-officer extraction team");
            PawnKindDef pawnKind = TokraJaffaOfficerCaptureMissionUtility
                .GetExtractionPawnKind();

            if (tokraFaction == null || pawnKind == null)
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
                attackTargets = new List<Thing> { state.targetOfficer }
            };

            if (!arrivalMode.Worker.TryResolveRaidSpawnCenter(parms))
            {
                return false;
            }

            int count = Rand.RangeInclusive(
                TokraJaffaOfficerCaptureMissionUtility
                    .GetExtractionTeamMinimumCount(),
                TokraJaffaOfficerCaptureMissionUtility
                    .GetExtractionTeamMaximumCount());
            List<Pawn> generatedPawns = new List<Pawn>();

            for (int index = 0; index < count; index++)
            {
                Pawn pawn = PawnGenerator.GeneratePawn(
                    pawnKind,
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
            state.extractionTeam = generatedPawns
                .Where(pawn => pawn != null
                    && !pawn.Destroyed
                    && pawn.Spawned
                    && pawn.Map == map)
                .ToList();

            if (state.extractionTeam.Count == 0)
            {
                return false;
            }

            state.extractionTeamSpawned = true;
            state.extractionPickupOrdered = false;
            state.extractionCarrier = null;
            state.extractionCarrierHadTarget = false;
            state.extractionTeamDepartureOrdered = false;
            TokraJaffaOfficerCaptureMissionUtility
                .EnsureTargetTransferRestraint(state.targetOfficer);

            Messages.Message(
                "GR_TokraJaffaOfficerCapture_ExtractionTeamArrived".Translate(
                    state.targetOfficer.LabelShortCap),
                state.extractionTeam[0],
                MessageTypeDefOf.PositiveEvent,
                historical: true);
            return true;
        }

        private static void TryAssignExtractionPickup(
            TokraJaffaOfficerCaptureTransferState state,
            Map map)
        {
            Pawn carrierHoldingTarget = FindExtractionCarrierHoldingTarget(
                state,
                map);

            if (carrierHoldingTarget != null)
            {
                state.extractionCarrier = carrierHoldingTarget;
                state.extractionCarrierHadTarget = true;
                return;
            }

            if (state.extractionCarrier != null
                && state.extractionCarrier.Spawned
                && state.extractionCarrier.Map == map
                && state.extractionCarrier.jobs?.curJob?.def == JobDefOf.Kidnap
                && state.extractionCarrier.jobs.curJob.targetA.Thing
                    == state.targetOfficer)
            {
                return;
            }

            state.extractionPickupOrdered = false;
            state.extractionCarrier = FindAvailableExtractionCarrier(state, map);

            if (state.extractionCarrier == null
                || !RCellFinder.TryFindBestExitSpot(
                    state.extractionCarrier,
                    out IntVec3 exitCell))
            {
                return;
            }

            Job job = JobMaker.MakeJob(JobDefOf.Kidnap);
            job.targetA = state.targetOfficer;
            job.targetB = exitCell;
            job.count = 1;

            if (state.extractionCarrier.jobs.TryTakeOrderedJob(job))
            {
                state.extractionPickupOrdered = true;
                Messages.Message(
                    "GR_TokraJaffaOfficerCapture_ExtractionPickupStarted"
                        .Translate(state.targetOfficer.LabelShortCap),
                    state.extractionCarrier,
                    MessageTypeDefOf.NeutralEvent,
                    historical: false);
            }
        }

        private static Pawn FindAvailableExtractionCarrier(
            TokraJaffaOfficerCaptureTransferState state,
            Map map)
        {
            return state.extractionTeam.FirstOrDefault(pawn => pawn != null
                && !pawn.Dead
                && !pawn.Destroyed
                && pawn.Spawned
                && pawn.Map == map
                && !pawn.Downed
                && pawn.carryTracker?.CarriedThing == null);
        }

        private static Pawn FindExtractionCarrierHoldingTarget(
            TokraJaffaOfficerCaptureTransferState state,
            Map map)
        {
            return state.extractionTeam.FirstOrDefault(pawn => pawn != null
                && !pawn.Dead
                && !pawn.Destroyed
                && pawn.Spawned
                && pawn.Map == map
                && pawn.carryTracker?.CarriedThing == state.targetOfficer);
        }

        private static bool HasActiveExtractionTeamMember(
            TokraJaffaOfficerCaptureTransferState state,
            Map map)
        {
            return state.extractionTeam.Any(pawn => pawn != null
                && !pawn.Dead
                && !pawn.Destroyed
                && pawn.Spawned
                && pawn.Map == map);
        }

        private static void CancelExtractionPickupOrder(
            TokraJaffaOfficerCaptureTransferState state)
        {
            if (state.extractionCarrier?.Spawned == true
                && state.extractionCarrier.jobs?.curJob?.def == JobDefOf.Kidnap)
            {
                state.extractionCarrier.jobs.EndCurrentJob(
                    JobCondition.InterruptForced);
            }

            state.extractionPickupOrdered = false;
            state.extractionCarrier = null;
            state.extractionCarrierHadTarget = false;
        }

        private static void ResetExtractionTeamForRetry(
            TokraJaffaOfficerCaptureTransferState state,
            int currentTick)
        {
            state.extractionTeamSpawned = false;
            state.extractionPickupOrdered = false;
            state.extractionCarrier = null;
            state.extractionCarrierHadTarget = false;
            state.extractionTeamDepartureOrdered = false;
            state.extractionTeam = new List<Pawn>();
            state.nextExtractionRetryTick = currentTick
                + TokraJaffaOfficerCaptureMissionUtility
                    .GetExtractionRetryTicks();
            TokraJaffaOfficerCaptureMissionUtility
                .ClearTargetTransferRestraint(state.targetOfficer);
        }

        private static void OrderRemainingExtractionTeamDeparture(
            TokraJaffaOfficerCaptureTransferState state,
            Map map)
        {
            if (state.extractionTeamDepartureOrdered)
            {
                return;
            }

            List<Pawn> remaining = state.extractionTeam
                .Where(pawn => pawn != null
                    && !pawn.Dead
                    && !pawn.Destroyed
                    && pawn.Spawned
                    && pawn.Map == map)
                .ToList();

            if (remaining.Count == 0)
            {
                state.extractionTeamDepartureOrdered = true;
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
            state.extractionTeamDepartureOrdered = true;
        }
    }
}
