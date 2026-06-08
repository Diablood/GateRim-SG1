using RimWorld;
using Verse;

namespace GateRimSG1.Jaffa
{
    /// <summary>
    /// Adds a small biological temperature layer on top of vanilla rotting.
    ///
    /// Vanilla already stops rot below 0 °C, slows it between 0 °C and 10 °C,
    /// and uses the normal rate above 10 °C. This component preserves that
    /// behavior and adds extra deterioration above the configured heat limits.
    ///
    /// The XML lists this component before CompRottable so its additional
    /// progress is applied before vanilla performs stage transitions and
    /// destruction checks during the same rare tick.
    /// </summary>
    public class Comp_PrimtaLarvaTemperature : ThingComp
    {
        private const int RareTickInterval = 250;

        private CompProperties_PrimtaLarvaTemperature Props
            => (CompProperties_PrimtaLarvaTemperature)props;

        public override void CompTickRare()
        {
            base.CompTickRare();
            ApplyAdditionalHeatDeterioration(RareTickInterval);
        }

        public override string CompInspectStringExtra()
        {
            float temperature = parent.AmbientTemperature;
            float effectiveRate = EffectiveDeteriorationRateAt(temperature);

            return "GR_PrimtaLarva_ThermalStatus".Translate(
                temperature.ToStringTemperature("F0"),
                ThermalConditionTranslationKey(temperature).Translate(),
                effectiveRate.ToString("0.##"));
        }

        private void ApplyAdditionalHeatDeterioration(int ticks)
        {
            CompRottable rottable = parent.TryGetComp<CompRottable>();

            if (rottable == null || !rottable.Active)
            {
                return;
            }

            float temperature = parent.AmbientTemperature;
            float multiplier = HeatMultiplierAt(temperature);

            if (multiplier <= 1f)
            {
                return;
            }

            float vanillaRate = GenTemperature.RotRateAtTemperature(temperature);

            if (vanillaRate <= 0f)
            {
                return;
            }

            rottable.RotProgress += vanillaRate * (multiplier - 1f) * ticks;
        }

        private float EffectiveDeteriorationRateAt(float temperature)
        {
            return GenTemperature.RotRateAtTemperature(temperature)
                * HeatMultiplierAt(temperature);
        }

        private float HeatMultiplierAt(float temperature)
        {
            if (temperature >= Props.criticalTemperature)
            {
                return Props.criticalRotRateMultiplier;
            }

            if (temperature >= Props.hotTemperature)
            {
                return Props.hotRotRateMultiplier;
            }

            return 1f;
        }

        private string ThermalConditionTranslationKey(float temperature)
        {
            if (temperature < Props.recommendedMinTemperature)
            {
                return "GR_PrimtaLarva_Thermal_Frozen";
            }

            if (temperature <= Props.recommendedMaxTemperature)
            {
                return "GR_PrimtaLarva_Thermal_Refrigerated";
            }

            if (temperature < Props.hotTemperature)
            {
                return "GR_PrimtaLarva_Thermal_Normal";
            }

            if (temperature < Props.criticalTemperature)
            {
                return "GR_PrimtaLarva_Thermal_Hot";
            }

            return "GR_PrimtaLarva_Thermal_Critical";
        }
    }
}
