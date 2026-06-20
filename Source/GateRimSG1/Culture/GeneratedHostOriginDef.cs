using System.Collections.Generic;
using GateRimSG1.Names;
using RimWorld;
using Verse;

namespace GateRimSG1.Culture
{
    /// <summary>
    /// Configures one possible cultural origin for the historical host of a
    /// pawn generated already joined with a symbiote.
    /// </summary>
    public class GeneratedHostOriginDef : Def
    {
        public float weight = 1f;
        public CulturalPawnNameGroup nameGroup = CulturalPawnNameGroup.None;
        public List<ThingDef> races = new List<ThingDef>();
        public List<XenotypeDef> xenotypes = new List<XenotypeDef>();
        public List<BackstoryDef> childhoods = new List<BackstoryDef>();
        public List<BackstoryDef> adulthoods = new List<BackstoryDef>();

        public bool CanGenerateFor(Pawn pawn)
        {
            if (pawn == null)
            {
                return false;
            }

            if (!races.NullOrEmpty() && !races.Contains(pawn.def))
            {
                return false;
            }

            if (!xenotypes.NullOrEmpty()
                && !xenotypes.Contains(pawn.genes?.Xenotype))
            {
                return false;
            }

            return true;
        }

        public override IEnumerable<string> ConfigErrors()
        {
            foreach (string error in base.ConfigErrors())
            {
                yield return error;
            }

            if (weight <= 0f)
            {
                yield return $"{defName} must have a positive weight.";
            }

            if (nameGroup == CulturalPawnNameGroup.None)
            {
                yield return $"{defName} has no generated-host name group.";
            }

            if (childhoods.NullOrEmpty())
            {
                yield return $"{defName} has no generated-host childhood.";
            }

            if (adulthoods.NullOrEmpty())
            {
                yield return $"{defName} has no generated-host adulthood.";
            }

            foreach (BackstoryDef childhood in childhoods
                ?? new List<BackstoryDef>())
            {
                if (childhood != null
                    && childhood.slot != BackstorySlot.Childhood)
                {
                    yield return $"{defName} uses adulthood "
                        + $"{childhood.defName} as a generated-host childhood.";
                }
            }

            foreach (BackstoryDef adulthood in adulthoods
                ?? new List<BackstoryDef>())
            {
                if (adulthood != null
                    && adulthood.slot != BackstorySlot.Adulthood)
                {
                    yield return $"{defName} uses childhood "
                        + $"{adulthood.defName} as a generated-host adulthood.";
                }
            }
        }
    }
}
