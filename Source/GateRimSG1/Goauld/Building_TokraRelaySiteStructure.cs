using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    internal static class TokraRelaySiteStructureUtility
    {
        public static AcceptanceReport CanDeconstruct(
            Building building,
            Faction faction)
        {
            if (building == null || building.Faction != faction)
            {
                return false;
            }

            return true;
        }
    }

    /// <summary>
    /// Enemy outpost structure that must be claimed before the player can
    /// deconstruct it. Once claimed, vanilla deconstruction rules apply even
    /// if a new hostile later enters the map. Direct damage is always allowed.
    /// </summary>
    public class Building_TokraRelaySiteStructure : Building
    {
        public override AcceptanceReport DeconstructibleBy(Faction faction)
        {
            AcceptanceReport sitePermission =
                TokraRelaySiteStructureUtility.CanDeconstruct(this, faction);

            if (!sitePermission.Accepted)
            {
                return sitePermission;
            }

            return base.DeconstructibleBy(faction);
        }
    }

    /// <summary>
    /// Door variant of the claim-gated Goa'uld outpost structure.
    /// </summary>
    public class Building_TokraRelaySiteDoor : Building_Door
    {
        public override AcceptanceReport DeconstructibleBy(Faction faction)
        {
            AcceptanceReport sitePermission =
                TokraRelaySiteStructureUtility.CanDeconstruct(this, faction);

            if (!sitePermission.Accepted)
            {
                return sitePermission;
            }

            return base.DeconstructibleBy(faction);
        }
    }
}
