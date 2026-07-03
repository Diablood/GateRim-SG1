using System;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public static class KaraKeshNeuralAttackUtility
    {
        public static bool CanAffect(Pawn pawn)
        {
            return pawn != null
                && !pawn.Destroyed
                && !pawn.Dead
                && !pawn.Downed
                && pawn.health?.hediffSet != null
                && pawn.RaceProps?.Humanlike == true
                && pawn.RaceProps.IsFlesh
                && pawn.health.capacities.CapableOf(
                    PawnCapacityDefOf.Consciousness);
        }

        public static bool HasEffect(Pawn pawn, HediffDef hediffDef)
        {
            return pawn?.health?.hediffSet != null
                && hediffDef != null
                && pawn.health.hediffSet.GetFirstHediffOfDef(
                    hediffDef) != null;
        }

        public static bool TryApply(
            Pawn pawn,
            HediffDef hediffDef,
            int durationTicks)
        {
            if (!CanAffect(pawn) || hediffDef == null)
            {
                return false;
            }

            Hediff effect = pawn.health.hediffSet.GetFirstHediffOfDef(
                hediffDef);

            if (effect == null)
            {
                effect = pawn.health.AddHediff(hediffDef);
            }

            if (effect == null)
            {
                return false;
            }

            HediffComp_Disappears disappears = effect
                .TryGetComp<HediffComp_Disappears>();
            disappears?.SetDuration(Math.Max(1, durationTicks));
            return true;
        }

        public static bool TryClear(Pawn pawn, HediffDef hediffDef)
        {
            if (pawn?.health?.hediffSet == null || hediffDef == null)
            {
                return false;
            }

            Hediff effect = pawn.health.hediffSet
                .GetFirstHediffOfDef(hediffDef);

            if (effect == null)
            {
                return false;
            }

            pawn.health.RemoveHediff(effect);
            return true;
        }
    }
}
