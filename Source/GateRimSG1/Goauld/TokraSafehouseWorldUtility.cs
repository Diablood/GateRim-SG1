using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    public static class TokraSafehouseWorldUtility
    {
        public static bool HasActiveSafehouseWorldObject()
        {
            if (Find.WorldObjects == null)
            {
                return false;
            }

            foreach (WorldObject worldObject in Find.WorldObjects.AllWorldObjects)
            {
                if (worldObject?.def == GR_DefOf.SG1_TokraHiddenSafehouseMarker
                    || worldObject?.def == GR_DefOf.SG1_TokraHiddenSafehouseSite)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
