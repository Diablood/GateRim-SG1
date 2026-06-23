using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GateRimSG1.Missions;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    public enum TokraIntroductionArcState
    {
        Uninitialized = 0,
        WaitingForOpportunity = 1,
        Offered = 2,
        Active = 3,
        RetryDelay = 4,
        Completed = 5
    }

    /// <summary>
    /// Persistent state for the Tok'ra introduction arc.
    ///
    /// The arc is unique only after the key artifact has been recovered. An
    /// ignored, expired or failed attempt schedules another hidden opportunity
    /// after a long variable delay. Once due, the arc opens a player-facing
    /// offer and creates a hostile world site only after explicit acceptance.
    /// </summary>
    public sealed class GameComponent_TokraIntroductionArc : GameComponent
    {
        private const int StateCheckIntervalTicks = 2500;
        private const int TicksPerDay = 60000;
        private const string InitialDelayContext = "TokraIntroduction.Initial";
        private const string ExpiredDelayContext = "TokraIntroduction.OfferExpired";
        private const string FailedDelayContext = "TokraIntroduction.AttemptFailed";
        private const string OfferTextRuntimeKey = "introductionOfferTextKey";
        private const string OfferLetterDefName = "SG1_TokraIntroductionArtifactOffer";
        private const string ReplacementDueTickCounterKey
            = "cipherModuleReplacementDueTick";
        private const string ReplacementCountCounterKey
            = "cipherModuleReplacementCount";

        private TokraIntroductionArcState state
            = TokraIntroductionArcState.Uninitialized;
        private bool initialized;
        private bool completedPermanently;
        private int nextStateCheckTick;
        private int nextOpportunityTick;
        private int offerExpiryTick;
        private int completionTick;
        private int attemptCount;
        private int failedAttemptCount;
        private int expiredOfferCount;
        private int declinedOfferCount;
        private int offerSourceMapId = -1;
        private int lastOfferTextVariantIndex = -1;
        private int lastSuccessTextVariantIndex = -1;
        private GateRimMissionRuntimeData runtime
            = new GateRimMissionRuntimeData();

        public GameComponent_TokraIntroductionArc(Game game)
        {
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(
                ref state,
                "tokraIntroductionArcState",
                TokraIntroductionArcState.Uninitialized);
            Scribe_Values.Look(
                ref initialized,
                "tokraIntroductionArcInitialized",
                false);
            Scribe_Values.Look(
                ref completedPermanently,
                "tokraIntroductionArcCompletedPermanently",
                false);
            Scribe_Values.Look(
                ref nextStateCheckTick,
                "tokraIntroductionArcNextStateCheckTick",
                0);
            Scribe_Values.Look(
                ref nextOpportunityTick,
                "tokraIntroductionArcNextOpportunityTick",
                0);
            Scribe_Values.Look(
                ref offerExpiryTick,
                "tokraIntroductionArcOfferExpiryTick",
                0);
            Scribe_Values.Look(
                ref completionTick,
                "tokraIntroductionArcCompletionTick",
                0);
            Scribe_Values.Look(
                ref attemptCount,
                "tokraIntroductionArcAttemptCount",
                0);
            Scribe_Values.Look(
                ref failedAttemptCount,
                "tokraIntroductionArcFailedAttemptCount",
                0);
            Scribe_Values.Look(
                ref expiredOfferCount,
                "tokraIntroductionArcExpiredOfferCount",
                0);
            Scribe_Values.Look(
                ref declinedOfferCount,
                "tokraIntroductionArcDeclinedOfferCount",
                0);
            Scribe_Values.Look(
                ref offerSourceMapId,
                "tokraIntroductionArcOfferSourceMapId",
                -1);
            Scribe_Values.Look(
                ref lastOfferTextVariantIndex,
                "tokraIntroductionArcLastOfferTextVariantIndex",
                -1);
            Scribe_Values.Look(
                ref lastSuccessTextVariantIndex,
                "tokraIntroductionArcLastSuccessTextVariantIndex",
                -1);
            Scribe_Deep.Look(ref runtime, "tokraIntroductionArcRuntime");

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                runtime = runtime ?? new GateRimMissionRuntimeData();
                nextStateCheckTick = 0;

                if (completedPermanently)
                {
                    state = TokraIntroductionArcState.Completed;
                    nextOpportunityTick = 0;
                    offerExpiryTick = 0;
                }
            }
        }

        public override void GameComponentTick()
        {
            base.GameComponentTick();

            if (Find.TickManager == null)
            {
                return;
            }

            int currentTick = Find.TickManager.TicksGame;

            if (currentTick < nextStateCheckTick)
            {
                return;
            }

            nextStateCheckTick = currentTick + StateCheckIntervalTicks;

            if (ReconcileFinishedSecureCommunicationsResearch())
            {
                return;
            }

            EnsureInitialized(currentTick);

            if (completedPermanently)
            {
                ReconcileCompletedArcStudy(currentTick);
                return;
            }

            if (state == TokraIntroductionArcState.Offered)
            {
                if (offerExpiryTick > 0 && currentTick >= offerExpiryTick)
                {
                    ExpireOfferInternal(currentTick, notifyPlayer: true);
                    return;
                }

                EnsureOfferChoiceLetter();
                return;
            }

            if (state == TokraIntroductionArcState.Active)
            {
                TickActiveAttempt();
                return;
            }

            Map offerMap = FindOfferMap();

            if (offerMap != null && CanOffer(offerMap))
            {
                ForceOfferInternal(offerMap);
            }
        }

        public static bool CanOffer(Map map)
        {
            GameComponent_TokraIntroductionArc tracker = GetCurrentTracker();

            if (tracker == null || map == null)
            {
                return false;
            }

            if (tracker.ReconcileFinishedSecureCommunicationsResearch())
            {
                return false;
            }

            tracker.EnsureInitialized(GetCurrentTick());

            return !tracker.completedPermanently
                && (tracker.state
                        == TokraIntroductionArcState.WaitingForOpportunity
                    || tracker.state == TokraIntroductionArcState.RetryDelay)
                && tracker.nextOpportunityTick > 0
                && GetCurrentTick() >= tracker.nextOpportunityTick;
        }

        public static bool DebugForceOffer(Map map)
        {
            GameComponent_TokraIntroductionArc tracker = GetCurrentTracker();
            return tracker != null && tracker.ForceOfferInternal(map);
        }

        public static bool DebugAcceptOffer(Map map)
        {
            GameComponent_TokraIntroductionArc tracker = GetCurrentTracker();
            return tracker != null && tracker.AcceptOfferInternal(map);
        }

        public static bool DebugDeclineOffer()
        {
            GameComponent_TokraIntroductionArc tracker = GetCurrentTracker();
            return tracker != null && tracker.DeclineOfferInternal();
        }

        public static bool IsCurrentOfferLetter(int attemptNumber)
        {
            GameComponent_TokraIntroductionArc tracker = GetCurrentTracker();

            return tracker != null
                && tracker.state == TokraIntroductionArcState.Offered
                && !tracker.completedPermanently
                && tracker.attemptCount == attemptNumber;
        }

        public static bool AcceptOfferFromLetter(int attemptNumber)
        {
            GameComponent_TokraIntroductionArc tracker = GetCurrentTracker();

            return tracker != null
                && tracker.attemptCount == attemptNumber
                && tracker.AcceptOfferInternal(null);
        }

        public static bool DeclineOfferFromLetter(int attemptNumber)
        {
            GameComponent_TokraIntroductionArc tracker = GetCurrentTracker();

            return tracker != null
                && tracker.attemptCount == attemptNumber
                && tracker.DeclineOfferInternal();
        }

        public static bool DebugFailAttempt()
        {
            GameComponent_TokraIntroductionArc tracker = GetCurrentTracker();
            return tracker != null && tracker.FailAttemptInternal();
        }

        public static bool DebugExpireOffer()
        {
            GameComponent_TokraIntroductionArc tracker = GetCurrentTracker();

            if (tracker == null
                || tracker.state != TokraIntroductionArcState.Offered)
            {
                return false;
            }

            tracker.ExpireOfferInternal(GetCurrentTick(), notifyPlayer: true);
            return true;
        }

        public static bool DebugMoveActiveSiteToWarningWindow()
        {
            GameComponent_TokraIntroductionArc tracker = GetCurrentTracker();
            WorldObject_TokraIntroductionArtifactSite site = tracker == null
                ? null
                : TokraIntroductionArtifactMissionUtility.FindWorldSite(
                    tracker.runtime);

            return tracker != null
                && tracker.state == TokraIntroductionArcState.Active
                && site != null
                && site.DebugMoveToDeadlineWarningWindow();
        }

        public static bool DebugExpireActiveSite()
        {
            GameComponent_TokraIntroductionArc tracker = GetCurrentTracker();
            WorldObject_TokraIntroductionArtifactSite site = tracker == null
                ? null
                : TokraIntroductionArtifactMissionUtility.FindWorldSite(
                    tracker.runtime);

            return tracker != null
                && tracker.state == TokraIntroductionArcState.Active
                && site != null
                && site.DebugExpireNow();
        }

        public static bool DebugDestroyTrackedArtifact()
        {
            GameComponent_TokraIntroductionArc tracker = GetCurrentTracker();
            WorldObject_TokraIntroductionArtifactSite site = tracker == null
                ? null
                : TokraIntroductionArtifactMissionUtility.FindWorldSite(
                    tracker.runtime);
            MapComponent_TokraIntroductionArtifactMission component
                = site?.HasMap == true
                    ? site.Map.GetComponent<
                        MapComponent_TokraIntroductionArtifactMission>()
                    : null;

            return tracker != null
                && tracker.state == TokraIntroductionArcState.Active
                && component != null
                && component.DebugDestroyTrackedArtifact();
        }

        public static bool DebugMakeOpportunityDue()
        {
            GameComponent_TokraIntroductionArc tracker = GetCurrentTracker();

            if (tracker == null || tracker.completedPermanently)
            {
                return false;
            }

            tracker.EnsureInitialized(GetCurrentTick());

            if (tracker.state == TokraIntroductionArcState.Offered
                || tracker.state == TokraIntroductionArcState.Active)
            {
                return false;
            }

            tracker.state = TokraIntroductionArcState.WaitingForOpportunity;
            tracker.nextOpportunityTick = GetCurrentTick();
            tracker.runtime.phaseId = "waiting";
            return true;
        }

        public static bool DebugRecoverArtifact(Map map)
        {
            GameComponent_TokraIntroductionArc tracker = GetCurrentTracker();
            return tracker != null && tracker.RecoverArtifactInternal(map);
        }

        public static bool DebugReset()
        {
            GameComponent_TokraIntroductionArc tracker = GetCurrentTracker();

            if (tracker == null)
            {
                return false;
            }

            tracker.ResetInternal();
            return true;
        }

        public static bool NotifyArtifactRecovered(Thing artifact)
        {
            GameComponent_TokraIntroductionArc tracker = GetCurrentTracker();

            if (tracker == null
                || artifact == null
                || artifact.def != GR_DefOf.SG1_TokraIntroductionArtifact)
            {
                return false;
            }

            return tracker.CompleteInternal(artifact);
        }

        public static bool IsCompletedPermanently
        {
            get
            {
                GameComponent_TokraIntroductionArc tracker
                    = GetCurrentTracker();
                return tracker != null && tracker.completedPermanently;
            }
        }

        public static string TrackedArtifactThingId
        {
            get
            {
                GameComponent_TokraIntroductionArc tracker
                    = GetCurrentTracker();
                return tracker?.runtime?.GetString(
                    TokraIntroductionArtifactMissionUtility
                        .ArtifactThingIdKey);
            }
        }

        public static bool IsTrackedRecoveredArtifact(Thing artifact)
        {
            if (artifact == null
                || artifact.def != GR_DefOf.SG1_TokraIntroductionArtifact)
            {
                return false;
            }

            GameComponent_TokraIntroductionArc tracker
                = GetCurrentTracker();
            string trackedThingId = tracker?.runtime?.GetString(
                TokraIntroductionArtifactMissionUtility
                    .ArtifactThingIdKey);

            return tracker != null
                && tracker.completedPermanently
                && !trackedThingId.NullOrEmpty()
                && artifact.ThingID == trackedThingId;
        }

        public static Thing FindTrackedRecoveredArtifact()
        {
            GameComponent_TokraIntroductionArc tracker
                = GetCurrentTracker();

            if (tracker == null || !tracker.completedPermanently)
            {
                return null;
            }

            return tracker.FindTrackedArtifactInPlayerPossession();
        }

        public static int CipherModuleReplacementCount
        {
            get
            {
                GameComponent_TokraIntroductionArc tracker
                    = GetCurrentTracker();
                return tracker?.GetRuntimeCounter(
                    ReplacementCountCounterKey) ?? 0;
            }
        }

        public static string GetCipherModuleReplacementDebugText()
        {
            GameComponent_TokraIntroductionArc tracker
                = GetCurrentTracker();
            int dueTick = tracker?.GetRuntimeCounter(
                ReplacementDueTickCounterKey) ?? 0;

            return FormatRemainingTicks(dueTick, GetCurrentTick());
        }

        public static bool DebugDestroyRecoveredCipherModule()
        {
            GameComponent_TokraIntroductionArc tracker
                = GetCurrentTracker();
            Thing artifact = tracker?.FindTrackedArtifactInPlayerPossession();

            if (tracker == null
                || artifact == null
                || TokraCipherModuleStudyUtility.IsAnalysisComplete
                || GR_DefOf.SG1_TokraSecureCommunications?.IsFinished == true)
            {
                return false;
            }

            artifact.Destroy(DestroyMode.Vanish);
            tracker.nextStateCheckTick = 0;
            return true;
        }

        public static bool DebugMakeCipherModuleReplacementDue()
        {
            GameComponent_TokraIntroductionArc tracker
                = GetCurrentTracker();

            if (tracker == null
                || !tracker.completedPermanently
                || TokraCipherModuleStudyUtility.IsAnalysisComplete
                || GR_DefOf.SG1_TokraSecureCommunications?.IsFinished == true
                || tracker.FindTrackedArtifactInPlayerPossession() != null)
            {
                return false;
            }

            tracker.SetRuntimeCounter(
                ReplacementDueTickCounterKey,
                System.Math.Max(1, GetCurrentTick()));
            tracker.nextStateCheckTick = 0;
            return true;
        }

        public static bool DebugPrepareForCompletedCipherAnalysis()
        {
            GameComponent_TokraIntroductionArc tracker
                = GetCurrentTracker();

            if (tracker == null)
            {
                return false;
            }

            if (tracker.ReconcileFinishedSecureCommunicationsResearch())
            {
                return true;
            }

            if (!tracker.completedPermanently)
            {
                tracker.CompleteSilently("analysisCompleted");
            }

            return true;
        }

        public static bool NotifyCipherModuleAnalysisCompleted(
            Thing artifact = null)
        {
            GameComponent_TokraIntroductionArc tracker
                = GetCurrentTracker();

            return tracker != null
                && tracker.MarkCipherModuleAnalysisCompleted(artifact);
        }

        public static bool NotifyArtifactCreated(
            WorldObject_TokraIntroductionArtifactSite site,
            Thing artifact)
        {
            GameComponent_TokraIntroductionArc tracker = GetCurrentTracker();

            if (tracker == null
                || site == null
                || artifact == null
                || artifact.def != GR_DefOf.SG1_TokraIntroductionArtifact
                || !tracker.IsTrackedSite(site))
            {
                return false;
            }

            tracker.runtime.SetString(
                TokraIntroductionArtifactMissionUtility.ArtifactThingIdKey,
                artifact.ThingID);
            return true;
        }

        public static bool NotifySiteFailed(
            WorldObject_TokraIntroductionArtifactSite site,
            string failureTextId)
        {
            GameComponent_TokraIntroductionArc tracker = GetCurrentTracker();

            if (tracker == null
                || site == null
                || !tracker.IsTrackedSite(site))
            {
                return false;
            }

            return tracker.FailAttemptInternal(failureTextId, site);
        }

        public static string GetDebugStateReport(Map map)
        {
            GameComponent_TokraIntroductionArc tracker = GetCurrentTracker();

            if (tracker == null)
            {
                return "Tok'ra introduction arc tracker unavailable.";
            }

            int currentTick = GetCurrentTick();
            tracker.EnsureInitialized(currentTick);
            GateRimMissionDef definition = GetDefinition();
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("GateRim SG-1 Tok'ra introduction arc");
            builder.AppendLine("State: " + tracker.state);
            builder.AppendLine(
                "Completed permanently: " + tracker.completedPermanently);
            builder.AppendLine("Attempts opened: " + tracker.attemptCount);
            builder.AppendLine(
                "Failed attempts: " + tracker.failedAttemptCount);
            builder.AppendLine(
                "Expired offers: " + tracker.expiredOfferCount);
            builder.AppendLine(
                "Declined offers: " + tracker.declinedOfferCount);
            builder.AppendLine(
                "Next opportunity: "
                + FormatRemainingTicks(
                    tracker.nextOpportunityTick,
                    currentTick));
            builder.AppendLine(
                "Offer expiry: "
                + FormatRemainingTicks(tracker.offerExpiryTick, currentTick));
            builder.AppendLine(
                "Completion tick: "
                + (tracker.completionTick > 0
                    ? tracker.completionTick.ToString()
                    : "none"));
            builder.AppendLine("Automatic player offer: enabled");
            builder.AppendLine(
                "Offer source map: "
                + (tracker.offerSourceMapId >= 0
                    ? tracker.offerSourceMapId.ToString()
                    : "none"));
            WorldObject_TokraIntroductionArtifactSite activeSite
                = TokraIntroductionArtifactMissionUtility.FindWorldSite(
                    tracker.runtime);
            builder.AppendLine(
                "World site: "
                + (activeSite?.ID.ToString() ?? "none"));
            builder.AppendLine(
                "Site deadline: "
                + (activeSite != null
                    ? FormatRemainingTicks(activeSite.ExpiryTick, currentTick)
                    : "none"));
            builder.AppendLine(
                "Site deadline warning sent: "
                + (activeSite != null
                    ? activeSite.DeadlineWarningSent.ToString()
                    : "n/a"));
            builder.AppendLine(
                "Site map entered: "
                + (activeSite != null
                    ? activeSite.MapEntered.ToString()
                    : "n/a"));
            builder.AppendLine(
                "Tracked artifact: "
                + (tracker.runtime.GetString(
                    TokraIntroductionArtifactMissionUtility
                        .ArtifactThingIdKey)
                    ?? "none"));
            builder.AppendLine(
                "Definition: "
                + (definition?.defName ?? "missing"));
            builder.AppendLine(
                "Artifact: "
                + (definition?.objectiveThingDef?.defName ?? "missing"));
            builder.AppendLine(
                "Threat snapshot: "
                + tracker.runtime.baseThreatPoints.ToString("0")
                + " -> "
                + tracker.runtime.scaledThreatPoints.ToString("0"));

            if (definition?.difficulty != null)
            {
                builder.AppendLine(
                    "Difficulty profile: factor "
                    + definition.difficulty.pointsFactor.ToString("0.00")
                    + ", points "
                    + definition.difficulty.minimumPoints.ToString("0")
                    + "-"
                    + definition.difficulty.maximumPoints.ToString("0"));
            }

            if (definition?.introduction != null)
            {
                builder.AppendLine(
                    "Introduction defenders: "
                    + definition.introduction.defenderMinimumCount
                    + "-"
                    + definition.introduction.defenderMaximumCount);
            }

            builder.AppendLine(
                "Can offer now: " + CanOffer(map));

            AppendDelayRange(builder, definition, InitialDelayContext);
            AppendDelayRange(builder, definition, ExpiredDelayContext);
            AppendDelayRange(builder, definition, FailedDelayContext);

            return builder.ToString().TrimEnd();
        }

        private bool ReconcileFinishedSecureCommunicationsResearch()
        {
            ResearchProjectDef project
                = GR_DefOf.SG1_TokraSecureCommunications;

            if (project?.IsFinished != true)
            {
                return false;
            }

            TokraCipherModuleStudyUtility
                .ForceCompleteForFinishedResearch();
            CompleteSilently("researchCompleted");
            MarkCipherModuleAnalysisCompleted();
            return true;
        }

        private void ReconcileCompletedArcStudy(int currentTick)
        {
            if (TokraCipherModuleStudyUtility.IsAnalysisComplete)
            {
                MarkCipherModuleAnalysisCompleted();
                return;
            }

            Thing trackedArtifact = FindTrackedArtifactInPlayerPossession();

            if (trackedArtifact != null)
            {
                SetRuntimeCounter(ReplacementDueTickCounterKey, 0);
                runtime.phaseId = "completedAwaitingStudy";
                return;
            }

            int replacementDueTick = GetRuntimeCounter(
                ReplacementDueTickCounterKey);

            if (replacementDueTick <= 0)
            {
                replacementDueTick = currentTick + Rand.RangeInclusive(
                    TokraCipherModuleStudyUtility
                        .ReplacementMinimumDelayTicks,
                    TokraCipherModuleStudyUtility
                        .ReplacementMaximumDelayTicks);
                SetRuntimeCounter(
                    ReplacementDueTickCounterKey,
                    replacementDueTick);
                runtime.phaseId = "replacementPending";

                GR_Log.Message(
                    "Scheduled a replacement Tok'ra cipher module for tick "
                    + replacementDueTick
                    + ".");
                return;
            }

            if (currentTick >= replacementDueTick)
            {
                TryIssueReplacementCipherModule();
            }
        }

        private bool TryIssueReplacementCipherModule()
        {
            Map map = FindOfferMap();

            if (map == null
                || GR_DefOf.SG1_TokraIntroductionArtifact == null
                || TokraCipherModuleStudyUtility.IsAnalysisComplete
                || GR_DefOf.SG1_TokraSecureCommunications?.IsFinished == true)
            {
                return false;
            }

            Thing created = ThingMaker.MakeThing(
                GR_DefOf.SG1_TokraIntroductionArtifact);
            Thing placed;

            if (!GenPlace.TryPlaceThing(
                    created,
                    map.Center,
                    map,
                    ThingPlaceMode.Near,
                    out placed))
            {
                if (!created.Destroyed)
                {
                    created.Destroy(DestroyMode.Vanish);
                }

                return false;
            }

            TrackRecoveredArtifact(placed);
            SetRuntimeCounter(ReplacementDueTickCounterKey, 0);
            SetRuntimeCounter(
                ReplacementCountCounterKey,
                GetRuntimeCounter(ReplacementCountCounterKey) + 1);
            runtime.phaseId = "completedAwaitingStudy";

            Find.LetterStack?.ReceiveLetter(
                "GR_TokraCipherStudy_ReplacementLabel".Translate(),
                "GR_TokraCipherStudy_ReplacementText".Translate(),
                LetterDefOf.NeutralEvent,
                placed);

            GR_Log.Message(
                "Issued replacement Tok'ra cipher module "
                + placed.ThingID
                + ".");
            return true;
        }

        private void CompleteSilently(string phaseId)
        {
            runtime = runtime ?? new GateRimMissionRuntimeData();
            WorldObject_TokraIntroductionArtifactSite site
                = TokraIntroductionArtifactMissionUtility.FindWorldSite(
                    runtime);

            initialized = true;
            completedPermanently = true;
            state = TokraIntroductionArcState.Completed;
            completionTick = completionTick > 0
                ? completionTick
                : GetCurrentTick();
            nextOpportunityTick = 0;
            offerExpiryTick = 0;
            offerSourceMapId = -1;
            runtime.missionDefName = GetDefinition()?.defName;
            runtime.phaseId = phaseId;
            RemoveOfferChoiceLetters();
            site?.NotifyArcResolved(true);
        }

        private bool MarkCipherModuleAnalysisCompleted(Thing artifact = null)
        {
            if (!completedPermanently)
            {
                CompleteSilently("analysisCompleted");
            }

            Thing trackedArtifact = artifact;

            if (trackedArtifact == null
                || !IsTrackedRecoveredArtifact(trackedArtifact))
            {
                trackedArtifact = FindTrackedArtifactInPlayerPossession();
            }

            runtime.SetString(
                TokraIntroductionArtifactMissionUtility.ArtifactThingIdKey,
                null);
            SetRuntimeCounter(ReplacementDueTickCounterKey, 0);
            runtime.phaseId = GR_DefOf.SG1_TokraSecureCommunications
                    ?.IsFinished == true
                ? "researchCompleted"
                : "analysisCompleted";
            nextStateCheckTick = 0;

            if (trackedArtifact != null && !trackedArtifact.Destroyed)
            {
                trackedArtifact.Destroy(DestroyMode.Vanish);
            }

            return true;
        }

        private void TrackRecoveredArtifact(Thing artifact)
        {
            if (artifact == null)
            {
                return;
            }

            runtime = runtime ?? new GateRimMissionRuntimeData();
            runtime.SetString(
                TokraIntroductionArtifactMissionUtility.ArtifactThingIdKey,
                artifact.ThingID);
        }

        private int GetRuntimeCounter(string key)
        {
            runtime = runtime ?? new GateRimMissionRuntimeData();
            runtime.counters = runtime.counters
                ?? new Dictionary<string, int>();

            int value;
            return !key.NullOrEmpty()
                    && runtime.counters.TryGetValue(key, out value)
                ? value
                : 0;
        }

        private void SetRuntimeCounter(string key, int value)
        {
            if (key.NullOrEmpty())
            {
                return;
            }

            runtime = runtime ?? new GateRimMissionRuntimeData();
            runtime.counters = runtime.counters
                ?? new Dictionary<string, int>();

            if (value <= 0)
            {
                runtime.counters.Remove(key);
                return;
            }

            runtime.counters[key] = value;
        }

        private void EnsureInitialized(int currentTick)
        {
            if (initialized)
            {
                return;
            }

            initialized = true;
            runtime = runtime ?? new GateRimMissionRuntimeData();

            if (completedPermanently)
            {
                state = TokraIntroductionArcState.Completed;
                return;
            }

            state = TokraIntroductionArcState.WaitingForOpportunity;
            runtime.Reset();
            runtime.missionDefName = GetDefinition()?.defName;
            runtime.phaseId = "waiting";
            nextOpportunityTick = currentTick
                + GetConfiguredDelayTicks(InitialDelayContext);
        }

        private bool ForceOfferInternal(Map map)
        {
            if (map == null || Find.TickManager == null)
            {
                return false;
            }

            int currentTick = Find.TickManager.TicksGame;
            EnsureInitialized(currentTick);

            if (completedPermanently
                || state == TokraIntroductionArcState.Offered
                || state == TokraIntroductionArcState.Active)
            {
                return false;
            }

            GateRimMissionDef definition = GetDefinition();

            if (definition == null
                || definition.objectiveThingDef
                    != GR_DefOf.SG1_TokraIntroductionArtifact
                || !TokraIntroductionArtifactMissionUtility
                    .CanCreateWorldSite(map, definition.introduction))
            {
                GR_Log.Error(
                    "Cannot open the Tok'ra introduction offer: its "
                    + "MissionDef or key artifact is unavailable.");
                return false;
            }

            GateRimMissionDifficultySnapshot snapshot
                = GateRimMissionFramework.CaptureDifficulty(
                    map,
                    definition.difficulty);

            runtime.Reset();
            runtime.missionDefName = definition.defName;
            runtime.phaseId = "offered";
            runtime.baseThreatPoints = snapshot.BaseThreatPoints;
            runtime.scaledThreatPoints = snapshot.ScaledThreatPoints;
            runtime.difficultyFactor = snapshot.Factor;

            state = TokraIntroductionArcState.Offered;
            attemptCount++;
            nextOpportunityTick = 0;
            offerSourceMapId = map.uniqueID;
            offerExpiryTick = currentTick
                + Math.Max(1, definition.timing.offerDurationTicks);

            int selectedIndex;
            string textKey = GateRimMissionFramework.SelectTextKey(
                definition.texts.offerLetterTexts,
                lastOfferTextVariantIndex,
                out selectedIndex);

            if (selectedIndex >= 0)
            {
                lastOfferTextVariantIndex = selectedIndex;
            }

            runtime.SetString(
                OfferTextRuntimeKey,
                textKey ?? "GR_TokraIntroduction_OfferText1");
            RemoveOfferChoiceLetters();
            EnsureOfferChoiceLetter();

            GR_Log.Message(
                "Opened Tok'ra introduction artifact offer: attempt="
                + attemptCount
                + "; threat="
                + snapshot.ScaledThreatPoints.ToString("0")
                + "; expiryTick="
                + offerExpiryTick
                + ".");
            return true;
        }

        private bool AcceptOfferInternal(Map map)
        {
            if (state != TokraIntroductionArcState.Offered
                || completedPermanently)
            {
                return false;
            }

            map = map ?? ResolveOfferMap();
            GateRimMissionDef definition = GetDefinition();

            if (map == null
                || definition == null
                || !TokraIntroductionArtifactMissionUtility
                    .TryCreateWorldSite(
                        map,
                        definition,
                        runtime,
                        out WorldObject_TokraIntroductionArtifactSite site))
            {
                Messages.Message(
                    "GR_TokraIntroduction_SiteCreationFailed".Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            state = TokraIntroductionArcState.Active;
            offerExpiryTick = 0;
            runtime.phaseId = "active";
            RemoveOfferChoiceLetters();

            Messages.Message(
                (definition.texts?.acceptedMessageKey
                    ?? "GR_TokraIntroduction_Accepted").Translate(),
                site,
                MessageTypeDefOf.PositiveEvent,
                historical: true);

            GR_Log.Message(
                "Accepted Tok'ra introduction artifact attempt "
                + attemptCount
                + "; site="
                + site.ID
                + ".");
            return true;
        }

        private bool DeclineOfferInternal()
        {
            if (state != TokraIntroductionArcState.Offered
                || completedPermanently)
            {
                return false;
            }

            declinedOfferCount++;
            RemoveOfferChoiceLetters();
            Messages.Message(
                "GR_TokraIntroduction_Declined".Translate(),
                MessageTypeDefOf.NeutralEvent,
                historical: true);
            ScheduleRetry(ExpiredDelayContext, GetCurrentTick());

            GR_Log.Message(
                "Declined Tok'ra introduction artifact offer; retryTick="
                + nextOpportunityTick
                + ".");
            return true;
        }

        private bool FailAttemptInternal(
            string failureTextId = null,
            WorldObject_TokraIntroductionArtifactSite site = null)
        {
            if (state != TokraIntroductionArcState.Active
                || completedPermanently)
            {
                return false;
            }

            failedAttemptCount++;
            string messageKey = ResolveFailureMessageKey(failureTextId);
            site = site
                ?? TokraIntroductionArtifactMissionUtility.FindWorldSite(
                    runtime);

            if (site != null)
            {
                Messages.Message(
                    messageKey.Translate(),
                    site,
                    MessageTypeDefOf.NegativeEvent,
                    historical: true);
            }
            else
            {
                Messages.Message(
                    messageKey.Translate(),
                    MessageTypeDefOf.NegativeEvent,
                    historical: true);
            }

            site?.NotifyArcResolved(false);
            ScheduleRetry(FailedDelayContext, GetCurrentTick());

            GR_Log.Message(
                "Failed Tok'ra introduction artifact attempt "
                + attemptCount
                + "; reason="
                + (failureTextId ?? "generic")
                + "; retryTick="
                + nextOpportunityTick
                + ".");
            return true;
        }

        private void ExpireOfferInternal(int currentTick, bool notifyPlayer)
        {
            expiredOfferCount++;

            if (notifyPlayer)
            {
                string messageKey = GetDefinition()?.texts
                    ?.offerExpiredMessageKey;

                Messages.Message(
                    (messageKey
                        ?? "GR_TokraIntroduction_OfferExpired").Translate(),
                    MessageTypeDefOf.NeutralEvent,
                    historical: true);
            }

            ScheduleRetry(ExpiredDelayContext, currentTick);

            GR_Log.Message(
                "Expired Tok'ra introduction artifact offer; retryTick="
                + nextOpportunityTick
                + ".");
        }

        private void ScheduleRetry(string contextKey, int currentTick)
        {
            state = TokraIntroductionArcState.RetryDelay;
            runtime.Reset();
            runtime.missionDefName = GetDefinition()?.defName;
            runtime.phaseId = "retry";
            offerExpiryTick = 0;
            offerSourceMapId = -1;
            RemoveOfferChoiceLetters();
            nextOpportunityTick = currentTick
                + GetConfiguredDelayTicks(contextKey);
        }

        private bool RecoverArtifactInternal(Map map)
        {
            if (map == null
                || GR_DefOf.SG1_TokraIntroductionArtifact == null)
            {
                return false;
            }

            if (ReconcileFinishedSecureCommunicationsResearch())
            {
                return true;
            }

            if (TokraCipherModuleStudyUtility.IsAnalysisComplete)
            {
                CompleteSilently("analysisCompleted");
                MarkCipherModuleAnalysisCompleted();
                return true;
            }

            Thing existing = FindTrackedArtifactInPlayerPossession();

            if (existing != null)
            {
                if (!completedPermanently)
                {
                    return CompleteInternal(existing);
                }

                return true;
            }

            Thing created = ThingMaker.MakeThing(
                GR_DefOf.SG1_TokraIntroductionArtifact);
            Thing placed;

            if (!GenPlace.TryPlaceThing(
                    created,
                    map.Center,
                    map,
                    ThingPlaceMode.Near,
                    out placed))
            {
                if (!created.Destroyed)
                {
                    created.Destroy(DestroyMode.Vanish);
                }

                return false;
            }

            if (completedPermanently)
            {
                TrackRecoveredArtifact(placed);
                runtime.phaseId = "completedAwaitingStudy";
                SetRuntimeCounter(ReplacementDueTickCounterKey, 0);
                nextStateCheckTick = 0;
                return true;
            }

            return CompleteInternal(placed);
        }

        private bool CompleteInternal(Thing artifact)
        {
            if (completedPermanently
                || artifact == null
                || artifact.def != GR_DefOf.SG1_TokraIntroductionArtifact)
            {
                return false;
            }

            GateRimMissionDef definition = GetDefinition();
            WorldObject_TokraIntroductionArtifactSite site
                = TokraIntroductionArtifactMissionUtility.FindWorldSite(
                    runtime);
            TrackRecoveredArtifact(artifact);
            completedPermanently = true;
            state = TokraIntroductionArcState.Completed;
            completionTick = GetCurrentTick();
            nextOpportunityTick = 0;
            offerExpiryTick = 0;
            runtime.phaseId = "completed";
            offerSourceMapId = -1;
            RemoveOfferChoiceLetters();
            site?.NotifyArcResolved(true);

            int selectedIndex;
            string textKey = GateRimMissionFramework.SelectTextKey(
                definition?.texts?.successLetterTexts,
                lastSuccessTextVariantIndex,
                out selectedIndex);

            if (selectedIndex >= 0)
            {
                lastSuccessTextVariantIndex = selectedIndex;
            }

            Find.LetterStack?.ReceiveLetter(
                (definition?.texts?.successLetterLabelKey
                    ?? "GR_TokraIntroduction_CompletedLabel").Translate(),
                (textKey
                    ?? "GR_TokraIntroduction_CompletedText1").Translate(),
                LetterDefOf.PositiveEvent,
                artifact);

            GR_Log.Message(
                "Completed Tok'ra introduction artifact arc permanently "
                + "on attempt "
                + attemptCount
                + ".");
            return true;
        }

        private void ResetInternal()
        {
            TokraIntroductionArtifactMissionUtility.FindWorldSite(runtime)
                ?.NotifyArcResolved(false);
            initialized = false;
            completedPermanently = false;
            state = TokraIntroductionArcState.Uninitialized;
            nextStateCheckTick = 0;
            nextOpportunityTick = 0;
            offerExpiryTick = 0;
            completionTick = 0;
            attemptCount = 0;
            failedAttemptCount = 0;
            expiredOfferCount = 0;
            declinedOfferCount = 0;
            offerSourceMapId = -1;
            RemoveOfferChoiceLetters();
            lastOfferTextVariantIndex = -1;
            lastSuccessTextVariantIndex = -1;
            runtime = new GateRimMissionRuntimeData();
            EnsureInitialized(GetCurrentTick());
        }

        private void TickActiveAttempt()
        {
            Thing recoveredArtifact = FindTrackedArtifactInPlayerPossession();

            if (recoveredArtifact != null)
            {
                CompleteInternal(recoveredArtifact);
                return;
            }

            WorldObject_TokraIntroductionArtifactSite site
                = TokraIntroductionArtifactMissionUtility.FindWorldSite(
                    runtime);

            if (site == null)
            {
                FailAttemptInternal("siteLost");
            }
        }

        private void EnsureOfferChoiceLetter()
        {
            if (state != TokraIntroductionArcState.Offered
                || Find.LetterStack == null
                || Find.TickManager == null)
            {
                return;
            }

            bool alreadyPresent = Find.LetterStack.LettersListForReading
                .OfType<ChoiceLetter_TokraIntroductionArtifactOffer>()
                .Any(letter => letter.AttemptNumber == attemptCount);

            if (alreadyPresent)
            {
                return;
            }

            LetterDef letterDef = DefDatabase<LetterDef>.GetNamedSilentFail(
                OfferLetterDefName);
            GateRimMissionDef definition = GetDefinition();
            string textKey = runtime.GetString(OfferTextRuntimeKey)
                ?? "GR_TokraIntroduction_OfferText1";

            if (letterDef == null
                || definition?.texts == null
                || !typeof(ChoiceLetter_TokraIntroductionArtifactOffer)
                    .IsAssignableFrom(letterDef.letterClass))
            {
                GR_Log.Error(
                    "Cannot create the Tok'ra introduction choice letter: "
                    + OfferLetterDefName
                    + " is missing or uses the wrong letter class.");
                return;
            }

            RemoveLegacyOfferLetters(definition);

            ChoiceLetter_TokraIntroductionArtifactOffer letter
                = LetterMaker.MakeLetter(
                    definition.texts.offerLetterLabelKey.Translate(),
                    textKey.Translate(),
                    letterDef)
                as ChoiceLetter_TokraIntroductionArtifactOffer;

            if (letter == null)
            {
                GR_Log.Error(
                    "Failed to instantiate the Tok'ra introduction "
                    + "choice letter.");
                return;
            }

            letter.Initialize(attemptCount);
            int remainingTicks = Math.Max(
                1,
                offerExpiryTick - Find.TickManager.TicksGame);
            letter.StartTimeout(remainingTicks);
            Find.LetterStack.ReceiveLetter(letter);
        }

        private void RemoveOfferChoiceLetters()
        {
            if (Find.LetterStack == null)
            {
                return;
            }

            List<Letter> letters = Find.LetterStack.LettersListForReading
                .Where(letter =>
                    letter is ChoiceLetter_TokraIntroductionArtifactOffer)
                .ToList();

            foreach (Letter letter in letters)
            {
                Find.LetterStack.RemoveLetter(letter);
            }
        }

        private static void RemoveLegacyOfferLetters(
            GateRimMissionDef definition)
        {
            if (Find.LetterStack == null
                || definition?.texts == null)
            {
                return;
            }

            string expectedLabel = definition.texts.offerLetterLabelKey
                .Translate()
                .Resolve();
            List<Letter> legacyLetters = Find.LetterStack
                .LettersListForReading
                .Where(letter =>
                    !(letter is ChoiceLetter_TokraIntroductionArtifactOffer)
                    && letter.Label.Resolve() == expectedLabel)
                .ToList();

            foreach (Letter letter in legacyLetters)
            {
                Find.LetterStack.RemoveLetter(letter);
            }
        }

        private static Map FindOfferMap()
        {
            return Find.Maps?.FirstOrDefault(map =>
                map != null
                && map.IsPlayerHome
                && map.mapPawns?.FreeColonistsSpawned != null
                && map.mapPawns.FreeColonistsSpawned.Count > 0);
        }

        private Map ResolveOfferMap()
        {
            if (offerSourceMapId >= 0 && Find.Maps != null)
            {
                Map storedMap = Find.Maps.FirstOrDefault(map =>
                    map != null && map.uniqueID == offerSourceMapId);

                if (storedMap != null)
                {
                    return storedMap;
                }
            }

            return FindOfferMap();
        }

        private Thing FindTrackedArtifactInPlayerPossession()
        {
            string artifactThingId = runtime.GetString(
                TokraIntroductionArtifactMissionUtility.ArtifactThingIdKey);

            if (artifactThingId.NullOrEmpty())
            {
                return null;
            }

            if (Find.WorldObjects?.Caravans != null)
            {
                for (int index = 0;
                    index < Find.WorldObjects.Caravans.Count;
                    index++)
                {
                    Caravan caravan = Find.WorldObjects.Caravans[index];

                    if (caravan == null
                        || caravan.Destroyed
                        || caravan.Faction != Faction.OfPlayer)
                    {
                        continue;
                    }

                    Thing match = CaravanInventoryUtility
                        .AllInventoryItems(caravan)
                        .FirstOrDefault(thing =>
                            thing != null
                            && thing.ThingID == artifactThingId);

                    if (match != null)
                    {
                        return match;
                    }
                }
            }

            if (Find.Maps != null)
            {
                foreach (Map map in Find.Maps)
                {
                    if (map == null
                        || !map.IsPlayerHome
                        || map.listerThings?.AllThings == null)
                    {
                        continue;
                    }

                    Thing match = map.listerThings.AllThings.FirstOrDefault(
                        thing => thing != null
                            && thing.ThingID == artifactThingId);

                    if (match != null)
                    {
                        return match;
                    }

                    if (map.mapPawns?.AllPawnsSpawned == null)
                    {
                        continue;
                    }

                    foreach (Pawn pawn in map.mapPawns.AllPawnsSpawned)
                    {
                        if (pawn?.Faction != Faction.OfPlayer
                            || pawn.inventory?.innerContainer == null)
                        {
                            continue;
                        }

                        match = pawn.inventory.innerContainer.FirstOrDefault(
                            thing => thing != null
                                && thing.ThingID == artifactThingId);

                        if (match != null)
                        {
                            return match;
                        }
                    }
                }
            }

            return null;
        }

        private bool IsTrackedSite(
            WorldObject_TokraIntroductionArtifactSite site)
        {
            if (site == null
                || state != TokraIntroductionArcState.Active
                || runtime?.counters == null)
            {
                return false;
            }

            int siteId;
            return runtime.counters.TryGetValue(
                    TokraIntroductionArtifactMissionUtility
                        .WorldObjectIdCounterKey,
                    out siteId)
                && site.ID == siteId;
        }

        private static string ResolveFailureMessageKey(string failureTextId)
        {
            switch (failureTextId)
            {
                case "artifactLost":
                    return "GR_TokraIntroduction_FailureArtifactLost";
                case "timeout":
                    return "GR_TokraIntroduction_FailureTimeout";
                case "siteLost":
                    return "GR_TokraIntroduction_FailureSiteLost";
                default:
                    return "GR_TokraIntroduction_AttemptFailed";
            }
        }

        private static int GetConfiguredDelayTicks(string contextKey)
        {
            GateRimMissionRecurrenceDef recurrence = GetDefinition()?.recurrence;
            int minimumDelay;
            int maximumDelay;

            if (recurrence != null
                && recurrence.TryGetDelayRange(
                    contextKey,
                    out minimumDelay,
                    out maximumDelay))
            {
                return Rand.RangeInclusive(
                    Math.Max(1, minimumDelay),
                    Math.Max(Math.Max(1, minimumDelay), maximumDelay));
            }

            minimumDelay = Math.Max(1, recurrence?.minimumDelayTicks ?? 1);
            maximumDelay = Math.Max(
                minimumDelay,
                recurrence?.maximumDelayTicks ?? minimumDelay);
            return Rand.RangeInclusive(minimumDelay, maximumDelay);
        }

        private static GateRimMissionDef GetDefinition()
        {
            return GR_DefOf.SG1_TokraIntroductionArtifactMission;
        }

        private static GameComponent_TokraIntroductionArc GetCurrentTracker()
        {
            return Current.Game
                ?.GetComponent<GameComponent_TokraIntroductionArc>();
        }

        private static int GetCurrentTick()
        {
            return Find.TickManager?.TicksGame ?? 0;
        }

        private static string FormatRemainingTicks(int targetTick, int currentTick)
        {
            if (targetTick <= 0)
            {
                return "none";
            }

            int remainingTicks = Math.Max(0, targetTick - currentTick);
            return targetTick
                + " ("
                + (remainingTicks / (float)TicksPerDay).ToString("0.0")
                + " days)";
        }

        private static void AppendDelayRange(
            StringBuilder builder,
            GateRimMissionDef definition,
            string contextKey)
        {
            int minimumDelay;
            int maximumDelay;

            if (definition?.recurrence != null
                && definition.recurrence.TryGetDelayRange(
                    contextKey,
                    out minimumDelay,
                    out maximumDelay))
            {
                builder.AppendLine(
                    contextKey
                    + ": "
                    + (minimumDelay / (float)TicksPerDay).ToString("0.0")
                    + "-"
                    + (maximumDelay / (float)TicksPerDay).ToString("0.0")
                    + " days");
            }
        }
    }
}
