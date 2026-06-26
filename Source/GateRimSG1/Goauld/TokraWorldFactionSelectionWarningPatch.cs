using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Adds a GateRim-specific warning to the vanilla world-faction selector
    /// when the selectable Tok'ra content anchor is removed.
    /// </summary>
    [HarmonyPatch(
        typeof(WorldFactionsUIUtility),
        nameof(WorldFactionsUIUtility.DoWindowContents))]
    internal static class TokraWorldFactionSelectionWarningPatch
    {
        private static IEnumerable<CodeInstruction> Transpiler(
            IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = new List<CodeInstruction>(
                instructions);
            ConstructorInfo stringBuilderCtor = AccessTools.Constructor(
                typeof(StringBuilder),
                Type.EmptyTypes);
            FieldInfo warningHeightField = AccessTools.Field(
                typeof(WorldFactionsUIUtility),
                "warningHeight");
            MethodInfo appendWarningMethod = AccessTools.Method(
                typeof(TokraWorldFactionSelectionWarningPatch),
                nameof(AppendWarningIfTokraDisabled));

            CodeInstruction stringBuilderStore = null;

            for (int index = 0; index < codes.Count - 1; index++)
            {
                if (codes[index].opcode == OpCodes.Newobj
                    && Equals(codes[index].operand, stringBuilderCtor)
                    && IsStoreLocal(codes[index + 1].opcode))
                {
                    stringBuilderStore = codes[index + 1];
                    break;
                }
            }

            if (stringBuilderStore == null)
            {
                GR_Log.Warning(
                    "Could not locate the world-faction warning buffer. "
                    + "The Tok'ra disabled warning will not be injected.");
                return codes;
            }

            for (int index = 1; index < codes.Count; index++)
            {
                if (codes[index - 1].opcode == OpCodes.Ldc_R4
                    && codes[index - 1].operand is float value
                    && value == 0f
                    && codes[index].opcode == OpCodes.Stsfld
                    && Equals(codes[index].operand, warningHeightField))
                {
                    // The reset instruction is a vanilla branch target.
                    // Insert after it so every warning path reaches us.
                    codes.InsertRange(
                        index + 1,
                        new[]
                        {
                            new CodeInstruction(OpCodes.Ldarg_1),
                            BuildLoadLocal(stringBuilderStore),
                            new CodeInstruction(
                                OpCodes.Call,
                                appendWarningMethod)
                        });

                    return codes;
                }
            }

            GR_Log.Warning(
                "Could not locate the world-faction warning render block. "
                + "The Tok'ra disabled warning will not be injected.");
            return codes;
        }

        private static void AppendWarningIfTokraDisabled(
            List<FactionDef> factions,
            StringBuilder warningText)
        {
            FactionDef tokraDef = GR_DefOf.SG1_Tokra;

            if (tokraDef == null
                || factions == null
                || warningText == null
                || factions.Contains(tokraDef))
            {
                return;
            }

            string warning = "Warning".Translate().ToString()
                + ": "
                + "GR_TokraWorldFactionSelection_DisabledWarning"
                    .Translate(tokraDef.label)
                    .ToString();

            warningText.AppendLine(warning);
        }

        private static bool IsStoreLocal(OpCode opcode)
        {
            return opcode == OpCodes.Stloc
                || opcode == OpCodes.Stloc_S
                || opcode == OpCodes.Stloc_0
                || opcode == OpCodes.Stloc_1
                || opcode == OpCodes.Stloc_2
                || opcode == OpCodes.Stloc_3;
        }

        private static CodeInstruction BuildLoadLocal(
            CodeInstruction storeInstruction)
        {
            if (storeInstruction.opcode == OpCodes.Stloc_0)
            {
                return new CodeInstruction(OpCodes.Ldloc_0);
            }

            if (storeInstruction.opcode == OpCodes.Stloc_1)
            {
                return new CodeInstruction(OpCodes.Ldloc_1);
            }

            if (storeInstruction.opcode == OpCodes.Stloc_2)
            {
                return new CodeInstruction(OpCodes.Ldloc_2);
            }

            if (storeInstruction.opcode == OpCodes.Stloc_3)
            {
                return new CodeInstruction(OpCodes.Ldloc_3);
            }

            return new CodeInstruction(
                storeInstruction.opcode == OpCodes.Stloc_S
                    ? OpCodes.Ldloc_S
                    : OpCodes.Ldloc,
                storeInstruction.operand);
        }
    }
}
