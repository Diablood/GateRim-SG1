using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI.Group;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Low-frequency biological threat that releases a small, threat-scaled
    /// group of hostile free Goa'uld symbiotes at a reachable map edge.
    ///
    /// The incident deliberately reuses the existing identity-safe autonomous
    /// implantation behavior instead of creating a parallel infection system.
    /// </summary>
    public sealed class IncidentWorker_GoauldFreeSymbioteIncursion
        : IncidentWorker
    {
        private const int MinimumTargetAgeYears = 13;
        private const int MinimumSymbioteCount = 1;
        private const int MaximumSymbioteCount = 4;
        private const float ThreatPointsPerAdditionalSymbiote = 800f;
        private const int SpawnRadius = 5;

        private static readonly string[] LetterTextKeys =
        {
            "GR_GoauldFreeSymbioteIncursion_LetterText_0",
            "GR_GoauldFreeSymbioteIncursion_LetterText_1",
            "GR_GoauldFreeSymbioteIncursion_LetterText_2"
        };

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            if (!base.CanFireNowSub(parms))
            {
                return false;
            }

            Map map = parms?.target as Map;

            if (map == null
                || GR_DefOf.SG1_GoauldSymbiote == null
                || GR_DefOf.SG1_GoauldRecentImplantation == null
                || GR_DefOf.SG1_GoauldHostSymbiote == null
                || ResolveExistingGoauldFaction() == null
                || !HasEligiblePlayerHost(map))
            {
                return false;
            }

            IntVec3 unusedEntryCell;
            return TryFindEntryCell(map, out unusedEntryCell);
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = parms?.target as Map;
            PawnKindDef symbioteKind = GR_DefOf.SG1_GoauldSymbiote;
            Faction goauldFaction = ResolveExistingGoauldFaction();

            if (map == null || symbioteKind == null || goauldFaction == null)
            {
                GR_Log.Warning(
                    "Cannot start the Goa'uld free-symbiote incursion: "
                    + "the target map, symbiote kind or System Lord faction "
                    + "is unavailable.");
                return false;
            }

            if (!HasEligiblePlayerHost(map))
            {
                GR_Log.Message(
                    "Cannot start the Goa'uld free-symbiote incursion: "
                    + "no living compatible player colonist is present.");
                return false;
            }

            IntVec3 entryCell;

            if (!TryFindEntryCell(map, out entryCell))
            {
                GR_Log.Message(
                    "Cannot start the Goa'uld free-symbiote incursion: "
                    + "no reachable unfogged map-edge cell was found.");
                return false;
            }

            float threatPoints = ResolveThreatPoints(parms, map);
            int requestedCount = CalculateSymbioteCount(threatPoints);
            List<Pawn> spawnedSymbiotes = new List<Pawn>(requestedCount);

            for (int index = 0; index < requestedCount; index++)
            {
                Pawn symbiote = PawnGenerator.GeneratePawn(
                    symbioteKind,
                    goauldFaction,
                    map.Tile);

                if (symbiote == null)
                {
                    continue;
                }

                IntVec3 spawnCell = CellFinder.RandomClosewalkCellNear(
                    entryCell,
                    map,
                    SpawnRadius);
                Pawn spawned = GenSpawn.Spawn(
                    symbiote,
                    spawnCell.IsValid ? spawnCell : entryCell,
                    map) as Pawn;

                if (spawned != null)
                {
                    spawnedSymbiotes.Add(spawned);
                }
            }

            if (spawnedSymbiotes.Count == 0)
            {
                GR_Log.Warning(
                    "Cannot start the Goa'uld free-symbiote incursion: "
                    + "symbiote generation produced no spawned pawn.");
                return false;
            }

            Lord assaultLord = LordMaker.MakeNewLord(
                goauldFaction,
                new LordJob_AssaultColony(
                    goauldFaction,
                    canKidnap: false,
                    canTimeoutOrFlee: false),
                map);

            for (int index = 0; index < spawnedSymbiotes.Count; index++)
            {
                assaultLord.AddPawn(spawnedSymbiotes[index]);
            }

            SendIncursionLetter(spawnedSymbiotes);

            GR_Log.Message(
                "Started Goa'uld free-symbiote incursion at "
                + $"{entryCell}; spawned={spawnedSymbiotes.Count}/"
                + $"{requestedCount}; points={threatPoints:0}; "
                + $"faction={goauldFaction.Name} ({goauldFaction.loadID}); "
                + "assaultLord=active; retreat=false.");

            return true;
        }

        public static int CalculateSymbioteCount(float threatPoints)
        {
            int additional = (int)Math.Floor(
                Math.Max(0f, threatPoints)
                    / ThreatPointsPerAdditionalSymbiote);
            int count = MinimumSymbioteCount + additional;

            if (count < MinimumSymbioteCount)
            {
                return MinimumSymbioteCount;
            }

            if (count > MaximumSymbioteCount)
            {
                return MaximumSymbioteCount;
            }

            return count;
        }

        public static float ResolveThreatPoints(IncidentParms parms, Map map)
        {
            if (parms != null && parms.points > 0f)
            {
                return parms.points;
            }

            return map == null
                ? 0f
                : StorytellerUtility.DefaultThreatPointsNow(map);
        }

        private static Faction ResolveExistingGoauldFaction()
        {
            return GR_DefOf.SG1_GoauldSystemLordPrototype == null
                ? null
                : Find.FactionManager?.FirstFactionOfDef(
                    GR_DefOf.SG1_GoauldSystemLordPrototype);
        }

        private static bool HasEligiblePlayerHost(Map map)
        {
            if (map?.mapPawns?.FreeColonistsSpawned == null)
            {
                return false;
            }

            IReadOnlyList<Pawn> colonists = map.mapPawns.FreeColonistsSpawned;

            for (int index = 0; index < colonists.Count; index++)
            {
                Pawn pawn = colonists[index];

                if (IsEligiblePlayerHost(pawn))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsEligiblePlayerHost(Pawn pawn)
        {
            if (pawn == null
                || pawn.Destroyed
                || !pawn.Spawned
                || pawn.Dead
                || pawn.health == null
                || !pawn.RaceProps.Humanlike)
            {
                return false;
            }

            if (pawn.ageTracker != null
                && pawn.ageTracker.AgeBiologicalYearsFloat
                    < MinimumTargetAgeYears)
            {
                return false;
            }

            return !HasHediff(pawn, GR_DefOf.SG1_GoauldRecentImplantation)
                && !HasHediff(pawn, GR_DefOf.SG1_GoauldHostSymbiote);
        }

        private static bool HasHediff(Pawn pawn, HediffDef hediffDef)
        {
            if (pawn?.health?.hediffSet?.hediffs == null || hediffDef == null)
            {
                return false;
            }

            List<Hediff> hediffs = pawn.health.hediffSet.hediffs;

            for (int index = 0; index < hediffs.Count; index++)
            {
                if (hediffs[index].def == hediffDef)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool TryFindEntryCell(Map map, out IntVec3 cell)
        {
            return CellFinder.TryFindRandomEdgeCellWith(
                candidate => candidate.Standable(map)
                    && !candidate.Fogged(map)
                    && map.reachability.CanReachColony(candidate),
                map,
                CellFinder.EdgeRoadChance_Hostile,
                out cell);
        }

        private static void SendIncursionLetter(
            List<Pawn> spawnedSymbiotes)
        {
            int variant = GameComponent_GoauldFreeSymbioteIncursionTracker
                    .Current
                    ?.SelectLetterVariant(LetterTextKeys.Length)
                ?? Rand.Range(0, LetterTextKeys.Length);
            string textKey = LetterTextKeys[variant];
            LookTargets targets = new LookTargets(spawnedSymbiotes[0]);

            Find.LetterStack?.ReceiveLetter(
                "GR_GoauldFreeSymbioteIncursion_LetterLabel".Translate(),
                textKey.Translate(spawnedSymbiotes.Count),
                LetterDefOf.ThreatSmall,
                targets);
        }
    }
}
