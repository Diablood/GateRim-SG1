using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    public class WorldObjectCompProperties_TokraRelayFormCaravan
        : WorldObjectCompProperties_FormCaravan
    {
        public WorldObjectCompProperties_TokraRelayFormCaravan()
        {
            compClass = typeof(WorldObjectComp_TokraRelayFormCaravan);
        }
    }

    /// <summary>
    /// Exposes the vanilla caravan-reformation flow only after the relay
    /// operation has reached a success or failure state. The inherited
    /// vanilla component still blocks reforming while active hostiles remain.
    /// </summary>
    public class WorldObjectComp_TokraRelayFormCaravan : FormCaravanComp
    {
        private bool OperationResolved
        {
            get
            {
                MapParent mapParent = parent as MapParent;

                if (mapParent == null || !mapParent.HasMap)
                {
                    return false;
                }

                MapComponent_TokraRelaySabotageMission component = mapParent
                    .Map
                    .GetComponent<MapComponent_TokraRelaySabotageMission>();

                return component != null && component.OperationResolved;
            }
        }

        public override void CompTickInterval(int delta)
        {
            if (OperationResolved)
            {
                base.CompTickInterval(delta);
            }
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            if (!OperationResolved)
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
