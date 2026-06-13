using RimWorld;
using Verse;

namespace GateRimSG1.Social
{
    /// <summary>
    /// Free Jaffa remember the cost of Goa'uld domination and remain wary of
    /// an identified active Goa'uld host.
    /// </summary>
    public class ThoughtWorker_FreeJaffaDistrustsGoauldHost : ThoughtWorker
    {
        protected override ThoughtState CurrentSocialStateInternal(
            Pawn pawn,
            Pawn otherPawn)
        {
            return pawn != otherPawn
                && ContextualSocialIdentityUtility.IsFreeJaffa(pawn)
                && ContextualSocialIdentityUtility.IsActiveGoauldHost(otherPawn)
                    ? ThoughtState.ActiveDefault
                    : ThoughtState.Inactive;
        }
    }
}
