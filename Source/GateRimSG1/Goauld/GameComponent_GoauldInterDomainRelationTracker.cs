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
    /// Stores one strategic relation for every unordered pair of Goa'uld
    /// System Lord faction instances.
    ///
    /// Automatic transitions are exclusive to the GateRim SG-1 storyteller.
    /// Their clocks are shifted forward while another storyteller is active,
    /// so returning to SG-1 Command resumes the simulation instead of applying
    /// an accumulated backlog immediately.
    /// </summary>
    public sealed class GameComponent_GoauldInterDomainRelationTracker
        : GameComponent
    {
        private const int CurrentSchemaVersion = 1;
        private const int CheckIntervalTicks = 250;
        private const int MinimumInitialDelayTicks = 480000;
        private const int MaximumInitialDelayTicks = 960000;
        private const int MinimumPairDelayTicks = 720000;
        private const int MaximumPairDelayTicks = 1440000;
        private const int MinimumGlobalDelayTicks = 300000;
        private const int MaximumGlobalDelayTicks = 600000;

        private int schemaVersion = CurrentSchemaVersion;
        private List<GoauldInterDomainRelationState> states =
            new List<GoauldInterDomainRelationState>();
        private int nextCheckTick;
        private int nextGlobalTransitionTick;
        private bool wasGateRimStorytellerActive;
        private int suspensionStartTick = -1;
        private int lastTransitionFirstLoadId = -1;
        private int lastTransitionSecondLoadId = -1;
        private string lastReportKey;

        public GameComponent_GoauldInterDomainRelationTracker(Game game)
        {
        }

        public static GameComponent_GoauldInterDomainRelationTracker Current
            => Verse.Current.Game
                ?.GetComponent<
                    GameComponent_GoauldInterDomainRelationTracker>();

        public bool AutomaticTransitionsActive
            => GateRimStorytellerUtility.IsGateRimStorytellerActive
                && GoauldSystemLordFactionUtility.GetAllFactions().Count >= 2;

        public int ActivePairCount
        {
            get
            {
                ReconcileAllPairs();
                return states.Count(IsPairActive);
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(
                ref schemaVersion,
                "goauldInterDomainRelationSchemaVersion",
                CurrentSchemaVersion);
            Scribe_Collections.Look(
                ref states,
                "goauldInterDomainRelationStates",
                LookMode.Deep);
            Scribe_Values.Look(
                ref nextCheckTick,
                "goauldInterDomainRelationNextCheckTick",
                0);
            Scribe_Values.Look(
                ref nextGlobalTransitionTick,
                "goauldInterDomainRelationNextGlobalTransitionTick",
                0);
            Scribe_Values.Look(
                ref wasGateRimStorytellerActive,
                "goauldInterDomainRelationStorytellerWasActive",
                false);
            Scribe_Values.Look(
                ref suspensionStartTick,
                "goauldInterDomainRelationSuspensionStartTick",
                -1);
            Scribe_Values.Look(
                ref lastTransitionFirstLoadId,
                "goauldInterDomainRelationLastFirstLoadId",
                -1);
            Scribe_Values.Look(
                ref lastTransitionSecondLoadId,
                "goauldInterDomainRelationLastSecondLoadId",
                -1);
            Scribe_Values.Look(
                ref lastReportKey,
                "goauldInterDomainRelationLastReportKey");

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
            ReconcileAllPairs();
            ObserveStorytellerBoundary(currentTick);

            if (!GateRimStorytellerUtility.IsGateRimStorytellerActive
                || currentTick < nextGlobalTransitionTick)
            {
                return;
            }

            TryRunAutomaticTransition(currentTick);
        }

        public void ReconcileAllPairs()
        {
            NormalizeState();

            List<Faction> activeDomains =
                GoauldSystemLordFactionUtility.GetAllFactions();
            int currentTick = CurrentTick();

            for (int firstIndex = 0;
                firstIndex < activeDomains.Count;
                firstIndex++)
            {
                for (int secondIndex = firstIndex + 1;
                    secondIndex < activeDomains.Count;
                    secondIndex++)
                {
                    GetOrCreateState(
                        activeDomains[firstIndex],
                        activeDomains[secondIndex],
                        currentTick);
                }
            }

            if (nextGlobalTransitionTick <= 0)
            {
                nextGlobalTransitionTick = currentTick
                    + StableDelay(
                        activeDomains.Count,
                        MinimumGlobalDelayTicks,
                        MaximumGlobalDelayTicks);
            }
        }

        public List<GoauldInterDomainRelationState> Snapshot(
            bool includeInactive = true)
        {
            ReconcileAllPairs();

            return states
                .Where(state => includeInactive || IsPairActive(state))
                .OrderBy(state => DomainName(state.firstDomain))
                .ThenBy(state => DomainName(state.secondDomain))
                .ToList();
        }

        public bool ForceNextTransitionDebug()
        {
            ReconcileAllPairs();

            List<GoauldInterDomainRelationState> candidates = states
                .Where(IsPairActive)
                .ToList();

            GoauldInterDomainRelationState selected =
                SelectPairWithAntiRepetition(candidates);

            if (selected == null)
            {
                return false;
            }

            ApplyTransition(
                selected,
                SelectNextRelation(selected.relation),
                CurrentTick(),
                sendReport: true,
                debugForced: true);
            return true;
        }

        public bool SetFirstPairRelationDebug(
            GoauldInterDomainRelation relation)
        {
            ReconcileAllPairs();

            GoauldInterDomainRelationState state = states
                .Where(IsPairActive)
                .OrderBy(candidate => candidate.firstDomain.loadID)
                .ThenBy(candidate => candidate.secondDomain.loadID)
                .FirstOrDefault();

            if (state == null)
            {
                return false;
            }

            ApplyTransition(
                state,
                relation,
                CurrentTick(),
                sendReport: state.relation != relation,
                debugForced: true);
            return true;
        }

        public int SetAllActivePairsRelationDebug(
            GoauldInterDomainRelation relation)
        {
            ReconcileAllPairs();

            List<GoauldInterDomainRelationState> activeStates = states
                .Where(IsPairActive)
                .ToList();
            int currentTick = CurrentTick();

            foreach (GoauldInterDomainRelationState state in activeStates)
            {
                ApplyTransition(
                    state,
                    relation,
                    currentTick,
                    sendReport: false,
                    debugForced: true);
            }

            return activeStates.Count;
        }

        public void ResetDebug()
        {
            states.Clear();
            nextCheckTick = 0;
            nextGlobalTransitionTick = 0;
            lastTransitionFirstLoadId = -1;
            lastTransitionSecondLoadId = -1;
            lastReportKey = null;
            suspensionStartTick = -1;
            wasGateRimStorytellerActive =
                GateRimStorytellerUtility.IsGateRimStorytellerActive;
            ReconcileAllPairs();
        }

        public string BuildOrchestrationSummary()
        {
            ReconcileAllPairs();
            ObserveStorytellerBoundary(CurrentTick());

            return "stored relation pairs: " + states.Count
                + "\nactive relation pairs: " + states.Count(IsPairActive)
                + "\nautomatic relation transitions active: "
                + FormatBoolean(AutomaticTransitionsActive)
                + "\nnext strategic transition check: "
                + FormatRemaining(nextGlobalTransitionTick);
        }

        public string BuildDebugReport()
        {
            ReconcileAllPairs();
            ObserveStorytellerBoundary(CurrentTick());

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Goa'uld inter-domain relation report");
            builder.AppendLine();
            builder.AppendLine("schema: " + schemaVersion);
            builder.AppendLine(
                "SG-1 storyteller active: "
                + FormatBoolean(
                    GateRimStorytellerUtility
                        .IsGateRimStorytellerActive));
            builder.AppendLine(
                "automatic transitions active: "
                + FormatBoolean(AutomaticTransitionsActive));
            builder.AppendLine("stored pairs: " + states.Count);
            builder.AppendLine(
                "active pairs: " + states.Count(IsPairActive));
            builder.AppendLine(
                "next global transition: "
                + FormatRemaining(nextGlobalTransitionTick));
            builder.AppendLine(
                "suspension started: "
                + FormatTick(suspensionStartTick));
            builder.AppendLine(
                "last transitioned pair: "
                + FormatLastPair());
            builder.AppendLine(
                "last RP report key: "
                + (lastReportKey ?? "<none>"));
            builder.AppendLine();

            if (states.Count == 0)
            {
                builder.AppendLine(
                    "No relation pair exists. At least two active Goa'uld "
                    + "domains are required.");
                return builder.ToString();
            }

            foreach (GoauldInterDomainRelationState state in Snapshot())
            {
                builder.AppendLine(
                    DomainName(state.firstDomain)
                    + " <-> "
                    + DomainName(state.secondDomain));
                builder.AppendLine(
                    "  active pair: "
                    + FormatBoolean(IsPairActive(state)));
                builder.AppendLine(
                    "  relation: "
                    + RelationLabel(state.relation));
                builder.AppendLine(
                    "  previous: "
                    + RelationLabel(state.previousRelation));
                builder.AppendLine(
                    "  transitions: " + state.transitionCount);
                builder.AppendLine(
                    "  established: "
                    + FormatTick(state.establishedTick));
                builder.AppendLine(
                    "  last transition: "
                    + FormatTick(state.lastTransitionTick));
                builder.AppendLine(
                    "  next transition: "
                    + FormatRemaining(state.nextTransitionTick));
            }

            builder.AppendLine();
            builder.AppendLine(
                "relation reports announce eligibility only; a transition "
                + "never launches a raid or battlefield immediately");
            builder.AppendLine(
                "published effects: bounded natural-raid pressure, "
                + "non-stacking doctrine-weight influence, "
                + "standard/delayed/joint alliance raid outcomes and "
                + "open-conflict battlefield opportunities");
            builder.AppendLine(
                "still inactive: territorial expansion, settlement "
                + "destruction and player-goodwill changes");

            return builder.ToString();
        }

        private void InitializeNow()
        {
            NormalizeState();
            ReconcileAllPairs();
            ObserveStorytellerBoundary(CurrentTick());
        }

        private void TryRunAutomaticTransition(int currentTick)
        {
            List<GoauldInterDomainRelationState> dueStates = states
                .Where(state =>
                    IsPairActive(state)
                    && currentTick >= state.nextTransitionTick)
                .ToList();

            GoauldInterDomainRelationState selected =
                SelectPairWithAntiRepetition(dueStates);

            if (selected == null)
            {
                int nextPairTick = states
                    .Where(IsPairActive)
                    .Select(state => state.nextTransitionTick)
                    .DefaultIfEmpty(
                        currentTick + MinimumGlobalDelayTicks)
                    .Min();
                nextGlobalTransitionTick = Math.Max(
                    currentTick + CheckIntervalTicks,
                    nextPairTick);
                return;
            }

            ApplyTransition(
                selected,
                SelectNextRelation(selected.relation),
                currentTick,
                sendReport: true,
                debugForced: false);
        }

        private void ApplyTransition(
            GoauldInterDomainRelationState state,
            GoauldInterDomainRelation relation,
            int currentTick,
            bool sendReport,
            bool debugForced)
        {
            if (state == null)
            {
                return;
            }

            GoauldInterDomainRelation previous = state.relation;

            if (previous != relation)
            {
                state.previousRelation = previous;
                state.relation = relation;
                state.lastTransitionTick = currentTick;
                state.transitionCount++;
                RememberPair(state);

                if (sendReport)
                {
                    SendRelationReport(state);
                }

                GR_Log.Message(
                    "Changed Goa'uld inter-domain relation between "
                    + $"{DomainName(state.firstDomain)} "
                    + $"({state.firstDomain?.loadID}) and "
                    + $"{DomainName(state.secondDomain)} "
                    + $"({state.secondDomain?.loadID}) from {previous} to "
                    + $"{relation}{(debugForced ? " [debug]" : string.Empty)}.");
            }

            state.nextTransitionTick = currentTick
                + Rand.RangeInclusive(
                    MinimumPairDelayTicks,
                    MaximumPairDelayTicks);
            nextGlobalTransitionTick = currentTick
                + Rand.RangeInclusive(
                    MinimumGlobalDelayTicks,
                    MaximumGlobalDelayTicks);
        }

        private void SendRelationReport(
            GoauldInterDomainRelationState state)
        {
            int variantCount = 3;
            int variant = Rand.Range(0, variantCount);
            string prefix = ReportPrefix(state.relation);
            string reportKey = prefix + "_Text" + variant;

            if (reportKey == lastReportKey && variantCount > 1)
            {
                variant = (variant + Rand.Range(1, variantCount))
                    % variantCount;
                reportKey = prefix + "_Text" + variant;
            }

            lastReportKey = reportKey;

            Find.LetterStack?.ReceiveLetter(
                (prefix + "_Label").Translate(),
                reportKey.Translate(
                    DomainName(state.firstDomain),
                    DomainName(state.secondDomain)),
                LetterDefOf.NeutralEvent,
                default(LookTargets));
        }

        private GoauldInterDomainRelationState GetOrCreateState(
            Faction firstDomain,
            Faction secondDomain,
            int currentTick)
        {
            Canonicalize(ref firstDomain, ref secondDomain);

            GoauldInterDomainRelationState state = states.FirstOrDefault(
                candidate =>
                    candidate.firstDomain == firstDomain
                    && candidate.secondDomain == secondDomain);

            if (state != null)
            {
                if (state.nextTransitionTick <= 0)
                {
                    state.nextTransitionTick = currentTick
                        + StableInitialDelay(firstDomain, secondDomain);
                }

                return state;
            }

            state = new GoauldInterDomainRelationState
            {
                firstDomain = firstDomain,
                secondDomain = secondDomain,
                relation = GoauldInterDomainRelation.Neutral,
                previousRelation = GoauldInterDomainRelation.Neutral,
                establishedTick = currentTick,
                nextTransitionTick = currentTick
                    + StableInitialDelay(firstDomain, secondDomain)
            };
            states.Add(state);
            return state;
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

            int pausedTicks = currentTick - suspensionStartTick;

            foreach (GoauldInterDomainRelationState state in states)
            {
                int pairSuspensionStart = Math.Max(
                    suspensionStartTick,
                    state.establishedTick);
                int pairPausedTicks = Math.Max(
                    0,
                    currentTick - pairSuspensionStart);

                if (state.nextTransitionTick > 0)
                {
                    state.nextTransitionTick += pairPausedTicks;
                }
            }

            if (nextGlobalTransitionTick > 0)
            {
                nextGlobalTransitionTick += pausedTicks;
            }

            suspensionStartTick = -1;
        }

        private GoauldInterDomainRelationState SelectPairWithAntiRepetition(
            List<GoauldInterDomainRelationState> candidates)
        {
            if (candidates == null || candidates.Count == 0)
            {
                return null;
            }

            List<GoauldInterDomainRelationState> alternatives = candidates
                .Where(state => !IsLastTransitionPair(state))
                .ToList();

            List<GoauldInterDomainRelationState> pool =
                alternatives.Count > 0 ? alternatives : candidates;
            return pool.RandomElement();
        }

        private static GoauldInterDomainRelation SelectNextRelation(
            GoauldInterDomainRelation current)
        {
            float roll = Rand.Value;

            switch (current)
            {
                case GoauldInterDomainRelation.Neutral:
                    return roll < 0.75f
                        ? GoauldInterDomainRelation.Rivalry
                        : GoauldInterDomainRelation.Alliance;
                case GoauldInterDomainRelation.Rivalry:
                    return roll < 0.60f
                        ? GoauldInterDomainRelation.OpenConflict
                        : GoauldInterDomainRelation.Neutral;
                case GoauldInterDomainRelation.OpenConflict:
                    return GoauldInterDomainRelation.Truce;
                case GoauldInterDomainRelation.Truce:
                    if (roll < 0.50f)
                    {
                        return GoauldInterDomainRelation.Neutral;
                    }

                    return roll < 0.85f
                        ? GoauldInterDomainRelation.Rivalry
                        : GoauldInterDomainRelation.Alliance;
                case GoauldInterDomainRelation.Alliance:
                    return roll < 0.65f
                        ? GoauldInterDomainRelation.Neutral
                        : GoauldInterDomainRelation.Rivalry;
                default:
                    return GoauldInterDomainRelation.Neutral;
            }
        }

        private void NormalizeState()
        {
            schemaVersion = CurrentSchemaVersion;
            states = states
                ?.Where(state =>
                    state?.firstDomain != null
                    && state.secondDomain != null
                    && state.firstDomain != state.secondDomain
                    && GoauldSystemLordFactionUtility
                        .IsSystemLordFaction(state.firstDomain)
                    && GoauldSystemLordFactionUtility
                        .IsSystemLordFaction(state.secondDomain))
                .Select(state =>
                {
                    Canonicalize(
                        ref state.firstDomain,
                        ref state.secondDomain);
                    state.relation = NormalizeRelation(state.relation);
                    state.previousRelation = NormalizeRelation(
                        state.previousRelation);
                    state.transitionCount = Math.Max(
                        0,
                        state.transitionCount);
                    state.establishedTick = Math.Max(
                        0,
                        state.establishedTick);
                    state.lastTransitionTick = Math.Max(
                        -1,
                        state.lastTransitionTick);
                    state.nextTransitionTick = Math.Max(
                        0,
                        state.nextTransitionTick);
                    return state;
                })
                .GroupBy(state =>
                    state.firstDomain.loadID + ":" + state.secondDomain.loadID)
                .Select(group => group.Last())
                .ToList()
                ?? new List<GoauldInterDomainRelationState>();

            nextCheckTick = Math.Max(0, nextCheckTick);
            nextGlobalTransitionTick = Math.Max(
                0,
                nextGlobalTransitionTick);
            suspensionStartTick = Math.Max(-1, suspensionStartTick);
        }


        private static GoauldInterDomainRelation NormalizeRelation(
            GoauldInterDomainRelation relation)
        {
            return Enum.IsDefined(typeof(GoauldInterDomainRelation), relation)
                ? relation
                : GoauldInterDomainRelation.Neutral;
        }

        private static void Canonicalize(
            ref Faction firstDomain,
            ref Faction secondDomain)
        {
            if (firstDomain != null
                && secondDomain != null
                && firstDomain.loadID > secondDomain.loadID)
            {
                Faction temporary = firstDomain;
                firstDomain = secondDomain;
                secondDomain = temporary;
            }
        }

        private static bool IsPairActive(
            GoauldInterDomainRelationState state)
        {
            return state?.firstDomain != null
                && state.secondDomain != null
                && !state.firstDomain.defeated
                && !state.secondDomain.defeated
                && GoauldSystemLordFactionUtility
                    .IsSystemLordFaction(state.firstDomain)
                && GoauldSystemLordFactionUtility
                    .IsSystemLordFaction(state.secondDomain);
        }

        private bool IsLastTransitionPair(
            GoauldInterDomainRelationState state)
        {
            return state != null
                && state.firstDomain?.loadID == lastTransitionFirstLoadId
                && state.secondDomain?.loadID
                    == lastTransitionSecondLoadId;
        }

        private void RememberPair(
            GoauldInterDomainRelationState state)
        {
            lastTransitionFirstLoadId =
                state.firstDomain?.loadID ?? -1;
            lastTransitionSecondLoadId =
                state.secondDomain?.loadID ?? -1;
        }

        private static int StableInitialDelay(
            Faction firstDomain,
            Faction secondDomain)
        {
            int mixed = unchecked(
                (firstDomain?.loadID ?? 0) * 397
                ^ (secondDomain?.loadID ?? 0) * 7919);
            return StableDelay(
                mixed,
                MinimumInitialDelayTicks,
                MaximumInitialDelayTicks);
        }

        private static int StableDelay(
            int seed,
            int minimum,
            int maximum)
        {
            uint mixed = unchecked((uint)seed * 2654435761u);
            int span = maximum - minimum + 1;
            return minimum + (int)(mixed % (uint)span);
        }

        private static int CurrentTick()
        {
            return Find.TickManager?.TicksGame ?? 0;
        }

        private static string DomainName(Faction faction)
        {
            return faction?.Name ?? "<missing domain>";
        }

        private static string RelationLabel(
            GoauldInterDomainRelation relation)
        {
            return RelationKey(relation).Translate().ToString();
        }

        private static string RelationKey(
            GoauldInterDomainRelation relation)
        {
            switch (relation)
            {
                case GoauldInterDomainRelation.Rivalry:
                    return "GR_GoauldInterDomainRelation_Rivalry";
                case GoauldInterDomainRelation.OpenConflict:
                    return "GR_GoauldInterDomainRelation_OpenConflict";
                case GoauldInterDomainRelation.Truce:
                    return "GR_GoauldInterDomainRelation_Truce";
                case GoauldInterDomainRelation.Alliance:
                    return "GR_GoauldInterDomainRelation_Alliance";
                default:
                    return "GR_GoauldInterDomainRelation_Neutral";
            }
        }

        private static string ReportPrefix(
            GoauldInterDomainRelation relation)
        {
            switch (relation)
            {
                case GoauldInterDomainRelation.Rivalry:
                    return "GR_GoauldInterDomainReport_Rivalry";
                case GoauldInterDomainRelation.OpenConflict:
                    return "GR_GoauldInterDomainReport_OpenConflict";
                case GoauldInterDomainRelation.Truce:
                    return "GR_GoauldInterDomainReport_Truce";
                case GoauldInterDomainRelation.Alliance:
                    return "GR_GoauldInterDomainReport_Alliance";
                default:
                    return "GR_GoauldInterDomainReport_Neutral";
            }
        }

        private string FormatLastPair()
        {
            if (lastTransitionFirstLoadId < 0
                || lastTransitionSecondLoadId < 0)
            {
                return "<none>";
            }

            GoauldInterDomainRelationState state = states.FirstOrDefault(
                candidate => IsLastTransitionPair(candidate));
            return state == null
                ? lastTransitionFirstLoadId
                    + ":"
                    + lastTransitionSecondLoadId
                : DomainName(state.firstDomain)
                    + " <-> "
                    + DomainName(state.secondDomain);
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
