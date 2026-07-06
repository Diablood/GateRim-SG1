using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Shared helper for Goa'uld System Lord world-faction instances.
    ///
    /// New worlds propose three visible domains by default while retaining one
    /// required baseline that players may select in the vanilla faction list.
    /// The utility also
    /// supports worlds configured with several instances and keeps the lazy
    /// runtime fallback used by older saves and isolated developer tests.
    /// </summary>
    internal static class GoauldSystemLordFactionUtility
    {
        public static List<Faction> GetAllFactions(
            bool includeDefeated = false)
        {
            if (Find.FactionManager?.AllFactionsListForReading == null
                || GR_DefOf.SG1_GoauldSystemLordPrototype == null)
            {
                return new List<Faction>();
            }

            return Find.FactionManager.AllFactionsListForReading
                .Where(faction =>
                    IsSystemLordFaction(faction)
                    && (includeDefeated || !faction.defeated))
                .OrderBy(faction => faction.loadID)
                .ToList();
        }

        public static Faction SelectRandomActiveFaction()
        {
            List<Faction> factions = GetAllFactions();

            return factions.Count == 0
                ? null
                : factions.RandomElement();
        }

        public static Faction GetOrCreateFaction(string purpose)
        {
            if (GR_DefOf.SG1_GoauldSystemLordPrototype == null
                || Find.FactionManager == null)
            {
                return null;
            }

            Faction existingFaction = GetAllFactions()
                .FirstOrDefault()
                ?? GetAllFactions(includeDefeated: true)
                    .FirstOrDefault();

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

            GameComponent_GoauldDomainDoctrineTracker.Current
                ?.GetOrAssignProfile(createdFaction);

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

        public static void NotifyTemporaryCooperationChanged(
            Faction firstFaction,
            Faction secondFaction)
        {
            if (firstFaction == null
                || secondFaction == null
                || Find.Maps == null)
            {
                return;
            }

            foreach (Map map in Find.Maps)
            {
                map?.attackTargetsCache
                    ?.Notify_FactionHostilityChanged(
                        firstFaction,
                        secondFaction);
            }
        }

        private static void RefreshAttackTargetCaches(
            Faction goauldFaction)
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
                    factionIndex
                        < Find.FactionManager
                            .AllFactionsListForReading.Count;
                    factionIndex++)
                {
                    Faction otherFaction =
                        Find.FactionManager
                            .AllFactionsListForReading[factionIndex];

                    if (otherFaction == null
                        || otherFaction == goauldFaction)
                    {
                        continue;
                    }

                    map.attackTargetsCache
                        .Notify_FactionHostilityChanged(
                            goauldFaction,
                            otherFaction);
                }
            }
        }
    }
}
