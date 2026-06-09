using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Initializes developer-spawned Tok'ra voluntary-host prototype pawns.
    ///
    /// The prototype pawn is a player-controlled human PawnKindDef. The first
    /// scan attaches one active Tok'ra symbiote with a persistent identity.
    ///
    /// Initialization is recorded once per pawn ThingID. If the symbiote is
    /// removed later, the same host pawn is not given an artificial replacement.
    /// </summary>
    public class GameComponent_TokraHostPrototypeInitializer : GameComponent
    {
        private const int ScanIntervalTicks = 60;

        private List<string> initializedPawnThingIds = new List<string>();

        public GameComponent_TokraHostPrototypeInitializer(Game game)
        {
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Collections.Look(
                ref initializedPawnThingIds,
                "initializedTokraHostPrototypePawnThingIds",
                LookMode.Value);

            if (initializedPawnThingIds == null)
            {
                initializedPawnThingIds = new List<string>();
            }
        }

        public override void GameComponentTick()
        {
            if (Find.TickManager.TicksGame % ScanIntervalTicks != 0)
            {
                return;
            }

            for (int mapIndex = 0; mapIndex < Find.Maps.Count; mapIndex++)
            {
                UpdateMap(Find.Maps[mapIndex]);
            }
        }

        private void UpdateMap(Map map)
        {
            IReadOnlyList<Pawn> pawns = map?.mapPawns?.AllPawnsSpawned;

            if (pawns == null)
            {
                return;
            }

            for (int pawnIndex = 0; pawnIndex < pawns.Count; pawnIndex++)
            {
                TryInitializePrototypePawn(pawns[pawnIndex]);
            }
        }

        private void TryInitializePrototypePawn(Pawn pawn)
        {
            if (pawn == null
                || pawn.Destroyed
                || pawn.Dead
                || pawn.health == null
                || pawn.kindDef != GR_DefOf.SG1_TokraVoluntaryHost)
            {
                return;
            }

            string pawnThingId = pawn.ThingID;

            if (string.IsNullOrEmpty(pawnThingId)
                || initializedPawnThingIds.Contains(pawnThingId))
            {
                return;
            }

            if (HasAdultSymbioteState(pawn))
            {
                initializedPawnThingIds.Add(pawnThingId);

                GR_Log.Message(
                    $"Registered existing Tok'ra voluntary-host prototype "
                    + $"{PawnDebugLabel(pawn)} without creating a duplicate symbiote.");

                return;
            }

            Hediff activeHostState = HediffMaker.MakeHediff(
                GR_DefOf.SG1_GoauldHostSymbiote,
                pawn);

            HediffComp_GoauldSymbiote targetComp
                = FindPersistentSymbioteComp(activeHostState);

            if (targetComp == null)
            {
                GR_Log.Error(
                    "Cannot initialize Tok'ra voluntary-host prototype: "
                    + "SG1_GoauldHostSymbiote is missing "
                    + "HediffComp_GoauldSymbiote.");

                return;
            }

            GoauldSymbioteData data = GoauldSymbioteData.CreateFree(
                Find.TickManager?.TicksGame ?? 0,
                GoauldSymbioteOrigin.Tokra);

            targetComp.InitializeWithTransferredData(data);
            pawn.health.AddHediff(activeHostState);

            initializedPawnThingIds.Add(pawnThingId);

            GR_Log.Message(
                $"Initialized Tok'ra voluntary-host prototype "
                + $"{PawnDebugLabel(pawn)} with symbiote {data.SymbioteId}.");

            Messages.Message(
                "GR_TokraHostPrototype_Initialized".Translate(
                    pawn.LabelShortCap,
                    data.SymbioteId),
                pawn,
                MessageTypeDefOf.PositiveEvent,
                historical: true);
        }

        private static bool HasAdultSymbioteState(Pawn pawn)
        {
            if (pawn?.health?.hediffSet?.hediffs == null)
            {
                return false;
            }

            for (int index = 0; index < pawn.health.hediffSet.hediffs.Count; index++)
            {
                Hediff hediff = pawn.health.hediffSet.hediffs[index];

                if (hediff.def == GR_DefOf.SG1_GoauldRecentImplantation
                    || hediff.def == GR_DefOf.SG1_GoauldHostSymbiote)
                {
                    return true;
                }
            }

            return false;
        }

        private static HediffComp_GoauldSymbiote FindPersistentSymbioteComp(
            Hediff hediff)
        {
            HediffWithComps withComps = hediff as HediffWithComps;
            return withComps?.GetComp<HediffComp_GoauldSymbiote>();
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
