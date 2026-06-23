using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public sealed class MapComponent_TokraIntroductionArtifactMission
        : MapComponent
    {
        private const int StateCheckIntervalTicks = 250;

        private bool initialized;
        private bool siteSecured;
        private bool operationResolved;
        private bool missionSucceeded;
        private bool securedMessageSent;
        private WorldObject_TokraIntroductionArtifactSite parentSite;
        private Thing artifact;
        private List<Pawn> defenders = new List<Pawn>();
        private IntVec3 preferredEntryCell = IntVec3.Invalid;
        private int nextStateCheckTick;

        public MapComponent_TokraIntroductionArtifactMission(Map map) : base(map)
        {
        }

        public bool Initialized => initialized;
        public bool SiteSecured => siteSecured;
        public bool OperationResolved => operationResolved;
        public IntVec3 PreferredEntryCell => preferredEntryCell;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(
                ref initialized,
                "introductionInitialized",
                false);
            Scribe_Values.Look(
                ref siteSecured,
                "introductionSiteSecured",
                false);
            Scribe_Values.Look(
                ref operationResolved,
                "introductionOperationResolved",
                false);
            Scribe_Values.Look(
                ref missionSucceeded,
                "introductionMissionSucceeded",
                false);
            Scribe_Values.Look(
                ref securedMessageSent,
                "introductionSecuredMessageSent",
                false);
            Scribe_References.Look(
                ref parentSite,
                "introductionParentSite");
            Scribe_References.Look(
                ref artifact,
                "introductionArtifact");
            Scribe_Collections.Look(
                ref defenders,
                "introductionDefenders",
                LookMode.Reference);
            Scribe_Values.Look(
                ref preferredEntryCell,
                "introductionPreferredEntryCell",
                IntVec3.Invalid);
            Scribe_Values.Look(
                ref nextStateCheckTick,
                "introductionNextStateCheckTick",
                0);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                defenders = defenders ?? new List<Pawn>();
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

            nextStateCheckTick = currentTick + StateCheckIntervalTicks;

            if (artifact == null || artifact.Destroyed)
            {
                ResolveFailure("artifactLost");
                return;
            }

            if (!siteSecured
                && !TokraIntroductionArtifactMissionUtility
                    .HasActiveHostiles(map))
            {
                siteSecured = true;
                SendSecuredMessage();
            }
        }

        public void Initialize(
            WorldObject_TokraIntroductionArtifactSite parent,
            Thing missionArtifact,
            List<Pawn> spawnedDefenders,
            IntVec3 entryCell)
        {
            parentSite = parent;
            artifact = missionArtifact;
            defenders = spawnedDefenders ?? new List<Pawn>();
            preferredEntryCell = entryCell;
            initialized = true;
            siteSecured = false;
            operationResolved = false;
            missionSucceeded = false;
            securedMessageSent = false;
            nextStateCheckTick = Find.TickManager?.TicksGame ?? 0;
        }

        public void MarkInitializationFailed(
            WorldObject_TokraIntroductionArtifactSite parent)
        {
            parentSite = parent;
            initialized = true;
            ResolveFailure("siteLost");
        }

        public bool DebugDestroyTrackedArtifact()
        {
            if (!initialized
                || operationResolved
                || artifact == null
                || artifact.Destroyed)
            {
                return false;
            }

            artifact.Destroy(DestroyMode.Vanish);
            nextStateCheckTick = 0;
            return true;
        }

        public void NotifyArcResolved(bool succeeded)
        {
            operationResolved = true;
            missionSucceeded = succeeded;

            if (succeeded)
            {
                siteSecured = true;
            }
        }

        private void SendSecuredMessage()
        {
            if (securedMessageSent)
            {
                return;
            }

            securedMessageSent = true;
            Messages.Message(
                "GR_TokraIntroduction_SiteSecured".Translate(),
                artifact,
                MessageTypeDefOf.PositiveEvent,
                historical: true);
        }

        private void ResolveFailure(string failureTextId)
        {
            if (operationResolved)
            {
                return;
            }

            operationResolved = true;
            missionSucceeded = false;

            if (!GameComponent_TokraIntroductionArc.NotifySiteFailed(
                    parentSite,
                    failureTextId))
            {
                parentSite?.NotifyArcResolved(false);
            }
        }
    }
}
