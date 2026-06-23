using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public static class TokraIntroductionDebugActions
    {
        public static void MakeOpportunityDue()
        {
            Run(
                GameComponent_TokraIntroductionArc
                    .DebugMakeOpportunityDue(),
                "GR_TokraIntroduction_DebugOpportunityDue");
        }

        public static void ForceOffer()
        {
            Run(
                GameComponent_TokraIntroductionArc
                    .DebugForceOffer(Find.CurrentMap),
                "GR_TokraIntroduction_DebugOfferForced");
        }

        public static void AcceptOffer()
        {
            Run(
                GameComponent_TokraIntroductionArc
                    .DebugAcceptOffer(Find.CurrentMap),
                "GR_TokraIntroduction_DebugOfferAccepted");
        }

        public static void DeclineOffer()
        {
            Run(
                GameComponent_TokraIntroductionArc.DebugDeclineOffer(),
                "GR_TokraIntroduction_DebugOfferDeclined");
        }

        public static void FailAttempt()
        {
            Run(
                GameComponent_TokraIntroductionArc.DebugFailAttempt(),
                "GR_TokraIntroduction_DebugAttemptFailed");
        }

        public static void ExpireOffer()
        {
            Run(
                GameComponent_TokraIntroductionArc.DebugExpireOffer(),
                "GR_TokraIntroduction_DebugOfferExpired");
        }

        public static void MoveSiteToDeadlineWarning()
        {
            Run(
                GameComponent_TokraIntroductionArc
                    .DebugMoveActiveSiteToWarningWindow(),
                "GR_TokraIntroduction_DebugDeadlineWarningWindow");
        }

        public static void ExpireActiveSite()
        {
            Run(
                GameComponent_TokraIntroductionArc.DebugExpireActiveSite(),
                "GR_TokraIntroduction_DebugSiteExpired");
        }

        public static void DestroyTrackedArtifact()
        {
            Run(
                GameComponent_TokraIntroductionArc
                    .DebugDestroyTrackedArtifact(),
                "GR_TokraIntroduction_DebugArtifactDestroyed");
        }

        public static void RecoverKeyArtifact()
        {
            Run(
                GameComponent_TokraIntroductionArc
                    .DebugRecoverArtifact(Find.CurrentMap),
                "GR_TokraIntroduction_DebugArtifactRecovered");
        }

        public static void ShowState()
        {
            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    GameComponent_TokraIntroductionArc
                        .GetDebugStateReport(Find.CurrentMap)));
        }

        public static void ResetArc()
        {
            Run(
                GameComponent_TokraIntroductionArc.DebugReset(),
                "GR_TokraIntroduction_DebugReset");
        }

        private static void Run(bool succeeded, string successMessageKey)
        {
            Messages.Message(
                succeeded
                    ? successMessageKey.Translate()
                    : "GR_TokraIntroduction_DebugUnavailable".Translate(),
                succeeded
                    ? MessageTypeDefOf.PositiveEvent
                    : MessageTypeDefOf.RejectInput,
                historical: false);
        }
    }
}
