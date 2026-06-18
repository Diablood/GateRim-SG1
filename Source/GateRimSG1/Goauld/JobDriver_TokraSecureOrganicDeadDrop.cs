using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace GateRimSG1.Goauld
{
    public class JobDriver_TokraSecureOrganicDeadDrop : JobDriver
    {
        private const TargetIndex DeadDropIndex = TargetIndex.A;
        private const int SecureTicks = 600;

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            LocalTargetInfo deadDrop = job.GetTarget(DeadDropIndex);

            return deadDrop.HasThing
                && pawn.Reserve(
                    deadDrop,
                    job,
                    1,
                    -1,
                    null,
                    errorOnFailed);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDespawnedNullOrForbidden(DeadDropIndex);
            this.FailOn(() =>
            {
                Comp_TokraOrganicDeadDrop comp = GetDeadDropComp();
                return comp == null || !comp.IsOperationActive();
            });

            yield return Toils_Goto.GotoThing(
                DeadDropIndex,
                PathEndMode.Touch);

            Toil secure = Toils_General.Wait(
                SecureTicks,
                DeadDropIndex);
            secure.WithProgressBarToilDelay(DeadDropIndex);
            yield return secure;

            Toil finish = new Toil
            {
                initAction = delegate
                {
                    GetDeadDropComp()?.TrySecure(pawn);
                },
                defaultCompleteMode = ToilCompleteMode.Instant
            };

            yield return finish;
        }

        private Comp_TokraOrganicDeadDrop GetDeadDropComp()
        {
            ThingWithComps deadDrop = job.GetTarget(DeadDropIndex).Thing
                as ThingWithComps;

            return deadDrop?.GetComp<Comp_TokraOrganicDeadDrop>();
        }
    }
}
