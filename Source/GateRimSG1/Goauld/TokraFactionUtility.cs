using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Shared helper for the lazily created hidden Tok'ra faction instance.
    ///
    /// The Tok'ra remain disconnected from normal world generation while
    /// visitor and therapeutic incidents reuse one persistent runtime faction.
    /// </summary>
    internal static class TokraFactionUtility
    {
        public static Faction GetOrCreateHiddenFaction(string purpose)
        {
            if (GR_DefOf.SG1_Tokra == null
                || Find.FactionManager == null)
            {
                return null;
            }

            Faction existingFaction = Find.FactionManager.FirstFactionOfDef(
                GR_DefOf.SG1_Tokra);

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
                $"Created hidden Tok'ra faction instance "
                + $"{createdFaction.Name} ({createdFaction.loadID}) "
                + $"for {purpose}.");

            return createdFaction;
        }
    }
}
