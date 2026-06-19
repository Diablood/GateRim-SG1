using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace GateRimSG1.Goauld
{
    public class Comp_TokraObservationDevice : ThingComp
    {
        private const string DeployJobDefName
            = "SG1_DeployTokraObservationDevice";

        public override IEnumerable<FloatMenuOption> CompFloatMenuOptions(
            Pawn selPawn)
        {
            foreach (FloatMenuOption option in base.CompFloatMenuOptions(selPawn))
            {
                yield return option;
            }

            if (!parent.Spawned)
            {
                yield break;
            }

            string label = "GR_TokraObservation_DeployAction"
                .Translate()
                .ToString();
            string disabledReason
                = GameComponent_TokraOrganicOperationManager
                    .GetObservationDeploymentDisabledReason(parent, selPawn);
            JobDef jobDef = DefDatabase<JobDef>.GetNamedSilentFail(
                DeployJobDefName);

            if (jobDef == null && string.IsNullOrEmpty(disabledReason))
            {
                disabledReason = "GR_TokraObservation_JobUnavailable"
                    .Translate()
                    .ToString();
            }

            if (!string.IsNullOrEmpty(disabledReason))
            {
                yield return new FloatMenuOption(
                    label + ": " + disabledReason,
                    null);
                yield break;
            }

            IntVec3 targetCell
                = GameComponent_TokraOrganicOperationManager
                    .GetObservationTargetCell(parent.Map);

            yield return new FloatMenuOption(
                label,
                delegate
                {
                    Job job = JobMaker.MakeJob(
                        jobDef,
                        parent,
                        new LocalTargetInfo(targetCell));
                    job.count = 1;
                    selPawn.jobs.TryTakeOrderedJob(job);
                });
        }
    }
}
