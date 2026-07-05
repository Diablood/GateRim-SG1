using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI.Group;

namespace GateRimSG1.Goauld
{
    internal static class TokraRelaySabotageMissionUtility
    {
        public const int ReinforcementDelayTicks = 30000;
        private const float DefenderThreatFactor = 0.80f;
        private const float ReinforcementThreatFactor = 0.25f;
        private const float MinimumDefenderPoints = 180f;
        private const float MinimumReinforcementPoints = 100f;
        private const int MinimumDefenderCount = 2;
        private const int DefenderAssaultDelayTicks = 25000;
        private const int MinimumReinforcementCount = 1;
        private const int RetaliationDelayMinTicks = 120000;
        private const int RetaliationDelayMaxTicks = 360000;
        private const int RetaliationRetryTicks = 120000;
        private const float RetaliationThreatFactor = 0.75f;
        private const float MinimumRetaliationPoints = 300f;

        public static void EnsureMissionMapInitialized(
            Map map,
            WorldObject_TokraDecodedMissionSite parent)
        {
            if (map == null)
            {
                return;
            }

            MapComponent_TokraRelaySabotageMission component = GetComponent(map);

            if (component == null || component.Initialized)
            {
                return;
            }

            Faction goauldFaction = GoauldSystemLordFactionUtility
                .GetOrCreateFaction("Tok'ra relay sabotage mission");
            float defenderPoints = GetInitialDefenderPoints(map, parent);
            TokraRelaySabotageSiteLayoutResult layout =
                TokraRelaySabotageSiteLayoutUtility.Generate(
                    map,
                    goauldFaction,
                    defenderPoints);
            Thing relay = TrySpawnRelayDevice(
                map,
                layout?.RelayCell ?? map.Center,
                goauldFaction);

            if (goauldFaction != null)
            {
                List<Pawn> defenders = SpawnJaffaGroup(
                    map,
                    goauldFaction,
                    defenderPoints,
                    layout?.DefenderRootCell ?? map.Center,
                    8,
                    MinimumDefenderCount,
                    assaultImmediately: false);

                if (defenders.Count == 0)
                {
                    GR_Log.Error(
                        "Tok'ra relay mission generated no Goa'uld/Jaffa "
                        + "defenders.");
                }
            }

            component.Initialize(
                parent,
                relay,
                layout?.RewardCell ?? relay?.Position ?? map.Center);

            if (relay != null)
            {
                Find.LetterStack.ReceiveLetter(
                    "GR_TokraRelaySabotageMission_LetterLabel".Translate(),
                    "GR_TokraRelaySabotageMission_LetterText".Translate(),
                    LetterDefOf.ThreatSmall,
                    relay);
            }
            else if (parent != null)
            {
                Find.LetterStack.ReceiveLetter(
                    "GR_TokraRelaySabotageMission_LetterLabel".Translate(),
                    "GR_TokraRelaySabotageMission_LetterText".Translate(),
                    LetterDefOf.ThreatSmall,
                    parent);
            }
            else
            {
                Find.LetterStack.ReceiveLetter(
                    "GR_TokraRelaySabotageMission_LetterLabel".Translate(),
                    "GR_TokraRelaySabotageMission_LetterText".Translate(),
                    LetterDefOf.ThreatSmall);
            }
        }

        public static MapComponent_TokraRelaySabotageMission GetComponent(Map map)
        {
            return map?.GetComponent<MapComponent_TokraRelaySabotageMission>();
        }

        public static bool HasActiveHostiles(Map map)
        {
            if (map?.mapPawns == null)
            {
                return false;
            }

            IReadOnlyList<Pawn> pawns = map.mapPawns.AllPawnsSpawned;

            for (int index = 0; index < pawns.Count; index++)
            {
                Pawn pawn = pawns[index];

                if (pawn != null
                    && !pawn.Dead
                    && !pawn.Downed
                    && pawn.Faction != null
                    && pawn.Faction.HostileTo(Faction.OfPlayer))
                {
                    return true;
                }
            }

            return false;
        }

