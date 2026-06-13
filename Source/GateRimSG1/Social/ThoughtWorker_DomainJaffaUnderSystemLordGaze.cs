using RimWorld;
using Verse;

namespace GateRimSG1.Social
{
    /// <summary>
    /// A Goa'uld-domain Jaffa close to the faction's active System Lord feels
    /// watched and maintains strict discipline. The small positive mood effect
    /// represents imposed composure rather than genuine happiness.
    /// </summary>
    public class ThoughtWorker_DomainJaffaUnderSystemLordGaze : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn pawn)
        {
            return ContextualSocialIdentityUtility.HasNearbySystemLord(pawn)
                ? ThoughtState.ActiveDefault
                : ThoughtState.Inactive;
        }
    }
}
