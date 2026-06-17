using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public class MapComponent_TokraRelaySabotageMission : MapComponent
    {
        private bool initialized;
        private bool sabotageCompleted;
        private bool reinforcementTimerStarted;
        private bool reinforcementsArrived;
        private int reinforcementsArriveTick;
        private int reinforcementSpawnAttempts;
        private Thing relayDevice;
        private WorldObject_TokraDecodedMissionSite parentSite;

        public MapComponent_TokraRelaySabotageMission(Map map) : base(map)
        {
        }

        public bool Initialized
        {
            get { return initialized; }
        }

        public bool SabotageCompleted
        {
            get { return sabotageCompleted; }
        }

        public bool ReinforcementTimerStarted
        {
            get { return reinforcementTimerStarted; }
        }

        public bool ReinforcementsArrived
        {
            get { return reinforcementsArrived; }
        }

        public int RemainingReinforcementTicks
        {
            get
            {
                if (!reinforcementTimerStarted || reinforcementsArrived)
                {
                    return 0;
                }

                return System.Math.Max(
                    0,
                    reinforcementsArriveTick - Find.TickManager.TicksGame);
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref initialized, "tokraRelaySabotageInitialized", false);
            Scribe_Values.Look(ref sabotageCompleted, "tokraRelaySabotageCompleted", false);
            Scribe_Values.Look(ref reinforcementTimerStarted, "tokraRelayReinforcementTimerStarted", false);
            Scribe_Values.Look(ref reinforcementsArrived, "tokraRelayReinforcementsArrived", false);
            Scribe_Values.Look(ref reinforcementsArriveTick, "tokraRelayReinforcementsArriveTick", 0);
            Scribe_Values.Look(ref reinforcementSpawnAttempts, "tokraRelayReinforcementSpawnAttempts", 0);
            Scribe_References.Look(ref relayDevice, "tokraRelaySabotageDevice");
            Scribe_References.Look(ref parentSite, "tokraRelaySabotageParentSite");
        }

        public override void MapComponentTick()
        {
            base.MapComponentTick();

            if (!reinforcementTimerStarted || reinforcementsArrived)
            {
                return;
            }

            if (Find.TickManager.TicksGame < reinforcementsArriveTick)
            {
                return;
            }

            bool spawned = TokraRelaySabotageMissionUtility
                .TrySpawnReinforcements(map);

            if (spawned)
            {
                reinforcementsArrived = true;
                return;
            }

            reinforcementSpawnAttempts++;

            if (reinforcementSpawnAttempts >= 3)
            {
                reinforcementsArrived = true;
                GR_Log.Error(
                    "Tok'ra relay reinforcements could not be spawned after "
                    + "three attempts.");
                return;
            }

            reinforcementsArriveTick = Find.TickManager.TicksGame + 2500;
            GR_Log.Warning(
                "Tok'ra relay reinforcements could not be spawned; retrying "
                + "in one in-game hour.");
        }

        public void Initialize(
            WorldObject_TokraDecodedMissionSite parent,
            Thing relay)
        {
            initialized = true;
            parentSite = parent;
            relayDevice = relay;
        }

        public void NotifySabotageStarted()
        {
            if (reinforcementTimerStarted || reinforcementsArrived)
            {
                return;
            }

            reinforcementTimerStarted = true;
            reinforcementSpawnAttempts = 0;
            reinforcementsArriveTick = Find.TickManager.TicksGame
                + TokraRelaySabotageMissionUtility.ReinforcementDelayTicks;

            if (relayDevice != null)
            {
                Find.LetterStack.ReceiveLetter(
                    "GR_TokraRelaySabotageMission_ReinforcementTimerLetterLabel"
                        .Translate(),
                    "GR_TokraRelaySabotageMission_ReinforcementTimerLetterText"
                        .Translate(FormatRemainingReinforcementTime()),
                    LetterDefOf.ThreatSmall,
                    relayDevice);
            }
            else if (map.Parent != null)
            {
                Find.LetterStack.ReceiveLetter(
                    "GR_TokraRelaySabotageMission_ReinforcementTimerLetterLabel"
                        .Translate(),
                    "GR_TokraRelaySabotageMission_ReinforcementTimerLetterText"
                        .Translate(FormatRemainingReinforcementTime()),
                    LetterDefOf.ThreatSmall,
                    map.Parent);
            }
            else
            {
                Find.LetterStack.ReceiveLetter(
                    "GR_TokraRelaySabotageMission_ReinforcementTimerLetterLabel"
                        .Translate(),
                    "GR_TokraRelaySabotageMission_ReinforcementTimerLetterText"
                        .Translate(FormatRemainingReinforcementTime()),
                    LetterDefOf.ThreatSmall);
            }
        }

        public void NotifySabotageCompleted(Pawn saboteur)
        {
            if (sabotageCompleted)
            {
                return;
            }

            sabotageCompleted = true;

            if (parentSite == null)
            {
                parentSite = map.Parent as WorldObject_TokraDecodedMissionSite;
            }

            parentSite?.NotifyRelaySabotageCompleted();

            if (!TokraRelaySabotageMissionUtility.HasActiveHostiles(map))
            {
                if (relayDevice != null)
                {
                    Messages.Message(
                        "GR_TokraRelaySabotageMission_ExtractionAvailable"
                            .Translate(),
                        relayDevice,
                        MessageTypeDefOf.PositiveEvent,
                        historical: true);
                }
                else if (map.Parent != null)
                {
                    Messages.Message(
                        "GR_TokraRelaySabotageMission_ExtractionAvailable"
                            .Translate(),
                        map.Parent,
                        MessageTypeDefOf.PositiveEvent,
                        historical: true);
                }
                else
                {
                    Messages.Message(
                        "GR_TokraRelaySabotageMission_ExtractionAvailable"
                            .Translate(),
                        MessageTypeDefOf.PositiveEvent,
                        historical: true);
                }
            }
            else
            {
                if (relayDevice != null)
                {
                    Messages.Message(
                        "GR_TokraRelaySabotageMission_ExtractionBlocked"
                            .Translate(),
                        relayDevice,
                        MessageTypeDefOf.NeutralEvent,
                        historical: true);
                }
                else if (map.Parent != null)
                {
                    Messages.Message(
                        "GR_TokraRelaySabotageMission_ExtractionBlocked"
                            .Translate(),
                        map.Parent,
                        MessageTypeDefOf.NeutralEvent,
                        historical: true);
                }
                else
                {
                    Messages.Message(
                        "GR_TokraRelaySabotageMission_ExtractionBlocked"
                            .Translate(),
                        MessageTypeDefOf.NeutralEvent,
                        historical: true);
                }
            }
        }

        public string FormatRemainingReinforcementTime()
        {
            return (RemainingReinforcementTicks / 2500f).ToString("0.#");
        }
    }
}
