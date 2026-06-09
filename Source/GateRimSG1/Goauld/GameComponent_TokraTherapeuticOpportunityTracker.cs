using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI.Group;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Persistent lifecycle for escorted therapeutic Tok'ra opportunities.
    ///
    /// A registered free symbiote remains available for a limited time. The
    /// player may accept the existing implantation workflow, explicitly refuse
    /// the offer or let it expire. Refusal, expiration and successful transfer
    /// all ask the escort to leave the map through vanilla lord behavior.
    /// </summary>
    public class GameComponent_TokraTherapeuticOpportunityTracker : GameComponent
    {
        public const int DefaultOfferDurationTicks = 120000;

        private const int ScanIntervalTicks = 60;
        private const int TicksPerDay = 60000;

        private List<TokraTherapeuticOpportunityRecord> activeOffers
            = new List<TokraTherapeuticOpportunityRecord>();

        public GameComponent_TokraTherapeuticOpportunityTracker(Game game)
        {
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Collections.Look(
                ref activeOffers,
                "activeTokraTherapeuticOpportunityRecords",
                LookMode.Deep);

            if (activeOffers == null)
            {
                activeOffers = new List<TokraTherapeuticOpportunityRecord>();
            }
        }

        public override void GameComponentTick()
        {
            TickManager tickManager = Find.TickManager;

            if (tickManager == null
                || tickManager.TicksGame % ScanIntervalTicks != 0)
            {
                return;
            }

            int currentTick = tickManager.TicksGame;

            for (int index = activeOffers.Count - 1; index >= 0; index--)
            {
                TokraTherapeuticOpportunityRecord record = activeOffers[index];

                if (record == null)
                {
                    activeOffers.RemoveAt(index);
                    continue;
                }

                Pawn symbiote = record.Symbiote;

                if (symbiote == null
                    || symbiote.Destroyed
                    || symbiote.Dead
                    || !symbiote.Spawned)
                {
                    activeOffers.RemoveAt(index);
                    OrderEscortDeparture(record.EscortPawns);

                    GR_Log.Warning(
                        "Closed a Tok'ra therapeutic opportunity because its "
                        + "free symbiote became unavailable before transfer.");

                    continue;
                }

                if (currentTick >= record.ExpirationTick)
                {
                    CloseOffer(
                        record,
                        "GR_TokraTherapeuticOpportunity_Expired",
                        "expired",
                        vanishSymbiote: true,
                        outcome: TokraTherapeuticOfferOutcome.Expired);
                }
            }
        }

        public static void RegisterOpportunity(
            Pawn symbiote,
            List<Pawn> escortPawns,
            int offerDurationTicks)
        {
            GameComponent_TokraTherapeuticOpportunityTracker tracker
                = GetCurrentTracker();

            if (tracker == null)
            {
                GR_Log.Error(
                    "Cannot register Tok'ra therapeutic opportunity: "
                    + "the lifecycle tracker is unavailable.");
                return;
            }

            tracker.RegisterOpportunityInternal(
                symbiote,
                escortPawns,
                offerDurationTicks);
        }

        public static bool IsTrackedOffer(Pawn symbiote)
        {
            return GetCurrentTracker()?.FindRecord(symbiote) != null;
        }

        public static string GetRemainingDaysLabel(Pawn symbiote)
        {
            TokraTherapeuticOpportunityRecord record
                = GetCurrentTracker()?.FindRecord(symbiote);

            if (record == null)
            {
                return string.Empty;
            }

            int remainingTicks = Math.Max(
                0,
                record.ExpirationTick - CurrentGameTick());

            float remainingDays = (float)remainingTicks / TicksPerDay;
            return remainingDays.ToString("0.0");
        }

        public static string GetInspectString(Pawn symbiote)
        {
            string remainingDays = GetRemainingDaysLabel(symbiote);

            if (string.IsNullOrEmpty(remainingDays))
            {
                return string.Empty;
            }

            string offerInspect = "GR_TokraTherapeuticOpportunity_Inspect"
                .Translate(remainingDays)
                .ToString();

            string trustInspect
                = GameComponent_TokraTrustTracker.GetInspectString();

            return offerInspect + "\n" + trustInspect;
        }

        public static bool TryRejectOffer(Pawn symbiote)
        {
            GameComponent_TokraTherapeuticOpportunityTracker tracker
                = GetCurrentTracker();

            TokraTherapeuticOpportunityRecord record
                = tracker?.FindRecord(symbiote);

            if (record == null)
            {
                return false;
            }

            tracker.CloseOffer(
                record,
                "GR_TokraTherapeuticOpportunity_Refused",
                "refused",
                vanishSymbiote: true,
                outcome: TokraTherapeuticOfferOutcome.Refused);

            return true;
        }

        public static void NotifySymbioteImplanted(Pawn symbiote)
        {
            GameComponent_TokraTherapeuticOpportunityTracker tracker
                = GetCurrentTracker();

            TokraTherapeuticOpportunityRecord record
                = tracker?.FindRecord(symbiote);

            if (record == null)
            {
                return;
            }

            tracker.activeOffers.Remove(record);
            OrderEscortDeparture(record.EscortPawns);
            GameComponent_TokraTrustTracker.NotifyTherapeuticOfferOutcome(
                TokraTherapeuticOfferOutcome.Accepted);

            GR_Log.Message(
                $"Closed accepted Tok'ra therapeutic opportunity for "
                + $"{PawnDebugLabel(symbiote)} and ordered its escort to leave.");
        }

        private void RegisterOpportunityInternal(
            Pawn symbiote,
            List<Pawn> escortPawns,
            int offerDurationTicks)
        {
            if (symbiote == null || symbiote.Destroyed)
            {
                GR_Log.Error(
                    "Cannot register Tok'ra therapeutic opportunity: "
                    + "the free symbiote is unavailable.");
                return;
            }

            int normalizedOfferDurationTicks
                = NormalizeOfferDurationTicks(offerDurationTicks);

            TokraTherapeuticOpportunityRecord existingRecord
                = FindRecord(symbiote);

            if (existingRecord != null)
            {
                existingRecord.ExpirationTick = CurrentGameTick()
                    + normalizedOfferDurationTicks;
                existingRecord.EscortPawns = CopyEscortList(escortPawns);
                return;
            }

            TokraTherapeuticOpportunityRecord record
                = new TokraTherapeuticOpportunityRecord
                {
                    Symbiote = symbiote,
                    EscortPawns = CopyEscortList(escortPawns),
                    ExpirationTick = CurrentGameTick()
                        + normalizedOfferDurationTicks
                };

            activeOffers.Add(record);

            GR_Log.Message(
                $"Registered Tok'ra therapeutic opportunity for "
                + $"{PawnDebugLabel(symbiote)} with "
                + $"{record.EscortPawns.Count} escort pawn(s) for "
                + $"{normalizedOfferDurationTicks} ticks.");
        }

        private void CloseOffer(
            TokraTherapeuticOpportunityRecord record,
            string messageKey,
            string logReason,
            bool vanishSymbiote,
            TokraTherapeuticOfferOutcome outcome)
        {
            if (record == null)
            {
                return;
            }

            Pawn symbiote = record.Symbiote;

            activeOffers.Remove(record);
            OrderEscortDeparture(record.EscortPawns);

            if (symbiote != null && !symbiote.Destroyed)
            {
                Messages.Message(
                    messageKey.Translate(),
                    symbiote,
                    MessageTypeDefOf.NeutralEvent,
                    historical: true);

                if (vanishSymbiote)
                {
                    symbiote.Destroy(DestroyMode.Vanish);
                }
            }

            GameComponent_TokraTrustTracker.NotifyTherapeuticOfferOutcome(
                outcome);

            GR_Log.Message(
                $"Closed {logReason} Tok'ra therapeutic opportunity for "
                + $"{PawnDebugLabel(symbiote)} and ordered its escort to leave.");
        }

        private TokraTherapeuticOpportunityRecord FindRecord(Pawn symbiote)
        {
            if (symbiote == null)
            {
                return null;
            }

            for (int index = 0; index < activeOffers.Count; index++)
            {
                TokraTherapeuticOpportunityRecord record = activeOffers[index];

                if (record?.Symbiote == symbiote)
                {
                    return record;
                }
            }

            return null;
        }

        private static void OrderEscortDeparture(List<Pawn> escortPawns)
        {
            if (escortPawns == null || escortPawns.Count == 0)
            {
                return;
            }

            HashSet<Lord> handledLords = new HashSet<Lord>();
            List<Pawn> unassignedPawns = new List<Pawn>();

            for (int index = 0; index < escortPawns.Count; index++)
            {
                Pawn escortPawn = escortPawns[index];

                if (escortPawn == null
                    || escortPawn.Destroyed
                    || escortPawn.Dead
                    || !escortPawn.Spawned)
                {
                    continue;
                }

                Lord lord = escortPawn.GetLord();

                if (lord == null)
                {
                    unassignedPawns.Add(escortPawn);
                    continue;
                }

                if (!handledLords.Add(lord))
                {
                    continue;
                }

                if (lord.LordJob is LordJob_TokraTherapeuticEscort)
                {
                    lord.ReceiveMemo(LordJob_TokraTherapeuticEscort.LeaveMemo);
                    continue;
                }

                if (lord.LordJob is LordJob_TravelAndExit)
                {
                    continue;
                }

                RebuildLegacyEscortDepartureLord(lord, escortPawns);
            }

            if (unassignedPawns.Count == 0)
            {
                return;
            }

            Pawn firstPawn = unassignedPawns[0];
            IntVec3 unassignedExitCell;

            if (!TryFindEscortExitCell(firstPawn, out unassignedExitCell))
            {
                GR_Log.Warning(
                    "Unable to create a departure lord for unassigned Tok'ra "
                    + "therapeutic escort pawns: no valid exit cell could be "
                    + $"found for {PawnDebugLabel(firstPawn)}.");
                return;
            }

            LordMaker.MakeNewLord(
                firstPawn.Faction,
                new LordJob_TravelAndExit(unassignedExitCell),
                firstPawn.Map,
                unassignedPawns);
        }

        private static void RebuildLegacyEscortDepartureLord(
            Lord legacyLord,
            List<Pawn> escortPawns)
        {
            if (legacyLord == null)
            {
                return;
            }

            List<Pawn> legacyPawns = new List<Pawn>();

            for (int index = 0; index < escortPawns.Count; index++)
            {
                Pawn escortPawn = escortPawns[index];

                if (escortPawn != null
                    && !escortPawn.Destroyed
                    && !escortPawn.Dead
                    && escortPawn.Spawned
                    && escortPawn.GetLord() == legacyLord)
                {
                    legacyPawns.Add(escortPawn);
                }
            }

            if (legacyPawns.Count == 0)
            {
                return;
            }

            Pawn firstPawn = legacyPawns[0];
            IntVec3 exitCell;

            if (!TryFindEscortExitCell(firstPawn, out exitCell))
            {
                GR_Log.Warning(
                    "Unable to rebuild a legacy Tok'ra therapeutic escort "
                    + "departure lord: no valid exit cell could be found "
                    + $"for {PawnDebugLabel(firstPawn)}.");
                return;
            }

            Map map = firstPawn.Map;
            Faction faction = firstPawn.Faction;

            legacyLord.lordManager.RemoveLord(legacyLord);

            LordMaker.MakeNewLord(
                faction,
                new LordJob_TravelAndExit(exitCell),
                map,
                legacyPawns);

            GR_Log.Warning(
                "Rebuilt a legacy Tok'ra therapeutic escort departure lord "
                + "created before the safe-memo departure fix.");
        }

        private static bool TryFindEscortExitCell(
            Pawn escortPawn,
            out IntVec3 exitCell)
        {
            exitCell = IntVec3.Invalid;

            if (escortPawn == null || escortPawn.Map == null)
            {
                return false;
            }

            if (CellFinder.TryFindRandomPawnExitCell(escortPawn, out exitCell))
            {
                return true;
            }

            RCellFinder.TryFindRandomPawnEntryCell(
                out exitCell,
                escortPawn.Map,
                0f);

            return exitCell.IsValid;
        }

        private static int NormalizeOfferDurationTicks(
            int offerDurationTicks)
        {
            return offerDurationTicks > 0
                ? offerDurationTicks
                : DefaultOfferDurationTicks;
        }

        private static List<Pawn> CopyEscortList(List<Pawn> escortPawns)
        {
            return escortPawns == null
                ? new List<Pawn>()
                : new List<Pawn>(escortPawns);
        }

        private static GameComponent_TokraTherapeuticOpportunityTracker
            GetCurrentTracker()
        {
            return Current.Game?
                .GetComponent<GameComponent_TokraTherapeuticOpportunityTracker>();
        }

        private static int CurrentGameTick()
        {
            return Find.TickManager?.TicksGame ?? 0;
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

    public class TokraTherapeuticOpportunityRecord : IExposable
    {
        public Pawn Symbiote;
        public List<Pawn> EscortPawns = new List<Pawn>();
        public int ExpirationTick;

        public void ExposeData()
        {
            Scribe_References.Look(ref Symbiote, "symbiote");
            Scribe_Collections.Look(
                ref EscortPawns,
                "escortPawns",
                LookMode.Reference);
            Scribe_Values.Look(ref ExpirationTick, "expirationTick", 0);

            if (EscortPawns == null)
            {
                EscortPawns = new List<Pawn>();
            }
        }
    }
}
