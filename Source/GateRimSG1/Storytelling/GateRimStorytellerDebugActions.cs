
using RimWorld;
using Verse;

namespace GateRimSG1.Storytelling
{
    public static class GateRimStorytellerDebugActions
    {
        public static void ShowOrchestrationReport()
        {
            GameComponent_GateRimStorytellerOrchestrator component =
                GameComponent_GateRimStorytellerOrchestrator.Current;

            if (component == null)
            {
                Messages.Message(
                    "The GateRim storyteller orchestrator is unavailable.",
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            Find.WindowStack.Add(
                new Dialog_MessageBox(component.BuildDebugReport()));
        }
    }
}
