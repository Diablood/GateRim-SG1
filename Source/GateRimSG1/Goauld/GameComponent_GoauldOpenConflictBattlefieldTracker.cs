using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GateRimSG1.Storytelling;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Schedules rare local battlefields for Goa'uld domain pairs that are
    /// already in persistent open conflict.
    ///
    /// The scheduler is exclusive to SG-1 Command, keeps one battlefield
    /// active at a time and shifts its hidden opportunity clock while a
    /// different storyteller is selected.
    /// </summary>
    public sealed class GameComponent_GoauldOpenConflictBattlefieldTracker
        : GameComponent
    {
        private const int CurrentSchemaVersion = 1;
        private const int CheckIntervalTicks = 250;
        private const int MinimumInitialDelayTicks = 480000;
        private const int MaximumInitialDelayTicks = 960000;
        private const int MinimumNoPairRetryTicks = 180000;
        private const int MaximumNoPairRetryTicks = 360000;
        private const int TemporaryFailureRetryTicks = 60000;
        private const int MinimumRecurrenceDelayTicks = 1200000;
        private const int MaximumRecurrenceDelayTicks = 2400000;

        private int schemaVersion = CurrentSchemaVersion;
        private int nextCheckTick;
        private int nextOpportunityTick;
        private int activeMapUniqueId = -1;
        private int lastFirstDomainLoadId = -1;
        private int lastSecondDomainLoadId = -1;
        private int lastLetterVariant = -1;
        private bool wasGateRimStorytellerActive;
        private int suspensionStartTick = -1;

        public GameComponent_GoauldOpenConflictBattlefieldTracker(Game game)
        {
        }

        public static GameComponent_GoauldOpenConflictBattlefieldTracker
            Current
            => Verse.Current.Game
                ?.GetComponent<
                    GameComponent_GoauldOpenConflictBattlefieldTracker>();

        public bool HasActiveBattlefield
            => ResolveActiveComponent() != null;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(
                ref schemaVersion,
                "goauldOpenConflictBattlefieldSchemaVersion",
                CurrentSchemaVersion);
            Scribe_Values.Look(
                ref nextCheckTick,
                "goauldOpenConflictBattlefieldNextCheckTick",
                0);
            Scribe_Values.Look(
                ref nextOpportunityTick,
                "goauldOpenConflictBattlefieldNextOpportunityTick",
                0);
            Scribe_Values.Look(
                ref activeMapUniqueId,
                "goauldOpenConflictBattlefieldActiveMapUniqueId",
                -1);
            Scribe_Values.Look(
                ref lastFirstDomainLoadId,
                "goauldOpenConflictBattlefieldLastFirstDomainLoadId",
                -1);
            Scribe_Values.Look(
                ref lastSecondDomainLoadId,
                "goauldOpenConflictBattlefieldLastSecondDomainLoadId",
                -1);
            Scribe_Values.Look(
                ref lastLetterVariant,
                "goauldOpenConflictBattlefieldLastLetterVariant",
                -1);
            Scribe_Values.Look(
                ref wasGateRimStorytellerActive,
                "goauldOpenConflictBattlefieldStorytellerWasActive",
                false);
            Scribe_Values.Look(
                ref suspensionStartTick,
                "goauldOpenConflictBattlefieldSuspensionStartTick",
                -1);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                NormalizeState();
            }
        }

        public override void FinalizeInit()
        {
            base.FinalizeInit();
            InitializeNow();
        }

        public override void StartedNewGame()
        {
            base.StartedNewGame();
            InitializeNow();
        }

        public override void LoadedGame()
        {
            base.LoadedGame();
            InitializeNow();
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
            ReconcileActiveBattlefield(currentTick);
            ObserveStorytellerBoundary(currentTick);

            if (HasActiveBattlefield
                || !GateRimStorytellerUtility.IsGateRimStorytellerActive
                || currentTick < nextOpportunityTick)
            {
                return;
            }

            TryRunNaturalOpportunity(currentTick);
        }

        public bool ForceBattlefieldDebug(Map map)
        {
            ReconcileActiveBattlefield(CurrentTick());

            if (map == null || HasActiveBattlefield)
            {
                return false;
            }

            GoauldInterDomainRelationState pair =
                GoauldOpenConflictBattlefieldUtility.SelectEligiblePair(
                    lastFirstDomainLoadId,
                    lastSecondDomainLoadId);

            return TryStartBattlefield(
                map,
                pair,
                debugForced: true,
                CurrentTick());
        }

        public void MakeOpportunityDueDebug()
        {
            nextOpportunityTick = CurrentTick();
        }

        public bool ForceWithdrawalDebug()
        {
            MapComponent_GoauldOpenConflictBattlefield component =
                ResolveActiveComponent();

            if (component == null)
            {
                return false;
            }

            component.OrderWithdrawalDebug();
            return true;
        }

        public void ResetDebug()
        {
            bool hadActiveBattlefield = HasActiveBattlefield;
            ForceWithdrawalDebug();
            lastFirstDomainLoadId = -1;
            lastSecondDomainLoadId = -1;
            lastLetterVariant = -1;
            suspensionStartTick = -1;
            wasGateRimStorytellerActive =
                GateRimStorytellerUtility.IsGateRimStorytellerActive;

            if (!hadActiveBattlefield)
            {
                activeMapUniqueId = -1;
                ScheduleInitial(CurrentTick());
            }
        }

        public void NotifyBattlefieldResolved(int mapUniqueId)
        {
            if (mapUniqueId != activeMapUniqueId)
            {
                return;
            }

            activeMapUniqueId = -1;
            ScheduleRecurrence(CurrentTick());
        }

        public string BuildOrchestrationSummary()
        {
            ReconcileActiveBattlefield(CurrentTick());

            return "eligible open-conflict pairs: "
                + GoauldOpenConflictBattlefieldUtility
                    .GetEligibleOpenConflictPairs().Count
                + "\nactive local battlefield: "
                + FormatBoolean(HasActiveBattlefield)
                + "\nnext local battlefield opportunity: "
                + FormatRemaining(nextOpportunityTick);
        }

        public string BuildDebugReport()
        {
            int currentTick = CurrentTick();
            ReconcileActiveBattlefield(currentTick);
            ObserveStorytellerBoundary(currentTick);

            List<GoauldInterDomainRelationState> pairs =
                GoauldOpenConflictBattlefieldUtility
                    .GetEligibleOpenConflictPairs();
            MapComponent_GoauldOpenConflictBattlefield component =
                ResolveActiveComponent();
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Goa'uld open-conflict battlefield report");
            builder.AppendLine();
            builder.AppendLine("schema: " + schemaVersion);
            builder.AppendLine(
                "SG-1 storyteller active: "
                + FormatBoolean(
                    GateRimStorytellerUtility
                        .IsGateRimStorytellerActive));
            builder.AppendLine(
                "eligible open-conflict pairs: " + pairs.Count);
            builder.AppendLine(
                "active battlefield map: "
                + (component == null
                    ? "<none>"
                    : component.MapLabel));
            builder.AppendLine(
                "next natural opportunity: "
                + FormatRemaining(nextOpportunityTick));
            builder.AppendLine(
                "suspension started: "
                + FormatTick(suspensionStartTick));
            builder.AppendLine(
                "last selected pair: " + FormatLastPair(pairs));
            builder.AppendLine(
                "last letter variant: "
                + (lastLetterVariant < 0
                    ? "<none>"
                    : lastLetterVariant.ToString()));

            if (component != null)
            {
                builder.AppendLine();
                builder.AppendLine(component.BuildDebugSummary());
            }

            builder.AppendLine();
            builder.AppendLine(
                "natural cadence: initial 8-16 days; recurrence 20-40 days");
            builder.AppendLine(
                "world-map battlefield site: reserved for 0.3.70-dev");
            return builder.ToString();
        }

        private void InitializeNow()
        {
            NormalizeState();
            int currentTick = CurrentTick();
            ReconcileActiveBattlefield(currentTick);
            ObserveStorytellerBoundary(currentTick);

            if (nextOpportunityTick <= 0 && !HasActiveBattlefield)
            {
                ScheduleInitial(currentTick);
            }
        }

        private void TryRunNaturalOpportunity(int currentTick)
        {
            List<GoauldInterDomainRelationState> pairs =
                GoauldOpenConflictBattlefieldUtility
                    .GetEligibleOpenConflictPairs();

            if (pairs.Count == 0)
            {
                nextOpportunityTick = currentTick
                    + Rand.RangeInclusive(
                        MinimumNoPairRetryTicks,
                        MaximumNoPairRetryTicks);
                return;
            }

            List<Map> maps = Find.Maps
                ?.Where(map =>
                    map != null
                    && map.IsPlayerHome
                    && map.mapPawns.FreeColonistsSpawnedCount > 0
                    && !TokraRelaySabotageMissionUtility
                        .HasActiveHostiles(map))
                .ToList()
                ?? new List<Map>();

            if (maps.Count == 0)
            {
                nextOpportunityTick = currentTick
                    + TemporaryFailureRetryTicks;
                return;
            }

            GoauldInterDomainRelationState pair =
                GoauldOpenConflictBattlefieldUtility.SelectEligiblePair(
                    lastFirstDomainLoadId,
                    lastSecondDomainLoadId);
            Map map = maps.RandomElement();

            if (!TryStartBattlefield(
                    map,
                    pair,
                    debugForced: false,
                    currentTick))
            {
                nextOpportunityTick = currentTick
                    + TemporaryFailureRetryTicks;
            }
        }

        private bool TryStartBattlefield(
            Map map,
            GoauldInterDomainRelationState pair,
            bool debugForced,
            int currentTick)
        {
            if (map == null || pair == null)
            {
                return false;
            }

            if (!GoauldOpenConflictBattlefieldUtility.TryStartOnMap(
                    map,
                    pair,
                    debugForced,
                    lastLetterVariant,
                    out MapComponent_GoauldOpenConflictBattlefield component,
                    out int letterVariant))
            {
                return false;
            }

            activeMapUniqueId = map.uniqueID;
            lastFirstDomainLoadId = pair.firstDomain?.loadID ?? -1;
            lastSecondDomainLoadId = pair.secondDomain?.loadID ?? -1;
            lastLetterVariant = letterVariant;
            nextOpportunityTick = 0;

            GR_Log.Message(
                "Started Goa'uld open-conflict battlefield between "
                + $"{pair.firstDomain?.Name ?? "<missing>"} and "
                + $"{pair.secondDomain?.Name ?? "<missing>"} on map "
                + $"{map.uniqueID}; debugForced={debugForced}; "
                + $"startTick={currentTick}.");
            return component != null;
        }

        private void ReconcileActiveBattlefield(int currentTick)
        {
            if (activeMapUniqueId < 0)
            {
                return;
            }

            MapComponent_GoauldOpenConflictBattlefield component =
                ResolveActiveComponent();

            if (component != null)
            {
                return;
            }

            activeMapUniqueId = -1;

            if (nextOpportunityTick <= currentTick)
            {
                ScheduleRecurrence(currentTick);
            }
        }

        private MapComponent_GoauldOpenConflictBattlefield
            ResolveActiveComponent()
        {
            if (activeMapUniqueId < 0 || Find.Maps == null)
            {
                return null;
            }

            Map map = Find.Maps.FirstOrDefault(
                candidate => candidate?.uniqueID == activeMapUniqueId);
            MapComponent_GoauldOpenConflictBattlefield component =
                map?.GetComponent<
                    MapComponent_GoauldOpenConflictBattlefield>();

            return component?.Active == true ? component : null;
        }

        private void ObserveStorytellerBoundary(int currentTick)
        {
            bool active =
                GateRimStorytellerUtility.IsGateRimStorytellerActive;

            if (!active && wasGateRimStorytellerActive)
            {
                suspensionStartTick = currentTick;
            }
            else if (!active && suspensionStartTick < 0)
            {
                suspensionStartTick = currentTick;
            }
            else if (active && !wasGateRimStorytellerActive)
            {
                ResumeAfterSuspension(currentTick);
            }

            wasGateRimStorytellerActive = active;
        }

        private void ResumeAfterSuspension(int currentTick)
        {
            if (suspensionStartTick < 0
                || currentTick <= suspensionStartTick)
            {
                suspensionStartTick = -1;
                return;
            }

            if (nextOpportunityTick > 0)
            {
                nextOpportunityTick += currentTick - suspensionStartTick;
            }

            suspensionStartTick = -1;
        }

        private void ScheduleInitial(int currentTick)
        {
            nextOpportunityTick = currentTick
                + Rand.RangeInclusive(
                    MinimumInitialDelayTicks,
                    MaximumInitialDelayTicks);
        }

        private void ScheduleRecurrence(int currentTick)
        {
            nextOpportunityTick = currentTick
                + Rand.RangeInclusive(
                    MinimumRecurrenceDelayTicks,
                    MaximumRecurrenceDelayTicks);
        }

        private void NormalizeState()
        {
            schemaVersion = CurrentSchemaVersion;
            nextCheckTick = Math.Max(0, nextCheckTick);
            nextOpportunityTick = Math.Max(0, nextOpportunityTick);
            activeMapUniqueId = Math.Max(-1, activeMapUniqueId);
            lastFirstDomainLoadId = Math.Max(-1, lastFirstDomainLoadId);
            lastSecondDomainLoadId = Math.Max(-1, lastSecondDomainLoadId);
            lastLetterVariant = Math.Max(-1, lastLetterVariant);
            suspensionStartTick = Math.Max(-1, suspensionStartTick);
        }

        private static int CurrentTick()
        {
            return Find.TickManager?.TicksGame ?? 0;
        }

        private string FormatLastPair(
            List<GoauldInterDomainRelationState> pairs)
        {
            if (lastFirstDomainLoadId < 0
                || lastSecondDomainLoadId < 0)
            {
                return "<none>";
            }

            GoauldInterDomainRelationState pair = pairs?.FirstOrDefault(
                state => state.firstDomain?.loadID == lastFirstDomainLoadId
                    && state.secondDomain?.loadID
                        == lastSecondDomainLoadId);

            return pair == null
                ? lastFirstDomainLoadId + ":" + lastSecondDomainLoadId
                : pair.firstDomain.Name + " <-> " + pair.secondDomain.Name;
        }

        private static string FormatBoolean(bool value)
        {
            return value ? "yes" : "no";
        }

        private static string FormatTick(int tick)
        {
            if (tick < 0)
            {
                return "<never>";
            }

            return tick
                + " ("
                + ((float)tick / GenDate.TicksPerDay).ToString("0.00")
                + " days)";
        }

        private static string FormatRemaining(int tick)
        {
            if (tick <= 0)
            {
                return "<not scheduled>";
            }

            int remaining = Math.Max(0, tick - CurrentTick());
            return remaining
                + " ticks ("
                + ((float)remaining / GenDate.TicksPerDay)
                    .ToString("0.00")
                + " days)";
        }
    }
}
