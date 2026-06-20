using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Initializes generated Tok'ra voluntary-host prototype pawns.
    ///
    /// The same PawnKindDef supports developer-spawned prototypes, peaceful
    /// visitors, therapeutic escorts, medical-support escorts and the internal
    /// leader of the persistent hidden Tok'ra world faction. The first scan
    /// attaches one active Tok'ra symbiote with a persistent identity.
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

        public static GameComponent_TokraHostPrototypeInitializer Current
        {
            get
            {
                return Verse.Current.Game
                    ?.GetComponent<GameComponent_TokraHostPrototypeInitializer>();
            }
        }

        public void NotifyPawnSpawned(Pawn pawn)
        {
            TryInitializePrototypePawn(pawn);
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

            InitializeFactionLeader();

            for (int mapIndex = 0; mapIndex < Find.Maps.Count; mapIndex++)
            {
                UpdateMap(Find.Maps[mapIndex]);
            }
        }

        private void InitializeFactionLeader()
        {
            Faction tokraFaction = Find.FactionManager?.FirstFactionOfDef(
                GR_DefOf.SG1_Tokra);

            TryInitializePrototypePawn(tokraFaction?.leader);
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

            if (string.IsNullOrEmpty(pawnThingId))
            {
                return;
            }

            HediffComp_GoauldSymbiote existingComp
                = FindExistingPersistentSymbioteComp(pawn);

            if (existingComp != null)
            {
                GoauldSymbioteData existingData = existingComp.SymbioteData;
                bool needsMigration = existingData != null
                    && existingData.HostIdentitySource
                        == TokraHostIdentitySource.Unknown;

                bool alreadyRegistered = initializedPawnThingIds.Contains(
                    pawnThingId);

                if (alreadyRegistered && !needsMigration)
                {
                    return;
                }

                bool migrated = needsMigration
                    && existingData.TryInitializeGeneratedPreJoinedHost(
                        pawn,
                        Find.TickManager?.TicksGame ?? 0);

                if (!alreadyRegistered)
                {
                    initializedPawnThingIds.Add(pawnThingId);
                }

                if (migrated)
                {
                    GR_Log.Message(
                        $"Migrated pre-joined Tok'ra {PawnDebugLabel(pawn)} "
                        + "to a distinct generated host identity.");
                }
                else if (!alreadyRegistered)
                {
                    GR_Log.Message(
                        $"Registered existing Tok'ra voluntary-host prototype "
                        + $"{PawnDebugLabel(pawn)} without creating a duplicate symbiote.");
                }

                return;
            }

            if (initializedPawnThingIds.Contains(pawnThingId))
            {
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

            GoauldSymbioteData data = GoauldSymbioteData.CreatePreJoinedTokra(
                pawn,
                Find.TickManager?.TicksGame ?? 0);

            targetComp.InitializeWithTransferredData(data);
            pawn.health.AddHediff(activeHostState);

            initializedPawnThingIds.Add(pawnThingId);

            GR_Log.Message(
                $"Initialized Tok'ra voluntary-host prototype "
                + $"{PawnDebugLabel(pawn)} with symbiote {data.SymbioteId} "
                + $"and generated host origin "
                + $"{data.GeneratedHostOrigin?.defName ?? "<none>"}.");

            Messages.Message(
                "GR_TokraHostPrototype_Initialized".Translate(
                    pawn.LabelShortCap,
                    data.SymbioteId),
                pawn,
                MessageTypeDefOf.PositiveEvent,
                historical: true);
        }

        private static HediffComp_GoauldSymbiote
            FindExistingPersistentSymbioteComp(Pawn pawn)
        {
            if (pawn?.health?.hediffSet?.hediffs == null)
            {
                return null;
            }

            for (int index = 0; index < pawn.health.hediffSet.hediffs.Count; index++)
            {
                Hediff hediff = pawn.health.hediffSet.hediffs[index];

                if (hediff.def != GR_DefOf.SG1_GoauldRecentImplantation
                    && hediff.def != GR_DefOf.SG1_GoauldHostSymbiote)
                {
                    continue;
                }

                HediffComp_GoauldSymbiote comp
                    = FindPersistentSymbioteComp(hediff);

                if (comp != null)
                {
                    return comp;
                }
            }

            return null;
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
