using HarmonyLib;
using RimWorld;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Prevents the two exact domain factions in one active reinforcement
    /// attack from treating each other as targets. Persistent GateRim
    /// diplomacy is also synchronized to the matching vanilla relation; this
    /// narrow fallback protects an already active cooperative force while its
    /// exact runtime state is being reconciled.
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
