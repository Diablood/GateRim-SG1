using System;
using System.Collections.Generic;
using System.Linq;
using GateRimSG1.Jaffa;
using GateRimSG1.Missions;
using RimWorld;
using RimWorld.Planet;
using Verse;
using Verse.AI.Group;

namespace GateRimSG1.Goauld
{
    internal static class TokraJaffaOfficerCaptureMissionUtility
    {
        public const string WorldObjectIdCounterKey
            = "jaffaOfficerCaptureWorldObjectId";
        public const string CaptureToolIssuedCounterKey
            = "jaffaOfficerCaptureToolIssued";
        public const int CheckIntervalTicks = 250;
        public const int CaptureStateCheckIntervalTicks = 30;
        public const int TargetLossGraceTicks = 2500;

        private const int DefenderAssaultDelayTicks = 5000;

        public static bool CanCreateWorldSite(
            Map map,
            GateRimMissionCaptureDef profile)
        {
            if (map == null
                || profile == null
                || map.Tile == PlanetTile.Invalid
                || Find.WorldObjects == null)
            {
                return false;
            }

            return ResolveWorldObjectDef(profile) != null
                && ResolvePawnKind(profile.targetPawnKindDefName) != null
                && ResolvePawnKind(profile.escortWarriorPawnKindDefName)
                    != null
                && ResolveThingDef(profile.captureToolThingDefName) != null
                && GoauldSystemLordFactionUtility.GetOrCreateFaction(
                    "Tok'ra Jaffa-officer capture offerability") != null;
        }

