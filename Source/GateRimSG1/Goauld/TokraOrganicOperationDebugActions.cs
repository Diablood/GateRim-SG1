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
            "Tok'ra ops: force delivery contract",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ForceTemporaryBaseDeliveryOffer()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceTemporaryBaseDeliveryOpportunity(
                        Find.CurrentMap),
                "GR_TokraTemporaryBaseDelivery_DebugForced");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: delivery late window",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ForceTemporaryBaseDeliveryLateWindow()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceTemporaryBaseDeliveryLateWindow(
                        Find.CurrentMap),
                "GR_TokraTemporaryBaseDelivery_DebugLateWindow");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: delivery interception",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ForceTemporaryBaseDeliveryInterception()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceTemporaryBaseDeliveryInterception(
                        Find.CurrentMap),
                "GR_TokraTemporaryBaseDelivery_DebugInterception");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: delivery approach ambush",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ForceTemporaryBaseDeliveryDestinationCompromise()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceTemporaryBaseDeliveryDestinationCompromise(
                        Find.CurrentMap),
                "GR_TokraTemporaryBaseDelivery_DebugDestinationCompromise");
        }


        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: force diversion offer",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ForceDiversionAssaultOffer()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceDiversionAssaultOpportunity(
                        Find.CurrentMap),
                "GR_TokraDecoyDefense_DebugForced");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: force diversion assault",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ForceDiversionAssaultRaid()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceDiversionAssaultRaid(
                        Find.CurrentMap),
                "GR_TokraDecoyDefense_DebugRaidForced");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: resolve diversion victory",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ResolveDiversionAssaultVictory()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugResolveDiversionAssaultVictory(
                        Find.CurrentMap),
                "GR_TokraDecoyDefense_DebugVictory");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: simulate diversion hostage loss",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void SimulateDiversionAssaultHostageLoss()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugResolveDiversionAssaultHostageLoss(
                        Find.CurrentMap),
                "GR_TokraDecoyDefense_DebugHostageLoss");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: force distress rescue offer",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ForceDistressRescueOffer()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceDistressCallOpportunity(
                        Find.CurrentMap,
                        TokraDistressCallVariant.GenuineRescue),
                "GR_TokraDistressCall_DebugForcedRescue");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: force distress trap offer",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ForceDistressTrapOffer()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceDistressCallOpportunity(
                        Find.CurrentMap,
                        TokraDistressCallVariant.CompromisedSignal),
                "GR_TokraDistressCall_DebugForcedTrap");
        }

        [DebugAction(
            "GateRim SG-1",
            "Tok'ra ops: force distress late offer",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ForceDistressLateOffer()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceDistressCallOpportunity(
                        Find.CurrentMap,
                        TokraDistressCallVariant.LateArrival),
                "GR_TokraDistressCall_DebugForcedLate");
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
            "Tok'ra ops: make natural offer due",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void MakeNaturalOfferDue()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugMakeNextNaturalOpportunityDue(),
                "GR_TokraOrganicOperation_DebugNaturalDue");
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
