using Verse;

namespace GateRimSG1.Goauld
{
    public class CompProperties_GoauldForcedImplantation : CompProperties
    {
        public float minimumTargetAgeYears = 13f;
        public GoauldSymbioteOrigin symbioteOrigin = GoauldSymbioteOrigin.Goauld;
        public bool allowForcedImplantation = true;
        public bool allowRitualImplantation = true;
        public bool allowAutonomousHuntToggle = true;
        public bool allowVoluntaryImplantation = false;
        public float voluntaryImplantationRange = 12f;
        public bool autonomousHuntingEnabled = true;
        public int autonomousScanIntervalTicks = 60;
        public float autonomousSearchRadius = 35f;
        public int autonomousCooldownAfterExtractionTicks = 2500;
        public float ritualImplantationRange = 12f;
        public int ritualCeremonyDurationTicks = 600;
        public float ritualBasinRange = 6f;

        public CompProperties_GoauldForcedImplantation()
        {
            compClass = typeof(Comp_GoauldForcedImplantation);
        }
    }
}
