using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace GateRimSG1.Missions
{
    public struct GateRimMissionDifficultySnapshot
    {
        public GateRimMissionDifficultySnapshot(
            float baseThreatPoints,
            float scaledThreatPoints,
            float factor)
        {
            BaseThreatPoints = baseThreatPoints;
            ScaledThreatPoints = scaledThreatPoints;
            Factor = factor;
        }

        public float BaseThreatPoints { get; }
        public float ScaledThreatPoints { get; }
        public float Factor { get; }
    }

    public static class GateRimMissionFramework
    {
        public const string OfferTextBankKey = "offer";

        public static GateRimMissionDifficultySnapshot CaptureDifficulty(
            Map map,
            GateRimMissionDifficultyDef profile)
        {
            if (map == null
                || profile == null
                || profile.mode == GateRimMissionDifficultyMode.None)
            {
                return new GateRimMissionDifficultySnapshot(0f, 0f, 1f);
            }

            float basePoints = StorytellerUtility.DefaultThreatPointsNow(map);
            float factor = Math.Max(0.01f, profile.pointsFactor);
            float scaledPoints = Clamp(
                basePoints * factor,
                Math.Max(0f, profile.minimumPoints),
                Math.Max(profile.minimumPoints, profile.maximumPoints));

            return new GateRimMissionDifficultySnapshot(
                basePoints,
                scaledPoints,
                factor);
        }

        public static string SelectTextKey(
            IList<GateRimMissionTextVariantDef> variants,
            int previousIndex,
            out int selectedIndex)
        {
            selectedIndex = -1;

            if (variants == null || variants.Count == 0)
            {
                return null;
            }

            List<int> candidates = Enumerable.Range(0, variants.Count)
                .Where(index => variants[index] != null
                    && !string.IsNullOrWhiteSpace(variants[index].key)
                    && variants[index].weight > 0f)
                .ToList();

            if (candidates.Count > 1 && candidates.Contains(previousIndex))
            {
                candidates.Remove(previousIndex);
            }

            if (candidates.Count == 0)
            {
                return null;
            }

            float totalWeight = candidates.Sum(
                index => variants[index].weight);
            float roll = Rand.Value * totalWeight;

            for (int i = 0; i < candidates.Count; i++)
            {
                int index = candidates[i];
                roll -= variants[index].weight;

                if (roll <= 0f)
                {
                    selectedIndex = index;
                    return variants[index].key;
                }
            }

            selectedIndex = candidates[candidates.Count - 1];
            return variants[selectedIndex].key;
        }

        public static string BuildDebugReport(Map map)
        {
            StringBuilder builder = new StringBuilder();
            List<GateRimMissionDef> definitions
                = DefDatabase<GateRimMissionDef>.AllDefsListForReading
                    .OrderBy(definition => definition.defName)
                    .ToList();

            builder.AppendLine("GateRim SG-1 mission framework");
            builder.AppendLine("Loaded definitions: " + definitions.Count);

            foreach (GateRimMissionDef definition in definitions)
            {
                GateRimMissionDifficultySnapshot snapshot
                    = CaptureDifficulty(map, definition.difficulty);

                builder.AppendLine();
                builder.AppendLine(definition.defName);
                builder.AppendLine("  Label: "
                    + (definition.label ?? definition.debugLabel));
                builder.AppendLine("  Adapter: "
                    + (definition.legacyAdapterKey ?? "none"));
                builder.AppendLine("  Phases: "
                    + (definition.phases?.Count ?? 0));
                builder.AppendLine("  Offer variants: "
                    + (definition.texts?.offerLetterTexts?.Count ?? 0));
                builder.AppendLine("  Success variants: "
                    + (definition.texts?.successLetterTexts?.Count ?? 0));
                builder.AppendLine("  Runtime texts: "
                    + (definition.texts?.runtimeTexts?.Count ?? 0));
                builder.AppendLine("  Named text banks: "
                    + (definition.texts?.namedTextBanks?.Count ?? 0));
                builder.AppendLine("  Repeat factor: "
                    + (definition.recurrence?.repeatedMissionWeightFactor
                        ?? 0f).ToString("0.00"));
                builder.AppendLine("  Recurrence delay: "
                    + (definition.recurrence?.minimumDelayTicks ?? 0)
                    + "-"
                    + (definition.recurrence?.maximumDelayTicks ?? 0)
                    + " ticks");
                foreach (GateRimMissionContextDelayDef contextDelay
                    in definition.recurrence?.contextDelays
                        ?? Enumerable.Empty<GateRimMissionContextDelayDef>())
                {
                    if (contextDelay == null)
                    {
                        continue;
                    }

                    builder.AppendLine(
                        "  Recurrence context "
                        + (contextDelay.contextKey ?? "none")
                        + ": "
                        + contextDelay.minimumDelayTicks
                        + "-"
                        + contextDelay.maximumDelayTicks
                        + " ticks");
                }

                builder.AppendLine("  Difficulty: "
                    + (definition.difficulty?.mode.ToString() ?? "None"));

                if (definition.pawnCare != null)
                {
                    GateRimMissionPawnCareDef pawnCare = definition.pawnCare;
                    builder.AppendLine(
                        "  Pawn care: kind="
                        + (pawnCare.pawnKindDefName ?? "none")
                        + ", initialHediff="
                        + (pawnCare.initialHediffDefName ?? "none")
                        + ", recoveryHediff="
                        + (pawnCare.recoveryHediffDefName ?? "none"));
                    builder.AppendLine(
                        "  Pawn care timing: stable="
                        + pawnCare.stableDurationTicks
                        + ", departureGrace="
                        + pawnCare.departureGraceTicks
                        + " ticks");
                    builder.AppendLine(
                        "  Pawn care thresholds: moving="
                        + pawnCare.minimumMovingCapacity.ToString("0.###")
                        + ", consciousness="
                        + pawnCare.minimumConsciousnessCapacity.ToString("0.###")
                        + ", health="
                        + pawnCare.minimumSummaryHealth.ToString("0.###")
                        + ", bleed="
                        + pawnCare.maximumBleedRate.ToString("0.###"));
                    builder.AppendLine(
                        "  Pawn care illness: def="
                        + (pawnCare.optionalIllnessHediffDefName ?? "none")
                        + ", chance="
                        + pawnCare.optionalIllnessChanceMinimum.ToString("0.###")
                        + "-"
                        + pawnCare.optionalIllnessChanceMaximum.ToString("0.###")
                        + ", severity="
                        + pawnCare.optionalIllnessSeverityMinimum.ToString("0.###")
                        + "-"
                        + pawnCare.optionalIllnessSeverityMaximum.ToString("0.###"));
                }

                if (definition.handoff != null)
                {
                    GateRimMissionHandoffDef handoff = definition.handoff;
                    builder.AppendLine(
                        "  Handoff: liaison="
                        + (handoff.liaisonPawnKindDefName ?? "none")
                        + ", arrival="
                        + handoff.arrivalMinimumDelayTicks
                        + "-"
                        + handoff.arrivalMaximumDelayTicks
                        + " ticks, departureGrace="
                        + handoff.departureGraceTicks
                        + " ticks");
                    builder.AppendLine(
                        "  Handoff post-death trust: "
                        + handoff.postHandoffDeathTrustChange);
                }

                if (definition.capture != null)
                {
                    GateRimMissionCaptureDef capture = definition.capture;
                    builder.AppendLine(
                        "  Capture: site="
                        + (capture.worldObjectDefName ?? "none")
                        + ", target="
                        + (capture.targetPawnKindDefName ?? "none")
                        + ", tool="
                        + (capture.captureToolThingDefName ?? "none"));
                    builder.AppendLine(
                        "  Capture escort: warrior="
                        + (capture.escortWarriorPawnKindDefName ?? "none")
                        + ", guard="
                        + (capture.escortGuardPawnKindDefName ?? "none")
                        + ", count="
                        + capture.escortMinimumCount
                        + "-"
                        + capture.escortMaximumCount
                        + ", threatFactor="
                        + capture.escortThreatFactor.ToString("0.00"));
                    builder.AppendLine(
                        "  Capture site range: "
                        + capture.minimumTileDistance
                        + "-"
                        + capture.maximumTileDistance
                        + " tiles, mapSize="
                        + capture.mapSize);
                }

                foreach (GateRimMissionNamedTextBankDef bank
                    in definition.texts?.namedTextBanks
                        ?? Enumerable.Empty<GateRimMissionNamedTextBankDef>())
                {
                    if (bank == null)
                    {
                        continue;
                    }

                    builder.AppendLine(
                        "  Text bank "
                        + (bank.id ?? "none")
                        + ": "
                        + (bank.texts?.Count ?? 0)
                        + " variants");
                }

                foreach (GateRimMissionSkillXpRewardDef skillReward
                    in definition.rewards?.skillXpRewards
                        ?? Enumerable.Empty<
                            GateRimMissionSkillXpRewardDef>())
                {
                    if (skillReward == null)
                    {
                        continue;
                    }

                    builder.AppendLine(
                        "  Skill XP reward: "
                        + (skillReward.skillDefName ?? "none")
                        + " +"
                        + skillReward.xp);
                }

                if (definition.difficulty != null
                    && definition.difficulty.mode
                        != GateRimMissionDifficultyMode.None)
                {
                    builder.AppendLine(
                        "  Threat snapshot: "
                        + snapshot.BaseThreatPoints.ToString("0")
                        + " -> "
                        + snapshot.ScaledThreatPoints.ToString("0")
                        + " (x"
                        + snapshot.Factor.ToString("0.00")
                        + ")");
                }

                foreach (GateRimMissionPhaseDef phase
                    in definition.phases
                        ?? Enumerable.Empty<GateRimMissionPhaseDef>())
                {
                    if (phase == null)
                    {
                        continue;
                    }

                    foreach (GateRimMissionObjectiveDef objective
                        in phase.objectives
                            ?? Enumerable.Empty<
                                GateRimMissionObjectiveDef>())
                    {
                        if (objective == null)
                        {
                            continue;
                        }

                        builder.AppendLine(
                            "  Objective "
                            + phase.id
                            + "/"
                            + objective.objectiveType
                            + ": target="
                            + (objective.targetDefName ?? "none")
                            + ", secondary="
                            + (objective.secondaryTargetDefName ?? "none")
                            + ", job="
                            + (objective.jobDefName ?? "none")
                            + ", count="
                            + objective.requiredCount
                            + ", work="
                            + objective.workTicks
                            + ", secondaryWork="
                            + objective.secondaryWorkTicks
                            + ", skill="
                            + (objective.skillDefName ?? "none")
                            + ", xp/tick="
                            + objective.xpPerTick.ToString("0.###"));
                    }

                    foreach (GateRimMissionTransitionDef transition
                        in phase.transitions
                            ?? Enumerable.Empty<
                                GateRimMissionTransitionDef>())
                    {
                        if (transition == null)
                        {
                            continue;
                        }

                        foreach (GateRimMissionConsequenceDef consequence
                            in transition.consequences
                                ?? Enumerable.Empty<
                                    GateRimMissionConsequenceDef>())
                        {
                            if (consequence == null)
                            {
                                continue;
                            }

                            builder.AppendLine(
                                "  Consequence "
                                + phase.id
                                + " -> "
                                + (transition.targetPhaseId ?? "none")
                                + "/"
                                + consequence.consequenceType
                                + ": target="
                                + (consequence.targetDefName ?? "none")
                                + ", value="
                                + consequence.value.ToString("0.###")
                                + ", chance="
                                + consequence.chance.ToString("0.###")
                                + ", delay="
                                + consequence.minimumDelayTicks
                                + "-"
                                + consequence.maximumDelayTicks
                                + ", retry="
                                + consequence.retryTicks);
                        }
                    }

                    foreach (GateRimMissionConsequenceDef consequence
                        in phase.onEnterConsequences
                            ?? Enumerable.Empty<
                                GateRimMissionConsequenceDef>())
                    {
                        if (consequence == null)
                        {
                            continue;
                        }

                        builder.AppendLine(
                            "  Consequence "
                            + phase.id
                            + "/"
                            + consequence.consequenceType
                            + ": target="
                            + (consequence.targetDefName ?? "none")
                            + ", value="
                            + consequence.value.ToString("0.###")
                            + ", chance="
                            + consequence.chance.ToString("0.###")
                            + ", delay="
                            + consequence.minimumDelayTicks
                            + "-"
                            + consequence.maximumDelayTicks
                            + ", retry="
                            + consequence.retryTicks);
                    }
                }
            }

            return builder.ToString().TrimEnd();
        }

        private static float Clamp(float value, float minimum, float maximum)
        {
            return Math.Max(minimum, Math.Min(maximum, value));
        }
    }
}
