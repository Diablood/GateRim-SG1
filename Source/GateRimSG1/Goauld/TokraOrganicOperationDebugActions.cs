using LudeonTK;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Shared debug surface for every organic Tok'ra operation worker.
    /// These actions are visible only through RimWorld developer tools.
    /// Equivalent diagnostics are exposed on the communicator when the
    /// GateRim SG-1 advanced-debug option is enabled.
    /// </summary>
    public static class TokraOrganicOperationDebugActions
    {
        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: force observation offer",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ForceObservationOffer()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceOpportunity(Find.CurrentMap),
                "GR_TokraOrganicOperation_DebugForced");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: deploy observation device",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void DeployObservationDevice()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugDeployObservationDevice(Find.CurrentMap),
                "GR_TokraObservation_DebugDeployed");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: finish observation recording",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void FinishObservationRecording()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugMakeObservationReady(Find.CurrentMap),
                "GR_TokraObservation_DebugReady");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: force intelligence offer",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ForceIntelligenceOffer()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceDeadDropOpportunity(Find.CurrentMap),
                "GR_TokraOrganicOperation_DebugForcedDeadDrop");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: intelligence cautious method",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void SelectCautiousIntelligence()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugSelectCautiousIntelligence(Find.CurrentMap),
                "GR_TokraOrganicOperation_DebugIntelligenceCautious");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: intelligence accelerated method",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void SelectAcceleratedIntelligence()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugSelectAcceleratedIntelligence(Find.CurrentMap),
                "GR_TokraOrganicOperation_DebugIntelligenceAccelerated");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: force intelligence interference",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ForceIntelligenceInterference()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceIntelligenceInterference(Find.CurrentMap),
                "GR_TokraOrganicOperation_DebugIntelligenceInterference");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: force wounded agent offer",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ForceWoundedAgentOffer()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceWoundedAgentOpportunity(Find.CurrentMap),
                "GR_TokraWoundedAgent_DebugForced");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: force medical handoff offer",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ForceMedicalHandoffOffer()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceMedicalSupplyOpportunity(Find.CurrentMap),
                "GR_TokraMedicalSupply_DebugForced");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: accept current offer",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void AcceptCurrentOffer()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugAcceptActiveOffer(Find.CurrentMap),
                "GR_TokraOrganicOperation_DebugAccepted");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: advance current phase",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void AdvanceCurrentPhase()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugMakeActiveReady(Find.CurrentMap),
                "GR_TokraOrganicOperation_DebugAdvanced");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: succeed current operation",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void SucceedCurrentOperation()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugSucceedActiveOperation(Find.CurrentMap),
                "GR_TokraOrganicOperation_DebugSucceeded");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: fail current operation",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void FailCurrentOperation()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugFailActiveOperation(Find.CurrentMap),
                "GR_TokraOrganicOperation_DebugFailed");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: expire current state",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ExpireCurrentState()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugExpireCurrentState(Find.CurrentMap),
                "GR_TokraOrganicOperation_DebugExpired");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: roll next natural offer",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void RollNextNaturalOffer()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugRollNextNaturalOpportunity(Find.CurrentMap),
                "GR_TokraOrganicOperation_DebugForced");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: show framework state",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ShowFrameworkState()
        {
            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    GameComponent_TokraOrganicOperationManager
                        .GetDebugStateReport(Find.CurrentMap)));
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: audit long-term orchestration",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void AuditLongTermOrchestration()
        {
            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    GameComponent_TokraOrganicOperationManager
                        .GetOrchestrationAuditReport(Find.CurrentMap)));
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: apply pending follow-up",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ApplyPendingFollowUp()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugApplyPendingFollowUp(),
                "GR_TokraOrganicOperation_DebugFollowUpApplied");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: reset framework",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ResetFramework()
        {
            Run(
                GameComponent_TokraOrganicOperationManager.DebugReset(),
                "GR_TokraOrganicOperation_DebugReset");
        }

        private static void Run(bool succeeded, string successMessageKey)
        {
            Messages.Message(
                succeeded
                    ? successMessageKey.Translate()
                    : "GR_TokraOrganicOperation_DebugUnavailable".Translate(),
                succeeded
                    ? MessageTypeDefOf.PositiveEvent
                    : MessageTypeDefOf.RejectInput,
                historical: false);
        }
    }
}
