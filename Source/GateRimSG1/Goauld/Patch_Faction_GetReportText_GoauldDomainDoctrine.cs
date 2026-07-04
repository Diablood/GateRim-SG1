using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Adds the domain's qualitative strategic preference to the normal
    /// faction report without exposing internal weights or hidden thresholds.
    /// </summary>
    [StaticConstructorOnStartup]
    public static class Patch_Faction_GetReportText_GoauldDomainDoctrine
    {
        private const string HarmonyId =
            "diablood.gaterimsg1.goauld-domain-doctrine-report";

        static Patch_Faction_GetReportText_GoauldDomainDoctrine()
        {
            MethodInfo getter = AccessTools.PropertyGetter(
                typeof(Faction),
                nameof(Faction.GetReportText));

            if (getter == null)
            {
                GR_Log.Error(
                    "Could not find Faction.GetReportText for the "
                    + "Goa'uld domain-doctrine report patch.");
                return;
            }

            Harmony harmony = new Harmony(HarmonyId);
            harmony.Patch(
                getter,
                postfix: new HarmonyMethod(
                    typeof(
                        Patch_Faction_GetReportText_GoauldDomainDoctrine),
                    nameof(AppendDoctrineReport)));
        }

        private static void AppendDoctrineReport(
            Faction __instance,
            ref string __result)
        {
            if (!GoauldSystemLordFactionUtility
                .IsSystemLordFaction(__instance))
            {
                return;
            }

            GameComponent_GoauldDomainDoctrineTracker tracker =
                GameComponent_GoauldDomainDoctrineTracker.Current;

            if (tracker == null
                || !tracker.TryGetProfile(
                    __instance,
                    out GoauldDomainDoctrineProfileDef profile)
                || profile == null)
            {
                return;
            }

            string doctrineText =
                "GR_GoauldDomainDoctrine_ReportText".Translate(
                    GoauldDomainDoctrineProfileUtility
                        .GetDisplayLabel(profile),
                    GoauldDomainDoctrineProfileUtility
                        .GetDisplayDescription(profile));

            if (__result.NullOrEmpty())
            {
                __result = doctrineText;
                return;
            }

            __result = __result.TrimEnd()
                + "\n\n"
                + doctrineText;
        }
    }
}
