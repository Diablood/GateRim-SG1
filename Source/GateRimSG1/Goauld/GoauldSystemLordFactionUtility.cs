using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Shared helper for the Goa'uld System Lord world-faction instance.
    ///
    /// New worlds generate one visible faction normally. Controlled developer
    /// incidents still retain a lazy runtime fallback for older saves or
    /// isolated regression tests that do not yet contain the world faction.
    /// </summary>
    internal static class GoauldSystemLordFactionUtility
    {
        public static Faction GetOrCreateFaction(string purpose)
        {
            if (GR_DefOf.SG1_GoauldSystemLordPrototype == null
                || Find.FactionManager == null)
            {
                return null;
            }

            Faction existingFaction = Find.FactionManager.FirstFactionOfDef(
                GR_DefOf.SG1_GoauldSystemLordPrototype);

            if (existingFaction != null)
            {
                RefreshAttackTargetCaches(existingFaction);
                return existingFaction;
            }

            Faction createdFaction = FactionGenerator.NewGeneratedFaction(
                new FactionGeneratorParms(
                    GR_DefOf.SG1_GoauldSystemLordPrototype,
                    default(IdeoGenerationParms),
                    hidden: false));

            Find.FactionManager.Add(createdFaction);
            RefreshAttackTargetCaches(createdFaction);

            return createdFaction;
        }

        public static Faction ResolveFaction(
            Faction preferredFaction,
            string purpose)
        {
            if (IsSystemLordFaction(preferredFaction))
            {
                RefreshAttackTargetCaches(preferredFaction);
                return preferredFaction;
            }

            return GetOrCreateFaction(purpose);
        }

        public static bool IsSystemLordFaction(Faction faction)
        {
            return faction != null
                && faction.def == GR_DefOf.SG1_GoauldSystemLordPrototype;
        }

        private static void RefreshAttackTargetCaches(Faction goauldFaction)
        {
            if (goauldFaction == null
                || Find.FactionManager == null
                || Find.Maps == null)
            {
                return;
            }

            for (int mapIndex = 0; mapIndex < Find.Maps.Count; mapIndex++)
            {
                Map map = Find.Maps[mapIndex];

                if (map?.attackTargetsCache == null)
                {
                    continue;
                }

                for (int factionIndex = 0;
                    factionIndex < Find.FactionManager.AllFactionsListForReading.Count;
                    factionIndex++)
                {
                    Faction otherFaction =
                        Find.FactionManager.AllFactionsListForReading[factionIndex];

                    if (otherFaction == null || otherFaction == goauldFaction)
                    {
                        continue;
                    }

                    map.attackTargetsCache.Notify_FactionHostilityChanged(
                        goauldFaction,
                        otherFaction);
                }
            }
        }
    }
}
