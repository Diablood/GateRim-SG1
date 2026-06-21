using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;
using Verse.AI.Group;

namespace GateRimSG1.Goauld
{
    internal static class TokraOrganicMedicalSupplyUtility
    {
        private const string SecureCommunicatorDefName
            = "SG1_TokraSecureCommunicator";
        private const int MeetingRadius = 10;
        private const int ArrivalDistanceSquared = 36;

        internal static bool TrySpawnLiaison(
            Map map,
            string liaisonPawnKindDefName,
            int visitDurationTicks,
            out Pawn liaison,
            out IntVec3 meetingCell)
        {
            liaison = null;
            meetingCell = IntVec3.Invalid;

            PawnKindDef liaisonKind = string.IsNullOrWhiteSpace(
                    liaisonPawnKindDefName)
                ? null
                : DefDatabase<PawnKindDef>.GetNamedSilentFail(
                    liaisonPawnKindDefName);

            if (map == null || liaisonKind == null)
            {
                return false;
            }

            Faction tokraFaction
                = TokraFactionUtility.GetOrCreatePersistentFaction(
                    "organic medical supply liaison");

            if (tokraFaction == null
                || !TryFindEntryCell(map, out IntVec3 entryCell)
                || !TryFindMeetingCell(map, out meetingCell))
            {
                return false;
            }

            Pawn generatedLiaison = PawnGenerator.GeneratePawn(
                liaisonKind,
                tokraFaction);

            if (generatedLiaison == null)
            {
                return false;
            }

            GenSpawn.Spawn(generatedLiaison, entryCell, map);

            LordMaker.MakeNewLord(
                tokraFaction,
                new LordJob_TokraMedicalSupplyLiaison(
                    tokraFaction,
                    meetingCell,
                    visitDurationTicks),
                map,
                new List<Pawn> { generatedLiaison });

            liaison = generatedLiaison;

            GR_Log.Message(
                "Spawned Tok'ra medical supply liaison "
                + $"{PawnDebugLabel(liaison)} at {entryCell}; meeting cell "
                + $"{meetingCell}; visit window {visitDurationTicks} ticks.");

            return true;
        }

        internal static bool HasReachedMeetingPoint(
            Pawn liaison,
            IntVec3 meetingCell)
        {
            return liaison != null
                && !liaison.Dead
                && !liaison.Destroyed
                && liaison.Spawned
                && liaison.Map != null
                && meetingCell.IsValid
                && liaison.Position.DistanceToSquared(meetingCell)
                    <= ArrivalDistanceSquared;
        }

        internal static bool TryOrderDeparture(Pawn liaison)
        {
            if (liaison == null
                || liaison.Dead
                || liaison.Destroyed
                || !liaison.Spawned
                || liaison.Map == null)
            {
                return false;
            }

            Lord lord = liaison.GetLord();

            if (lord?.LordJob is LordJob_TokraMedicalSupplyLiaison)
            {
                lord.ReceiveMemo(LordJob_TokraMedicalSupplyLiaison.LeaveMemo);
                return true;
            }

            return lord?.LordJob is LordJob_TravelAndExit;
        }

        internal static bool HasEnoughResource(
            Map map,
            Pawn negotiator,
            string thingDefName,
            int requiredCount)
        {
            return requiredCount > 0
                && CountAvailableResource(
                    map,
                    negotiator,
                    thingDefName) >= requiredCount;
        }

        internal static bool TryConsumeResource(
            Map map,
            Pawn negotiator,
            string thingDefName,
            int requiredCount)
        {
            if (requiredCount <= 0)
            {
                return false;
            }

            List<Thing> availableResources = GetAvailableResource(
                map,
                negotiator,
                thingDefName);
            int totalCount = 0;

            for (int index = 0; index < availableResources.Count; index++)
            {
                totalCount += availableResources[index].stackCount;
            }

            if (totalCount < requiredCount)
            {
                return false;
            }

            int remaining = requiredCount;

            for (int index = 0;
                index < availableResources.Count && remaining > 0;
                index++)
            {
                Thing resource = availableResources[index];
                int consumedCount = System.Math.Min(
                    remaining,
                    resource.stackCount);

                if (consumedCount == resource.stackCount)
                {
                    resource.Destroy(DestroyMode.Vanish);
                }
                else
                {
                    Thing consumed = resource.SplitOff(consumedCount);
                    consumed.Destroy(DestroyMode.Vanish);
                }

                remaining -= consumedCount;
            }

            return remaining == 0;
        }

        private static int CountAvailableResource(
            Map map,
            Pawn negotiator,
            string thingDefName)
        {
            List<Thing> availableResources = GetAvailableResource(
                map,
                negotiator,
                thingDefName);
            int totalCount = 0;

            for (int index = 0; index < availableResources.Count; index++)
            {
                totalCount += availableResources[index].stackCount;
            }

            return totalCount;
        }

