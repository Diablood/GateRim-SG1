using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace GateRimSG1.Goauld
{
    public class JobDriver_TokraTransmitObservationData : JobDriver
    {
        private const TargetIndex CommunicatorIndex = TargetIndex.A;
        private const TargetIndex DeviceIndex = TargetIndex.B;

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            Thing communicator = job.GetTarget(CommunicatorIndex).Thing;
            Thing device = job.GetTarget(DeviceIndex).Thing;

            if (communicator == null || device == null)
            {
                return false;
            }

            if (!pawn.Reserve(
                    communicator,
                    job,
                    1,
                    -1,
                    null,
                    errorOnFailed))
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
            this.FailOnDespawnedNullOrForbidden(CommunicatorIndex);
            this.FailOn(() => !GameComponent_TokraOrganicOperationManager
                .CanContinueObservationTransmission(
                    job.GetTarget(CommunicatorIndex).Thing,
                    pawn));

            if (TokraObservationUtility.IsObservationPoint(
                    job.GetTarget(DeviceIndex).Thing))
            {
                yield return Toils_Goto.GotoThing(
                    DeviceIndex,
                    PathEndMode.Touch);

                Toil recover = Toils_General.Wait(
                    GameComponent_TokraOrganicOperationManager
                        .ObservationRecoveryWorkTicks,
                    DeviceIndex);
                recover.WithProgressBarToilDelay(DeviceIndex);
                recover.WithEffect(
                    EffecterDefOf.ConstructMetal,
                    DeviceIndex);
                yield return recover;

                Toil pack = ToilMaker.MakeToil(
                    "RecoverTokraObservationDevice");
                pack.initAction = delegate
                {
                    Thing recoveredDevice;

                    if (!GameComponent_TokraOrganicOperationManager
                        .TryRecoverObservationDevice(
                            job.GetTarget(DeviceIndex).Thing,
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
                pack.defaultCompleteMode = ToilCompleteMode.Instant;
                yield return pack;
            }

            if (pawn.carryTracker?.CarriedThing
                != job.GetTarget(DeviceIndex).Thing)
            {
                yield return Toils_Goto.GotoThing(
                    DeviceIndex,
                    PathEndMode.ClosestTouch);

                yield return Toils_Haul.StartCarryThing(DeviceIndex);
            }

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
