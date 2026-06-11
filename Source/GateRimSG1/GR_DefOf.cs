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
        public static PawnKindDef SG1_TokraSymbiote;
        public static PawnKindDef SG1_TokraVoluntaryHost;
        public static PawnKindDef SG1_GoauldJaffaWarrior;
        public static PawnKindDef SG1_GoauldJaffaGuard;
        public static FactionDef SG1_Tokra;
        public static FactionDef SG1_GoauldSystemLordPrototype;
        public static IncidentDef SG1_TokraPeacefulVisitors;
        public static IncidentDef SG1_GoauldJaffaControlledRaid;
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
        public static ThingDef SG1_TretoninDose;
        public static HediffDef SG1_TretoninSubstitution;
        public static ThingDef SG1_JaffaDeployedHelmet;
        public static ThingDef SG1_JaffaRetractedHelmet;

        static GR_DefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(GR_DefOf));
        }
    }
}
