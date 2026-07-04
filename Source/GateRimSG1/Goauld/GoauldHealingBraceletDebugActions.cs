using System.Linq;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    internal static class GoauldHealingBraceletDebugActions
    {
        public static void PrepareWearer(Pawn pawn)
        {
            if (pawn?.apparel == null)
            {
                Report("Choose a humanlike pawn that can wear apparel.", pawn, false);
                return;
            }

            Comp_GoauldHealingBracelet comp = GetComp(pawn);

            if (comp == null)
            {
                Apparel bracelet = ThingMaker.MakeThing(
                    GR_DefOf.SG1_GoauldHealingBracelet) as Apparel;

                if (bracelet == null)
                {
                    Report("The healing-bracelet definition did not create apparel.", pawn, false);
                    return;
                }

                bracelet.TryGetComp<CompQuality>()?.SetQuality(
                    QualityCategory.Normal,
                    ArtGenerationContext.Outsider);
                pawn.apparel.Wear(bracelet, dropReplacedApparel: false);
                comp = GetComp(pawn);

                if (comp == null && !bracelet.Destroyed)
                {
                    bracelet.Destroy(DestroyMode.Vanish);
                }
            }

            if (comp == null)
            {
                Report("Could not equip the Goa'uld healing bracelet.", pawn, false);
                return;
            }

            NaquadahTraceUtility.EnsurePersistentTrace(
                pawn,
                "healing bracelet developer action");
            comp.ResetForDebug();
            Report(
                "Equipped the healing bracelet, applied naquadah traces and reset its test state.",
                pawn,
                true);
        }

        public static void PrepareBleedingPatient(Pawn pawn)
        {
            if (pawn?.health == null
                || pawn.RaceProps?.Humanlike != true
                || pawn.Dead)
            {
                Report("Choose a living biological humanlike pawn.", pawn, false);
                return;
            }

            BodyPartRecord[] bodyParts = pawn.health.hediffSet
                .GetNotMissingParts()
                .Where(part => part.depth == BodyPartDepth.Outside)
                .OrderByDescending(part => part.coverageAbs)
                .Take(3)
                .ToArray();

            for (int index = 0; index < bodyParts.Length; index++)
            {
                Hediff_Injury injury = HediffMaker.MakeHediff(
                    HediffDefOf.Cut,
                    pawn,
                    bodyParts[index]) as Hediff_Injury;

                if (injury == null)
                {
                    continue;
                }

                injury.Severity = 7f;
                pawn.health.AddHediff(injury);
            }

            Hediff bloodLoss = pawn.health.hediffSet.GetFirstHediffOfDef(
                HediffDefOf.BloodLoss);

            if (bloodLoss == null)
            {
                bloodLoss = HediffMaker.MakeHediff(HediffDefOf.BloodLoss, pawn);
                bloodLoss.Severity = 0.3f;
                pawn.health.AddHediff(bloodLoss);
            }
            else
            {
                bloodLoss.Severity = System.Math.Max(
                    bloodLoss.Severity,
                    0.3f);
            }

            Report(
                "Applied three controlled cut injuries and at least 30% blood loss for the healing test.",
                pawn,
                true);
        }

        public static void InspectState(Pawn pawn)
        {
            Comp_GoauldHealingBracelet comp = GetComp(pawn);

            if (comp == null)
            {
                Report("The target is not wearing a Goa'uld healing bracelet.", pawn, false);
                return;
            }

            int injuryCount = pawn.health?.hediffSet?.hediffs
                ?.OfType<Hediff_Injury>()
                .Count(injury => injury.Severity > 0f) ?? 0;
            float bloodLoss = pawn.health?.hediffSet
                ?.GetFirstHediffOfDef(HediffDefOf.BloodLoss)
                ?.Severity ?? 0f;
            bool fatigue = pawn.health?.hediffSet?.HasHediff(
                GR_DefOf.SG1_GoauldHealingBraceletFatigue) == true;

            Report(
                $"Healing bracelet state for {pawn.LabelShortCap}: "
                + $"trace={NaquadahTraceUtility.HasPersistentTrace(pawn)}, "
                + $"cooldownTicks={comp.CooldownRemainingTicks}, "
                + $"fatigue={fatigue}, injuries={injuryCount}, "
                + $"bloodLoss={bloodLoss:0.00}.",
                pawn,
                true);
        }

        public static void UseSelectedWearerBracelet(Pawn patient)
        {
            Pawn wearer = Find.Selector?.SingleSelectedThing as Pawn;
            Comp_GoauldHealingBracelet comp = GetComp(wearer);

            if (comp == null)
            {
                Report(
                    "Select a pawn wearing a healing bracelet before choosing the patient.",
                    patient,
                    false);
                return;
            }

            comp.TryHeal(patient);
        }

        public static void ResetState(Pawn pawn)
        {
            Comp_GoauldHealingBracelet comp = GetComp(pawn);

            if (comp == null)
            {
                Report("The target is not wearing a Goa'uld healing bracelet.", pawn, false);
                return;
            }

            comp.ResetForDebug();
            Report("Reset healing-bracelet cooldown and healer fatigue.", pawn, true);
        }

        private static Comp_GoauldHealingBracelet GetComp(Pawn pawn)
        {
            Apparel bracelet = pawn?.apparel?.WornApparel.FirstOrDefault(
                apparel => apparel?.def == GR_DefOf.SG1_GoauldHealingBracelet);
            return bracelet?.TryGetComp<Comp_GoauldHealingBracelet>();
        }

        private static void Report(
            string message,
            Pawn pawn,
            bool success)
        {
            Messages.Message(
                message,
                pawn,
                success
                    ? MessageTypeDefOf.NeutralEvent
                    : MessageTypeDefOf.RejectInput,
                historical: false);
        }
    }
}
