using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Jaffa
{
    /// <summary>
    /// First medical Prim'ta implantation prototype.
    ///
    /// The operation validates that the patient carries the inherited Jaffa
    /// compatibility genes and does not already host an immature symbiote.
    /// Since 0.1.27-dev, the vanilla bill must supply one physical
    /// SG1_PrimtaLarva ingredient. Age ceremony, acquisition and dependency
    /// systems remain future milestones.
    /// </summary>
    public class Recipe_ImplantJaffaPrimta : Recipe_Surgery
    {
        public override bool AvailableOnNow(Thing thing, BodyPartRecord part = null)
        {
            if (!base.AvailableOnNow(thing, part))
            {
                return false;
            }

            Pawn pawn = thing as Pawn;

            return JaffaPrimtaUtility.IsEligibleForPrimtaImplantation(pawn);
        }

        public override void ApplyOnPawn(
            Pawn pawn,
            BodyPartRecord part,
            Pawn billDoer,
            List<Thing> ingredients,
            Bill bill)
        {
            if (!JaffaPrimtaUtility.IsCompatibleJaffa(pawn))
            {
                GR_Log.Warning(
                    $"Skipped Jaffa Prim'ta implantation for "
                    + $"{JaffaPrimtaUtility.PawnDebugLabel(pawn)} "
                    + "because the patient is not a compatible Jaffa.");

                return;
            }

            if (!JaffaPrimtaUtility.MeetsPrimtaImplantationAge(pawn))
            {
                GR_Log.Warning(
                    $"Skipped Jaffa Prim'ta implantation for "
                    + $"{JaffaPrimtaUtility.PawnDebugLabel(pawn)} "
                    + $"because the patient is younger than "
                    + $"{JaffaPrimtaUtility.MinimumPrimtaImplantationBiologicalAge} "
                    + "biological years.");

                Messages.Message(
                    "GR_JaffaPrimtaImplantation_TooYoung".Translate(
                        pawn.LabelShortCap,
                        JaffaPrimtaUtility.MinimumPrimtaImplantationBiologicalAge),
                    pawn,
                    MessageTypeDefOf.RejectInput,
                    historical: false);

                return;
            }

            if (JaffaPrimtaUtility.HasPrimta(pawn))
            {
                GR_Log.Warning(
                    $"Skipped duplicate Jaffa Prim'ta implantation for "
                    + $"{JaffaPrimtaUtility.PawnDebugLabel(pawn)}.");

                return;
            }

            if (!ContainsPrimtaLarva(ingredients))
            {
                GR_Log.Error(
                    $"Skipped Jaffa Prim'ta implantation for "
                    + $"{JaffaPrimtaUtility.PawnDebugLabel(pawn)} "
                    + "because the physical Prim'ta larva ingredient is missing.");

                return;
            }

            if (billDoer != null)
            {
                if (CheckSurgeryFail(billDoer, pawn, ingredients, part, bill))
                {
                    GR_Log.Warning(
                        $"Jaffa Prim'ta implantation surgery failed for "
                        + $"{JaffaPrimtaUtility.PawnDebugLabel(pawn)}.");

                    return;
                }

                TaleRecorder.RecordTale(TaleDefOf.DidSurgery, billDoer, pawn);
            }

            Hediff primta = HediffMaker.MakeHediff(
                GR_DefOf.SG1_JaffaPrimta,
                pawn);

            pawn.health.AddHediff(primta);

            GR_Log.Message(
                $"Jaffa Prim'ta implantation surgery completed for "
                + $"{JaffaPrimtaUtility.PawnDebugLabel(pawn)} "
                + $"using physical Prim'ta larva with surgeon "
                + $"{JaffaPrimtaUtility.PawnDebugLabel(billDoer)}.");

            Messages.Message(
                "GR_JaffaPrimtaImplantation_Success".Translate(
                    pawn.LabelShortCap),
                pawn,
                MessageTypeDefOf.PositiveEvent,
                historical: true);
        }
        private static bool ContainsPrimtaLarva(List<Thing> ingredients)
        {
            if (ingredients == null)
            {
                return false;
            }

            for (int index = 0; index < ingredients.Count; index++)
            {
                Thing ingredient = ingredients[index];

                if (ingredient != null
                    && ingredient.def == GR_DefOf.SG1_PrimtaLarva)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
