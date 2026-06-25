using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Shared debug surface for every organic Tok'ra operation worker.
    /// These state-changing actions are visible only through RimWorld
    /// developer tools. The advanced GateRim SG-1 option exposes reports and
    /// technical state, but never grants these commands.
    /// </summary>
    public static class TokraOrganicOperationDebugActions
    {
        public static void ForceObservationOffer()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceOpportunity(Find.CurrentMap),
                "GR_TokraOrganicOperation_DebugForced");
        }

        public static void DeployObservationDevice()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugDeployObservationDevice(Find.CurrentMap),
                "GR_TokraObservation_DebugDeployed");
        }

        public static void FinishObservationRecording()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugMakeObservationReady(Find.CurrentMap),
                "GR_TokraObservation_DebugReady");
        }

        public static void ForceIntelligenceOffer()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceDeadDropOpportunity(Find.CurrentMap),
                "GR_TokraOrganicOperation_DebugForcedDeadDrop");
        }

        public static void SelectCautiousIntelligence()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugSelectCautiousIntelligence(Find.CurrentMap),
                "GR_TokraOrganicOperation_DebugIntelligenceCautious");
        }

        public static void SelectAcceleratedIntelligence()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugSelectAcceleratedIntelligence(Find.CurrentMap),
                "GR_TokraOrganicOperation_DebugIntelligenceAccelerated");
        }

        public static void ForceIntelligenceInterference()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceIntelligenceInterference(Find.CurrentMap),
                "GR_TokraOrganicOperation_DebugIntelligenceInterference");
        }

        public static void ForceWoundedAgentOffer()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceWoundedAgentOpportunity(Find.CurrentMap),
                "GR_TokraWoundedAgent_DebugForced");
        }

        public static void ForceMedicalHandoffOffer()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceMedicalSupplyOpportunity(Find.CurrentMap),
                "GR_TokraMedicalSupply_DebugForced");
        }

        public static void ForceTemporaryBaseDeliveryOffer()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceTemporaryBaseDeliveryOpportunity(
                        Find.CurrentMap),
                "GR_TokraTemporaryBaseDelivery_DebugForced");
        }

        public static void ForceTemporaryBaseDeliveryLateWindow()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceTemporaryBaseDeliveryLateWindow(
                        Find.CurrentMap),
                "GR_TokraTemporaryBaseDelivery_DebugLateWindow");
        }

        public static void ForceTemporaryBaseDeliveryInterception()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceTemporaryBaseDeliveryInterception(
                        Find.CurrentMap),
                "GR_TokraTemporaryBaseDelivery_DebugInterception");
        }

        public static void ForceTemporaryBaseDeliveryDestinationCompromise()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceTemporaryBaseDeliveryDestinationCompromise(
                        Find.CurrentMap),
                "GR_TokraTemporaryBaseDelivery_DebugDestinationCompromise");
        }

        public static void ForceJaffaOfficerCaptureOffer()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceJaffaOfficerCaptureOpportunity(
                        Find.CurrentMap),
                "GR_TokraJaffaOfficerCapture_DebugForced");
        }

        public static void ForceDiversionAssaultOffer()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceDiversionAssaultOpportunity(
                        Find.CurrentMap),
                "GR_TokraDecoyDefense_DebugForced");
        }

        public static void ForceDiversionAssaultRaid()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceDiversionAssaultRaid(
                        Find.CurrentMap),
                "GR_TokraDecoyDefense_DebugRaidForced");
        }

        public static void ResolveDiversionAssaultVictory()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugResolveDiversionAssaultVictory(
                        Find.CurrentMap),
                "GR_TokraDecoyDefense_DebugVictory");
        }

        public static void SimulateDiversionAssaultHostageLoss()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugResolveDiversionAssaultHostageLoss(
                        Find.CurrentMap),
                "GR_TokraDecoyDefense_DebugHostageLoss");
        }

        public static void ForceDistressRescueOffer()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceDistressCallOpportunity(
                        Find.CurrentMap,
                        TokraDistressCallVariant.GenuineRescue),
                "GR_TokraDistressCall_DebugForcedRescue");
        }

        public static void ForceDistressTrapOffer()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceDistressCallOpportunity(
                        Find.CurrentMap,
                        TokraDistressCallVariant.CompromisedSignal),
                "GR_TokraDistressCall_DebugForcedTrap");
        }

        public static void ForceDistressLateOffer()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugForceDistressCallOpportunity(
                        Find.CurrentMap,
                        TokraDistressCallVariant.LateArrival),
                "GR_TokraDistressCall_DebugForcedLate");
        }

        public static void AcceptCurrentOffer()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugAcceptActiveOffer(Find.CurrentMap),
                "GR_TokraOrganicOperation_DebugAccepted");
        }

        public static void AdvanceCurrentPhase()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugMakeActiveReady(Find.CurrentMap),
                "GR_TokraOrganicOperation_DebugAdvanced");
        }

        public static void SucceedCurrentOperation()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugSucceedActiveOperation(Find.CurrentMap),
                "GR_TokraOrganicOperation_DebugSucceeded");
        }

        public static void FailCurrentOperation()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugFailActiveOperation(Find.CurrentMap),
                "GR_TokraOrganicOperation_DebugFailed");
        }

        public static void ExpireCurrentState()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugExpireCurrentState(Find.CurrentMap),
                "GR_TokraOrganicOperation_DebugExpired");
        }

        public static void MakeNaturalOfferDue()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugMakeNextNaturalOpportunityDue(),
                "GR_TokraOrganicOperation_DebugNaturalDue");
        }

        public static void RollNextNaturalOffer()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugRollNextNaturalOpportunity(Find.CurrentMap),
                "GR_TokraOrganicOperation_DebugForced");
        }

        public static void ShowFrameworkState()
        {
            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    GameComponent_TokraOrganicOperationManager
                        .GetDebugStateReport(Find.CurrentMap)));
        }

        public static void AuditLongTermOrchestration()
        {
            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    GameComponent_TokraOrganicOperationManager
                        .GetOrchestrationAuditReport(Find.CurrentMap)));
        }

        public static void ApplyPendingFollowUp()
        {
            Run(
                GameComponent_TokraOrganicOperationManager
                    .DebugApplyPendingFollowUp(),
                "GR_TokraOrganicOperation_DebugFollowUpApplied");
        }

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
