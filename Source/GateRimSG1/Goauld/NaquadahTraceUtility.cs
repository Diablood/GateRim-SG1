using System.Collections.Generic;
using GateRimSG1.Jaffa;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Shared biological eligibility service for naquadah-reactive technology.
    ///
    /// SG1_NaquadahBlood is an acquired xenogene marker. Once granted, it is
    /// deliberately never removed by normal gameplay so former adult hosts and
    /// former Prim'ta carriers retain the biological traces left in their blood.
    /// </summary>
    public static class NaquadahTraceUtility
    {
        public static bool HasPersistentTrace(Pawn pawn)
        {
            return GetPersistentTraceGene(pawn) != null;
        }

        public static bool CanActivateNaquadahTechnology(Pawn pawn)
        {
            return HasPersistentTrace(pawn);
        }

        public static bool HasActiveAdultSymbiote(Pawn pawn)
        {
            List<Hediff> hediffs = pawn?.health?.hediffSet?.hediffs;

            if (hediffs == null)
            {
                return false;
            }

            for (int index = 0; index < hediffs.Count; index++)
            {
                Hediff hediff = hediffs[index];

                if (hediff?.def == GR_DefOf.SG1_GoauldRecentImplantation
                    || hediff?.def == GR_DefOf.SG1_GoauldHostSymbiote)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool HasActivePrimta(Pawn pawn)
        {
            return JaffaPrimtaUtility.HasPrimta(pawn);
        }

        public static bool ShouldCarryPersistentTrace(Pawn pawn)
        {
            return HasPersistentTrace(pawn)
                || HasActiveAdultSymbiote(pawn)
                || HasActivePrimta(pawn);
        }

        public static bool EnsurePersistentTrace(
            Pawn pawn,
            string source,
            bool writeLog = true)
        {
            if (pawn?.RaceProps?.Humanlike != true
                || pawn.genes == null
                || GR_DefOf.SG1_NaquadahBlood == null
                || HasPersistentTrace(pawn))
            {
                return false;
            }

            Gene addedGene = pawn.genes.AddGene(
                GR_DefOf.SG1_NaquadahBlood,
                xenogene: true);

            if (addedGene == null)
            {
                GR_Log.Warning(
                    "Could not add persistent naquadah traces to "
                    + $"{PawnDebugLabel(pawn)} from {source ?? "unknown source"}.");
                return false;
            }

            if (writeLog)
            {
                GR_Log.Message(
                    "Recorded persistent biological naquadah traces for "
                    + $"{PawnDebugLabel(pawn)} from "
                    + $"{source ?? "unknown source"}.");
            }

            return true;
        }

        internal static bool RemovePersistentTraceForDebug(Pawn pawn)
        {
            Gene traceGene = GetPersistentTraceGene(pawn);

            if (traceGene == null || pawn?.genes == null)
            {
                return false;
            }

            pawn.genes.RemoveGene(traceGene);
            GR_Log.Message(
                "Removed persistent biological naquadah traces through a "
                + $"developer action from {PawnDebugLabel(pawn)}.");
            return true;
        }

        public static string ActiveTraceSourceLabel(Pawn pawn)
        {
            bool adultSymbiote = HasActiveAdultSymbiote(pawn);
            bool primta = HasActivePrimta(pawn);

            if (adultSymbiote && primta)
            {
                return "adult symbiote and Prim'ta";
            }

            if (adultSymbiote)
            {
                return "adult symbiote";
            }

            if (primta)
            {
                return "Prim'ta";
            }

            return HasPersistentTrace(pawn)
                ? "persistent former exposure"
                : "none";
        }

        private static Gene GetPersistentTraceGene(Pawn pawn)
        {
            if (pawn?.genes == null || GR_DefOf.SG1_NaquadahBlood == null)
            {
                return null;
            }

            return pawn.genes.GetGene(GR_DefOf.SG1_NaquadahBlood);
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
