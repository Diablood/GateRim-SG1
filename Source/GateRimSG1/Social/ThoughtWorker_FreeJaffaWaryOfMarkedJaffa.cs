using RimWorld;
using Verse;

namespace GateRimSG1.Social
{
    /// <summary>
    /// A forehead mark can mean former service, coercion, pride, infiltration
    /// or an old wound. Free Jaffa therefore react with limited caution rather
    /// than automatic hostility.
    /// </summary>
    public class ThoughtWorker_FreeJaffaWaryOfMarkedJaffa : ThoughtWorker
    {
        protected override ThoughtState CurrentSocialStateInternal(
            Pawn pawn,
            Pawn otherPawn)
        {
            return pawn != otherPawn
                && ContextualSocialIdentityUtility.IsFreeJaffa(pawn)
                && ContextualSocialIdentityUtility.IsMarkedJaffa(otherPawn)
                    ? ThoughtState.ActiveDefault
                    : ThoughtState.Inactive;
        }
    }
}
