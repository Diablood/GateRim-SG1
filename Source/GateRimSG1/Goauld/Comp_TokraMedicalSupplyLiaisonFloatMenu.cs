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
        public override IEnumerable<FloatMenuOption> CompFloatMenuOptions(
            Pawn selPawn)
        {
            foreach (FloatMenuOption option in base.CompFloatMenuOptions(selPawn))
            {
                yield return option;
            }

            Pawn liaison = parent as Pawn;
            if (!GameComponent_TokraOrganicOperationManager
                .IsMedicalSupplyLiaison(liaison))
            {
                yield break;
            }

            string talkKey
                = GameComponent_TokraOrganicOperationManager
                    .GetMedicalSupplyRuntimeTextKey("talkToLiaison");
            string label = talkKey.Translate(liaison.LabelShortCap);
            string disabledReason
                = GameComponent_TokraOrganicOperationManager
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

            JobDef jobDef
                = GameComponent_TokraOrganicOperationManager
                    .GetMedicalSupplyDialogueJobDef();

            if (jobDef == null)
            {
                string unavailableKey
                    = GameComponent_TokraOrganicOperationManager
                        .GetMedicalSupplyRuntimeTextKey("jobUnavailable");
                yield return new FloatMenuOption(
                    label + ": " + unavailableKey.Translate(),
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
