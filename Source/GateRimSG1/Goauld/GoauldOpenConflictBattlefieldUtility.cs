using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI.Group;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Shared local-battlefield generation used by the colony-map incident.
    /// Its faction, point, arrival and rally contracts are intentionally
    /// reusable by the planned world-map battlefield site.
    /// </summary>
    public static class GoauldOpenConflictBattlefieldUtility
    {
        public const float DetachmentThreatFactor = 0.35f;
        public const float MinimumDetachmentPoints = 250f;
        public const float MaximumDetachmentPoints = 1800f;
        public const int MinimumDetachmentCount = 2;
        public const int MaximumDetachmentCount = 10;
        public const int MaximumBattlefieldDurationTicks = 120000;
        public const int WithdrawalGraceTicks = 30000;
        public const int CombatCheckIntervalTicks = 30;
        public const int RallyHoldTicks = 1800;
        public const int RallyTimeoutTicks = 12000;
        public const int RallyRadius = 7;
        public const int PlayerRetaliationTimeoutTicks = 1800;
        public const int WithdrawalRetaliationMaximumTicks = 6000;
        public const int PlayerPursuitMaximumDistance = 35;
        public const float EarlyWithdrawalFraction = 0.30f;

        private const int DefenderAssaultDelayTicks = 180000;
        private const int AnchorSeparation = 48;
        private const int AnchorSearchRadius = 12;
        private const int MapEdgeInset = 42;
        private const int EntrySearchRadius = 10;
        private const int EntrySpawnRadius = 5;
        private const int MaximumEntryInset = 4;
        private const int LetterVariantCount = 3;

        public static List<GoauldInterDomainRelationState>
            GetEligibleOpenConflictPairs()
        {
            GameComponent_GoauldInterDomainRelationTracker tracker =
                GameComponent_GoauldInterDomainRelationTracker.Current;

            if (tracker == null)
            {
                return new List<GoauldInterDomainRelationState>();
            }

            return tracker.Snapshot(includeInactive: false)
                .Where(state =>
                    state != null
                    && state.relation
                        == GoauldInterDomainRelation.OpenConflict
                    && state.firstDomain != null
                    && state.secondDomain != null)
                .ToList();
        }

        public static GoauldInterDomainRelationState SelectEligiblePair(
            int lastFirstDomainLoadId,
            int lastSecondDomainLoadId)
        {
            List<GoauldInterDomainRelationState> candidates =
                GetEligibleOpenConflictPairs();

            if (candidates.Count == 0)
            {
                return null;
            }

            List<GoauldInterDomainRelationState> alternatives = candidates
                .Where(state =>
                    state.firstDomain.loadID != lastFirstDomainLoadId
                    || state.secondDomain.loadID
                        != lastSecondDomainLoadId)
                .ToList();

            return (alternatives.Count > 0
                ? alternatives
                : candidates).RandomElement();
        }

        public static float CalculateDetachmentPoints(
            float vanillaThreatPoints)
        {
            return Math.Max(
                MinimumDetachmentPoints,
                Math.Min(
                    MaximumDetachmentPoints,
                    vanillaThreatPoints * DetachmentThreatFactor));
        }

        public static bool TryStartOnMap(
            Map map,
            GoauldInterDomainRelationState pair,
            bool debugForced,
            int previousLetterVariant,
            out MapComponent_GoauldOpenConflictBattlefield component,
            out int letterVariant)
        {
            component = map?.GetComponent<
                MapComponent_GoauldOpenConflictBattlefield>();
            letterVariant = SelectLetterVariant(previousLetterVariant);

            if (map == null
                || component == null
                || component.Active
                || pair?.firstDomain == null
                || pair.secondDomain == null
                || pair.relation
                    != GoauldInterDomainRelation.OpenConflict)
            {
                return false;
            }

            if (!debugForced
                && TokraRelaySabotageMissionUtility.HasActiveHostiles(map))
            {
                return false;
            }

            if (!TryFindBattlefieldPositions(
                    map,
                    out IntVec3 firstEntry,
                    out IntVec3 firstRally,
                    out IntVec3 secondEntry,
                    out IntVec3 secondRally))
            {
                return false;
            }

            float vanillaPoints =
                StorytellerUtility.DefaultThreatPointsNow(map);
            float detachmentPoints = CalculateDetachmentPoints(
                vanillaPoints);
            List<Pawn> firstDetachment = SpawnDetachment(
                map,
                pair.firstDomain,
                detachmentPoints,
                firstEntry,
                firstRally);

            if (firstDetachment.Count == 0)
            {
                return false;
            }

            List<Pawn> secondDetachment = SpawnDetachment(
                map,
                pair.secondDomain,
                detachmentPoints,
                secondEntry,
                secondRally);

            if (secondDetachment.Count == 0)
            {
                RemoveGeneratedPawns(firstDetachment);
                return false;
            }

            component.Initialize(
                pair.firstDomain,
                pair.secondDomain,
                firstDetachment,
                secondDetachment,
                firstEntry,
                secondEntry,
                firstRally,
                secondRally,
                vanillaPoints,
                detachmentPoints);
            SendBattlefieldLetter(
                pair.firstDomain,
                pair.secondDomain,
                firstDetachment[0],
                letterVariant);
            return true;
        }

        private static List<Pawn> SpawnDetachment(
            Map map,
            Faction faction,
            float requestedPoints,
            IntVec3 entryCell,
            IntVec3 rallyCell)
        {
            List<Pawn> spawnedPawns = new List<Pawn>();
            PawnKindDef warriorKind = GR_DefOf.SG1_GoauldJaffaWarrior;
            PawnKindDef guardKind = GR_DefOf.SG1_GoauldJaffaGuard;

            if (map == null
                || faction == null
                || !entryCell.IsValid
                || !rallyCell.IsValid
                || (warriorKind == null && guardKind == null))
            {
                return spawnedPawns;
            }

            PawnKindDef referenceKind = warriorKind ?? guardKind;
            float combatPower = Math.Max(1f, referenceKind.combatPower);
            int count = Math.Max(
                MinimumDetachmentCount,
                Math.Min(
                    MaximumDetachmentCount,
                    (int)Math.Ceiling(requestedPoints / combatPower)));
            Lord lord = LordMaker.MakeNewLord(
                faction,
                new LordJob_DefendBase(
                    faction,
                    rallyCell,
                    DefenderAssaultDelayTicks,
                    attackWhenPlayerBecameEnemy: false),
                map);

            for (int index = 0; index < count; index++)
            {
                PawnKindDef kind = guardKind != null
                        && (warriorKind == null || index % 5 == 4)
                    ? guardKind
                    : warriorKind ?? guardKind;
                Pawn pawn = PawnGenerator.GeneratePawn(
                    kind,
                    faction,
                    map.Tile);

                if (pawn == null)
                {
                    continue;
                }

                IntVec3 spawnCell = FindEntrySpawnCell(
                    map,
                    entryCell);
                Pawn spawnedPawn = GenSpawn.Spawn(
                    pawn,
                    spawnCell,
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

        private static bool TryFindBattlefieldPositions(
            Map map,
            out IntVec3 firstEntry,
            out IntVec3 firstRally,
            out IntVec3 secondEntry,
            out IntVec3 secondRally)
        {
            firstEntry = IntVec3.Invalid;
            firstRally = IntVec3.Invalid;
            secondEntry = IntVec3.Invalid;
            secondRally = IntVec3.Invalid;

            if (map == null)
            {
                return false;
            }

            int inset = Math.Max(
                24,
                Math.Min(
                    MapEdgeInset,
                    Math.Min(map.Size.x, map.Size.z) / 3));
            int side = Rand.Range(0, 4);
            IntVec3 center;
            IntVec3 firstCandidate;
            IntVec3 secondCandidate;
            int halfSeparation = AnchorSeparation / 2;

            switch (side)
            {
                case 1:
                    center = new IntVec3(
                        map.Size.x - inset - 1,
                        0,
                        map.Center.z);
                    firstCandidate = center
                        + new IntVec3(0, 0, -halfSeparation);
                    secondCandidate = center
                        + new IntVec3(0, 0, halfSeparation);
                    break;
                case 2:
                    center = new IntVec3(
                        map.Center.x,
                        0,
                        inset);
                    firstCandidate = center
                        + new IntVec3(-halfSeparation, 0, 0);
                    secondCandidate = center
                        + new IntVec3(halfSeparation, 0, 0);
                    break;
                case 3:
                    center = new IntVec3(
                        map.Center.x,
                        0,
                        map.Size.z - inset - 1);
                    firstCandidate = center
                        + new IntVec3(-halfSeparation, 0, 0);
                    secondCandidate = center
                        + new IntVec3(halfSeparation, 0, 0);
                    break;
                default:
                    center = new IntVec3(
                        inset,
                        0,
                        map.Center.z);
                    firstCandidate = center
                        + new IntVec3(0, 0, -halfSeparation);
                    secondCandidate = center
                        + new IntVec3(0, 0, halfSeparation);
                    break;
            }

            return TryResolveAnchor(map, firstCandidate, out firstRally)
                && TryResolveAnchor(map, secondCandidate, out secondRally)
                && HorizontalDistanceSquared(firstRally, secondRally)
                    >= 1024
                && TryResolveEntryCell(
                    map,
                    firstRally,
                    side,
                    out firstEntry)
                && TryResolveEntryCell(
                    map,
                    secondRally,
                    side,
                    out secondEntry)
                && HorizontalDistanceSquared(firstEntry, secondEntry)
                    >= 100;
        }

        private static bool TryResolveAnchor(
            Map map,
            IntVec3 candidate,
            out IntVec3 anchor)
        {
            return CellFinder.TryFindRandomCellNear(
                candidate,
                map,
                AnchorSearchRadius,
                cell => IsOpenCell(cell, map),
                out anchor);
        }

        private static bool TryResolveEntryCell(
            Map map,
            IntVec3 rallyCell,
            int side,
            out IntVec3 entryCell)
        {
            IntVec3 candidate;

            switch (side)
            {
                case 1:
                    candidate = new IntVec3(
                        map.Size.x - 1,
                        0,
                        rallyCell.z);
                    break;
                case 2:
                    candidate = new IntVec3(
                        rallyCell.x,
                        0,
                        0);
                    break;
                case 3:
                    candidate = new IntVec3(
                        rallyCell.x,
                        0,
                        map.Size.z - 1);
                    break;
                default:
                    candidate = new IntVec3(
                        0,
                        0,
                        rallyCell.z);
                    break;
            }

            return CellFinder.TryFindRandomCellNear(
                candidate,
                map,
                EntrySearchRadius,
                cell => IsOpenCell(cell, map)
                    && DistanceToNearestMapEdge(cell, map)
                        <= MaximumEntryInset,
                out entryCell);
        }

        private static IntVec3 FindEntrySpawnCell(
            Map map,
            IntVec3 entryCell)
        {
            if (CellFinder.TryFindRandomCellNear(
                    entryCell,
                    map,
                    EntrySpawnRadius,
                    cell => IsOpenCell(cell, map)
                        && DistanceToNearestMapEdge(cell, map)
                            <= MaximumEntryInset,
                    out IntVec3 spawnCell))
            {
                return spawnCell;
            }

            return entryCell;
        }

        private static bool IsOpenCell(IntVec3 cell, Map map)
        {
            return cell.InBounds(map)
                && cell.Standable(map)
                && cell.GetFirstBuilding(map) == null
                && cell.GetFirstPawn(map) == null;
        }

        private static int DistanceToNearestMapEdge(
            IntVec3 cell,
            Map map)
        {
            return Math.Min(
                Math.Min(cell.x, map.Size.x - 1 - cell.x),
                Math.Min(cell.z, map.Size.z - 1 - cell.z));
        }

        private static int HorizontalDistanceSquared(
            IntVec3 first,
            IntVec3 second)
        {
            int x = first.x - second.x;
            int z = first.z - second.z;
            return x * x + z * z;
        }

        private static int SelectLetterVariant(int previousVariant)
        {
            int variant = Rand.Range(0, LetterVariantCount);

            if (variant == previousVariant && LetterVariantCount > 1)
            {
                variant = (variant + Rand.Range(1, LetterVariantCount))
                    % LetterVariantCount;
            }

            return variant;
        }

        private static void SendBattlefieldLetter(
            Faction firstDomain,
            Faction secondDomain,
            Pawn focusPawn,
            int variant)
        {
            string textKey =
                "GR_GoauldOpenConflictBattlefield_Text" + variant;
            LookTargets targets = focusPawn == null
                ? default(LookTargets)
                : new LookTargets(focusPawn);

            Find.LetterStack?.ReceiveLetter(
                "GR_GoauldOpenConflictBattlefield_Label".Translate(),
                textKey.Translate(
                    firstDomain?.Name ?? "<missing domain>",
                    secondDomain?.Name ?? "<missing domain>"),
                LetterDefOf.ThreatSmall,
                targets);
        }

        private static void RemoveGeneratedPawns(List<Pawn> pawns)
        {
            if (pawns == null)
            {
                return;
            }

            foreach (Pawn pawn in pawns)
            {
                pawn?.GetLord()?.RemovePawn(pawn);

                if (pawn != null && !pawn.Destroyed)
                {
                    pawn.Destroy(DestroyMode.Vanish);
                }
            }
        }
    }
}
