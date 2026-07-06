using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public enum GoauldTerritorialReservationOutcome
    {
        None,
        Pending,
        CompletedDryRun,
        CancelledStorytellerInactive,
        CancelledTooFewActiveDomains,
        CancelledSparseWorld,
        CancelledInactiveDomain,
        CancelledMissingSettlement,
        CancelledOwnershipChanged,
        CancelledRelationChanged,
        CancelledLastSettlementProtected,
        CancelledHegemonyLimit,
        CancelledGlobalCooldown,
        CancelledDomainCooldown,
        CancelledPairCooldown,
        CancelledIncompatibleTransition,
        CancelledDebug
    }

    public sealed class GoauldTerritorialReservationState : IExposable
    {
        public Faction gainingDomain;
        public Faction losingDomain;
        public int settlementWorldObjectId = -1;
        public int createdTick;
        public int resolutionTick;
        public int completedTick;
        public GoauldInterDomainRelation requiredRelation
            = GoauldInterDomainRelation.OpenConflict;
        public GoauldTerritorialReservationOutcome outcome;
        public bool pending;
        public bool debugShortDelay;
        public int activeDomainCountAtCreation;
        public int totalSettlementCountAtCreation;
        public int gainingSettlementCountAtCreation;
        public int losingSettlementCountAtCreation;

        public void ExposeData()
        {
            Scribe_References.Look(ref gainingDomain, "gainingDomain");
            Scribe_References.Look(ref losingDomain, "losingDomain");
            Scribe_Values.Look(
                ref settlementWorldObjectId,
                "settlementWorldObjectId",
                -1);
            Scribe_Values.Look(ref createdTick, "createdTick", 0);
            Scribe_Values.Look(ref resolutionTick, "resolutionTick", 0);
            Scribe_Values.Look(ref completedTick, "completedTick", 0);
            Scribe_Values.Look(
                ref requiredRelation,
                "requiredRelation",
                GoauldInterDomainRelation.OpenConflict);
            Scribe_Values.Look(
                ref outcome,
                "outcome",
                GoauldTerritorialReservationOutcome.None);
            Scribe_Values.Look(ref pending, "pending", false);
            Scribe_Values.Look(
                ref debugShortDelay,
                "debugShortDelay",
                false);
            Scribe_Values.Look(
                ref activeDomainCountAtCreation,
                "activeDomainCountAtCreation",
                0);
            Scribe_Values.Look(
                ref totalSettlementCountAtCreation,
                "totalSettlementCountAtCreation",
                0);
            Scribe_Values.Look(
                ref gainingSettlementCountAtCreation,
                "gainingSettlementCountAtCreation",
                0);
            Scribe_Values.Look(
                ref losingSettlementCountAtCreation,
                "losingSettlementCountAtCreation",
                0);
        }
    }

    public sealed class GoauldTerritorialDomainCooldownState : IExposable
    {
        public Faction domain;
        public int nextEligibleTick;

        public void ExposeData()
        {
            Scribe_References.Look(ref domain, "domain");
            Scribe_Values.Look(
                ref nextEligibleTick,
                "nextEligibleTick",
                0);
        }
    }

    public sealed class GoauldTerritorialPairCooldownState : IExposable
    {
        public Faction firstDomain;
        public Faction secondDomain;
        public int nextEligibleTick;

        public void ExposeData()
        {
            Scribe_References.Look(ref firstDomain, "firstDomain");
            Scribe_References.Look(ref secondDomain, "secondDomain");
            Scribe_Values.Look(
                ref nextEligibleTick,
                "nextEligibleTick",
                0);
        }
    }
}
