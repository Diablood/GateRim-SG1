using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Persistent scheduler for Tok'ra-initiated operational opportunities.
    /// Manual communicator requests remain separate and keep their existing
    /// trust requirements and cooldowns.
    /// </summary>
    public class GameComponent_TokraOrganicOperationTracker : GameComponent
    {
        private const int StateCheckIntervalTicks = 2500;
        private const int InitialMinimumDelayTicks = 180000;
        private const int InitialMaximumDelayTicks = 360000;

        private int frameworkSaveVersion;
        private int nextStateCheckTick;
        private int nextOpportunityTick;
        private TokraOrganicOperationArchetype activeArchetype;
        private TokraOrganicOperationState activeState;
        private int activeMapId = -1;
        private int offerCreatedTick;
        private int offerExpiryTick;
        private int acceptedTick;
        private int reportReadyTick;
        private int operationDeadlineTick;
        private bool readyNotificationSent;
        private bool resolutionApplied;
        private Thing activeDeadDrop;
        private TokraOrganicOperationArchetype lastOfferedArchetype;
        private TokraOrganicOperationArchetype lastCompletedArchetype;
        private int completedOperationCount;
        private int failedOperationCount;
        private int expiredOfferCount;

        public GameComponent_TokraOrganicOperationTracker(Game game)
        {
            frameworkSaveVersion
                = TokraOrganicOperationFramework.CurrentSaveVersion;
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(
                ref frameworkSaveVersion,
                "tokraOrganicFrameworkSaveVersion",
                0);
            Scribe_Values.Look(
                ref nextStateCheckTick,
                "tokraOrganicNextStateCheckTick",
                0);
            Scribe_Values.Look(
                ref nextOpportunityTick,
                "tokraOrganicNextOpportunityTick",
                0);
            Scribe_Values.Look(
                ref activeArchetype,
                "tokraOrganicActiveArchetype",
                TokraOrganicOperationArchetype.None);
            Scribe_Values.Look(
                ref activeState,
                "tokraOrganicActiveState",
                TokraOrganicOperationState.None);
            Scribe_Values.Look(
                ref activeMapId,
                "tokraOrganicActiveMapId",
                -1);
            Scribe_Values.Look(
                ref offerCreatedTick,
                "tokraOrganicOfferCreatedTick",
                0);
            Scribe_Values.Look(
                ref offerExpiryTick,
                "tokraOrganicOfferExpiryTick",
                0);
            Scribe_Values.Look(
                ref acceptedTick,
                "tokraOrganicAcceptedTick",
                0);
            Scribe_Values.Look(
                ref reportReadyTick,
                "tokraOrganicReportReadyTick",
                0);
            Scribe_Values.Look(
                ref operationDeadlineTick,
                "tokraOrganicOperationDeadlineTick",
                0);
            Scribe_Values.Look(
                ref readyNotificationSent,
                "tokraOrganicReadyNotificationSent",
                false);
            Scribe_Values.Look(
                ref resolutionApplied,
                "tokraOrganicResolutionApplied",
                false);
            Scribe_References.Look(
                ref activeDeadDrop,
                "tokraOrganicActiveDeadDrop");
            Scribe_Values.Look(
                ref lastOfferedArchetype,
                "tokraOrganicLastOfferedArchetype",
                TokraOrganicOperationArchetype.None);
            Scribe_Values.Look(
                ref lastCompletedArchetype,
                "tokraOrganicLastCompletedArchetype",
                TokraOrganicOperationArchetype.None);
            Scribe_Values.Look(
                ref completedOperationCount,
                "tokraOrganicCompletedOperationCount",
                0);
            Scribe_Values.Look(
                ref failedOperationCount,
                "tokraOrganicFailedOperationCount",
                0);
            Scribe_Values.Look(
                ref expiredOfferCount,
                "tokraOrganicExpiredOfferCount",
                0);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                RepairLoadedState();
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

            if (activeState != TokraOrganicOperationState.None)
            {
                TickActiveOpportunity(currentTick);
                return;
            }

            if (nextOpportunityTick <= 0)
            {
                nextOpportunityTick = currentTick + Rand.RangeInclusive(
                    InitialMinimumDelayTicks,
                    InitialMaximumDelayTicks);
                return;
            }

            if (currentTick < nextOpportunityTick)
            {
                return;
            }

            if (!TryCreateNextOpportunity(currentTick))
            {
                nextOpportunityTick = currentTick + StateCheckIntervalTicks;
            }
        }

        public static bool HasActiveOpportunityForMap(Map map)
        {
            GameComponent_TokraOrganicOperationTracker tracker
                = GetCurrentTracker();

            return tracker != null
                && tracker.HasCommunicatorInteractionForMap(map);
        }

        public static string GetCommunicatorActionLabel(Map map)
        {
            GameComponent_TokraOrganicOperationTracker tracker
                = GetCurrentTracker();

            if (tracker == null
                || !tracker.HasCommunicatorInteractionForMap(map))
            {
                return "GR_TokraOrganicOperation_FloatMenuUnavailable"
                    .Translate()
                    .ToString();
            }

            TokraOrganicOperationDefinition definition
                = tracker.GetActiveDefinition();

            if (definition == null)
            {
                return "GR_TokraOrganicOperation_FloatMenuUnavailable"
                    .Translate()
                    .ToString();
            }

            string key = tracker.activeState
                == TokraOrganicOperationState.Offered
                ? definition.AcceptActionKey
                : definition.CompleteActionKey;

            return string.IsNullOrEmpty(key)
                ? "GR_TokraOrganicOperation_FloatMenuUnavailable"
                    .Translate()
                    .ToString()
                : key.Translate().ToString();
        }

        public static string GetCommunicatorDisabledReason(Map map)
        {
            GameComponent_TokraOrganicOperationTracker tracker
                = GetCurrentTracker();

            if (tracker == null)
            {
                return "GR_TokraOrganicOperation_TrackerUnavailable"
                    .Translate()
                    .ToString();
            }

            if (!tracker.HasCommunicatorInteractionForMap(map))
            {
                return "GR_TokraOrganicOperation_NoActiveOpportunity"
                    .Translate()
                    .ToString();
            }

            if (tracker.activeState == TokraOrganicOperationState.Offered)
            {
                return null;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            tracker.UpdateObservationReadyState(currentTick, notifyPlayer: false);

            if (tracker.activeState == TokraOrganicOperationState.Accepted
                && currentTick < tracker.reportReadyTick)
            {
                int remainingHours = GetRoundedUpHours(
                    tracker.reportReadyTick - currentTick);

                return "GR_TokraOrganicOperation_ObservationInProgress"
                    .Translate(remainingHours.ToString())
                    .ToString();
            }

            if (tracker.operationDeadlineTick > 0
                && currentTick >= tracker.operationDeadlineTick)
            {
                return "GR_TokraOrganicOperation_ObservationExpired"
                    .Translate()
                    .ToString();
            }

            return null;
        }

        public static string GetInspectStatusForMap(Map map)
        {
            GameComponent_TokraOrganicOperationTracker tracker
                = GetCurrentTracker();

            if (tracker == null || !tracker.IsActiveForMap(map))
            {
                return null;
            }

            return tracker.GetActiveStatusLabel(includePrefix: true);
        }

        public static string GetStatusReportLineForMap(Map map)
        {
            GameComponent_TokraOrganicOperationTracker tracker
                = GetCurrentTracker();

            if (tracker == null || !tracker.IsActiveForMap(map))
            {
                return "GR_TokraOrganicOperation_StatusReportNone"
                    .Translate()
                    .ToString();
            }

            return "GR_TokraOrganicOperation_StatusReportActive"
                .Translate(tracker.GetActiveStatusLabel(includePrefix: false))
                .ToString();
        }

        public static bool TryHandleCommunicatorInteraction(
            Map map,
            Pawn operatorPawn)
        {
            GameComponent_TokraOrganicOperationTracker tracker
                = GetCurrentTracker();

            return tracker != null
                && tracker.HasCommunicatorInteractionForMap(map)
                && tracker.TryHandleInteraction(map, operatorPawn);
        }

        public static bool IsActiveDeadDrop(Thing deadDrop)
        {
            GameComponent_TokraOrganicOperationTracker tracker
                = GetCurrentTracker();

            return tracker != null
                && tracker.IsExactActiveDeadDrop(deadDrop)
                && !tracker.IsOperationDeadlineExpired();
        }

        public static string GetDeadDropDisabledReason(Thing deadDrop)
        {
            GameComponent_TokraOrganicOperationTracker tracker
                = GetCurrentTracker();

            if (tracker == null)
            {
                return "GR_TokraOrganicOperation_TrackerUnavailable"
                    .Translate()
                    .ToString();
            }

            if (!tracker.IsExactActiveDeadDrop(deadDrop))
            {
                return "GR_TokraOrganicOperation_DeadDropNoLongerActive"
                    .Translate()
                    .ToString();
            }

            if (tracker.IsOperationDeadlineExpired())
            {
                return "GR_TokraOrganicOperation_DeadDropWindowExpired"
                    .Translate()
                    .ToString();
            }

            return null;
        }

        public static bool TrySecureDeadDrop(
            Thing deadDrop,
            Pawn operatorPawn)
        {
            GameComponent_TokraOrganicOperationTracker tracker
                = GetCurrentTracker();

            return tracker != null
                && tracker.IsExactActiveDeadDrop(deadDrop)
                && !tracker.IsOperationDeadlineExpired()
                && tracker.TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Succeeded,
                    operatorPawn,
                    null);
        }

        public static bool DebugForceOpportunity(Map map)
        {
            return DebugForceSpecificOpportunity(
                map,
                TokraOrganicOperationArchetype.GoauldObservation);
        }

        public static bool DebugForceDeadDropOpportunity(Map map)
        {
            return DebugForceSpecificOpportunity(
                map,
                TokraOrganicOperationArchetype.DeadDropRecovery);
        }

        public static bool DebugMakeActiveReady(Map map)
        {
            GameComponent_TokraOrganicOperationTracker tracker
                = GetCurrentTracker();

            if (tracker == null
                || !tracker.IsActiveForMap(map)
                || tracker.activeState != TokraOrganicOperationState.Accepted)
            {
                return false;
            }

            TokraOrganicOperationDefinition definition
                = tracker.GetActiveDefinition();

            if (definition == null)
            {
                return false;
            }

            if (definition.HasPhysicalObjective)
            {
                return tracker.activeDeadDrop != null
                    && tracker.activeDeadDrop.Spawned
                    && !tracker.activeDeadDrop.Destroyed;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            tracker.reportReadyTick = currentTick;
            tracker.operationDeadlineTick = Math.Max(
                tracker.operationDeadlineTick,
                currentTick + StateCheckIntervalTicks);
            tracker.activeState = TokraOrganicOperationState.Ready;
            tracker.readyNotificationSent = true;

            GR_Log.Message(
                "Advanced Tok'ra organic operation "
                + $"{definition.DebugLabel} to ready state on map "
                + $"{map?.uniqueID.ToString() ?? "unknown"}.");

            return true;
        }

        public static bool DebugMakeObservationReady(Map map)
        {
            GameComponent_TokraOrganicOperationTracker tracker
                = GetCurrentTracker();

            return tracker != null
                && tracker.activeArchetype
                    == TokraOrganicOperationArchetype.GoauldObservation
                && DebugMakeActiveReady(map);
        }

        public static bool DebugFailActiveOperation(Map map)
        {
            GameComponent_TokraOrganicOperationTracker tracker
                = GetCurrentTracker();

            if (tracker == null
                || !tracker.IsActiveForMap(map)
                || (tracker.activeState
                        != TokraOrganicOperationState.Accepted
                    && tracker.activeState
                        != TokraOrganicOperationState.Ready))
            {
                return false;
            }

            string failureTextKey = tracker.activeArchetype
                == TokraOrganicOperationArchetype.DeadDropRecovery
                ? "GR_TokraOrganicOperation_DeadDropTimedOutLetterText"
                : null;

            return tracker.TryResolveActiveOperation(
                TokraOrganicOperationOutcome.Failed,
                null,
                failureTextKey);
        }

        public static bool DebugExpireActiveOperation(Map map)
        {
            return DebugFailActiveOperation(map);
        }

        public static bool DebugReset()
        {
            GameComponent_TokraOrganicOperationTracker tracker
                = GetCurrentTracker();

            if (tracker == null)
            {
                return false;
            }

            tracker.ClearActiveOpportunity();
            int currentTick = Find.TickManager?.TicksGame ?? 0;
            tracker.nextOpportunityTick = currentTick + Rand.RangeInclusive(
                InitialMinimumDelayTicks,
                InitialMaximumDelayTicks);
            return true;
        }

        private static bool DebugForceSpecificOpportunity(
            Map map,
            TokraOrganicOperationArchetype archetype)
        {
            GameComponent_TokraOrganicOperationTracker tracker
                = GetCurrentTracker();

            if (tracker == null
                || map == null
                || FindPoweredCommunicator(map) == null)
            {
                return false;
            }

            tracker.ClearActiveOpportunity();
            tracker.nextOpportunityTick = 0;

            return tracker.TryCreateOpportunity(
                map,
                archetype,
                Find.TickManager?.TicksGame ?? 0,
                forced: true);
        }

        private void TickActiveOpportunity(int currentTick)
        {
            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();

            if (definition == null)
            {
                ClearActiveOpportunity();
                ScheduleNextOpportunity(currentTick);
                return;
            }

            Map activeMap = GetActiveMap();

            if (activeMap == null)
            {
                if (activeState == TokraOrganicOperationState.Accepted
                    && definition.HasPhysicalObjective)
                {
                    TryResolveActiveOperation(
                        TokraOrganicOperationOutcome.Failed,
                        null,
                        "GR_TokraOrganicOperation_DeadDropLostLetterText");
                }
                else
                {
                    ClearActiveOpportunity();
                    ScheduleNextOpportunity(currentTick);
                }

                return;
            }

            if (activeState == TokraOrganicOperationState.Offered)
            {
                if (offerExpiryTick > 0 && currentTick >= offerExpiryTick)
                {
                    expiredOfferCount++;
                    Messages.Message(
                        definition.OfferExpiredMessageKey.Translate(),
                        MessageTypeDefOf.NeutralEvent,
                        historical: true);
                    ClearActiveOpportunity();
                    ScheduleNextOpportunity(currentTick);
                }

                return;
            }

            if (activeState != TokraOrganicOperationState.Accepted
                && activeState != TokraOrganicOperationState.Ready)
            {
                return;
            }

            if (definition.HasPhysicalObjective)
            {
                TickAcceptedPhysicalObjective(currentTick);
                return;
            }

            TickAcceptedObservation(currentTick);
        }

        private void TickAcceptedObservation(int currentTick)
        {
            UpdateObservationReadyState(currentTick, notifyPlayer: true);

            if (operationDeadlineTick > 0
                && currentTick >= operationDeadlineTick)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    null,
                    null);
            }
        }

        private void UpdateObservationReadyState(
            int currentTick,
            bool notifyPlayer)
        {
            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();

            if (definition == null
                || definition.HasPhysicalObjective
                || activeState == TokraOrganicOperationState.Offered
                || activeState == TokraOrganicOperationState.None
                || currentTick < reportReadyTick)
            {
                return;
            }

            activeState = TokraOrganicOperationState.Ready;

            if (readyNotificationSent)
            {
                return;
            }

            readyNotificationSent = true;

            if (notifyPlayer)
            {
                Messages.Message(
                    "GR_TokraOrganicOperation_ReportReady".Translate(),
                    MessageTypeDefOf.PositiveEvent,
                    historical: true);
            }
        }

        private void TickAcceptedPhysicalObjective(int currentTick)
        {
            if (activeDeadDrop == null
                || activeDeadDrop.Destroyed
                || !activeDeadDrop.Spawned
                || activeDeadDrop.Map == null
                || activeDeadDrop.Map.uniqueID != activeMapId)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    null,
                    "GR_TokraOrganicOperation_DeadDropLostLetterText");
                return;
            }

            if (operationDeadlineTick > 0
                && currentTick >= operationDeadlineTick)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    null,
                    "GR_TokraOrganicOperation_DeadDropTimedOutLetterText");
            }
        }

        private bool TryCreateNextOpportunity(int currentTick)
        {
            Map map = FindEligibleMap();

            if (map == null)
            {
                return false;
            }

            TokraOrganicOperationArchetype archetype = SelectArchetype(
                GameComponent_TokraTrustTracker.GetCurrentTier());

            if (archetype == TokraOrganicOperationArchetype.None)
            {
                ScheduleNextOpportunity(currentTick);
                return true;
            }

            return TryCreateOpportunity(
                map,
                archetype,
                currentTick,
                forced: false);
        }

        private bool TryCreateOpportunity(
            Map map,
            TokraOrganicOperationArchetype archetype,
            int currentTick,
            bool forced)
        {
            TokraOrganicOperationDefinition definition
                = TokraOrganicOperationFramework.GetDefinition(archetype);

            if (map == null || definition == null)
            {
                return false;
            }

            ThingWithComps communicator = FindPoweredCommunicator(map);

            if (communicator == null)
            {
                return false;
            }

            Faction tokraFaction
                = TokraFactionUtility.GetOrCreatePersistentFaction(
                    "organic operation opportunity");

            if (tokraFaction == null)
            {
                return false;
            }

            activeArchetype = archetype;
            activeState = TokraOrganicOperationState.Offered;
            activeMapId = map.uniqueID;
            offerCreatedTick = currentTick;
            offerExpiryTick = currentTick + definition.OfferDurationTicks;
            acceptedTick = 0;
            reportReadyTick = 0;
            operationDeadlineTick = 0;
            readyNotificationSent = false;
            resolutionApplied = false;
            activeDeadDrop = null;
            lastOfferedArchetype = archetype;
            nextOpportunityTick = 0;

            Find.LetterStack?.ReceiveLetter(
                definition.OfferLetterLabelKey.Translate(),
                definition.OfferLetterTextKey.Translate(
                    GetRoundedUpHours(
                        definition.OfferDurationTicks).ToString()),
                LetterDefOf.NeutralEvent,
                communicator);

            GR_Log.Message(
                "Created Tok'ra organic operation opportunity "
                + $"{definition.DebugLabel} on map {map.uniqueID}; "
                + $"offer expires at tick {offerExpiryTick}; "
                + $"forced={forced}.");

            return true;
        }

        private bool TryHandleInteraction(Map map, Pawn operatorPawn)
        {
            if (activeState == TokraOrganicOperationState.Offered)
            {
                TokraOrganicOperationDefinition definition
                    = GetActiveDefinition();

                return definition != null && definition.HasPhysicalObjective
                    ? TryAcceptPhysicalObjectiveOperation(map, operatorPawn)
                    : TryAcceptObservationOperation(map, operatorPawn);
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            UpdateObservationReadyState(currentTick, notifyPlayer: false);

            return activeArchetype
                    == TokraOrganicOperationArchetype.GoauldObservation
                && activeState == TokraOrganicOperationState.Ready
                && TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Succeeded,
                    operatorPawn,
                    null);
        }

        private bool TryAcceptObservationOperation(Map map, Pawn operatorPawn)
        {
            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();

            if (definition == null)
            {
                return false;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            activeState = TokraOrganicOperationState.Accepted;
            acceptedTick = currentTick;
            reportReadyTick = currentTick + definition.ReadyDelayTicks;
            operationDeadlineTick = currentTick + definition.DeadlineTicks;
            readyNotificationSent = false;
            resolutionApplied = false;

            Messages.Message(
                "GR_TokraOrganicOperation_Accepted".Translate(
                    operatorPawn?.LabelShortCap ?? "?",
                    GetRoundedUpHours(definition.ReadyDelayTicks).ToString()),
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            GR_Log.Message(
                "Accepted Tok'ra organic operation "
                + $"{definition.DebugLabel} on map {map.uniqueID}; "
                + $"ready at tick {reportReadyTick}; "
                + $"deadline {operationDeadlineTick}; operator "
                + $"{operatorPawn?.LabelShortCap ?? "unknown"}.");

            return true;
        }

        private bool TryAcceptPhysicalObjectiveOperation(
            Map map,
            Pawn operatorPawn)
        {
            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();
            Thing objective;

            if (definition == null
                || !TokraOrganicOperationFramework.TryPlaceObjective(
                    map,
                    definition,
                    out objective))
            {
                Messages.Message(
                    "GR_TokraOrganicOperation_DeadDropSpawnFailed".Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            activeState = TokraOrganicOperationState.Accepted;
            acceptedTick = currentTick;
            reportReadyTick = currentTick;
            operationDeadlineTick = currentTick + definition.DeadlineTicks;
            readyNotificationSent = true;
            resolutionApplied = false;
            activeDeadDrop = objective;

            Messages.Message(
                "GR_TokraOrganicOperation_DeadDropAccepted".Translate(
                    operatorPawn?.LabelShortCap ?? "?",
                    GetRoundedUpHours(definition.DeadlineTicks).ToString()),
                objective,
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            Find.LetterStack?.ReceiveLetter(
                "GR_TokraOrganicOperation_DeadDropLocatedLetterLabel"
                    .Translate(),
                "GR_TokraOrganicOperation_DeadDropLocatedLetterText"
                    .Translate(
                        GetRoundedUpHours(
                            definition.DeadlineTicks).ToString()),
                LetterDefOf.NeutralEvent,
                objective);

            GR_Log.Message(
                "Accepted Tok'ra organic operation "
                + $"{definition.DebugLabel} on map {map.uniqueID}; "
                + $"objective at {objective.Position}; "
                + $"deadline {operationDeadlineTick}; operator "
                + $"{operatorPawn?.LabelShortCap ?? "unknown"}.");

            return true;
        }

        private bool TryResolveActiveOperation(
            TokraOrganicOperationOutcome outcome,
            Pawn operatorPawn,
            string failureTextKey)
        {
            if ((activeState != TokraOrganicOperationState.Accepted
                    && activeState != TokraOrganicOperationState.Ready)
                || resolutionApplied)
            {
                return false;
            }

            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();

            if (definition == null)
            {
                return false;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;

            if (outcome == TokraOrganicOperationOutcome.Succeeded
                && (IsOperationDeadlineExpired()
                    || (!definition.HasPhysicalObjective
                        && currentTick < reportReadyTick)))
            {
                return false;
            }

            resolutionApplied = true;
            Thing letterTarget = operatorPawn ?? activeDeadDrop;

            if (outcome == TokraOrganicOperationOutcome.Succeeded)
            {
                GrantIntellectualExperience(
                    operatorPawn,
                    definition.IntellectualXp);
                completedOperationCount++;
                lastCompletedArchetype = activeArchetype;
            }
            else
            {
                failedOperationCount++;
            }

            GameComponent_TokraTrustTracker.NotifyOrganicOperationOutcome(
                activeArchetype,
                outcome);

            SendResolutionLetter(
                definition,
                outcome,
                operatorPawn,
                failureTextKey,
                letterTarget);

            GR_Log.Message(
                "Resolved Tok'ra organic operation "
                + $"{definition.DebugLabel} with outcome {outcome}; "
                + $"map {activeMapId}; operator "
                + $"{operatorPawn?.LabelShortCap ?? "none"}; "
                + $"XP {(outcome == TokraOrganicOperationOutcome.Succeeded ? definition.IntellectualXp : 0)}.");

            DestroyActiveObjective();
            ClearActiveOpportunity(destroyObjective: false);
            ScheduleNextOpportunity(currentTick);
            return true;
        }

        private static void SendResolutionLetter(
            TokraOrganicOperationDefinition definition,
            TokraOrganicOperationOutcome outcome,
            Pawn operatorPawn,
            string failureTextKey,
            Thing letterTarget)
        {
            if (definition.Archetype
                == TokraOrganicOperationArchetype.GoauldObservation)
            {
                if (outcome == TokraOrganicOperationOutcome.Succeeded)
                {
                    Find.LetterStack?.ReceiveLetter(
                        "GR_TokraOrganicOperation_SuccessLetterLabel"
                            .Translate(),
                        "GR_TokraOrganicOperation_SuccessLetterText"
                            .Translate(
                                operatorPawn?.LabelShortCap ?? "?",
                                definition.IntellectualXp.ToString()),
                        LetterDefOf.PositiveEvent,
                        letterTarget);
                }
                else
                {
                    Find.LetterStack?.ReceiveLetter(
                        "GR_TokraOrganicOperation_FailedLetterLabel"
                            .Translate(),
                        "GR_TokraOrganicOperation_FailedLetterText"
                            .Translate(),
                        LetterDefOf.NegativeEvent);
                }

                return;
            }

            if (outcome == TokraOrganicOperationOutcome.Succeeded)
            {
                Find.LetterStack?.ReceiveLetter(
                    "GR_TokraOrganicOperation_DeadDropSuccessLetterLabel"
                        .Translate(),
                    "GR_TokraOrganicOperation_DeadDropSuccessLetterText"
                        .Translate(
                            operatorPawn?.LabelShortCap ?? "?",
                            definition.IntellectualXp.ToString()),
                    LetterDefOf.PositiveEvent,
                    letterTarget);
                return;
            }

            string textKey = string.IsNullOrEmpty(failureTextKey)
                ? "GR_TokraOrganicOperation_DeadDropTimedOutLetterText"
                : failureTextKey;

            Find.LetterStack?.ReceiveLetter(
                "GR_TokraOrganicOperation_DeadDropFailedLetterLabel"
                    .Translate(),
                textKey.Translate(),
                LetterDefOf.NegativeEvent);
        }

        private void ScheduleNextOpportunity(int currentTick)
        {
            int minimumDelay;
            int maximumDelay;

            TokraOrganicOperationFramework.GetDelayRange(
                GameComponent_TokraTrustTracker.GetCurrentTier(),
                out minimumDelay,
                out maximumDelay);

            nextOpportunityTick = currentTick + Rand.RangeInclusive(
                minimumDelay,
                maximumDelay);
        }

        private TokraOrganicOperationArchetype SelectArchetype(
            TokraTrustTier tier)
        {
            List<OrganicOperationCandidate> candidates
                = TokraOrganicOperationFramework.AllDefinitions
                    .Select(definition => new OrganicOperationCandidate(
                        definition.Archetype,
                        definition.GetWeight(tier)))
                    .Where(candidate => candidate.Weight > 0f)
                    .ToList();

            if (candidates.Count == 0)
            {
                return TokraOrganicOperationArchetype.None;
            }

            if (candidates.Count > 1
                && lastOfferedArchetype != TokraOrganicOperationArchetype.None)
            {
                for (int i = 0; i < candidates.Count; i++)
                {
                    OrganicOperationCandidate candidate = candidates[i];

                    if (candidate.Archetype == lastOfferedArchetype)
                    {
                        candidate.Weight *= TokraOrganicOperationFramework
                            .RepeatedArchetypeWeightFactor;
                        candidates[i] = candidate;
                    }
                }
            }

            float totalWeight = candidates.Sum(candidate => candidate.Weight);

            if (totalWeight <= 0f)
            {
                return TokraOrganicOperationArchetype.None;
            }

            float selection = Rand.Value * totalWeight;

            for (int i = 0; i < candidates.Count; i++)
            {
                selection -= candidates[i].Weight;

                if (selection <= 0f)
                {
                    return candidates[i].Archetype;
                }
            }

            return candidates[candidates.Count - 1].Archetype;
        }

        private bool HasCommunicatorInteractionForMap(Map map)
        {
            if (!IsActiveForMap(map))
            {
                return false;
            }

            if (activeState == TokraOrganicOperationState.Offered)
            {
                return true;
            }

            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();

            if (definition == null
                || !definition.UsesCommunicatorForCompletion)
            {
                return false;
            }

            UpdateObservationReadyState(
                Find.TickManager?.TicksGame ?? 0,
                notifyPlayer: false);

            return activeState == TokraOrganicOperationState.Accepted
                || activeState == TokraOrganicOperationState.Ready;
        }

        private bool IsActiveForMap(Map map)
        {
            return map != null
                && activeState != TokraOrganicOperationState.None
                && activeArchetype != TokraOrganicOperationArchetype.None
                && activeMapId == map.uniqueID;
        }

        private bool IsExactActiveDeadDrop(Thing deadDrop)
        {
            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();

            return deadDrop != null
                && definition != null
                && definition.HasPhysicalObjective
                && activeState == TokraOrganicOperationState.Accepted
                && activeDeadDrop == deadDrop;
        }

        private bool IsOperationDeadlineExpired()
        {
            int currentTick = Find.TickManager?.TicksGame ?? 0;

            return operationDeadlineTick > 0
                && currentTick >= operationDeadlineTick;
        }

        private TokraOrganicOperationDefinition GetActiveDefinition()
        {
            return TokraOrganicOperationFramework.GetDefinition(
                activeArchetype);
        }

        private Map GetActiveMap()
        {
            if (activeMapId < 0 || Find.Maps == null)
            {
                return null;
            }

            return Find.Maps.FirstOrDefault(map => map.uniqueID == activeMapId);
        }

        private static Map FindEligibleMap()
        {
            if (Find.Maps == null)
            {
                return null;
            }

            for (int i = 0; i < Find.Maps.Count; i++)
            {
                Map map = Find.Maps[i];

                if (map != null
                    && map.IsPlayerHome
                    && FindPoweredCommunicator(map) != null)
                {
                    return map;
                }
            }

            return null;
        }

        private static ThingWithComps FindPoweredCommunicator(Map map)
        {
            if (map?.listerThings?.AllThings == null)
            {
                return null;
            }

            List<Thing> things = map.listerThings.AllThings;

            for (int i = 0; i < things.Count; i++)
            {
                ThingWithComps thing = things[i] as ThingWithComps;

                if (thing == null
                    || thing.Faction != Faction.OfPlayer
                    || thing.GetComp<Comp_TokraSecureCommunicator>() == null)
                {
                    continue;
                }

                CompPowerTrader powerComp = thing.GetComp<CompPowerTrader>();

                if (powerComp != null && powerComp.PowerOn)
                {
                    return thing;
                }
            }

            return null;
        }

        private string GetActiveStatusLabel(bool includePrefix)
        {
            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();

            if (definition == null)
            {
                return string.Empty;
            }

            string status;
            int currentTick = Find.TickManager?.TicksGame ?? 0;

            if (activeState == TokraOrganicOperationState.Offered)
            {
                status = definition.OfferedStatusKey.Translate(
                    GetRoundedUpHours(
                        offerExpiryTick - currentTick).ToString())
                    .ToString();
            }
            else if (definition.HasPhysicalObjective)
            {
                status = definition.ActiveStatusKey.Translate(
                    GetRoundedUpHours(
                        operationDeadlineTick - currentTick).ToString())
                    .ToString();
            }
            else if (activeState == TokraOrganicOperationState.Accepted
                && currentTick < reportReadyTick)
            {
                status = definition.ActiveStatusKey.Translate(
                    GetRoundedUpHours(
                        reportReadyTick - currentTick).ToString())
                    .ToString();
            }
            else
            {
                status = definition.ReadyStatusKey.Translate(
                    GetRoundedUpHours(
                        operationDeadlineTick - currentTick).ToString())
                    .ToString();
            }

            return includePrefix
                ? "GR_TokraOrganicOperation_InspectPrefix"
                    .Translate(status)
                    .ToString()
                : status;
        }

        private static void GrantIntellectualExperience(Pawn pawn, int amount)
        {
            SkillRecord intellectual = pawn?.skills?.GetSkill(
                SkillDefOf.Intellectual);

            if (intellectual != null && !intellectual.TotallyDisabled)
            {
                intellectual.Learn(amount, true);
            }
        }

        private void DestroyActiveObjective()
        {
            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();
            Map map = GetActiveMap();
            Thing objective = activeDeadDrop;
            activeDeadDrop = null;

            if (objective != null && !objective.Destroyed)
            {
                objective.Destroy(DestroyMode.Vanish);
            }

            TokraOrganicOperationFramework.DestroyObjectives(map, definition);
        }

        private void ClearActiveOpportunity(bool destroyObjective = true)
        {
            if (destroyObjective)
            {
                DestroyActiveObjective();
            }

            activeArchetype = TokraOrganicOperationArchetype.None;
            activeState = TokraOrganicOperationState.None;
            activeMapId = -1;
            offerCreatedTick = 0;
            offerExpiryTick = 0;
            acceptedTick = 0;
            reportReadyTick = 0;
            operationDeadlineTick = 0;
            readyNotificationSent = false;
            resolutionApplied = false;
            activeDeadDrop = null;
        }

        private void RepairLoadedState()
        {
            int previousFrameworkSaveVersion = frameworkSaveVersion;

            completedOperationCount = Math.Max(0, completedOperationCount);
            failedOperationCount = Math.Max(0, failedOperationCount);
            expiredOfferCount = Math.Max(0, expiredOfferCount);

            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();
            bool hasValidState = activeState
                    == TokraOrganicOperationState.None
                || activeState == TokraOrganicOperationState.Offered
                || activeState == TokraOrganicOperationState.Accepted
                || activeState == TokraOrganicOperationState.Ready;

            if (!hasValidState
                || activeState == TokraOrganicOperationState.None
                || definition == null)
            {
                ClearActiveOpportunity();
                TokraOrganicOperationFramework.DestroyAllObjectivesExcept(null);
                frameworkSaveVersion
                    = TokraOrganicOperationFramework.CurrentSaveVersion;
                return;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;

            if (resolutionApplied)
            {
                ClearActiveOpportunity();
                ScheduleNextOpportunity(currentTick);
                TokraOrganicOperationFramework.DestroyAllObjectivesExcept(null);
                frameworkSaveVersion
                    = TokraOrganicOperationFramework.CurrentSaveVersion;
                return;
            }

            if (activeState == TokraOrganicOperationState.Offered)
            {
                if (offerExpiryTick <= 0)
                {
                    offerExpiryTick = Math.Max(currentTick, offerCreatedTick)
                        + definition.OfferDurationTicks;
                }

                activeDeadDrop = null;
            }
            else if (activeState == TokraOrganicOperationState.Accepted
                || activeState == TokraOrganicOperationState.Ready)
            {
                if (operationDeadlineTick <= 0)
                {
                    operationDeadlineTick
                        = Math.Max(currentTick, acceptedTick)
                        + definition.DeadlineTicks;
                }

                if (definition.HasPhysicalObjective)
                {
                    activeState = TokraOrganicOperationState.Accepted;

                    if (activeDeadDrop == null
                        || activeDeadDrop.Destroyed
                        || !activeDeadDrop.Spawned)
                    {
                        activeDeadDrop
                            = TokraOrganicOperationFramework
                                .FindExistingObjective(
                                    GetActiveMap(),
                                    definition);
                    }

                    reportReadyTick = acceptedTick;
                    readyNotificationSent = true;
                }
                else
                {
                    activeDeadDrop = null;

                    if (reportReadyTick <= 0)
                    {
                        reportReadyTick = Math.Max(currentTick, acceptedTick)
                            + definition.ReadyDelayTicks;
                    }

                    if (activeState == TokraOrganicOperationState.Ready
                        || currentTick >= reportReadyTick)
                    {
                        activeState = TokraOrganicOperationState.Ready;
                        readyNotificationSent = true;
                    }
                    else
                    {
                        activeState = TokraOrganicOperationState.Accepted;
                    }
                }
            }

            TokraOrganicOperationFramework.DestroyAllObjectivesExcept(
                activeDeadDrop);
            frameworkSaveVersion
                = TokraOrganicOperationFramework.CurrentSaveVersion;

            GR_Log.Message(
                "Repaired Tok'ra organic operation framework state from "
                + $"save version {previousFrameworkSaveVersion}; archetype "
                + $"{activeArchetype}; state {activeState}; map "
                + $"{activeMapId}.");
        }

        private static int GetRoundedUpHours(int ticks)
        {
            return Math.Max(0, (int)Math.Ceiling(Math.Max(0, ticks) / 2500f));
        }

        private static GameComponent_TokraOrganicOperationTracker
            GetCurrentTracker()
        {
            return Current.Game?.GetComponent<
                GameComponent_TokraOrganicOperationTracker>();
        }

        private struct OrganicOperationCandidate
        {
            public OrganicOperationCandidate(
                TokraOrganicOperationArchetype archetype,
                float weight)
            {
                Archetype = archetype;
                Weight = weight;
            }

            public TokraOrganicOperationArchetype Archetype;
            public float Weight;
        }
    }

    public enum TokraOrganicOperationArchetype
    {
        None = 0,
        GoauldObservation = 1,
        DeadDropRecovery = 2
    }

    public enum TokraOrganicOperationState
    {
        None = 0,
        Offered = 1,
        Accepted = 2,
        Ready = 3
    }
}
