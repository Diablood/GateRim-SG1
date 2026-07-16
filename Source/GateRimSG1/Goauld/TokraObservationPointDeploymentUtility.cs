using System.Reflection;
using HarmonyLib;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Keeps the temporary observation marker visually distinct from the real
    /// sensor and restores it when an undeployed marker has been removed.
    /// </summary>
    internal static class TokraObservationPointDeploymentUtility
    {
        private static readonly PropertyInfo ActiveArchetypeProperty
            = AccessTools.Property(
                typeof(GameComponent_TokraOrganicOperationManager),
                "activeArchetype");

        private static readonly PropertyInfo ActiveStateProperty
            = AccessTools.Property(
                typeof(GameComponent_TokraOrganicOperationManager),
                "activeState");

        private static readonly PropertyInfo ActiveMapIdProperty
            = AccessTools.Property(
                typeof(GameComponent_TokraOrganicOperationManager),
                "activeMapId");

        private static readonly PropertyInfo ActiveDeadDropProperty
            = AccessTools.Property(
                typeof(GameComponent_TokraOrganicOperationManager),
                "activeDeadDrop");

        private static readonly PropertyInfo ObservationPointMarkerProperty
            = AccessTools.Property(
                typeof(GameComponent_TokraOrganicOperationManager),
                "observationPointMarker");

        private static readonly PropertyInfo ObservationTargetCellProperty
            = AccessTools.Property(
                typeof(GameComponent_TokraOrganicOperationManager),
                "observationTargetCell");

        private static readonly PropertyInfo ObservationDeviceDeployedProperty
            = AccessTools.Property(
                typeof(GameComponent_TokraOrganicOperationManager),
                "observationDeviceDeployed");

        public static bool IsUndeployedMarker(Thing marker)
        {
            if (marker == null
                || marker.Destroyed
                || !marker.Spawned
                || marker.Map == null)
            {
                return false;
            }

            return GameComponent_TokraOrganicOperationManager
                    .GetObservationPoint(marker.Map) == marker
                && !GameComponent_TokraOrganicOperationManager
                    .IsObservationRecoveryVisible(marker);
        }

        public static bool EnsureMarkerForDeployment(Thing device)
        {
            GameComponent_TokraOrganicOperationManager manager
                = Current.Game
                    ?.GetComponent<GameComponent_TokraOrganicOperationManager>();
            Map map = device?.Map;

            if (manager == null
                || device == null
                || device.Destroyed
                || !device.Spawned
                || map == null
                || GetProperty(
                        manager,
                        ActiveArchetypeProperty,
                        TokraOrganicOperationArchetype.None)
                    != TokraOrganicOperationArchetype.GoauldObservation
                || GetProperty(
                        manager,
                        ActiveStateProperty,
                        TokraOrganicOperationState.None)
                    != TokraOrganicOperationState.Accepted
                || GetProperty<Thing>(
                        manager,
                        ActiveDeadDropProperty,
                        null) != device
                || GetProperty(
                        manager,
                        ObservationDeviceDeployedProperty,
                        false)
                || GetProperty(manager, ActiveMapIdProperty, -1)
                    != map.uniqueID)
            {
                return false;
            }

            Thing currentMarker = GetProperty<Thing>(
                manager,
                ObservationPointMarkerProperty,
                null);

            if (IsValidMarker(currentMarker, map))
            {
                return true;
            }

            IntVec3 targetCell = GetProperty(
                manager,
                ObservationTargetCellProperty,
                IntVec3.Invalid);

            if (!IsValidTargetCell(map, targetCell)
                && !TokraObservationUtility.TryFindObservationCell(
                    map,
                    out targetCell))
            {
                return false;
            }

            Thing placedMarker;

            if (!TryPlaceMarker(map, targetCell, out placedMarker))
            {
                if (!TokraObservationUtility.TryFindObservationCell(
                        map,
                        out targetCell)
                    || !TryPlaceMarker(map, targetCell, out placedMarker))
                {
                    return false;
                }
            }

            if (!SetProperty(
                    manager,
                    ObservationPointMarkerProperty,
                    placedMarker)
                || !SetProperty(
                    manager,
                    ObservationTargetCellProperty,
                    targetCell))
            {
                if (!placedMarker.Destroyed)
                {
                    placedMarker.Destroy(DestroyMode.Vanish);
                }

                return false;
            }

            GR_Log.Message(
                "Restored undeployed Tok'ra observation blueprint at "
                + $"{targetCell} on map {map.uniqueID}.");
            return true;
        }

        private static bool TryPlaceMarker(
            Map map,
            IntVec3 targetCell,
            out Thing placedMarker)
        {
            placedMarker = null;

            ThingDef markerDef = DefDatabase<ThingDef>.GetNamedSilentFail(
                TokraObservationUtility.MarkerDefName);

            if (markerDef == null
                || map == null
                || !IsValidTargetCell(map, targetCell))
            {
                return false;
            }

            Thing createdMarker = ThingMaker.MakeThing(markerDef);

            if (!GenPlace.TryPlaceThing(
                    createdMarker,
                    targetCell,
                    map,
                    ThingPlaceMode.Direct,
                    out placedMarker))
            {
                if (!createdMarker.Destroyed)
                {
                    createdMarker.Destroy(DestroyMode.Vanish);
                }

                placedMarker = null;
                return false;
            }

            return placedMarker != null
                && !placedMarker.Destroyed
                && placedMarker.Spawned
                && placedMarker.Map == map;
        }

        private static bool IsValidMarker(Thing marker, Map map)
        {
            return marker != null
                && !marker.Destroyed
                && marker.Spawned
                && marker.Map == map
                && marker.def?.defName
                    == TokraObservationUtility.MarkerDefName;
        }

        private static bool IsValidTargetCell(Map map, IntVec3 cell)
        {
            return map != null
                && cell.IsValid
                && cell.InBounds(map)
                && cell.Standable(map)
                && !cell.Fogged(map)
                && !cell.Roofed(map)
                && cell.GetFirstBuilding(map) == null
                && map.reachability.CanReachColony(cell);
        }

        private static T GetProperty<T>(
            GameComponent_TokraOrganicOperationManager manager,
            PropertyInfo property,
            T fallback)
        {
            if (manager == null || property == null)
            {
                return fallback;
            }

            object value = property.GetValue(manager, null);
            return value is T typedValue ? typedValue : fallback;
        }

        private static bool SetProperty<T>(
            GameComponent_TokraOrganicOperationManager manager,
            PropertyInfo property,
            T value)
        {
            if (manager == null
                || property == null
                || !property.CanWrite)
            {
                return false;
            }

            property.SetValue(manager, value, null);
            return true;
        }
    }
}
