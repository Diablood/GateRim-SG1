using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    public sealed class WorldObjectCompProperties_TokraIntroductionFormCaravan
        : WorldObjectCompProperties_FormCaravan
    {
        public WorldObjectCompProperties_TokraIntroductionFormCaravan()
        {
            compClass = typeof(WorldObjectComp_TokraIntroductionFormCaravan);
        }
    }

    public sealed class WorldObjectComp_TokraIntroductionFormCaravan
        : FormCaravanComp
    {
        private bool CanReform
        {
            get
            {
                MapParent mapParent = parent as MapParent;

                if (mapParent == null || !mapParent.HasMap)
                {
                    return false;
                }

                MapComponent_TokraIntroductionArtifactMission component
                    = mapParent.Map.GetComponent<
                        MapComponent_TokraIntroductionArtifactMission>();

                return component != null
                    && (component.SiteSecured || component.OperationResolved);
            }
        }

        public override void CompTickInterval(int delta)
        {
            if (CanReform)
            {
                base.CompTickInterval(delta);
            }
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            if (!CanReform)
            {
                yield break;
            }

            foreach (Gizmo gizmo in base.GetGizmos())
            {
                yield return gizmo;
            }
        }
    }
}
