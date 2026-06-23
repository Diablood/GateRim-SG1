using System.Collections.Generic;
using System.Text;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public enum TokraCommunicatorAvailabilityFailure
    {
        None,
        MissingThingDef,
        NoPlayerHomeMap,
        NotBuiltOnPlayerHomeMap,
        NotSpawned,
        NotPlayerControlled,
        MissingCommunicatorComp,
        MissingPowerComp,
        UnpoweredOrSwitchedOff,
        Unavailable
    }

    public sealed class TokraCommunicatorAvailabilitySnapshot
    {
        public bool ResearchFinished { get; internal set; }
        public int LoadedMapCount { get; internal set; }
        public int PlayerHomeMapCount { get; internal set; }
        public int TotalCommunicatorCount { get; internal set; }
        public int HomeCommunicatorCount { get; internal set; }
        public int SpawnedHomeCommunicatorCount { get; internal set; }
        public int PlayerControlledCommunicatorCount { get; internal set; }
        public int ConfiguredCommunicatorCount { get; internal set; }
        public int PoweredComponentCount { get; internal set; }
        public int AvailableCommunicatorCount { get; internal set; }
        public ThingWithComps AvailableCommunicator { get; internal set; }
        public Map AvailableMap { get; internal set; }
        public TokraCommunicatorAvailabilityFailure Failure { get; internal set; }

        public bool IsAvailable => AvailableCommunicator != null;
    }

    /// <summary>
    /// Shared availability test for the player Tok'ra secure communicator.
    ///
    /// Research completion gates new construction only. A communicator that
    /// already exists in an older save remains valid when it is on a player
    /// home map, player-controlled, correctly configured and powered.
    /// </summary>
    public static class TokraSecureCommunicatorAvailabilityUtility
    {
        public static bool TryFindAvailableCommunicator(
            out ThingWithComps communicator)
        {
            TokraCommunicatorAvailabilitySnapshot snapshot = Inspect();
            communicator = snapshot.AvailableCommunicator;
            return snapshot.IsAvailable;
        }

        public static bool TryFindAvailableCommunicator(
            Map map,
            out ThingWithComps communicator)
        {
            communicator = null;

            if (map == null || !map.IsPlayerHome)
            {
                return false;
            }

            ThingDef communicatorDef = GR_DefOf.SG1_TokraSecureCommunicator;

            if (communicatorDef == null || map.listerThings == null)
            {
                return false;
            }

            List<Thing> things = map.listerThings.ThingsOfDef(communicatorDef);

            for (int index = 0; index < things.Count; index++)
            {
                ThingWithComps candidate = things[index] as ThingWithComps;

                if (!IsAvailableCommunicator(candidate, map))
                {
                    continue;
                }

                communicator = candidate;
                return true;
            }

            return false;
        }

        public static TokraCommunicatorAvailabilitySnapshot Inspect()
        {
            TokraCommunicatorAvailabilitySnapshot snapshot
                = new TokraCommunicatorAvailabilitySnapshot
                {
                    ResearchFinished = GR_DefOf.SG1_TokraSecureCommunications
                        ?.IsFinished == true
                };

            ThingDef communicatorDef = GR_DefOf.SG1_TokraSecureCommunicator;

            if (communicatorDef == null)
            {
                snapshot.Failure
                    = TokraCommunicatorAvailabilityFailure.MissingThingDef;
                return snapshot;
            }

            List<Map> maps = Find.Maps;

            if (maps == null)
            {
                snapshot.Failure
                    = TokraCommunicatorAvailabilityFailure.NoPlayerHomeMap;
                return snapshot;
            }

            snapshot.LoadedMapCount = maps.Count;

            for (int mapIndex = 0; mapIndex < maps.Count; mapIndex++)
            {
                Map map = maps[mapIndex];

                if (map == null)
                {
                    continue;
                }

                if (map.IsPlayerHome)
                {
                    snapshot.PlayerHomeMapCount++;
                }

                if (map.listerThings == null)
                {
                    continue;
                }

                List<Thing> things = map.listerThings.ThingsOfDef(
                    communicatorDef);

                for (int thingIndex = 0; thingIndex < things.Count; thingIndex++)
                {
                    ThingWithComps communicator
                        = things[thingIndex] as ThingWithComps;
                    snapshot.TotalCommunicatorCount++;

                    if (!map.IsPlayerHome)
                    {
                        continue;
                    }

                    snapshot.HomeCommunicatorCount++;

                    if (communicator == null
                        || communicator.Destroyed
                        || !communicator.Spawned
                        || communicator.Map != map)
                    {
                        continue;
                    }

                    snapshot.SpawnedHomeCommunicatorCount++;

                    if (communicator.Faction != Faction.OfPlayer)
                    {
                        continue;
                    }

                    snapshot.PlayerControlledCommunicatorCount++;

                    if (communicator.GetComp<Comp_TokraSecureCommunicator>()
                        == null)
                    {
                        continue;
                    }

                    snapshot.ConfiguredCommunicatorCount++;

                    CompPowerTrader powerComp
                        = communicator.GetComp<CompPowerTrader>();

                    if (powerComp == null)
                    {
                        continue;
                    }

                    snapshot.PoweredComponentCount++;

                    if (!powerComp.PowerOn)
                    {
                        continue;
                    }

                    snapshot.AvailableCommunicatorCount++;

                    if (snapshot.AvailableCommunicator == null)
                    {
                        snapshot.AvailableCommunicator = communicator;
                        snapshot.AvailableMap = map;
                    }
                }
            }

            snapshot.Failure = GetFailure(snapshot);
            return snapshot;
        }

        public static string GetDebugReport()
        {
            TokraCommunicatorAvailabilitySnapshot snapshot = Inspect();
            StringBuilder report = new StringBuilder();

            report.AppendLine("Tok'ra secure communicator availability");
            report.AppendLine();
            report.AppendLine(
                "Secure-communications research finished: "
                + snapshot.ResearchFinished);
            report.AppendLine(
                "Construction prerequisite: SG1_TokraSecureCommunications");
            report.AppendLine(
                "Recurrent-operation gating active in this revision: True");
            report.AppendLine();
            report.AppendLine("Loaded maps: " + snapshot.LoadedMapCount);
            report.AppendLine(
                "Player home maps: " + snapshot.PlayerHomeMapCount);
            report.AppendLine(
                "Communicators on all loaded maps: "
                + snapshot.TotalCommunicatorCount);
            report.AppendLine(
                "Communicators on player home maps: "
                + snapshot.HomeCommunicatorCount);
            report.AppendLine(
                "Spawned home communicators: "
                + snapshot.SpawnedHomeCommunicatorCount);
            report.AppendLine(
                "Player-controlled communicators: "
                + snapshot.PlayerControlledCommunicatorCount);
            report.AppendLine(
                "Communicators with GateRim component: "
                + snapshot.ConfiguredCommunicatorCount);
            report.AppendLine(
                "Communicators with power component: "
                + snapshot.PoweredComponentCount);
            report.AppendLine(
                "Available powered communicators: "
                + snapshot.AvailableCommunicatorCount);
            report.AppendLine();
            report.AppendLine("Available: " + snapshot.IsAvailable);
            report.AppendLine("Failure: " + snapshot.Failure);

            if (snapshot.AvailableCommunicator != null)
            {
                report.AppendLine(
                    "Selected communicator: "
                    + snapshot.AvailableCommunicator.ThingID);
                string mapLabel = snapshot.AvailableMap?.Parent == null
                    ? snapshot.AvailableMap?.ToString() ?? "<none>"
                    : snapshot.AvailableMap.Parent.LabelCap.ToString();
                report.AppendLine("Selected map: " + mapLabel);
            }

            report.AppendLine();
            report.AppendLine(
                "Compatibility rule: existing built communicators remain "
                + "usable even when the new construction research is not "
                + "finished.");

            return report.ToString().TrimEndNewlines();
        }

        private static bool IsAvailableCommunicator(
            ThingWithComps communicator,
            Map map)
        {
            if (communicator == null
                || map == null
                || !map.IsPlayerHome
                || communicator.Destroyed
                || !communicator.Spawned
                || communicator.Map != map
                || communicator.Faction != Faction.OfPlayer
                || communicator.GetComp<Comp_TokraSecureCommunicator>() == null)
            {
                return false;
            }

            CompPowerTrader powerComp
                = communicator.GetComp<CompPowerTrader>();

            return powerComp != null && powerComp.PowerOn;
        }

        private static TokraCommunicatorAvailabilityFailure GetFailure(
            TokraCommunicatorAvailabilitySnapshot snapshot)
        {
            if (snapshot.IsAvailable)
            {
                return TokraCommunicatorAvailabilityFailure.None;
            }

            if (snapshot.PlayerHomeMapCount == 0)
            {
                return TokraCommunicatorAvailabilityFailure.NoPlayerHomeMap;
            }

            if (snapshot.HomeCommunicatorCount == 0)
            {
                return TokraCommunicatorAvailabilityFailure
                    .NotBuiltOnPlayerHomeMap;
            }

            if (snapshot.SpawnedHomeCommunicatorCount == 0)
            {
                return TokraCommunicatorAvailabilityFailure.NotSpawned;
            }

            if (snapshot.PlayerControlledCommunicatorCount == 0)
            {
                return TokraCommunicatorAvailabilityFailure
                    .NotPlayerControlled;
            }

            if (snapshot.ConfiguredCommunicatorCount == 0)
            {
                return TokraCommunicatorAvailabilityFailure
                    .MissingCommunicatorComp;
            }

            if (snapshot.PoweredComponentCount == 0)
            {
                return TokraCommunicatorAvailabilityFailure.MissingPowerComp;
            }

            if (snapshot.AvailableCommunicatorCount == 0)
            {
                return TokraCommunicatorAvailabilityFailure
                    .UnpoweredOrSwitchedOff;
            }

            return TokraCommunicatorAvailabilityFailure.Unavailable;
        }
    }
}
