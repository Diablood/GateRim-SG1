using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace GateRimSG1.Missions
{
    public enum GateRimMissionDifficultyMode
    {
        None = 0,
        ThreatPointsSnapshot = 1,
        ThreatPointsScaled = 2
    }

    public sealed class GateRimMissionContextWeight
    {
        public string contextKey;
        public float weight = 1f;
    }

    public sealed class GateRimMissionContextDelayDef
    {
        public string contextKey;
        public int minimumDelayTicks;
        public int maximumDelayTicks;
    }

    public sealed class GateRimMissionTimingDef
    {
        public int offerDurationTicks;
        public int readyDelayTicks;
        public int deadlineTicks;
    }

    public sealed class GateRimMissionRecurrenceDef
    {
        public float repeatedMissionWeightFactor = 0.25f;
        public int minimumDelayTicks;
        public int maximumDelayTicks;
        public List<GateRimMissionContextWeight> contextWeights
            = new List<GateRimMissionContextWeight>();
        public List<GateRimMissionContextDelayDef> contextDelays
            = new List<GateRimMissionContextDelayDef>();

        public float GetWeight(string contextKey)
        {
            float weight;
            return TryGetWeight(contextKey, out weight) ? weight : 0f;
        }

        public bool TryGetWeight(string contextKey, out float weight)
        {
            GateRimMissionContextWeight match = contextWeights?
                .FirstOrDefault(item => item != null
                    && string.Equals(
                        item.contextKey,
                        contextKey,
                        StringComparison.OrdinalIgnoreCase));

            if (match == null)
            {
                weight = 0f;
                return false;
            }

            weight = match.weight;
            return true;
        }

        public bool TryGetDelayRange(
            string contextKey,
            out int minimumDelay,
            out int maximumDelay)
        {
            GateRimMissionContextDelayDef match = contextDelays?
                .FirstOrDefault(item => item != null
                    && string.Equals(
                        item.contextKey,
                        contextKey,
                        StringComparison.OrdinalIgnoreCase));

            if (match == null)
            {
                minimumDelay = 0;
                maximumDelay = 0;
                return false;
            }

            minimumDelay = match.minimumDelayTicks;
            maximumDelay = match.maximumDelayTicks;
            return true;
        }
    }

    public sealed class GateRimMissionDifficultyDef
    {
        public GateRimMissionDifficultyMode mode
            = GateRimMissionDifficultyMode.None;
        public float pointsFactor = 1f;
        public float minimumPoints;
        public float maximumPoints = 100000f;
    }

    public sealed class GateRimMissionPawnCareDef
    {
        public string pawnKindDefName;
        public string initialHediffDefName;
        public string recoveryHediffDefName;
        public string optionalIllnessHediffDefName;
        public int stableDurationTicks;
        public int departureGraceTicks;
        public float minimumMovingCapacity;
        public float minimumConsciousnessCapacity;
        public float minimumSummaryHealth;
        public float maximumBleedRate;
        public float criticalHediffSeverityFraction = 0.70f;
        public float optionalIllnessChanceMinimum;
        public float optionalIllnessChanceMaximum;
        public float optionalIllnessSeverityMinimum;
        public float optionalIllnessSeverityMaximum;
    }

    public sealed class GateRimMissionDistressCallDef
    {
        public string worldObjectDefName;
        public string survivorPawnKindDefName;
        public string salvageThingDefName;
        public string salvageContainerDefName = "Shelf";
        public int minimumTileDistance = 6;
        public int maximumTileDistance = 18;
        public int mapSize = 120;
        public int lateArrivalTicks = 120000;
        public int survivorMinimumCount = 1;
        public int survivorMaximumCount = 3;
        public int defenderMinimumCount = 2;
        public int defenderMaximumCount = 8;
        public int salvageMinimumCount = 4;
        public int salvageMaximumCount = 10;
        public float genuineRescueWeight = 0.50f;
        public float compromisedSignalWeight = 0.30f;
        public float lateArrivalWeight = 0.20f;
        public float genuineRescueThreatFactor = 0.65f;
        public float compromisedSignalThreatFactor = 1.00f;
        public float lateArrivalThreatFactor = 0.75f;
        public int evacuationDelayTicks = 1200;
        public string recoveryPawnKindDefName;
        public int recoveryTeamDelayTicks = 600;
        public int recoveryTeamRetryTicks = 1200;
        public int recoveryTeamMinimumCount = 2;
        public int recoveryTeamMaximumCount = 3;
        public int preferredEntryRadius = 18;
        public float genuineRescueTemporaryCampChance = 0.55f;
        public float lateArrivalOverrunCampChance = 0.70f;
        public int rescueTokraCorpseMinimumCount;
        public int rescueTokraCorpseMaximumCount = 1;
        public int rescueJaffaCorpseMinimumCount;
        public int rescueJaffaCorpseMaximumCount = 2;
        public int trapTokraCorpseMinimumCount;
        public int trapTokraCorpseMaximumCount = 1;
        public int trapJaffaCorpseMinimumCount;
        public int trapJaffaCorpseMaximumCount = 1;
        public int lateTokraCorpseMinimumCount = 1;
        public int lateTokraCorpseMaximumCount = 3;
        public int lateJaffaCorpseMinimumCount;
        public int lateJaffaCorpseMaximumCount = 2;
    }



    public sealed class GateRimMissionIntroductionDef
    {
        public string worldObjectDefName;
        public int minimumTileDistance = 6;
        public int maximumTileDistance = 18;
        public int mapSize = 120;
        public int defenderMinimumCount = 3;
        public int defenderMaximumCount = 12;
        public int preferredEntryRadius = 18;
        public int deadlineWarningTicks = 60000;
    }

    public sealed class GateRimMissionDeliveryCandidateDef
    {
        public string thingDefName;
        public int minimumCount = 3;
        public int maximumCount = 8;
        public bool requireQuality;
        public QualityCategory minimumQuality = QualityCategory.Normal;
        public float minimumHitPointsPercent = 0.80f;
        public float weight = 1f;
    }

    public sealed class GateRimMissionDeliveryDef
    {
        public string worldObjectDefName;
        public int minimumTileDistance = 6;
        public int maximumTileDistance = 18;
        public TechLevel maximumTechLevel = TechLevel.Industrial;
        public int minimumCrafterSkill = 4;
        public bool requireExistingWorkTable = true;
        public int lateGraceTicks = 120000;
        public int lateSuccessTrustChange = 1;
        public string interceptionIncidentDefName;
        public float interceptionChance;
        public int interceptionMinimumDelayTicks;
        public int interceptionMaximumDelayTicks;
        public int interceptionRetryTicks = 2500;
        public float interceptionThreatFactor = 0.65f;
        public float interceptionMinimumPoints = 120f;
        public float interceptionMaximumPoints = 2500f;
        public string destinationCompromiseIncidentDefName;
        public float destinationCompromiseChance;
        public int destinationCompromiseRetryTicks = 2500;
        public float destinationCompromiseThreatFactor = 0.80f;
        public float destinationCompromiseMinimumPoints = 150f;
        public float destinationCompromiseMaximumPoints = 2800f;
        public List<GateRimMissionDeliveryCandidateDef> candidates
            = new List<GateRimMissionDeliveryCandidateDef>();
    }

    public sealed class GateRimMissionHandoffDef
    {
        public string liaisonPawnKindDefName;
        public int arrivalMinimumDelayTicks;
        public int arrivalMaximumDelayTicks;
        public int departureGraceTicks;
        public int postHandoffDeathTrustChange;
    }

    public sealed class GateRimMissionCaptureDef
    {
        public string worldObjectDefName;
        public string targetPawnKindDefName;
        public string captureToolThingDefName;
        public string restraintHediffDefName;
        public string escortWarriorPawnKindDefName;
        public string escortGuardPawnKindDefName;
        public int minimumTileDistance = 6;
        public int maximumTileDistance = 18;
        public int mapSize = 120;
        public int escortMinimumCount = 2;
        public int escortMaximumCount = 10;
        public float escortThreatFactor = 0.70f;
        public int handoffDurationTicks = 2500;
        public string extractionPawnKindDefName;
        public int extractionMinimumDelayTicks = 10000;
        public int extractionMaximumDelayTicks = 30000;
        public int extractionRetryTicks = 1200;
        public int extractionTeamMinimumCount = 2;
        public int extractionTeamMaximumCount = 3;
    }

    public sealed class GateRimMissionTextVariantDef
    {
        public string key;
        public float weight = 1f;
    }

    public sealed class GateRimMissionNamedTextDef
    {
        public string id;
        public string key;
    }

    public sealed class GateRimMissionNamedTextBankDef
    {
        public string id;
        public List<GateRimMissionTextVariantDef> texts
            = new List<GateRimMissionTextVariantDef>();
    }

    public sealed class GateRimMissionTextBankDef
    {
        public string offerLetterLabelKey;
        public List<GateRimMissionTextVariantDef> offerLetterTexts
            = new List<GateRimMissionTextVariantDef>();
        public string offerExpiredMessageKey;
        public string acceptedMessageKey;
        public string successMessageKey;
        public string failureMessageKey;
        public string successLetterLabelKey;
        public string failureLetterLabelKey;
        public List<GateRimMissionTextVariantDef> successLetterTexts
            = new List<GateRimMissionTextVariantDef>();
        public List<GateRimMissionNamedTextDef> runtimeTexts
            = new List<GateRimMissionNamedTextDef>();
        public List<GateRimMissionNamedTextBankDef> namedTextBanks
            = new List<GateRimMissionNamedTextBankDef>();

        public string GetRuntimeTextKey(string id)
        {
            if (string.IsNullOrWhiteSpace(id) || runtimeTexts == null)
            {
                return null;
            }

            return runtimeTexts.FirstOrDefault(item => item != null
                && string.Equals(
                    item.id,
                    id,
                    StringComparison.OrdinalIgnoreCase))?.key;
        }

        public List<GateRimMissionTextVariantDef> GetNamedTextBank(string id)
        {
            if (string.IsNullOrWhiteSpace(id) || namedTextBanks == null)
            {
                return null;
            }

            return namedTextBanks.FirstOrDefault(item => item != null
                && string.Equals(
                    item.id,
                    id,
                    StringComparison.OrdinalIgnoreCase))?.texts;
        }
    }

    public sealed class GateRimMissionActionDef
    {
        public string acceptActionKey;
        public string completeActionKey;
        public string deployActionKey;
        public string continueActionKey;
        public string recoverActionKey;
        public string resumeActionKey;
        public string offeredStatusKey;
        public string activeStatusKey;
        public string readyStatusKey;
        public string successTrustMessageKey;
        public string failureTrustMessageKey;
    }

    public sealed class GateRimMissionSkillXpRewardDef
    {
        public string skillDefName;
        public int xp;
    }

    public sealed class GateRimMissionRewardDef
    {
        public int intellectualXp;
        public int medicineXp;
        public int socialXp;
        public int successTrustChange;
        public int failureTrustChange;
        public List<GateRimMissionSkillXpRewardDef> skillXpRewards
            = new List<GateRimMissionSkillXpRewardDef>();
    }

    public sealed class GateRimMissionObjectiveDef
    {
        public string objectiveType;
        public string targetDefName;
        public string secondaryTargetDefName;
        public string jobDefName;
        public string skillDefName;
        public int requiredCount = 1;
        public int workTicks;
        public int secondaryWorkTicks;
        public float xpPerTick;
        public bool optional;
    }

    public sealed class GateRimMissionConditionDef
    {
        public string conditionType;
        public string targetDefName;
        public float value;
        public bool invert;
    }

    public sealed class GateRimMissionConsequenceDef
    {
        public string consequenceType;
        public string targetDefName;
        public float value;
        public string textKey;
        public float chance = 1f;
        public int minimumDelayTicks;
        public int maximumDelayTicks;
        public int retryTicks;
    }

    public sealed class GateRimMissionTransitionDef
    {
        public string targetPhaseId;
        public int priority;
        public List<GateRimMissionConditionDef> conditions
            = new List<GateRimMissionConditionDef>();
        public List<GateRimMissionConsequenceDef> consequences
            = new List<GateRimMissionConsequenceDef>();
    }

    public sealed class GateRimMissionPhaseDef
    {
        public string id;
        public string labelKey;
        public bool terminal;
        public List<GateRimMissionObjectiveDef> objectives
            = new List<GateRimMissionObjectiveDef>();
        public List<GateRimMissionConsequenceDef> onEnterConsequences
            = new List<GateRimMissionConsequenceDef>();
        public List<GateRimMissionTransitionDef> transitions
            = new List<GateRimMissionTransitionDef>();
    }

    /// <summary>
    /// Data-driven mission description shared by recurring operations and
    /// longer questlines. Specialized workers remain available for mechanics
    /// that do not fit the common phase and objective vocabulary.
    /// </summary>
    public sealed class GateRimMissionDef : Def
    {
        public string debugLabel;
        public string legacyAdapterKey;
        public Type workerClass;
        public ThingDef objectiveThingDef;
        public GateRimMissionTimingDef timing = new GateRimMissionTimingDef();
        public GateRimMissionRecurrenceDef recurrence
            = new GateRimMissionRecurrenceDef();
        public GateRimMissionDifficultyDef difficulty
            = new GateRimMissionDifficultyDef();
        public GateRimMissionPawnCareDef pawnCare;
        public GateRimMissionDistressCallDef distressCall;
        public GateRimMissionIntroductionDef introduction;
        public GateRimMissionDeliveryDef delivery;
        public GateRimMissionHandoffDef handoff;
        public GateRimMissionCaptureDef capture;
        public GateRimMissionTextBankDef texts
            = new GateRimMissionTextBankDef();
        public GateRimMissionActionDef actions
            = new GateRimMissionActionDef();
        public GateRimMissionRewardDef rewards
            = new GateRimMissionRewardDef();
        public List<GateRimMissionPhaseDef> phases
            = new List<GateRimMissionPhaseDef>();

        private GateRimMissionWorker workerInt;

        public GateRimMissionWorker Worker
        {
            get
            {
                if (workerInt == null && workerClass != null)
                {
                    workerInt = (GateRimMissionWorker)Activator.CreateInstance(
                        workerClass);
                    workerInt.def = this;
                }

                return workerInt;
            }
        }

        public GateRimMissionPhaseDef GetPhase(string phaseId)
        {
            if (string.IsNullOrWhiteSpace(phaseId) || phases == null)
            {
                return null;
            }

            return phases.FirstOrDefault(phase => phase != null
                && string.Equals(
                    phase.id,
                    phaseId,
                    StringComparison.OrdinalIgnoreCase));
        }

        public GateRimMissionObjectiveDef GetObjective(
            string phaseId,
            string objectiveType)
        {
            if (string.IsNullOrWhiteSpace(objectiveType))
            {
                return null;
            }

            GateRimMissionPhaseDef phase = GetPhase(phaseId);

            return phase?.objectives?.FirstOrDefault(objective =>
                objective != null
                && string.Equals(
                    objective.objectiveType,
                    objectiveType,
                    StringComparison.OrdinalIgnoreCase));
        }

        public int GetObjectiveWorkTicks(
            string phaseId,
            string objectiveType,
            int fallbackTicks)
        {
            int configuredTicks
                = GetObjective(phaseId, objectiveType)?.workTicks ?? 0;

            return configuredTicks > 0
                ? configuredTicks
                : fallbackTicks;
        }

        public string GetRuntimeTextKey(string id)
        {
            return texts?.GetRuntimeTextKey(id);
        }

        public List<GateRimMissionTextVariantDef> GetNamedTextBank(string id)
        {
            return texts?.GetNamedTextBank(id);
        }

        public GateRimMissionConsequenceDef GetPhaseConsequence(
            string phaseId,
            string consequenceType)
        {
            if (string.IsNullOrWhiteSpace(consequenceType))
            {
                return null;
            }

            return GetPhase(phaseId)?.onEnterConsequences?.FirstOrDefault(
                consequence => consequence != null
                    && string.Equals(
                        consequence.consequenceType,
                        consequenceType,
                        StringComparison.OrdinalIgnoreCase));
        }

        public GateRimMissionConsequenceDef GetTransitionConsequence(
            string phaseId,
            string targetPhaseId,
            string consequenceType)
        {
            if (string.IsNullOrWhiteSpace(targetPhaseId)
                || string.IsNullOrWhiteSpace(consequenceType))
            {
                return null;
            }

            GateRimMissionTransitionDef transition = GetPhase(phaseId)?
                .transitions?.FirstOrDefault(item => item != null
                    && string.Equals(
                        item.targetPhaseId,
                        targetPhaseId,
                        StringComparison.OrdinalIgnoreCase));

            return transition?.consequences?.FirstOrDefault(
                consequence => consequence != null
                    && string.Equals(
                        consequence.consequenceType,
                        consequenceType,
                        StringComparison.OrdinalIgnoreCase));
        }

        public override IEnumerable<string> ConfigErrors()
        {
            foreach (string error in base.ConfigErrors())
            {
                yield return error;
            }

            if (string.IsNullOrWhiteSpace(debugLabel))
            {
                yield return "debugLabel is required";
            }

            if (workerClass != null
                && !typeof(GateRimMissionWorker).IsAssignableFrom(workerClass))
            {
                yield return $"workerClass {workerClass} does not derive from "
                    + nameof(GateRimMissionWorker);
            }
            else if (workerClass != null && workerClass.IsAbstract)
            {
                yield return $"workerClass {workerClass} cannot be abstract";
            }
            else if (workerClass != null
                && workerClass.GetConstructor(Type.EmptyTypes) == null)
            {
                yield return $"workerClass {workerClass} requires a public "
                    + "parameterless constructor";
            }

            if (timing == null)
            {
                yield return "timing is required";
            }
            else
            {
                if (timing.offerDurationTicks <= 0)
                {
                    yield return "offerDurationTicks must be positive";
                }

                if (timing.deadlineTicks < 0 || timing.readyDelayTicks < 0)
                {
                    yield return "mission timing values cannot be negative";
                }
            }

            if (recurrence == null)
            {
                yield return "recurrence is required";
            }
            else
            {
                if (recurrence.repeatedMissionWeightFactor < 0f
                    || recurrence.repeatedMissionWeightFactor > 1f)
                {
                    yield return "repeatedMissionWeightFactor must be between 0 and 1";
                }

                if (recurrence.minimumDelayTicks < 0
                    || recurrence.maximumDelayTicks < recurrence.minimumDelayTicks)
                {
                    yield return "recurrence delay range is invalid";
                }

                if (recurrence.contextWeights == null
                    || recurrence.contextWeights.Count == 0)
                {
                    yield return "at least one recurrence context weight is required";
                }
                else
                {
                    foreach (GateRimMissionContextWeight contextWeight
                        in recurrence.contextWeights)
                    {
                        if (contextWeight == null
                            || string.IsNullOrWhiteSpace(contextWeight.contextKey))
                        {
                            yield return "recurrence context weights require a contextKey";
                        }
                        else if (contextWeight.weight < 0f)
                        {
                            yield return $"recurrence weight for {contextWeight.contextKey} cannot be negative";
                        }
                    }
                }

                if (recurrence.contextDelays != null)
                {
                    HashSet<string> delayContextKeys = new HashSet<string>(
                        StringComparer.OrdinalIgnoreCase);

                    foreach (GateRimMissionContextDelayDef contextDelay
                        in recurrence.contextDelays)
                    {
                        if (contextDelay == null
                            || string.IsNullOrWhiteSpace(contextDelay.contextKey))
                        {
                            yield return "recurrence context delays require a contextKey";
                            continue;
                        }

                        if (!delayContextKeys.Add(contextDelay.contextKey))
                        {
                            yield return "duplicate recurrence delay context: "
                                + contextDelay.contextKey;
                        }

                        if (contextDelay.minimumDelayTicks < 0
                            || contextDelay.maximumDelayTicks
                                < contextDelay.minimumDelayTicks)
                        {
                            yield return "recurrence delay range for "
                                + contextDelay.contextKey + " is invalid";
                        }
                    }
                }
            }

            if (difficulty == null)
            {
                yield return "difficulty is required";
            }
            else if (difficulty.mode != GateRimMissionDifficultyMode.None)
            {
                if (difficulty.pointsFactor <= 0f)
                {
                    yield return "difficulty pointsFactor must be positive";
                }

                if (difficulty.minimumPoints < 0f
                    || difficulty.maximumPoints < difficulty.minimumPoints)
                {
                    yield return "difficulty point bounds are invalid";
                }
            }

            if (introduction != null)
            {
                if (string.IsNullOrWhiteSpace(
                        introduction.worldObjectDefName))
                {
                    yield return "introduction worldObjectDefName is required";
                }

                if (introduction.minimumTileDistance <= 0
                    || introduction.maximumTileDistance
                        < introduction.minimumTileDistance)
                {
                    yield return "introduction tile distance range is invalid";
                }

                if (introduction.mapSize < 80)
                {
                    yield return "introduction mapSize must be at least 80";
                }

                if (introduction.defenderMinimumCount <= 0
                    || introduction.defenderMaximumCount
                        < introduction.defenderMinimumCount)
                {
                    yield return "introduction defender count range is invalid";
                }

                if (introduction.preferredEntryRadius <= 0)
                {
                    yield return "introduction preferredEntryRadius must be positive";
                }

                if (introduction.deadlineWarningTicks <= 0)
                {
                    yield return "introduction deadlineWarningTicks must be positive";
                }
                else if (timing != null
                    && timing.deadlineTicks > 0
                    && introduction.deadlineWarningTicks
                        >= timing.deadlineTicks)
                {
                    yield return "introduction deadlineWarningTicks must be shorter than the mission deadline";
                }
            }

            if (pawnCare != null)
            {
                if (string.IsNullOrWhiteSpace(pawnCare.pawnKindDefName))
                {
                    yield return "pawnCare pawnKindDefName is required";
                }

                if (string.IsNullOrWhiteSpace(pawnCare.initialHediffDefName))
                {
                    yield return "pawnCare initialHediffDefName is required";
                }

                if (string.IsNullOrWhiteSpace(pawnCare.recoveryHediffDefName))
                {
                    yield return "pawnCare recoveryHediffDefName is required";
                }

                if (pawnCare.stableDurationTicks <= 0
                    || pawnCare.departureGraceTicks <= 0)
                {
                    yield return "pawnCare durations must be positive";
                }

                if (pawnCare.minimumMovingCapacity < 0f
                    || pawnCare.minimumMovingCapacity > 1f
                    || pawnCare.minimumConsciousnessCapacity < 0f
                    || pawnCare.minimumConsciousnessCapacity > 1f
                    || pawnCare.minimumSummaryHealth < 0f
                    || pawnCare.minimumSummaryHealth > 1f)
                {
                    yield return "pawnCare health capacity thresholds must be between 0 and 1";
                }

                if (pawnCare.maximumBleedRate < 0f)
                {
                    yield return "pawnCare maximumBleedRate cannot be negative";
                }

                if (pawnCare.criticalHediffSeverityFraction <= 0f
                    || pawnCare.criticalHediffSeverityFraction > 1f)
                {
                    yield return "pawnCare criticalHediffSeverityFraction must be between 0 and 1";
                }

                if (pawnCare.optionalIllnessChanceMinimum < 0f
                    || pawnCare.optionalIllnessChanceMaximum
                        < pawnCare.optionalIllnessChanceMinimum
                    || pawnCare.optionalIllnessChanceMaximum > 1f)
                {
                    yield return "pawnCare optional illness chance range is invalid";
                }

                if (pawnCare.optionalIllnessSeverityMinimum < 0f
                    || pawnCare.optionalIllnessSeverityMaximum
                        < pawnCare.optionalIllnessSeverityMinimum
                    || pawnCare.optionalIllnessSeverityMaximum > 1f)
                {
                    yield return "pawnCare optional illness severity range is invalid";
                }
            }

            if (handoff != null)
            {
                if (string.IsNullOrWhiteSpace(
                        handoff.liaisonPawnKindDefName))
                {
                    yield return "handoff liaisonPawnKindDefName is required";
                }

                if (handoff.arrivalMinimumDelayTicks <= 0
                    || handoff.arrivalMaximumDelayTicks
                        < handoff.arrivalMinimumDelayTicks)
                {
                    yield return "handoff arrival delay range is invalid";
                }

                if (handoff.departureGraceTicks <= 0)
                {
                    yield return "handoff departureGraceTicks must be positive";
                }

                if (handoff.postHandoffDeathTrustChange >= 0)
                {
                    yield return "handoff postHandoffDeathTrustChange must be negative";
                }
            }

            if (capture != null)
            {
                if (string.IsNullOrWhiteSpace(capture.worldObjectDefName))
                {
                    yield return "capture worldObjectDefName is required";
                }

                if (string.IsNullOrWhiteSpace(capture.targetPawnKindDefName))
                {
                    yield return "capture targetPawnKindDefName is required";
                }

                if (string.IsNullOrWhiteSpace(capture.captureToolThingDefName))
                {
                    yield return "capture captureToolThingDefName is required";
                }

                if (string.IsNullOrWhiteSpace(capture.restraintHediffDefName))
                {
                    yield return "capture restraintHediffDefName is required";
                }

                if (string.IsNullOrWhiteSpace(
                        capture.escortWarriorPawnKindDefName))
                {
                    yield return "capture escortWarriorPawnKindDefName is required";
                }

                if (capture.minimumTileDistance <= 0
                    || capture.maximumTileDistance
                        < capture.minimumTileDistance)
                {
                    yield return "capture tile distance range is invalid";
                }

                if (capture.mapSize < 80)
                {
                    yield return "capture mapSize must be at least 80";
                }

                if (capture.escortMinimumCount <= 0
                    || capture.escortMaximumCount
                        < capture.escortMinimumCount)
                {
                    yield return "capture escort count range is invalid";
                }

                if (capture.escortThreatFactor <= 0f)
                {
                    yield return "capture escortThreatFactor must be positive";
                }

                if (string.IsNullOrWhiteSpace(
                        capture.extractionPawnKindDefName))
                {
                    yield return "capture extractionPawnKindDefName is required";
                }

                if (capture.extractionMinimumDelayTicks <= 0
                    || capture.extractionMaximumDelayTicks
                        < capture.extractionMinimumDelayTicks)
                {
                    yield return "capture extraction delay range is invalid";
                }

                if (capture.extractionRetryTicks <= 0)
                {
                    yield return "capture extractionRetryTicks must be positive";
                }

                if (capture.extractionTeamMinimumCount <= 0
                    || capture.extractionTeamMaximumCount
                        < capture.extractionTeamMinimumCount)
                {
                    yield return "capture extraction team count range is invalid";
                }
            }

            if (texts == null)
            {
                yield return "texts is required";
            }
            else
            {
                if (string.IsNullOrWhiteSpace(texts.offerLetterLabelKey))
                {
                    yield return "offerLetterLabelKey is required";
                }

                if (texts.offerLetterTexts == null
                    || texts.offerLetterTexts.Count == 0)
                {
                    yield return "at least one offer letter text variant is required";
                }
                else
                {
                    foreach (GateRimMissionTextVariantDef variant
                        in texts.offerLetterTexts)
                    {
                        if (variant == null || string.IsNullOrWhiteSpace(variant.key))
                        {
                            yield return "offer letter text variants require a key";
                        }
                        else if (variant.weight <= 0f)
                        {
                            yield return $"offer text variant {variant.key} requires a positive weight";
                        }
                    }
                }
            }

            if (texts?.successLetterTexts != null)
            {
                foreach (GateRimMissionTextVariantDef variant
                    in texts.successLetterTexts)
                {
                    if (variant == null || string.IsNullOrWhiteSpace(variant.key))
                    {
                        yield return "success letter text variants require a key";
                    }
                    else if (variant.weight <= 0f)
                    {
                        yield return $"success text variant {variant.key} requires a positive weight";
                    }
                }
            }

            if (texts?.runtimeTexts != null)
            {
                HashSet<string> runtimeTextIds = new HashSet<string>(
                    StringComparer.OrdinalIgnoreCase);

                foreach (GateRimMissionNamedTextDef runtimeText
                    in texts.runtimeTexts)
                {
                    if (runtimeText == null
                        || string.IsNullOrWhiteSpace(runtimeText.id)
                        || string.IsNullOrWhiteSpace(runtimeText.key))
                    {
                        yield return "runtime text entries require an id and key";
                        continue;
                    }

                    if (!runtimeTextIds.Add(runtimeText.id))
                    {
                        yield return $"duplicate runtime text id: {runtimeText.id}";
                    }
                }
            }

            if (texts?.namedTextBanks != null)
            {
                HashSet<string> bankIds = new HashSet<string>(
                    StringComparer.OrdinalIgnoreCase);

                foreach (GateRimMissionNamedTextBankDef bank
                    in texts.namedTextBanks)
                {
                    if (bank == null || string.IsNullOrWhiteSpace(bank.id))
                    {
                        yield return "named text banks require an id";
                        continue;
                    }

                    if (!bankIds.Add(bank.id))
                    {
                        yield return "duplicate named text bank id: " + bank.id;
                    }

                    if (bank.texts == null || bank.texts.Count == 0)
                    {
                        yield return "named text bank " + bank.id
                            + " requires at least one text variant";
                        continue;
                    }

                    foreach (GateRimMissionTextVariantDef variant in bank.texts)
                    {
                        if (variant == null
                            || string.IsNullOrWhiteSpace(variant.key))
                        {
                            yield return "named text bank " + bank.id
                                + " contains a variant without a key";
                        }
                        else if (variant.weight <= 0f)
                        {
                            yield return "named text bank " + bank.id
                                + " contains a non-positive variant weight";
                        }
                    }
                }
            }

            if (actions == null)
            {
                yield return "actions is required";
            }

            if (rewards == null)
            {
                yield return "rewards is required";
            }
            else if (rewards.skillXpRewards != null)
            {
                HashSet<string> rewardSkillDefNames = new HashSet<string>(
                    StringComparer.OrdinalIgnoreCase);

                foreach (GateRimMissionSkillXpRewardDef skillReward
                    in rewards.skillXpRewards)
                {
                    if (skillReward == null
                        || string.IsNullOrWhiteSpace(
                            skillReward.skillDefName))
                    {
                        yield return "skill XP rewards require a skillDefName";
                        continue;
                    }

                    if (!rewardSkillDefNames.Add(skillReward.skillDefName))
                    {
                        yield return "duplicate skill XP reward for "
                            + skillReward.skillDefName;
                    }

                    if (skillReward.xp <= 0)
                    {
                        yield return $"skill XP reward for "
                            + $"{skillReward.skillDefName} must be positive";
                    }
                }
            }

            if (phases == null || phases.Count == 0)
            {
                yield return "at least one phase is required";
            }
            else
            {
                HashSet<string> phaseIds = new HashSet<string>(
                    StringComparer.OrdinalIgnoreCase);

                foreach (GateRimMissionPhaseDef phase in phases)
                {
                    if (phase == null || string.IsNullOrWhiteSpace(phase.id))
                    {
                        yield return "mission phases require an id";
                        continue;
                    }

                    if (!phaseIds.Add(phase.id))
                    {
                        yield return $"duplicate mission phase id: {phase.id}";
                    }
                }

                foreach (GateRimMissionPhaseDef phase in phases
                    .Where(item => item != null
                        && !string.IsNullOrWhiteSpace(item.id)))
                {
                    foreach (GateRimMissionObjectiveDef objective
                        in phase.objectives
                            ?? Enumerable.Empty<GateRimMissionObjectiveDef>())
                    {
                        if (objective == null
                            || string.IsNullOrWhiteSpace(
                                objective.objectiveType))
                        {
                            yield return $"phase {phase.id} contains an "
                                + "objective without objectiveType";
                        }
                        else if (objective.workTicks < 0
                            || objective.secondaryWorkTicks < 0)
                        {
                            yield return $"phase {phase.id} objective "
                                + $"{objective.objectiveType} cannot use "
                                + "negative work ticks";
                        }
                        else if (objective.xpPerTick < 0f)
                        {
                            yield return $"phase {phase.id} objective "
                                + $"{objective.objectiveType} cannot use "
                                + "negative XP per tick";
                        }
                    }

                    foreach (GateRimMissionTransitionDef transition
                        in phase.transitions
                            ?? Enumerable.Empty<GateRimMissionTransitionDef>())
                    {
                        if (transition == null
                            || string.IsNullOrWhiteSpace(
                                transition.targetPhaseId))
                        {
                            yield return $"phase {phase.id} contains a "
                                + "transition without targetPhaseId";
                            continue;
                        }

                        if (!phaseIds.Contains(transition.targetPhaseId))
                        {
                            yield return $"phase {phase.id} targets unknown "
                                + $"phase {transition.targetPhaseId}";
                        }

                        foreach (GateRimMissionConditionDef condition
                            in transition.conditions
                                ?? Enumerable.Empty<
                                    GateRimMissionConditionDef>())
                        {
                            if (condition == null
                                || string.IsNullOrWhiteSpace(
                                    condition.conditionType))
                            {
                                yield return $"transition {phase.id} -> "
                                    + $"{transition.targetPhaseId} contains "
                                    + "a condition without conditionType";
                            }
                        }

                        foreach (GateRimMissionConsequenceDef consequence
                            in transition.consequences
                                ?? Enumerable.Empty<
                                    GateRimMissionConsequenceDef>())
                        {
                            if (consequence == null
                                || string.IsNullOrWhiteSpace(
                                    consequence.consequenceType))
                            {
                                yield return $"transition {phase.id} -> "
                                    + $"{transition.targetPhaseId} contains "
                                    + "a consequence without consequenceType";
                            }
                            else
                            {
                                foreach (string error in ValidateConsequence(
                                    consequence,
                                    $"transition {phase.id} -> "
                                        + transition.targetPhaseId))
                                {
                                    yield return error;
                                }
                            }
                        }
                    }

                    foreach (GateRimMissionConsequenceDef consequence
                        in phase.onEnterConsequences
                            ?? Enumerable.Empty<
                                GateRimMissionConsequenceDef>())
                    {
                        if (consequence == null
                            || string.IsNullOrWhiteSpace(
                                consequence.consequenceType))
                        {
                            yield return $"phase {phase.id} contains an "
                                + "entry consequence without consequenceType";
                        }
                        else
                        {
                            foreach (string error in ValidateConsequence(
                                consequence,
                                "phase " + phase.id))
                            {
                                yield return error;
                            }
                        }
                    }
                }
            }
        }

        private static IEnumerable<string> ValidateConsequence(
            GateRimMissionConsequenceDef consequence,
            string owner)
        {
            if (consequence.chance < 0f || consequence.chance > 1f)
            {
                yield return owner + " consequence "
                    + consequence.consequenceType
                    + " must use a chance between 0 and 1";
            }

            if (consequence.minimumDelayTicks < 0
                || consequence.maximumDelayTicks
                    < consequence.minimumDelayTicks)
            {
                yield return owner + " consequence "
                    + consequence.consequenceType
                    + " has an invalid delay range";
            }

            if (consequence.retryTicks < 0)
            {
                yield return owner + " consequence "
                    + consequence.consequenceType
                    + " cannot use a negative retry delay";
            }
        }
    }
}
