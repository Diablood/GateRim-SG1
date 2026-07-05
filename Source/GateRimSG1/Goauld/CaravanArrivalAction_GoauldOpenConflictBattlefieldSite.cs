using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Standard caravan-arrival action for the optional Goa'uld battlefield
    /// site. The action persists while travelling and enters the generated map
    /// automatically when the caravan reaches the tile.
    /// </summary>
    public sealed class CaravanArrivalAction_GoauldOpenConflictBattlefieldSite
        : CaravanArrivalAction
    {
        private WorldObject_GoauldOpenConflictBattlefieldSite site;

        public override string Label
            => "GR_GoauldOpenConflictWorldSite_TravelCommand".Translate();

        public override string ReportString
            => "GR_GoauldOpenConflictWorldSite_Approaching".Translate();

        public CaravanArrivalAction_GoauldOpenConflictBattlefieldSite()
        {
        }

        public CaravanArrivalAction_GoauldOpenConflictBattlefieldSite(
            WorldObject_GoauldOpenConflictBattlefieldSite targetSite)
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
            Scribe_References.Look(ref site, "goauldBattlefieldSite");
        }

        public static FloatMenuAcceptanceReport CanVisit(
            Caravan caravan,
            WorldObject_GoauldOpenConflictBattlefieldSite targetSite)
        {
            if (targetSite == null || targetSite.Destroyed)
            {
                return false;
            }

            return targetSite.CanEnter(caravan);
        }

        public static IEnumerable<FloatMenuOption> GetFloatMenuOptions(
            Caravan caravan,
            WorldObject_GoauldOpenConflictBattlefieldSite targetSite)
        {
            return CaravanArrivalActionUtility.GetFloatMenuOptions(
                () => CanVisit(caravan, targetSite),
                () => new CaravanArrivalAction_GoauldOpenConflictBattlefieldSite(
                    targetSite),
                "GR_GoauldOpenConflictWorldSite_TravelCommand".Translate(),
                caravan,
                targetSite.Tile,
                targetSite);
        }
    }
}
