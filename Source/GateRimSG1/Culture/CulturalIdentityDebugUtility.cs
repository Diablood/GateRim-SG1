using System.Collections.Generic;
using System.Linq;
using System.Text;
using GateRimSG1.Goauld;
using GateRimSG1.Jaffa;
using GateRimSG1.Names;
using GateRimSG1.Social;
using RimWorld;
using Verse;

namespace GateRimSG1.Culture
{
    /// <summary>
    /// Builds one read-only technical report from the culture, naming,
    /// backstory, Jaffa and symbiote services already used by gameplay.
    /// </summary>
    public static class CulturalIdentityDebugUtility
    {
        public static string BuildReport(Pawn pawn)
        {
            if (pawn == null)
            {
                return "GR_CulturalIdentity_NoPawnSelected".Translate().ToString();
            }

            StringBuilder report = new StringBuilder();
            report.AppendLine(
                "GR_CulturalIdentity_ReportTitle".Translate(
                    pawn.LabelShortCap).ToString());
            report.AppendLine();

            AppendValue(report, "ThingID", pawn.ThingID);
            AppendValue(report, "Name", pawn.Name?.ToStringFull);
            AppendValue(report, "Race", pawn.def?.defName);
            AppendValue(report, "Xenotype", pawn.genes?.Xenotype?.defName);
            AppendValue(report, "PawnKindDef", pawn.kindDef?.defName);
            AppendValue(report, "Faction", pawn.Faction?.def?.defName);
            AppendValue(
                report,
                "Childhood",
                pawn.story?.Childhood?.defName);
            AppendValue(
                report,
                "Adulthood",
                pawn.story?.Adulthood?.defName);

            report.AppendLine();
            AppendProfileContext(
                report,
                pawn,
                PawnGenerationContext.NonPlayer,
                "NonPlayer");
            AppendProfileContext(
                report,
                pawn,
                PawnGenerationContext.PlayerStarter,
                "PlayerStarter");

            report.AppendLine();
            report.AppendLine("Jaffa state");
            AppendValue(
                report,
                "Compatible physiology",
                JaffaPrimtaUtility.IsCompatibleJaffa(pawn));
            AppendValue(
                report,
                "Carries Prim'ta",
                JaffaPrimtaUtility.HasPrimta(pawn));
            AppendValue(
                report,
                "Forehead mark",
                JaffaForeheadMarkUtility.MarkFor(pawn)?.defName);

            report.AppendLine();
            report.AppendLine("Contextual social identity");
            AppendValue(
                report,
                "Free Jaffa",
                ContextualSocialIdentityUtility.IsFreeJaffa(pawn));
            AppendValue(
                report,
                "Goa'uld-domain Jaffa",
                ContextualSocialIdentityUtility.IsGoauldDomainJaffa(pawn));
            AppendValue(
                report,
                "Marked Jaffa",
                ContextualSocialIdentityUtility.IsMarkedJaffa(pawn));
            AppendValue(
                report,
                "Active Goa'uld host",
                ContextualSocialIdentityUtility.IsActiveGoauldHost(pawn));
            AppendValue(
                report,
                "Active Tok'ra host",
                ContextualSocialIdentityUtility.IsActiveTokraHost(pawn));
            AppendValue(
                report,
                "System Lord host",
                ContextualSocialIdentityUtility.IsSystemLordHost(pawn));
            AppendValue(
                report,
                "Nearby System Lord",
                ContextualSocialIdentityUtility.HasNearbySystemLord(pawn));

            report.AppendLine();
            report.AppendLine("Persistent symbiote identity");
            HediffComp_GoauldSymbiote symbioteComp = FindSymbioteComp(pawn);
            AppendValue(
                report,
                "Host symbiote Hediff",
                symbioteComp?.parent?.def?.defName);
            AppendValue(
                report,
                "Data",
                symbioteComp?.SymbioteData?.ToDebugString());

            return report.ToString().TrimEndNewlines();
        }

        private static void AppendProfileContext(
            StringBuilder report,
            Pawn pawn,
            PawnGenerationContext context,
            string contextLabel)
        {
            List<CulturalPawnProfileDef> matches =
                DefDatabase<CulturalPawnProfileDef>
                    .AllDefsListForReading
                    .Where(profile => profile != null
                        && profile.Matches(pawn, context))
                    .OrderByDescending(profile => profile.priority)
                    .ThenBy(profile => profile.defName)
                    .ToList();

            CulturalPawnProfileDef selected =
                CulturalProfileResolver.ResolveProfile(pawn, context);
            CulturalPawnNameGroup nameGroup =
                CulturalProfileResolver.ResolveNameGroup(pawn, context);

            report.AppendLine($"Cultural profiles ({contextLabel})");
            AppendValue(
                report,
                "Matches",
                matches.Count == 0
                    ? null
                    : string.Join(
                        ", ",
                        matches.Select(profile =>
                            $"{profile.defName}[priority={profile.priority}]")));
            AppendValue(report, "Selected", selected?.defName);
            AppendValue(report, "Name group", nameGroup);
        }

        private static HediffComp_GoauldSymbiote FindSymbioteComp(Pawn pawn)
        {
            List<Hediff> hediffs = pawn?.health?.hediffSet?.hediffs;
            if (hediffs == null)
            {
                return null;
            }

            for (int index = 0; index < hediffs.Count; index++)
            {
                HediffComp_GoauldSymbiote comp =
                    (hediffs[index] as HediffWithComps)
                    ?.GetComp<HediffComp_GoauldSymbiote>();
                if (comp != null)
                {
                    return comp;
                }
            }

            return null;
        }

        private static void AppendValue(
            StringBuilder report,
            string label,
            object value)
        {
            string text = value?.ToString();
            report.Append(label);
            report.Append(": ");
            report.AppendLine(text.NullOrEmpty() ? "<none>" : text);
        }
    }
}
