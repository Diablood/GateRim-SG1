using GateRimSG1.Goauld;
using Verse;

namespace GateRimSG1.Missions
{
    /// <summary>
    /// Specialized adapter for the unique-until-completed Tok'ra introduction
    /// arc. The persistent component owns attempts and retry delays; the later
    /// world-site revision will extend this worker with combat-site mechanics.
    /// </summary>
    public sealed class GateRimMissionWorker_TokraIntroductionArtifact
        : GateRimMissionWorker
    {
        public override bool CanOffer(Map map)
        {
            return GameComponent_TokraIntroductionArc.CanOffer(map);
        }
    }
}
