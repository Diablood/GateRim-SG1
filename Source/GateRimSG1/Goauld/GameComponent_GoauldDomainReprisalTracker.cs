using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Persistent reactions to successful active Goa'uld extractions.
    /// One announced reprisal may be pending per domain; later triggers can
    /// reuse this layer without creating a parallel mission framework.
    /// </summary>
    public sealed class GameComponent_GoauldDomainReprisalTracker
        : GameComponent
    {
        private const int MinimumDelayTicks = 60000;
        private const int MaximumDelayTicks = 180000;
        private const int DebugDelayTicks = 5000;
        private const int CooldownTicks = 900000;
        private const int CheckIntervalTicks = 2500;

        private List<GoauldDomainReprisalState> states =
            new List<GoauldDomainReprisalState>();
        private int nextCheckTick;

        public GameComponent_GoauldDomainReprisalTracker(Game game)
        {
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(
                ref states,
                "goauldDomainReprisalStates",
                LookMode.Deep);
            Scribe_Values.Look(
                ref nextCheckTick,
                "goauldDomainReprisalNextCheckTick",
                0);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                states = states
                    ?.Where(state => state != null)
                    .ToList()
                    ?? new List<GoauldDomainReprisalState>();
            }
        }

        public override void GameComponentTick()
        {
            base.GameComponentTick();

            int currentTick = CurrentTick();

            if (currentTick < nextCheckTick)
            {
                return;
            }

            nextCheckTick = currentTick + CheckIntervalTicks;

            for (int index = 0; index < states.Count; index++)
            {
                GoauldDomainReprisalState state = states[index];

                if (state?.pending == true
                    && currentTick >= state.reprisalTick)
                {
                    TryResolveReprisal(state, currentTick);
                }
            }
        }

        public static void NotifyActiveHostExtracted(
            Pawn formerHost,
            GoauldSymbioteData symbioteData)
        {
            Map map = formerHost?.Map;
            Faction domainFaction = symbioteData?.AllegianceFaction;

            if (map == null
                || !map.IsPlayerHome
                || symbioteData?.Origin != GoauldSymbioteOrigin.Goauld
                || !GoauldSystemLordFactionUtility.IsSystemLordFaction(
                    domainFaction))
            {
                return;
            }

            Current?.TrySchedule(
                map,
                domainFaction,
                formerHost.LabelShortCap,
                symbioteData.SymbioteName,
                debugShortDelay: false);
        }

        public static GameComponent_GoauldDomainReprisalTracker Current
            => Verse.Current.Game
                ?.GetComponent<GameComponent_GoauldDomainReprisalTracker>();

        public bool TryScheduleDebug(Map map)
        {
            Faction faction = Find.FactionManager?.FirstFactionOfDef(
                GR_DefOf.SG1_GoauldSystemLordPrototype);

            return TrySchedule(
                map,
                faction,
                "GR_GoauldDomainReprisal_DebugFormerHost"
                    .Translate()
                    .ToString(),
                "GR_GoauldDomainReprisal_DebugSymbiote"
                    .Translate()
                    .ToString(),
                debugShortDelay: true);
        }

        public bool TriggerFirstPendingNow()
        {
            GoauldDomainReprisalState state = states.FirstOrDefault(
                candidate => candidate?.pending == true);

            if (state == null)
            {
                return false;
            }

            int currentTick = CurrentTick();
            state.reprisalTick = currentTick;
            return TryResolveReprisal(state, currentTick);
        }

        public void ResetDebug()
        {
            states.Clear();
            nextCheckTick = 0;
        }

        public string BuildDebugReport()
        {
            if (states.Count == 0)
            {
                return "No Goa'uld domain reprisal state is recorded.";
            }

            int currentTick = CurrentTick();
            return string.Join(
                "\n",
                states.Select(state =>
                    $"{state.domainFaction?.Name ?? "<missing domain>"}: "
                    + (state.pending
                        ? $"pending in {Math.Max(0, state.reprisalTick - currentTick)} ticks, "
                            + $"map {state.targetMapUniqueId}, {state.raidPoints:0} points"
                        : $"cooldown {Math.Max(0, state.nextEligibleTick - currentTick)} ticks")));
        }

        private bool TrySchedule(
            Map map,
            Faction domainFaction,
            string formerHostLabel,
            string symbioteName,
            bool debugShortDelay)
        {
            if (map == null
                || domainFaction == null
                || Find.TickManager == null
                || !GoauldSystemLordFactionUtility.IsSystemLordFaction(
                    domainFaction))
            {
                return false;
            }

            int currentTick = CurrentTick();
            GoauldDomainReprisalState state = FindOrCreateState(domainFaction);

            if (state.pending || currentTick < state.nextEligibleTick)
            {
                return false;
            }

            int delay = debugShortDelay
                ? DebugDelayTicks
                : Rand.RangeInclusive(MinimumDelayTicks, MaximumDelayTicks);

            state.targetMapUniqueId = map.uniqueID;
            state.reprisalTick = currentTick + delay;
            state.raidPoints = Math.Max(
                1f,
                StorytellerUtility.DefaultThreatPointsNow(map));
            state.formerHostLabel = formerHostLabel ?? string.Empty;
            state.symbioteName = symbioteName ?? string.Empty;
            state.pending = true;

            Find.LetterStack?.ReceiveLetter(
                "GR_GoauldDomainReprisal_WarningLabel".Translate(),
                "GR_GoauldDomainReprisal_WarningText".Translate(
                    domainFaction.Name,
                    state.symbioteName,
                    state.formerHostLabel),
                LetterDefOf.ThreatBig,
                new LookTargets(map.Center, map));

            GR_Log.Message(
                $"Scheduled Goa'uld domain reprisal from {domainFaction.Name} "
                + $"({domainFaction.loadID}) against map {map.uniqueID}: "
                + $"delay={delay}, points={state.raidPoints:0}.");

            return true;
        }

        private bool TryResolveReprisal(
            GoauldDomainReprisalState state,
            int currentTick)
        {
            Map map = Find.Maps?.FirstOrDefault(
                candidate => candidate?.uniqueID == state.targetMapUniqueId);

            if (map == null || !map.IsPlayerHome)
            {
                CompleteState(state, currentTick);
                return false;
            }

            if (!GoauldSystemLordFactionUtility.IsSystemLordFaction(
                    state.domainFaction))
            {
                CompleteState(state, currentTick);
                return false;
            }

            IncidentDef incident = GR_DefOf.SG1_GoauldJaffaNaturalRaid;

            if (incident?.Worker == null)
            {
                GR_Log.Error(
                    "Cannot start Goa'uld domain reprisal: natural raid "
                    + "incident is unavailable.");
                CompleteState(state, currentTick);
                return false;
            }

            IncidentParms parms = new IncidentParms
            {
                target = map,
                faction = state.domainFaction,
                forced = true,
                points = Math.Max(1f, state.raidPoints)
            };

            bool succeeded = incident.Worker.TryExecute(parms);

            if (!succeeded)
            {
                Messages.Message(
                    "GR_GoauldDomainReprisal_AttackFailed".Translate(),
                    MessageTypeDefOf.NeutralEvent,
                    historical: true);
            }

            GR_Log.Message(
                $"Resolved Goa'uld domain reprisal from "
                + $"{state.domainFaction?.Name ?? "<missing>"}: "
                + $"success={succeeded}, map={map.uniqueID}, "
                + $"points={state.raidPoints:0}.");

            CompleteState(state, currentTick);
            return succeeded;
        }

        private GoauldDomainReprisalState FindOrCreateState(Faction faction)
        {
            GoauldDomainReprisalState state = states.FirstOrDefault(
                candidate => candidate?.domainFaction == faction);

            if (state != null)
            {
                return state;
            }

            state = new GoauldDomainReprisalState
            {
                domainFaction = faction
            };
            states.Add(state);
            return state;
        }

        private static void CompleteState(
            GoauldDomainReprisalState state,
            int currentTick)
        {
            state.pending = false;
            state.targetMapUniqueId = -1;
            state.reprisalTick = 0;
            state.nextEligibleTick = currentTick + CooldownTicks;
            state.raidPoints = 0f;
            state.formerHostLabel = string.Empty;
            state.symbioteName = string.Empty;
        }

        private static int CurrentTick()
        {
            return Find.TickManager?.TicksGame ?? 0;
        }
    }
}
