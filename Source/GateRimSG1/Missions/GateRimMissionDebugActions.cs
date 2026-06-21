using LudeonTK;
using RimWorld;
using Verse;

namespace GateRimSG1.Missions
{
    public static class GateRimMissionDebugActions
    {
        [DebugAction(
            "GateRim SG-1",
            "Mission framework: inspect definitions",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void InspectDefinitions()
        {
            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    GateRimMissionFramework.BuildDebugReport(
                        Find.CurrentMap)));
        }
    }
}
