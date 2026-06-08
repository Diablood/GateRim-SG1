using RimWorld;
using Verse;

namespace GateRimSG1.Jaffa
{
    /// <summary>
    /// Shared compatibility checks for Jaffa Prim'ta mechanics.
    ///
    /// The inherited Jaffa lineage remains distinct from the acquired Prim'ta
    /// health state. Future ceremony, larva-resource and tretonin systems should
    /// reuse this utility instead of duplicating eligibility rules.
    /// </summary>
    public static class JaffaPrimtaUtility
    {
        public const int MinimumPrimtaImplantationBiologicalAge = 10;

        public static bool IsCompatibleJaffa(Pawn pawn)
        {
            return pawn != null
                && pawn.genes != null
                && pawn.genes.HasActiveGene(GR_DefOf.SG1_JaffaLineage)
                && pawn.genes.HasActiveGene(GR_DefOf.SG1_JaffaPouchPotential)
                && pawn.genes.HasActiveGene(GR_DefOf.SG1_JaffaSymbioteCompatibility);
        }

        public static bool MeetsPrimtaImplantationAge(Pawn pawn)
        {
            return pawn?.ageTracker != null
                && pawn.ageTracker.AgeBiologicalYears
                    >= MinimumPrimtaImplantationBiologicalAge;
        }

        public static bool IsEligibleForPrimtaImplantation(Pawn pawn)
        {
            return IsCompatibleJaffa(pawn)
                && MeetsPrimtaImplantationAge(pawn)
                && !HasPrimta(pawn);
        }

        public static bool HasPrimta(Pawn pawn)
        {
            if (pawn?.health?.hediffSet?.hediffs == null)
            {
                return false;
            }

            for (int index = 0; index < pawn.health.hediffSet.hediffs.Count; index++)
            {
                if (pawn.health.hediffSet.hediffs[index].def
                    == GR_DefOf.SG1_JaffaPrimta)
                {
                    return true;
                }
            }

            return false;
        }

        public static string PawnDebugLabel(Pawn pawn)
        {
            if (pawn == null)
            {
                return "<null pawn>";
            }

            return $"{pawn.LabelShort} ({pawn.ThingID})";
        }
    }
}
