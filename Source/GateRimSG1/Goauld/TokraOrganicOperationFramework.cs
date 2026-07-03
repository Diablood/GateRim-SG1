using System;
using System.Collections.Generic;
using System.Linq;
using GateRimSG1.Missions;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    internal sealed class TokraOrganicOperationDefinition
    {
        public TokraOrganicOperationDefinition(
            TokraOrganicOperationArchetype archetype,
            int offerDurationTicks,
            int readyDelayTicks,
            int deadlineTicks,
            int intellectualXp,
            int medicineXp,
            int socialXp,
            int successTrustChange,
            int failureTrustChange,
            float waryWeight,
            float neutralWeight,
            float cooperativeWeight,
            float trustedWeight,
            string objectiveThingDefName,
            string acceptActionKey,
            string completeActionKey,
            string offerLetterLabelKey,
            string offerLetterTextKey,
            string offerExpiredMessageKey,
            string offeredStatusKey,
            string activeStatusKey,
            string readyStatusKey,
            string successTrustMessageKey,
            string failureTrustMessageKey,
            string debugLabel)
        {
            Archetype = archetype;
            OfferDurationTicks = offerDurationTicks;
            ReadyDelayTicks = readyDelayTicks;
            DeadlineTicks = deadlineTicks;
            IntellectualXp = intellectualXp;
            MedicineXp = medicineXp;
            SocialXp = socialXp;
            SuccessTrustChange = successTrustChange;
            FailureTrustChange = failureTrustChange;
            WaryWeight = waryWeight;
            NeutralWeight = neutralWeight;
            CooperativeWeight = cooperativeWeight;
            TrustedWeight = trustedWeight;
            ObjectiveThingDefName = objectiveThingDefName;
            AcceptActionKey = acceptActionKey;
            CompleteActionKey = completeActionKey;
            OfferLetterLabelKey = offerLetterLabelKey;
            OfferLetterTextKey = offerLetterTextKey;
            OfferExpiredMessageKey = offerExpiredMessageKey;
            OfferedStatusKey = offeredStatusKey;
            ActiveStatusKey = activeStatusKey;
            ReadyStatusKey = readyStatusKey;
            SuccessTrustMessageKey = successTrustMessageKey;
            FailureTrustMessageKey = failureTrustMessageKey;
            DebugLabel = debugLabel;
            RepeatedArchetypeWeightFactor
                = TokraOrganicOperationFramework
                    .LegacyRepeatedArchetypeWeightFactor;
        }

        public TokraOrganicOperationDefinition(
            TokraOrganicOperationArchetype archetype,
            GateRimMissionDef missionDef)
        {
            if (missionDef == null)
            {
                throw new ArgumentNullException(nameof(missionDef));
            }

            GateRimMissionObjectiveDef deployment = missionDef.GetObjective(
                "accepted",
                "DeployThing");
            GateRimMissionObjectiveDef observation = missionDef.GetObjective(
                "observing",
                "MaintainOperator");
            GateRimMissionObjectiveDef recovery = missionDef.GetObjective(
                "ready",
                "RecoverAndTransmit");
            GateRimMissionObjectiveDef cautiousAnalysis = missionDef.GetObjective(
                "cautious",
                "AnalyzeThing");
            GateRimMissionObjectiveDef acceleratedAnalysis
                = missionDef.GetObjective(
                    "accelerated",
                    "AnalyzeThing");
            GateRimMissionObjectiveDef handoffDelivery
                = missionDef.GetObjective(
                    "ready",
                    "DeliverThing");
            GateRimMissionConsequenceDef acceleratedXpBonus
                = missionDef.GetTransitionConsequence(
                    "accelerated",
                    "succeeded",
                    "GrantSkillXp");
            GateRimMissionConsequenceDef patrolConsequence
                = missionDef.GetTransitionConsequence(
                    "accelerated",
                    "succeeded",
                    "QueueIncident");
            GateRimMissionConsequenceDef decoyRaidConsequence
                = missionDef.GetPhaseConsequence(
                    "accepted",
                    "QueueIncident");
            GateRimMissionSkillXpRewardDef skillReward
                = missionDef.rewards?.skillXpRewards?.FirstOrDefault(
                    item => item != null
                        && !string.IsNullOrWhiteSpace(item.skillDefName)
                        && item.xp > 0);

            MissionDef = missionDef;
            Archetype = archetype;
            OfferDurationTicks = missionDef.timing?.offerDurationTicks ?? 0;
            ReadyDelayTicks = missionDef.timing?.readyDelayTicks ?? 0;
            DeadlineTicks = missionDef.timing?.deadlineTicks ?? 0;
            IntellectualXp = missionDef.rewards?.intellectualXp ?? 0;
            MedicineXp = missionDef.rewards?.medicineXp ?? 0;
            SocialXp = missionDef.rewards?.socialXp ?? 0;
            SuccessTrustChange = missionDef.rewards?.successTrustChange ?? 0;
            FailureTrustChange = missionDef.rewards?.failureTrustChange ?? 0;
            SkillXpRewardDefName = skillReward?.skillDefName;
            SkillXpRewardAmount = skillReward?.xp ?? 0;
            WaryWeight = GetMissionWeight(missionDef, "TokraTrust.Wary");
            NeutralWeight = GetMissionWeight(missionDef, "TokraTrust.Neutral");
            CooperativeWeight = GetMissionWeight(
                missionDef,
                "TokraTrust.Cooperative");
            TrustedWeight = GetMissionWeight(
                missionDef,
                "TokraTrust.Trusted");
            ObjectiveThingDefName = missionDef.objectiveThingDef?.defName;
            AcceptActionKey = missionDef.actions?.acceptActionKey;
            CompleteActionKey = missionDef.actions?.completeActionKey;
            DeployActionKey = missionDef.actions?.deployActionKey;
            ContinueActionKey = missionDef.actions?.continueActionKey;
            RecoverActionKey = missionDef.actions?.recoverActionKey;
            ResumeActionKey = missionDef.actions?.resumeActionKey;
            OfferLetterLabelKey = missionDef.texts?.offerLetterLabelKey;
            OfferLetterTextKey = missionDef.texts?.offerLetterTexts?
                .FirstOrDefault(item => item != null
                    && !string.IsNullOrWhiteSpace(item.key))?.key;
            OfferExpiredMessageKey = missionDef.texts?.offerExpiredMessageKey;
            AcceptedMessageKey = missionDef.texts?.acceptedMessageKey;
            SuccessLetterLabelKey = missionDef.texts?.successLetterLabelKey;
            FailureLetterLabelKey = missionDef.texts?.failureLetterLabelKey;
            OfferedStatusKey = missionDef.actions?.offeredStatusKey;
            ActiveStatusKey = missionDef.actions?.activeStatusKey;
            ReadyStatusKey = missionDef.actions?.readyStatusKey;
            SuccessTrustMessageKey
                = missionDef.actions?.successTrustMessageKey;
            FailureTrustMessageKey
                = missionDef.actions?.failureTrustMessageKey;
            DebugLabel = missionDef.debugLabel;
            RepeatedArchetypeWeightFactor
                = missionDef.recurrence?.repeatedMissionWeightFactor ?? 0f;
            MinimumRecurrenceDelayTicks
                = missionDef.recurrence?.minimumDelayTicks ?? 0;
            MaximumRecurrenceDelayTicks
                = missionDef.recurrence?.maximumDelayTicks ?? 0;
            ObservationDeviceDefName = deployment?.targetDefName;
            ObservationPointDefName = deployment?.secondaryTargetDefName;
            ObservationDeploymentJobDefName = deployment?.jobDefName;
            ObservationDeploymentWorkTicks = deployment?.workTicks ?? 0;
            ObservationWorkTargetDefName = observation?.targetDefName;
            ObservationWorkTicks = observation?.workTicks ?? 0;
            ObservationSkillDefName = observation?.skillDefName;
            ObservationXpPerTick = observation?.xpPerTick ?? 0f;
            ObservationRecoveryTargetDefName = recovery?.targetDefName;
            ObservationTransmissionJobDefName = recovery?.jobDefName;
            ObservationRecoveryWorkTicks = recovery?.workTicks ?? 0;
            ObservationTransmissionWorkTicks = recovery?.secondaryWorkTicks ?? 0;
            IntelligenceAnalysisJobDefName = cautiousAnalysis?.jobDefName;
            IntelligenceAnalysisTargetDefName = cautiousAnalysis?.targetDefName;
            IntelligenceSkillDefName = cautiousAnalysis?.skillDefName;
            IntelligenceCautiousWorkTicks = cautiousAnalysis?.workTicks ?? 0;
            IntelligenceAcceleratedWorkTicks
                = acceleratedAnalysis?.workTicks ?? 0;
            IntelligenceAcceleratedXpBonus
                = (int)Math.Round(acceleratedXpBonus?.value ?? 0f);
            IntelligencePatrolIncidentDefName
                = patrolConsequence?.targetDefName;
            IntelligenceInterferenceChance
                = patrolConsequence?.chance ?? 0f;
            IntelligencePatrolDelayMinimumTicks
                = patrolConsequence?.minimumDelayTicks ?? 0;
            IntelligencePatrolDelayMaximumTicks
                = patrolConsequence?.maximumDelayTicks ?? 0;
            IntelligencePatrolRetryTicks
                = patrolConsequence?.retryTicks ?? 0;
            MedicalSupplyThingDefName
                = handoffDelivery?.targetDefName;
            MedicalSupplyDialogueJobDefName
                = handoffDelivery?.jobDefName;
            MedicalSupplySkillDefName
                = handoffDelivery?.skillDefName;
            MedicalSupplyRequiredCount
                = handoffDelivery?.requiredCount ?? 0;
            DecoyRaidIncidentDefName
                = decoyRaidConsequence?.targetDefName;
            DecoyRaidDelayMinimumTicks
                = decoyRaidConsequence?.minimumDelayTicks ?? 0;
            DecoyRaidDelayMaximumTicks
                = decoyRaidConsequence?.maximumDelayTicks ?? 0;
            DecoyRaidRetryTicks
                = decoyRaidConsequence?.retryTicks ?? 0;
        }

        public TokraOrganicOperationArchetype Archetype { get; }
        public int OfferDurationTicks { get; }
        public int ReadyDelayTicks { get; }
        public int DeadlineTicks { get; }
        public int IntellectualXp { get; }
        public int MedicineXp { get; }
        public int SocialXp { get; }
        public int SuccessTrustChange { get; }
        public int FailureTrustChange { get; }
        public string SkillXpRewardDefName { get; }
        public int SkillXpRewardAmount { get; }
        public string ObjectiveThingDefName { get; }
        public string AcceptActionKey { get; }
        public string CompleteActionKey { get; }
        public string DeployActionKey { get; }
        public string ContinueActionKey { get; }
        public string RecoverActionKey { get; }
        public string ResumeActionKey { get; }
        public string OfferLetterLabelKey { get; }
        public string OfferLetterTextKey { get; }
        public string OfferExpiredMessageKey { get; }
        public string AcceptedMessageKey { get; }
        public string SuccessLetterLabelKey { get; }
        public string FailureLetterLabelKey { get; }
        public string OfferedStatusKey { get; }
        public string ActiveStatusKey { get; }
        public string ReadyStatusKey { get; }
        public string SuccessTrustMessageKey { get; }
        public string FailureTrustMessageKey { get; }
        public string DebugLabel { get; }
        public GateRimMissionDef MissionDef { get; }
        public GateRimMissionPawnCareDef PawnCare => MissionDef?.pawnCare;
        public GateRimMissionHandoffDef Handoff => MissionDef?.handoff;
        public GateRimMissionDistressCallDef DistressCall
            => MissionDef?.distressCall;
        public GateRimMissionDeliveryDef Delivery
            => MissionDef?.delivery;
        public GateRimMissionCaptureDef Capture
            => MissionDef?.capture;
        public string MissionDefName => MissionDef?.defName;
        public bool UsesMissionFrameworkDef => MissionDef != null;
        public float RepeatedArchetypeWeightFactor { get; }
        public int MinimumRecurrenceDelayTicks { get; }
        public int MaximumRecurrenceDelayTicks { get; }
        public string ObservationDeviceDefName { get; }
        public string ObservationPointDefName { get; }
        public string ObservationDeploymentJobDefName { get; }
        public string ObservationTransmissionJobDefName { get; }
        public string ObservationRecoveryTargetDefName { get; }
        public int ObservationDeploymentWorkTicks { get; }
        public string ObservationWorkTargetDefName { get; }
        public int ObservationWorkTicks { get; }
        public int ObservationRecoveryWorkTicks { get; }
        public int ObservationTransmissionWorkTicks { get; }
        public string ObservationSkillDefName { get; }
        public float ObservationXpPerTick { get; }
        public string IntelligenceAnalysisJobDefName { get; }
        public string IntelligenceAnalysisTargetDefName { get; }
        public string IntelligenceSkillDefName { get; }
        public int IntelligenceCautiousWorkTicks { get; }
        public int IntelligenceAcceleratedWorkTicks { get; }
        public int IntelligenceAcceleratedXpBonus { get; }
        public string IntelligencePatrolIncidentDefName { get; }
        public float IntelligenceInterferenceChance { get; }
        public int IntelligencePatrolDelayMinimumTicks { get; }
        public int IntelligencePatrolDelayMaximumTicks { get; }
        public int IntelligencePatrolRetryTicks { get; }
        public string MedicalSupplyThingDefName { get; }
        public string MedicalSupplyDialogueJobDefName { get; }
        public string MedicalSupplySkillDefName { get; }
        public int MedicalSupplyRequiredCount { get; }
        public string DecoyRaidIncidentDefName { get; }
        public int DecoyRaidDelayMinimumTicks { get; }
        public int DecoyRaidDelayMaximumTicks { get; }
        public int DecoyRaidRetryTicks { get; }

        public bool HasPhysicalObjective => !string.IsNullOrEmpty(
            ObjectiveThingDefName);

        public bool UsesCommunicatorForCompletion => !string.IsNullOrEmpty(
            CompleteActionKey);

        private float WaryWeight { get; }
        private float NeutralWeight { get; }
        private float CooperativeWeight { get; }
        private float TrustedWeight { get; }

        public string SelectOfferLetterTextKey(
            int previousIndex,
            out int selectedIndex)
        {
            if (MissionDef?.texts?.offerLetterTexts != null)
            {
                string selectedKey = GateRimMissionFramework.SelectTextKey(
                    MissionDef.texts.offerLetterTexts,
                    previousIndex,
                    out selectedIndex);

                if (!string.IsNullOrEmpty(selectedKey))
                {
                    return selectedKey;
                }
            }

            selectedIndex = -1;
            return OfferLetterTextKey;
        }

        public string SelectSuccessLetterTextKey(
            int previousIndex,
            out int selectedIndex)
        {
            return GateRimMissionFramework.SelectTextKey(
                MissionDef?.texts?.successLetterTexts,
                previousIndex,
                out selectedIndex);
        }

        public string SelectNamedTextKey(
            string bankId,
            int previousIndex,
            out int selectedIndex)
        {
            return GateRimMissionFramework.SelectTextKey(
                MissionDef?.GetNamedTextBank(bankId),
                previousIndex,
                out selectedIndex);
        }

        public string GetNamedTextKey(string bankId, int selectedIndex)
        {
            List<GateRimMissionTextVariantDef> variants
                = MissionDef?.GetNamedTextBank(bankId);

            if (variants == null || variants.Count == 0)
            {
                return null;
            }

            int index = Math.Max(
                0,
                Math.Min(variants.Count - 1, selectedIndex));
            return variants[index]?.key;
        }

        public string GetRuntimeTextKey(string id)
        {
            return MissionDef?.GetRuntimeTextKey(id);
        }

        public float GetWeight(TokraTrustTier tier)
        {
            switch (tier)
            {
                case TokraTrustTier.Wary:
                    return WaryWeight;
                case TokraTrustTier.Neutral:
                    return NeutralWeight;
                case TokraTrustTier.Cooperative:
                    return CooperativeWeight;
                case TokraTrustTier.Trusted:
                    return TrustedWeight;
                default:
                    return 0f;
            }
        }

        public bool HasCompleteObservationConfiguration(out string error)
        {
            List<string> missing = new List<string>();

            if (OfferDurationTicks <= 0)
            {
                missing.Add("offer duration");
            }

            if (DeadlineTicks <= 0)
            {
                missing.Add("deadline");
            }

            Require(AcceptActionKey, "accept action", missing);
            Require(CompleteActionKey, "complete action", missing);
            Require(OfferLetterLabelKey, "offer letter label", missing);
            Require(OfferLetterTextKey, "offer letter text", missing);
            Require(OfferExpiredMessageKey, "offer expiry message", missing);
            Require(OfferedStatusKey, "offered status", missing);
            Require(ActiveStatusKey, "active status", missing);
            Require(ReadyStatusKey, "ready status", missing);
            Require(SuccessTrustMessageKey, "success trust message", missing);
            Require(FailureTrustMessageKey, "failure trust message", missing);
            Require(ObjectiveThingDefName, "objectiveThingDef", missing);
            Require(ObservationDeviceDefName, "deployment target", missing);
            Require(ObservationPointDefName, "observation point", missing);
            Require(
                ObservationWorkTargetDefName,
                "observation work target",
                missing);
            Require(
                ObservationRecoveryTargetDefName,
                "recovery target",
                missing);
            Require(ObservationDeploymentJobDefName, "deployment job", missing);
            Require(ObservationTransmissionJobDefName, "transmission job", missing);
            Require(ObservationSkillDefName, "work skill", missing);
            Require(SkillXpRewardDefName, "skill XP reward", missing);
            Require(DeployActionKey, "deploy action", missing);
            Require(ContinueActionKey, "continue action", missing);
            Require(RecoverActionKey, "recover action", missing);
            Require(ResumeActionKey, "resume action", missing);
            Require(AcceptedMessageKey, "accepted message", missing);
            Require(SuccessLetterLabelKey, "success letter label", missing);
            Require(FailureLetterLabelKey, "failure letter label", missing);

            string[] runtimeTextIds =
            {
                "spawnFailed",
                "targetLetterLabel",
                "targetLetterText",
                "deployed",
                "dataReady",
                "observationStarted",
                "recoveryStarted",
                "transmissionStarted",
                "failureTimeout",
                "failureDeviceLost",
                "noActiveDeployment",
                "alreadyDeployed",
                "deviceLost",
                "dataNotReady",
                "noReadyRecovery",
                "cannotReachDevice",
                "cannotReachPoint",
                "cannotReachCommunicator",
                "jobUnavailable",
                "operatorIncapable",
                "communicatorUnpowered",
                "expired",
                "statusAwaitingDeployment",
                "statusRecording",
                "statusTransmissionInterrupted",
                "statusDataReady"
            };

            foreach (string runtimeTextId in runtimeTextIds)
            {
                Require(
                    GetRuntimeTextKey(runtimeTextId),
                    "runtime text " + runtimeTextId,
                    missing);
            }

            if (ObservationDeploymentWorkTicks <= 0)
            {
                missing.Add("deployment work ticks");
            }

            if (ObservationWorkTicks <= 0)
            {
                missing.Add("observation work ticks");
            }

            if (ObservationRecoveryWorkTicks <= 0)
            {
                missing.Add("recovery work ticks");
            }

            if (ObservationTransmissionWorkTicks <= 0)
            {
                missing.Add("transmission work ticks");
            }

            if (ObservationXpPerTick <= 0f)
            {
                missing.Add("positive work XP per tick");
            }

            if (SkillXpRewardAmount <= 0)
            {
                missing.Add("positive skill XP reward amount");
            }

            ValidateObservationTargetConsistency(missing);
            ValidateObservationDefReferences(missing);

            if (MinimumRecurrenceDelayTicks <= 0
                || MaximumRecurrenceDelayTicks < MinimumRecurrenceDelayTicks)
            {
                missing.Add("recurrence delay range");
            }

            if (MissionDef?.texts?.successLetterTexts == null
                || MissionDef.texts.successLetterTexts.Count == 0)
            {
                missing.Add("success letter text variants");
            }

            error = missing.Count == 0
                ? null
                : string.Join(", ", missing);
            return missing.Count == 0;
        }

        public bool HasCompleteWoundedAgentConfiguration(
            out string error)
        {
            List<string> missing = new List<string>();

            if (OfferDurationTicks <= 0)
            {
                missing.Add("offer duration");
            }

            if (DeadlineTicks <= 0)
            {
                missing.Add("deadline");
            }

            Require(AcceptActionKey, "accept action", missing);
            Require(OfferLetterLabelKey, "offer letter label", missing);
            Require(OfferLetterTextKey, "offer letter text", missing);
            Require(OfferExpiredMessageKey, "offer expiry message", missing);
            Require(AcceptedMessageKey, "accepted message", missing);
            Require(SuccessLetterLabelKey, "success letter label", missing);
            Require(FailureLetterLabelKey, "failure letter label", missing);
            Require(OfferedStatusKey, "offered status", missing);
            Require(ActiveStatusKey, "active status", missing);
            Require(ReadyStatusKey, "ready status", missing);
            Require(SuccessTrustMessageKey, "success trust message", missing);
            Require(FailureTrustMessageKey, "failure trust message", missing);

            string[] runtimeTextIds =
            {
                "spawnFailed",
                "arrivalLabel",
                "arrivalText",
                "initialCareMessage",
                "stableMessage",
                "departingMessage",
                "statusAwaitingCare",
                "failureLost",
                "failureDeath",
                "failureCaptured",
                "failureTimeout"
            };

            foreach (string runtimeTextId in runtimeTextIds)
            {
                Require(
                    GetRuntimeTextKey(runtimeTextId),
                    "runtime text " + runtimeTextId,
                    missing);
            }

            GateRimMissionPawnCareDef pawnCare = PawnCare;

            if (pawnCare == null)
            {
                missing.Add("pawn care profile");
            }
            else
            {
                Require(pawnCare.pawnKindDefName, "pawn kind", missing);
                Require(
                    pawnCare.initialHediffDefName,
                    "initial health condition",
                    missing);
                Require(
                    pawnCare.recoveryHediffDefName,
                    "recovery health condition",
                    missing);
                Require(
                    pawnCare.optionalIllnessHediffDefName,
                    "optional illness",
                    missing);

                if (pawnCare.stableDurationTicks <= 0)
                {
                    missing.Add("positive stable duration");
                }

                if (pawnCare.departureGraceTicks <= 0)
                {
                    missing.Add("positive departure grace duration");
                }

                if (pawnCare.minimumMovingCapacity <= 0f
                    || pawnCare.minimumConsciousnessCapacity <= 0f
                    || pawnCare.minimumSummaryHealth <= 0f)
                {
                    missing.Add("positive medical departure thresholds");
                }

                if (pawnCare.maximumBleedRate < 0f)
                {
                    missing.Add("non-negative maximum bleed rate");
                }

                if (pawnCare.criticalHediffSeverityFraction <= 0f
                    || pawnCare.criticalHediffSeverityFraction > 1f)
                {
                    missing.Add("critical hediff severity fraction");
                }

                if (pawnCare.optionalIllnessChanceMinimum < 0f
                    || pawnCare.optionalIllnessChanceMaximum
                        < pawnCare.optionalIllnessChanceMinimum
                    || pawnCare.optionalIllnessChanceMaximum > 1f)
                {
                    missing.Add("optional illness chance range");
                }

                if (pawnCare.optionalIllnessSeverityMinimum < 0f
                    || pawnCare.optionalIllnessSeverityMaximum
                        < pawnCare.optionalIllnessSeverityMinimum
                    || pawnCare.optionalIllnessSeverityMaximum > 1f)
                {
                    missing.Add("optional illness severity range");
                }

                ValidateWoundedAgentDefReferences(pawnCare, missing);
            }

            if (MinimumRecurrenceDelayTicks <= 0
                || MaximumRecurrenceDelayTicks < MinimumRecurrenceDelayTicks)
            {
                missing.Add("recurrence delay range");
            }

            if (MissionDef?.difficulty?.mode
                != GateRimMissionDifficultyMode.ThreatPointsScaled)
            {
                missing.Add("ThreatPointsScaled difficulty profile");
            }

            string[] recurrenceContextKeys =
            {
                "TokraTrust.Wary",
                "TokraTrust.Neutral",
                "TokraTrust.Cooperative",
                "TokraTrust.Trusted"
            };

            foreach (string contextKey in recurrenceContextKeys)
            {
                int contextMinimumDelay;
                int contextMaximumDelay;

                if (MissionDef?.recurrence == null
                    || !MissionDef.recurrence.TryGetDelayRange(
                        contextKey,
                        out contextMinimumDelay,
                        out contextMaximumDelay)
                    || contextMinimumDelay <= 0
                    || contextMaximumDelay < contextMinimumDelay)
                {
                    missing.Add(
                        "recurrence delay range for " + contextKey);
                }
            }

            if (MissionDef?.texts?.successLetterTexts == null
                || MissionDef.texts.successLetterTexts.Count == 0)
            {
                missing.Add("success letter text variants");
            }

            error = missing.Count == 0
                ? null
                : string.Join(", ", missing);
            return missing.Count == 0;
        }

        public bool HasCompleteDistressCallConfiguration(
            out string error)
        {
            List<string> missing = new List<string>();

            if (OfferDurationTicks <= 0)
            {
                missing.Add("offer duration");
            }

            if (DeadlineTicks <= 0)
            {
                missing.Add("deadline");
            }

            Require(AcceptActionKey, "accept action", missing);
            Require(OfferLetterLabelKey, "offer letter label", missing);
            Require(OfferLetterTextKey, "offer letter text", missing);
            Require(OfferExpiredMessageKey, "offer expiry message", missing);
            Require(AcceptedMessageKey, "accepted message", missing);
            Require(SuccessLetterLabelKey, "success letter label", missing);
            Require(FailureLetterLabelKey, "failure letter label", missing);
            Require(OfferedStatusKey, "offered status", missing);
            Require(ActiveStatusKey, "active status", missing);
            Require(ReadyStatusKey, "ready status", missing);
            Require(SuccessTrustMessageKey, "success trust message", missing);
            Require(FailureTrustMessageKey, "failure trust message", missing);

            string[] runtimeTextIds =
            {
                "spawnFailed",
                "targetLetterLabel",
                "targetLetterText",
                "failureTimeout",
                "failureSiteLost",
                "failureSurvivorsLost"
            };

            foreach (string runtimeTextId in runtimeTextIds)
            {
                Require(
                    GetRuntimeTextKey(runtimeTextId),
                    "runtime text " + runtimeTextId,
                    missing);
            }

            GateRimMissionDistressCallDef profile = DistressCall;

            if (profile == null)
            {
                missing.Add("distress-call profile");
            }
            else
            {
                Require(
                    profile.worldObjectDefName,
                    "world object Def",
                    missing);
                Require(
                    profile.survivorPawnKindDefName,
                    "survivor PawnKindDef",
                    missing);
                Require(
                    profile.recoveryPawnKindDefName,
                    "recovery-team PawnKindDef",
                    missing);

                if (profile.minimumTileDistance <= 0
                    || profile.maximumTileDistance
                        < profile.minimumTileDistance)
                {
                    missing.Add("world-site distance range");
                }

                if (profile.mapSize < 80)
                {
                    missing.Add("world-site map size");
                }

                if (profile.lateArrivalTicks <= 0
                    || profile.lateArrivalTicks >= DeadlineTicks)
                {
                    missing.Add("late-arrival threshold");
                }

                if (profile.survivorMinimumCount <= 0
                    || profile.survivorMaximumCount
                        < profile.survivorMinimumCount)
                {
                    missing.Add("survivor count range");
                }

                if (profile.defenderMinimumCount <= 0)
                {
                    missing.Add("defender minimum count");
                }

                if (profile.genuineRescueWeight < 0f
                    || profile.compromisedSignalWeight < 0f
                    || profile.lateArrivalWeight < 0f
                    || profile.genuineRescueWeight
                        + profile.compromisedSignalWeight
                        + profile.lateArrivalWeight <= 0f)
                {
                    missing.Add("positive variant weights");
                }

                if (profile.genuineRescueThreatFactor <= 0f
                    || profile.compromisedSignalThreatFactor <= 0f
                    || profile.lateArrivalThreatFactor <= 0f)
                {
                    missing.Add("positive threat factors");
                }

                if (profile.recoveryTeamDelayTicks <= 0
                    || profile.recoveryTeamRetryTicks <= 0)
                {
                    missing.Add("positive recovery-team timing");
                }

                if (profile.recoveryTeamMinimumCount <= 0
                    || profile.recoveryTeamMaximumCount
                        < profile.recoveryTeamMinimumCount)
                {
                    missing.Add("recovery-team count range");
                }

                if (!string.IsNullOrWhiteSpace(profile.worldObjectDefName)
                    && DefDatabase<WorldObjectDef>.GetNamedSilentFail(
                        profile.worldObjectDefName) == null)
                {
                    missing.Add(
                        "unknown WorldObjectDef "
                        + profile.worldObjectDefName);
                }

                if (!string.IsNullOrWhiteSpace(
                        profile.survivorPawnKindDefName)
                    && DefDatabase<PawnKindDef>.GetNamedSilentFail(
                        profile.survivorPawnKindDefName) == null)
                {
                    missing.Add(
                        "unknown PawnKindDef "
                        + profile.survivorPawnKindDefName);
                }

                if (!string.IsNullOrWhiteSpace(
                        profile.recoveryPawnKindDefName)
                    && DefDatabase<PawnKindDef>.GetNamedSilentFail(
                        profile.recoveryPawnKindDefName) == null)
                {
                    missing.Add(
                        "unknown recovery-team PawnKindDef "
                        + profile.recoveryPawnKindDefName);
                }

                if (!string.IsNullOrWhiteSpace(profile.salvageThingDefName)
                    && DefDatabase<ThingDef>.GetNamedSilentFail(
                        profile.salvageThingDefName) == null)
                {
                    missing.Add(
                        "unknown salvage ThingDef "
                        + profile.salvageThingDefName);
                }

                if (string.IsNullOrWhiteSpace(
                        profile.salvageContainerDefName))
                {
                    missing.Add("salvage container ThingDef");
                }
                else
                {
                    ThingDef salvageContainerDef
                        = DefDatabase<ThingDef>.GetNamedSilentFail(
                            profile.salvageContainerDefName);

                    if (salvageContainerDef == null)
                    {
                        missing.Add(
                            "unknown salvage container ThingDef "
                            + profile.salvageContainerDefName);
                    }
                    else if (salvageContainerDef.thingClass == null
                        || !typeof(Building_Storage).IsAssignableFrom(
                            salvageContainerDef.thingClass))
                    {
                        missing.Add(
                            "salvage container is not Building_Storage "
                            + profile.salvageContainerDefName);
                    }
                }

                if (DefDatabase<PawnKindDef>.GetNamedSilentFail(
                        "SG1_GoauldJaffaWarrior") == null
                    && DefDatabase<PawnKindDef>.GetNamedSilentFail(
                        "SG1_GoauldJaffaGuard") == null)
                {
                    missing.Add("Goa'uld Jaffa PawnKindDefs");
                }
            }

            GateRimMissionPawnCareDef pawnCare = PawnCare;

            if (pawnCare == null)
            {
                missing.Add("pawn care profile");
            }
            else
            {
                Require(
                    pawnCare.initialHediffDefName,
                    "initial health condition",
                    missing);
                Require(
                    pawnCare.recoveryHediffDefName,
                    "recovery health condition",
                    missing);

                if (pawnCare.stableDurationTicks <= 0
                    || pawnCare.departureGraceTicks <= 0)
                {
                    missing.Add("survivor recovery durations");
                }

                ValidateWoundedAgentDefReferences(pawnCare, missing);
            }

            if (MinimumRecurrenceDelayTicks <= 0
                || MaximumRecurrenceDelayTicks < MinimumRecurrenceDelayTicks)
            {
                missing.Add("recurrence delay range");
            }

            if (MissionDef?.difficulty?.mode
                != GateRimMissionDifficultyMode.ThreatPointsScaled)
            {
                missing.Add("ThreatPointsScaled difficulty profile");
            }

            if (MissionDef?.texts?.successLetterTexts == null
                || MissionDef.texts.successLetterTexts.Count == 0)
            {
                missing.Add("success letter text variants");
            }

            error = missing.Count == 0
                ? null
                : string.Join(", ", missing);
            return missing.Count == 0;
        }


        public bool HasCompleteTemporaryBaseDeliveryConfiguration(
            out string error)
        {
            List<string> missing = new List<string>();

            if (OfferDurationTicks <= 0)
            {
                missing.Add("offer duration");
            }

            if (DeadlineTicks <= 0)
            {
                missing.Add("deadline");
            }

            Require(AcceptActionKey, "accept action", missing);
            Require(OfferLetterLabelKey, "offer letter label", missing);
            Require(OfferLetterTextKey, "offer letter text", missing);
            Require(OfferExpiredMessageKey, "offer expiry message", missing);
            Require(AcceptedMessageKey, "accepted message", missing);
            Require(SuccessLetterLabelKey, "success letter label", missing);
            Require(FailureLetterLabelKey, "failure letter label", missing);
            Require(OfferedStatusKey, "offered status", missing);
            Require(ActiveStatusKey, "active status", missing);
            Require(ReadyStatusKey, "ready status", missing);
            Require(SuccessTrustMessageKey, "success trust message", missing);
            Require(FailureTrustMessageKey, "failure trust message", missing);

            string[] runtimeTextIds =
            {
                "spawnFailed",
                "targetLetterLabel",
                "targetLetterText",
                "lateStatus",
                "lateWarningLabel",
                "lateWarningText",
                "lateSuccessLetterLabel",
                "lateSuccessLetterText",
                "lateTrustMessage",
                "failureTimeout",
                "failureGraceExpired",
                "failureSiteLost"
            };

            foreach (string runtimeTextId in runtimeTextIds)
            {
                Require(
                    GetRuntimeTextKey(runtimeTextId),
                    "runtime text " + runtimeTextId,
                    missing);
            }

            GateRimMissionDeliveryDef profile = Delivery;

            if (profile == null)
            {
                missing.Add("temporary-base delivery profile");
            }
            else
            {
                Require(
                    profile.worldObjectDefName,
                    "world object Def",
                    missing);

                if (profile.minimumTileDistance <= 0
                    || profile.maximumTileDistance
                        < profile.minimumTileDistance)
                {
                    missing.Add("world-site distance range");
                }

                if (profile.minimumCrafterSkill < 0
                    || profile.minimumCrafterSkill > 20)
                {
                    missing.Add("minimum crafter skill");
                }

                if (profile.lateGraceTicks <= 0)
                {
                    missing.Add("late delivery grace duration");
                }

                if (profile.lateSuccessTrustChange < 0
                    || profile.lateSuccessTrustChange
                        > SuccessTrustChange)
                {
                    missing.Add("late delivery trust reward");
                }

                if (profile.interceptionChance < 0f
                    || profile.interceptionChance > 1f)
                {
                    missing.Add("interception chance");
                }

                if (profile.interceptionMinimumDelayTicks <= 0
                    || profile.interceptionMaximumDelayTicks
                        < profile.interceptionMinimumDelayTicks)
                {
                    missing.Add("interception delay range");
                }

                if (profile.interceptionRetryTicks <= 0)
                {
                    missing.Add("interception retry delay");
                }

                if (profile.interceptionThreatFactor <= 0f
                    || profile.interceptionMinimumPoints <= 0f
                    || profile.interceptionMaximumPoints
                        < profile.interceptionMinimumPoints)
                {
                    missing.Add("interception threat scaling");
                }

                IncidentDef interceptionIncidentDef
                    = string.IsNullOrWhiteSpace(
                        profile.interceptionIncidentDefName)
                        ? null
                        : DefDatabase<IncidentDef>.GetNamedSilentFail(
                            profile.interceptionIncidentDefName);

                if (interceptionIncidentDef == null)
                {
                    missing.Add(
                        "unknown interception IncidentDef "
                        + profile.interceptionIncidentDefName);
                }
                else if (!(interceptionIncidentDef.Worker
                    is IncidentWorker_TokraDeliveryInterception))
                {
                    missing.Add(
                        "interception IncidentDef has incompatible worker");
                }

                if (profile.destinationCompromiseChance < 0f
                    || profile.destinationCompromiseChance > 1f)
                {
                    missing.Add("destination compromise chance");
                }

                if (profile.destinationCompromiseRetryTicks <= 0)
                {
                    missing.Add("destination compromise retry delay");
                }

                if (profile.destinationCompromiseThreatFactor <= 0f
                    || profile.destinationCompromiseMinimumPoints <= 0f
                    || profile.destinationCompromiseMaximumPoints
                        < profile.destinationCompromiseMinimumPoints)
                {
                    missing.Add("destination compromise threat scaling");
                }

                IncidentDef destinationCompromiseIncidentDef
                    = string.IsNullOrWhiteSpace(
                        profile.destinationCompromiseIncidentDefName)
                        ? null
                        : DefDatabase<IncidentDef>.GetNamedSilentFail(
                            profile.destinationCompromiseIncidentDefName);

                if (destinationCompromiseIncidentDef == null)
                {
                    missing.Add(
                        "unknown destination compromise IncidentDef "
                        + profile.destinationCompromiseIncidentDefName);
                }
                else if (!(destinationCompromiseIncidentDef.Worker
                    is IncidentWorker_TokraDeliveryDestinationCompromise))
                {
                    missing.Add(
                        "destination compromise IncidentDef has "
                        + "incompatible worker");
                }

                WorldObjectDef worldObjectDef
                    = string.IsNullOrWhiteSpace(profile.worldObjectDefName)
                        ? null
                        : DefDatabase<WorldObjectDef>.GetNamedSilentFail(
                            profile.worldObjectDefName);

                if (worldObjectDef == null)
                {
                    missing.Add(
                        "unknown WorldObjectDef "
                        + profile.worldObjectDefName);
                }
                else if (worldObjectDef.worldObjectClass == null
                    || !typeof(WorldObject_TokraTemporaryBaseDeliverySite)
                        .IsAssignableFrom(
                            worldObjectDef.worldObjectClass))
                {
                    missing.Add(
                        "delivery WorldObjectDef has incompatible class");
                }

                if (profile.candidates == null
                    || profile.candidates.Count == 0)
                {
                    missing.Add("delivery candidate list");
                }
                else
                {
                    foreach (GateRimMissionDeliveryCandidateDef candidate
                        in profile.candidates)
                    {
                        if (candidate == null
                            || string.IsNullOrWhiteSpace(
                                candidate.thingDefName))
                        {
                            missing.Add("delivery candidate ThingDef");
                            continue;
                        }

                        ThingDef thingDef
                            = DefDatabase<ThingDef>.GetNamedSilentFail(
                                candidate.thingDefName);

                        if (thingDef == null)
                        {
                            missing.Add(
                                "unknown delivery ThingDef "
                                + candidate.thingDefName);
                        }
                        else
                        {
                            if (thingDef.category != ThingCategory.Item)
                            {
                                missing.Add(
                                    "delivery candidate is not an item "
                                    + candidate.thingDefName);
                            }

                            if (thingDef.techLevel > profile.maximumTechLevel)
                            {
                                missing.Add(
                                    "delivery candidate exceeds tech ceiling "
                                    + candidate.thingDefName);
                            }

                            if (candidate.requireQuality
                                && !thingDef.HasComp(
                                    typeof(CompQuality)))
                            {
                                missing.Add(
                                    "delivery candidate has no quality comp "
                                    + candidate.thingDefName);
                            }
                        }

                        if (candidate.minimumCount <= 0
                            || candidate.maximumCount
                                < candidate.minimumCount)
                        {
                            missing.Add(
                                "invalid delivery count range for "
                                + candidate.thingDefName);
                        }

                        if (candidate.minimumHitPointsPercent < 0f
                            || candidate.minimumHitPointsPercent > 1f)
                        {
                            missing.Add(
                                "invalid minimum hit points for "
                                + candidate.thingDefName);
                        }

                        if (candidate.weight <= 0f)
                        {
                            missing.Add(
                                "invalid delivery weight for "
                                + candidate.thingDefName);
                        }
                    }
                }
            }

            if (MinimumRecurrenceDelayTicks <= 0
                || MaximumRecurrenceDelayTicks < MinimumRecurrenceDelayTicks)
            {
                missing.Add("recurrence delay range");
            }

            error = missing.Count == 0
                ? null
                : string.Join(", ", missing);
            return missing.Count == 0;
        }

        private static void ValidateWoundedAgentDefReferences(
            GateRimMissionPawnCareDef pawnCare,
            ICollection<string> missing)
        {
            if (!string.IsNullOrWhiteSpace(pawnCare.pawnKindDefName)
                && DefDatabase<PawnKindDef>.GetNamedSilentFail(
                    pawnCare.pawnKindDefName) == null)
            {
                missing.Add(
                    "unknown PawnKindDef " + pawnCare.pawnKindDefName);
            }

            ValidateHediffDefReference(
                pawnCare.initialHediffDefName,
                "initial",
                missing);
            ValidateHediffDefReference(
                pawnCare.recoveryHediffDefName,
                "recovery",
                missing);
            ValidateHediffDefReference(
                pawnCare.optionalIllnessHediffDefName,
                "optional illness",
                missing);
        }

        private static void ValidateHediffDefReference(
            string defName,
            string label,
            ICollection<string> missing)
        {
            if (!string.IsNullOrWhiteSpace(defName)
                && DefDatabase<HediffDef>.GetNamedSilentFail(defName) == null)
            {
                missing.Add("unknown " + label + " HediffDef " + defName);
            }
        }

        public bool HasCompleteMedicalSupplyConfiguration(
            out string error)
        {
            List<string> missing = new List<string>();

            if (OfferDurationTicks <= 0)
            {
                missing.Add("offer duration");
            }

            if (DeadlineTicks <= 0)
            {
                missing.Add("deadline");
            }

            Require(AcceptActionKey, "accept action", missing);
            Require(OfferLetterLabelKey, "offer letter label", missing);
            Require(OfferLetterTextKey, "offer letter text", missing);
            Require(OfferExpiredMessageKey, "offer expiry message", missing);
            Require(AcceptedMessageKey, "accepted message", missing);
            Require(SuccessLetterLabelKey, "success letter label", missing);
            Require(FailureLetterLabelKey, "failure letter label", missing);
            Require(OfferedStatusKey, "offered status", missing);
            Require(ActiveStatusKey, "active status", missing);
            Require(ReadyStatusKey, "ready status", missing);
            Require(SuccessTrustMessageKey, "success trust message", missing);
            Require(FailureTrustMessageKey, "failure trust message", missing);
            Require(MedicalSupplyThingDefName, "handoff resource", missing);
            Require(
                MedicalSupplyDialogueJobDefName,
                "handoff dialogue job",
                missing);
            Require(MedicalSupplySkillDefName, "negotiation skill", missing);
            Require(SkillXpRewardDefName, "skill XP reward", missing);

            string[] runtimeTextIds =
            {
                "arrivalLabel",
                "arrivalText",
                "liaisonReady",
                "talkToLiaison",
                "noLongerActive",
                "liaisonEnRoute",
                "windowExpired",
                "operatorIncapable",
                "cannotReachLiaison",
                "liaisonReserved",
                "needMedicine",
                "jobUnavailable",
                "dialogTitle",
                "dialogText",
                "giveMedicine",
                "cancelDialogue",
                "statusApproaching",
                "failureTimeout",
                "failureDeath",
                "failureLost",
                "failureCaptured",
                "postHandoffDeathLabel",
                "postHandoffDeathText",
                "postHandoffDeathTrust"
            };

            foreach (string runtimeTextId in runtimeTextIds)
            {
                Require(
                    GetRuntimeTextKey(runtimeTextId),
                    "runtime text " + runtimeTextId,
                    missing);
            }

            GateRimMissionHandoffDef handoff = Handoff;

            if (handoff == null)
            {
                missing.Add("handoff profile");
            }
            else
            {
                Require(
                    handoff.liaisonPawnKindDefName,
                    "liaison pawn kind",
                    missing);

                if (handoff.arrivalMinimumDelayTicks <= 0
                    || handoff.arrivalMaximumDelayTicks
                        < handoff.arrivalMinimumDelayTicks)
                {
                    missing.Add("arrival delay range");
                }

                if (handoff.departureGraceTicks <= 0)
                {
                    missing.Add("departure grace duration");
                }

                if (handoff.postHandoffDeathTrustChange >= 0)
                {
                    missing.Add("negative post-handoff death trust change");
                }

                if (!string.IsNullOrWhiteSpace(
                        handoff.liaisonPawnKindDefName)
                    && DefDatabase<PawnKindDef>.GetNamedSilentFail(
                        handoff.liaisonPawnKindDefName) == null)
                {
                    missing.Add(
                        "unknown PawnKindDef "
                        + handoff.liaisonPawnKindDefName);
                }
            }

            if (MedicalSupplyRequiredCount <= 0)
            {
                missing.Add("positive handoff resource count");
            }

            if (!string.Equals(
                    MedicalSupplySkillDefName,
                    SkillXpRewardDefName,
                    StringComparison.OrdinalIgnoreCase))
            {
                missing.Add("handoff skill must match skill XP reward");
            }

            if (SkillXpRewardAmount <= 0)
            {
                missing.Add("positive skill XP reward amount");
            }

            if (!string.IsNullOrWhiteSpace(MedicalSupplyThingDefName)
                && DefDatabase<ThingDef>.GetNamedSilentFail(
                    MedicalSupplyThingDefName) == null)
            {
                missing.Add(
                    "unknown handoff ThingDef "
                    + MedicalSupplyThingDefName);
            }

            if (!string.IsNullOrWhiteSpace(
                    MedicalSupplyDialogueJobDefName)
                && DefDatabase<JobDef>.GetNamedSilentFail(
                    MedicalSupplyDialogueJobDefName) == null)
            {
                missing.Add(
                    "unknown handoff JobDef "
                    + MedicalSupplyDialogueJobDefName);
            }

            if (!string.IsNullOrWhiteSpace(MedicalSupplySkillDefName)
                && DefDatabase<SkillDef>.GetNamedSilentFail(
                    MedicalSupplySkillDefName) == null)
            {
                missing.Add(
                    "unknown handoff SkillDef "
                    + MedicalSupplySkillDefName);
            }

            if (MinimumRecurrenceDelayTicks <= 0
                || MaximumRecurrenceDelayTicks < MinimumRecurrenceDelayTicks)
            {
                missing.Add("recurrence delay range");
            }

            string[] recurrenceContextKeys =
            {
                "TokraTrust.Wary",
                "TokraTrust.Neutral",
                "TokraTrust.Cooperative",
                "TokraTrust.Trusted"
            };

            foreach (string contextKey in recurrenceContextKeys)
            {
                int contextMinimumDelay;
                int contextMaximumDelay;

                if (MissionDef?.recurrence == null
                    || !MissionDef.recurrence.TryGetDelayRange(
                        contextKey,
                        out contextMinimumDelay,
                        out contextMaximumDelay)
                    || contextMinimumDelay <= 0
                    || contextMaximumDelay < contextMinimumDelay)
                {
                    missing.Add(
                        "recurrence delay range for " + contextKey);
                }
            }

            if (MissionDef?.texts?.successLetterTexts == null
                || MissionDef.texts.successLetterTexts.Count == 0)
            {
                missing.Add("success letter text variants");
            }

            error = missing.Count == 0
                ? null
                : string.Join(", ", missing);
            return missing.Count == 0;
        }


        public bool HasCompleteJaffaOfficerCaptureConfiguration(
            out string error)
        {
            List<string> missing = new List<string>();

            if (OfferDurationTicks <= 0)
            {
                missing.Add("offer duration");
            }

            if (DeadlineTicks <= 0)
            {
                missing.Add("deadline");
            }

            Require(AcceptActionKey, "accept action", missing);
            Require(CompleteActionKey, "communicator completion action", missing);
            Require(OfferLetterLabelKey, "offer letter label", missing);
            Require(OfferLetterTextKey, "offer letter text", missing);
            Require(OfferExpiredMessageKey, "offer expiry message", missing);
            Require(AcceptedMessageKey, "accepted message", missing);
            Require(SuccessLetterLabelKey, "success letter label", missing);
            Require(FailureLetterLabelKey, "failure letter label", missing);
            Require(OfferedStatusKey, "offered status", missing);
            Require(ActiveStatusKey, "active status", missing);
            Require(ReadyStatusKey, "ready status", missing);
            Require(SuccessTrustMessageKey, "success trust message", missing);
            Require(FailureTrustMessageKey, "failure trust message", missing);

            string[] runtimeTextIds =
            {
                "spawnFailed",
                "targetLetterLabel",
                "targetLetterText",
                "failureTimeout",
                "failureSiteLost",
                "failureTargetKilled",
                "failureTargetLost"
            };

            foreach (string runtimeTextId in runtimeTextIds)
            {
                Require(
                    GetRuntimeTextKey(runtimeTextId),
                    "runtime text " + runtimeTextId,
                    missing);
            }

            GateRimMissionCaptureDef profile = Capture;

            if (profile == null)
            {
                missing.Add("capture profile");
            }
            else
            {
                Require(profile.worldObjectDefName, "world object Def", missing);
                Require(
                    profile.targetPawnKindDefName,
                    "target PawnKindDef",
                    missing);
                Require(
                    profile.captureToolThingDefName,
                    "capture tool ThingDef",
                    missing);
                Require(
                    profile.restraintHediffDefName,
                    "transfer-restraint HediffDef",
                    missing);
                Require(
                    profile.escortWarriorPawnKindDefName,
                    "escort warrior PawnKindDef",
                    missing);
                Require(
                    profile.extractionPawnKindDefName,
                    "extraction PawnKindDef",
                    missing);

                if (profile.minimumTileDistance <= 0
                    || profile.maximumTileDistance
                        < profile.minimumTileDistance)
                {
                    missing.Add("world-site distance range");
                }

                if (profile.mapSize < 80)
                {
                    missing.Add("map size");
                }

                if (profile.escortMinimumCount <= 0
                    || profile.escortThreatFactor <= 0f)
                {
                    missing.Add("adaptive escort profile");
                }

                if (profile.extractionMinimumDelayTicks <= 0
                    || profile.extractionMaximumDelayTicks
                        < profile.extractionMinimumDelayTicks)
                {
                    missing.Add("extraction arrival delay range");
                }

                if (profile.extractionRetryTicks <= 0)
                {
                    missing.Add("positive extraction retry delay");
                }

                if (profile.extractionTeamMinimumCount <= 0
                    || profile.extractionTeamMaximumCount
                        < profile.extractionTeamMinimumCount)
                {
                    missing.Add("extraction team count range");
                }

                WorldObjectDef worldObjectDef
                    = string.IsNullOrWhiteSpace(profile.worldObjectDefName)
                        ? null
                        : DefDatabase<WorldObjectDef>.GetNamedSilentFail(
                            profile.worldObjectDefName);

                if (worldObjectDef?.worldObjectClass == null
                    || !typeof(WorldObject_TokraJaffaOfficerCaptureSite)
                        .IsAssignableFrom(worldObjectDef.worldObjectClass))
                {
                    missing.Add("capture WorldObjectDef class");
                }

                string[] pawnKindDefNames =
                {
                    profile.targetPawnKindDefName,
                    profile.escortWarriorPawnKindDefName,
                    profile.escortGuardPawnKindDefName,
                    profile.extractionPawnKindDefName
                };

                foreach (string pawnKindDefName in pawnKindDefNames
                    .Where(item => !string.IsNullOrWhiteSpace(item)))
                {
                    if (DefDatabase<PawnKindDef>.GetNamedSilentFail(
                            pawnKindDefName) == null)
                    {
                        missing.Add(
                            "unknown PawnKindDef " + pawnKindDefName);
                    }
                }

                if (DefDatabase<ThingDef>.GetNamedSilentFail(
                        profile.captureToolThingDefName) == null)
                {
                    missing.Add(
                        "unknown capture tool ThingDef "
                        + profile.captureToolThingDefName);
                }


                if (DefDatabase<HediffDef>.GetNamedSilentFail(
                        profile.restraintHediffDefName) == null)
                {
                    missing.Add(
                        "unknown transfer-restraint HediffDef "
                        + profile.restraintHediffDefName);
                }
            }

            if (MinimumRecurrenceDelayTicks <= 0
                || MaximumRecurrenceDelayTicks
                    < MinimumRecurrenceDelayTicks)
            {
                missing.Add("recurrence delay range");
            }

            if (MissionDef?.difficulty?.mode
                != GateRimMissionDifficultyMode.ThreatPointsScaled)
            {
                missing.Add("ThreatPointsScaled difficulty profile");
            }

            if (MissionDef?.texts?.offerLetterTexts == null
                || MissionDef.texts.offerLetterTexts.Count < 2)
            {
                missing.Add("offer letter text variants");
            }

            if (MissionDef?.texts?.successLetterTexts == null
                || MissionDef.texts.successLetterTexts.Count < 2)
            {
                missing.Add("success letter text variants");
            }

            error = missing.Count == 0
                ? null
                : string.Join(", ", missing);
            return missing.Count == 0;
        }


        public bool HasCompleteDecoyTransmissionDefenseConfiguration(
            out string error)
        {
            List<string> missing = new List<string>();

            if (OfferDurationTicks <= 0)
            {
                missing.Add("offer duration");
            }

            if (DeadlineTicks != 0)
            {
                missing.Add("zero accepted-state deadline");
            }

            if (DecoyRaidDelayMinimumTicks <= 0
                || DecoyRaidDelayMaximumTicks
                    < DecoyRaidDelayMinimumTicks)
            {
                missing.Add("valid raid delay range");
            }

            if (DecoyRaidRetryTicks <= 0)
            {
                missing.Add("raid retry delay");
            }

            Require(AcceptActionKey, "accept action", missing);
            Require(OfferLetterLabelKey, "offer letter label", missing);
            Require(OfferLetterTextKey, "offer letter text", missing);
            Require(OfferExpiredMessageKey, "offer expiry message", missing);
            Require(OfferedStatusKey, "offered status", missing);
            Require(ActiveStatusKey, "waiting status", missing);
            Require(ReadyStatusKey, "assault status", missing);
            Require(SuccessTrustMessageKey, "success trust message", missing);
            Require(FailureTrustMessageKey, "failure trust message", missing);
            Require(AcceptedMessageKey, "accepted message", missing);
            Require(SuccessLetterLabelKey, "success letter label", missing);
            Require(FailureLetterLabelKey, "failure letter label", missing);
            Require(
                DecoyRaidIncidentDefName,
                "Goa'uld assault incident",
                missing);

            if (!string.IsNullOrWhiteSpace(ObjectiveThingDefName))
            {
                missing.Add("no physical objectiveThingDef");
            }

            string[] runtimeTextIds =
            {
                "failureTimeout",
                "failureMapLost",
                "failureHostage",
                "failureLoot",
                "statusWaiting",
                "statusAssault"
            };

            foreach (string runtimeTextId in runtimeTextIds)
            {
                Require(
                    GetRuntimeTextKey(runtimeTextId),
                    "runtime text " + runtimeTextId,
                    missing);
            }

            if (MissionDef?.texts?.successLetterTexts == null
                || MissionDef.texts.successLetterTexts.Count < 2)
            {
                missing.Add("success letter text variants");
            }

            if (MissionDef?.texts?.offerLetterTexts == null
                || MissionDef.texts.offerLetterTexts.Count < 2)
            {
                missing.Add("offer letter text variants");
            }

            IncidentDef incidentDef = string.IsNullOrWhiteSpace(
                    DecoyRaidIncidentDefName)
                ? null
                : DefDatabase<IncidentDef>.GetNamedSilentFail(
                    DecoyRaidIncidentDefName);

            if (incidentDef == null || incidentDef.category == null)
            {
                missing.Add("known categorized assault IncidentDef");
            }

            RaidStrategyDef breachStrategy
                = DefDatabase<RaidStrategyDef>.GetNamedSilentFail(
                    "SG1_GoauldJaffaLuredBreachingAssault");

            if (breachStrategy == null || breachStrategy.Worker == null)
            {
                missing.Add("known breaching RaidStrategyDef");
            }

            PawnGroupKindDef assaultGroupKind
                = DefDatabase<PawnGroupKindDef>.GetNamedSilentFail(
                    "SG1_TokraDiversionAssault");
            FactionDef factionDef
                = GR_DefOf.SG1_GoauldSystemLordPrototype;
            PawnGroupMaker assaultGroupMaker
                = factionDef?.pawnGroupMakers?.FirstOrDefault(
                    groupMaker => groupMaker != null
                        && groupMaker.kindDef == assaultGroupKind);

            if (assaultGroupKind == null
                || assaultGroupKind.Worker == null)
            {
                missing.Add("known diversion assault PawnGroupKindDef");
            }
            else if (assaultGroupMaker == null)
            {
                missing.Add("Goa'uld diversion assault pawn group maker");
            }
            else if (assaultGroupMaker.options == null
                || !assaultGroupMaker.options.Any(
                    option => option != null
                        && option.kind != null
                        && option.kind.isGoodBreacher
                        && option.kind.weaponTags != null
                        && option.kind.weaponTags.Contains(
                            "SG1_MatokStaff")))
            {
                missing.Add("Ma'Tok-equipped Jaffa breacher PawnKindDef");
            }

            if (MissionDef?.difficulty == null
                || MissionDef.difficulty.mode
                    != GateRimMissionDifficultyMode.ThreatPointsScaled
                || MissionDef.difficulty.pointsFactor <= 0f
                || MissionDef.difficulty.minimumPoints <= 0f
                || MissionDef.difficulty.maximumPoints
                    < MissionDef.difficulty.minimumPoints)
            {
                missing.Add("scaled threat-point difficulty bounds");
            }

            error = missing.Count == 0
                ? null
                : string.Join(", ", missing);
            return missing.Count == 0;
        }

        public bool HasCompleteIntelligenceConfiguration(
            out string error)
        {
            List<string> missing = new List<string>();

            if (OfferDurationTicks <= 0)
            {
                missing.Add("offer duration");
            }

            if (DeadlineTicks <= 0)
            {
                missing.Add("deadline");
            }

            Require(AcceptActionKey, "accept action", missing);
            Require(CompleteActionKey, "complete action", missing);
            Require(OfferLetterLabelKey, "offer letter label", missing);
            Require(OfferLetterTextKey, "offer letter text", missing);
            Require(OfferExpiredMessageKey, "offer expiry message", missing);
            Require(OfferedStatusKey, "offered status", missing);
            Require(ActiveStatusKey, "active status", missing);
            Require(ReadyStatusKey, "ready status", missing);
            Require(SuccessTrustMessageKey, "success trust message", missing);
            Require(FailureTrustMessageKey, "failure trust message", missing);
            Require(ObjectiveThingDefName, "objectiveThingDef", missing);
            Require(
                IntelligenceAnalysisTargetDefName,
                "analysis target",
                missing);
            Require(
                IntelligenceAnalysisJobDefName,
                "analysis job",
                missing);
            Require(IntelligenceSkillDefName, "analysis skill", missing);
            Require(SkillXpRewardDefName, "skill XP reward", missing);
            Require(AcceptedMessageKey, "accepted message", missing);
            Require(SuccessLetterLabelKey, "success letter label", missing);
            Require(FailureLetterLabelKey, "failure letter label", missing);
            Require(
                IntelligencePatrolIncidentDefName,
                "interference patrol incident",
                missing);

            string[] runtimeTextIds =
            {
                "spawnFailed",
                "targetLetterLabel",
                "targetLetterText",
                "failureTimeout",
                "failureObjectiveLost",
                "noLongerActive",
                "windowExpired",
                "cautiousStarted",
                "acceleratedStarted",
                "methodLocked",
                "jobUnavailable",
                "statusAwaitingAnalysis",
                "statusAnalyzing",
                "methodCautious",
                "methodAccelerated"
            };

            foreach (string runtimeTextId in runtimeTextIds)
            {
                Require(
                    GetRuntimeTextKey(runtimeTextId),
                    "runtime text " + runtimeTextId,
                    missing);
            }

            string[] namedTextBanks =
            {
                "cautiousSuccess",
                "acceleratedSuccess",
                "interferenceSuccess"
            };

            foreach (string bankId in namedTextBanks)
            {
                List<GateRimMissionTextVariantDef> variants
                    = MissionDef?.GetNamedTextBank(bankId);

                if (variants == null || variants.Count == 0)
                {
                    missing.Add("named text bank " + bankId);
                }
            }

            if (IntelligenceCautiousWorkTicks <= 0)
            {
                missing.Add("cautious analysis work ticks");
            }

            if (IntelligenceAcceleratedWorkTicks <= 0)
            {
                missing.Add("accelerated analysis work ticks");
            }

            GateRimMissionObjectiveDef acceleratedObjective
                = MissionDef?.GetObjective("accelerated", "AnalyzeThing");

            if (acceleratedObjective == null)
            {
                missing.Add("accelerated analysis objective");
            }
            else
            {
                if (!string.Equals(
                        acceleratedObjective.targetDefName,
                        IntelligenceAnalysisTargetDefName,
                        StringComparison.OrdinalIgnoreCase))
                {
                    missing.Add(
                        "accelerated analysis target must match cautious target");
                }

                if (!string.Equals(
                        acceleratedObjective.jobDefName,
                        IntelligenceAnalysisJobDefName,
                        StringComparison.OrdinalIgnoreCase))
                {
                    missing.Add(
                        "accelerated analysis job must match cautious job");
                }

                if (!string.Equals(
                        acceleratedObjective.skillDefName,
                        IntelligenceSkillDefName,
                        StringComparison.OrdinalIgnoreCase))
                {
                    missing.Add(
                        "accelerated analysis skill must match cautious skill");
                }
            }

            if (IntelligenceAcceleratedXpBonus <= 0)
            {
                missing.Add("accelerated analysis XP bonus");
            }

            if (SkillXpRewardAmount <= 0)
            {
                missing.Add("positive skill XP reward amount");
            }

            if (IntelligenceInterferenceChance < 0f
                || IntelligenceInterferenceChance > 1f)
            {
                missing.Add("interference chance between 0 and 1");
            }

            if (IntelligencePatrolDelayMinimumTicks <= 0
                || IntelligencePatrolDelayMaximumTicks
                    < IntelligencePatrolDelayMinimumTicks)
            {
                missing.Add("patrol delay range");
            }

            if (IntelligencePatrolRetryTicks <= 0)
            {
                missing.Add("positive patrol retry delay");
            }

            if (MinimumRecurrenceDelayTicks <= 0
                || MaximumRecurrenceDelayTicks < MinimumRecurrenceDelayTicks)
            {
                missing.Add("recurrence delay range");
            }

            if (MissionDef?.difficulty?.mode
                != GateRimMissionDifficultyMode.ThreatPointsScaled)
            {
                missing.Add("ThreatPointsScaled difficulty profile");
            }

            string[] recurrenceContextKeys =
            {
                "TokraTrust.Wary",
                "TokraTrust.Neutral",
                "TokraTrust.Cooperative",
                "TokraTrust.Trusted"
            };

            foreach (string contextKey in recurrenceContextKeys)
            {
                int contextMinimumDelay;
                int contextMaximumDelay;

                if (MissionDef?.recurrence == null
                    || !MissionDef.recurrence.TryGetDelayRange(
                        contextKey,
                        out contextMinimumDelay,
                        out contextMaximumDelay)
                    || contextMinimumDelay <= 0
                    || contextMaximumDelay < contextMinimumDelay)
                {
                    missing.Add(
                        "recurrence delay range for " + contextKey);
                }
            }

            ValidateIntelligenceTargetConsistency(missing);
            ValidateIntelligenceDefReferences(missing);

            error = missing.Count == 0
                ? null
                : string.Join(", ", missing);
            return missing.Count == 0;
        }

        private void ValidateIntelligenceTargetConsistency(
            ICollection<string> missing)
        {
            if (!string.IsNullOrWhiteSpace(ObjectiveThingDefName)
                && !string.Equals(
                    ObjectiveThingDefName,
                    IntelligenceAnalysisTargetDefName,
                    StringComparison.OrdinalIgnoreCase))
            {
                missing.Add("objectiveThingDef must match analysis target");
            }
        }

        private void ValidateIntelligenceDefReferences(
            ICollection<string> missing)
        {
            if (!string.IsNullOrWhiteSpace(ObjectiveThingDefName)
                && DefDatabase<ThingDef>.GetNamedSilentFail(
                    ObjectiveThingDefName) == null)
            {
                missing.Add(
                    "unknown objective ThingDef " + ObjectiveThingDefName);
            }

            if (!string.IsNullOrWhiteSpace(IntelligenceAnalysisJobDefName)
                && DefDatabase<JobDef>.GetNamedSilentFail(
                    IntelligenceAnalysisJobDefName) == null)
            {
                missing.Add(
                    "unknown analysis JobDef "
                    + IntelligenceAnalysisJobDefName);
            }

            if (!string.IsNullOrWhiteSpace(IntelligenceSkillDefName)
                && DefDatabase<SkillDef>.GetNamedSilentFail(
                    IntelligenceSkillDefName) == null)
            {
                missing.Add(
                    "unknown analysis SkillDef "
                    + IntelligenceSkillDefName);
            }

            if (!string.IsNullOrWhiteSpace(SkillXpRewardDefName)
                && DefDatabase<SkillDef>.GetNamedSilentFail(
                    SkillXpRewardDefName) == null)
            {
                missing.Add(
                    "unknown reward SkillDef " + SkillXpRewardDefName);
            }

            if (!string.IsNullOrWhiteSpace(
                    IntelligencePatrolIncidentDefName)
                && DefDatabase<IncidentDef>.GetNamedSilentFail(
                    IntelligencePatrolIncidentDefName) == null)
            {
                missing.Add(
                    "unknown patrol IncidentDef "
                    + IntelligencePatrolIncidentDefName);
            }
        }

        private void ValidateObservationTargetConsistency(
            ICollection<string> missing)
        {
            if (!string.IsNullOrWhiteSpace(ObjectiveThingDefName)
                && !string.Equals(
                    ObjectiveThingDefName,
                    ObservationDeviceDefName,
                    StringComparison.OrdinalIgnoreCase))
            {
                missing.Add(
                    "objectiveThingDef must match deployment target");
            }

            if (!string.IsNullOrWhiteSpace(ObservationWorkTargetDefName)
                && !string.Equals(
                    ObservationWorkTargetDefName,
                    ObservationPointDefName,
                    StringComparison.OrdinalIgnoreCase))
            {
                missing.Add(
                    "observation work target must match observation point");
            }

            if (!string.IsNullOrWhiteSpace(ObservationRecoveryTargetDefName)
                && !string.Equals(
                    ObservationRecoveryTargetDefName,
                    ObservationDeviceDefName,
                    StringComparison.OrdinalIgnoreCase))
            {
                missing.Add(
                    "recovery target must match observation device");
            }
        }

        private void ValidateObservationDefReferences(
            ICollection<string> missing)
        {
            if (!string.IsNullOrWhiteSpace(ObservationDeviceDefName)
                && DefDatabase<ThingDef>.GetNamedSilentFail(
                    ObservationDeviceDefName) == null)
            {
                missing.Add(
                    "unknown device ThingDef " + ObservationDeviceDefName);
            }

            if (!string.IsNullOrWhiteSpace(ObservationPointDefName)
                && DefDatabase<ThingDef>.GetNamedSilentFail(
                    ObservationPointDefName) == null)
            {
                missing.Add(
                    "unknown observation point ThingDef "
                    + ObservationPointDefName);
            }

            if (!string.IsNullOrWhiteSpace(
                    ObservationDeploymentJobDefName)
                && DefDatabase<JobDef>.GetNamedSilentFail(
                    ObservationDeploymentJobDefName) == null)
            {
                missing.Add(
                    "unknown deployment JobDef "
                    + ObservationDeploymentJobDefName);
            }

            if (!string.IsNullOrWhiteSpace(
                    ObservationTransmissionJobDefName)
                && DefDatabase<JobDef>.GetNamedSilentFail(
                    ObservationTransmissionJobDefName) == null)
            {
                missing.Add(
                    "unknown transmission JobDef "
                    + ObservationTransmissionJobDefName);
            }

            if (!string.IsNullOrWhiteSpace(ObservationSkillDefName)
                && DefDatabase<SkillDef>.GetNamedSilentFail(
                    ObservationSkillDefName) == null)
            {
                missing.Add(
                    "unknown work SkillDef " + ObservationSkillDefName);
            }

            if (!string.IsNullOrWhiteSpace(SkillXpRewardDefName)
                && DefDatabase<SkillDef>.GetNamedSilentFail(
                    SkillXpRewardDefName) == null)
            {
                missing.Add(
                    "unknown reward SkillDef " + SkillXpRewardDefName);
            }
        }

        private static void Require(
            string value,
            string label,
            ICollection<string> missing)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                missing.Add(label);
            }
        }

        private static float GetMissionWeight(
            GateRimMissionDef missionDef,
            string contextKey)
        {
            float weight;
            return missionDef.recurrence != null
                && missionDef.recurrence.TryGetWeight(contextKey, out weight)
                ? weight
                : 0f;
        }
    }

    internal static class TokraOrganicOperationFramework
    {
        public const float LegacyRepeatedArchetypeWeightFactor = 0.25f;
        private const string ObservationMissionDefName
            = "SG1_TokraOrganic_GoauldObservation";
        private const string IntelligenceMissionDefName
            = "SG1_TokraOrganic_IntelligenceRecovery";
        private const string WoundedAgentMissionDefName
            = "SG1_TokraOrganic_WoundedAgentCare";
        private const string MedicalSupplyMissionDefName
            = "SG1_TokraOrganic_MedicalSupplyHandoff";
        private const string DistressCallMissionDefName
            = "SG1_TokraOrganic_DistressCall";
        private const string TemporaryBaseDeliveryMissionDefName
            = "SG1_TokraOrganic_TemporaryBaseDelivery";
        private const string DecoyTransmissionDefenseMissionDefName
            = "SG1_TokraOrganic_DecoyTransmissionDefense";
        private const string JaffaOfficerCaptureMissionDefName
            = "SG1_TokraOrganic_JaffaOfficerCapture";

        private static GateRimMissionDef cachedObservationMissionDef;
        private static TokraOrganicOperationDefinition
            cachedObservationDefinition;
        private static bool observationConfigurationErrorLogged;
        private static GateRimMissionDef cachedIntelligenceMissionDef;
        private static TokraOrganicOperationDefinition
            cachedIntelligenceDefinition;
        private static bool intelligenceConfigurationErrorLogged;
        private static GateRimMissionDef cachedWoundedAgentMissionDef;
        private static TokraOrganicOperationDefinition
            cachedWoundedAgentDefinition;
        private static bool woundedAgentConfigurationErrorLogged;
        private static GateRimMissionDef cachedMedicalSupplyMissionDef;
        private static TokraOrganicOperationDefinition
            cachedMedicalSupplyDefinition;
        private static bool medicalSupplyConfigurationErrorLogged;
        private static GateRimMissionDef cachedDistressCallMissionDef;
        private static TokraOrganicOperationDefinition
            cachedDistressCallDefinition;
        private static bool distressCallConfigurationErrorLogged;
        private static GateRimMissionDef cachedTemporaryBaseDeliveryMissionDef;
        private static TokraOrganicOperationDefinition
            cachedTemporaryBaseDeliveryDefinition;
        private static bool temporaryBaseDeliveryConfigurationErrorLogged;
        private static GateRimMissionDef
            cachedDecoyTransmissionDefenseMissionDef;
        private static TokraOrganicOperationDefinition
            cachedDecoyTransmissionDefenseDefinition;
        private static bool
            decoyTransmissionDefenseConfigurationErrorLogged;
        private static GateRimMissionDef cachedJaffaOfficerCaptureMissionDef;
        private static TokraOrganicOperationDefinition
            cachedJaffaOfficerCaptureDefinition;
        private static bool jaffaOfficerCaptureConfigurationErrorLogged;

        public static IEnumerable<TokraOrganicOperationDefinition>
            AllDefinitions
        {
            get
            {
                TokraOrganicOperationDefinition observation
                    = ResolveObservationDefinition();

                if (observation != null)
                {
                    yield return observation;
                }

                TokraOrganicOperationDefinition intelligence
                    = ResolveIntelligenceDefinition();

                if (intelligence != null)
                {
                    yield return intelligence;
                }

                TokraOrganicOperationDefinition woundedAgent
                    = ResolveWoundedAgentDefinition();

                if (woundedAgent != null)
                {
                    yield return woundedAgent;
                }

                TokraOrganicOperationDefinition medicalSupply
                    = ResolveMedicalSupplyDefinition();

                if (medicalSupply != null)
                {
                    yield return medicalSupply;
                }

                TokraOrganicOperationDefinition distressCall
                    = ResolveDistressCallDefinition();

                if (distressCall != null)
                {
                    yield return distressCall;
                }

                TokraOrganicOperationDefinition temporaryBaseDelivery
                    = ResolveTemporaryBaseDeliveryDefinition();

                if (temporaryBaseDelivery != null)
                {
                    yield return temporaryBaseDelivery;
                }

                TokraOrganicOperationDefinition decoyTransmissionDefense
                    = ResolveDecoyTransmissionDefenseDefinition();

                if (decoyTransmissionDefense != null)
                {
                    yield return decoyTransmissionDefense;
                }

                TokraOrganicOperationDefinition jaffaOfficerCapture
                    = ResolveJaffaOfficerCaptureDefinition();

                if (jaffaOfficerCapture != null)
                {
                    yield return jaffaOfficerCapture;
                }
            }
        }

        public static bool TryGetDefinition(
            TokraOrganicOperationArchetype archetype,
            out TokraOrganicOperationDefinition definition)
        {
            if (archetype
                == TokraOrganicOperationArchetype.GoauldObservation)
            {
                definition = ResolveObservationDefinition();
                return definition != null;
            }

            if (archetype
                == TokraOrganicOperationArchetype.DeadDropRecovery)
            {
                definition = ResolveIntelligenceDefinition();
                return definition != null;
            }

            if (archetype
                == TokraOrganicOperationArchetype.WoundedAgentCare)
            {
                definition = ResolveWoundedAgentDefinition();
                return definition != null;
            }

            if (archetype
                == TokraOrganicOperationArchetype.MedicalSupplyHandoff)
            {
                definition = ResolveMedicalSupplyDefinition();
                return definition != null;
            }

            if (archetype
                == TokraOrganicOperationArchetype.DistressCall)
            {
                definition = ResolveDistressCallDefinition();
                return definition != null;
            }

            if (archetype
                == TokraOrganicOperationArchetype.TemporaryBaseDelivery)
            {
                definition = ResolveTemporaryBaseDeliveryDefinition();
                return definition != null;
            }

            if (archetype
                == TokraOrganicOperationArchetype.DecoyTransmissionDefense)
            {
                definition = ResolveDecoyTransmissionDefenseDefinition();
                return definition != null;
            }

            if (archetype
                == TokraOrganicOperationArchetype.JaffaOfficerCapture)
            {
                definition = ResolveJaffaOfficerCaptureDefinition();
                return definition != null;
            }

            definition = null;
            return false;
        }

        public static TokraOrganicOperationDefinition GetDefinition(
            TokraOrganicOperationArchetype archetype)
        {
            TokraOrganicOperationDefinition definition;

            return TryGetDefinition(archetype, out definition)
                ? definition
                : null;
        }

        private static TokraOrganicOperationDefinition
            ResolveObservationDefinition()
        {
            GateRimMissionDef missionDef
                = DefDatabase<GateRimMissionDef>.GetNamedSilentFail(
                    ObservationMissionDefName);

            if (missionDef == null)
            {
                LogObservationConfigurationError(
                    "required MissionDef " + ObservationMissionDefName
                    + " is missing");
                return null;
            }

            if (cachedObservationDefinition == null
                || cachedObservationMissionDef != missionDef)
            {
                TokraOrganicOperationDefinition definition
                    = new TokraOrganicOperationDefinition(
                        TokraOrganicOperationArchetype.GoauldObservation,
                        missionDef);
                string error;

                if (!definition.HasCompleteObservationConfiguration(
                        out error))
                {
                    LogObservationConfigurationError(
                        ObservationMissionDefName
                        + " is incomplete: " + error);
                    return null;
                }

                cachedObservationMissionDef = missionDef;
                cachedObservationDefinition = definition;
            }

            return cachedObservationDefinition;
        }

        private static TokraOrganicOperationDefinition
            ResolveIntelligenceDefinition()
        {
            GateRimMissionDef missionDef
                = DefDatabase<GateRimMissionDef>.GetNamedSilentFail(
                    IntelligenceMissionDefName);

            if (missionDef == null)
            {
                LogIntelligenceConfigurationError(
                    "required MissionDef " + IntelligenceMissionDefName
                    + " is missing");
                return null;
            }

            if (cachedIntelligenceDefinition == null
                || cachedIntelligenceMissionDef != missionDef)
            {
                TokraOrganicOperationDefinition definition
                    = new TokraOrganicOperationDefinition(
                        TokraOrganicOperationArchetype.DeadDropRecovery,
                        missionDef);
                string error;

                if (!definition.HasCompleteIntelligenceConfiguration(
                        out error))
                {
                    LogIntelligenceConfigurationError(
                        IntelligenceMissionDefName
                        + " is incomplete: " + error);
                    return null;
                }

                cachedIntelligenceMissionDef = missionDef;
                cachedIntelligenceDefinition = definition;
            }

            return cachedIntelligenceDefinition;
        }

        private static TokraOrganicOperationDefinition
            ResolveWoundedAgentDefinition()
        {
            GateRimMissionDef missionDef
                = DefDatabase<GateRimMissionDef>.GetNamedSilentFail(
                    WoundedAgentMissionDefName);

            if (missionDef == null)
            {
                LogWoundedAgentConfigurationError(
                    "required MissionDef " + WoundedAgentMissionDefName
                    + " is missing");
                return null;
            }

            if (cachedWoundedAgentDefinition == null
                || cachedWoundedAgentMissionDef != missionDef)
            {
                TokraOrganicOperationDefinition definition
                    = new TokraOrganicOperationDefinition(
                        TokraOrganicOperationArchetype.WoundedAgentCare,
                        missionDef);
                string error;

                if (!definition.HasCompleteWoundedAgentConfiguration(
                        out error))
                {
                    LogWoundedAgentConfigurationError(
                        WoundedAgentMissionDefName
                        + " is incomplete: " + error);
                    return null;
                }

                cachedWoundedAgentMissionDef = missionDef;
                cachedWoundedAgentDefinition = definition;
            }

            return cachedWoundedAgentDefinition;
        }

        private static TokraOrganicOperationDefinition
            ResolveMedicalSupplyDefinition()
        {
            GateRimMissionDef missionDef
                = DefDatabase<GateRimMissionDef>.GetNamedSilentFail(
                    MedicalSupplyMissionDefName);

            if (missionDef == null)
            {
                LogMedicalSupplyConfigurationError(
                    "required MissionDef " + MedicalSupplyMissionDefName
                    + " is missing");
                return null;
            }

            if (cachedMedicalSupplyDefinition == null
                || cachedMedicalSupplyMissionDef != missionDef)
            {
                TokraOrganicOperationDefinition definition
                    = new TokraOrganicOperationDefinition(
                        TokraOrganicOperationArchetype.MedicalSupplyHandoff,
                        missionDef);
                string error;

                if (!definition.HasCompleteMedicalSupplyConfiguration(
                        out error))
                {
                    LogMedicalSupplyConfigurationError(
                        MedicalSupplyMissionDefName
                        + " is incomplete: " + error);
                    return null;
                }

                cachedMedicalSupplyMissionDef = missionDef;
                cachedMedicalSupplyDefinition = definition;
            }

            return cachedMedicalSupplyDefinition;
        }

        private static TokraOrganicOperationDefinition
            ResolveDistressCallDefinition()
        {
            GateRimMissionDef missionDef
                = DefDatabase<GateRimMissionDef>.GetNamedSilentFail(
                    DistressCallMissionDefName);

            if (missionDef == null)
            {
                LogDistressCallConfigurationError(
                    "required MissionDef " + DistressCallMissionDefName
                    + " is missing");
                return null;
            }

            if (cachedDistressCallDefinition == null
                || cachedDistressCallMissionDef != missionDef)
            {
                TokraOrganicOperationDefinition definition
                    = new TokraOrganicOperationDefinition(
                        TokraOrganicOperationArchetype.DistressCall,
                        missionDef);
                string error;

                if (!definition.HasCompleteDistressCallConfiguration(
                        out error))
                {
                    LogDistressCallConfigurationError(
                        DistressCallMissionDefName
                        + " is incomplete: " + error);
                    return null;
                }

                cachedDistressCallMissionDef = missionDef;
                cachedDistressCallDefinition = definition;
            }

            return cachedDistressCallDefinition;
        }


        private static TokraOrganicOperationDefinition
            ResolveTemporaryBaseDeliveryDefinition()
        {
            GateRimMissionDef missionDef
                = DefDatabase<GateRimMissionDef>.GetNamedSilentFail(
                    TemporaryBaseDeliveryMissionDefName);

            if (missionDef == null)
            {
                LogTemporaryBaseDeliveryConfigurationError(
                    "required MissionDef "
                    + TemporaryBaseDeliveryMissionDefName
                    + " is missing");
                return null;
            }

            if (cachedTemporaryBaseDeliveryDefinition == null
                || cachedTemporaryBaseDeliveryMissionDef != missionDef)
            {
                TokraOrganicOperationDefinition definition
                    = new TokraOrganicOperationDefinition(
                        TokraOrganicOperationArchetype
                            .TemporaryBaseDelivery,
                        missionDef);
                string error;

                if (!definition
                    .HasCompleteTemporaryBaseDeliveryConfiguration(
                        out error))
                {
                    LogTemporaryBaseDeliveryConfigurationError(
                        TemporaryBaseDeliveryMissionDefName
                        + " is incomplete: " + error);
                    return null;
                }

                cachedTemporaryBaseDeliveryMissionDef = missionDef;
                cachedTemporaryBaseDeliveryDefinition = definition;
            }

            return cachedTemporaryBaseDeliveryDefinition;
        }


        private static TokraOrganicOperationDefinition
            ResolveDecoyTransmissionDefenseDefinition()
        {
            GateRimMissionDef missionDef
                = DefDatabase<GateRimMissionDef>.GetNamedSilentFail(
                    DecoyTransmissionDefenseMissionDefName);

            if (missionDef == null)
            {
                LogDecoyTransmissionDefenseConfigurationError(
                    "required MissionDef "
                    + DecoyTransmissionDefenseMissionDefName
                    + " is missing");
                return null;
            }

            if (cachedDecoyTransmissionDefenseDefinition == null
                || cachedDecoyTransmissionDefenseMissionDef != missionDef)
            {
                TokraOrganicOperationDefinition definition
                    = new TokraOrganicOperationDefinition(
                        TokraOrganicOperationArchetype
                            .DecoyTransmissionDefense,
                        missionDef);
                string error;

                if (!definition
                    .HasCompleteDecoyTransmissionDefenseConfiguration(
                        out error))
                {
                    LogDecoyTransmissionDefenseConfigurationError(
                        DecoyTransmissionDefenseMissionDefName
                        + " is incomplete: " + error);
                    return null;
                }

                cachedDecoyTransmissionDefenseMissionDef = missionDef;
                cachedDecoyTransmissionDefenseDefinition = definition;
            }

            return cachedDecoyTransmissionDefenseDefinition;
        }

        private static TokraOrganicOperationDefinition
            ResolveJaffaOfficerCaptureDefinition()
        {
            GateRimMissionDef missionDef
                = DefDatabase<GateRimMissionDef>.GetNamedSilentFail(
                    JaffaOfficerCaptureMissionDefName);

            if (missionDef == null)
            {
                LogJaffaOfficerCaptureConfigurationError(
                    "required MissionDef "
                    + JaffaOfficerCaptureMissionDefName
                    + " is missing");
                return null;
            }

            if (cachedJaffaOfficerCaptureDefinition == null
                || cachedJaffaOfficerCaptureMissionDef != missionDef)
            {
                TokraOrganicOperationDefinition definition
                    = new TokraOrganicOperationDefinition(
                        TokraOrganicOperationArchetype.JaffaOfficerCapture,
                        missionDef);
                string error;

                if (!definition.HasCompleteJaffaOfficerCaptureConfiguration(
                        out error))
                {
                    LogJaffaOfficerCaptureConfigurationError(
                        JaffaOfficerCaptureMissionDefName
                        + " is incomplete: " + error);
                    return null;
                }

                cachedJaffaOfficerCaptureMissionDef = missionDef;
                cachedJaffaOfficerCaptureDefinition = definition;
            }

            return cachedJaffaOfficerCaptureDefinition;
        }

        private static void LogJaffaOfficerCaptureConfigurationError(
            string detail)
        {
            if (jaffaOfficerCaptureConfigurationErrorLogged)
            {
                return;
            }

            jaffaOfficerCaptureConfigurationErrorLogged = true;
            Log.Error(
                "[GateRim SG-1] Tok'ra Jaffa-officer capture operation "
                + "disabled: " + detail + ".");
        }


        private static void LogDecoyTransmissionDefenseConfigurationError(
            string detail)
        {
            if (decoyTransmissionDefenseConfigurationErrorLogged)
            {
                return;
            }

            decoyTransmissionDefenseConfigurationErrorLogged = true;
            Log.Error(
                "[GateRim SG-1] Tok'ra diversion assault "
                + "operation disabled: " + detail + ".");
        }

        private static void LogTemporaryBaseDeliveryConfigurationError(
            string detail)
        {
            if (temporaryBaseDeliveryConfigurationErrorLogged)
            {
                return;
            }

            temporaryBaseDeliveryConfigurationErrorLogged = true;
            Log.Error(
                "[GateRim SG-1] Tok'ra temporary-base delivery operation "
                + "disabled: " + detail + ".");
        }

        private static void LogDistressCallConfigurationError(string detail)
        {
            if (distressCallConfigurationErrorLogged)
            {
                return;
            }

            distressCallConfigurationErrorLogged = true;
            Log.Error(
                "[GateRim SG-1] Tok'ra distress-call operation disabled: "
                + detail + ".");
        }

        private static void LogMedicalSupplyConfigurationError(string detail)
        {
            if (medicalSupplyConfigurationErrorLogged)
            {
                return;
            }

            medicalSupplyConfigurationErrorLogged = true;
            Log.Error(
                "[GateRim SG-1] Tok'ra medical supply handoff operation "
                + "disabled: " + detail + ".");
        }

        private static void LogWoundedAgentConfigurationError(string detail)
        {
            if (woundedAgentConfigurationErrorLogged)
            {
                return;
            }

            woundedAgentConfigurationErrorLogged = true;
            Log.Error(
                "[GateRim SG-1] Tok'ra wounded-agent care operation "
                + "disabled: " + detail + ".");
        }

        private static void LogIntelligenceConfigurationError(string detail)
        {
            if (intelligenceConfigurationErrorLogged)
            {
                return;
            }

            intelligenceConfigurationErrorLogged = true;
            Log.Error(
                "[GateRim SG-1] Tok'ra intelligence recovery operation "
                + "disabled: " + detail + ".");
        }

        private static void LogObservationConfigurationError(string detail)
        {
            if (observationConfigurationErrorLogged)
            {
                return;
            }

            observationConfigurationErrorLogged = true;
            Log.Error(
                "[GateRim SG-1] Tok'ra observation operation disabled: "
                + detail + ".");
        }

        public static void GetDelayRange(
            TokraOrganicOperationArchetype previousArchetype,
            TokraTrustTier tier,
            out int minimumDelay,
            out int maximumDelay)
        {
            TokraOrganicOperationDefinition previousDefinition
                = GetDefinition(previousArchetype);

            if (previousDefinition?.UsesMissionFrameworkDef == true)
            {
                string contextKey = "TokraTrust." + tier;

                GateRimMissionRecurrenceDef recurrence
                    = previousDefinition.MissionDef?.recurrence;

                if (recurrence != null
                    && recurrence.TryGetDelayRange(
                        contextKey,
                        out minimumDelay,
                        out maximumDelay)
                    && minimumDelay > 0
                    && maximumDelay >= minimumDelay)
                {
                    return;
                }

                if (previousDefinition.MinimumRecurrenceDelayTicks > 0
                    && previousDefinition.MaximumRecurrenceDelayTicks
                        >= previousDefinition.MinimumRecurrenceDelayTicks)
                {
                    minimumDelay
                        = previousDefinition.MinimumRecurrenceDelayTicks;
                    maximumDelay
                        = previousDefinition.MaximumRecurrenceDelayTicks;
                    return;
                }
            }

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

        public static bool TryPlaceObjective(
            Map map,
            TokraOrganicOperationDefinition definition,
            out Thing objective)
        {
            objective = null;

            if (map == null
                || definition == null
                || !definition.HasPhysicalObjective)
            {
                return false;
            }

            ThingDef thingDef = DefDatabase<ThingDef>.GetNamedSilentFail(
                definition.ObjectiveThingDefName);

            if (thingDef == null)
            {
                return false;
            }

            DestroyObjectives(map, definition);

            Thing thing = ThingMaker.MakeThing(thingDef);

            if (thingDef.category == ThingCategory.Building)
            {
                thing.SetFaction(Faction.OfPlayer);
            }

            Thing placedThing;

            if (!TokraDeliveryDropUtility
                .TryPlaceThingNearPreferredDeliveryCell(
                    thing,
                    map,
                    null,
                    out placedThing))
            {
                if (!thing.Destroyed)
                {
                    thing.Destroy(DestroyMode.Vanish);
                }

                return false;
            }

            if (!IsValidPlacedObjective(map, placedThing))
            {
                if (placedThing != null && !placedThing.Destroyed)
                {
                    placedThing.Destroy(DestroyMode.Vanish);
                }

                return false;
            }

            GR_Log.Message(
                "Placed Tok'ra organic objective "
                + $"{definition.DebugLabel} at {placedThing.Position} "
                + "through the shared preferred-delivery route.");

            objective = placedThing;
            return true;
        }

        public static Thing FindExistingObjective(
            Map map,
            TokraOrganicOperationDefinition definition)
        {
            if (map?.listerThings?.AllThings == null
                || definition == null
                || !definition.HasPhysicalObjective)
            {
                return null;
            }

            return map.listerThings.AllThings.FirstOrDefault(thing =>
                thing != null
                && !thing.Destroyed
                && thing.def?.defName == definition.ObjectiveThingDefName);
        }

        public static void DestroyObjectives(
            Map map,
            TokraOrganicOperationDefinition definition)
        {
            if (map?.listerThings?.AllThings == null
                || definition == null
                || !definition.HasPhysicalObjective)
            {
                return;
            }

            List<Thing> staleObjectives = map.listerThings.AllThings
                .Where(thing =>
                    thing != null
                    && thing.def?.defName == definition.ObjectiveThingDefName)
                .ToList();

            for (int i = 0; i < staleObjectives.Count; i++)
            {
                Thing staleObjective = staleObjectives[i];

                if (!staleObjective.Destroyed)
                {
                    staleObjective.Destroy(DestroyMode.Vanish);
                }
            }
        }

        public static void DestroyAllObjectivesExcept(
            Thing retainedObjective)
        {
            if (Find.Maps == null)
            {
                return;
            }

            foreach (TokraOrganicOperationDefinition definition
                in AllDefinitions.Where(item => item.HasPhysicalObjective))
            {
                for (int mapIndex = 0; mapIndex < Find.Maps.Count; mapIndex++)
                {
                    Map map = Find.Maps[mapIndex];

                    if (map?.listerThings?.AllThings == null)
                    {
                        continue;
                    }

                    List<Thing> objectives = map.listerThings.AllThings
                        .Where(thing =>
                            thing != null
                            && thing.def?.defName
                                == definition.ObjectiveThingDefName)
                        .ToList();

                    for (int thingIndex = 0;
                        thingIndex < objectives.Count;
                        thingIndex++)
                    {
                        Thing objective = objectives[thingIndex];

                        if (objective != retainedObjective
                            && !objective.Destroyed)
                        {
                            objective.Destroy(DestroyMode.Vanish);
                        }
                    }
                }
            }
        }

        private static bool IsValidPlacedObjective(Map map, Thing objective)
        {
            return objective != null
                && objective.Spawned
                && objective.Map == map
                && !objective.Position.Fogged(map)
                && map.reachability.CanReachColony(objective.Position);
        }
    }
}
