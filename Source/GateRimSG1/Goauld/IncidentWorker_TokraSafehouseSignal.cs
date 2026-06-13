using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Rare, letter-only clandestine Tok'ra contact.
    ///
    /// This is a safe intermediate step before true hidden world sites. The
    /// Tok'ra transmit a short safehouse signal. The player gains a tiny
    /// amount of trust and stores one persistent safehouse lead for a future
    /// world-site milestone. No site, pawn, caravan, trader, loot or raid is
    /// created yet.
    /// </summary>
    public class IncidentWorker_TokraSafehouseSignal : IncidentWorker
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            if (!base.CanFireNowSub(parms))
            {
                return false;
            }

            Map map = parms.target as Map;

            return map != null
                && GR_DefOf.SG1_Tokra != null
                && IsCurrentTierEligible();
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = parms.target as Map;

            if (map == null)
            {
                GR_Log.Warning(
                    "Cannot start the Tok'ra safehouse signal: the incident "
                    + "target is not a map.");
                return false;
            }

            TokraTrustTier trustTier
                = GameComponent_TokraTrustTracker.GetCurrentTier();
            int trustScore
                = GameComponent_TokraTrustTracker.GetCurrentTrustScore();

            if (!IsEligibleTier(trustTier))
            {
                GR_Log.Message(
                    "Cannot start the Tok'ra safehouse signal: the current "
                    + $"{GetTierLogLabel(trustTier)} trust tier ({trustScore}) "
                    + "is too wary for clandestine coordinates.");
                return false;
            }

            Faction tokraFaction = TokraFactionUtility.GetOrCreatePersistentFaction(
                "hidden Tok'ra safehouse signal");

            if (tokraFaction == null)
            {
                GR_Log.Error(
                    "Cannot start the Tok'ra safehouse signal: the persistent "
                    + "hidden Tok'ra world faction could not be resolved.");
                return false;
            }

            Pawn letterTarget = null;

            if (map.mapPawns?.FreeColonistsSpawned != null
                && map.mapPawns.FreeColonistsSpawned.Count > 0)
            {
                letterTarget = map.mapPawns.FreeColonistsSpawned
                    .RandomElement();
            }

            int previousTrustScore
                = GameComponent_TokraTrustTracker.GetCurrentTrustScore();
            int previousLeadCount
                = GameComponent_TokraSafehouseLeadTracker.GetCurrentLeadCount();

            GameComponent_TokraTrustTracker
                .NotifyHiddenSafehouseSignalAcknowledged();
            GameComponent_TokraSafehouseLeadTracker
                .NotifySafehouseSignalAcknowledged();

            int currentTrustScore
                = GameComponent_TokraTrustTracker.GetCurrentTrustScore();
            int currentLeadCount
                = GameComponent_TokraSafehouseLeadTracker.GetCurrentLeadCount();
            int appliedTrustChange = currentTrustScore - previousTrustScore;
            int appliedLeadChange = currentLeadCount - previousLeadCount;

            string trustTierLabel = GetTierLogLabel(trustTier);
            string signedTrustChange = FormatSignedChange(appliedTrustChange);
            string signedLeadChange = FormatSignedChange(appliedLeadChange);

            GR_Log.Message(
                $"Started Tok'ra safehouse signal using {tokraFaction.Name} "
                + $"({tokraFaction.loadID}) at {trustTierLabel} trust tier "
                + $"({previousTrustScore}); trust change {signedTrustChange}; "
                + $"safehouse leads {previousLeadCount} -> "
                + $"{currentLeadCount} ({signedLeadChange}).");

            SendStandardLetter(
                parms,
                letterTarget,
                signedTrustChange.Named("TRUSTCHANGE"),
                currentTrustScore.ToString().Named("TRUSTSCORE"),
                trustTierLabel.Named("TIER"),
                signedLeadChange.Named("LEADCHANGE"),
                currentLeadCount.ToString().Named("LEADCOUNT"),
                GameComponent_TokraSafehouseLeadTracker
                    .MaximumSafehouseLeads
                    .ToString()
                    .Named("LEADMAX"));

            return true;
        }

        private static bool IsCurrentTierEligible()
        {
            return IsEligibleTier(
                GameComponent_TokraTrustTracker.GetCurrentTier());
        }

        private static bool IsEligibleTier(TokraTrustTier tier)
        {
            return tier == TokraTrustTier.Neutral
                || tier == TokraTrustTier.Cooperative
                || tier == TokraTrustTier.Trusted;
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

        private static string FormatSignedChange(int change)
        {
            return change > 0 ? $"+{change}" : change.ToString();
        }
    }
}
