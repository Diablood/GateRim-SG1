using GateRimSG1.Goauld;
using GateRimSG1.Jaffa;
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
        public static PawnKindDef SG1_FreeJaffaWarrior;
        public static PawnKindDef SG1_FreeJaffaGuard;
        public static FactionDef SG1_Tokra;
        public static FactionDef SG1_GoauldSystemLordPrototype;
        public static FactionDef SG1_FreeJaffa;
        public static IncidentDef SG1_TokraPeacefulVisitors;
        public static IncidentDef SG1_GoauldJaffaNaturalRaid;
        public static IncidentDef SG1_GoauldJaffaControlledRaid;
        public static IncidentDef SG1_GoauldJaffaControlledAbductionRaid;
        public static IncidentDef SG1_GoauldJaffaControlledDestructionRaid;
        public static RaidStrategyDef SG1_GoauldJaffaAbductionAssault;
        public static RaidStrategyDef SG1_GoauldJaffaDestructionAssault;
        public static JobDef SG1_GoauldAutonomousImplant;
        public static ThingDef SG1_GoauldRitualBasin;

        // Legacy technical genes retained only for save migration.
        public static GeneDef SG1_JaffaForeheadMark_Generic;
        public static GeneDef SG1_JaffaForeheadMark_GenericSilver;
        public static GeneDef SG1_JaffaForeheadMark_GenericGold;

        // Intrinsic forehead-mark data Defs.
        public static JaffaForeheadMarkDef SG1_JaffaForeheadMark_GenericIntrinsic;
        public static JaffaForeheadMarkDef SG1_JaffaForeheadMark_GenericSilverIntrinsic;
        public static JaffaForeheadMarkDef SG1_JaffaForeheadMark_GenericGoldIntrinsic;

        public static GoauldSystemLordDomainDef SG1_GoauldSystemLordDomainPrototype;
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
