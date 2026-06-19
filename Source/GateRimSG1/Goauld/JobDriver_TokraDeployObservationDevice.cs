using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace GateRimSG1.Goauld
{
    public class JobDriver_TokraDeployObservationDevice : JobDriver
    {
        private const TargetIndex DeviceIndex = TargetIndex.A;
        private const TargetIndex ObservationPointIndex = TargetIndex.B;
        private const TargetIndex CommunicatorIndex = TargetIndex.C;

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            Thing device = job.GetTarget(DeviceIndex).Thing;
            Thing observationPoint = job.GetTarget(ObservationPointIndex).Thing;
            Thing communicator = job.GetTarget(CommunicatorIndex).Thing;

            if (device == null
                || observationPoint == null
                || communicator == null)
            {
                return false;
            }

            if (pawn.carryTracker?.CarriedThing != device
                && !pawn.Reserve(
                    device,
                    job,
                    1,
                    1,
                    null,
                    errorOnFailed))
            {
                return false;
            }

            if (!pawn.Reserve(
                    observationPoint,
                    job,
                    1,
                    -1,
                    null,
                    errorOnFailed))
            {
                return false;
            }

            return pawn.Reserve(
                communicator,
                job,
                1,
                -1,
                null,
                errorOnFailed);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            Thing initialDevice = job.GetTarget(DeviceIndex).Thing;

            if (pawn.carryTracker?.CarriedThing != initialDevice)
            {
                Toil gotoDevice = Toils_Goto.GotoThing(
                    DeviceIndex,
                    PathEndMode.ClosestTouch);
                gotoDevice.FailOn(() => !GameComponent_TokraOrganicOperationManager
                    .CanContinueObservationDeployment(
                        job.GetTarget(DeviceIndex).Thing,
                        pawn));
                yield return gotoDevice;

                yield return Toils_Haul.StartCarryThing(DeviceIndex);
            }

            Toil gotoObservationPoint = Toils_Goto.GotoThing(
                ObservationPointIndex,
                PathEndMode.Touch);
            gotoObservationPoint.FailOn(() =>
                !GameComponent_TokraOrganicOperationManager
                    .CanContinueObservationDeployment(
                        job.GetTarget(DeviceIndex).Thing,
                        pawn));
            yield return gotoObservationPoint;

            Toil deploy = Toils_General.Wait(
                GameComponent_TokraOrganicOperationManager
                    .ObservationDeploymentWorkTicks,
                ObservationPointIndex);
            deploy.WithProgressBarToilDelay(ObservationPointIndex);
            deploy.FailOn(() =>
                !GameComponent_TokraOrganicOperationManager
                    .CanContinueObservationDeployment(
                        job.GetTarget(DeviceIndex).Thing,
                        pawn));
            yield return deploy;

            Toil finishDeployment = ToilMaker.MakeToil(
                "DeployTokraObservationDevice");
            finishDeployment.initAction = delegate
            {
                if (!GameComponent_TokraOrganicOperationManager
                    .NotifyObservationDeviceDeployed(
                        job.GetTarget(DeviceIndex).Thing,
                        pawn))
                {
                    EndJobWith(JobCondition.Incompletable);
                }
            };
            finishDeployment.defaultCompleteMode = ToilCompleteMode.Instant;
            yield return finishDeployment;

            Toil observe = ToilMaker.MakeToil(
                "OperateTokraObservationScope");
            observe.tickAction = delegate
            {
                Thing observationPoint
                    = job.GetTarget(ObservationPointIndex).Thing;
                Pawn actor = observe.actor;
                actor.rotationTracker.FaceTarget(observationPoint);

                if (!GameComponent_TokraOrganicOperationManager
                    .PerformObservationWork(observationPoint, actor))
                {
                    EndJobWith(JobCondition.Incompletable);
                    return;
                }

                if (GameComponent_TokraOrganicOperationManager
                    .IsObservationWorkComplete(observationPoint))
                {
                    ReadyForNextToil();
                }
            };
            observe.defaultCompleteMode = ToilCompleteMode.Never;
            observe.handlingFacing = true;
            observe.WithProgressBar(
                ObservationPointIndex,
                delegate
                {
                    return GameComponent_TokraOrganicOperationManager
                        .GetObservationWorkProgress(
                            job.GetTarget(ObservationPointIndex).Thing);
                });
            yield return observe;

            Toil pack = Toils_General.Wait(
                GameComponent_TokraOrganicOperationManager
                    .ObservationRecoveryWorkTicks,
                ObservationPointIndex);
            pack.WithProgressBarToilDelay(ObservationPointIndex);
            yield return pack;

            Toil recover = ToilMaker.MakeToil(
                "RecoverTokraObservationDevice");
            recover.initAction = delegate
            {
                Thing recoveredDevice;

                if (!GameComponent_TokraOrganicOperationManager
                    .TryRecoverObservationDevice(
                        job.GetTarget(ObservationPointIndex).Thing,
                        pawn,
                        out recoveredDevice))
                {
                    EndJobWith(JobCondition.Incompletable);
                    return;
                }

                job.SetTarget(DeviceIndex, recoveredDevice);

                if (!pawn.Reserve(
                        recoveredDevice,
                        job,
                        1,
                        1,
                        null,
                        false))
                {
                    EndJobWith(JobCondition.Incompletable);
                }
            };
            recover.defaultCompleteMode = ToilCompleteMode.Instant;
            yield return recover;

            yield return Toils_Goto.GotoThing(
                DeviceIndex,
                PathEndMode.ClosestTouch);
            yield return Toils_Haul.StartCarryThing(DeviceIndex);
            yield return Toils_Goto.GotoThing(
                CommunicatorIndex,
                PathEndMode.Touch);

            Toil transmit = ToilMaker.MakeToil(
                "TransmitTokraObservationData");
            transmit.tickAction = delegate
            {
                Thing communicator = job.GetTarget(CommunicatorIndex).Thing;

                if (!GameComponent_TokraOrganicOperationManager
                    .PerformObservationTransmissionWork(
                        communicator,
                        transmit.actor))
                {
                    EndJobWith(JobCondition.Incompletable);
                    return;
                }

                if (GameComponent_TokraOrganicOperationManager
                    .IsObservationTransmissionComplete(communicator))
                {
                    ReadyForNextToil();
                }
            };
            transmit.defaultCompleteMode = ToilCompleteMode.Never;
            transmit.WithProgressBar(
                CommunicatorIndex,
                delegate
                {
                    return GameComponent_TokraOrganicOperationManager
                        .GetObservationTransmissionProgress(
                            job.GetTarget(CommunicatorIndex).Thing);
                });
            yield return transmit;

            Toil finish = ToilMaker.MakeToil(
                "CompleteTokraObservationTransmission");
            finish.initAction = delegate
            {
                GameComponent_TokraOrganicOperationManager
                    .TryCompleteObservationTransmission(
                        job.GetTarget(CommunicatorIndex).Thing,
                        pawn);
            };
            finish.defaultCompleteMode = ToilCompleteMode.Instant;
            yield return finish;
        }
    }
}
