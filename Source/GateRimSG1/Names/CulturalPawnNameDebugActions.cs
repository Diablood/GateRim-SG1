using LudeonTK;
using Verse;

namespace GateRimSG1.Names
{
    /// <summary>
    /// Single grouped developer entry point for inspecting all cultural name
    /// generators without spawning dozens of test pawns.
    /// </summary>
    public static class CulturalPawnNameDebugActions
    {
        [DebugAction(
            "GateRim SG-1",
            "Cultural names: show samples",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ShowSamples()
        {
            OpenSampleReport();
        }

        public static void OpenSampleReport()
        {
            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    CulturalPawnNameUtility.BuildSampleReport(12)));
        }
    }
}
