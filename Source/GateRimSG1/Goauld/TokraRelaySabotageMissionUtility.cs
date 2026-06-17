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
        private const float MaximumDefenderPoints = 900f;
        private const float MinimumReinforcementPoints = 100f;
        private const float MaximumReinforcementPoints = 300f;
        private const int MinimumDefenderCount = 2;
        private const int MaximumDefenderCount = 8;
        private const int MinimumReinforcementCount = 1;
        private const int MaximumReinforcementCount = 3;

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

            Thing relay = TrySpawnRelayDevice(map);
            Faction goauldFaction = GoauldSystemLordFactionUtility
                .GetOrCreateFaction("Tok'ra relay sabotage mission");

            if (goauldFaction != null)
            {
                List<Pawn> defenders = SpawnJaffaGroup(
                    map,
                    goauldFaction,
                    GetInitialDefenderPoints(map),
                    true,
                    MinimumDefenderCount,
                    MaximumDefenderCount);

                if (defenders.Count == 0)
                {
                    GR_Log.Error(
                        "Tok'ra relay mission generated no Goa'uld/Jaffa "
                        + "defenders.");
                }
            }

            component.Initialize(parent, relay);

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

            List<Pawn> reinforcements = SpawnJaffaGroup(
                map,
                goauldFaction,
                GetReinforcementPoints(map),
                false,
                MinimumReinforcementCount,
                MaximumReinforcementCount);

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

        private static Thing TrySpawnRelayDevice(Map map)
        {
            if (GR_DefOf.SG1_TokraRelaySabotageDevice == null)
            {
                GR_Log.Error(
                    "Cannot spawn Tok'ra relay sabotage device: missing "
                    + "ThingDef SG1_TokraRelaySabotageDevice.");
                return null;
            }

            IntVec3 cell;

            if (!TryFindCentralStandableCell(map, out cell))
            {
                GR_Log.Warning(
                    "Unable to find a central cell for the Tok'ra relay "
                    + "sabotage device.");
                return null;
            }

            Thing relay = ThingMaker.MakeThing(
                GR_DefOf.SG1_TokraRelaySabotageDevice);

            return GenSpawn.Spawn(relay, cell, map);
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
            bool nearCenter,
            int minimumCount,
            int maximumCount)
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
                minimumCount,
                maximumCount);

            IntVec3 rootCell = nearCenter
                ? map.Center
                : GetReinforcementEntryCell(map);

            Lord lord = LordMaker.MakeNewLord(
                faction,
                new LordJob_AssaultColony(faction),
                map);

            for (int index = 0; index < pawnCount; index++)
            {
                PawnKindDef pawnKind = ChooseJaffaPawnKind(
                    index,
                    warriorKind,
                    guardKind);

                Pawn pawn = PawnGenerator.GeneratePawn(
                    pawnKind,
                    faction,
                    map.Tile);

                if (pawn == null)
                {
                    GR_Log.Warning(
                        "One Tok'ra relay Jaffa could not be generated.");
                    continue;
                }

                IntVec3 spawnCell = CellFinder.RandomClosewalkCellNear(
                    rootCell,
                    map,
                    nearCenter ? 12 : 6);

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

        private static PawnKindDef ChooseJaffaPawnKind(
            int index,
            PawnKindDef warriorKind,
            PawnKindDef guardKind)
        {
            if (guardKind != null && (warriorKind == null || index % 3 == 2))
            {
                return guardKind;
            }

            return warriorKind ?? guardKind;
        }

        private static int CalculatePawnCount(
            float points,
            PawnKindDef referenceKind,
            int minimumCount,
            int maximumCount)
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

            if (count > maximumCount)
            {
                return maximumCount;
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

        private static float GetInitialDefenderPoints(Map map)
        {
            return ClampPoints(
                StorytellerUtility.DefaultThreatPointsNow(map)
                    * DefenderThreatFactor,
                MinimumDefenderPoints,
                MaximumDefenderPoints);
        }

        private static float GetReinforcementPoints(Map map)
        {
            return ClampPoints(
                GetInitialDefenderPoints(map) * ReinforcementThreatFactor,
                MinimumReinforcementPoints,
                MaximumReinforcementPoints);
        }

        private static float ClampPoints(
            float points,
            float minimum,
            float maximum)
        {
            if (points < minimum)
            {
                return minimum;
            }

            if (points > maximum)
            {
                return maximum;
            }

            return points;
        }
    }
}
