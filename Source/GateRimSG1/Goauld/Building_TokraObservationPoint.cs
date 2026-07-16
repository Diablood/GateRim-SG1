using UnityEngine;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Temporary map marker identifying the peripheral location selected by
    /// the Tok'ra cell for an observation operation.
    ///
    /// Before the portable sensor is installed, the marker is drawn as a
    /// translucent blueprint. Once deployment is complete, the normal device
    /// visual is rendered at full opacity.
    /// </summary>
    public class Building_TokraObservationPoint : Building
    {
        private static readonly Color BlueprintTint
            = new Color(0.28f, 0.70f, 1f, 0.62f);

        private Graphic blueprintGraphic;

        protected override void DrawAt(
            Vector3 drawLoc,
            bool flip = false)
        {
            if (!TokraObservationPointDeploymentUtility
                    .IsUndeployedMarker(this))
            {
                base.DrawAt(drawLoc, flip);
                return;
            }

            if (blueprintGraphic == null)
            {
                blueprintGraphic = GraphicDatabase.Get<Graphic_Single>(
                    def.graphicData.texPath,
                    ShaderDatabase.Transparent,
                    def.graphicData.drawSize,
                    BlueprintTint);
            }

            blueprintGraphic.Draw(
                drawLoc,
                flip ? Rotation.Opposite : Rotation,
                this);
        }
    }
}
