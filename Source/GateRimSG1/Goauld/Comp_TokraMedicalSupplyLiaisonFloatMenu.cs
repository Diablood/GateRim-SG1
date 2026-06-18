using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace GateRimSG1.Goauld
{
    public class CompProperties_TokraMedicalSupplyLiaisonFloatMenu
        : CompProperties
    {
        public CompProperties_TokraMedicalSupplyLiaisonFloatMenu()
        {
            compClass = typeof(Comp_TokraMedicalSupplyLiaisonFloatMenu);
        }
    }

    public class Comp_TokraMedicalSupplyLiaisonFloatMenu : ThingComp
    {
        private const string DialogueJobDefName
            = "SG1_TalkToTokraMedicalSupplyLiaison";

        public override IEnumerable<FloatMenuOption> CompFloatMenuOptions(
            Pawn selPawn)
        {
            foreach (FloatMenuOption option in base.CompFloatMenuOptions(selPawn))
            {
                yield return option;
            }

            Pawn liaison = parent as Pawn;

            if (!GameComponent_TokraOrganicOperationTracker
                .IsMedicalSupplyLiaison(liaison))
            {
                yield break;
            }

            string label = "GR_TokraMedicalSupply_TalkToLiaison"
                .Translate(liaison.LabelShortCap);
            string disabledReason
                = GameComponent_TokraOrganicOperationTracker
                    .GetMedicalSupplyLiaisonDisabledReason(
                        liaison,
                        selPawn);

            if (!string.IsNullOrEmpty(disabledReason))
            {
                yield return new FloatMenuOption(
                    label + ": " + disabledReason,
                    null);
                yield break;
            }

            JobDef jobDef = DefDatabase<JobDef>.GetNamedSilentFail(
                DialogueJobDefName);

            if (jobDef == null)
            {
                yield return new FloatMenuOption(
                    label + ": "
                    + "GR_TokraMedicalSupply_JobUnavailable"
                        .Translate(),
                    null);
                yield break;
            }

            yield return new FloatMenuOption(
                label,
                delegate
                {
                    Job job = JobMaker.MakeJob(jobDef, liaison);
                    selPawn.jobs.TryTakeOrderedJob(job);
                });
        }
    }
}
