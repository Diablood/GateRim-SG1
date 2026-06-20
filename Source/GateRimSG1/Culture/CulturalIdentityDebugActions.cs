using LudeonTK;
using RimWorld;
using Verse;

namespace GateRimSG1.Culture
{
    /// <summary>
    /// Grouped entry point for the unified cultural-identity report.
    /// </summary>
    public static class CulturalIdentityDebugActions
    {
        [DebugAction(
            "GateRim SG-1",
            "Cultural identity: inspect selected pawn",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void InspectSelectedPawn()
        {
            OpenSelectedPawnReport();
        }

        public static void OpenSelectedPawnReport()
        {
            Pawn pawn = Find.Selector?.SingleSelectedThing as Pawn;
            if (pawn == null)
            {
                Messages.Message(
                    "GR_CulturalIdentity_NoPawnSelected".Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    CulturalIdentityDebugUtility.BuildReport(pawn)));
        }
    }
}
