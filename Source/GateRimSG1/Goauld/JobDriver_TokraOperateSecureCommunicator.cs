using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Operates the trusted Tok'ra secure communicator through a selected
    /// player pawn. This keeps contact and support requests grounded as a
    /// colonist action instead of an instant building-only remote command.
    /// </summary>
    public class JobDriver_TokraOperateSecureCommunicator : JobDriver
    {
        private const TargetIndex CommunicatorIndex = TargetIndex.A;
        private const int OperationTicks = 180;
        private const string RequestDiversionJobDefName = "SG1_RequestTokraDefensiveDiversion";
        private const string RequestMedicalSupportJobDefName = "SG1_RequestTokraMedicalSupport";
        private const string RequestMedicalCacheJobDefName = "SG1_RequestTokraEmergencyMedicalCache";
        private const string RequestThreatAssessmentJobDefName = "SG1_RequestTokraThreatAssessment";

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            LocalTargetInfo communicator = job.GetTarget(CommunicatorIndex);

            return communicator.HasThing
                && pawn.Reserve(
                    communicator,
                    job,
                    1,
                    -1,
                    null,
                    errorOnFailed);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDespawnedNullOrForbidden(CommunicatorIndex);
            this.FailOn(() => GetCommunicatorComp() == null);
            this.FailOn(() => IsOperationCurrentlyInvalid());

            yield return Toils_Goto.GotoThing(
                CommunicatorIndex,
                PathEndMode.Touch);

            Toil operate = Toils_General.Wait(
                OperationTicks,
                CommunicatorIndex);
            operate.WithProgressBarToilDelay(CommunicatorIndex);
            yield return operate;

            Toil finish = new Toil
            {
                initAction = delegate
                {
                    Comp_TokraSecureCommunicator comp = GetCommunicatorComp();

                    if (comp == null)
                    {
                        return;
                    }

                    if (IsDiversionRequestJob())
                    {
                        comp.TryRequestDefensiveDiversion(pawn);
                    }
                    else if (IsMedicalSupportRequestJob())
                    {
                        comp.TryRequestMedicalSupport(pawn);
                    }
                    else if (IsThreatAssessmentRequestJob())
                    {
                        comp.TryRequestTacticalThreatAssessment(pawn);
                    }
                    else if (IsMedicalCacheRequestJob())
                    {
                        comp.TryRequestEmergencyMedicalCache(pawn);
                    }
                    else
                    {
                        comp.TryOpenSecureChannel(pawn);
                    }
                },
                defaultCompleteMode = ToilCompleteMode.Instant
            };

            yield return finish;
        }

        private bool IsOperationCurrentlyInvalid()
        {
            Comp_TokraSecureCommunicator comp = GetCommunicatorComp();

            if (comp == null)
            {
                return true;
            }

            string disabledReason;

            if (IsDiversionRequestJob())
            {
                disabledReason = comp.GetDiversionDisabledReason();
            }
            else if (IsMedicalSupportRequestJob())
            {
                disabledReason = comp.GetMedicalSupportDisabledReason();
            }
            else if (IsThreatAssessmentRequestJob())
            {
                disabledReason = comp.GetThreatAssessmentDisabledReason();
            }
            else if (IsMedicalCacheRequestJob())
            {
                disabledReason = comp.GetMedicalCacheDisabledReason();
            }
            else
            {
                disabledReason = comp.GetChannelDisabledReason();
            }

            return !string.IsNullOrEmpty(disabledReason);
        }

        private bool IsDiversionRequestJob()
        {
            return job?.def?.defName == RequestDiversionJobDefName;
        }

        private bool IsMedicalSupportRequestJob()
        {
            return job?.def?.defName == RequestMedicalSupportJobDefName;
        }

        private bool IsMedicalCacheRequestJob()
        {
            return job?.def?.defName == RequestMedicalCacheJobDefName;
        }

        private bool IsThreatAssessmentRequestJob()
        {
            return job?.def?.defName == RequestThreatAssessmentJobDefName;
        }

        private Comp_TokraSecureCommunicator GetCommunicatorComp()
        {
            ThingWithComps communicator = job.GetTarget(CommunicatorIndex).Thing
                as ThingWithComps;

            return communicator?.GetComp<Comp_TokraSecureCommunicator>();
        }
    }
}
