using System.Text;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Shared resolver for the optional Tok'ra world-faction instance.
    ///
    /// World creation selects one hidden, settlement-free instance by default.
    /// If the player removes the faction from the world-faction list, this
    /// helper deliberately returns null and Tok'ra content remains disabled.
    /// It never fabricates a replacement faction at runtime.
    /// </summary>
    internal static class TokraFactionUtility
    {
        private const int DuplicateFactionWarningKey = 73492017;

        public static bool HasPersistentFaction()
        {
            return GetExistingFaction() != null;
        }

        public static Faction GetPersistentFaction(string purpose)
        {
            if (GR_DefOf.SG1_Tokra == null
                || Find.FactionManager == null)
            {
                return null;
            }

            Faction existingFaction = GetExistingFaction();
            WarnAboutDuplicateFactions(CountExistingFactions());
            return existingFaction;
        }

        /// <summary>
        /// Historical method name retained for source compatibility.
        /// Despite its name, optional-world behavior means it only resolves an
        /// existing selected faction and never creates one.
        /// </summary>
        public static Faction GetOrCreatePersistentFaction(string purpose)
        {
            return GetPersistentFaction(purpose);
        }

        /// <summary>
        /// Historical alias retained for older local patches. It resolves only.
        /// </summary>
        public static Faction GetOrCreateHiddenFaction(string purpose)
        {
            return GetPersistentFaction(purpose);
        }

        public static string GetWorldPresenceAuditReport()
        {
            StringBuilder report = new StringBuilder();
            FactionDef factionDef = GR_DefOf.SG1_Tokra;
            int instanceCount = CountExistingFactions();
            Faction existingFaction = GetExistingFaction();

            report.AppendLine("Tok'ra world-selection audit");
            report.AppendLine();
            report.AppendLine(
                $"FactionDef loaded: {FormatBoolean(factionDef != null)}");

            if (factionDef != null)
            {
                report.AppendLine(
                    $"Hidden in normal diplomacy list: "
                    + FormatBoolean(factionDef.hidden));
                report.AppendLine(
                    "Displayed in world faction selection: "
                    + FormatBoolean(factionDef.displayInFactionSelection));
                report.AppendLine(
                    "Default selected count: "
                    + factionDef.startingCountAtWorldCreation);
                report.AppendLine(
                    "Maximum configurable count: "
                    + factionDef.maxConfigurableAtWorldCreation);
                report.AppendLine(
                    "Mandatory count outside selection: "
                    + factionDef.requiredCountAtGameStart);
                report.AppendLine(
                    "Settlement generation weight: "
                    + factionDef.settlementGenerationWeight);
            }

            report.AppendLine();
            report.AppendLine($"Saved faction instances: {instanceCount}");
            report.AppendLine(
                existingFaction != null
                    ? $"Tok'ra content enabled: yes ({existingFaction.Name}, "
                        + $"{existingFaction.loadID})"
                    : "Tok'ra content enabled: no");
            report.AppendLine();
            report.AppendLine(
                "Selection rule: the faction is present once by default. If "
                + "the player removes it during world creation, no runtime "
                + "reconciliation recreates it and Tok'ra-generated content "
                + "remains disabled for that game.");

            if (instanceCount > 1)
            {
                report.AppendLine();
                report.AppendLine(
                    "Warning: multiple saved Tok'ra faction instances exist. "
                    + "The first one is reused; duplicates are not deleted "
                    + "automatically to avoid damaging save references.");
            }

            return report.ToString().TrimEnd();
        }

        private static Faction GetExistingFaction()
        {
            return GR_DefOf.SG1_Tokra == null
                || Find.FactionManager == null
                    ? null
                    : Find.FactionManager.FirstFactionOfDef(
                        GR_DefOf.SG1_Tokra);
        }

        private static int CountExistingFactions()
        {
            if (GR_DefOf.SG1_Tokra == null
                || Find.FactionManager?.AllFactionsListForReading == null)
            {
                return 0;
            }

            int count = 0;
            for (int factionIndex = 0;
                factionIndex
                    < Find.FactionManager.AllFactionsListForReading.Count;
                factionIndex++)
            {
                Faction faction =
                    Find.FactionManager.AllFactionsListForReading[
                        factionIndex];

                if (faction?.def == GR_DefOf.SG1_Tokra)
                {
                    count++;
                }
            }

            return count;
        }

        private static void WarnAboutDuplicateFactions(int existingCount)
        {
            if (existingCount <= 1)
            {
                return;
            }

            GR_Log.WarningOnce(
                $"Detected {existingCount} saved Tok'ra faction instances. "
                + "GateRim SG-1 will reuse the first instance and will not "
                + "delete duplicates automatically because world objects or "
                + "pawns may still reference them.",
                DuplicateFactionWarningKey);
        }

        private static string FormatBoolean(bool value)
        {
            return value ? "yes" : "no";
        }
    }
}
