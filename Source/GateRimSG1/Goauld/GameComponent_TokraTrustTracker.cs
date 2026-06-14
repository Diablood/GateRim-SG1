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
    ///
    /// Trust tiers now provide the first concrete gameplay integration: they
    /// adjust therapeutic-offer duration, escort size and lightweight
    /// tretonin support gifts without enabling full diplomacy or quests yet.
    /// </summary>
    public class GameComponent_TokraTrustTracker : GameComponent
    {
        public const int MinimumTrust = -100;
        public const int MaximumTrust = 100;
        public const int AcceptedOfferTrustChange = 5;
        public const int HiddenSafehouseSignalTrustChange = 1;
        public const int SafehouseContactTrustChange = 1;
        public const int RefusedOfferTrustChange = -1;
        public const int ExpiredOfferTrustChange = -2;

        public const int RefusedWaryDiplomaticCooldownTicks = 180000;
        public const int ExpiredWaryDiplomaticCooldownTicks = 300000;

        public const int CooperativeThreshold = 10;
        public const int TrustedThreshold = 25;

        public const int WaryOfferDurationTicks = 60000;
        public const int NeutralOfferDurationTicks = 120000;
        public const int CooperativeOfferDurationTicks = 180000;
        public const int TrustedOfferDurationTicks = 240000;

        public const int CooperativeTretoninGiftCount = 1;
        public const int TrustedTretoninGiftCount = 2;

        public const float WaryTherapeuticOpportunityChanceFactor = 0.50f;
        public const float NeutralTherapeuticOpportunityChanceFactor = 1.00f;
        public const float CooperativeTherapeuticOpportunityChanceFactor = 1.25f;
        public const float TrustedTherapeuticOpportunityChanceFactor = 1.50f;

        public const float LockedMedicalSupportDeliveryChanceFactor = 0.00f;
        public const float CooperativeMedicalSupportDeliveryChanceFactor = 1.00f;
        public const float TrustedMedicalSupportDeliveryChanceFactor = 1.50f;

        private int trustScore;
        private int waryDiplomaticCooldownUntilTick;

        public GameComponent_TokraTrustTracker(Game game)
        {
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref trustScore, "tokraTrustScore", 0);
            Scribe_Values.Look(
                ref waryDiplomaticCooldownUntilTick,
                "tokraWaryDiplomaticCooldownUntilTick",
                0);

            trustScore = ClampTrust(trustScore);

            if (trustScore >= 0)
            {
                waryDiplomaticCooldownUntilTick = 0;
            }
        }

        public static int GetCurrentTrustScore()
        {
            return GetCurrentTracker()?.trustScore ?? 0;
        }

        public static TokraTrustTier GetCurrentTier()
        {
            return GetTierForScore(GetCurrentTrustScore());
        }

        public static bool DebugSetTrustScore(int value)
        {
            GameComponent_TokraTrustTracker tracker = GetCurrentTracker();

            if (tracker == null)
            {
                return false;
            }

            tracker.trustScore = ClampTrust(value);
            tracker.waryDiplomaticCooldownUntilTick = 0;
            return true;
        }

        public static string GetInspectString()
        {
            int score = GetCurrentTrustScore();
            TokraTrustTier tier = GetTierForScore(score);

            if (GR_Debug.ShowAdvancedInformation)
            {
                return "GR_TokraTrust_Inspect"
                    .Translate(score, GetTierLabel(tier))
                    .ToString();
            }

            return "GR_TokraTrust_InspectTierOnly"
                .Translate(GetTierLabel(tier))
                .ToString();
        }

        public static int GetCurrentOfferDurationTicks()
        {
            return GetOfferDurationTicks(GetCurrentTier());
        }

        public static int RollCurrentEscortCount()
        {
            TokraTrustTier tier = GetCurrentTier();
            int minimumEscortCount;
            int maximumEscortCount;

            GetEscortCountRange(
                tier,
                out minimumEscortCount,
                out maximumEscortCount);

            return Rand.RangeInclusive(minimumEscortCount, maximumEscortCount);
        }

        public static string GetCurrentTierLogLabel()
        {
            return GetTierLogLabel(GetCurrentTier());
        }

        public static int GetCurrentTretoninGiftCount()
        {
            return GetTretoninGiftCount(GetCurrentTier());
        }

        public static float GetCurrentTherapeuticOpportunityChanceFactor()
        {
            return GetTherapeuticOpportunityChanceFactor(GetCurrentTier());
        }

        public static float GetCurrentMedicalSupportDeliveryChanceFactor()
        {
            return GetMedicalSupportDeliveryChanceFactor(GetCurrentTier());
        }

        public static bool IsWaryDiplomaticCooldownActive()
        {
            return GetCurrentTracker()?.HasActiveWaryDiplomaticCooldown()
                ?? false;
        }

        public static int GetRemainingWaryDiplomaticCooldownTicks()
        {
            return GetCurrentTracker()?.GetRemainingWaryCooldownTicks() ?? 0;
        }

        public static float GetRemainingWaryDiplomaticCooldownDays()
        {
            return GetRemainingWaryDiplomaticCooldownTicks() / 60000f;
        }

        public static void NotifyHiddenSafehouseSignalAcknowledged()
        {
            GameComponent_TokraTrustTracker tracker = GetCurrentTracker();

            if (tracker == null)
            {
                GR_Log.Error(
                    "Cannot update Tok'ra trust: the trust tracker is "
                    + "unavailable.");
                return;
            }

            tracker.ApplyFlatTrustChange(
                HiddenSafehouseSignalTrustChange,
                "hidden safehouse signal",
                "GR_TokraTrust_HiddenSafehouseSignalAcknowledged");
        }

        public static void NotifySafehouseContactAcknowledged()
        {
            GameComponent_TokraTrustTracker tracker = GetCurrentTracker();

            if (tracker == null)
            {
                GR_Log.Error(
                    "Cannot update Tok'ra trust: the trust tracker is "
                    + "unavailable.");
                return;
            }

            tracker.ApplyFlatTrustChange(
                SafehouseContactTrustChange,
                "safehouse contact dialogue",
                "GR_TokraTrust_SafehouseContactAcknowledged");
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

        private void ApplyFlatTrustChange(
            int requestedChange,
            string reasonLabel,
            string messageKey)
        {
            int previousTrust = trustScore;
            TokraTrustTier previousTier = GetTierForScore(previousTrust);

            trustScore = ClampTrust(trustScore + requestedChange);

            int appliedChange = trustScore - previousTrust;
            TokraTrustTier currentTier = GetTierForScore(trustScore);
            string signedChange = FormatSignedChange(appliedChange);

            Messages.Message(
                messageKey.Translate(trustScore, signedChange),
                GetMessageType(appliedChange),
                historical: true);

            GR_Log.Message(
                $"Adjusted Tok'ra trust after {reasonLabel}: "
                + $"{previousTrust} -> {trustScore} ({signedChange}); "
                + $"tier {GetTierLogLabel(previousTier)} -> "
                + $"{GetTierLogLabel(currentTier)}.");
        }

        private void ApplyTherapeuticOfferOutcome(
            TokraTherapeuticOfferOutcome outcome)
        {
            int previousTrust = trustScore;
            TokraTrustTier previousTier = GetTierForScore(previousTrust);
            int requestedChange = GetTrustChange(outcome);

            trustScore = ClampTrust(trustScore + requestedChange);

            int appliedChange = trustScore - previousTrust;
            TokraTrustTier currentTier = GetTierForScore(trustScore);
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
                + $"({signedChange}); tier {GetTierLogLabel(previousTier)} "
                + $"-> {GetTierLogLabel(currentTier)}.");

            UpdateWaryDiplomaticCooldown(outcome, currentTier);
        }

        private void UpdateWaryDiplomaticCooldown(
            TokraTherapeuticOfferOutcome outcome,
            TokraTrustTier currentTier)
        {
            if (currentTier != TokraTrustTier.Wary)
            {
                waryDiplomaticCooldownUntilTick = 0;
                return;
            }

            int cooldownTicks = GetWaryDiplomaticCooldownTicks(outcome);

            if (cooldownTicks <= 0 || Find.TickManager == null)
            {
                return;
            }

            int requestedCooldownUntilTick
                = Find.TickManager.TicksGame + cooldownTicks;

            if (requestedCooldownUntilTick > waryDiplomaticCooldownUntilTick)
            {
                waryDiplomaticCooldownUntilTick = requestedCooldownUntilTick;
            }

            float remainingDays = GetRemainingWaryCooldownTicks() / 60000f;

            Messages.Message(
                "GR_TokraDiplomaticCooldown_Started"
                    .Translate(remainingDays.ToString("0.#")),
                MessageTypeDefOf.NegativeEvent,
                historical: true);

            GR_Log.Message(
                $"Started Tok'ra wary diplomatic cooldown after "
                + $"{GetOutcomeLogLabel(outcome)} therapeutic offer: "
                + $"{GetRemainingWaryCooldownTicks()} tick(s) "
                + $"({remainingDays:0.#} RimWorld day(s)) remaining.");
        }

        private bool HasActiveWaryDiplomaticCooldown()
        {
            return trustScore < 0 && GetRemainingWaryCooldownTicks() > 0;
        }

        private int GetRemainingWaryCooldownTicks()
        {
            if (Find.TickManager == null)
            {
                return 0;
            }

            return Math.Max(
                0,
                waryDiplomaticCooldownUntilTick - Find.TickManager.TicksGame);
        }

        private static int GetWaryDiplomaticCooldownTicks(
            TokraTherapeuticOfferOutcome outcome)
        {
            switch (outcome)
            {
                case TokraTherapeuticOfferOutcome.Refused:
                    return RefusedWaryDiplomaticCooldownTicks;
                case TokraTherapeuticOfferOutcome.Expired:
                    return ExpiredWaryDiplomaticCooldownTicks;
                default:
                    return 0;
            }
        }

        private static TokraTrustTier GetTierForScore(int score)
        {
            if (score < 0)
            {
                return TokraTrustTier.Wary;
            }

            if (score < CooperativeThreshold)
            {
                return TokraTrustTier.Neutral;
            }

            if (score < TrustedThreshold)
            {
                return TokraTrustTier.Cooperative;
            }

            return TokraTrustTier.Trusted;
        }

        private static int GetOfferDurationTicks(TokraTrustTier tier)
        {
            switch (tier)
            {
                case TokraTrustTier.Wary:
                    return WaryOfferDurationTicks;
                case TokraTrustTier.Cooperative:
                    return CooperativeOfferDurationTicks;
                case TokraTrustTier.Trusted:
                    return TrustedOfferDurationTicks;
                default:
                    return NeutralOfferDurationTicks;
            }
        }

        private static void GetEscortCountRange(
            TokraTrustTier tier,
            out int minimumEscortCount,
            out int maximumEscortCount)
        {
            switch (tier)
            {
                case TokraTrustTier.Wary:
                    minimumEscortCount = 1;
                    maximumEscortCount = 1;
                    return;
                case TokraTrustTier.Cooperative:
                    minimumEscortCount = 2;
                    maximumEscortCount = 2;
                    return;
                case TokraTrustTier.Trusted:
                    minimumEscortCount = 2;
                    maximumEscortCount = 3;
                    return;
                default:
                    minimumEscortCount = 1;
                    maximumEscortCount = 2;
                    return;
            }
        }

        private static int GetTretoninGiftCount(TokraTrustTier tier)
        {
            switch (tier)
            {
                case TokraTrustTier.Cooperative:
                    return CooperativeTretoninGiftCount;
                case TokraTrustTier.Trusted:
                    return TrustedTretoninGiftCount;
                default:
                    return 0;
            }
        }

        private static float GetTherapeuticOpportunityChanceFactor(
            TokraTrustTier tier)
        {
            switch (tier)
            {
                case TokraTrustTier.Wary:
                    return WaryTherapeuticOpportunityChanceFactor;
                case TokraTrustTier.Cooperative:
                    return CooperativeTherapeuticOpportunityChanceFactor;
                case TokraTrustTier.Trusted:
                    return TrustedTherapeuticOpportunityChanceFactor;
                default:
                    return NeutralTherapeuticOpportunityChanceFactor;
            }
        }

        private static float GetMedicalSupportDeliveryChanceFactor(
            TokraTrustTier tier)
        {
            switch (tier)
            {
                case TokraTrustTier.Cooperative:
                    return CooperativeMedicalSupportDeliveryChanceFactor;
                case TokraTrustTier.Trusted:
                    return TrustedMedicalSupportDeliveryChanceFactor;
                default:
                    return LockedMedicalSupportDeliveryChanceFactor;
            }
        }

        private static string GetTierLabel(TokraTrustTier tier)
        {
            switch (tier)
            {
                case TokraTrustTier.Wary:
                    return "GR_TokraTrust_Tier_Wary".Translate().ToString();
                case TokraTrustTier.Cooperative:
                    return "GR_TokraTrust_Tier_Cooperative"
                        .Translate()
                        .ToString();
                case TokraTrustTier.Trusted:
                    return "GR_TokraTrust_Tier_Trusted".Translate().ToString();
                default:
                    return "GR_TokraTrust_Tier_Neutral".Translate().ToString();
            }
        }

        private static string GetTierLogLabel(TokraTrustTier tier)
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

    public enum TokraTrustTier
    {
        Wary,
        Neutral,
        Cooperative,
        Trusted
    }

    public enum TokraTherapeuticOfferOutcome
    {
        Accepted,
        Refused,
        Expired
    }
}
