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
                    "No safe open-conflict territorial reservation is currently available. Check the territorial report for the exact refusal reason.");
                return;
            }

            Messages.Message(
                "Created one Goa'uld territorial dry-run reservation.",
                MessageTypeDefOf.PositiveEvent,
                historical: false);
        }

        public static void TriggerPendingReservation()
        {
            if (GameComponent_GoauldTerritorialStrategyTracker.Current
                ?.TriggerPendingReservationNow() != true)
            {
                Reject(
                    "No eligible Goa'uld territorial reservation completed its dry run.");
                return;
            }

            Messages.Message(
                "Completed the territorial dry run without modifying the world.",
                MessageTypeDefOf.PositiveEvent,
                historical: false);
        }

        public static void CancelPendingReservation()
        {
            if (GameComponent_GoauldTerritorialStrategyTracker.Current
                ?.CancelPendingReservationDebug() != true)
            {
                Reject("No territorial reservation is pending.");
            }
        }

        public static void Reset()
        {
            GameComponent_GoauldTerritorialStrategyTracker.Current
                ?.ResetDebug();
            Messages.Message(
                "Reset Goa'uld territorial safeguard state.",
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
