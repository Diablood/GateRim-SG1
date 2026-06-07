using Verse;

namespace GateRimSG1.Goauld
{
    public class CompProperties_GoauldForcedImplantation : CompProperties
    {
        public float minimumTargetAgeYears = 13f;

        public CompProperties_GoauldForcedImplantation()
        {
            compClass = typeof(Comp_GoauldForcedImplantation);
        }
    }
}
