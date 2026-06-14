using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Consumes one stored Tok'ra safehouse lead and creates a temporary,
    /// non-hostile vanilla site with a small medical stash.
    /// </summary>
    public class IncidentWorker_TokraHiddenSafehouseSite : IncidentWorker
    {
        private const int MinimumSiteDistance = 5;
        private const int MaximumSiteDistance = 12;
        private const int SiteDurationTicks = 600000;
        private const int TretoninDoseCount = 2;
        private const int MedicineCount = 4;

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            if (!base.CanFireNowSub(parms))
            {
                return false;
            }

            Map map = parms.target as Map;

            if (map == null
                || GR_DefOf.SG1_Tokra == null
                || GR_DefOf.SG1_TokraHiddenSafehouseSite == null
                || GR_DefOf.SG1_TokraHiddenSafehouseSitePart == null
                || GR_DefOf.SG1_TretoninDose == null
                || ResolveMedicineDef() == null
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
                    "Cannot create the Tok'ra hidden safehouse site: the "
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
                    "Cannot create the Tok'ra hidden safehouse site: the "
                    + $"{GetTierLogLabel(trustTier)} trust tier ({trustScore}) "
                    + "is too wary for a safehouse visit.");
                return false;
            }

            int leadCountBefore
                = GameComponent_TokraSafehouseLeadTracker.GetCurrentLeadCount();

            if (leadCountBefore <= 0)
            {
                GR_Log.Message(
                    "Cannot create the Tok'ra hidden safehouse site: no "
                    + "safehouse lead is stored.");
                return false;
            }

            if (TokraSafehouseWorldUtility.HasActiveSafehouseWorldObject())
            {
                GR_Log.Message(
                    "Cannot create the Tok'ra hidden safehouse site: an "
                    + "active safehouse marker or site already exists.");
                return false;
            }

            Faction tokraFaction = TokraFactionUtility.GetOrCreatePersistentFaction(
                "Tok'ra hidden safehouse site");

            if (tokraFaction == null)
            {
                GR_Log.Error(
                    "Cannot create the Tok'ra hidden safehouse site: the "
                    + "persistent hidden Tok'ra world faction could not be "
                    + "resolved.");
                return false;
            }

            PlanetTile safehouseTile;

            if (!TryFindSafehouseTile(map.Tile, out safehouseTile))
            {
                GR_Log.Message(
                    "Cannot create the Tok'ra hidden safehouse site: no "
                    + "valid nearby world tile was found.");
                return false;
            }

            Site site = SiteMaker.MakeSite(
                GR_DefOf.SG1_TokraHiddenSafehouseSitePart,
                safehouseTile,
                tokraFaction,
                false,
                0f,
                GR_DefOf.SG1_TokraHiddenSafehouseSite);

            if (site == null || !TryPrepareSite(site))
            {
                GR_Log.Warning(
                    "Cannot create the Tok'ra hidden safehouse site: the "
                    + "vanilla site or its medical stash could not be "
                    + "prepared.");
                return false;
            }

            if (!GameComponent_TokraSafehouseLeadTracker
                    .TryConsumeSafehouseLead("hidden safehouse site"))
            {
                DestroyPreparedContents(site);
                return false;
            }

            Find.WorldObjects.Add(site);

            int leadCountAfter
                = GameComponent_TokraSafehouseLeadTracker.GetCurrentLeadCount();

            GR_Log.Message(
                $"Created enterable Tok'ra hidden safehouse site at tile "
                + $"{safehouseTile} using {tokraFaction.Name} "
                + $"({tokraFaction.loadID}); prepared {TretoninDoseCount} "
                + $"tretonin dose(s) and {MedicineCount} industrial medicine "
                + $"unit(s); leads {leadCountBefore} -> {leadCountAfter}.");

            SendStandardLetter(
                parms,
                site,
                TretoninDoseCount.ToString().Named("TRETONINCOUNT"),
                MedicineCount.ToString().Named("MEDICINECOUNT"),
                leadCountAfter.ToString().Named("LEADCOUNT"),
                GameComponent_TokraSafehouseLeadTracker
                    .MaximumSafehouseLeads
                    .ToString()
                    .Named("LEADMAX"));

            return true;
        }

        private static bool TryPrepareSite(Site site)
        {
            ItemStashContentsComp stash
                = site.GetComponent<ItemStashContentsComp>();
            TimeoutComp timeout = site.GetComponent<TimeoutComp>();

            if (stash == null || timeout == null)
            {
                return false;
            }

            ThingOwner contents = stash.GetDirectlyHeldThings();
            Thing tretonin = MakeStack(
                GR_DefOf.SG1_TretoninDose,
                TretoninDoseCount);
            Thing medicine = MakeStack(
                ResolveMedicineDef(),
                MedicineCount);

            if (contents == null
                || tretonin == null
                || medicine == null
                || !contents.TryAdd(tretonin)
                || !contents.TryAdd(medicine))
            {
                if (tretonin != null && !tretonin.Destroyed)
                {
                    tretonin.Destroy(DestroyMode.Vanish);
                }

                if (medicine != null && !medicine.Destroyed)
                {
                    medicine.Destroy(DestroyMode.Vanish);
                }

                contents?.ClearAndDestroyContents(DestroyMode.Vanish);
                return false;
            }

            timeout.StartTimeout(SiteDurationTicks);
            return true;
        }

        private static void DestroyPreparedContents(Site site)
        {
            site?.GetComponent<ItemStashContentsComp>()
                ?.GetDirectlyHeldThings()
                ?.ClearAndDestroyContents(DestroyMode.Vanish);
        }

        private static Thing MakeStack(ThingDef thingDef, int count)
        {
            if (thingDef == null)
            {
                return null;
            }

            Thing thing = ThingMaker.MakeThing(thingDef);
            thing.stackCount = count;
            return thing;
        }

        private static ThingDef ResolveMedicineDef()
        {
            return DefDatabase<ThingDef>.GetNamedSilentFail(
                "MedicineIndustrial");
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
