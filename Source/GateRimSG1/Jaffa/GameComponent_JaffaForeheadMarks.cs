using System.Collections.Generic;
using GateRimSG1.Goauld;
using Verse;

namespace GateRimSG1.Jaffa
{
    /// <summary>
    /// Stores intrinsic forehead marks independently from xenotypes and genes.
    ///
    /// Automatic assignment is restricted to Jaffa attached to a Goa'uld
    /// System Lord domain. Free Jaffa remain unmarked unless a mark is
    /// applied manually or migrated from earlier save data.
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
                TryInitializeCompatibleJaffa(pawns[pawnIndex]);
            }
        }

        private void TryInitializeCompatibleJaffa(Pawn pawn)
        {
            if (pawn == null
                || pawn.Destroyed
                || pawn.Dead
                || !JaffaPrimtaUtility.IsCompatibleJaffa(pawn)
                || !GoauldSystemLordDomainUtility.HasAssignedDomain(
                    pawn.Faction))
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
