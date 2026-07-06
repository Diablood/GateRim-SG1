using HarmonyLib;
using RimWorld;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Prevents the two exact domain factions in one active reinforcement
    /// attack from treating each other as targets. Their persistent vanilla
    /// relation remains untouched and becomes authoritative again afterwards.
    /// </summary>
    [HarmonyPatch(
        typeof(FactionUtility),
        nameof(FactionUtility.HostileTo),
        new[] { typeof(Faction), typeof(Faction) })]
    internal static class FactionUtility_GoauldAlliedReinforcementPatch
    {
        private static void Postfix(
            Faction __0,
            Faction __1,
            ref bool __result)
        {
            if (__result
                && GameComponent_GoauldAlliedReinforcementTracker
                    .AreTemporarilyCooperating(__0, __1))
            {
                __result = false;
            }
        }
    }
}
