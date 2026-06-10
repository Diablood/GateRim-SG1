using RimWorld;
using Verse;

namespace GateRimSG1.Jaffa
{
    public class Comp_PrimtaPreservationBasin : ThingComp
    {
        private CompProperties_PrimtaPreservationBasin Props
            => (CompProperties_PrimtaPreservationBasin)props;

        public float IdealTemperature => Props.idealTemperature;

        public bool IsOperational
        {
            get
            {
                CompPowerTrader power = parent.TryGetComp<CompPowerTrader>();

                return parent.Spawned
                    && power != null
                    && power.PowerOn;
            }
        }

        public override string CompInspectStringExtra()
        {
            if (IsOperational)
            {
                return "GR_PrimtaPreservationBasin_Online".Translate(
                    IdealTemperature.ToStringTemperature("F0"));
            }

            return "GR_PrimtaPreservationBasin_Offline".Translate();
        }
    }
}
