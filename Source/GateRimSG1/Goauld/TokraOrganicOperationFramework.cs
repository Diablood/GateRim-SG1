using System;
using System.Collections.Generic;
using System.Linq;
using GateRimSG1.Missions;
using RimWorld;
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

        private static GateRimMissionDef cachedObservationMissionDef;
        private static TokraOrganicOperationDefinition
            cachedObservationDefinition;
        private static bool observationConfigurationErrorLogged;

        private static readonly IReadOnlyDictionary<
            TokraOrganicOperationArchetype,
            TokraOrganicOperationDefinition> LegacyDefinitions
                = new Dictionary<
                    TokraOrganicOperationArchetype,
                    TokraOrganicOperationDefinition>
                {
                    {
                        TokraOrganicOperationArchetype.DeadDropRecovery,
                        new TokraOrganicOperationDefinition(
                            TokraOrganicOperationArchetype.DeadDropRecovery,
                            offerDurationTicks: 120000,
                            readyDelayTicks: 0,
                            deadlineTicks: 90000,
                            intellectualXp: 350,
                            medicineXp: 0,
                            socialXp: 0,
                            successTrustChange:
                                GameComponent_TokraTrustTracker
                                    .OrganicDeadDropSuccessTrustChange,
                            failureTrustChange:
                                GameComponent_TokraTrustTracker
                                    .OrganicDeadDropFailureTrustChange,
                            waryWeight: 0.35f,
                            neutralWeight: 0.85f,
                            cooperativeWeight: 1.00f,
                            trustedWeight: 0.55f,
                            objectiveThingDefName:
                                "SG1_TokraOrganicDeadDrop",
                            acceptActionKey:
                                "GR_TokraOrganicOperation_FloatMenuAcceptDeadDrop",
                            completeActionKey:
                                "GR_TokraOrganicOperation_FloatMenuAnalyzeIntelligence",
                            offerLetterLabelKey:
                                "GR_TokraOrganicOperation_DeadDropOfferLetterLabel",
                            offerLetterTextKey:
                                "GR_TokraOrganicOperation_DeadDropOfferLetterText",
                            offerExpiredMessageKey:
                                "GR_TokraOrganicOperation_DeadDropOfferExpired",
                            offeredStatusKey:
                                "GR_TokraOrganicOperation_StatusDeadDropOffered",
                            activeStatusKey:
                                "GR_TokraOrganicOperation_StatusDeadDropActive",
                            readyStatusKey:
                                "GR_TokraOrganicOperation_StatusDeadDropActive",
                            successTrustMessageKey:
                                "GR_TokraTrust_OrganicDeadDropSucceeded",
                            failureTrustMessageKey:
                                "GR_TokraTrust_OrganicDeadDropFailed",
                            debugLabel: "IntelligenceRecovery")
                    },
                    {
                        TokraOrganicOperationArchetype.WoundedAgentCare,
                        new TokraOrganicOperationDefinition(
                            TokraOrganicOperationArchetype.WoundedAgentCare,
                            offerDurationTicks: 120000,
                            readyDelayTicks: 0,
                            deadlineTicks: 300000,
                            intellectualXp: 0,
                            medicineXp: 0,
                            socialXp: 0,
                            successTrustChange:
                                GameComponent_TokraTrustTracker
                                    .OrganicWoundedAgentSuccessTrustChange,
                            failureTrustChange:
                                GameComponent_TokraTrustTracker
                                    .OrganicWoundedAgentFailureTrustChange,
                            waryWeight: 0.10f,
                            neutralWeight: 0.55f,
                            cooperativeWeight: 1.00f,
                            trustedWeight: 0.85f,
                            objectiveThingDefName: null,
                            acceptActionKey:
                                "GR_TokraWoundedAgent_Accept",
                            completeActionKey: null,
                            offerLetterLabelKey:
                                "GR_TokraWoundedAgent_OfferLabel",
                            offerLetterTextKey:
                                "GR_TokraWoundedAgent_OfferText",
                            offerExpiredMessageKey:
                                "GR_TokraWoundedAgent_OfferExpired",
                            offeredStatusKey:
                                "GR_TokraWoundedAgent_StatusOffered",
                            activeStatusKey:
                                "GR_TokraWoundedAgent_StatusCare",
                            readyStatusKey:
                                "GR_TokraWoundedAgent_StatusDeparting",
                            successTrustMessageKey:
                                "GR_TokraTrust_WoundedAgentSucceeded",
                            failureTrustMessageKey:
                                "GR_TokraTrust_WoundedAgentFailed",
                            debugLabel: "WoundedAgentCare")
                    },
                    {
                        TokraOrganicOperationArchetype.MedicalSupplyHandoff,
                        new TokraOrganicOperationDefinition(
                            TokraOrganicOperationArchetype.MedicalSupplyHandoff,
                            offerDurationTicks: 120000,
                            readyDelayTicks: 0,
                            deadlineTicks: 15000,
                            intellectualXp: 0,
                            medicineXp: 0,
                            socialXp: 350,
                            successTrustChange:
                                GameComponent_TokraTrustTracker
                                    .OrganicMedicalSupplySuccessTrustChange,
                            failureTrustChange:
                                GameComponent_TokraTrustTracker
                                    .OrganicMedicalSupplyFailureTrustChange,
                            waryWeight: 0.20f,
                            neutralWeight: 0.70f,
                            cooperativeWeight: 1.00f,
                            trustedWeight: 0.65f,
                            objectiveThingDefName: null,
                            acceptActionKey:
                                "GR_TokraMedicalSupply_Accept",
                            completeActionKey: null,
                            offerLetterLabelKey:
                                "GR_TokraMedicalSupply_OfferLabel",
                            offerLetterTextKey:
                                "GR_TokraMedicalSupply_OfferText",
                            offerExpiredMessageKey:
                                "GR_TokraMedicalSupply_OfferExpired",
                            offeredStatusKey:
                                "GR_TokraMedicalSupply_StatusOffered",
                            activeStatusKey:
                                "GR_TokraMedicalSupply_StatusAwaitingArrival",
                            readyStatusKey:
                                "GR_TokraMedicalSupply_StatusReady",
                            successTrustMessageKey:
                                "GR_TokraTrust_MedicalSupplySucceeded",
                            failureTrustMessageKey:
                                "GR_TokraTrust_MedicalSupplyFailed",
                            debugLabel: "MedicalSupplyHandoff")
                    }
                };

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

                foreach (TokraOrganicOperationDefinition definition
                    in LegacyDefinitions.Values)
                {
                    yield return definition;
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

            return LegacyDefinitions.TryGetValue(archetype, out definition);
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

            if (previousDefinition?.UsesMissionFrameworkDef == true
                && previousDefinition.MinimumRecurrenceDelayTicks > 0
                && previousDefinition.MaximumRecurrenceDelayTicks
                    >= previousDefinition.MinimumRecurrenceDelayTicks)
            {
                minimumDelay
                    = previousDefinition.MinimumRecurrenceDelayTicks;
                maximumDelay
                    = previousDefinition.MaximumRecurrenceDelayTicks;
                return;
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
