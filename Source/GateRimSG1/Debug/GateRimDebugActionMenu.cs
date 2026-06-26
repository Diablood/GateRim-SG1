using System;
using LudeonTK;
using Verse;
using GateRimSG1.Culture;
using GateRimSG1.Goauld;
using GateRimSG1.Jaffa;
using GateRimSG1.Missions;
using GateRimSG1.Names;
using GateRimSG1.Weapons;

namespace GateRimSG1.Debugging
{
    /// <summary>
    /// Compact developer hierarchy for all GateRim SG-1 debug actions.
    /// The hierarchy mirrors the logical test flow of each system and mission.
    /// </summary>
    public static class GateRimDebugActionMenu
    {
        private const string Category = "GateRim SG-1";

        private static readonly DebugActionAttribute MapOnlyAttribute
            = new DebugActionAttribute(Category)
            {
                allowedGameStates = AllowedGameStates.PlayingOnMap
            };

        [DebugAction(
            Category,
            "Tok'ra...",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap,
            displayPriority = 400)]
        public static DebugActionNode OpenTokra()
        {
            DebugActionNode root = new DebugActionNode();

            root.AddChild(MenuNode(
                "World selection...",
                600,
                ActionNode(
                    "Show audit",
                    TokraWorldPresenceDebugActions.ShowAudit,
                    200)));
            root.AddChild(ActionNode(
                "Show communicator availability",
                TokraSecureCommunicatorDebugActions.ShowAvailability,
                500));
            root.AddChild(BuildTokraIntroductionMenu());
            root.AddChild(BuildTokraStudyMenu());
            root.AddChild(BuildTokraOrganicOperationsMenu());
            root.AddChild(BuildTokraSafehouseMenu());

            return root;
        }

        [DebugAction(
            Category,
            "Goa'uld...",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap,
            displayPriority = 350)]
        public static DebugActionNode OpenGoauld()
        {
            DebugActionNode root = new DebugActionNode();

            root.AddChild(MenuNode(
                "Free-symbiote incursion...",
                200,
                ActionNode(
                    "Show current scaling",
                    GoauldFreeSymbioteIncursionDebugActions.ShowCurrentScaling,
                    400),
                ActionNode(
                    "Force current scaling",
                    GoauldFreeSymbioteIncursionDebugActions.ForceCurrentScaling,
                    300),
                ActionNode(
                    "Force weak-colony scaling",
                    GoauldFreeSymbioteIncursionDebugActions.ForceWeakColonyScaling,
                    200),
                ActionNode(
                    "Force advanced-colony scaling",
                    GoauldFreeSymbioteIncursionDebugActions.ForceAdvancedColonyScaling,
                    100)));
            root.AddChild(MenuNode(
                "Host takeover...",
                100,
                PawnToolNode(
                    "Inspect host control",
                    GoauldHostTakeoverDebugActions.InspectHostControl,
                    300),
                PawnToolNode(
                    "Force recent conversion",
                    GoauldHostTakeoverDebugActions.ForceRecentConversion,
                    200),
                PawnToolNode(
                    "Restore displaced faction",
                    GoauldHostTakeoverDebugActions.RestoreDisplacedFaction,
                    100)));

            return root;
        }

        [DebugAction(
            Category,
            "Jaffa...",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap,
            displayPriority = 300)]
        public static DebugActionNode OpenJaffa()
        {
            DebugActionNode root = new DebugActionNode();

            root.AddChild(MenuNode(
                "Forehead marks...",
                100,
                PawnToolNode(
                    "Apply black mark",
                    JaffaForeheadMarkDebugActions.SetOrdinaryBlack,
                    400),
                PawnToolNode(
                    "Apply silver mark",
                    JaffaForeheadMarkDebugActions.SetEliteSilver,
                    300),
                PawnToolNode(
                    "Apply gold mark",
                    JaffaForeheadMarkDebugActions.SetFirstPrimeGold,
                    200),
                PawnToolNode(
                    "Clear mark",
                    JaffaForeheadMarkDebugActions.Remove,
                    100)));

            return root;
        }

        [DebugAction(
            Category,
            "Equipment...",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap,
            displayPriority = 250)]
        public static DebugActionNode OpenEquipment()
        {
            DebugActionNode root = new DebugActionNode();

            root.AddChild(PawnToolNode(
                "Show target neutralization chances",
                NonLethalCaptureDebugActions.ShowNeutralizationChances,
                400));
            root.AddChild(PawnToolNode(
                "Give bolas",
                NonLethalCaptureDebugActions.GiveBolas,
                300));
            root.AddChild(PawnToolNode(
                "Give Tok'ra hypodermic rifle",
                NonLethalCaptureDebugActions.GiveTokraHypodermicRifle,
                200));
            root.AddChild(PawnToolNode(
                "Clear temporary neutralization",
                NonLethalCaptureDebugActions.ClearNeutralization,
                100));

            return root;
        }

