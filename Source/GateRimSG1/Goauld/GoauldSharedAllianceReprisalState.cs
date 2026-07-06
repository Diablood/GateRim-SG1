using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public sealed class GoauldSharedAllianceRaidObservation : IExposable
    {
        public int targetMapUniqueId = -1;
        public Faction primaryDomain;
        public Faction alliedDomain;
        public List<Pawn> primaryPawns = new List<Pawn>();
        public int initialPawnCount;

        public void ExposeData()
        {
            Scribe_Values.Look(
                ref targetMapUniqueId,
                "targetMapUniqueId",
                -1);
            Scribe_References.Look(ref primaryDomain, "primaryDomain");
            Scribe_References.Look(ref alliedDomain, "alliedDomain");
            Scribe_Collections.Look(
                ref primaryPawns,
                "primaryPawns",
                LookMode.Reference);
            Scribe_Values.Look(
                ref initialPawnCount,
                "initialPawnCount",
                0);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                primaryPawns = primaryPawns
                    ?? new List<Pawn>();
            }
        }
    }

    public sealed class GoauldSharedAllianceReprisalState : IExposable
    {
        public Faction primaryDomain;
        public Faction alliedDomain;
        public int targetMapUniqueId = -1;
        public int reprisalTick;
        public int nextEligibleTick;
        public float raidPoints;
        public bool pending;
        public bool debugShortDelay;
        public bool debugForcedExecution;

        public void ExposeData()
        {
            Scribe_References.Look(ref primaryDomain, "primaryDomain");
            Scribe_References.Look(ref alliedDomain, "alliedDomain");
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
            Scribe_Values.Look(ref pending, "pending", false);
            Scribe_Values.Look(
                ref debugShortDelay,
                "debugShortDelay",
                false);
            Scribe_Values.Look(
                ref debugForcedExecution,
                "debugForcedExecution",
                false);
        }
    }

    public sealed class GoauldAllianceMajorFailureObservation : IExposable
    {
        public int targetMapUniqueId = -1;
        public Faction primaryDomain;
        public Faction alliedDomain;
        public List<Pawn> primaryPawns = new List<Pawn>();
        public List<Pawn> alliedPawns = new List<Pawn>();
        public int initialPawnCount;

        public void ExposeData()
        {
            Scribe_Values.Look(
                ref targetMapUniqueId,
                "targetMapUniqueId",
                -1);
            Scribe_References.Look(ref primaryDomain, "primaryDomain");
            Scribe_References.Look(ref alliedDomain, "alliedDomain");
            Scribe_Collections.Look(
                ref primaryPawns,
                "primaryPawns",
                LookMode.Reference);
            Scribe_Collections.Look(
                ref alliedPawns,
                "alliedPawns",
                LookMode.Reference);
            Scribe_Values.Look(
                ref initialPawnCount,
                "initialPawnCount",
                0);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                primaryPawns = primaryPawns
                    ?? new List<Pawn>();
                alliedPawns = alliedPawns
                    ?? new List<Pawn>();
            }
        }
    }

    public enum GoauldAllianceRuptureOutcome
    {
        None,
        Pending,
        Completed,
        CancelledNoLongerAllied,
        CancelledInactiveDomain,
        CancelledTransitionFailed
    }

    public sealed class GoauldAllianceRuptureState : IExposable
    {
        public Faction primaryDomain;
        public Faction alliedDomain;
        public int targetMapUniqueId = -1;
        public int failureTick;
        public int ruptureTick;
        public int resolutionTick;
        public int initialPawnCount;
        public int activePawnCountAtFailure;
        public bool pending;
        public bool debugShortDelay;
        public GoauldAllianceRuptureOutcome outcome;

        public void ExposeData()
        {
            Scribe_References.Look(ref primaryDomain, "primaryDomain");
            Scribe_References.Look(ref alliedDomain, "alliedDomain");
            Scribe_Values.Look(
                ref targetMapUniqueId,
                "targetMapUniqueId",
                -1);
            Scribe_Values.Look(ref failureTick, "failureTick", 0);
            Scribe_Values.Look(ref ruptureTick, "ruptureTick", 0);
            Scribe_Values.Look(ref resolutionTick, "resolutionTick", 0);
            Scribe_Values.Look(
                ref initialPawnCount,
                "initialPawnCount",
                0);
            Scribe_Values.Look(
                ref activePawnCountAtFailure,
                "activePawnCountAtFailure",
                0);
            Scribe_Values.Look(ref pending, "pending", false);
            Scribe_Values.Look(
                ref debugShortDelay,
                "debugShortDelay",
                false);
            Scribe_Values.Look(
                ref outcome,
                "outcome",
                GoauldAllianceRuptureOutcome.None);
        }
    }
}
