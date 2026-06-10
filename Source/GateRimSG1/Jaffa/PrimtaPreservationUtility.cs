using Verse;

namespace GateRimSG1.Jaffa
{
    public static class PrimtaPreservationUtility
    {
        public static bool TryGetActiveBasin(
            Thing resource,
            out Comp_PrimtaPreservationBasin basin)
        {
            basin = null;

            if (resource == null
                || !resource.Spawned
                || resource.Map == null)
            {
                return false;
            }

            foreach (Thing thing in resource.Position.GetThingList(resource.Map))
            {
                Comp_PrimtaPreservationBasin candidate =
                    thing.TryGetComp<Comp_PrimtaPreservationBasin>();

                if (candidate != null && candidate.IsOperational)
                {
                    basin = candidate;
                    return true;
                }
            }

            return false;
        }
    }
}
