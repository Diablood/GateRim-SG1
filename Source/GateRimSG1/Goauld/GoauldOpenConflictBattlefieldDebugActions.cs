using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public static class GoauldOpenConflictBattlefieldDebugActions
    {
        public static void ShowReport()
        {
            GameComponent_GoauldOpenConflictBattlefieldTracker tracker =
                GameComponent_GoauldOpenConflictBattlefieldTracker.Current;

            if (tracker == null)
            {
                Reject("The Goa'uld battlefield tracker is unavailable.");
                return;
            }

            Find.WindowStack.Add(
                new Dialog_MessageBox(tracker.BuildDebugReport()));
        }

        public static void MakeOpportunityDue()
        {
            GameComponent_GoauldOpenConflictBattlefieldTracker tracker =
                GameComponent_GoauldOpenConflictBattlefieldTracker.Current;

            if (tracker == null)
            {
                Reject("The Goa'uld battlefield tracker is unavailable.");
                return;
            }

            tracker.MakeOpportunityDueDebug();
            Messages.Message(
                "The next Goa'uld battlefield opportunity is now due.",
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        public static void ForceBattlefield()
        {
            Map map = Find.CurrentMap;
            GameComponent_GoauldOpenConflictBattlefieldTracker tracker =
                GameComponent_GoauldOpenConflictBattlefieldTracker.Current;

            if (map == null || tracker == null)
            {
                Reject("The current map or battlefield tracker is unavailable.");
                return;
            }

            if (!tracker.ForceBattlefieldDebug(map))
            {
                Reject(
                    "Could not start a battlefield. An active open-conflict "
                    + "pair and no existing battlefield are required.");
                return;
            }

            Messages.Message(
                "Started a Goa'uld open-conflict battlefield on the current map.",
                map.Parent,
                MessageTypeDefOf.ThreatSmall,
                historical: false);
        }

        public static void OrderWithdrawal()
        {
            GameComponent_GoauldOpenConflictBattlefieldTracker tracker =
                GameComponent_GoauldOpenConflictBattlefieldTracker.Current;

            if (tracker?.ForceWithdrawalDebug() != true)
            {
                Reject("No active Goa'uld battlefield can be withdrawn.");
            }
        }

        public static void Reset()
        {
            GameComponent_GoauldOpenConflictBattlefieldTracker.Current
                ?.ResetDebug();
            Messages.Message(
                "Reset the Goa'uld battlefield scheduler.",
                MessageTypeDefOf.NeutralEvent,
                historical: false);
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
