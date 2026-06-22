using GateRimSG1.Goauld;
using Verse;

namespace GateRimSG1.Missions
{
    /// <summary>
    /// Adapter for a craft-and-deliver contract whose destination is a
    /// temporary Tok'ra world site rather than a permanent settlement.
    /// </summary>
    public sealed class GateRimMissionWorker_TokraTemporaryBaseDelivery
        : GateRimMissionWorker
    {
        public override bool CanOffer(Map map)
        {
            return TokraTemporaryBaseDeliveryMissionUtility
                .CanCreateContract(map, def?.delivery);
        }

        public override void OnOffered(
            GateRimMissionRuntimeData runtime,
            Map map)
        {
            if (!TokraTemporaryBaseDeliveryMissionUtility
                .TrySelectAndStoreContract(map, def?.delivery, runtime))
            {
                GR_Log.Error(
                    "A Tok'ra temporary-base delivery offer was selected "
                    + "without any valid craftable contract.");
            }
        }

        public override void OnResolved(
            GateRimMissionRuntimeData runtime,
            Map map,
            Pawn operatorPawn,
            GateRimMissionOutcome outcome)
        {
            TokraTemporaryBaseDeliveryMissionUtility.FindWorldSite(runtime)
                ?.NotifyManagerResolved(
                    outcome == GateRimMissionOutcome.Succeeded);
        }
    }
}
