using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// First Tok'ra therapeutic-hosting prototype.
    ///
    /// Active Tok'ra symbiotes cure a deliberately narrow configured list of
    /// serious pathologies. Injuries, scars and unlisted diseases remain
    /// untouched so later milestones can refine balance and XML configuration.
    /// </summary>
    public class GameComponent_TokraTherapeuticHosting : GameComponent
    {
        private const int ScanIntervalTicks = 60;

        private static readonly HashSet<string> CurablePathologyDefNames
            = new HashSet<string>(StringComparer.Ordinal)
            {
                "Carcinoma",
                "Infection",
                "Plague",
                "Malaria",
                "Flu",
                "SleepingSickness",
                "BloodRot"
            };

        public GameComponent_TokraTherapeuticHosting(Game game)
        {
        }

        public static bool HasConfiguredCurablePathology(Pawn pawn)
        {
            return !string.IsNullOrEmpty(
                GetConfiguredCurablePathologyLabels(pawn));
        }

        public static string GetConfiguredCurablePathologyLabels(Pawn pawn)
        {
            List<Hediff> hediffs = pawn?.health?.hediffSet?.hediffs;

            if (hediffs == null)
            {
                return string.Empty;
            }

            List<string> labels = new List<string>();

            for (int index = 0; index < hediffs.Count; index++)
            {
                Hediff hediff = hediffs[index];
                string defName = hediff?.def?.defName;

                if (string.IsNullOrEmpty(defName)
                    || !CurablePathologyDefNames.Contains(defName))
                {
                    continue;
                }

                labels.Add(hediff.LabelCap.ToString());
            }

            return string.Join(", ", labels.ToArray());
        }

        public override void GameComponentTick()
        {
            TickManager tickManager = Find.TickManager;

            if (tickManager == null
                || tickManager.TicksGame % ScanIntervalTicks != 0
                || Find.Maps == null)
            {
                return;
            }

            for (int mapIndex = 0; mapIndex < Find.Maps.Count; mapIndex++)
            {
                TreatMap(Find.Maps[mapIndex]);
            }
        }

        private static void TreatMap(Map map)
        {
            IReadOnlyList<Pawn> pawns = map?.mapPawns?.AllPawnsSpawned;

            if (pawns == null)
            {
                return;
            }

            for (int pawnIndex = 0; pawnIndex < pawns.Count; pawnIndex++)
            {
                TryTreatActiveTokraHost(pawns[pawnIndex]);
            }
        }

        private static void TryTreatActiveTokraHost(Pawn pawn)
        {
            if (pawn == null
                || pawn.Destroyed
                || pawn.Dead
                || pawn.health?.hediffSet?.hediffs == null)
            {
                return;
            }

            HediffComp_GoauldSymbiote symbioteComp
                = FindActiveTokraSymbioteComp(pawn);

            if (symbioteComp?.SymbioteData?.Origin != GoauldSymbioteOrigin.Tokra)
            {
                return;
            }

            List<string> removedLabels = new List<string>();
            List<Hediff> hediffs = pawn.health.hediffSet.hediffs;

            for (int index = hediffs.Count - 1; index >= 0; index--)
            {
                Hediff hediff = hediffs[index];
                string defName = hediff?.def?.defName;

                if (string.IsNullOrEmpty(defName)
                    || !CurablePathologyDefNames.Contains(defName))
                {
                    continue;
                }

                removedLabels.Add(hediff.LabelCap.ToString());
                pawn.health.RemoveHediff(hediff);
            }

            if (removedLabels.Count == 0)
            {
                return;
            }

            string healedPathologies = string.Join(", ", removedLabels.ToArray());

            GR_Log.Message(
                $"Tok'ra therapeutic hosting removed {healedPathologies} "
                + $"from {PawnDebugLabel(pawn)} for symbiote "
                + $"{symbioteComp.SymbioteData.SymbioteId}.");

            Messages.Message(
                "GR_TokraTherapeuticHosting_Healed".Translate(
                    pawn.LabelShortCap,
                    healedPathologies),
                pawn,
                MessageTypeDefOf.PositiveEvent,
                historical: true);
        }

        private static HediffComp_GoauldSymbiote FindActiveTokraSymbioteComp(
            Pawn pawn)
        {
            List<Hediff> hediffs = pawn.health.hediffSet.hediffs;

            for (int index = 0; index < hediffs.Count; index++)
            {
                Hediff hediff = hediffs[index];

                if (hediff.def != GR_DefOf.SG1_GoauldHostSymbiote)
                {
                    continue;
                }

                HediffWithComps withComps = hediff as HediffWithComps;
                HediffComp_GoauldSymbiote comp
                    = withComps?.GetComp<HediffComp_GoauldSymbiote>();

                if (comp?.SymbioteData?.Origin == GoauldSymbioteOrigin.Tokra)
                {
                    return comp;
                }
            }

            return null;
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
