using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using GateRimSG1.Goauld;
using RimWorld;
using Verse;

namespace GateRimSG1.Names
{
    /// <summary>
    /// Applies GateRim SG-1 cultural names once to newly generated pawns.
    ///
    /// Existing pawns are registered without renaming when this component is
    /// introduced into an older save. Starting player pawns are likewise kept
    /// intact so scenario-editor or player-selected names are respected.
    /// </summary>
    public class GameComponent_CulturalPawnNameManager : GameComponent
    {
        private const int ScanIntervalTicks = 60;
        private const int WorldScanIntervalTicks = 600;
        private const int MaxUniqueNameAttempts = 100;

        private List<string> processedPawnThingIds = new List<string>();
        private List<string> reservedNameKeys = new List<string>();
        private bool baselineInitialized;
        private int protectPlayerPawnsUntilTick = -1;

        private HashSet<string> processedPawnThingIdSet;
        private HashSet<string> reservedNameKeySet;

        public GameComponent_CulturalPawnNameManager(Game game)
        {
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Collections.Look(
                ref processedPawnThingIds,
                "culturalPawnNameProcessedThingIds",
                LookMode.Value);
            Scribe_Collections.Look(
                ref reservedNameKeys,
                "culturalPawnNameReservedKeys",
                LookMode.Value);
            Scribe_Values.Look(
                ref baselineInitialized,
                "culturalPawnNameBaselineInitialized",
                false);
            Scribe_Values.Look(
                ref protectPlayerPawnsUntilTick,
                "culturalPawnNameProtectPlayerPawnsUntilTick",
                -1);

            if (processedPawnThingIds == null)
            {
                processedPawnThingIds = new List<string>();
            }

            if (reservedNameKeys == null)
            {
                reservedNameKeys = new List<string>();
            }

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                RebuildRuntimeCaches();
            }
        }

        public override void StartedNewGame()
        {
            base.StartedNewGame();

            RebuildRuntimeCaches();
            baselineInitialized = true;
            // Starting pawns are registered immediately below. Future pawns,
            // including pawns spawned through developer tools while paused,
            // must remain eligible for cultural naming.
            protectPlayerPawnsUntilTick = -1;

            ScanKnownPawns(
                renameEligiblePawns: true,
                preserveCurrentPlayerPawns: true,
                includeWorldPawns: true);
        }

        public override void LoadedGame()
        {
            base.LoadedGame();

            RebuildRuntimeCaches();

            if (!baselineInitialized)
            {
                ScanKnownPawns(
                    renameEligiblePawns: false,
                    preserveCurrentPlayerPawns: true,
                    includeWorldPawns: true);
                baselineInitialized = true;

                GR_Log.Message(
                    "Registered existing pawns without renaming while enabling "
                    + "the cultural pawn-name system on an older save.");

                return;
            }

            ScanKnownPawns(
                renameEligiblePawns: true,
                preserveCurrentPlayerPawns: false,
                includeWorldPawns: true);
        }

        public override void GameComponentTick()
        {
            if (!baselineInitialized
                || Find.TickManager == null
                || Find.TickManager.TicksGame % ScanIntervalTicks != 0)
            {
                return;
            }

            int currentTick = Find.TickManager.TicksGame;

            ScanKnownPawns(
                renameEligiblePawns: true,
                preserveCurrentPlayerPawns: false,
                includeWorldPawns:
                    currentTick % WorldScanIntervalTicks == 0);
        }

        public void NotifyPawnSpawned(Pawn pawn)
        {
            if (!baselineInitialized || pawn == null || pawn.Destroyed)
            {
                return;
            }

            ProcessKnownPawn(
                pawn,
                renameEligiblePawns: true,
                preserveCurrentPlayerPawns: false,
                visitedThingIds: new HashSet<string>());
        }

        public Name GenerateUniqueName(
            CulturalPawnNameGroup group,
            Gender gender)
        {
            EnsureRuntimeCaches();

            Name fallback = null;

            for (int attempt = 0;
                attempt < MaxUniqueNameAttempts;
                attempt++)
            {
                Name candidate = CulturalPawnNameUtility.GenerateName(
                    group,
                    gender);

                if (candidate == null)
                {
                    return null;
                }

                fallback = candidate;
                string nameKey = candidate.ToStringFull;

                if (nameKey.NullOrEmpty()
                    || reservedNameKeySet.Contains(nameKey))
                {
                    continue;
                }

                ReserveNameKey(nameKey);
                return candidate;
            }

            if (fallback != null)
            {
                ReserveNameKey(fallback.ToStringFull);
            }

            return fallback;
        }

