using RimWorld;
using Verse;

namespace GateRimSG1.Weapons
{
    /// <summary>
    /// XML-configurable properties for a non-reloadable weapon whose parent
    /// is destroyed after its final integrated charge is fired.
    /// </summary>
    public class CompProperties_ConsumableCharges : CompProperties
    {
        public int maxCharges = 1;
        public bool destroyOnEmpty = true;

        [MustTranslate]
        public string chargeNoun = "charge";

        public CompProperties_ConsumableCharges()
        {
            compClass = typeof(CompConsumableCharges);
        }

        public override void ResolveReferences(ThingDef parentDef)
        {
            base.ResolveReferences(parentDef);

            if (maxCharges < 1)
            {
                maxCharges = 1;
            }
        }
    }

    /// <summary>
    /// Persistent sealed-charge counter used by GateRim weapons. It does not
    /// expose any reload job, ammunition Def or developer reload command.
    /// </summary>
    public class CompConsumableCharges : ThingComp
    {
        private int remainingCharges = -1;

        public CompProperties_ConsumableCharges Props =>
            (CompProperties_ConsumableCharges)props;

        public int RemainingCharges => remainingCharges;

        public int MaxCharges => Props.maxCharges;

        public bool CanBeUsed => remainingCharges > 0;

        public override void PostPostMake()
        {
            base.PostPostMake();
            remainingCharges = MaxCharges;
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(
                ref remainingCharges,
                "remainingCharges",
                -1);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                if (remainingCharges < 0)
                {
                    remainingCharges = MaxCharges;
                }
                else if (remainingCharges > MaxCharges)
                {
                    remainingCharges = MaxCharges;
                }
            }
        }

        public override string CompInspectStringExtra()
        {
            return ChargeStatusText();
        }

        public override string CompTipStringExtra()
        {
            return ChargeStatusText();
        }

        private string ChargeStatusText()
        {
            int displayedCharges = remainingCharges < 0
                ? MaxCharges
                : remainingCharges;

            return "ChargesRemaining".Translate(
                Props.chargeNoun.Named("CHARGENOUN"))
                + ": "
                + displayedCharges
                + " / "
                + MaxCharges;
        }

        public void UsedOnce()
        {
            if (remainingCharges > 0)
            {
                remainingCharges--;
            }

            if (
                Props.destroyOnEmpty
                && remainingCharges <= 0
                && !parent.Destroyed)
            {
                parent.Destroy(DestroyMode.Vanish);
            }
        }
    }
}
