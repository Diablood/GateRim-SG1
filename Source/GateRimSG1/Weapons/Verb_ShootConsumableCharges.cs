using Verse;

namespace GateRimSG1.Weapons
{
    /// <summary>
    /// Standard projectile verb that consumes one sealed charge after every
    /// projectile launch, including shots that subsequently miss or resist.
    /// </summary>
    public class Verb_Shoot_ConsumableCharges : Verb_Shoot
    {
        private CompConsumableCharges ChargeComp =>
            EquipmentSource?.TryGetComp<CompConsumableCharges>();

        public override bool Available()
        {
            CompConsumableCharges charges = ChargeComp;

            return base.Available()
                && (charges == null || charges.CanBeUsed);
        }

        protected override bool TryCastShot()
        {
            CompConsumableCharges charges = ChargeComp;

            if (charges != null && !charges.CanBeUsed)
            {
                return false;
            }

            bool launched = base.TryCastShot();

            if (launched)
            {
                charges?.UsedOnce();
            }

            return launched;
        }
    }
}
