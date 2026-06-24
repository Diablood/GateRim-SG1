using RimWorld;
using UnityEngine;
using Verse;

namespace GateRimSG1.Weapons
{
    /// <summary>
    /// Shared neutralization rules for the early bolas and the experimental
    /// Tok'ra hypodermic rifle.
    ///
    /// A successful effect applies a temporary neuromuscular inhibition
    /// Hediff that removes Moving capacity while leaving Consciousness intact.
    /// RimWorld then uses its ordinary downed and capture flow.
    /// </summary>
    public static class NonLethalCaptureUtility
    {
        public static float CalculateNeutralizationChance(
            Pawn pawn,
            NonLethalCaptureProjectileExtension profile)
        {
            if (!CanAffect(pawn, profile))
            {
                return 0f;
            }

            float chance = profile.baseNeutralizationChance;
            float excessBodySize = Mathf.Max(0f, pawn.BodySize - 1f);
            chance -= excessBodySize * profile.bodySizePenaltyPerPoint;

            if (profile.armorStat != null)
            {
                float armor = Mathf.Max(
                    0f,
                    pawn.GetStatValue(profile.armorStat));
                chance *= Mathf.Clamp01(
                    1f - armor * profile.armorResistanceFactor);
            }

            return Mathf.Clamp(
                chance,
                profile.minimumNeutralizationChance,
                profile.maximumNeutralizationChance);
        }

        public static bool TryNeutralize(
            Pawn pawn,
            NonLethalCaptureProjectileExtension profile)
        {
            float chance = CalculateNeutralizationChance(pawn, profile);

            if (chance <= 0f)
            {
                ThrowResultText(pawn, "GR_NonLethalCapture_NoEffect");
                return false;
            }

            if (!Rand.Chance(chance))
            {
                ThrowResultText(pawn, "GR_NonLethalCapture_Resisted");
                return false;
            }

            int duration = profile.neutralizationTicks.RandomInRange;
            Hediff neutralization = pawn.health.hediffSet
                .GetFirstHediffOfDef(profile.neutralizationHediff);

            if (neutralization == null)
            {
                // Add the capacity Hediff before the projectile resolves its
                // light blunt impact. The resulting downing is caused by the
                // Hediff itself rather than by external-violence DamageInfo.
                neutralization = pawn.health.AddHediff(
                    profile.neutralizationHediff);
            }

            HediffComp_Disappears disappears = neutralization
                ?.TryGetComp<HediffComp_Disappears>();
            disappears?.SetDuration(duration);

            ThrowResultText(pawn, "GR_NonLethalCapture_Neutralized");
            return true;
        }

        public static bool HasTemporaryNeutralization(Pawn pawn)
        {
            if (pawn?.health?.hediffSet == null)
            {
                return false;
            }

            return pawn.health.hediffSet.GetFirstHediffOfDef(
                    GR_DefOf.SG1_NonLethalNeutralization) != null
                || pawn.health.hediffSet.GetFirstHediffOfDef(
                    GR_DefOf.SG1_BolasRestraint) != null;
        }

        public static void ClearTemporaryNeutralization(Pawn pawn)
        {
            if (pawn?.health?.hediffSet == null)
            {
                return;
            }

            RemoveHediffIfPresent(
                pawn,
                GR_DefOf.SG1_NonLethalNeutralization);
            RemoveHediffIfPresent(
                pawn,
                GR_DefOf.SG1_BolasRestraint);
        }

        private static void RemoveHediffIfPresent(
            Pawn pawn,
            HediffDef hediffDef)
        {
            Hediff hediff = pawn.health.hediffSet
                .GetFirstHediffOfDef(hediffDef);

            if (hediff != null)
            {
                pawn.health.RemoveHediff(hediff);
            }
        }

        public static bool CanAffect(
            Pawn pawn,
            NonLethalCaptureProjectileExtension profile)
        {
            if (pawn == null
                || profile == null
                || profile.neutralizationHediff == null
                || pawn.Dead
                || pawn.Downed
                || pawn.health == null
                || pawn.RaceProps == null)
            {
                return false;
            }

            if (profile.humanlikeOnly && !pawn.RaceProps.Humanlike)
            {
                return false;
            }

            if (profile.requireFlesh && !pawn.RaceProps.IsFlesh)
            {
                return false;
            }

            return pawn.health.capacities
                .CapableOf(PawnCapacityDefOf.Moving);
        }

        private static void ThrowResultText(Pawn pawn, string translationKey)
        {
            if (pawn?.Spawned != true
                || pawn.Map == null
                || pawn.Position.Fogged(pawn.Map))
            {
                return;
            }

            MoteMaker.ThrowText(
                pawn.DrawPos,
                pawn.Map,
                translationKey.Translate());
        }
    }
}
