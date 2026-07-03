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
    internal static class TokraDistressCallMissionUtility
    {
        public const string WorldObjectIdCounterKey = "distressWorldObjectId";
        public const string VariantCounterKey = "distressVariant";
        public const string ForcedVariantCounterKey = "distressForcedVariant";

        private const int StateCheckIntervalTicks = 250;
        private const int DefenderAssaultDelayTicks = 5000;

        public static int CheckIntervalTicks => StateCheckIntervalTicks;

        public static bool CanCreateWorldSite(
            Map map,
            GateRimMissionDistressCallDef profile)
        {
            if (map == null
                || profile == null
                || map.Tile == PlanetTile.Invalid
                || Find.WorldObjects == null)
            {
                return false;
            }

            WorldObjectDef worldObjectDef = ResolveWorldObjectDef(profile);
            PawnKindDef survivorKind = ResolveSurvivorKind(profile);

            return worldObjectDef != null
                && survivorKind != null
                && TokraFactionUtility.GetOrCreatePersistentFaction(
                    "distress-call offerability") != null;
        }

        public static bool TryCreateWorldSite(
            Map map,
            TokraOrganicOperationDefinition definition,
            GateRimMissionRuntimeData runtime,
            out WorldObject_TokraDistressCallSite site)
        {
            site = null;
            GateRimMissionDistressCallDef profile
                = definition?.MissionDef?.distressCall;

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
            Faction tokraFaction
                = TokraFactionUtility.GetOrCreatePersistentFaction(
                    "Tok'ra distress-call world site");
            WorldObject_TokraDistressCallSite createdSite
                = WorldObjectMaker.MakeWorldObject(worldObjectDef)
                    as WorldObject_TokraDistressCallSite;

            if (createdSite == null || tokraFaction == null)
            {
                return false;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            int forcedVariant = GetRuntimeCounter(
                runtime,
                ForcedVariantCounterKey,
                0);
            TokraDistressCallVariant variant = forcedVariant > 0
                ? (TokraDistressCallVariant)forcedVariant
                : SelectVariant(profile);
            int deadlineTicks = Math.Max(
                1,
                definition.DeadlineTicks);
            int lateArrivalTick = currentTick + Math.Max(
                1,
                Math.Min(profile.lateArrivalTicks, deadlineTicks - 1));
            int expiryTick = currentTick + deadlineTicks;

            createdSite.Tile = tile;
            createdSite.SetFaction(tokraFaction);
            createdSite.Initialize(
                map.uniqueID,
                definition.MissionDefName,
                variant,
                runtime.scaledThreatPoints,
                lateArrivalTick,
                expiryTick,
                Math.Max(80, profile.mapSize));

            Find.WorldObjects.Add(createdSite);
            SetRuntimeCounter(runtime, WorldObjectIdCounterKey, createdSite.ID);
            SetRuntimeCounter(runtime, VariantCounterKey, (int)variant);

            site = createdSite;

            GR_Log.Message(
                "Created Tok'ra distress-call world site at tile "
                + $"{tile}; planned variant {variant}; threat snapshot "
                + $"{runtime.scaledThreatPoints:0}; late threshold "
                + $"{lateArrivalTick}; expiry {expiryTick}.");

            return true;
        }

        public static WorldObject_TokraDistressCallSite FindWorldSite(
            GateRimMissionRuntimeData runtime)
        {
            int worldObjectId = GetRuntimeCounter(
                runtime,
                WorldObjectIdCounterKey,
                -1);

            if (worldObjectId < 0 || Find.WorldObjects == null)
            {
                return null;
            }

            return Find.WorldObjects.AllWorldObjects
                .OfType<WorldObject_TokraDistressCallSite>()
                .FirstOrDefault(worldObject =>
                    worldObject != null
                    && !worldObject.Destroyed
                    && worldObject.ID == worldObjectId);
        }

        public static void EnsureMissionMapInitialized(
            Map map,
            WorldObject_TokraDistressCallSite parent)
        {
            if (map == null || parent == null)
            {
                return;
            }

            MapComponent_TokraDistressCallMission component
                = map.GetComponent<MapComponent_TokraDistressCallMission>();

            if (component == null || component.Initialized)
            {
                return;
            }

            TokraOrganicOperationDefinition definition
                = TokraOrganicOperationFramework.GetDefinition(
                    TokraOrganicOperationArchetype.DistressCall);
            GateRimMissionDistressCallDef profile
                = definition?.MissionDef?.distressCall;

            if (definition == null || profile == null)
            {
                component.MarkInitializationFailed(parent);
                return;
            }

            TokraDistressCallVariant variant = parent.GetArrivalVariant();
            TokraDistressCallSceneResult scene
                = TokraDistressCallSceneUtility.Generate(
                    map,
                    profile,
                    variant);
            List<Pawn> survivors = new List<Pawn>();
            List<Pawn> hostiles = new List<Pawn>();
            Thing salvage = null;

            if (scene == null || !scene.Anchor.IsValid)
            {
                GR_Log.Error(
                    "Cannot initialize Tok'ra distress-call site: no "
                    + "coherent scene anchor could be generated.");
                component.MarkInitializationFailed(parent);
                return;
            }

            if (variant == TokraDistressCallVariant.GenuineRescue)
            {
                survivors = SpawnSurvivors(
                    map,
                    definition,
                    parent.ScaledThreatPoints,
                    parent.RemainingTicks,
                    scene.SurvivorAnchor);
            }

            float threatFactor = GetThreatFactor(profile, variant);
            hostiles = SpawnJaffaGroup(
                map,
                parent.ScaledThreatPoints * threatFactor,
                profile,
                scene.DefenderAnchor,
                variant == TokraDistressCallVariant.CompromisedSignal);

            if (variant == TokraDistressCallVariant.LateArrival)
            {
                salvage = SpawnLateArrivalSalvage(
                    map,
                    profile,
                    scene.SalvageAnchor);
            }

            if (hostiles.Count == 0)
            {
                foreach (Pawn survivor in survivors)
                {
                    TokraOrganicWoundedAgentUtility.RemoveLivingPatient(
                        survivor);
                }

                if (salvage != null && !salvage.Destroyed)
                {
                    salvage.Destroy(DestroyMode.Vanish);
                }

                GR_Log.Error(
                    "Cannot initialize Tok'ra distress-call site: no "
                    + "Goa'uld/Jaffa defender could be generated.");
                component.MarkInitializationFailed(parent);
                return;
            }

            component.Initialize(
                parent,
                variant,
                survivors,
                hostiles,
                salvage,
                scene.PreferredEntryCell);

            SendArrivalLetter(parent, variant, survivors, hostiles);

            GR_Log.Message(
                "Initialized Tok'ra distress-call mission map "
                + $"{map.uniqueID}; variant {variant}; scene "
                + $"{scene.SceneType}; anchor {scene.Anchor}; survivors "
                + $"{survivors.Count}; hostiles {hostiles.Count}; "
                + $"Tok'ra corpses {scene.TokraCorpseCount}; Jaffa "
                + $"corpses {scene.JaffaCorpseCount}; "
                + $"salvage={(salvage != null)}.");
        }

        public static bool HasActiveHostiles(Map map)
        {
            return TokraRelaySabotageMissionUtility.HasActiveHostiles(map);
        }

        public static void SetRuntimeCounter(
            GateRimMissionRuntimeData runtime,
            string key,
            int value)
        {
            if (runtime?.counters == null || string.IsNullOrEmpty(key))
            {
                return;
            }

            runtime.counters[key] = value;
        }

        public static int GetRuntimeCounter(
            GateRimMissionRuntimeData runtime,
            string key,
            int fallback)
        {
            int value;

            return runtime?.counters != null
                    && !string.IsNullOrEmpty(key)
                    && runtime.counters.TryGetValue(key, out value)
                ? value
                : fallback;
        }

        private static WorldObjectDef ResolveWorldObjectDef(
            GateRimMissionDistressCallDef profile)
        {
            if (GR_DefOf.SG1_TokraDistressCallWorldSite != null)
            {
                return GR_DefOf.SG1_TokraDistressCallWorldSite;
            }

            return string.IsNullOrWhiteSpace(profile?.worldObjectDefName)
                ? null
                : DefDatabase<WorldObjectDef>.GetNamedSilentFail(
                    profile.worldObjectDefName);
        }

        private static PawnKindDef ResolveSurvivorKind(
            GateRimMissionDistressCallDef profile)
        {
            return string.IsNullOrWhiteSpace(profile?.survivorPawnKindDefName)
                ? null
                : DefDatabase<PawnKindDef>.GetNamedSilentFail(
                    profile.survivorPawnKindDefName);
        }

        private static TokraDistressCallVariant SelectVariant(
            GateRimMissionDistressCallDef profile)
        {
            float rescueWeight = Math.Max(0f, profile.genuineRescueWeight);
            float trapWeight = Math.Max(0f, profile.compromisedSignalWeight);
            float lateWeight = Math.Max(0f, profile.lateArrivalWeight);
            float totalWeight = rescueWeight + trapWeight + lateWeight;

            if (totalWeight <= 0f)
            {
                return TokraDistressCallVariant.GenuineRescue;
            }

            float roll = Rand.Value * totalWeight;

            if ((roll -= rescueWeight) <= 0f)
            {
                return TokraDistressCallVariant.GenuineRescue;
            }

            if ((roll -= trapWeight) <= 0f)
            {
                return TokraDistressCallVariant.CompromisedSignal;
            }

            return TokraDistressCallVariant.LateArrival;
        }

        private static List<Pawn> SpawnSurvivors(
            Map map,
            TokraOrganicOperationDefinition definition,
            float scaledThreatPoints,
            int remainingTicks,
            IntVec3 sceneAnchor)
        {
            List<Pawn> survivors = new List<Pawn>();
            GateRimMissionDistressCallDef profile
                = definition.MissionDef.distressCall;
            int minimum = Math.Max(1, profile.survivorMinimumCount);
            int maximum = Math.Max(minimum, profile.survivorMaximumCount);
            int count = Rand.RangeInclusive(minimum, maximum);

            for (int index = 0; index < count; index++)
            {
                Pawn survivor;

                if (!TokraOrganicWoundedAgentUtility.TrySpawnPatient(
                        map,
                        definition,
                        scaledThreatPoints,
                        Math.Max(remainingTicks, 60000),
                        sceneAnchor,
                        out survivor))
                {
                    continue;
                }

                survivors.Add(survivor);
            }

            return survivors;
        }

        private static List<Pawn> SpawnJaffaGroup(
            Map map,
            float requestedPoints,
            GateRimMissionDistressCallDef profile,
            IntVec3 sceneAnchor,
            bool assaultImmediately)
        {
            List<Pawn> spawnedPawns = new List<Pawn>();
            Faction faction = GoauldSystemLordFactionUtility.GetOrCreateFaction(
                "Tok'ra distress-call mission");
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
            int pawnCount = Math.Max(
                minimum,
                (int)Math.Ceiling(
                    Math.Max(combatPower, requestedPoints) / combatPower));
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

            LordJob lordJob = assaultImmediately
                ? (LordJob)new LordJob_AssaultColony(faction)
                : new LordJob_DefendBase(
                    faction,
                    rootCell,
                    DefenderAssaultDelayTicks,
                    attackWhenPlayerBecameEnemy: false);
            Lord lord = LordMaker.MakeNewLord(faction, lordJob, map);

            for (int index = 0; index < pawnCount; index++)
            {
                PawnKindDef kind = guardKind != null
                        && (warriorKind == null || index % 5 == 4)
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

        private static Thing SpawnLateArrivalSalvage(
            Map map,
            GateRimMissionDistressCallDef profile,
            IntVec3 sceneAnchor)
        {
            ThingDef salvageDef = string.IsNullOrWhiteSpace(
                    profile.salvageThingDefName)
                ? ThingDefOf.ComponentIndustrial
                : DefDatabase<ThingDef>.GetNamedSilentFail(
                    profile.salvageThingDefName);
            ThingDef containerDef = string.IsNullOrWhiteSpace(
                    profile.salvageContainerDefName)
                ? DefDatabase<ThingDef>.GetNamedSilentFail("Shelf")
                : DefDatabase<ThingDef>.GetNamedSilentFail(
                    profile.salvageContainerDefName);

            if (map == null
                || salvageDef == null
                || containerDef == null
                || containerDef.thingClass == null
                || !typeof(Building_Storage).IsAssignableFrom(
                    containerDef.thingClass))
            {
                return null;
            }

            IntVec3 anchor = sceneAnchor.IsValid
                ? sceneAnchor
                : map.Center;

            if (!TryFindSalvageContainerCell(
                    map,
                    anchor,
                    containerDef,
                    out IntVec3 containerCell))
            {
                GR_Log.Warning(
                    "Could not place the Tok'ra distress-call salvage "
                    + "container near the generated scene.");
                return null;
            }

            ThingDef stuff = null;

            if (containerDef.MadeFromStuff)
            {
                stuff = DefDatabase<ThingDef>.GetNamedSilentFail("Steel")
                    ?? DefDatabase<ThingDef>.GetNamedSilentFail("WoodLog");

                if (stuff == null)
                {
                    return null;
                }
            }

            Thing container = ThingMaker.MakeThing(containerDef, stuff);
            Thing spawnedContainer = GenSpawn.Spawn(
                container,
                containerCell,
                map,
                Rot4.North);

            if (spawnedContainer == null)
            {
                return null;
            }

            IntVec3 storageCell = IntVec3.Invalid;
            CellRect occupiedRect = GenAdj.OccupiedRect(
                spawnedContainer.Position,
                spawnedContainer.Rotation,
                spawnedContainer.def.Size);

            foreach (IntVec3 cell in occupiedRect.Cells)
            {
                if (cell.InBounds(map))
                {
                    storageCell = cell;
                    break;
                }
            }

            if (!storageCell.IsValid)
            {
                spawnedContainer.Destroy(DestroyMode.Vanish);
                return null;
            }

            Thing salvage = ThingMaker.MakeThing(salvageDef);
            int minimum = Math.Max(1, profile.salvageMinimumCount);
            int maximum = Math.Max(minimum, profile.salvageMaximumCount);
            salvage.stackCount = Math.Min(
                salvageDef.stackLimit,
                Rand.RangeInclusive(minimum, maximum));

            Thing spawnedSalvage = GenSpawn.Spawn(
                salvage,
                storageCell,
                map);

            if (spawnedSalvage == null)
            {
                spawnedContainer.Destroy(DestroyMode.Vanish);
                return null;
            }

            return spawnedSalvage;
        }

        private static bool TryFindSalvageContainerCell(
            Map map,
            IntVec3 anchor,
            ThingDef containerDef,
            out IntVec3 result)
        {
            result = IntVec3.Invalid;

            if (map == null || containerDef == null)
            {
                return false;
            }

            foreach (IntVec3 candidate in GenRadial.RadialCellsAround(
                anchor,
                7f,
                true))
            {
                CellRect occupiedRect = GenAdj.OccupiedRect(
                    candidate,
                    Rot4.North,
                    containerDef.Size);
                bool valid = true;

                foreach (IntVec3 cell in occupiedRect.Cells)
                {
                    if (!cell.InBounds(map)
                        || !cell.Standable(map)
                        || cell.GetFirstBuilding(map) != null
                        || cell.GetFirstPawn(map) != null)
                    {
                        valid = false;
                        break;
                    }

                    List<Thing> things = cell.GetThingList(map);

                    if (things.Any(thing => thing != null
                        && !thing.Destroyed
                        && thing.def.category != ThingCategory.Filth))
                    {
                        valid = false;
                        break;
                    }
                }

                if (!valid)
                {
                    continue;
                }

                result = candidate;
                return true;
            }

            return false;
        }

        private static float GetThreatFactor(
            GateRimMissionDistressCallDef profile,
            TokraDistressCallVariant variant)
        {
            switch (variant)
            {
                case TokraDistressCallVariant.CompromisedSignal:
                    return Math.Max(0.05f, profile.compromisedSignalThreatFactor);
                case TokraDistressCallVariant.LateArrival:
                    return Math.Max(0.05f, profile.lateArrivalThreatFactor);
                default:
                    return Math.Max(0.05f, profile.genuineRescueThreatFactor);
            }
        }

        private static void SendArrivalLetter(
            WorldObject_TokraDistressCallSite parent,
            TokraDistressCallVariant variant,
            List<Pawn> survivors,
            List<Pawn> hostiles)
        {
            string textKey;
            LetterDef letterDef;
            LookTargets targets;

            switch (variant)
            {
                case TokraDistressCallVariant.CompromisedSignal:
                    textKey = "GR_TokraDistressCall_ArrivalTrapText";
                    letterDef = LetterDefOf.ThreatBig;
                    targets = hostiles.Count > 0
                        ? new LookTargets(hostiles[0])
                        : new LookTargets(parent);
                    break;
                case TokraDistressCallVariant.LateArrival:
                    textKey = "GR_TokraDistressCall_ArrivalLateText";
                    letterDef = LetterDefOf.ThreatSmall;
                    targets = hostiles.Count > 0
                        ? new LookTargets(hostiles[0])
                        : new LookTargets(parent);
                    break;
                default:
                    textKey = "GR_TokraDistressCall_ArrivalRescueText";
                    letterDef = LetterDefOf.ThreatSmall;
                    targets = survivors.Count > 0
                        ? new LookTargets(survivors[0])
                        : new LookTargets(parent);
                    break;
            }

            Find.LetterStack?.ReceiveLetter(
                "GR_TokraDistressCall_ArrivalLabel".Translate(),
                textKey.Translate(
                    survivors.Count.ToString(),
                    hostiles.Count.ToString()),
                letterDef,
                targets);
        }
    }
}
