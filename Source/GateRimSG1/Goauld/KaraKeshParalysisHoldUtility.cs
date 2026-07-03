using System;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public static class KaraKeshParalysisHoldUtility
    {
        public static bool CanAffect(Pawn pawn, HediffDef hediffDef)
        {
            return pawn != null
                && !pawn.Destroyed
                && !pawn.Dead
                && !pawn.Downed
                && pawn.health?.hediffSet != null
                && pawn.RaceProps?.Humanlike == true
                && pawn.RaceProps.IsFlesh
                && pawn.health.capacities.CapableOf(
                    PawnCapacityDefOf.Consciousness)
                && !HasEffect(pawn, hediffDef);
        }

        public static bool HasEffect(Pawn pawn, HediffDef hediffDef)
        {
            return pawn?.health?.hediffSet != null
                && hediffDef != null
                && pawn.health.hediffSet.GetFirstHediffOfDef(
                    hediffDef) != null;
        }

        public static bool IsHeldBy(
            Pawn pawn,
            HediffDef hediffDef,
            Apparel sourceKaraKesh)
        {
            if (hediffDef == null || sourceKaraKesh == null)
            {
                return false;
            }

            Hediff effect = pawn?.health?.hediffSet
                ?.GetFirstHediffOfDef(hediffDef);
            HediffComp_KaraKeshParalysisHold comp = effect
                ?.TryGetComp<HediffComp_KaraKeshParalysisHold>();
            return comp != null && comp.IsMaintainedBy(sourceKaraKesh);
        }

        public static bool TryApply(
            Pawn pawn,
            HediffDef hediffDef,
            int durationTicks,
            Apparel sourceKaraKesh)
        {
            if (!CanAffect(pawn, hediffDef)
                || hediffDef == null
                || sourceKaraKesh == null)
            {
                return false;
            }

            Hediff effect = pawn.health.AddHediff(hediffDef);

            if (effect == null)
            {
                return false;
            }

            HediffComp_KaraKeshParalysisHold holdComp = effect
                .TryGetComp<HediffComp_KaraKeshParalysisHold>();
            HediffComp_Disappears disappears = effect
                .TryGetComp<HediffComp_Disappears>();

            if (holdComp == null || disappears == null)
            {
                pawn.health.RemoveHediff(effect);
                return false;
            }

            holdComp.InitializeSource(sourceKaraKesh);
            disappears.SetDuration(Math.Max(1, durationTicks));
            return true;
        }

        public static bool TryClear(
            Pawn pawn,
            HediffDef hediffDef,
            Apparel sourceKaraKesh = null)
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

            if (sourceKaraKesh != null)
            {
                HediffComp_KaraKeshParalysisHold holdComp = effect
                    .TryGetComp<HediffComp_KaraKeshParalysisHold>();

                if (holdComp == null
                    || !holdComp.IsMaintainedBy(sourceKaraKesh))
                {
                    return false;
                }
            }

            pawn.health.RemoveHediff(effect);
            return true;
        }
    }
}
