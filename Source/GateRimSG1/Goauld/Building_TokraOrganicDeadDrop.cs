using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Temporary Tok'ra intelligence-module objective spawned by an organic operation.
    /// It remains physically destructible but cannot be deconstructed for
    /// resources or moved as colony furniture.
    /// </summary>
    public class Building_TokraOrganicDeadDrop : Building
    {
        public override AcceptanceReport DeconstructibleBy(Faction faction)
        {
            return false;
        }
    }
}
