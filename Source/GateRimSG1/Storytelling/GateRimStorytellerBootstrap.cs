
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace GateRimSG1.Storytelling
{
    /// <summary>
    /// Builds the GateRim storyteller from the currently installed Cassandra
    /// definition instead of freezing a copied Core XML component list.
    ///
    /// This preserves the active RimWorld 1.6 and DLC incident contracts while
    /// appending one no-op GateRim orchestration component.
    /// </summary>
    [StaticConstructorOnStartup]
    public static class GateRimStorytellerBootstrap
    {
        private const int MissingDefErrorKey = 19650365;

        private static bool initialized;
        private static int baselineComponentCount;
        private static int gateRimComponentCount;

        static GateRimStorytellerBootstrap()
        {
            EnsureInitialized();
        }

        public static bool Initialized => initialized;

        public static int BaselineComponentCount
            => baselineComponentCount;

        public static int GateRimComponentCount
            => gateRimComponentCount;

        public static bool EnsureInitialized()
        {
            StorytellerDef target =
                GateRimStorytellerUtility.GateRimStorytellerDef;
            StorytellerDef baseline =
                GateRimStorytellerUtility.BaselineStorytellerDef;

            if (target == null || baseline == null)
            {
                initialized = false;
                baselineComponentCount = 0;
                gateRimComponentCount = 0;

                Log.ErrorOnce(
                    "<color=#D9B44A>[GateRim SG-1]</color> "
                    + "Could not initialize the SG-1 storyteller because "
                    + "its definition or Cassandra is unavailable.",
                    MissingDefErrorKey);
                return false;
            }

            StorytellerCompProperties orchestrator =
                target.comps?.FirstOrDefault(
                    item => item
                        is StorytellerCompProperties_GateRimOrchestrator)
                ?? new StorytellerCompProperties_GateRimOrchestrator();

            CopyBaselineSettings(baseline, target);

            List<StorytellerCompProperties> components =
                new List<StorytellerCompProperties>(
                    (baseline.comps?.Count ?? 0) + 1);

            if (baseline.comps != null)
            {
                components.AddRange(baseline.comps);
            }

            components.Add(orchestrator);
            target.comps = components;

            baselineComponentCount = baseline.comps?.Count ?? 0;
            gateRimComponentCount = target.comps.Count;
            initialized = true;
            return true;
        }

        private static void CopyBaselineSettings(
            StorytellerDef source,
            StorytellerDef target)
        {
            target.tutorialMode = source.tutorialMode;
            target.disableAdaptiveTraining =
                source.disableAdaptiveTraining;
            target.disableAlerts = source.disableAlerts;
            target.disablePermadeath = source.disablePermadeath;
            target.forcedDifficulty = source.forcedDifficulty;

            target.populationIntentFactorFromPopCurve =
                source.populationIntentFactorFromPopCurve;
            target.populationIntentFactorFromPopAdaptDaysCurve =
                source.populationIntentFactorFromPopAdaptDaysCurve;
            target.pointsFactorFromDaysPassed =
                source.pointsFactorFromDaysPassed;
            target.adaptDaysMin = source.adaptDaysMin;
            target.adaptDaysMax = source.adaptDaysMax;
            target.adaptDaysGameStartGraceDays =
                source.adaptDaysGameStartGraceDays;
            target.pointsFactorFromAdaptDays =
                source.pointsFactorFromAdaptDays;
            target.adaptDaysLossFromColonistLostByPostPopulation =
                source.adaptDaysLossFromColonistLostByPostPopulation;
            target.adaptDaysLossFromColonistViolentlyDownedByPopulation =
                source
                    .adaptDaysLossFromColonistViolentlyDownedByPopulation;
            target.adaptDaysGrowthRateCurve =
                source.adaptDaysGrowthRateCurve;
        }
    }
}
