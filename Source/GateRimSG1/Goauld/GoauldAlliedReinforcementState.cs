using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
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
