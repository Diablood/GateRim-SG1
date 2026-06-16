using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Consumes one stored Tok'ra safehouse lead and resolves it as a small
    /// medical cache on the active colony map.
    ///
    /// This deliberately remains below the complexity of a true world site:
    /// no world object, map generation, visitors, traders, recruitment,
    /// military aid or raid is created.
    /// </summary>
    public class IncidentWorker_TokraSafehouseLeadCache : IncidentWorker
    {
        private const int TretoninDoseCount = 2;
        private const int MedicineCount = 3;

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            if (!base.CanFireNowSub(parms))
            {
                return false;
            }

            Map map = parms.target as Map;

            if (map == null
                || GR_DefOf.SG1_Tokra == null
                || GR_DefOf.SG1_TretoninDose == null
                || ResolveMedicineDef() == null
                || GameComponent_TokraSafehouseLeadTracker
                    .GetCurrentLeadCount() <= 0
                || !IsCurrentTierEligible())
            {
                return false;
            }

            IntVec3 unusedCell;
            return TryFindCacheCell(map, out unusedCell);
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = parms.target as Map;

            if (map == null)
            {
                GR_Log.Warning(
                    "Cannot resolve the Tok'ra safehouse lead cache: the "
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
                    "Cannot resolve the Tok'ra safehouse lead cache: the "
                    + $"{GetTierLogLabel(trustTier)} trust tier ({trustScore}) "
                    + "is too wary for a follow-up cache.");
                return false;
            }

            int leadCountBefore
                = GameComponent_TokraSafehouseLeadTracker.GetCurrentLeadCount();

            if (leadCountBefore <= 0)
            {
                GR_Log.Message(
                    "Cannot resolve the Tok'ra safehouse lead cache: no "
                    + "safehouse lead is stored.");
                return false;
            }

            Faction tokraFaction = TokraFactionUtility.GetOrCreatePersistentFaction(
                "Tok'ra safehouse lead cache");

            if (tokraFaction == null)
            {
                GR_Log.Error(
                    "Cannot resolve the Tok'ra safehouse lead cache: the "
                    + "persistent hidden Tok'ra world faction could not be "
                    + "resolved.");
                return false;
            }

            IntVec3 cacheCell;

            if (!TryFindCacheCell(map, out cacheCell))
            {
                GR_Log.Message(
                    "Cannot resolve the Tok'ra safehouse lead cache: no "
                    + "reachable unfogged edge cell was found.");
                return false;
            }

            Thing placedTretonin;
            Thing placedMedicine;

            if (!TrySpawnStack(
                    map,
                    cacheCell,
                    GR_DefOf.SG1_TretoninDose,
                    TretoninDoseCount,
                    "tretonin dose(s)",
                    out placedTretonin))
            {
                return false;
            }

            if (!TrySpawnStack(
                    map,
                    cacheCell,
                    ResolveMedicineDef(),
                    MedicineCount,
                    "industrial medicine unit(s)",
                    out placedMedicine))
            {
                DestroyIfSpawned(placedTretonin);
                return false;
            }

            if (!GameComponent_TokraSafehouseLeadTracker
                    .TryConsumeSafehouseLead("safehouse lead cache"))
            {
                DestroyIfSpawned(placedTretonin);
                DestroyIfSpawned(placedMedicine);
                return false;
            }

            int leadCountAfter
                = GameComponent_TokraSafehouseLeadTracker.GetCurrentLeadCount();

            GR_Log.Message(
                $"Resolved Tok'ra safehouse lead cache at {cacheCell} using "
                + $"{tokraFaction.Name} ({tokraFaction.loadID}); placed "
                + $"{TretoninDoseCount} tretonin dose(s) and "
                + $"{MedicineCount} industrial medicine unit(s); leads "
                + $"{leadCountBefore} -> {leadCountAfter}.");

            SendStandardLetter(
                parms,
                placedTretonin,
                TretoninDoseCount.ToString().Named("TRETONINCOUNT"),
                MedicineCount.ToString().Named("MEDICINECOUNT"),
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

        private static ThingDef ResolveMedicineDef()
        {
            return DefDatabase<ThingDef>.GetNamedSilentFail(
                "MedicineIndustrial");
        }

        private static bool TrySpawnStack(
            Map map,
            IntVec3 cacheCell,
            ThingDef thingDef,
            int count,
            string logLabel,
            out Thing placedThing)
        {
            placedThing = null;

            if (thingDef == null)
            {
                GR_Log.Warning(
                    "Cannot resolve the Tok'ra safehouse lead cache: a "
                    + "required ThingDef could not be resolved.");
                return false;
            }

            Thing thing = ThingMaker.MakeThing(thingDef);
            thing.stackCount = count;

            if (!GenPlace.TryPlaceThing(
                    thing,
                    cacheCell,
                    map,
                    ThingPlaceMode.Near,
                    out placedThing))
            {
                if (!thing.Destroyed)
                {
                    thing.Destroy(DestroyMode.Vanish);
                }

                GR_Log.Warning(
                    "Cannot resolve the Tok'ra safehouse lead cache: unable "
                    + $"to place {count} {logLabel} near {cacheCell}.");
                return false;
            }

            return true;
        }

        private static void DestroyIfSpawned(Thing thing)
        {
            if (thing != null && !thing.Destroyed)
            {
                thing.Destroy(DestroyMode.Vanish);
            }
        }

        private static bool TryFindCacheCell(Map map, out IntVec3 cell)
        {
            return TokraDeliveryDropUtility.TryFindPreferredDeliveryCell(
                map,
                null,
                out cell);
        }
    }
}
