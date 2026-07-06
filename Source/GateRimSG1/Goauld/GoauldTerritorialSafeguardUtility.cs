using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    public enum GoauldTerritorialSafeguardFailure
    {
        None,
        StorytellerInactive,
        TooFewActiveDomains,
        SparseWorld,
        InactiveDomain,
        MissingSettlement,
        OwnershipChanged,
        RelationChanged,
        LastSettlementProtected,
        HegemonyLimit,
        IncompatibleTransition
    }

    public sealed class GoauldTerritorialSafeguardEvaluation
    {
        public bool allowed;
        public GoauldTerritorialSafeguardFailure failure;
        public string detail;
        public int activeDomainCount;
        public int totalSettlementCount;
        public int minimumSettlementCount;
        public int gainingSettlementCount;
        public int losingSettlementCount;
        public int projectedGainingSettlementCount;
        public float projectedTerritorialShare;
    }

    internal static class GoauldTerritorialSafeguardUtility
    {
        public const int MinimumActiveDomainCount = 2;
        public const int MinimumWorldSurplusSettlementCount = 2;
        public const float MaximumAutomaticTerritorialShare = 0.50f;

        public static List<Settlement> GetPermanentGoauldSettlements()
        {
            if (Find.WorldObjects?.AllWorldObjects == null)
            {
                return new List<Settlement>();
            }

            return Find.WorldObjects.AllWorldObjects
                .OfType<Settlement>()
                .Where(settlement =>
                    settlement != null
                    && !settlement.Destroyed
                    && GoauldSystemLordFactionUtility
                        .IsSystemLordFaction(settlement.Faction)
                    && !settlement.Faction.defeated)
                .OrderBy(settlement => settlement.ID)
                .ToList();
        }

        public static List<Settlement> GetPermanentSettlements(
            Faction domain)
        {
            return GetPermanentGoauldSettlements()
                .Where(settlement => settlement.Faction == domain)
                .ToList();
        }

        public static List<Faction> GetActiveTerritorialDomains()
        {
            HashSet<Faction> territorialFactions =
                new HashSet<Faction>(
                    GetPermanentGoauldSettlements()
                        .Select(settlement => settlement.Faction)
                        .Where(faction => faction != null));

            return GoauldSystemLordFactionUtility.GetAllFactions()
                .Where(faction => territorialFactions.Contains(faction))
                .OrderBy(faction => faction.loadID)
                .ToList();
        }

        public static Settlement FindSettlement(int worldObjectId)
        {
            if (worldObjectId < 0
                || Find.WorldObjects?.AllWorldObjects == null)
            {
                return null;
            }

            return Find.WorldObjects.AllWorldObjects
                .OfType<Settlement>()
                .FirstOrDefault(settlement =>
                    settlement != null
                    && !settlement.Destroyed
                    && settlement.ID == worldObjectId);
        }

        public static int MinimumSettlementCountForTransfers(
            int activeDomainCount)
        {
            return Math.Max(
                MinimumActiveDomainCount
                    + MinimumWorldSurplusSettlementCount,
                activeDomainCount
                    + MinimumWorldSurplusSettlementCount);
        }

        public static float ExpansionWeight(int settlementCount)
        {
            if (settlementCount <= 1)
            {
                return 1f;
            }

            if (settlementCount == 2)
            {
                return 0.50f;
            }

            if (settlementCount == 3)
            {
                return 0.25f;
            }

            return 0.10f;
        }

        public static int ExpansionDelayMultiplier(int settlementCount)
        {
            if (settlementCount <= 1)
            {
                return 1;
            }

            if (settlementCount == 2)
            {
                return 2;
            }

            if (settlementCount == 3)
            {
                return 4;
            }

            return 8;
        }

        public static GoauldTerritorialSafeguardEvaluation EvaluateTransfer(
            Faction gainingDomain,
            Faction losingDomain,
            Settlement settlement,
            GoauldInterDomainRelation requiredRelation)
        {
            GoauldTerritorialSafeguardEvaluation evaluation =
                BuildBaseline(gainingDomain, losingDomain);

            if (!GateRimSG1.Storytelling.GateRimStorytellerUtility
                    .IsGateRimStorytellerActive)
            {
                return Reject(
                    evaluation,
                    GoauldTerritorialSafeguardFailure.StorytellerInactive,
                    "Commandement SG-1 is not active.");
            }

            if (evaluation.activeDomainCount < MinimumActiveDomainCount)
            {
                return Reject(
                    evaluation,
                    GoauldTerritorialSafeguardFailure.TooFewActiveDomains,
                    "At least two territorial Goa'uld domains are required.");
            }

            List<Faction> activeDomains = GetActiveTerritorialDomains();

            if (gainingDomain == null
                || losingDomain == null
                || gainingDomain == losingDomain
                || !activeDomains.Contains(gainingDomain)
                || !activeDomains.Contains(losingDomain))
            {
                return Reject(
                    evaluation,
                    GoauldTerritorialSafeguardFailure.InactiveDomain,
                    "Both distinct domains must remain active and territorial.");
            }

            if (settlement == null || settlement.Destroyed)
            {
                return Reject(
                    evaluation,
                    GoauldTerritorialSafeguardFailure.MissingSettlement,
                    "The reserved permanent settlement no longer exists.");
            }

            if (settlement.Faction != losingDomain)
            {
                return Reject(
                    evaluation,
                    GoauldTerritorialSafeguardFailure.OwnershipChanged,
                    "The reserved settlement is no longer owned by the losing domain.");
            }

            GameComponent_GoauldInterDomainRelationTracker relationTracker =
                GameComponent_GoauldInterDomainRelationTracker.Current;
            GoauldInterDomainRelation relation =
                GoauldInterDomainRelation.Neutral;
            bool hasRelation =
                relationTracker != null
                && relationTracker.TryGetRelation(
                    gainingDomain,
                    losingDomain,
                    out relation);

            if (!hasRelation || relation != requiredRelation)
            {
                return Reject(
                    evaluation,
                    GoauldTerritorialSafeguardFailure.RelationChanged,
                    "The exact pair no longer has the required strategic relation.");
            }

            if (GameComponent_GoauldDomainReprisalTracker.Current
                    ?.HasPendingAllianceRuptureForPair(
                        gainingDomain,
                        losingDomain) == true)
            {
                return Reject(
                    evaluation,
                    GoauldTerritorialSafeguardFailure
                        .IncompatibleTransition,
                    "The exact pair already has an incompatible alliance rupture pending.");
            }

            if (evaluation.totalSettlementCount
                < evaluation.minimumSettlementCount)
            {
                return Reject(
                    evaluation,
                    GoauldTerritorialSafeguardFailure.SparseWorld,
                    "The world does not contain enough permanent Goa'uld settlements for a safe transfer.");
            }

            if (evaluation.losingSettlementCount <= 1)
            {
                return Reject(
                    evaluation,
                    GoauldTerritorialSafeguardFailure
                        .LastSettlementProtected,
                    "The transfer would remove the losing domain's final permanent settlement.");
            }

            evaluation.projectedGainingSettlementCount =
                evaluation.gainingSettlementCount + 1;
            evaluation.projectedTerritorialShare =
                evaluation.totalSettlementCount <= 0
                    ? 1f
                    : (float)evaluation.projectedGainingSettlementCount
                        / evaluation.totalSettlementCount;

            if (evaluation.projectedTerritorialShare
                > MaximumAutomaticTerritorialShare)
            {
                return Reject(
                    evaluation,
                    GoauldTerritorialSafeguardFailure.HegemonyLimit,
                    "The transfer would place the gaining domain above 50% of permanent Goa'uld settlements.");
            }

            evaluation.allowed = true;
            evaluation.failure = GoauldTerritorialSafeguardFailure.None;
            evaluation.detail = "All territorial safeguards allow this dry-run reservation.";
            return evaluation;
        }

        private static GoauldTerritorialSafeguardEvaluation BuildBaseline(
            Faction gainingDomain,
            Faction losingDomain)
        {
            List<Settlement> settlements = GetPermanentGoauldSettlements();
            int activeDomainCount = settlements
                .Select(settlement => settlement.Faction)
                .Where(faction => faction != null && !faction.defeated)
                .Distinct()
                .Count();
            int gainingCount = settlements.Count(settlement =>
                settlement.Faction == gainingDomain);
            int losingCount = settlements.Count(settlement =>
                settlement.Faction == losingDomain);

            return new GoauldTerritorialSafeguardEvaluation
            {
                activeDomainCount = activeDomainCount,
                totalSettlementCount = settlements.Count,
                minimumSettlementCount =
                    MinimumSettlementCountForTransfers(activeDomainCount),
                gainingSettlementCount = gainingCount,
                losingSettlementCount = losingCount,
                projectedGainingSettlementCount = gainingCount + 1,
                projectedTerritorialShare = settlements.Count <= 0
                    ? 1f
                    : (float)(gainingCount + 1) / settlements.Count
            };
        }

        private static GoauldTerritorialSafeguardEvaluation Reject(
            GoauldTerritorialSafeguardEvaluation evaluation,
            GoauldTerritorialSafeguardFailure failure,
            string detail)
        {
            evaluation.allowed = false;
            evaluation.failure = failure;
            evaluation.detail = detail;
            return evaluation;
        }
    }
}
