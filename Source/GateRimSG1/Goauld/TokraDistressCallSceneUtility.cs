using System;
using System.Collections.Generic;
using GateRimSG1.Missions;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    internal sealed class TokraDistressCallSceneResult
    {
        public TokraDistressCallSceneType SceneType;
        public IntVec3 Anchor;
        public IntVec3 SurvivorAnchor;
        public IntVec3 DefenderAnchor;
        public IntVec3 SalvageAnchor;
        public IntVec3 PreferredEntryCell;
        public int TokraCorpseCount;
        public int JaffaCorpseCount;
    }

    /// <summary>
    /// Builds a compact narrative scene from vanilla terrain, buildings,
    /// items and pawns. Every mission actor is then placed relative to the
    /// same anchor so survivors and defenders form one coherent encounter.
    /// </summary>
    internal static class TokraDistressCallSceneUtility
    {
        private const int SceneSearchRadius = 14;
        private const int SceneClearance = 14;
        private const int SurvivorRadius = 4;
        private const int DefenderRadius = 15;

        public static TokraDistressCallSceneResult Generate(
            Map map,
            GateRimMissionDistressCallDef profile,
            TokraDistressCallVariant variant)
        {
            TokraDistressCallSceneResult result
                = new TokraDistressCallSceneResult
                {
                    SceneType = SelectSceneType(profile, variant),
                    Anchor = FindSceneAnchor(map)
                };

            Faction tokraFaction
                = TokraFactionUtility.GetOrCreatePersistentFaction(
                    "distress-call scene");
            Faction goauldFaction
                = GoauldSystemLordFactionUtility.GetOrCreateFaction(
                    "Tok'ra distress-call scene");

            switch (result.SceneType)
            {
                case TokraDistressCallSceneType.TemporaryCamp:
                    GenerateTemporaryCamp(
                        map,
                        result.Anchor,
                        tokraFaction,
                        damaged: false);
                    break;
                case TokraDistressCallSceneType.CompromisedPosition:
                    GenerateCompromisedPosition(
                        map,
                        result.Anchor,
                        goauldFaction);
                    break;
                case TokraDistressCallSceneType.OverrunCamp:
                    GenerateTemporaryCamp(
                        map,
                        result.Anchor,
                        tokraFaction,
                        damaged: true);
                    break;
                case TokraDistressCallSceneType.AmbushedCaravan:
                    GenerateAmbushedCaravan(map, result.Anchor);
                    break;
            }

            result.SurvivorAnchor = FindOpenCellNear(
                map,
                result.Anchor,
                SurvivorRadius,
                result.Anchor);
            result.DefenderAnchor = FindDefenderAnchor(map, result.Anchor);
            result.SalvageAnchor = FindOpenCellNear(
                map,
                result.Anchor,
                7,
                result.Anchor);
            result.PreferredEntryCell = FindNearestEdgeCell(
                map,
                result.Anchor);

            GetCorpseRange(
                profile,
                variant,
                tokra: true,
                out int tokraMinimum,
                out int tokraMaximum);
            GetCorpseRange(
                profile,
                variant,
                tokra: false,
                out int jaffaMinimum,
                out int jaffaMaximum);

            result.TokraCorpseCount = SpawnCorpses(
                map,
                result.Anchor,
                ResolveTokraPawnKind(profile),
                tokraFaction,
                tokraMinimum,
                tokraMaximum);
            result.JaffaCorpseCount = SpawnCorpses(
                map,
                result.Anchor,
                ResolveRandomJaffaKind(),
                goauldFaction,
                jaffaMinimum,
                jaffaMaximum);

            return result;
        }

        private static TokraDistressCallSceneType SelectSceneType(
            GateRimMissionDistressCallDef profile,
            TokraDistressCallVariant variant)
        {
            switch (variant)
            {
                case TokraDistressCallVariant.CompromisedSignal:
                    return TokraDistressCallSceneType.CompromisedPosition;
                case TokraDistressCallVariant.LateArrival:
                    return Rand.Value
                            < Clamp01(
                                profile?.lateArrivalOverrunCampChance
                                    ?? 0.70f)
                        ? TokraDistressCallSceneType.OverrunCamp
                        : TokraDistressCallSceneType.AmbushedCaravan;
                default:
                    return Rand.Value
                            < Clamp01(
                                profile?.genuineRescueTemporaryCampChance
                                    ?? 0.55f)
                        ? TokraDistressCallSceneType.TemporaryCamp
                        : TokraDistressCallSceneType.AmbushedCaravan;
            }
        }

        private static IntVec3 FindSceneAnchor(Map map)
        {
            if (map == null)
            {
                return IntVec3.Invalid;
            }

            int minimumDimension = Math.Min(map.Size.x, map.Size.z);
            int inset = Math.Max(
                24,
                Math.Min(36, minimumDimension / 4));
            int lateralJitter = Math.Max(4, minimumDimension / 12);
            int side = Rand.Range(0, 4);
            IntVec3 candidate;

            switch (side)
            {
                case 1:
                    candidate = new IntVec3(
                        map.Size.x - inset - 1,
                        0,
                        map.Center.z + Rand.RangeInclusive(
                            -lateralJitter,
                            lateralJitter));
                    break;
                case 2:
                    candidate = new IntVec3(
                        map.Center.x + Rand.RangeInclusive(
                            -lateralJitter,
                            lateralJitter),
                        0,
                        inset);
                    break;
                case 3:
                    candidate = new IntVec3(
                        map.Center.x + Rand.RangeInclusive(
                            -lateralJitter,
                            lateralJitter),
                        0,
                        map.Size.z - inset - 1);
                    break;
                default:
                    candidate = new IntVec3(
                        inset,
                        0,
                        map.Center.z + Rand.RangeInclusive(
                            -lateralJitter,
                            lateralJitter));
                    break;
            }

            if (CellFinder.TryFindRandomCellNear(
                    candidate,
                    map,
                    SceneSearchRadius,
                    cell => IsUsableSceneCell(cell, map),
                    out IntVec3 anchor))
            {
                return anchor;
            }

            if (CellFinder.TryFindRandomCellNear(
                    map.Center,
                    map,
                    SceneSearchRadius,
                    cell => IsUsableSceneCell(cell, map),
                    out anchor))
            {
                return anchor;
            }

            return map.Center;
        }

        private static bool IsUsableSceneCell(IntVec3 cell, Map map)
        {
            return cell.InBounds(map)
                && cell.x >= SceneClearance
                && cell.z >= SceneClearance
                && cell.x < map.Size.x - SceneClearance
                && cell.z < map.Size.z - SceneClearance
                && cell.Standable(map)
                && cell.GetFirstBuilding(map) == null;
        }

        private static IntVec3 FindNearestEdgeCell(
            Map map,
            IntVec3 anchor)
        {
            if (map == null || !anchor.IsValid)
            {
                return IntVec3.Invalid;
            }

            int west = anchor.x;
            int east = map.Size.x - 1 - anchor.x;
            int south = anchor.z;
            int north = map.Size.z - 1 - anchor.z;
            int minimum = Math.Min(Math.Min(west, east), Math.Min(south, north));

            if (minimum == west)
            {
                return new IntVec3(0, 0, anchor.z);
            }

            if (minimum == east)
            {
                return new IntVec3(map.Size.x - 1, 0, anchor.z);
            }

            if (minimum == south)
            {
                return new IntVec3(anchor.x, 0, 0);
            }

            return new IntVec3(anchor.x, 0, map.Size.z - 1);
        }

        private static IntVec3 FindDefenderAnchor(
            Map map,
            IntVec3 anchor)
        {
            if (map == null)
            {
                return anchor;
            }

            if (CellFinder.TryFindRandomCellNear(
                    anchor,
                    map,
                    DefenderRadius,
                    cell => IsOpenCell(cell, map)
                        && HorizontalDistanceSquared(cell, anchor) >= 64,
                    out IntVec3 result))
            {
                return result;
            }

            return FindOpenCellNear(map, anchor, 8, anchor);
        }

        private static void GenerateTemporaryCamp(
            Map map,
            IntVec3 anchor,
            Faction faction,
            bool damaged)
        {
            if (map == null || !anchor.IsValid)
            {
                return;
            }

            ClearSceneArea(map, anchor, 6);
            PaintPackedDirt(map, anchor, 5);
            ThingDef wood = DefDatabase<ThingDef>.GetNamedSilentFail(
                "WoodLog");

            for (int offset = -4; offset <= 4; offset++)
            {
                TrySpawnBuilding(
                    map,
                    anchor + new IntVec3(offset, 0, 3),
                    "Wall",
                    faction,
                    wood,
                    damaged && Rand.Chance(0.35f));
            }

            for (int offset = -2; offset <= 3; offset++)
            {
                TrySpawnBuilding(
                    map,
                    anchor + new IntVec3(-4, 0, offset),
                    "Wall",
                    faction,
                    wood,
                    damaged && Rand.Chance(0.30f));
                TrySpawnBuilding(
                    map,
                    anchor + new IntVec3(4, 0, offset),
                    "Wall",
                    faction,
                    wood,
                    damaged && Rand.Chance(0.30f));
            }

            for (int offset = -3; offset <= 3; offset++)
            {
                if (offset == 0)
                {
                    continue;
                }

                TrySpawnBuilding(
                    map,
                    anchor + new IntVec3(offset, 0, -3),
                    "Sandbags",
                    faction);
            }

            TrySpawnBuilding(
                map,
                anchor + new IntVec3(-2, 0, 0),
                "SleepingSpot",
                faction);
            TrySpawnBuilding(
                map,
                anchor,
                "SleepingSpot",
                faction);
            TrySpawnBuilding(
                map,
                anchor + new IntVec3(2, 0, 0),
                "SleepingSpot",
                faction);
            TrySpawnBuilding(
                map,
                anchor + new IntVec3(0, 0, -1),
                "Campfire",
                faction);

            AddConstructedRoof(map, anchor, 3, 2);

            if (damaged)
            {
                SpawnDebris(map, anchor, 3, 6);
            }
        }

        private static void GenerateCompromisedPosition(
            Map map,
            IntVec3 anchor,
            Faction faction)
        {
            if (map == null || !anchor.IsValid)
            {
                return;
            }

            ClearSceneArea(map, anchor, 5);
            PaintPackedDirt(map, anchor, 4);

            for (int offset = -4; offset <= 4; offset++)
            {
                if (offset == 0)
                {
                    continue;
                }

                TrySpawnBuilding(
                    map,
                    anchor + new IntVec3(offset, 0, 2),
                    "Sandbags",
                    faction);
            }

            TrySpawnBuilding(
                map,
                anchor + new IntVec3(-4, 0, 1),
                "Sandbags",
                faction);
            TrySpawnBuilding(
                map,
                anchor + new IntVec3(4, 0, 1),
                "Sandbags",
                faction);
            SpawnDebris(map, anchor, 1, 3);
        }

        private static void GenerateAmbushedCaravan(
            Map map,
            IntVec3 anchor)
        {
            if (map == null || !anchor.IsValid)
            {
                return;
            }

            ClearSceneArea(map, anchor, 4);
            SpawnDebris(map, anchor, 4, 8);
        }

        private static void PaintPackedDirt(
            Map map,
            IntVec3 anchor,
            int radius)
        {
            if (map == null || TerrainDefOf.PackedDirt == null)
            {
                return;
            }

            CellRect rect = new CellRect(
                anchor.x - radius,
                anchor.z - radius,
                radius * 2 + 1,
                radius * 2 + 1);

            foreach (IntVec3 cell in rect.Cells)
            {
                if (cell.InBounds(map))
                {
                    map.terrainGrid.SetTerrain(cell, TerrainDefOf.PackedDirt);
                }
            }
        }

        private static void AddConstructedRoof(
            Map map,
            IntVec3 anchor,
            int horizontalRadius,
            int verticalRadius)
        {
            if (map?.roofGrid == null || RoofDefOf.RoofConstructed == null)
            {
                return;
            }

            CellRect rect = new CellRect(
                anchor.x - horizontalRadius,
                anchor.z - verticalRadius,
                horizontalRadius * 2 + 1,
                verticalRadius * 2 + 1);

            foreach (IntVec3 cell in rect.Cells)
            {
                if (cell.InBounds(map))
                {
                    map.roofGrid.SetRoof(cell, RoofDefOf.RoofConstructed);
                }
            }
        }

        private static void ClearSceneArea(
            Map map,
            IntVec3 anchor,
            int radius)
        {
            if (map == null)
            {
                return;
            }

            CellRect rect = new CellRect(
                anchor.x - radius,
                anchor.z - radius,
                radius * 2 + 1,
                radius * 2 + 1);

            foreach (IntVec3 cell in rect.Cells)
            {
                if (!cell.InBounds(map))
                {
                    continue;
                }

                List<Thing> things = cell.GetThingList(map);

                for (int index = things.Count - 1; index >= 0; index--)
                {
                    Thing thing = things[index];

                    if (thing == null
                        || thing.Destroyed
                        || thing is Pawn
                        || thing.def.category == ThingCategory.Filth)
                    {
                        continue;
                    }

                    thing.Destroy(DestroyMode.Vanish);
                }
            }
        }

        private static void SpawnDebris(
            Map map,
            IntVec3 anchor,
            int minimum,
            int maximum)
        {
            ThingDef debrisDef = DefDatabase<ThingDef>.GetNamedSilentFail(
                "ChunkSlagSteel");

            if (map == null || debrisDef == null)
            {
                return;
            }

            int count = Rand.RangeInclusive(
                Math.Max(0, minimum),
                Math.Max(minimum, maximum));

            for (int index = 0; index < count; index++)
            {
                if (!TryFindOpenCellNear(
                        map,
                        anchor,
                        7,
                        out IntVec3 cell))
                {
                    continue;
                }

                GenSpawn.Spawn(ThingMaker.MakeThing(debrisDef), cell, map);
            }
        }

        private static bool TrySpawnBuilding(
            Map map,
            IntVec3 cell,
            string defName,
            Faction faction,
            ThingDef stuff = null,
            bool omit = false)
        {
            if (omit
                || map == null
                || !cell.InBounds(map)
                || cell.GetFirstBuilding(map) != null)
            {
                return false;
            }

            ThingDef thingDef = DefDatabase<ThingDef>.GetNamedSilentFail(
                defName);

            if (thingDef == null)
            {
                return false;
            }

            ThingDef resolvedStuff = thingDef.MadeFromStuff ? stuff : null;

            if (thingDef.MadeFromStuff && resolvedStuff == null)
            {
                resolvedStuff = DefDatabase<ThingDef>.GetNamedSilentFail(
                    "WoodLog");

                if (resolvedStuff == null)
                {
                    return false;
                }
            }

            Thing thing = ThingMaker.MakeThing(thingDef, resolvedStuff);
            Thing spawned = GenSpawn.Spawn(thing, cell, map);
            spawned?.SetFaction(faction);
            return spawned != null;
        }

        private static int SpawnCorpses(
            Map map,
            IntVec3 anchor,
            PawnKindDef pawnKind,
            Faction faction,
            int minimum,
            int maximum)
        {
            if (map == null || pawnKind == null || faction == null)
            {
                return 0;
            }

            int safeMinimum = Math.Max(0, minimum);
            int safeMaximum = Math.Max(safeMinimum, maximum);
            int requestedCount = Rand.RangeInclusive(
                safeMinimum,
                safeMaximum);
            int spawnedCount = 0;

            for (int index = 0; index < requestedCount; index++)
            {
                if (!TryFindOpenCellNear(
                        map,
                        anchor,
                        8,
                        out IntVec3 cell))
                {
                    continue;
                }

                Pawn pawn = PawnGenerator.GeneratePawn(
                    pawnKind,
                    faction,
                    map.Tile);

                if (pawn == null)
                {
                    continue;
                }

                Pawn spawnedPawn = GenSpawn.Spawn(pawn, cell, map) as Pawn;

                if (spawnedPawn == null)
                {
                    continue;
                }

                spawnedPawn.Kill(null, null);
                spawnedCount++;
            }

            return spawnedCount;
        }

        private static IntVec3 FindOpenCellNear(
            Map map,
            IntVec3 anchor,
            int radius,
            IntVec3 fallback)
        {
            return TryFindOpenCellNear(map, anchor, radius, out IntVec3 cell)
                ? cell
                : fallback;
        }

        private static bool TryFindOpenCellNear(
            Map map,
            IntVec3 anchor,
            int radius,
            out IntVec3 cell)
        {
            return CellFinder.TryFindRandomCellNear(
                anchor,
                map,
                radius,
                candidate => IsOpenCell(candidate, map),
                out cell);
        }

        private static bool IsOpenCell(IntVec3 cell, Map map)
        {
            return cell.InBounds(map)
                && cell.Standable(map)
                && cell.GetFirstBuilding(map) == null
                && cell.GetFirstPawn(map) == null;
        }

        private static int HorizontalDistanceSquared(
            IntVec3 first,
            IntVec3 second)
        {
            int deltaX = first.x - second.x;
            int deltaZ = first.z - second.z;
            return deltaX * deltaX + deltaZ * deltaZ;
        }

        private static PawnKindDef ResolveTokraPawnKind(
            GateRimMissionDistressCallDef profile)
        {
            return string.IsNullOrWhiteSpace(profile?.survivorPawnKindDefName)
                ? null
                : DefDatabase<PawnKindDef>.GetNamedSilentFail(
                    profile.survivorPawnKindDefName);
        }

        private static PawnKindDef ResolveRandomJaffaKind()
        {
            if (GR_DefOf.SG1_GoauldJaffaWarrior == null)
            {
                return GR_DefOf.SG1_GoauldJaffaGuard;
            }

            if (GR_DefOf.SG1_GoauldJaffaGuard == null)
            {
                return GR_DefOf.SG1_GoauldJaffaWarrior;
            }

            return Rand.Bool
                ? GR_DefOf.SG1_GoauldJaffaWarrior
                : GR_DefOf.SG1_GoauldJaffaGuard;
        }

        private static void GetCorpseRange(
            GateRimMissionDistressCallDef profile,
            TokraDistressCallVariant variant,
            bool tokra,
            out int minimum,
            out int maximum)
        {
            minimum = 0;
            maximum = 0;

            if (profile == null)
            {
                return;
            }

            switch (variant)
            {
                case TokraDistressCallVariant.CompromisedSignal:
                    minimum = tokra
                        ? profile.trapTokraCorpseMinimumCount
                        : profile.trapJaffaCorpseMinimumCount;
                    maximum = tokra
                        ? profile.trapTokraCorpseMaximumCount
                        : profile.trapJaffaCorpseMaximumCount;
                    break;
                case TokraDistressCallVariant.LateArrival:
                    minimum = tokra
                        ? profile.lateTokraCorpseMinimumCount
                        : profile.lateJaffaCorpseMinimumCount;
                    maximum = tokra
                        ? profile.lateTokraCorpseMaximumCount
                        : profile.lateJaffaCorpseMaximumCount;
                    break;
                default:
                    minimum = tokra
                        ? profile.rescueTokraCorpseMinimumCount
                        : profile.rescueJaffaCorpseMinimumCount;
                    maximum = tokra
                        ? profile.rescueTokraCorpseMaximumCount
                        : profile.rescueJaffaCorpseMaximumCount;
                    break;
            }
        }

        private static float Clamp01(float value)
        {
            return Math.Max(0f, Math.Min(1f, value));
        }
    }
}
