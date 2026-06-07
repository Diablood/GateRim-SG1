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
    /// A dedicated physical larva resource, age ceremony and dependency system
    /// remain future milestones.
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

            return JaffaPrimtaUtility.IsCompatibleJaffa(pawn)
                && !JaffaPrimtaUtility.HasPrimta(pawn);
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

            if (JaffaPrimtaUtility.HasPrimta(pawn))
            {
                GR_Log.Warning(
                    $"Skipped duplicate Jaffa Prim'ta implantation for "
                    + $"{JaffaPrimtaUtility.PawnDebugLabel(pawn)}.");

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
                + $"with surgeon {JaffaPrimtaUtility.PawnDebugLabel(billDoer)}.");

            Messages.Message(
                "GR_JaffaPrimtaImplantation_Success".Translate(
                    pawn.LabelShortCap),
                pawn,
                MessageTypeDefOf.PositiveEvent,
                historical: true);
        }
    }
}
