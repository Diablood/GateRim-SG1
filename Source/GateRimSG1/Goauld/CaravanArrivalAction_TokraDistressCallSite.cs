using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Uses RimWorld's standard caravan-arrival action. The caravan stores
    /// this action while travelling and automatically enters the encounter
    /// map when it reaches the world tile.
    /// </summary>
    public sealed class CaravanArrivalAction_TokraDistressCallSite
        : CaravanArrivalAction
    {
        private WorldObject_TokraDistressCallSite site;

        public override string Label
            => "GR_TokraDistressCall_TravelCommandLabel".Translate();

        public override string ReportString
            => "GR_TokraDistressCall_ApproachingReport".Translate();

        public CaravanArrivalAction_TokraDistressCallSite()
        {
        }

        public CaravanArrivalAction_TokraDistressCallSite(
            WorldObject_TokraDistressCallSite targetSite)
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
            Scribe_References.Look(ref site, "distressSite");
        }

        public static FloatMenuAcceptanceReport CanVisit(
            Caravan caravan,
            WorldObject_TokraDistressCallSite targetSite)
        {
            if (targetSite == null || targetSite.Destroyed)
            {
                return false;
            }

            return targetSite.CanEnter(caravan);
        }

        public static IEnumerable<FloatMenuOption> GetFloatMenuOptions(
            Caravan caravan,
            WorldObject_TokraDistressCallSite targetSite)
        {
            return CaravanArrivalActionUtility.GetFloatMenuOptions(
                () => CanVisit(caravan, targetSite),
                () => new CaravanArrivalAction_TokraDistressCallSite(
                    targetSite),
                "GR_TokraDistressCall_TravelCommandLabel".Translate(),
                caravan,
                targetSite.Tile,
                targetSite);
        }
    }
}
