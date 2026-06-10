using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Jaffa
{
    public class CompProperties_PrimtaBiologicalPreservation
        : CompProperties_Rottable
    {
        public float deepFreezeTemperature = -15f;
        public float criticalDeepFreezeTemperature = -30f;
        public int deepFreezeGraceTicks = 60000;
        public float deepFreezeRecoveryRate = 2f;
        public float deepFreezeRotRateMultiplier = 0.25f;
        public float criticalDeepFreezeRotRateMultiplier = 0.5f;

        public CompProperties_PrimtaBiologicalPreservation()
        {
            compClass = typeof(Comp_PrimtaBiologicalPreservation);
        }

        public override IEnumerable<string> ConfigErrors(ThingDef parentDef)
        {
            foreach (string error in base.ConfigErrors(parentDef))
            {
                yield return error;
            }

            if (criticalDeepFreezeTemperature >= deepFreezeTemperature)
            {
                yield return "criticalDeepFreezeTemperature must be lower "
                    + "than deepFreezeTemperature";
            }

            if (deepFreezeGraceTicks < 0)
            {
                yield return "deepFreezeGraceTicks must be >= 0";
            }

            if (deepFreezeRecoveryRate <= 0f)
            {
                yield return "deepFreezeRecoveryRate must be > 0";
            }

            if (deepFreezeRotRateMultiplier < 0f)
            {
                yield return "deepFreezeRotRateMultiplier must be >= 0";
            }

            if (criticalDeepFreezeRotRateMultiplier
                < deepFreezeRotRateMultiplier)
            {
                yield return "criticalDeepFreezeRotRateMultiplier must be "
                    + ">= deepFreezeRotRateMultiplier";
            }
        }
    }
}
