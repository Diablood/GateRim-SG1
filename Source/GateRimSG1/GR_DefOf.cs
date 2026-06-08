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
        public static JobDef SG1_GoauldAutonomousImplant;
        public static ThingDef SG1_GoauldRitualBasin;
        public static GeneDef SG1_JaffaLineage;
        public static GeneDef SG1_JaffaPouchPotential;
        public static GeneDef SG1_JaffaSymbioteCompatibility;
        public static HediffDef SG1_JaffaPrimta;
        public static HediffDef SG1_JaffaPrimtaDependency;
        public static ThingDef SG1_PrimtaLarva;
        public static ThoughtDef SG1_AwaitingPrimta;
        public static ThoughtDef SG1_ReceivedPrimta;

        static GR_DefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(GR_DefOf));
        }
    }
}
