using System;
using LudeonTK;
using Verse;

namespace GateRimSG1.Goauld
{
    public static class GoauldInterDomainRelationDebugMenu
    {
        private const string Category = "GateRim SG-1";

        private static readonly DebugActionAttribute MapOnlyAttribute
            = new DebugActionAttribute(Category)
            {
                allowedGameStates = AllowedGameStates.PlayingOnMap
            };

        [DebugAction(
            Category,
            "Goa'uld inter-domain relations...",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap,
            displayPriority = 340)]
        public static DebugActionNode OpenRelations()
        {
            DebugActionNode root = new DebugActionNode();

            root.AddChild(ActionNode(
                "Show relation report",
                GoauldInterDomainRelationDebugActions.ShowReport,
                900));
            root.AddChild(ActionNode(
                "Show natural raid pressure report",
                GoauldOpenConflictPressureDebugActions.ShowReport,
                875));
            root.AddChild(ActionNode(
                "Force current natural raid (pressure applied)",
                GoauldThreatProgressionDebugActions
                    .ForceCurrentNaturalRaidWithPressure,
                850));
            root.AddChild(ActionNode(
                "Show battlefield report",
                GoauldOpenConflictBattlefieldDebugActions.ShowReport,
                845));
            root.AddChild(ActionNode(
                "Make battlefield opportunity due",
                GoauldOpenConflictBattlefieldDebugActions.MakeOpportunityDue,
                840));
            root.AddChild(ActionNode(
                "Force local battlefield now",
                GoauldOpenConflictBattlefieldDebugActions.ForceBattlefield,
                835));
            root.AddChild(ActionNode(
                "Force world battlefield site now",
                GoauldOpenConflictBattlefieldDebugActions.ForceWorldSite,
                833));
            root.AddChild(ActionNode(
                "Expire unvisited world battlefield site",
                GoauldOpenConflictBattlefieldDebugActions.ExpireWorldSite,
                831));
            root.AddChild(ActionNode(
                "Order battlefield withdrawal",
                GoauldOpenConflictBattlefieldDebugActions.OrderWithdrawal,
                830));
            root.AddChild(ActionNode(
                "Reset battlefield scheduler",
                GoauldOpenConflictBattlefieldDebugActions.Reset,
                825));
            root.AddChild(ActionNode(
                "Create additional test domain",
                GoauldInterDomainRelationDebugActions
                    .CreateAdditionalTestDomain,
                800));
            root.AddChild(ActionNode(
                "Reconcile relation pairs",
                GoauldInterDomainRelationDebugActions.ReconcilePairs,
                700));
            root.AddChild(ActionNode(
                "Force next transition",
                GoauldInterDomainRelationDebugActions.ForceNextTransition,
                600));
            root.AddChild(ActionNode(
                "Set first pair: Neutral",
                GoauldInterDomainRelationDebugActions.SetNeutral,
                500));
            root.AddChild(ActionNode(
                "Set first pair: Rivalry",
                GoauldInterDomainRelationDebugActions.SetRivalry,
                400));
            root.AddChild(ActionNode(
                "Set first pair: Open conflict",
                GoauldInterDomainRelationDebugActions.SetOpenConflict,
                300));
            root.AddChild(ActionNode(
                "Set first pair: Truce",
                GoauldInterDomainRelationDebugActions.SetTruce,
                200));
            root.AddChild(ActionNode(
                "Set first pair: Alliance",
                GoauldInterDomainRelationDebugActions.SetAlliance,
                150));
            root.AddChild(ActionNode(
                "Reset relations",
                GoauldInterDomainRelationDebugActions.Reset,
                100));

            return root;
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
    }
}
