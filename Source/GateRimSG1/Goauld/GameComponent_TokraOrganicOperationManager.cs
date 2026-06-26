using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GateRimSG1.Missions;
using RimWorld;
using RimWorld.Planet;
using Verse;
using Verse.AI;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Persistent manager for Tok'ra-initiated operational opportunities.
    /// Manual communicator requests remain separate and keep their existing
    /// trust requirements and cooldowns.
    /// </summary>
    public class GameComponent_TokraOrganicOperationManager : GameComponent
    {
        internal const int StateCheckIntervalTicks = 2500;
        internal const int ActiveWorkStateCheckIntervalTicks = 250;
        internal const int MedicalSupplyStateCheckIntervalTicks = 250;
        internal const int DiversionAssaultStateCheckIntervalTicks = 15;
        internal static int ObservationDeploymentWorkTicks
            => Math.Max(
                1,
                GetObservationDefinition()?.ObservationDeploymentWorkTicks ?? 1);
        internal static int ObservationRecoveryWorkTicks
            => Math.Max(
                1,
                GetObservationDefinition()?.ObservationRecoveryWorkTicks ?? 1);
        internal static int ObservationTransmissionWorkTicks
            => Math.Max(
                1,
                GetObservationDefinition()?.ObservationTransmissionWorkTicks ?? 1);
        private const int InitialMinimumDelayTicks = 180000;
        private const int InitialMaximumDelayTicks = 360000;

        private int nextStateCheckTick;
        private int nextOpportunityTick;
        private bool communicatorGateBlocked;
        private int communicatorGateBlockedSinceTick;
        private TokraOrganicOperationInstance activeOperation
            = new TokraOrganicOperationInstance();
        private TokraOrganicOperationFollowUp followUp
            = new TokraOrganicOperationFollowUp();
        private TokraOrganicOperationArchetype lastOfferedArchetype;
        private TokraOrganicOperationArchetype lastCompletedArchetype;
        private int completedOperationCount;
        private int failedOperationCount;
        private int expiredOfferCount;
        private int lastObservationResultVariant = -1;
        private int lastIntelligenceResultVariant = -1;
        private Dictionary<string, int> lastMissionTextVariantIndexes
            = new Dictionary<string, int>();

        private TokraOrganicOperationArchetype activeArchetype
        {
            get => activeOperation.archetype;
            set => activeOperation.archetype = value;
        }

        private TokraOrganicOperationState activeState
        {
            get => activeOperation.state;
            set
            {
                activeOperation.state = value;

                if (activeOperation.frameworkRuntime != null
                    && !string.IsNullOrEmpty(
                        activeOperation.frameworkRuntime.missionDefName))
                {
                    activeOperation.frameworkRuntime.phaseId
                        = value.ToString().ToLowerInvariant();
                }
            }
        }

        private int activeMapId
        {
            get => activeOperation.mapId;
            set => activeOperation.mapId = value;
        }

        private int offerCreatedTick
        {
            get => activeOperation.offerCreatedTick;
            set => activeOperation.offerCreatedTick = value;
        }

        private int offerExpiryTick
        {
            get => activeOperation.offerExpiryTick;
            set => activeOperation.offerExpiryTick = value;
        }

        private int acceptedTick
        {
            get => activeOperation.acceptedTick;
            set => activeOperation.acceptedTick = value;
        }

        private int reportReadyTick
        {
            get => activeOperation.reportReadyTick;
            set => activeOperation.reportReadyTick = value;
        }

        private int operationDeadlineTick
        {
            get => activeOperation.deadlineTick;
            set => activeOperation.deadlineTick = value;
        }

        private bool readyNotificationSent
        {
            get => activeOperation.readyNotificationSent;
            set => activeOperation.readyNotificationSent = value;
        }

        private bool resolutionApplied
        {
            get => activeOperation.resolutionApplied;
            set => activeOperation.resolutionApplied = value;
        }

        private Thing activeDeadDrop
        {
            get => activeOperation.objective;
            set => activeOperation.objective = value;
        }

        private Thing observationPointMarker
        {
            get => activeOperation.observationPointMarker;
            set => activeOperation.observationPointMarker = value;
        }

        private IntVec3 observationTargetCell
        {
            get => activeOperation.observationTargetCell;
            set => activeOperation.observationTargetCell = value;
        }

        private bool observationDeviceDeployed
        {
            get => activeOperation.observationDeviceDeployed;
            set => activeOperation.observationDeviceDeployed = value;
        }

        private int observationReadyTick
        {
            get => activeOperation.observationReadyTick;
            set => activeOperation.observationReadyTick = value;
        }

        private int observationWorkTotalTicks
        {
            get => activeOperation.observationWorkTotalTicks;
            set => activeOperation.observationWorkTotalTicks = value;
        }

        private int observationWorkRemainingTicks
        {
            get => activeOperation.observationWorkRemainingTicks;
            set => activeOperation.observationWorkRemainingTicks = value;
        }

        private int observationTransmissionTotalTicks
        {
            get => activeOperation.observationTransmissionTotalTicks;
            set => activeOperation.observationTransmissionTotalTicks = value;
        }

        private int observationTransmissionRemainingTicks
        {
            get => activeOperation.observationTransmissionRemainingTicks;
            set => activeOperation.observationTransmissionRemainingTicks = value;
        }

        private int observationResultVariant
        {
            get => activeOperation.observationResultVariant;
            set => activeOperation.observationResultVariant = value;
        }

        private TokraIntelligenceAnalysisMethod intelligenceAnalysisMethod
        {
            get => activeOperation.intelligenceAnalysisMethod;
            set => activeOperation.intelligenceAnalysisMethod = value;
        }

        private int intelligenceWorkTotalTicks
        {
            get => activeOperation.intelligenceWorkTotalTicks;
            set => activeOperation.intelligenceWorkTotalTicks = value;
        }

        private int intelligenceWorkRemainingTicks
        {
            get => activeOperation.intelligenceWorkRemainingTicks;
            set => activeOperation.intelligenceWorkRemainingTicks = value;
        }

        private bool intelligenceInterferenceRollResolved
        {
            get => activeOperation.intelligenceInterferenceRollResolved;
            set => activeOperation.intelligenceInterferenceRollResolved = value;
        }

        private bool intelligenceInterferenceTriggered
        {
            get => activeOperation.intelligenceInterferenceTriggered;
            set => activeOperation.intelligenceInterferenceTriggered = value;
        }

        private bool intelligencePatrolQueued
        {
            get => activeOperation.intelligencePatrolQueued;
            set => activeOperation.intelligencePatrolQueued = value;
        }

        private int intelligenceResultVariant
        {
            get => activeOperation.intelligenceResultVariant;
            set => activeOperation.intelligenceResultVariant = value;
        }

        private Pawn activeWoundedAgent
        {
            get => activeOperation.woundedAgent;
            set => activeOperation.woundedAgent = value;
        }

        private bool woundedAgentInitialCareReceived
        {
            get => activeOperation.woundedAgentInitialCareReceived;
            set => activeOperation.woundedAgentInitialCareReceived = value;
        }

        private int woundedAgentInitialTendedConditionCount
        {
            get => activeOperation.woundedAgentInitialTendedConditionCount;
            set => activeOperation.woundedAgentInitialTendedConditionCount = value;
        }

        private int woundedAgentStableSinceTick
        {
            get => activeOperation.woundedAgentStableSinceTick;
            set => activeOperation.woundedAgentStableSinceTick = value;
        }

        private bool woundedAgentDepartureOrdered
        {
            get => activeOperation.woundedAgentDepartureOrdered;
            set => activeOperation.woundedAgentDepartureOrdered = value;
        }

        private int woundedAgentDepartureDeadlineTick
        {
            get => activeOperation.woundedAgentDepartureDeadlineTick;
            set => activeOperation.woundedAgentDepartureDeadlineTick = value;
        }

        private Pawn activeMedicalSupplyLiaison
        {
            get => activeOperation.medicalSupplyLiaison;
            set => activeOperation.medicalSupplyLiaison = value;
        }

        private IntVec3 medicalSupplyMeetingCell
        {
            get => activeOperation.medicalSupplyMeetingCell;
            set => activeOperation.medicalSupplyMeetingCell = value;
        }

        private int medicalSupplyArrivalTick
        {
            get => activeOperation.medicalSupplyArrivalTick;
            set => activeOperation.medicalSupplyArrivalTick = value;
        }

        private bool medicalSupplyArrivalNotified
        {
            get => activeOperation.medicalSupplyArrivalNotified;
            set => activeOperation.medicalSupplyArrivalNotified = value;
        }

        private bool medicalSupplyDepartureOrdered
        {
            get => activeOperation.medicalSupplyDepartureOrdered;
            set => activeOperation.medicalSupplyDepartureOrdered = value;
        }

        private TokraJaffaOfficerCaptureTransferState jaffaOfficerCapture
            => activeOperation.jaffaOfficerCapture;

        private Pawn departingMedicalSupplyLiaison
        {
            get => followUp.departingMedicalSupplyLiaison;
            set => followUp.departingMedicalSupplyLiaison = value;
        }

        private bool departingMedicalSupplyDeathPenaltyPending
        {
            get => followUp.medicalSupplyDeathPenaltyPending;
            set => followUp.medicalSupplyDeathPenaltyPending = value;
        }

        internal TokraOrganicOperationState ActiveState => activeState;


        public GameComponent_TokraOrganicOperationManager(Game game)
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
                ref communicatorGateBlocked,
                "tokraOrganicCommunicatorGateBlocked",
                false);
            Scribe_Values.Look(
                ref communicatorGateBlockedSinceTick,
                "tokraOrganicCommunicatorGateBlockedSinceTick",
                0);
            Scribe_Deep.Look(
                ref activeOperation,
                "tokraOrganicActiveOperation");
            Scribe_Deep.Look(
                ref followUp,
                "tokraOrganicOperationFollowUp");
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
            Scribe_Values.Look(
                ref lastObservationResultVariant,
                "tokraOrganicLastObservationResultVariant",
                -1);
            Scribe_Values.Look(
                ref lastIntelligenceResultVariant,
                "tokraOrganicLastIntelligenceResultVariant",
                -1);
            Scribe_Collections.Look(
                ref lastMissionTextVariantIndexes,
                "tokraOrganicLastMissionTextVariantIndexes",
                LookMode.Value,
                LookMode.Value);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                NormalizeLoadedState();
            }
        }

        public override void GameComponentTick()
        {
            base.GameComponentTick();

            if (Find.TickManager == null
                || !TokraFactionUtility.HasPersistentFaction())
            {
                return;
            }

            int currentTick = Find.TickManager.TicksGame;

            if (currentTick < nextStateCheckTick)
            {
                return;
            }

            int stateCheckInterval = IsDiversionAssaultStateActive()
                ? DiversionAssaultStateCheckIntervalTicks
                : IsMedicalSupplyStateActive()
                    ? MedicalSupplyStateCheckIntervalTicks
                    : IsObservationStateActive()
                        ? ActiveWorkStateCheckIntervalTicks
                        : StateCheckIntervalTicks;
            nextStateCheckTick = currentTick + stateCheckInterval;
            TickDepartingMedicalSupplyLiaison();

            if (activeState != TokraOrganicOperationState.None)
            {
                TickActiveOpportunity(currentTick);
                return;
            }

            ThingWithComps availableCommunicator;

            if (!TokraSecureCommunicatorAvailabilityUtility
                    .TryFindAvailableCommunicator(
                        out availableCommunicator))
            {
                if (!communicatorGateBlocked)
                {
                    communicatorGateBlocked = true;
                    communicatorGateBlockedSinceTick = currentTick;
                }

                return;
            }

            if (communicatorGateBlocked)
            {
                communicatorGateBlocked = false;
                communicatorGateBlockedSinceTick = 0;
                ScheduleNextOpportunity(currentTick);
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
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                && manager.HasCommunicatorInteractionForMap(map);
        }

        public static string GetCommunicatorActionLabel(Map map)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null
                || !manager.HasCommunicatorInteractionForMap(map))
            {
                return "GR_TokraOrganicOperation_FloatMenuUnavailable"
                    .Translate()
                    .ToString();
            }

            TokraOrganicOperationDefinition definition
                = manager.GetActiveDefinition();

            if (definition == null)
            {
                return "GR_TokraOrganicOperation_FloatMenuUnavailable"
                    .Translate()
                    .ToString();
            }

            string key;

            if (manager.activeState == TokraOrganicOperationState.Offered)
            {
                key = definition.AcceptActionKey;
            }
            else if (manager.activeArchetype
                    == TokraOrganicOperationArchetype.GoauldObservation
                && manager.observationTransmissionTotalTicks > 0
                && manager.observationTransmissionRemainingTicks > 0)
            {
                key = definition.ResumeActionKey;
            }
            else if (manager.activeArchetype
                == TokraOrganicOperationArchetype.DeadDropRecovery
                && manager.intelligenceAnalysisMethod
                    != TokraIntelligenceAnalysisMethod.None)
            {
                key = "GR_TokraOrganicOperation_FloatMenuResumeIntelligence";
            }
            else
            {
                key = definition.CompleteActionKey;
            }

            return string.IsNullOrEmpty(key)
                ? "GR_TokraOrganicOperation_FloatMenuUnavailable"
                    .Translate()
                    .ToString()
                : key.Translate().ToString();
        }

        public static string GetCommunicatorDisabledReason(Map map)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null)
            {
                return "GR_TokraOrganicOperation_ManagerUnavailable"
                    .Translate()
                    .ToString();
            }

            if (!manager.HasCommunicatorInteractionForMap(map))
            {
                return "GR_TokraOrganicOperation_NoActiveOpportunity"
                    .Translate()
                    .ToString();
            }

            if (manager.activeState == TokraOrganicOperationState.Offered)
            {
                return null;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;

            if (manager.activeArchetype
                == TokraOrganicOperationArchetype.JaffaOfficerCapture)
            {
                manager.SyncJaffaOfficerCaptureStateFromWorldSite();
                return TokraJaffaOfficerCaptureTransferController
                    .GetExtractionRequestDisabledReason(
                        manager.jaffaOfficerCapture,
                        map);
            }

            if (manager.activeArchetype
                == TokraOrganicOperationArchetype.GoauldObservation)
            {
                if (!manager.HasValidObservationDevice())
                {
                    return manager.GetObservationTextKey("deviceLost")
                        .Translate()
                        .ToString();
                }

                if (manager.operationDeadlineTick > 0
                    && currentTick >= manager.operationDeadlineTick)
                {
                    return manager.GetObservationTextKey("expired")
                        .Translate()
                        .ToString();
                }

                return manager.activeState == TokraOrganicOperationState.Ready
                    ? null
                    : manager.GetObservationTextKey("dataNotReady")
                        .Translate()
                        .ToString();
            }

            if (manager.activeArchetype
                == TokraOrganicOperationArchetype.DeadDropRecovery)
            {
                if (!manager.HasValidIntelligenceObjective())
                {
                    return manager.GetIntelligenceTextKey("noLongerActive")
                        .Translate()
                        .ToString();
                }

                if (manager.operationDeadlineTick > 0
                    && currentTick >= manager.operationDeadlineTick)
                {
                    return manager.GetIntelligenceTextKey("windowExpired")
                        .Translate()
                        .ToString();
                }

                return null;
            }

            manager.UpdateObservationReadyState(currentTick, notifyPlayer: false);

            if (manager.activeState == TokraOrganicOperationState.Accepted
                && currentTick < manager.reportReadyTick)
            {
                int remainingHours = GetRoundedUpHours(
                    manager.reportReadyTick - currentTick);

                return "GR_TokraOrganicOperation_ObservationInProgress"
                    .Translate(remainingHours.ToString())
                    .ToString();
            }

            if (manager.operationDeadlineTick > 0
                && currentTick >= manager.operationDeadlineTick)
            {
                return "GR_TokraOrganicOperation_ObservationExpired"
                    .Translate()
                    .ToString();
            }

            return null;
        }

        public static string GetInspectStatusForMap(Map map)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null || !manager.IsActiveForMap(map))
            {
                return null;
            }

            return manager.GetActiveStatusLabel(includePrefix: true);
        }

        public static string GetStatusReportLineForMap(Map map)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null || !manager.IsActiveForMap(map))
            {
                return "GR_TokraOrganicOperation_StatusReportNone"
                    .Translate()
                    .ToString();
            }

            return "GR_TokraOrganicOperation_StatusReportActive"
                .Translate(manager.GetActiveStatusLabel(includePrefix: false))
                .ToString();
        }

        public static bool TryHandleCommunicatorInteraction(
            Map map,
            Pawn operatorPawn)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                && manager.HasCommunicatorInteractionForMap(map)
                && manager.TryHandleInteraction(map, operatorPawn);
        }

        private static TokraOrganicOperationDefinition
            GetObservationDefinition()
        {
            return TokraOrganicOperationFramework.GetDefinition(
                TokraOrganicOperationArchetype.GoauldObservation);
        }

        private static TokraOrganicOperationDefinition
            GetIntelligenceDefinition()
        {
            return TokraOrganicOperationFramework.GetDefinition(
                TokraOrganicOperationArchetype.DeadDropRecovery);
        }

        private static int GetConfiguredIntelligenceWorkTicks(
            TokraIntelligenceAnalysisMethod method)
        {
            TokraOrganicOperationDefinition definition
                = GetIntelligenceDefinition();

            return Math.Max(
                1,
                method == TokraIntelligenceAnalysisMethod.Cautious
                    ? definition?.IntelligenceCautiousWorkTicks ?? 1
                    : definition?.IntelligenceAcceleratedWorkTicks ?? 1);
        }

        private static JobDef GetIntelligenceAnalysisJobDef()
        {
            string defName
                = GetIntelligenceDefinition()?.IntelligenceAnalysisJobDefName;
            return string.IsNullOrEmpty(defName)
                ? null
                : DefDatabase<JobDef>.GetNamedSilentFail(defName);
        }

        public static JobDef GetObservationDeploymentJobDef()
        {
            TokraOrganicOperationDefinition definition
                = GetObservationDefinition();
            string defName = definition?.ObservationDeploymentJobDefName;
            return string.IsNullOrEmpty(defName)
                ? null
                : DefDatabase<JobDef>.GetNamedSilentFail(defName);
        }

        public static JobDef GetObservationTransmissionJobDef()
        {
            TokraOrganicOperationDefinition definition
                = GetObservationDefinition();
            string defName = definition?.ObservationTransmissionJobDefName;
            return string.IsNullOrEmpty(defName)
                ? null
                : DefDatabase<JobDef>.GetNamedSilentFail(defName);
        }

        public static string GetObservationDeploymentActionLabel()
        {
            string key = GetObservationDefinition()?.DeployActionKey;
            return string.IsNullOrEmpty(key)
                ? null
                : key.Translate().ToString();
        }

        public static string GetObservationRecoveryActionLabel(bool ready)
        {
            TokraOrganicOperationDefinition definition
                = GetObservationDefinition();
            string key = ready
                ? definition?.RecoverActionKey
                : definition?.ContinueActionKey;
            return string.IsNullOrEmpty(key)
                ? null
                : key.Translate().ToString();
        }

        public static string GetObservationJobUnavailableText()
        {
            TokraOrganicOperationDefinition definition
                = GetObservationDefinition();
            string key = definition?.GetRuntimeTextKey("jobUnavailable");
            return string.IsNullOrEmpty(key)
                ? null
                : key.Translate().ToString();
        }

        public static IntVec3 GetObservationTargetCell(Map map)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                    && manager.IsActiveForMap(map)
                    && manager.activeArchetype
                        == TokraOrganicOperationArchetype.GoauldObservation
                ? manager.observationTargetCell
                : IntVec3.Invalid;
        }

        public static string GetObservationDeploymentDisabledReason(
            Thing device,
            Pawn operatorPawn)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null)
            {
                return "GR_TokraOrganicOperation_ManagerUnavailable"
                    .Translate()
                    .ToString();
            }

            return manager.GetObservationDeploymentDisabledReasonInternal(
                device,
                operatorPawn);
        }

        public static bool CanContinueObservationDeployment(
            Thing device,
            Pawn operatorPawn)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                && manager.CanContinueObservationDeploymentInternal(
                    device,
                    operatorPawn);
        }

        public static bool NotifyObservationDeviceDeployed(
            Thing device,
            Pawn operatorPawn)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                && manager.NotifyObservationDeviceDeployedInternal(
                    device,
                    operatorPawn);
        }

        public static Thing GetObservationPoint(Map map)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                    && manager.IsActiveForMap(map)
                    && manager.activeArchetype
                        == TokraOrganicOperationArchetype.GoauldObservation
                ? manager.observationPointMarker
                : null;
        }

        public static Thing GetPoweredObservationCommunicator(Map map)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                ? FindPoweredCommunicator(map)
                : null;
        }

        public static bool PerformObservationWork(
            Thing observationPoint,
            Pawn operatorPawn)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                && manager.PerformObservationWorkInternal(
                    observationPoint,
                    operatorPawn);
        }

        public static bool IsObservationWorkComplete(
            Thing observationPoint)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                && manager.IsObservationWorkCompleteInternal(
                    observationPoint);
        }

        public static float GetObservationWorkProgress(
            Thing observationPoint)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager?.GetObservationWorkProgressInternal(
                observationPoint) ?? 0f;
        }

        public static bool IsObservationPointReady(
            Thing observationPoint)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                && manager.activeArchetype
                    == TokraOrganicOperationArchetype.GoauldObservation
                && manager.activeState == TokraOrganicOperationState.Ready
                && manager.observationDeviceDeployed
                && manager.activeDeadDrop == observationPoint;
        }

        public static bool IsObservationRecoveryVisible(
            Thing observationPoint)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                && manager.activeArchetype
                    == TokraOrganicOperationArchetype.GoauldObservation
                && manager.observationDeviceDeployed
                && manager.activeDeadDrop == observationPoint
                && TokraObservationUtility.IsObservationPoint(
                    observationPoint);
        }

        public static string GetObservationRecoveryDisabledReason(
            Thing observationPoint,
            Pawn operatorPawn)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null)
            {
                return "GR_TokraOrganicOperation_ManagerUnavailable"
                    .Translate()
                    .ToString();
            }

            return manager.GetObservationRecoveryDisabledReasonInternal(
                observationPoint,
                operatorPawn);
        }

        public static bool TryStartObservationRecovery(
            Thing observationPoint,
            Pawn operatorPawn)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                && manager.TryStartObservationRecoveryInternal(
                    observationPoint,
                    operatorPawn);
        }

        public static bool TryRecoverObservationDevice(
            Thing observationPoint,
            Pawn operatorPawn,
            out Thing recoveredDevice)
        {
            recoveredDevice = null;
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                && manager.TryRecoverObservationDeviceInternal(
                    observationPoint,
                    operatorPawn,
                    out recoveredDevice);
        }

        public static bool CanContinueObservationTransmission(
            Thing communicator,
            Pawn operatorPawn)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                && manager.CanContinueObservationTransmissionInternal(
                    communicator,
                    operatorPawn);
        }

        public static bool PerformObservationTransmissionWork(
            Thing communicator,
            Pawn operatorPawn)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                && manager.PerformObservationTransmissionWorkInternal(
                    communicator,
                    operatorPawn);
        }

        public static bool IsObservationTransmissionComplete(
            Thing communicator)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                && manager.IsObservationTransmissionCompleteInternal(
                    communicator);
        }

        public static float GetObservationTransmissionProgress(
            Thing communicator)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager?.GetObservationTransmissionProgressInternal(
                communicator) ?? 0f;
        }

        public static bool TryCompleteObservationTransmission(
            Thing communicator,
            Pawn operatorPawn)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                && manager.TryCompleteObservationTransmissionInternal(
                    communicator,
                    operatorPawn);
        }

        public static bool TryStartIntelligenceAnalysis(
            Thing communicator,
            Pawn operatorPawn,
            TokraIntelligenceAnalysisMethod method)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                && manager.TryStartIntelligenceAnalysisInternal(
                    communicator,
                    operatorPawn,
                    method);
        }

        public static bool CanContinueIntelligenceAnalysis(
            Thing communicator,
            Pawn operatorPawn)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                && manager.CanContinueIntelligenceAnalysisInternal(
                    communicator,
                    operatorPawn);
        }

        public static bool PerformIntelligenceAnalysisWork(
            Thing communicator,
            Pawn operatorPawn)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                && manager.PerformIntelligenceAnalysisWorkInternal(
                    communicator,
                    operatorPawn);
        }

        public static bool IsIntelligenceAnalysisComplete(Thing communicator)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                && manager.IsIntelligenceAnalysisCompleteInternal(communicator);
        }

        public static float GetIntelligenceAnalysisProgress(Thing communicator)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager?.GetIntelligenceAnalysisProgressInternal(
                communicator) ?? 0f;
        }

        public static bool TryCompleteIntelligenceAnalysis(
            Thing communicator,
            Pawn operatorPawn)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                && manager.TryCompleteIntelligenceAnalysisInternal(
                    communicator,
                    operatorPawn);
        }

        public static bool IsActiveDeadDrop(Thing deadDrop)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                && manager.IsExactActiveDeadDrop(deadDrop)
                && !manager.IsOperationDeadlineExpired();
        }

        public static string GetDeadDropDisabledReason(Thing deadDrop)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null)
            {
                return "GR_TokraOrganicOperation_ManagerUnavailable"
                    .Translate()
                    .ToString();
            }

            if (!manager.IsExactActiveDeadDrop(deadDrop))
            {
                return manager.GetIntelligenceTextKey("noLongerActive")
                    .Translate()
                    .ToString();
            }

            if (manager.IsOperationDeadlineExpired())
            {
                return manager.GetIntelligenceTextKey("windowExpired")
                    .Translate()
                    .ToString();
            }

            return null;
        }

        public static bool TrySecureDeadDrop(
            Thing deadDrop,
            Pawn operatorPawn)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                && manager.IsExactActiveDeadDrop(deadDrop)
                && !manager.IsOperationDeadlineExpired()
                && manager.TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Succeeded,
                    operatorPawn,
                    null);
        }

        internal static string GetMedicalSupplyRuntimeTextKey(
            string id)
        {
            return TokraOrganicOperationFramework.GetDefinition(
                    TokraOrganicOperationArchetype.MedicalSupplyHandoff)
                ?.GetRuntimeTextKey(id);
        }

        internal static int GetMedicalSupplyRequiredCount()
        {
            return TokraOrganicOperationFramework.GetDefinition(
                    TokraOrganicOperationArchetype.MedicalSupplyHandoff)
                ?.MedicalSupplyRequiredCount ?? 0;
        }

        internal static JobDef GetMedicalSupplyDialogueJobDef()
        {
            string defName = TokraOrganicOperationFramework.GetDefinition(
                    TokraOrganicOperationArchetype.MedicalSupplyHandoff)
                ?.MedicalSupplyDialogueJobDefName;

            return string.IsNullOrWhiteSpace(defName)
                ? null
                : DefDatabase<JobDef>.GetNamedSilentFail(defName);
        }

        public static bool IsMedicalSupplyLiaison(Pawn liaison)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                && manager.IsExactMedicalSupplyLiaison(liaison);
        }

        public static bool IsActiveMedicalSupplyLiaison(Pawn liaison)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                && manager.IsExactMedicalSupplyLiaison(liaison)
                && manager.activeState == TokraOrganicOperationState.Ready
                && !manager.IsOperationDeadlineExpired();
        }

        public static string GetMedicalSupplyLiaisonDisabledReason(
            Pawn liaison,
            Pawn negotiator)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null)
            {
                return "GR_TokraOrganicOperation_ManagerUnavailable"
                    .Translate()
                    .ToString();
            }

            TokraOrganicOperationDefinition definition
                = manager.GetActiveDefinition();

            if (definition == null)
            {
                return "GR_TokraOrganicOperation_ManagerUnavailable"
                    .Translate()
                    .ToString();
            }

            if (!manager.IsExactMedicalSupplyLiaison(liaison))
            {
                return definition.GetRuntimeTextKey("noLongerActive")
                    .Translate()
                    .ToString();
            }

            if (manager.activeState != TokraOrganicOperationState.Ready)
            {
                return definition.GetRuntimeTextKey("liaisonEnRoute")
                    .Translate()
                    .ToString();
            }

            if (manager.IsOperationDeadlineExpired())
            {
                return definition.GetRuntimeTextKey("windowExpired")
                    .Translate()
                    .ToString();
            }

            if (negotiator == null
                || negotiator.Dead
                || negotiator.Downed
                || negotiator.Faction != Faction.OfPlayer
                || negotiator.RaceProps?.Humanlike != true
                || negotiator.jobs == null)
            {
                return "GR_TokraSecureCommunicator_PlayerPawnRequired"
                    .Translate()
                    .ToString();
            }

            SkillDef negotiationSkill
                = DefDatabase<SkillDef>.GetNamedSilentFail(
                    definition.MedicalSupplySkillDefName);
            SkillRecord skill = negotiationSkill == null
                ? null
                : negotiator.skills?.GetSkill(negotiationSkill);

            if (skill == null || skill.TotallyDisabled)
            {
                return definition.GetRuntimeTextKey("operatorIncapable")
                    .Translate()
                    .ToString();
            }

            if (!negotiator.CanReach(
                    liaison,
                    Verse.AI.PathEndMode.Touch,
                    Danger.Some))
            {
                return definition.GetRuntimeTextKey("cannotReachLiaison")
                    .Translate()
                    .ToString();
            }

            if (!negotiator.CanReserve(liaison))
            {
                return definition.GetRuntimeTextKey("liaisonReserved")
                    .Translate()
                    .ToString();
            }

            return null;
        }

        public static bool TryOpenMedicalSupplyDialogue(
            Pawn liaison,
            Pawn negotiator)
        {
            string disabledReason = GetMedicalSupplyLiaisonDisabledReason(
                liaison,
                negotiator);

            if (!string.IsNullOrEmpty(disabledReason))
            {
                Messages.Message(
                    disabledReason,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            Find.WindowStack.Add(
                new Dialog_TokraMedicalSupplyHandoff(
                    liaison,
                    negotiator));
            return true;
        }

        public static bool TryCompleteMedicalSupplyHandoff(
            Pawn liaison,
            Pawn negotiator)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();
            string disabledReason = GetMedicalSupplyLiaisonDisabledReason(
                liaison,
                negotiator);

            if (manager == null || !string.IsNullOrEmpty(disabledReason))
            {
                if (!string.IsNullOrEmpty(disabledReason))
                {
                    Messages.Message(
                        disabledReason,
                        MessageTypeDefOf.RejectInput,
                        historical: false);
                }

                return false;
            }

            Map map = liaison.Map;

            TokraOrganicOperationDefinition definition
                = manager.GetActiveDefinition();

            if (definition == null)
            {
                return false;
            }

            if (!TokraOrganicMedicalSupplyUtility.HasEnoughResource(
                    map,
                    negotiator,
                    definition.MedicalSupplyThingDefName,
                    definition.MedicalSupplyRequiredCount))
            {
                Messages.Message(
                    definition.GetRuntimeTextKey("needMedicine").Translate(
                        definition.MedicalSupplyRequiredCount.ToString()),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            if (!TokraOrganicMedicalSupplyUtility.TryConsumeResource(
                    map,
                    negotiator,
                    definition.MedicalSupplyThingDefName,
                    definition.MedicalSupplyRequiredCount))
            {
                Messages.Message(
                    definition.GetRuntimeTextKey("needMedicine").Translate(
                        definition.MedicalSupplyRequiredCount.ToString()),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            manager.BeginMedicalSupplyDeparture(
                liaison,
                monitorDeath: true);

            return manager.TryResolveActiveOperation(
                TokraOrganicOperationOutcome.Succeeded,
                negotiator,
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

        public static bool DebugForceWoundedAgentOpportunity(Map map)
        {
            return DebugForceSpecificOpportunity(
                map,
                TokraOrganicOperationArchetype.WoundedAgentCare);
        }

        public static bool DebugForceMedicalSupplyOpportunity(Map map)
        {
            return DebugForceSpecificOpportunity(
                map,
                TokraOrganicOperationArchetype.MedicalSupplyHandoff);
        }

        public static bool DebugForceTemporaryBaseDeliveryOpportunity(
            Map map)
        {
            return DebugForceSpecificOpportunity(
                map,
                TokraOrganicOperationArchetype.TemporaryBaseDelivery);
        }

        public static bool DebugForceDiversionAssaultOpportunity(
            Map map)
        {
            return DebugForceSpecificOpportunity(
                map,
                TokraOrganicOperationArchetype.DecoyTransmissionDefense);
        }

        public static bool DebugForceJaffaOfficerCaptureOpportunity(
            Map map)
        {
            return DebugForceSpecificOpportunity(
                map,
                TokraOrganicOperationArchetype.JaffaOfficerCapture);
        }

        public static bool DebugForceDiversionAssaultRaid(Map map)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                && manager.IsActiveForMap(map)
                && manager.activeArchetype
                    == TokraOrganicOperationArchetype
                        .DecoyTransmissionDefense
                && manager.activeState
                    == TokraOrganicOperationState.Accepted
                && manager.TryTriggerDiversionAssaultRaid(
                    Find.TickManager?.TicksGame ?? 0);
        }

        public static bool DebugResolveDiversionAssaultVictory(Map map)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                && manager.IsActiveForMap(map)
                && manager.activeArchetype
                    == TokraOrganicOperationArchetype
                        .DecoyTransmissionDefense
                && manager.activeState
                    == TokraOrganicOperationState.Accepted
                && TokraDiversionAssaultUtility.IsRaidTriggered(
                    manager.activeOperation.frameworkRuntime)
                && manager.TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Succeeded,
                    null,
                    null,
                    bypassSuccessValidation: true);
        }

        public static bool DebugResolveDiversionAssaultHostageLoss(Map map)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();
            TokraOrganicOperationDefinition definition
                = manager?.GetActiveDefinition();

            return manager != null
                && definition != null
                && manager.IsActiveForMap(map)
                && manager.activeArchetype
                    == TokraOrganicOperationArchetype
                        .DecoyTransmissionDefense
                && manager.activeState
                    == TokraOrganicOperationState.Accepted
                && manager.TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    null,
                    definition.GetRuntimeTextKey("failureHostage"),
                    bypassSuccessValidation: true);
        }

        internal static void RegisterDiversionAssaultRaidPawns(
            Map map,
            IEnumerable<Pawn> pawns)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null
                || map == null
                || !manager.IsActiveForMap(map)
                || manager.activeArchetype
                    != TokraOrganicOperationArchetype
                        .DecoyTransmissionDefense
                || manager.activeState
                    != TokraOrganicOperationState.Accepted)
            {
                return;
            }

            TokraDiversionAssaultUtility.RegisterRaidPawns(
                manager.activeOperation.frameworkRuntime,
                pawns);
        }

        public static bool DebugForceDistressCallOpportunity(
            Map map,
            TokraDistressCallVariant variant)
        {
            if (!DebugForceSpecificOpportunity(
                    map,
                    TokraOrganicOperationArchetype.DistressCall))
            {
                return false;
            }

            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            TokraDistressCallMissionUtility.SetRuntimeCounter(
                manager?.activeOperation?.frameworkRuntime,
                TokraDistressCallMissionUtility.ForcedVariantCounterKey,
                (int)variant);
            return manager != null;
        }

        public static void RegisterJaffaOfficerCaptureTarget(
            WorldObject_TokraJaffaOfficerCaptureSite site,
            Pawn target)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null
                || manager.activeArchetype
                    != TokraOrganicOperationArchetype.JaffaOfficerCapture
                || target == null)
            {
                return;
            }

            manager.EnsureJaffaOfficerCaptureState();
            manager.jaffaOfficerCapture.targetOfficer = target;
            manager.SyncJaffaOfficerCaptureStateFromSite(site);
        }

        public static void NotifyJaffaOfficerCaptureSiteEvacuated(
            WorldObject_TokraJaffaOfficerCaptureSite site)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null
                || manager.activeArchetype
                    != TokraOrganicOperationArchetype.JaffaOfficerCapture)
            {
                return;
            }

            manager.SyncJaffaOfficerCaptureStateFromSite(site);
            manager.jaffaOfficerCapture.fieldSiteCleared = true;
            TokraJaffaOfficerCaptureMissionUtility.SetRuntimeCounter(
                manager.activeOperation?.frameworkRuntime,
                TokraJaffaOfficerCaptureMissionUtility.WorldObjectIdCounterKey,
                -1);

            GR_Log.Message(
                "Cleared Tok'ra Jaffa-officer field site after caravan "
                + "extraction; prisoner tracking remains active in the "
                + "operation instance.");
        }

        public static bool NotifyJaffaOfficerCaptured(
            WorldObject_TokraJaffaOfficerCaptureSite site)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null
                || manager.activeArchetype
                    != TokraOrganicOperationArchetype.JaffaOfficerCapture
                || (manager.activeState
                        != TokraOrganicOperationState.Accepted
                    && manager.activeState
                        != TokraOrganicOperationState.Ready))
            {
                return false;
            }

            manager.SyncJaffaOfficerCaptureStateFromSite(site);

            if (manager.jaffaOfficerCapture?.targetOfficer == null)
            {
                return false;
            }

            manager.activeState = TokraOrganicOperationState.Ready;
            manager.reportReadyTick = Find.TickManager?.TicksGame ?? 0;
            manager.readyNotificationSent = true;
            return true;
        }

        public static bool NotifyJaffaOfficerCaptureSiteResolved(
            WorldObject_TokraJaffaOfficerCaptureSite site,
            bool succeeded,
            string failureTextId)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null
                || !manager.TryGetActiveJaffaOfficerCaptureSite(
                    site,
                    out TokraOrganicOperationDefinition definition))
            {
                return false;
            }

            string failureTextKey = succeeded
                || string.IsNullOrWhiteSpace(failureTextId)
                    ? null
                    : definition.GetRuntimeTextKey(failureTextId);

            return manager.TryResolveActiveOperation(
                succeeded
                    ? TokraOrganicOperationOutcome.Succeeded
                    : TokraOrganicOperationOutcome.Failed,
                null,
                failureTextKey,
                bypassSuccessValidation: true);
        }

        private bool TryGetActiveJaffaOfficerCaptureSite(
            WorldObject_TokraJaffaOfficerCaptureSite site,
            out TokraOrganicOperationDefinition definition)
        {
            definition = GetActiveDefinition();

            if (site == null
                || definition == null
                || activeArchetype
                    != TokraOrganicOperationArchetype.JaffaOfficerCapture
                || (activeState != TokraOrganicOperationState.Accepted
                    && activeState != TokraOrganicOperationState.Ready))
            {
                return false;
            }

            WorldObject_TokraJaffaOfficerCaptureSite activeSite
                = TokraJaffaOfficerCaptureMissionUtility.FindWorldSite(
                    activeOperation.frameworkRuntime);

            return activeSite != null && activeSite.ID == site.ID;
        }


        public static bool NotifyDistressCallSiteResolved(
            WorldObject_TokraDistressCallSite site,
            bool succeeded,
            string failureTextId)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null
                || site == null
                || manager.activeArchetype
                    != TokraOrganicOperationArchetype.DistressCall
                || (manager.activeState
                        != TokraOrganicOperationState.Accepted
                    && manager.activeState
                        != TokraOrganicOperationState.Ready))
            {
                return false;
            }

            WorldObject_TokraDistressCallSite activeSite
                = TokraDistressCallMissionUtility.FindWorldSite(
                    manager.activeOperation.frameworkRuntime);

            if (activeSite == null || activeSite.ID != site.ID)
            {
                return false;
            }

            TokraOrganicOperationDefinition definition
                = manager.GetActiveDefinition();
            string failureTextKey = succeeded
                || string.IsNullOrWhiteSpace(failureTextId)
                    ? null
                    : definition?.GetRuntimeTextKey(failureTextId);

            return manager.TryResolveActiveOperation(
                succeeded
                    ? TokraOrganicOperationOutcome.Succeeded
                    : TokraOrganicOperationOutcome.Failed,
                null,
                failureTextKey,
                bypassSuccessValidation: true);
        }


        public static bool NotifyTemporaryBaseDeliverySiteResolved(
            WorldObject_TokraTemporaryBaseDeliverySite site,
            bool succeeded,
            string failureTextId,
            bool deliveredLate = false)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null
                || site == null
                || manager.activeArchetype
                    != TokraOrganicOperationArchetype.TemporaryBaseDelivery
                || (manager.activeState
                        != TokraOrganicOperationState.Accepted
                    && manager.activeState
                        != TokraOrganicOperationState.Ready))
            {
                return false;
            }

            WorldObject_TokraTemporaryBaseDeliverySite activeSite
                = TokraTemporaryBaseDeliveryMissionUtility.FindWorldSite(
                    manager.activeOperation.frameworkRuntime);

            if (activeSite == null || activeSite.ID != site.ID)
            {
                return false;
            }

            TokraOrganicOperationDefinition definition
                = manager.GetActiveDefinition();
            string failureTextKey = succeeded
                || string.IsNullOrWhiteSpace(failureTextId)
                    ? null
                    : definition?.GetRuntimeTextKey(failureTextId);
            int? trustChangeOverride = null;
            string trustMessageKeyOverride = null;

            if (succeeded && deliveredLate)
            {
                TokraTemporaryBaseDeliveryMissionUtility.SetDeliveredLate(
                    manager.activeOperation.frameworkRuntime,
                    true);
                trustChangeOverride
                    = definition?.Delivery?.lateSuccessTrustChange;
                trustMessageKeyOverride
                    = definition?.GetRuntimeTextKey("lateTrustMessage");
            }

            return manager.TryResolveActiveOperation(
                succeeded
                    ? TokraOrganicOperationOutcome.Succeeded
                    : TokraOrganicOperationOutcome.Failed,
                null,
                failureTextKey,
                bypassSuccessValidation: true,
                trustChangeOverride: trustChangeOverride,
                trustMessageKeyOverride: trustMessageKeyOverride);
        }

        public static bool NotifyTemporaryBaseDeliveryLateWindowStarted(
            WorldObject_TokraTemporaryBaseDeliverySite site)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null
                || !manager.TryGetActiveTemporaryBaseDeliverySite(
                    site,
                    out TokraOrganicOperationDefinition definition))
            {
                return false;
            }

            GateRimMissionRuntimeData runtime
                = manager.activeOperation.frameworkRuntime;

            if (TokraTemporaryBaseDeliveryMissionUtility
                .IsLateWindowStarted(runtime))
            {
                return true;
            }

            TokraTemporaryBaseDeliveryMissionUtility.SetLateWindowStarted(
                runtime,
                true);

            string requiredCount = TokraTemporaryBaseDeliveryMissionUtility
                .GetRequiredCount(runtime).ToString();
            string itemLabel = TokraTemporaryBaseDeliveryMissionUtility
                .GetContractLabel(runtime);
            string remainingHours = site.GetRemainingHoursString();

            Find.LetterStack?.ReceiveLetter(
                definition.GetRuntimeTextKey("lateWarningLabel").Translate(),
                definition.GetRuntimeTextKey("lateWarningText").Translate(
                    requiredCount,
                    itemLabel,
                    remainingHours),
                LetterDefOf.NeutralEvent,
                site);

            GR_Log.Message(
                "Tok'ra temporary-base delivery entered its late window: "
                + $"site={site.ID}; remainingHours={remainingHours}.");
            return true;
        }

        public static void NotifyTemporaryBaseDeliveryInterceptionTriggered(
            WorldObject_TokraTemporaryBaseDeliverySite site,
            Caravan caravan,
            float threatPoints)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null
                || !manager.TryGetActiveTemporaryBaseDeliverySite(
                    site,
                    out _))
            {
                return;
            }

            GR_Log.Message(
                "Tok'ra temporary-base delivery interception triggered: "
                + $"site={site.ID}; caravan={caravan?.ID ?? -1}; "
                + $"points={threatPoints:0}.");
        }

        private bool TryGetActiveTemporaryBaseDeliverySite(
            WorldObject_TokraTemporaryBaseDeliverySite site,
            out TokraOrganicOperationDefinition definition)
        {
            definition = GetActiveDefinition();

            if (site == null
                || definition == null
                || activeArchetype
                    != TokraOrganicOperationArchetype.TemporaryBaseDelivery
                || (activeState != TokraOrganicOperationState.Accepted
                    && activeState != TokraOrganicOperationState.Ready))
            {
                return false;
            }

            WorldObject_TokraTemporaryBaseDeliverySite activeSite
                = TokraTemporaryBaseDeliveryMissionUtility.FindWorldSite(
                    activeOperation.frameworkRuntime);
            return activeSite != null && activeSite.ID == site.ID;
        }

        public static bool DebugForceTemporaryBaseDeliveryLateWindow(Map map)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null
                || !manager.IsActiveForMap(map)
                || manager.activeArchetype
                    != TokraOrganicOperationArchetype.TemporaryBaseDelivery)
            {
                return false;
            }

            WorldObject_TokraTemporaryBaseDeliverySite site
                = TokraTemporaryBaseDeliveryMissionUtility.FindWorldSite(
                    manager.activeOperation.frameworkRuntime);
            return site?.DebugEnterLateWindow() == true;
        }

        public static bool DebugForceTemporaryBaseDeliveryInterception(Map map)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null
                || !manager.IsActiveForMap(map)
                || manager.activeArchetype
                    != TokraOrganicOperationArchetype.TemporaryBaseDelivery)
            {
                return false;
            }

            WorldObject_TokraTemporaryBaseDeliverySite site
                = TokraTemporaryBaseDeliveryMissionUtility.FindWorldSite(
                    manager.activeOperation.frameworkRuntime);
            return site?.DebugForceInterception() == true;
        }

        public static bool DebugForceTemporaryBaseDeliveryDestinationCompromise(
            Map map)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null
                || !manager.IsActiveForMap(map)
                || manager.activeArchetype
                    != TokraOrganicOperationArchetype.TemporaryBaseDelivery)
            {
                return false;
            }

            WorldObject_TokraTemporaryBaseDeliverySite site
                = TokraTemporaryBaseDeliveryMissionUtility.FindWorldSite(
                    manager.activeOperation.frameworkRuntime);
            return site?.DebugForceDestinationCompromise() == true;
        }

        public static bool DebugAcceptActiveOffer(Map map)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null
                || !manager.IsActiveForMap(map)
                || manager.activeState != TokraOrganicOperationState.Offered)
            {
                return false;
            }

            return manager.TryHandleInteraction(map, null);
        }

        public static bool DebugSucceedActiveOperation(Map map)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null
                || !manager.IsActiveForMap(map)
                || (manager.activeState != TokraOrganicOperationState.Accepted
                    && manager.activeState != TokraOrganicOperationState.Ready))
            {
                return false;
            }

            if (manager.activeArchetype
                == TokraOrganicOperationArchetype.MedicalSupplyHandoff)
            {
                manager.BeginMedicalSupplyDeparture(
                    manager.activeMedicalSupplyLiaison,
                    monitorDeath: true);
            }
            else if (manager.activeArchetype
                == TokraOrganicOperationArchetype.WoundedAgentCare)
            {
                TokraOrganicWoundedAgentUtility.TryOrderDeparture(
                    manager.activeWoundedAgent);
            }

            return manager.TryResolveActiveOperation(
                TokraOrganicOperationOutcome.Succeeded,
                null,
                null,
                bypassSuccessValidation: true);
        }

        public static bool DebugExpireCurrentState(Map map)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null || !manager.IsActiveForMap(map))
            {
                return false;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;

            if (manager.activeState == TokraOrganicOperationState.Offered)
            {
                manager.offerExpiryTick = currentTick;
                manager.TickActiveOpportunity(currentTick);
                return true;
            }

            if (manager.activeArchetype
                == TokraOrganicOperationArchetype.TemporaryBaseDelivery)
            {
                WorldObject_TokraTemporaryBaseDeliverySite site
                    = TokraTemporaryBaseDeliveryMissionUtility.FindWorldSite(
                        manager.activeOperation.frameworkRuntime);
                return site?.DebugExpireContract() == true;
            }

            if (manager.activeArchetype
                == TokraOrganicOperationArchetype.JaffaOfficerCapture)
            {
                WorldObject_TokraJaffaOfficerCaptureSite site
                    = TokraJaffaOfficerCaptureMissionUtility.FindWorldSite(
                        manager.activeOperation.frameworkRuntime);

                if (site != null)
                {
                    return site.DebugExpireOperation();
                }

                TokraOrganicOperationDefinition captureDefinition
                    = manager.GetActiveDefinition();
                return manager.TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    null,
                    captureDefinition?.GetRuntimeTextKey("failureTimeout"),
                    bypassSuccessValidation: true);
            }

            manager.operationDeadlineTick = currentTick;
            manager.TickActiveOpportunity(currentTick);
            return true;
        }

        public static string GetDebugStateReport(Map map)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null)
            {
                return "Tok'ra organic operation manager unavailable.";
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            TokraOrganicOperationDefinition definition
                = manager.GetActiveDefinition();

            if (!manager.IsActiveForMap(map) || definition == null)
            {
                TokraCommunicatorAvailabilitySnapshot availability
                    = TokraSecureCommunicatorAvailabilityUtility.Inspect();

                return "Tok'ra organic operation: none active on this map."
                    + "\nCommunicator gate blocked: "
                    + manager.communicatorGateBlocked
                    + " | blocked since: "
                    + manager.communicatorGateBlockedSinceTick
                    + "\nCommunicator available: "
                    + availability.IsAvailable
                    + " | failure: "
                    + availability.Failure
                    + "\nNext opportunity tick: "
                    + manager.nextOpportunityTick
                    + " | remaining: "
                    + Math.Max(0, manager.nextOpportunityTick - currentTick)
                    + "\nCompleted: "
                    + manager.completedOperationCount
                    + " | Failed: "
                    + manager.failedOperationCount
                    + " | Ignored offers: "
                    + manager.expiredOfferCount
                    + "\nPost-resolution penalty pending: "
                    + manager.departingMedicalSupplyDeathPenaltyPending
                    + " | Liaison: "
                    + (manager.departingMedicalSupplyLiaison?.LabelShortCap
                        ?? "none");
            }

            WorldObject_TokraJaffaOfficerCaptureSite captureSite
                = TokraJaffaOfficerCaptureMissionUtility.FindWorldSite(
                    manager.activeOperation.frameworkRuntime);
            manager.SyncJaffaOfficerCaptureStateFromSite(captureSite);
            Pawn captureTarget = manager.jaffaOfficerCapture?.targetOfficer;
            Caravan captureCaravan
                = TokraJaffaOfficerCaptureMissionUtility
                    .FindPlayerCaravanContaining(captureTarget);

            return "Tok'ra organic operation debug"
                + "\nArchetype: " + definition.DebugLabel
                + "\nState: " + manager.activeState
                + "\nMap: " + manager.activeMapId
                + "\nCurrent tick: " + currentTick
                + "\nOffer expiry: " + manager.offerExpiryTick
                + "\nAccepted: " + manager.acceptedTick
                + "\nReady/arrival: " + manager.reportReadyTick
                + "\nDeadline: " + manager.operationDeadlineTick
                + "\nObjective: "
                + (manager.activeDeadDrop?.LabelShortCap ?? "none")
                + " | carried by: "
                + (manager.FindIntelligenceObjectiveCarrier()?.LabelShortCap
                    ?? "none")
                + "\nObservation target: "
                + manager.observationTargetCell
                + " | deployed: "
                + manager.observationDeviceDeployed
                + " | ready tick: "
                + manager.observationReadyTick
                + " | observation work: "
                + manager.observationWorkRemainingTicks
                + "/"
                + manager.observationWorkTotalTicks
                + " | transmission: "
                + manager.observationTransmissionRemainingTicks
                + "/"
                + manager.observationTransmissionTotalTicks
                + " | result variant: "
                + manager.observationResultVariant
                + "\nIntelligence method: "
                + manager.intelligenceAnalysisMethod
                + "\nIntelligence work: "
                + manager.intelligenceWorkRemainingTicks
                + "/"
                + manager.intelligenceWorkTotalTicks
                + "\nInterference resolved: "
                + manager.intelligenceInterferenceRollResolved
                + " | triggered: "
                + manager.intelligenceInterferenceTriggered
                + " | patrol queued: "
                + manager.intelligencePatrolQueued
                + " | result variant: "
                + manager.intelligenceResultVariant
                + "\nWounded agent: "
                + (manager.activeWoundedAgent?.LabelShortCap ?? "none")
                + "\nMedical liaison: "
                + (manager.activeMedicalSupplyLiaison?.LabelShortCap ?? "none")
                + "\nDistress site: "
                + (TokraDistressCallMissionUtility.FindWorldSite(
                        manager.activeOperation.frameworkRuntime)?.ID.ToString()
                    ?? "none")
                + " | planned variant: "
                + ((TokraDistressCallVariant)TokraDistressCallMissionUtility
                    .GetRuntimeCounter(
                        manager.activeOperation.frameworkRuntime,
                        TokraDistressCallMissionUtility.VariantCounterKey,
                        0))
                + "\nDiversion raid due: "
                + TokraDiversionAssaultUtility.GetRaidDueTick(
                    manager.activeOperation.frameworkRuntime)
                + " | raid triggered: "
                + TokraDiversionAssaultUtility.IsRaidTriggered(
                    manager.activeOperation.frameworkRuntime)
                + " | registered raiders: "
                + TokraDiversionAssaultUtility.GetRegisteredRaidPawnCount(
                    manager.activeOperation.frameworkRuntime)
                + " | registered breachers: "
                + manager.GetRegisteredDiversionAssaultBreacherCount()
                + " | active raiders: "
                + manager.GetActiveDiversionAssaultRaiderCount()
                + " | extracted cargo: "
                + (TokraDiversionAssaultUtility.GetExtractedCargoKind(
                        manager.activeOperation.frameworkRuntime)
                    ?? "none")
                + "\nCapture site: "
                + (captureSite?.ID.ToString() ?? "none")
                + " | map loaded: "
                + (captureSite?.HasMap ?? false)
                + " | target: "
                + (captureTarget?.LabelShortCap ?? "none")
                + " | prisoner: "
                + (captureTarget?.IsPrisonerOfColony ?? false)
                + " | caravan: "
                + (captureCaravan?.ID.ToString() ?? "none")
                + " | remaining: "
                + Math.Max(
                    0,
                    manager.operationDeadlineTick - currentTick)
                + "\nMission Def: "
                + (manager.activeOperation.frameworkRuntime?.missionDefName
                    ?? "legacy C#")
                + " | phase: "
                + (manager.activeOperation.frameworkRuntime?.phaseId
                    ?? "none")
                + " | offer variant: "
                + (manager.activeOperation.frameworkRuntime?.GetTextVariant(GateRimMissionFramework.OfferTextBankKey)
                    ?? -1)
                + "\nThreat snapshot: "
                + (manager.activeOperation.frameworkRuntime?.baseThreatPoints.ToString("0")
                    ?? "0")
                + " -> "
                + (manager.activeOperation.frameworkRuntime?.scaledThreatPoints.ToString("0")
                    ?? "0")
                + " (x"
                + (manager.activeOperation.frameworkRuntime?.difficultyFactor.ToString("0.00")
                    ?? "1.00")
                + ")"
                + "\nResolution applied: " + manager.resolutionApplied
                + "\nPost-resolution penalty pending: "
                + manager.departingMedicalSupplyDeathPenaltyPending;
        }

        public static string GetOrchestrationAuditReport(Map map)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null)
            {
                return "Tok'ra organic operation manager unavailable.";
            }

            List<TokraOrganicOperationDefinition> definitions
                = TokraOrganicOperationFramework.AllDefinitions.ToList();
            List<TokraOrganicOperationArchetype> expectedArchetypes
                = Enum.GetValues(typeof(TokraOrganicOperationArchetype))
                    .Cast<TokraOrganicOperationArchetype>()
                    .Where(archetype
                        => archetype != TokraOrganicOperationArchetype.None)
                    .OrderBy(archetype => (int)archetype)
                    .ToList();
            Dictionary<TokraOrganicOperationArchetype, int> definitionCounts
                = definitions
                    .GroupBy(definition => definition.Archetype)
                    .ToDictionary(group => group.Key, group => group.Count());
            List<TokraOrganicOperationArchetype> missingArchetypes
                = expectedArchetypes
                    .Where(archetype
                        => !definitionCounts.ContainsKey(archetype))
                    .ToList();
            List<TokraOrganicOperationArchetype> duplicateArchetypes
                = definitionCounts
                    .Where(pair => pair.Value != 1)
                    .Select(pair => pair.Key)
                    .OrderBy(archetype => (int)archetype)
                    .ToList();
            bool allMissionDefBacked = definitions.All(definition
                => definition.UsesMissionFrameworkDef);
            TokraTrustTier currentTier
                = GameComponent_TokraTrustTracker.GetCurrentTier();
            int currentTick = Find.TickManager?.TicksGame ?? 0;
            StringBuilder report = new StringBuilder();
            bool passed = definitions.Count == expectedArchetypes.Count
                && missingArchetypes.Count == 0
                && duplicateArchetypes.Count == 0
                && allMissionDefBacked;

            report.AppendLine("Tok'ra organic-operation pool audit");
            report.AppendLine(
                "Persisted archetypes: " + expectedArchetypes.Count
                + " | resolved definitions: " + definitions.Count);
            report.AppendLine(
                "Coverage: missing=" + FormatArchetypeList(missingArchetypes)
                + " | duplicates="
                + FormatArchetypeList(duplicateArchetypes));
            report.AppendLine(
                "All definitions MissionDef-backed: "
                + allMissionDefBacked);
            report.AppendLine(
                "Single global active slot: "
                + (manager.activeOperation.IsActive
                    ? manager.activeArchetype + " / " + manager.activeState
                    : "empty"));
            report.AppendLine(
                "Current map: " + (map?.uniqueID.ToString() ?? "none")
                + " | trust: " + currentTier);
            report.AppendLine(
                "Next opportunity: " + manager.nextOpportunityTick
                + " | remaining: "
                + Math.Max(0, manager.nextOpportunityTick - currentTick));
            TokraCommunicatorAvailabilitySnapshot availability
                = TokraSecureCommunicatorAvailabilityUtility.Inspect();
            report.AppendLine(
                "Communicator gate blocked: "
                + manager.communicatorGateBlocked
                + " | blocked since: "
                + manager.communicatorGateBlockedSinceTick
                + " | available: "
                + availability.IsAvailable
                + " | failure: "
                + availability.Failure);
            report.AppendLine(
                "Last offered: " + manager.lastOfferedArchetype
                + " | last completed: " + manager.lastCompletedArchetype);
            report.AppendLine(
                "Resolved: " + manager.completedOperationCount
                + " succeeded / " + manager.failedOperationCount
                + " failed / " + manager.expiredOfferCount
                + " ignored");

            report.AppendLine();
            report.AppendLine("[Definition invariants]");

            foreach (TokraOrganicOperationDefinition definition
                in definitions.OrderBy(item => (int)item.Archetype))
            {
                int offerTextCount
                    = definition.MissionDef?.texts?.offerLetterTexts?.Count
                        ?? 0;
                int successTextCount
                    = CountMissionSuccessNarrativeVariants(definition);
                bool repeatFactorValid
                    = definition.RepeatedArchetypeWeightFactor > 0f
                        && definition.RepeatedArchetypeWeightFactor < 1f;
                bool offerVariantsValid = offerTextCount >= 2;
                bool successVariantsValid = successTextCount >= 2;
                passed &= repeatFactorValid
                    && offerVariantsValid
                    && successVariantsValid;

                report.AppendLine(
                    "- " + definition.Archetype
                    + ": enum=" + (int)definition.Archetype
                    + ", def=" + (definition.MissionDefName ?? "none")
                    + ", repeat="
                    + definition.RepeatedArchetypeWeightFactor.ToString("0.00")
                    + ", offerVariants=" + offerTextCount
                    + ", successVariants=" + successTextCount
                    + ", valid="
                    + (repeatFactorValid
                        && offerVariantsValid
                        && successVariantsValid));
            }

            TokraTrustTier[] tiers =
            {
                TokraTrustTier.Wary,
                TokraTrustTier.Neutral,
                TokraTrustTier.Cooperative,
                TokraTrustTier.Trusted
            };

            foreach (TokraTrustTier tier in tiers)
            {
                report.AppendLine();
                report.AppendLine("[" + tier + "]");

                int configuredCandidateCount;
                List<OrganicOperationCandidate> currentlyEligible
                    = manager.BuildCandidates(
                        tier,
                        map,
                        manager.lastOfferedArchetype,
                        filterOfferability: true,
                        out configuredCandidateCount);
                report.AppendLine(
                    "Eligible now: " + currentlyEligible.Count + "/"
                    + configuredCandidateCount);

                foreach (TokraOrganicOperationDefinition definition
                    in definitions.OrderBy(item => (int)item.Archetype))
                {
                    int minimumDelay;
                    int maximumDelay;
                    TokraOrganicOperationFramework.GetDelayRange(
                        definition.Archetype,
                        tier,
                        out minimumDelay,
                        out maximumDelay);
                    float weight = definition.GetWeight(tier);
                    bool weightValid = weight > 0f;
                    bool delayValid = minimumDelay > 0
                        && maximumDelay >= minimumDelay;
                    bool offerable = IsDefinitionOfferable(definition, map);
                    passed &= weightValid && delayValid;

                    report.AppendLine(
                        "- " + definition.Archetype
                        + ": weight=" + weight.ToString("0.00")
                        + ", delay=" + minimumDelay + "-" + maximumDelay
                        + ", offerable=" + offerable
                        + ", valid=" + (weightValid && delayValid));
                }

                const int simulationCount = 5000;
                Dictionary<TokraOrganicOperationArchetype, int> counts
                    = expectedArchetypes.ToDictionary(
                        archetype => archetype,
                        archetype => 0);
                Random penalizedRandom = new Random(32800 + (int)tier);
                Random baselineRandom = new Random(42800 + (int)tier);
                TokraOrganicOperationArchetype previous
                    = TokraOrganicOperationArchetype.None;
                TokraOrganicOperationArchetype baselinePrevious
                    = TokraOrganicOperationArchetype.None;
                int immediateRepeats = 0;
                int baselineImmediateRepeats = 0;
                int completedDraws = 0;
                int baselineCompletedDraws = 0;

                for (int i = 0; i < simulationCount; i++)
                {
                    List<OrganicOperationCandidate> candidates
                        = manager.BuildCandidates(
                            tier,
                            map,
                            previous,
                            filterOfferability: false,
                            out _);
                    List<OrganicOperationCandidate> baselineCandidates
                        = manager.BuildCandidates(
                            tier,
                            map,
                            TokraOrganicOperationArchetype.None,
                            filterOfferability: false,
                            out _);

                    if (candidates.Count == 0
                        || baselineCandidates.Count == 0)
                    {
                        break;
                    }

                    TokraOrganicOperationArchetype selected
                        = SelectCandidate(
                            candidates,
                            (float)penalizedRandom.NextDouble());
                    TokraOrganicOperationArchetype baselineSelected
                        = SelectCandidate(
                            baselineCandidates,
                            (float)baselineRandom.NextDouble());

                    if (selected == TokraOrganicOperationArchetype.None
                        || baselineSelected
                            == TokraOrganicOperationArchetype.None)
                    {
                        break;
                    }

                    if (selected == previous)
                    {
                        immediateRepeats++;
                    }

                    if (baselineSelected == baselinePrevious)
                    {
                        baselineImmediateRepeats++;
                    }

                    if (counts.ContainsKey(selected))
                    {
                        counts[selected]++;
                    }

                    previous = selected;
                    baselinePrevious = baselineSelected;
                    completedDraws++;
                    baselineCompletedDraws++;
                }

                List<TokraOrganicOperationArchetype> weightedArchetypes
                    = definitions
                        .Where(definition => definition.GetWeight(tier) > 0f)
                        .Select(definition => definition.Archetype)
                        .Distinct()
                        .ToList();
                bool allReached = weightedArchetypes.All(archetype
                    => counts.ContainsKey(archetype)
                        && counts[archetype] > 0);
                bool repeatPenaltyEffective = completedDraws
                        == simulationCount
                    && baselineCompletedDraws == simulationCount
                    && immediateRepeats < baselineImmediateRepeats;
                passed &= allReached && repeatPenaltyEffective;

                string repeatPercentage = completedDraws > 0
                    ? (100f * immediateRepeats / completedDraws)
                        .ToString("0.0")
                    : "0.0";
                string baselineRepeatPercentage = baselineCompletedDraws > 0
                    ? (100f * baselineImmediateRepeats
                        / baselineCompletedDraws).ToString("0.0")
                    : "0.0";
                report.AppendLine(
                    "Simulation: " + completedDraws + " draws, repeats="
                    + immediateRepeats + " (" + repeatPercentage
                    + "%) versus no-penalty baseline="
                    + baselineImmediateRepeats + " ("
                    + baselineRepeatPercentage + "%), all reached="
                    + allReached + ", penalty effective="
                    + repeatPenaltyEffective);

                foreach (TokraOrganicOperationArchetype archetype
                    in expectedArchetypes)
                {
                    report.AppendLine(
                        "  " + archetype + "=" + counts[archetype]);
                }
            }

            report.AppendLine();
            report.AppendLine(
                "Audit result: " + (passed ? "PASS" : "CHECK REQUIRED"));
            report.AppendLine(
                "Runtime selection filters CanOffer before the weighted draw; "
                + "temporarily unavailable missions do not suppress eligible ones.");
            report.AppendLine(
                "Success, failure and ignored offers all schedule another hidden "
                + "delay through the same persistent manager.");

            return report.ToString().TrimEnd();
        }

        private static int CountMissionSuccessNarrativeVariants(
            TokraOrganicOperationDefinition definition)
        {
            int directCount
                = definition?.MissionDef?.texts?.successLetterTexts?.Count
                    ?? 0;
            IEnumerable<GateRimMissionNamedTextBankDef> namedBanks
                = definition?.MissionDef?.texts?.namedTextBanks;

            if (namedBanks == null)
            {
                return directCount;
            }

            return directCount + namedBanks
                .Where(bank => bank != null
                    && !string.IsNullOrEmpty(bank.id)
                    && bank.id.IndexOf(
                        "success",
                        StringComparison.OrdinalIgnoreCase) >= 0)
                .Sum(bank => bank.texts?.Count ?? 0);
        }

        private static string FormatArchetypeList(
            IEnumerable<TokraOrganicOperationArchetype> archetypes)
        {
            if (archetypes == null)
            {
                return "none";
            }

            List<TokraOrganicOperationArchetype> values
                = archetypes.ToList();
            return values.Count == 0
                ? "none"
                : string.Join(", ", values.Select(value => value.ToString()));
        }

        public static bool DebugMakeActiveReady(Map map)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null
                || !manager.IsActiveForMap(map)
                || manager.activeState != TokraOrganicOperationState.Accepted)
            {
                return false;
            }

            TokraOrganicOperationDefinition definition
                = manager.GetActiveDefinition();

            if (definition == null)
            {
                return false;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;

            if (definition.Archetype
                == TokraOrganicOperationArchetype.GoauldObservation)
            {
                if (!manager.HasValidObservationDevice())
                {
                    return false;
                }

                if (!manager.observationDeviceDeployed)
                {
                    Thing device = manager.activeDeadDrop;

                    if (!device.Spawned)
                    {
                        return false;
                    }

                    Thing station = manager.observationPointMarker;

                    if (station == null
                        || station.Destroyed
                        || !station.Spawned)
                    {
                        return false;
                    }

                    device.Destroy(DestroyMode.Vanish);
                    manager.activeDeadDrop = station;
                    manager.observationDeviceDeployed = true;
                    manager.observationReadyTick = 0;
                    int workTicks
                        = manager.GetConfiguredObservationWorkTicks();
                    manager.observationWorkTotalTicks = workTicks;
                    manager.observationWorkRemainingTicks = workTicks;
                    manager.reportReadyTick = 0;
                    return true;
                }

                manager.observationWorkRemainingTicks = 0;
                manager.UpdateObservationReadyState(
                    currentTick,
                    notifyPlayer: false);
                return manager.activeState
                    == TokraOrganicOperationState.Ready;
            }

            if (definition.Archetype
                == TokraOrganicOperationArchetype.MedicalSupplyHandoff)
            {
                manager.medicalSupplyArrivalTick = currentTick;

                if (manager.activeMedicalSupplyLiaison != null)
                {
                    manager.medicalSupplyMeetingCell
                        = manager.activeMedicalSupplyLiaison.Position;
                }

                manager.TickAcceptedMedicalSupply(currentTick);
                return manager.activeMedicalSupplyLiaison != null;
            }

            if (definition.Archetype
                == TokraOrganicOperationArchetype.WoundedAgentCare)
            {
                Pawn patient = manager.activeWoundedAgent;

                if (patient == null || patient.Dead || patient.Destroyed)
                {
                    return false;
                }

                manager.woundedAgentInitialCareReceived = true;
                TokraOrganicWoundedAgentUtility
                    .BeginPostShockRecovery(patient);
                manager.woundedAgentStableSinceTick
                    = currentTick - Math.Max(
                        1,
                        definition.PawnCare?.stableDurationTicks ?? 1);
                manager.TickAcceptedWoundedAgent(currentTick);
                return true;
            }

            if (definition.Archetype
                == TokraOrganicOperationArchetype.DeadDropRecovery)
            {
                if (!manager.HasValidIntelligenceObjective())
                {
                    return false;
                }

                if (manager.intelligenceAnalysisMethod
                    == TokraIntelligenceAnalysisMethod.None)
                {
                    manager.intelligenceAnalysisMethod
                        = TokraIntelligenceAnalysisMethod.Cautious;
                    manager.intelligenceWorkTotalTicks
                        = GetConfiguredIntelligenceWorkTicks(
                            TokraIntelligenceAnalysisMethod.Cautious);
                }

                manager.intelligenceWorkRemainingTicks = 0;
                manager.activeState = TokraOrganicOperationState.Ready;
                return true;
            }

            if (definition.HasPhysicalObjective)
            {
                return manager.activeDeadDrop != null
                    && manager.activeDeadDrop.Spawned
                    && !manager.activeDeadDrop.Destroyed;
            }

            manager.reportReadyTick = currentTick;
            manager.operationDeadlineTick = Math.Max(
                manager.operationDeadlineTick,
                currentTick + StateCheckIntervalTicks);
            manager.activeState = TokraOrganicOperationState.Ready;
            manager.readyNotificationSent = true;

            GR_Log.Message(
                "Advanced Tok'ra organic operation "
                + $"{definition.DebugLabel} to its next testable phase on map "
                + $"{map?.uniqueID.ToString() ?? "unknown"}.");

            return true;
        }

        public static bool DebugSelectCautiousIntelligence(Map map)
        {
            return DebugSelectIntelligenceMethod(
                map,
                TokraIntelligenceAnalysisMethod.Cautious);
        }

        public static bool DebugSelectAcceleratedIntelligence(Map map)
        {
            return DebugSelectIntelligenceMethod(
                map,
                TokraIntelligenceAnalysisMethod.Accelerated);
        }

        public static bool DebugForceIntelligenceInterference(Map map)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null
                || !manager.IsActiveForMap(map)
                || manager.activeArchetype
                    != TokraOrganicOperationArchetype.DeadDropRecovery
                || manager.activeState == TokraOrganicOperationState.Offered)
            {
                return false;
            }

            manager.intelligenceAnalysisMethod
                = TokraIntelligenceAnalysisMethod.Accelerated;
            manager.SetIntelligenceFrameworkPhase(
                TokraIntelligenceAnalysisMethod.Accelerated);
            manager.intelligenceWorkTotalTicks
                = GetConfiguredIntelligenceWorkTicks(
                    TokraIntelligenceAnalysisMethod.Accelerated);
            manager.intelligenceInterferenceRollResolved = true;
            manager.intelligencePatrolQueued
                = manager.TryQueueIntelligencePatrol(map);
            manager.intelligenceInterferenceTriggered
                = manager.intelligencePatrolQueued;
            return manager.intelligencePatrolQueued;
        }

        private static bool DebugSelectIntelligenceMethod(
            Map map,
            TokraIntelligenceAnalysisMethod method)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null
                || !manager.IsActiveForMap(map)
                || manager.activeArchetype
                    != TokraOrganicOperationArchetype.DeadDropRecovery
                || manager.activeState != TokraOrganicOperationState.Accepted
                || !manager.HasValidIntelligenceObjective())
            {
                return false;
            }

            manager.intelligenceAnalysisMethod = method;
            manager.SetIntelligenceFrameworkPhase(method);
            manager.intelligenceWorkTotalTicks
                = GetConfiguredIntelligenceWorkTicks(method);
            manager.intelligenceWorkRemainingTicks
                = manager.intelligenceWorkTotalTicks;
            manager.intelligenceInterferenceRollResolved = false;
            manager.intelligenceInterferenceTriggered = false;
            manager.intelligencePatrolQueued = false;
            manager.intelligenceResultVariant = -1;
            return true;
        }

        public static bool DebugDeployObservationDevice(Map map)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                && manager.activeArchetype
                    == TokraOrganicOperationArchetype.GoauldObservation
                && manager.activeState
                    == TokraOrganicOperationState.Accepted
                && !manager.observationDeviceDeployed
                && DebugMakeActiveReady(map);
        }

        public static bool DebugMakeObservationReady(Map map)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null
                || manager.activeArchetype
                    != TokraOrganicOperationArchetype.GoauldObservation
                || manager.activeState
                    != TokraOrganicOperationState.Accepted)
            {
                return false;
            }

            if (!manager.observationDeviceDeployed
                && !DebugMakeActiveReady(map))
            {
                return false;
            }

            return manager.observationDeviceDeployed
                && DebugMakeActiveReady(map);
        }

        public static bool DebugFailActiveOperation(Map map)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null
                || !manager.IsActiveForMap(map)
                || (manager.activeState
                        != TokraOrganicOperationState.Accepted
                    && manager.activeState
                        != TokraOrganicOperationState.Ready))
            {
                return false;
            }

            string failureTextKey = null;

            if (manager.activeArchetype
                == TokraOrganicOperationArchetype.DeadDropRecovery)
            {
                failureTextKey
                    = manager.GetIntelligenceTextKey("failureTimeout");
            }
            else if (manager.activeArchetype
                == TokraOrganicOperationArchetype.WoundedAgentCare)
            {
                failureTextKey
                    = manager.GetWoundedAgentTextKey("failureTimeout");
            }
            else if (manager.activeArchetype
                == TokraOrganicOperationArchetype.MedicalSupplyHandoff)
            {
                failureTextKey
                    = manager.GetActiveDefinition()
                        ?.GetRuntimeTextKey("failureTimeout");
            }
            else if (manager.activeArchetype
                == TokraOrganicOperationArchetype.DistressCall
                || manager.activeArchetype
                    == TokraOrganicOperationArchetype
                        .DecoyTransmissionDefense
                || manager.activeArchetype
                    == TokraOrganicOperationArchetype.JaffaOfficerCapture)
            {
                failureTextKey
                    = manager.GetActiveDefinition()
                        ?.GetRuntimeTextKey("failureTimeout");
            }

            return manager.TryResolveActiveOperation(
                TokraOrganicOperationOutcome.Failed,
                null,
                failureTextKey);
        }

        public static bool DebugExpireActiveOperation(Map map)
        {
            return DebugFailActiveOperation(map);
        }

        public static bool DebugApplyPendingFollowUp()
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null
                || !manager.departingMedicalSupplyDeathPenaltyPending)
            {
                return false;
            }

            Pawn liaison = manager.departingMedicalSupplyLiaison;
            manager.departingMedicalSupplyDeathPenaltyPending = false;
            manager.departingMedicalSupplyLiaison = null;
            manager.ApplyMedicalSupplyLiaisonDeathConsequence(liaison);

            return true;
        }

        public static bool DebugReset()
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null)
            {
                return false;
            }

            manager.ClearActiveOpportunity();
            manager.followUp = new TokraOrganicOperationFollowUp();
            manager.communicatorGateBlocked = false;
            manager.communicatorGateBlockedSinceTick = 0;
            int currentTick = Find.TickManager?.TicksGame ?? 0;
            manager.nextOpportunityTick = currentTick + Rand.RangeInclusive(
                InitialMinimumDelayTicks,
                InitialMaximumDelayTicks);
            return true;
        }

        public static bool DebugMakeNextNaturalOpportunityDue()
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null || manager.activeOperation.IsActive)
            {
                return false;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            manager.nextOpportunityTick = currentTick;
            manager.nextStateCheckTick = currentTick;
            return true;
        }

        public static bool DebugRollNextNaturalOpportunity(Map map)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            ThingWithComps communicator;

            if (manager == null
                || map == null
                || manager.activeOperation.IsActive
                || !TokraSecureCommunicatorAvailabilityUtility
                    .TryFindAvailableCommunicator(map, out communicator))
            {
                return false;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            manager.communicatorGateBlocked = false;
            manager.communicatorGateBlockedSinceTick = 0;
            manager.nextOpportunityTick = currentTick;

            return manager.TryCreateNextOpportunity(currentTick)
                && manager.activeState == TokraOrganicOperationState.Offered;
        }

        private static bool DebugForceSpecificOpportunity(
            Map map,
            TokraOrganicOperationArchetype archetype)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            ThingWithComps communicator;

            if (manager == null
                || map == null
                || !TokraSecureCommunicatorAvailabilityUtility
                    .TryFindAvailableCommunicator(map, out communicator))
            {
                return false;
            }

            manager.ClearActiveOpportunity();
            manager.communicatorGateBlocked = false;
            manager.communicatorGateBlockedSinceTick = 0;
            manager.nextOpportunityTick = 0;

            return manager.TryCreateOpportunity(
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
                    || activeState == TokraOrganicOperationState.Ready)
                {
                    string lostTextKey = definition.Archetype
                        == TokraOrganicOperationArchetype.WoundedAgentCare
                        ? definition.GetRuntimeTextKey("failureLost")
                        : definition.Archetype
                            == TokraOrganicOperationArchetype
                                .MedicalSupplyHandoff
                            ? definition.GetRuntimeTextKey("failureLost")
                            : definition.Archetype
                                == TokraOrganicOperationArchetype
                                    .DistressCall
                                ? definition.GetRuntimeTextKey(
                                    "failureSiteLost")
                                : definition.Archetype
                                    == TokraOrganicOperationArchetype
                                        .JaffaOfficerCapture
                                    ? definition.GetRuntimeTextKey(
                                        "failureSiteLost")
                                : definition.Archetype
                                    == TokraOrganicOperationArchetype
                                        .GoauldObservation
                                    ? definition.GetRuntimeTextKey(
                                        "failureDeviceLost")
                                    : definition.Archetype
                                        == TokraOrganicOperationArchetype
                                            .DecoyTransmissionDefense
                                        ? definition.GetRuntimeTextKey(
                                            "failureMapLost")
                                    : definition.HasPhysicalObjective
                                    ? definition.GetRuntimeTextKey(
                                        "failureObjectiveLost")
                                    : null;

                    TryResolveActiveOperation(
                        TokraOrganicOperationOutcome.Failed,
                        null,
                        lostTextKey);
                }
                else
                {
                    ClearActiveOpportunity();
                    ScheduleNextOpportunity(currentTick);
                }

                return;
            }

            definition.MissionDef?.Worker?.Tick(
                activeOperation.frameworkRuntime,
                activeMap);

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

            TokraOrganicOperationWorker worker
                = TokraOrganicOperationWorkerRegistry.Get(activeArchetype);

            if (worker == null)
            {
                ClearActiveOpportunity();
                ScheduleNextOpportunity(currentTick);
                return;
            }

            worker.Tick(this, currentTick);
        }


        internal void TickAcceptedDiversionAssault(int currentTick)
        {
            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();
            Map map = GetActiveMap();

            if (definition == null || map == null)
            {
                return;
            }

            GateRimMissionRuntimeData runtime
                = activeOperation.frameworkRuntime;
            TokraDiversionAssaultUtility.Normalize(
                runtime,
                definition,
                acceptedTick,
                currentTick);

            if (!TokraDiversionAssaultUtility.IsRaidTriggered(runtime))
            {
                if (currentTick
                    >= TokraDiversionAssaultUtility.GetRaidDueTick(runtime))
                {
                    TryTriggerDiversionAssaultRaid(currentTick);
                }

                return;
            }

            operationDeadlineTick = 0;

            string extractionFailureTextKey;

            if (TryDetectDiversionAssaultExtraction(
                    map,
                    runtime,
                    definition,
                    out extractionFailureTextKey))
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    null,
                    extractionFailureTextKey,
                    bypassSuccessValidation: true);
                return;
            }

            if (TokraDiversionAssaultUtility
                    .GetRegisteredRaidPawnCount(runtime) > 0
                && GetActiveDiversionAssaultRaiderCount() <= 0)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Succeeded,
                    null,
                    null);
            }
        }

        private bool TryTriggerDiversionAssaultRaid(int currentTick)
        {
            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();
            Map map = GetActiveMap();
            GateRimMissionRuntimeData runtime
                = activeOperation.frameworkRuntime;

            if (definition == null
                || definition.Archetype
                    != TokraOrganicOperationArchetype
                        .DecoyTransmissionDefense
                || map == null
                || activeState != TokraOrganicOperationState.Accepted
                || TokraDiversionAssaultUtility.IsRaidTriggered(runtime))
            {
                return false;
            }

            IncidentDef incidentDef = string.IsNullOrWhiteSpace(
                    definition.DecoyRaidIncidentDefName)
                ? null
                : DefDatabase<IncidentDef>.GetNamedSilentFail(
                    definition.DecoyRaidIncidentDefName);
            Faction goauldFaction = GoauldSystemLordFactionUtility
                .GetOrCreateFaction("Tok'ra diversion assault operation");

            if (incidentDef == null
                || incidentDef.category == null
                || incidentDef.Worker == null
                || goauldFaction == null)
            {
                TokraDiversionAssaultUtility.ScheduleRaidRetry(
                    runtime,
                    definition,
                    currentTick);
                GR_Log.Warning(
                    "Could not trigger the Goa'uld force drawn by the Tok'ra "
                    + "diversion; the incident or faction is unavailable "
                    + "and the operation will retry.");
                return false;
            }

            if (runtime.scaledThreatPoints <= 0f)
            {
                GateRimMissionDifficultySnapshot snapshot
                    = GateRimMissionFramework.CaptureDifficulty(
                        map,
                        definition.MissionDef?.difficulty);
                runtime.baseThreatPoints = snapshot.BaseThreatPoints;
                runtime.scaledThreatPoints = snapshot.ScaledThreatPoints;
                runtime.difficultyFactor = snapshot.Factor;
            }

            IncidentParms parms = StorytellerUtility.DefaultParmsNow(
                incidentDef.category,
                map);
            parms.forced = true;
            parms.faction = goauldFaction;
            parms.points = runtime.scaledThreatPoints;

            if (!incidentDef.Worker.TryExecute(parms))
            {
                TokraDiversionAssaultUtility.ScheduleRaidRetry(
                    runtime,
                    definition,
                    currentTick);
                GR_Log.Warning(
                    "The Goa'uld force drawn by the Tok'ra diversion could "
                    + "not enter the map and will retry; "
                    + $"map={map.uniqueID}; points={parms.points:0}.");
                return false;
            }

            if (TokraDiversionAssaultUtility
                    .GetRegisteredRaidPawnCount(runtime) <= 0)
            {
                TokraDiversionAssaultUtility.RegisterRaidPawns(
                    runtime,
                    map.mapPawns.AllPawnsSpawned.Where(
                        pawn => pawn != null
                            && pawn.Faction == goauldFaction));
            }

            TokraDiversionAssaultUtility.MarkRaidTriggered(runtime);
            operationDeadlineTick = 0;

            GR_Log.Message(
                "Triggered the Goa'uld force drawn by the Tok'ra diversion; "
                + $"map={map.uniqueID}; snapshot="
                + $"{runtime.baseThreatPoints:0}; points={parms.points:0}; "
                + "registered="
                + TokraDiversionAssaultUtility
                    .GetRegisteredRaidPawnCount(runtime)
                + ".");
            return true;
        }

        private bool TryDetectDiversionAssaultExtraction(
            Map map,
            GateRimMissionRuntimeData runtime,
            TokraOrganicOperationDefinition definition,
            out string failureTextKey)
        {
            failureTextKey = null;

            if (map == null || runtime == null || definition == null)
            {
                return false;
            }

            Dictionary<string, Pawn> spawnedById
                = map.mapPawns.AllPawnsSpawned
                    .Where(pawn => pawn != null)
                    .GroupBy(pawn => pawn.ThingID)
                    .ToDictionary(group => group.Key, group => group.First());

            foreach (string pawnId in TokraDiversionAssaultUtility
                .GetRegisteredRaidPawnIds(runtime))
            {
                Pawn pawn;

                if (!spawnedById.TryGetValue(pawnId, out pawn)
                    || pawn == null
                    || pawn.Dead)
                {
                    continue;
                }

                string cargoKind;
                bool carriesMissionCargo = TryGetDiversionAssaultCargoKind(
                    pawn,
                    out cargoKind);

                TokraDiversionAssaultUtility.SetCargoCarrierState(
                    runtime,
                    pawn,
                    carriesMissionCargo);

                if (!carriesMissionCargo || !pawn.Position.OnEdge(map))
                {
                    continue;
                }

                TokraDiversionAssaultUtility.SetExtractedCargoKind(
                    runtime,
                    cargoKind);
                failureTextKey = cargoKind == "hostage"
                    ? definition.GetRuntimeTextKey("failureHostage")
                    : definition.GetRuntimeTextKey("failureLoot");
                return true;
            }

            return false;
        }

        private int GetRegisteredDiversionAssaultBreacherCount()
        {
            Map map = GetActiveMap();
            GateRimMissionRuntimeData runtime
                = activeOperation.frameworkRuntime;

            if (map == null || runtime == null)
            {
                return 0;
            }

            HashSet<string> registeredIds = new HashSet<string>(
                TokraDiversionAssaultUtility
                    .GetRegisteredRaidPawnIds(runtime));

            return map.mapPawns.AllPawnsSpawned.Count(
                pawn => pawn != null
                    && registeredIds.Contains(pawn.ThingID)
                    && pawn.kindDef?.isGoodBreacher == true);
        }

        private int GetActiveDiversionAssaultRaiderCount()
        {
            Map map = GetActiveMap();
            GateRimMissionRuntimeData runtime
                = activeOperation.frameworkRuntime;

            if (map == null || runtime == null)
            {
                return 0;
            }

            HashSet<string> registeredIds = new HashSet<string>(
                TokraDiversionAssaultUtility
                    .GetRegisteredRaidPawnIds(runtime));

            return map.mapPawns.AllPawnsSpawned.Count(
                pawn => pawn != null
                    && registeredIds.Contains(pawn.ThingID)
                    && IsActiveDiversionAssaultRaider(pawn));
        }

        private static bool IsActiveDiversionAssaultRaider(Pawn pawn)
        {
            if (pawn == null
                || pawn.Dead
                || pawn.Downed
                || !pawn.Spawned
                || !pawn.HostileTo(Faction.OfPlayer))
            {
                return false;
            }

            string cargoKind;

            if (TryGetDiversionAssaultCargoKind(pawn, out cargoKind))
            {
                return true;
            }

            string dutyDefName = pawn.mindState?.duty?.def?.defName;
            string jobDefName = pawn.CurJob?.def?.defName;

            return !IsExitOrFleeDefName(dutyDefName)
                && !IsExitOrFleeDefName(jobDefName);
        }

        private static bool TryGetDiversionAssaultCargoKind(
            Pawn pawn,
            out string cargoKind)
        {
            cargoKind = null;
            Thing carriedThing = pawn?.carryTracker?.CarriedThing;

            if (carriedThing is Pawn carriedPawn
                && (carriedPawn.IsColonist
                    || carriedPawn.Faction == Faction.OfPlayer))
            {
                cargoKind = "hostage";
                return true;
            }

            if (carriedThing == null || carriedThing is Pawn)
            {
                return false;
            }

            // A hostile raid pawn only uses carryTracker for a hauled object.
            // Once the object has been picked up, vanilla can immediately
            // replace the Steal job with an exit job, so the carried object
            // itself is the reliable indication that plunder is leaving.
            cargoKind = "loot";
            return true;
        }

        private static bool IsExitOrFleeDefName(string defName)
        {
            return !string.IsNullOrEmpty(defName)
                && (defName.IndexOf(
                        "ExitMap",
                        StringComparison.OrdinalIgnoreCase) >= 0
                    || defName.IndexOf(
                        "Flee",
                        StringComparison.OrdinalIgnoreCase) >= 0);
        }

        internal void TickAcceptedDistressCall(int currentTick)
        {
            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();

            if (definition == null)
            {
                return;
            }

            WorldObject_TokraDistressCallSite site
                = TokraDistressCallMissionUtility.FindWorldSite(
                    activeOperation.frameworkRuntime);

            if (site == null)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    null,
                    definition.GetRuntimeTextKey("failureSiteLost"),
                    bypassSuccessValidation: true);
                return;
            }

            if (operationDeadlineTick > 0
                && currentTick >= operationDeadlineTick)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    null,
                    definition.GetRuntimeTextKey("failureTimeout"),
                    bypassSuccessValidation: true);
            }
        }


        internal void TickAcceptedJaffaOfficerCapture(int currentTick)
        {
            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();

            if (definition == null)
            {
                return;
            }

            WorldObject_TokraJaffaOfficerCaptureSite site
                = TokraJaffaOfficerCaptureMissionUtility.FindWorldSite(
                    activeOperation.frameworkRuntime);

            if (site != null)
            {
                SyncJaffaOfficerCaptureStateFromSite(site);
            }
            else if (jaffaOfficerCapture?.fieldSiteCleared != true)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    null,
                    definition.GetRuntimeTextKey("failureSiteLost"),
                    bypassSuccessValidation: true);
                return;
            }

            Pawn target = jaffaOfficerCapture?.targetOfficer;

            if (target != null
                && target.IsPrisonerOfColony
                && (TokraJaffaOfficerCaptureMissionUtility
                        .FindPlayerCaravanContaining(target) != null
                    || TokraJaffaOfficerCaptureMissionUtility
                        .FindPlayerHomeMapContaining(target) != null))
            {
                activeState = TokraOrganicOperationState.Ready;
                readyNotificationSent = true;
            }

            TokraJaffaOfficerCaptureTransferTickResult result
                = TokraJaffaOfficerCaptureTransferController.Tick(
                    jaffaOfficerCapture,
                    currentTick);

            if (result
                == TokraJaffaOfficerCaptureTransferTickResult.Succeeded)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Succeeded,
                    null,
                    null,
                    bypassSuccessValidation: true);
                return;
            }

            if (result
                == TokraJaffaOfficerCaptureTransferTickResult
                    .FailedTargetKilled)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    null,
                    definition.GetRuntimeTextKey("failureTargetKilled"),
                    bypassSuccessValidation: true);
                return;
            }

            if (result
                == TokraJaffaOfficerCaptureTransferTickResult
                    .FailedTargetLost)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    null,
                    definition.GetRuntimeTextKey("failureTargetLost"),
                    bypassSuccessValidation: true);
                return;
            }

            if (operationDeadlineTick > 0
                && currentTick >= operationDeadlineTick)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    null,
                    definition.GetRuntimeTextKey("failureTimeout"),
                    bypassSuccessValidation: true);
            }
        }


        internal void TickAcceptedTemporaryBaseDelivery(int currentTick)
        {
            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();

            if (definition == null)
            {
                return;
            }

            WorldObject_TokraTemporaryBaseDeliverySite site
                = TokraTemporaryBaseDeliveryMissionUtility.FindWorldSite(
                    activeOperation.frameworkRuntime);

            if (site == null)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    null,
                    definition.GetRuntimeTextKey("failureSiteLost"),
                    bypassSuccessValidation: true);
                return;
            }

            if (site.IsLate)
            {
                TokraTemporaryBaseDeliveryMissionUtility.SetLateWindowStarted(
                    activeOperation.frameworkRuntime,
                    true);
            }
        }

        internal void TickAcceptedMedicalSupply(int currentTick)
        {
            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();

            if (definition == null)
            {
                return;
            }

            if (operationDeadlineTick > 0
                && currentTick >= operationDeadlineTick)
            {
                BeginMedicalSupplyDeparture(
                    activeMedicalSupplyLiaison,
                    monitorDeath: false);
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    activeMedicalSupplyLiaison,
                    definition.GetRuntimeTextKey("failureTimeout"));
                return;
            }

            if (activeMedicalSupplyLiaison == null)
            {
                if (currentTick < medicalSupplyArrivalTick)
                {
                    return;
                }

                Pawn liaison;
                IntVec3 meetingCell;

                GateRimMissionHandoffDef handoff = definition.Handoff;

                if (handoff == null
                    || !TokraOrganicMedicalSupplyUtility.TrySpawnLiaison(
                        GetActiveMap(),
                        handoff.liaisonPawnKindDefName,
                        definition.DeadlineTicks
                            + handoff.departureGraceTicks,
                        out liaison,
                        out meetingCell))
                {
                    medicalSupplyArrivalTick
                        = currentTick + MedicalSupplyStateCheckIntervalTicks;
                    return;
                }

                activeMedicalSupplyLiaison = liaison;
                medicalSupplyMeetingCell = meetingCell;
                activeState = TokraOrganicOperationState.Accepted;
                reportReadyTick = currentTick;
                operationDeadlineTick
                    = currentTick + definition.DeadlineTicks;
                readyNotificationSent = false;
                medicalSupplyArrivalNotified = false;
                medicalSupplyDepartureOrdered = false;

                Find.LetterStack?.ReceiveLetter(
                    definition.GetRuntimeTextKey("arrivalLabel").Translate(),
                    definition.GetRuntimeTextKey("arrivalText").Translate(
                        liaison.LabelShortCap,
                        GetRoundedUpHours(
                            operationDeadlineTick - currentTick).ToString()),
                    LetterDefOf.NeutralEvent,
                    liaison);

                GR_Log.Message(
                    "Tok'ra medical supply liaison "
                    + $"{liaison.LabelShortCap} entered map {activeMapId}; "
                    + $"meeting cell {medicalSupplyMeetingCell}; deadline "
                    + $"{operationDeadlineTick}.");
                return;
            }

            Pawn activeLiaison = activeMedicalSupplyLiaison;

            if (activeLiaison.Dead)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    activeLiaison,
                    definition.GetRuntimeTextKey("failureDeath"));
                return;
            }

            if (activeLiaison.Destroyed
                || activeLiaison.MapHeld == null
                || activeLiaison.MapHeld.uniqueID != activeMapId)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    activeLiaison,
                    definition.GetRuntimeTextKey("failureLost"));
                return;
            }

            if (activeLiaison.IsPrisonerOfColony)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    activeLiaison,
                    definition.GetRuntimeTextKey("failureCaptured"));
                return;
            }

            if (activeState == TokraOrganicOperationState.Ready)
            {
                return;
            }

            if (!TokraOrganicMedicalSupplyUtility.HasReachedMeetingPoint(
                    activeLiaison,
                    medicalSupplyMeetingCell))
            {
                return;
            }

            activeState = TokraOrganicOperationState.Ready;
            readyNotificationSent = true;

            if (!medicalSupplyArrivalNotified)
            {
                medicalSupplyArrivalNotified = true;
                Messages.Message(
                    definition.GetRuntimeTextKey("liaisonReady").Translate(
                        activeLiaison.LabelShortCap),
                    activeLiaison,
                    MessageTypeDefOf.NeutralEvent,
                    historical: true);
            }
        }

        private void TickDepartingMedicalSupplyLiaison()
        {
            Pawn liaison = departingMedicalSupplyLiaison;

            if (liaison == null)
            {
                departingMedicalSupplyDeathPenaltyPending = false;
                return;
            }

            if (liaison.Dead)
            {
                if (departingMedicalSupplyDeathPenaltyPending)
                {
                    departingMedicalSupplyDeathPenaltyPending = false;
                    ApplyMedicalSupplyLiaisonDeathConsequence(liaison);

                }

                departingMedicalSupplyLiaison = null;
                return;
            }

            if (liaison.Destroyed || liaison.MapHeld == null)
            {
                departingMedicalSupplyLiaison = null;
                departingMedicalSupplyDeathPenaltyPending = false;
            }
        }

        private void ApplyMedicalSupplyLiaisonDeathConsequence(
            Pawn liaison)
        {
            TokraOrganicOperationDefinition definition
                = TokraOrganicOperationFramework.GetDefinition(
                    TokraOrganicOperationArchetype.MedicalSupplyHandoff);
            GateRimMissionHandoffDef handoff = definition?.Handoff;

            if (definition == null || handoff == null)
            {
                return;
            }

            GameComponent_TokraTrustTracker
                .NotifyOrganicMedicalSupplyLiaisonDeath(
                    handoff.postHandoffDeathTrustChange,
                    definition.GetRuntimeTextKey(
                        "postHandoffDeathTrust"));

            Find.LetterStack?.ReceiveLetter(
                definition.GetRuntimeTextKey(
                    "postHandoffDeathLabel").Translate(),
                definition.GetRuntimeTextKey(
                    "postHandoffDeathText").Translate(
                        liaison?.LabelShortCap ?? "?"),
                LetterDefOf.NegativeEvent,
                liaison);
        }

        private void BeginMedicalSupplyDeparture(
            Pawn liaison,
            bool monitorDeath)
        {
            if (liaison == null || liaison.Dead || liaison.Destroyed)
            {
                return;
            }

            medicalSupplyDepartureOrdered
                = TokraOrganicMedicalSupplyUtility.TryOrderDeparture(liaison);
            departingMedicalSupplyLiaison = liaison;
            departingMedicalSupplyDeathPenaltyPending = monitorDeath;
        }

        internal void TickAcceptedWoundedAgent(int currentTick)
        {
            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();
            GateRimMissionPawnCareDef profile = definition?.PawnCare;
            Pawn patient = activeWoundedAgent;

            if (definition == null || profile == null)
            {
                return;
            }

            if (patient == null)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    null,
                    definition.GetRuntimeTextKey("failureLost"));
                return;
            }

            if (patient.Dead)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    patient,
                    definition.GetRuntimeTextKey("failureDeath"));
                return;
            }

            if (woundedAgentDepartureOrdered)
            {
                activeState = TokraOrganicOperationState.Ready;

                if (patient.IsPrisonerOfColony)
                {
                    TryResolveActiveOperation(
                        TokraOrganicOperationOutcome.Failed,
                        patient,
                        definition.GetRuntimeTextKey("failureCaptured"));
                    return;
                }

                Map heldMap = patient.MapHeld;

                if (heldMap == null)
                {
                    TryResolveActiveOperation(
                        TokraOrganicOperationOutcome.Succeeded,
                        patient,
                        null);
                    return;
                }

                if (heldMap.uniqueID != activeMapId)
                {
                    TryResolveActiveOperation(
                        TokraOrganicOperationOutcome.Failed,
                        patient,
                        definition.GetRuntimeTextKey("failureLost"));
                    return;
                }

                if (woundedAgentDepartureDeadlineTick > 0
                    && currentTick >= woundedAgentDepartureDeadlineTick)
                {
                    TryResolveActiveOperation(
                        TokraOrganicOperationOutcome.Failed,
                        patient,
                        definition.GetRuntimeTextKey("failureTimeout"));
                }

                return;
            }

            Map patientMap = patient.Destroyed
                ? null
                : patient.MapHeld;

            if (patient.Destroyed
                || patientMap == null
                || patientMap.uniqueID != activeMapId)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    patient,
                    definition.GetRuntimeTextKey("failureLost"));
                return;
            }

            if (patient.IsPrisonerOfColony)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    patient,
                    definition.GetRuntimeTextKey("failureCaptured"));
                return;
            }

            if (operationDeadlineTick > 0
                && currentTick >= operationDeadlineTick)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    patient,
                    definition.GetRuntimeTextKey("failureTimeout"));
                return;
            }

            if (!woundedAgentInitialCareReceived)
            {
                TokraOrganicWoundedAgentUtility.EnsureSymbioteShock(patient);

                if (!TokraOrganicWoundedAgentUtility
                    .HasReceivedInitialCare(
                        patient,
                        woundedAgentInitialTendedConditionCount))
                {
                    woundedAgentStableSinceTick = 0;
                    return;
                }

                woundedAgentInitialCareReceived = true;
                TokraOrganicWoundedAgentUtility
                    .BeginPostShockRecovery(patient);
                SetWoundedAgentFrameworkPhase("recovering");

                Messages.Message(
                    definition.GetRuntimeTextKey("initialCareMessage").Translate(
                        patient.LabelShortCap),
                    patient,
                    MessageTypeDefOf.PositiveEvent,
                    historical: true);

                GR_Log.Message(
                    "Initial colony treatment received by wounded Tok'ra "
                    + $"agent {patient.LabelShortCap}; symbiote shock lifted "
                    + "and weakened recovery applied.");
            }
            else
            {
                TokraOrganicWoundedAgentUtility.RemoveSymbioteShock(patient);
                TokraOrganicWoundedAgentUtility
                    .EnsurePostShockRecovery(patient);
                SetWoundedAgentFrameworkPhase("recovering");
            }

            if (!TokraOrganicWoundedAgentUtility.IsFitForDeparture(patient))
            {
                woundedAgentStableSinceTick = 0;
                return;
            }

            if (woundedAgentStableSinceTick <= 0)
            {
                woundedAgentStableSinceTick = currentTick;
                Messages.Message(
                    definition.GetRuntimeTextKey("stableMessage").Translate(
                        patient.LabelShortCap),
                    patient,
                    MessageTypeDefOf.PositiveEvent,
                    historical: true);
                return;
            }

            if (currentTick - woundedAgentStableSinceTick
                < profile.stableDurationTicks)
            {
                return;
            }

            if (!TokraOrganicWoundedAgentUtility.TryOrderDeparture(patient))
            {
                return;
            }

            woundedAgentDepartureOrdered = true;
            woundedAgentDepartureDeadlineTick
                = currentTick + profile.departureGraceTicks;
            activeState = TokraOrganicOperationState.Ready;
            readyNotificationSent = true;

            Messages.Message(
                definition.GetRuntimeTextKey("departingMessage").Translate(
                    patient.LabelShortCap),
                patient,
                MessageTypeDefOf.PositiveEvent,
                historical: true);

            GR_Log.Message(
                "Ordered recovered Tok'ra agent "
                + $"{patient.LabelShortCap} to leave map {activeMapId}; "
                + $"departure grace ends at tick "
                + $"{woundedAgentDepartureDeadlineTick}.");
        }

        internal void TickAcceptedObservation(int currentTick)
        {
            if (!TryRepairLegacyObservationState(currentTick)
                || !HasValidObservationDevice())
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    null,
                    GetObservationTextKey("failureDeviceLost"));
                return;
            }

            if (operationDeadlineTick > 0
                && currentTick >= operationDeadlineTick)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    null,
                    GetObservationTextKey("failureTimeout"));
            }
        }

        internal void UpdateObservationReadyState(
            int currentTick,
            bool notifyPlayer)
        {
            if (activeArchetype
                    != TokraOrganicOperationArchetype.GoauldObservation
                || activeState != TokraOrganicOperationState.Accepted
                || !observationDeviceDeployed
                || observationWorkTotalTicks <= 0
                || observationWorkRemainingTicks > 0)
            {
                return;
            }

            activeState = TokraOrganicOperationState.Ready;
            observationReadyTick = currentTick;
            reportReadyTick = currentTick;

            if (readyNotificationSent)
            {
                return;
            }

            readyNotificationSent = true;

            if (notifyPlayer)
            {
                Messages.Message(
                    GetObservationTextKey("dataReady").Translate(),
                    activeDeadDrop,
                    MessageTypeDefOf.PositiveEvent,
                    historical: true);
            }
        }

        internal void TickAcceptedPhysicalObjective(int currentTick)
        {
            if (!HasValidIntelligenceObjective())
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    null,
                    GetIntelligenceTextKey("failureObjectiveLost"));
                return;
            }

            if (operationDeadlineTick > 0
                && currentTick >= operationDeadlineTick)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    null,
                    GetIntelligenceTextKey("failureTimeout"));
            }
        }

        private bool TryCreateNextOpportunity(int currentTick)
        {
            Map map = FindEligibleMap();

            if (map == null)
            {
                return false;
            }

            TokraOrganicOperationArchetype archetype;

            if (!TrySelectArchetype(
                    GameComponent_TokraTrustTracker.GetCurrentTier(),
                    map,
                    out archetype))
            {
                return false;
            }

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

            GateRimMissionWorker missionWorker
                = definition.MissionDef?.Worker;

            if (missionWorker != null && !missionWorker.CanOffer(map))
            {
                return false;
            }

            ThingWithComps communicator;

            if (!TokraSecureCommunicatorAvailabilityUtility
                    .TryFindAvailableCommunicator(map, out communicator))
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
            observationPointMarker = null;
            observationTargetCell = IntVec3.Invalid;
            observationDeviceDeployed = false;
            observationReadyTick = 0;
            observationWorkTotalTicks = 0;
            observationWorkRemainingTicks = 0;
            observationTransmissionTotalTicks = 0;
            observationTransmissionRemainingTicks = 0;
            observationResultVariant = -1;
            intelligenceAnalysisMethod
                = TokraIntelligenceAnalysisMethod.None;
            intelligenceWorkTotalTicks = 0;
            intelligenceWorkRemainingTicks = 0;
            intelligenceInterferenceRollResolved = false;
            intelligenceInterferenceTriggered = false;
            intelligencePatrolQueued = false;
            intelligenceResultVariant = -1;
            activeWoundedAgent = null;
            woundedAgentInitialCareReceived = false;
            woundedAgentInitialTendedConditionCount = 0;
            woundedAgentStableSinceTick = 0;
            woundedAgentDepartureOrdered = false;
            woundedAgentDepartureDeadlineTick = 0;
            activeMedicalSupplyLiaison = null;
            medicalSupplyMeetingCell = IntVec3.Invalid;
            medicalSupplyArrivalTick = 0;
            medicalSupplyArrivalNotified = false;
            medicalSupplyDepartureOrdered = false;
            lastOfferedArchetype = archetype;
            nextOpportunityTick = 0;

            InitializeFrameworkRuntime(definition, map);
            missionWorker?.OnOffered(
                activeOperation.frameworkRuntime,
                map);
            int offerTextVariantIndex;
            string offerLetterTextKey = SelectOfferLetterTextKey(
                definition,
                out offerTextVariantIndex);
            activeOperation.frameworkRuntime.SetTextVariant(
                GateRimMissionFramework.OfferTextBankKey,
                offerTextVariantIndex);

            Find.LetterStack?.ReceiveLetter(
                definition.OfferLetterLabelKey.Translate(),
                GetOfferLetterText(definition, offerLetterTextKey),
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
            TokraOrganicOperationWorker worker
                = TokraOrganicOperationWorkerRegistry.Get(activeArchetype);

            if (worker == null)
            {
                return false;
            }

            return activeState == TokraOrganicOperationState.Offered
                ? worker.TryAccept(this, map, operatorPawn)
                : worker.TryHandleCommunicatorCompletion(this, operatorPawn);
        }

        internal bool TryStartObservationTransmission(Pawn operatorPawn)
        {
            ThingWithComps communicator = FindPoweredCommunicator(
                GetActiveMap());

            if (communicator == null)
            {
                return false;
            }

            return TryStartObservationTransmissionInternal(
                communicator,
                operatorPawn);
        }

        private string GetObservationDeploymentDisabledReasonInternal(
            Thing device,
            Pawn operatorPawn)
        {
            if (activeArchetype
                    != TokraOrganicOperationArchetype.GoauldObservation
                || activeState != TokraOrganicOperationState.Accepted
                || activeDeadDrop != device)
            {
                return GetObservationTextKey("noActiveDeployment")
                    .Translate()
                    .ToString();
            }

            if (observationDeviceDeployed)
            {
                return GetObservationTextKey("alreadyDeployed")
                    .Translate()
                    .ToString();
            }

            if (IsOperationDeadlineExpired())
            {
                return GetObservationTextKey("expired")
                    .Translate()
                    .ToString();
            }

            if (!CanUseObservationOperator(operatorPawn))
            {
                return GetObservationTextKey("operatorIncapable")
                    .Translate()
                    .ToString();
            }

            Thing observationPoint = observationPointMarker;
            ThingWithComps communicator = FindPoweredCommunicator(
                GetActiveMap());

            if (!observationTargetCell.IsValid
                || device == null
                || device.Destroyed
                || (!device.Spawned
                    && operatorPawn?.carryTracker?.CarriedThing != device)
                || observationPoint == null
                || observationPoint.Destroyed
                || !observationPoint.Spawned)
            {
                return GetObservationTextKey("deviceLost")
                    .Translate()
                    .ToString();
            }

            if (communicator == null)
            {
                return GetObservationTextKey("communicatorUnpowered")
                    .Translate()
                    .ToString();
            }

            if (device.Spawned
                && !operatorPawn.CanReserveAndReach(
                    device,
                    PathEndMode.ClosestTouch,
                    Danger.Some))
            {
                return GetObservationTextKey("cannotReachDevice")
                    .Translate()
                    .ToString();
            }

            if (!operatorPawn.CanReach(
                    observationPoint,
                    PathEndMode.Touch,
                    Danger.Some))
            {
                return GetObservationTextKey("cannotReachPoint")
                    .Translate()
                    .ToString();
            }

            if (!operatorPawn.CanReach(
                    communicator,
                    PathEndMode.Touch,
                    Danger.Some))
            {
                return GetObservationTextKey("cannotReachCommunicator")
                    .Translate()
                    .ToString();
            }

            return null;
        }

        private bool CanContinueObservationDeploymentInternal(
            Thing device,
            Pawn operatorPawn)
        {
            return string.IsNullOrEmpty(
                GetObservationDeploymentDisabledReasonInternal(
                    device,
                    operatorPawn));
        }

        private bool NotifyObservationDeviceDeployedInternal(
            Thing device,
            Pawn operatorPawn)
        {
            Thing observationPoint = observationPointMarker;

            if (!CanUseObservationOperator(operatorPawn)
                || activeArchetype
                    != TokraOrganicOperationArchetype.GoauldObservation
                || activeState != TokraOrganicOperationState.Accepted
                || activeDeadDrop != device
                || device == null
                || device.Destroyed
                || operatorPawn?.carryTracker?.CarriedThing != device
                || observationPoint == null
                || observationPoint.Destroyed
                || !observationPoint.Spawned
                || !observationTargetCell.IsValid
                || observationPoint.Map?.uniqueID != activeMapId
                || observationPoint.Position
                    .DistanceToSquared(observationTargetCell) > 2)
            {
                return false;
            }

            device.Destroy(DestroyMode.Vanish);
            activeDeadDrop = observationPoint;

            observationDeviceDeployed = true;
            observationReadyTick = 0;
            reportReadyTick = 0;
            readyNotificationSent = false;
            observationWorkTotalTicks = GetConfiguredObservationWorkTicks();
            observationWorkRemainingTicks = observationWorkTotalTicks;
            observationTransmissionTotalTicks = 0;
            observationTransmissionRemainingTicks = 0;

            Messages.Message(
                GetObservationTextKey("deployed").Translate(
                    operatorPawn.LabelShortCap,
                    GetRoundedUpHours(observationWorkTotalTicks).ToString()),
                observationPoint,
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            GR_Log.Message(
                "Installed Tok'ra observation device at "
                + $"{observationPoint.Position}; observation work "
                + $"{observationWorkTotalTicks} ticks; operator "
                + $"{operatorPawn.LabelShortCap}.");
            return true;
        }

        private bool PerformObservationWorkInternal(
            Thing observationPoint,
            Pawn operatorPawn)
        {
            if (IsObservationWorkCompleteInternal(observationPoint))
            {
                return true;
            }

            if (activeArchetype
                    != TokraOrganicOperationArchetype.GoauldObservation
                || activeState != TokraOrganicOperationState.Accepted
                || !observationDeviceDeployed
                || activeDeadDrop != observationPoint
                || !TokraObservationUtility.IsObservationPoint(
                    observationPoint)
                || observationPoint == null
                || observationPoint.Destroyed
                || !observationPoint.Spawned
                || IsOperationDeadlineExpired()
                || !CanUseObservationOperator(operatorPawn))
            {
                return false;
            }

            if (observationWorkTotalTicks <= 0)
            {
                observationWorkTotalTicks
                    = GetConfiguredObservationWorkTicks();
                observationWorkRemainingTicks = observationWorkTotalTicks;
            }

            if (observationWorkRemainingTicks > 0)
            {
                observationWorkRemainingTicks--;
                SkillDef workSkill = GetObservationSkillDef();
                float xpPerTick
                    = GetActiveDefinition()?.ObservationXpPerTick ?? 0f;

                if (workSkill != null && xpPerTick > 0f)
                {
                    operatorPawn.skills?.Learn(workSkill, xpPerTick);
                }
            }

            if (observationWorkRemainingTicks > 0)
            {
                return true;
            }

            activeState = TokraOrganicOperationState.Ready;
            observationReadyTick = Find.TickManager?.TicksGame ?? 0;
            reportReadyTick = observationReadyTick;
            readyNotificationSent = true;

            Messages.Message(
                GetObservationTextKey("dataReady").Translate(),
                observationPoint,
                MessageTypeDefOf.PositiveEvent,
                historical: true);

            GR_Log.Message(
                "Completed Tok'ra field observation at "
                + $"{observationPoint.Position}; operator "
                + $"{operatorPawn.LabelShortCap}.");
            return true;
        }

        private bool IsObservationWorkCompleteInternal(
            Thing observationPoint)
        {
            return activeArchetype
                    == TokraOrganicOperationArchetype.GoauldObservation
                && activeState == TokraOrganicOperationState.Ready
                && observationDeviceDeployed
                && activeDeadDrop == observationPoint
                && TokraObservationUtility.IsObservationPoint(
                    observationPoint);
        }

        private float GetObservationWorkProgressInternal(
            Thing observationPoint)
        {
            if (activeArchetype
                    != TokraOrganicOperationArchetype.GoauldObservation
                || !observationDeviceDeployed
                || activeDeadDrop != observationPoint
                || observationWorkTotalTicks <= 0)
            {
                return 0f;
            }

            return 1f - Math.Max(
                0f,
                Math.Min(
                    1f,
                    observationWorkRemainingTicks
                        / (float)observationWorkTotalTicks));
        }

        private string GetObservationRecoveryDisabledReasonInternal(
            Thing observationPoint,
            Pawn operatorPawn)
        {
            if (activeArchetype
                    != TokraOrganicOperationArchetype.GoauldObservation
                || (activeState != TokraOrganicOperationState.Accepted
                    && activeState != TokraOrganicOperationState.Ready)
                || !observationDeviceDeployed
                || activeDeadDrop != observationPoint
                || !TokraObservationUtility
                    .IsObservationPoint(observationPoint))
            {
                return GetObservationTextKey("noReadyRecovery")
                    .Translate()
                    .ToString();
            }

            if (IsOperationDeadlineExpired())
            {
                return GetObservationTextKey("expired")
                    .Translate()
                    .ToString();
            }

            if (!CanUseObservationOperator(operatorPawn))
            {
                return GetObservationTextKey("operatorIncapable")
                    .Translate()
                    .ToString();
            }

            ThingWithComps communicator = FindPoweredCommunicator(
                GetActiveMap());

            if (communicator == null)
            {
                return GetObservationTextKey("communicatorUnpowered")
                    .Translate()
                    .ToString();
            }

            if (!operatorPawn.CanReserveAndReach(
                    observationPoint,
                    PathEndMode.Touch,
                    Danger.Some))
            {
                return GetObservationTextKey("cannotReachPoint")
                    .Translate()
                    .ToString();
            }

            if (!operatorPawn.CanReach(
                    communicator,
                    PathEndMode.Touch,
                    Danger.Some))
            {
                return GetObservationTextKey("cannotReachCommunicator")
                    .Translate()
                    .ToString();
            }

            return null;
        }

        private bool CanContinueObservationRecoveryInternal(
            Thing observationPoint,
            Pawn operatorPawn)
        {
            return activeArchetype
                    == TokraOrganicOperationArchetype.GoauldObservation
                && activeState == TokraOrganicOperationState.Ready
                && observationDeviceDeployed
                && activeDeadDrop == observationPoint
                && TokraObservationUtility.IsObservationPoint(
                    observationPoint)
                && observationPoint != null
                && !observationPoint.Destroyed
                && observationPoint.Spawned
                && !IsOperationDeadlineExpired()
                && CanUseObservationOperator(operatorPawn);
        }

        private bool TryStartObservationRecoveryInternal(
            Thing observationPoint,
            Pawn operatorPawn)
        {
            string disabledReason
                = GetObservationRecoveryDisabledReasonInternal(
                    observationPoint,
                    operatorPawn);

            if (!string.IsNullOrEmpty(disabledReason))
            {
                Messages.Message(
                    disabledReason,
                    observationPoint,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            ThingWithComps communicator = FindPoweredCommunicator(
                GetActiveMap());
            JobDef jobDef = GetObservationTransmissionJobDef();

            if (communicator == null || jobDef == null)
            {
                return false;
            }

            Job job = JobMaker.MakeJob(
                jobDef,
                communicator,
                observationPoint);
            job.count = 1;

            if (!operatorPawn.jobs.TryTakeOrderedJob(job))
            {
                return false;
            }

            string messageKey = activeState
                    == TokraOrganicOperationState.Ready
                ? GetObservationTextKey("recoveryStarted")
                : GetObservationTextKey("observationStarted");

            Messages.Message(
                messageKey.Translate(operatorPawn.LabelShortCap),
                observationPoint,
                MessageTypeDefOf.NeutralEvent,
                historical: false);
            return true;
        }

        private bool TryRecoverObservationDeviceInternal(
            Thing observationPoint,
            Pawn operatorPawn,
            out Thing recoveredDevice)
        {
            recoveredDevice = null;

            if (!CanContinueObservationRecoveryInternal(
                    observationPoint,
                    operatorPawn))
            {
                return false;
            }

            ThingDef deviceDef = DefDatabase<ThingDef>.GetNamedSilentFail(
                TokraObservationUtility.DeviceDefName);

            if (deviceDef == null)
            {
                return false;
            }

            Thing created = ThingMaker.MakeThing(deviceDef);
            Thing placed;

            if (!GenPlace.TryPlaceThing(
                    created,
                    operatorPawn.Position,
                    operatorPawn.Map,
                    ThingPlaceMode.Near,
                    out placed))
            {
                if (!created.Destroyed)
                {
                    created.Destroy(DestroyMode.Vanish);
                }

                return false;
            }

            activeDeadDrop = placed;
            observationPointMarker = null;

            if (!observationPoint.Destroyed)
            {
                observationPoint.Destroy(DestroyMode.Vanish);
            }

            recoveredDevice = placed;

            GR_Log.Message(
                "Recovered Tok'ra observation device at "
                + $"{operatorPawn.Position}; operator "
                + $"{operatorPawn.LabelShortCap}.");
            return true;
        }

        private bool TryStartObservationTransmissionInternal(
            Thing communicator,
            Pawn operatorPawn)
        {
            if (!CanContinueObservationTransmissionInternal(
                    communicator,
                    operatorPawn)
                || TokraObservationUtility.IsObservationPoint(activeDeadDrop))
            {
                return false;
            }

            JobDef jobDef = GetObservationTransmissionJobDef();

            if (jobDef == null)
            {
                return false;
            }

            if (observationTransmissionTotalTicks <= 0)
            {
                observationTransmissionTotalTicks
                    = ObservationTransmissionWorkTicks;
                observationTransmissionRemainingTicks
                    = ObservationTransmissionWorkTicks;
            }

            bool deviceAlreadyCarried
                = operatorPawn.carryTracker?.CarriedThing == activeDeadDrop;

            if (!deviceAlreadyCarried
                && (!activeDeadDrop.Spawned
                    || !operatorPawn.CanReserveAndReach(
                        activeDeadDrop,
                        PathEndMode.ClosestTouch,
                        Danger.Some)))
            {
                return false;
            }

            Job job = JobMaker.MakeJob(
                jobDef,
                communicator,
                activeDeadDrop);
            job.count = 1;

            if (!operatorPawn.jobs.TryTakeOrderedJob(job))
            {
                return false;
            }

            Messages.Message(
                GetObservationTextKey("transmissionStarted").Translate(
                    operatorPawn.LabelShortCap),
                communicator,
                MessageTypeDefOf.NeutralEvent,
                historical: false);
            return true;
        }

        private bool CanContinueObservationTransmissionInternal(
            Thing communicator,
            Pawn operatorPawn)
        {
            return activeArchetype
                    == TokraOrganicOperationArchetype.GoauldObservation
                && activeState == TokraOrganicOperationState.Ready
                && observationDeviceDeployed
                && HasValidObservationDevice()
                && !IsOperationDeadlineExpired()
                && IsPoweredSecureCommunicator(communicator)
                && communicator.Map?.uniqueID == activeMapId
                && CanUseObservationOperator(operatorPawn);
        }

        private bool PerformObservationTransmissionWorkInternal(
            Thing communicator,
            Pawn operatorPawn)
        {
            if (!CanContinueObservationTransmissionInternal(
                    communicator,
                    operatorPawn))
            {
                return false;
            }

            if (observationTransmissionTotalTicks <= 0)
            {
                observationTransmissionTotalTicks
                    = ObservationTransmissionWorkTicks;
                observationTransmissionRemainingTicks
                    = ObservationTransmissionWorkTicks;
            }

            if (observationTransmissionRemainingTicks > 0)
            {
                observationTransmissionRemainingTicks--;
            }

            return true;
        }

        private bool IsObservationTransmissionCompleteInternal(
            Thing communicator)
        {
            return IsPoweredSecureCommunicator(communicator)
                && observationTransmissionTotalTicks > 0
                && observationTransmissionRemainingTicks <= 0;
        }

        private float GetObservationTransmissionProgressInternal(
            Thing communicator)
        {
            if (!IsPoweredSecureCommunicator(communicator)
                || observationTransmissionTotalTicks <= 0)
            {
                return 0f;
            }

            return 1f - (float)observationTransmissionRemainingTicks
                / observationTransmissionTotalTicks;
        }

        private bool TryCompleteObservationTransmissionInternal(
            Thing communicator,
            Pawn operatorPawn)
        {
            if (!CanContinueObservationTransmissionInternal(
                    communicator,
                    operatorPawn)
                || observationTransmissionRemainingTicks > 0)
            {
                return false;
            }

            return TryResolveActiveOperation(
                TokraOrganicOperationOutcome.Succeeded,
                operatorPawn,
                null);
        }

        internal bool TryOpenIntelligenceAnalysis(Pawn operatorPawn)
        {
            if (!CanUseIntellectualOperator(operatorPawn)
                || activeArchetype
                    != TokraOrganicOperationArchetype.DeadDropRecovery
                || activeState != TokraOrganicOperationState.Accepted
                || !HasValidIntelligenceObjective()
                || IsOperationDeadlineExpired())
            {
                return false;
            }

            ThingWithComps communicator = FindPoweredCommunicator(
                GetActiveMap());

            if (communicator == null)
            {
                return false;
            }

            if (intelligenceAnalysisMethod
                == TokraIntelligenceAnalysisMethod.None)
            {
                Find.WindowStack.Add(
                    new Dialog_TokraIntelligenceAnalysis(
                        communicator,
                        operatorPawn));
                return true;
            }

            return TryStartIntelligenceAnalysisJob(
                communicator,
                operatorPawn);
        }

        private bool TryStartIntelligenceAnalysisInternal(
            Thing communicator,
            Pawn operatorPawn,
            TokraIntelligenceAnalysisMethod method)
        {
            if (method == TokraIntelligenceAnalysisMethod.None
                || !CanUseIntellectualOperator(operatorPawn)
                || activeArchetype
                    != TokraOrganicOperationArchetype.DeadDropRecovery
                || activeState != TokraOrganicOperationState.Accepted
                || !HasValidIntelligenceObjective()
                || IsOperationDeadlineExpired()
                || !IsPoweredSecureCommunicator(communicator))
            {
                return false;
            }

            if (intelligenceAnalysisMethod
                == TokraIntelligenceAnalysisMethod.None)
            {
                intelligenceAnalysisMethod = method;
                SetIntelligenceFrameworkPhase(method);
                intelligenceWorkTotalTicks
                    = GetConfiguredIntelligenceWorkTicks(method);
                intelligenceWorkRemainingTicks
                    = intelligenceWorkTotalTicks;
                intelligenceInterferenceRollResolved = false;
                intelligenceInterferenceTriggered = false;
                intelligencePatrolQueued = false;
                intelligenceResultVariant = -1;

                Messages.Message(
                    GetIntelligenceTextKey(
                        method == TokraIntelligenceAnalysisMethod.Cautious
                            ? "cautiousStarted"
                            : "acceleratedStarted")
                        .Translate(operatorPawn.LabelShortCap),
                    communicator,
                    MessageTypeDefOf.NeutralEvent,
                    historical: true);
            }
            else if (intelligenceAnalysisMethod != method)
            {
                Messages.Message(
                    GetIntelligenceTextKey("methodLocked").Translate(),
                    communicator,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            return TryStartIntelligenceAnalysisJob(
                communicator,
                operatorPawn);
        }

        private bool TryStartIntelligenceAnalysisJob(
            Thing communicator,
            Pawn operatorPawn)
        {
            JobDef jobDef = GetIntelligenceAnalysisJobDef();
            Thing intelligenceModule = activeDeadDrop;

            bool moduleAlreadyCarried
                = IsIntelligenceObjectiveCarriedBy(operatorPawn);

            if (jobDef == null
                || operatorPawn?.jobs == null
                || intelligenceModule == null
                || intelligenceModule.Destroyed
                || (!moduleAlreadyCarried
                    && (!intelligenceModule.Spawned
                        || intelligenceModule.Map?.uniqueID != activeMapId
                        || !operatorPawn.CanReach(
                            intelligenceModule,
                            PathEndMode.ClosestTouch,
                            Danger.Some)
                        || !operatorPawn.CanReserve(intelligenceModule)))
                || !operatorPawn.CanReach(
                    communicator,
                    PathEndMode.Touch,
                    Danger.Some)
                || !operatorPawn.CanReserve(communicator))
            {
                Messages.Message(
                    GetIntelligenceTextKey("jobUnavailable").Translate(),
                    communicator,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            Job job = JobMaker.MakeJob(
                jobDef,
                communicator,
                intelligenceModule);
            job.count = 1;
            operatorPawn.jobs.TryTakeOrderedJob(job);
            return true;
        }

        private bool CanContinueIntelligenceAnalysisInternal(
            Thing communicator,
            Pawn operatorPawn)
        {
            return CanUseIntellectualOperator(operatorPawn)
                && activeArchetype
                    == TokraOrganicOperationArchetype.DeadDropRecovery
                && (activeState == TokraOrganicOperationState.Accepted
                    || activeState == TokraOrganicOperationState.Ready)
                && intelligenceAnalysisMethod
                    != TokraIntelligenceAnalysisMethod.None
                && HasValidIntelligenceObjective()
                && !IsOperationDeadlineExpired()
                && IsPoweredSecureCommunicator(communicator)
                && communicator.Map?.uniqueID == activeMapId;
        }

        private bool PerformIntelligenceAnalysisWorkInternal(
            Thing communicator,
            Pawn operatorPawn)
        {
            if (!CanContinueIntelligenceAnalysisInternal(
                    communicator,
                    operatorPawn)
                || !IsIntelligenceObjectiveCarriedBy(operatorPawn))
            {
                return false;
            }

            if (intelligenceWorkRemainingTicks > 0)
            {
                intelligenceWorkRemainingTicks--;
            }

            if (intelligenceWorkRemainingTicks <= 0)
            {
                intelligenceWorkRemainingTicks = 0;
                activeState = TokraOrganicOperationState.Ready;
            }

            return true;
        }

        private bool IsIntelligenceAnalysisCompleteInternal(Thing communicator)
        {
            return IsPoweredSecureCommunicator(communicator)
                && activeArchetype
                    == TokraOrganicOperationArchetype.DeadDropRecovery
                && intelligenceAnalysisMethod
                    != TokraIntelligenceAnalysisMethod.None
                && intelligenceWorkRemainingTicks <= 0;
        }

        private float GetIntelligenceAnalysisProgressInternal(Thing communicator)
        {
            if (!IsPoweredSecureCommunicator(communicator)
                || intelligenceWorkTotalTicks <= 0)
            {
                return 0f;
            }

            return 1f - Math.Min(
                1f,
                Math.Max(
                    0f,
                    intelligenceWorkRemainingTicks
                        / (float)intelligenceWorkTotalTicks));
        }

        private bool TryCompleteIntelligenceAnalysisInternal(
            Thing communicator,
            Pawn operatorPawn)
        {
            if (!CanContinueIntelligenceAnalysisInternal(
                    communicator,
                    operatorPawn)
                || intelligenceWorkRemainingTicks > 0)
            {
                return false;
            }

            activeState = TokraOrganicOperationState.Ready;

            return TryResolveActiveOperation(
                TokraOrganicOperationOutcome.Succeeded,
                operatorPawn,
                null);
        }

        internal bool TryAcceptDistressCall(
            Map map,
            Pawn operatorPawn)
        {
            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();
            GateRimMissionRuntimeData runtime
                = activeOperation.frameworkRuntime;
            WorldObject_TokraDistressCallSite site;

            if (definition == null
                || definition.DistressCall == null
                || map == null
                || runtime == null)
            {
                return false;
            }

            if (!TokraDistressCallMissionUtility.TryCreateWorldSite(
                    map,
                    definition,
                    runtime,
                    out site))
            {
                Messages.Message(
                    definition.GetRuntimeTextKey("spawnFailed").Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            activeState = TokraOrganicOperationState.Accepted;
            acceptedTick = currentTick;
            reportReadyTick = currentTick;
            operationDeadlineTick = 0;
            readyNotificationSent = false;
            resolutionApplied = false;
            activeDeadDrop = null;

            NotifyMissionAccepted(map, operatorPawn);

            Messages.Message(
                definition.AcceptedMessageKey.Translate(
                    operatorPawn?.LabelShortCap ?? "?",
                    GetRoundedUpHours(definition.DeadlineTicks).ToString()),
                site,
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            Find.LetterStack?.ReceiveLetter(
                definition.GetRuntimeTextKey("targetLetterLabel").Translate(),
                definition.GetRuntimeTextKey("targetLetterText").Translate(
                    GetRoundedUpHours(definition.DeadlineTicks).ToString()),
                LetterDefOf.NeutralEvent,
                site);

            GR_Log.Message(
                "Accepted Tok'ra distress-call operation on map "
                + $"{map.uniqueID}; world site {site.ID} at tile "
                + $"{site.Tile}; deadline {operationDeadlineTick}; "
                + $"operator {operatorPawn?.LabelShortCap ?? "unknown"}.");

            return true;
        }


        internal bool TryAcceptJaffaOfficerCapture(
            Map map,
            Pawn operatorPawn)
        {
            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();
            GateRimMissionRuntimeData runtime
                = activeOperation.frameworkRuntime;
            WorldObject_TokraJaffaOfficerCaptureSite site;

            if (definition == null
                || definition.Capture == null
                || map == null
                || runtime == null)
            {
                return false;
            }

            if (!TokraJaffaOfficerCaptureMissionUtility.TryCreateWorldSite(
                    map,
                    definition,
                    runtime,
                    out site))
            {
                Messages.Message(
                    definition.GetRuntimeTextKey("spawnFailed").Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            if (!TokraJaffaOfficerCaptureMissionUtility.TryIssueCaptureTool(
                    map,
                    operatorPawn,
                    definition.Capture,
                    runtime))
            {
                site.NotifyManagerResolved(false);
                Messages.Message(
                    definition.GetRuntimeTextKey("spawnFailed").Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            EnsureJaffaOfficerCaptureState();
            jaffaOfficerCapture.Reset();
            activeState = TokraOrganicOperationState.Accepted;
            acceptedTick = currentTick;
            reportReadyTick = 0;
            operationDeadlineTick = currentTick
                + Math.Max(1, definition.DeadlineTicks);
            readyNotificationSent = false;
            resolutionApplied = false;
            activeDeadDrop = null;

            NotifyMissionAccepted(map, operatorPawn);

            string remainingHours = GetRoundedUpHours(
                definition.DeadlineTicks).ToString();

            Messages.Message(
                definition.AcceptedMessageKey.Translate(
                    operatorPawn?.LabelShortCap ?? "?",
                    remainingHours),
                site,
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            Find.LetterStack?.ReceiveLetter(
                definition.GetRuntimeTextKey("targetLetterLabel").Translate(),
                definition.GetRuntimeTextKey("targetLetterText").Translate(
                    remainingHours),
                LetterDefOf.NeutralEvent,
                site);

            GR_Log.Message(
                "Accepted Tok'ra Jaffa-officer capture operation on map "
                + $"{map.uniqueID}; world site {site.ID} at tile "
                + $"{site.Tile}; operator "
                + $"{operatorPawn?.LabelShortCap ?? "unknown"}.");

            return true;
        }


        internal bool TryRequestJaffaOfficerExtraction(Pawn operatorPawn)
        {
            if (activeArchetype
                    != TokraOrganicOperationArchetype.JaffaOfficerCapture
                || (activeState != TokraOrganicOperationState.Accepted
                    && activeState != TokraOrganicOperationState.Ready))
            {
                return false;
            }

            SyncJaffaOfficerCaptureStateFromWorldSite();
            Map map = operatorPawn?.Map ?? GetActiveMap();

            return TokraJaffaOfficerCaptureTransferController
                .TryRequestHomeExtraction(
                    jaffaOfficerCapture,
                    map,
                    operatorPawn);
        }


        internal bool TryAcceptTemporaryBaseDelivery(
            Map map,
            Pawn operatorPawn)
        {
            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();
            GateRimMissionRuntimeData runtime
                = activeOperation.frameworkRuntime;
            WorldObject_TokraTemporaryBaseDeliverySite site;

            if (definition == null
                || definition.Delivery == null
                || map == null
                || runtime == null
                || !TokraTemporaryBaseDeliveryMissionUtility
                    .HasStoredContract(runtime))
            {
                return false;
            }

            if (!TokraTemporaryBaseDeliveryMissionUtility.TryCreateWorldSite(
                    map,
                    definition,
                    runtime,
                    out site))
            {
                Messages.Message(
                    definition.GetRuntimeTextKey("spawnFailed").Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            activeState = TokraOrganicOperationState.Accepted;
            acceptedTick = currentTick;
            reportReadyTick = currentTick;
            operationDeadlineTick = 0;
            readyNotificationSent = true;
            resolutionApplied = false;
            activeDeadDrop = null;

            NotifyMissionAccepted(map, operatorPawn);

            int requiredCount = TokraTemporaryBaseDeliveryMissionUtility
                .GetRequiredCount(runtime);
            string itemLabel = TokraTemporaryBaseDeliveryMissionUtility
                .GetContractLabel(runtime);
            string qualityLabel = TokraTemporaryBaseDeliveryMissionUtility
                .GetQualityLabel(runtime);
            string remainingHours = GetRoundedUpHours(
                definition.DeadlineTicks).ToString();

            Messages.Message(
                definition.AcceptedMessageKey.Translate(
                    operatorPawn?.LabelShortCap ?? "?",
                    requiredCount.ToString(),
                    itemLabel,
                    qualityLabel,
                    remainingHours),
                site,
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            Find.LetterStack?.ReceiveLetter(
                definition.GetRuntimeTextKey("targetLetterLabel").Translate(),
                definition.GetRuntimeTextKey("targetLetterText").Translate(
                    requiredCount.ToString(),
                    itemLabel,
                    qualityLabel,
                    remainingHours),
                LetterDefOf.NeutralEvent,
                site);

            GR_Log.Message(
                "Accepted Tok'ra temporary-base delivery operation on map "
                + $"{map.uniqueID}; world site {site.ID} at tile "
                + $"{site.Tile}; contract {requiredCount}x {itemLabel}; "
                + $"deadline {operationDeadlineTick}; operator "
                + $"{operatorPawn?.LabelShortCap ?? "unknown"}.");

            return true;
        }

        internal bool TryAcceptMedicalSupplyHandoff(
            Map map,
            Pawn operatorPawn)
        {
            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();

            if (definition == null || map == null)
            {
                return false;
            }

            GateRimMissionHandoffDef handoff = definition.Handoff;

            if (handoff == null)
            {
                return false;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            int arrivalDelay = Rand.RangeInclusive(
                handoff.arrivalMinimumDelayTicks,
                handoff.arrivalMaximumDelayTicks);

            activeState = TokraOrganicOperationState.Accepted;
            acceptedTick = currentTick;
            medicalSupplyArrivalTick = currentTick + arrivalDelay;
            reportReadyTick = medicalSupplyArrivalTick;
            operationDeadlineTick = 0;
            readyNotificationSent = false;
            resolutionApplied = false;
            activeDeadDrop = null;
            activeMedicalSupplyLiaison = null;
            medicalSupplyMeetingCell = IntVec3.Invalid;
            medicalSupplyArrivalNotified = false;
            medicalSupplyDepartureOrdered = false;
            nextStateCheckTick
                = currentTick + MedicalSupplyStateCheckIntervalTicks;

            NotifyMissionAccepted(map, operatorPawn);

            Messages.Message(
                definition.AcceptedMessageKey.Translate(
                    operatorPawn?.LabelShortCap ?? "?",
                    GetRoundedUpHours(arrivalDelay).ToString()),
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            GR_Log.Message(
                "Accepted Tok'ra organic operation "
                + $"{definition.DebugLabel} on map {map.uniqueID}; "
                + $"liaison arrival scheduled at tick "
                + $"{medicalSupplyArrivalTick}; deadline "
                + $"{operationDeadlineTick}; operator "
                + $"{operatorPawn?.LabelShortCap ?? "unknown"}.");

            return true;
        }

        internal bool TryAcceptWoundedAgentCare(
            Map map,
            Pawn operatorPawn)
        {
            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();
            Pawn patient;

            if (definition == null || definition.PawnCare == null)
            {
                return false;
            }

            if (!TokraOrganicWoundedAgentUtility.TrySpawnPatient(
                    map,
                    definition,
                    activeOperation.frameworkRuntime?.scaledThreatPoints ?? 0f,
                    definition.DeadlineTicks
                        + definition.PawnCare.departureGraceTicks,
                    out patient))
            {
                Messages.Message(
                    definition.GetRuntimeTextKey("spawnFailed").Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            activeState = TokraOrganicOperationState.Accepted;
            acceptedTick = currentTick;
            reportReadyTick = currentTick;
            operationDeadlineTick = currentTick + definition.DeadlineTicks;
            readyNotificationSent = false;
            resolutionApplied = false;
            activeDeadDrop = null;
            activeWoundedAgent = patient;
            woundedAgentInitialCareReceived = false;
            woundedAgentInitialTendedConditionCount
                = TokraOrganicWoundedAgentUtility
                    .CountTendedConditions(patient);
            woundedAgentStableSinceTick = 0;
            woundedAgentDepartureOrdered = false;
            woundedAgentDepartureDeadlineTick = 0;

            NotifyMissionAccepted(map, operatorPawn);

            Messages.Message(
                definition.AcceptedMessageKey.Translate(
                    operatorPawn?.LabelShortCap ?? "?",
                    patient.LabelShortCap),
                patient,
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            Find.LetterStack?.ReceiveLetter(
                definition.GetRuntimeTextKey("arrivalLabel").Translate(),
                definition.GetRuntimeTextKey("arrivalText").Translate(
                    patient.LabelShortCap,
                    GetRoundedUpHours(definition.DeadlineTicks).ToString()),
                LetterDefOf.NeutralEvent,
                patient);

            GR_Log.Message(
                "Accepted Tok'ra organic operation "
                + $"{definition.DebugLabel} on map {map.uniqueID}; "
                + $"patient {patient.LabelShortCap}; deadline "
                + $"{operationDeadlineTick}; operator "
                + $"{operatorPawn?.LabelShortCap ?? "unknown"}.");

            return true;
        }

        internal bool TryAcceptObservationOperation(Map map, Pawn operatorPawn)
        {
            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();
            Thing device;
            Thing marker;
            IntVec3 targetCell;

            if (definition == null)
            {
                return false;
            }

            if (!CanUseObservationOperator(operatorPawn)
                || !TokraObservationUtility.TryCreateOperationTargets(
                    map,
                    out device,
                    out marker,
                    out targetCell))
            {
                Messages.Message(
                    definition.GetRuntimeTextKey("spawnFailed").Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            activeState = TokraOrganicOperationState.Accepted;
            acceptedTick = currentTick;
            reportReadyTick = 0;
            operationDeadlineTick = currentTick + definition.DeadlineTicks;
            readyNotificationSent = false;
            resolutionApplied = false;
            activeDeadDrop = device;
            observationPointMarker = marker;
            observationTargetCell = targetCell;
            observationDeviceDeployed = false;
            observationReadyTick = 0;
            observationWorkTotalTicks = 0;
            observationWorkRemainingTicks = 0;
            observationTransmissionTotalTicks = 0;
            observationTransmissionRemainingTicks = 0;
            observationResultVariant = -1;

            NotifyMissionAccepted(map, operatorPawn);

            Messages.Message(
                definition.AcceptedMessageKey.Translate(
                    operatorPawn?.LabelShortCap ?? "?",
                    GetRoundedUpHours(definition.DeadlineTicks).ToString()),
                marker,
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            Find.LetterStack?.ReceiveLetter(
                definition.GetRuntimeTextKey("targetLetterLabel").Translate(),
                definition.GetRuntimeTextKey("targetLetterText").Translate(
                    GetRoundedUpHours(definition.DeadlineTicks).ToString()),
                LetterDefOf.NeutralEvent,
                marker);

            GR_Log.Message(
                "Accepted Tok'ra observation operation on map "
                + $"{map.uniqueID}; device at {device.Position}; "
                + $"target {targetCell}; deadline "
                + $"{operationDeadlineTick}; operator "
                + $"{operatorPawn?.LabelShortCap ?? "unknown"}.");

            return true;
        }


        internal bool TryAcceptDiversionAssault(
            Map map,
            Pawn operatorPawn)
        {
            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();

            if (definition == null
                || definition.Archetype
                    != TokraOrganicOperationArchetype
                        .DecoyTransmissionDefense)
            {
                Messages.Message(
                    "GR_TokraOrganicOperation_FloatMenuUnavailable"
                        .Translate(),
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
            activeDeadDrop = null;
            TokraDiversionAssaultUtility.Initialize(
                activeOperation.frameworkRuntime,
                definition,
                currentTick);

            NotifyMissionAccepted(map, operatorPawn);

            Thing letterTarget = operatorPawn
                ?? FindPoweredCommunicator(map);

            Messages.Message(
                definition.AcceptedMessageKey.Translate(
                    operatorPawn?.LabelShortCap ?? "?"),
                letterTarget,
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            GR_Log.Message(
                "Accepted Tok'ra diversion assault operation on map "
                + map.uniqueID + "; raid due "
                + TokraDiversionAssaultUtility.GetRaidDueTick(
                    activeOperation.frameworkRuntime)
                + "; operator "
                + (operatorPawn?.LabelShortCap ?? "unknown") + ".");

            return true;
        }

        internal bool TryAcceptPhysicalObjectiveOperation(
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
                    definition.GetRuntimeTextKey("spawnFailed").Translate(),
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
            intelligenceAnalysisMethod
                = TokraIntelligenceAnalysisMethod.None;
            intelligenceWorkTotalTicks = 0;
            intelligenceWorkRemainingTicks = 0;
            intelligenceInterferenceRollResolved = false;
            intelligenceInterferenceTriggered = false;
            intelligencePatrolQueued = false;
            intelligenceResultVariant = -1;

            NotifyMissionAccepted(map, operatorPawn);

            string acceptedKey = definition.AcceptedMessageKey;
            string locatedLabelKey
                = definition.GetRuntimeTextKey("targetLetterLabel");
            string locatedTextKey
                = definition.GetRuntimeTextKey("targetLetterText");
            string remainingHours = GetRoundedUpHours(
                definition.DeadlineTicks).ToString();

            Messages.Message(
                acceptedKey.Translate(
                    operatorPawn?.LabelShortCap ?? "?",
                    remainingHours),
                objective,
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            Find.LetterStack?.ReceiveLetter(
                locatedLabelKey.Translate(),
                locatedTextKey.Translate(remainingHours),
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


        private void NotifyMissionAccepted(Map map, Pawn operatorPawn)
        {
            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();
            GateRimMissionWorker missionWorker
                = definition?.MissionDef?.Worker;

            missionWorker?.OnAccepted(
                activeOperation.frameworkRuntime,
                map,
                operatorPawn);
        }

        private void NotifyMissionResolved(
            TokraOrganicOperationDefinition definition,
            Map map,
            Pawn operatorPawn,
            TokraOrganicOperationOutcome outcome)
        {
            GateRimMissionOutcome frameworkOutcome
                = outcome == TokraOrganicOperationOutcome.Succeeded
                    ? GateRimMissionOutcome.Succeeded
                    : GateRimMissionOutcome.Failed;

            if (activeOperation.frameworkRuntime != null
                && !string.IsNullOrEmpty(
                    activeOperation.frameworkRuntime.missionDefName))
            {
                activeOperation.frameworkRuntime.phaseId
                    = frameworkOutcome == GateRimMissionOutcome.Succeeded
                        ? "succeeded"
                        : "failed";
            }

            GateRimMissionWorker missionWorker
                = definition?.MissionDef?.Worker;

            missionWorker?.OnResolved(
                activeOperation.frameworkRuntime,
                map,
                operatorPawn,
                frameworkOutcome);
        }

        internal bool TryResolveActiveOperation(
            TokraOrganicOperationOutcome outcome,
            Pawn operatorPawn,
            string failureTextKey,
            bool bypassSuccessValidation = false,
            int? trustChangeOverride = null,
            string trustMessageKeyOverride = null)
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
            bool isObservation = definition.Archetype
                == TokraOrganicOperationArchetype.GoauldObservation;
            bool isWoundedAgent = definition.Archetype
                == TokraOrganicOperationArchetype.WoundedAgentCare;
            bool isMedicalSupply = definition.Archetype
                == TokraOrganicOperationArchetype.MedicalSupplyHandoff;
            bool isIntelligence = definition.Archetype
                == TokraOrganicOperationArchetype.DeadDropRecovery;

            if (isObservation
                && outcome == TokraOrganicOperationOutcome.Succeeded
                && bypassSuccessValidation)
            {
                observationDeviceDeployed = true;
                observationReadyTick = currentTick;
                observationWorkTotalTicks
                    = GetConfiguredObservationWorkTicks();
                observationWorkRemainingTicks = 0;
                observationTransmissionTotalTicks
                    = ObservationTransmissionWorkTicks;
                observationTransmissionRemainingTicks = 0;
                activeState = TokraOrganicOperationState.Ready;
            }

            if (isIntelligence
                && outcome == TokraOrganicOperationOutcome.Succeeded
                && bypassSuccessValidation
                && intelligenceAnalysisMethod
                    == TokraIntelligenceAnalysisMethod.None)
            {
                intelligenceAnalysisMethod
                    = TokraIntelligenceAnalysisMethod.Cautious;
                intelligenceWorkTotalTicks
                    = GetConfiguredIntelligenceWorkTicks(
                        TokraIntelligenceAnalysisMethod.Cautious);
                intelligenceWorkRemainingTicks = 0;
                activeState = TokraOrganicOperationState.Ready;
            }

            if (outcome == TokraOrganicOperationOutcome.Succeeded
                && !bypassSuccessValidation)
            {
                if (isObservation)
                {
                    if (!observationDeviceDeployed
                        || !HasValidObservationDevice()
                        || activeState != TokraOrganicOperationState.Ready
                        || observationTransmissionTotalTicks <= 0
                        || observationTransmissionRemainingTicks > 0
                        || IsOperationDeadlineExpired())
                    {
                        return false;
                    }
                }
                else if (isWoundedAgent)
                {
                    if (!woundedAgentDepartureOrdered
                        || activeWoundedAgent == null
                        || activeWoundedAgent.Dead
                        || activeWoundedAgent.Spawned)
                    {
                        return false;
                    }
                }
                else if (isMedicalSupply)
                {
                    if (activeMedicalSupplyLiaison == null
                        || activeMedicalSupplyLiaison.Dead
                        || activeState != TokraOrganicOperationState.Ready
                        || IsOperationDeadlineExpired())
                    {
                        return false;
                    }
                }
                else if (isIntelligence)
                {
                    if (!HasValidIntelligenceObjective()
                        || intelligenceAnalysisMethod
                            == TokraIntelligenceAnalysisMethod.None
                        || intelligenceWorkRemainingTicks > 0
                        || IsOperationDeadlineExpired())
                    {
                        return false;
                    }
                }
                else if (IsOperationDeadlineExpired()
                    || (!definition.HasPhysicalObjective
                        && currentTick < reportReadyTick))
                {
                    return false;
                }
            }

            if (isMedicalSupply
                && outcome == TokraOrganicOperationOutcome.Failed)
            {
                BeginMedicalSupplyDeparture(
                    activeMedicalSupplyLiaison,
                    monitorDeath: false);
            }

            if (isObservation
                && outcome == TokraOrganicOperationOutcome.Succeeded)
            {
                PrepareObservationOutcome();
            }

            if (isIntelligence
                && outcome == TokraOrganicOperationOutcome.Succeeded)
            {
                PrepareIntelligenceOutcome();
            }

            int intellectualXp = definition.IntellectualXp;
            int configuredSkillXp = definition.UsesMissionFrameworkDef
                ? definition.SkillXpRewardAmount
                : 0;

            if (isIntelligence
                && intelligenceAnalysisMethod
                    == TokraIntelligenceAnalysisMethod.Accelerated)
            {
                configuredSkillXp
                    += definition.IntelligenceAcceleratedXpBonus;
            }

            if (isIntelligence && definition.UsesMissionFrameworkDef)
            {
                intellectualXp = configuredSkillXp;
            }

            resolutionApplied = true;
            Pawn patient = activeWoundedAgent;
            Thing patientTarget = patient != null && patient.Spawned
                ? patient
                : null;
            Thing liaisonTarget = activeMedicalSupplyLiaison != null
                    && activeMedicalSupplyLiaison.Spawned
                ? activeMedicalSupplyLiaison
                : null;
            Thing letterTarget = operatorPawn
                ?? activeDeadDrop
                ?? patientTarget
                ?? liaisonTarget
                ?? FindPoweredCommunicator(GetActiveMap());

            if (outcome == TokraOrganicOperationOutcome.Succeeded)
            {
                if (definition.UsesMissionFrameworkDef)
                {
                    GrantSkillExperience(
                        operatorPawn,
                        definition.SkillXpRewardDefName,
                        configuredSkillXp);
                }
                else
                {
                    GrantIntellectualExperience(
                        operatorPawn,
                        intellectualXp);
                }
                GrantMedicineExperience(
                    operatorPawn,
                    definition.MedicineXp);
                GrantSocialExperience(
                    operatorPawn,
                    definition.SocialXp);
                completedOperationCount++;
                lastCompletedArchetype = activeArchetype;
            }
            else
            {
                failedOperationCount++;
            }

            GameComponent_TokraTrustTracker.NotifyOrganicOperationOutcome(
                activeArchetype,
                outcome,
                trustChangeOverride,
                trustMessageKeyOverride);

            SendResolutionLetter(
                definition,
                outcome,
                operatorPawn,
                failureTextKey,
                letterTarget,
                patient,
                intellectualXp);

            TokraOrganicWoundedAgentUtility
                .ClearOperationHealthConditions(patient);

            string skillXpReport = definition.UsesMissionFrameworkDef
                ? $"{definition.SkillXpRewardDefName ?? "none"} XP "
                    + $"{(outcome == TokraOrganicOperationOutcome.Succeeded ? configuredSkillXp : 0)}"
                : "Intellectual XP "
                    + $"{(outcome == TokraOrganicOperationOutcome.Succeeded ? intellectualXp : 0)}";

            GR_Log.Message(
                "Resolved Tok'ra organic operation "
                + $"{definition.DebugLabel} with outcome {outcome}; "
                + $"map {activeMapId}; operator "
                + $"{operatorPawn?.LabelShortCap ?? "none"}; "
                + $"patient {patient?.LabelShortCap ?? "none"}; "
                + skillXpReport + "; "
                + $"Medicine XP "
                + $"{(outcome == TokraOrganicOperationOutcome.Succeeded ? definition.MedicineXp : 0)}; "
                + $"Social XP "
                + $"{(outcome == TokraOrganicOperationOutcome.Succeeded ? definition.SocialXp : 0)}.");

            NotifyMissionResolved(
                definition,
                GetActiveMap(),
                operatorPawn,
                outcome);

            bool preservePatientAfterFailure = patient != null
                && (patient.Dead
                    || failureTextKey
                        == definition.GetRuntimeTextKey("failureCaptured"));

            if (outcome == TokraOrganicOperationOutcome.Failed
                && !preservePatientAfterFailure)
            {
                TokraOrganicWoundedAgentUtility.RemoveLivingPatient(patient);
            }

            DestroyActiveObjective();
            ClearActiveOpportunity(
                destroyObjective: false,
                removeLivingPatient: false);
            ScheduleNextOpportunity(currentTick);
            return true;
        }

        private void SendResolutionLetter(
            TokraOrganicOperationDefinition definition,
            TokraOrganicOperationOutcome outcome,
            Pawn operatorPawn,
            string failureTextKey,
            Thing letterTarget,
            Pawn patient,
            int intellectualXp)
        {
            if (definition.Archetype
                == TokraOrganicOperationArchetype.TemporaryBaseDelivery)
            {
                GateRimMissionRuntimeData runtime
                    = activeOperation.frameworkRuntime;
                string requiredCount
                    = TokraTemporaryBaseDeliveryMissionUtility
                        .GetRequiredCount(runtime).ToString();
                string itemLabel
                    = TokraTemporaryBaseDeliveryMissionUtility
                        .GetContractLabel(runtime);

                if (outcome == TokraOrganicOperationOutcome.Succeeded)
                {
                    bool deliveredLate
                        = TokraTemporaryBaseDeliveryMissionUtility
                            .WasDeliveredLate(runtime);
                    string labelKey = deliveredLate
                        ? definition.GetRuntimeTextKey(
                            "lateSuccessLetterLabel")
                        : definition.SuccessLetterLabelKey;
                    string textKey = deliveredLate
                        ? definition.GetRuntimeTextKey(
                            "lateSuccessLetterText")
                        : GetMissionSuccessTextKey(definition);

                    Find.LetterStack?.ReceiveLetter(
                        labelKey.Translate(),
                        textKey.Translate(requiredCount, itemLabel),
                        LetterDefOf.PositiveEvent,
                        letterTarget);
                }
                else
                {
                    string textKey = string.IsNullOrEmpty(failureTextKey)
                        ? definition.GetRuntimeTextKey("failureTimeout")
                        : failureTextKey;

                    Find.LetterStack?.ReceiveLetter(
                        definition.FailureLetterLabelKey.Translate(),
                        textKey.Translate(requiredCount, itemLabel),
                        LetterDefOf.NegativeEvent,
                        letterTarget);
                }

                return;
            }


            if (definition.Archetype
                == TokraOrganicOperationArchetype.DecoyTransmissionDefense)
            {
                if (outcome == TokraOrganicOperationOutcome.Succeeded)
                {
                    Find.LetterStack?.ReceiveLetter(
                        definition.SuccessLetterLabelKey.Translate(),
                        GetMissionSuccessTextKey(definition).Translate(),
                        LetterDefOf.PositiveEvent,
                        letterTarget);
                }
                else
                {
                    string textKey = string.IsNullOrEmpty(failureTextKey)
                        ? definition.GetRuntimeTextKey("failureTimeout")
                        : failureTextKey;

                    Find.LetterStack?.ReceiveLetter(
                        definition.FailureLetterLabelKey.Translate(),
                        textKey.Translate(),
                        LetterDefOf.NegativeEvent,
                        letterTarget);
                }

                return;
            }

            if (definition.Archetype
                == TokraOrganicOperationArchetype.JaffaOfficerCapture)
            {
                if (outcome == TokraOrganicOperationOutcome.Succeeded)
                {
                    Find.LetterStack?.ReceiveLetter(
                        definition.SuccessLetterLabelKey.Translate(),
                        GetMissionSuccessTextKey(definition).Translate(),
                        LetterDefOf.PositiveEvent,
                        letterTarget);
                }
                else
                {
                    string textKey = string.IsNullOrEmpty(failureTextKey)
                        ? definition.GetRuntimeTextKey("failureTimeout")
                        : failureTextKey;

                    Find.LetterStack?.ReceiveLetter(
                        definition.FailureLetterLabelKey.Translate(),
                        textKey.Translate(),
                        LetterDefOf.NegativeEvent,
                        letterTarget);
                }

                return;
            }


            if (definition.Archetype
                == TokraOrganicOperationArchetype.DistressCall)
            {
                if (outcome == TokraOrganicOperationOutcome.Succeeded)
                {
                    Find.LetterStack?.ReceiveLetter(
                        definition.SuccessLetterLabelKey.Translate(),
                        GetMissionSuccessTextKey(definition).Translate(),
                        LetterDefOf.PositiveEvent,
                        letterTarget);
                }
                else
                {
                    string textKey = string.IsNullOrEmpty(failureTextKey)
                        ? definition.GetRuntimeTextKey("failureTimeout")
                        : failureTextKey;

                    Find.LetterStack?.ReceiveLetter(
                        definition.FailureLetterLabelKey.Translate(),
                        textKey.Translate(),
                        LetterDefOf.NegativeEvent,
                        letterTarget);
                }

                return;
            }

            if (definition.Archetype
                == TokraOrganicOperationArchetype.WoundedAgentCare)
            {
                if (outcome == TokraOrganicOperationOutcome.Succeeded)
                {
                    Find.LetterStack?.ReceiveLetter(
                        definition.SuccessLetterLabelKey.Translate(),
                        GetMissionSuccessTextKey(definition).Translate(
                            patient?.LabelShortCap ?? "?"),
                        LetterDefOf.PositiveEvent,
                        letterTarget);
                }
                else
                {
                    string textKey = string.IsNullOrEmpty(failureTextKey)
                        ? definition.GetRuntimeTextKey("failureTimeout")
                        : failureTextKey;

                    Find.LetterStack?.ReceiveLetter(
                        definition.FailureLetterLabelKey.Translate(),
                        textKey.Translate(patient?.LabelShortCap ?? "?"),
                        LetterDefOf.NegativeEvent,
                        letterTarget);
                }

                return;
            }

            if (definition.Archetype
                == TokraOrganicOperationArchetype.GoauldObservation)
            {
                if (outcome == TokraOrganicOperationOutcome.Succeeded)
                {
                    Find.LetterStack?.ReceiveLetter(
                        definition.SuccessLetterLabelKey.Translate(),
                        GetObservationSuccessTextKey(definition).Translate(
                            operatorPawn?.LabelShortCap ?? "?",
                            definition.SkillXpRewardAmount.ToString()),
                        LetterDefOf.PositiveEvent,
                        letterTarget);
                }
                else
                {
                    string observationFailureKey
                        = string.IsNullOrEmpty(failureTextKey)
                            ? definition.GetRuntimeTextKey("failureTimeout")
                            : failureTextKey;

                    Find.LetterStack?.ReceiveLetter(
                        definition.FailureLetterLabelKey.Translate(),
                        observationFailureKey.Translate(),
                        LetterDefOf.NegativeEvent,
                        letterTarget);
                }

                return;
            }

            if (definition.Archetype
                == TokraOrganicOperationArchetype.MedicalSupplyHandoff)
            {
                if (outcome == TokraOrganicOperationOutcome.Succeeded)
                {
                    Find.LetterStack?.ReceiveLetter(
                        definition.SuccessLetterLabelKey.Translate(),
                        GetMissionSuccessTextKey(definition).Translate(
                            operatorPawn?.LabelShortCap ?? "?",
                            definition.SkillXpRewardAmount.ToString()),
                        LetterDefOf.PositiveEvent,
                        letterTarget);
                }
                else
                {
                    string textKey = string.IsNullOrEmpty(failureTextKey)
                        ? definition.GetRuntimeTextKey("failureTimeout")
                        : failureTextKey;

                    Find.LetterStack?.ReceiveLetter(
                        definition.FailureLetterLabelKey.Translate(),
                        textKey.Translate(),
                        LetterDefOf.NegativeEvent,
                        letterTarget);
                }

                return;
            }

            if (outcome == TokraOrganicOperationOutcome.Succeeded)
            {
                Find.LetterStack?.ReceiveLetter(
                    definition.SuccessLetterLabelKey.Translate(),
                    GetIntelligenceSuccessTextKey(definition).Translate(
                        operatorPawn?.LabelShortCap ?? "?",
                        intellectualXp.ToString()),
                    intelligencePatrolQueued
                        ? LetterDefOf.ThreatSmall
                        : LetterDefOf.PositiveEvent,
                    letterTarget);
                return;
            }

            string deadDropTextKey = string.IsNullOrEmpty(failureTextKey)
                ? definition.GetRuntimeTextKey("failureTimeout")
                : failureTextKey;

            Find.LetterStack?.ReceiveLetter(
                definition.FailureLetterLabelKey.Translate(),
                deadDropTextKey.Translate(),
                LetterDefOf.NegativeEvent);
        }

        private void PrepareObservationOutcome()
        {
            if (observationResultVariant >= 0)
            {
                return;
            }

            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();
            int selectedIndex = -1;
            string selectedKey = definition?.SelectSuccessLetterTextKey(
                lastObservationResultVariant,
                out selectedIndex);

            if (string.IsNullOrEmpty(selectedKey))
            {
                observationResultVariant = -1;
                return;
            }

            observationResultVariant = selectedIndex;
            lastObservationResultVariant = selectedIndex;
        }

        private string GetObservationSuccessTextKey(
            TokraOrganicOperationDefinition definition)
        {
            List<GateRimMissionTextVariantDef> variants
                = definition?.MissionDef?.texts?.successLetterTexts;

            if (variants == null || variants.Count == 0)
            {
                return null;
            }

            int variant = Math.Max(
                0,
                Math.Min(variants.Count - 1, observationResultVariant));
            return variants[variant].key;
        }

        private void PrepareIntelligenceOutcome()
        {
            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();

            if (definition == null)
            {
                return;
            }

            if (!intelligenceInterferenceRollResolved)
            {
                intelligenceInterferenceRollResolved = true;

                if (intelligenceAnalysisMethod
                        == TokraIntelligenceAnalysisMethod.Accelerated
                    && Rand.Chance(
                        definition.IntelligenceInterferenceChance))
                {
                    intelligencePatrolQueued = TryQueueIntelligencePatrol(
                        GetActiveMap());
                    intelligenceInterferenceTriggered
                        = intelligencePatrolQueued;
                }
                else
                {
                    intelligenceInterferenceTriggered = false;
                    intelligencePatrolQueued = false;
                }
            }

            if (intelligenceResultVariant >= 0)
            {
                return;
            }

            string bankId = GetIntelligenceSuccessBankId();
            string storageKey = definition.MissionDefName + ":" + bankId;
            int previousIndex = -1;
            int storedIndex;

            if (lastMissionTextVariantIndexes != null
                && lastMissionTextVariantIndexes.TryGetValue(
                    storageKey,
                    out storedIndex))
            {
                previousIndex = storedIndex;
            }

            int selectedIndex;
            string selectedKey = definition.SelectNamedTextKey(
                bankId,
                previousIndex,
                out selectedIndex);

            if (string.IsNullOrEmpty(selectedKey) || selectedIndex < 0)
            {
                intelligenceResultVariant = -1;
                return;
            }

            intelligenceResultVariant = selectedIndex;
            lastIntelligenceResultVariant = selectedIndex;

            if (lastMissionTextVariantIndexes != null)
            {
                lastMissionTextVariantIndexes[storageKey] = selectedIndex;
            }
        }

        private string GetIntelligenceSuccessTextKey(
            TokraOrganicOperationDefinition definition)
        {
            return definition?.GetNamedTextKey(
                GetIntelligenceSuccessBankId(),
                intelligenceResultVariant);
        }

        private string GetIntelligenceSuccessBankId()
        {
            if (intelligenceAnalysisMethod
                == TokraIntelligenceAnalysisMethod.Cautious)
            {
                return "cautiousSuccess";
            }

            return intelligencePatrolQueued
                ? "interferenceSuccess"
                : "acceleratedSuccess";
        }

        private bool TryQueueIntelligencePatrol(Map map)
        {
            TokraOrganicOperationDefinition definition
                = GetActiveDefinition()
                    ?? GetIntelligenceDefinition();
            IncidentDef patrolDef = string.IsNullOrEmpty(
                    definition?.IntelligencePatrolIncidentDefName)
                ? null
                : DefDatabase<IncidentDef>.GetNamedSilentFail(
                    definition.IntelligencePatrolIncidentDefName);

            if (map == null
                || definition == null
                || patrolDef == null
                || patrolDef.category == null
                || Find.Storyteller?.incidentQueue == null)
            {
                GR_Log.Warning(
                    "Could not queue Goa'uld patrol after accelerated Tok'ra "
                    + "intelligence analysis: mission configuration, map or "
                    + "storyteller queue is unavailable.");
                return false;
            }

            GateRimMissionRuntimeData runtime
                = activeOperation.frameworkRuntime;

            if (runtime == null)
            {
                runtime = new GateRimMissionRuntimeData();
                activeOperation.frameworkRuntime = runtime;
            }

            if (runtime.scaledThreatPoints <= 0f)
            {
                GateRimMissionDifficultySnapshot snapshot
                    = GateRimMissionFramework.CaptureDifficulty(
                        map,
                        definition.MissionDef?.difficulty);
                runtime.baseThreatPoints = snapshot.BaseThreatPoints;
                runtime.scaledThreatPoints = snapshot.ScaledThreatPoints;
                runtime.difficultyFactor = snapshot.Factor;
            }

            if (runtime.scaledThreatPoints <= 0f)
            {
                GR_Log.Warning(
                    "Could not queue Goa'uld patrol after accelerated Tok'ra "
                    + "intelligence analysis: the captured threat snapshot "
                    + "contains no usable points.");
                return false;
            }

            IncidentParms parms = StorytellerUtility.DefaultParmsNow(
                patrolDef.category,
                map);
            parms.forced = true;
            parms.faction = GoauldSystemLordFactionUtility.GetOrCreateFaction(
                "Tok'ra accelerated intelligence interference patrol");
            parms.points = runtime.scaledThreatPoints;

            int fireTick = (Find.TickManager?.TicksGame ?? 0)
                + Rand.RangeInclusive(
                    definition.IntelligencePatrolDelayMinimumTicks,
                    definition.IntelligencePatrolDelayMaximumTicks);

            Find.Storyteller.incidentQueue.Add(
                patrolDef,
                fireTick,
                parms,
                definition.IntelligencePatrolRetryTicks);

            GR_Log.Message(
                "Queued a small Goa'uld patrol after accelerated Tok'ra "
                + $"intelligence analysis; map={map.uniqueID}; "
                + $"snapshot={runtime.baseThreatPoints:0}; "
                + $"points={parms.points:0}; fireTick={fireTick}.");

            return true;
        }

        private void InitializeFrameworkRuntime(
            TokraOrganicOperationDefinition definition,
            Map map)
        {
            if (activeOperation.frameworkRuntime == null)
            {
                activeOperation.frameworkRuntime
                    = new GateRimMissionRuntimeData();
            }

            GateRimMissionRuntimeData runtime
                = activeOperation.frameworkRuntime;
            runtime.Reset();
            runtime.missionDefName = definition.MissionDefName;
            runtime.phaseId = definition.UsesMissionFrameworkDef
                ? "offered"
                : null;

            GateRimMissionDifficultySnapshot snapshot
                = GateRimMissionFramework.CaptureDifficulty(
                    map,
                    definition.MissionDef?.difficulty);
            runtime.baseThreatPoints = snapshot.BaseThreatPoints;
            runtime.scaledThreatPoints = snapshot.ScaledThreatPoints;
            runtime.difficultyFactor = snapshot.Factor;
        }


        private TaggedString GetOfferLetterText(
            TokraOrganicOperationDefinition definition,
            string offerLetterTextKey)
        {
            string remainingHours = GetRoundedUpHours(
                definition.OfferDurationTicks).ToString();

            if (definition.Archetype
                != TokraOrganicOperationArchetype.TemporaryBaseDelivery)
            {
                return offerLetterTextKey.Translate(remainingHours);
            }

            GateRimMissionRuntimeData runtime
                = activeOperation.frameworkRuntime;

            return offerLetterTextKey.Translate(
                TokraTemporaryBaseDeliveryMissionUtility
                    .GetRequiredCount(runtime).ToString(),
                TokraTemporaryBaseDeliveryMissionUtility
                    .GetContractLabel(runtime),
                TokraTemporaryBaseDeliveryMissionUtility
                    .GetQualityLabel(runtime),
                remainingHours);
        }

        private string SelectOfferLetterTextKey(
            TokraOrganicOperationDefinition definition,
            out int selectedIndex)
        {
            string bankId = (definition.MissionDefName
                ?? definition.Archetype.ToString())
                + ":"
                + GateRimMissionFramework.OfferTextBankKey;
            int previousIndex = -1;

            if (definition.UsesMissionFrameworkDef
                && lastMissionTextVariantIndexes != null)
            {
                int storedIndex;

                if (lastMissionTextVariantIndexes.TryGetValue(
                    bankId,
                    out storedIndex))
                {
                    previousIndex = storedIndex;
                }
            }

            string selectedKey = definition.SelectOfferLetterTextKey(
                previousIndex,
                out selectedIndex);

            if (definition.UsesMissionFrameworkDef
                && selectedIndex >= 0
                && lastMissionTextVariantIndexes != null)
            {
                lastMissionTextVariantIndexes[bankId] = selectedIndex;
            }

            return selectedKey ?? definition.OfferLetterTextKey;
        }

        private void ScheduleNextOpportunity(int currentTick)
        {
            int minimumDelay;
            int maximumDelay;

            TokraOrganicOperationFramework.GetDelayRange(
                lastOfferedArchetype,
                GameComponent_TokraTrustTracker.GetCurrentTier(),
                out minimumDelay,
                out maximumDelay);

            nextOpportunityTick = currentTick + Rand.RangeInclusive(
                minimumDelay,
                maximumDelay);
        }

        private bool TrySelectArchetype(
            TokraTrustTier tier,
            Map map,
            out TokraOrganicOperationArchetype archetype)
        {
            int configuredCandidateCount;
            List<OrganicOperationCandidate> candidates = BuildCandidates(
                tier,
                map,
                lastOfferedArchetype,
                filterOfferability: true,
                out configuredCandidateCount);

            if (candidates.Count == 0)
            {
                archetype = TokraOrganicOperationArchetype.None;

                // No configured weight is a stable state. Temporarily
                // unavailable missions are retried on the normal state-check
                // interval instead of consuming a full recurrence delay.
                return configuredCandidateCount == 0;
            }

            archetype = SelectCandidate(candidates, Rand.Value);
            return archetype != TokraOrganicOperationArchetype.None;
        }

        private List<OrganicOperationCandidate> BuildCandidates(
            TokraTrustTier tier,
            Map map,
            TokraOrganicOperationArchetype previousArchetype,
            bool filterOfferability,
            out int configuredCandidateCount)
        {
            List<OrganicOperationCandidate> candidates
                = new List<OrganicOperationCandidate>();
            configuredCandidateCount = 0;

            foreach (TokraOrganicOperationDefinition definition
                in TokraOrganicOperationFramework.AllDefinitions)
            {
                float weight = definition.GetWeight(tier);

                if (weight <= 0f)
                {
                    continue;
                }

                configuredCandidateCount++;

                if (filterOfferability
                    && !IsDefinitionOfferable(definition, map))
                {
                    continue;
                }

                candidates.Add(new OrganicOperationCandidate(
                    definition.Archetype,
                    weight,
                    definition.RepeatedArchetypeWeightFactor));
            }

            if (candidates.Count > 1
                && previousArchetype
                    != TokraOrganicOperationArchetype.None)
            {
                for (int i = 0; i < candidates.Count; i++)
                {
                    OrganicOperationCandidate candidate = candidates[i];

                    if (candidate.Archetype == previousArchetype)
                    {
                        candidate.Weight *= candidate
                            .RepeatedArchetypeWeightFactor;
                        candidates[i] = candidate;
                    }
                }
            }

            return candidates;
        }

        private static bool IsDefinitionOfferable(
            TokraOrganicOperationDefinition definition,
            Map map)
        {
            if (definition == null || map == null)
            {
                return false;
            }

            GateRimMissionWorker worker = definition.MissionDef?.Worker;
            return worker == null || worker.CanOffer(map);
        }

        private static TokraOrganicOperationArchetype SelectCandidate(
            IReadOnlyList<OrganicOperationCandidate> candidates,
            float roll)
        {
            float totalWeight = candidates.Sum(candidate => candidate.Weight);

            if (totalWeight <= 0f)
            {
                return TokraOrganicOperationArchetype.None;
            }

            float boundedRoll = Math.Max(0f, Math.Min(0.999999f, roll));
            float selection = boundedRoll * totalWeight;

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
            if (map == null
                || activeState == TokraOrganicOperationState.None
                || activeArchetype == TokraOrganicOperationArchetype.None)
            {
                return false;
            }

            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();

            if (definition == null)
            {
                return false;
            }

            if (IsActiveForMap(map)
                && activeState == TokraOrganicOperationState.Offered)
            {
                return true;
            }

            if (!definition.UsesCommunicatorForCompletion)
            {
                return false;
            }

            if (activeArchetype
                == TokraOrganicOperationArchetype.JaffaOfficerCapture)
            {
                SyncJaffaOfficerCaptureStateFromWorldSite();

                if (jaffaOfficerCapture?.extractionRequested == true)
                {
                    return false;
                }

                Pawn target = jaffaOfficerCapture?.targetOfficer;
                bool targetOnMap = target != null
                    && map.IsPlayerHome
                    && TokraJaffaOfficerCaptureMissionUtility
                        .IsPawnPresentOnMapIncludingCarried(target, map);

                return (IsActiveForMap(map) || targetOnMap)
                    && (activeState == TokraOrganicOperationState.Accepted
                        || activeState == TokraOrganicOperationState.Ready);
            }

            if (!IsActiveForMap(map))
            {
                return false;
            }

            UpdateObservationReadyState(
                Find.TickManager?.TicksGame ?? 0,
                notifyPlayer: false);

            if (activeArchetype
                == TokraOrganicOperationArchetype.GoauldObservation)
            {
                if (activeState != TokraOrganicOperationState.Ready)
                {
                    return false;
                }

                return !TokraObservationUtility
                    .IsObservationPoint(activeDeadDrop);
            }

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
                && definition.Archetype
                    == TokraOrganicOperationArchetype.DeadDropRecovery
                && activeState == TokraOrganicOperationState.Accepted
                && activeDeadDrop == deadDrop;
        }

        private bool IsExactMedicalSupplyLiaison(Pawn liaison)
        {
            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();

            return liaison != null
                && definition != null
                && definition.Archetype
                    == TokraOrganicOperationArchetype.MedicalSupplyHandoff
                && (activeState == TokraOrganicOperationState.Accepted
                    || activeState == TokraOrganicOperationState.Ready)
                && activeMedicalSupplyLiaison == liaison;
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

                ThingWithComps communicator;

                if (TokraSecureCommunicatorAvailabilityUtility
                    .TryFindAvailableCommunicator(map, out communicator))
                {
                    return map;
                }
            }

            return null;
        }

        private string GetObservationTextKey(string id)
        {
            return GetActiveDefinition()?.GetRuntimeTextKey(id);
        }

        private string GetWoundedAgentTextKey(string id)
        {
            return GetActiveDefinition()?.GetRuntimeTextKey(id)
                ?? TokraOrganicOperationFramework.GetDefinition(
                    TokraOrganicOperationArchetype.WoundedAgentCare)
                    ?.GetRuntimeTextKey(id);
        }

        private string GetMissionSuccessTextKey(
            TokraOrganicOperationDefinition definition)
        {
            if (definition == null)
            {
                return null;
            }

            string storageKey = definition.MissionDefName + ":success";
            int previousIndex = -1;
            int storedIndex;

            if (lastMissionTextVariantIndexes != null
                && lastMissionTextVariantIndexes.TryGetValue(
                    storageKey,
                    out storedIndex))
            {
                previousIndex = storedIndex;
            }

            int selectedIndex;
            string selectedKey = definition.SelectSuccessLetterTextKey(
                previousIndex,
                out selectedIndex);

            if (!string.IsNullOrEmpty(selectedKey) && selectedIndex >= 0)
            {
                if (lastMissionTextVariantIndexes == null)
                {
                    lastMissionTextVariantIndexes
                        = new Dictionary<string, int>();
                }

                lastMissionTextVariantIndexes[storageKey] = selectedIndex;
            }

            return selectedKey;
        }

        private void SetWoundedAgentFrameworkPhase(string phaseId)
        {
            if (activeOperation.frameworkRuntime == null
                || string.IsNullOrEmpty(
                    activeOperation.frameworkRuntime.missionDefName)
                || string.IsNullOrEmpty(phaseId))
            {
                return;
            }

            activeOperation.frameworkRuntime.phaseId = phaseId;
        }

        private void SetIntelligenceFrameworkPhase(
            TokraIntelligenceAnalysisMethod method)
        {
            if (activeOperation.frameworkRuntime == null
                || string.IsNullOrEmpty(
                    activeOperation.frameworkRuntime.missionDefName))
            {
                return;
            }

            activeOperation.frameworkRuntime.phaseId = method
                == TokraIntelligenceAnalysisMethod.Cautious
                ? "cautious"
                : "accelerated";
        }

        private string GetIntelligenceTextKey(string id)
        {
            return GetActiveDefinition()?.GetRuntimeTextKey(id)
                ?? GetIntelligenceDefinition()?.GetRuntimeTextKey(id);
        }

        private SkillDef GetObservationSkillDef()
        {
            string defName = GetActiveDefinition()?.ObservationSkillDefName;
            return string.IsNullOrEmpty(defName)
                ? null
                : DefDatabase<SkillDef>.GetNamedSilentFail(defName);
        }

        private bool CanUseObservationOperator(Pawn pawn)
        {
            SkillDef skillDef = GetObservationSkillDef();

            if (pawn == null
                || pawn.Dead
                || pawn.Downed
                || pawn.Faction != Faction.OfPlayer
                || pawn.RaceProps?.Humanlike != true
                || skillDef == null
                || pawn.skills == null
                || pawn.jobs == null)
            {
                return false;
            }

            SkillRecord skill = pawn.skills.GetSkill(skillDef);
            return skill != null && !skill.TotallyDisabled;
        }

        private bool HasValidObservationDevice()
        {
            if (activeArchetype
                    != TokraOrganicOperationArchetype.GoauldObservation
                || activeDeadDrop == null
                || activeDeadDrop.Destroyed)
            {
                return false;
            }

            if (activeDeadDrop.Spawned
                && activeDeadDrop.Map?.uniqueID == activeMapId)
            {
                return true;
            }

            Map map = GetActiveMap();

            if (map?.mapPawns?.AllPawnsSpawned == null)
            {
                return false;
            }

            IReadOnlyList<Pawn> pawns = map.mapPawns.AllPawnsSpawned;

            for (int i = 0; i < pawns.Count; i++)
            {
                if (pawns[i]?.carryTracker?.CarriedThing == activeDeadDrop)
                {
                    return true;
                }
            }

            return false;
        }

        private int GetConfiguredObservationWorkTicks()
        {
            int configuredTicks
                = GetActiveDefinition()?.ObservationWorkTicks ?? 0;

            return Math.Max(1, configuredTicks);
        }

        private bool TryRepairLegacyObservationState(int currentTick)
        {
            if (activeArchetype
                    != TokraOrganicOperationArchetype.GoauldObservation
                || (activeState != TokraOrganicOperationState.Accepted
                    && activeState != TokraOrganicOperationState.Ready))
            {
                return true;
            }

            if (observationDeviceDeployed
                && observationWorkTotalTicks <= 0)
            {
                int configuredWorkTicks
                    = GetConfiguredObservationWorkTicks();
                observationWorkTotalTicks = configuredWorkTicks;

                if (activeState == TokraOrganicOperationState.Ready)
                {
                    observationWorkRemainingTicks = 0;
                }
                else
                {
                    int legacyRemaining = observationReadyTick > currentTick
                        ? observationReadyTick - currentTick
                        : configuredWorkTicks;
                    observationWorkRemainingTicks = Math.Max(
                        1,
                        Math.Min(
                            configuredWorkTicks,
                            legacyRemaining));
                }

                observationReadyTick = 0;
                reportReadyTick = 0;

                GR_Log.Message(
                    "Converted a timed Tok'ra observation into active "
                    + "operator-controlled observation work.");
            }

            if (observationDeviceDeployed
                && TokraObservationUtility.IsObservationDevice(activeDeadDrop)
                && activeDeadDrop.Spawned
                && observationTransmissionTotalTicks <= 0)
            {
                Map legacyMap = GetActiveMap();
                ThingDef markerDef = DefDatabase<ThingDef>.GetNamedSilentFail(
                    TokraObservationUtility.MarkerDefName);

                if (legacyMap != null && markerDef != null)
                {
                    IntVec3 legacyCell = activeDeadDrop.Position;
                    Thing createdMarker = ThingMaker.MakeThing(markerDef);
                    Thing placedMarker;

                    if (GenPlace.TryPlaceThing(
                            createdMarker,
                            legacyCell,
                            legacyMap,
                            ThingPlaceMode.Near,
                            out placedMarker))
                    {
                        Thing legacyDevice = activeDeadDrop;
                        activeDeadDrop = placedMarker;
                        observationPointMarker = placedMarker;
                        observationTargetCell = placedMarker.Position;

                        if (legacyDevice != null
                            && !legacyDevice.Destroyed)
                        {
                            legacyDevice.Destroy(DestroyMode.Vanish);
                        }

                        GR_Log.Message(
                            "Converted a 0.3.2 loose observation device "
                            + "into an installed field station.");
                    }
                    else if (!createdMarker.Destroyed)
                    {
                        createdMarker.Destroy(DestroyMode.Vanish);
                    }
                }
            }

            if (HasValidObservationDevice())
            {
                return true;
            }

            if (observationTargetCell.IsValid)
            {
                return false;
            }

            Map map = GetActiveMap();
            Thing device;
            Thing marker;
            IntVec3 targetCell;

            if (!TokraObservationUtility.TryCreateOperationTargets(
                    map,
                    out device,
                    out marker,
                    out targetCell))
            {
                return false;
            }

            activeDeadDrop = device;
            observationPointMarker = marker;
            observationTargetCell = targetCell;
            observationDeviceDeployed = false;
            observationReadyTick = 0;
            observationWorkTotalTicks = 0;
            observationWorkRemainingTicks = 0;
            observationTransmissionTotalTicks = 0;
            observationTransmissionRemainingTicks = 0;
            observationResultVariant = -1;
            activeState = TokraOrganicOperationState.Accepted;
            reportReadyTick = 0;
            readyNotificationSent = false;
            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();
            int restoredDeadline = Math.Max(
                1,
                definition?.DeadlineTicks ?? 1);
            operationDeadlineTick = Math.Max(
                operationDeadlineTick,
                currentTick + restoredDeadline);

            GR_Log.Message(
                "Converted a legacy accepted Tok'ra observation into the "
                + "0.3.2 field-device workflow.");
            return true;
        }

        private bool HasValidIntelligenceObjective()
        {
            if (activeDeadDrop == null || activeDeadDrop.Destroyed)
            {
                return false;
            }

            if (activeDeadDrop.Spawned
                && activeDeadDrop.Map?.uniqueID == activeMapId)
            {
                return true;
            }

            Map map = GetActiveMap();

            if (map?.mapPawns?.AllPawnsSpawned == null)
            {
                return false;
            }

            IReadOnlyList<Pawn> pawns = map.mapPawns.AllPawnsSpawned;

            for (int i = 0; i < pawns.Count; i++)
            {
                if (IsIntelligenceObjectiveCarriedBy(pawns[i]))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsIntelligenceObjectiveCarriedBy(Pawn pawn)
        {
            return pawn?.carryTracker?.CarriedThing == activeDeadDrop;
        }

        private Pawn FindIntelligenceObjectiveCarrier()
        {
            Map map = GetActiveMap();

            if (map?.mapPawns?.AllPawnsSpawned == null)
            {
                return null;
            }

            IReadOnlyList<Pawn> pawns = map.mapPawns.AllPawnsSpawned;

            for (int i = 0; i < pawns.Count; i++)
            {
                if (IsIntelligenceObjectiveCarriedBy(pawns[i]))
                {
                    return pawns[i];
                }
            }

            return null;
        }

        private static bool CanUseIntellectualOperator(Pawn pawn)
        {
            if (pawn == null
                || pawn.Dead
                || pawn.Downed
                || pawn.Faction != Faction.OfPlayer
                || pawn.RaceProps?.Humanlike != true
                || pawn.skills == null
                || pawn.jobs == null)
            {
                return false;
            }

            SkillRecord intellectual = pawn.skills.GetSkill(
                SkillDefOf.Intellectual);

            return intellectual != null && !intellectual.TotallyDisabled;
        }

        private static bool IsPoweredSecureCommunicator(Thing thing)
        {
            ThingWithComps communicator = thing as ThingWithComps;

            if (communicator == null
                || !communicator.Spawned
                || communicator.Destroyed
                || communicator.Faction != Faction.OfPlayer
                || communicator.GetComp<Comp_TokraSecureCommunicator>() == null)
            {
                return false;
            }

            CompPowerTrader powerComp = communicator.GetComp<CompPowerTrader>();
            return powerComp != null && powerComp.PowerOn;
        }

        private static ThingWithComps FindPoweredCommunicator(Map map)
        {
            ThingWithComps communicator;

            return TokraSecureCommunicatorAvailabilityUtility
                .TryFindAvailableCommunicator(map, out communicator)
                ? communicator
                : null;
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
                if (definition.Archetype
                    == TokraOrganicOperationArchetype.TemporaryBaseDelivery)
                {
                    GateRimMissionRuntimeData runtime
                        = activeOperation.frameworkRuntime;
                    status = definition.OfferedStatusKey.Translate(
                        TokraTemporaryBaseDeliveryMissionUtility
                            .GetRequiredCount(runtime).ToString(),
                        TokraTemporaryBaseDeliveryMissionUtility
                            .GetContractLabel(runtime),
                        TokraTemporaryBaseDeliveryMissionUtility
                            .GetQualityLabel(runtime),
                        GetRoundedUpHours(
                            offerExpiryTick - currentTick).ToString())
                        .ToString();
                }
                else
                {
                    status = definition.OfferedStatusKey.Translate(
                        GetRoundedUpHours(
                            offerExpiryTick - currentTick).ToString())
                        .ToString();
                }
            }
            else if (definition.Archetype
                == TokraOrganicOperationArchetype.TemporaryBaseDelivery)
            {
                GateRimMissionRuntimeData runtime
                    = activeOperation.frameworkRuntime;
                WorldObject_TokraTemporaryBaseDeliverySite site
                    = TokraTemporaryBaseDeliveryMissionUtility.FindWorldSite(
                        runtime);
                bool isLate = site?.IsLate == true
                    || TokraTemporaryBaseDeliveryMissionUtility
                        .IsLateWindowStarted(runtime);
                string statusKey = isLate
                    ? definition.GetRuntimeTextKey("lateStatus")
                    : definition.ActiveStatusKey;
                string remainingHours = site != null
                    ? site.GetRemainingHoursString()
                    : GetRoundedUpHours(
                        operationDeadlineTick - currentTick).ToString();

                status = statusKey.Translate(
                    TokraTemporaryBaseDeliveryMissionUtility
                        .GetRequiredCount(runtime).ToString(),
                    TokraTemporaryBaseDeliveryMissionUtility
                        .GetContractLabel(runtime),
                    TokraTemporaryBaseDeliveryMissionUtility
                        .GetQualityLabel(runtime),
                    remainingHours)
                    .ToString();
            }
            else if (definition.Archetype
                == TokraOrganicOperationArchetype.JaffaOfficerCapture)
            {
                SyncJaffaOfficerCaptureStateFromWorldSite();
                Pawn target = jaffaOfficerCapture?.targetOfficer;
                string remainingHours = GetRoundedUpHours(
                    operationDeadlineTick - currentTick).ToString();
                bool targetSecured = target != null
                    && (jaffaOfficerCapture.extractionRequested
                        || TokraJaffaOfficerCaptureMissionUtility
                            .FindPlayerCaravanContaining(target) != null
                        || (target.IsPrisonerOfColony
                            && TokraJaffaOfficerCaptureMissionUtility
                                .FindPlayerHomeMapContaining(target) != null));
                string statusKey = targetSecured
                    ? definition.ReadyStatusKey
                    : definition.ActiveStatusKey;
                string targetLabel = target?.LabelShortCap
                    ?? "GR_TokraJaffaOfficerCapture_TargetUnknown"
                        .Translate()
                        .ToString();

                status = statusKey.Translate(
                    targetLabel,
                    remainingHours).ToString();
            }
            else if (definition.Archetype
                == TokraOrganicOperationArchetype.DistressCall)
            {
                status = definition.ActiveStatusKey.Translate(
                    GetRoundedUpHours(
                        operationDeadlineTick - currentTick).ToString())
                    .ToString();
            }

            else if (definition.Archetype
                == TokraOrganicOperationArchetype.DecoyTransmissionDefense)
            {
                GateRimMissionRuntimeData runtime
                    = activeOperation.frameworkRuntime;
                string statusKey
                    = TokraDiversionAssaultUtility.IsRaidTriggered(runtime)
                        ? definition.GetRuntimeTextKey("statusAssault")
                        : definition.GetRuntimeTextKey("statusWaiting");

                status = statusKey.Translate().ToString();
            }
            else if (definition.Archetype
                == TokraOrganicOperationArchetype.GoauldObservation)
            {
                if (!observationDeviceDeployed)
                {
                    status = definition.GetRuntimeTextKey("statusAwaitingDeployment")
                        .Translate(
                            GetRoundedUpHours(
                                operationDeadlineTick - currentTick)
                                .ToString())
                        .ToString();
                }
                else if (activeState == TokraOrganicOperationState.Accepted)
                {
                    status = definition.GetRuntimeTextKey("statusRecording")
                        .Translate(
                            GetRoundedUpHours(
                                observationWorkRemainingTicks)
                                .ToString())
                        .ToString();
                }
                else if (observationTransmissionTotalTicks > 0
                    && observationTransmissionRemainingTicks > 0)
                {
                    status = definition.GetRuntimeTextKey("statusTransmissionInterrupted")
                        .Translate(
                            GetRoundedUpHours(
                                operationDeadlineTick - currentTick)
                                .ToString())
                        .ToString();
                }
                else
                {
                    status = definition.GetRuntimeTextKey("statusDataReady")
                        .Translate(
                            GetRoundedUpHours(
                                operationDeadlineTick - currentTick)
                                .ToString())
                        .ToString();
                }
            }
            else if (definition.Archetype
                == TokraOrganicOperationArchetype.WoundedAgentCare)
            {
                if (woundedAgentDepartureOrdered
                    || activeState == TokraOrganicOperationState.Ready)
                {
                    status = definition.ReadyStatusKey.Translate(
                        activeWoundedAgent?.LabelShortCap ?? "?")
                        .ToString();
                }
                else if (!woundedAgentInitialCareReceived)
                {
                    status = definition.GetRuntimeTextKey("statusAwaitingCare")
                        .Translate(
                            activeWoundedAgent?.LabelShortCap ?? "?",
                            GetRoundedUpHours(
                                operationDeadlineTick - currentTick)
                                .ToString())
                        .ToString();
                }
                else
                {
                    status = definition.ActiveStatusKey.Translate(
                        activeWoundedAgent?.LabelShortCap ?? "?",
                        GetRoundedUpHours(
                            operationDeadlineTick - currentTick).ToString())
                        .ToString();
                }
            }
            else if (definition.Archetype
                == TokraOrganicOperationArchetype.MedicalSupplyHandoff)
            {
                if (activeMedicalSupplyLiaison == null)
                {
                    status = definition.ActiveStatusKey.Translate(
                        GetRoundedUpHours(
                            medicalSupplyArrivalTick - currentTick).ToString())
                        .ToString();
                }
                else if (activeState == TokraOrganicOperationState.Ready)
                {
                    status = definition.ReadyStatusKey.Translate(
                        activeMedicalSupplyLiaison.LabelShortCap,
                        GetRoundedUpHours(
                            operationDeadlineTick - currentTick).ToString())
                        .ToString();
                }
                else
                {
                    status = definition.GetRuntimeTextKey(
                            "statusApproaching")
                        .Translate(
                            activeMedicalSupplyLiaison.LabelShortCap,
                            GetRoundedUpHours(
                                operationDeadlineTick - currentTick).ToString())
                        .ToString();
                }
            }
            else if (definition.Archetype
                == TokraOrganicOperationArchetype.DeadDropRecovery)
            {
                if (intelligenceAnalysisMethod
                    == TokraIntelligenceAnalysisMethod.None)
                {
                    status = definition.GetRuntimeTextKey(
                            "statusAwaitingAnalysis")
                        .Translate(
                            GetRoundedUpHours(
                                operationDeadlineTick - currentTick)
                                .ToString())
                        .ToString();
                }
                else
                {
                    string methodKey = definition.GetRuntimeTextKey(
                        intelligenceAnalysisMethod
                            == TokraIntelligenceAnalysisMethod.Cautious
                            ? "methodCautious"
                            : "methodAccelerated");
                    status = definition.GetRuntimeTextKey("statusAnalyzing")
                        .Translate(
                            methodKey.Translate(),
                            GetRoundedUpHours(
                                operationDeadlineTick - currentTick)
                                .ToString())
                        .ToString();
                }
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

        private static void GrantSkillExperience(
            Pawn pawn,
            string skillDefName,
            int amount)
        {
            if (pawn == null
                || string.IsNullOrEmpty(skillDefName)
                || amount <= 0)
            {
                return;
            }

            SkillDef skillDef = DefDatabase<SkillDef>.GetNamedSilentFail(
                skillDefName);
            SkillRecord skill = skillDef != null
                ? pawn.skills?.GetSkill(skillDef)
                : null;

            if (skill != null && !skill.TotallyDisabled)
            {
                skill.Learn(amount, true);
            }
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

        private static void GrantMedicineExperience(Pawn pawn, int amount)
        {
            if (amount <= 0)
            {
                return;
            }

            SkillRecord medicine = pawn?.skills?.GetSkill(
                SkillDefOf.Medicine);

            if (medicine != null && !medicine.TotallyDisabled)
            {
                medicine.Learn(amount, true);
            }
        }

        private static void GrantSocialExperience(Pawn pawn, int amount)
        {
            if (amount <= 0)
            {
                return;
            }

            SkillRecord social = pawn?.skills?.GetSkill(SkillDefOf.Social);

            if (social != null && !social.TotallyDisabled)
            {
                social.Learn(amount, true);
            }
        }

        private void DestroyActiveObjective()
        {
            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();
            Map map = GetActiveMap();
            Thing objective = activeDeadDrop;
            Thing marker = observationPointMarker;
            activeDeadDrop = null;
            observationPointMarker = null;

            if (objective != null && !objective.Destroyed)
            {
                objective.Destroy(DestroyMode.Vanish);
            }

            if (marker != null && !marker.Destroyed)
            {
                marker.Destroy(DestroyMode.Vanish);
            }

            TokraOrganicOperationFramework.DestroyObjectives(map, definition);
            TokraObservationUtility.DestroyAllTargets(map, null, null);
        }

        private void ClearActiveOpportunity(
            bool destroyObjective = true,
            bool removeLivingPatient = true)
        {
            if (activeArchetype
                == TokraOrganicOperationArchetype.DistressCall)
            {
                TokraDistressCallMissionUtility.FindWorldSite(
                        activeOperation.frameworkRuntime)
                    ?.NotifyManagerResolved(false);
            }

            if (activeArchetype
                == TokraOrganicOperationArchetype.TemporaryBaseDelivery)
            {
                TokraTemporaryBaseDeliveryMissionUtility.FindWorldSite(
                        activeOperation.frameworkRuntime)
                    ?.NotifyManagerResolved(false);
            }

            if (activeArchetype
                == TokraOrganicOperationArchetype.JaffaOfficerCapture)
            {
                TokraJaffaOfficerCaptureTransferController.Cleanup(
                    jaffaOfficerCapture);
                TokraJaffaOfficerCaptureMissionUtility.FindWorldSite(
                        activeOperation.frameworkRuntime)
                    ?.NotifyManagerResolved(false);
            }

            if (destroyObjective)
            {
                DestroyActiveObjective();
            }

            if (removeLivingPatient)
            {
                TokraOrganicWoundedAgentUtility.RemoveLivingPatient(
                    activeWoundedAgent);
            }

            if (activeMedicalSupplyLiaison != null
                && activeMedicalSupplyLiaison
                    != departingMedicalSupplyLiaison)
            {
                TokraOrganicMedicalSupplyUtility.TryOrderDeparture(
                    activeMedicalSupplyLiaison);
            }

            activeOperation.Reset();
        }

        private void EnsureJaffaOfficerCaptureState()
        {
            if (activeOperation.jaffaOfficerCapture == null)
            {
                activeOperation.jaffaOfficerCapture
                    = new TokraJaffaOfficerCaptureTransferState();
            }
        }

        private void SyncJaffaOfficerCaptureStateFromWorldSite()
        {
            WorldObject_TokraJaffaOfficerCaptureSite site
                = TokraJaffaOfficerCaptureMissionUtility.FindWorldSite(
                    activeOperation?.frameworkRuntime);
            SyncJaffaOfficerCaptureStateFromSite(site);
        }

        private void SyncJaffaOfficerCaptureStateFromSite(
            WorldObject_TokraJaffaOfficerCaptureSite site)
        {
            EnsureJaffaOfficerCaptureState();

            if (site == null)
            {
                return;
            }

            site.MigrateTransferStateTo(jaffaOfficerCapture);

            if (operationDeadlineTick <= 0 && site.ExpiryTick > 0)
            {
                operationDeadlineTick = site.ExpiryTick;
            }
        }

        private void NormalizeLoadedState()
        {
            if (activeOperation == null)
            {
                activeOperation = new TokraOrganicOperationInstance();
            }

            if (followUp == null)
            {
                followUp = new TokraOrganicOperationFollowUp();
            }

            if (lastMissionTextVariantIndexes == null)
            {
                lastMissionTextVariantIndexes
                    = new Dictionary<string, int>();
            }

            if (activeOperation.frameworkRuntime == null)
            {
                activeOperation.frameworkRuntime
                    = new GateRimMissionRuntimeData();
            }

            EnsureJaffaOfficerCaptureState();

            if (activeArchetype
                == TokraOrganicOperationArchetype.JaffaOfficerCapture)
            {
                SyncJaffaOfficerCaptureStateFromWorldSite();
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            TokraOrganicOperationDefinition definition = GetActiveDefinition();

            if (activeOperation.IsActive)
            {
                communicatorGateBlocked = false;
                communicatorGateBlockedSinceTick = 0;
            }
            else if (!communicatorGateBlocked)
            {
                communicatorGateBlockedSinceTick = 0;
            }
            else
            {
                communicatorGateBlockedSinceTick = Math.Max(
                    0,
                    communicatorGateBlockedSinceTick);
            }

            if (!activeOperation.IsActive || definition == null)
            {
                activeOperation.Reset();
            }
            else
            {
                if (definition.UsesMissionFrameworkDef
                    && string.IsNullOrEmpty(
                        activeOperation.frameworkRuntime.missionDefName))
                {
                    activeOperation.frameworkRuntime.missionDefName
                        = definition.MissionDefName;
                    activeOperation.frameworkRuntime.phaseId
                        = activeState.ToString().ToLowerInvariant();

                    GateRimMissionDifficultySnapshot snapshot
                        = GateRimMissionFramework.CaptureDifficulty(
                            GetActiveMap(),
                            definition.MissionDef?.difficulty);
                    activeOperation.frameworkRuntime.baseThreatPoints
                        = snapshot.BaseThreatPoints;
                    activeOperation.frameworkRuntime.scaledThreatPoints
                        = snapshot.ScaledThreatPoints;
                    activeOperation.frameworkRuntime.difficultyFactor
                        = snapshot.Factor;
                }

                if (activeState == TokraOrganicOperationState.Offered
                    && offerExpiryTick <= 0)
                {
                    offerExpiryTick = Math.Max(currentTick, offerCreatedTick)
                        + definition.OfferDurationTicks;
                }

                if ((activeState == TokraOrganicOperationState.Accepted
                        || activeState == TokraOrganicOperationState.Ready)
                    && operationDeadlineTick <= 0
                    && activeArchetype
                        != TokraOrganicOperationArchetype.MedicalSupplyHandoff
                    && activeArchetype
                        != TokraOrganicOperationArchetype
                            .DecoyTransmissionDefense)
                {
                    operationDeadlineTick = Math.Max(currentTick, acceptedTick)
                        + definition.DeadlineTicks;
                }



                if (activeArchetype
                        == TokraOrganicOperationArchetype
                            .DecoyTransmissionDefense
                    && activeState == TokraOrganicOperationState.Accepted)
                {
                    TokraDiversionAssaultUtility.Normalize(
                        activeOperation.frameworkRuntime,
                        definition,
                        acceptedTick,
                        currentTick);

                    if (TokraDiversionAssaultUtility.IsRaidTriggered(
                            activeOperation.frameworkRuntime))
                    {
                        operationDeadlineTick = 0;
                    }
                }
                if (activeArchetype
                        == TokraOrganicOperationArchetype.GoauldObservation
                    && (activeState == TokraOrganicOperationState.Accepted
                        || activeState == TokraOrganicOperationState.Ready))
                {
                    if (observationTransmissionTotalTicks <= 0)
                    {
                        observationTransmissionTotalTicks = 0;
                        observationTransmissionRemainingTicks = 0;
                    }
                    else
                    {
                        observationTransmissionTotalTicks
                            = ObservationTransmissionWorkTicks;
                        observationTransmissionRemainingTicks = Math.Max(
                            0,
                            Math.Min(
                                observationTransmissionTotalTicks,
                                observationTransmissionRemainingTicks));
                    }
                }

                if (activeArchetype
                        == TokraOrganicOperationArchetype.DeadDropRecovery
                    && (activeState == TokraOrganicOperationState.Accepted
                        || activeState == TokraOrganicOperationState.Ready)
                    && intelligenceAnalysisMethod
                        != TokraIntelligenceAnalysisMethod.None)
                {
                    int expectedTotal
                        = GetConfiguredIntelligenceWorkTicks(
                            intelligenceAnalysisMethod);

                    if (activeState
                        == TokraOrganicOperationState.Accepted)
                    {
                        SetIntelligenceFrameworkPhase(
                            intelligenceAnalysisMethod);
                    }

                    if (intelligenceWorkTotalTicks <= 0)
                    {
                        intelligenceWorkTotalTicks = expectedTotal;
                    }

                    intelligenceWorkRemainingTicks = Math.Max(
                        0,
                        Math.Min(
                            intelligenceWorkTotalTicks,
                            intelligenceWorkRemainingTicks));

                    if (activeState == TokraOrganicOperationState.Ready)
                    {
                        intelligenceWorkRemainingTicks = 0;
                    }
                }
            }

            TokraOrganicOperationFramework.DestroyAllObjectivesExcept(
                activeDeadDrop);
            TokraObservationUtility.DestroyAllTargets(
                GetActiveMap(),
                activeArchetype
                        == TokraOrganicOperationArchetype.GoauldObservation
                    ? activeDeadDrop
                    : null,
                observationPointMarker);
        }

        private bool IsDiversionAssaultStateActive()
        {
            return activeArchetype
                    == TokraOrganicOperationArchetype
                        .DecoyTransmissionDefense
                && activeState == TokraOrganicOperationState.Accepted
                && TokraDiversionAssaultUtility.IsRaidTriggered(
                    activeOperation.frameworkRuntime);
        }

        private bool IsObservationStateActive()
        {
            return activeArchetype
                    == TokraOrganicOperationArchetype.GoauldObservation
                && (activeState == TokraOrganicOperationState.Accepted
                    || activeState == TokraOrganicOperationState.Ready);
        }

        private bool IsMedicalSupplyStateActive()
        {
            return activeArchetype
                    == TokraOrganicOperationArchetype.MedicalSupplyHandoff
                && (activeState == TokraOrganicOperationState.Accepted
                    || activeState == TokraOrganicOperationState.Ready);
        }



        private static int GetRoundedUpHours(int ticks)
        {
            return Math.Max(0, (int)Math.Ceiling(Math.Max(0, ticks) / 2500f));
        }

        private static GameComponent_TokraOrganicOperationManager
            GetCurrentManager()
        {
            return Current.Game?.GetComponent<
                GameComponent_TokraOrganicOperationManager>();
        }

        private struct OrganicOperationCandidate
        {
            public OrganicOperationCandidate(
                TokraOrganicOperationArchetype archetype,
                float weight,
                float repeatedArchetypeWeightFactor)
            {
                Archetype = archetype;
                Weight = weight;
                RepeatedArchetypeWeightFactor
                    = repeatedArchetypeWeightFactor;
            }

            public TokraOrganicOperationArchetype Archetype;
            public float Weight;
            public float RepeatedArchetypeWeightFactor;
        }
    }

}
