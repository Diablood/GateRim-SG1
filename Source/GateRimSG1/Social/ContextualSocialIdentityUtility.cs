using System.Collections.Generic;
using GateRimSG1.Goauld;
using GateRimSG1.Jaffa;
using RimWorld;
using Verse;

namespace GateRimSG1.Social
{
    /// <summary>
    /// Central cultural and symbiote-identity checks for the first contextual
    /// social layer.
    ///
    /// The utility deliberately distinguishes:
    ///
    /// - inherited Jaffa physiology;
    /// - current faction allegiance;
    /// - persistent cultural background carried by PawnKindDef;
    /// - acquired adult-symbiote origin;
    /// - intrinsic forehead marks.
    ///
    /// This keeps the initial thoughts contextual rather than absolute.
    /// </summary>
    public static class ContextualSocialIdentityUtility
    {
        public const float SystemLordGazeRadius = 12f;

        public static bool IsFreeJaffa(Pawn pawn)
        {
            return JaffaPrimtaUtility.IsCompatibleJaffa(pawn)
                && (pawn.Faction?.def == GR_DefOf.SG1_FreeJaffa
                    || pawn.kindDef == GR_DefOf.SG1_FreeJaffaWarrior
                    || pawn.kindDef == GR_DefOf.SG1_FreeJaffaGuard);
        }

        public static bool IsGoauldDomainJaffa(Pawn pawn)
        {
            return JaffaPrimtaUtility.IsCompatibleJaffa(pawn)
                && GoauldSystemLordDomainUtility.HasAssignedDomain(
                    pawn.Faction);
        }

        public static bool IsMarkedJaffa(Pawn pawn)
        {
            return JaffaPrimtaUtility.IsCompatibleJaffa(pawn)
                && JaffaForeheadMarkUtility.MarkFor(pawn) != null;
        }

        public static bool IsActiveGoauldHost(Pawn pawn)
        {
            return HasActiveAdultSymbioteOrigin(
                pawn,
                GoauldSymbioteOrigin.Goauld);
        }

        public static bool IsActiveTokraHost(Pawn pawn)
        {
            return HasActiveAdultSymbioteOrigin(
                pawn,
                GoauldSymbioteOrigin.Tokra);
        }

        public static bool IsSystemLordHost(Pawn pawn)
        {
            return pawn?.kindDef == GR_DefOf.SG1_GoauldSystemLordHost
                && IsActiveGoauldHost(pawn);
        }

        public static bool HasNearbySystemLord(Pawn pawn)
        {
            if (!IsGoauldDomainJaffa(pawn)
                || pawn?.Map == null
                || !pawn.Spawned)
            {
                return false;
            }

            IReadOnlyList<Pawn> pawns = pawn.Map.mapPawns?.AllPawnsSpawned;

            if (pawns == null)
            {
                return false;
            }

            for (int pawnIndex = 0; pawnIndex < pawns.Count; pawnIndex++)
            {
                Pawn candidate = pawns[pawnIndex];

                if (candidate == null
                    || candidate == pawn
                    || candidate.Destroyed
                    || candidate.Dead
                    || candidate.Faction != pawn.Faction
                    || !candidate.Spawned
                    || !IsSystemLordHost(candidate))
                {
                    continue;
                }

                if (pawn.Position.InHorDistOf(
                    candidate.Position,
                    SystemLordGazeRadius))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool HasActiveAdultSymbioteOrigin(
            Pawn pawn,
            GoauldSymbioteOrigin origin)
        {
            List<Hediff> hediffs = pawn?.health?.hediffSet?.hediffs;

            if (hediffs == null)
            {
                return false;
            }

            for (int hediffIndex = 0;
                hediffIndex < hediffs.Count;
                hediffIndex++)
            {
                Hediff hediff = hediffs[hediffIndex];

                if (hediff?.def != GR_DefOf.SG1_GoauldHostSymbiote)
                {
                    continue;
                }

                HediffComp_GoauldSymbiote comp =
                    (hediff as HediffWithComps)
                        ?.GetComp<HediffComp_GoauldSymbiote>();

                if (comp?.SymbioteData?.Origin == origin)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
