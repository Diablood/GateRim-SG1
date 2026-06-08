using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Jaffa
{
    /// <summary>
    /// First medical tretonin-administration prototype.
    ///
    /// The bill consumes one physical SG1_TretoninDose. The temporary Hediff
    /// suppresses the puberty dependency for one in-game day.
    /// </summary>
    public class Recipe_AdministerTretonin : Recipe_Surgery
    {
        public override bool AvailableOnNow(Thing thing, BodyPartRecord part = null)
        {
            if (!base.AvailableOnNow(thing, part))
            {
                return false;
            }

            Pawn pawn = thing as Pawn;

            return JaffaPrimtaUtility.IsEligibleForTretoninAdministration(pawn);
        }

        public override void ApplyOnPawn(
            Pawn pawn,
            BodyPartRecord part,
            Pawn billDoer,
            List<Thing> ingredients,
            Bill bill)
        {
            if (!JaffaPrimtaUtility.IsEligibleForTretoninAdministration(pawn))
            {
                GR_Log.Warning(
                    $"Skipped tretonin administration for "
                    + $"{JaffaPrimtaUtility.PawnDebugLabel(pawn)} "
                    + "because the patient is not an eligible Jaffa.");

                return;
            }

            if (!ContainsTretoninDose(ingredients))
            {
                GR_Log.Error(
                    $"Skipped tretonin administration for "
                    + $"{JaffaPrimtaUtility.PawnDebugLabel(pawn)} "
                    + "because the physical tretonin dose ingredient is missing.");

                return;
            }

            Hediff substitution = HediffMaker.MakeHediff(
                GR_DefOf.SG1_TretoninSubstitution,
                pawn);

            pawn.health.AddHediff(substitution);

            GR_Log.Message(
                $"Tretonin dose administered to "
                + $"{JaffaPrimtaUtility.PawnDebugLabel(pawn)} "
                + $"by {JaffaPrimtaUtility.PawnDebugLabel(billDoer)}.");

            Messages.Message(
                "GR_TretoninAdministration_Success".Translate(
                    pawn.LabelShortCap),
                pawn,
                MessageTypeDefOf.PositiveEvent,
                historical: true);
        }

        private static bool ContainsTretoninDose(List<Thing> ingredients)
        {
            if (ingredients == null)
            {
                return false;
            }

            for (int index = 0; index < ingredients.Count; index++)
            {
                Thing ingredient = ingredients[index];

                if (ingredient != null
                    && ingredient.def == GR_DefOf.SG1_TretoninDose)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
