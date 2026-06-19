using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace GateRimSG1.Goauld
{
    public class JobDriver_TokraAnalyzeOrganicIntelligence : JobDriver
    {
        private const TargetIndex CommunicatorIndex = TargetIndex.A;
        private const TargetIndex IntelligenceModuleIndex = TargetIndex.B;

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            Thing communicator = job.GetTarget(CommunicatorIndex).Thing;
            Thing intelligenceModule = job.GetTarget(
                IntelligenceModuleIndex).Thing;

            if (communicator == null || intelligenceModule == null)
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

            if (pawn.carryTracker?.CarriedThing == intelligenceModule)
            {
                return true;
            }

            return pawn.Reserve(
                intelligenceModule,
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
                .CanContinueIntelligenceAnalysis(
                    job.GetTarget(CommunicatorIndex).Thing,
                    pawn));

            if (pawn.carryTracker?.CarriedThing
                != job.GetTarget(IntelligenceModuleIndex).Thing)
            {
                yield return Toils_Goto.GotoThing(
                    IntelligenceModuleIndex,
                    PathEndMode.ClosestTouch);

                yield return Toils_Haul.StartCarryThing(
                    IntelligenceModuleIndex);
            }

            yield return Toils_Goto.GotoThing(
                CommunicatorIndex,
                PathEndMode.Touch);

            Toil analyze = ToilMaker.MakeToil(
                "AnalyzeTokraOrganicIntelligence");
            analyze.tickAction = delegate
            {
                Thing communicator = job.GetTarget(CommunicatorIndex).Thing;

                if (!GameComponent_TokraOrganicOperationManager
                    .PerformIntelligenceAnalysisWork(
                        communicator,
                        analyze.actor))
                {
                    EndJobWith(JobCondition.Incompletable);
                    return;
                }

                if (GameComponent_TokraOrganicOperationManager
                    .IsIntelligenceAnalysisComplete(communicator))
                {
                    ReadyForNextToil();
                }
            };
            analyze.defaultCompleteMode = ToilCompleteMode.Never;
            analyze.WithProgressBar(
                CommunicatorIndex,
                delegate
                {
                    return GameComponent_TokraOrganicOperationManager
                        .GetIntelligenceAnalysisProgress(
                            job.GetTarget(CommunicatorIndex).Thing);
                });
            yield return analyze;

            Toil finish = ToilMaker.MakeToil(
                "CompleteTokraOrganicIntelligenceAnalysis");
            finish.initAction = delegate
            {
                GameComponent_TokraOrganicOperationManager
                    .TryCompleteIntelligenceAnalysis(
                        job.GetTarget(CommunicatorIndex).Thing,
                        pawn);
            };
            finish.defaultCompleteMode = ToilCompleteMode.Instant;
            yield return finish;
        }
    }
}
