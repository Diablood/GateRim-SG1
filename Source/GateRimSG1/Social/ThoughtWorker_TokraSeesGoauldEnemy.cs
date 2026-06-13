using RimWorld;
using Verse;

namespace GateRimSG1.Social
{
    /// <summary>
    /// An active Tok'ra host recognizes an identified active Goa'uld host as a
    /// direct representative of the enemy it has resisted for generations.
    /// </summary>
    public class ThoughtWorker_TokraSeesGoauldEnemy : ThoughtWorker
    {
        protected override ThoughtState CurrentSocialStateInternal(
            Pawn pawn,
            Pawn otherPawn)
        {
            return pawn != otherPawn
                && ContextualSocialIdentityUtility.IsActiveTokraHost(pawn)
                && ContextualSocialIdentityUtility.IsActiveGoauldHost(otherPawn)
                    ? ThoughtState.ActiveDefault
                    : ThoughtState.Inactive;
        }
    }
}
