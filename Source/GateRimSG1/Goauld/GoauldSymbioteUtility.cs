using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Shared helpers for adult symbiote variants.
    ///
    /// Extracted Tok'ra must return as the non-hunting Tok'ra pawn variant while
    /// Goa'uld symbiotes keep the original autonomous-hunt pawn kind.
    /// </summary>
    public static class GoauldSymbioteUtility
    {
        public static PawnKindDef GetFreeSymbiotePawnKind(
            GoauldSymbioteData data)
        {
            if (data?.Origin == GoauldSymbioteOrigin.Tokra
                && GR_DefOf.SG1_TokraSymbiote != null)
            {
                return GR_DefOf.SG1_TokraSymbiote;
            }

            return GR_DefOf.SG1_GoauldSymbiote;
        }
    }
}
