using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public static class GoauldDomainReprisalDebugActions
    {
        public static void ShowState()
        {
            GameComponent_GoauldDomainReprisalTracker tracker =
                GameComponent_GoauldDomainReprisalTracker.Current;

            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    tracker?.BuildDebugReport()
                        ?? "Goa'uld domain reprisal tracker is unavailable."));
        }

        public static void ScheduleExtractionReprisal()
        {
            Map map = Find.CurrentMap;
            GameComponent_GoauldDomainReprisalTracker tracker =
                GameComponent_GoauldDomainReprisalTracker.Current;

            if (tracker?.TryScheduleDebug(map) != true)
            {
                Reject(
                    "Could not schedule a Goa'uld extraction reprisal. "
                    + "Reset an existing pending reaction or cooldown first.");
            }
        }

        public static void TriggerPendingReprisal()
        {
            if (GameComponent_GoauldDomainReprisalTracker.Current
                    ?.TriggerFirstPendingNow() != true)
            {
                Reject("No pending Goa'uld domain reprisal could be triggered.");
            }
        }

        public static void Reset()
        {
            GameComponent_GoauldDomainReprisalTracker tracker =
                GameComponent_GoauldDomainReprisalTracker.Current;

            if (tracker == null)
            {
                Reject("Goa'uld domain reprisal tracker is unavailable.");
                return;
            }

            tracker.ResetDebug();
            Messages.Message(
                "Goa'uld domain reprisal state reset.",
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
