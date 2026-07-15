using GateRimSG1.Goauld;
using GateRimSG1.Jaffa;
using GateRimSG1.Missions;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1
{
    [DefOf]
    public static class GR_DefOf
    {
        public static HediffDef SG1_GoauldRecentImplantation;
        public static GeneDef SG1_NaquadahBlood;
        public static XenotypeDef SG1_GoauldHost;
        public static XenotypeDef SG1_Jaffa;
        public static HediffDef SG1_GoauldHostSymbiote;
        public static HediffDef SG1_KaraKeshNeuralAgony;
        public static HediffDef SG1_KaraKeshParalysisHold;
        public static HediffDef SG1_GoauldHealingBraceletFatigue;
        public static PawnKindDef SG1_GoauldSymbiote;
        public static PawnKindDef SG1_TokraSymbiote;
        public static PawnKindDef SG1_TokraVoluntaryHost;
        public static PawnKindDef SG1_GoauldJaffaWarrior;
        public static PawnKindDef SG1_GoauldJaffaGuard;
        public static PawnKindDef SG1_GoauldJaffaOfficer;
        public static PawnKindDef SG1_GoauldJaffaFieldOfficer;
        public static PawnKindDef SG1_GoauldSettlementJaffaWarrior;
        public static PawnKindDef SG1_GoauldSettlementJaffaGuard;
        public static PawnKindDef SG1_GoauldSettlementJaffaOfficer;
        public static PawnKindDef SG1_FreeJaffaWarrior;
        public static PawnKindDef SG1_FreeJaffaGuard;
        public static PawnKindDef SG1_FreeJaffaTrader;
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
        public static WorldObjectDef SG1_TokraDistressCallWorldSite;
        public static WorldObjectDef SG1_TokraIntroductionArtifactWorldSite;
        public static WorldObjectDef SG1_TokraTemporaryBaseDeliverySite;
        public static WorldObjectDef SG1_GoauldOpenConflictBattlefieldSite;
        public static GateRimMissionDef SG1_TokraIntroductionArtifactMission;
        public static SitePartDef SG1_TokraHiddenSafehouseSitePart;
        public static IncidentDef SG1_FreeJaffaPeacefulVisitors;
        public static IncidentDef SG1_GoauldJaffaNaturalRaid;
        public static IncidentDef SG1_GoauldQueenArrival;
        public static IncidentDef SG1_GoauldFreeSymbioteIncursion;
        public static IncidentDef SG1_GoauldJaffaControlledRaid;

        public static IncidentDef SG1_GoauldJaffaSignalPatrol;
        public static IncidentDef SG1_GoauldJaffaControlledAbductionRaid;
        public static IncidentDef SG1_GoauldJaffaControlledDestructionRaid;
        public static RaidStrategyDef SG1_GoauldJaffaAbductionAssault;
        public static RaidStrategyDef SG1_GoauldJaffaDestructionAssault;
        public static JobDef SG1_GoauldAutonomousImplant;
        public static ThingDef SG1_GoauldRitualBasin;
        public static ThingDef SG1_KaraKesh;
        public static ThingDef SG1_GoauldHealingBracelet;
        public static ThingDef SG1_TokraIntroductionArtifact;
        public static ThingDef SG1_TokraSecureCommunicator;
        public static ResearchProjectDef SG1_TokraSecureCommunications;
        public static ThingDef SG1_TokraRelaySabotageDevice;
        public static JobDef SG1_TokraSabotageRelayDevice;

        // Legacy technical genes retained only for save migration.
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
        public static ThingDef SG1_Bolas;
        public static ThingDef SG1_BolasProjectile;
        public static ThingDef SG1_TokraHypodermicRifle;
        public static ThingDef SG1_TokraHypodermicDart;
        public static HediffDef SG1_NonLethalNeutralization;
        public static HediffDef SG1_BolasRestraint;
        public static HediffDef SG1_TretoninSubstitution;
        public static ThingDef SG1_JaffaDeployedHelmet;
        public static ThingDef SG1_JaffaRetractedHelmet;
        public static ThingDef SG1_JaffaOfficerArmor;
        public static ThingDef SG1_JaffaOfficerDeployedHelmet;
        public static ThingDef SG1_JaffaOfficerRetractedHelmet;

        static GR_DefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(GR_DefOf));
        }
    }
}
