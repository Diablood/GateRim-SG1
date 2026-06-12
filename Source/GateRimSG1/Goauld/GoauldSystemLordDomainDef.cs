using GateRimSG1.Jaffa;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Data-driven identity profile for one Goa'uld System Lord domain.
    ///
    /// The first foundation Def only exposes generic black, silver and gold
    /// marks. Future named System Lords can provide their own rank variants
    /// without changing raid doctrines or pawn rendering.
    /// </summary>
    public class GoauldSystemLordDomainDef : Def
    {
        public JaffaForeheadMarkDef ordinaryJaffaMark;
        public JaffaForeheadMarkDef eliteJaffaMark;
        public JaffaForeheadMarkDef firstPrimeJaffaMark;

        public JaffaForeheadMarkDef MarkFor(GoauldJaffaMarkRank rank)
        {
            switch (rank)
            {
                case GoauldJaffaMarkRank.Elite:
                    return eliteJaffaMark ?? ordinaryJaffaMark;

                case GoauldJaffaMarkRank.FirstPrime:
                    return firstPrimeJaffaMark
                        ?? eliteJaffaMark
                        ?? ordinaryJaffaMark;

                default:
                    return ordinaryJaffaMark;
            }
        }
    }
}
