using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

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
        internal const int MedicalSupplyStateCheckIntervalTicks = 250;
        private const int InitialMinimumDelayTicks = 180000;
        private const int InitialMaximumDelayTicks = 360000;
        private const int WoundedAgentStableDurationTicks = 5000;
        private const int WoundedAgentDepartureGraceTicks = 60000;
        private const int MedicalSupplyArrivalMinimumDelayTicks = 2500;
        private const int MedicalSupplyArrivalMaximumDelayTicks = 5000;
        private const int MedicalSupplyDepartureGraceTicks = 60000;

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

        private TokraOrganicOperationArchetype activeArchetype
        {
            get => activeOperation.archetype;
            set => activeOperation.archetype = value;
        }

        private TokraOrganicOperationState activeState
        {
            get => activeOperation.state;
            set => activeOperation.state = value;
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

            string key = manager.activeState
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
                + "\nWounded agent: "
                + (manager.activeWoundedAgent?.LabelShortCap ?? "none")
                + "\nMedical liaison: "
                + (manager.activeMedicalSupplyLiaison?.LabelShortCap ?? "none")
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
                TokraOrganicWoundedAgentUtility.RemoveSymbioteShock(patient);
                manager.woundedAgentStableSinceTick
                    = currentTick - WoundedAgentStableDurationTicks;
                manager.TickAcceptedWoundedAgent(currentTick);
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

        public static bool DebugMakeObservationReady(Map map)
        {
            GameComponent_TokraOrganicOperationManager manager
                = GetCurrentManager();

            return manager != null
                && manager.activeArchetype
                    == TokraOrganicOperationArchetype.GoauldObservation
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
                TokraOrganicWoundedAgentUtility.RemoveSymbioteShock(patient);

                Messages.Message(
                    "GR_TokraWoundedAgent_InitialCareMessage".Translate(
                        patient.LabelShortCap),
                    patient,
                    MessageTypeDefOf.PositiveEvent,
                    historical: true);

                GR_Log.Message(
                    "Initial colony treatment received by wounded Tok'ra "
                    + $"agent {patient.LabelShortCap}; symbiote shock lifted.");
            }
            else
            {
                TokraOrganicWoundedAgentUtility.RemoveSymbioteShock(patient);
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

        internal void UpdateObservationReadyState(
            int currentTick,
            bool notifyPlayer)
        {
            TokraOrganicOperationDefinition definition
                = GetActiveDefinition();

            if (definition == null
                || definition.HasPhysicalObjective
                || definition.Archetype
                    == TokraOrganicOperationArchetype.WoundedAgentCare
                || definition.Archetype
                    == TokraOrganicOperationArchetype.MedicalSupplyHandoff
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

        internal void TickAcceptedPhysicalObjective(int currentTick)
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
            bool isWoundedAgent = definition.Archetype
                == TokraOrganicOperationArchetype.WoundedAgentCare;
            bool isMedicalSupply = definition.Archetype
                == TokraOrganicOperationArchetype.MedicalSupplyHandoff;

            if (outcome == TokraOrganicOperationOutcome.Succeeded
                && !bypassSuccessValidation)
            {
                if (isWoundedAgent)
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
                    definition.IntellectualXp);
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
                patient);

            GR_Log.Message(
                "Resolved Tok'ra organic operation "
                + $"{definition.DebugLabel} with outcome {outcome}; "
                + $"map {activeMapId}; operator "
                + $"{operatorPawn?.LabelShortCap ?? "none"}; "
                + $"patient {patient?.LabelShortCap ?? "none"}; "
                + $"Intellectual XP "
                + $"{(outcome == TokraOrganicOperationOutcome.Succeeded ? definition.IntellectualXp : 0)}; "
                + $"Medicine XP "
                + $"{(outcome == TokraOrganicOperationOutcome.Succeeded ? definition.MedicineXp : 0)}; "
                + $"Social XP "
                + $"{(outcome == TokraOrganicOperationOutcome.Succeeded ? definition.SocialXp : 0)}.");

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

        private static void SendResolutionLetter(
            TokraOrganicOperationDefinition definition,
            TokraOrganicOperationOutcome outcome,
            Pawn operatorPawn,
            string failureTextKey,
            Thing letterTarget,
            Pawn patient)
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
                    "GR_TokraOrganicOperation_DeadDropSuccessLetterText"
                        .Translate(
                            operatorPawn?.LabelShortCap ?? "?",
                            definition.IntellectualXp.ToString()),
                    LetterDefOf.PositiveEvent,
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
            activeDeadDrop = null;

            if (objective != null && !objective.Destroyed)
            {
                objective.Destroy(DestroyMode.Vanish);
            }

            TokraOrganicOperationFramework.DestroyObjectives(map, definition);
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

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            TokraOrganicOperationDefinition definition = GetActiveDefinition();

            if (!activeOperation.IsActive || definition == null)
            {
                activeOperation.Reset();
            }
            else
            {
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
            }

            TokraOrganicOperationFramework.DestroyAllObjectivesExcept(
                activeDeadDrop);
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
                float weight)
            {
                Archetype = archetype;
                Weight = weight;
            }

            public TokraOrganicOperationArchetype Archetype;
            public float Weight;
        }
    }

}
