using System.Collections.Generic;
using Verse;

namespace GateRimSG1.Jaffa
{
    /// <summary>
    /// Intrinsic forehead-mark visual attached to a pawn independently from
    /// genes, xenotypes, apparel and the pawn's current faction.
    /// </summary>
    public class JaffaForeheadMarkDef : Def
    {
        public List<PawnRenderNodeProperties> renderNodeProperties;

        public bool HasDefinedGraphicProperties =>
            !renderNodeProperties.NullOrEmpty();

        public List<PawnRenderNodeProperties> RenderNodeProperties =>
            renderNodeProperties;

        public override void ResolveReferences()
        {
            base.ResolveReferences();

            if (renderNodeProperties == null)
            {
                return;
            }

            for (int index = 0; index < renderNodeProperties.Count; index++)
            {
                renderNodeProperties[index].ResolveReferencesRecursive();
            }
        }

        public override IEnumerable<string> ConfigErrors()
        {
            foreach (string error in base.ConfigErrors())
            {
                yield return error;
            }

            if (!HasDefinedGraphicProperties)
            {
                yield return $"{defName} has no renderNodeProperties.";
                yield break;
            }

            for (int index = 0; index < renderNodeProperties.Count; index++)
            {
                foreach (string error in renderNodeProperties[index].ConfigErrors())
                {
                    yield return error;
                }
            }
        }
    }
}
