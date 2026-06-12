using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI.Group;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Rare trust-gated Tok'ra medical-support delivery.
    ///
    /// Cooperative and trusted colonies may receive physical tretonin doses
    /// independently from therapeutic symbiosis offers. A small peaceful Tok'ra
    /// team accompanies the supplies, then leaves through the vanilla visitor
    /// behavior after a short stay.
    /// </summary>
    public class IncidentWorker_TokraMedicalSupportDelivery : IncidentWorker
    {
        private const int EscortSpawnRadius = 5;
        private const int VisitDurationTicks = 60000;
        private const int CooperativeDeliveryDoseCount = 2;
        private const int TrustedDeliveryDoseCount = 4;
        private const int CooperativeEscortCount = 1;
        private const int TrustedEscortCount = 2;
        private const int TrustedAdvancedMedicineCount = 1;

        public override float BaseChanceThisGame
        {
            get
            {
                return base.BaseChanceThisGame
                    * GameComponent_TokraTrustTracker
                        .GetCurrentMedicalSupportDeliveryChanceFactor();
            }
        }

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            if (!base.CanFireNowSub(parms))
            {
                return false;
            }

            Map map = parms.target as Map;

            if (map == null
                || !IsCurrentTierEligible()
                || GR_DefOf.SG1_TokraVoluntaryHost == null
                || GR_DefOf.SG1_Tokra == null
                || ResolveTretoninDoseDef() == null
                || !CanResolveTrustedAdvancedMedicine())
            {
                return false;
            }

            IntVec3 unusedEntryCell;
            return TryFindEntryCell(map, out unusedEntryCell);
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = parms.target as Map;

            if (map == null)
            {
                GR_Log.Warning(
                    "Cannot start the Tok'ra medical-support delivery: "
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
                    "Cannot start the Tok'ra medical-support delivery: "
                    + $"the current {GetTierLogLabel(trustTier)} trust tier "
                    + $"({trustScore}) has not unlocked independent "
                    + "support deliveries.");
                return false;
            }

            PawnKindDef escortKind = GR_DefOf.SG1_TokraVoluntaryHost;

            if (escortKind == null)
            {
                GR_Log.Warning(
                    "Cannot start the Tok'ra medical-support delivery: "
                    + "SG1_TokraVoluntaryHost could not be resolved from "
                    + "GR_DefOf.");
                return false;
            }

            ThingDef tretoninDoseDef = ResolveTretoninDoseDef();

            if (tretoninDoseDef == null)
            {
                GR_Log.Warning(
                    "Cannot start the Tok'ra medical-support delivery: "
                    + "SG1_TretoninDose could not be resolved.");
                return false;
            }

            IntVec3 entryCell;

            if (!TryFindEntryCell(map, out entryCell))
            {
                GR_Log.Message(
                    "Cannot start the Tok'ra medical-support delivery: no "
                    + "reachable unfogged map-edge entry cell was found.");
                return false;
            }

            Faction tokraFaction = TokraFactionUtility.GetOrCreateHiddenFaction(
                "medical-support deliveries");

            if (tokraFaction == null)
            {
                GR_Log.Error(
                    "Cannot start the Tok'ra medical-support delivery: "
                    + "the hidden Tok'ra faction could not be created.");
                return false;
            }

            int requestedDoseCount = GetDeliveryDoseCount(trustTier);
            Thing placedDelivery;

            if (!TrySpawnTretoninDelivery(
                    map,
                    entryCell,
                    tretoninDoseDef,
                    requestedDoseCount,
                    out placedDelivery))
            {
                return false;
            }

            int requestedAdvancedMedicineCount
                = GetAdvancedMedicineCount(trustTier);
            Thing placedAdvancedMedicine;

            if (!TrySpawnTrustedAdvancedMedicineDelivery(
                    map,
                    entryCell,
                    requestedAdvancedMedicineCount,
                    out placedAdvancedMedicine))
            {
                DestroyIfSpawned(placedDelivery);
                return false;
            }

            int requestedEscortCount = GetEscortCount(trustTier);
            List<Pawn> escortPawns = SpawnEscortPawns(
                map,
                entryCell,
                tokraFaction,
                escortKind,
                requestedEscortCount);

            if (escortPawns.Count == 0)
            {
                DestroyIfSpawned(placedDelivery);
                DestroyIfSpawned(placedAdvancedMedicine);

                GR_Log.Warning(
                    "Cannot start the Tok'ra medical-support delivery: no "
                    + "escort pawn could be generated.");
                return false;
            }

            StartSupportVisit(map, entryCell, tokraFaction, escortPawns);

            string trustTierLabel = GetTierLogLabel(trustTier);
            float storytellerChanceFactor
                = GameComponent_TokraTrustTracker
                    .GetCurrentMedicalSupportDeliveryChanceFactor();

            GR_Log.Message(
                $"Started Tok'ra medical-support delivery at {entryCell} "
                + $"with {requestedDoseCount} tretonin dose(s), "
                + $"{requestedAdvancedMedicineCount} advanced medicine "
                + $"unit(s), {escortPawns.Count} visitor pawn(s) and "
                + $"{trustTierLabel} trust tier ({trustScore}); "
                + $"storyteller chance factor "
                + $"x{storytellerChanceFactor:0.00}, effective base chance "
                + $"{BaseChanceThisGame:0.####}.");

            SendStandardLetter(
                parms,
                placedDelivery,
                requestedDoseCount.ToString().Named("COUNT"),
                trustTierLabel.Named("TIER"));

            if (placedAdvancedMedicine != null)
            {
                Messages.Message(
                    "GR_TokraTrustedAdvancedMedicine_GiftReceived"
                        .Translate(requestedAdvancedMedicineCount),
                    placedAdvancedMedicine,
                    MessageTypeDefOf.PositiveEvent,
                    historical: true);
            }

            return true;
        }

        private static bool IsCurrentTierEligible()
        {
            return IsEligibleTier(
                GameComponent_TokraTrustTracker.GetCurrentTier());
        }

        private static bool IsEligibleTier(TokraTrustTier tier)
        {
            return tier == TokraTrustTier.Cooperative
                || tier == TokraTrustTier.Trusted;
        }

        private static int GetDeliveryDoseCount(TokraTrustTier tier)
        {
            return tier == TokraTrustTier.Trusted
                ? TrustedDeliveryDoseCount
                : CooperativeDeliveryDoseCount;
        }

        private static int GetEscortCount(TokraTrustTier tier)
        {
            return tier == TokraTrustTier.Trusted
                ? TrustedEscortCount
                : CooperativeEscortCount;
        }

        private static int GetAdvancedMedicineCount(TokraTrustTier tier)
        {
            return tier == TokraTrustTier.Trusted
                ? TrustedAdvancedMedicineCount
                : 0;
        }

        private static string GetTierLogLabel(TokraTrustTier tier)
        {
            switch (tier)
            {
                case TokraTrustTier.Wary:
                    return "wary";
                case TokraTrustTier.Neutral:
                    return "neutral";
                case TokraTrustTier.Trusted:
                    return "trusted";
                default:
                    return "cooperative";
            }
        }

        private static ThingDef ResolveTretoninDoseDef()
        {
            return DefDatabase<ThingDef>.GetNamedSilentFail("SG1_TretoninDose");
        }

        private static ThingDef ResolveTrustedAdvancedMedicineDef()
        {
            return DefDatabase<ThingDef>.GetNamedSilentFail(
                "MedicineUltratech");
        }

        private static bool CanResolveTrustedAdvancedMedicine()
        {
            return GameComponent_TokraTrustTracker.GetCurrentTier()
                    != TokraTrustTier.Trusted
                || ResolveTrustedAdvancedMedicineDef() != null;
        }

        private static bool TrySpawnTretoninDelivery(
            Map map,
            IntVec3 entryCell,
            ThingDef tretoninDoseDef,
            int requestedDoseCount,
            out Thing placedDelivery)
        {
            Thing delivery = ThingMaker.MakeThing(tretoninDoseDef);
            delivery.stackCount = requestedDoseCount;

            if (!GenPlace.TryPlaceThing(
                    delivery,
                    entryCell,
                    map,
                    ThingPlaceMode.Near,
                    out placedDelivery))
            {
                if (!delivery.Destroyed)
                {
                    delivery.Destroy(DestroyMode.Vanish);
                }

                GR_Log.Warning(
                    "Cannot start the Tok'ra medical-support delivery: "
                    + $"unable to place {requestedDoseCount} tretonin "
                    + $"dose(s) near entry cell {entryCell}.");
                return false;
            }

            return true;
        }

        private static bool TrySpawnTrustedAdvancedMedicineDelivery(
            Map map,
            IntVec3 entryCell,
            int requestedMedicineCount,
            out Thing placedAdvancedMedicine)
        {
            placedAdvancedMedicine = null;

            if (requestedMedicineCount <= 0)
            {
                return true;
            }

            ThingDef medicineDef = ResolveTrustedAdvancedMedicineDef();

            if (medicineDef == null)
            {
                GR_Log.Warning(
                    "Cannot start the trusted Tok'ra medical-support "
                    + "delivery: MedicineUltratech could not be resolved.");
                return false;
            }

            Thing medicine = ThingMaker.MakeThing(medicineDef);
            medicine.stackCount = requestedMedicineCount;

            if (!GenPlace.TryPlaceThing(
                    medicine,
                    entryCell,
                    map,
                    ThingPlaceMode.Near,
                    out placedAdvancedMedicine))
            {
                if (!medicine.Destroyed)
                {
                    medicine.Destroy(DestroyMode.Vanish);
                }

                GR_Log.Warning(
                    "Cannot start the trusted Tok'ra medical-support "
                    + $"delivery: unable to place {requestedMedicineCount} "
                    + $"advanced medicine unit(s) near entry cell "
                    + $"{entryCell}.");
                return false;
            }

            GR_Log.Message(
                $"Spawned {requestedMedicineCount} advanced medicine "
                + $"unit(s) near {entryCell} for a trusted Tok'ra "
                + "medical-support delivery.");

            return true;
        }

        private static void DestroyIfSpawned(Thing thing)
        {
            if (thing != null && !thing.Destroyed)
            {
                thing.Destroy(DestroyMode.Vanish);
            }
        }

        private static List<Pawn> SpawnEscortPawns(
            Map map,
            IntVec3 entryCell,
            Faction tokraFaction,
            PawnKindDef escortKind,
            int escortCount)
        {
            List<Pawn> escortPawns = new List<Pawn>(escortCount);

            for (int index = 0; index < escortCount; index++)
            {
                Pawn escortPawn = PawnGenerator.GeneratePawn(
                    escortKind,
                    tokraFaction);

                if (escortPawn == null)
                {
                    GR_Log.Warning(
                        "Unable to generate one Tok'ra visitor pawn for a "
                        + "medical-support delivery.");
                    continue;
                }

                IntVec3 escortCell = CellFinder.RandomClosewalkCellNear(
                    entryCell,
                    map,
                    EscortSpawnRadius);

                GenSpawn.Spawn(escortPawn, escortCell, map);
                escortPawns.Add(escortPawn);
            }

            return escortPawns;
        }

        private static void StartSupportVisit(
            Map map,
            IntVec3 entryCell,
            Faction tokraFaction,
            List<Pawn> escortPawns)
        {
            IntVec3 visitSpot;

            if (!RCellFinder.TryFindRandomSpotJustOutsideColony(
                    escortPawns[0],
                    out visitSpot))
            {
                visitSpot = entryCell;
            }

            LordMaker.MakeNewLord(
                tokraFaction,
                new LordJob_VisitColony(
                    tokraFaction,
                    visitSpot,
                    VisitDurationTicks),
                map,
                escortPawns);
        }

        private static bool TryFindEntryCell(Map map, out IntVec3 cell)
        {
            return CellFinder.TryFindRandomEdgeCellWith(
                candidateCell => map.reachability.CanReachColony(candidateCell)
                    && !candidateCell.Fogged(map),
                map,
                CellFinder.EdgeRoadChance_Neutral,
                out cell);
        }
    }
}
