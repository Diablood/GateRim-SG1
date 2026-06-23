using RimWorld;
using Verse;

namespace GateRimSG1.Missions
{
    public static class GateRimMissionDebugActions
    {
        public static void InspectDefinitions()
        {
            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    GateRimMissionFramework.BuildDebugReport(
                        Find.CurrentMap)));
        }
    }
}
