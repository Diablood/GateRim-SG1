using RimWorld;
using Verse;

namespace GateRimSG1.Jaffa
{
    /// <summary>
    /// Extends vanilla rotting for fragile Prim'ta biological resources.
    ///
    /// A powered dedicated preservation basin disables ambient rot progression
    /// without repairing deterioration accumulated before storage.
    ///
    /// Because this component is itself the active CompRottable instance, it
    /// also suppresses the vanilla ambient-temperature spoilage line directly
    /// while biological stabilization is active.
    /// </summary>
    public class Comp_PrimtaBiologicalPreservation : CompRottable
    {
        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);

            RefreshDisabledState();
        }

        public override void PostDeSpawn(
            Map map,
            DestroyMode mode = DestroyMode.Vanish)
        {
            disabled = false;

            base.PostDeSpawn(map, mode);
        }

        public override void CompTickInterval(int delta)
        {
            RefreshDisabledState();

            base.CompTickInterval(delta);
        }

        public override void CompTickRare()
        {
            RefreshDisabledState();

            base.CompTickRare();
        }

        public override string CompInspectStringExtra()
        {
            RefreshDisabledState();

            if (disabled
                && PrimtaPreservationUtility.TryGetActiveBasin(
                    parent,
                    out Comp_PrimtaPreservationBasin basin))
            {
                if (parent.TryGetComp<Comp_PrimtaLarvaTemperature>() != null)
                {
                    return null;
                }

                return "GR_PrimtaImmature_PreservationBasinStatus".Translate(
                    basin.IdealTemperature.ToStringTemperature("F0"));
            }

            return base.CompInspectStringExtra();
        }

        private void RefreshDisabledState()
        {
            disabled =
                PrimtaPreservationUtility.TryGetActiveBasin(parent, out _);
        }
    }
}
