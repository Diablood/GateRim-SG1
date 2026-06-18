using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Legacy unpublished r1/r2 objective retained only so development saves
    /// can load before the organic-operation tracker removes it.
    /// </summary>
    public class Building_TokraOrganicMedicalSupplyContainer : Building
    {
        public override AcceptanceReport DeconstructibleBy(Faction faction)
        {
            return false;
        }
    }
}
