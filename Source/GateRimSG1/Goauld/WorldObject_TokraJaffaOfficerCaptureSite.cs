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
    public sealed class WorldObject_TokraJaffaOfficerCaptureSite : MapParent
    {
        private int homeMapId = -1;
        private string missionDefName;
        private float scaledThreatPoints;
        private int expiryTick;
        private int missionMapSize = 120;
        private bool operationLaunched;
        private bool managerResolved;
        private bool missionSucceeded;
        private Pawn targetOfficer;
        private int nextStateCheckTick;
        private int targetMissingSinceTick;

        // Legacy r2/r3 rendezvous state is kept for save compatibility only.
        private bool handoffInProgress;
        private int handoffCompletionTick;
        private Caravan handoffCaravan;

        private bool extractionRequested;
        private int extractionMapId = -1;
        private int extractionArrivalTick;
        private int nextExtractionRetryTick;
        private List<Pawn> extractionTeam = new List<Pawn>();
        private Pawn extractionCarrier;
        private bool extractionTeamSpawned;
        private bool extractionPickupOrdered;
        private bool extractionCarrierHadTarget;
        private bool extractionTargetDeparted;
        private bool extractionTeamDepartureOrdered;
        private bool extractionWaitingMessageSent;

        public float ScaledThreatPoints => scaledThreatPoints;
        public bool OperationLaunched => operationLaunched;
        public Pawn TargetOfficer => targetOfficer;
        public bool ExtractionRequested => extractionRequested;
        public bool ExtractionTeamSpawned => extractionTeamSpawned;
        public bool ExtractionTargetDeparted => extractionTargetDeparted;
        public int ExpiryTick => expiryTick;

        public int RemainingTicks
        {
            get
            {
                int currentTick = Find.TickManager?.TicksGame ?? 0;
                return Math.Max(0, expiryTick - currentTick);
            }
        }

        public void Initialize(
            int sourceMapId,
            string sourceMissionDefName,
            float threatPoints,
            int missionExpiryTick,
            int mapSize)
        {
            homeMapId = sourceMapId;
            missionDefName = sourceMissionDefName;
            scaledThreatPoints = Math.Max(0f, threatPoints);
            expiryTick = missionExpiryTick;
            missionMapSize = Math.Max(80, mapSize);
            nextStateCheckTick = Find.TickManager?.TicksGame ?? 0;
        }

        public void RegisterTarget(Pawn target)
        {
            targetOfficer = target;
            targetMissingSinceTick = 0;
            GameComponent_TokraOrganicOperationManager
                .RegisterJaffaOfficerCaptureTarget(this, target);
        }

        internal void MigrateTransferStateTo(
            TokraJaffaOfficerCaptureTransferState state)
        {
            if (state == null)
            {
                return;
            }

            Pawn trackedTarget = targetOfficer;

            if (trackedTarget == null && HasMap)
            {
                trackedTarget = Map
                    .GetComponent<MapComponent_TokraJaffaOfficerCaptureMission>()
                    ?.TargetOfficer;
            }

            if (state.targetOfficer == null && trackedTarget != null)
            {
                state.targetOfficer = trackedTarget;
            }

            if (extractionRequested && !state.extractionRequested)
            {
                state.extractionRequested = true;
                state.extractionMapId = extractionMapId;
                state.extractionArrivalTick = extractionArrivalTick;
                state.nextExtractionRetryTick = nextExtractionRetryTick;
                state.extractionTeam = extractionTeam != null
                    ? new List<Pawn>(extractionTeam)
                    : new List<Pawn>();
                state.extractionCarrier = extractionCarrier;
                state.extractionTeamSpawned = extractionTeamSpawned;
                state.extractionPickupOrdered = extractionPickupOrdered;
                state.extractionCarrierHadTarget = extractionCarrierHadTarget;
                state.extractionTargetDeparted = extractionTargetDeparted;
                state.extractionTeamDepartureOrdered
                    = extractionTeamDepartureOrdered;
                state.extractionWaitingMessageSent
                    = extractionWaitingMessageSent;
            }

            if (state.extractionRequested)
            {
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

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref homeMapId, "captureHomeMapId", -1);
            Scribe_Values.Look(
                ref missionDefName,
                "captureMissionDefName");
            Scribe_Values.Look(
                ref scaledThreatPoints,
                "captureScaledThreatPoints",
                0f);
            Scribe_Values.Look(ref expiryTick, "captureExpiryTick", 0);
            Scribe_Values.Look(
                ref missionMapSize,
                "captureMissionMapSize",
                120);
            Scribe_Values.Look(
                ref operationLaunched,
                "captureOperationLaunched",
                false);
            Scribe_Values.Look(
                ref managerResolved,
                "captureManagerResolved",
                false);
            Scribe_Values.Look(
                ref missionSucceeded,
                "captureMissionSucceeded",
                false);
            Scribe_References.Look(ref targetOfficer, "captureTargetOfficer");
            Scribe_Values.Look(
                ref nextStateCheckTick,
                "captureNextStateCheckTick",
                0);
            Scribe_Values.Look(
                ref targetMissingSinceTick,
                "captureTargetMissingSinceTick",
                0);
            Scribe_Values.Look(
                ref handoffInProgress,
                "captureHandoffInProgress",
                false);
            Scribe_Values.Look(
                ref handoffCompletionTick,
                "captureHandoffCompletionTick",
                0);
            Scribe_References.Look(
                ref handoffCaravan,
                "captureHandoffCaravan");
            Scribe_Values.Look(
                ref extractionRequested,
                "captureExtractionRequested",
                false);
            Scribe_Values.Look(
                ref extractionMapId,
                "captureExtractionMapId",
                -1);
            Scribe_Values.Look(
                ref extractionArrivalTick,
                "captureExtractionArrivalTick",
                0);
            Scribe_Values.Look(
                ref nextExtractionRetryTick,
                "captureNextExtractionRetryTick",
                0);
            Scribe_Collections.Look(
                ref extractionTeam,
                "captureExtractionTeam",
                LookMode.Reference);
            Scribe_References.Look(
                ref extractionCarrier,
                "captureExtractionCarrier");
            Scribe_Values.Look(
                ref extractionTeamSpawned,
                "captureExtractionTeamSpawned",
                false);
            Scribe_Values.Look(
                ref extractionPickupOrdered,
                "captureExtractionPickupOrdered",
                false);
            Scribe_Values.Look(
                ref extractionCarrierHadTarget,
                "captureExtractionCarrierHadTarget",
                false);
            Scribe_Values.Look(
                ref extractionTargetDeparted,
                "captureExtractionTargetDeparted",
                false);
            Scribe_Values.Look(
                ref extractionTeamDepartureOrdered,
                "captureExtractionTeamDepartureOrdered",
                false);
            Scribe_Values.Look(
                ref extractionWaitingMessageSent,
                "captureExtractionWaitingMessageSent",
                false);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                extractionTeam = extractionTeam ?? new List<Pawn>();

                // Old world-map handoffs are converted back to a normal
                // prisoner return. The communicator now owns the extraction.
                handoffInProgress = false;
                handoffCompletionTick = 0;
                handoffCaravan = null;
            }
        }

        protected override void Tick()
        {
            base.Tick();

            if (managerResolved)
            {
                return;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;

            if (currentTick < nextStateCheckTick)
            {
                return;
            }

            nextStateCheckTick = currentTick
                + TokraJaffaOfficerCaptureMissionUtility.CheckIntervalTicks;

            if (expiryTick > 0 && currentTick >= expiryTick)
            {
                ResolveFailure("failureTimeout");
                return;
            }

            if (!operationLaunched || targetOfficer == null)
            {
                return;
            }

            if (targetOfficer.Dead)
            {
                ResolveFailure("failureTargetKilled");
                return;
            }

            if (extractionRequested && TickHomeExtraction(currentTick))
            {
                return;
            }

            if (targetOfficer.Destroyed && !extractionTargetDeparted)
            {
                ResolveFailure("failureTargetLost");
                return;
            }

            bool presentOnMissionMap = HasMap
                && TokraJaffaOfficerCaptureMissionUtility
                    .IsPawnPresentOnMapIncludingCarried(
                        targetOfficer,
                        Map);
            Map playerHomeMap = TokraJaffaOfficerCaptureMissionUtility
                .FindPlayerHomeMapContaining(targetOfficer);
            bool presentOnPlayerHomeMap = playerHomeMap != null;
            Caravan targetCaravan = TokraJaffaOfficerCaptureMissionUtility
                .FindPlayerCaravanContaining(targetOfficer);

            if (targetCaravan != null
                && targetOfficer.IsPrisonerOfColony)
            {
                TokraJaffaOfficerCaptureMissionUtility
                    .EnsureTargetTransferRestraint(targetOfficer);
                GameComponent_TokraOrganicOperationManager
                    .NotifyJaffaOfficerCaptured(this);
            }
            else if (presentOnPlayerHomeMap)
            {
                if (targetOfficer.IsPrisonerOfColony)
                {
                    GameComponent_TokraOrganicOperationManager
                        .NotifyJaffaOfficerCaptured(this);
                }

                if (!extractionTeamSpawned)
                {
                    TokraJaffaOfficerCaptureMissionUtility
                        .ClearTargetTransferRestraint(targetOfficer);
                }
            }

            if (presentOnMissionMap
                || presentOnPlayerHomeMap
                || targetCaravan != null
                || extractionTargetDeparted)
            {
                targetMissingSinceTick = 0;
                return;
            }

            if (targetMissingSinceTick <= 0)
            {
                targetMissingSinceTick = currentTick;
                return;
            }

            if (currentTick - targetMissingSinceTick
                < TokraJaffaOfficerCaptureMissionUtility
                    .TargetLossGraceTicks)
            {
                return;
            }

            ResolveFailure("failureTargetLost");
        }

        public override IEnumerable<FloatMenuOption> GetFloatMenuOptions(
            Caravan caravan)
        {
            IEnumerable<FloatMenuOption> baseOptions = base.GetFloatMenuOptions(
                caravan);

            if (baseOptions != null)
            {
                foreach (FloatMenuOption option in baseOptions)
                {
                    yield return option;
                }
            }

            if (!IsValidPlayerCaravan(caravan) || managerResolved)
            {
                yield break;
            }

            foreach (FloatMenuOption option in
                CaravanArrivalAction_TokraJaffaOfficerCaptureSite
                    .GetFloatMenuOptions(caravan, this))
            {
                yield return option;
            }
        }

        public override string GetInspectString()
        {
            string baseInspectString = base.GetInspectString();
            string targetLabel = targetOfficer?.LabelShortCap
                ?? "GR_TokraJaffaOfficerCapture_TargetUnknown"
                    .Translate()
                    .ToString();
            string details;

            if (managerResolved)
            {
                details = missionSucceeded
                    ? "GR_TokraJaffaOfficerCapture_InspectSucceeded"
                        .Translate()
                    : "GR_TokraJaffaOfficerCapture_InspectFailed"
                        .Translate();
            }
            else if (extractionTargetDeparted)
            {
                details = "GR_TokraJaffaOfficerCapture_InspectExtractionLeaving"
                    .Translate(targetLabel);
            }
            else if (extractionTeamSpawned)
            {
                details = "GR_TokraJaffaOfficerCapture_InspectExtractionActive"
                    .Translate(targetLabel);
            }
            else if (extractionRequested)
            {
                details = "GR_TokraJaffaOfficerCapture_InspectExtractionInbound"
                    .Translate(targetLabel, GetExtractionHoursString());
            }
            else if (TokraJaffaOfficerCaptureMissionUtility
                .FindPlayerHomeMapContaining(targetOfficer) != null
                && targetOfficer?.IsPrisonerOfColony == true)
            {
                details = "GR_TokraJaffaOfficerCapture_InspectAwaitingCall"
                    .Translate(targetLabel, GetRemainingHoursString());
            }
            else if (TokraJaffaOfficerCaptureMissionUtility
                .FindPlayerCaravanContaining(targetOfficer) != null)
            {
                details = "GR_TokraJaffaOfficerCapture_InspectReturnHome"
                    .Translate(targetLabel, GetRemainingHoursString());
            }
            else if (operationLaunched || HasMap)
            {
                details = "GR_TokraJaffaOfficerCapture_InspectActive"
                    .Translate(targetLabel, GetRemainingHoursString());
            }
            else
            {
                details = "GR_TokraJaffaOfficerCapture_InspectPending"
                    .Translate(GetRemainingHoursString());
            }

            return string.IsNullOrEmpty(baseInspectString)
                ? details
                : baseInspectString + "\n" + details;
        }

        public override bool ShouldRemoveMapNow(
            out bool alsoRemoveWorldObject)
        {
            alsoRemoveWorldObject = managerResolved;

            if (!HasMap)
            {
                return false;
            }

            MapComponent_TokraJaffaOfficerCaptureMission component = Map
                .GetComponent<MapComponent_TokraJaffaOfficerCaptureMission>();

            if (component == null)
            {
                return false;
            }

            bool targetExtracted = targetOfficer == null
                || targetOfficer.Dead
                || TokraJaffaOfficerCaptureMissionUtility
                    .FindPlayerCaravanContaining(targetOfficer) != null
                || TokraJaffaOfficerCaptureMissionUtility
                    .FindPlayerHomeMapContaining(targetOfficer) != null;

            if (!component.CanReformCaravan
                && !component.OperationResolved
                && !targetExtracted)
            {
                return false;
            }

            if (TransporterUtility.IncomingTransporterPreventingMapRemoval(Map))
            {
                return false;
            }

            bool playerPawnStillPresent = Map.mapPawns.AllPawnsSpawned
                .Any(pawn => pawn?.Faction == Faction.OfPlayer);

            if (playerPawnStillPresent)
            {
                return false;
            }

            if (!component.OperationResolved && !targetExtracted)
            {
                return false;
            }

            GameComponent_TokraOrganicOperationManager
                .NotifyJaffaOfficerCaptureSiteEvacuated(this);
            alsoRemoveWorldObject = true;
            return true;
        }

        public FloatMenuAcceptanceReport CanVisit(Caravan caravan)
        {
            if (!IsValidPlayerCaravan(caravan))
            {
                return false;
            }

            if (managerResolved)
            {
                return FloatMenuAcceptanceReport.WithFailMessage(
                    "GR_TokraJaffaOfficerCapture_AlreadyResolved"
                        .Translate());
            }

            if (operationLaunched && !HasMap)
            {
                return FloatMenuAcceptanceReport.WithFailMessage(
                    "GR_TokraJaffaOfficerCapture_ReturnToColony"
                        .Translate());
            }

            if (operationLaunched && HasMap)
            {
                return FloatMenuAcceptanceReport.WithFailMessage(
                    "GR_TokraJaffaOfficerCapture_AlreadyEntered"
                        .Translate());
            }

            return true;
        }

        public string GetExtractionRequestDisabledReason(Map map)
        {
            if (managerResolved)
            {
                return "GR_TokraJaffaOfficerCapture_AlreadyResolved"
                    .Translate()
                    .ToString();
            }

            if (extractionRequested)
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

            if (targetOfficer == null
                || targetOfficer.Dead
                || targetOfficer.Destroyed)
            {
                return "GR_TokraJaffaOfficerCapture_TargetUnavailable"
                    .Translate()
                    .ToString();
            }

            if (!TokraJaffaOfficerCaptureMissionUtility
                .IsPawnPresentOnMapIncludingCarried(targetOfficer, map))
            {
                return "GR_TokraJaffaOfficerCapture_ExtractionTargetNotPresent"
                    .Translate(targetOfficer.LabelShortCap)
                    .ToString();
            }

            if (!targetOfficer.IsPrisonerOfColony)
            {
                return "GR_TokraJaffaOfficerCapture_TargetNotPrisoner"
                    .Translate(targetOfficer.LabelShortCap)
                    .ToString();
            }

            return null;
        }

        public bool TryRequestHomeExtraction(Map map, Pawn operatorPawn)
        {
            string disabledReason = GetExtractionRequestDisabledReason(map);

            if (!string.IsNullOrEmpty(disabledReason))
            {
                Thing rejectionTarget = operatorPawn != null
                    ? (Thing)operatorPawn
                    : targetOfficer;
                Messages.Message(
                    disabledReason,
                    rejectionTarget,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            extractionRequested = true;
            extractionMapId = map.uniqueID;
            extractionArrivalTick = currentTick + Rand.RangeInclusive(
                TokraJaffaOfficerCaptureMissionUtility
                    .GetExtractionMinimumDelayTicks(),
                TokraJaffaOfficerCaptureMissionUtility
                    .GetExtractionMaximumDelayTicks());
            nextExtractionRetryTick = extractionArrivalTick;
            extractionWaitingMessageSent = false;
            handoffInProgress = false;
            handoffCompletionTick = 0;
            handoffCaravan = null;

            GameComponent_TokraOrganicOperationManager
                .NotifyJaffaOfficerCaptured(this);

            Thing messageTarget = operatorPawn != null
                ? (Thing)operatorPawn
                : targetOfficer;
            Messages.Message(
                "GR_TokraJaffaOfficerCapture_ExtractionRequested".Translate(
                    targetOfficer.LabelShortCap,
                    GetExtractionHoursString()),
                messageTarget,
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            GR_Log.Message(
                "Requested Tok'ra home-map extraction for Jaffa officer "
                + $"{targetOfficer.LabelShortCap}; map {map.uniqueID}; "
                + $"arrival tick {extractionArrivalTick}.");
            return true;
        }

        public void EnterFromCaravan(Caravan caravan)
        {
            if (!IsValidPlayerCaravanAtSite(caravan))
            {
                Messages.Message(
                    "GR_TokraJaffaOfficerCapture_RequiresCaravan".Translate(),
                    this,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            FloatMenuAcceptanceReport acceptance = CanVisit(caravan);

            if (!acceptance)
            {
                if (!acceptance.FailMessage.NullOrEmpty())
                {
                    Messages.Message(
                        acceptance.FailMessage,
                        this,
                        MessageTypeDefOf.RejectInput,
                        historical: false);
                }

                return;
            }

            bool generatedNewMap = !HasMap;
            Map map = GetOrGenerateMapUtility.GetOrGenerateMap(
                Tile,
                new IntVec3(missionMapSize, 1, missionMapSize),
                def);

            TokraJaffaOfficerCaptureMissionUtility
                .EnsureMissionMapInitialized(map, this);
            operationLaunched = true;

            if (generatedNewMap)
            {
                Find.TickManager.Notify_GeneratedPotentiallyHostileMap();
            }

            CaravanEnterMapUtility.Enter(
                caravan,
                map,
                CaravanEnterMode.Edge,
                CaravanDropInventoryMode.DoNotDrop,
                draftColonists: true);
        }

        public bool DebugExpireOperation()
        {
            if (managerResolved)
            {
                return false;
            }

            ResolveFailure("failureTimeout");
            return true;
        }

        public void PrepareManagerResolution(bool succeeded)
        {
            if (managerResolved)
            {
                return;
            }

            managerResolved = true;
            missionSucceeded = succeeded;
            handoffInProgress = false;
            handoffCompletionTick = 0;
            handoffCaravan = null;

            if (!succeeded)
            {
                TokraJaffaOfficerCaptureMissionUtility
                    .ClearTargetTransferRestraint(targetOfficer);
            }
        }

        public void NotifyManagerResolved(bool succeeded)
        {
            if (!succeeded)
            {
                Map extractionMap = TokraJaffaOfficerCaptureMissionUtility
                    .FindPlayerHomeMapById(extractionMapId);
                CancelExtractionPickupOrder();
                TokraJaffaOfficerCaptureMissionUtility
                    .ClearTargetTransferRestraint(targetOfficer);

                if (extractionMap != null)
                {
                    OrderRemainingExtractionTeamDeparture(extractionMap);
                }
            }

            if (!managerResolved)
            {
                PrepareManagerResolution(succeeded);
            }
            else if (succeeded)
            {
                missionSucceeded = true;
            }

            if (HasMap)
            {
                Map.GetComponent<
                        MapComponent_TokraJaffaOfficerCaptureMission>()
                    ?.NotifyManagerResolved(succeeded);
                return;
            }

            if (!Destroyed)
            {
                Destroy();
            }
        }

        private bool TickHomeExtraction(int currentTick)
        {
            Map extractionMap = TokraJaffaOfficerCaptureMissionUtility
                .FindPlayerHomeMapById(extractionMapId);

            if (extractionMap == null)
            {
                return false;
            }

            bool targetPresent = TokraJaffaOfficerCaptureMissionUtility
                .IsPawnPresentOnMapIncludingCarried(
                    targetOfficer,
                    extractionMap);
            Pawn currentCarrier = FindExtractionCarrierHoldingTarget(
                extractionMap);

            if (currentCarrier != null)
            {
                extractionCarrier = currentCarrier;
                extractionCarrierHadTarget = true;
            }

            if (extractionCarrierHadTarget && !targetPresent)
            {
                if (!extractionTargetDeparted)
                {
                    extractionTargetDeparted = true;
                    extractionPickupOrdered = false;
                    TokraJaffaOfficerCaptureMissionUtility
                        .ClearTargetTransferRestraint(targetOfficer);
                    Messages.Message(
                        "GR_TokraJaffaOfficerCapture_ExtractionTargetDeparted"
                            .Translate(targetOfficer.LabelShortCap),
                        extractionMap.Parent,
                        MessageTypeDefOf.PositiveEvent,
                        historical: true);
                }

                OrderRemainingExtractionTeamDeparture(extractionMap);

                if (!HasActiveExtractionTeamMember(extractionMap))
                {
                    ResolveSuccess();
                    return true;
                }

                return false;
            }

            if (currentCarrier != null)
            {
                return false;
            }

            if (!extractionTeamSpawned)
            {
                if (currentTick < Math.Max(
                        extractionArrivalTick,
                        nextExtractionRetryTick))
                {
                    return false;
                }

                if (!targetPresent || !targetOfficer.IsPrisonerOfColony)
                {
                    if (!extractionWaitingMessageSent)
                    {
                        Messages.Message(
                            "GR_TokraJaffaOfficerCapture_ExtractionWaiting"
                                .Translate(targetOfficer.LabelShortCap),
                            extractionMap.Parent,
                            MessageTypeDefOf.NegativeEvent,
                            historical: false);
                        extractionWaitingMessageSent = true;
                    }

                    nextExtractionRetryTick = currentTick
                        + TokraJaffaOfficerCaptureMissionUtility
                            .GetExtractionRetryTicks();
                    return false;
                }

                if (!TrySpawnExtractionTeam(extractionMap))
                {
                    nextExtractionRetryTick = currentTick
                        + TokraJaffaOfficerCaptureMissionUtility
                            .GetExtractionRetryTicks();
                    return false;
                }

                extractionWaitingMessageSent = false;
            }

            if (!targetPresent)
            {
                return false;
            }

            if (!targetOfficer.IsPrisonerOfColony)
            {
                CancelExtractionPickupOrder();
                TokraJaffaOfficerCaptureMissionUtility
                    .ClearTargetTransferRestraint(targetOfficer);
                return false;
            }

            TokraJaffaOfficerCaptureMissionUtility
                .EnsureTargetTransferRestraint(targetOfficer);

            if (!HasActiveExtractionTeamMember(extractionMap))
            {
                ResetExtractionTeamForRetry(currentTick);
                return false;
            }

            TryAssignExtractionPickup(extractionMap);
            return false;
        }

        private bool TrySpawnExtractionTeam(Map map)
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
                attackTargets = new List<Thing> { targetOfficer }
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
            extractionTeam = generatedPawns
                .Where(pawn => pawn != null
                    && !pawn.Destroyed
                    && pawn.Spawned
                    && pawn.Map == map)
                .ToList();

            if (extractionTeam.Count == 0)
            {
                return false;
            }

            extractionTeamSpawned = true;
            extractionPickupOrdered = false;
            extractionCarrier = null;
            extractionCarrierHadTarget = false;
            extractionTeamDepartureOrdered = false;
            TokraJaffaOfficerCaptureMissionUtility
                .EnsureTargetTransferRestraint(targetOfficer);

            Messages.Message(
                "GR_TokraJaffaOfficerCapture_ExtractionTeamArrived".Translate(
                    targetOfficer.LabelShortCap),
                extractionTeam[0],
                MessageTypeDefOf.PositiveEvent,
                historical: true);
            return true;
        }

        private void TryAssignExtractionPickup(Map map)
        {
            Pawn carrierHoldingTarget = FindExtractionCarrierHoldingTarget(map);

            if (carrierHoldingTarget != null)
            {
                extractionCarrier = carrierHoldingTarget;
                extractionCarrierHadTarget = true;
                return;
            }

            if (extractionCarrier != null
                && extractionCarrier.Spawned
                && extractionCarrier.Map == map
                && extractionCarrier.jobs?.curJob?.def == JobDefOf.Kidnap
                && extractionCarrier.jobs.curJob.targetA.Thing == targetOfficer)
            {
                return;
            }

            extractionPickupOrdered = false;
            extractionCarrier = FindAvailableExtractionCarrier(map);

            if (extractionCarrier == null
                || !RCellFinder.TryFindBestExitSpot(
                    extractionCarrier,
                    out IntVec3 exitCell))
            {
                return;
            }

            Job job = JobMaker.MakeJob(JobDefOf.Kidnap);
            job.targetA = targetOfficer;
            job.targetB = exitCell;
            job.count = 1;

            if (extractionCarrier.jobs.TryTakeOrderedJob(job))
            {
                extractionPickupOrdered = true;
                Messages.Message(
                    "GR_TokraJaffaOfficerCapture_ExtractionPickupStarted"
                        .Translate(targetOfficer.LabelShortCap),
                    extractionCarrier,
                    MessageTypeDefOf.NeutralEvent,
                    historical: false);
            }
        }

        private Pawn FindAvailableExtractionCarrier(Map map)
        {
            return extractionTeam.FirstOrDefault(pawn => pawn != null
                && !pawn.Dead
                && !pawn.Destroyed
                && pawn.Spawned
                && pawn.Map == map
                && !pawn.Downed
                && pawn.carryTracker?.CarriedThing == null);
        }

        private Pawn FindExtractionCarrierHoldingTarget(Map map)
        {
            return extractionTeam.FirstOrDefault(pawn => pawn != null
                && !pawn.Dead
                && !pawn.Destroyed
                && pawn.Spawned
                && pawn.Map == map
                && pawn.carryTracker?.CarriedThing == targetOfficer);
        }

        private bool HasActiveExtractionTeamMember(Map map)
        {
            return extractionTeam.Any(pawn => pawn != null
                && !pawn.Dead
                && !pawn.Destroyed
                && pawn.Spawned
                && pawn.Map == map);
        }

        private void CancelExtractionPickupOrder()
        {
            if (extractionCarrier?.Spawned == true
                && extractionCarrier.jobs?.curJob?.def == JobDefOf.Kidnap)
            {
                extractionCarrier.jobs.EndCurrentJob(
                    JobCondition.InterruptForced);
            }

            extractionPickupOrdered = false;
            extractionCarrier = null;
            extractionCarrierHadTarget = false;
        }

        private void ResetExtractionTeamForRetry(int currentTick)
        {
            extractionTeamSpawned = false;
            extractionPickupOrdered = false;
            extractionCarrier = null;
            extractionCarrierHadTarget = false;
            extractionTeamDepartureOrdered = false;
            extractionTeam = new List<Pawn>();
            nextExtractionRetryTick = currentTick
                + TokraJaffaOfficerCaptureMissionUtility
                    .GetExtractionRetryTicks();
            TokraJaffaOfficerCaptureMissionUtility
                .ClearTargetTransferRestraint(targetOfficer);
        }

        private void OrderRemainingExtractionTeamDeparture(Map map)
        {
            if (extractionTeamDepartureOrdered)
            {
                return;
            }

            List<Pawn> remaining = extractionTeam
                .Where(pawn => pawn != null
                    && !pawn.Dead
                    && !pawn.Destroyed
                    && pawn.Spawned
                    && pawn.Map == map)
                .ToList();

            if (remaining.Count == 0)
            {
                extractionTeamDepartureOrdered = true;
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
            extractionTeamDepartureOrdered = true;
        }

        private void ResolveSuccess()
        {
            PrepareManagerResolution(true);

            if (!GameComponent_TokraOrganicOperationManager
                .NotifyJaffaOfficerCaptureSiteResolved(
                    this,
                    succeeded: true,
                    failureTextId: null))
            {
                NotifyManagerResolved(true);
                return;
            }

            NotifyManagerResolved(true);
        }

        private void ResolveFailure(string failureTextId)
        {
            Map extractionMap = TokraJaffaOfficerCaptureMissionUtility
                .FindPlayerHomeMapById(extractionMapId);
            CancelExtractionPickupOrder();
            TokraJaffaOfficerCaptureMissionUtility
                .ClearTargetTransferRestraint(targetOfficer);

            if (extractionMap != null)
            {
                OrderRemainingExtractionTeamDeparture(extractionMap);
            }

            PrepareManagerResolution(false);

            if (!GameComponent_TokraOrganicOperationManager
                .NotifyJaffaOfficerCaptureSiteResolved(
                    this,
                    succeeded: false,
                    failureTextId: failureTextId))
            {
                NotifyManagerResolved(false);
                return;
            }

            NotifyManagerResolved(false);
        }

        private bool IsValidPlayerCaravan(Caravan caravan)
        {
            return caravan != null
                && !caravan.Destroyed
                && caravan.Faction == Faction.OfPlayer;
        }

        private bool IsValidPlayerCaravanAtSite(Caravan caravan)
        {
            return IsValidPlayerCaravan(caravan) && caravan.Tile == Tile;
        }

        private string GetRemainingHoursString()
        {
            return Math.Ceiling(RemainingTicks / 2500f).ToString("0");
        }

        private string GetExtractionHoursString()
        {
            int currentTick = Find.TickManager?.TicksGame ?? 0;
            int remainingTicks = Math.Max(
                0,
                extractionArrivalTick - currentTick);
            return Math.Max(1, Math.Ceiling(remainingTicks / 2500f))
                .ToString("0");
        }
    }

    public sealed class
        WorldObjectCompProperties_TokraJaffaOfficerCaptureFormCaravan
        : WorldObjectCompProperties_FormCaravan
    {
        public WorldObjectCompProperties_TokraJaffaOfficerCaptureFormCaravan()
        {
            compClass = typeof(
                WorldObjectComp_TokraJaffaOfficerCaptureFormCaravan);
        }
    }

    public sealed class WorldObjectComp_TokraJaffaOfficerCaptureFormCaravan
        : FormCaravanComp
    {
        private bool CanReform
        {
            get
            {
                WorldObject_TokraJaffaOfficerCaptureSite site
                    = parent as WorldObject_TokraJaffaOfficerCaptureSite;

                if (site?.HasMap != true)
                {
                    return false;
                }

                MapComponent_TokraJaffaOfficerCaptureMission component
                    = site.Map.GetComponent<
                        MapComponent_TokraJaffaOfficerCaptureMission>();

                return component != null
                    && (component.CanReformCaravan
                        || component.OperationResolved);
            }
        }

        public override void CompTickInterval(int delta)
        {
            if (CanReform)
            {
                base.CompTickInterval(delta);
            }
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            if (!CanReform)
            {
                yield break;
            }

            foreach (Gizmo gizmo in base.GetGizmos())
            {
                yield return gizmo;
            }
        }
    }
}
