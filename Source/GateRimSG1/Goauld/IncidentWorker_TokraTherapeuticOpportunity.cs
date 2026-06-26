using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI.Group;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Rare natural escorted Tok'ra therapeutic opportunity.
    ///
    /// The incident looks for a player-controlled compatible humanoid with at
    /// least one non-traumatic biological condition accepted by the shared
    /// Tok'ra healing filter. It then spawns one free Tok'ra symbiote with a
    /// small peaceful escort and leaves the final implantation choice to the
    /// player's existing therapeutic-implantation command. Cooperative and
    /// trusted teams also bring a small physical tretonin-support gift.
    /// </summary>
    public class IncidentWorker_TokraTherapeuticOpportunity : IncidentWorker
    {
        private const float MinimumVoluntaryHostAgeYears = 13f;
        private const int EscortSpawnRadius = 5;

        public override float BaseChanceThisGame
        {
            get
            {
                return base.BaseChanceThisGame
                    * GameComponent_TokraTrustTracker
                        .GetCurrentTherapeuticOpportunityChanceFactor();
            }
        }

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
                || GameComponent_TokraTrustTracker
                    .IsWaryDiplomaticCooldownActive()
                || FindBestCandidate(map) == null
                || GR_DefOf.SG1_TokraSymbiote == null
                || GR_DefOf.SG1_TokraVoluntaryHost == null
                || GR_DefOf.SG1_Tokra == null)
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
                    "Cannot start the escorted Tok'ra therapeutic "
                    + "opportunity: the incident target is not a map.");
                return false;
            }

            if (GameComponent_TokraTrustTracker
                .IsWaryDiplomaticCooldownActive())
            {
                int remainingTicks
                    = GameComponent_TokraTrustTracker
                        .GetRemainingWaryDiplomaticCooldownTicks();
                float remainingDays
                    = GameComponent_TokraTrustTracker
                        .GetRemainingWaryDiplomaticCooldownDays();

                GR_Log.Message(
                    "Cannot start the escorted Tok'ra therapeutic "
                    + "opportunity: the wary diplomatic cooldown remains "
                    + $"active for {remainingTicks} tick(s) "
                    + $"({remainingDays:0.#} RimWorld day(s)).");
                return false;
            }

            Pawn candidate = FindBestCandidate(map);

            if (candidate == null)
            {
                GR_Log.Message(
                    "Cannot start the escorted Tok'ra therapeutic "
                    + "opportunity: no eligible sick player-controlled "
                    + "humanoid was found. "
                    + BuildCandidateDiagnostics(map));
                return false;
            }

            PawnKindDef symbioteKind = GR_DefOf.SG1_TokraSymbiote;

            if (symbioteKind == null)
            {
                GR_Log.Warning(
                    "Cannot start the escorted Tok'ra therapeutic "
                    + "opportunity: SG1_TokraSymbiote could not be "
                    + "resolved from GR_DefOf.");
                return false;
            }

            PawnKindDef escortKind = GR_DefOf.SG1_TokraVoluntaryHost;

            if (escortKind == null)
            {
                GR_Log.Warning(
                    "Cannot start the escorted Tok'ra therapeutic "
                    + "opportunity: SG1_TokraVoluntaryHost could not be "
                    + "resolved from GR_DefOf.");
                return false;
            }

            IntVec3 entryCell;

            if (!TryFindEntryCell(map, out entryCell))
            {
                GR_Log.Message(
                    "Cannot start the escorted Tok'ra therapeutic "
                    + "opportunity: no reachable unfogged map-edge entry "
                    + "cell was found.");
                return false;
            }

            Faction tokraFaction = TokraFactionUtility.GetOrCreatePersistentFaction(
                "therapeutic Tok'ra opportunities");

            if (tokraFaction == null)
            {
                GR_Log.Error(
                    "Cannot start the Tok'ra therapeutic opportunity: "
                    + "the persistent hidden Tok'ra world faction could not be resolved.");
                return false;
            }

            Pawn symbiote = PawnGenerator.GeneratePawn(symbioteKind);

            if (symbiote == null)
            {
                GR_Log.Error(
                    "Unable to generate the free Tok'ra symbiote for a "
                    + "therapeutic-opportunity incident.");
                return false;
            }

            GenSpawn.Spawn(symbiote, entryCell, map);

            int offerDurationTicks
                = GameComponent_TokraTrustTracker
                    .GetCurrentOfferDurationTicks();
            int requestedEscortCount
                = GameComponent_TokraTrustTracker.RollCurrentEscortCount();
            string trustTierLabel
                = GameComponent_TokraTrustTracker.GetCurrentTierLogLabel();
            int requestedTretoninGiftCount
                = GameComponent_TokraTrustTracker
                    .GetCurrentTretoninGiftCount();
            float storytellerChanceFactor
                = GameComponent_TokraTrustTracker
                    .GetCurrentTherapeuticOpportunityChanceFactor();
            Thing placedTretoninGift;
            int spawnedTretoninGiftCount = SpawnTretoninGift(
                map,
                entryCell,
                requestedTretoninGiftCount,
                out placedTretoninGift);

            List<Pawn> escortPawns = SpawnEscortPawns(
                map,
                entryCell,
                tokraFaction,
                escortKind,
                requestedEscortCount);

            if (escortPawns.Count == 0)
            {
                GR_Log.Warning(
                    "Started a Tok'ra therapeutic opportunity without an "
                    + "escort because no escort pawn could be generated.");
            }
            else
            {
                StartEscortVisit(
                    map,
                    entryCell,
                    tokraFaction,
                    escortPawns,
                    offerDurationTicks);
            }

            GameComponent_TokraTherapeuticOpportunityTracker
                .RegisterOpportunity(
                    symbiote,
                    escortPawns,
                    offerDurationTicks);

            string conditionLabels
                = GameComponent_TokraTherapeuticHosting
                    .GetSeriousTherapeuticNeedLabels(candidate);

            GR_Log.Message(
                $"Started escorted Tok'ra therapeutic opportunity for "
                + $"{PawnDebugLabel(candidate)} with conditions "
                + $"{conditionLabels}; spawned free symbiote "
                + $"{PawnDebugLabel(symbiote)} at {entryCell} with "
                + $"{escortPawns.Count} escort pawn(s), "
                + $"{spawnedTretoninGiftCount} tretonin support dose(s), "
                + $"for {offerDurationTicks} ticks at {trustTierLabel} trust "
                + $"tier; storyteller chance factor "
                + $"x{storytellerChanceFactor:0.00}, effective base chance "
                + $"{BaseChanceThisGame:0.####}.");

            SendStandardLetter(
                parms,
                symbiote,
                candidate.Named("PAWN"),
                conditionLabels.Named("CONDITIONS"));

            if (spawnedTretoninGiftCount > 0 && placedTretoninGift != null)
            {
                Messages.Message(
                    "GR_TokraMedicalSupport_GiftReceived"
                        .Translate(spawnedTretoninGiftCount),
                    placedTretoninGift,
                    MessageTypeDefOf.PositiveEvent,
                    historical: true);
            }

            return true;
        }

        private static int SpawnTretoninGift(
            Map map,
            IntVec3 entryCell,
            int requestedDoseCount,
            out Thing placedTretoninGift)
        {
            placedTretoninGift = null;

            if (requestedDoseCount <= 0)
            {
                return 0;
            }

            ThingDef tretoninDoseDef
                = DefDatabase<ThingDef>.GetNamedSilentFail("SG1_TretoninDose");

            if (tretoninDoseDef == null)
            {
                GR_Log.Warning(
                    "Unable to spawn Tok'ra tretonin-support gift: "
                    + "SG1_TretoninDose could not be resolved.");
                return 0;
            }

            Thing tretoninGift = ThingMaker.MakeThing(tretoninDoseDef);
            tretoninGift.stackCount = requestedDoseCount;

            if (!GenPlace.TryPlaceThing(
                    tretoninGift,
                    entryCell,
                    map,
                    ThingPlaceMode.Near,
                    out placedTretoninGift))
            {
                if (!tretoninGift.Destroyed)
                {
                    tretoninGift.Destroy(DestroyMode.Vanish);
                }

                GR_Log.Warning(
                    "Unable to place Tok'ra tretonin-support gift near "
                    + $"entry cell {entryCell}.");
                return 0;
            }

            GR_Log.Message(
                $"Spawned {requestedDoseCount} tretonin support dose(s) "
                + $"near {placedTretoninGift.Position} for a Tok'ra "
                + "therapeutic opportunity.");

            return requestedDoseCount;
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
                        "Unable to generate one Tok'ra escort pawn for a "
                        + "therapeutic-opportunity incident.");
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

        private static void StartEscortVisit(
            Map map,
            IntVec3 entryCell,
            Faction tokraFaction,
            List<Pawn> escortPawns,
            int offerDurationTicks)
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
                new LordJob_TokraTherapeuticEscort(
                    tokraFaction,
                    visitSpot,
                    offerDurationTicks),
                map,
                escortPawns);
        }

        private static Pawn FindBestCandidate(Map map)
        {
            IReadOnlyList<Pawn> pawns = map?.mapPawns?.AllPawnsSpawned;

            if (pawns == null)
            {
                return null;
            }

            Pawn bestCandidate = null;
            float bestScore = 0f;

            for (int index = 0; index < pawns.Count; index++)
            {
                Pawn candidate = pawns[index];

                if (!IsValidCandidate(candidate))
                {
                    continue;
                }

                float score
                    = GameComponent_TokraTherapeuticHosting
                        .GetSeriousTherapeuticNeedScore(candidate);

                if (bestCandidate == null || score > bestScore)
                {
                    bestCandidate = candidate;
                    bestScore = score;
                }
            }

            return bestCandidate;
        }

        private static string BuildCandidateDiagnostics(Map map)
        {
            IReadOnlyList<Pawn> pawns = map?.mapPawns?.AllPawnsSpawned;

            if (pawns == null)
            {
                return "The map pawn list is unavailable.";
            }

            int playerHumanlikes = 0;
            int underMinimumAge = 0;
            int existingSymbioteState = 0;
            int seriousTherapeuticNeed = 0;

            for (int index = 0; index < pawns.Count; index++)
            {
                Pawn pawn = pawns[index];

                if (pawn == null
                    || !pawn.Spawned
                    || pawn.Destroyed
                    || pawn.Dead
                    || pawn.Faction != Faction.OfPlayer
                    || !pawn.RaceProps.Humanlike)
                {
                    continue;
                }

                playerHumanlikes++;

                if (pawn.ageTracker != null
                    && pawn.ageTracker.AgeBiologicalYearsFloat
                        < MinimumVoluntaryHostAgeYears)
                {
                    underMinimumAge++;
                }

                if (HasExistingSymbioteState(pawn))
                {
                    existingSymbioteState++;
                }

                if (GameComponent_TokraTherapeuticHosting
                    .HasSeriousTherapeuticNeed(pawn))
                {
                    seriousTherapeuticNeed++;
                }
            }

            return $"Player humanoids: {playerHumanlikes}; "
                + $"under {MinimumVoluntaryHostAgeYears:0} years: "
                + $"{underMinimumAge}; existing symbiote state: "
                + $"{existingSymbioteState}; with a serious therapeutic "
                + $"need: {seriousTherapeuticNeed}.";
        }

        private static bool IsValidCandidate(Pawn pawn)
        {
            return pawn != null
                && pawn.Spawned
                && !pawn.Destroyed
                && !pawn.Dead
                && pawn.Faction == Faction.OfPlayer
                && pawn.RaceProps.Humanlike
                && pawn.health?.hediffSet?.hediffs != null
                && (pawn.ageTracker == null
                    || pawn.ageTracker.AgeBiologicalYearsFloat
                        >= MinimumVoluntaryHostAgeYears)
                && !HasExistingSymbioteState(pawn)
                && GameComponent_TokraTherapeuticHosting
                    .HasSeriousTherapeuticNeed(pawn);
        }

        private static bool HasExistingSymbioteState(Pawn pawn)
        {
            List<Hediff> hediffs = pawn.health.hediffSet.hediffs;

            for (int index = 0; index < hediffs.Count; index++)
            {
                HediffDef def = hediffs[index].def;

                if (def == GR_DefOf.SG1_GoauldRecentImplantation
                    || def == GR_DefOf.SG1_GoauldHostSymbiote)
                {
                    return true;
                }
            }

            return false;
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

        private static string PawnDebugLabel(Pawn pawn)
        {
            if (pawn == null)
            {
                return "<null pawn>";
            }

            return $"{pawn.LabelShort} ({pawn.ThingID})";
        }
    }
}
