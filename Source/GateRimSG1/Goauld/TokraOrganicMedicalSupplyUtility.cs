using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;
using Verse.AI.Group;

namespace GateRimSG1.Goauld
{
    internal static class TokraOrganicMedicalSupplyUtility
    {
        internal const int RequiredMedicineCount = 2;

        private const string MedicineDefName = "MedicineIndustrial";
        private const string SecureCommunicatorDefName
            = "SG1_TokraSecureCommunicator";
        private const int MeetingRadius = 10;
        private const int ArrivalDistanceSquared = 36;

        internal static bool TrySpawnLiaison(
            Map map,
            int visitDurationTicks,
            out Pawn liaison,
            out IntVec3 meetingCell)
        {
            liaison = null;
            meetingCell = IntVec3.Invalid;

            if (map == null || GR_DefOf.SG1_TokraVoluntaryHost == null)
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
                GR_DefOf.SG1_TokraVoluntaryHost,
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

        internal static bool HasEnoughIndustrialMedicine(
            Map map,
            Pawn negotiator)
        {
            return CountAvailableIndustrialMedicine(map, negotiator)
                >= RequiredMedicineCount;
        }

        internal static bool TryConsumeIndustrialMedicine(
            Map map,
            Pawn negotiator)
        {
            List<Thing> availableMedicine = GetAvailableIndustrialMedicine(
                map,
                negotiator);
            int totalCount = 0;

            for (int index = 0; index < availableMedicine.Count; index++)
            {
                totalCount += availableMedicine[index].stackCount;
            }

            if (totalCount < RequiredMedicineCount)
            {
                return false;
            }

            int remaining = RequiredMedicineCount;

            for (int index = 0;
                index < availableMedicine.Count && remaining > 0;
                index++)
            {
                Thing medicine = availableMedicine[index];
                int consumedCount = System.Math.Min(
                    remaining,
                    medicine.stackCount);

                if (consumedCount == medicine.stackCount)
                {
                    medicine.Destroy(DestroyMode.Vanish);
                }
                else
                {
                    Thing consumed = medicine.SplitOff(consumedCount);
                    consumed.Destroy(DestroyMode.Vanish);
                }

                remaining -= consumedCount;
            }

            return remaining == 0;
        }

        private static int CountAvailableIndustrialMedicine(
            Map map,
            Pawn negotiator)
        {
            List<Thing> availableMedicine = GetAvailableIndustrialMedicine(
                map,
                negotiator);
            int totalCount = 0;

            for (int index = 0; index < availableMedicine.Count; index++)
            {
                totalCount += availableMedicine[index].stackCount;
            }

            return totalCount;
        }

        private static List<Thing> GetAvailableIndustrialMedicine(
            Map map,
            Pawn negotiator)
        {
            List<Thing> result = new List<Thing>();
            ThingDef medicineDef = DefDatabase<ThingDef>.GetNamedSilentFail(
                MedicineDefName);

            if (map?.listerThings == null || medicineDef == null)
            {
                return result;
            }

            List<Thing> medicines = map.listerThings.ThingsOfDef(medicineDef);

            for (int index = 0; index < medicines.Count; index++)
            {
                Thing medicine = medicines[index];

                if (medicine == null
                    || medicine.Destroyed
                    || !medicine.Spawned
                    || medicine.stackCount <= 0
                    || medicine.IsForbidden(negotiator)
                    || (negotiator != null
                        && !negotiator.CanReach(
                            medicine,
                            PathEndMode.ClosestTouch,
                            Danger.Some)))
                {
                    continue;
                }

                result.Add(medicine);
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
