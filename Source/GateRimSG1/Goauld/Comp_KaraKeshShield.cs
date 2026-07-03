using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public class CompProperties_KaraKeshShield : CompProperties_Shield
    {
        public int rechargeDelayAfterAbsorbedHitTicks = 300;

        public CompProperties_KaraKeshShield()
        {
            compClass = typeof(Comp_KaraKeshShield);
        }
    }

    public class Comp_KaraKeshShield : CompShield
    {
        private const int NoAbsorbedHitTick = -99999;

        private int lastAbsorbedHitTick = NoAbsorbedHitTick;

        private CompProperties_KaraKeshShield KaraKeshProps
            => props as CompProperties_KaraKeshShield;

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(
                ref lastAbsorbedHitTick,
                "karaKeshLastAbsorbedHitTick",
                NoAbsorbedHitTick);
        }

        public override void PostPreApplyDamage(
            ref DamageInfo dinfo,
            out bool absorbed)
        {
            base.PostPreApplyDamage(ref dinfo, out absorbed);

            if (absorbed && Find.TickManager != null)
            {
                lastAbsorbedHitTick = Find.TickManager.TicksGame;
            }
        }

        public override void CompTick()
        {
            int currentTick = Find.TickManager?.TicksGame ?? 0;
            int rechargeDelay = KaraKeshProps
                ?.rechargeDelayAfterAbsorbedHitTicks ?? 0;

            if (ShieldState == ShieldState.Active
                && currentTick - lastAbsorbedHitTick < rechargeDelay)
            {
                return;
            }

            base.CompTick();
        }
    }
}
