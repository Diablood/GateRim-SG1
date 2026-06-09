using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// RimWorld-oriented Tok'ra therapeutic-hosting prototype.
    ///
    /// Active Tok'ra symbiotes remove biological conditions that vanilla marks
    /// as curable by an item and progressively regenerate non-permanent
    /// injuries. Permanent scars, missing body parts, implants, addictions,
    /// dependencies and GateRim SG-1 state Hediffs remain untouched.
    /// </summary>
    public class GameComponent_TokraTherapeuticHosting : GameComponent
    {
        private const int ScanIntervalTicks = 60;
        private const float InjuryHealingPerScan = 0.05f;

        public GameComponent_TokraTherapeuticHosting(Game game)
        {
        }

        /// <summary>
        /// Kept for compatibility with the existing therapeutic-implantation
        /// action. The milestone now accepts any treatable biological condition
        /// rather than a small hard-coded pathology list.
        /// </summary>
        public static bool HasConfiguredCurablePathology(Pawn pawn)
        {
            return !string.IsNullOrEmpty(
                GetConfiguredCurablePathologyLabels(pawn));
        }

        /// <summary>
        /// Returns whether the pawn has at least one non-traumatic biological
        /// condition suitable for a narrative Tok'ra therapeutic offer. Recent
        /// injuries are healed by active hosts but do not trigger an envoy
        /// incident on their own.
        /// </summary>
        public static bool HasSeriousTherapeuticNeed(Pawn pawn)
        {
            return !string.IsNullOrEmpty(
                GetSeriousTherapeuticNeedLabels(pawn));
        }

        /// <summary>
        /// Formats the non-traumatic biological conditions that can justify a
        /// Tok'ra therapeutic-opportunity incident.
        /// </summary>
        public static string GetSeriousTherapeuticNeedLabels(Pawn pawn)
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

                if (!IsSeriousTherapeuticNeed(hediff))
                {
                    continue;
                }

                labels.Add(hediff.LabelCap.ToString());
            }

            return FormatLabels(labels);
        }

        /// <summary>
        /// Provides a stable ordering score when several colonists could
        /// receive the same rare therapeutic opportunity.
        /// </summary>
        public static float GetSeriousTherapeuticNeedScore(Pawn pawn)
        {
            List<Hediff> hediffs = pawn?.health?.hediffSet?.hediffs;

            if (hediffs == null)
            {
                return 0f;
            }

            float score = 0f;

            for (int index = 0; index < hediffs.Count; index++)
            {
                Hediff hediff = hediffs[index];

                if (IsSeriousTherapeuticNeed(hediff))
                {
                    score += Math.Max(0.1f, hediff.Severity);
                }
            }

            return score;
        }

        /// <summary>
        /// Kept for compatibility with the existing therapeutic-implantation
        /// confirmation dialog.
        /// </summary>
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

                if (!IsTreatableCondition(hediff))
                {
                    continue;
                }

                labels.Add(hediff.LabelCap.ToString());
            }

            return FormatLabels(labels);
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

            List<string> completedLabels = new List<string>();
            List<Hediff> hediffs = pawn.health.hediffSet.hediffs;

            for (int index = hediffs.Count - 1; index >= 0; index--)
            {
                Hediff hediff = hediffs[index];

                if (!IsTreatableCondition(hediff))
                {
                    continue;
                }

                Hediff_Injury injury = hediff as Hediff_Injury;

                if (injury != null)
                {
                    TryRegenerateInjury(pawn, injury, completedLabels);
                    continue;
                }

                completedLabels.Add(hediff.LabelCap.ToString());
                pawn.health.RemoveHediff(hediff);
            }

            if (completedLabels.Count == 0)
            {
                return;
            }

            string healedConditions = FormatLabels(completedLabels);

            GR_Log.Message(
                $"Tok'ra therapeutic hosting healed {healedConditions} "
                + $"for {PawnDebugLabel(pawn)} with symbiote "
                + $"{symbioteComp.SymbioteData.SymbioteId}.");

            Messages.Message(
                "GR_TokraTherapeuticHosting_Healed".Translate(
                    pawn.LabelShortCap,
                    healedConditions),
                pawn,
                MessageTypeDefOf.PositiveEvent,
                historical: true);
        }

        private static bool IsTreatableCondition(Hediff hediff)
        {
            if (hediff == null
                || hediff.def == null
                || !hediff.Visible)
            {
                return false;
            }

            string defName = hediff.def.defName;

            if (string.IsNullOrEmpty(defName)
                || defName.StartsWith("SG1_", StringComparison.Ordinal)
                || IsExcludedDefName(defName))
            {
                return false;
            }

            if (hediff is Hediff_MissingPart
                || hediff is Hediff_AddedPart
                || hediff is Hediff_Implant
                || hediff is Hediff_Addiction)
            {
                return false;
            }

            Hediff_Injury injury = hediff as Hediff_Injury;

            if (injury != null)
            {
                return !IsPermanentInjury(injury);
            }

            return hediff.def.isBad && hediff.def.everCurableByItem;
        }

        private static bool IsSeriousTherapeuticNeed(Hediff hediff)
        {
            return IsTreatableCondition(hediff)
                && !(hediff is Hediff_Injury);
        }

        private static bool IsExcludedDefName(string defName)
        {
            return defName.IndexOf("Pregnan", StringComparison.OrdinalIgnoreCase)
                    >= 0
                || defName.EndsWith("Withdrawal", StringComparison.OrdinalIgnoreCase)
                || defName.EndsWith("Dependency", StringComparison.OrdinalIgnoreCase)
                || defName.EndsWith("Addiction", StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsPermanentInjury(Hediff_Injury injury)
        {
            HediffComp_GetsPermanent permanentComp
                = injury.TryGetComp<HediffComp_GetsPermanent>();

            return permanentComp?.IsPermanent == true;
        }

        private static void TryRegenerateInjury(
            Pawn pawn,
            Hediff_Injury injury,
            List<string> completedLabels)
        {
            string injuryLabel = injury.LabelCap.ToString();

            injury.Heal(InjuryHealingPerScan);

            if (injury.Severity > 0f)
            {
                return;
            }

            if (pawn.health.hediffSet.hediffs.Contains(injury))
            {
                pawn.health.RemoveHediff(injury);
            }

            completedLabels.Add(injuryLabel);
        }

        private static string FormatLabels(List<string> labels)
        {
            if (labels == null || labels.Count == 0)
            {
                return string.Empty;
            }

            Dictionary<string, int> counts
                = new Dictionary<string, int>(StringComparer.Ordinal);
            List<string> orderedLabels = new List<string>();

            for (int index = 0; index < labels.Count; index++)
            {
                string label = labels[index];

                if (string.IsNullOrEmpty(label))
                {
                    continue;
                }

                int count;

                if (counts.TryGetValue(label, out count))
                {
                    counts[label] = count + 1;
                    continue;
                }

                counts.Add(label, 1);
                orderedLabels.Add(label);
            }

            List<string> formattedLabels = new List<string>();

            for (int index = 0; index < orderedLabels.Count; index++)
            {
                string label = orderedLabels[index];
                int count = counts[label];

                formattedLabels.Add(
                    count > 1
                        ? $"{label} x{count}"
                        : label);
            }

            return string.Join(", ", formattedLabels.ToArray());
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
