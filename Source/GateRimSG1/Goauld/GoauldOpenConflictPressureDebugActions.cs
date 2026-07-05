using System.Text;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public static class GoauldOpenConflictPressureDebugActions
    {
        public static void ShowReport()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Goa'uld open-conflict raid-pressure report");
            builder.AppendLine();
            builder.AppendLine(
                "SG-1 storyteller active: "
                + (GateRimSG1.Storytelling.GateRimStorytellerUtility
                    .IsGateRimStorytellerActive
                    ? "yes"
                    : "no"));
            builder.AppendLine(
                "open-conflict natural raid factor: "
                + $"{GoauldOpenConflictPressureUtility.NaturalRaidFactor * 100f:0}%");
            builder.AppendLine();

            var domains = GoauldSystemLordFactionUtility.GetAllFactions();

            if (domains.Count == 0)
            {
                builder.AppendLine("No active Goa'uld domain exists.");
            }
            else
            {
                foreach (Faction domain in domains)
                {
                    bool openConflict = GoauldOpenConflictPressureUtility
                        .IsDomainInActiveOpenConflict(domain);
                    float factor = GoauldOpenConflictPressureUtility
                        .ResolveNaturalRaidPressureFactor(domain);

                    builder.AppendLine(domain.Name ?? "<missing domain>");
                    builder.AppendLine(
                        "  active open conflict: "
                        + (openConflict ? "yes" : "no"));
                    builder.AppendLine(
                        "  ordinary natural raid factor: "
                        + $"{factor * 100f:0}%");
                }
            }

            builder.AppendLine();
            builder.AppendLine(
                "Forced raids, extraction reprisals, intercepted threats "
                + "and mission attacks remain at 100%.");

            Find.WindowStack.Add(
                new Dialog_MessageBox(builder.ToString()));
        }
    }
}
