using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Persistent right-side warning while a Tok'ra-intercepted hostile force
    /// is still approaching.
    /// </summary>
    public class Alert_TokraInterceptedThreat : Alert
    {
        public Alert_TokraInterceptedThreat()
        {
            defaultLabel = "GR_TokraInterceptedThreat_AlertLabel".Translate();
            defaultExplanation = "GR_TokraInterceptedThreat_AlertExplanationFallback"
                .Translate();
        }

        public override AlertReport GetReport()
        {
            if (!GameComponent_TokraInterceptedThreatTracker.HasActiveThreat())
            {
                return false;
            }

            defaultLabel = GameComponent_TokraInterceptedThreatTracker.GetAlertLabel();
            defaultExplanation = GameComponent_TokraInterceptedThreatTracker
                .GetAlertExplanation();

            return true;
        }
    }
}
