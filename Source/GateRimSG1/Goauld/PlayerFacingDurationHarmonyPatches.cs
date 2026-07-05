using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Redirects legacy player-facing duration helpers to the shared vanilla
    /// formatter without changing any stored deadline or gameplay timing.
    /// </summary>
    [HarmonyPatch(
        typeof(WorldObject_TokraDecodedMissionSite),
        "GetRemainingDaysString")]
    internal static class Patch_DecodedMissionDuration
    {
        [HarmonyPrefix]
        private static bool Prefix(
            int ___ticksRemaining,
            ref string __result)
        {
            __result = GR_PlayerFacingDurationUtility.Format(
                ___ticksRemaining);
            return false;
        }
    }

    [HarmonyPatch(
        typeof(WorldObject_TokraDistressCallSite),
        "GetRemainingHoursString")]
    internal static class Patch_DistressCallSiteDuration
    {
        [HarmonyPrefix]
        private static bool Prefix(
            WorldObject_TokraDistressCallSite __instance,
            ref string __result)
        {
            __result = GR_PlayerFacingDurationUtility.Format(
                __instance.RemainingTicks);
            return false;
        }
    }

    [HarmonyPatch(
        typeof(WorldObject_TokraIntroductionArtifactSite),
        "GetRemainingHoursString")]
    internal static class Patch_IntroductionArtifactSiteDuration
    {
        [HarmonyPrefix]
        private static bool Prefix(
            WorldObject_TokraIntroductionArtifactSite __instance,
            ref string __result)
        {
            __result = GR_PlayerFacingDurationUtility.Format(
                __instance.RemainingTicks);
            return false;
        }
    }

    [HarmonyPatch(
        typeof(WorldObject_TokraTemporaryBaseDeliverySite),
        "GetRemainingHoursString")]
    internal static class Patch_TemporaryBaseDeliveryDuration
    {
        [HarmonyPrefix]
        private static bool Prefix(
            int ___deadlineTick,
            int ___finalExpiryTick,
            bool ___lateWindowStarted,
            ref string __result)
        {
            int currentTick = Find.TickManager?.TicksGame ?? 0;
            int targetTick = ___lateWindowStarted
                ? ___finalExpiryTick
                : ___deadlineTick;

            __result = GR_PlayerFacingDurationUtility.Format(
                Math.Max(0, targetTick - currentTick));
            return false;
        }
    }

    [HarmonyPatch(
        typeof(WorldObject_TokraJaffaOfficerCaptureSite),
        "GetRemainingHoursString")]
    internal static class Patch_JaffaOfficerCaptureDuration
    {
        [HarmonyPrefix]
        private static bool Prefix(
            WorldObject_TokraJaffaOfficerCaptureSite __instance,
            ref string __result)
        {
            __result = GR_PlayerFacingDurationUtility.Format(
                __instance.RemainingTicks);
            return false;
        }
    }

    [HarmonyPatch(
        typeof(WorldObject_TokraJaffaOfficerCaptureSite),
        "GetExtractionHoursString")]
    internal static class Patch_JaffaOfficerExtractionDuration
    {
        [HarmonyPrefix]
        private static bool Prefix(
            int ___extractionArrivalTick,
            ref string __result)
        {
            int currentTick = Find.TickManager?.TicksGame ?? 0;
            int remainingTicks = Math.Max(
                0,
                ___extractionArrivalTick - currentTick);

            __result = GR_PlayerFacingDurationUtility
                .FormatAtLeastOneHour(remainingTicks);
            return false;
        }
    }

    [HarmonyPatch(
        typeof(WorldObject_GoauldOpenConflictBattlefieldSite),
        "GetRemainingDurationString")]
    internal static class Patch_GoauldWorldBattlefieldDuration
    {
        [HarmonyPrefix]
        private static bool Prefix(
            WorldObject_GoauldOpenConflictBattlefieldSite __instance,
            ref string __result)
        {
            __result = GR_PlayerFacingDurationUtility.Format(
                __instance.RemainingTicks);
            return false;
        }
    }

    [HarmonyPatch(
        typeof(MapComponent_TokraRelaySabotageMission),
        nameof(MapComponent_TokraRelaySabotageMission
            .FormatRemainingReinforcementTime))]
    internal static class Patch_RelayReinforcementDuration
    {
        [HarmonyPrefix]
        private static bool Prefix(
            MapComponent_TokraRelaySabotageMission __instance,
            ref string __result)
        {
            __result = GR_PlayerFacingDurationUtility.Format(
                __instance.RemainingReinforcementTicks);
            return false;
        }
    }
}
