using RimWorld;
using Verse;

namespace GateRimSG1.Weapons
{
    /// <summary>
    /// First-shot Zat'nik'tel prototype.
    ///
    /// Vanilla Stun remains the primary projectile effect. A deliberately
    /// small EMP follow-up is added only for non-organic pawns and buildings,
    /// keeping the sidearm useful against vanilla mechanical targets without
    /// turning it into a structural weapon.
    ///
    /// Lethal second-shot and disintegrating third-shot behavior are
    /// intentionally deferred to later milestones.
    /// </summary>
    public class Projectile_ZatnikTelDisruption : Bullet
    {
        private const float NonOrganicPawnEmpDamage = 10f;
        private const float BuildingEmpDamage = 8f;

        protected override void Impact(
            Thing hitThing,
            bool blockedByShield = false)
        {
            float impactAngle = ExactRotation.eulerAngles.y;
            Thing impactLauncher = launcher;
            ThingDef impactEquipmentDef = equipmentDef;
            Thing impactIntendedTarget = intendedTarget.Thing;
            QualityCategory impactEquipmentQuality = equipmentQuality;

            base.Impact(hitThing, blockedByShield);

            if (blockedByShield
                || hitThing == null
                || hitThing.Destroyed)
            {
                return;
            }

            float empDamage = EmpDamageFor(hitThing);
            if (empDamage <= 0f)
            {
                return;
            }

            bool instigatorGuilty = !(impactLauncher is Pawn pawn)
                || !pawn.Drafted;

            DamageInfo empDisruption = new DamageInfo(
                DamageDefOf.EMP,
                empDamage,
                0f,
                impactAngle,
                impactLauncher,
                null,
                impactEquipmentDef,
                DamageInfo.SourceCategory.ThingOrUnknown,
                impactIntendedTarget,
                instigatorGuilty);

            empDisruption.SetWeaponQuality(impactEquipmentQuality);
            hitThing.TakeDamage(empDisruption);
        }

        private static float EmpDamageFor(Thing hitThing)
        {
            if (hitThing.def.category == ThingCategory.Building)
            {
                return BuildingEmpDamage;
            }

            if (hitThing is Pawn pawn
                && pawn.RaceProps != null
                && !pawn.RaceProps.IsFlesh)
            {
                return NonOrganicPawnEmpDamage;
            }

            return 0f;
        }
    }
}
