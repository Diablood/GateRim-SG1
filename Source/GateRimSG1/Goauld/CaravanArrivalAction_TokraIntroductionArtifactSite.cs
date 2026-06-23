using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    public sealed class CaravanArrivalAction_TokraIntroductionArtifactSite
        : CaravanArrivalAction
    {
        private WorldObject_TokraIntroductionArtifactSite site;

        public override string Label
            => "GR_TokraIntroduction_TravelCommandLabel".Translate();

        public override string ReportString
            => "GR_TokraIntroduction_ApproachingReport".Translate();

        public CaravanArrivalAction_TokraIntroductionArtifactSite()
        {
        }

        public CaravanArrivalAction_TokraIntroductionArtifactSite(
            WorldObject_TokraIntroductionArtifactSite targetSite)
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
            Scribe_References.Look(ref site, "introductionArtifactSite");
        }

        public static FloatMenuAcceptanceReport CanVisit(
            Caravan caravan,
            WorldObject_TokraIntroductionArtifactSite targetSite)
        {
            if (targetSite == null || targetSite.Destroyed)
            {
                return false;
            }

            return targetSite.CanEnter(caravan);
        }

        public static IEnumerable<FloatMenuOption> GetFloatMenuOptions(
            Caravan caravan,
            WorldObject_TokraIntroductionArtifactSite targetSite)
        {
            return CaravanArrivalActionUtility.GetFloatMenuOptions(
                () => CanVisit(caravan, targetSite),
                () => new CaravanArrivalAction_TokraIntroductionArtifactSite(
                    targetSite),
                "GR_TokraIntroduction_TravelCommandLabel".Translate(),
                caravan,
                targetSite.Tile,
                targetSite);
        }
    }
}
