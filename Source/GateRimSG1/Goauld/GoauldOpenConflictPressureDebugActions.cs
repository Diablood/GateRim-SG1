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
            builder.AppendLine("Goa'uld inter-domain raid-pressure report");
            builder.AppendLine();
            builder.AppendLine(
                "SG-1 storyteller active: "
                + (GateRimSG1.Storytelling.GateRimStorytellerUtility
                    .IsGateRimStorytellerActive
                    ? "yes"
                    : "no"));
            builder.AppendLine(
                "open-conflict natural raid factor: "
                + $"{GoauldOpenConflictPressureUtility.OpenConflictNaturalRaidFactor * 100f:0}%");
            builder.AppendLine(
                "alliance natural raid factor: "
                + $"{GoauldOpenConflictPressureUtility.AllianceNaturalRaidFactor * 100f:0}%");
            builder.AppendLine(
                "precedence: open conflict overrides alliance");
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
                    bool alliance = GoauldOpenConflictPressureUtility
                        .IsDomainInActiveAlliance(domain);
                    float factor = GoauldOpenConflictPressureUtility
                        .ResolveNaturalRaidPressureFactor(domain);

                    builder.AppendLine(domain.Name ?? "<missing domain>");
                    builder.AppendLine(
                        "  active open conflict: "
                        + (openConflict ? "yes" : "no"));
                    builder.AppendLine(
                        "  active alliance: "
                        + (alliance ? "yes" : "no"));
                    builder.AppendLine(
                        "  ordinary natural raid factor: "
                        + $"{factor * 100f:0}%");
                }
            }

            builder.AppendLine();
            builder.AppendLine(
                "Several conflicts or alliances never stack. Forced raids, "
                + "extraction reprisals, intercepted threats and mission "
                + "attacks remain at 100% unless the dedicated relation-"
                + "pressure test is used.");

            Find.WindowStack.Add(
                new Dialog_MessageBox(builder.ToString()));
        }
    }
}
