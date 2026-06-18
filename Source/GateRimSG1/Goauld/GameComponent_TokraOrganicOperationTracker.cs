using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Persistent scheduler for Tok'ra-initiated operational opportunities.
    ///
    /// The scheduler deliberately remains separate from manual communicator
    /// requests. It can therefore offer low-sensitivity work before the
    /// Trusted tier while existing trusted-channel actions keep their current
    /// requirements.
    /// </summary>
    public class GameComponent_TokraOrganicOperationTracker : GameComponent
    {
        private const int StateCheckIntervalTicks = 2500;
        private const int InitialMinimumDelayTicks = 180000;
        private const int InitialMaximumDelayTicks = 360000;
        private const int OfferDurationTicks = 120000;
        private const int ObservationMinimumDurationTicks = 15000;
        private const int ObservationDeadlineTicks = 120000;
        private const int DeadDropDeadlineTicks = 90000;
        private const int ObservationIntellectualXp = 250;
        private const int DeadDropIntellectualXp = 200;
        private const float RepeatedArchetypeWeightFactor = 0.25f;
        private const string DeadDropThingDefName =
            "SG1_TokraOrganicDeadDrop";

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
        private Thing activeDeadDrop;
        private TokraOrganicOperationArchetype lastOfferedArchetype;
        private TokraOrganicOperationArchetype lastCompletedArchetype;
        private int completedOperationCount;
        private int failedOperationCount;
        private int expiredOfferCount;

        public GameComponent_TokraOrganicOperationTracker(Game game)
        {
        }

        public override void ExposeData()
        {
            base.ExposeData();

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

            if (tracker.activeState == TokraOrganicOperationState.Offered)
            {
                if (tracker.activeArchetype
                    == TokraOrganicOperationArchetype.DeadDropRecovery)
                {
                    return "GR_TokraOrganicOperation_FloatMenuAcceptDeadDrop"
                        .Translate()
                        .ToString();
                }

                return "GR_TokraOrganicOperation_FloatMenuAcceptObservation"
                    .Translate()
                    .ToString();
            }

            return "GR_TokraOrganicOperation_FloatMenuTransmitObservation"
                .Translate()
                .ToString();
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

            if (currentTick < tracker.reportReadyTick)
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

            if (tracker == null
                || !tracker.HasCommunicatorInteractionForMap(map))
            {
                return false;
            }

            return tracker.TryHandleInteraction(map, operatorPawn);
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

            if (tracker == null
                || !tracker.IsExactActiveDeadDrop(deadDrop)
                || tracker.IsOperationDeadlineExpired())
            {
                return false;
            }

            return tracker.CompleteDeadDropOperation(deadDrop, operatorPawn);
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

        public static bool DebugMakeObservationReady(Map map)
        {
            GameComponent_TokraOrganicOperationTracker tracker
                = GetCurrentTracker();

            if (tracker == null
                || !tracker.IsActiveForMap(map)
                || tracker.activeArchetype
                    != TokraOrganicOperationArchetype.GoauldObservation
                || tracker.activeState != TokraOrganicOperationState.Accepted)
            {
                return false;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            tracker.reportReadyTick = currentTick;
            tracker.operationDeadlineTick = Math.Max(
                tracker.operationDeadlineTick,
                currentTick + StateCheckIntervalTicks);
            tracker.readyNotificationSent = true;
            return true;
        }

        public static bool DebugExpireActiveOperation(Map map)
        {
            GameComponent_TokraOrganicOperationTracker tracker
                = GetCurrentTracker();

            if (tracker == null
                || !tracker.IsActiveForMap(map)
                || tracker.activeState != TokraOrganicOperationState.Accepted)
            {
                return false;
            }

            tracker.operationDeadlineTick = Find.TickManager?.TicksGame ?? 0;
            tracker.nextStateCheckTick = 0;
            return true;
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
            Map activeMap = GetActiveMap();

            if (activeMap == null)
            {
                if (activeState == TokraOrganicOperationState.Accepted
                    && activeArchetype
                        == TokraOrganicOperationArchetype.DeadDropRecovery)
                {
                    FailDeadDropOperation(
                        currentTick,
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
                        GetOfferExpiredMessageKey().Translate(),
                        MessageTypeDefOf.NeutralEvent,
                        historical: true);
                    ClearActiveOpportunity();
                    ScheduleNextOpportunity(currentTick);
                }

                return;
            }

            if (activeState != TokraOrganicOperationState.Accepted)
            {
                return;
            }

            if (activeArchetype
                == TokraOrganicOperationArchetype.DeadDropRecovery)
            {
                TickAcceptedDeadDrop(currentTick);
                return;
            }

            TickAcceptedObservation(currentTick);
        }

        private void TickAcceptedObservation(int currentTick)
        {
            if (!readyNotificationSent && currentTick >= reportReadyTick)
            {
                readyNotificationSent = true;
                Messages.Message(
                    "GR_TokraOrganicOperation_ReportReady".Translate(),
                    MessageTypeDefOf.PositiveEvent,
                    historical: true);
            }

            if (operationDeadlineTick > 0
                && currentTick >= operationDeadlineTick)
            {
                failedOperationCount++;
                GameComponent_TokraTrustTracker.NotifyOrganicObservationOutcome(
                    TokraOrganicOperationOutcome.Failed);

                Find.LetterStack?.ReceiveLetter(
                    "GR_TokraOrganicOperation_FailedLetterLabel".Translate(),
                    "GR_TokraOrganicOperation_FailedLetterText".Translate(),
                    LetterDefOf.NegativeEvent);

                ClearActiveOpportunity();
                ScheduleNextOpportunity(currentTick);
            }
        }

        private void TickAcceptedDeadDrop(int currentTick)
        {
            if (activeDeadDrop == null
                || activeDeadDrop.Destroyed
                || !activeDeadDrop.Spawned
                || activeDeadDrop.Map == null
                || activeDeadDrop.Map.uniqueID != activeMapId)
            {
                FailDeadDropOperation(
                    currentTick,
                    "GR_TokraOrganicOperation_DeadDropLostLetterText");
                return;
            }

            if (operationDeadlineTick > 0
                && currentTick >= operationDeadlineTick)
            {
                FailDeadDropOperation(
                    currentTick,
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
            if (map == null || archetype == TokraOrganicOperationArchetype.None)
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
            offerExpiryTick = currentTick + OfferDurationTicks;
            acceptedTick = 0;
            reportReadyTick = 0;
            operationDeadlineTick = 0;
            readyNotificationSent = false;
            activeDeadDrop = null;
            lastOfferedArchetype = archetype;
            nextOpportunityTick = 0;

            Find.LetterStack?.ReceiveLetter(
                GetOfferLetterLabelKey().Translate(),
                GetOfferLetterTextKey().Translate(
                    GetRoundedUpHours(OfferDurationTicks).ToString()),
                LetterDefOf.NeutralEvent,
                communicator);

            GR_Log.Message(
                "Created Tok'ra organic operation opportunity "
                + $"{archetype} on map {map.uniqueID}; "
                + $"offer expires at tick {offerExpiryTick}; "
                + $"forced={forced}.");

            return true;
        }

        private bool TryHandleInteraction(Map map, Pawn operatorPawn)
        {
            if (activeState == TokraOrganicOperationState.Offered)
            {
                if (activeArchetype
                    == TokraOrganicOperationArchetype.DeadDropRecovery)
                {
                    return TryAcceptDeadDropOperation(map, operatorPawn);
                }

                return TryAcceptObservationOperation(map, operatorPawn);
            }

            if (activeArchetype
                != TokraOrganicOperationArchetype.GoauldObservation)
            {
                return false;
            }

            return TryCompleteObservationOperation(map, operatorPawn);
        }

        private bool TryAcceptObservationOperation(Map map, Pawn operatorPawn)
        {
            int currentTick = Find.TickManager?.TicksGame ?? 0;
            activeState = TokraOrganicOperationState.Accepted;
            acceptedTick = currentTick;
            reportReadyTick = currentTick + ObservationMinimumDurationTicks;
            operationDeadlineTick = currentTick + ObservationDeadlineTicks;
            readyNotificationSent = false;

            Messages.Message(
                "GR_TokraOrganicOperation_Accepted".Translate(
                    operatorPawn?.LabelShortCap ?? "?",
                    GetRoundedUpHours(
                        ObservationMinimumDurationTicks).ToString()),
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            GR_Log.Message(
                "Accepted Tok'ra Goa'uld observation opportunity on map "
                + $"{map.uniqueID}; report ready at tick "
                + $"{reportReadyTick}; deadline {operationDeadlineTick}; "
                + $"operator {operatorPawn?.LabelShortCap ?? "unknown"}.");

            return true;
        }

        private bool TryCompleteObservationOperation(
            Map map,
            Pawn operatorPawn)
        {
            int currentTick = Find.TickManager?.TicksGame ?? 0;

            if (activeState != TokraOrganicOperationState.Accepted
                || currentTick < reportReadyTick
                || currentTick >= operationDeadlineTick)
            {
                return false;
            }

            GrantIntellectualExperience(
                operatorPawn,
                ObservationIntellectualXp);
            completedOperationCount++;
            lastCompletedArchetype = activeArchetype;

            GameComponent_TokraTrustTracker.NotifyOrganicObservationOutcome(
                TokraOrganicOperationOutcome.Succeeded);

            Find.LetterStack?.ReceiveLetter(
                "GR_TokraOrganicOperation_SuccessLetterLabel".Translate(),
                "GR_TokraOrganicOperation_SuccessLetterText".Translate(
                    operatorPawn?.LabelShortCap ?? "?",
                    ObservationIntellectualXp.ToString()),
                LetterDefOf.PositiveEvent);

            GR_Log.Message(
                "Completed Tok'ra Goa'uld observation opportunity on map "
                + $"{map.uniqueID}; operator "
                + $"{operatorPawn?.LabelShortCap ?? "unknown"}; "
                + $"Intellectual XP {ObservationIntellectualXp}.");

            ClearActiveOpportunity();
            ScheduleNextOpportunity(currentTick);
            return true;
        }

        private bool TryAcceptDeadDropOperation(Map map, Pawn operatorPawn)
        {
            Thing deadDrop;

            if (!TrySpawnDeadDrop(map, out deadDrop))
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
            reportReadyTick = 0;
            operationDeadlineTick = currentTick + DeadDropDeadlineTicks;
            readyNotificationSent = false;
            activeDeadDrop = deadDrop;

            Messages.Message(
                "GR_TokraOrganicOperation_DeadDropAccepted".Translate(
                    operatorPawn?.LabelShortCap ?? "?",
                    GetRoundedUpHours(DeadDropDeadlineTicks).ToString()),
                deadDrop,
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            Find.LetterStack?.ReceiveLetter(
                "GR_TokraOrganicOperation_DeadDropLocatedLetterLabel"
                    .Translate(),
                "GR_TokraOrganicOperation_DeadDropLocatedLetterText"
                    .Translate(
                        GetRoundedUpHours(DeadDropDeadlineTicks).ToString()),
                LetterDefOf.NeutralEvent,
                deadDrop);

            GR_Log.Message(
                "Accepted Tok'ra organic intelligence recovery on map "
                + $"{map.uniqueID}; cache at {deadDrop.Position}; "
                + $"deadline {operationDeadlineTick}; operator "
                + $"{operatorPawn?.LabelShortCap ?? "unknown"}.");

            return true;
        }

        private bool CompleteDeadDropOperation(
            Thing deadDrop,
            Pawn operatorPawn)
        {
            int currentTick = Find.TickManager?.TicksGame ?? 0;
            Map map = deadDrop?.Map;

            GrantIntellectualExperience(operatorPawn, DeadDropIntellectualXp);
            completedOperationCount++;
            lastCompletedArchetype = activeArchetype;

            GameComponent_TokraTrustTracker.NotifyOrganicDeadDropOutcome(
                TokraOrganicOperationOutcome.Succeeded);

            Find.LetterStack?.ReceiveLetter(
                "GR_TokraOrganicOperation_DeadDropSuccessLetterLabel"
                    .Translate(),
                "GR_TokraOrganicOperation_DeadDropSuccessLetterText"
                    .Translate(
                        operatorPawn?.LabelShortCap ?? "?",
                        DeadDropIntellectualXp.ToString()),
                LetterDefOf.PositiveEvent,
                operatorPawn ?? deadDrop);

            GR_Log.Message(
                "Completed Tok'ra organic intelligence recovery on map "
                + $"{map?.uniqueID.ToString() ?? "unknown"}; operator "
                + $"{operatorPawn?.LabelShortCap ?? "unknown"}; "
                + $"Intellectual XP {DeadDropIntellectualXp}.");

            DestroyActiveDeadDrop();
            ClearActiveOpportunity(destroyDeadDrop: false);
            ScheduleNextOpportunity(currentTick);
            return true;
        }

        private void FailDeadDropOperation(
            int currentTick,
            string failureTextKey)
        {
            failedOperationCount++;
            GameComponent_TokraTrustTracker.NotifyOrganicDeadDropOutcome(
                TokraOrganicOperationOutcome.Failed);

            Find.LetterStack?.ReceiveLetter(
                "GR_TokraOrganicOperation_DeadDropFailedLetterLabel"
                    .Translate(),
                failureTextKey.Translate(),
                LetterDefOf.NegativeEvent);

            GR_Log.Message(
                "Failed Tok'ra organic intelligence recovery; reason key "
                + $"{failureTextKey}; map {activeMapId}.");

            DestroyActiveDeadDrop();
            ClearActiveOpportunity(destroyDeadDrop: false);
            ScheduleNextOpportunity(currentTick);
        }

        private static bool TrySpawnDeadDrop(Map map, out Thing deadDrop)
        {
            deadDrop = null;

            ThingDef thingDef = DefDatabase<ThingDef>.GetNamedSilentFail(
                DeadDropThingDefName);

            if (map == null || thingDef == null)
            {
                return false;
            }

            IntVec3 deliveryCell;

            if (!TokraDeliveryDropUtility.TryFindPreferredDeliveryCell(
                    map,
                    null,
                    out deliveryCell))
            {
                return false;
            }

            DestroyDeadDropsOnMap(map);

            Thing thing = ThingMaker.MakeThing(thingDef);
            thing.SetFaction(Faction.OfPlayer);

            Thing placedThing;

            if (!GenPlace.TryPlaceThing(
                    thing,
                    deliveryCell,
                    map,
                    ThingPlaceMode.Near,
                    out placedThing))
            {
                if (!thing.Destroyed)
                {
                    thing.Destroy(DestroyMode.Vanish);
                }

                return false;
            }

            if (placedThing == null
                || !placedThing.Spawned
                || placedThing.Map != map
                || placedThing.Position.Fogged(map)
                || !map.reachability.CanReachColony(placedThing.Position))
            {
                if (placedThing != null
                    && !placedThing.Destroyed)
                {
                    placedThing.Destroy(DestroyMode.Vanish);
                }

                return false;
            }

            GR_Log.Message(
                "Placed Tok'ra organic intelligence module from preferred "
                + $"delivery cell {deliveryCell} to {placedThing.Position}.");

            deadDrop = placedThing;
            return true;
        }

        private void ScheduleNextOpportunity(int currentTick)
        {
            int minimumDelay;
            int maximumDelay;

            GetDelayRange(
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
                = BuildCompatibleCandidates(tier);

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
                        candidate.Weight *= RepeatedArchetypeWeightFactor;
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

        private static List<OrganicOperationCandidate> BuildCompatibleCandidates(
            TokraTrustTier tier)
        {
            return new List<OrganicOperationCandidate>
            {
                new OrganicOperationCandidate(
                    TokraOrganicOperationArchetype.GoauldObservation,
                    GetObservationWeight(tier)),
                new OrganicOperationCandidate(
                    TokraOrganicOperationArchetype.DeadDropRecovery,
                    GetDeadDropWeight(tier))
            };
        }

        private static float GetObservationWeight(TokraTrustTier tier)
        {
            switch (tier)
            {
                case TokraTrustTier.Wary:
                    return 0.60f;
                case TokraTrustTier.Neutral:
                    return 1.00f;
                case TokraTrustTier.Cooperative:
                    return 0.85f;
                case TokraTrustTier.Trusted:
                    return 0.35f;
                default:
                    return 0f;
            }
        }

        private static float GetDeadDropWeight(TokraTrustTier tier)
        {
            switch (tier)
            {
                case TokraTrustTier.Wary:
                    return 0.35f;
                case TokraTrustTier.Neutral:
                    return 0.85f;
                case TokraTrustTier.Cooperative:
                    return 1.00f;
                case TokraTrustTier.Trusted:
                    return 0.55f;
                default:
                    return 0f;
            }
        }

        private static void GetDelayRange(
            TokraTrustTier tier,
            out int minimumDelay,
            out int maximumDelay)
        {
            switch (tier)
            {
                case TokraTrustTier.Wary:
                    minimumDelay = 360000;
                    maximumDelay = 720000;
                    return;
                case TokraTrustTier.Cooperative:
                    minimumDelay = 180000;
                    maximumDelay = 420000;
                    return;
                case TokraTrustTier.Trusted:
                    minimumDelay = 360000;
                    maximumDelay = 720000;
                    return;
                default:
                    minimumDelay = 240000;
                    maximumDelay = 480000;
                    return;
            }
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

            return activeState == TokraOrganicOperationState.Accepted
                && activeArchetype
                    == TokraOrganicOperationArchetype.GoauldObservation;
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
            return deadDrop != null
                && activeArchetype
                    == TokraOrganicOperationArchetype.DeadDropRecovery
                && activeState == TokraOrganicOperationState.Accepted
                && activeDeadDrop == deadDrop;
        }

        private bool IsOperationDeadlineExpired()
        {
            int currentTick = Find.TickManager?.TicksGame ?? 0;

            return operationDeadlineTick > 0
                && currentTick >= operationDeadlineTick;
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
            string status;
            int currentTick = Find.TickManager?.TicksGame ?? 0;

            if (activeState == TokraOrganicOperationState.Offered)
            {
                string key = activeArchetype
                    == TokraOrganicOperationArchetype.DeadDropRecovery
                    ? "GR_TokraOrganicOperation_StatusDeadDropOffered"
                    : "GR_TokraOrganicOperation_StatusOffered";

                status = key.Translate(
                    GetRoundedUpHours(offerExpiryTick - currentTick).ToString())
                    .ToString();
            }
            else if (activeArchetype
                == TokraOrganicOperationArchetype.DeadDropRecovery)
            {
                status = "GR_TokraOrganicOperation_StatusDeadDropActive"
                    .Translate(
                        GetRoundedUpHours(
                            operationDeadlineTick - currentTick).ToString())
                    .ToString();
            }
            else if (currentTick < reportReadyTick)
            {
                status = "GR_TokraOrganicOperation_StatusObserving".Translate(
                    GetRoundedUpHours(reportReadyTick - currentTick).ToString())
                    .ToString();
            }
            else
            {
                status = "GR_TokraOrganicOperation_StatusReady".Translate(
                    GetRoundedUpHours(
                        operationDeadlineTick - currentTick).ToString())
                    .ToString();
            }

            if (!includePrefix)
            {
                return status;
            }

            return "GR_TokraOrganicOperation_InspectPrefix"
                .Translate(status)
                .ToString();
        }

        private string GetOfferLetterLabelKey()
        {
            return activeArchetype
                == TokraOrganicOperationArchetype.DeadDropRecovery
                ? "GR_TokraOrganicOperation_DeadDropOfferLetterLabel"
                : "GR_TokraOrganicOperation_OfferLetterLabel";
        }

        private string GetOfferLetterTextKey()
        {
            return activeArchetype
                == TokraOrganicOperationArchetype.DeadDropRecovery
                ? "GR_TokraOrganicOperation_DeadDropOfferLetterText"
                : "GR_TokraOrganicOperation_OfferLetterText";
        }

        private string GetOfferExpiredMessageKey()
        {
            return activeArchetype
                == TokraOrganicOperationArchetype.DeadDropRecovery
                ? "GR_TokraOrganicOperation_DeadDropOfferExpired"
                : "GR_TokraOrganicOperation_OfferExpired";
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

        private void DestroyActiveDeadDrop()
        {
            Map map = GetActiveMap();
            Thing deadDrop = activeDeadDrop;
            activeDeadDrop = null;

            if (deadDrop != null && !deadDrop.Destroyed)
            {
                deadDrop.Destroy(DestroyMode.Vanish);
            }

            DestroyDeadDropsOnMap(map);
        }

        private static void DestroyDeadDropsOnMap(Map map)
        {
            if (map?.listerThings?.AllThings == null)
            {
                return;
            }

            List<Thing> staleDeadDrops = map.listerThings.AllThings
                .Where(thing =>
                    thing != null
                    && thing.def?.defName == DeadDropThingDefName)
                .ToList();

            for (int i = 0; i < staleDeadDrops.Count; i++)
            {
                Thing staleDeadDrop = staleDeadDrops[i];

                if (!staleDeadDrop.Destroyed)
                {
                    staleDeadDrop.Destroy(DestroyMode.Vanish);
                }
            }
        }

        private void ClearActiveOpportunity(bool destroyDeadDrop = true)
        {
            if (destroyDeadDrop)
            {
                DestroyActiveDeadDrop();
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
            activeDeadDrop = null;
        }

        private void RepairLoadedState()
        {
            if (activeState == TokraOrganicOperationState.None
                || activeArchetype == TokraOrganicOperationArchetype.None)
            {
                ClearActiveOpportunity();
            }
            else if (activeArchetype
                != TokraOrganicOperationArchetype.DeadDropRecovery)
            {
                activeDeadDrop = null;
            }

            completedOperationCount = Math.Max(0, completedOperationCount);
            failedOperationCount = Math.Max(0, failedOperationCount);
            expiredOfferCount = Math.Max(0, expiredOfferCount);
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
        None,
        GoauldObservation,
        DeadDropRecovery
    }

    public enum TokraOrganicOperationState
    {
        None,
        Offered,
        Accepted
    }
}
