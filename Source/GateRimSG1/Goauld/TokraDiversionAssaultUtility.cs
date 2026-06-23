using System;
using System.Collections.Generic;
using System.Linq;
using GateRimSG1.Missions;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Persistent runtime state for the recurring Tok'ra diversion assault.
    ///
    /// The stable archetype and MissionDef identifiers keep the original
    /// DecoyTransmissionDefense internal names to avoid renumbering the persisted
    /// archetype, while the player-facing operation no longer uses a physical
    /// device. Active r1 transmitter test saves are intentionally unsupported.
    /// </summary>
    internal static class TokraDiversionAssaultUtility
    {
        public const string RaidDueTickCounterKey = "decoyRaidDueTick";
        public const string RaidTriggeredCounterKey = "decoyRaidTriggered";
        public const string RaidPawnIdsStringKey = "decoyRaidPawnIds";
        public const string CargoCarrierIdsStringKey
            = "decoyRaidCargoCarrierIds";
        public const string ExtractedCargoKindStringKey
            = "decoyRaidExtractedCargoKind";

        private const char IdSeparator = '|';

        public static void Initialize(
            GateRimMissionRuntimeData runtime,
            TokraOrganicOperationDefinition definition,
            int currentTick)
        {
            if (runtime == null || definition == null)
            {
                return;
            }

            EnsureCounters(runtime);
            runtime.counters[RaidDueTickCounterKey]
                = currentTick + GetConfiguredRaidDelay(definition);
            runtime.counters[RaidTriggeredCounterKey] = 0;
            runtime.SetString(RaidPawnIdsStringKey, null);
            runtime.SetString(CargoCarrierIdsStringKey, null);
            runtime.SetString(ExtractedCargoKindStringKey, null);
            runtime.phaseId = "accepted";
        }

        public static void Normalize(
            GateRimMissionRuntimeData runtime,
            TokraOrganicOperationDefinition definition,
            int acceptedTick,
            int currentTick)
        {
            if (runtime == null || definition == null)
            {
                return;
            }

            EnsureCounters(runtime);

            if (!IsRaidTriggered(runtime)
                && GetRaidDueTick(runtime) <= 0)
            {
                int baseTick = Math.Max(currentTick, acceptedTick);
                runtime.counters[RaidDueTickCounterKey]
                    = baseTick + GetConfiguredRaidDelay(definition);
            }
        }

        public static int GetRaidDueTick(
            GateRimMissionRuntimeData runtime)
        {
            return GetCounter(runtime, RaidDueTickCounterKey);
        }

        public static bool IsRaidTriggered(
            GateRimMissionRuntimeData runtime)
        {
            return GetCounter(runtime, RaidTriggeredCounterKey) > 0;
        }

        public static void MarkRaidTriggered(
            GateRimMissionRuntimeData runtime)
        {
            if (runtime == null)
            {
                return;
            }

            EnsureCounters(runtime);
            runtime.counters[RaidTriggeredCounterKey] = 1;
            runtime.counters[RaidDueTickCounterKey] = 0;
            runtime.phaseId = "assault";
        }

        public static void ScheduleRaidRetry(
            GateRimMissionRuntimeData runtime,
            TokraOrganicOperationDefinition definition,
            int currentTick)
        {
            if (runtime == null || definition == null)
            {
                return;
            }

            EnsureCounters(runtime);
            runtime.counters[RaidDueTickCounterKey]
                = currentTick + Math.Max(1, definition.DecoyRaidRetryTicks);
        }

        public static void RegisterRaidPawns(
            GateRimMissionRuntimeData runtime,
            IEnumerable<Pawn> pawns)
        {
            if (runtime == null || pawns == null)
            {
                return;
            }

            HashSet<string> ids = GetIdSet(runtime, RaidPawnIdsStringKey);

            foreach (Pawn pawn in pawns)
            {
                if (pawn != null && !string.IsNullOrEmpty(pawn.ThingID))
                {
                    ids.Add(pawn.ThingID);
                }
            }

            SetIdSet(runtime, RaidPawnIdsStringKey, ids);
        }

        public static IReadOnlyCollection<string> GetRegisteredRaidPawnIds(
            GateRimMissionRuntimeData runtime)
        {
            return GetIdSet(runtime, RaidPawnIdsStringKey).ToList();
        }

        public static int GetRegisteredRaidPawnCount(
            GateRimMissionRuntimeData runtime)
        {
            return GetIdSet(runtime, RaidPawnIdsStringKey).Count;
        }

        public static void SetCargoCarrierState(
            GateRimMissionRuntimeData runtime,
            Pawn pawn,
            bool isCarryingMissionCargo)
        {
            if (runtime == null
                || pawn == null
                || string.IsNullOrEmpty(pawn.ThingID))
            {
                return;
            }

            HashSet<string> ids = GetIdSet(
                runtime,
                CargoCarrierIdsStringKey);

            if (isCarryingMissionCargo)
            {
                ids.Add(pawn.ThingID);
            }
            else
            {
                ids.Remove(pawn.ThingID);
            }

            SetIdSet(runtime, CargoCarrierIdsStringKey, ids);
        }

        public static bool WasCargoCarrier(
            GateRimMissionRuntimeData runtime,
            string pawnThingId)
        {
            return !string.IsNullOrEmpty(pawnThingId)
                && GetIdSet(runtime, CargoCarrierIdsStringKey)
                    .Contains(pawnThingId);
        }

        public static void SetExtractedCargoKind(
            GateRimMissionRuntimeData runtime,
            string cargoKind)
        {
            runtime?.SetString(
                ExtractedCargoKindStringKey,
                cargoKind);
        }

        public static string GetExtractedCargoKind(
            GateRimMissionRuntimeData runtime)
        {
            return runtime?.GetString(
                ExtractedCargoKindStringKey,
                null);
        }

        private static int GetConfiguredRaidDelay(
            TokraOrganicOperationDefinition definition)
        {
            int minimum = Math.Max(
                1,
                definition.DecoyRaidDelayMinimumTicks);
            int maximum = Math.Max(
                minimum,
                definition.DecoyRaidDelayMaximumTicks);

            return Rand.RangeInclusive(minimum, maximum);
        }

        private static int GetCounter(
            GateRimMissionRuntimeData runtime,
            string key)
        {
            if (runtime?.counters == null || string.IsNullOrEmpty(key))
            {
                return 0;
            }

            int value;
            return runtime.counters.TryGetValue(key, out value)
                ? value
                : 0;
        }

        private static void EnsureCounters(
            GateRimMissionRuntimeData runtime)
        {
            if (runtime.counters == null)
            {
                runtime.counters = new Dictionary<string, int>();
            }
        }

        private static HashSet<string> GetIdSet(
            GateRimMissionRuntimeData runtime,
            string key)
        {
            string raw = runtime?.GetString(key, null);

            if (string.IsNullOrWhiteSpace(raw))
            {
                return new HashSet<string>(StringComparer.Ordinal);
            }

            return new HashSet<string>(
                raw.Split(new[] { IdSeparator },
                    StringSplitOptions.RemoveEmptyEntries),
                StringComparer.Ordinal);
        }

        private static void SetIdSet(
            GateRimMissionRuntimeData runtime,
            string key,
            HashSet<string> ids)
        {
            if (runtime == null)
            {
                return;
            }

            if (ids == null || ids.Count == 0)
            {
                runtime.SetString(key, null);
                return;
            }

            runtime.SetString(
                key,
                string.Join(
                    IdSeparator.ToString(),
                    ids.OrderBy(id => id)));
        }
    }
}
