using System.Collections.Generic;
using Verse;

namespace GateRimSG1.Jaffa
{
    /// <summary>
    /// Configuration for the lightweight Prim'ta-larva temperature tuning.
    ///
    /// Vanilla CompRottable remains responsible for the base preservation model.
    /// This comp only adds extra deterioration in hot and critically hot storage.
    /// </summary>
    public class CompProperties_PrimtaLarvaTemperature : CompProperties
    {
        public float recommendedMinTemperature = 0f;
        public float recommendedMaxTemperature = 10f;
        public float hotTemperature = 25f;
        public float criticalTemperature = 40f;
        public float hotRotRateMultiplier = 2f;
        public float criticalRotRateMultiplier = 3f;

        public CompProperties_PrimtaLarvaTemperature()
        {
            compClass = typeof(Comp_PrimtaLarvaTemperature);
        }

        public override IEnumerable<string> ConfigErrors(ThingDef parentDef)
        {
            foreach (string error in base.ConfigErrors(parentDef))
            {
                yield return error;
            }

            if (recommendedMinTemperature > recommendedMaxTemperature)
            {
                yield return "recommendedMinTemperature must be <= recommendedMaxTemperature";
            }

            if (hotTemperature <= recommendedMaxTemperature)
            {
                yield return "hotTemperature must be > recommendedMaxTemperature";
            }

            if (criticalTemperature <= hotTemperature)
            {
                yield return "criticalTemperature must be > hotTemperature";
            }

            if (hotRotRateMultiplier < 1f)
            {
                yield return "hotRotRateMultiplier must be >= 1";
            }

            if (criticalRotRateMultiplier < hotRotRateMultiplier)
            {
                yield return "criticalRotRateMultiplier must be >= hotRotRateMultiplier";
            }
        }
    }
}
