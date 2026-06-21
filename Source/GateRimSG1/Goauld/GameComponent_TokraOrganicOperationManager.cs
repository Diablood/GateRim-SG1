using System;
using System.Collections.Generic;
using System.Linq;
using GateRimSG1.Missions;
using RimWorld;
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
        internal const int ObservationStateCheckIntervalTicks = 250;
        internal const int MedicalSupplyStateCheckIntervalTicks = 250;
        internal const int ObservationDeploymentWorkTicks = 500;
        internal const int LegacyObservationWorkTicks = 5000;
        internal const int ObservationDurationTicks = LegacyObservationWorkTicks;
        internal const int ObservationRecoveryWorkTicks = 500;
        internal const int ObservationTransmissionWorkTicks = 1000;
        private const string ObservationDeploymentJobDefName
            = "SG1_DeployTokraObservationDevice";
        private const string ObservationTransmissionJobDefName
            = "SG1_TransmitTokraObservationData";
        private const int InitialMinimumDelayTicks = 180000;
        private const int InitialMaximumDelayTicks = 360000;
        private const int WoundedAgentStableDurationTicks = 5000;
        private const int WoundedAgentDepartureGraceTicks = 60000;
        private const int MedicalSupplyArrivalMinimumDelayTicks = 2500;
        private const int MedicalSupplyArrivalMaximumDelayTicks = 5000;
        private const int MedicalSupplyDepartureGraceTicks = 60000;
        private const string IntelligenceAnalysisJobDefName
            = "SG1_AnalyzeTokraOrganicIntelligence";
        private const int IntelligenceCautiousWorkTicks = 5000;
        private const int IntelligenceAcceleratedWorkTicks = 2000;
        private const int IntelligenceAcceleratedXpBonus = 150;
        private const float IntelligenceInterferenceChance = 0.35f;
        private const int IntelligencePatrolDelayMinimumTicks = 5000;
        private const int IntelligencePatrolDelayMaximumTicks = 12500;
        private const int IntelligencePatrolRetryTicks = 2500;
        private const float IntelligencePatrolThreatFactor = 0.35f;
        private const float IntelligencePatrolMinimumPoints = 180f;
        private const float IntelligencePatrolMaximumPoints = 700f;

        private int nextStateCheckTick;
        private int nextOpportunityTick;
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

            if (Find.TickManager == null)
            {
                return;
            }

            int currentTick = Find.TickManager.TicksGame;

            if (currentTick < nextStateCheckTick)
            {
                return;
            }

            int stateCheckInterval = IsMedicalSupplyStateActive()
                ? MedicalSupplyStateCheckIntervalTicks
                : IsObservationStateActive()
                    ? ObservationStateCheckIntervalTicks
                    : StateCheckIntervalTicks;
            nextStateCheckTick = currentTick + stateCheckInterval;
            TickDepartingMedicalSupplyLiaison();

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
                key = "GR_TokraObservation_ResumeTransmissionAction";
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
                == TokraOrganicOperationArchetype.GoauldObservation)
            {
                if (!manager.HasValidObservationDevice())
                {
                    return "GR_TokraObservation_DeviceLost"
                        .Translate()
                        .ToString();
                }

                if (manager.operationDeadlineTick > 0
                    && currentTick >= manager.operationDeadlineTick)
                {
                    return "GR_TokraOrganicOperation_ObservationExpired"
                        .Translate()
                        .ToString();
                }

                return manager.activeState == TokraOrganicOperationState.Ready
                    ? null
                    : "GR_TokraObservation_DataNotReady"
                        .Translate()
                        .ToString();
            }

            if (manager.activeArchetype
                == TokraOrganicOperationArchetype.DeadDropRecovery)
            {
                if (!manager.HasValidIntelligenceObjective())
                {
                    return "GR_TokraOrganicOperation_DeadDropNoLongerActive"
                        .Translate()
                        .ToString();
                }

                if (manager.operationDeadlineTick > 0
                    && currentTick >= manager.operationDeadlineTick)
                {
                    return "GR_TokraOrganicOperation_DeadDropWindowExpired"
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
                return "GR_TokraOrganicOperation_DeadDropNoLongerActive"
                    .Translate()
                    .ToString();
            }

            if (manager.IsOperationDeadlineExpired())
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

            if (!manager.IsExactMedicalSupplyLiaison(liaison))
            {
                return "GR_TokraMedicalSupply_NoLongerActive"
                    .Translate()
                    .ToString();
            }

            if (manager.activeState != TokraOrganicOperationState.Ready)
            {
                return "GR_TokraMedicalSupply_LiaisonEnRoute"
                    .Translate()
                    .ToString();
            }

            if (manager.IsOperationDeadlineExpired())
            {
                return "GR_TokraMedicalSupply_WindowExpired"
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

            SkillRecord social = negotiator.skills?.GetSkill(
                SkillDefOf.Social);

            if (social == null || social.TotallyDisabled)
            {
                return "GR_TokraMedicalSupply_OperatorIncapable"
                    .Translate()
                    .ToString();
            }

            if (!negotiator.CanReach(
                    liaison,
                    Verse.AI.PathEndMode.Touch,
                    Danger.Some))
            {
                return "GR_TokraMedicalSupply_CannotReachLiaison"
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

            if (!TokraOrganicMedicalSupplyUtility
                .HasEnoughIndustrialMedicine(map, negotiator))
            {
                Messages.Message(
                    "GR_TokraMedicalSupply_NeedMedicine".Translate(
                        TokraOrganicMedicalSupplyUtility
                            .RequiredMedicineCount.ToString()),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            if (!TokraOrganicMedicalSupplyUtility
                .TryConsumeIndustrialMedicine(map, negotiator))
            {
                Messages.Message(
                    "GR_TokraMedicalSupply_NeedMedicine".Translate(
                        TokraOrganicMedicalSupplyUtility
                            .RequiredMedicineCount.ToString()),
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
                return "Tok'ra organic operation: none active on this map."
                    + "\nNext opportunity tick: "
                    + manager.nextOpportunityTick
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
                    = currentTick - WoundedAgentStableDurationTicks;
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
                        = IntelligenceCautiousWorkTicks;
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
            manager.intelligenceWorkTotalTicks
                = IntelligenceAcceleratedWorkTicks;
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
            manager.intelligenceWorkTotalTicks = method
                == TokraIntelligenceAnalysisMethod.Cautious
                ? IntelligenceCautiousWorkTicks
                : IntelligenceAcceleratedWorkTicks;
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
                    = "GR_TokraOrganicOperation_DeadDropTimedOutLetterText";
            }
            else if (manager.activeArchetype
                == TokraOrganicOperationArchetype.WoundedAgentCare)
            {
                failureTextKey = "GR_TokraWoundedAgent_FailedTimeoutText";
            }
            else if (manager.activeArchetype
                == TokraOrganicOperationArchetype.MedicalSupplyHandoff)
            {
                failureTextKey = "GR_TokraMedicalSupply_TimedOutText";
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
            GameComponent_TokraTrustTracker
                .NotifyOrganicMedicalSupplyLiaisonDeath();

            Find.LetterStack?.ReceiveLetter(
                "GR_TokraMedicalSupply_PostHandoffDeathLabel".Translate(),
                "GR_TokraMedicalSupply_PostHandoffDeathText".Translate(
                    liaison?.LabelShortCap ?? "?"),
                LetterDefOf.NegativeEvent,
                liaison);

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
            int currentTick = Find.TickManager?.TicksGame ?? 0;
            manager.nextOpportunityTick = currentTick + Rand.RangeInclusive(
                InitialMinimumDelayTicks,
                InitialMaximumDelayTicks);
            return true;
        }

        private static bool DebugForceSpecificOpportunity(
            Map map,
            TokraOrganicOperationArchetype archetype)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            if (manager == null
                || map == null
                || FindPoweredCommunicator(map) == null)
            {
                return false;
            }

            manager.ClearActiveOpportunity();
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
                        ? "GR_TokraWoundedAgent_FailedLostText"
                        : definition.Archetype
                            == TokraOrganicOperationArchetype
                                .MedicalSupplyHandoff
                            ? "GR_TokraMedicalSupply_LiaisonLostText"
                            : definition.Archetype
                                == TokraOrganicOperationArchetype
                                    .GoauldObservation
                                ? "GR_TokraObservation_FailedDeviceLostText"
                                : definition.HasPhysicalObjective
                                    ? "GR_TokraOrganicOperation_DeadDropLostLetterText"
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
                    "GR_TokraMedicalSupply_TimedOutText");
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

                if (!TokraOrganicMedicalSupplyUtility.TrySpawnLiaison(
                        GetActiveMap(),
                        definition.DeadlineTicks
                            + MedicalSupplyDepartureGraceTicks,
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
                    "GR_TokraMedicalSupply_ArrivalLabel".Translate(),
                    "GR_TokraMedicalSupply_ArrivalText".Translate(
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
                    "GR_TokraMedicalSupply_LiaisonDeathText");
                return;
            }

            if (activeLiaison.Destroyed
                || activeLiaison.MapHeld == null
                || activeLiaison.MapHeld.uniqueID != activeMapId)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    activeLiaison,
                    "GR_TokraMedicalSupply_LiaisonLostText");
                return;
            }

            if (activeLiaison.IsPrisonerOfColony)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    activeLiaison,
                    "GR_TokraMedicalSupply_LiaisonCapturedText");
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
                    "GR_TokraMedicalSupply_LiaisonReady".Translate(
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
                    GameComponent_TokraTrustTracker
                        .NotifyOrganicMedicalSupplyLiaisonDeath();

                    Find.LetterStack?.ReceiveLetter(
                        "GR_TokraMedicalSupply_PostHandoffDeathLabel"
                            .Translate(),
                        "GR_TokraMedicalSupply_PostHandoffDeathText"
                            .Translate(liaison.LabelShortCap),
                        LetterDefOf.NegativeEvent,
                        liaison);
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
            Pawn patient = activeWoundedAgent;

            if (patient == null)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    null,
                    "GR_TokraWoundedAgent_FailedLostText");
                return;
            }

            if (patient.Dead)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    patient,
                    "GR_TokraWoundedAgent_FailedDeathText");
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
                        "GR_TokraWoundedAgent_FailedCapturedText");
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
                        "GR_TokraWoundedAgent_FailedLostText");
                    return;
                }

                if (woundedAgentDepartureDeadlineTick > 0
                    && currentTick >= woundedAgentDepartureDeadlineTick)
                {
                    TryResolveActiveOperation(
                        TokraOrganicOperationOutcome.Failed,
                        patient,
                        "GR_TokraWoundedAgent_FailedTimeoutText");
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
                    "GR_TokraWoundedAgent_FailedLostText");
                return;
            }

            if (patient.IsPrisonerOfColony)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    patient,
                    "GR_TokraWoundedAgent_FailedCapturedText");
                return;
            }

            if (operationDeadlineTick > 0
                && currentTick >= operationDeadlineTick)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    patient,
                    "GR_TokraWoundedAgent_FailedTimeoutText");
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

                Messages.Message(
                    "GR_TokraWoundedAgent_InitialCareMessage".Translate(
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
                    "GR_TokraWoundedAgent_StableMessage".Translate(
                        patient.LabelShortCap),
                    patient,
                    MessageTypeDefOf.PositiveEvent,
                    historical: true);
                return;
            }

            if (currentTick - woundedAgentStableSinceTick
                < WoundedAgentStableDurationTicks)
            {
                return;
            }

            if (!TokraOrganicWoundedAgentUtility.TryOrderDeparture(patient))
            {
                return;
            }

            woundedAgentDepartureOrdered = true;
            woundedAgentDepartureDeadlineTick
                = currentTick + WoundedAgentDepartureGraceTicks;
            activeState = TokraOrganicOperationState.Ready;
            readyNotificationSent = true;

            Messages.Message(
                "GR_TokraWoundedAgent_DepartingMessage".Translate(
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
                    "GR_TokraObservation_FailedDeviceLostText");
                return;
            }

            if (operationDeadlineTick > 0
                && currentTick >= operationDeadlineTick)
            {
                TryResolveActiveOperation(
                    TokraOrganicOperationOutcome.Failed,
                    null,
                    "GR_TokraObservation_FailedTimeoutText");
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
                    "GR_TokraObservation_DataReady".Translate(),
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

            GateRimMissionWorker missionWorker
                = definition.MissionDef?.Worker;

            if (missionWorker != null && !missionWorker.CanOffer(map))
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
                offerLetterTextKey.Translate(
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
                return "GR_TokraObservation_NoActiveDeployment"
                    .Translate()
                    .ToString();
            }

            if (observationDeviceDeployed)
            {
                return "GR_TokraObservation_AlreadyDeployed"
                    .Translate()
                    .ToString();
            }

            if (IsOperationDeadlineExpired())
            {
                return "GR_TokraOrganicOperation_ObservationExpired"
                    .Translate()
                    .ToString();
            }

            if (!CanUseIntellectualOperator(operatorPawn))
            {
                return "GR_TokraOrganicOperation_OperatorIncapable"
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
                return "GR_TokraObservation_DeviceLost"
                    .Translate()
                    .ToString();
            }

            if (communicator == null)
            {
                return "GR_TokraSecureCommunicator_Unpowered"
                    .Translate()
                    .ToString();
            }

            if (device.Spawned
                && !operatorPawn.CanReserveAndReach(
                    device,
                    PathEndMode.ClosestTouch,
                    Danger.Some))
            {
                return "GR_TokraObservation_CannotReachDevice"
                    .Translate()
                    .ToString();
            }

            if (!operatorPawn.CanReach(
                    observationPoint,
                    PathEndMode.Touch,
                    Danger.Some))
            {
                return "GR_TokraObservation_CannotReachPoint"
                    .Translate()
                    .ToString();
            }

            if (!operatorPawn.CanReach(
                    communicator,
                    PathEndMode.Touch,
                    Danger.Some))
            {
                return "GR_TokraObservation_CannotReachCommunicator"
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

            if (!CanUseIntellectualOperator(operatorPawn)
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
                "GR_TokraObservation_Deployed".Translate(
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
                || !CanUseIntellectualOperator(operatorPawn))
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
                operatorPawn.skills?.Learn(
                    SkillDefOf.Intellectual,
                    0.04f);
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
                "GR_TokraObservation_DataReady".Translate(),
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
                return "GR_TokraObservation_NoReadyRecovery"
                    .Translate()
                    .ToString();
            }

            if (IsOperationDeadlineExpired())
            {
                return "GR_TokraOrganicOperation_ObservationExpired"
                    .Translate()
                    .ToString();
            }

            if (!CanUseIntellectualOperator(operatorPawn))
            {
                return "GR_TokraOrganicOperation_OperatorIncapable"
                    .Translate()
                    .ToString();
            }

            ThingWithComps communicator = FindPoweredCommunicator(
                GetActiveMap());

            if (communicator == null)
            {
                return "GR_TokraSecureCommunicator_Unpowered"
                    .Translate()
                    .ToString();
            }

            if (!operatorPawn.CanReserveAndReach(
                    observationPoint,
                    PathEndMode.Touch,
                    Danger.Some))
            {
                return "GR_TokraObservation_CannotReachPoint"
                    .Translate()
                    .ToString();
            }

            if (!operatorPawn.CanReach(
                    communicator,
                    PathEndMode.Touch,
                    Danger.Some))
            {
                return "GR_TokraObservation_CannotReachCommunicator"
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
                && CanUseIntellectualOperator(operatorPawn);
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
            JobDef jobDef = DefDatabase<JobDef>.GetNamedSilentFail(
                ObservationTransmissionJobDefName);

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
                ? "GR_TokraObservation_RecoveryStarted"
                : "GR_TokraObservation_ObservationStarted";

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

            JobDef jobDef = DefDatabase<JobDef>.GetNamedSilentFail(
                ObservationTransmissionJobDefName);

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
                "GR_TokraObservation_TransmissionStarted".Translate(
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
                && CanUseIntellectualOperator(operatorPawn);
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
                intelligenceWorkTotalTicks = method
                    == TokraIntelligenceAnalysisMethod.Cautious
                    ? IntelligenceCautiousWorkTicks
                    : IntelligenceAcceleratedWorkTicks;
                intelligenceWorkRemainingTicks
                    = intelligenceWorkTotalTicks;
                intelligenceInterferenceRollResolved = false;
                intelligenceInterferenceTriggered = false;
                intelligencePatrolQueued = false;
                intelligenceResultVariant = -1;

                Messages.Message(
                    (method == TokraIntelligenceAnalysisMethod.Cautious
                        ? "GR_TokraOrganicOperation_IntelligenceCautiousStarted"
                        : "GR_TokraOrganicOperation_IntelligenceAcceleratedStarted")
                        .Translate(operatorPawn.LabelShortCap),
                    communicator,
                    MessageTypeDefOf.NeutralEvent,
                    historical: true);
            }
            else if (intelligenceAnalysisMethod != method)
            {
                Messages.Message(
                    "GR_TokraOrganicOperation_IntelligenceMethodLocked"
                        .Translate(),
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
            JobDef jobDef = DefDatabase<JobDef>.GetNamedSilentFail(
                IntelligenceAnalysisJobDefName);
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
                    "GR_TokraOrganicOperation_IntelligenceJobUnavailable"
                        .Translate(),
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

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            int arrivalDelay = Rand.RangeInclusive(
                MedicalSupplyArrivalMinimumDelayTicks,
                MedicalSupplyArrivalMaximumDelayTicks);

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
                "GR_TokraMedicalSupply_Accepted".Translate(
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

            if (definition == null
                || !TokraOrganicWoundedAgentUtility.TrySpawnPatient(
                    map,
                    definition.DeadlineTicks
                        + WoundedAgentDepartureGraceTicks,
                    out patient))
            {
                Messages.Message(
                    "GR_TokraWoundedAgent_SpawnFailed".Translate(),
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
                "GR_TokraWoundedAgent_AcceptedMessage".Translate(
                    operatorPawn?.LabelShortCap ?? "?",
                    patient.LabelShortCap),
                patient,
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            Find.LetterStack?.ReceiveLetter(
                "GR_TokraWoundedAgent_ArrivalLabel".Translate(),
                "GR_TokraWoundedAgent_ArrivalText".Translate(
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

            if (definition == null
                || !CanUseIntellectualOperator(operatorPawn)
                || !TokraObservationUtility.TryCreateOperationTargets(
                    map,
                    out device,
                    out marker,
                    out targetCell))
            {
                Messages.Message(
                    "GR_TokraObservation_SpawnFailed".Translate(),
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
                "GR_TokraObservation_Accepted".Translate(
                    operatorPawn?.LabelShortCap ?? "?",
                    GetRoundedUpHours(definition.DeadlineTicks).ToString()),
                marker,
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            Find.LetterStack?.ReceiveLetter(
                "GR_TokraObservation_TargetLetterLabel".Translate(),
                "GR_TokraObservation_TargetLetterText".Translate(
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
            intelligenceAnalysisMethod
                = TokraIntelligenceAnalysisMethod.None;
            intelligenceWorkTotalTicks = 0;
            intelligenceWorkRemainingTicks = 0;
            intelligenceInterferenceRollResolved = false;
            intelligenceInterferenceTriggered = false;
            intelligencePatrolQueued = false;
            intelligenceResultVariant = -1;

            NotifyMissionAccepted(map, operatorPawn);

            string acceptedKey
                = "GR_TokraOrganicOperation_DeadDropAccepted";
            string locatedLabelKey
                = "GR_TokraOrganicOperation_DeadDropLocatedLetterLabel";
            string locatedTextKey
                = "GR_TokraOrganicOperation_DeadDropLocatedLetterText";
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
            bool bypassSuccessValidation = false)
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
                intelligenceWorkTotalTicks = IntelligenceCautiousWorkTicks;
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

            if (isIntelligence
                && intelligenceAnalysisMethod
                    == TokraIntelligenceAnalysisMethod.Accelerated)
            {
                intellectualXp += IntelligenceAcceleratedXpBonus;
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
                GrantIntellectualExperience(
                    operatorPawn,
                    intellectualXp);
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
                outcome);

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

            GR_Log.Message(
                "Resolved Tok'ra organic operation "
                + $"{definition.DebugLabel} with outcome {outcome}; "
                + $"map {activeMapId}; operator "
                + $"{operatorPawn?.LabelShortCap ?? "none"}; "
                + $"patient {patient?.LabelShortCap ?? "none"}; "
                + $"Intellectual XP "
                + $"{(outcome == TokraOrganicOperationOutcome.Succeeded ? intellectualXp : 0)}; "
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
                        == "GR_TokraWoundedAgent_FailedCapturedText");

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
                == TokraOrganicOperationArchetype.WoundedAgentCare)
            {
                if (outcome == TokraOrganicOperationOutcome.Succeeded)
                {
                    Find.LetterStack?.ReceiveLetter(
                        "GR_TokraWoundedAgent_SuccessLabel".Translate(),
                        "GR_TokraWoundedAgent_SuccessText".Translate(
                            patient?.LabelShortCap ?? "?"),
                        LetterDefOf.PositiveEvent,
                        letterTarget);
                }
                else
                {
                    string textKey = string.IsNullOrEmpty(failureTextKey)
                        ? "GR_TokraWoundedAgent_FailedTimeoutText"
                        : failureTextKey;

                    Find.LetterStack?.ReceiveLetter(
                        "GR_TokraWoundedAgent_FailedLabel".Translate(),
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
                        "GR_TokraOrganicOperation_SuccessLetterLabel"
                            .Translate(),
                        GetObservationSuccessTextKey().Translate(
                            operatorPawn?.LabelShortCap ?? "?",
                            definition.IntellectualXp.ToString()),
                        LetterDefOf.PositiveEvent,
                        letterTarget);
                }
                else
                {
                    string observationFailureKey
                        = string.IsNullOrEmpty(failureTextKey)
                            ? "GR_TokraObservation_FailedTimeoutText"
                            : failureTextKey;

                    Find.LetterStack?.ReceiveLetter(
                        "GR_TokraOrganicOperation_FailedLetterLabel"
                            .Translate(),
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
                        "GR_TokraMedicalSupply_SuccessLabel".Translate(),
                        "GR_TokraMedicalSupply_SuccessText".Translate(
                            operatorPawn?.LabelShortCap ?? "?",
                            definition.SocialXp.ToString()),
                        LetterDefOf.PositiveEvent,
                        letterTarget);
                }
                else
                {
                    string textKey = string.IsNullOrEmpty(failureTextKey)
                        ? "GR_TokraMedicalSupply_TimedOutText"
                        : failureTextKey;

                    Find.LetterStack?.ReceiveLetter(
                        "GR_TokraMedicalSupply_FailedLabel".Translate(),
                        textKey.Translate(),
                        LetterDefOf.NegativeEvent,
                        letterTarget);
                }

                return;
            }

            if (outcome == TokraOrganicOperationOutcome.Succeeded)
            {
                Find.LetterStack?.ReceiveLetter(
                    "GR_TokraOrganicOperation_DeadDropSuccessLetterLabel"
                        .Translate(),
                    GetIntelligenceSuccessTextKey().Translate(
                        operatorPawn?.LabelShortCap ?? "?",
                        intellectualXp.ToString()),
                    intelligencePatrolQueued
                        ? LetterDefOf.ThreatSmall
                        : LetterDefOf.PositiveEvent,
                    letterTarget);
                return;
            }

            string deadDropTextKey = string.IsNullOrEmpty(failureTextKey)
                ? "GR_TokraOrganicOperation_DeadDropTimedOutLetterText"
                : failureTextKey;

            Find.LetterStack?.ReceiveLetter(
                "GR_TokraOrganicOperation_DeadDropFailedLetterLabel"
                    .Translate(),
                deadDropTextKey.Translate(),
                LetterDefOf.NegativeEvent);
        }

        private void PrepareObservationOutcome()
        {
            if (observationResultVariant >= 0)
            {
                return;
            }

            int variant = Rand.Range(0, 3);

            if (variant == lastObservationResultVariant)
            {
                variant = (variant + Rand.RangeInclusive(1, 2)) % 3;
            }

            observationResultVariant = variant;
            lastObservationResultVariant = variant;
        }

        private string GetObservationSuccessTextKey()
        {
            int variant = Math.Max(0, Math.Min(2, observationResultVariant))
                + 1;

            return "GR_TokraObservation_SuccessText" + variant;
        }

        private void PrepareIntelligenceOutcome()
        {
            if (intelligenceResultVariant < 0)
            {
                intelligenceResultVariant = SelectIntelligenceResultVariant();
                lastIntelligenceResultVariant = intelligenceResultVariant;
            }

            if (intelligenceInterferenceRollResolved)
            {
                return;
            }

            intelligenceInterferenceRollResolved = true;

            if (intelligenceAnalysisMethod
                    != TokraIntelligenceAnalysisMethod.Accelerated
                || !Rand.Chance(IntelligenceInterferenceChance))
            {
                intelligenceInterferenceTriggered = false;
                intelligencePatrolQueued = false;
                return;
            }

            intelligencePatrolQueued = TryQueueIntelligencePatrol(
                GetActiveMap());
            intelligenceInterferenceTriggered = intelligencePatrolQueued;
        }

        private int SelectIntelligenceResultVariant()
        {
            int variant = Rand.Range(0, 3);

            if (variant == lastIntelligenceResultVariant)
            {
                variant = (variant + Rand.RangeInclusive(1, 2)) % 3;
            }

            return variant;
        }

        private string GetIntelligenceSuccessTextKey()
        {
            int variant = Math.Max(0, Math.Min(2, intelligenceResultVariant))
                + 1;

            if (intelligenceAnalysisMethod
                == TokraIntelligenceAnalysisMethod.Cautious)
            {
                return "GR_TokraOrganicOperation_IntelligenceCautiousSuccess"
                    + variant;
            }

            if (intelligencePatrolQueued)
            {
                return "GR_TokraOrganicOperation_IntelligenceInterferenceSuccess"
                    + variant;
            }

            return "GR_TokraOrganicOperation_IntelligenceAcceleratedSuccess"
                + variant;
        }

        private bool TryQueueIntelligencePatrol(Map map)
        {
            IncidentDef patrolDef = GR_DefOf.SG1_GoauldJaffaSignalPatrol;

            if (map == null
                || patrolDef == null
                || patrolDef.category == null
                || Find.Storyteller?.incidentQueue == null)
            {
                GR_Log.Warning(
                    "Could not queue Goa'uld patrol after accelerated Tok'ra "
                    + "intelligence analysis: incident definition, map or "
                    + "storyteller queue is unavailable.");
                return false;
            }

            IncidentParms parms = StorytellerUtility.DefaultParmsNow(
                patrolDef.category,
                map);
            float basePoints = parms.points > 0f
                ? parms.points
                : StorytellerUtility.DefaultThreatPointsNow(map);

            parms.forced = true;
            parms.faction = GoauldSystemLordFactionUtility.GetOrCreateFaction(
                "Tok'ra accelerated intelligence interference patrol");
            parms.points = Math.Max(
                IntelligencePatrolMinimumPoints,
                Math.Min(
                    IntelligencePatrolMaximumPoints,
                    basePoints * IntelligencePatrolThreatFactor));

            int fireTick = (Find.TickManager?.TicksGame ?? 0)
                + Rand.RangeInclusive(
                    IntelligencePatrolDelayMinimumTicks,
                    IntelligencePatrolDelayMaximumTicks);

            Find.Storyteller.incidentQueue.Add(
                patrolDef,
                fireTick,
                parms,
                IntelligencePatrolRetryTicks);

            GR_Log.Message(
                "Queued a small Goa'uld patrol after accelerated Tok'ra "
                + $"intelligence analysis; map={map.uniqueID}; "
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
                        definition.GetWeight(tier),
                        definition.RepeatedArchetypeWeightFactor))
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
                        candidate.Weight *= candidate
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

                if (map != null
                    && map.IsPlayerHome
                    && FindPoweredCommunicator(map) != null)
                {
                    return map;
                }
            }

            return null;
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
                = GetActiveDefinition()?.ObservationWorkTicks
                    ?? LegacyObservationWorkTicks;

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
            int restoredDeadline = definition != null
                ? definition.DeadlineTicks
                : ObservationDurationTicks + ObservationTransmissionWorkTicks;
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
            else if (definition.Archetype
                == TokraOrganicOperationArchetype.GoauldObservation)
            {
                if (!observationDeviceDeployed)
                {
                    status = "GR_TokraObservation_StatusAwaitingDeployment"
                        .Translate(
                            GetRoundedUpHours(
                                operationDeadlineTick - currentTick)
                                .ToString())
                        .ToString();
                }
                else if (activeState == TokraOrganicOperationState.Accepted)
                {
                    status = "GR_TokraObservation_StatusRecording"
                        .Translate(
                            GetRoundedUpHours(
                                observationWorkRemainingTicks)
                                .ToString())
                        .ToString();
                }
                else if (observationTransmissionTotalTicks > 0
                    && observationTransmissionRemainingTicks > 0)
                {
                    status = "GR_TokraObservation_StatusTransmissionInterrupted"
                        .Translate(
                            GetRoundedUpHours(
                                operationDeadlineTick - currentTick)
                                .ToString())
                        .ToString();
                }
                else
                {
                    status = "GR_TokraObservation_StatusDataReady"
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
                    status = "GR_TokraWoundedAgent_StatusAwaitingCare"
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
                    status = "GR_TokraMedicalSupply_StatusApproaching"
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
                    status = "GR_TokraOrganicOperation_StatusIntelligenceAwaitingAnalysis"
                        .Translate(
                            GetRoundedUpHours(
                                operationDeadlineTick - currentTick)
                                .ToString())
                        .ToString();
                }
                else
                {
                    string methodKey = intelligenceAnalysisMethod
                        == TokraIntelligenceAnalysisMethod.Cautious
                        ? "GR_TokraOrganicOperation_IntelligenceMethodCautious"
                        : "GR_TokraOrganicOperation_IntelligenceMethodAccelerated";
                    status = "GR_TokraOrganicOperation_StatusIntelligenceAnalyzing"
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

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            TokraOrganicOperationDefinition definition = GetActiveDefinition();

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
                        != TokraOrganicOperationArchetype.MedicalSupplyHandoff)
                {
                    operationDeadlineTick = Math.Max(currentTick, acceptedTick)
                        + definition.DeadlineTicks;
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
                    int expectedTotal = intelligenceAnalysisMethod
                        == TokraIntelligenceAnalysisMethod.Cautious
                        ? IntelligenceCautiousWorkTicks
                        : IntelligenceAcceleratedWorkTicks;

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
