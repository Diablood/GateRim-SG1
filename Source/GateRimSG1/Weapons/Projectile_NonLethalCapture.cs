using RimWorld;
using Verse;

namespace GateRimSG1.Weapons
{
    /// <summary>
    /// Projectile that performs a configurable neutralization roll and then
    /// resolves its deliberately light blunt impact.
    /// </summary>
    public class Projectile_NonLethalCapture : Bullet
    {
        protected override void Impact(
            Thing hitThing,
            bool blockedByShield = false)
        {
            Pawn pawn = hitThing as Pawn;
            NonLethalCaptureProjectileExtension profile = def
                .GetModExtension<NonLethalCaptureProjectileExtension>();

            if (!blockedByShield && pawn != null)
            {
                NonLethalCaptureUtility.TryNeutralize(
                    pawn,
                    profile);
            }

            base.Impact(hitThing, blockedByShield);
        }
    }
}
