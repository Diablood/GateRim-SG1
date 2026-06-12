using GateRimSG1.Jaffa;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Central resolver for Goa'uld System Lord domain identity.
    ///
    /// The generic prototype remains the fallback while named domains are
    /// still being introduced incrementally.
    /// </summary>
    public static class GoauldSystemLordDomainUtility
    {
        public static GoauldSystemLordDomainDef DomainFor(Faction faction)
        {
            GoauldSystemLordDomainExtension extension =
                faction?.def
                    ?.GetModExtension<GoauldSystemLordDomainExtension>();

            return extension?.domain
                ?? GR_DefOf.SG1_GoauldSystemLordDomainPrototype;
        }

        public static JaffaForeheadMarkDef MarkFor(
            Faction faction,
            GoauldJaffaMarkRank rank)
        {
            GoauldSystemLordDomainDef domain = DomainFor(faction);

            return domain?.MarkFor(rank)
                ?? GR_DefOf.SG1_JaffaForeheadMark_GenericIntrinsic;
        }
    }
}
