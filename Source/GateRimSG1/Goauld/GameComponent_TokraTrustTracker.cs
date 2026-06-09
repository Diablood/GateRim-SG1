using System;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Lightweight persistent Tok'ra trust foundation.
    ///
    /// The runtime Tok'ra faction deliberately remains hidden during the
    /// current prototype phase. Hidden RimWorld factions do not participate in
    /// the standard goodwill system, so therapeutic-offer outcomes update a
    /// small mod-owned trust score until broader diplomacy is introduced.
    /// </summary>
    public class GameComponent_TokraTrustTracker : GameComponent
    {
        public const int MinimumTrust = -100;
        public const int MaximumTrust = 100;
        public const int AcceptedOfferTrustChange = 5;
        public const int RefusedOfferTrustChange = -1;
        public const int ExpiredOfferTrustChange = -2;

        private int trustScore;

        public GameComponent_TokraTrustTracker(Game game)
        {
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref trustScore, "tokraTrustScore", 0);
            trustScore = ClampTrust(trustScore);
        }

        public static int GetCurrentTrustScore()
        {
            return GetCurrentTracker()?.trustScore ?? 0;
        }

        public static string GetInspectString()
        {
            return "GR_TokraTrust_Inspect"
                .Translate(GetCurrentTrustScore())
                .ToString();
        }

        public static void NotifyTherapeuticOfferOutcome(
            TokraTherapeuticOfferOutcome outcome)
        {
            GameComponent_TokraTrustTracker tracker = GetCurrentTracker();

            if (tracker == null)
            {
                GR_Log.Error(
                    "Cannot update Tok'ra trust: the trust tracker is "
                    + "unavailable.");
                return;
            }

            tracker.ApplyTherapeuticOfferOutcome(outcome);
        }

        private void ApplyTherapeuticOfferOutcome(
            TokraTherapeuticOfferOutcome outcome)
        {
            int previousTrust = trustScore;
            int requestedChange = GetTrustChange(outcome);

            trustScore = ClampTrust(trustScore + requestedChange);

            int appliedChange = trustScore - previousTrust;
            string signedChange = FormatSignedChange(appliedChange);
            string outcomeLabel = GetOutcomeLogLabel(outcome);
            string messageKey = GetOutcomeMessageKey(outcome);

            Messages.Message(
                messageKey.Translate(trustScore, signedChange),
                GetMessageType(appliedChange),
                historical: true);

            GR_Log.Message(
                $"Adjusted Tok'ra trust after {outcomeLabel} therapeutic "
                + $"offer: {previousTrust} -> {trustScore} "
                + $"({signedChange}).");
        }

        private static int GetTrustChange(TokraTherapeuticOfferOutcome outcome)
        {
            switch (outcome)
            {
                case TokraTherapeuticOfferOutcome.Accepted:
                    return AcceptedOfferTrustChange;
                case TokraTherapeuticOfferOutcome.Refused:
                    return RefusedOfferTrustChange;
                case TokraTherapeuticOfferOutcome.Expired:
                    return ExpiredOfferTrustChange;
                default:
                    return 0;
            }
        }

        private static string GetOutcomeMessageKey(
            TokraTherapeuticOfferOutcome outcome)
        {
            switch (outcome)
            {
                case TokraTherapeuticOfferOutcome.Accepted:
                    return "GR_TokraTrust_OfferAccepted";
                case TokraTherapeuticOfferOutcome.Refused:
                    return "GR_TokraTrust_OfferRefused";
                case TokraTherapeuticOfferOutcome.Expired:
                    return "GR_TokraTrust_OfferExpired";
                default:
                    return "GR_TokraTrust_Changed";
            }
        }

        private static string GetOutcomeLogLabel(
            TokraTherapeuticOfferOutcome outcome)
        {
            switch (outcome)
            {
                case TokraTherapeuticOfferOutcome.Accepted:
                    return "accepted";
                case TokraTherapeuticOfferOutcome.Refused:
                    return "refused";
                case TokraTherapeuticOfferOutcome.Expired:
                    return "expired";
                default:
                    return "unknown";
            }
        }

        private static MessageTypeDef GetMessageType(int change)
        {
            if (change > 0)
            {
                return MessageTypeDefOf.PositiveEvent;
            }

            if (change < 0)
            {
                return MessageTypeDefOf.NegativeEvent;
            }

            return MessageTypeDefOf.NeutralEvent;
        }

        private static int ClampTrust(int value)
        {
            return Math.Max(MinimumTrust, Math.Min(MaximumTrust, value));
        }

        private static string FormatSignedChange(int change)
        {
            return change > 0 ? $"+{change}" : change.ToString();
        }

        private static GameComponent_TokraTrustTracker GetCurrentTracker()
        {
            return Current.Game?.GetComponent<GameComponent_TokraTrustTracker>();
        }
    }

    public enum TokraTherapeuticOfferOutcome
    {
        Accepted,
        Refused,
        Expired
    }
}