        [DebugAction(
            Category,
            "Culture...",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap,
            displayPriority = 200)]
        public static DebugActionNode OpenCulture()
        {
            DebugActionNode root = new DebugActionNode();

            root.AddChild(ActionNode(
                "Inspect selected pawn",
                CulturalIdentityDebugActions.InspectSelectedPawn,
                200));
            root.AddChild(ActionNode(
                "Show cultural name samples",
                CulturalPawnNameDebugActions.ShowSamples,
                100));

            return root;
        }

        [DebugAction(
            Category,
            "Inspect mission definitions",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap,
            displayPriority = 100)]
        public static void InspectMissionDefinitions()
        {
            GateRimMissionDebugActions.InspectDefinitions();
        }

        private static DebugActionNode BuildTokraIntroductionMenu()
        {
            return MenuNode(
                "Introduction arc...",
                400,
                ActionNode(
                    "Show state",
                    TokraIntroductionDebugActions.ShowState,
                    1200),
                ActionNode(
                    "Make opportunity due",
                    TokraIntroductionDebugActions.MakeOpportunityDue,
                    1100),
                ActionNode(
                    "Force offer",
                    TokraIntroductionDebugActions.ForceOffer,
                    1000),
                ActionNode(
                    "Accept offer",
                    TokraIntroductionDebugActions.AcceptOffer,
                    900),
                ActionNode(
                    "Decline offer",
                    TokraIntroductionDebugActions.DeclineOffer,
                    800),
                ActionNode(
                    "Expire offer",
                    TokraIntroductionDebugActions.ExpireOffer,
                    700),
                ActionNode(
                    "Move site to deadline warning",
                    TokraIntroductionDebugActions.MoveSiteToDeadlineWarning,
                    600),
                ActionNode(
                    "Expire active site",
                    TokraIntroductionDebugActions.ExpireActiveSite,
                    500),
                ActionNode(
                    "Fail attempt",
                    TokraIntroductionDebugActions.FailAttempt,
                    400),
                ActionNode(
                    "Destroy tracked artifact",
                    TokraIntroductionDebugActions.DestroyTrackedArtifact,
                    300),
                ActionNode(
                    "Recover key artifact",
                    TokraIntroductionDebugActions.RecoverKeyArtifact,
                    200),
                ActionNode(
                    "Reset arc",
                    TokraIntroductionDebugActions.ResetArc,
                    100));
        }

        private static DebugActionNode BuildTokraStudyMenu()
        {
            return MenuNode(
                "Module study...",
                300,
                ActionNode(
                    "Show state",
                    TokraCipherModuleStudyDebugActions.ShowState,
                    500),
                ActionNode(
                    "Finish module analysis",
                    TokraCipherModuleStudyDebugActions.FinishAnalysis,
                    400),
                ActionNode(
                    "Destroy tracked module",
                    TokraCipherModuleStudyDebugActions.DestroyTrackedModule,
                    300),
                ActionNode(
                    "Make replacement due",
                    TokraCipherModuleStudyDebugActions.MakeReplacementDue,
                    200),
                ActionNode(
                    "Reset module analysis",
                    TokraCipherModuleStudyDebugActions.ResetAnalysis,
                    100));
        }

        private static DebugActionNode BuildTokraOrganicOperationsMenu()
        {
            return MenuNode(
                "Organic operations...",
                200,
                BuildOrganicFrameworkMenu(),
                BuildObservationMenu(),
                BuildIntelligenceMenu(),
                BuildWoundedAgentMenu(),
                BuildMedicalHandoffMenu(),
                BuildDistressCallMenu(),
                BuildTemporaryBaseDeliveryMenu(),
                BuildJaffaOfficerCaptureMenu(),
                BuildDiversionAssaultMenu());
        }

