using HarmonyLib;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Complements the vanilla xenotype percentages with acquired Goa'uld
    /// host castes without changing the faction's generation xenotype set.
    /// </summary>
    [HarmonyPatch(
        typeof(FactionDef),
        nameof(FactionDef.Description),
        MethodType.Getter)]
    internal static class GoauldFactionCasteSummaryPatch
    {
        private static void Postfix(
            FactionDef __instance,
            ref string __result)
        {
            if (__instance != GR_DefOf.SG1_GoauldSystemLordPrototype)
            {
                return;
            }

            string title = "GR_GoauldFactionCasteSummary_Title"
                .Translate()
                .ToString()
                .AsTipTitle();
            string summary = "GR_GoauldFactionCasteSummary_Body"
                .Translate()
                .ToString();
            string prefix = string.IsNullOrWhiteSpace(__result)
                ? string.Empty
                : __result.TrimEnd() + "\n\n";

            __result = prefix + title + "\n" + summary;
        }
    }
}
