using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Consumes one stored Tok'ra safehouse lead and creates a temporary
    /// non-hostile world marker.
    ///
    /// This milestone intentionally avoids generated maps and site rewards.
    /// It validates the world-object footprint before a later true safehouse
    /// site prototype.
    /// </summary>
    public class IncidentWorker_TokraHiddenSafehouseWorldMarker : IncidentWorker
    {
        private const int MinimumSiteDistance = 5;
        private const int MaximumSiteDistance = 12;

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            if (!TokraFactionUtility.HasPersistentFaction())
            {
                return false;
            }

            if (!base.CanFireNowSub(parms))
            {
                return false;
            }

            Map map = parms.target as Map;

            if (map == null
                || GR_DefOf.SG1_Tokra == null
                || GR_DefOf.SG1_TokraHiddenSafehouseMarker == null
                || GameComponent_TokraSafehouseLeadTracker
                    .GetCurrentLeadCount() <= 0
                || !IsCurrentTierEligible()
                || TokraSafehouseWorldUtility
                    .HasActiveSafehouseWorldObject())
            {
                return false;
            }

            PlanetTile unusedTile;
            return TryFindSafehouseTile(map.Tile, out unusedTile);
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = parms.target as Map;

            if (map == null)
            {
                GR_Log.Warning(
                    "Cannot create the Tok'ra hidden safehouse marker: the "
                    + "incident target is not a map.");
                return false;
            }

            TokraTrustTier trustTier
                = GameComponent_TokraTrustTracker.GetCurrentTier();
            int trustScore
                = GameComponent_TokraTrustTracker.GetCurrentTrustScore();

            if (!IsEligibleTier(trustTier))
            {
                GR_Log.Message(
                    "Cannot create the Tok'ra hidden safehouse marker: the "
                    + $"{GetTierLogLabel(trustTier)} trust tier ({trustScore}) "
                    + "is too wary for safehouse coordinates.");
                return false;
            }

            int leadCountBefore
                = GameComponent_TokraSafehouseLeadTracker.GetCurrentLeadCount();

            if (leadCountBefore <= 0)
            {
                GR_Log.Message(
                    "Cannot create the Tok'ra hidden safehouse marker: no "
                    + "safehouse lead is stored.");
                return false;
            }

            if (TokraSafehouseWorldUtility.HasActiveSafehouseWorldObject())
            {
                GR_Log.Message(
                    "Cannot create the Tok'ra hidden safehouse marker: an "
                    + "active safehouse marker or site already exists.");
                return false;
            }

            Faction tokraFaction = TokraFactionUtility.GetOrCreatePersistentFaction(
                "Tok'ra hidden safehouse world marker");

            if (tokraFaction == null)
            {
                GR_Log.Error(
                    "Cannot create the Tok'ra hidden safehouse marker: the "
                    + "persistent hidden Tok'ra world faction could not be "
                    + "resolved.");
                return false;
            }

            PlanetTile safehouseTile;

            if (!TryFindSafehouseTile(map.Tile, out safehouseTile))
            {
                GR_Log.Message(
                    "Cannot create the Tok'ra hidden safehouse marker: no "
                    + "valid nearby world tile was found.");
                return false;
            }

            if (!GameComponent_TokraSafehouseLeadTracker
                    .TryConsumeSafehouseLead("hidden safehouse world marker"))
            {
                return false;
            }

            WorldObject marker = WorldObjectMaker.MakeWorldObject(
                GR_DefOf.SG1_TokraHiddenSafehouseMarker);
            marker.Tile = safehouseTile;
            marker.SetFaction(tokraFaction);

            Find.WorldObjects.Add(marker);

            int leadCountAfter
                = GameComponent_TokraSafehouseLeadTracker.GetCurrentLeadCount();

            GR_Log.Message(
                $"Created Tok'ra hidden safehouse marker at tile "
                + $"{safehouseTile} using {tokraFaction.Name} "
                + $"({tokraFaction.loadID}); leads {leadCountBefore} -> "
                + $"{leadCountAfter}.");

            SendStandardLetter(
                parms,
                marker,
                leadCountAfter.ToString().Named("LEADCOUNT"),
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

        private static bool TryFindSafehouseTile(
            PlanetTile originTile,
            out PlanetTile tile)
        {
            return TileFinder.TryFindNewSiteTile(
                out tile,
                originTile,
                minDist: MinimumSiteDistance,
                maxDist: MaximumSiteDistance,
                allowCaravans: false,
                selectLandmarkChance: 0f,
                layer: originTile.Layer);
        }

    }
}
