using System.Linq;
using GateRimSG1.Storytelling;
using RimWorld;

namespace GateRimSG1.Goauld
{
    public static class GoauldOpenConflictPressureUtility
    {
        public const float NaturalRaidFactor = 0.75f;

        public static bool IsDomainInActiveOpenConflict(
            Faction domainFaction)
        {
            if (!GoauldSystemLordFactionUtility.IsSystemLordFaction(
                    domainFaction))
            {
                return false;
            }

            GameComponent_GoauldInterDomainRelationTracker tracker =
                GameComponent_GoauldInterDomainRelationTracker.Current;

            return tracker != null
                && tracker.Snapshot(includeInactive: false).Any(state =>
                    state.relation
                        == GoauldInterDomainRelation.OpenConflict
                    && (state.firstDomain == domainFaction
                        || state.secondDomain == domainFaction));
        }

        public static float ResolveNaturalRaidPressureFactor(
            Faction domainFaction)
        {
            if (!GateRimStorytellerUtility.IsGateRimStorytellerActive)
            {
                return 1f;
            }

            return IsDomainInActiveOpenConflict(domainFaction)
                ? NaturalRaidFactor
                : 1f;
        }
    }
}
