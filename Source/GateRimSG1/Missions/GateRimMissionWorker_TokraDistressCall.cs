using GateRimSG1.Goauld;
using Verse;

namespace GateRimSG1.Missions
{
    /// <summary>
    /// Specialized adapter for the Tok'ra distress-call world site. The
    /// shared mission framework still owns recurrence, timing, difficulty,
    /// rewards and texts; this worker only gates and cleans up world-site
    /// mechanics that cannot be represented by common objectives alone.
    /// </summary>
    public sealed class GateRimMissionWorker_TokraDistressCall
        : GateRimMissionWorker
    {
        public override bool CanOffer(Map map)
        {
            return TokraDistressCallMissionUtility.CanCreateWorldSite(
                map,
                def?.distressCall);
        }

        public override void OnResolved(
            GateRimMissionRuntimeData runtime,
            Map map,
            Pawn operatorPawn,
            GateRimMissionOutcome outcome)
        {
            WorldObject_TokraDistressCallSite site
                = TokraDistressCallMissionUtility.FindWorldSite(runtime);

            site?.NotifyManagerResolved(
                outcome == GateRimMissionOutcome.Succeeded);
        }
    }
}
