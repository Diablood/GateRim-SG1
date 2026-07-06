using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public static class GoauldTerritorialStrategyDebugActions
    {
        public static void ShowReport()
        {
            GameComponent_GoauldTerritorialStrategyTracker tracker =
                GameComponent_GoauldTerritorialStrategyTracker.Current;

            if (tracker == null)
            {
                Reject("The Goa'uld territorial tracker is unavailable.");
                return;
            }

            Find.WindowStack.Add(
                new Dialog_MessageBox(tracker.BuildDebugReport()));
        }

        public static void Reconcile()
        {
            GameComponent_GoauldTerritorialStrategyTracker.Current
                ?.ReconcileDebug();
            Messages.Message(
                "Reconciled Goa'uld territorial and diplomatic state.",
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        public static void CreatePendingReservation()
        {
            if (GameComponent_GoauldTerritorialStrategyTracker.Current
                ?.TryCreateDebugReservation() != true)
            {
                Reject(
                    "No safe open-conflict territorial takeover is currently available. Check the territorial report for the exact refusal reason.");
                return;
            }

            Messages.Message(
                "Created one pending Goa'uld territorial takeover.",
                MessageTypeDefOf.PositiveEvent,
                historical: false);
        }

        public static void RunNaturalAttempt()
        {
            if (GameComponent_GoauldTerritorialStrategyTracker.Current
                ?.TryRunNaturalAttemptDebug() != true)
            {
                Reject(
                    "The natural territorial scheduling attempt found no eligible takeover. Check the territorial report for the exact refusal reason.");
                return;
            }

            Messages.Message(
                "The natural territorial scheduler created one pending takeover.",
                MessageTypeDefOf.PositiveEvent,
                historical: false);
        }

        public static void TriggerPendingReservation()
        {
            if (GameComponent_GoauldTerritorialStrategyTracker.Current
                ?.TriggerPendingReservationNow() != true)
            {
                Reject(
                    "No eligible Goa'uld territorial takeover was completed.");
                return;
            }

            Messages.Message(
                "Completed the pending Goa'uld territorial takeover.",
                MessageTypeDefOf.PositiveEvent,
                historical: false);
        }

        public static void CancelPendingReservation()
        {
            if (GameComponent_GoauldTerritorialStrategyTracker.Current
                ?.CancelPendingReservationDebug() != true)
            {
                Reject("No territorial takeover is pending.");
            }
        }

        public static void Reset()
        {
            GameComponent_GoauldTerritorialStrategyTracker.Current
                ?.ResetDebug();
            Messages.Message(
                "Reset Goa'uld territorial strategy state.",
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
