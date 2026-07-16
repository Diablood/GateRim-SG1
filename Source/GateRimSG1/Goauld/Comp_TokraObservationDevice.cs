using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace GateRimSG1.Goauld
{
    public class Comp_TokraObservationDevice : ThingComp
    {
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

            string label
                = GameComponent_TokraOrganicOperationManager
                    .GetObservationDeploymentActionLabel();
            if (string.IsNullOrEmpty(label))
            {
                yield break;
            }

            TokraObservationPointDeploymentUtility
                .EnsureMarkerForDeployment(parent);

            string disabledReason
                = GameComponent_TokraOrganicOperationManager
                    .GetObservationDeploymentDisabledReason(parent, selPawn);
            JobDef jobDef
                = GameComponent_TokraOrganicOperationManager
                    .GetObservationDeploymentJobDef();

            if (jobDef == null && string.IsNullOrEmpty(disabledReason))
            {
                disabledReason
                    = GameComponent_TokraOrganicOperationManager
                        .GetObservationJobUnavailableText();
            }

            if (!string.IsNullOrEmpty(disabledReason))
            {
                yield return new FloatMenuOption(
                    label + ": " + disabledReason,
                    null);
                yield break;
            }

            Thing observationPoint
                = GameComponent_TokraOrganicOperationManager
                    .GetObservationPoint(parent.Map);
            Thing communicator
                = GameComponent_TokraOrganicOperationManager
                    .GetPoweredObservationCommunicator(parent.Map);

            yield return new FloatMenuOption(
                label,
                delegate
                {
                    Job job = JobMaker.MakeJob(
                        jobDef,
                        parent,
                        observationPoint,
                        communicator);
                    job.count = 1;
                    selPawn.jobs.TryTakeOrderedJob(job);
                });
        }
    }
}
