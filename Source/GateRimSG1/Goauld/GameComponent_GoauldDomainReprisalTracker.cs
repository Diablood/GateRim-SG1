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
        private const string UltimatumLetterDefName =
            "SG1_GoauldExtractionUltimatum";
        private const int UltimatumDurationTicks = 60000;
        private const int MinimumDelayTicks = 60000;
        private const int MaximumDelayTicks = 180000;
        private const int DebugDelayTicks = 5000;
        private const int CooldownTicks = 900000;
        private const int CheckIntervalTicks = 250;

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

                if (state?.ultimatumPending == true)
                {
                    if (DemandedSymbioteWasDestroyed(state))
                    {
                        ScheduleReprisal(
                            state,
                            currentTick,
                            UltimatumResolution.SymbioteDestroyed);
                    }
                    else if (currentTick >= state.ultimatumExpiryTick)
                    {
                        ScheduleReprisal(
                            state,
                            currentTick,
                            UltimatumResolution.Expired);
                    }
                    else
                    {
                        MaintainUltimatumSedation(state);
                    }
                }
                else if (state?.pending == true
                    && currentTick >= state.reprisalTick)
                {
                    TryResolveReprisal(state, currentTick);
                }
            }
        }

        public static void NotifyActiveHostExtracted(
            Pawn formerHost,
            GoauldSymbioteData symbioteData,
            Pawn freeSymbiote)
        {
            Map map = formerHost?.Map;
            Faction domainFaction = symbioteData?.AllegianceFaction;

            if (map == null
                || !map.IsPlayerHome
                || freeSymbiote == null
                || !freeSymbiote.Spawned
                || symbioteData?.Origin != GoauldSymbioteOrigin.Goauld
                || !GoauldSystemLordFactionUtility.IsSystemLordFaction(
                    domainFaction))
            {
                return;
            }

            Current?.TryBeginUltimatum(
                map,
                domainFaction,
                formerHost.LabelShortCap,
                symbioteData.SymbioteName,
                freeSymbiote,
                debugShortDelay: false,
                debugGeneratedSymbiote: false);
        }

        public static void NotifyFatalActiveHostExtraction(
            Map map,
            string formerHostLabel,
            GoauldSymbioteData symbioteData)
        {
            Faction domainFaction = symbioteData?.AllegianceFaction;

            if (map == null
                || !map.IsPlayerHome
                || symbioteData?.Origin != GoauldSymbioteOrigin.Goauld
                || !GoauldSystemLordFactionUtility.IsSystemLordFaction(
                    domainFaction))
            {
                return;
            }

            Current?.TryScheduleFatalExtractionReprisal(
                map,
                domainFaction,
                formerHostLabel,
                symbioteData.SymbioteName,
                debugShortDelay: false);
        }

        public static GameComponent_GoauldDomainReprisalTracker Current
            => Verse.Current.Game
                ?.GetComponent<GameComponent_GoauldDomainReprisalTracker>();

        public bool TryCreateDebugUltimatum(Map map)
        {
            Faction faction = Find.FactionManager?.FirstFactionOfDef(
                GR_DefOf.SG1_GoauldSystemLordPrototype);

            if (map == null || faction == null)
            {
                return false;
            }

            Pawn symbiote = PawnGenerator.GeneratePawn(
                GR_DefOf.SG1_GoauldSymbiote,
                faction,
                map.Tile);

            if (symbiote == null
                || !GenPlace.TryPlaceThing(
                    symbiote,
                    map.Center,
                    map,
                    ThingPlaceMode.Near))
            {
                symbiote?.Destroy(DestroyMode.Vanish);
                return false;
            }

            SedateDebugSymbiote(symbiote);

            bool started = TryBeginUltimatum(
                map,
                faction,
                "GR_GoauldDomainReprisal_DebugFormerHost"
                    .Translate()
                    .ToString(),
                symbiote.LabelShortCap,
                symbiote,
                debugShortDelay: true,
                debugGeneratedSymbiote: true);

            if (!started && !symbiote.Destroyed)
            {
                symbiote.Destroy(DestroyMode.Vanish);
            }

            return started;
        }

        public bool TryCreateDebugFatalExtractionReprisal(Map map)
        {
            Faction faction = Find.FactionManager?.FirstFactionOfDef(
                GR_DefOf.SG1_GoauldSystemLordPrototype);

            return TryScheduleFatalExtractionReprisal(
                map,
                faction,
                "GR_GoauldDomainReprisal_DebugFormerHost"
                    .Translate()
                    .ToString(),
                "GR_GoauldDomainFatalExtraction_DebugSymbiote"
                    .Translate()
                    .ToString(),
                debugShortDelay: true);
        }

        public static bool IsCurrentUltimatum(Faction domainFaction)
        {
            return Current?.FindState(domainFaction)?.ultimatumPending == true;
        }

        public static bool CanSurrenderDemandedSymbiote(
            Faction domainFaction,
            out string disabledReason)
        {
            disabledReason = string.Empty;
            GoauldDomainReprisalState state = Current?.FindState(domainFaction);

            if (state?.ultimatumPending != true)
            {
                disabledReason = "GR_GoauldDomainUltimatum_NoLongerActive"
                    .Translate();
                return false;
            }

            Pawn symbiote = state.demandedSymbiote;
            Map targetMap = Current?.FindTargetMap(state);

            if (symbiote == null
                || symbiote.Destroyed
                || !symbiote.Spawned
                || symbiote.Map != targetMap)
            {
                disabledReason =
                    "GR_GoauldDomainUltimatum_SymbioteUnavailable"
                    .Translate(state.symbioteName);
                return false;
            }

            return true;
        }

        public static void SurrenderFromLetter(Faction domainFaction)
        {
            Current?.TrySurrender(domainFaction);
        }

        public static void DefyFromLetter(Faction domainFaction)
        {
            Current?.TryDefy(domainFaction);
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

        public bool ExpireFirstUltimatumNow()
        {
            GoauldDomainReprisalState state = states.FirstOrDefault(
                candidate => candidate?.ultimatumPending == true);

            if (state == null)
            {
                return false;
            }

            ScheduleReprisal(
                state,
                CurrentTick(),
                UltimatumResolution.Expired);
            return true;
        }

        public bool KillFirstDemandedSymbioteDebug()
        {
            GoauldDomainReprisalState state = states.FirstOrDefault(
                candidate => candidate?.ultimatumPending == true);
            Pawn symbiote = state?.demandedSymbiote;

            if (symbiote == null || symbiote.Destroyed || symbiote.Dead)
            {
                return false;
            }

            symbiote.Kill(null);

            if (symbiote.Dead || symbiote.Destroyed)
            {
                ScheduleReprisal(
                    state,
                    CurrentTick(),
                    UltimatumResolution.SymbioteDestroyed);
                return true;
            }

            return false;
        }

        public void ResetDebug()
        {
            foreach (GoauldDomainReprisalState state in states)
            {
                RemoveUltimatumLetters(state?.domainFaction);
                DestroyDebugSymbiote(state);
            }

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
                    + (state.ultimatumPending
                        ? $"ultimatum expires in {Math.Max(0, state.ultimatumExpiryTick - currentTick)} ticks, "
                            + $"symbiote {state.demandedSymbiote?.ThingID ?? "<missing>"}, "
                            + $"map {state.targetMapUniqueId}, {state.raidPoints:0} points"
                        : state.pending
                        ? $"pending in {Math.Max(0, state.reprisalTick - currentTick)} ticks, "
                            + $"map {state.targetMapUniqueId}, {state.raidPoints:0} points"
                        : $"cooldown {Math.Max(0, state.nextEligibleTick - currentTick)} ticks")));
        }

        private bool TryBeginUltimatum(
            Map map,
            Faction domainFaction,
            string formerHostLabel,
            string symbioteName,
            Pawn demandedSymbiote,
            bool debugShortDelay,
            bool debugGeneratedSymbiote)
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

            if (state.ultimatumPending
                || state.pending
                || currentTick < state.nextEligibleTick)
            {
                return false;
            }

            state.targetMapUniqueId = map.uniqueID;
            state.ultimatumExpiryTick = currentTick + UltimatumDurationTicks;
            state.raidPoints = Math.Max(
                1f,
                StorytellerUtility.DefaultThreatPointsNow(map));
            state.formerHostLabel = formerHostLabel ?? string.Empty;
            state.symbioteName = symbioteName ?? string.Empty;
            state.demandedSymbiote = demandedSymbiote;
            state.debugGeneratedSymbiote = debugGeneratedSymbiote;
            state.debugShortDelay = debugShortDelay;
            state.ultimatumPending = true;
            state.pending = false;
            state.reprisalTick = 0;

            MaintainUltimatumSedation(state);

            if (!CreateUltimatumLetter(state))
            {
                DestroyDebugSymbiote(state);
                ClearState(state, nextEligibleTick: 0);
                return false;
            }

            GR_Log.Message(
                $"Started Goa'uld extraction ultimatum from "
                + $"{domainFaction.Name} ({domainFaction.loadID}) against "
                + $"map {map.uniqueID}: symbiote="
                + $"{demandedSymbiote?.ThingID ?? "<missing>"}, "
                + $"points={state.raidPoints:0}.");

            return true;
        }

        private bool TryScheduleFatalExtractionReprisal(
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

            if (state.ultimatumPending
                || state.pending
                || currentTick < state.nextEligibleTick)
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
                "GR_GoauldDomainFatalExtraction_Label".Translate(),
                "GR_GoauldDomainFatalExtraction_Text".Translate(
                    domainFaction.Name,
                    state.symbioteName,
                    state.formerHostLabel,
                    delay.ToStringTicksToPeriod()),
                LetterDefOf.ThreatBig,
                new LookTargets(map.Center, map));

            GR_Log.Message(
                $"Scheduled fatal-extraction reprisal from "
                + $"{domainFaction.Name} ({domainFaction.loadID}) against "
                + $"map {map.uniqueID}: delay={delay}, "
                + $"points={state.raidPoints:0}.");

            return true;
        }

        private bool TrySurrender(Faction domainFaction)
        {
            if (!CanSurrenderDemandedSymbiote(
                    domainFaction,
                    out string disabledReason))
            {
                Messages.Message(
                    disabledReason,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            GoauldDomainReprisalState state = FindState(domainFaction);
            Pawn symbiote = state.demandedSymbiote;
            string symbioteName = state.symbioteName;

            RemoveUltimatumLetters(domainFaction);
            symbiote.Destroy(DestroyMode.Vanish);
            CompleteState(state, CurrentTick());

            Messages.Message(
                "GR_GoauldDomainUltimatum_Surrendered".Translate(
                    symbioteName,
                    domainFaction.Name),
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            GR_Log.Message(
                $"Surrendered extracted symbiote {symbiote.ThingID} to "
                + $"Goa'uld domain {domainFaction.Name} "
                + $"({domainFaction.loadID}); reprisal cancelled.");

            return true;
        }

        private bool TryDefy(Faction domainFaction)
        {
            GoauldDomainReprisalState state = FindState(domainFaction);

            if (state?.ultimatumPending != true)
            {
                return false;
            }

            ScheduleReprisal(
                state,
                CurrentTick(),
                UltimatumResolution.Defied);
            return true;
        }

        private void ScheduleReprisal(
            GoauldDomainReprisalState state,
            int currentTick,
            UltimatumResolution resolution)
        {
            int delay = state.debugShortDelay
                ? DebugDelayTicks
                : Rand.RangeInclusive(MinimumDelayTicks, MaximumDelayTicks);
            Map map = FindTargetMap(state);

            RemoveUltimatumLetters(state.domainFaction);
            DestroyDebugSymbiote(state);

            state.demandedSymbiote = null;
            state.debugGeneratedSymbiote = false;
            state.debugShortDelay = false;
            state.ultimatumPending = false;
            state.ultimatumExpiryTick = 0;
            state.pending = true;
            state.reprisalTick = currentTick + delay;

            string textKey;

            switch (resolution)
            {
                case UltimatumResolution.Expired:
                    textKey = "GR_GoauldDomainReprisal_ExpiredText";
                    break;
                case UltimatumResolution.SymbioteDestroyed:
                    textKey = "GR_GoauldDomainReprisal_SymbioteDestroyedText";
                    break;
                default:
                    textKey = "GR_GoauldDomainReprisal_DefiedText";
                    break;
            }

            Find.LetterStack?.ReceiveLetter(
                "GR_GoauldDomainReprisal_WarningLabel".Translate(),
                textKey.Translate(
                    state.domainFaction.Name,
                    state.symbioteName,
                    state.formerHostLabel,
                    delay.ToStringTicksToPeriod()),
                LetterDefOf.ThreatBig,
                map == null
                    ? null
                    : new LookTargets(map.Center, map));

            GR_Log.Message(
                $"Scheduled Goa'uld domain reprisal from "
                + $"{state.domainFaction.Name} ({state.domainFaction.loadID}) "
                + $"after extraction ultimatum: resolution={resolution}, "
                + $"delay={delay}, points={state.raidPoints:0}.");
        }

        private static bool DemandedSymbioteWasDestroyed(
            GoauldDomainReprisalState state)
        {
            Pawn symbiote = state?.demandedSymbiote;
            return symbiote == null || symbiote.Destroyed || symbiote.Dead;
        }

        private static void MaintainUltimatumSedation(
            GoauldDomainReprisalState state)
        {
            Pawn symbiote = state?.demandedSymbiote;

            if (symbiote?.health == null
                || symbiote.Destroyed
                || symbiote.Dead
                || !symbiote.Spawned)
            {
                return;
            }

            Hediff anesthetic = symbiote.health.hediffSet
                .GetFirstHediffOfDef(HediffDefOf.Anesthetic);

            if (anesthetic == null)
            {
                anesthetic = HediffMaker.MakeHediff(
                    HediffDefOf.Anesthetic,
                    symbiote);
                symbiote.health.AddHediff(anesthetic);
            }

            anesthetic.Severity = Math.Max(anesthetic.Severity, 1f);
            symbiote.jobs?.StopAll();
        }

        private bool CreateUltimatumLetter(
            GoauldDomainReprisalState state)
        {
            LetterDef letterDef = DefDatabase<LetterDef>.GetNamedSilentFail(
                UltimatumLetterDefName);

            if (letterDef == null
                || !typeof(ChoiceLetter_GoauldExtractionUltimatum)
                    .IsAssignableFrom(letterDef.letterClass))
            {
                GR_Log.Error(
                    "Cannot create Goa'uld extraction ultimatum: "
                    + UltimatumLetterDefName
                    + " is missing or uses the wrong letter class.");
                return false;
            }

            ChoiceLetter_GoauldExtractionUltimatum letter
                = LetterMaker.MakeLetter(
                    "GR_GoauldDomainUltimatum_Label".Translate(),
                    "GR_GoauldDomainUltimatum_Text".Translate(
                        state.domainFaction.Name,
                        state.symbioteName,
                        state.formerHostLabel),
                    letterDef)
                as ChoiceLetter_GoauldExtractionUltimatum;

            if (letter == null)
            {
                GR_Log.Error(
                    "Failed to instantiate the Goa'uld extraction "
                    + "ultimatum letter.");
                return false;
            }

            letter.Initialize(state.domainFaction);
            letter.StartTimeout(UltimatumDurationTicks);
            Find.LetterStack.ReceiveLetter(letter);
            return true;
        }

        private static void RemoveUltimatumLetters(Faction domainFaction)
        {
            if (Find.LetterStack == null || domainFaction == null)
            {
                return;
            }

            List<Letter> letters = Find.LetterStack.LettersListForReading
                .OfType<ChoiceLetter_GoauldExtractionUltimatum>()
                .Where(letter => letter.DomainFaction == domainFaction)
                .Cast<Letter>()
                .ToList();

            foreach (Letter letter in letters)
            {
                Find.LetterStack.RemoveLetter(letter);
            }
        }

        private GoauldDomainReprisalState FindState(Faction faction)
        {
            return states.FirstOrDefault(
                candidate => candidate?.domainFaction == faction);
        }

        private Map FindTargetMap(GoauldDomainReprisalState state)
        {
            return Find.Maps?.FirstOrDefault(
                candidate => candidate?.uniqueID == state.targetMapUniqueId);
        }

        private static void DestroyDebugSymbiote(
            GoauldDomainReprisalState state)
        {
            Pawn symbiote = state?.demandedSymbiote;

            if (state?.debugGeneratedSymbiote == true
                && symbiote != null
                && !symbiote.Destroyed)
            {
                symbiote.Destroy(DestroyMode.Vanish);
            }
        }

        private static void SedateDebugSymbiote(Pawn symbiote)
        {
            if (symbiote?.health == null)
            {
                return;
            }

            Hediff anesthetic = HediffMaker.MakeHediff(
                HediffDefOf.Anesthetic,
                symbiote);
            anesthetic.Severity = 1f;
            symbiote.health.AddHediff(anesthetic);
            symbiote.jobs?.StopAll();
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
            ClearState(state, currentTick + CooldownTicks);
        }

        private static void ClearState(
            GoauldDomainReprisalState state,
            int nextEligibleTick)
        {
            state.ultimatumPending = false;
            state.pending = false;
            state.targetMapUniqueId = -1;
            state.ultimatumExpiryTick = 0;
            state.reprisalTick = 0;
            state.nextEligibleTick = nextEligibleTick;
            state.raidPoints = 0f;
            state.formerHostLabel = string.Empty;
            state.symbioteName = string.Empty;
            state.demandedSymbiote = null;
            state.debugGeneratedSymbiote = false;
            state.debugShortDelay = false;
        }

        private static int CurrentTick()
        {
            return Find.TickManager?.TicksGame ?? 0;
        }
    }

    internal enum UltimatumResolution
    {
        Defied,
        Expired,
        SymbioteDestroyed
    }
}
