using Verse;

namespace GateRimSG1.Goauld
{
    public class CompProperties_GoauldForcedImplantation : CompProperties
    {
        public float minimumTargetAgeYears = 13f;
        public bool autonomousHuntingEnabled = true;
        public int autonomousScanIntervalTicks = 60;
        public float autonomousSearchRadius = 35f;
        public int autonomousCooldownAfterExtractionTicks = 2500;

        public CompProperties_GoauldForcedImplantation()
        {
            compClass = typeof(Comp_GoauldForcedImplantation);
        }
    }
}
