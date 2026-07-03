using System.Collections.Generic;
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

        private bool WearerCanActivate
            => NaquadahTraceUtility.CanActivateNaquadahTechnology(PawnOwner);

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(
                ref lastAbsorbedHitTick,
                "karaKeshLastAbsorbedHitTick",
                NoAbsorbedHitTick);
        }

        public override IEnumerable<Gizmo> CompGetWornGizmosExtra()
        {
            if (!WearerCanActivate)
            {
                yield break;
            }

            foreach (Gizmo gizmo in base.CompGetWornGizmosExtra())
            {
                yield return gizmo;
            }
        }

        public override string CompInspectStringExtra()
        {
            string baseText = base.CompInspectStringExtra();

            if (PawnOwner == null || WearerCanActivate)
            {
                return baseText;
            }

            string inactiveText = "GR_KaraKesh_InactiveWithoutNaquadah"
                .Translate(PawnOwner.LabelShortCap)
                .ToString();

            return baseText.NullOrEmpty()
                ? inactiveText
                : baseText + "\n" + inactiveText;
        }

        public override void PostPreApplyDamage(
            ref DamageInfo dinfo,
            out bool absorbed)
        {
            if (!WearerCanActivate)
            {
                absorbed = false;
                return;
            }

            base.PostPreApplyDamage(ref dinfo, out absorbed);

            if (absorbed && Find.TickManager != null)
            {
                lastAbsorbedHitTick = Find.TickManager.TicksGame;
            }
        }

        public override void CompTick()
        {
            if (PawnOwner != null && !WearerCanActivate)
            {
                return;
            }

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

        public override void CompDrawWornExtras()
        {
            if (!WearerCanActivate)
            {
                return;
            }

            base.CompDrawWornExtras();
        }

        public override bool CompAllowVerbCast(Verb verb)
        {
            return !WearerCanActivate || base.CompAllowVerbCast(verb);
        }
    }
}
