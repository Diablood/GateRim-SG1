using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public enum GoauldJaffaRaidDoctrine
    {
        Direct,
        Abduction,
        Destruction
    }

    public struct GoauldJaffaRaidDoctrineWeights
    {
        public float direct;
        public float abduction;
        public float destruction;

        public float Total
        {
            get
            {
                return direct + abduction + destruction;
            }
        }

        public float Percentage(float weight)
        {
            return Total > 0f
                ? weight / Total * 100f
                : 0f;
        }
    }

    /// <summary>
    /// Low-frequency natural Goa'uld Jaffa raid.
    ///
    /// One storyteller incident selects a doctrine from vanilla threat points,
    /// readable colony context and the attacking domain's persistent strategic
    /// profile. Keeping one IncidentDef preserves the original frequency and
    /// refire delay instead of giving every doctrine an independent roll.
    /// </summary>
    public class IncidentWorker_GoauldJaffaNaturalRaid
        : IncidentWorker_GoauldJaffaControlledRaid
    {
        public const float AbductionMinimumPoints = 800f;
        public const int AbductionMinimumColonists = 2;
        public const float DestructionMinimumPoints = 1800f;
        public const float DestructionMinimumBuildingWealth = 10000f;

        private const float FallbackDirectWeight = 2f;
        private const float FallbackAbductionWeight = 1f;
        private const float FallbackDestructionWeight = 1f;

        private GoauldJaffaRaidDoctrine? forcedDebugDoctrine;
        private bool forceRelationPressureForDebug;
        private GoauldAllianceRaidOutcome? forcedDebugAllianceOutcome;
        private bool forceOfficerForDebug;
        private bool executionWasExternallyForced;
        private GoauldAllianceRaidOutcome preparedAllianceOutcome;
        private Faction preparedAlliedDomain;
        private float preparedAlliedPoints;
        private float preparedCombinedPoints;
        private IntVec3 preparedAlliedSpawnCenter = IntVec3.Invalid;

        protected override string ControlledRaidPurpose
        {
            get
            {
                return "natural Goa'uld Jaffa raids";
            }
        }

        protected override string RaidLogContext
        {
            get
            {
                return "natural Goa'uld Jaffa raid";
            }
        }

        protected override float ResolveMissingRaidPoints(Map map)
        {
            return map == null
                ? 0f
                : StorytellerUtility.DefaultThreatPointsNow(map);
        }

        protected override void ConfigureRaidParms(IncidentParms parms)
        {
            Map map = parms.target as Map;
            GoauldJaffaRaidDoctrine doctrine = forcedDebugDoctrine
                ?? SelectDoctrine(
                    CalculateDoctrineWeights(
                        map,
                        parms.points,
                        parms.faction));

            parms.canSteal = false;

            switch (doctrine)
            {
                case GoauldJaffaRaidDoctrine.Abduction:
                    parms.raidStrategy =
                        GR_DefOf.SG1_GoauldJaffaAbductionAssault;
                    parms.canKidnap = true;
                    parms.canTimeoutOrFlee = false;
                    break;

                case GoauldJaffaRaidDoctrine.Destruction:
                    parms.raidStrategy =
                        GR_DefOf.SG1_GoauldJaffaDestructionAssault;
                    parms.canKidnap = false;
                    parms.canTimeoutOrFlee = false;
                    break;

                default:
                    parms.raidStrategy =
                        RaidStrategyDefOf.ImmediateAttack;
                    parms.canKidnap = false;
                    parms.canTimeoutOrFlee = true;
                    break;
            }

            ApplyInterDomainPressureModifier(parms);
            PrepareAlliedRaid(parms, doctrine);
        }

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            return base.CanFireNowSub(parms)
                && GoauldSystemLordFactionUtility
                    .GetAllFactions()
                    .Count > 0;
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            bool previousExternalForced = executionWasExternallyForced;
            executionWasExternallyForced = parms?.forced == true;
            ClearPreparedAlliedReinforcement();

            try
            {
                if (parms != null
                    && !GoauldSystemLordFactionUtility
                        .IsSystemLordFaction(parms.faction))
                {
                    parms.faction =
                        GoauldSystemLordFactionUtility
                            .SelectRandomActiveFaction();
                }

                Map map = parms?.target as Map;
                HashSet<string> existingPrimaryPawnIds =
                    PawnIdsForFaction(map, parms?.faction);
                bool succeeded;

                if (executionWasExternallyForced
                    && !forceOfficerForDebug)
                {
                    succeeded = base.TryExecuteWorker(parms);
                }
                else
                {
                    using (GoauldJaffaOfficerForceUtility
                        .BeginCombatOfficerGeneration(
                            PawnGroupKindDefOf.Combat,
                            "natural Goa'uld Jaffa raid",
                            forceOfficerForDebug))
                    {
                        succeeded = base.TryExecuteWorker(parms);
                    }
                }

                if (succeeded && preparedAlliedDomain != null)
                {
                    List<Pawn> primaryPawns = NewPawnsForFaction(
                        map,
                        parms.faction,
                        existingPrimaryPawnIds);
                    GameComponent_GoauldAlliedReinforcementTracker tracker =
                        GameComponent_GoauldAlliedReinforcementTracker
                            .Current;
                    bool scheduled = preparedAllianceOutcome
                            == GoauldAllianceRaidOutcome.JointRaid
                        ? tracker?.TryStartJointRaid(
                            map,
                            parms.faction,
                            preparedAlliedDomain,
                            preparedAlliedPoints,
                            primaryPawns,
                            preparedAlliedSpawnCenter) == true
                        : tracker?.TrySchedule(
                            map,
                            parms.faction,
                            preparedAlliedDomain,
                            preparedAlliedPoints,
                            primaryPawns,
                            forcedDebugAllianceOutcome
                                == GoauldAllianceRaidOutcome
                                    .DelayedReinforcement) == true;

                    if (!scheduled)
                    {
                        GR_Log.Error(
                            "The primary Goa'uld raid used a shared allied "
                            + $"budget, but its {preparedAllianceOutcome} "
                            + "detachment could not be started.");
                    }
                }

                return succeeded;
            }
            finally
            {
                ClearPreparedAlliedReinforcement();
                executionWasExternallyForced = previousExternalForced;
            }
        }

        public static GoauldJaffaRaidDoctrineWeights
            CalculateDoctrineWeights(Map map, float points)
        {
            Faction faction = GoauldSystemLordFactionUtility
                .GetAllFactions()
                .FirstOrDefault();

            return CalculateDoctrineWeights(
                map,
                points,
                faction);
        }

        public static GoauldJaffaRaidDoctrineWeights
            CalculateDoctrineWeights(
                Map map,
                float points,
                Faction faction)
        {
            GoauldJaffaRaidDoctrineWeights baseWeights =
                ResolveBaseWeights(faction);
            GoauldJaffaRaidDoctrineWeights weights =
                new GoauldJaffaRaidDoctrineWeights
                {
                    direct = baseWeights.direct
                };

            if (map == null)
            {
                return weights;
            }

            if (points >= AbductionMinimumPoints
                && map.mapPawns.FreeColonistsSpawnedCount
                    >= AbductionMinimumColonists)
            {
                weights.abduction = baseWeights.abduction;
            }

            if (points >= DestructionMinimumPoints
                && map.wealthWatcher.WealthBuildings
                    >= DestructionMinimumBuildingWealth)
            {
                weights.destruction = baseWeights.destruction;
            }

            return weights;
        }

        public static float ResolveNaturalRaidPressureFactor(
            Faction faction)
        {
            return GoauldOpenConflictPressureUtility
                .ResolveNaturalRaidPressureFactor(faction);
        }

        public static float CalculateEffectiveNaturalRaidPoints(
            Faction faction,
            float points)
        {
            if (!(points > 0f))
            {
                return points;
            }

            return Math.Max(
                1f,
                points * ResolveNaturalRaidPressureFactor(faction));
        }

        public bool TryExecuteForcedDebugDoctrine(
            IncidentParms parms,
            GoauldJaffaRaidDoctrine doctrine)
        {
            // Debug access validates the real worker without requiring the
            // test map to satisfy normal storyteller eligibility. Forced
            // doctrine regressions intentionally preserve their exact points.
            forcedDebugDoctrine = doctrine;

            try
            {
                return TryExecute(parms);
            }
            finally
            {
                forcedDebugDoctrine = null;
            }
        }

        public bool TryExecuteForcedDebugDoctrineWithOfficer(
            IncidentParms parms,
            GoauldJaffaRaidDoctrine doctrine)
        {
            GoauldJaffaRaidDoctrine? previousDoctrine = forcedDebugDoctrine;
            bool previousOfficerValue = forceOfficerForDebug;
            forcedDebugDoctrine = doctrine;
            forceOfficerForDebug = true;

            try
            {
                return TryExecute(parms);
            }
            finally
            {
                forcedDebugDoctrine = previousDoctrine;
                forceOfficerForDebug = previousOfficerValue;
            }
        }

        public bool TryExecuteForcedWithRelationPressure(
            IncidentParms parms)
        {
            bool previousValue = forceRelationPressureForDebug;
            forceRelationPressureForDebug = true;

            try
            {
                return TryExecute(parms);
            }
            finally
            {
                forceRelationPressureForDebug = previousValue;
            }
        }

        public bool TryExecuteForcedWithOpenConflictPressure(
            IncidentParms parms)
        {
            return TryExecuteForcedWithRelationPressure(parms);
        }

        public bool TryExecuteForcedWithAlliedReinforcement(
            IncidentParms parms)
        {
            return TryExecuteForcedWithAllianceOutcome(
                parms,
                GoauldAllianceRaidOutcome.DelayedReinforcement);
        }

        public bool TryExecuteForcedJointRaid(IncidentParms parms)
        {
            return TryExecuteForcedWithAllianceOutcome(
                parms,
                GoauldAllianceRaidOutcome.JointRaid);
        }

        public bool TryExecuteForcedStandardAllianceRaid(
            IncidentParms parms)
        {
            return TryExecuteForcedWithAllianceOutcome(
                parms,
                GoauldAllianceRaidOutcome.Standard);
        }

        private bool TryExecuteForcedWithAllianceOutcome(
            IncidentParms parms,
            GoauldAllianceRaidOutcome outcome)
        {
            bool previousPressureValue = forceRelationPressureForDebug;
            GoauldAllianceRaidOutcome? previousOutcome =
                forcedDebugAllianceOutcome;
            GoauldJaffaRaidDoctrine? previousDoctrine =
                forcedDebugDoctrine;
            forceRelationPressureForDebug = true;
            forcedDebugAllianceOutcome = outcome;

            if (outcome == GoauldAllianceRaidOutcome.JointRaid)
            {
                forcedDebugDoctrine = GoauldJaffaRaidDoctrine.Direct;
            }

            try
            {
                return TryExecute(parms);
            }
            finally
            {
                forceRelationPressureForDebug = previousPressureValue;
                forcedDebugAllianceOutcome = previousOutcome;
                forcedDebugDoctrine = previousDoctrine;
            }
        }

        private void ApplyInterDomainPressureModifier(
            IncidentParms parms)
        {
            if (parms == null
                || !(parms.points > 0f)
                || (executionWasExternallyForced
                    && !forceRelationPressureForDebug))
            {
                return;
            }

            float factor = ResolveNaturalRaidPressureFactor(parms.faction);

            if (Math.Abs(factor - 1f) < 0.001f)
            {
                return;
            }

            float originalPoints = parms.points;
            float effectivePoints = CalculateEffectiveNaturalRaidPoints(
                parms.faction,
                originalPoints);
            parms.points = effectivePoints;

            bool reduced = factor < 1f;
            string relationReason = reduced
                ? "open conflict"
                : "an active alliance";

            GR_Log.Message(
                (reduced ? "Reduced" : "Increased")
                + " natural Goa'uld Jaffa raid pressure for "
                + $"{parms.faction?.Name ?? "<missing domain>"} "
                + $"({parms.faction?.loadID ?? -1}) from "
                + $"{originalPoints:0} to {effectivePoints:0} points "
                + $"(factor {factor:0.00}) because the domain is in "
                + relationReason
                + " under SG-1 Command.");
        }

        private void PrepareAlliedRaid(
            IncidentParms parms,
            GoauldJaffaRaidDoctrine doctrine)
        {
            if (parms == null
                || !(parms.points > 0f)
                || (executionWasExternallyForced
                    && !forcedDebugAllianceOutcome.HasValue))
            {
                return;
            }

            if (!GameComponent_GoauldAlliedReinforcementTracker
                .TryResolvePlan(
                    parms.target as Map,
                    parms.faction,
                    parms.points,
                    doctrine,
                    forcedDebugAllianceOutcome,
                    out GoauldAllianceRaidOutcome outcome,
                    out Faction alliedDomain,
                    out float primaryPoints,
                    out float alliedPoints))
            {
                return;
            }

            if (outcome == GoauldAllianceRaidOutcome.Standard)
            {
                GR_Log.Message(
                    "Kept an eligible alliance-context natural Goa'uld "
                    + "raid standard; no allied detachment was selected.");
                return;
            }

            preparedAllianceOutcome = outcome;
            preparedAlliedDomain = alliedDomain;
            preparedAlliedPoints = alliedPoints;
            preparedCombinedPoints = parms.points;
            parms.points = primaryPoints;

            if (outcome == GoauldAllianceRaidOutcome.JointRaid)
            {
                if (TryFindJointSpawnCenters(
                        parms.target as Map,
                        out IntVec3 primarySpawnCenter,
                        out IntVec3 alliedSpawnCenter))
                {
                    parms.spawnCenter = primarySpawnCenter;
                    preparedAlliedSpawnCenter = alliedSpawnCenter;
                    parms.sendLetter = true;
                    parms.customLetterDef = LetterDefOf.ThreatBig;
                    parms.customLetterLabel =
                        "GR_GoauldJointRaid_ArrivalLabel".Translate();
                    parms.customLetterText =
                        "GR_GoauldJointRaid_ArrivalText".Translate(
                            parms.faction?.Name ?? "<missing domain>",
                            alliedDomain.Name);
                }
                else
                {
                    preparedAllianceOutcome =
                        GoauldAllianceRaidOutcome.DelayedReinforcement;
                    preparedAlliedPoints = Math.Max(
                        1f,
                        preparedCombinedPoints
                            * GameComponent_GoauldAlliedReinforcementTracker
                                .AlliedBudgetFraction);
                    parms.points = Math.Max(
                        1f,
                        preparedCombinedPoints - preparedAlliedPoints);
                    GR_Log.Warning(
                        "Could not find two reachable opposite map edges "
                        + "for a joint Goa'uld raid; using a delayed allied "
                        + "reinforcement instead.");
                }
            }

            GR_Log.Message(
                $"Prepared {preparedAllianceOutcome} Goa'uld raid budget: "
                + $"primary={parms.faction?.Name ?? "<missing>"}, "
                + $"ally={alliedDomain.Name}, combined="
                + $"{preparedCombinedPoints:0}, primary="
                + $"{parms.points:0}, allied="
                + $"{preparedAlliedPoints:0}.");
        }

        private void ClearPreparedAlliedReinforcement()
        {
            preparedAllianceOutcome = GoauldAllianceRaidOutcome.Standard;
            preparedAlliedDomain = null;
            preparedAlliedPoints = 0f;
            preparedCombinedPoints = 0f;
            preparedAlliedSpawnCenter = IntVec3.Invalid;
        }

        private static HashSet<string> PawnIdsForFaction(
            Map map,
            Faction faction)
        {
            return new HashSet<string>(
                map?.mapPawns?.AllPawnsSpawned
                    ?.Where(pawn => pawn?.Faction == faction)
                    .Select(pawn => pawn.ThingID)
                ?? Enumerable.Empty<string>());
        }

        private static List<Pawn> NewPawnsForFaction(
            Map map,
            Faction faction,
            HashSet<string> existingPawnIds)
        {
            return map?.mapPawns?.AllPawnsSpawned
                ?.Where(pawn =>
                    pawn?.Faction == faction
                    && !existingPawnIds.Contains(pawn.ThingID))
                .ToList()
                ?? new List<Pawn>();
        }

        private static bool TryFindJointSpawnCenters(
            Map map,
            out IntVec3 primarySpawnCenter,
            out IntVec3 alliedSpawnCenter)
        {
            primarySpawnCenter = IntVec3.Invalid;
            alliedSpawnCenter = IntVec3.Invalid;

            if (map == null)
            {
                return false;
            }

            int firstSide = Rand.Range(0, 4);

            for (int offset = 0; offset < 4; offset++)
            {
                int side = (firstSide + offset) % 4;
                int oppositeSide = (side + 2) % 4;

                if (TryFindSpawnCenterOnSide(
                        map,
                        side,
                        out primarySpawnCenter)
                    && TryFindSpawnCenterOnSide(
                        map,
                        oppositeSide,
                        out alliedSpawnCenter))
                {
                    return true;
                }
            }

            primarySpawnCenter = IntVec3.Invalid;
            alliedSpawnCenter = IntVec3.Invalid;
            return false;
        }

        private static bool TryFindSpawnCenterOnSide(
            Map map,
            int side,
            out IntVec3 spawnCenter)
        {
            return CellFinder.TryFindRandomEdgeCellWith(
                cell => cell.Standable(map)
                    && IsCellOnSide(cell, map, side)
                    && map.reachability.CanReachColony(cell),
                map,
                CellFinder.EdgeRoadChance_Hostile,
                out spawnCenter);
        }

        private static bool IsCellOnSide(
            IntVec3 cell,
            Map map,
            int side)
        {
            switch (side)
            {
                case 0:
                    return cell.x <= 1;
                case 1:
                    return cell.z >= map.Size.z - 2;
                case 2:
                    return cell.x >= map.Size.x - 2;
                default:
                    return cell.z <= 1;
            }
        }

        private static GoauldJaffaRaidDoctrineWeights
            ResolveBaseWeights(Faction faction)
        {
            GameComponent_GoauldDomainDoctrineTracker tracker =
                GameComponent_GoauldDomainDoctrineTracker.Current;

            if (tracker != null
                && tracker.TryGetProfile(
                    faction,
                    out GoauldDomainDoctrineProfileDef profile)
                && profile != null)
            {
                return profile.BaseWeights;
            }

            return new GoauldJaffaRaidDoctrineWeights
            {
                direct = FallbackDirectWeight,
                abduction = FallbackAbductionWeight,
                destruction = FallbackDestructionWeight
            };
        }

        private static GoauldJaffaRaidDoctrine SelectDoctrine(
            GoauldJaffaRaidDoctrineWeights weights)
        {
            if (weights.Total <= 0f)
            {
                return GoauldJaffaRaidDoctrine.Direct;
            }

            float roll = Rand.Value * weights.Total;

            if (roll < weights.destruction)
            {
                return GoauldJaffaRaidDoctrine.Destruction;
            }

            roll -= weights.destruction;

            if (roll < weights.abduction)
            {
                return GoauldJaffaRaidDoctrine.Abduction;
            }

            return GoauldJaffaRaidDoctrine.Direct;
        }
    }
}
