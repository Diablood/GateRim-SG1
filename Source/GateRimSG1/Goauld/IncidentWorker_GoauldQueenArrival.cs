using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public class IncidentWorker_GoauldQueenArrival : IncidentWorker
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            if (!base.CanFireNowSub(parms))
            {
                return false;
            }

            Map map = parms.target as Map;

            if (map == null
                || GR_DefOf.SG1_GoauldQueen == null
                || HasLivingPlayerQueen())
            {
                return false;
            }

            IntVec3 unusedEntryCell;
            return TryFindEntryCell(map, out unusedEntryCell);
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = parms.target as Map;
            PawnKindDef queenKind = GR_DefOf.SG1_GoauldQueen;

            if (map == null || queenKind == null || HasLivingPlayerQueen())
            {
                return false;
            }

            IntVec3 entryCell;

            if (!TryFindEntryCell(map, out entryCell))
            {
                GR_Log.Message(
                    "Cannot start Goa'uld queen arrival: no reachable "
                    + "unfogged map-edge entry cell was found.");
                return false;
            }

            Pawn queen = PawnGenerator.GeneratePawn(
                queenKind,
                Faction.OfPlayer);

            if (queen == null)
            {
                GR_Log.Warning(
                    "Cannot start Goa'uld queen arrival: "
                    + "SG1_GoauldQueen generation failed.");
                return false;
            }

            GenSpawn.Spawn(queen, entryCell, map);

            GR_Log.Message(
                $"Started Goa'uld queen arrival at {entryCell}; "
                + $"spawned player-controlled queen {queen.ThingID}.");

            SendStandardLetter(parms, queen);
            return true;
        }

        private static bool HasLivingPlayerQueen()
        {
            PawnKindDef queenKind = GR_DefOf.SG1_GoauldQueen;

            if (queenKind == null)
            {
                return false;
            }

            foreach (Pawn pawn in PawnsFinder
                .AllMapsCaravansAndTravellingTransporters_Alive_OfPlayerFaction)
            {
                if (pawn.kindDef == queenKind)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool TryFindEntryCell(Map map, out IntVec3 cell)
        {
            return CellFinder.TryFindRandomEdgeCellWith(
                candidateCell => map.reachability.CanReachColony(candidateCell)
                    && !candidateCell.Fogged(map),
                map,
                CellFinder.EdgeRoadChance_Neutral,
                out cell);
        }
    }
}
