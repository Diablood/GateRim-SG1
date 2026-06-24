using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public sealed class MapComponent_TokraJaffaOfficerCaptureMission
        : MapComponent
    {
        private WorldObject_TokraJaffaOfficerCaptureSite parentSite;
        private Pawn targetOfficer;
        private List<Pawn> escorts = new List<Pawn>();
        private bool initialized;
        private bool operationResolved;
        private bool missionSucceeded;
        private bool captureNotified;
        private int nextCheckTick;

        public MapComponent_TokraJaffaOfficerCaptureMission(Map map)
            : base(map)
        {
        }

        public bool Initialized => initialized;
        public bool OperationResolved => operationResolved;
        public bool MissionSucceeded => missionSucceeded;
        public Pawn TargetOfficer => targetOfficer;

        public bool CanReformCaravan
            => initialized
                && !operationResolved
                && TokraJaffaOfficerCaptureMissionUtility
                    .IsTargetReadyForCaravanExtraction(targetOfficer, map)
                && !TokraJaffaOfficerCaptureMissionUtility
                    .HasActiveHostiles(map);

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_References.Look(ref parentSite, "captureParentSite");
            Scribe_References.Look(ref targetOfficer, "captureTargetOfficer");
            Scribe_Collections.Look(
                ref escorts,
                "captureEscorts",
                LookMode.Reference);
            Scribe_Values.Look(ref initialized, "captureInitialized", false);
            Scribe_Values.Look(
                ref operationResolved,
                "captureOperationResolved",
                false);
            Scribe_Values.Look(
                ref missionSucceeded,
                "captureMissionSucceeded",
                false);
            Scribe_Values.Look(
                ref captureNotified,
                "captureNotified",
                false);
            Scribe_Values.Look(ref nextCheckTick, "captureNextCheckTick", 0);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                escorts = escorts ?? new List<Pawn>();
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

            if (currentTick < nextCheckTick)
            {
                return;
            }

            nextCheckTick = currentTick
                + TokraJaffaOfficerCaptureMissionUtility
                    .CaptureStateCheckIntervalTicks;

            if (targetOfficer == null
                || targetOfficer.Dead
                || targetOfficer.Destroyed)
            {
                ResolveFailure("failureTargetKilled");
                return;
            }

            bool targetSecured = TokraJaffaOfficerCaptureMissionUtility
                .IsTargetSecured(targetOfficer);
            bool targetBeingCarried = TokraJaffaOfficerCaptureMissionUtility
                .IsTargetBeingCarriedByPlayerPawn(targetOfficer, map);
            bool captureInProgress = targetSecured || targetBeingCarried;

            if (captureInProgress)
            {
                TokraJaffaOfficerCaptureMissionUtility
                    .EnsureTargetTransferRestraint(targetOfficer);
            }

            if (!captureNotified && captureInProgress)
            {
                captureNotified = true;
                GameComponent_TokraOrganicOperationManager
                    .NotifyJaffaOfficerCaptured(parentSite);
                Messages.Message(
                    "GR_TokraJaffaOfficerCapture_TargetSecured".Translate(
                        targetOfficer.LabelShortCap),
                    targetOfficer,
                    MessageTypeDefOf.PositiveEvent,
                    historical: true);
            }
        }

        public void Initialize(
            WorldObject_TokraJaffaOfficerCaptureSite parent,
            Pawn target,
            List<Pawn> spawnedEscorts)
        {
            parentSite = parent;
            targetOfficer = target;
            escorts = spawnedEscorts ?? new List<Pawn>();
            initialized = true;
            operationResolved = false;
            missionSucceeded = false;
            captureNotified = false;
            nextCheckTick = Find.TickManager?.TicksGame ?? 0;
        }

        public void MarkInitializationFailed(
            WorldObject_TokraJaffaOfficerCaptureSite parent)
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

        private void ResolveFailure(string failureTextId)
        {
            operationResolved = true;
            missionSucceeded = false;
            TokraJaffaOfficerCaptureMissionUtility
                .ClearTargetTransferRestraint(targetOfficer);

            if (parentSite != null)
            {
                GameComponent_TokraOrganicOperationManager
                    .NotifyJaffaOfficerCaptureSiteResolved(
                        parentSite,
                        succeeded: false,
                        failureTextId: failureTextId);
            }
        }
    }
}
