using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    public sealed class CaravanArrivalAction_TokraJaffaOfficerCaptureSite
        : CaravanArrivalAction
    {
        private WorldObject_TokraJaffaOfficerCaptureSite site;

        public override string Label
            => "GR_TokraJaffaOfficerCapture_TravelCommandLabel".Translate();

        public override string ReportString
            => "GR_TokraJaffaOfficerCapture_ApproachingReport".Translate();

        public CaravanArrivalAction_TokraJaffaOfficerCaptureSite()
        {
        }

        public CaravanArrivalAction_TokraJaffaOfficerCaptureSite(
            WorldObject_TokraJaffaOfficerCaptureSite targetSite)
        {
            site = targetSite;
        }

        public override FloatMenuAcceptanceReport StillValid(
            Caravan caravan,
            PlanetTile destinationTile)
        {
            FloatMenuAcceptanceReport report = base.StillValid(
                caravan,
                destinationTile);

            if (!report)
            {
                return report;
            }

            if (site == null || site.Tile != destinationTile)
            {
                return false;
            }

            return CanVisit(caravan, site);
        }

        public override void Arrived(Caravan caravan)
        {
            if (site == null || site.Destroyed)
            {
                return;
            }

            if (site.OperationLaunched && !site.HasMap)
            {
                Messages.Message(
                    "GR_TokraJaffaOfficerCapture_ReturnToColony"
                        .Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            if (!site.HasMap)
            {
                LongEventHandler.QueueLongEvent(
                    () => site.EnterFromCaravan(caravan),
                    "GeneratingMapForNewEncounter",
                    doAsynchronously: false,
                    exceptionHandler: null);
                return;
            }

            site.EnterFromCaravan(caravan);
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_References.Look(ref site, "captureSite");
        }

        public static FloatMenuAcceptanceReport CanVisit(
            Caravan caravan,
            WorldObject_TokraJaffaOfficerCaptureSite targetSite)
        {
            return targetSite == null || targetSite.Destroyed
                ? false
                : targetSite.CanVisit(caravan);
        }

        public static IEnumerable<FloatMenuOption> GetFloatMenuOptions(
            Caravan caravan,
            WorldObject_TokraJaffaOfficerCaptureSite targetSite)
        {
            return CaravanArrivalActionUtility.GetFloatMenuOptions(
                () => CanVisit(caravan, targetSite),
                () => new CaravanArrivalAction_TokraJaffaOfficerCaptureSite(
                    targetSite),
                "GR_TokraJaffaOfficerCapture_TravelCommandLabel".Translate(),
                caravan,
                targetSite.Tile,
                targetSite);
        }
    }
}