        public static GameComponent_CulturalPawnNameManager Current
        {
            get
            {
                return Verse.Current.Game
                    ?.GetComponent<GameComponent_CulturalPawnNameManager>();
            }
        }

        private void ScanKnownPawns(
            bool renameEligiblePawns,
            bool preserveCurrentPlayerPawns,
            bool includeWorldPawns)
        {
            HashSet<string> visitedThingIds = new HashSet<string>();

            if (Find.Maps != null)
            {
                for (int mapIndex = 0;
                    mapIndex < Find.Maps.Count;
                    mapIndex++)
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
                        ProcessKnownPawn(
                            mapPawns[pawnIndex],
                            renameEligiblePawns,
                            preserveCurrentPlayerPawns,
                            visitedThingIds);
                    }
                }
            }

            if (includeWorldPawns)
            {
                if (Find.FactionManager?.AllFactionsListForReading != null)
                {
                    List<Faction> factions =
                        Find.FactionManager.AllFactionsListForReading;

                    for (int factionIndex = 0;
                        factionIndex < factions.Count;
                        factionIndex++)
                    {
                        ProcessKnownPawn(
                            factions[factionIndex]?.leader,
                            renameEligiblePawns,
                            preserveCurrentPlayerPawns,
                            visitedThingIds);
                    }
                }

                foreach (Pawn worldPawn in EnumerateWorldPawns())
                {
                    ProcessKnownPawn(
                        worldPawn,
                        renameEligiblePawns,
                        preserveCurrentPlayerPawns,
                        visitedThingIds);
                }
            }

        }

        private void ProcessKnownPawn(
            Pawn pawn,
            bool renameEligiblePawns,
            bool preserveCurrentPlayerPawns,
            HashSet<string> visitedThingIds)
        {
            if (pawn == null || pawn.Destroyed)
            {
                return;
            }

            string pawnThingId = pawn.ThingID;

            if (pawnThingId.NullOrEmpty()
                || !visitedThingIds.Add(pawnThingId)
                || IsProcessed(pawnThingId))
            {
                return;
            }

            if (!renameEligiblePawns
                || preserveCurrentPlayerPawns && pawn.Faction?.IsPlayer == true)
            {
                RegisterExistingPawn(pawn);
                return;
            }

            CulturalPawnNameGroup group = ResolveNameGroup(pawn);

            if (group == CulturalPawnNameGroup.None)
            {
                RegisterProcessedPawn(pawnThingId);
                return;
            }

            GoauldSymbioteData symbioteData = null;

            if (IsAdultSymbioteHostKind(pawn.kindDef))
            {
                symbioteData = FindSymbioteData(pawn);

                if (symbioteData == null)
                {
                    // The host initializer runs on the same short cadence.
                    // Leave this pawn pending until its persistent symbiote
                    // identity exists instead of assigning two unrelated names.
                    return;
                }

                symbioteData.CaptureHostName(pawn);
            }

            Name generatedName = GenerateUniqueName(group, pawn.gender);

            if (generatedName == null)
            {
                RegisterProcessedPawn(pawnThingId);
                return;
            }

            pawn.Name = generatedName;

            if (symbioteData != null)
            {
                symbioteData.SetSymbioteName(generatedName.ToStringFull);
            }

            RegisterProcessedPawn(pawnThingId);

            GR_Log.Message(
                $"Assigned {group} cultural name {generatedName.ToStringFull} "
                + $"to {pawnThingId} ({pawn.kindDef?.defName ?? "no kind"}).");
        }

        private void RegisterExistingPawn(Pawn pawn)
        {
            if (pawn == null || pawn.ThingID.NullOrEmpty())
            {
                return;
            }

            RegisterProcessedPawn(pawn.ThingID);

            string existingName = pawn.Name?.ToStringFull;

            if (!existingName.NullOrEmpty())
            {
                ReserveNameKey(existingName);
            }
        }

        private void RegisterProcessedPawn(string pawnThingId)
        {
            EnsureRuntimeCaches();

            if (pawnThingId.NullOrEmpty()
                || !processedPawnThingIdSet.Add(pawnThingId))
            {
                return;
            }

            processedPawnThingIds.Add(pawnThingId);
        }

        private bool IsProcessed(string pawnThingId)
        {
            EnsureRuntimeCaches();
            return processedPawnThingIdSet.Contains(pawnThingId);
        }

        private void ReserveNameKey(string nameKey)
        {
            EnsureRuntimeCaches();

            if (nameKey.NullOrEmpty()
                || !reservedNameKeySet.Add(nameKey))
            {
                return;
            }

            reservedNameKeys.Add(nameKey);
        }

        private void EnsureRuntimeCaches()
        {
            if (processedPawnThingIdSet == null
                || reservedNameKeySet == null)
            {
                RebuildRuntimeCaches();
            }
        }

        private void RebuildRuntimeCaches()
        {
            processedPawnThingIdSet = new HashSet<string>(
                processedPawnThingIds ?? new List<string>());
            reservedNameKeySet = new HashSet<string>(
                reservedNameKeys ?? new List<string>());
        }

        private static CulturalPawnNameGroup ResolveNameGroup(Pawn pawn)
        {
            PawnKindDef kindDef = pawn?.kindDef;

            if (kindDef == GR_DefOf.SG1_GoauldHostCaste
                || kindDef == GR_DefOf.SG1_GoauldSystemLordHost
                || kindDef == GR_DefOf.SG1_GoauldQueen
                || kindDef == GR_DefOf.SG1_GoauldSymbiote)
            {
                return CulturalPawnNameGroup.Goauld;
            }

            if (kindDef == GR_DefOf.SG1_TokraVoluntaryHost
                || kindDef == GR_DefOf.SG1_TokraSymbiote)
            {
                return CulturalPawnNameGroup.Tokra;
            }

            if (kindDef == GR_DefOf.SG1_GoauldJaffaWarrior
                || kindDef == GR_DefOf.SG1_GoauldJaffaGuard
                || kindDef == GR_DefOf.SG1_GoauldSettlementJaffaWarrior
                || kindDef == GR_DefOf.SG1_GoauldSettlementJaffaGuard)
            {
                return CulturalPawnNameGroup.GoauldJaffa;
            }

            if (kindDef == GR_DefOf.SG1_FreeJaffaWarrior
                || kindDef == GR_DefOf.SG1_FreeJaffaGuard)
            {
                return CulturalPawnNameGroup.FreeJaffa;
            }

            FactionDef factionDef = pawn.Faction?.def;

            if (factionDef == GR_DefOf.SG1_Tokra)
            {
                return CulturalPawnNameGroup.Tokra;
            }

            if (factionDef == GR_DefOf.SG1_FreeJaffa)
            {
                return CulturalPawnNameGroup.FreeJaffa;
            }

            if (factionDef == GR_DefOf.SG1_PlayerSGCExpedition)
            {
                return CulturalPawnNameGroup.Tauri;
            }

            return CulturalPawnNameGroup.None;
        }

        private static bool IsAdultSymbioteHostKind(PawnKindDef pawnKindDef)
        {
            return pawnKindDef == GR_DefOf.SG1_GoauldHostCaste
                || pawnKindDef == GR_DefOf.SG1_GoauldSystemLordHost
                || pawnKindDef == GR_DefOf.SG1_TokraVoluntaryHost;
        }

        private static GoauldSymbioteData FindSymbioteData(Pawn pawn)
        {
            List<Hediff> hediffs = pawn?.health?.hediffSet?.hediffs;

            if (hediffs == null)
            {
                return null;
            }

            for (int index = 0; index < hediffs.Count; index++)
            {
                HediffWithComps withComps = hediffs[index] as HediffWithComps;
                HediffComp_GoauldSymbiote comp =
                    withComps?.GetComp<HediffComp_GoauldSymbiote>();

                if (comp?.SymbioteData != null)
                {
                    return comp.SymbioteData;
                }
            }

            return null;
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
