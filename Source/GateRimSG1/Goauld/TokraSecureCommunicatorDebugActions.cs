using LudeonTK;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public static class TokraSecureCommunicatorDebugActions
    {
        [DebugAction(
            "GateRim SG-1",
            "Tok'ra communicator: show availability",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ShowAvailability()
        {
            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    TokraSecureCommunicatorAvailabilityUtility
                        .GetDebugReport()));
        }
    }
}
