using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Temporary Tok'ra mission marker created from the decoded operational
    /// lead. The public player flow is now a single world action that opens a
    /// playable sabotage map instead of chaining several similar world-map
    /// interactions on the same marker.
    /// </summary>
    public class WorldObject_TokraDecodedMissionSite : MapParent
    {
        public const int DurationTicks = 720000;
        private const int MissionMapSize = 120;

        private int ticksRemaining = DurationTicks;
        private bool operationLaunched;
        private bool operationFailed;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(
                ref ticksRemaining,
                "ticksRemaining",
                DurationTicks);
            Scribe_Values.Look(
                ref operationLaunched,
                "tokraRelaySabotageOperationLaunched",
                false);
            Scribe_Values.Look(
                ref operationFailed,
                "tokraRelaySabotageOperationFailed",
                false);
        }

        protected override void Tick()
        {
            base.Tick();

            if (HasMap)
            {
                return;
            }

            ticksRemaining--;

            if (ticksRemaining <= 0)
            {
                Expire();
            }
        }

        public override bool ShouldRemoveMapNow(
            out bool alsoRemoveWorldObject)
        {
            alsoRemoveWorldObject = false;

            if (!HasMap)
            {
                return false;
            }

            MapComponent_TokraRelaySabotageMission component = Map
                .GetComponent<MapComponent_TokraRelaySabotageMission>();

            if (component == null || !component.OperationResolved)
            {
                return false;
            }

            if (Map.mapPawns.AnyPawnBlockingMapRemoval)
            {
                return false;
            }

            if (TransporterUtility.IncomingTransporterPreventingMapRemoval(Map))
            {
                return false;
            }

            GameComponent_TokraTrustTracker
                .NotifyFirstTrustMissionRelayOperationDeparted(
                    component.SabotageCompleted && !component.MissionFailed);

            alsoRemoveWorldObject = true;
            return true;
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            foreach (Gizmo gizmo in base.GetGizmos())
            {
                yield return gizmo;
            }

            if (!GR_Debug.ShowAdvancedInformation)
            {
                yield break;
            }

            Command_Action launchCommand = new Command_Action
            {
                defaultLabel = "GR_TokraDecodedMissionWorldSite_LaunchCommandLabel"
                    .Translate(),
                defaultDesc = "GR_TokraDecodedMissionWorldSite_LaunchCommandDesc"
                    .Translate(GetRemainingDaysString()),
                action = TryLaunchOperation
            };

            if (operationLaunched || HasMap)
            {
                launchCommand.Disable(
                    "GR_TokraDecodedMissionWorldSite_OperationAlreadyLaunched"
                        .Translate());
            }
            else if (GetPlayerCaravanAtSite() == null)
            {
                launchCommand.Disable(
                    "GR_TokraDecodedMissionWorldSite_LaunchRequiresCaravan"
                        .Translate());
            }

            yield return launchCommand;
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

            if (!IsValidPlayerCaravan(caravan))
            {
                yield break;
            }

            if (caravan.Tile != Tile)
            {
                yield return GetTravelToSiteFloatMenuOption(caravan);
                yield break;
            }

            yield return GetLaunchOperationFloatMenuOption(caravan);
        }

        public override string GetInspectString()
        {
            string baseInspectString = base.GetInspectString();
            string missionInspectString;

            if (operationFailed)
            {
                missionInspectString =
                    "GR_TokraDecodedMissionWorldSite_InspectStringOperationFailed"
                        .Translate();
            }
            else if (GameComponent_TokraTrustTracker
                .IsFirstTrustMissionRelaySabotageCompleted())
            {
                missionInspectString =
                    "GR_TokraDecodedMissionWorldSite_InspectStringSabotageCompleted"
                        .Translate();
            }
            else if (operationLaunched || HasMap)
            {
                missionInspectString =
                    "GR_TokraDecodedMissionWorldSite_InspectStringOperationActive"
                        .Translate();
            }
            else
            {
                missionInspectString =
                    "GR_TokraDecodedMissionWorldSite_InspectStringPlayable"
                        .Translate(GetRemainingDaysString());
            }

            if (string.IsNullOrEmpty(baseInspectString))
            {
                return missionInspectString;
            }

            return baseInspectString + "\n" + missionInspectString;
        }

        public void NotifyRelaySabotageMapReady(Map map)
        {
            operationLaunched = true;
            GameComponent_TokraTrustTracker
                .NotifyFirstTrustMissionWorldSiteReconnoitered();
            GameComponent_TokraTrustTracker
                .NotifyFirstTrustMissionRelaySabotagePrepared();

            GR_Log.Message(
                "Tok'ra relay sabotage mission map ready at tile "
                + $"{Tile} on map {map?.uniqueID ?? -1}.");
        }

        public void NotifyRelaySabotageCompleted()
        {
            if (!GameComponent_TokraTrustTracker
                .NotifyFirstTrustMissionRelaySabotageCompleted())
            {
                return;
            }

            Find.LetterStack.ReceiveLetter(
                "GR_TokraDecodedMissionWorldSite_CompletedLetterLabel"
                    .Translate(),
                "GR_TokraDecodedMissionWorldSite_CompletedLetterText"
                    .Translate(),
                LetterDefOf.PositiveEvent,
                this);

            Messages.Message(
                "GR_TokraDecodedMissionWorldSite_SabotageCompleted"
                    .Translate(),
                this,
                MessageTypeDefOf.PositiveEvent,
                historical: true);
        }

        public void NotifyRelaySabotageFailed()
        {
            operationFailed = true;
            GR_Log.Warning(
                "Tok'ra relay sabotage operation marked as failed after "
                + "destructive loss of the control node.");
        }

        private void TryLaunchOperation()
        {
            TryLaunchOperation(GetPlayerCaravanAtSite());
        }

        private void TryLaunchOperation(Caravan caravan)
        {
            if (!IsValidPlayerCaravanAtSite(caravan))
            {
                Messages.Message(
                    "GR_TokraDecodedMissionWorldSite_LaunchRequiresCaravan"
                        .Translate(),
                    this,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            if (operationLaunched || HasMap)
            {
                Messages.Message(
                    "GR_TokraDecodedMissionWorldSite_OperationAlreadyLaunched"
                        .Translate(),
                    this,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            LongEventHandler.QueueLongEvent(
                delegate
                {
                    Map map = GetOrGenerateMapUtility.GetOrGenerateMap(
                        Tile,
                        new IntVec3(MissionMapSize, 1, MissionMapSize),
                        def);

                    TokraRelaySabotageMissionUtility.EnsureMissionMapInitialized(
                        map,
                        this);

                    NotifyRelaySabotageMapReady(map);

                    CaravanEnterMapUtility.Enter(
                        caravan,
                        map,
                        CaravanEnterMode.Edge,
                        CaravanDropInventoryMode.DoNotDrop,
                        true);

                    Find.TickManager.CurTimeSpeed = TimeSpeed.Paused;
                },
                "GeneratingMap",
                doAsynchronously: false,
                exceptionHandler: null);
        }

        private FloatMenuOption GetTravelToSiteFloatMenuOption(Caravan caravan)
        {
            return new FloatMenuOption(
                "GR_TokraDecodedMissionWorldSite_TravelCommandLabel"
                    .Translate()
                    .ToString(),
                () => caravan.pather.StartPath(Tile, null, true, true));
        }

        private FloatMenuOption GetLaunchOperationFloatMenuOption(
            Caravan caravan)
        {
            string label = "GR_TokraDecodedMissionWorldSite_LaunchCommandLabel"
                .Translate()
                .ToString();

            if (operationLaunched || HasMap)
            {
                return new FloatMenuOption(
                    label + ": "
                    + "GR_TokraDecodedMissionWorldSite_OperationAlreadyLaunched"
                        .Translate()
                        .ToString(),
                    null);
            }

            if (!IsValidPlayerCaravanAtSite(caravan))
            {
                return new FloatMenuOption(
                    label + ": "
                    + "GR_TokraDecodedMissionWorldSite_LaunchRequiresCaravan"
                        .Translate()
                        .ToString(),
                    null);
            }

            return new FloatMenuOption(
                label,
                () => TryLaunchOperation(caravan));
        }

        private bool IsValidPlayerCaravan(Caravan caravan)
        {
            return caravan != null
                && !caravan.Destroyed
                && caravan.Faction == Faction.OfPlayer;
        }

        private Caravan GetPlayerCaravanAtSite()
        {
            if (Find.WorldObjects == null)
            {
                return null;
            }

            foreach (WorldObject worldObject in Find.WorldObjects.AllWorldObjects)
            {
                Caravan caravan = worldObject as Caravan;

                if (IsValidPlayerCaravanAtSite(caravan))
                {
                    return caravan;
                }
            }

            return null;
        }

        private bool IsValidPlayerCaravanAtSite(Caravan caravan)
        {
            return caravan != null
                && !caravan.Destroyed
                && caravan.Faction == Faction.OfPlayer
                && caravan.Tile == Tile;
        }

        private void Expire()
        {
            GR_Log.Message(
                "Tok'ra decoded mission world site expired at tile "
                + $"{Tile}.");

            Messages.Message(
                "GR_TokraDecodedMissionWorldSite_Expired".Translate(),
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            Destroy();
        }

        private string GetRemainingDaysString()
        {
            float remainingDays = ticksRemaining / 60000f;

            if (remainingDays < 0f)
            {
                remainingDays = 0f;
            }

            return remainingDays.ToString("0.#");
        }
    }
}
