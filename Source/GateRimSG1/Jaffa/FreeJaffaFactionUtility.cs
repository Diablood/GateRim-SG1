using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Jaffa
{
    /// <summary>
    /// Shared selection helper for visible Free Jaffa world factions.
    ///
    /// New worlds normally contain one Free Jaffa faction, while players may
    /// configure additional communities during world creation. Peaceful
    /// encounters select one existing non-hostile community at random and do
    /// not fabricate a hidden fallback faction for old saves.
    /// </summary>
    internal static class FreeJaffaFactionUtility
    {
        public static bool HasExistingNonHostileFaction()
        {
            Faction unusedFaction;
            return TryGetRandomNonHostileFaction(out unusedFaction);
        }

        public static bool TryGetRandomNonHostileFaction(out Faction faction)
        {
            faction = null;

            if (GR_DefOf.SG1_FreeJaffa == null
                || Find.FactionManager?.AllFactionsListForReading == null
                || Faction.OfPlayer == null)
            {
                return false;
            }

            List<Faction> candidates = new List<Faction>();

            for (int factionIndex = 0;
                factionIndex < Find.FactionManager.AllFactionsListForReading.Count;
                factionIndex++)
            {
                Faction candidate =
                    Find.FactionManager.AllFactionsListForReading[factionIndex];

                if (!IsEligibleNonHostileFaction(candidate))
                {
                    continue;
                }

                candidates.Add(candidate);
            }

            if (candidates.Count == 0)
            {
                return false;
            }

            faction = candidates.RandomElement();
            return true;
        }

        private static bool IsEligibleNonHostileFaction(Faction faction)
        {
            return faction?.def == GR_DefOf.SG1_FreeJaffa
                && faction != Faction.OfPlayer
                && !faction.HostileTo(Faction.OfPlayer)
                && !Faction.OfPlayer.HostileTo(faction);
        }
    }
}
