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
        public const int MinimumPrimtaDependencyBiologicalAge = 12;
        public const float PrimtaDependencySeverityPerDay = 0.1f;
        public const float MaximumPrimtaDependencySeverity = 1f;

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

        public static bool ShouldHavePrimtaDependency(Pawn pawn)
        {
            return IsCompatibleJaffa(pawn)
                && pawn?.ageTracker != null
                && pawn.ageTracker.AgeBiologicalYears
                    >= MinimumPrimtaDependencyBiologicalAge
                && !HasPrimta(pawn)
                && !HasTretoninSubstitution(pawn);
        }

        public static Hediff GetPrimtaDependency(Pawn pawn)
        {
            if (pawn?.health?.hediffSet?.hediffs == null)
            {
                return null;
            }

            for (int index = 0; index < pawn.health.hediffSet.hediffs.Count; index++)
            {
                Hediff hediff = pawn.health.hediffSet.hediffs[index];

                if (hediff.def == GR_DefOf.SG1_JaffaPrimtaDependency)
                {
                    return hediff;
                }
            }

            return null;
        }

        public static bool RemovePrimtaDependency(
            Pawn pawn,
            bool showMessage)
        {
            Hediff dependency = GetPrimtaDependency(pawn);

            if (dependency == null)
            {
                return false;
            }

            pawn.health.RemoveHediff(dependency);

            GR_Log.Message(
                $"Removed Jaffa Prim'ta dependency from "
                + $"{PawnDebugLabel(pawn)}.");

            if (showMessage)
            {
                Messages.Message(
                    "GR_JaffaPrimtaDependency_Relieved".Translate(
                        pawn.LabelShortCap),
                    pawn,
                    MessageTypeDefOf.PositiveEvent,
                    historical: true);
            }

            return true;
        }

        public static Hediff GetTretoninSubstitution(Pawn pawn)
        {
            if (pawn?.health?.hediffSet?.hediffs == null)
            {
                return null;
            }

            for (int index = 0; index < pawn.health.hediffSet.hediffs.Count; index++)
            {
                Hediff hediff = pawn.health.hediffSet.hediffs[index];

                if (hediff.def == GR_DefOf.SG1_TretoninSubstitution)
                {
                    return hediff;
                }
            }

            return null;
        }

        public static bool HasTretoninSubstitution(Pawn pawn)
        {
            return GetTretoninSubstitution(pawn) != null;
        }

        public static bool IsEligibleForTretoninAdministration(Pawn pawn)
        {
            return IsCompatibleJaffa(pawn)
                && pawn?.ageTracker != null
                && pawn.ageTracker.AgeBiologicalYears
                    >= MinimumPrimtaDependencyBiologicalAge
                && !HasPrimta(pawn)
                && !HasTretoninSubstitution(pawn);
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
