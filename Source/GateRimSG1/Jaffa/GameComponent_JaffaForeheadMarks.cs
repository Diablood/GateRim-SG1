using System.Collections.Generic;
using GateRimSG1.Goauld;
using RimWorld;
using Verse;

namespace GateRimSG1.Jaffa
{
    /// <summary>
    /// Stores intrinsic forehead marks independently from xenotypes and genes.
    ///
    /// Existing technical mark genes remain declared only as migration
    /// placeholders. When an affected pawn is encountered on a map, the gene
    /// is converted into intrinsic data and removed from the pawn.
    /// </summary>
    public class GameComponent_JaffaForeheadMarks : GameComponent
    {
        private const int ScanIntervalTicks = 60;

        private List<JaffaForeheadMarkData> foreheadMarks =
            new List<JaffaForeheadMarkData>();

        private List<string> initializedCompatibleJaffaPawnThingIds =
            new List<string>();

        [Unsaved(false)]
        private Dictionary<string, JaffaForeheadMarkData> marksByPawnThingId;

        public GameComponent_JaffaForeheadMarks(Game game)
        {
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Collections.Look(
                ref foreheadMarks,
                "jaffaForeheadMarks",
                LookMode.Deep);

            Scribe_Collections.Look(
                ref initializedCompatibleJaffaPawnThingIds,
                "initializedCompatibleJaffaForeheadMarkPawnThingIds",
                LookMode.Value);

            if (foreheadMarks == null)
            {
                foreheadMarks = new List<JaffaForeheadMarkData>();
            }

            if (initializedCompatibleJaffaPawnThingIds == null)
            {
                initializedCompatibleJaffaPawnThingIds = new List<string>();
            }

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                RebuildCache();
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

        public JaffaForeheadMarkDef MarkFor(Pawn pawn)
        {
            string pawnThingId = pawn?.ThingID;

            if (string.IsNullOrEmpty(pawnThingId))
            {
                return null;
            }

            EnsureCache();

            return marksByPawnThingId.TryGetValue(
                pawnThingId,
                out JaffaForeheadMarkData data)
                    ? data.markDef
                    : null;
        }

        public bool SetMark(
            Pawn pawn,
            JaffaForeheadMarkDef markDef)
        {
            string pawnThingId = pawn?.ThingID;

            if (string.IsNullOrEmpty(pawnThingId) || markDef == null)
            {
                return false;
            }

            EnsureCache();

            if (marksByPawnThingId.TryGetValue(
                pawnThingId,
                out JaffaForeheadMarkData existingData))
            {
                if (existingData.markDef == markDef)
                {
                    return false;
                }

                existingData.markDef = markDef;
            }
            else
            {
                JaffaForeheadMarkData newData = new JaffaForeheadMarkData(
                    pawnThingId,
                    markDef);

                foreheadMarks.Add(newData);
                marksByPawnThingId.Add(pawnThingId, newData);
            }

            JaffaForeheadMarkUtility.NotifyGraphicsDirty(pawn);

            GR_Log.Message(
                $"Assigned intrinsic Jaffa forehead mark {markDef.defName} "
                + $"to {JaffaForeheadMarkUtility.PawnDebugLabel(pawn)}.");

            return true;
        }

        public bool RemoveMark(Pawn pawn)
        {
            string pawnThingId = pawn?.ThingID;

            if (string.IsNullOrEmpty(pawnThingId))
            {
                return false;
            }

            EnsureCache();

            if (!marksByPawnThingId.TryGetValue(
                pawnThingId,
                out JaffaForeheadMarkData existingData))
            {
                return false;
            }

            foreheadMarks.Remove(existingData);
            marksByPawnThingId.Remove(pawnThingId);
            JaffaForeheadMarkUtility.NotifyGraphicsDirty(pawn);

            GR_Log.Message(
                $"Removed intrinsic Jaffa forehead mark from "
                + $"{JaffaForeheadMarkUtility.PawnDebugLabel(pawn)}.");

            return true;
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
                TryMigrateLegacyTechnicalGenes(pawns[pawnIndex]);
                TryInitializeCompatibleJaffa(pawns[pawnIndex]);
            }
        }

