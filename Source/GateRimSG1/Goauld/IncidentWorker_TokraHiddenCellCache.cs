using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Rare low-impact hidden Tok'ra cell contact.
    ///
    /// This is the first non-territorial Tok'ra cell footprint. It does not
    /// create a settlement, a caravan, a trader, a quest site or military aid.
    /// A clandestine cell simply leaves a small medical cache on a reachable
    /// map-edge cell, then disappears.
    /// </summary>
    public class IncidentWorker_TokraHiddenCellCache : IncidentWorker
    {
        private const int NeutralTretoninDoseCount = 1;
        private const int CooperativeTretoninDoseCount = 2;
        private const int TrustedTretoninDoseCount = 2;

        private const int NeutralMedicineCount = 2;
        private const int CooperativeMedicineCount = 2;
        private const int TrustedMedicineCount = 3;

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
                    "Cannot start the Tok'ra hidden-cell cache: "
                    + "the incident target is not a map.");
                return false;
            }

            TokraTrustTier trustTier
                = GameComponent_TokraTrustTracker.GetCurrentTier();
            int trustScore
                = GameComponent_TokraTrustTracker.GetCurrentTrustScore();

            if (!IsEligibleTier(trustTier))
            {
                GR_Log.Message(
                    "Cannot start the Tok'ra hidden-cell cache: "
                    + $"the current {GetTierLogLabel(trustTier)} trust tier "
                    + $"({trustScore}) is too wary for clandestine support.");
                return false;
            }

            Faction tokraFaction = TokraFactionUtility.GetOrCreatePersistentFaction(
                "hidden Tok'ra cell cache");

            if (tokraFaction == null)
            {
                GR_Log.Error(
                    "Cannot start the Tok'ra hidden-cell cache: "
                    + "the persistent hidden Tok'ra world faction could not "
                    + "be resolved.");
                return false;
            }

            IntVec3 cacheCell;

            if (!TryFindCacheCell(map, out cacheCell))
            {
                GR_Log.Message(
                    "Cannot start the Tok'ra hidden-cell cache: no reachable "
                    + "unfogged edge cell was found.");
                return false;
            }

            int tretoninDoseCount = GetTretoninDoseCount(trustTier);
            int medicineCount = GetMedicineCount(trustTier);

            Thing placedTretonin;
            Thing placedMedicine;

            if (!TrySpawnStack(
                    map,
                    cacheCell,
                    GR_DefOf.SG1_TretoninDose,
                    tretoninDoseCount,
                    "tretonin dose(s)",
                    out placedTretonin))
            {
                return false;
            }

            if (!TrySpawnStack(
                    map,
                    cacheCell,
                    ResolveMedicineDef(),
                    medicineCount,
                    "industrial medicine unit(s)",
                    out placedMedicine))
            {
                DestroyIfSpawned(placedTretonin);
                return false;
            }

            string trustTierLabel = GetTierLogLabel(trustTier);

            GR_Log.Message(
                $"Started Tok'ra hidden-cell cache at {cacheCell} using "
                + $"{tokraFaction.Name} ({tokraFaction.loadID}); placed "
                + $"{tretoninDoseCount} tretonin dose(s) and "
                + $"{medicineCount} industrial medicine unit(s) at "
                + $"{trustTierLabel} trust tier ({trustScore}).");

            SendStandardLetter(
                parms,
                placedTretonin,
                tretoninDoseCount.ToString().Named("TRETONINCOUNT"),
                medicineCount.ToString().Named("MEDICINECOUNT"),
                trustTierLabel.Named("TIER"));

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

        private static int GetTretoninDoseCount(TokraTrustTier tier)
        {
            switch (tier)
            {
                case TokraTrustTier.Trusted:
                    return TrustedTretoninDoseCount;
                case TokraTrustTier.Cooperative:
                    return CooperativeTretoninDoseCount;
                default:
                    return NeutralTretoninDoseCount;
            }
        }

        private static int GetMedicineCount(TokraTrustTier tier)
        {
            switch (tier)
            {
                case TokraTrustTier.Trusted:
                    return TrustedMedicineCount;
                case TokraTrustTier.Cooperative:
                    return CooperativeMedicineCount;
                default:
                    return NeutralMedicineCount;
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
                    "Cannot start the Tok'ra hidden-cell cache: a required "
                    + "ThingDef could not be resolved.");
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
                    "Cannot start the Tok'ra hidden-cell cache: unable to "
                    + $"place {count} {logLabel} near {cacheCell}.");
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
