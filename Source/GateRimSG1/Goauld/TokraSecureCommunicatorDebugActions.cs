using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public static class TokraSecureCommunicatorDebugActions
    {
        public static void ShowAvailability()
        {
            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    TokraSecureCommunicatorAvailabilityUtility
                        .GetDebugReport()));
        }
    }
}
