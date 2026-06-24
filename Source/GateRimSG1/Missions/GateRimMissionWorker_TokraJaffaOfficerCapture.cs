using GateRimSG1.Goauld;
using Verse;

namespace GateRimSG1.Missions
{
    /// <summary>
    /// Adapter for the recurrent Tok'ra operation that creates a temporary
    /// Goa'uld field site and asks the player to extract one officer alive.
    /// </summary>
    public sealed class GateRimMissionWorker_TokraJaffaOfficerCapture
        : GateRimMissionWorker
    {
        public override bool CanOffer(Map map)
        {
            return TokraJaffaOfficerCaptureMissionUtility.CanCreateWorldSite(
                map,
                def?.capture);
        }

        public override void OnResolved(
            GateRimMissionRuntimeData runtime,
            Map map,
            Pawn operatorPawn,
            GateRimMissionOutcome outcome)
        {
            TokraJaffaOfficerCaptureMissionUtility.FindWorldSite(runtime)
                ?.NotifyManagerResolved(
                    outcome == GateRimMissionOutcome.Succeeded);
        }
    }
}
