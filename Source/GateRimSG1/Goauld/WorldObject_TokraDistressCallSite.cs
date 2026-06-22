using System;
using System.Collections.Generic;
using GateRimSG1.Missions;
using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Temporary world site created by a recurrent Tok'ra distress call. The
    /// actual situation remains hidden until a player caravan enters the map.
    /// </summary>
    public sealed class WorldObject_TokraDistressCallSite : MapParent
    {
        private int homeMapId = -1;
        private string missionDefName;
        private TokraDistressCallVariant plannedVariant
            = TokraDistressCallVariant.None;
        private float scaledThreatPoints;
        private int lateArrivalTick;
        private int expiryTick;
        private int missionMapSize = 120;
        private bool operationLaunched;
        private bool managerResolved;
        private bool missionSucceeded;

        public float ScaledThreatPoints => scaledThreatPoints;

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
            TokraDistressCallVariant sourceVariant,
            float threatPoints,
            int lateThresholdTick,
            int missionExpiryTick,
            int mapSize)
        {
            homeMapId = sourceMapId;
            missionDefName = sourceMissionDefName;
            plannedVariant = sourceVariant;
            scaledThreatPoints = Math.Max(0f, threatPoints);
            lateArrivalTick = lateThresholdTick;
            expiryTick = missionExpiryTick;
            missionMapSize = Math.Max(80, mapSize);
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref homeMapId, "distressHomeMapId", -1);
            Scribe_Values.Look(
                ref missionDefName,
                "distressMissionDefName");
            Scribe_Values.Look(
                ref plannedVariant,
                "distressPlannedVariant",
                TokraDistressCallVariant.None);
            Scribe_Values.Look(
                ref scaledThreatPoints,
                "distressScaledThreatPoints",
                0f);
            Scribe_Values.Look(
                ref lateArrivalTick,
                "distressLateArrivalTick",
                0);
            Scribe_Values.Look(
                ref expiryTick,
                "distressExpiryTick",
                0);
            Scribe_Values.Look(
                ref missionMapSize,
                "distressMissionMapSize",
                120);
            Scribe_Values.Look(
                ref operationLaunched,
                "distressOperationLaunched",
                false);
            Scribe_Values.Look(
                ref managerResolved,
                "distressManagerResolved",
                false);
            Scribe_Values.Look(
                ref missionSucceeded,
                "distressMissionSucceeded",
                false);
        }

        protected override void Tick()
        {
            base.Tick();

            if (managerResolved || HasMap || expiryTick <= 0)
            {
                return;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;

            if (currentTick >= expiryTick)
            {
                Expire();
            }
        }

        public TokraDistressCallVariant GetArrivalVariant()
        {
            int currentTick = Find.TickManager?.TicksGame ?? 0;

            if (plannedVariant == TokraDistressCallVariant.GenuineRescue
                && lateArrivalTick > 0
                && currentTick >= lateArrivalTick)
            {
                return TokraDistressCallVariant.LateArrival;
            }

            return plannedVariant == TokraDistressCallVariant.None
                ? TokraDistressCallVariant.GenuineRescue
                : plannedVariant;
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
                CaravanArrivalAction_TokraDistressCallSite
                    .GetFloatMenuOptions(caravan, this))
            {
                yield return option;
            }
        }

        public override string GetInspectString()
        {
            string baseInspectString = base.GetInspectString();
            string missionInspectString;

            if (managerResolved)
            {
                missionInspectString = missionSucceeded
                    ? "GR_TokraDistressCall_InspectResolvedSuccess".Translate()
                    : "GR_TokraDistressCall_InspectResolvedFailure".Translate();
            }
            else if (operationLaunched || HasMap)
            {
                missionInspectString
                    = "GR_TokraDistressCall_InspectActive".Translate();
            }
            else
            {
                missionInspectString
                    = "GR_TokraDistressCall_InspectPending"
                        .Translate(GetRemainingHoursString());
            }

            return string.IsNullOrEmpty(baseInspectString)
                ? missionInspectString
                : baseInspectString + "\n" + missionInspectString;
        }

        public override bool ShouldRemoveMapNow(
            out bool alsoRemoveWorldObject)
        {
            alsoRemoveWorldObject = false;

            if (!HasMap)
            {
                return false;
            }

            MapComponent_TokraDistressCallMission component = Map
                .GetComponent<MapComponent_TokraDistressCallMission>();

            if (component == null || !component.OperationResolved)
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

        public void NotifyManagerResolved(bool succeeded)
        {
            if (managerResolved)
            {
                return;
            }

            managerResolved = true;
            missionSucceeded = succeeded;

            if (HasMap)
            {
                Map.GetComponent<MapComponent_TokraDistressCallMission>()
                    ?.NotifyManagerResolved(succeeded);
                return;
            }

            if (!Destroyed)
            {
                Destroy();
            }
        }

        public FloatMenuAcceptanceReport CanEnter(Caravan caravan)
        {
            if (!IsValidPlayerCaravan(caravan))
            {
                return false;
            }

            if (managerResolved || operationLaunched || HasMap)
            {
                return FloatMenuAcceptanceReport.WithFailMessage(
                    "GR_TokraDistressCall_AlreadyEntered".Translate());
            }

            return true;
        }

        public void EnterFromCaravan(Caravan caravan)
        {
            if (!IsValidPlayerCaravanAtSite(caravan))
            {
                Messages.Message(
                    "GR_TokraDistressCall_EnterRequiresCaravan".Translate(),
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

            TokraDistressCallMissionUtility.EnsureMissionMapInitialized(
                map,
                this);
            operationLaunched = true;

            if (generatedNewMap)
            {
                Find.TickManager.Notify_GeneratedPotentiallyHostileMap();
            }

            MapComponent_TokraDistressCallMission component
                = map.GetComponent<MapComponent_TokraDistressCallMission>();
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


        private int ResolvePreferredEntryRadius()
        {
            GateRimMissionDistressCallDef profile
                = TokraOrganicOperationFramework.GetDefinition(
                        TokraOrganicOperationArchetype.DistressCall)
                    ?.MissionDef?.distressCall;

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

        private void Expire()
        {
            if (managerResolved)
            {
                return;
            }

            if (!GameComponent_TokraOrganicOperationManager
                .NotifyDistressCallSiteResolved(
                    this,
                    succeeded: false,
                    failureTextId: "failureTimeout"))
            {
                NotifyManagerResolved(false);
            }
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
    }
}
