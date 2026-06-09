using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Rare natural Tok'ra therapeutic-opportunity prototype.
    ///
    /// The incident looks for a player-controlled compatible humanoid with at
    /// least one non-traumatic biological condition accepted by the shared
    /// Tok'ra healing filter. It then spawns one free Tok'ra symbiote at a
    /// reachable map edge and leaves the final implantation choice to the
    /// player's existing therapeutic-implantation command.
    /// </summary>
    public class IncidentWorker_TokraTherapeuticOpportunity : IncidentWorker
    {
        private const string TokraSymbiotePawnKindDefName
            = "SG1_TokraSymbiote";

        private const float MinimumVoluntaryHostAgeYears = 13f;

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            if (!base.CanFireNowSub(parms))
            {
                return false;
            }

            Map map = parms.target as Map;

            if (map == null
                || FindBestCandidate(map) == null
                || ResolveTokraSymbiotePawnKind() == null)
            {
                return false;
            }

            IntVec3 unusedEntryCell;
            return TryFindEntryCell(map, out unusedEntryCell);
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = parms.target as Map;
            Pawn candidate = FindBestCandidate(map);
            PawnKindDef symbioteKind = ResolveTokraSymbiotePawnKind();
            IntVec3 entryCell;

            if (map == null
                || candidate == null
                || symbioteKind == null
                || !TryFindEntryCell(map, out entryCell))
            {
                return false;
            }

            Pawn symbiote = PawnGenerator.GeneratePawn(symbioteKind);

            if (symbiote == null)
            {
                GR_Log.Error(
                    "Unable to generate the free Tok'ra symbiote for a "
                    + "therapeutic-opportunity incident.");
                return false;
            }

            GenSpawn.Spawn(symbiote, entryCell, map);

            string conditionLabels
                = GameComponent_TokraTherapeuticHosting
                    .GetSeriousTherapeuticNeedLabels(candidate);

            GR_Log.Message(
                $"Started Tok'ra therapeutic opportunity for "
                + $"{PawnDebugLabel(candidate)} with conditions "
                + $"{conditionLabels}; spawned free symbiote "
                + $"{PawnDebugLabel(symbiote)} at {entryCell}.");

            SendStandardLetter(
                parms,
                symbiote,
                candidate.Named("PAWN"),
                conditionLabels.Named("CONDITIONS"));

            return true;
        }

        private static Pawn FindBestCandidate(Map map)
        {
            IReadOnlyList<Pawn> pawns = map?.mapPawns?.AllPawnsSpawned;

            if (pawns == null)
            {
                return null;
            }

            Pawn bestCandidate = null;
            float bestScore = 0f;

            for (int index = 0; index < pawns.Count; index++)
            {
                Pawn candidate = pawns[index];

                if (!IsValidCandidate(candidate))
                {
                    continue;
                }

                float score
                    = GameComponent_TokraTherapeuticHosting
                        .GetSeriousTherapeuticNeedScore(candidate);

                if (bestCandidate == null || score > bestScore)
                {
                    bestCandidate = candidate;
                    bestScore = score;
                }
            }

            return bestCandidate;
        }

        private static bool IsValidCandidate(Pawn pawn)
        {
            return pawn != null
                && pawn.Spawned
                && !pawn.Destroyed
                && !pawn.Dead
                && pawn.Faction == Faction.OfPlayer
                && pawn.RaceProps.Humanlike
                && pawn.health?.hediffSet?.hediffs != null
                && (pawn.ageTracker == null
                    || pawn.ageTracker.AgeBiologicalYearsFloat
                        >= MinimumVoluntaryHostAgeYears)
                && !HasExistingSymbioteState(pawn)
                && GameComponent_TokraTherapeuticHosting
                    .HasSeriousTherapeuticNeed(pawn);
        }

        private static bool HasExistingSymbioteState(Pawn pawn)
        {
            List<Hediff> hediffs = pawn.health.hediffSet.hediffs;

            for (int index = 0; index < hediffs.Count; index++)
            {
                HediffDef def = hediffs[index].def;

                if (def == GR_DefOf.SG1_GoauldRecentImplantation
                    || def == GR_DefOf.SG1_GoauldHostSymbiote)
                {
                    return true;
                }
            }

            return false;
        }

        private static PawnKindDef ResolveTokraSymbiotePawnKind()
        {
            return DefDatabase<PawnKindDef>.GetNamedSilentFail(
                TokraSymbiotePawnKindDefName);
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

        private static string PawnDebugLabel(Pawn pawn)
        {
            if (pawn == null)
            {
                return "<null pawn>";
            }

            return $"{pawn.LabelShort} ({pawn.ThingID})";
        }
    }
}
