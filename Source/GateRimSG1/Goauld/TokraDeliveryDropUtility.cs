using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Shared placement utility for discreet Tok'ra deliveries.
    ///
    /// Delivery priority is deliberately player-readable: use the dedicated
    /// delivery spot when present, otherwise fall back to a powered Tok'ra
    /// communicator, then a reachable unfogged map edge cell.
    /// </summary>
    internal static class TokraDeliveryDropUtility
    {
        internal const string DeliveryDropSpotDefName = "SG1_TokraDeliveryDropSpot";
        private const string SecureCommunicatorDefName = "SG1_TokraSecureCommunicator";

        internal static bool TryFindPreferredDeliveryCell(
            Map map,
            Thing requestingCommunicator,
            out IntVec3 cell)
        {
            cell = IntVec3.Invalid;

            if (map == null)
            {
                return false;
            }

            Thing deliveryDropSpot = FindDeliveryDropSpot(map);

            if (deliveryDropSpot != null
                && IsUsableDeliveryCell(deliveryDropSpot.Position, map))
            {
                cell = deliveryDropSpot.Position;
                return true;
            }

            if (requestingCommunicator != null
                && requestingCommunicator.Spawned
                && requestingCommunicator.Map == map
                && IsUsableDeliveryCell(requestingCommunicator.Position, map))
            {
                cell = requestingCommunicator.Position;
                return true;
            }

            Thing fallbackCommunicator = FindPoweredSecureCommunicator(map);

            if (fallbackCommunicator != null
                && IsUsableDeliveryCell(fallbackCommunicator.Position, map))
            {
                cell = fallbackCommunicator.Position;
                return true;
            }

            return TryFindEdgeDeliveryCell(map, out cell);
        }

        internal static bool TryPlaceThingNearPreferredDeliveryCell(
            Thing thing,
            Map map,
            Thing requestingCommunicator,
            out Thing placedThing)
        {
            placedThing = null;

            if (thing == null || map == null)
            {
                return false;
            }

            IntVec3 deliveryCell;

            if (!TryFindPreferredDeliveryCell(
                    map,
                    requestingCommunicator,
                    out deliveryCell))
            {
                return false;
            }

            return GenPlace.TryPlaceThing(
                thing,
                deliveryCell,
                map,
                ThingPlaceMode.Near,
                out placedThing);
        }

        internal static bool RemoveOtherDeliveryDropSpots(Thing keepSpot)
        {
            if (keepSpot == null
                || keepSpot.Map == null
                || keepSpot.def == null)
            {
                return false;
            }

            List<Thing> existingSpots = keepSpot.Map.listerThings.ThingsOfDef(
                keepSpot.def);
            List<Thing> spotsToRemove = new List<Thing>();

            for (int index = 0; index < existingSpots.Count; index++)
            {
                Thing existingSpot = existingSpots[index];

                if (existingSpot != null
                    && existingSpot != keepSpot
                    && !existingSpot.Destroyed)
                {
                    spotsToRemove.Add(existingSpot);
                }
            }

            for (int index = 0; index < spotsToRemove.Count; index++)
            {
                spotsToRemove[index].Destroy(DestroyMode.Vanish);
            }

            if (spotsToRemove.Count > 0)
            {
                Messages.Message(
                    "GR_TokraDeliveryDropSpot_ReplacedOldSpot".Translate(),
                    keepSpot,
                    MessageTypeDefOf.NeutralEvent,
                    historical: false);
            }

            return spotsToRemove.Count > 0;
        }

        private static Thing FindDeliveryDropSpot(Map map)
        {
            ThingDef deliverySpotDef = DefDatabase<ThingDef>.GetNamedSilentFail(
                DeliveryDropSpotDefName);

            if (deliverySpotDef == null || map?.listerThings == null)
            {
                return null;
            }

            List<Thing> deliverySpots = map.listerThings.ThingsOfDef(
                deliverySpotDef);

            for (int index = 0; index < deliverySpots.Count; index++)
            {
                Thing deliverySpot = deliverySpots[index];

                if (deliverySpot != null
                    && deliverySpot.Spawned
                    && !deliverySpot.Destroyed)
                {
                    return deliverySpot;
                }
            }

            return null;
        }

        private static Thing FindPoweredSecureCommunicator(Map map)
        {
            ThingDef communicatorDef = DefDatabase<ThingDef>.GetNamedSilentFail(
                SecureCommunicatorDefName);

            if (communicatorDef == null || map?.listerThings == null)
            {
                return null;
            }

            List<Thing> communicators = map.listerThings.ThingsOfDef(
                communicatorDef);

            for (int index = 0; index < communicators.Count; index++)
            {
                Thing communicator = communicators[index];

                if (communicator != null
                    && communicator.Spawned
                    && !communicator.Destroyed
                    && IsPoweredOrUnpoweredBuilding(communicator))
                {
                    return communicator;
                }
            }

            return null;
        }

        private static bool IsPoweredOrUnpoweredBuilding(Thing thing)
        {
            CompPowerTrader powerTrader = thing.TryGetComp<CompPowerTrader>();

            return powerTrader == null || powerTrader.PowerOn;
        }

        private static bool TryFindEdgeDeliveryCell(Map map, out IntVec3 cell)
        {
            return CellFinder.TryFindRandomEdgeCellWith(
                candidateCell => map.reachability.CanReachColony(candidateCell)
                    && !candidateCell.Fogged(map),
                map,
                CellFinder.EdgeRoadChance_Neutral,
                out cell);
        }

        private static bool IsUsableDeliveryCell(IntVec3 cell, Map map)
        {
            return map != null
                && cell.IsValid
                && cell.InBounds(map)
                && !cell.Fogged(map);
        }
    }
}
