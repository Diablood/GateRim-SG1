using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Disappearing-Hediff properties that convert recent implantation into
    /// the persistent active-host state immediately before expiry.
    /// </summary>
    public class HediffCompProperties_GoauldImplantationConversion
        : HediffCompProperties_Disappears
    {
        public HediffCompProperties_GoauldImplantationConversion()
        {
            compClass = typeof(HediffComp_GoauldImplantationConversion);
        }
    }
}
