using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public sealed class GoauldDomainReprisalState : IExposable
    {
        public Faction domainFaction;
        public Faction alliedDomainFaction;
        public int targetMapUniqueId = -1;
        public int reprisalTick;
        public int nextEligibleTick;
        public float raidPoints;
        public string formerHostLabel = string.Empty;
        public string symbioteName = string.Empty;
        public Pawn demandedSymbiote;
        public int ultimatumExpiryTick;
        public bool ultimatumPending;
        public bool debugGeneratedSymbiote;
        public bool debugShortDelay;
        public bool pending;
        public bool sharedAllianceReprisal;
        public bool monitoringAllianceDefeat;
        public bool debugForcedSharedReprisal;
        public int sourceInitialPawnCount;
        public List<Pawn> sourcePawns = new List<Pawn>();

        public void ExposeData()
        {
            Scribe_References.Look(ref domainFaction, "domainFaction");
            Scribe_References.Look(
                ref alliedDomainFaction,
                "alliedDomainFaction");
            Scribe_Values.Look(
                ref targetMapUniqueId,
                "targetMapUniqueId",
                -1);
            Scribe_Values.Look(ref reprisalTick, "reprisalTick", 0);
            Scribe_Values.Look(
                ref nextEligibleTick,
                "nextEligibleTick",
                0);
            Scribe_Values.Look(ref raidPoints, "raidPoints", 0f);
            Scribe_Values.Look(
                ref formerHostLabel,
                "formerHostLabel",
                string.Empty);
            Scribe_Values.Look(
                ref symbioteName,
                "symbioteName",
                string.Empty);
            Scribe_References.Look(
                ref demandedSymbiote,
                "demandedSymbiote");
            Scribe_Values.Look(
                ref ultimatumExpiryTick,
                "ultimatumExpiryTick",
                0);
            Scribe_Values.Look(
                ref ultimatumPending,
                "ultimatumPending",
                false);
            Scribe_Values.Look(
                ref debugGeneratedSymbiote,
                "debugGeneratedSymbiote",
                false);
            Scribe_Values.Look(
                ref debugShortDelay,
                "debugShortDelay",
                false);
            Scribe_Values.Look(ref pending, "pending", false);
            Scribe_Values.Look(
                ref sharedAllianceReprisal,
                "sharedAllianceReprisal",
                false);
            Scribe_Values.Look(
                ref monitoringAllianceDefeat,
                "monitoringAllianceDefeat",
                false);
            Scribe_Values.Look(
                ref debugForcedSharedReprisal,
                "debugForcedSharedReprisal",
                false);
            Scribe_Values.Look(
                ref sourceInitialPawnCount,
                "sourceInitialPawnCount",
                0);
            Scribe_Collections.Look(
                ref sourcePawns,
                "sourcePawns",
                LookMode.Reference);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                sourcePawns = sourcePawns ?? new List<Pawn>();
            }
        }
    }
}
