using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Shared helper for the lazily created hidden Goa'uld System Lord
    /// prototype faction instance.
    ///
    /// The faction remains disconnected from normal world generation.
    /// Controlled developer incidents may create one persistent runtime
    /// instance so vanilla raid generation can be tested without enabling
    /// storyteller raids, settlements or traders.
    /// </summary>
    internal static class GoauldSystemLordFactionUtility
    {
        public static Faction GetOrCreateHiddenFaction(string purpose)
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
                return existingFaction;
            }

            Faction createdFaction = FactionGenerator.NewGeneratedFaction(
                new FactionGeneratorParms(
                    GR_DefOf.SG1_GoauldSystemLordPrototype,
                    default(IdeoGenerationParms),
                    hidden: true));

            Find.FactionManager.Add(createdFaction);

            return createdFaction;
        }
    }
}
