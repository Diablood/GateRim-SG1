using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace GateRimSG1.Goauld
{
    public class JobDriver_TokraSafehouseContactDialogue : JobDriver
    {
        private const TargetIndex ContactIndex = TargetIndex.A;

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return true;
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOn(() => !TokraSafehouseContactDialogueUtility
                .IsValidSafehouseContact(GetContact()));

            yield return Toils_Goto.GotoThing(ContactIndex, PathEndMode.Touch);

            Toil exchange = new Toil
            {
                initAction = delegate
                {
                    TokraSafehouseContactDialogueUtility.TryAcknowledge(
                        GetContact(),
                        pawn);
                },
                defaultCompleteMode = ToilCompleteMode.Instant
            };

            yield return exchange;
        }

        private Pawn GetContact()
        {
            return job.GetTarget(ContactIndex).Thing as Pawn;
        }
    }
}
