using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace GateRimSG1.Goauld
{
    public class JobDriver_TokraAnalyzeMissionIntelPacket : JobDriver
    {
        private const int AnalyzeTicks = 360;
        private const TargetIndex PacketIndex = TargetIndex.A;

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return pawn.Reserve(job.GetTarget(PacketIndex), job, 1, -1, null, errorOnFailed);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDespawnedNullOrForbidden(PacketIndex);
            this.FailOn(() => GetPacketComp() == null);

            yield return Toils_Goto.GotoThing(PacketIndex, PathEndMode.Touch);

            Toil analyze = Toils_General.Wait(AnalyzeTicks);
            analyze.WithProgressBarToilDelay(PacketIndex);
            analyze.defaultCompleteMode = ToilCompleteMode.Delay;
            yield return analyze;

            Toil finish = ToilMaker.MakeToil("AnalyzeTokraMissionIntelPacket");
            finish.initAction = delegate
            {
                GetPacketComp()?.TryAnalyzeIntelligence(pawn);
            };
            finish.defaultCompleteMode = ToilCompleteMode.Instant;
            yield return finish;
        }

        private Comp_TokraMissionIntelPacket GetPacketComp()
        {
            Thing thing = job.GetTarget(PacketIndex).Thing;
            return thing?.TryGetComp<Comp_TokraMissionIntelPacket>();
        }
    }
}
