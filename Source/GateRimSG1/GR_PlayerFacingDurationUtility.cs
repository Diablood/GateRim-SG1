using System;
using RimWorld;

namespace GateRimSG1
{
    /// <summary>
    /// Centralizes durations shown to players through RimWorld's localized
    /// vanilla formatter. Developer reports may continue to expose raw ticks.
    /// </summary>
    public static class GR_PlayerFacingDurationUtility
    {
        public static string Format(int ticks)
        {
            return Math.Max(0, ticks).ToStringTicksToPeriodVerbose(
                allowHours: true,
                allowQuadrums: true);
        }

        public static string FormatAtLeastOneHour(int ticks)
        {
            return Math.Max(GenDate.TicksPerHour, ticks)
                .ToStringTicksToPeriodVerbose(
                    allowHours: true,
                    allowQuadrums: true);
        }
    }
}
