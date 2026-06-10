using Verse;

namespace GateRimSG1.Jaffa
{
    public class CompProperties_PrimtaPreservationBasin : CompProperties
    {
        public float idealTemperature = 4f;

        public CompProperties_PrimtaPreservationBasin()
        {
            compClass = typeof(Comp_PrimtaPreservationBasin);
        }
    }
}
