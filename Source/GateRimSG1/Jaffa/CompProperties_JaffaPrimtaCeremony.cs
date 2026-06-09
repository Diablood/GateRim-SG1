using Verse;

namespace GateRimSG1.Jaffa
{
    /// <summary>
    /// Configuration for the first Core + Biotech formal Prim'ta ceremony.
    /// </summary>
    public class CompProperties_JaffaPrimtaCeremony : CompProperties
    {
        public float ceremonyRange = 6f;
        public int ceremonyDurationTicks = 600;

        public CompProperties_JaffaPrimtaCeremony()
        {
            compClass = typeof(Comp_JaffaPrimtaCeremony);
        }
    }
}
