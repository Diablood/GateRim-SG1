using LudeonTK;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public static class TokraIntroductionDebugActions
    {
        [DebugAction(
            "GateRim SG-1",
            "Tok'ra intro: make opportunity due",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void MakeOpportunityDue()
        {
            Run(
                GameComponent_TokraIntroductionArc
                    .DebugMakeOpportunityDue(),
                "GR_TokraIntroduction_DebugOpportunityDue");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra intro: force offer",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ForceOffer()
        {
            Run(
                GameComponent_TokraIntroductionArc
                    .DebugForceOffer(Find.CurrentMap),
                "GR_TokraIntroduction_DebugOfferForced");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra intro: accept offer",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void AcceptOffer()
        {
            Run(
                GameComponent_TokraIntroductionArc
                    .DebugAcceptOffer(Find.CurrentMap),
                "GR_TokraIntroduction_DebugOfferAccepted");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra intro: decline offer",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void DeclineOffer()
        {
            Run(
                GameComponent_TokraIntroductionArc.DebugDeclineOffer(),
                "GR_TokraIntroduction_DebugOfferDeclined");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra intro: fail attempt",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void FailAttempt()
        {
            Run(
                GameComponent_TokraIntroductionArc.DebugFailAttempt(),
                "GR_TokraIntroduction_DebugAttemptFailed");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra intro: expire offer",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ExpireOffer()
        {
            Run(
                GameComponent_TokraIntroductionArc.DebugExpireOffer(),
                "GR_TokraIntroduction_DebugOfferExpired");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra intro: move site to deadline warning",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void MoveSiteToDeadlineWarning()
        {
            Run(
                GameComponent_TokraIntroductionArc
                    .DebugMoveActiveSiteToWarningWindow(),
                "GR_TokraIntroduction_DebugDeadlineWarningWindow");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra intro: expire active site",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ExpireActiveSite()
        {
            Run(
                GameComponent_TokraIntroductionArc.DebugExpireActiveSite(),
                "GR_TokraIntroduction_DebugSiteExpired");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra intro: destroy tracked artifact",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void DestroyTrackedArtifact()
        {
            Run(
                GameComponent_TokraIntroductionArc
                    .DebugDestroyTrackedArtifact(),
                "GR_TokraIntroduction_DebugArtifactDestroyed");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra intro: recover key artifact",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void RecoverKeyArtifact()
        {
            Run(
                GameComponent_TokraIntroductionArc
                    .DebugRecoverArtifact(Find.CurrentMap),
                "GR_TokraIntroduction_DebugArtifactRecovered");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra intro: show state",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ShowState()
        {
            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    GameComponent_TokraIntroductionArc
                        .GetDebugStateReport(Find.CurrentMap)));
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra intro: reset arc",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
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
