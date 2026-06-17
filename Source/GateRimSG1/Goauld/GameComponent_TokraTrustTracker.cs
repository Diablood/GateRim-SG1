using System;
using RimWorld;
using RimWorld.Planet;
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

        private const int FirstTrustMissionBriefingMinimumDelayTicks = 30000;
        private const int FirstTrustMissionBriefingMaximumDelayTicks = 90000;
        private const int FirstTrustMissionBriefingCheckIntervalTicks = 2500;
        private const int FirstTrustMissionCacheMinimumDelayTicks = 15000;
        private const int FirstTrustMissionCacheMaximumDelayTicks = 45000;
        private const int FirstTrustMissionCacheCheckIntervalTicks = 2500;
        private const int FirstTrustMissionCacheRetryDelayTicks = 7500;
        private const int FirstTrustMissionDecodedLeadMinimumDelayTicks = 15000;
        private const int FirstTrustMissionDecodedLeadMaximumDelayTicks = 45000;
        private const int FirstTrustMissionDecodedLeadCheckIntervalTicks = 2500;
        private const int FirstTrustMissionWorldSiteMinimumDelayTicks = 15000;
        private const int FirstTrustMissionWorldSiteMaximumDelayTicks = 45000;
        private const int FirstTrustMissionWorldSiteCheckIntervalTicks = 2500;
        private const int FirstTrustMissionWorldSiteRetryDelayTicks = 7500;
        private const int FirstTrustMissionWorldSiteMinimumDistance = 6;
        private const int FirstTrustMissionWorldSiteMaximumDistance = 18;
        private const string FirstTrustMissionCacheThingDefName =
            "SG1_TokraMissionIntelPacket";
        private const string FirstTrustMissionWorldSiteDefName =
            "SG1_TokraDecodedMissionWorldSite";

        private int trustScore;
        private int waryDiplomaticCooldownUntilTick;
        private bool firstTrustMissionHookPrepared;
        private int firstTrustMissionHookPreparedTick;
        private bool firstTrustMissionBriefingReceived;
        private int firstTrustMissionBriefingContactTick;
        private int nextFirstTrustMissionBriefingCheckTick;
        private bool firstTrustMissionCacheDelivered;
        private int firstTrustMissionCacheDeliveryTick;
        private int nextFirstTrustMissionCacheCheckTick;
        private bool firstTrustMissionIntelAnalyzed;
        private int firstTrustMissionIntelAnalyzedTick;
        private bool firstTrustMissionLeadDecoded;
        private int firstTrustMissionLeadDecodedTick;
        private int firstTrustMissionLeadDecodeContactTick;
        private int nextFirstTrustMissionLeadDecodeCheckTick;
        private bool firstTrustMissionWorldSiteRevealed;
        private int firstTrustMissionWorldSiteRevealedTick;
        private int firstTrustMissionWorldSiteRevealTick;
        private int nextFirstTrustMissionWorldSiteRevealCheckTick;
        private bool firstTrustMissionWorldSiteReconnoitered;
        private int firstTrustMissionWorldSiteReconnoiteredTick;

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
            Scribe_Values.Look(
                ref firstTrustMissionHookPrepared,
                "tokraFirstTrustMissionHookPrepared",
                false);
            Scribe_Values.Look(
                ref firstTrustMissionHookPreparedTick,
                "tokraFirstTrustMissionHookPreparedTick",
                0);
            Scribe_Values.Look(
                ref firstTrustMissionBriefingReceived,
                "tokraFirstTrustMissionBriefingReceived",
                false);
            Scribe_Values.Look(
                ref firstTrustMissionBriefingContactTick,
                "tokraFirstTrustMissionBriefingContactTick",
                0);
            Scribe_Values.Look(
                ref nextFirstTrustMissionBriefingCheckTick,
                "tokraNextFirstTrustMissionBriefingCheckTick",
                0);
            Scribe_Values.Look(
                ref firstTrustMissionCacheDelivered,
                "tokraFirstTrustMissionCacheDelivered",
                false);
            Scribe_Values.Look(
                ref firstTrustMissionCacheDeliveryTick,
                "tokraFirstTrustMissionCacheDeliveryTick",
                0);
            Scribe_Values.Look(
                ref nextFirstTrustMissionCacheCheckTick,
                "tokraNextFirstTrustMissionCacheCheckTick",
                0);
            Scribe_Values.Look(
                ref firstTrustMissionIntelAnalyzed,
                "tokraFirstTrustMissionIntelAnalyzed",
                false);
            Scribe_Values.Look(
                ref firstTrustMissionIntelAnalyzedTick,
                "tokraFirstTrustMissionIntelAnalyzedTick",
                0);
            Scribe_Values.Look(
                ref firstTrustMissionLeadDecoded,
                "tokraFirstTrustMissionLeadDecoded",
                false);
            Scribe_Values.Look(
                ref firstTrustMissionLeadDecodedTick,
                "tokraFirstTrustMissionLeadDecodedTick",
                0);
            Scribe_Values.Look(
                ref firstTrustMissionLeadDecodeContactTick,
                "tokraFirstTrustMissionLeadDecodeContactTick",
                0);
            Scribe_Values.Look(
                ref nextFirstTrustMissionLeadDecodeCheckTick,
                "tokraNextFirstTrustMissionLeadDecodeCheckTick",
                0);
            Scribe_Values.Look(
                ref firstTrustMissionWorldSiteRevealed,
                "tokraFirstTrustMissionWorldSiteRevealed",
                false);
            Scribe_Values.Look(
                ref firstTrustMissionWorldSiteRevealedTick,
                "tokraFirstTrustMissionWorldSiteRevealedTick",
                0);
            Scribe_Values.Look(
                ref firstTrustMissionWorldSiteRevealTick,
                "tokraFirstTrustMissionWorldSiteRevealTick",
                0);
            Scribe_Values.Look(
                ref nextFirstTrustMissionWorldSiteRevealCheckTick,
                "tokraNextFirstTrustMissionWorldSiteRevealCheckTick",
                0);
            Scribe_Values.Look(
                ref firstTrustMissionWorldSiteReconnoitered,
                "tokraFirstTrustMissionWorldSiteReconnoitered",
                false);
            Scribe_Values.Look(
                ref firstTrustMissionWorldSiteReconnoiteredTick,
                "tokraFirstTrustMissionWorldSiteReconnoiteredTick",
                0);

            trustScore = ClampTrust(trustScore);

            if (trustScore >= 0)
            {
                waryDiplomaticCooldownUntilTick = 0;
            }
        }

        public override void GameComponentTick()
        {
            base.GameComponentTick();

            TrySendPendingFirstTrustMissionBriefingContact();
            TryDeliverPendingFirstTrustMissionCache();
            TrySendPendingFirstTrustMissionDecodedLead();
            TryRevealPendingFirstTrustMissionWorldSite();
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

        public static bool IsFirstTrustMissionHookPrepared()
        {
            return GetCurrentTracker()?.firstTrustMissionHookPrepared ?? false;
        }

        public static int GetFirstTrustMissionHookPreparedTick()
        {
            return GetCurrentTracker()?.firstTrustMissionHookPreparedTick ?? 0;
        }

        public static bool IsFirstTrustMissionBriefingReceived()
        {
            return GetCurrentTracker()?.firstTrustMissionBriefingReceived ?? false;
        }

        public static int GetFirstTrustMissionBriefingContactTick()
        {
            return GetCurrentTracker()?.firstTrustMissionBriefingContactTick ?? 0;
        }

        public static int GetRemainingFirstTrustMissionBriefingContactTicks()
        {
            GameComponent_TokraTrustTracker tracker = GetCurrentTracker();

            if (tracker == null || tracker.firstTrustMissionBriefingReceived)
            {
                return 0;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            return Math.Max(
                0,
                tracker.firstTrustMissionBriefingContactTick - currentTick);
        }

        public static bool IsFirstTrustMissionCacheDelivered()
        {
            return GetCurrentTracker()?.firstTrustMissionCacheDelivered ?? false;
        }

        public static int GetFirstTrustMissionCacheDeliveryTick()
        {
            return GetCurrentTracker()?.firstTrustMissionCacheDeliveryTick ?? 0;
        }

        public static bool IsFirstTrustMissionIntelAnalyzed()
        {
            return GetCurrentTracker()?.firstTrustMissionIntelAnalyzed ?? false;
        }

        public static int GetFirstTrustMissionIntelAnalyzedTick()
        {
            return GetCurrentTracker()?.firstTrustMissionIntelAnalyzedTick ?? 0;
        }

        public static bool IsFirstTrustMissionLeadDecoded()
        {
            return GetCurrentTracker()?.firstTrustMissionLeadDecoded ?? false;
        }

        public static int GetFirstTrustMissionLeadDecodedTick()
        {
            return GetCurrentTracker()?.firstTrustMissionLeadDecodedTick ?? 0;
        }

        public static bool IsFirstTrustMissionWorldSiteRevealed()
        {
            return GetCurrentTracker()?.firstTrustMissionWorldSiteRevealed ?? false;
        }

        public static int GetFirstTrustMissionWorldSiteRevealedTick()
        {
            return GetCurrentTracker()?.firstTrustMissionWorldSiteRevealedTick ?? 0;
        }

        public static bool IsFirstTrustMissionWorldSiteReconnoitered()
        {
            return GetCurrentTracker()?.firstTrustMissionWorldSiteReconnoitered ?? false;
        }

        public static int GetFirstTrustMissionWorldSiteReconnoiteredTick()
        {
            return GetCurrentTracker()?.firstTrustMissionWorldSiteReconnoiteredTick ?? 0;
        }

        public static int GetRemainingFirstTrustMissionWorldSiteRevealTicks()
        {
            GameComponent_TokraTrustTracker tracker = GetCurrentTracker();

            if (tracker == null
                || tracker.firstTrustMissionWorldSiteRevealed
                || !tracker.firstTrustMissionLeadDecoded)
            {
                return 0;
            }

            tracker.EnsureFirstTrustMissionWorldSiteRevealScheduled();

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            return Math.Max(
                0,
                tracker.firstTrustMissionWorldSiteRevealTick - currentTick);
        }

        public static int GetRemainingFirstTrustMissionLeadDecodeTicks()
        {
            GameComponent_TokraTrustTracker tracker = GetCurrentTracker();

            if (tracker == null
                || tracker.firstTrustMissionLeadDecoded
                || !tracker.firstTrustMissionIntelAnalyzed)
            {
                return 0;
            }

            tracker.EnsureFirstTrustMissionLeadDecodeScheduled();

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            return Math.Max(
                0,
                tracker.firstTrustMissionLeadDecodeContactTick - currentTick);
        }

        public static int GetRemainingFirstTrustMissionCacheDeliveryTicks()
        {
            GameComponent_TokraTrustTracker tracker = GetCurrentTracker();

            if (tracker == null || tracker.firstTrustMissionCacheDelivered)
            {
                return 0;
            }

            tracker.EnsureFirstTrustMissionCacheDeliveryScheduled();

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            return Math.Max(
                0,
                tracker.firstTrustMissionCacheDeliveryTick - currentTick);
        }

        public static bool DebugMarkFirstTrustMissionBriefingReceived()
        {
            GameComponent_TokraTrustTracker tracker = GetCurrentTracker();

            if (tracker == null)
            {
                return false;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            tracker.firstTrustMissionHookPrepared = true;
            tracker.firstTrustMissionBriefingReceived = true;

            if (tracker.firstTrustMissionHookPreparedTick <= 0)
            {
                tracker.firstTrustMissionHookPreparedTick = currentTick;
            }

            if (tracker.firstTrustMissionBriefingContactTick <= 0)
            {
                tracker.firstTrustMissionBriefingContactTick = currentTick;
            }

            if (!tracker.firstTrustMissionCacheDelivered
                && tracker.firstTrustMissionCacheDeliveryTick <= 0)
            {
                tracker.ScheduleFirstTrustMissionCacheDelivery();
            }

            return true;
        }

        public static bool DebugDeliverFirstTrustMissionCache(Map map)
        {
            GameComponent_TokraTrustTracker tracker = GetCurrentTracker();

            if (tracker == null)
            {
                return false;
            }

            tracker.firstTrustMissionHookPrepared = true;
            tracker.firstTrustMissionBriefingReceived = true;
            return tracker.TryDeliverFirstTrustMissionCache(map);
        }

        public static bool DebugResetFirstTrustMissionCacheDelivery()
        {
            GameComponent_TokraTrustTracker tracker = GetCurrentTracker();

            if (tracker == null)
            {
                return false;
            }

            tracker.firstTrustMissionCacheDelivered = false;
            tracker.firstTrustMissionCacheDeliveryTick = 0;
            tracker.nextFirstTrustMissionCacheCheckTick = 0;
            tracker.firstTrustMissionIntelAnalyzed = false;
            tracker.firstTrustMissionIntelAnalyzedTick = 0;
            tracker.firstTrustMissionLeadDecoded = false;
            tracker.firstTrustMissionLeadDecodedTick = 0;
            tracker.firstTrustMissionLeadDecodeContactTick = 0;
            tracker.nextFirstTrustMissionLeadDecodeCheckTick = 0;
            tracker.firstTrustMissionWorldSiteRevealed = false;
            tracker.firstTrustMissionWorldSiteRevealedTick = 0;
            tracker.firstTrustMissionWorldSiteRevealTick = 0;
            tracker.nextFirstTrustMissionWorldSiteRevealCheckTick = 0;
            tracker.firstTrustMissionWorldSiteReconnoitered = false;
            tracker.firstTrustMissionWorldSiteReconnoiteredTick = 0;
            tracker.EnsureFirstTrustMissionCacheDeliveryScheduled();
            return true;
        }

        public static bool DebugMarkFirstTrustMissionLeadDecoded()
        {
            GameComponent_TokraTrustTracker tracker = GetCurrentTracker();

            if (tracker == null)
            {
                return false;
            }

            tracker.firstTrustMissionHookPrepared = true;
            tracker.firstTrustMissionBriefingReceived = true;
            tracker.firstTrustMissionCacheDelivered = true;
            tracker.firstTrustMissionIntelAnalyzed = true;
            return tracker.MarkFirstTrustMissionLeadDecoded(
                "debug action",
                sendLetter: true,
                sendMessage: true);
        }

        public static bool DebugRevealFirstTrustMissionWorldSite(Map map)
        {
            GameComponent_TokraTrustTracker tracker = GetCurrentTracker();

            if (tracker == null)
            {
                return false;
            }

            tracker.firstTrustMissionHookPrepared = true;
            tracker.firstTrustMissionBriefingReceived = true;
            tracker.firstTrustMissionCacheDelivered = true;
            tracker.firstTrustMissionIntelAnalyzed = true;
            tracker.MarkFirstTrustMissionLeadDecoded(
                "debug world-site reveal action",
                sendLetter: false,
                sendMessage: false);
            return tracker.TryRevealFirstTrustMissionWorldSite(map);
        }

        public static bool DebugReconnoiterFirstTrustMissionWorldSite()
        {
            GameComponent_TokraTrustTracker tracker = GetCurrentTracker();

            if (tracker == null)
            {
                return false;
            }

            tracker.firstTrustMissionHookPrepared = true;
            tracker.firstTrustMissionBriefingReceived = true;
            tracker.firstTrustMissionCacheDelivered = true;
            tracker.firstTrustMissionIntelAnalyzed = true;
            tracker.firstTrustMissionLeadDecoded = true;
            tracker.MarkFirstTrustMissionWorldSiteRevealed(
                "debug world-site reconnaissance action");
            return tracker.MarkFirstTrustMissionWorldSiteReconnoitered(
                "debug action");
        }

        public static bool NotifyFirstTrustMissionWorldSiteReconnoitered()
        {
            GameComponent_TokraTrustTracker tracker = GetCurrentTracker();

            if (tracker == null)
            {
                GR_Log.Error(
                    "Cannot mark Tok'ra mission site as reconnoitered: "
                    + "the trust tracker is unavailable.");
                return false;
            }

            return tracker.MarkFirstTrustMissionWorldSiteReconnoitered(
                "caravan reconnaissance");
        }

        public static bool NotifyFirstTrustMissionIntelAnalyzed(Pawn analyzer)
        {
            GameComponent_TokraTrustTracker tracker = GetCurrentTracker();

            if (tracker == null)
            {
                GR_Log.Error(
                    "Cannot mark Tok'ra mission intelligence as analyzed: "
                    + "the trust tracker is unavailable.");
                return false;
            }

            if (tracker.firstTrustMissionIntelAnalyzed)
            {
                return false;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;

            tracker.firstTrustMissionHookPrepared = true;
            tracker.firstTrustMissionBriefingReceived = true;
            tracker.firstTrustMissionCacheDelivered = true;

            if (tracker.firstTrustMissionHookPreparedTick <= 0)
            {
                tracker.firstTrustMissionHookPreparedTick = currentTick;
            }

            if (tracker.firstTrustMissionBriefingContactTick <= 0)
            {
                tracker.firstTrustMissionBriefingContactTick = currentTick;
            }

            if (tracker.firstTrustMissionCacheDeliveryTick <= 0
                || tracker.firstTrustMissionCacheDeliveryTick > currentTick)
            {
                tracker.firstTrustMissionCacheDeliveryTick = currentTick;
            }

            tracker.nextFirstTrustMissionCacheCheckTick = 0;
            tracker.firstTrustMissionIntelAnalyzed = true;
            tracker.firstTrustMissionIntelAnalyzedTick = currentTick;
            tracker.EnsureFirstTrustMissionLeadDecodeScheduled();

            GR_Log.Message(
                "Tok'ra first mission encoded intelligence analyzed by "
                + $"{analyzer?.LabelShortCap ?? "unknown"}.");

            return true;
        }

        public static bool NotifyFirstTrustMissionHookPrepared()
        {
            GameComponent_TokraTrustTracker tracker = GetCurrentTracker();

            if (tracker == null)
            {
                GR_Log.Error(
                    "Cannot prepare Tok'ra trust mission hook: the trust "
                    + "tracker is unavailable.");
                return false;
            }

            if (!tracker.firstTrustMissionHookPrepared)
            {
                tracker.firstTrustMissionHookPrepared = true;
                tracker.firstTrustMissionHookPreparedTick =
                    Find.TickManager?.TicksGame ?? 0;
                tracker.ScheduleFirstTrustMissionBriefingContact();
            }
            else if (!tracker.firstTrustMissionBriefingReceived
                && tracker.firstTrustMissionBriefingContactTick <= 0)
            {
                tracker.ScheduleFirstTrustMissionBriefingContact();
            }

            tracker.EnsureFirstTrustMissionCacheDeliveryScheduled();

            return true;
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

        private void TrySendPendingFirstTrustMissionBriefingContact()
        {
            if (!firstTrustMissionHookPrepared
                || firstTrustMissionBriefingReceived
                || Find.TickManager == null)
            {
                return;
            }

            int currentTick = Find.TickManager.TicksGame;

            if (firstTrustMissionBriefingContactTick <= 0)
            {
                ScheduleFirstTrustMissionBriefingContact();
                return;
            }

            if (currentTick < nextFirstTrustMissionBriefingCheckTick)
            {
                return;
            }

            nextFirstTrustMissionBriefingCheckTick = currentTick
                + FirstTrustMissionBriefingCheckIntervalTicks;

            if (currentTick < firstTrustMissionBriefingContactTick)
            {
                return;
            }

            firstTrustMissionBriefingReceived = true;
            ScheduleFirstTrustMissionCacheDelivery();

            Find.LetterStack.ReceiveLetter(
                "GR_TokraFirstMissionBriefing_LetterLabel".Translate(),
                "GR_TokraFirstMissionBriefing_LetterText".Translate(),
                LetterDefOf.NeutralEvent);

            Messages.Message(
                "GR_TokraFirstMissionBriefing_Received".Translate(),
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            GR_Log.Message(
                "Tok'ra first mission briefing contact sent after prepared "
                + "trusted mission hook.");
        }

        private void ScheduleFirstTrustMissionBriefingContact()
        {
            if (Find.TickManager == null)
            {
                return;
            }

            firstTrustMissionBriefingContactTick = Find.TickManager.TicksGame
                + Rand.RangeInclusive(
                    FirstTrustMissionBriefingMinimumDelayTicks,
                    FirstTrustMissionBriefingMaximumDelayTicks);
            nextFirstTrustMissionBriefingCheckTick = Find.TickManager.TicksGame
                + FirstTrustMissionBriefingCheckIntervalTicks;

            GR_Log.Message(
                "Scheduled Tok'ra first mission briefing contact in "
                + $"{firstTrustMissionBriefingContactTick - Find.TickManager.TicksGame} "
                + "tick(s).");
        }

        private void EnsureFirstTrustMissionCacheDeliveryScheduled()
        {
            if (!firstTrustMissionBriefingReceived
                || firstTrustMissionCacheDelivered
                || Find.TickManager == null
                || firstTrustMissionCacheDeliveryTick > 0)
            {
                return;
            }

            ScheduleFirstTrustMissionCacheDelivery();
        }

        private void TrySendPendingFirstTrustMissionDecodedLead()
        {
            if (!firstTrustMissionIntelAnalyzed
                || firstTrustMissionLeadDecoded
                || Find.TickManager == null)
            {
                return;
            }

            EnsureFirstTrustMissionLeadDecodeScheduled();

            int currentTick = Find.TickManager.TicksGame;

            if (firstTrustMissionLeadDecodeContactTick <= 0)
            {
                return;
            }

            if (currentTick < nextFirstTrustMissionLeadDecodeCheckTick)
            {
                return;
            }

            nextFirstTrustMissionLeadDecodeCheckTick = currentTick
                + FirstTrustMissionDecodedLeadCheckIntervalTicks;

            if (currentTick < firstTrustMissionLeadDecodeContactTick)
            {
                return;
            }

            MarkFirstTrustMissionLeadDecoded(
                "delayed Tok'ra decoded mission lead",
                sendLetter: true,
                sendMessage: true);
        }

        private void TryRevealPendingFirstTrustMissionWorldSite()
        {
            if (!firstTrustMissionLeadDecoded
                || firstTrustMissionWorldSiteRevealed
                || Find.TickManager == null)
            {
                return;
            }

            EnsureFirstTrustMissionWorldSiteRevealScheduled();

            int currentTick = Find.TickManager.TicksGame;

            if (firstTrustMissionWorldSiteRevealTick <= 0)
            {
                return;
            }

            if (currentTick < nextFirstTrustMissionWorldSiteRevealCheckTick)
            {
                return;
            }

            nextFirstTrustMissionWorldSiteRevealCheckTick = currentTick
                + FirstTrustMissionWorldSiteCheckIntervalTicks;

            if (currentTick < firstTrustMissionWorldSiteRevealTick)
            {
                return;
            }

            if (!TryRevealFirstTrustMissionWorldSite(null))
            {
                RescheduleFirstTrustMissionWorldSiteRetry();
            }
        }

        private void EnsureFirstTrustMissionLeadDecodeScheduled()
        {
            if (!firstTrustMissionIntelAnalyzed
                || firstTrustMissionLeadDecoded
                || Find.TickManager == null
                || firstTrustMissionLeadDecodeContactTick > 0)
            {
                return;
            }

            ScheduleFirstTrustMissionLeadDecode();
        }

        private void ScheduleFirstTrustMissionLeadDecode()
        {
            if (Find.TickManager == null || firstTrustMissionLeadDecoded)
            {
                return;
            }

            firstTrustMissionLeadDecodeContactTick = Find.TickManager.TicksGame
                + Rand.RangeInclusive(
                    FirstTrustMissionDecodedLeadMinimumDelayTicks,
                    FirstTrustMissionDecodedLeadMaximumDelayTicks);
            nextFirstTrustMissionLeadDecodeCheckTick = Find.TickManager.TicksGame
                + FirstTrustMissionDecodedLeadCheckIntervalTicks;

            GR_Log.Message(
                "Scheduled Tok'ra decoded mission lead contact in "
                + $"{firstTrustMissionLeadDecodeContactTick - Find.TickManager.TicksGame} "
                + "tick(s).");
        }

        private bool MarkFirstTrustMissionLeadDecoded(
            string reasonLabel,
            bool sendLetter,
            bool sendMessage)
        {
            if (firstTrustMissionLeadDecoded)
            {
                return true;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;

            firstTrustMissionHookPrepared = true;
            firstTrustMissionBriefingReceived = true;
            firstTrustMissionCacheDelivered = true;
            firstTrustMissionIntelAnalyzed = true;

            if (firstTrustMissionHookPreparedTick <= 0)
            {
                firstTrustMissionHookPreparedTick = currentTick;
            }

            if (firstTrustMissionBriefingContactTick <= 0)
            {
                firstTrustMissionBriefingContactTick = currentTick;
            }

            if (firstTrustMissionCacheDeliveryTick <= 0
                || firstTrustMissionCacheDeliveryTick > currentTick)
            {
                firstTrustMissionCacheDeliveryTick = currentTick;
            }

            if (firstTrustMissionIntelAnalyzedTick <= 0
                || firstTrustMissionIntelAnalyzedTick > currentTick)
            {
                firstTrustMissionIntelAnalyzedTick = currentTick;
            }

            firstTrustMissionLeadDecoded = true;
            firstTrustMissionLeadDecodedTick = currentTick;
            firstTrustMissionLeadDecodeContactTick = currentTick;
            nextFirstTrustMissionLeadDecodeCheckTick = 0;
            EnsureFirstTrustMissionWorldSiteRevealScheduled();

            if (sendLetter)
            {
                Find.LetterStack.ReceiveLetter(
                    "GR_TokraDecodedMissionLead_LetterLabel".Translate(),
                    "GR_TokraDecodedMissionLead_LetterText".Translate(),
                    LetterDefOf.NeutralEvent);
            }

            if (sendMessage)
            {
                Messages.Message(
                    "GR_TokraDecodedMissionLead_Decoded".Translate(),
                    MessageTypeDefOf.NeutralEvent,
                    historical: true);
            }

            GR_Log.Message(
                "Tok'ra decoded first mission lead recorded after "
                + reasonLabel
                + ".");

            return true;
        }

        private void EnsureFirstTrustMissionWorldSiteRevealScheduled()
        {
            if (!firstTrustMissionLeadDecoded
                || firstTrustMissionWorldSiteRevealed
                || Find.TickManager == null
                || firstTrustMissionWorldSiteRevealTick > 0)
            {
                return;
            }

            ScheduleFirstTrustMissionWorldSiteReveal();
        }

        private void ScheduleFirstTrustMissionWorldSiteReveal()
        {
            if (Find.TickManager == null
                || firstTrustMissionWorldSiteRevealed)
            {
                return;
            }

            firstTrustMissionWorldSiteRevealTick = Find.TickManager.TicksGame
                + Rand.RangeInclusive(
                    FirstTrustMissionWorldSiteMinimumDelayTicks,
                    FirstTrustMissionWorldSiteMaximumDelayTicks);
            nextFirstTrustMissionWorldSiteRevealCheckTick = Find.TickManager.TicksGame
                + FirstTrustMissionWorldSiteCheckIntervalTicks;

            GR_Log.Message(
                "Scheduled Tok'ra decoded mission world-site reveal in "
                + $"{firstTrustMissionWorldSiteRevealTick - Find.TickManager.TicksGame} "
                + "tick(s).");
        }

        private bool TryRevealFirstTrustMissionWorldSite(Map preferredMap)
        {
            WorldObjectDef siteDef = DefDatabase<WorldObjectDef>
                .GetNamedSilentFail(FirstTrustMissionWorldSiteDefName);

            if (siteDef == null)
            {
                GR_Log.Error(
                    "Cannot reveal Tok'ra decoded mission world site: the "
                    + "world-object def is missing.");
                return false;
            }

            if (HasActiveFirstTrustMissionWorldSite(siteDef))
            {
                MarkFirstTrustMissionWorldSiteRevealed(
                    "existing active Tok'ra decoded mission world site");
                return true;
            }

            Map map = GetMissionCacheDeliveryMap(preferredMap);

            if (map == null)
            {
                GR_Log.Message(
                    "Tok'ra decoded mission world site reveal delayed: no "
                    + "player home map is currently available.");
                return false;
            }

            Faction tokraFaction = TokraFactionUtility.GetOrCreatePersistentFaction(
                "Tok'ra decoded mission world site");

            if (tokraFaction == null)
            {
                GR_Log.Error(
                    "Cannot reveal Tok'ra decoded mission world site: the "
                    + "persistent hidden Tok'ra world faction could not be "
                    + "resolved.");
                return false;
            }

            PlanetTile siteTile;

            if (!TryFindFirstTrustMissionWorldSiteTile(map.Tile, out siteTile))
            {
                GR_Log.Message(
                    "Tok'ra decoded mission world site reveal delayed: no "
                    + "valid nearby world tile was found.");
                return false;
            }

            WorldObject site = WorldObjectMaker.MakeWorldObject(siteDef);
            site.Tile = siteTile;
            site.SetFaction(tokraFaction);

            Find.WorldObjects.Add(site);
            MarkFirstTrustMissionWorldSiteRevealed(
                "decoded Tok'ra mission lead");

            Find.LetterStack.ReceiveLetter(
                "GR_TokraDecodedMissionWorldSite_LetterLabel".Translate(),
                "GR_TokraDecodedMissionWorldSite_LetterText".Translate(),
                LetterDefOf.NeutralEvent,
                site);

            Messages.Message(
                "GR_TokraDecodedMissionWorldSite_Revealed".Translate(),
                site,
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            GR_Log.Message(
                "Tok'ra decoded mission world site revealed at tile "
                + $"{siteTile} using {tokraFaction.Name} "
                + $"({tokraFaction.loadID}).");

            return true;
        }

        private void MarkFirstTrustMissionWorldSiteRevealed(
            string reasonLabel)
        {
            if (firstTrustMissionWorldSiteRevealed)
            {
                return;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;

            firstTrustMissionHookPrepared = true;
            firstTrustMissionBriefingReceived = true;
            firstTrustMissionCacheDelivered = true;
            firstTrustMissionIntelAnalyzed = true;
            firstTrustMissionLeadDecoded = true;

            if (firstTrustMissionLeadDecodedTick <= 0
                || firstTrustMissionLeadDecodedTick > currentTick)
            {
                firstTrustMissionLeadDecodedTick = currentTick;
            }

            firstTrustMissionWorldSiteRevealed = true;
            firstTrustMissionWorldSiteRevealedTick = currentTick;
            firstTrustMissionWorldSiteRevealTick = currentTick;
            nextFirstTrustMissionWorldSiteRevealCheckTick = 0;

            GR_Log.Message(
                "Tok'ra first mission world-site reveal recorded after "
                + reasonLabel
                + ".");
        }

        private bool MarkFirstTrustMissionWorldSiteReconnoitered(
            string reasonLabel)
        {
            if (firstTrustMissionWorldSiteReconnoitered)
            {
                return false;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;

            firstTrustMissionHookPrepared = true;
            firstTrustMissionBriefingReceived = true;
            firstTrustMissionCacheDelivered = true;
            firstTrustMissionIntelAnalyzed = true;
            firstTrustMissionLeadDecoded = true;
            firstTrustMissionWorldSiteRevealed = true;

            if (firstTrustMissionLeadDecodedTick <= 0
                || firstTrustMissionLeadDecodedTick > currentTick)
            {
                firstTrustMissionLeadDecodedTick = currentTick;
            }

            if (firstTrustMissionWorldSiteRevealedTick <= 0
                || firstTrustMissionWorldSiteRevealedTick > currentTick)
            {
                firstTrustMissionWorldSiteRevealedTick = currentTick;
            }

            firstTrustMissionWorldSiteReconnoitered = true;
            firstTrustMissionWorldSiteReconnoiteredTick = currentTick;

            GR_Log.Message(
                "Tok'ra first mission world site reconnoitered after "
                + reasonLabel
                + ".");

            return true;
        }

        private static bool HasActiveFirstTrustMissionWorldSite(
            WorldObjectDef siteDef)
        {
            if (siteDef == null || Find.WorldObjects == null)
            {
                return false;
            }

            foreach (WorldObject worldObject in Find.WorldObjects.AllWorldObjects)
            {
                if (worldObject?.def == siteDef
                    && !worldObject.Destroyed)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool TryFindFirstTrustMissionWorldSiteTile(
            PlanetTile originTile,
            out PlanetTile tile)
        {
            return TileFinder.TryFindNewSiteTile(
                out tile,
                originTile,
                minDist: FirstTrustMissionWorldSiteMinimumDistance,
                maxDist: FirstTrustMissionWorldSiteMaximumDistance,
                allowCaravans: false,
                selectLandmarkChance: 0f,
                layer: originTile.Layer);
        }

        private void RescheduleFirstTrustMissionWorldSiteRetry()
        {
            if (Find.TickManager == null
                || firstTrustMissionWorldSiteRevealed)
            {
                return;
            }

            firstTrustMissionWorldSiteRevealTick = Find.TickManager.TicksGame
                + FirstTrustMissionWorldSiteRetryDelayTicks;
            nextFirstTrustMissionWorldSiteRevealCheckTick = Find.TickManager.TicksGame
                + FirstTrustMissionWorldSiteCheckIntervalTicks;
        }

        private void TryDeliverPendingFirstTrustMissionCache()
        {
            if (!firstTrustMissionBriefingReceived
                || firstTrustMissionCacheDelivered
                || Find.TickManager == null)
            {
                return;
            }

            EnsureFirstTrustMissionCacheDeliveryScheduled();

            int currentTick = Find.TickManager.TicksGame;

            if (firstTrustMissionCacheDeliveryTick <= 0)
            {
                return;
            }

            if (currentTick < nextFirstTrustMissionCacheCheckTick)
            {
                return;
            }

            nextFirstTrustMissionCacheCheckTick = currentTick
                + FirstTrustMissionCacheCheckIntervalTicks;

            if (currentTick < firstTrustMissionCacheDeliveryTick)
            {
                return;
            }

            if (!TryDeliverFirstTrustMissionCache(null))
            {
                RescheduleFirstTrustMissionCacheRetry();
            }
        }

        private bool TryDeliverFirstTrustMissionCache(Map preferredMap)
        {
            ThingDef cacheThingDef = DefDatabase<ThingDef>.GetNamedSilentFail(
                FirstTrustMissionCacheThingDefName);

            if (cacheThingDef == null)
            {
                GR_Log.Error(
                    "Cannot deliver Tok'ra mission cache: encoded "
                    + "intelligence packet ThingDef is missing.");
                return false;
            }

            Map map = GetMissionCacheDeliveryMap(preferredMap);

            if (map == null)
            {
                GR_Log.Message(
                    "Tok'ra mission cache delivery delayed: no player home "
                    + "map is currently available.");
                return false;
            }

            Thing cacheThing = ThingMaker.MakeThing(cacheThingDef);
            cacheThing.stackCount = 1;
            Thing placedThing;

            if (!TokraDeliveryDropUtility.TryPlaceThingNearPreferredDeliveryCell(
                    cacheThing,
                    map,
                    null,
                    out placedThing))
            {
                GR_Log.Message(
                    "Tok'ra mission cache delivery delayed: no valid "
                    + $"delivery cell found on map {map.uniqueID}.");
                return false;
            }

            firstTrustMissionCacheDelivered = true;
            firstTrustMissionCacheDeliveryTick = Find.TickManager?.TicksGame ?? 0;
            nextFirstTrustMissionCacheCheckTick = 0;

            Find.LetterStack.ReceiveLetter(
                "GR_TokraFirstMissionCache_LetterLabel".Translate(),
                "GR_TokraFirstMissionCache_LetterText".Translate(),
                LetterDefOf.NeutralEvent);

            Messages.Message(
                "GR_TokraFirstMissionCache_Delivered".Translate(),
                placedThing,
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            GR_Log.Message(
                "Tok'ra first mission cache delivered at "
                + $"{placedThing.Position} on map {map.uniqueID}.");

            return true;
        }

        private static Map GetMissionCacheDeliveryMap(Map preferredMap)
        {
            if (preferredMap != null && preferredMap.IsPlayerHome)
            {
                return preferredMap;
            }

            if (Find.Maps == null)
            {
                return null;
            }

            for (int index = 0; index < Find.Maps.Count; index++)
            {
                Map map = Find.Maps[index];

                if (map != null && map.IsPlayerHome)
                {
                    return map;
                }
            }

            return null;
        }

        private void ScheduleFirstTrustMissionCacheDelivery()
        {
            if (Find.TickManager == null || firstTrustMissionCacheDelivered)
            {
                return;
            }

            firstTrustMissionCacheDeliveryTick = Find.TickManager.TicksGame
                + Rand.RangeInclusive(
                    FirstTrustMissionCacheMinimumDelayTicks,
                    FirstTrustMissionCacheMaximumDelayTicks);
            nextFirstTrustMissionCacheCheckTick = Find.TickManager.TicksGame
                + FirstTrustMissionCacheCheckIntervalTicks;

            GR_Log.Message(
                "Scheduled Tok'ra first mission cache delivery in "
                + $"{firstTrustMissionCacheDeliveryTick - Find.TickManager.TicksGame} "
                + "tick(s).");
        }

        private void RescheduleFirstTrustMissionCacheRetry()
        {
            if (Find.TickManager == null || firstTrustMissionCacheDelivered)
            {
                return;
            }

            firstTrustMissionCacheDeliveryTick = Find.TickManager.TicksGame
                + FirstTrustMissionCacheRetryDelayTicks;
            nextFirstTrustMissionCacheCheckTick = Find.TickManager.TicksGame
                + FirstTrustMissionCacheCheckIntervalTicks;
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
