using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Moves a free symbiote into contact with a compatible target, then asks
    /// the ThingComp to perform the same identity-safe implantation used by the
    /// manual prototype.
    /// </summary>
    public class JobDriver_GoauldAutonomousImplant : JobDriver
    {
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return true;
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            yield return Toils_Goto.GotoThing(
                TargetIndex.A,
                PathEndMode.Touch);

            Toil implant = ToilMaker.MakeToil(
                "GateRimSG1AutonomousImplant");

            implant.initAction = delegate
            {
                Pawn target = TargetPawnA;

                Comp_GoauldForcedImplantation comp
                    = pawn.GetComp<Comp_GoauldForcedImplantation>();

                if (comp == null
                    || target == null
                    || !comp.TryImplantHost(target, autonomous: true))
                {
                    EndJobWith(JobCondition.Incompletable);
                }
            };

            implant.defaultCompleteMode = ToilCompleteMode.Instant;

            yield return implant;
        }
    }
}