        public static bool TryCreateWorldSite(
            Map map,
            TokraOrganicOperationDefinition definition,
            GateRimMissionRuntimeData runtime,
            out WorldObject_TokraJaffaOfficerCaptureSite site)
        {
            site = null;
            GateRimMissionCaptureDef profile = definition?.Capture;

            if (!CanCreateWorldSite(map, profile) || runtime == null)
            {
                return false;
            }

            if (!TileFinder.TryFindNewSiteTile(
                    out PlanetTile tile,
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

            WorldObject_TokraJaffaOfficerCaptureSite createdSite
                = WorldObjectMaker.MakeWorldObject(
                    ResolveWorldObjectDef(profile))
                    as WorldObject_TokraJaffaOfficerCaptureSite;
            Faction goauldFaction
                = GoauldSystemLordFactionUtility.GetOrCreateFaction(
                    "Tok'ra Jaffa-officer capture site");

            if (createdSite == null || goauldFaction == null)
            {
                return false;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            int expiryTick = currentTick + Math.Max(
                1,
                definition.DeadlineTicks);

            createdSite.Tile = tile;
            createdSite.SetFaction(goauldFaction);
            createdSite.Initialize(
                map.uniqueID,
                definition.MissionDefName,
                runtime.scaledThreatPoints,
                expiryTick,
                Math.Max(80, profile.mapSize));

            Find.WorldObjects.Add(createdSite);
            SetRuntimeCounter(runtime, WorldObjectIdCounterKey, createdSite.ID);
            site = createdSite;

            GR_Log.Message(
                "Created Tok'ra Jaffa-officer capture site at tile "
                + $"{tile}; threat snapshot {runtime.scaledThreatPoints:0}; "
                + $"expiry {expiryTick}.");

            return true;
        }

        public static WorldObject_TokraJaffaOfficerCaptureSite FindWorldSite(
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
                .OfType<WorldObject_TokraJaffaOfficerCaptureSite>()
                .FirstOrDefault(worldObject => worldObject != null
                    && !worldObject.Destroyed
                    && worldObject.ID == worldObjectId);
        }

        public static bool TryIssueCaptureTool(
            Map map,
            Pawn operatorPawn,
            GateRimMissionCaptureDef profile,
            GateRimMissionRuntimeData runtime)
        {
            if (map == null
                || profile == null
                || runtime == null
                || GetRuntimeCounter(
                    runtime,
                    CaptureToolIssuedCounterKey,
                    0) > 0)
            {
                return false;
            }

            ThingDef toolDef = ResolveThingDef(profile.captureToolThingDefName);

            if (toolDef == null)
            {
                return false;
            }

            Thing tool = ThingMaker.MakeThing(toolDef);
            Thing placedTool;

            if (!TokraDeliveryDropUtility
                .TryPlaceThingNearPreferredDeliveryCell(
                    tool,
                    map,
                    null,
                    out placedTool))
            {
                tool.Destroy(DestroyMode.Vanish);
                return false;
            }

            SetRuntimeCounter(runtime, CaptureToolIssuedCounterKey, 1);

            GR_Log.Message(
                "Issued Tok'ra Jaffa-officer capture rifle at "
                + $"{placedTool?.Position.ToString() ?? "unknown"}; "
                + $"operator {operatorPawn?.LabelShortCap ?? "unknown"}.");
            return true;
        }

        public static void EnsureMissionMapInitialized(
            Map map,
            WorldObject_TokraJaffaOfficerCaptureSite parent)
        {
            if (map == null || parent == null)
            {
                return;
            }

            MapComponent_TokraJaffaOfficerCaptureMission component
                = map.GetComponent<
                    MapComponent_TokraJaffaOfficerCaptureMission>();

            if (component == null || component.Initialized)
            {
                return;
            }

            TokraOrganicOperationDefinition definition
                = TokraOrganicOperationFramework.GetDefinition(
                    TokraOrganicOperationArchetype.JaffaOfficerCapture);
            GateRimMissionCaptureDef profile = definition?.Capture;
            Faction faction = GoauldSystemLordFactionUtility.GetOrCreateFaction(
                "Tok'ra Jaffa-officer capture map");

            if (definition == null || profile == null || faction == null)
            {
                component.MarkInitializationFailed(parent);
                return;
            }

            IntVec3 anchor = FindEncounterAnchor(map);
            Pawn target = SpawnTargetOfficer(
                map,
                profile,
                faction,
                anchor);
            List<Pawn> escorts = SpawnEscort(
                map,
                profile,
                faction,
                parent.ScaledThreatPoints,
                anchor,
                target);

            if (target == null || escorts.Count == 0)
            {
                if (target != null && !target.Destroyed)
                {
                    target.Destroy(DestroyMode.Vanish);
                }

                foreach (Pawn escort in escorts)
                {
                    if (escort != null && !escort.Destroyed)
                    {
                        escort.Destroy(DestroyMode.Vanish);
                    }
                }

                component.MarkInitializationFailed(parent);
                return;
            }

            parent.RegisterTarget(target);
            component.Initialize(parent, target, escorts);

            Find.LetterStack?.ReceiveLetter(
                "GR_TokraJaffaOfficerCapture_ArrivalLabel".Translate(),
                "GR_TokraJaffaOfficerCapture_ArrivalText".Translate(
                    target.LabelShortCap,
                    escorts.Count.ToString()),
                LetterDefOf.ThreatBig,
                target);

            GR_Log.Message(
                "Initialized Tok'ra Jaffa-officer capture map "
                + $"{map.uniqueID}; target {target.LabelShortCap}; "
                + $"escorts {escorts.Count}; anchor {anchor}.");
        }

        public static bool HasActiveHostiles(Map map)
        {
            return TokraRelaySabotageMissionUtility.HasActiveHostiles(map);
        }

        public static Caravan FindPlayerCaravanContaining(Pawn pawn)
        {
            if (pawn == null || Find.WorldObjects == null)
            {
                return null;
            }

            return Find.WorldObjects.AllWorldObjects
                .OfType<Caravan>()
                .FirstOrDefault(caravan => caravan != null
                    && !caravan.Destroyed
                    && caravan.Faction == Faction.OfPlayer
                    && caravan.PawnsListForReading.Contains(pawn));
        }

        public static bool IsTargetSecured(Pawn pawn)
        {
            return pawn != null
                && !pawn.Dead
                && !pawn.Destroyed
                && pawn.IsPrisonerOfColony;
        }

        public static bool IsTargetReadyForCaravanExtraction(
            Pawn pawn,
            Map map)
        {
            if (pawn == null
                || pawn.Dead
                || pawn.Destroyed
                || map == null)
            {
                return false;
            }

            if (pawn.IsPrisonerOfColony
                || IsTargetBeingCarriedByPlayerPawn(pawn, map))
            {
                return true;
            }

            return pawn.Spawned
                && pawn.Map == map
                && pawn.Downed;
        }

        public static bool IsTargetBeingCarriedByPlayerPawn(
            Pawn pawn,
            Map map)
        {
            if (pawn == null || map?.mapPawns == null)
            {
                return false;
            }

            IReadOnlyList<Pawn> spawnedPawns = map.mapPawns.AllPawnsSpawned;

            for (int index = 0; index < spawnedPawns.Count; index++)
            {
                Pawn carrier = spawnedPawns[index];

                if (carrier?.Faction == Faction.OfPlayer
                    && carrier.carryTracker?.CarriedThing == pawn)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool EnsureTargetTransferRestraint(Pawn pawn)
        {
            GateRimMissionCaptureDef profile = GetCaptureProfile();
            HediffDef restraintDef = ResolveHediffDef(
                profile?.restraintHediffDefName);

            if (pawn?.health?.hediffSet == null
                || pawn.Dead
                || pawn.Destroyed
                || restraintDef == null)
            {
                return false;
            }

            if (pawn.health.hediffSet.GetFirstHediffOfDef(restraintDef) != null)
            {
                return true;
            }

            return pawn.health.AddHediff(restraintDef) != null;
        }

        public static void ClearTargetTransferRestraint(Pawn pawn)
        {
            GateRimMissionCaptureDef profile = GetCaptureProfile();
            HediffDef restraintDef = ResolveHediffDef(
                profile?.restraintHediffDefName);

            if (pawn?.health?.hediffSet == null || restraintDef == null)
            {
                return;
            }

            Hediff restraint = pawn.health.hediffSet.GetFirstHediffOfDef(
                restraintDef);

            if (restraint != null)
            {
                pawn.health.RemoveHediff(restraint);
            }
        }

        public static int GetExtractionMinimumDelayTicks()
        {
            return Math.Max(1,
                GetCaptureProfile()?.extractionMinimumDelayTicks ?? 10000);
        }

        public static int GetExtractionMaximumDelayTicks()
        {
            return Math.Max(
                GetExtractionMinimumDelayTicks(),
                GetCaptureProfile()?.extractionMaximumDelayTicks ?? 30000);
        }

        public static int GetExtractionRetryTicks()
        {
            return Math.Max(250,
                GetCaptureProfile()?.extractionRetryTicks ?? 1200);
        }

        public static int GetExtractionTeamMinimumCount()
        {
            return Math.Max(1,
                GetCaptureProfile()?.extractionTeamMinimumCount ?? 2);
        }

        public static int GetExtractionTeamMaximumCount()
        {
            return Math.Max(
                GetExtractionTeamMinimumCount(),
                GetCaptureProfile()?.extractionTeamMaximumCount ?? 3);
        }

        public static PawnKindDef GetExtractionPawnKind()
        {
            return ResolvePawnKind(
                GetCaptureProfile()?.extractionPawnKindDefName);
        }

        public static Map FindPlayerHomeMapById(int mapId)
        {
            if (mapId < 0 || Find.Maps == null)
            {
                return null;
            }

            return Find.Maps.FirstOrDefault(map => map != null
                && map.uniqueID == mapId
                && map.IsPlayerHome);
        }

        public static Map FindPlayerHomeMapContaining(Pawn pawn)
        {
            if (pawn == null
                || pawn.Dead
                || pawn.Destroyed
                || Find.Maps == null)
            {
                return null;
            }

            return Find.Maps.FirstOrDefault(map => map?.IsPlayerHome == true
                && IsPawnPresentOnMapIncludingCarried(pawn, map));
        }

        public static bool IsPawnPresentOnPlayerHomeMap(Pawn pawn)
        {
            if (pawn == null
                || pawn.Dead
                || pawn.Destroyed
                || Find.Maps == null)
            {
                return false;
            }

            IReadOnlyList<Map> maps = Find.Maps;

            for (int index = 0; index < maps.Count; index++)
            {
                Map map = maps[index];

                if (map?.IsPlayerHome == true
                    && IsPawnPresentOnMapIncludingCarried(pawn, map))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsPawnPresentOnMapIncludingCarried(
            Pawn pawn,
            Map map)
        {
            if (pawn == null || map == null)
            {
                return false;
            }

            if (pawn.Spawned && pawn.Map == map)
            {
                return true;
            }

            IReadOnlyList<Pawn> spawnedPawns
                = map.mapPawns?.AllPawnsSpawned;

            if (spawnedPawns == null)
            {
                return false;
            }

            for (int index = 0; index < spawnedPawns.Count; index++)
            {
                if (spawnedPawns[index]?.carryTracker?.CarriedThing == pawn)
                {
                    return true;
                }
            }

            return false;
        }

        public static void SetRuntimeCounter(
            GateRimMissionRuntimeData runtime,
            string key,
            int value)
        {
            if (runtime?.counters != null && !string.IsNullOrEmpty(key))
            {
                runtime.counters[key] = value;
            }
        }

        public static int GetRuntimeCounter(
            GateRimMissionRuntimeData runtime,
            string key,
            int fallback)
        {
            return runtime?.counters != null
                    && !string.IsNullOrEmpty(key)
                    && runtime.counters.TryGetValue(key, out int value)
                ? value
                : fallback;
        }

        private static IntVec3 FindEncounterAnchor(Map map)
        {
            IntVec3 anchor = map.Center;

            if (CellFinder.TryFindRandomCellNear(
                    map.Center,
                    map,
                    12,
                    cell => cell.Standable(map)
                        && cell.GetFirstBuilding(map) == null
                        && cell.GetFirstPawn(map) == null,
                    out IntVec3 result))
            {
                anchor = result;
            }

            return anchor;
        }

        private static Pawn SpawnTargetOfficer(
            Map map,
            GateRimMissionCaptureDef profile,
            Faction faction,
            IntVec3 anchor)
        {
            PawnKindDef kind = ResolvePawnKind(profile.targetPawnKindDefName);

            if (kind == null)
            {
                return null;
            }

            Pawn pawn = PawnGenerator.GeneratePawn(kind, faction, map.Tile);

            if (!EnsureOfficerCommandApparel(pawn))
            {
                if (pawn != null && !pawn.Destroyed)
                {
                    pawn.Destroy(DestroyMode.Vanish);
                }

                return null;
            }

            IntVec3 spawnCell = CellFinder.RandomClosewalkCellNear(
                anchor,
                map,
                5);
            Pawn spawned = GenSpawn.Spawn(
                pawn,
                spawnCell.IsValid ? spawnCell : anchor,
                map) as Pawn;

            if (spawned != null)
            {
                JaffaForeheadMarkUtility.SetMarkForRank(
                    spawned,
                    faction,
                    GoauldJaffaMarkRank.Elite);
            }

            return spawned;
        }

        private static bool EnsureOfficerCommandApparel(Pawn pawn)
        {
            if (pawn?.apparel == null)
            {
                GR_Log.Warning(
                    "Cannot equip generated Jaffa officer: apparel tracker "
                    + "is unavailable.");
                return false;
            }

            bool armorEquipped = EnsureWornApparel(
                pawn,
                GR_DefOf.SG1_JaffaOfficerArmor);
            bool helmetEquipped = EnsureWornApparel(
                pawn,
                GR_DefOf.SG1_JaffaOfficerDeployedHelmet);

            if (!armorEquipped || !helmetEquipped)
            {
                GR_Log.Warning(
                    "Could not complete the distinctive Jaffa officer "
                    + $"loadout for {pawn.LabelShortCap}; "
                    + $"armor={armorEquipped}, helmet={helmetEquipped}.");
                return false;
            }

            GR_Log.Message(
                "Verified distinctive Jaffa officer command apparel for "
                + pawn.LabelShortCap + ".");
            return true;
        }

        private static bool EnsureWornApparel(
            Pawn pawn,
            ThingDef apparelDef)
        {
            if (pawn?.apparel == null || apparelDef == null)
            {
                return false;
            }

            if (pawn.apparel.WornApparel.Any(
                    apparel => apparel?.def == apparelDef))
            {
                return true;
            }

            Apparel apparel = ThingMaker.MakeThing(apparelDef) as Apparel;

            if (apparel == null)
            {
                GR_Log.Warning(
                    "Cannot equip generated Jaffa officer: "
                    + $"{apparelDef.defName} is not apparel.");
                return false;
            }

            apparel.TryGetComp<CompQuality>()?.SetQuality(
                QualityCategory.Normal,
                ArtGenerationContext.Outsider);
            pawn.apparel.Wear(
                apparel,
                dropReplacedApparel: false);

            if (pawn.apparel.WornApparel.Contains(apparel))
            {
                return true;
            }

            if (!apparel.Destroyed)
            {
                apparel.Destroy(DestroyMode.Vanish);
            }

            GR_Log.Warning(
                "Cannot equip generated Jaffa officer with "
                + apparelDef.defName + ".");
            return false;
        }

        private static List<Pawn> SpawnEscort(
            Map map,
            GateRimMissionCaptureDef profile,
            Faction faction,
            float scaledThreatPoints,
            IntVec3 anchor,
            Pawn targetOfficer)
        {
            List<Pawn> spawned = new List<Pawn>();
            PawnKindDef warriorKind = ResolvePawnKind(
                profile.escortWarriorPawnKindDefName);
            PawnKindDef guardKind = ResolvePawnKind(
                profile.escortGuardPawnKindDefName);
            PawnKindDef referenceKind = warriorKind ?? guardKind;

            if (referenceKind == null)
            {
                return spawned;
            }

            float requestedPoints = Math.Max(
                referenceKind.combatPower,
                scaledThreatPoints
                    * Math.Max(0.05f, profile.escortThreatFactor));
            float combatPower = Math.Max(1f, referenceKind.combatPower);
            int minimum = Math.Max(1, profile.escortMinimumCount);
            int count = Math.Max(
                minimum,
                (int)Math.Ceiling(requestedPoints / combatPower));
            Lord lord = LordMaker.MakeNewLord(
                faction,
                new LordJob_DefendBase(
                    faction,
                    anchor,
                    DefenderAssaultDelayTicks,
                    attackWhenPlayerBecameEnemy: false),
                map);

            if (targetOfficer != null && targetOfficer.Spawned)
            {
                lord.AddPawn(targetOfficer);
            }

            for (int index = 0; index < count; index++)
            {
                PawnKindDef kind = guardKind != null
                        && (warriorKind == null || index % 5 == 4)
                    ? guardKind
                    : warriorKind ?? guardKind;
                Pawn pawn = PawnGenerator.GeneratePawn(kind, faction, map.Tile);
                IntVec3 spawnCell = CellFinder.RandomClosewalkCellNear(
                    anchor,
                    map,
                    8);
                Pawn spawnedPawn = GenSpawn.Spawn(
                    pawn,
                    spawnCell.IsValid ? spawnCell : anchor,
                    map) as Pawn;

                if (spawnedPawn == null)
                {
                    continue;
                }

                lord.AddPawn(spawnedPawn);
                spawned.Add(spawnedPawn);
            }

            return spawned;
        }

        private static WorldObjectDef ResolveWorldObjectDef(
            GateRimMissionCaptureDef profile)
        {
            return string.IsNullOrWhiteSpace(profile?.worldObjectDefName)
                ? null
                : DefDatabase<WorldObjectDef>.GetNamedSilentFail(
                    profile.worldObjectDefName);
        }

        private static PawnKindDef ResolvePawnKind(string defName)
        {
            return string.IsNullOrWhiteSpace(defName)
                ? null
                : DefDatabase<PawnKindDef>.GetNamedSilentFail(defName);
        }

        private static ThingDef ResolveThingDef(string defName)
        {
            return string.IsNullOrWhiteSpace(defName)
                ? null
                : DefDatabase<ThingDef>.GetNamedSilentFail(defName);
        }

        private static HediffDef ResolveHediffDef(string defName)
        {
            return string.IsNullOrWhiteSpace(defName)
                ? null
                : DefDatabase<HediffDef>.GetNamedSilentFail(defName);
        }

        private static GateRimMissionCaptureDef GetCaptureProfile()
        {
            return TokraOrganicOperationFramework.GetDefinition(
                    TokraOrganicOperationArchetype.JaffaOfficerCapture)
                ?.Capture;
        }
    }
}
