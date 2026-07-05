using System;
using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Optional temporary world battlefield for one exact Goa'uld domain pair.
    /// Ignoring the marker has no failure consequence. Entering it creates a
    /// normal encounter map driven by the validated local-battlefield component.
    /// </summary>
    public sealed class WorldObject_GoauldOpenConflictBattlefieldSite
        : MapParent
    {
        private Faction firstDomain;
        private Faction secondDomain;
        private float vanillaThreatPoints;
        private float detachmentPoints;
        private int expiryTick;
        private int battlefieldMapSize = 140;
        private bool battlefieldLaunched;
        private bool battlefieldResolved;
        private bool trackerResolutionNotified;

        public Faction FirstDomain => firstDomain;
        public Faction SecondDomain => secondDomain;
        public float VanillaThreatPoints => vanillaThreatPoints;
        public float DetachmentPoints => detachmentPoints;
        public bool BattlefieldResolved => battlefieldResolved;

        public int RemainingTicks
        {
            get
            {
                int currentTick = Find.TickManager?.TicksGame ?? 0;
                return Math.Max(0, expiryTick - currentTick);
            }
        }

        public void Initialize(
            Faction firstFaction,
            Faction secondFaction,
            float sourceThreatPoints,
            float pointsPerDetachment,
            int siteExpiryTick,
            int mapSize)
        {
            firstDomain = firstFaction;
            secondDomain = secondFaction;
            vanillaThreatPoints = Math.Max(0f, sourceThreatPoints);
            detachmentPoints = Math.Max(0f, pointsPerDetachment);
            expiryTick = Math.Max(0, siteExpiryTick);
            battlefieldMapSize = Math.Max(100, mapSize);
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_References.Look(
                ref firstDomain,
                "goauldWorldBattlefieldFirstDomain");
            Scribe_References.Look(
                ref secondDomain,
                "goauldWorldBattlefieldSecondDomain");
            Scribe_Values.Look(
                ref vanillaThreatPoints,
                "goauldWorldBattlefieldVanillaThreatPoints",
                0f);
            Scribe_Values.Look(
                ref detachmentPoints,
                "goauldWorldBattlefieldDetachmentPoints",
                0f);
            Scribe_Values.Look(
                ref expiryTick,
                "goauldWorldBattlefieldExpiryTick",
                0);
            Scribe_Values.Look(
                ref battlefieldMapSize,
                "goauldWorldBattlefieldMapSize",
                140);
            Scribe_Values.Look(
                ref battlefieldLaunched,
                "goauldWorldBattlefieldLaunched",
                false);
            Scribe_Values.Look(
                ref battlefieldResolved,
                "goauldWorldBattlefieldResolved",
                false);
            Scribe_Values.Look(
                ref trackerResolutionNotified,
                "goauldWorldBattlefieldTrackerResolutionNotified",
                false);
        }

        protected override void Tick()
        {
            base.Tick();

            if (battlefieldResolved || HasMap || expiryTick <= 0)
            {
                return;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;

            if (currentTick >= expiryTick)
            {
                ExpireIgnoredSite();
            }
        }

        public override IEnumerable<FloatMenuOption> GetFloatMenuOptions(
            Caravan caravan)
        {
            IEnumerable<FloatMenuOption> baseOptions = base.GetFloatMenuOptions(
                caravan);

            if (baseOptions != null)
            {
                foreach (FloatMenuOption option in baseOptions)
                {
                    yield return option;
                }
            }

            if (!IsValidPlayerCaravan(caravan) || battlefieldResolved)
            {
                yield break;
            }

            foreach (FloatMenuOption option in
                CaravanArrivalAction_GoauldOpenConflictBattlefieldSite
                    .GetFloatMenuOptions(caravan, this))
            {
                yield return option;
            }
        }

        public override string GetInspectString()
        {
            string baseInspectString = base.GetInspectString();
            string siteInspectString;

            if (battlefieldResolved)
            {
                siteInspectString =
                    "GR_GoauldOpenConflictWorldSite_InspectResolved".Translate();
            }
            else if (battlefieldLaunched || HasMap)
            {
                siteInspectString =
                    "GR_GoauldOpenConflictWorldSite_InspectActive".Translate(
                        DomainName(firstDomain),
                        DomainName(secondDomain));
            }
            else
            {
                siteInspectString =
                    "GR_GoauldOpenConflictWorldSite_InspectPending".Translate(
                        DomainName(firstDomain),
                        DomainName(secondDomain),
                        GetRemainingDurationString());
            }

            return string.IsNullOrEmpty(baseInspectString)
                ? siteInspectString
                : baseInspectString + "\n" + siteInspectString;
        }

        public override bool ShouldRemoveMapNow(
            out bool alsoRemoveWorldObject)
        {
            alsoRemoveWorldObject = false;

            if (!HasMap || !battlefieldResolved)
            {
                return false;
            }

            if (Map.mapPawns.AnyPawnBlockingMapRemoval
                || TransporterUtility.IncomingTransporterPreventingMapRemoval(
                    Map))
            {
                return false;
            }

            NotifyTrackerResolved();
            alsoRemoveWorldObject = true;
            return true;
        }

        public FloatMenuAcceptanceReport CanEnter(Caravan caravan)
        {
            if (!IsValidPlayerCaravan(caravan))
            {
                return false;
            }

            if (battlefieldResolved)
            {
                return FloatMenuAcceptanceReport.WithFailMessage(
                    "GR_GoauldOpenConflictWorldSite_AlreadyResolved"
                        .Translate());
            }

            return true;
        }

        public void EnterFromCaravan(Caravan caravan)
        {
            if (!IsValidPlayerCaravanAtSite(caravan))
            {
                Messages.Message(
                    "GR_GoauldOpenConflictWorldSite_RequiresCaravan".Translate(),
                    this,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            FloatMenuAcceptanceReport acceptance = CanEnter(caravan);

            if (!acceptance)
            {
                if (!acceptance.FailMessage.NullOrEmpty())
                {
                    Messages.Message(
                        acceptance.FailMessage,
                        this,
                        MessageTypeDefOf.RejectInput,
                        historical: false);
                }

                return;
            }

            bool generatedNewMap = !HasMap;
            Map map = GetOrGenerateMapUtility.GetOrGenerateMap(
                Tile,
                new IntVec3(
                    battlefieldMapSize,
                    1,
                    battlefieldMapSize),
                def);

            MapComponent_GoauldOpenConflictBattlefield component = map
                .GetComponent<MapComponent_GoauldOpenConflictBattlefield>();

            if (component == null || !component.Initialized)
            {
                if (!GoauldOpenConflictBattlefieldUtility
                        .TryStartWorldSiteMap(map, this))
                {
                    Messages.Message(
                        "GR_GoauldOpenConflictWorldSite_GenerationFailed"
                            .Translate(),
                        this,
                        MessageTypeDefOf.RejectInput,
                        historical: false);
                    return;
                }

                component = map.GetComponent<
                    MapComponent_GoauldOpenConflictBattlefield>();
            }

            battlefieldLaunched = true;
            GameComponent_GoauldOpenConflictBattlefieldTracker.Current
                ?.NotifyWorldSiteMapGenerated(ID, map.uniqueID);

            if (generatedNewMap)
            {
                Find.TickManager.Notify_GeneratedPotentiallyHostileMap();
            }
            Predicate<IntVec3> entryValidator = component == null
                ? null
                : (Predicate<IntVec3>)component.IsPreferredPlayerEntryCell;

            CaravanEnterMapUtility.Enter(
                caravan,
                map,
                CaravanEnterMode.Edge,
                CaravanDropInventoryMode.DoNotDrop,
                true,
                entryValidator);
        }

        public void NotifyBattlefieldResolved()
        {
            battlefieldResolved = true;
        }

        public void ExpireDebug()
        {
            if (!HasMap && !battlefieldResolved)
            {
                ExpireIgnoredSite();
            }
        }

        private void ExpireIgnoredSite()
        {
            if (battlefieldResolved)
            {
                return;
            }

            battlefieldResolved = true;
            NotifyTrackerResolved();

            if (!Destroyed)
            {
                Destroy();
            }
        }

        private void NotifyTrackerResolved()
        {
            if (trackerResolutionNotified)
            {
                return;
            }

            trackerResolutionNotified = true;
            GameComponent_GoauldOpenConflictBattlefieldTracker.Current
                ?.NotifyWorldSiteResolved(ID);
        }

        private static bool IsValidPlayerCaravan(Caravan caravan)
        {
            return caravan != null
                && !caravan.Destroyed
                && caravan.Faction == Faction.OfPlayer;
        }

        private bool IsValidPlayerCaravanAtSite(Caravan caravan)
        {
            return IsValidPlayerCaravan(caravan) && caravan.Tile == Tile;
        }

        private string GetRemainingDurationString()
        {
            int totalHours = Math.Max(
                1,
                (int)Math.Ceiling(RemainingTicks / 2500f));
            int days = totalHours / 24;
            int hours = totalHours % 24;

            string hourText = hours == 1
                ? "GR_GoauldOpenConflictWorldSite_DurationOneHour"
                    .Translate()
                : "GR_GoauldOpenConflictWorldSite_DurationHours"
                    .Translate(hours);

            if (days <= 0)
            {
                return totalHours == 1
                    ? "GR_GoauldOpenConflictWorldSite_DurationOneHour"
                        .Translate()
                    : "GR_GoauldOpenConflictWorldSite_DurationHours"
                        .Translate(totalHours);
            }

            string dayText = days == 1
                ? "GR_GoauldOpenConflictWorldSite_DurationOneDay"
                    .Translate()
                : "GR_GoauldOpenConflictWorldSite_DurationDays"
                    .Translate(days);

            if (hours <= 0)
            {
                return dayText;
            }

            return "GR_GoauldOpenConflictWorldSite_DurationCombined"
                .Translate(dayText, hourText);
        }

        private static string DomainName(Faction faction)
        {
            return faction?.Name ?? "<missing domain>";
        }
    }
}
