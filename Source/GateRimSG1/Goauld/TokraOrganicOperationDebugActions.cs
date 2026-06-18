using LudeonTK;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public static class TokraOrganicOperationDebugActions
    {
        [DebugAction(
            "GateRim SG-1",
            "Force Tok'ra observation offer",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ForceOrganicObservationOpportunity()
        {
            if (!GameComponent_TokraOrganicOperationTracker
                .DebugForceOpportunity(Find.CurrentMap))
            {
                ShowUnavailableMessage();
                return;
            }

            Messages.Message(
                "GR_TokraOrganicOperation_DebugForced".Translate(),
                MessageTypeDefOf.PositiveEvent,
                historical: false);
        }

        [DebugAction(
            "GateRim SG-1",
            "Force Tok'ra intelligence module offer",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ForceOrganicDeadDropOpportunity()
        {
            if (!GameComponent_TokraOrganicOperationTracker
                .DebugForceDeadDropOpportunity(Find.CurrentMap))
            {
                ShowUnavailableMessage();
                return;
            }

            Messages.Message(
                "GR_TokraOrganicOperation_DebugForcedDeadDrop".Translate(),
                MessageTypeDefOf.PositiveEvent,
                historical: false);
        }

        [DebugAction(
            "GateRim SG-1",
            "Advance active Tok'ra operation",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void AdvanceActiveOrganicOperation()
        {
            if (!GameComponent_TokraOrganicOperationTracker
                .DebugMakeActiveReady(Find.CurrentMap))
            {
                Messages.Message(
                    "GR_TokraOrganicOperation_DebugNoAdvanceableOperation"
                        .Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            Messages.Message(
                "GR_TokraOrganicOperation_DebugAdvanced".Translate(),
                MessageTypeDefOf.PositiveEvent,
                historical: false);
        }

        [DebugAction(
            "GateRim SG-1",
            "Fail active Tok'ra operation",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void FailActiveOrganicOperation()
        {
            if (!GameComponent_TokraOrganicOperationTracker
                .DebugFailActiveOperation(Find.CurrentMap))
            {
                Messages.Message(
                    "GR_TokraOrganicOperation_DebugNoActiveOperation"
                        .Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            Messages.Message(
                "GR_TokraOrganicOperation_DebugFailed".Translate(),
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        [DebugAction(
            "GateRim SG-1",
            "Reset Tok'ra operations",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ResetOrganicOperationTracker()
        {
            if (!GameComponent_TokraOrganicOperationTracker.DebugReset())
            {
                ShowUnavailableMessage();
                return;
            }

            Messages.Message(
                "GR_TokraOrganicOperation_DebugReset".Translate(),
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        private static void ShowUnavailableMessage()
        {
            Messages.Message(
                "GR_TokraOrganicOperation_DebugUnavailable".Translate(),
                MessageTypeDefOf.RejectInput,
                historical: false);
        }
    }
}
