using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using HarmonyLib;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Converts legacy numeric duration arguments inside known GateRim
    /// translation keys. This compatibility layer lets older operation and
    /// communicator code keep its stored ticks and call structure while the
    /// final player-facing text uses RimWorld's localized period formatter.
    /// </summary>
    [HarmonyPatch]
    internal static class Patch_PlayerFacingDurationTranslationArguments
    {
        private enum LegacyDurationUnit
        {
            RoundedHours,
            Days
        }

        private readonly struct DurationArgumentSpec
        {
            public DurationArgumentSpec(
                int argumentIndex,
                LegacyDurationUnit unit)
            {
                ArgumentIndex = argumentIndex;
                Unit = unit;
            }

            public int ArgumentIndex { get; }
            public LegacyDurationUnit Unit { get; }
        }

        private static readonly Dictionary<string, DurationArgumentSpec>
            DurationArguments = new Dictionary<string, DurationArgumentSpec>
            {
                // Shared organic-operation observation and intelligence flows.
                { "GR_MissionFramework_ObservationOfferText2", Hours(0) },
                { "GR_MissionFramework_ObservationOfferText3", Hours(0) },
                { "GR_TokraOrganicOperation_ObservationInProgress", Hours(0) },
                { "GR_TokraOrganicOperation_OfferLetterText", Hours(0) },
                { "GR_TokraOrganicOperation_Accepted", Hours(1) },
                { "GR_TokraObservation_Accepted", Hours(1) },
                { "GR_TokraObservation_TargetLetterText", Hours(0) },
                { "GR_TokraObservation_Deployed", Hours(1) },
                { "GR_TokraObservation_StatusAwaitingDeployment", Hours(0) },
                { "GR_TokraObservation_StatusRecording", Hours(0) },
                { "GR_TokraObservation_StatusDataReady", Hours(0) },
                { "GR_TokraObservation_StatusTransmissionInterrupted", Hours(0) },
                { "GR_TokraOrganicOperation_DeadDropOfferLetterText", Hours(0) },
                { "GR_TokraOrganicOperation_DeadDropOfferLetterText2", Hours(0) },
                { "GR_TokraOrganicOperation_DeadDropOfferLetterText3", Hours(0) },
                { "GR_TokraOrganicOperation_DeadDropAccepted", Hours(1) },
                { "GR_TokraOrganicOperation_DeadDropLocatedLetterText", Hours(0) },
                { "GR_TokraOrganicOperation_StatusOffered", Hours(0) },
                { "GR_TokraOrganicOperation_StatusObserving", Hours(0) },
                { "GR_TokraOrganicOperation_StatusReady", Hours(0) },
                { "GR_TokraOrganicOperation_StatusDeadDropOffered", Hours(0) },
                { "GR_TokraOrganicOperation_StatusDeadDropActive", Hours(0) },
                { "GR_TokraOrganicOperation_StatusIntelligenceAwaitingAnalysis", Hours(0) },
                { "GR_TokraOrganicOperation_StatusIntelligenceAnalyzing", Hours(1) },

                // Organic wounded-agent care.
                { "GR_TokraWoundedAgent_OfferText", Hours(0) },
                { "GR_TokraWoundedAgent_OfferText2", Hours(0) },
                { "GR_TokraWoundedAgent_OfferText3", Hours(0) },
                { "GR_TokraWoundedAgent_ArrivalText", Hours(1) },
                { "GR_TokraWoundedAgent_StatusOffered", Hours(0) },
                { "GR_TokraWoundedAgent_StatusAwaitingCare", Hours(1) },
                { "GR_TokraWoundedAgent_StatusCare", Hours(1) },

                // Organic medical-supply handoff.
                { "GR_TokraMedicalSupply_OfferText", Hours(0) },
                { "GR_TokraMedicalSupply_OfferText2", Hours(0) },
                { "GR_TokraMedicalSupply_OfferText3", Hours(0) },
                { "GR_TokraMedicalSupply_Accepted", Hours(1) },
                { "GR_TokraMedicalSupply_ArrivalText", Hours(1) },
                { "GR_TokraMedicalSupply_StatusOffered", Hours(0) },
                { "GR_TokraMedicalSupply_StatusAwaitingArrival", Hours(0) },
                { "GR_TokraMedicalSupply_StatusApproaching", Hours(1) },
                { "GR_TokraMedicalSupply_StatusReady", Hours(1) },

                // Organic diversion assault.
                { "GR_TokraDecoyDefense_OfferText1", Hours(0) },
                { "GR_TokraDecoyDefense_OfferText2", Hours(0) },
                { "GR_TokraDecoyDefense_OfferText3", Hours(0) },

                // Distress-call offer and shared status surfaces.
                { "GR_TokraDistressCall_OfferText1", Hours(0) },
                { "GR_TokraDistressCall_OfferText2", Hours(0) },
                { "GR_TokraDistressCall_OfferText3", Hours(0) },
                { "GR_TokraDistressCall_Accepted", Hours(1) },
                { "GR_TokraDistressCall_TargetLetterText", Hours(0) },
                { "GR_TokraDistressCall_StatusActive", Hours(0) },

                // Temporary-base delivery offers and scheduler status.
                { "GR_TokraTemporaryBaseDelivery_OfferText1", Hours(3) },
                { "GR_TokraTemporaryBaseDelivery_OfferText2", Hours(3) },
                { "GR_TokraTemporaryBaseDelivery_OfferText3", Hours(3) },
                { "GR_TokraTemporaryBaseDelivery_Accepted", Hours(4) },
                { "GR_TokraTemporaryBaseDelivery_TargetLetterText", Hours(3) },
                { "GR_TokraTemporaryBaseDelivery_StatusOffered", Hours(3) },
                { "GR_TokraTemporaryBaseDelivery_StatusActive", Hours(3) },
                { "GR_TokraTemporaryBaseDelivery_StatusLate", Hours(3) },
                { "GR_TokraTemporaryBaseDelivery_LateWarningText", Hours(2) },

                // Living Jaffa-officer capture and extraction messaging.
                { "GR_TokraJaffaOfficerCapture_OfferText1", Hours(0) },
                { "GR_TokraJaffaOfficerCapture_OfferText2", Hours(0) },
                { "GR_TokraJaffaOfficerCapture_OfferText3", Hours(0) },
                { "GR_TokraJaffaOfficerCapture_Accepted", Hours(1) },
                { "GR_TokraJaffaOfficerCapture_TargetLetterText", Hours(0) },
                { "GR_TokraJaffaOfficerCapture_StatusOffered", Hours(0) },
                { "GR_TokraJaffaOfficerCapture_StatusActive", Hours(1) },
                { "GR_TokraJaffaOfficerCapture_StatusReady", Hours(1) },
                { "GR_TokraJaffaOfficerCapture_HandoffStarted", Hours(1) },
                { "GR_TokraJaffaOfficerCapture_InspectReturn", Hours(1) },
                { "GR_TokraJaffaOfficerCapture_InspectHandoff", Hours(1) },
                { "GR_TokraJaffaOfficerCapture_ExtractionRequested", Hours(1) },

                // Trusted communicator cooldowns and first-mission delays.
                { "GR_TokraSecureCommunicator_DebriefRequested", Days(3) },
                { "GR_TokraSecureCommunicator_DebriefDialog", Days(3) },
                { "GR_TokraSecureCommunicator_DiversionRequested", Days(1) },
                { "GR_TokraSecureCommunicator_DiversionDialog", Days(1) },
                { "GR_TokraSecureCommunicator_ThreatRequested", Days(2) },
                { "GR_TokraSecureCommunicator_ThreatDialog", Days(5) },
                { "GR_TokraSecureCommunicator_InterceptedThreatRequested", Days(3) },
                { "GR_TokraSecureCommunicator_InterceptedThreatDialog", Days(5) },
                { "GR_TokraSecureCommunicator_MedicalRequested", Days(3) },
                { "GR_TokraSecureCommunicator_MedicalDialog", Days(3) },
                { "GR_TokraSecureCommunicator_MedicalCacheRequested", Days(3) },
                { "GR_TokraSecureCommunicator_MedicalCacheDialog", Days(3) },
                { "GR_TokraSecureCommunicator_FloatMenuCooldown", Days(0) },
                { "GR_TokraSecureCommunicator_DebriefCooldown", Days(0) },
                { "GR_TokraSecureCommunicator_DiversionCooldown", Days(0) },
                { "GR_TokraSecureCommunicator_ThreatCooldown", Days(0) },
                { "GR_TokraSecureCommunicator_MedicalCooldown", Days(0) },
                { "GR_TokraSecureCommunicator_MedicalCacheCooldown", Days(0) },
                { "GR_TokraSecureCommunicator_DebriefStatusCooldown", Days(0) },
                { "GR_TokraSecureCommunicator_FirstMissionStatusOutcomeDebriefPending", Days(0) },
                { "GR_TokraSecureCommunicator_FirstMissionStatusWorldSitePending", Days(0) },
                { "GR_TokraSecureCommunicator_FirstMissionStatusLeadPending", Days(0) },
                { "GR_TokraSecureCommunicator_FirstMissionStatusCachePending", Days(0) },
                { "GR_TokraSecureCommunicator_DiversionStatusCooldown", Days(0) },
                { "GR_TokraSecureCommunicator_ThreatStatusCooldown", Days(0) },
                { "GR_TokraSecureCommunicator_MedicalStatusCooldown", Days(0) },
                { "GR_TokraSecureCommunicator_MedicalCacheStatusCooldown", Days(0) },

                // Earlier biological, diplomatic and world-site displays.
                { "GR_GoauldOpenConflictWorldSite_DurationHours", Hours(0) },
                { "GR_GoauldOpenConflictWorldSite_DurationDays", Days(0) },
                { "GR_GoauldQueenHarvestCooldown", Days(0) },
                { "GR_TokraDiplomaticCooldown_Started", Days(0) },
                { "GR_TokraTherapeuticOpportunity_Inspect", Days(0) },

                // Older safehouse and intercepted-threat displays.
                { "GR_TokraHiddenSafehouseMarker_InspectString", Days(0) },
                { "GR_TokraInterceptedThreat_WindowDays", Days(0) }
            };

        private static DurationArgumentSpec Hours(int argumentIndex)
        {
            return new DurationArgumentSpec(
                argumentIndex,
                LegacyDurationUnit.RoundedHours);
        }

        private static DurationArgumentSpec Days(int argumentIndex)
        {
            return new DurationArgumentSpec(
                argumentIndex,
                LegacyDurationUnit.Days);
        }

        private static IEnumerable<MethodBase> TargetMethods()
        {
            return typeof(TranslatorFormattedStringExtensions)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(IsFormattedTranslateMethod)
                .Cast<MethodBase>();
        }

        private static bool IsFormattedTranslateMethod(MethodInfo method)
        {
            if (method == null || method.Name != "Translate")
            {
                return false;
            }

            ParameterInfo[] parameters = method.GetParameters();

            if (parameters.Length < 2
                || parameters[0].ParameterType != typeof(string))
            {
                return false;
            }

            if (parameters.Length == 2
                && parameters[1].ParameterType == typeof(NamedArgument[]))
            {
                return true;
            }

            for (int index = 1; index < parameters.Length; index++)
            {
                if (parameters[index].ParameterType != typeof(NamedArgument))
                {
                    return false;
                }
            }

            return true;
        }

        [HarmonyPostfix]
        private static void Postfix(
            object[] __args,
            ref TaggedString __result)
        {
            if (__args == null
                || __args.Length < 2
                || !(__args[0] is string key)
                || !DurationArguments.TryGetValue(
                    key,
                    out DurationArgumentSpec spec)
                || !TryGetFormatArgument(
                    __args,
                    spec.ArgumentIndex,
                    out object legacyValue)
                || !TryParseDurationNumber(
                    legacyValue,
                    out double numericValue))
            {
                return;
            }

            string legacyToken = legacyValue?.ToString();

            if (string.IsNullOrEmpty(legacyToken))
            {
                return;
            }

            int ticks = ConvertToTicks(numericValue, spec.Unit);
            string formattedDuration = GR_PlayerFacingDurationUtility.Format(
                ticks);
            string replaced = ReplaceLegacyDurationToken(
                __result.ToString(),
                legacyToken,
                formattedDuration,
                spec.Unit);

            if (!string.Equals(
                    replaced,
                    __result.ToString(),
                    StringComparison.Ordinal))
            {
                __result = replaced;
            }
        }

        private static bool TryGetFormatArgument(
            object[] methodArguments,
            int formatArgumentIndex,
            out object value)
        {
            value = null;

            if (formatArgumentIndex < 0)
            {
                return false;
            }

            if (methodArguments.Length == 2
                && methodArguments[1] is NamedArgument[] argumentArray)
            {
                if (argumentArray == null
                    || formatArgumentIndex >= argumentArray.Length)
                {
                    return false;
                }

                value = argumentArray[formatArgumentIndex].arg;
                return true;
            }

            int methodArgumentIndex = formatArgumentIndex + 1;

            if (methodArgumentIndex >= methodArguments.Length
                || !(methodArguments[methodArgumentIndex]
                    is NamedArgument argument))
            {
                return false;
            }

            value = argument.arg;
            return true;
        }

        private static bool TryParseDurationNumber(
            object value,
            out double number)
        {
            number = 0d;

            if (value == null)
            {
                return false;
            }

            if (value is byte
                || value is short
                || value is int
                || value is long
                || value is float
                || value is double
                || value is decimal)
            {
                try
                {
                    number = Math.Max(
                        0d,
                        Convert.ToDouble(value, CultureInfo.InvariantCulture));
                    return !double.IsNaN(number)
                        && !double.IsInfinity(number);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            string text = value.ToString();

            if (double.TryParse(
                    text,
                    NumberStyles.Float,
                    CultureInfo.CurrentCulture,
                    out number)
                || double.TryParse(
                    text,
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out number))
            {
                number = Math.Max(0d, number);
                return !double.IsNaN(number)
                    && !double.IsInfinity(number);
            }

            number = 0d;
            return false;
        }

        private static int ConvertToTicks(
            double value,
            LegacyDurationUnit unit)
        {
            double ticksPerUnit = unit == LegacyDurationUnit.Days
                ? GenDate.TicksPerDay
                : GenDate.TicksPerHour;
            double rawTicks = Math.Max(0d, value) * ticksPerUnit;

            if (rawTicks >= int.MaxValue)
            {
                return int.MaxValue;
            }

            return Math.Max(
                0,
                (int)Math.Round(
                    rawTicks,
                    MidpointRounding.AwayFromZero));
        }

        private static string ReplaceLegacyDurationToken(
            string source,
            string legacyToken,
            string formattedDuration,
            LegacyDurationUnit unit)
        {
            if (string.IsNullOrEmpty(source)
                || string.IsNullOrEmpty(legacyToken)
                || string.IsNullOrEmpty(formattedDuration))
            {
                return source;
            }

            string[] suffixes = unit == LegacyDurationUnit.Days
                ? new[]
                {
                    "RimWorld day(s)",
                    "RimWorld days",
                    "RimWorld day",
                    "day(s)",
                    "days",
                    "day",
                    "jour(s) RimWorld",
                    "jours RimWorld",
                    "jour RimWorld",
                    "jour(s)",
                    "jours",
                    "jour",
                    "d"
                }
                : new[]
                {
                    "hour(s)",
                    "hours",
                    "hour",
                    "heure(s)",
                    "heures",
                    "heure",
                    "h"
                };

            for (int index = 0; index < suffixes.Length; index++)
            {
                string legacyPhrase = legacyToken + " " + suffixes[index];
                string pattern = "(?<![0-9.,])"
                    + Regex.Escape(legacyPhrase);
                string replaced = Regex.Replace(
                    source,
                    pattern,
                    _ => formattedDuration,
                    RegexOptions.CultureInvariant);

                if (!string.Equals(replaced, source, StringComparison.Ordinal))
                {
                    return replaced;
                }
            }

            return source;
        }
    }
}