        private void TryInitializeCompatibleJaffa(Pawn pawn)
        {
            if (pawn == null
                || pawn.Destroyed
                || pawn.Dead
                || !JaffaPrimtaUtility.IsCompatibleJaffa(pawn))
            {
                return;
            }

            string pawnThingId = pawn.ThingID;

            if (string.IsNullOrEmpty(pawnThingId)
                || initializedCompatibleJaffaPawnThingIds.Contains(pawnThingId))
            {
                return;
            }

            initializedCompatibleJaffaPawnThingIds.Add(pawnThingId);

            if (MarkFor(pawn) != null)
            {
                return;
            }

            SetMark(
                pawn,
                GoauldSystemLordDomainUtility.MarkFor(
                    pawn.Faction,
                    GoauldJaffaMarkRank.Ordinary));
        }

        private void TryMigrateLegacyTechnicalGenes(Pawn pawn)
        {
            List<Gene> genes = pawn?.genes?.GenesListForReading;

            if (genes == null || genes.Count == 0)
            {
                return;
            }

            JaffaForeheadMarkDef migratedMark = null;
            List<Gene> legacyGenes = null;

            for (int geneIndex = 0; geneIndex < genes.Count; geneIndex++)
            {
                Gene gene = genes[geneIndex];
                JaffaForeheadMarkDef candidate = LegacyIntrinsicMarkFor(gene?.def);

                if (candidate == null)
                {
                    continue;
                }

                if (legacyGenes == null)
                {
                    legacyGenes = new List<Gene>();
                }

                legacyGenes.Add(gene);

                if (migratedMark == null
                    || LegacyPriority(candidate) > LegacyPriority(migratedMark))
                {
                    migratedMark = candidate;
                }
            }

            if (legacyGenes == null || legacyGenes.Count == 0)
            {
                return;
            }

            if (MarkFor(pawn) == null && migratedMark != null)
            {
                SetMark(pawn, migratedMark);
            }

            for (int geneIndex = legacyGenes.Count - 1; geneIndex >= 0; geneIndex--)
            {
                pawn.genes.RemoveGene(legacyGenes[geneIndex]);
            }

            JaffaForeheadMarkUtility.NotifyGraphicsDirty(pawn);

            GR_Log.Message(
                $"Migrated {legacyGenes.Count} technical Jaffa forehead-mark "
                + $"gene(s) for {JaffaForeheadMarkUtility.PawnDebugLabel(pawn)}.");
        }

        private static JaffaForeheadMarkDef LegacyIntrinsicMarkFor(
            GeneDef legacyGene)
        {
            if (legacyGene == GR_DefOf.SG1_JaffaForeheadMark_GenericGold)
            {
                return GR_DefOf.SG1_JaffaForeheadMark_GenericGoldIntrinsic;
            }

            if (legacyGene == GR_DefOf.SG1_JaffaForeheadMark_GenericSilver)
            {
                return GR_DefOf.SG1_JaffaForeheadMark_GenericSilverIntrinsic;
            }

            if (legacyGene == GR_DefOf.SG1_JaffaForeheadMark_Generic)
            {
                return GR_DefOf.SG1_JaffaForeheadMark_GenericIntrinsic;
            }

            return null;
        }

        private static int LegacyPriority(JaffaForeheadMarkDef markDef)
        {
            if (markDef == GR_DefOf.SG1_JaffaForeheadMark_GenericGoldIntrinsic)
            {
                return 3;
            }

            if (markDef == GR_DefOf.SG1_JaffaForeheadMark_GenericSilverIntrinsic)
            {
                return 2;
            }

            if (markDef == GR_DefOf.SG1_JaffaForeheadMark_GenericIntrinsic)
            {
                return 1;
            }

            return 0;
        }

        private void EnsureCache()
        {
            if (marksByPawnThingId == null)
            {
                RebuildCache();
            }
        }

        private void RebuildCache()
        {
            marksByPawnThingId =
                new Dictionary<string, JaffaForeheadMarkData>();

            for (int index = foreheadMarks.Count - 1; index >= 0; index--)
            {
                JaffaForeheadMarkData data = foreheadMarks[index];

                if (data == null
                    || string.IsNullOrEmpty(data.pawnThingId)
                    || data.markDef == null)
                {
                    foreheadMarks.RemoveAt(index);
                    continue;
                }

                marksByPawnThingId[data.pawnThingId] = data;
            }
        }
    }
}
