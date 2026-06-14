using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Stores the once-per-contact state for the peaceful Tok'ra contact
    /// generated inside hidden safehouse sites.
    ///
    /// The actual player-facing interaction is exposed through the vanilla
    /// colonist-right-click float menu, not through a gizmo on the Tok'ra pawn.
    /// </summary>
    public class HediffComp_TokraSafehouseContactDialogue : HediffComp
    {
        private bool contactAcknowledged;

        public bool ContactAcknowledged => contactAcknowledged;

        public override void CompExposeData()
        {
            base.CompExposeData();

            Scribe_Values.Look(
                ref contactAcknowledged,
                "tokraSafehouseContactAcknowledged",
                false);
        }

        public override IEnumerable<Gizmo> CompGetGizmos()
        {
            yield break;
        }

        internal bool TryAcknowledgeContact(Pawn negotiator)
        {
            Pawn contact = Pawn;

            if (!TokraSafehouseContactDialogueUtility
                    .IsValidSafehouseContact(contact))
            {
                GR_Log.Message(
                    "Cannot acknowledge the Tok'ra safehouse contact: the "
                    + "target pawn is no longer a valid safehouse contact.");
                return false;
            }

            if (contactAcknowledged)
            {
                Messages.Message(
                    "GR_TokraSafehouseContactDialogue_AlreadyAcknowledged"
                        .Translate(),
                    contact,
                    MessageTypeDefOf.NeutralEvent,
                    historical: false);
                return false;
            }

            contactAcknowledged = true;

            string negotiatorLabel = negotiator?.LabelShortCap ?? "";
            Messages.Message(
                "GR_TokraSafehouseContactDialogue_Acknowledged"
                    .Translate(contact.LabelShortCap, negotiatorLabel),
                contact,
                MessageTypeDefOf.PositiveEvent,
                historical: true);

            GameComponent_TokraTrustTracker.NotifySafehouseContactAcknowledged();

            GR_Log.Message(
                $"Acknowledged Tok'ra safehouse contact dialogue with "
                + $"{contact.LabelShortCap} on map "
                + $"{contact.Map?.uniqueID.ToString() ?? "unknown"}.");

            return true;
        }
    }
}
