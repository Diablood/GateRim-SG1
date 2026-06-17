using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace GateRimSG1.Goauld
{
    public class JobDriver_TokraSabotageRelayDevice : JobDriver
    {
        private const TargetIndex RelayIndex = TargetIndex.A;

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return pawn.Reserve(
                job.GetTarget(RelayIndex),
                job,
                1,
                -1,
                null,
                errorOnFailed);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDespawnedNullOrForbidden(RelayIndex);
            this.FailOn(() => GetRelayComp() == null || GetRelayComp().Sabotaged);

            yield return Toils_Goto.GotoThing(RelayIndex, PathEndMode.Touch);

            Toil start = ToilMaker.MakeToil("StartTokraRelaySabotage");
            start.initAction = delegate
            {
                GetRelayComp()?.NotifySabotageStarted();
            };
            start.defaultCompleteMode = ToilCompleteMode.Instant;
            yield return start;

            Toil sabotage = ToilMaker.MakeToil("WorkOnTokraRelaySabotage");
            sabotage.tickAction = delegate
            {
                Comp_TokraRelaySabotageDevice relayComp = GetRelayComp();

                if (relayComp != null
                    && relayComp.PerformSabotageWork(sabotage.actor))
                {
                    ReadyForNextToil();
                }
            };
            sabotage.defaultCompleteMode = ToilCompleteMode.Never;
            sabotage.WithProgressBar(
                RelayIndex,
                delegate
                {
                    Comp_TokraRelaySabotageDevice relayComp = GetRelayComp();
                    return relayComp?.SabotageProgress ?? 0f;
                });
            yield return sabotage;

            Toil finish = ToilMaker.MakeToil("CompleteTokraRelaySabotage");
            finish.initAction = delegate
            {
                GetRelayComp()?.CompleteSabotage(pawn);
            };
            finish.defaultCompleteMode = ToilCompleteMode.Instant;
            yield return finish;
        }

        private Comp_TokraRelaySabotageDevice GetRelayComp()
        {
            Thing thing = job.GetTarget(RelayIndex).Thing;
            return thing?.TryGetComp<Comp_TokraRelaySabotageDevice>();
        }
    }
}