        private static DebugActionNode BuildOrganicFrameworkMenu()
        {
            return MenuNode(
                "Framework...",
                800,
                ActionNode(
                    "Show state",
                    TokraOrganicOperationDebugActions.ShowFrameworkState,
                    1100),
                ActionNode(
                    "Make natural offer due",
                    TokraOrganicOperationDebugActions.MakeNaturalOfferDue,
                    1000),
                ActionNode(
                    "Roll next natural offer",
                    TokraOrganicOperationDebugActions.RollNextNaturalOffer,
                    900),
                ActionNode(
                    "Accept current offer",
                    TokraOrganicOperationDebugActions.AcceptCurrentOffer,
                    800),
                ActionNode(
                    "Advance current phase",
                    TokraOrganicOperationDebugActions.AdvanceCurrentPhase,
                    700),
                ActionNode(
                    "Succeed current operation",
                    TokraOrganicOperationDebugActions.SucceedCurrentOperation,
                    600),
                ActionNode(
                    "Fail current operation",
                    TokraOrganicOperationDebugActions.FailCurrentOperation,
                    500),
                ActionNode(
                    "Expire current state",
                    TokraOrganicOperationDebugActions.ExpireCurrentState,
                    400),
                ActionNode(
                    "Apply pending follow-up",
                    TokraOrganicOperationDebugActions.ApplyPendingFollowUp,
                    300),
                ActionNode(
                    "Audit long-term orchestration",
                    TokraOrganicOperationDebugActions.AuditLongTermOrchestration,
                    200),
                ActionNode(
                    "Reset framework",
                    TokraOrganicOperationDebugActions.ResetFramework,
                    100));
        }

        private static DebugActionNode BuildObservationMenu()
        {
            return MenuNode(
                "Observation...",
                700,
                ActionNode(
                    "Force offer",
                    TokraOrganicOperationDebugActions.ForceObservationOffer,
                    300),
                ActionNode(
                    "Deploy device",
                    TokraOrganicOperationDebugActions.DeployObservationDevice,
                    200),
                ActionNode(
                    "Finish recording",
                    TokraOrganicOperationDebugActions.FinishObservationRecording,
                    100));
        }

        private static DebugActionNode BuildIntelligenceMenu()
        {
            return MenuNode(
                "Intelligence recovery...",
                600,
                ActionNode(
                    "Force offer",
                    TokraOrganicOperationDebugActions.ForceIntelligenceOffer,
                    400),
                ActionNode(
                    "Select cautious method",
                    TokraOrganicOperationDebugActions.SelectCautiousIntelligence,
                    300),
                ActionNode(
                    "Select accelerated method",
                    TokraOrganicOperationDebugActions.SelectAcceleratedIntelligence,
                    200),
                ActionNode(
                    "Force interference",
                    TokraOrganicOperationDebugActions.ForceIntelligenceInterference,
                    100));
        }

        private static DebugActionNode BuildWoundedAgentMenu()
        {
            return MenuNode(
                "Wounded agent...",
                500,
                ActionNode(
                    "Force offer",
                    TokraOrganicOperationDebugActions.ForceWoundedAgentOffer,
                    100));
        }

        private static DebugActionNode BuildMedicalHandoffMenu()
        {
            return MenuNode(
                "Medical handoff...",
                400,
                ActionNode(
                    "Force offer",
                    TokraOrganicOperationDebugActions.ForceMedicalHandoffOffer,
                    100));
        }

        private static DebugActionNode BuildDistressCallMenu()
        {
            return MenuNode(
                "Distress call...",
                300,
                ActionNode(
                    "Force rescue offer",
                    TokraOrganicOperationDebugActions.ForceDistressRescueOffer,
                    300),
                ActionNode(
                    "Force trap offer",
                    TokraOrganicOperationDebugActions.ForceDistressTrapOffer,
                    200),
                ActionNode(
                    "Force late-arrival offer",
                    TokraOrganicOperationDebugActions.ForceDistressLateOffer,
                    100));
        }

        private static DebugActionNode BuildTemporaryBaseDeliveryMenu()
        {
            return MenuNode(
                "Temporary-base delivery...",
                200,
                ActionNode(
                    "Force contract",
                    TokraOrganicOperationDebugActions.ForceTemporaryBaseDeliveryOffer,
                    400),
                ActionNode(
                    "Start late window",
                    TokraOrganicOperationDebugActions.ForceTemporaryBaseDeliveryLateWindow,
                    300),
                ActionNode(
                    "Force interception",
                    TokraOrganicOperationDebugActions.ForceTemporaryBaseDeliveryInterception,
                    200),
                ActionNode(
                    "Force approach ambush",
                    TokraOrganicOperationDebugActions.ForceTemporaryBaseDeliveryDestinationCompromise,
                    100));
        }

        private static DebugActionNode BuildJaffaOfficerCaptureMenu()
        {
            return MenuNode(
                "Jaffa officer capture...",
                150,
                ActionNode(
                    "Force offer",
                    TokraOrganicOperationDebugActions
                        .ForceJaffaOfficerCaptureOffer,
                    100));
        }

