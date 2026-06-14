using System;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Tracks persistent Tok'ra safehouse leads discovered through clandestine
    /// signals.
    ///
    /// This intentionally does not create a world site yet. It gives future
    /// safehouse-site milestones a saved progression value to consume.
    /// </summary>
    public class GameComponent_TokraSafehouseLeadTracker : GameComponent
    {
        public const int MaximumSafehouseLeads = 3;
        public const int SafehouseSignalLeadGain = 1;

        private int safehouseLeadCount;

        public GameComponent_TokraSafehouseLeadTracker(Game game)
        {
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(
                ref safehouseLeadCount,
                "tokraSafehouseLeadCount",
                0);

            safehouseLeadCount = ClampLeadCount(safehouseLeadCount);
        }

        public static int GetCurrentLeadCount()
        {
            return GetCurrentTracker()?.safehouseLeadCount ?? 0;
        }

        public static bool DebugSetLeadCount(int value)
        {
            GameComponent_TokraSafehouseLeadTracker tracker
                = GetCurrentTracker();

            if (tracker == null)
            {
                return false;
            }

            tracker.safehouseLeadCount = ClampLeadCount(value);
            return true;
        }

        public static int GetRemainingLeadCapacity()
        {
            return MaximumSafehouseLeads - GetCurrentLeadCount();
        }

        public static bool CanStoreMoreLeads()
        {
            return GetCurrentLeadCount() < MaximumSafehouseLeads;
        }

        public static bool TryStoreSafehouseLead(
            int requestedGain,
            string reasonLabel)
        {
            GameComponent_TokraSafehouseLeadTracker tracker
                = GetCurrentTracker();

            if (tracker == null)
            {
                GR_Log.Error(
                    "Cannot store a Tok'ra safehouse lead: the lead "
                    + "tracker is unavailable.");
                return false;
            }

            return tracker.TryStoreLead(requestedGain, reasonLabel);
        }

        public static bool TryConsumeSafehouseLead(string reasonLabel)
        {
            GameComponent_TokraSafehouseLeadTracker tracker
                = GetCurrentTracker();

            if (tracker == null)
            {
                GR_Log.Error(
                    "Cannot consume a Tok'ra safehouse lead: the lead "
                    + "tracker is unavailable.");
                return false;
            }

            return tracker.TryConsumeLead(reasonLabel);
        }

        public static void NotifySafehouseSignalAcknowledged()
        {
            GameComponent_TokraSafehouseLeadTracker tracker
                = GetCurrentTracker();

            if (tracker == null)
            {
                GR_Log.Error(
                    "Cannot update Tok'ra safehouse leads: the lead tracker "
                    + "is unavailable.");
                return;
            }

            tracker.ApplyLeadGain(
                SafehouseSignalLeadGain,
                "safehouse signal");
        }

        private bool TryStoreLead(int requestedGain, string reasonLabel)
        {
            if (requestedGain <= 0)
            {
                GR_Log.Message(
                    $"Ignored non-positive Tok'ra safehouse lead gain "
                    + $"request after {reasonLabel}: {requestedGain}.");
                return false;
            }

            int previousLeadCount = safehouseLeadCount;

            ApplyLeadGain(requestedGain, reasonLabel);

            return safehouseLeadCount > previousLeadCount;
        }

        private bool TryConsumeLead(string reasonLabel)
        {
            if (safehouseLeadCount <= 0)
            {
                Messages.Message(
                    "GR_TokraSafehouseLead_NoneToConsume"
                        .Translate(),
                    MessageTypeDefOf.NeutralEvent,
                    historical: true);

                GR_Log.Message(
                    $"Could not consume a Tok'ra safehouse lead for "
                    + $"{reasonLabel}: no leads are stored.");
                return false;
            }

            int previousLeadCount = safehouseLeadCount;
            safehouseLeadCount = ClampLeadCount(safehouseLeadCount - 1);
            int appliedChange = safehouseLeadCount - previousLeadCount;

            Messages.Message(
                "GR_TokraSafehouseLead_Consumed"
                    .Translate(
                        safehouseLeadCount,
                        MaximumSafehouseLeads,
                        FormatSignedChange(appliedChange)),
                MessageTypeDefOf.PositiveEvent,
                historical: true);

            GR_Log.Message(
                $"Consumed a Tok'ra safehouse lead for {reasonLabel}: "
                + $"{previousLeadCount} -> {safehouseLeadCount} "
                + $"({FormatSignedChange(appliedChange)}); maximum "
                + $"{MaximumSafehouseLeads}.");

            return true;
        }

        private void ApplyLeadGain(int requestedGain, string reasonLabel)
        {
            int previousLeadCount = safehouseLeadCount;

            safehouseLeadCount = ClampLeadCount(
                safehouseLeadCount + requestedGain);

            int appliedGain = safehouseLeadCount - previousLeadCount;
            string signedChange = FormatSignedChange(appliedGain);

            if (appliedGain > 0)
            {
                Messages.Message(
                    "GR_TokraSafehouseLead_Gained"
                        .Translate(
                            safehouseLeadCount,
                            MaximumSafehouseLeads,
                            signedChange),
                    MessageTypeDefOf.PositiveEvent,
                    historical: true);
            }
            else
            {
                Messages.Message(
                    "GR_TokraSafehouseLead_AlreadyFull"
                        .Translate(
                            safehouseLeadCount,
                            MaximumSafehouseLeads),
                    MessageTypeDefOf.NeutralEvent,
                    historical: true);
            }

            GR_Log.Message(
                $"Adjusted Tok'ra safehouse leads after {reasonLabel}: "
                + $"{previousLeadCount} -> {safehouseLeadCount} "
                + $"({signedChange}); maximum {MaximumSafehouseLeads}.");
        }

        private static int ClampLeadCount(int value)
        {
            return Math.Max(0, Math.Min(MaximumSafehouseLeads, value));
        }

        private static string FormatSignedChange(int change)
        {
            return change > 0 ? $"+{change}" : change.ToString();
        }

        private static GameComponent_TokraSafehouseLeadTracker GetCurrentTracker()
        {
            return Current.Game
                ?.GetComponent<GameComponent_TokraSafehouseLeadTracker>();
        }
    }
}
