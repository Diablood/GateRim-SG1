using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public static class GoauldInterDomainRelationDebugActions
    {
        public static void ShowReport()
        {
            GameComponent_GoauldInterDomainRelationTracker tracker =
                GameComponent_GoauldInterDomainRelationTracker.Current;

            if (tracker == null)
            {
                Reject("The Goa'uld relation tracker is unavailable.");
                return;
            }

            Find.WindowStack.Add(
                new Dialog_MessageBox(tracker.BuildDebugReport()));
        }

        public static void CreateAdditionalTestDomain()
        {
            if (Find.FactionManager == null
                || GR_DefOf.SG1_GoauldSystemLordPrototype == null)
            {
                Reject("A Goa'uld System Lord faction cannot be generated.");
                return;
            }

            Faction faction = FactionGenerator.NewGeneratedFaction(
                new FactionGeneratorParms(
                    GR_DefOf.SG1_GoauldSystemLordPrototype,
                    default(IdeoGenerationParms),
                    hidden: false));

            if (faction == null)
            {
                Reject("A Goa'uld test domain could not be generated.");
                return;
            }

            Find.FactionManager.Add(faction);
            GameComponent_GoauldDomainDoctrineTracker.Current
                ?.GetOrAssignProfile(faction);
            GameComponent_GoauldInterDomainRelationTracker.Current
                ?.ReconcileAllPairs();

            Messages.Message(
                "Created additional Goa'uld test domain: " + faction.Name,
                MessageTypeDefOf.PositiveEvent,
                historical: false);
        }

        public static void ReconcilePairs()
        {
            GameComponent_GoauldInterDomainRelationTracker.Current
                ?.ReconcileAllPairs();
            Messages.Message(
                "Reconciled Goa'uld inter-domain relation pairs.",
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        public static void ForceNextTransition()
        {
            if (GameComponent_GoauldInterDomainRelationTracker.Current
                ?.ForceNextTransitionDebug() != true)
            {
                Reject(
                    "At least two active Goa'uld domains are required.");
            }
        }

        public static void SetNeutral()
        {
            SetRelation(GoauldInterDomainRelation.Neutral);
        }

        public static void SetRivalry()
        {
            SetRelation(GoauldInterDomainRelation.Rivalry);
        }

        public static void SetOpenConflict()
        {
            SetRelation(GoauldInterDomainRelation.OpenConflict);
        }

        public static void SetTruce()
        {
            SetRelation(GoauldInterDomainRelation.Truce);
        }

        public static void SetAlliance()
        {
            SetRelation(GoauldInterDomainRelation.Alliance);
        }

        public static void Reset()
        {
            GameComponent_GoauldInterDomainRelationTracker.Current
                ?.ResetDebug();
            Messages.Message(
                "Reset Goa'uld inter-domain relations to neutral.",
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        private static void SetRelation(
            GoauldInterDomainRelation relation)
        {
            if (GameComponent_GoauldInterDomainRelationTracker.Current
                ?.SetFirstPairRelationDebug(relation) != true)
            {
                Reject(
                    "At least two active Goa'uld domains are required.");
            }
        }

        private static void Reject(string text)
        {
            Messages.Message(
                text,
                MessageTypeDefOf.RejectInput,
                historical: false);
        }
    }
}
