using System;
using System.Collections.Generic;
using GateRimSG1.Missions;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    public sealed class WorldObject_TokraIntroductionArtifactSite : MapParent
    {
        private int homeMapId = -1;
        private string missionDefName;
        private float scaledThreatPoints;
        private int expiryTick;
        private int missionMapSize = 120;
        private bool mapEntered;
        private bool arcResolved;
        private bool missionSucceeded;
        private bool deadlineWarningSent;

        public float ScaledThreatPoints => scaledThreatPoints;
        public int ExpiryTick => expiryTick;
        public bool DeadlineWarningSent => deadlineWarningSent;
        public bool MapEntered => mapEntered;
        public bool ArcResolved => arcResolved;

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
            deadlineWarningSent = false;
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref homeMapId, "introductionHomeMapId", -1);
            Scribe_Values.Look(
                ref missionDefName,
                "introductionMissionDefName");
            Scribe_Values.Look(
                ref scaledThreatPoints,
                "introductionScaledThreatPoints",
                0f);
            Scribe_Values.Look(
                ref expiryTick,
                "introductionExpiryTick",
                0);
            Scribe_Values.Look(
                ref missionMapSize,
                "introductionMissionMapSize",
                120);
            Scribe_Values.Look(
                ref mapEntered,
                "introductionMapEntered",
                false);
            Scribe_Values.Look(
                ref arcResolved,
                "introductionArcResolved",
                false);
            Scribe_Values.Look(
                ref missionSucceeded,
                "introductionMissionSucceeded",
                false);
            Scribe_Values.Look(
                ref deadlineWarningSent,
                "introductionDeadlineWarningSent",
                false);
        }

        protected override void Tick()
        {
            base.Tick();

            if (arcResolved || expiryTick <= 0)
            {
                return;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;

            if (!deadlineWarningSent
                && currentTick < expiryTick
                && expiryTick - currentTick <= ResolveDeadlineWarningTicks())
            {
                deadlineWarningSent = true;
                Messages.Message(
                    "GR_TokraIntroduction_DeadlineWarning".Translate(
                        GetRemainingHoursString()),
                    this,
                    MessageTypeDefOf.ThreatSmall,
                    historical: true);
            }

            if (currentTick >= expiryTick)
            {
                if (!GameComponent_TokraIntroductionArc.NotifySiteFailed(
                        this,
                        "timeout"))
                {
                    NotifyArcResolved(false);
                }
            }
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

            if (!IsValidPlayerCaravan(caravan) || arcResolved)
            {
                yield break;
            }

            foreach (FloatMenuOption option in
                CaravanArrivalAction_TokraIntroductionArtifactSite
                    .GetFloatMenuOptions(caravan, this))
            {
                yield return option;
            }
        }

        public override string GetInspectString()
        {
            string baseInspectString = base.GetInspectString();
            string missionInspectString;

            if (arcResolved)
            {
                missionInspectString = missionSucceeded
                    ? "GR_TokraIntroduction_InspectCompleted".Translate()
                    : "GR_TokraIntroduction_InspectFailed".Translate();
            }
            else if (HasMap)
            {
                MapComponent_TokraIntroductionArtifactMission component
                    = Map.GetComponent<
                        MapComponent_TokraIntroductionArtifactMission>();
                missionInspectString = component != null
                        && component.SiteSecured
                    ? "GR_TokraIntroduction_InspectSecured".Translate()
                    : "GR_TokraIntroduction_InspectActive".Translate();
            }
            else
            {
                missionInspectString
                    = "GR_TokraIntroduction_InspectPending".Translate(
                        GetRemainingHoursString());
            }

            return string.IsNullOrEmpty(baseInspectString)
                ? missionInspectString
                : baseInspectString + "\n" + missionInspectString;
        }

        public override bool ShouldRemoveMapNow(
            out bool alsoRemoveWorldObject)
        {
            alsoRemoveWorldObject = false;

            if (!HasMap || !arcResolved)
            {
                return false;
            }

            if (Map.mapPawns.AnyPawnBlockingMapRemoval
                || TransporterUtility.IncomingTransporterPreventingMapRemoval(
                    Map))
            {
                return false;
            }

            alsoRemoveWorldObject = true;
            return true;
        }

        public FloatMenuAcceptanceReport CanEnter(Caravan caravan)
        {
            if (!IsValidPlayerCaravan(caravan))
            {
                return false;
            }

            if (arcResolved)
            {
                return FloatMenuAcceptanceReport.WithFailMessage(
                    "GR_TokraIntroduction_SiteResolved".Translate());
            }

            return true;
        }

        public void EnterFromCaravan(Caravan caravan)
        {
            if (!IsValidPlayerCaravanAtSite(caravan))
            {
                Messages.Message(
                    "GR_TokraIntroduction_EnterRequiresCaravan".Translate(),
                    this,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            FloatMenuAcceptanceReport acceptance = CanEnter(caravan);

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
                new IntVec3(
                    missionMapSize,
                    1,
                    missionMapSize),
                def);

            TokraIntroductionArtifactMissionUtility
                .EnsureMissionMapInitialized(map, this);
            mapEntered = true;

            if (generatedNewMap)
            {
                Find.TickManager.Notify_GeneratedPotentiallyHostileMap();
            }

            MapComponent_TokraIntroductionArtifactMission component
                = map.GetComponent<
                    MapComponent_TokraIntroductionArtifactMission>();
            IntVec3 preferredEntry = component?.PreferredEntryCell
                ?? IntVec3.Invalid;
            int preferredRadius = ResolvePreferredEntryRadius();
            Predicate<IntVec3> entryValidator = preferredEntry.IsValid
                ? (Predicate<IntVec3>)(cell => IsNearPreferredEntry(
                    cell,
                    preferredEntry,
                    preferredRadius))
                : null;

            CaravanEnterMapUtility.Enter(
                caravan,
                map,
                CaravanEnterMode.Edge,
                CaravanDropInventoryMode.DoNotDrop,
                true,
                entryValidator);
        }

        public bool DebugMoveToDeadlineWarningWindow()
        {
            if (arcResolved || Find.TickManager == null)
            {
                return false;
            }

            int warningTicks = ResolveDeadlineWarningTicks();
            expiryTick = Find.TickManager.TicksGame
                + Math.Max(2, warningTicks - 1);
            deadlineWarningSent = false;
            return true;
        }

        public bool DebugExpireNow()
        {
            if (arcResolved || Find.TickManager == null)
            {
                return false;
            }

            expiryTick = Find.TickManager.TicksGame;
            return true;
        }

        public void NotifyArcResolved(bool succeeded)
        {
            if (arcResolved)
            {
                return;
            }

            arcResolved = true;
            missionSucceeded = succeeded;

            if (HasMap)
            {
                Map.GetComponent<MapComponent_TokraIntroductionArtifactMission>()
                    ?.NotifyArcResolved(succeeded);
                return;
            }

            if (!Destroyed)
            {
                Destroy();
            }
        }

        private int ResolveDeadlineWarningTicks()
        {
            GateRimMissionIntroductionDef profile
                = GR_DefOf.SG1_TokraIntroductionArtifactMission
                    ?.introduction;

            return Math.Max(1, profile?.deadlineWarningTicks ?? 60000);
        }

        private int ResolvePreferredEntryRadius()
        {
            GateRimMissionIntroductionDef profile
                = GR_DefOf.SG1_TokraIntroductionArtifactMission
                    ?.introduction;

            return Math.Max(4, profile?.preferredEntryRadius ?? 18);
        }

        private static bool IsNearPreferredEntry(
            IntVec3 cell,
            IntVec3 preferredEntry,
            int radius)
        {
            int deltaX = cell.x - preferredEntry.x;
            int deltaZ = cell.z - preferredEntry.z;
            return deltaX * deltaX + deltaZ * deltaZ
                <= radius * radius;
        }

        private static bool IsValidPlayerCaravan(Caravan caravan)
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
    }
}
