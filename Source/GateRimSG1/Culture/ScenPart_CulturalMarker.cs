using RimWorld;

namespace GateRimSG1.Culture
{
    /// <summary>
    /// Hidden marker used by cultural starter rules to identify a scenario.
    /// All restrictions and loadout data remain in CulturalPawnProfileDef XML.
    /// </summary>
    public class ScenPart_CulturalMarker : ScenPart
    {
        public ScenPart_CulturalMarker()
        {
            visible = false;
        }

        public override string Summary(Scenario scen)
        {
            return null;
        }

        public override bool CanCoexistWith(ScenPart other)
        {
            return other?.def != def;
        }
    }
}
