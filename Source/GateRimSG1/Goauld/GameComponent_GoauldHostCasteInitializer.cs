using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Initializes naturally generated Goa'uld host-caste pawns with one
    /// persistent adult Goa'uld symbiote identity.
    ///
    /// The host body remains biologically human. Possession is represented by
    /// SG1_GoauldHostSymbiote, matching the existing implantation and transfer
    /// architecture instead of forcing the legacy xenotype prototype.
    ///
    /// Initialization is recorded once per pawn ThingID. Removing the symbiote
    /// later must not create an artificial replacement.
    /// </summary>
    public class GameComponent_GoauldHostCasteInitializer : GameComponent
    {
        private const int ScanIntervalTicks = 60;

        /// <summary>
        /// Vanilla chronic biological ailments that should not survive the
        /// initial attachment of an adult Goa'uld symbiote to a generated
        /// host-caste body.
        ///
        /// Keep this deliberately narrower than a full health reset: scars,
        /// missing body parts and ordinary combat injuries remain untouched.
        /// </summary>
        private static readonly HashSet<string> GeneratedHostAilmentsToHeal =
            new HashSet<string>
            {
                "BadBack",
                "Frail",
                "Cataract",
                "Blindness",
                "HearingLoss",
                "Dementia",
                "Alzheimers",
                "Asthma",
                "ArteryBlockage",
                "Carcinoma",
                "Cirrhosis",
                "OrganDecay"
            };

        private List<string> initializedPawnThingIds = new List<string>();

        public GameComponent_GoauldHostCasteInitializer(Game game)
        {
        }

        public static GameComponent_GoauldHostCasteInitializer Current
        {
            get
            {
                return Verse.Current.Game
                    ?.GetComponent<GameComponent_GoauldHostCasteInitializer>();
            }
        }

        public void NotifyPawnSpawned(Pawn pawn)
        {
            TryInitializeHostPawn(pawn);
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Collections.Look(
                ref initializedPawnThingIds,
                "initializedGoauldHostCastePawnThingIds",
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

            InitializeFactionLeaders();

            for (int mapIndex = 0; mapIndex < Find.Maps.Count; mapIndex++)
            {
                UpdateMap(Find.Maps[mapIndex]);
            }
        }

        private void InitializeFactionLeaders()
        {
            if (Find.FactionManager?.AllFactionsListForReading == null)
            {
                return;
            }

            for (int factionIndex = 0;
                factionIndex < Find.FactionManager.AllFactionsListForReading.Count;
                factionIndex++)
            {
                Faction faction =
                    Find.FactionManager.AllFactionsListForReading[factionIndex];

                if (faction?.def == GR_DefOf.SG1_GoauldSystemLordPrototype)
                {
                    TryInitializeHostPawn(faction.leader);
                }
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
                TryInitializeHostPawn(pawns[pawnIndex]);
            }
        }

        private void TryInitializeHostPawn(Pawn pawn)
        {
            if (pawn == null
                || pawn.Destroyed
                || pawn.Dead
                || pawn.health == null
                || !IsGeneratedGoauldHostKind(pawn.kindDef))
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
                HealGeneratedHostAilments(pawn);
                initializedPawnThingIds.Add(pawnThingId);

                GR_Log.Message(
                    $"Registered existing generated Goa'uld host "
                    + $"{PawnDebugLabel(pawn)} without creating a duplicate symbiote.");

                return;
            }

            Hediff activeHostState = HediffMaker.MakeHediff(
                GR_DefOf.SG1_GoauldHostSymbiote,
                pawn);

            HediffComp_GoauldSymbiote targetComp =
                FindPersistentSymbioteComp(activeHostState);

            if (targetComp == null)
            {
                GR_Log.Error(
                    "Cannot initialize generated Goa'uld host: "
                    + "SG1_GoauldHostSymbiote is missing "
                    + "HediffComp_GoauldSymbiote.");

                return;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            GoauldSymbioteData data = GoauldSymbioteData.CreateFree(
                currentTick,
                GoauldSymbioteOrigin.Goauld);

            if (pawn.kindDef == GR_DefOf.SG1_GoauldSystemLordHost
                && !data.TryInitializeGeneratedSystemLordHost(
                    pawn,
                    currentTick))
            {
                GR_Log.Warning(
                    "Generated Goa'uld System Lord retained its visible name, "
                    + "but a distinct historical host name could not be "
                    + $"prepared for {PawnDebugLabel(pawn)}.");
            }

            targetComp.InitializeWithTransferredData(data);
            pawn.health.AddHediff(activeHostState);
            HealGeneratedHostAilments(pawn);

            initializedPawnThingIds.Add(pawnThingId);

            GR_Log.Message(
                $"Initialized generated Goa'uld host {PawnDebugLabel(pawn)} "
                + $"with persistent symbiote {data.SymbioteId}; "
                + $"symbioteName={data.SymbioteName}, "
                + $"hostName={data.HostName}.");
        }

        private static void HealGeneratedHostAilments(Pawn pawn)
        {
            List<Hediff> hediffs = pawn?.health?.hediffSet?.hediffs;

            if (hediffs == null)
            {
                return;
            }

            int removedCount = 0;

            for (int index = hediffs.Count - 1; index >= 0; index--)
            {
                Hediff hediff = hediffs[index];
                string defName = hediff?.def?.defName;

                if (defName.NullOrEmpty()
                    || !GeneratedHostAilmentsToHeal.Contains(defName))
                {
                    continue;
                }

                pawn.health.RemoveHediff(hediff);
                removedCount++;
            }

            if (removedCount > 0)
            {
                GR_Log.Message(
                    $"Healed {removedCount} generated chronic ailment(s) "
                    + $"after Goa'uld host initialization for "
                    + $"{PawnDebugLabel(pawn)}.");
            }
        }

        private static bool HasAdultSymbioteState(Pawn pawn)
        {
            if (pawn?.health?.hediffSet?.hediffs == null)
            {
                return false;
            }

            for (int index = 0;
                index < pawn.health.hediffSet.hediffs.Count;
                index++)
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

        private static bool IsGeneratedGoauldHostKind(PawnKindDef pawnKindDef)
        {
            return pawnKindDef == GR_DefOf.SG1_GoauldHostCaste
                || pawnKindDef == GR_DefOf.SG1_GoauldSystemLordHost;
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
