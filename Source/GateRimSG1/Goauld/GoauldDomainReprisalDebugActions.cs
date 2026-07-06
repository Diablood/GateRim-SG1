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

        public static void CreateExtractionUltimatum()
        {
            Map map = Find.CurrentMap;
            GameComponent_GoauldDomainReprisalTracker tracker =
                GameComponent_GoauldDomainReprisalTracker.Current;

            if (tracker?.TryCreateDebugUltimatum(map) != true)
            {
                Reject(
                    "Could not create a Goa'uld extraction ultimatum. "
                    + "Reset an existing pending reaction or cooldown first.");
            }
        }

        public static void DefyCurrentUltimatum()
        {
            Faction faction = Find.FactionManager?.FirstFactionOfDef(
                GR_DefOf.SG1_GoauldSystemLordPrototype);

            if (faction == null
                || !GameComponent_GoauldDomainReprisalTracker
                    .IsCurrentUltimatum(faction))
            {
                Reject("No Goa'uld extraction ultimatum can be defied.");
                return;
            }

            GameComponent_GoauldDomainReprisalTracker
                .DefyFromLetter(faction);
        }

        public static void CreateFatalExtractionReprisal()
        {
            Map map = Find.CurrentMap;

            if (GameComponent_GoauldDomainReprisalTracker.Current
                    ?.TryCreateDebugFatalExtractionReprisal(map) != true)
            {
                Reject(
                    "Could not create a fatal-extraction reprisal. "
                    + "Reset an existing pending reaction or cooldown first.");
            }
        }

        public static void ExpireCurrentUltimatum()
        {
            if (GameComponent_GoauldDomainReprisalTracker.Current
                    ?.ExpireFirstUltimatumNow() != true)
            {
                Reject("No Goa'uld extraction ultimatum can be expired.");
            }
        }

        public static void KillDemandedSymbiote()
        {
            if (GameComponent_GoauldDomainReprisalTracker.Current
                    ?.KillFirstDemandedSymbioteDebug() != true)
            {
                Reject("No demanded Goa'uld symbiote can be killed.");
            }
        }

        public static void CreateSharedAllianceReprisal()
        {
            if (GameComponent_GoauldDomainReprisalTracker.Current
                    ?.TryCreateDebugSharedAllianceReprisal(
                        Find.CurrentMap) != true)
            {
                Reject(
                    "Could not create a shared Goa'uld alliance reprisal. "
                    + "Use SG-1 Command, create at least two active domains, "
                    + "set an alliance and clear any pending shared reaction.");
            }
        }

        public static void TriggerPendingSharedAllianceReprisal()
        {
            if (GameComponent_GoauldDomainReprisalTracker.Current
                    ?.TriggerFirstPendingSharedAllianceReprisalNow() != true)
            {
                Reject(
                    "No pending shared Goa'uld alliance reprisal could be "
                    + "triggered.");
            }
        }

        public static void ResetSharedAllianceReprisals()
        {
            GameComponent_GoauldDomainReprisalTracker tracker =
                GameComponent_GoauldDomainReprisalTracker.Current;

            if (tracker == null)
            {
                Reject("Goa'uld domain reprisal tracker is unavailable.");
                return;
            }

            tracker.ResetSharedAllianceReprisalsDebug();
            Messages.Message(
                "Shared Goa'uld alliance reprisal state reset.",
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        public static void CreateMajorAllianceFailure()
        {
            if (GameComponent_GoauldDomainReprisalTracker.Current
                    ?.TryCreateDebugMajorAllianceFailure(
                        Find.CurrentMap) != true)
            {
                Reject(
                    "Could not create a major Goa'uld alliance failure. "
                    + "Use SG-1 Command, create at least two active domains, "
                    + "set the first pair to Alliance and clear any pending "
                    + "alliance rupture.");
                return;
            }

            Messages.Message(
                "Major Goa'uld alliance failure recorded; a rupture is "
                + "pending.",
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        public static void TriggerPendingAllianceRupture()
        {
            if (GameComponent_GoauldDomainReprisalTracker.Current
                    ?.TriggerFirstPendingAllianceRuptureNow() != true)
            {
                Reject(
                    "No pending Goa'uld alliance rupture could be triggered.");
            }
        }

        public static void ResetAllianceRuptureState()
        {
            GameComponent_GoauldDomainReprisalTracker tracker =
                GameComponent_GoauldDomainReprisalTracker.Current;

            if (tracker == null)
            {
                Reject("Goa'uld domain reprisal tracker is unavailable.");
                return;
            }

            tracker.ResetAllianceRupturesDebug();
            Messages.Message(
                "Goa'uld alliance rupture state reset.",
                MessageTypeDefOf.NeutralEvent,
                historical: false);
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
                "Goa'uld domain extraction reaction state reset.",
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
