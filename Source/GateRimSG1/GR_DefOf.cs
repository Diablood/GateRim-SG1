using RimWorld;
using Verse;

namespace GateRimSG1
{
    [DefOf]
    public static class GR_DefOf
    {
        public static HediffDef SG1_GoauldRecentImplantation;
        public static HediffDef SG1_GoauldHostSymbiote;
        public static PawnKindDef SG1_GoauldSymbiote;

        static GR_DefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(GR_DefOf));
        }
    }
}
