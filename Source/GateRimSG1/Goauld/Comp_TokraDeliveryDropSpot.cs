using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Keeps the Tok'ra delivery spot unique on the current map.
    /// Placing a new marker silently removes the previous one.
    /// </summary>
    public class Comp_TokraDeliveryDropSpot : ThingComp
    {
        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);

            if (respawningAfterLoad)
            {
                return;
            }

            TokraDeliveryDropUtility.RemoveOtherDeliveryDropSpots(parent);
        }
    }
}
