using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    public sealed class CaravanArrivalAction_TokraTemporaryBaseDeliverySite
        : CaravanArrivalAction
    {
        private WorldObject_TokraTemporaryBaseDeliverySite site;

        public override string Label
            => "GR_TokraTemporaryBaseDelivery_TravelCommandLabel"
                .Translate();

        public override string ReportString
            => "GR_TokraTemporaryBaseDelivery_ApproachingReport"
                .Translate();

        public CaravanArrivalAction_TokraTemporaryBaseDeliverySite()
        {
        }

        public CaravanArrivalAction_TokraTemporaryBaseDeliverySite(
            WorldObject_TokraTemporaryBaseDeliverySite targetSite)
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
            site?.NotifyCaravanArrived(caravan);
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_References.Look(ref site, "deliverySite");
        }

        public bool Targets(
            WorldObject_TokraTemporaryBaseDeliverySite targetSite)
        {
            return site != null
                && targetSite != null
                && site.ID == targetSite.ID;
        }

        public static FloatMenuAcceptanceReport CanVisit(
            Caravan caravan,
            WorldObject_TokraTemporaryBaseDeliverySite targetSite)
        {
            return targetSite == null || targetSite.Destroyed
                ? false
                : targetSite.CanVisit(caravan);
        }

        public static IEnumerable<FloatMenuOption> GetFloatMenuOptions(
            Caravan caravan,
            WorldObject_TokraTemporaryBaseDeliverySite targetSite)
        {
            return CaravanArrivalActionUtility.GetFloatMenuOptions(
                () => CanVisit(caravan, targetSite),
                () => new CaravanArrivalAction_TokraTemporaryBaseDeliverySite(
                    targetSite),
                "GR_TokraTemporaryBaseDelivery_TravelCommandLabel"
                    .Translate(),
                caravan,
                targetSite.Tile,
                targetSite);
        }
    }
}
