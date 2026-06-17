using LudeonTK;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public static class TokraOrganicOperationDebugActions
    {
        [DebugAction(
            "GateRim SG-1",
            "Force Tok'ra organic observation opportunity",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ForceOrganicObservationOpportunity()
        {
            Map map = Find.CurrentMap;

            if (!GameComponent_TokraOrganicOperationTracker
                .DebugForceOpportunity(map))
            {
                Messages.Message(
                    "GR_TokraOrganicOperation_DebugUnavailable".Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            Messages.Message(
                "GR_TokraOrganicOperation_DebugForced".Translate(),
                MessageTypeDefOf.PositiveEvent,
                historical: false);
        }

        [DebugAction(
            "GateRim SG-1",
            "Make Tok'ra organic observation report ready",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void MakeOrganicObservationReportReady()
        {
            if (!GameComponent_TokraOrganicOperationTracker
                .DebugMakeObservationReady(Find.CurrentMap))
            {
                Messages.Message(
                    "GR_TokraOrganicOperation_DebugNoAcceptedOperation"
                        .Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            Messages.Message(
                "GR_TokraOrganicOperation_DebugReady".Translate(),
                MessageTypeDefOf.PositiveEvent,
                historical: false);
        }

        [DebugAction(
            "GateRim SG-1",
            "Reset Tok'ra organic operation tracker",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ResetOrganicOperationTracker()
        {
            if (!GameComponent_TokraOrganicOperationTracker.DebugReset())
            {
                Messages.Message(
                    "GR_TokraOrganicOperation_DebugUnavailable".Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            Messages.Message(
                "GR_TokraOrganicOperation_DebugReset".Translate(),
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }
    }
}
