using LudeonTK;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public static class TokraCipherModuleStudyDebugActions
    {
        [DebugAction(
            "GateRim SG-1",
            "Tok'ra study: show state",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ShowState()
        {
            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    TokraCipherModuleStudyUtility
                        .GetDebugStateReport()));
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra study: finish module analysis",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void FinishAnalysis()
        {
            Run(
                TokraCipherModuleStudyUtility.DebugFinishAnalysis(),
                "GR_TokraCipherStudy_DebugFinished");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra study: destroy tracked module",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void DestroyTrackedModule()
        {
            Run(
                GameComponent_TokraIntroductionArc
                    .DebugDestroyRecoveredCipherModule(),
                "GR_TokraCipherStudy_DebugDestroyed");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra study: make replacement due",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void MakeReplacementDue()
        {
            Run(
                GameComponent_TokraIntroductionArc
                    .DebugMakeCipherModuleReplacementDue(),
                "GR_TokraCipherStudy_DebugReplacementDue");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra study: reset module analysis",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ResetAnalysis()
        {
            Run(
                TokraCipherModuleStudyUtility.DebugResetAnalysis(),
                "GR_TokraCipherStudy_DebugReset");
        }

        private static void Run(bool succeeded, string successKey)
        {
            Messages.Message(
                succeeded
                    ? successKey.Translate()
                    : "GR_TokraCipherStudy_DebugUnavailable"
                        .Translate(),
                succeeded
                    ? MessageTypeDefOf.PositiveEvent
                    : MessageTypeDefOf.RejectInput,
                historical: false);
        }
    }
}