        private static DebugActionNode BuildDiversionAssaultMenu()
        {
            return MenuNode(
                "Diversion assault...",
                100,
                ActionNode(
                    "Force offer",
                    TokraOrganicOperationDebugActions.ForceDiversionAssaultOffer,
                    400),
                ActionNode(
                    "Force assault",
                    TokraOrganicOperationDebugActions.ForceDiversionAssaultRaid,
                    300),
                ActionNode(
                    "Resolve victory",
                    TokraOrganicOperationDebugActions.ResolveDiversionAssaultVictory,
                    200),
                ActionNode(
                    "Simulate hostage loss",
                    TokraOrganicOperationDebugActions.SimulateDiversionAssaultHostageLoss,
                    100));
        }

        private static DebugActionNode BuildTokraSafehouseMenu()
        {
            return MenuNode(
                "Safehouse and intelligence chain...",
                100,
                MenuNode(
                    "Trust...",
                    600,
                    ActionNode(
                        "Increase by 5",
                        TokraSafehouseDebugActions.IncreaseTokraTrustTestStep,
                        200),
                    ActionNode(
                        "Decrease by 5",
                        TokraSafehouseDebugActions.DecreaseTokraTrustTestStep,
                        100)),
                MenuNode(
                    "Safehouse contact...",
                    500,
                    ActionNode(
                        "Prepare site",
                        TokraSafehouseDebugActions.PrepareSafehouseSiteTest,
                        300),
                    ActionNode(
                        "Create site",
                        TokraSafehouseDebugActions.CreateSafehouseTestSite,
                        200),
                    ActionNode(
                        "Verify contact",
                        TokraSafehouseDebugActions.VerifySafehouseContactTest,
                        100)),
                MenuNode(
                    "First mission cache...",
                    400,
                    ActionNode(
                        "Deliver cache",
                        TokraSafehouseDebugActions.DeliverTokraFirstMissionCacheTest,
                        200),
                    ActionNode(
                        "Reset cache",
                        TokraSafehouseDebugActions.ResetTokraFirstMissionCacheTest,
                        100)),
                MenuNode(
                    "Decoded lead...",
                    300,
                    ActionNode(
                        "Decode lead",
                        TokraSafehouseDebugActions.DecodeTokraMissionLeadTest,
                        300),
                    ActionNode(
                        "Reveal site",
                        TokraSafehouseDebugActions.RevealTokraDecodedMissionWorldSiteTest,
                        200),
                    ActionNode(
                        "Recon site",
                        TokraSafehouseDebugActions.ReconTokraDecodedMissionWorldSiteTest,
                        100)),
                MenuNode(
                    "Relay sabotage...",
                    200,
                    ActionNode(
                        "Prepare objective",
                        TokraSafehouseDebugActions.PrepareTokraRelaySabotageObjectiveTest,
                        200),
                    ActionNode(
                        "Complete objective",
                        TokraSafehouseDebugActions.CompleteTokraRelaySabotageTest,
                        100)),
                MenuNode(
                    "Threat intelligence...",
                    100,
                    ActionNode(
                        "Create threat",
                        TokraSafehouseDebugActions.CreateTokraInterceptedThreatTest,
                        200),
                    ActionNode(
                        "Clear threat",
                        TokraSafehouseDebugActions.ClearTokraInterceptedThreatTest,
                        100)));
        }

        private static DebugActionNode MenuNode(
            string label,
            int displayPriority,
            params DebugActionNode[] children)
        {
            DebugActionNode node = new DebugActionNode(label)
            {
                category = Category,
                displayPriority = displayPriority,
                sourceAttribute = MapOnlyAttribute
            };

            foreach (DebugActionNode child in children)
            {
                node.AddChild(child);
            }

            return node;
        }

        private static DebugActionNode ActionNode(
            string label,
            Action action,
            int displayPriority)
        {
            return new DebugActionNode(
                label,
                DebugActionType.Action,
                action)
            {
                category = Category,
                displayPriority = displayPriority,
                sourceAttribute = MapOnlyAttribute
            };
        }

        private static DebugActionNode PawnToolNode(
            string label,
            Action<Pawn> pawnAction,
            int displayPriority)
        {
            return new DebugActionNode(
                label,
                DebugActionType.ToolMapForPawns,
                pawnAction: pawnAction)
            {
                category = Category,
                displayPriority = displayPriority,
                sourceAttribute = MapOnlyAttribute
            };
        }
    }
}
