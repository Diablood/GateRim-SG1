using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GateRimSG1.Storytelling;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    public enum GoauldOpenConflictBattlefieldKind
    {
        None,
        Local,
        World
    }

    /// <summary>
    /// Shared persistent scheduler for local and world-map battlefields created
    /// by exact Goa'uld domain pairs already in open conflict.
    ///
    /// SG-1 Command owns one cadence, one active slot and one pair anti-repeat
    /// memory. When both forms are available, the scheduler alternates between
    /// local and world battlefields instead of allowing parallel occurrences.
    /// </summary>
    public sealed class GameComponent_GoauldOpenConflictBattlefieldTracker
        : GameComponent
    {
        private const int CurrentSchemaVersion = 2;
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
        private int activeWorldObjectId = -1;
        private int lastFirstDomainLoadId = -1;
        private int lastSecondDomainLoadId = -1;
        private int lastLetterVariant = -1;
        private GoauldOpenConflictBattlefieldKind lastBattlefieldKind
            = GoauldOpenConflictBattlefieldKind.None;
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
            => ResolveActiveComponent() != null
                || ResolveActiveWorldSite() != null;

        public bool HasActiveWorldSite
            => ResolveActiveWorldSite() != null;

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
                ref activeWorldObjectId,
                "goauldOpenConflictBattlefieldActiveWorldObjectId",
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
                ref lastBattlefieldKind,
                "goauldOpenConflictBattlefieldLastKind",
                GoauldOpenConflictBattlefieldKind.None);
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

            return TryStartLocalBattlefield(
                map,
                pair,
                debugForced: true,
                CurrentTick());
        }

        public bool ForceWorldSiteDebug(Map sourceMap)
        {
            ReconcileActiveBattlefield(CurrentTick());

            if (sourceMap == null || HasActiveBattlefield)
            {
                return false;
            }

            GoauldInterDomainRelationState pair =
                GoauldOpenConflictBattlefieldUtility.SelectEligiblePair(
                    lastFirstDomainLoadId,
                    lastSecondDomainLoadId);

            return TryStartWorldSite(
                sourceMap,
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

        public bool ExpireWorldSiteDebug()
        {
            WorldObject_GoauldOpenConflictBattlefieldSite site =
                ResolveActiveWorldSite();

            if (site == null || site.HasMap)
            {
                return false;
            }

            site.ExpireDebug();
            return true;
        }

        public void ResetDebug()
        {
            bool hadActiveBattlefield = HasActiveBattlefield;

            if (!ExpireWorldSiteDebug())
            {
                ForceWithdrawalDebug();
            }

            lastFirstDomainLoadId = -1;
            lastSecondDomainLoadId = -1;
            lastLetterVariant = -1;
            lastBattlefieldKind = GoauldOpenConflictBattlefieldKind.None;
            suspensionStartTick = -1;
            wasGateRimStorytellerActive =
                GateRimStorytellerUtility.IsGateRimStorytellerActive;

            if (!hadActiveBattlefield)
            {
                activeMapUniqueId = -1;
                activeWorldObjectId = -1;
                ScheduleInitial(CurrentTick());
            }
        }

        public void NotifyLocalBattlefieldResolved(int mapUniqueId)
        {
            if (activeWorldObjectId >= 0
                || mapUniqueId != activeMapUniqueId)
            {
                return;
            }

            activeMapUniqueId = -1;
            ScheduleRecurrence(CurrentTick());
        }

        public void NotifyWorldSiteMapGenerated(
            int worldObjectId,
            int mapUniqueId)
        {
            if (worldObjectId != activeWorldObjectId)
            {
                return;
            }

            activeMapUniqueId = mapUniqueId;
        }

        public void NotifyWorldSiteResolved(int worldObjectId)
        {
            if (worldObjectId != activeWorldObjectId)
            {
                return;
            }

            activeWorldObjectId = -1;
            activeMapUniqueId = -1;
            ScheduleRecurrence(CurrentTick());
        }

        public string BuildOrchestrationSummary()
        {
            ReconcileActiveBattlefield(CurrentTick());

            return "eligible open-conflict pairs: "
                + GoauldOpenConflictBattlefieldUtility
                    .GetEligibleOpenConflictPairs().Count
                + "\nshared active battlefield slot: "
                + ActiveKindLabel()
                + "\nnext shared battlefield opportunity: "
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
            WorldObject_GoauldOpenConflictBattlefieldSite site =
                ResolveActiveWorldSite();
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
                "shared active slot: " + ActiveKindLabel());
            builder.AppendLine(
                "active map: "
                + (component == null
                    ? "<none>"
                    : component.MapLabel));
            builder.AppendLine(
                "active world object ID: "
                + (site == null ? "<none>" : site.ID.ToString()));
            builder.AppendLine(
                "next natural opportunity: "
                + FormatRemaining(nextOpportunityTick));
            builder.AppendLine(
                "suspension started: "
                + FormatTick(suspensionStartTick));
            builder.AppendLine(
                "last selected pair: " + FormatLastPair(pairs));
            builder.AppendLine(
                "last battlefield kind: " + lastBattlefieldKind);
            builder.AppendLine(
                "last letter variant: "
                + (lastLetterVariant < 0
                    ? "<none>"
                    : lastLetterVariant.ToString()));

            if (site != null)
            {
                builder.AppendLine();
                builder.AppendLine("World site");
                builder.AppendLine(
                    "  pair: "
                    + (site.FirstDomain?.Name ?? "<missing>")
                    + " <-> "
                    + (site.SecondDomain?.Name ?? "<missing>"));
                builder.AppendLine("  tile: " + site.Tile);
                builder.AppendLine(
                    "  map generated: " + FormatBoolean(site.HasMap));
                builder.AppendLine(
                    "  remaining before ignored expiry: "
                    + site.RemainingTicks + " ticks");
                builder.AppendLine(
                    "  battle resolved: "
                    + FormatBoolean(site.BattlefieldResolved));
            }

            if (component != null)
            {
                builder.AppendLine();
                builder.AppendLine(component.BuildDebugSummary());
            }

            builder.AppendLine();
            builder.AppendLine(
                "shared cadence: initial 8-16 days; recurrence 20-40 days");
            builder.AppendLine(
                "world site: 6-18 tiles; ignored expiry after 8 days");
            builder.AppendLine(
                "local/world duplication: blocked by the shared active slot");
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

            List<Map> sourceMaps = Find.Maps
                ?.Where(map =>
                    map != null
                    && map.IsPlayerHome
                    && map.Tile != PlanetTile.Invalid
                    && map.mapPawns.FreeColonistsSpawnedCount > 0)
                .ToList()
                ?? new List<Map>();

            if (sourceMaps.Count == 0)
            {
                nextOpportunityTick = currentTick
                    + TemporaryFailureRetryTicks;
                return;
            }

            List<Map> localMaps = sourceMaps
                .Where(map => !TokraRelaySabotageMissionUtility
                    .HasActiveHostiles(map))
                .ToList();
            List<Map> worldSourceMaps = sourceMaps
                .Where(GoauldOpenConflictBattlefieldWorldSiteUtility
                    .CanCreateFrom)
                .ToList();
            bool localAvailable = localMaps.Count > 0;
            bool worldAvailable = worldSourceMaps.Count > 0;

            if (!localAvailable && !worldAvailable)
            {
                nextOpportunityTick = currentTick
                    + TemporaryFailureRetryTicks;
                return;
            }

            GoauldInterDomainRelationState pair =
                GoauldOpenConflictBattlefieldUtility.SelectEligiblePair(
                    lastFirstDomainLoadId,
                    lastSecondDomainLoadId);
            bool worldFirst = ShouldTryWorldFirst(
                localAvailable,
                worldAvailable);
            bool started = false;

            if (worldFirst)
            {
                started = TryStartWorldSite(
                    worldSourceMaps.RandomElement(),
                    pair,
                    debugForced: false,
                    currentTick);

                if (!started && localAvailable)
                {
                    started = TryStartLocalBattlefield(
                        localMaps.RandomElement(),
                        pair,
                        debugForced: false,
                        currentTick);
                }
            }
            else
            {
                started = TryStartLocalBattlefield(
                    localMaps.RandomElement(),
                    pair,
                    debugForced: false,
                    currentTick);

                if (!started && worldAvailable)
                {
                    started = TryStartWorldSite(
                        worldSourceMaps.RandomElement(),
                        pair,
                        debugForced: false,
                        currentTick);
                }
            }

            if (!started)
            {
                nextOpportunityTick = currentTick
                    + TemporaryFailureRetryTicks;
            }
        }

        private bool ShouldTryWorldFirst(
            bool localAvailable,
            bool worldAvailable)
        {
            if (!worldAvailable)
            {
                return false;
            }

            if (!localAvailable)
            {
                return true;
            }

            if (lastBattlefieldKind
                == GoauldOpenConflictBattlefieldKind.World)
            {
                return false;
            }

            if (lastBattlefieldKind
                == GoauldOpenConflictBattlefieldKind.Local)
            {
                return true;
            }

            return Rand.Value < 0.5f;
        }

        private bool TryStartLocalBattlefield(
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
            activeWorldObjectId = -1;
            RememberSelection(
                pair,
                GoauldOpenConflictBattlefieldKind.Local,
                letterVariant);
            nextOpportunityTick = 0;

            GR_Log.Message(
                "Started local Goa'uld open-conflict battlefield between "
                + $"{pair.firstDomain?.Name ?? "<missing>"} and "
                + $"{pair.secondDomain?.Name ?? "<missing>"} on map "
                + $"{map.uniqueID}; debugForced={debugForced}; "
                + $"startTick={currentTick}.");
            return component != null;
        }

        private bool TryStartWorldSite(
            Map sourceMap,
            GoauldInterDomainRelationState pair,
            bool debugForced,
            int currentTick)
        {
            if (sourceMap == null || pair == null)
            {
                return false;
            }

            if (!GoauldOpenConflictBattlefieldWorldSiteUtility.TryCreate(
                    sourceMap,
                    pair,
                    lastLetterVariant,
                    out WorldObject_GoauldOpenConflictBattlefieldSite site,
                    out int letterVariant))
            {
                return false;
            }

            activeMapUniqueId = -1;
            activeWorldObjectId = site.ID;
            RememberSelection(
                pair,
                GoauldOpenConflictBattlefieldKind.World,
                letterVariant);
            nextOpportunityTick = 0;

            GR_Log.Message(
                "Created Goa'uld open-conflict world battlefield between "
                + $"{pair.firstDomain?.Name ?? "<missing>"} and "
                + $"{pair.secondDomain?.Name ?? "<missing>"} at tile "
                + $"{site.Tile}; worldObjectId={site.ID}; "
                + $"debugForced={debugForced}; startTick={currentTick}.");
            return true;
        }

        private void RememberSelection(
            GoauldInterDomainRelationState pair,
            GoauldOpenConflictBattlefieldKind kind,
            int letterVariant)
        {
            lastFirstDomainLoadId = pair.firstDomain?.loadID ?? -1;
            lastSecondDomainLoadId = pair.secondDomain?.loadID ?? -1;
            lastBattlefieldKind = kind;
            lastLetterVariant = letterVariant;
        }

        private void ReconcileActiveBattlefield(int currentTick)
        {
            if (activeWorldObjectId >= 0)
            {
                WorldObject_GoauldOpenConflictBattlefieldSite site =
                    ResolveActiveWorldSite();

                if (site != null)
                {
                    if (site.HasMap)
                    {
                        activeMapUniqueId = site.Map.uniqueID;
                    }

                    return;
                }

                activeWorldObjectId = -1;
                activeMapUniqueId = -1;

                if (nextOpportunityTick <= currentTick)
                {
                    ScheduleRecurrence(currentTick);
                }

                return;
            }

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
            Map map = null;

            if (activeMapUniqueId >= 0 && Find.Maps != null)
            {
                map = Find.Maps.FirstOrDefault(
                    candidate => candidate?.uniqueID == activeMapUniqueId);
            }

            if (map == null)
            {
                WorldObject_GoauldOpenConflictBattlefieldSite site =
                    ResolveActiveWorldSite();

                if (site?.HasMap == true)
                {
                    map = site.Map;
                }
            }

            MapComponent_GoauldOpenConflictBattlefield component =
                map?.GetComponent<
                    MapComponent_GoauldOpenConflictBattlefield>();

            return component?.Active == true ? component : null;
        }

        private WorldObject_GoauldOpenConflictBattlefieldSite
            ResolveActiveWorldSite()
        {
            if (activeWorldObjectId < 0 || Find.WorldObjects == null)
            {
                return null;
            }

            return Find.WorldObjects.AllWorldObjects
                .OfType<WorldObject_GoauldOpenConflictBattlefieldSite>()
                .FirstOrDefault(worldObject =>
                    worldObject != null
                    && !worldObject.Destroyed
                    && worldObject.ID == activeWorldObjectId);
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
            activeWorldObjectId = Math.Max(-1, activeWorldObjectId);
            lastFirstDomainLoadId = Math.Max(-1, lastFirstDomainLoadId);
            lastSecondDomainLoadId = Math.Max(-1, lastSecondDomainLoadId);
            lastLetterVariant = Math.Max(-1, lastLetterVariant);
            suspensionStartTick = Math.Max(-1, suspensionStartTick);

            if (!Enum.IsDefined(
                    typeof(GoauldOpenConflictBattlefieldKind),
                    lastBattlefieldKind))
            {
                lastBattlefieldKind =
                    GoauldOpenConflictBattlefieldKind.None;
            }
        }

        private static int CurrentTick()
        {
            return Find.TickManager?.TicksGame ?? 0;
        }

        private string ActiveKindLabel()
        {
            WorldObject_GoauldOpenConflictBattlefieldSite site =
                ResolveActiveWorldSite();

            if (site != null)
            {
                return site.HasMap ? "world site map" : "world site";
            }

            return ResolveActiveComponent() != null
                ? "local battlefield"
                : "empty";
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