        public static int ActivateDefendersForAssault(Map map)
        {
            if (map?.mapPawns == null)
            {
                return 0;
            }

            HashSet<Lord> changedLords = new HashSet<Lord>();
            IReadOnlyList<Pawn> pawns = map.mapPawns.AllPawnsSpawned;
            int activatedPawnCount = 0;

            for (int index = 0; index < pawns.Count; index++)
            {
                Pawn pawn = pawns[index];

                if (pawn == null
                    || pawn.Dead
                    || pawn.Faction == null
                    || !pawn.Faction.HostileTo(Faction.OfPlayer))
                {
                    continue;
                }

                Lord lord = pawn.GetLord();

                if (lord == null
                    || !(lord.LordJob is LordJob_DefendBase)
                    || !changedLords.Add(lord))
                {
                    continue;
                }

                activatedPawnCount += lord.ownedPawns.Count;
                lord.SetJob(new LordJob_AssaultColony(lord.faction));
                lord.GotoToil(lord.Graph.StartingToil);
            }

            if (activatedPawnCount > 0)
            {
                GR_Log.Message(
                    $"Activated {activatedPawnCount} Goa'uld/Jaffa relay "
                    + "defender(s) after the infiltration was compromised.");
            }

            return activatedPawnCount;
        }

        public static bool TryQueueRelayDestructionRetaliation()
        {
            IncidentDef retaliationDef =
                GR_DefOf.SG1_GoauldJaffaControlledRaid;
            Map targetMap = Find.AnyPlayerHomeMap;

            if (retaliationDef == null
                || retaliationDef.category == null
                || targetMap == null
                || Find.Storyteller?.incidentQueue == null)
            {
                GR_Log.Warning(
                    "Could not queue the Goa'uld retaliation after the relay "
                    + "was destroyed: incident definition, target colony or "
                    + "storyteller queue is unavailable.");
                return false;
            }

            IncidentParms parms = StorytellerUtility.DefaultParmsNow(
                retaliationDef.category,
                targetMap);
            Faction goauldFaction = GoauldSystemLordFactionUtility
                .GetOrCreateFaction("destroyed Tok'ra relay retaliation");

            parms.forced = true;
            parms.faction = goauldFaction;
            parms.points = Math.Max(
                MinimumRetaliationPoints,
                parms.points * RetaliationThreatFactor);

            int fireTick = Find.TickManager.TicksGame
                + Rand.RangeInclusive(
                    RetaliationDelayMinTicks,
                    RetaliationDelayMaxTicks);

            Find.Storyteller.incidentQueue.Add(
                retaliationDef,
                fireTick,
                parms,
                RetaliationRetryTicks);

            GR_Log.Message(
                "Queued a delayed Goa'uld retaliation after destructive "
                + "failure of the Tok'ra relay operation.");

            return true;
        }

        public static bool TrySpawnReinforcements(Map map)
        {
            if (map == null)
            {
                return false;
            }

            Faction goauldFaction = GoauldSystemLordFactionUtility
                .GetOrCreateFaction("Tok'ra relay sabotage reinforcements");

            if (goauldFaction == null)
            {
                return false;
            }

            IntVec3 entryCell = GetReinforcementEntryCell(map);
            List<Pawn> reinforcements = SpawnJaffaGroup(
                map,
                goauldFaction,
                GetReinforcementPoints(map),
                entryCell,
                6,
                MinimumReinforcementCount,
                assaultImmediately: true);

            if (reinforcements.Count == 0)
            {
                GR_Log.Error(
                    "Tok'ra relay reinforcement generation produced no "
                    + "spawned Jaffa pawns.");
                return false;
            }

            Pawn focusPawn = reinforcements[0];

            Find.LetterStack.ReceiveLetter(
                "GR_TokraRelaySabotageMission_ReinforcementsLetterLabel"
                    .Translate(),
                "GR_TokraRelaySabotageMission_ReinforcementsLetterText"
                    .Translate(),
                LetterDefOf.ThreatSmall,
                focusPawn);

            Messages.Message(
                "GR_TokraRelaySabotageMission_ReinforcementsArrived"
                    .Translate(reinforcements.Count),
                focusPawn,
                MessageTypeDefOf.ThreatSmall,
                historical: true);

            GR_Log.Message(
                $"Spawned {reinforcements.Count} Goa'uld/Jaffa relay "
                + "reinforcement pawn(s).");

            return true;
        }

