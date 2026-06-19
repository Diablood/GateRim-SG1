using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace GateRimSG1.Goauld
{
    public class JobDriver_TokraMedicalSupplyDialogue : JobDriver
    {
        private const TargetIndex LiaisonIndex = TargetIndex.A;
        private const int ConversationTicks = 180;

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return pawn.Reserve(
                job.GetTarget(LiaisonIndex),
                job,
                1,
                -1,
                null,
                errorOnFailed);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOn(() => !GameComponent_TokraOrganicOperationManager
                .IsActiveMedicalSupplyLiaison(GetLiaison()));

            yield return Toils_Goto.GotoThing(
                LiaisonIndex,
                PathEndMode.Touch);

            Toil converse = Toils_General.Wait(
                ConversationTicks,
                LiaisonIndex);
            converse.WithProgressBarToilDelay(LiaisonIndex);
            yield return converse;

            Toil openDialogue = new Toil
            {
                initAction = delegate
                {
                    GameComponent_TokraOrganicOperationManager
                        .TryOpenMedicalSupplyDialogue(
                            GetLiaison(),
                            pawn);
                },
                defaultCompleteMode = ToilCompleteMode.Instant
            };

            yield return openDialogue;
        }

        private Pawn GetLiaison()
        {
            return job.GetTarget(LiaisonIndex).Thing as Pawn;
        }
    }
}
