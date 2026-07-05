using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Applies the bounded Jaffa-officer replacement after vanilla has built
    /// an eligible Goa'uld pawn group.
    ///
    /// Settlement groups are handled globally. Combat groups are modified only
    /// while an explicit incident-scoped generation context is active.
    /// </summary>
    [HarmonyPatch]
    internal static class PawnGroupMakerUtility_GoauldSettlementOfficerPatch
    {
        private static MethodBase TargetMethod()
        {
            return AccessTools
                .GetDeclaredMethods(typeof(PawnGroupMakerUtility))
                .FirstOrDefault(method =>
                    method.Name == nameof(PawnGroupMakerUtility.GeneratePawns)
                    && method.GetParameters().Length >= 1
                    && method.GetParameters()[0].ParameterType
                        == typeof(PawnGroupMakerParms)
                    && typeof(IEnumerable<Pawn>).IsAssignableFrom(
                        method.ReturnType));
        }

        private static void Postfix(
            PawnGroupMakerParms __0,
            ref IEnumerable<Pawn> __result)
        {
            if (__result == null
                || !GoauldSystemLordFactionUtility
                    .IsSystemLordFaction(__0.faction))
            {
                return;
            }

            if (__0.groupKind == PawnGroupKindDefOf.Settlement)
            {
                List<Pawn> settlementPawns = __result.ToList();
                GoauldJaffaOfficerForceUtility
                    .TryReplaceSettlementGuardWithOfficer(
                        settlementPawns,
                        __0.faction,
                        "Goa'uld settlement defense group");
                __result = settlementPawns;
                return;
            }

            if (!GoauldJaffaOfficerForceUtility
                .IsActiveCombatOfficerGenerationFor(__0))
            {
                return;
            }

            List<Pawn> combatPawns = __result.ToList();
            GoauldJaffaOfficerForceUtility
                .TryProcessActiveCombatOfficerGeneration(combatPawns, __0);
            __result = combatPawns;
        }
    }
}
