using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    internal static class TokraObservationUtility
    {
        public static string DeviceDefName
            => TokraOrganicOperationFramework.GetDefinition(
                TokraOrganicOperationArchetype.GoauldObservation)?.ObservationDeviceDefName;

        public static string MarkerDefName
            => TokraOrganicOperationFramework.GetDefinition(
                TokraOrganicOperationArchetype.GoauldObservation)?.ObservationPointDefName;

        public static bool TryCreateOperationTargets(
            Map map,
            out Thing device,
            out Thing marker,
            out IntVec3 observationCell)
        {
            device = null;
            marker = null;
            observationCell = IntVec3.Invalid;

            if (map == null)
            {
                return false;
            }

            DestroyAllTargets(map, null, null);

            string deviceDefName = DeviceDefName;
            string markerDefName = MarkerDefName;

            if (string.IsNullOrEmpty(deviceDefName)
                || string.IsNullOrEmpty(markerDefName))
            {
                return false;
            }

            ThingDef deviceDef = DefDatabase<ThingDef>.GetNamedSilentFail(
                deviceDefName);
            ThingDef markerDef = DefDatabase<ThingDef>.GetNamedSilentFail(
                markerDefName);

            if (deviceDef == null
                || markerDef == null
                || !TryFindObservationCell(map, out observationCell))
            {
                return false;
            }

            Thing createdDevice = ThingMaker.MakeThing(deviceDef);
            Thing placedDevice;

            if (!TokraDeliveryDropUtility
                .TryPlaceThingNearPreferredDeliveryCell(
                    createdDevice,
                    map,
                    null,
                    out placedDevice))
            {
                if (!createdDevice.Destroyed)
                {
                    createdDevice.Destroy(DestroyMode.Vanish);
                }

                return false;
            }

            Thing createdMarker = ThingMaker.MakeThing(markerDef);
            Thing placedMarker;

            if (!GenPlace.TryPlaceThing(
                    createdMarker,
                    observationCell,
                    map,
                    ThingPlaceMode.Direct,
                    out placedMarker))
            {
                if (!createdMarker.Destroyed)
                {
                    createdMarker.Destroy(DestroyMode.Vanish);
                }

                if (placedDevice != null && !placedDevice.Destroyed)
                {
                    placedDevice.Destroy(DestroyMode.Vanish);
                }

                return false;
            }

            device = placedDevice;
            marker = placedMarker;

            GR_Log.Message(
                "Created Tok'ra observation operation targets; device at "
                + $"{device.Position}, observation point at {observationCell}.");
            return true;
        }

        public static bool TryFindObservationCell(
            Map map,
            out IntVec3 observationCell)
        {
            observationCell = IntVec3.Invalid;

            if (map == null || map.Size.x < 20 || map.Size.z < 20)
            {
                return false;
            }

            int edgeBand = Math.Max(5, Math.Min(map.Size.x, map.Size.z) / 7);

            for (int attempt = 0; attempt < 500; attempt++)
            {
                int edge = Rand.Range(0, 4);
                int x;
                int z;

                if (edge == 0)
                {
                    x = Rand.RangeInclusive(4, edgeBand);
                    z = Rand.RangeInclusive(4, map.Size.z - 5);
                }
                else if (edge == 1)
                {
                    x = Rand.RangeInclusive(map.Size.x - edgeBand - 1,
                        map.Size.x - 5);
                    z = Rand.RangeInclusive(4, map.Size.z - 5);
                }
                else if (edge == 2)
                {
                    x = Rand.RangeInclusive(4, map.Size.x - 5);
                    z = Rand.RangeInclusive(4, edgeBand);
                }
                else
                {
                    x = Rand.RangeInclusive(4, map.Size.x - 5);
                    z = Rand.RangeInclusive(map.Size.z - edgeBand - 1,
                        map.Size.z - 5);
                }

                IntVec3 cell = new IntVec3(x, 0, z);

                if (!IsValidObservationCell(map, cell))
                {
                    continue;
                }

                observationCell = cell;
                return true;
            }

            return false;
        }


        public static bool IsObservationDevice(Thing thing)
        {
            return thing != null
                && thing.def?.defName == DeviceDefName;
        }

        public static bool IsObservationPoint(Thing thing)
        {
            return thing != null
                && thing.def?.defName == MarkerDefName;
        }

        public static Thing FindExistingDevice(Map map)
        {
            return FindExisting(map, DeviceDefName);
        }

        public static Thing FindExistingMarker(Map map)
        {
            return FindExisting(map, MarkerDefName);
        }

        public static void DestroyAllTargets(
            Map map,
            Thing retainedDevice,
            Thing retainedMarker)
        {
            if (map?.listerThings?.AllThings == null)
            {
                return;
            }

            List<Thing> stale = map.listerThings.AllThings
                .Where(thing =>
                    thing != null
                    && (thing.def?.defName == DeviceDefName
                        || thing.def?.defName == MarkerDefName)
                    && thing != retainedDevice
                    && thing != retainedMarker)
                .ToList();

            for (int i = 0; i < stale.Count; i++)
            {
                if (!stale[i].Destroyed)
                {
                    stale[i].Destroy(DestroyMode.Vanish);
                }
            }
        }

        private static Thing FindExisting(Map map, string defName)
        {
            return map?.listerThings?.AllThings?.FirstOrDefault(thing =>
                thing != null
                && !thing.Destroyed
                && thing.def?.defName == defName);
        }

        private static bool IsValidObservationCell(Map map, IntVec3 cell)
        {
            return cell.InBounds(map)
                && cell.Standable(map)
                && !cell.Fogged(map)
                && !cell.Roofed(map)
                && cell.GetFirstBuilding(map) == null
                && map.reachability.CanReachColony(cell);
        }
    }
}
