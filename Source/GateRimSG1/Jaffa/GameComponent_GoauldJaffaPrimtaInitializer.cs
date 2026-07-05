using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Jaffa
{
    /// <summary>
    /// Assigns one initial Prim'ta to supported generated Jaffa pawn kinds.
    ///
    /// The initializer originally targeted the two Goa'uld-aligned pawn kinds.
    /// It now also covers the Free Jaffa warrior, guard and trader kinds plus
    /// the Goa'uld combat, officer and settlement profiles while keeping
    /// the historical component and save-data key for compatibility.
    /// Initialization is recorded once per pawn ThingID. Removing the Prim'ta
    /// later must not create an artificial replacement.
    /// </summary>
    public class GameComponent_GoauldJaffaPrimtaInitializer : GameComponent
    {
        private const int ScanIntervalTicks = 60;

        private List<string> initializedPawnThingIds = new List<string>();

        public GameComponent_GoauldJaffaPrimtaInitializer(Game game)
        {
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Collections.Look(
                ref initializedPawnThingIds,
                "initializedGoauldJaffaPrimtaPawnThingIds",
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
                TryInitializePawn(pawns[pawnIndex]);
            }
        }

        private void TryInitializePawn(Pawn pawn)
        {
            if (pawn == null
                || pawn.Destroyed
                || pawn.Dead
                || pawn.health == null
                || !IsGeneratedJaffaPawnKind(pawn.kindDef))
            {
                return;
            }

            string pawnThingId = pawn.ThingID;
            if (string.IsNullOrEmpty(pawnThingId)
                || initializedPawnThingIds.Contains(pawnThingId))
            {
                return;
            }

            if (JaffaPrimtaUtility.HasPrimta(pawn))
            {
                initializedPawnThingIds.Add(pawnThingId);

                GR_Log.Message(
                    $"Registered existing Prim'ta for generated Jaffa "
                    + $"{JaffaPrimtaUtility.PawnDebugLabel(pawn)}.");

                return;
            }

            if (!JaffaPrimtaUtility.IsEligibleForPrimtaImplantation(pawn))
            {
                return;
            }

            Hediff primta = HediffMaker.MakeHediff(
                GR_DefOf.SG1_JaffaPrimta,
                pawn);

            pawn.health.AddHediff(primta);
            initializedPawnThingIds.Add(pawnThingId);

            GR_Log.Message(
                $"Initialized generated Jaffa "
                + $"{JaffaPrimtaUtility.PawnDebugLabel(pawn)} "
                + "with an automatic Prim'ta.");
        }

        private static bool IsGeneratedJaffaPawnKind(PawnKindDef pawnKindDef)
        {
            return pawnKindDef == GR_DefOf.SG1_GoauldJaffaWarrior
                || pawnKindDef == GR_DefOf.SG1_GoauldJaffaGuard
                || pawnKindDef == GR_DefOf.SG1_GoauldJaffaFieldOfficer
                || pawnKindDef == GR_DefOf.SG1_GoauldSettlementJaffaWarrior
                || pawnKindDef == GR_DefOf.SG1_GoauldSettlementJaffaGuard
                || pawnKindDef == GR_DefOf.SG1_GoauldSettlementJaffaOfficer
                || pawnKindDef == GR_DefOf.SG1_FreeJaffaWarrior
                || pawnKindDef == GR_DefOf.SG1_FreeJaffaGuard
                || pawnKindDef == GR_DefOf.SG1_FreeJaffaTrader;
        }
    }
}
