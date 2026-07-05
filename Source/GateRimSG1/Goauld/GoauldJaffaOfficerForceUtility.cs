using System;
using System.Collections.Generic;
using GateRimSG1.Jaffa;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Adds at most one Jaffa officer to eligible Goa'uld forces without
    /// increasing their generated pawn count or ordinary threat budget.
    /// </summary>
    public static class GoauldJaffaOfficerForceUtility
    {
        public const int MinimumEligibleJaffaCount = 5;

        private const string BreacherPawnKindDefName
            = "SG1_GoauldJaffaBreacher";

        [ThreadStatic]
        private static CombatOfficerGenerationRequest
            activeCombatOfficerGeneration;

        public static IDisposable BeginCombatOfficerGeneration(
            PawnGroupKindDef expectedGroupKind,
            string context,
            bool allowWarriorFallbackForDebug = false)
        {
            return new CombatOfficerGenerationScope(
                new CombatOfficerGenerationRequest(
                    expectedGroupKind,
                    context,
                    allowWarriorFallbackForDebug));
        }

        public static bool IsActiveCombatOfficerGenerationFor(
            PawnGroupMakerParms groupParms)
        {
            CombatOfficerGenerationRequest request
                = activeCombatOfficerGeneration;

            return request != null
                && !request.consumed
                && request.expectedGroupKind == groupParms.groupKind
                && GoauldSystemLordFactionUtility
                    .IsSystemLordFaction(groupParms.faction);
        }

        public static bool TryProcessActiveCombatOfficerGeneration(
            List<Pawn> pawns,
            PawnGroupMakerParms groupParms)
        {
            CombatOfficerGenerationRequest request
                = activeCombatOfficerGeneration;

            if (pawns == null
                || !IsActiveCombatOfficerGenerationFor(groupParms))
            {
                return false;
            }

            request.consumed = true;
            TryReplaceCombatGuardWithOfficer(
                pawns,
                groupParms.faction,
                request.context,
                request.allowWarriorFallbackForDebug);
            return true;
        }

        public static PawnKindDef SelectMissionPawnKind(
            int index,
            int pawnCount,
            PawnKindDef warriorKind,
            PawnKindDef guardKind)
        {
            bool officerSlot = pawnCount >= MinimumEligibleJaffaCount
                && index == MinimumEligibleJaffaCount - 1
                && guardKind != null
                && GR_DefOf.SG1_GoauldJaffaFieldOfficer != null;

            if (officerSlot)
            {
                return GR_DefOf.SG1_GoauldJaffaFieldOfficer;
            }

            return guardKind != null
                    && (warriorKind == null
                        || index % MinimumEligibleJaffaCount
                            == MinimumEligibleJaffaCount - 1)
                ? guardKind
                : warriorKind ?? guardKind;
        }

        public static Pawn GenerateForcePawn(
            PawnKindDef kind,
            Faction faction,
            int tile,
            string context,
            PawnKindDef fallbackKind = null)
        {
            if (kind == null || faction == null)
            {
                return null;
            }

            Pawn pawn = PawnGenerator.GeneratePawn(kind, faction, tile);

            if (pawn == null || !IsOfficerKind(kind))
            {
                return pawn;
            }

            if (EnsureOfficerCommandIdentity(pawn, faction, context))
            {
                return pawn;
            }

            if (!pawn.Destroyed)
            {
                pawn.Destroy(DestroyMode.Vanish);
            }

            if (fallbackKind != null)
            {
                GR_Log.Warning(
                    "Falling back to "
                    + fallbackKind.defName
                    + " because officer generation failed in "
                    + context
                    + ".");
                return PawnGenerator.GeneratePawn(
                    fallbackKind,
                    faction,
                    tile);
            }

            return null;
        }

        public static bool TryReplaceCombatGuardWithOfficer(
            List<Pawn> pawns,
            Faction faction,
            string context,
            bool allowWarriorFallbackForDebug = false)
        {
            return TryReplaceGuardWithOfficer(
                pawns,
                faction,
                GR_DefOf.SG1_GoauldJaffaGuard,
                GR_DefOf.SG1_GoauldJaffaFieldOfficer,
                IsCombatJaffa,
                context,
                allowWarriorFallbackForDebug
                    ? GR_DefOf.SG1_GoauldJaffaWarrior
                    : null);
        }

        public static bool TryReplaceSettlementGuardWithOfficer(
            List<Pawn> pawns,
            Faction faction,
            string context)
        {
            return TryReplaceGuardWithOfficer(
                pawns,
                faction,
                GR_DefOf.SG1_GoauldSettlementJaffaGuard,
                GR_DefOf.SG1_GoauldSettlementJaffaOfficer,
                IsSettlementJaffa,
                context,
                fallbackKind: null);
        }

        public static bool EnsureOfficerCommandIdentity(
            Pawn pawn,
            Faction faction,
            string context)
        {
            if (pawn?.apparel == null)
            {
                GR_Log.Warning(
                    "Cannot equip generated Jaffa officer for "
                    + context
                    + ": apparel tracker is unavailable.");
                return false;
            }

            bool armorEquipped = EnsureWornApparel(
                pawn,
                GR_DefOf.SG1_JaffaOfficerArmor);
            bool helmetEquipped = EnsureWornApparel(
                pawn,
                GR_DefOf.SG1_JaffaOfficerDeployedHelmet);

            if (!armorEquipped || !helmetEquipped)
            {
                GR_Log.Warning(
                    "Could not complete the distinctive Jaffa officer "
                    + "loadout for "
                    + pawn.LabelShortCap
                    + " in "
                    + context
                    + "; armor="
                    + armorEquipped
                    + ", helmet="
                    + helmetEquipped
                    + ".");
                return false;
            }

            JaffaForeheadMarkUtility.SetMarkForRank(
                pawn,
                faction,
                GoauldJaffaMarkRank.Elite);

            GR_Log.Message(
                "Verified distinctive Jaffa officer command identity for "
                + pawn.LabelShortCap
                + " in "
                + context
                + ".");
            return true;
        }

        public static bool IsOfficerKind(PawnKindDef kind)
        {
            return kind == GR_DefOf.SG1_GoauldJaffaOfficer
                || kind == GR_DefOf.SG1_GoauldJaffaFieldOfficer
                || kind == GR_DefOf.SG1_GoauldSettlementJaffaOfficer;
        }

        private static bool TryReplaceGuardWithOfficer(
            List<Pawn> pawns,
            Faction faction,
            PawnKindDef guardKind,
            PawnKindDef officerKind,
            System.Predicate<PawnKindDef> isEligibleJaffaKind,
            string context,
            PawnKindDef fallbackKind)
        {
            if (pawns == null
                || faction == null
                || guardKind == null
                || officerKind == null
                || isEligibleJaffaKind == null)
            {
                return false;
            }

            int eligibleCount = 0;
            int replacementIndex = -1;
            int fallbackIndex = -1;

            for (int index = 0; index < pawns.Count; index++)
            {
                Pawn pawn = pawns[index];
                PawnKindDef kind = pawn?.kindDef;

                if (kind == null)
                {
                    continue;
                }

                if (IsOfficerKind(kind))
                {
                    EnsureOfficerCommandIdentity(pawn, faction, context);
                    return false;
                }

                if (!isEligibleJaffaKind(kind))
                {
                    continue;
                }

                eligibleCount++;

                if (replacementIndex < 0 && kind == guardKind)
                {
                    replacementIndex = index;
                }

                if (fallbackIndex < 0
                    && fallbackKind != null
                    && kind == fallbackKind)
                {
                    fallbackIndex = index;
                }
            }

            if (eligibleCount < MinimumEligibleJaffaCount)
            {
                return false;
            }

            if (replacementIndex < 0)
            {
                replacementIndex = fallbackIndex;
            }

            if (replacementIndex < 0)
            {
                GR_Log.Message(
                    "Eligible Goa'uld Jaffa force in "
                    + context
                    + " contains "
                    + eligibleCount
                    + " Jaffa but no budget-equivalent guard to replace; "
                    + "no officer was added.");
                return false;
            }

            Pawn officer = GenerateForcePawn(
                officerKind,
                faction,
                pawns[replacementIndex]?.Tile ?? -1,
                context);

            if (officer == null)
            {
                return false;
            }

            Pawn replacedPawn = pawns[replacementIndex];
            PawnKindDef replacedKind = replacedPawn?.kindDef;
            pawns[replacementIndex] = officer;

            if (replacedPawn != null && !replacedPawn.Destroyed)
            {
                replacedPawn.Destroy(DestroyMode.Vanish);
            }

            bool budgetNeutral = replacedKind != null
                && System.Math.Abs(
                    replacedKind.combatPower - officerKind.combatPower)
                    < 0.001f;
            GR_Log.Message(
                "Replaced one "
                + (replacedKind?.defName ?? "<missing kind>")
                + " with "
                + officerKind.defName
                + " in "
                + context
                + "; group size remains "
                + pawns.Count
                + (budgetNeutral
                    ? " and combatPower remains budget-neutral."
                    : " through the dedicated forced-debug fallback."));
            return true;
        }

        private static bool IsCombatJaffa(PawnKindDef kind)
        {
            return kind == GR_DefOf.SG1_GoauldJaffaWarrior
                || kind == GR_DefOf.SG1_GoauldJaffaGuard
                || kind?.defName == BreacherPawnKindDefName;
        }

        private static bool IsSettlementJaffa(PawnKindDef kind)
        {
            return kind == GR_DefOf.SG1_GoauldSettlementJaffaWarrior
                || kind == GR_DefOf.SG1_GoauldSettlementJaffaGuard;
        }

        private sealed class CombatOfficerGenerationRequest
        {
            public readonly PawnGroupKindDef expectedGroupKind;
            public readonly string context;
            public readonly bool allowWarriorFallbackForDebug;
            public bool consumed;

            public CombatOfficerGenerationRequest(
                PawnGroupKindDef expectedGroupKind,
                string context,
                bool allowWarriorFallbackForDebug)
            {
                this.expectedGroupKind = expectedGroupKind;
                this.context = context;
                this.allowWarriorFallbackForDebug
                    = allowWarriorFallbackForDebug;
            }
        }

        private sealed class CombatOfficerGenerationScope : IDisposable
        {
            private readonly CombatOfficerGenerationRequest previousRequest;
            private bool disposed;

            public CombatOfficerGenerationScope(
                CombatOfficerGenerationRequest request)
            {
                previousRequest = activeCombatOfficerGeneration;
                activeCombatOfficerGeneration = request;
            }

            public void Dispose()
            {
                if (disposed)
                {
                    return;
                }

                activeCombatOfficerGeneration = previousRequest;
                disposed = true;
            }
        }

        private static bool EnsureWornApparel(
            Pawn pawn,
            ThingDef apparelDef)
        {
            if (pawn?.apparel == null || apparelDef == null)
            {
                return false;
            }

            for (int index = 0;
                index < pawn.apparel.WornApparel.Count;
                index++)
            {
                if (pawn.apparel.WornApparel[index]?.def == apparelDef)
                {
                    return true;
                }
            }

            Apparel apparel = ThingMaker.MakeThing(apparelDef) as Apparel;

            if (apparel == null)
            {
                GR_Log.Warning(
                    "Cannot equip generated Jaffa officer: "
                    + apparelDef.defName
                    + " is not apparel.");
                return false;
            }

            apparel.TryGetComp<CompQuality>()?.SetQuality(
                QualityCategory.Normal,
                ArtGenerationContext.Outsider);
            pawn.apparel.Wear(
                apparel,
                dropReplacedApparel: false);

            if (pawn.apparel.WornApparel.Contains(apparel))
            {
                return true;
            }

            if (!apparel.Destroyed)
            {
                apparel.Destroy(DestroyMode.Vanish);
            }

            GR_Log.Warning(
                "Cannot equip generated Jaffa officer with "
                + apparelDef.defName
                + ".");
            return false;
        }
    }
}