        private static List<Thing> GetAvailableResource(
            Map map,
            Pawn negotiator,
            string thingDefName)
        {
            List<Thing> result = new List<Thing>();
            ThingDef resourceDef = string.IsNullOrWhiteSpace(thingDefName)
                ? null
                : DefDatabase<ThingDef>.GetNamedSilentFail(thingDefName);

            if (map?.listerThings == null || resourceDef == null)
            {
                return result;
            }

            List<Thing> resources = map.listerThings.ThingsOfDef(resourceDef);

            for (int index = 0; index < resources.Count; index++)
            {
                Thing resource = resources[index];

                if (resource == null
                    || resource.Destroyed
                    || !resource.Spawned
                    || resource.stackCount <= 0
                    || resource.IsForbidden(negotiator)
                    || (negotiator != null
                        && !negotiator.CanReach(
                            resource,
                            PathEndMode.ClosestTouch,
                            Danger.Some)))
                {
                    continue;
                }

                result.Add(resource);
            }

            return result;
        }

        private static bool TryFindMeetingCell(
            Map map,
            out IntVec3 meetingCell)
        {
            meetingCell = IntVec3.Invalid;

            Thing deliverySpot = FindFirstSpawnedThing(
                map,
                TokraDeliveryDropUtility.DeliveryDropSpotDefName,
                requirePower: false);

            if (deliverySpot != null
                && TryFindStandableCellNear(
                    deliverySpot.Position,
                    map,
                    out meetingCell))
            {
                return true;
            }

            Thing communicator = FindFirstSpawnedThing(
                map,
                SecureCommunicatorDefName,
                requirePower: true);

            if (communicator != null
                && TryFindStandableCellNear(
                    communicator.Position,
                    map,
                    out meetingCell))
            {
                return true;
            }

            IntVec3 colonyCenter = FindColonyCenter(map);

            return TryFindStandableCellNear(
                colonyCenter,
                map,
                out meetingCell);
        }

        private static Thing FindFirstSpawnedThing(
            Map map,
            string defName,
            bool requirePower)
        {
            ThingDef thingDef = DefDatabase<ThingDef>.GetNamedSilentFail(
                defName);

            if (map?.listerThings == null || thingDef == null)
            {
                return null;
            }

            List<Thing> things = map.listerThings.ThingsOfDef(thingDef);

            for (int index = 0; index < things.Count; index++)
            {
                Thing thing = things[index];

                if (thing == null
                    || thing.Destroyed
                    || !thing.Spawned)
                {
                    continue;
                }

                if (requirePower)
                {
                    CompPowerTrader powerComp
                        = thing.TryGetComp<CompPowerTrader>();

                    if (powerComp == null || !powerComp.PowerOn)
                    {
                        continue;
                    }
                }

                return thing;
            }

            return null;
        }

        private static IntVec3 FindColonyCenter(Map map)
        {
            if (map?.mapPawns?.FreeColonistsSpawned == null
                || map.mapPawns.FreeColonistsSpawned.Count == 0)
            {
                return map == null
                    ? IntVec3.Invalid
                    : map.Center;
            }

            int totalX = 0;
            int totalZ = 0;
            int count = map.mapPawns.FreeColonistsSpawned.Count;

            for (int index = 0; index < count; index++)
            {
                Pawn colonist = map.mapPawns.FreeColonistsSpawned[index];
                totalX += colonist.Position.x;
                totalZ += colonist.Position.z;
            }

            return new IntVec3(totalX / count, 0, totalZ / count);
        }

        private static bool TryFindStandableCellNear(
            IntVec3 center,
            Map map,
            out IntVec3 meetingCell)
        {
            meetingCell = IntVec3.Invalid;

            if (map == null || !center.IsValid || !center.InBounds(map))
            {
                return false;
            }

            foreach (IntVec3 candidate in GenRadial.RadialCellsAround(
                         center,
                         MeetingRadius,
                         true))
            {
                if (candidate.InBounds(map)
                    && candidate.Standable(map)
                    && !candidate.Fogged(map)
                    && map.reachability.CanReachColony(candidate))
                {
                    meetingCell = candidate;
                    return true;
                }
            }

            return false;
        }

        private static bool TryFindEntryCell(Map map, out IntVec3 entryCell)
        {
            return CellFinder.TryFindRandomEdgeCellWith(
                cell => map.reachability.CanReachColony(cell)
                    && !cell.Fogged(map),
                map,
                CellFinder.EdgeRoadChance_Neutral,
                out entryCell);
        }

        private static string PawnDebugLabel(Pawn pawn)
        {
            return pawn == null
                ? "<null>"
                : $"{pawn.LabelShort} ({pawn.ThingID})";
        }
    }
}
