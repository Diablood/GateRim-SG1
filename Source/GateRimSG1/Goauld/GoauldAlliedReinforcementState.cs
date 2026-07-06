using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public enum GoauldAlliedRaidManifestation
    {
        DelayedReinforcement = 0,
        JointRaid = 1
    }

    public sealed class GoauldAlliedReinforcementState : IExposable
    {
        public int targetMapUniqueId = -1;
        public Faction primaryDomain;
        public Faction alliedDomain;
        public float alliedPoints;
        public int arrivalTick;
        public int spawnAttempts;
        public bool arrived;
        public bool withdrawalOrdered;
        public int cooperationExpiryTick;
        public GoauldAlliedRaidManifestation manifestation =
            GoauldAlliedRaidManifestation.DelayedReinforcement;
        public IntVec3 alliedSpawnCenter = IntVec3.Invalid;
        public int initialPrimaryPawnCount;
        public int initialAlliedPawnCount;
        public List<Pawn> primaryPawns = new List<Pawn>();
        public List<Pawn> alliedPawns = new List<Pawn>();

        public void ExposeData()
        {
            Scribe_Values.Look(
                ref targetMapUniqueId,
                "targetMapUniqueId",
                -1);
            Scribe_References.Look(ref primaryDomain, "primaryDomain");
            Scribe_References.Look(ref alliedDomain, "alliedDomain");
            Scribe_Values.Look(ref alliedPoints, "alliedPoints", 0f);
            Scribe_Values.Look(ref arrivalTick, "arrivalTick", 0);
            Scribe_Values.Look(ref spawnAttempts, "spawnAttempts", 0);
            Scribe_Values.Look(ref arrived, "arrived", false);
            Scribe_Values.Look(
                ref withdrawalOrdered,
                "withdrawalOrdered",
                false);
            Scribe_Values.Look(
                ref cooperationExpiryTick,
                "cooperationExpiryTick",
                0);
            int manifestationValue = (int)manifestation;
            Scribe_Values.Look(
                ref manifestationValue,
                "manifestation",
                (int)GoauldAlliedRaidManifestation
                    .DelayedReinforcement);
            manifestation = Enum.IsDefined(
                typeof(GoauldAlliedRaidManifestation),
                manifestationValue)
                ? (GoauldAlliedRaidManifestation)manifestationValue
                : GoauldAlliedRaidManifestation.DelayedReinforcement;
            Scribe_Values.Look(
                ref alliedSpawnCenter,
                "alliedSpawnCenter",
                IntVec3.Invalid);
            Scribe_Values.Look(
                ref initialPrimaryPawnCount,
                "initialPrimaryPawnCount",
                0);
            Scribe_Values.Look(
                ref initialAlliedPawnCount,
                "initialAlliedPawnCount",
                0);
            Scribe_Collections.Look(
                ref primaryPawns,
                "primaryPawns",
                LookMode.Reference);
            Scribe_Collections.Look(
                ref alliedPawns,
                "alliedPawns",
                LookMode.Reference);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                primaryPawns = primaryPawns ?? new List<Pawn>();
                alliedPawns = alliedPawns ?? new List<Pawn>();
            }
        }
    }
}
