using System.Collections.Generic;
using GateRimSG1.Storytelling;
using RimWorld;

namespace GateRimSG1.Goauld
{
    public static class GoauldOpenConflictPressureUtility
    {
        public const float OpenConflictNaturalRaidFactor = 0.75f;
        public const float AllianceNaturalRaidFactor = 1.10f;

        // Retain the historical constant for source compatibility with older
        // diagnostics while the utility now resolves both relation effects.
        public const float NaturalRaidFactor =
            OpenConflictNaturalRaidFactor;

        public static bool IsDomainInActiveOpenConflict(
            Faction domainFaction)
        {
            return IsDomainInActiveRelation(
                domainFaction,
                GoauldInterDomainRelation.OpenConflict);
        }

        public static bool IsDomainInActiveAlliance(
            Faction domainFaction)
        {
            return IsDomainInActiveRelation(
                domainFaction,
                GoauldInterDomainRelation.Alliance);
        }

        public static float ResolveNaturalRaidPressureFactor(
            Faction domainFaction)
        {
            if (!GateRimStorytellerUtility.IsGateRimStorytellerActive
                || !GoauldSystemLordFactionUtility.IsSystemLordFaction(
                    domainFaction))
            {
                return 1f;
            }

            GameComponent_GoauldInterDomainRelationTracker tracker =
                GameComponent_GoauldInterDomainRelationTracker.Current;

            if (tracker == null)
            {
                return 1f;
            }

            bool activeAlliance = false;
            List<GoauldInterDomainRelationState> states =
                tracker.Snapshot(includeInactive: false);

            foreach (GoauldInterDomainRelationState state in states)
            {
                if (state.firstDomain != domainFaction
                    && state.secondDomain != domainFaction)
                {
                    continue;
                }

                if (state.relation
                    == GoauldInterDomainRelation.OpenConflict)
                {
                    return OpenConflictNaturalRaidFactor;
                }

                if (state.relation == GoauldInterDomainRelation.Alliance)
                {
                    activeAlliance = true;
                }
            }

            return activeAlliance
                ? AllianceNaturalRaidFactor
                : 1f;
        }

        private static bool IsDomainInActiveRelation(
            Faction domainFaction,
            GoauldInterDomainRelation relation)
        {
            if (!GoauldSystemLordFactionUtility.IsSystemLordFaction(
                    domainFaction))
            {
                return false;
            }

            GameComponent_GoauldInterDomainRelationTracker tracker =
                GameComponent_GoauldInterDomainRelationTracker.Current;

            if (tracker == null)
            {
                return false;
            }

            foreach (GoauldInterDomainRelationState state in
                tracker.Snapshot(includeInactive: false))
            {
                if (state.relation == relation
                    && (state.firstDomain == domainFaction
                        || state.secondDomain == domainFaction))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
