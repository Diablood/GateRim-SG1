using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Shared helper for the persistent hidden Tok'ra world-faction instance.
    ///
    /// New games receive one instance through the Tok'ra FactionDef. Older
    /// saves receive the same presence through
    /// GameComponent_TokraWorldPresenceInitializer.
    ///
    /// All Tok'ra incidents reuse the first saved SG1_Tokra faction instead of
    /// creating event-specific factions.
    /// </summary>
    internal static class TokraFactionUtility
    {
        public static bool HasPersistentFaction()
        {
            return GetExistingFaction() != null;
        }

        public static Faction GetOrCreatePersistentFaction(string purpose)
        {
            if (GR_DefOf.SG1_Tokra == null
                || Find.FactionManager == null)
            {
                return null;
            }

            Faction existingFaction = GetExistingFaction();

            if (existingFaction != null)
            {
                return existingFaction;
            }

            Faction createdFaction = FactionGenerator.NewGeneratedFaction(
                new FactionGeneratorParms(
                    GR_DefOf.SG1_Tokra,
                    default(IdeoGenerationParms),
                    hidden: true));

            Find.FactionManager.Add(createdFaction);

            GR_Log.Message(
                $"Created persistent hidden Tok'ra world-faction instance "
                + $"{createdFaction.Name} ({createdFaction.loadID}) "
                + $"for {purpose}.");

            return createdFaction;
        }

        /// <summary>
        /// Historical alias retained for source compatibility with older local
        /// patches. New code should use GetOrCreatePersistentFaction.
        /// </summary>
        public static Faction GetOrCreateHiddenFaction(string purpose)
        {
            return GetOrCreatePersistentFaction(purpose);
        }

        private static Faction GetExistingFaction()
        {
            return GR_DefOf.SG1_Tokra == null
                || Find.FactionManager == null
                    ? null
                    : Find.FactionManager.FirstFactionOfDef(
                        GR_DefOf.SG1_Tokra);
        }
    }
}
