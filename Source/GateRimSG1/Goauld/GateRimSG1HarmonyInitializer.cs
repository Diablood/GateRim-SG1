using System;
using HarmonyLib;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Centralized Harmony bootstrap for small UI integrations that have no
    /// equivalent Def hook in RimWorld.
    /// </summary>
    internal static class GateRimSG1HarmonyInitializer
    {
        private const string HarmonyId = "diablood.gaterimsg1";

        private static bool applied;

        public static void Apply()
        {
            if (applied)
            {
                return;
            }

            try
            {
                Harmony harmony = new Harmony(HarmonyId);
                harmony.PatchAll(
                    typeof(GateRimSG1HarmonyInitializer).Assembly);
                applied = true;
            }
            catch (Exception exception)
            {
                GR_Log.Error(
                    "Failed to apply GateRim SG-1 Harmony patches: "
                    + exception);
            }
        }
    }
}
