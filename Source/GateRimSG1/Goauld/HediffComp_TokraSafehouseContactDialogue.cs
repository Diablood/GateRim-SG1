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
        private const int WaryMedicalBriefingMedicineXp = 250;
        private const int NeutralMedicalBriefingMedicineXp = 400;
        private const int CooperativeMedicalBriefingMedicineXp = 600;
        private const int TrustedMedicalBriefingMedicineXp = 800;
        private const int FollowUpLeadGain = 1;

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

            TokraTrustTier briefingTier = GameComponent_TokraTrustTracker
                .GetCurrentTier();
            int medicalBriefingMedicineXp = GetMedicalBriefingMedicineXp(
                briefingTier);
            bool appliedMedicalBriefing = TryApplyMedicalBriefingTraining(
                negotiator,
                medicalBriefingMedicineXp);

            string negotiatorLabel = negotiator?.LabelShortCap ?? "";
            string medicineXpText = medicalBriefingMedicineXp.ToString();
            string medicalHint = GetMedicalHintMessage(briefingTier);
            FollowUpLeadResult followUpLeadResult = TryGrantFollowUpLead(
                briefingTier);
            string followUpLeadText = GetFollowUpLeadDialogMessage(
                briefingTier,
                followUpLeadResult);

            Messages.Message(
                GetAcknowledgementMessageKey(briefingTier)
                    .Translate(
                        contact.LabelShortCap,
                        negotiatorLabel,
                        medicineXpText),
                contact,
                MessageTypeDefOf.PositiveEvent,
                historical: true);

            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    GetBriefingDialogMessageKey(briefingTier)
                        .Translate(
                            contact.LabelShortCap,
                            negotiatorLabel,
                            medicineXpText,
                            medicalHint,
                            followUpLeadText)
                        .ToString()));

            GameComponent_TokraTrustTracker.NotifySafehouseContactAcknowledged();

            GR_Log.Message(
                $"Acknowledged Tok'ra safehouse contact dialogue with "
                + $"{contact.LabelShortCap} on map "
                + $"{contact.Map?.uniqueID.ToString() ?? "unknown"}; "
                + $"trust tier {GetBriefingTierLogLabel(briefingTier)}; "
                + $"medicine XP {medicalBriefingMedicineXp}; "
                + $"follow-up lead result: "
                + $"{GetFollowUpLeadResultLogLabel(followUpLeadResult)}; "
                + $"medical briefing applied: {appliedMedicalBriefing}.");

            return true;
        }

        private static FollowUpLeadResult TryGrantFollowUpLead(
            TokraTrustTier tier)
        {
            if (tier == TokraTrustTier.Wary || tier == TokraTrustTier.Neutral)
            {
                return FollowUpLeadResult.NotOffered;
            }

            if (!GameComponent_TokraSafehouseLeadTracker.CanStoreMoreLeads())
            {
                return FollowUpLeadResult.AlreadyFull;
            }

            bool stored = GameComponent_TokraSafehouseLeadTracker
                .TryStoreSafehouseLead(
                    FollowUpLeadGain,
                    "safehouse contact follow-up");

            return stored
                ? FollowUpLeadResult.Stored
                : FollowUpLeadResult.Unavailable;
        }

        private static bool TryApplyMedicalBriefingTraining(
            Pawn negotiator,
            int medicineXp)
        {
            if (medicineXp <= 0 || negotiator?.skills == null)
            {
                return false;
            }

            SkillRecord medicine = negotiator.skills.GetSkill(
                SkillDefOf.Medicine);

            if (medicine == null)
            {
                return false;
            }

            medicine.Learn(medicineXp, true);
            return true;
        }

        private static int GetMedicalBriefingMedicineXp(TokraTrustTier tier)
        {
            switch (tier)
            {
                case TokraTrustTier.Wary:
                    return WaryMedicalBriefingMedicineXp;
                case TokraTrustTier.Cooperative:
                    return CooperativeMedicalBriefingMedicineXp;
                case TokraTrustTier.Trusted:
                    return TrustedMedicalBriefingMedicineXp;
                default:
                    return NeutralMedicalBriefingMedicineXp;
            }
        }

        private static string GetAcknowledgementMessageKey(TokraTrustTier tier)
        {
            switch (tier)
            {
                case TokraTrustTier.Wary:
                    return "GR_TokraSafehouseContactDialogue_Acknowledged_Wary";
                case TokraTrustTier.Cooperative:
                    return "GR_TokraSafehouseContactDialogue_Acknowledged_Cooperative";
                case TokraTrustTier.Trusted:
                    return "GR_TokraSafehouseContactDialogue_Acknowledged_Trusted";
                default:
                    return "GR_TokraSafehouseContactDialogue_Acknowledged_Neutral";
            }
        }

        private static string GetMedicalHintMessage(TokraTrustTier tier)
        {
            return GetMedicalHintMessageKey(tier).Translate().ToString();
        }

        private static string GetMedicalHintMessageKey(TokraTrustTier tier)
        {
            switch (tier)
            {
                case TokraTrustTier.Wary:
                    return "GR_TokraSafehouseContactDialogue_Hint_Wary";
                case TokraTrustTier.Cooperative:
                    return "GR_TokraSafehouseContactDialogue_Hint_Cooperative";
                case TokraTrustTier.Trusted:
                    return "GR_TokraSafehouseContactDialogue_Hint_Trusted";
                default:
                    return "GR_TokraSafehouseContactDialogue_Hint_Neutral";
            }
        }

        private static string GetFollowUpLeadDialogMessage(
            TokraTrustTier tier,
            FollowUpLeadResult result)
        {
            return GetFollowUpLeadDialogMessageKey(tier, result)
                .Translate()
                .ToString();
        }

        private static string GetFollowUpLeadDialogMessageKey(
            TokraTrustTier tier,
            FollowUpLeadResult result)
        {
            if (result == FollowUpLeadResult.Stored)
            {
                return tier == TokraTrustTier.Trusted
                    ? "GR_TokraSafehouseContactDialogue_FollowUpLead_Trusted"
                    : "GR_TokraSafehouseContactDialogue_FollowUpLead_Cooperative";
            }

            if (result == FollowUpLeadResult.AlreadyFull)
            {
                return "GR_TokraSafehouseContactDialogue_FollowUpLead_Full";
            }

            if (result == FollowUpLeadResult.Unavailable)
            {
                return "GR_TokraSafehouseContactDialogue_FollowUpLead_Unavailable";
            }

            return "GR_TokraSafehouseContactDialogue_FollowUpLead_None";
        }

        private static string GetBriefingDialogMessageKey(TokraTrustTier tier)
        {
            switch (tier)
            {
                case TokraTrustTier.Wary:
                    return "GR_TokraSafehouseContactDialogue_BriefingDialog_Wary";
                case TokraTrustTier.Cooperative:
                    return "GR_TokraSafehouseContactDialogue_BriefingDialog_Cooperative";
                case TokraTrustTier.Trusted:
                    return "GR_TokraSafehouseContactDialogue_BriefingDialog_Trusted";
                default:
                    return "GR_TokraSafehouseContactDialogue_BriefingDialog_Neutral";
            }
        }

        private static string GetBriefingTierLogLabel(TokraTrustTier tier)
        {
            switch (tier)
            {
                case TokraTrustTier.Wary:
                    return "wary";
                case TokraTrustTier.Cooperative:
                    return "cooperative";
                case TokraTrustTier.Trusted:
                    return "trusted";
                default:
                    return "neutral";
            }
        }

        private static string GetFollowUpLeadResultLogLabel(
            FollowUpLeadResult result)
        {
            switch (result)
            {
                case FollowUpLeadResult.Stored:
                    return "stored";
                case FollowUpLeadResult.AlreadyFull:
                    return "already full";
                case FollowUpLeadResult.Unavailable:
                    return "unavailable";
                default:
                    return "not offered";
            }
        }

        private enum FollowUpLeadResult
        {
            NotOffered,
            Stored,
            AlreadyFull,
            Unavailable
        }
    }
}
