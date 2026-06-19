using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Single persistent runtime record for the currently offered or active
    /// Tok'ra organic operation. New operation workers reuse this common state
    /// instead of adding fields to the game component.
    /// </summary>
    public sealed class TokraOrganicOperationInstance : IExposable
    {
        public TokraOrganicOperationArchetype archetype;
        public TokraOrganicOperationState state;
        public int mapId = -1;
        public int offerCreatedTick;
        public int offerExpiryTick;
        public int acceptedTick;
        public int reportReadyTick;
        public int deadlineTick;
        public bool readyNotificationSent;
        public bool resolutionApplied;

        public Thing objective;

        public Thing observationPointMarker;
        public IntVec3 observationTargetCell = IntVec3.Invalid;
        public bool observationDeviceDeployed;
        public int observationReadyTick;
        public int observationTransmissionTotalTicks;
        public int observationTransmissionRemainingTicks;
        public int observationResultVariant = -1;

        public TokraIntelligenceAnalysisMethod intelligenceAnalysisMethod;
        public int intelligenceWorkTotalTicks;
        public int intelligenceWorkRemainingTicks;
        public bool intelligenceInterferenceRollResolved;
        public bool intelligenceInterferenceTriggered;
        public bool intelligencePatrolQueued;
        public int intelligenceResultVariant = -1;

        public Pawn woundedAgent;
        public bool woundedAgentInitialCareReceived;
        public int woundedAgentInitialTendedConditionCount;
        public int woundedAgentStableSinceTick;
        public bool woundedAgentDepartureOrdered;
        public int woundedAgentDepartureDeadlineTick;

        public Pawn medicalSupplyLiaison;
        public IntVec3 medicalSupplyMeetingCell = IntVec3.Invalid;
        public int medicalSupplyArrivalTick;
        public bool medicalSupplyArrivalNotified;
        public bool medicalSupplyDepartureOrdered;

        public bool IsActive => archetype != TokraOrganicOperationArchetype.None
            && state != TokraOrganicOperationState.None;

        public void ExposeData()
        {
            Scribe_Values.Look(
                ref archetype,
                "archetype",
                TokraOrganicOperationArchetype.None);
            Scribe_Values.Look(
                ref state,
                "state",
                TokraOrganicOperationState.None);
            Scribe_Values.Look(ref mapId, "mapId", -1);
            Scribe_Values.Look(ref offerCreatedTick, "offerCreatedTick", 0);
            Scribe_Values.Look(ref offerExpiryTick, "offerExpiryTick", 0);
            Scribe_Values.Look(ref acceptedTick, "acceptedTick", 0);
            Scribe_Values.Look(ref reportReadyTick, "reportReadyTick", 0);
            Scribe_Values.Look(ref deadlineTick, "deadlineTick", 0);
            Scribe_Values.Look(
                ref readyNotificationSent,
                "readyNotificationSent",
                false);
            Scribe_Values.Look(
                ref resolutionApplied,
                "resolutionApplied",
                false);

            Scribe_References.Look(ref objective, "objective");

            Scribe_References.Look(
                ref observationPointMarker,
                "observationPointMarker");
            Scribe_Values.Look(
                ref observationTargetCell,
                "observationTargetCell",
                IntVec3.Invalid);
            Scribe_Values.Look(
                ref observationDeviceDeployed,
                "observationDeviceDeployed",
                false);
            Scribe_Values.Look(
                ref observationReadyTick,
                "observationReadyTick",
                0);
            Scribe_Values.Look(
                ref observationTransmissionTotalTicks,
                "observationTransmissionTotalTicks",
                0);
            Scribe_Values.Look(
                ref observationTransmissionRemainingTicks,
                "observationTransmissionRemainingTicks",
                0);
            Scribe_Values.Look(
                ref observationResultVariant,
                "observationResultVariant",
                -1);

            Scribe_Values.Look(
                ref intelligenceAnalysisMethod,
                "intelligenceAnalysisMethod",
                TokraIntelligenceAnalysisMethod.None);
            Scribe_Values.Look(
                ref intelligenceWorkTotalTicks,
                "intelligenceWorkTotalTicks",
                0);
            Scribe_Values.Look(
                ref intelligenceWorkRemainingTicks,
                "intelligenceWorkRemainingTicks",
                0);
            Scribe_Values.Look(
                ref intelligenceInterferenceRollResolved,
                "intelligenceInterferenceRollResolved",
                false);
            Scribe_Values.Look(
                ref intelligenceInterferenceTriggered,
                "intelligenceInterferenceTriggered",
                false);
            Scribe_Values.Look(
                ref intelligencePatrolQueued,
                "intelligencePatrolQueued",
                false);
            Scribe_Values.Look(
                ref intelligenceResultVariant,
                "intelligenceResultVariant",
                -1);

            Scribe_References.Look(ref woundedAgent, "woundedAgent");
            Scribe_Values.Look(
                ref woundedAgentInitialCareReceived,
                "woundedAgentInitialCareReceived",
                false);
            Scribe_Values.Look(
                ref woundedAgentInitialTendedConditionCount,
                "woundedAgentInitialTendedConditionCount",
                0);
            Scribe_Values.Look(
                ref woundedAgentStableSinceTick,
                "woundedAgentStableSinceTick",
                0);
            Scribe_Values.Look(
                ref woundedAgentDepartureOrdered,
                "woundedAgentDepartureOrdered",
                false);
            Scribe_Values.Look(
                ref woundedAgentDepartureDeadlineTick,
                "woundedAgentDepartureDeadlineTick",
                0);

            Scribe_References.Look(
                ref medicalSupplyLiaison,
                "medicalSupplyLiaison");
            Scribe_Values.Look(
                ref medicalSupplyMeetingCell,
                "medicalSupplyMeetingCell",
                IntVec3.Invalid);
            Scribe_Values.Look(
                ref medicalSupplyArrivalTick,
                "medicalSupplyArrivalTick",
                0);
            Scribe_Values.Look(
                ref medicalSupplyArrivalNotified,
                "medicalSupplyArrivalNotified",
                false);
            Scribe_Values.Look(
                ref medicalSupplyDepartureOrdered,
                "medicalSupplyDepartureOrdered",
                false);
        }

        public void Reset()
        {
            archetype = TokraOrganicOperationArchetype.None;
            state = TokraOrganicOperationState.None;
            mapId = -1;
            offerCreatedTick = 0;
            offerExpiryTick = 0;
            acceptedTick = 0;
            reportReadyTick = 0;
            deadlineTick = 0;
            readyNotificationSent = false;
            resolutionApplied = false;
            objective = null;
            observationPointMarker = null;
            observationTargetCell = IntVec3.Invalid;
            observationDeviceDeployed = false;
            observationReadyTick = 0;
            observationTransmissionTotalTicks = 0;
            observationTransmissionRemainingTicks = 0;
            observationResultVariant = -1;
            intelligenceAnalysisMethod = TokraIntelligenceAnalysisMethod.None;
            intelligenceWorkTotalTicks = 0;
            intelligenceWorkRemainingTicks = 0;
            intelligenceInterferenceRollResolved = false;
            intelligenceInterferenceTriggered = false;
            intelligencePatrolQueued = false;
            intelligenceResultVariant = -1;
            woundedAgent = null;
            woundedAgentInitialCareReceived = false;
            woundedAgentInitialTendedConditionCount = 0;
            woundedAgentStableSinceTick = 0;
            woundedAgentDepartureOrdered = false;
            woundedAgentDepartureDeadlineTick = 0;
            medicalSupplyLiaison = null;
            medicalSupplyMeetingCell = IntVec3.Invalid;
            medicalSupplyArrivalTick = 0;
            medicalSupplyArrivalNotified = false;
            medicalSupplyDepartureOrdered = false;
        }
    }

    /// <summary>
    /// Persistent consequence that may outlive the primary operation instance.
    /// </summary>
    public sealed class TokraOrganicOperationFollowUp : IExposable
    {
        public Pawn departingMedicalSupplyLiaison;
        public bool medicalSupplyDeathPenaltyPending;

        public void ExposeData()
        {
            Scribe_References.Look(
                ref departingMedicalSupplyLiaison,
                "departingMedicalSupplyLiaison");
            Scribe_Values.Look(
                ref medicalSupplyDeathPenaltyPending,
                "medicalSupplyDeathPenaltyPending",
                false);
        }
    }
}
