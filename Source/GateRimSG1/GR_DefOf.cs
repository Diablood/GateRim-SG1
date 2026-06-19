using GateRimSG1.Goauld;
using GateRimSG1.Jaffa;
using RimWorld;
using RimWorld.Planet;
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
        public static PawnKindDef SG1_GoauldSettlementJaffaWarrior;
        public static PawnKindDef SG1_GoauldSettlementJaffaGuard;
        public static PawnKindDef SG1_FreeJaffaWarrior;
        public static PawnKindDef SG1_FreeJaffaGuard;
        public static PawnKindDef SG1_GoauldHostCaste;
        public static PawnKindDef SG1_GoauldSystemLordHost;
        public static PawnKindDef SG1_GoauldQueen;
        public static FactionDef SG1_Tokra;
        public static FactionDef SG1_GoauldSystemLordPrototype;
        public static FactionDef SG1_FreeJaffa;
        public static FactionDef SG1_PlayerSGCExpedition;
        public static IncidentDef SG1_TokraPeacefulVisitors;
        public static IncidentDef SG1_TokraHiddenCellCache;
        public static IncidentDef SG1_TokraSafehouseSignal;
        public static IncidentDef SG1_TokraSafehouseLeadCache;
        public static IncidentDef SG1_TokraHiddenSafehouseWorldMarker;
        public static IncidentDef SG1_TokraHiddenSafehouseSiteIncident;
        public static WorldObjectDef SG1_TokraHiddenSafehouseMarker;
        public static WorldObjectDef SG1_TokraHiddenSafehouseSite;
        public static SitePartDef SG1_TokraHiddenSafehouseSitePart;
        public static IncidentDef SG1_FreeJaffaPeacefulVisitors;
        public static IncidentDef SG1_GoauldJaffaNaturalRaid;
        public static IncidentDef SG1_GoauldQueenArrival;
        public static IncidentDef SG1_GoauldJaffaControlledRaid;

        public static IncidentDef SG1_GoauldJaffaSignalPatrol;
        public static IncidentDef SG1_GoauldJaffaControlledAbductionRaid;
        public static IncidentDef SG1_GoauldJaffaControlledDestructionRaid;
        public static RaidStrategyDef SG1_GoauldJaffaAbductionAssault;
        public static RaidStrategyDef SG1_GoauldJaffaDestructionAssault;
        public static JobDef SG1_GoauldAutonomousImplant;
        public static ThingDef SG1_GoauldRitualBasin;
        public static ThingDef SG1_TokraRelaySabotageDevice;
        public static ThingDef SG1_TokraRelaySiteWall;
        public static ThingDef SG1_TokraRelaySiteDoor;
        public static ThingDef SG1_TokraRelaySiteBarricade;
        public static JobDef SG1_TokraSabotageRelayDevice;

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
        public static ThingDef SG1_MatokStaff;
        public static ThingDef SG1_ZatnikTel;
        public static HediffDef SG1_TretoninSubstitution;
        public static ThingDef SG1_JaffaDeployedHelmet;
        public static ThingDef SG1_JaffaRetractedHelmet;

        static GR_DefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(GR_DefOf));
        }
    }
}
