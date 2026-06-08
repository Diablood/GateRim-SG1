using RimWorld;
using Verse;

namespace GateRimSG1.Jaffa
{
    /// <summary>
    /// Light cultural expectation after the Jaffa reaches the implantation age.
    ///
    /// This thought is deliberately separate from the medical dependency that
    /// starts at biological age 12.
    /// </summary>
    public class ThoughtWorker_AwaitingPrimta : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn pawn)
        {
            return JaffaPrimtaUtility.IsCompatibleJaffa(pawn)
                && JaffaPrimtaUtility.MeetsPrimtaImplantationAge(pawn)
                && !JaffaPrimtaUtility.HasPrimta(pawn)
                    ? ThoughtState.ActiveDefault
                    : ThoughtState.Inactive;
        }
    }
}
