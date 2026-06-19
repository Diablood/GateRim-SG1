using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace GateRimSG1.Goauld
{
    public class JobDriver_TokraDeployObservationDevice : JobDriver
    {
        private const TargetIndex DeviceIndex = TargetIndex.A;
        private const TargetIndex ObservationCellIndex = TargetIndex.B;

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            Thing device = job.GetTarget(DeviceIndex).Thing;

            if (device == null)
            {
                return false;
            }

            if (pawn.carryTracker?.CarriedThing == device)
            {
                return true;
            }

            return pawn.Reserve(
                device,
                job,
                1,
                1,
                null,
                errorOnFailed);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOn(() => !GameComponent_TokraOrganicOperationManager
                .CanContinueObservationDeployment(
                    job.GetTarget(DeviceIndex).Thing,
                    pawn));

            if (pawn.carryTracker?.CarriedThing
                != job.GetTarget(DeviceIndex).Thing)
            {
                yield return Toils_Goto.GotoThing(
                    DeviceIndex,
                    PathEndMode.ClosestTouch);

                yield return Toils_Haul.StartCarryThing(DeviceIndex);
            }

            yield return Toils_Goto.GotoCell(
                ObservationCellIndex,
                PathEndMode.OnCell);

            Toil deploy = Toils_General.Wait(
                GameComponent_TokraOrganicOperationManager
                    .ObservationDeploymentWorkTicks,
                ObservationCellIndex);
            deploy.WithProgressBarToilDelay(ObservationCellIndex);
            deploy.WithEffect(
                EffecterDefOf.ConstructMetal,
                ObservationCellIndex);
            yield return deploy;

            Toil finish = ToilMaker.MakeToil(
                "DeployTokraObservationDevice");
            finish.initAction = delegate
            {
                GameComponent_TokraOrganicOperationManager
                    .NotifyObservationDeviceDeployed(
                        job.GetTarget(DeviceIndex).Thing,
                        pawn);
            };
            finish.defaultCompleteMode = ToilCompleteMode.Instant;
            yield return finish;
        }
    }
}
