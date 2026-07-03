using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Reconciles persistent naquadah traces for current and existing saves.
    ///
    /// Immediate lifecycle hooks cover new adult symbiotes and Prim'ta. This
    /// component remains the migration and safety net for map pawns, caravans,
    /// faction leaders and world pawns that already carried those states before
    /// this milestone was introduced.
    /// </summary>
    public class GameComponent_NaquadahTraceReconciler : GameComponent
    {
        private const int MapScanIntervalTicks = 600;
        private const int WorldScanIntervalTicks = 60000;

        public GameComponent_NaquadahTraceReconciler(Game game)
        {
        }

        public static GameComponent_NaquadahTraceReconciler Current
        {
            get
            {
                return Verse.Current.Game
                    ?.GetComponent<GameComponent_NaquadahTraceReconciler>();
            }
        }

        public override void StartedNewGame()
        {
            base.StartedNewGame();
            ReconcileKnownPawns(
                includeWorldPawns: true,
                writeSummaryLog: true);
        }

        public override void LoadedGame()
        {
            base.LoadedGame();
            ReconcileKnownPawns(
                includeWorldPawns: true,
                writeSummaryLog: true);
        }

        public override void GameComponentTick()
        {
            if (Find.TickManager == null
                || Find.TickManager.TicksGame % MapScanIntervalTicks != 0)
            {
                return;
            }

            int currentTick = Find.TickManager.TicksGame;
            ReconcileKnownPawns(
                includeWorldPawns:
                    currentTick % WorldScanIntervalTicks == 0,
                writeSummaryLog: false);
        }

        public void NotifyPawnSpawned(Pawn pawn)
        {
            TryReconcilePawn(pawn);
        }

        public int ReconcileKnownPawns(
            bool includeWorldPawns,
            bool writeSummaryLog)
        {
            int addedCount = 0;
            HashSet<string> visitedThingIds = new HashSet<string>();

            if (Find.Maps != null)
            {
                for (int mapIndex = 0; mapIndex < Find.Maps.Count; mapIndex++)
                {
                    IReadOnlyList<Pawn> mapPawns = Find.Maps[mapIndex]
                        ?.mapPawns
                        ?.AllPawnsSpawned;

                    if (mapPawns == null)
                    {
                        continue;
                    }

                    for (int pawnIndex = 0;
                        pawnIndex < mapPawns.Count;
                        pawnIndex++)
                    {
                        addedCount += ProcessKnownPawn(
                            mapPawns[pawnIndex],
                            visitedThingIds);
                    }
                }
            }

            IEnumerable<Pawn> playerPawns = PawnsFinder
                .AllMapsCaravansAndTravellingTransporters_Alive_OfPlayerFaction;

            if (playerPawns != null)
            {
                foreach (Pawn pawn in playerPawns)
                {
                    addedCount += ProcessKnownPawn(pawn, visitedThingIds);
                }
            }

            if (includeWorldPawns)
            {
                List<Faction> factions = Find.FactionManager
                    ?.AllFactionsListForReading;

                if (factions != null)
                {
                    for (int factionIndex = 0;
                        factionIndex < factions.Count;
                        factionIndex++)
                    {
                        addedCount += ProcessKnownPawn(
                            factions[factionIndex]?.leader,
                            visitedThingIds);
                    }
                }

                foreach (Pawn worldPawn in EnumerateWorldPawns())
                {
                    addedCount += ProcessKnownPawn(
                        worldPawn,
                        visitedThingIds);
                }
            }

            if (writeSummaryLog)
            {
                GR_Log.Message(
                    "Reconciled persistent biological naquadah traces; "
                    + $"newMarkers={addedCount}, "
                    + $"includeWorldPawns={includeWorldPawns}.");
            }

            return addedCount;
        }

        private static int ProcessKnownPawn(
            Pawn pawn,
            HashSet<string> visitedThingIds)
        {
            if (pawn == null || pawn.Destroyed)
            {
                return 0;
            }

            string pawnThingId = pawn.ThingID;

            if (pawnThingId.NullOrEmpty()
                || !visitedThingIds.Add(pawnThingId))
            {
                return 0;
            }

            return TryReconcilePawn(pawn) ? 1 : 0;
        }

        private static bool TryReconcilePawn(Pawn pawn)
        {
            if (!NaquadahTraceUtility.ShouldCarryPersistentTrace(pawn))
            {
                return false;
            }

            return NaquadahTraceUtility.EnsurePersistentTrace(
                pawn,
                NaquadahTraceUtility.ActiveTraceSourceLabel(pawn));
        }

        private static IEnumerable<Pawn> EnumerateWorldPawns()
        {
            object worldPawns = Find.WorldPawns;

            if (worldPawns == null)
            {
                yield break;
            }

            IEnumerable pawnCollection = TryGetPawnCollection(
                worldPawns,
                "AllPawnsAliveOrDead")
                ?? TryGetPawnCollection(worldPawns, "AllPawnsAlive");

            if (pawnCollection == null)
            {
                yield break;
            }

            foreach (object value in pawnCollection)
            {
                Pawn pawn = value as Pawn;

                if (pawn != null)
                {
                    yield return pawn;
                }
            }
        }

        private static IEnumerable TryGetPawnCollection(
            object worldPawns,
            string propertyName)
        {
            PropertyInfo property = worldPawns.GetType().GetProperty(
                propertyName,
                BindingFlags.Instance | BindingFlags.Public);

            return property?.GetValue(worldPawns, null) as IEnumerable;
        }
    }
}
