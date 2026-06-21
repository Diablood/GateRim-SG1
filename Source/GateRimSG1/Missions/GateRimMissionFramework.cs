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
                builder.AppendLine("  Repeat factor: "
                    + (definition.recurrence?.repeatedMissionWeightFactor
                        ?? 0f).ToString("0.00"));
                builder.AppendLine("  Difficulty: "
                    + (definition.difficulty?.mode.ToString() ?? "None"));

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
            }

            return builder.ToString().TrimEnd();
        }

        private static float Clamp(float value, float minimum, float maximum)
        {
            return Math.Max(minimum, Math.Min(maximum, value));
        }
    }
}
