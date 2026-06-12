using RimWorld;
using Verse;

namespace GateRimSG1.Weapons
{
    /// <summary>
    /// Preserves the Ma'Tok's primary thermal plasma injury while adding a
    /// reduced structural impact against non-organic pawns and buildings.
    ///
    /// Organic pawns receive only the existing burn damage. Mechanical pawns
    /// and structures receive the burn impact plus a deliberately modest
    /// blunt follow-up so the weapon remains useful without becoming an
    /// anti-mech specialist.
    /// </summary>
    public class Projectile_MatokPlasmaImpact : Bullet
    {
        private const float NonOrganicPawnImpactDamage = 8f;
        private const float BuildingImpactDamage = 12f;
        private const float StructuralImpactArmorPenetration = 0.18f;

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

            float structuralDamage = StructuralDamageFor(hitThing);
            if (structuralDamage <= 0f)
            {
                return;
            }

            bool instigatorGuilty = !(impactLauncher is Pawn pawn)
                || !pawn.Drafted;

            DamageInfo structuralImpact = new DamageInfo(
                DamageDefOf.Blunt,
                structuralDamage,
                StructuralImpactArmorPenetration,
                impactAngle,
                impactLauncher,
                null,
                impactEquipmentDef,
                DamageInfo.SourceCategory.ThingOrUnknown,
                impactIntendedTarget,
                instigatorGuilty);

            structuralImpact.SetWeaponQuality(impactEquipmentQuality);
            hitThing.TakeDamage(structuralImpact);
        }

        private static float StructuralDamageFor(Thing hitThing)
        {
            if (hitThing.def.category == ThingCategory.Building)
            {
                return BuildingImpactDamage;
            }

            if (hitThing is Pawn pawn
                && pawn.RaceProps != null
                && !pawn.RaceProps.IsFlesh)
            {
                return NonOrganicPawnImpactDamage;
            }

            return 0f;
        }
    }
}
