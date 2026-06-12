using System;
using System.Collections.Generic;
using Verse;

namespace GateRimSG1.Jaffa
{
    /// <summary>
    /// Adds the intrinsic forehead-mark node to the vanilla pawn render tree.
    /// This deliberately mirrors the native dynamic setup used by genes and
    /// Hediffs while sourcing its data from the GateRim save component.
    /// </summary>
    public class DynamicPawnRenderNodeSetup_JaffaForeheadMarks :
        DynamicPawnRenderNodeSetup
    {
        public override bool HumanlikeOnly => true;

        public override IEnumerable<(
            PawnRenderNode node,
            PawnRenderNode parent)> GetDynamicNodes(
                Pawn pawn,
                PawnRenderTree tree)
        {
            JaffaForeheadMarkDef markDef =
                JaffaForeheadMarkUtility.MarkFor(pawn);

            if (markDef == null || !markDef.HasDefinedGraphicProperties)
            {
                yield break;
            }

            for (int index = 0; index < markDef.RenderNodeProperties.Count; index++)
            {
                PawnRenderNodeProperties properties =
                    markDef.RenderNodeProperties[index];

                if (!tree.ShouldAddNodeToTree(properties))
                {
                    continue;
                }

                PawnRenderNode node = (PawnRenderNode)Activator.CreateInstance(
                    properties.nodeClass,
                    pawn,
                    properties,
                    tree);

                yield return (node, null);
            }
        }
    }
}
