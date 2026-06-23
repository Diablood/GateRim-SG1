using System;
using System.Collections.Generic;
using System.Linq;
using GateRimSG1.Missions;
using RimWorld;
using RimWorld.Planet;
using Verse;
using Verse.AI.Group;

namespace GateRimSG1.Goauld
{
    internal static class TokraIntroductionArtifactMissionUtility
    {
        public const string WorldObjectIdCounterKey
            = "introductionWorldObjectId";
        public const string ArtifactThingIdKey
            = "introductionArtifactThingId";

        private const int DefenderAssaultDelayTicks = 2500;
        private const int SceneSearchRadius = 28;
        private const int SceneClearance = 12;

        public static bool CanCreateWorldSite(
            Map map,
            GateRimMissionIntroductionDef profile)
        {
            return map != null
                && profile != null
                && map.Tile != PlanetTile.Invalid
                && Find.WorldObjects != null
                && ResolveWorldObjectDef(profile) != null
                && GoauldSystemLordFactionUtility.GetOrCreateFaction(
                    "Tok'ra introduction artifact offerability") != null;
        }

        public static bool TryCreateWorldSite(
            Map map,
            GateRimMissionDef definition,
            GateRimMissionRuntimeData runtime,
            out WorldObject_TokraIntroductionArtifactSite site)
        {
            site = null;
            GateRimMissionIntroductionDef profile = definition?.introduction;

            if (!CanCreateWorldSite(map, profile) || runtime == null)
            {
                return false;
            }

            PlanetTile tile;

            if (!TileFinder.TryFindNewSiteTile(
                    out tile,
                    map.Tile,
                    minDist: Math.Max(1, profile.minimumTileDistance),
                    maxDist: Math.Max(
                        profile.minimumTileDistance,
                        profile.maximumTileDistance),
                    allowCaravans: false,
                    selectLandmarkChance: 0f,
                    layer: map.Tile.Layer))
            {
                return false;
            }

            WorldObjectDef worldObjectDef = ResolveWorldObjectDef(profile);
            Faction goauldFaction = GoauldSystemLordFactionUtility
                .GetOrCreateFaction("Tok'ra introduction artifact site");
            WorldObject_TokraIntroductionArtifactSite createdSite
                = WorldObjectMaker.MakeWorldObject(worldObjectDef)
                    as WorldObject_TokraIntroductionArtifactSite;

            if (createdSite == null || goauldFaction == null)
            {
                return false;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            int expiryTick = currentTick + Math.Max(
                1,
                definition.timing?.deadlineTicks ?? 360000);

            createdSite.Tile = tile;
            createdSite.SetFaction(goauldFaction);
            createdSite.Initialize(
                map.uniqueID,
                definition.defName,
                runtime.scaledThreatPoints,
                expiryTick,
                Math.Max(80, profile.mapSize));

            Find.WorldObjects.Add(createdSite);
            runtime.counters[WorldObjectIdCounterKey] = createdSite.ID;
            runtime.SetString(ArtifactThingIdKey, null);
            site = createdSite;

            Find.LetterStack?.ReceiveLetter(
                "GR_TokraIntroduction_TargetLabel".Translate(),
                "GR_TokraIntroduction_TargetText".Translate(),
                LetterDefOf.NeutralEvent,
                createdSite);

            GR_Log.Message(
                "Created Tok'ra introduction artifact site at tile "
                + tile
                + "; threat snapshot "
                + runtime.scaledThreatPoints.ToString("0")
                + "; expiry "
                + expiryTick
                + ".");

            return true;
        }

        public static WorldObject_TokraIntroductionArtifactSite FindWorldSite(
            GateRimMissionRuntimeData runtime)
        {
            if (runtime?.counters == null || Find.WorldObjects == null)
            {
                return null;
            }

            int worldObjectId;

            if (!runtime.counters.TryGetValue(
                    WorldObjectIdCounterKey,
                    out worldObjectId))
            {
                return null;
            }

            return Find.WorldObjects.AllWorldObjects
                .OfType<WorldObject_TokraIntroductionArtifactSite>()
                .FirstOrDefault(worldObject =>
                    worldObject != null
                    && !worldObject.Destroyed
                    && worldObject.ID == worldObjectId);
        }

        public static void EnsureMissionMapInitialized(
            Map map,
            WorldObject_TokraIntroductionArtifactSite parent)
        {
            if (map == null || parent == null)
            {
                return;
            }

            MapComponent_TokraIntroductionArtifactMission component
                = map.GetComponent<
                    MapComponent_TokraIntroductionArtifactMission>();

            if (component == null || component.Initialized)
            {
                return;
            }

            GateRimMissionDef definition
                = GR_DefOf.SG1_TokraIntroductionArtifactMission;
            GateRimMissionIntroductionDef profile = definition?.introduction;

            if (definition == null
                || profile == null
                || GR_DefOf.SG1_TokraIntroductionArtifact == null)
            {
                component.MarkInitializationFailed(parent);
                return;
            }

            IntVec3 anchor = FindSceneAnchor(map);
            Thing artifact = SpawnArtifact(map, anchor);
            List<Pawn> hostiles = SpawnJaffaGroup(
                map,
                parent.ScaledThreatPoints,
                profile,
                anchor);

            if (artifact == null || hostiles.Count == 0)
            {
                if (artifact != null && !artifact.Destroyed)
                {
                    artifact.Destroy(DestroyMode.Vanish);
                }

                component.MarkInitializationFailed(parent);
                return;
            }

            IntVec3 preferredEntry = FindPreferredEntryCell(map, anchor);
            component.Initialize(
                parent,
                artifact,
                hostiles,
                preferredEntry);

            GameComponent_TokraIntroductionArc.NotifyArtifactCreated(
                parent,
                artifact);

            Find.LetterStack?.ReceiveLetter(
                "GR_TokraIntroduction_ArrivalLabel".Translate(),
                "GR_TokraIntroduction_ArrivalText".Translate(
                    hostiles.Count.ToString()),
                LetterDefOf.ThreatSmall,
                artifact);

            GR_Log.Message(
                "Initialized Tok'ra introduction artifact map "
                + map.uniqueID
                + "; artifact "
                + artifact.ThingID
                + "; hostiles "
                + hostiles.Count
                + "; scaled threat "
                + parent.ScaledThreatPoints.ToString("0")
                + "; anchor "
                + anchor
                + ".");
        }

        public static bool HasActiveHostiles(Map map)
        {
            return TokraRelaySabotageMissionUtility.HasActiveHostiles(map);
        }

        private static WorldObjectDef ResolveWorldObjectDef(
            GateRimMissionIntroductionDef profile)
        {
            if (GR_DefOf.SG1_TokraIntroductionArtifactWorldSite != null)
            {
                return GR_DefOf.SG1_TokraIntroductionArtifactWorldSite;
            }

            return string.IsNullOrWhiteSpace(profile?.worldObjectDefName)
                ? null
                : DefDatabase<WorldObjectDef>.GetNamedSilentFail(
                    profile.worldObjectDefName);
        }

        private static Thing SpawnArtifact(Map map, IntVec3 anchor)
        {
            Thing artifact = ThingMaker.MakeThing(
                GR_DefOf.SG1_TokraIntroductionArtifact);
            Thing placed;

            if (GenPlace.TryPlaceThing(
                    artifact,
                    anchor,
                    map,
                    ThingPlaceMode.Near,
                    out placed))
            {
                return placed;
            }

            if (!artifact.Destroyed)
            {
                artifact.Destroy(DestroyMode.Vanish);
            }

            return null;
        }

        private static List<Pawn> SpawnJaffaGroup(
            Map map,
            float requestedPoints,
            GateRimMissionIntroductionDef profile,
            IntVec3 sceneAnchor)
        {
            List<Pawn> spawnedPawns = new List<Pawn>();
            Faction faction = GoauldSystemLordFactionUtility.GetOrCreateFaction(
                "Tok'ra introduction artifact defenders");
            PawnKindDef warriorKind = GR_DefOf.SG1_GoauldJaffaWarrior;
            PawnKindDef guardKind = GR_DefOf.SG1_GoauldJaffaGuard;

            if (map == null
                || faction == null
                || (warriorKind == null && guardKind == null))
            {
                return spawnedPawns;
            }

            PawnKindDef referenceKind = warriorKind ?? guardKind;
            float combatPower = Math.Max(1f, referenceKind.combatPower);
            int minimum = Math.Max(1, profile.defenderMinimumCount);
            int maximum = Math.Max(minimum, profile.defenderMaximumCount);
            int pawnCount = Math.Max(
                minimum,
                Math.Min(
                    maximum,
                    (int)Math.Ceiling(
                        Math.Max(combatPower, requestedPoints)
                        / combatPower)));
            IntVec3 rootCell = sceneAnchor.IsValid
                ? sceneAnchor
                : map.Center;

            if (!CellFinder.TryFindRandomCellNear(
                    rootCell,
                    map,
                    8,
                    cell => cell.Standable(map)
                        && cell.GetFirstBuilding(map) == null
                        && cell.GetFirstPawn(map) == null,
                    out rootCell))
            {
                rootCell = sceneAnchor.IsValid
                    ? sceneAnchor
                    : map.Center;
            }

            Lord lord = LordMaker.MakeNewLord(
                faction,
                new LordJob_DefendBase(
                    faction,
                    rootCell,
                    DefenderAssaultDelayTicks,
                    attackWhenPlayerBecameEnemy: false),
                map);

            for (int index = 0; index < pawnCount; index++)
            {
                PawnKindDef kind = guardKind != null
                        && (warriorKind == null || index % 3 == 2)
                    ? guardKind
                    : warriorKind ?? guardKind;
                Pawn pawn = PawnGenerator.GeneratePawn(kind, faction, map.Tile);

                if (pawn == null)
                {
                    continue;
                }

                IntVec3 spawnCell = CellFinder.RandomClosewalkCellNear(
                    rootCell,
                    map,
                    8);
                Pawn spawnedPawn = GenSpawn.Spawn(
                    pawn,
                    spawnCell.IsValid ? spawnCell : rootCell,
                    map) as Pawn;

                if (spawnedPawn == null)
                {
                    continue;
                }

                lord.AddPawn(spawnedPawn);
                spawnedPawns.Add(spawnedPawn);
            }

            return spawnedPawns;
        }

        private static IntVec3 FindSceneAnchor(Map map)
        {
            if (map == null)
            {
                return IntVec3.Invalid;
            }

            if (CellFinder.TryFindRandomCellNear(
                    map.Center,
                    map,
                    SceneSearchRadius,
                    cell => cell.InBounds(map)
                        && cell.x >= SceneClearance
                        && cell.z >= SceneClearance
                        && cell.x < map.Size.x - SceneClearance
                        && cell.z < map.Size.z - SceneClearance
                        && cell.Standable(map)
                        && cell.GetFirstBuilding(map) == null,
                    out IntVec3 anchor))
            {
                return anchor;
            }

            return map.Center;
        }

        private static IntVec3 FindPreferredEntryCell(
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
    }
}