        public static Thing TryPrepareMissionRewardCache(
            Map map,
            IntVec3 preferredCell)
        {
            if (map == null)
            {
                return null;
            }

            IntVec3 shelfCell = FindRewardSpawnCell(
                map,
                preferredCell,
                0);
            Thing shelf = TrySpawnRewardShelf(map, shelfCell);
            List<IntVec3> slotCells = GetRewardSlotCells(
                map,
                shelf,
                preferredCell);

            if (slotCells.Count == 0)
            {
                GR_Log.Error(
                    "Tok'ra relay sabotage reward cache has no valid "
                    + "storage cell.");
                return null;
            }

            Thing focusThing = null;
            ThingDef weaponDef = ChooseRewardWeaponDef();

            if (weaponDef != null)
            {
                Thing weapon = ThingMaker.MakeThing(weaponDef);
                focusThing = SpawnRewardThing(
                    map,
                    weapon,
                    slotCells[0]);
            }

            if (ThingDefOf.ComponentIndustrial != null)
            {
                Thing components = ThingMaker.MakeThing(
                    ThingDefOf.ComponentIndustrial);
                components.stackCount = Rand.RangeInclusive(1, 2);
                IntVec3 componentCell = slotCells.Count > 1
                    ? slotCells[1]
                    : slotCells[0];
                Thing spawnedComponents = SpawnRewardThing(
                    map,
                    components,
                    componentCell);

                if (focusThing == null)
                {
                    focusThing = spawnedComponents;
                }
            }

            if (focusThing == null)
            {
                GR_Log.Error(
                    "Tok'ra relay sabotage reward cache could not be "
                    + "prepared.");
                return null;
            }

            GR_Log.Message(
                "Prepared accessible Tok'ra relay sabotage salvage on a "
                + "storage-room shelf.");

            return focusThing;
        }

        private static Thing TrySpawnRewardShelf(
            Map map,
            IntVec3 cell)
        {
            ThingDef shelfDef = DefDatabase<ThingDef>
                .GetNamedSilentFail("Shelf");

            if (shelfDef == null)
            {
                GR_Log.Warning(
                    "Vanilla shelf ThingDef was not found; Tok'ra relay "
                    + "salvage will be placed on the storage-room floor.");
                return null;
            }

            if (!cell.IsValid
                || !cell.InBounds(map)
                || !cell.Standable(map)
                || cell.GetEdifice(map) != null)
            {
                GR_Log.Warning(
                    "No valid shelf cell was available for the Tok'ra relay "
                    + "salvage cache.");
                return null;
            }

            ThingDef stuff = shelfDef.MadeFromStuff
                ? ThingDefOf.Steel
                : null;
            Thing shelf = ThingMaker.MakeThing(shelfDef, stuff);
            shelf.Rotation = Rot4.North;

            return GenSpawn.Spawn(shelf, cell, map);
        }

        private static List<IntVec3> GetRewardSlotCells(
            Map map,
            Thing shelf,
            IntVec3 preferredCell)
        {
            List<IntVec3> cells = new List<IntVec3>();

            if (shelf != null && !shelf.Destroyed && shelf.Spawned)
            {
                foreach (IntVec3 cell in shelf.OccupiedRect().Cells)
                {
                    if (cell.InBounds(map))
                    {
                        cells.Add(cell);
                    }
                }
            }

            if (cells.Count > 0)
            {
                return cells;
            }

            IntVec3 firstCell = FindRewardSpawnCell(
                map,
                preferredCell,
                0);

            if (firstCell.IsValid)
            {
                cells.Add(firstCell);
            }

            IntVec3 secondCell = FindRewardSpawnCell(
                map,
                preferredCell,
                1);

            if (secondCell.IsValid && secondCell != firstCell)
            {
                cells.Add(secondCell);
            }

            return cells;
        }

