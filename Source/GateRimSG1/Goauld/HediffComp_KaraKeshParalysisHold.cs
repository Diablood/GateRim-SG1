using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public class HediffCompProperties_KaraKeshParalysisHold
        : HediffCompProperties
    {
        public int validationIntervalTicks = 15;

        public HediffCompProperties_KaraKeshParalysisHold()
        {
            compClass = typeof(HediffComp_KaraKeshParalysisHold);
        }
    }

    public class HediffComp_KaraKeshParalysisHold : HediffComp
    {
        private Apparel sourceKaraKesh;
        private bool shouldRemove;

        private HediffCompProperties_KaraKeshParalysisHold Props
            => props as HediffCompProperties_KaraKeshParalysisHold;

        public override bool CompShouldRemove => shouldRemove;

        public void InitializeSource(Apparel source)
        {
            sourceKaraKesh = source;
            shouldRemove = false;
        }

        public bool IsMaintainedBy(Apparel source)
        {
            return source != null && sourceKaraKesh == source;
        }

        public override void CompExposeData()
        {
            base.CompExposeData();
            Scribe_References.Look(
                ref sourceKaraKesh,
                "karaKeshParalysisSource");
        }

        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);

            if (shouldRemove)
            {
                return;
            }

            int interval = Props?.validationIntervalTicks ?? 15;

            if (Pawn == null
                || Pawn.IsHashIntervalTick(System.Math.Max(1, interval)))
            {
                Comp_KaraKeshShield sourceComp = sourceKaraKesh
                    ?.TryGetComp<Comp_KaraKeshShield>();
                shouldRemove = sourceComp == null
                    || !sourceComp.CanSustainParalysisHold(Pawn);
            }
        }

        public override void CompPostPostRemoved()
        {
            base.CompPostPostRemoved();
            sourceKaraKesh
                ?.TryGetComp<Comp_KaraKeshShield>()
                ?.NotifyParalysisHoldEnded(Pawn);
        }
    }
}
