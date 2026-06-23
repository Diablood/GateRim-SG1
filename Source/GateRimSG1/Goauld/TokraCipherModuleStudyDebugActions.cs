using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public static class TokraCipherModuleStudyDebugActions
    {
        public static void ShowState()
        {
            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    TokraCipherModuleStudyUtility
                        .GetDebugStateReport()));
        }

        public static void FinishAnalysis()
        {
            Run(
                TokraCipherModuleStudyUtility.DebugFinishAnalysis(),
                "GR_TokraCipherStudy_DebugFinished");
        }

        public static void DestroyTrackedModule()
        {
            Run(
                GameComponent_TokraIntroductionArc
                    .DebugDestroyRecoveredCipherModule(),
                "GR_TokraCipherStudy_DebugDestroyed");
        }

        public static void MakeReplacementDue()
        {
            Run(
                GameComponent_TokraIntroductionArc
                    .DebugMakeCipherModuleReplacementDue(),
                "GR_TokraCipherStudy_DebugReplacementDue");
        }

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