        private static Thing SpawnRewardThing(
            Map map,
            Thing thing,
            IntVec3 cell)
        {
            if (map == null
                || thing == null
                || !cell.IsValid
                || !cell.InBounds(map))
            {
                return null;
            }

            return GenSpawn.Spawn(thing, cell, map);
        }

        private static Thing TrySpawnRelayDevice(
            Map map,
            IntVec3 preferredCell,
            Faction faction)
        {
            if (GR_DefOf.SG1_TokraRelaySabotageDevice == null)
            {
                GR_Log.Error(
                    "Cannot spawn Tok'ra relay sabotage device: missing "
                    + "ThingDef SG1_TokraRelaySabotageDevice.");
                return null;
            }

            IntVec3 cell = preferredCell;

            if (!IsValidRelayCell(map, cell)
                && !TryFindCentralStandableCell(map, out cell))
            {
                GR_Log.Warning(
                    "Unable to find a central cell for the Tok'ra relay "
                    + "sabotage device.");
                return null;
            }

            Thing relay = ThingMaker.MakeThing(
                GR_DefOf.SG1_TokraRelaySabotageDevice);

            if (faction != null)
            {
                relay.SetFaction(faction);
            }

            return GenSpawn.Spawn(relay, cell, map);
        }

        private static bool IsValidRelayCell(Map map, IntVec3 cell)
        {
            return map != null
                && cell.IsValid
                && cell.InBounds(map)
                && cell.Standable(map)
                && cell.GetFirstBuilding(map) == null;
        }

        private static bool TryFindCentralStandableCell(
            Map map,
            out IntVec3 cell)
        {
            return CellFinder.TryFindRandomCellNear(
                map.Center,
                map,
                18,
                candidate => candidate.Standable(map)
                    && candidate.GetFirstBuilding(map) == null,
                out cell);
        }

        private static List<Pawn> SpawnJaffaGroup(
            Map map,
            Faction faction,
            float points,
            IntVec3 rootCell,
            int spawnRadius,
            int minimumCount,
            bool assaultImmediately)
        {
            List<Pawn> spawnedPawns = new List<Pawn>();
            PawnKindDef warriorKind = GR_DefOf.SG1_GoauldJaffaWarrior;
            PawnKindDef guardKind = GR_DefOf.SG1_GoauldJaffaGuard;

            if (warriorKind == null && guardKind == null)
            {
                GR_Log.Error(
                    "Cannot generate Tok'ra relay Jaffa: no Goa'uld Jaffa "
                    + "PawnKindDef is available.");
                return spawnedPawns;
            }

            int pawnCount = CalculatePawnCount(
                points,
                warriorKind ?? guardKind,
                minimumCount);

            if (!rootCell.IsValid || !rootCell.InBounds(map))
            {
                rootCell = map.Center;
            }

            LordJob lordJob = assaultImmediately
                ? (LordJob)new LordJob_AssaultColony(faction)
                : new LordJob_DefendBase(
                    faction,
                    rootCell,
                    DefenderAssaultDelayTicks,
                    attackWhenPlayerBecameEnemy: false);
            Lord lord = LordMaker.MakeNewLord(
                faction,
                lordJob,
                map);

            for (int index = 0; index < pawnCount; index++)
            {
                PawnKindDef pawnKind = GoauldJaffaOfficerForceUtility
                    .SelectMissionPawnKind(
                        index,
                        pawnCount,
                        warriorKind,
                        guardKind);

                Pawn pawn = GoauldJaffaOfficerForceUtility.GenerateForcePawn(
                    pawnKind,
                    faction,
                    map.Tile,
                    "Tok'ra relay Jaffa force",
                    guardKind);

                if (pawn == null)
                {
                    GR_Log.Warning(
                        "One Tok'ra relay Jaffa could not be generated.");
                    continue;
                }

                IntVec3 spawnCell = CellFinder.RandomClosewalkCellNear(
                    rootCell,
                    map,
                    spawnRadius);

                if (!spawnCell.IsValid || !spawnCell.Standable(map))
                {
                    spawnCell = rootCell;
                }

                Pawn spawnedPawn = GenSpawn.Spawn(
                    pawn,
                    spawnCell,
                    map) as Pawn;

                if (spawnedPawn == null)
                {
                    GR_Log.Warning(
                        "One Tok'ra relay Jaffa could not be spawned on the "
                        + "mission map.");
                    continue;
                }

                lord.AddPawn(spawnedPawn);
                spawnedPawns.Add(spawnedPawn);
            }

            return spawnedPawns;
        }

