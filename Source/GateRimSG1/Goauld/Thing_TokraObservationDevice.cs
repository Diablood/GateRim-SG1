using RimWorld;
using UnityEngine;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Portable Tok'ra field sensor used only by the organic observation
    /// operation. It is delivered, deployed, recovered and finally consumed
    /// when its data is transmitted through the secure communicator.
    ///
    /// While portable, it reuses RimWorld's vanilla minified-building crate
    /// front so players can immediately read it as equipment meant to be
    /// carried and installed.
    /// </summary>
    public class Thing_TokraObservationDevice : ThingWithComps
    {
        private Graphic crateFrontGraphic;

        private Graphic CrateFrontGraphic
        {
            get
            {
                if (crateFrontGraphic == null)
                {
                    crateFrontGraphic = GraphicDatabase.Get<Graphic_Single>(
                        "Things/Item/Minified/CrateFront",
                        ShaderDatabase.Cutout,
                        Vector2.one * MinifiedThing.CrateToGraphicScale,
                        Color.white);
                }

                return crateFrontGraphic;
            }
        }

        protected override void DrawAt(
            Vector3 drawLoc,
            bool flip = false)
        {
            CrateFrontGraphic.DrawFromDef(
                drawLoc + Altitudes.AltIncVect * 0.1f,
                Rot4.North,
                null);

            base.DrawAt(drawLoc, flip);
        }
    }
}
