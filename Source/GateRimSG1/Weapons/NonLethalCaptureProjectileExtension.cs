using RimWorld;
using Verse;

namespace GateRimSG1.Weapons
{
    /// <summary>
    /// XML-driven balance profile shared by non-lethal capture projectiles.
    ///
    /// A successful projectile hit still performs a separate neutralization
    /// roll. Body size and armor can reduce that roll, allowing the tools to
    /// remain useful without guaranteeing every capture attempt.
    /// </summary>
    public class NonLethalCaptureProjectileExtension : DefModExtension
    {
        public HediffDef neutralizationHediff;
        public float baseNeutralizationChance = 0.5f;
        public float minimumNeutralizationChance = 0.05f;
        public float maximumNeutralizationChance = 0.95f;
        public float bodySizePenaltyPerPoint = 0.2f;
        public StatDef armorStat;
        public float armorResistanceFactor = 0.25f;
        public IntRange neutralizationTicks = new IntRange(300, 600);
        public bool humanlikeOnly = true;
        public bool requireFlesh = true;
    }
}