        private static ThingDef ChooseRewardWeaponDef()
        {
            ThingDef zat = GR_DefOf.SG1_ZatnikTel;
            ThingDef matok = GR_DefOf.SG1_MatokStaff;

            if (zat == null)
            {
                return matok;
            }

            if (matok == null)
            {
                return zat;
            }

            return Rand.Chance(0.75f) ? zat : matok;
        }

        private static IntVec3 FindRewardSpawnCell(
            Map map,
            IntVec3 preferredCell,
            int minimumDistance)
        {
            for (int radius = minimumDistance; radius <= 5; radius++)
            {
                for (int x = -radius; x <= radius; x++)
                {
                    for (int z = -radius; z <= radius; z++)
                    {
                        if (radius > 0
                            && Math.Abs(x) < radius
                            && Math.Abs(z) < radius)
                        {
                            continue;
                        }

                        IntVec3 cell = preferredCell + new IntVec3(x, 0, z);

                        if (cell.InBounds(map)
                            && cell.Standable(map)
                            && cell.GetEdifice(map) == null)
                        {
                            return cell;
                        }
                    }
                }
            }

            return IntVec3.Invalid;
        }

        private static int CalculatePawnCount(
            float points,
            PawnKindDef referenceKind,
            int minimumCount)
        {
            float combatPower = referenceKind?.combatPower ?? 100f;

            if (combatPower <= 0f)
            {
                combatPower = 100f;
            }

            int count = (int)Math.Ceiling(points / combatPower);

            if (count < minimumCount)
            {
                return minimumCount;
            }

            return count;
        }

        private static IntVec3 GetReinforcementEntryCell(Map map)
        {
            IntVec3 cell;

            if (CellFinder.TryFindRandomEdgeCellWith(
                    candidate => candidate.Standable(map),
                    map,
                    CellFinder.EdgeRoadChance_Hostile,
                    out cell))
            {
                return cell;
            }

            return CellFinder.RandomCell(map);
        }

        private static float GetInitialDefenderPoints(
            Map map,
            WorldObject_TokraDecodedMissionSite parent)
        {
            float sourcePoints = parent?.ThreatPoints ?? 0f;

            if (!(sourcePoints > 0f))
            {
                sourcePoints = StorytellerUtility.DefaultThreatPointsNow(map);
                parent?.InitializeThreatPoints(sourcePoints);
            }

            return CalculateDefenderPoints(sourcePoints);
        }

        private static float GetReinforcementPoints(Map map)
        {
            WorldObject_TokraDecodedMissionSite parent
                = map?.Parent as WorldObject_TokraDecodedMissionSite;
            return Math.Max(
                MinimumReinforcementPoints,
                GetInitialDefenderPoints(map, parent)
                    * ReinforcementThreatFactor);
        }

        internal static float CalculateDefenderPoints(float sourcePoints)
        {
            return Math.Max(
                MinimumDefenderPoints,
                sourcePoints * DefenderThreatFactor);
        }

        internal static float CalculateReinforcementPoints(float sourcePoints)
        {
            return Math.Max(
                MinimumReinforcementPoints,
                CalculateDefenderPoints(sourcePoints)
                    * ReinforcementThreatFactor);
        }

        internal static int CalculateJaffaCount(
            float points,
            int minimumCount)
        {
            return CalculatePawnCount(
                points,
                GR_DefOf.SG1_GoauldJaffaWarrior
                    ?? GR_DefOf.SG1_GoauldJaffaGuard,
                minimumCount);
        }
    }
}
