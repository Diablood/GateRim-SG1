using System;
using System.Collections.Generic;
using GateRimSG1.Missions;
using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Hidden Tok'ra rendezvous represented only for the duration of one
    /// manufacturing-and-delivery contract.
    /// </summary>
    public sealed class WorldObject_TokraTemporaryBaseDeliverySite
        : WorldObject
    {
        private const int InterceptionCheckIntervalTicks = 250;

        private int homeMapId = -1;
        private string missionDefName;
        private string requiredThingDefName;
        private int requiredCount;
        private bool requireQuality;
        private QualityCategory minimumQuality = QualityCategory.Normal;
        private float minimumHitPointsPercent = 0.80f;
        private int deadlineTick;
        private int finalExpiryTick;
        private bool lateWindowStarted;
        private bool lateWarningSent;
        private bool interceptionStateInitialized;
        private bool interceptionPlanned;
        private bool interceptionTriggered;
        private int interceptionTriggerTick;
        private int nextInterceptionCheckTick;
        private float interceptionThreatPoints;
        private bool destinationCompromiseStateInitialized;
        private bool destinationCompromisePlanned;
        private bool destinationCompromiseTriggered;
        private bool destinationCompromiseCleared;
        private PlanetTile destinationCompromiseEncounterTile
            = PlanetTile.Invalid;
        private bool destinationCompromiseEncounterObserved;
        private int destinationCompromiseEncounterWorldObjectId = -1;
        private int nextDestinationCompromiseCheckTick;
        private float destinationCompromiseThreatPoints;
        private bool managerResolved;
        private bool missionSucceeded;

        public bool IsLate => lateWindowStarted && !managerResolved;

        public void Initialize(
            int sourceMapId,
            string sourceMissionDefName,
            string thingDefName,
            int count,
            bool qualityRequired,
            QualityCategory quality,
            float minimumHitPoints,
            int missionDeadlineTick,
            int missionFinalExpiryTick,
            bool planInterception,
            int plannedInterceptionTick,
            float plannedInterceptionThreatPoints,
            bool planDestinationCompromise,
            float plannedDestinationCompromiseThreatPoints)
        {
            homeMapId = sourceMapId;
            missionDefName = sourceMissionDefName;
            requiredThingDefName = thingDefName;
            requiredCount = Math.Max(1, count);
            requireQuality = qualityRequired;
            minimumQuality = quality;
            minimumHitPointsPercent = Mathf.Clamp01(minimumHitPoints);
            deadlineTick = missionDeadlineTick;
            finalExpiryTick = Math.Max(
                missionDeadlineTick + 1,
                missionFinalExpiryTick);
            interceptionStateInitialized = true;
            interceptionPlanned = planInterception;
            interceptionTriggered = false;
            interceptionTriggerTick = plannedInterceptionTick;
            nextInterceptionCheckTick = plannedInterceptionTick;
            interceptionThreatPoints = Math.Max(
                0f,
                plannedInterceptionThreatPoints);
            destinationCompromiseStateInitialized = true;
            destinationCompromisePlanned = planDestinationCompromise;
            destinationCompromiseTriggered = false;
            destinationCompromiseCleared = false;
            destinationCompromiseEncounterTile = PlanetTile.Invalid;
            destinationCompromiseEncounterObserved = false;
            destinationCompromiseEncounterWorldObjectId = -1;
            nextDestinationCompromiseCheckTick = Find.TickManager?.TicksGame ?? 0;
            destinationCompromiseThreatPoints = Math.Max(
                0f,
                plannedDestinationCompromiseThreatPoints);
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref homeMapId, "deliveryHomeMapId", -1);
            Scribe_Values.Look(
                ref missionDefName,
                "deliveryMissionDefName");
            Scribe_Values.Look(
                ref requiredThingDefName,
                "deliveryThingDefName");
            Scribe_Values.Look(
                ref requiredCount,
                "deliveryRequiredCount",
                0);
            Scribe_Values.Look(
                ref requireQuality,
                "deliveryRequireQuality",
                false);
            Scribe_Values.Look(
                ref minimumQuality,
                "deliveryMinimumQuality",
                QualityCategory.Normal);
            Scribe_Values.Look(
                ref minimumHitPointsPercent,
                "deliveryMinimumHitPointsPercent",
                0.80f);
            Scribe_Values.Look(
                ref deadlineTick,
                "deliveryExpiryTick",
                0);
            Scribe_Values.Look(
                ref finalExpiryTick,
                "deliveryFinalExpiryTick",
                0);
            Scribe_Values.Look(
                ref lateWindowStarted,
                "deliveryLateWindowStarted",
                false);
            Scribe_Values.Look(
                ref lateWarningSent,
                "deliveryLateWarningSent",
                false);
            Scribe_Values.Look(
                ref interceptionStateInitialized,
                "deliveryInterceptionStateInitialized",
                false);
            Scribe_Values.Look(
                ref interceptionPlanned,
                "deliveryInterceptionPlanned",
                false);
            Scribe_Values.Look(
                ref interceptionTriggered,
                "deliveryInterceptionTriggered",
                false);
            Scribe_Values.Look(
                ref interceptionTriggerTick,
                "deliveryInterceptionTriggerTick",
                0);
            Scribe_Values.Look(
                ref nextInterceptionCheckTick,
                "deliveryNextInterceptionCheckTick",
                0);
            Scribe_Values.Look(
                ref interceptionThreatPoints,
                "deliveryInterceptionThreatPoints",
                0f);
            Scribe_Values.Look(
                ref destinationCompromiseStateInitialized,
                "deliveryDestinationCompromiseStateInitialized",
                false);
            Scribe_Values.Look(
                ref destinationCompromisePlanned,
                "deliveryDestinationCompromisePlanned",
                false);
            Scribe_Values.Look(
                ref destinationCompromiseTriggered,
                "deliveryDestinationCompromiseTriggered",
                false);
            Scribe_Values.Look(
                ref destinationCompromiseCleared,
                "deliveryDestinationCompromiseCleared",
                false);
            string serializedDestinationCompromiseEncounterTile
                = destinationCompromiseEncounterTile.Valid
                    ? destinationCompromiseEncounterTile.ToString()
                    : null;
            Scribe_Values.Look(
                ref serializedDestinationCompromiseEncounterTile,
                "deliveryDestinationCompromiseEncounterTile");

            if (Scribe.mode == LoadSaveMode.LoadingVars)
            {
                destinationCompromiseEncounterTile
                    = !string.IsNullOrWhiteSpace(
                            serializedDestinationCompromiseEncounterTile)
                        && PlanetTile.TryParse(
                            serializedDestinationCompromiseEncounterTile,
                            out PlanetTile loadedEncounterTile)
                        ? loadedEncounterTile
                        : PlanetTile.Invalid;
            }

            Scribe_Values.Look(
                ref destinationCompromiseEncounterObserved,
                "deliveryDestinationCompromiseEncounterObserved",
                false);
            Scribe_Values.Look(
                ref destinationCompromiseEncounterWorldObjectId,
                "deliveryDestinationCompromiseEncounterWorldObjectId",
                -1);
            Scribe_Values.Look(
                ref nextDestinationCompromiseCheckTick,
                "deliveryNextDestinationCompromiseCheckTick",
                0);
            Scribe_Values.Look(
                ref destinationCompromiseThreatPoints,
                "deliveryDestinationCompromiseThreatPoints",
                0f);
            Scribe_Values.Look(
                ref managerResolved,
                "deliveryManagerResolved",
                false);
            Scribe_Values.Look(
                ref missionSucceeded,
                "deliveryMissionSucceeded",
                false);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                MigrateLoadedState();
            }
        }

        protected override void Tick()
        {
            base.Tick();

            if (managerResolved || deadlineTick <= 0)
            {
                return;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;

            if (finalExpiryTick > 0 && currentTick >= finalExpiryTick)
            {
                Expire();
                return;
            }

            EnsureLateState(currentTick);

            if (interceptionPlanned
                && !interceptionTriggered
                && currentTick >= interceptionTriggerTick
                && currentTick >= nextInterceptionCheckTick)
            {
                TryTriggerInterception(currentTick);
            }

            if (destinationCompromiseTriggered
                && !destinationCompromiseCleared)
            {
                TryMarkDestinationCompromiseCleared(currentTick);
            }

            if (destinationCompromisePlanned
                && !destinationCompromiseTriggered
                && currentTick >= nextDestinationCompromiseCheckTick)
            {
                TryTriggerDestinationCompromise(currentTick);
            }
        }

        public override IEnumerable<FloatMenuOption> GetFloatMenuOptions(
            Caravan caravan)
        {
            IEnumerable<FloatMenuOption> baseOptions
                = base.GetFloatMenuOptions(caravan);

            if (baseOptions != null)
            {
                foreach (FloatMenuOption option in baseOptions)
                {
                    yield return option;
                }
            }

            if (!IsValidPlayerCaravan(caravan) || managerResolved)
            {
                yield break;
            }

            foreach (FloatMenuOption option in
                CaravanArrivalAction_TokraTemporaryBaseDeliverySite
                    .GetFloatMenuOptions(caravan, this))
            {
                yield return option;
            }
        }

        public override IEnumerable<Gizmo> GetCaravanGizmos(Caravan caravan)
        {
            foreach (Gizmo gizmo in base.GetCaravanGizmos(caravan))
            {
                yield return gizmo;
            }

            if (!IsValidPlayerCaravanAtSite(caravan) || managerResolved)
            {
                yield break;
            }

            Command_Action command = new Command_Action
            {
                defaultLabel = "GR_TokraTemporaryBaseDelivery_DeliverCommandLabel"
                    .Translate(),
                defaultDesc = "GR_TokraTemporaryBaseDelivery_DeliverCommandDesc"
                    .Translate(
                        requiredCount.ToString(),
                        RequiredThingDef?.label ?? "?",
                        GetQualityLabel()),
                icon = Settlement.ShowSellableItemsCommand,
                action = () => Deliver(caravan)
            };

            FloatMenuAcceptanceReport acceptance = CanDeliver(caravan);

            if (!acceptance)
            {
                command.Disable(
                    acceptance.FailMessage.NullOrEmpty()
                        ? null
                        : acceptance.FailMessage.ToString());
            }

            yield return command;
        }

        public override string GetInspectString()
        {
            string baseInspect = base.GetInspectString();
            string details;

            if (managerResolved)
            {
                details = missionSucceeded
                    ? "GR_TokraTemporaryBaseDelivery_InspectSucceeded"
                        .Translate()
                    : "GR_TokraTemporaryBaseDelivery_InspectFailed"
                        .Translate();
            }
            else if (destinationCompromiseTriggered
                && !destinationCompromiseCleared)
            {
                details = "GR_TokraTemporaryBaseDelivery_InspectCompromised"
                    .Translate();
            }
            else if (lateWindowStarted)
            {
                details = "GR_TokraTemporaryBaseDelivery_InspectLate"
                    .Translate(
                        requiredCount.ToString(),
                        RequiredThingDef?.label?.CapitalizeFirst() ?? "?",
                        GetQualityLabel(),
                        GetRemainingHoursString());
            }
            else
            {
                details = "GR_TokraTemporaryBaseDelivery_InspectPending"
                    .Translate(
                        requiredCount.ToString(),
                        RequiredThingDef?.label?.CapitalizeFirst() ?? "?",
                        GetQualityLabel(),
                        GetRemainingHoursString());
            }

            return string.IsNullOrEmpty(baseInspect)
                ? details
                : baseInspect + "\n" + details;
        }

        public FloatMenuAcceptanceReport CanVisit(Caravan caravan)
        {
            if (!IsValidPlayerCaravan(caravan))
            {
                return false;
            }

            if (managerResolved)
            {
                return FloatMenuAcceptanceReport.WithFailMessage(
                    "GR_TokraTemporaryBaseDelivery_AlreadyResolved"
                        .Translate());
            }

            return RequiredThingDef == null
                ? FloatMenuAcceptanceReport.WithFailMessage(
                    "GR_TokraTemporaryBaseDelivery_InvalidContract"
                        .Translate())
                : true;
        }

        public FloatMenuAcceptanceReport CanDeliver(Caravan caravan)
        {
            FloatMenuAcceptanceReport visitReport = CanVisit(caravan);

            if (!visitReport)
            {
                return visitReport;
            }

            if (destinationCompromisePlanned
                && !destinationCompromiseCleared)
            {
                return FloatMenuAcceptanceReport.WithFailMessage(
                    "GR_TokraTemporaryBaseDelivery_DestinationNotSecure"
                        .Translate());
            }

            ThingDef requiredDef = RequiredThingDef;
            int available = TokraTemporaryBaseDeliveryMissionUtility
                .CountMatchingThings(
                    caravan,
                    requiredDef,
                    requireQuality,
                    minimumQuality,
                    minimumHitPointsPercent);

            if (available < requiredCount)
            {
                return FloatMenuAcceptanceReport.WithFailMessage(
                    "GR_TokraTemporaryBaseDelivery_NotEnoughCargo"
                        .Translate(
                            available.ToString(),
                            requiredCount.ToString(),
                            requiredDef.label));
            }

            return true;
        }

        public void NotifyCaravanArrived(Caravan caravan)
        {
            if (!IsValidPlayerCaravanAtSite(caravan) || managerResolved)
            {
                return;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;

            if (destinationCompromiseTriggered
                && !destinationCompromiseCleared)
            {
                TryMarkDestinationCompromiseCleared(currentTick);

                if (!destinationCompromiseCleared)
                {
                    Messages.Message(
                        "GR_TokraTemporaryBaseDelivery_DestinationNotSecure"
                            .Translate(),
                        caravan,
                        MessageTypeDefOf.RejectInput,
                        historical: false);
                    return;
                }
            }

            if (destinationCompromisePlanned
                && !destinationCompromiseTriggered)
            {
                destinationCompromisePlanned = false;
                destinationCompromiseCleared = true;

                GR_Log.Warning(
                    "Skipped the planned Tok'ra delivery approach ambush "
                    + $"because caravan {caravan.ID} reached site {ID} "
                    + "without an eligible final-approach incident tile.");
            }

            Messages.Message(
                "GR_TokraTemporaryBaseDelivery_ArrivedAtRendezvous"
                    .Translate(),
                caravan,
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        public void Deliver(Caravan caravan)
        {
            if (!IsValidPlayerCaravanAtSite(caravan))
            {
                Messages.Message(
                    "GR_TokraTemporaryBaseDelivery_RequiresCaravan"
                        .Translate(),
                    this,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            FloatMenuAcceptanceReport acceptance = CanDeliver(caravan);

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

            if (!TokraTemporaryBaseDeliveryMissionUtility
                .TryConsumeMatchingThings(
                    caravan,
                    RequiredThingDef,
                    requiredCount,
                    requireQuality,
                    minimumQuality,
                    minimumHitPointsPercent))
            {
                Messages.Message(
                    "GR_TokraTemporaryBaseDelivery_TransferFailed"
                        .Translate(),
                    this,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            if (!GameComponent_TokraOrganicOperationManager
                .NotifyTemporaryBaseDeliverySiteResolved(
                    this,
                    succeeded: true,
                    failureTextId: null,
                    deliveredLate: lateWindowStarted))
            {
                NotifyManagerResolved(true);
            }
        }

        public bool DebugEnterLateWindow()
        {
            if (managerResolved)
            {
                return false;
            }

            TokraOrganicOperationDefinition definition = ResolveDefinition();
            int graceTicks = Math.Max(
                1,
                definition?.Delivery?.lateGraceTicks ?? 120000);
            int currentTick = Find.TickManager?.TicksGame ?? 0;

            deadlineTick = currentTick;
            finalExpiryTick = currentTick + graceTicks;
            lateWindowStarted = false;
            lateWarningSent = false;
            EnsureLateState(currentTick);
            return lateWindowStarted;
        }

        public bool DebugExpireContract()
        {
            if (managerResolved)
            {
                return false;
            }

            finalExpiryTick = Find.TickManager?.TicksGame ?? 0;
            Expire();
            return true;
        }

        public bool DebugForceInterception()
        {
            if (managerResolved || interceptionTriggered)
            {
                return false;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            interceptionStateInitialized = true;
            interceptionPlanned = true;
            interceptionTriggerTick = currentTick;
            nextInterceptionCheckTick = currentTick;

            if (interceptionThreatPoints <= 0f)
            {
                Caravan caravan = FindEligibleInterceptionCaravan();
                GateRimMissionDeliveryDef profile = ResolveDefinition()?.Delivery;
                float factor = Math.Max(
                    0.01f,
                    profile?.interceptionThreatFactor ?? 0.65f);
                float basePoints = caravan == null
                    ? 0f
                    : StorytellerUtility.DefaultThreatPointsNow(caravan);
                interceptionThreatPoints = ClampInterceptionPoints(
                    basePoints * factor,
                    profile);
            }

            TryTriggerInterception(currentTick);
            return true;
        }

        public bool DebugForceDestinationCompromise()
        {
            if (managerResolved || destinationCompromiseTriggered)
            {
                return false;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            destinationCompromiseStateInitialized = true;
            destinationCompromisePlanned = true;
            destinationCompromiseCleared = false;
            destinationCompromiseEncounterTile = PlanetTile.Invalid;
            destinationCompromiseEncounterObserved = false;
            destinationCompromiseEncounterWorldObjectId = -1;
            nextDestinationCompromiseCheckTick = currentTick;

            if (!interceptionTriggered)
            {
                interceptionPlanned = false;
            }

            if (destinationCompromiseThreatPoints <= 0f)
            {
                GateRimMissionDeliveryDef profile = ResolveDefinition()?.Delivery;
                float factor = Math.Max(
                    0.01f,
                    profile?.destinationCompromiseThreatFactor ?? 0.80f);
                float interceptionFactor = Math.Max(
                    0.01f,
                    profile?.interceptionThreatFactor ?? 0.65f);
                float capturedThreatPoints = interceptionThreatPoints > 0f
                    ? interceptionThreatPoints / interceptionFactor
                    : 0f;
                destinationCompromiseThreatPoints
                    = ClampDestinationCompromisePoints(
                        capturedThreatPoints * factor,
                        profile);
            }

            TryTriggerDestinationCompromise(currentTick);
            return true;
        }

        public void NotifyManagerResolved(bool succeeded)
        {
            if (managerResolved)
            {
                return;
            }

            managerResolved = true;
            missionSucceeded = succeeded;

            if (!Destroyed)
            {
                Destroy();
            }
        }

        private ThingDef RequiredThingDef
            => string.IsNullOrWhiteSpace(requiredThingDefName)
                ? null
                : DefDatabase<ThingDef>.GetNamedSilentFail(
                    requiredThingDefName);

        private void MigrateLoadedState()
        {
            TokraOrganicOperationDefinition definition = ResolveDefinition();
            int graceTicks = Math.Max(
                1,
                definition?.Delivery?.lateGraceTicks ?? 120000);

            if (deadlineTick > 0 && finalExpiryTick <= deadlineTick)
            {
                finalExpiryTick = deadlineTick + graceTicks;
            }

            if (!interceptionStateInitialized)
            {
                interceptionStateInitialized = true;
                interceptionPlanned = false;
                interceptionTriggered = false;
                interceptionTriggerTick = 0;
                nextInterceptionCheckTick = 0;
                interceptionThreatPoints = 0f;
            }

            if (!destinationCompromiseStateInitialized)
            {
                destinationCompromiseStateInitialized = true;
                destinationCompromisePlanned = false;
                destinationCompromiseTriggered = false;
                destinationCompromiseCleared = false;
                destinationCompromiseEncounterTile = PlanetTile.Invalid;
                destinationCompromiseEncounterObserved = false;
                destinationCompromiseEncounterWorldObjectId = -1;
                nextDestinationCompromiseCheckTick = 0;
                destinationCompromiseThreatPoints = 0f;
            }
        }

        private void EnsureLateState(int currentTick)
        {
            if (!lateWindowStarted && currentTick >= deadlineTick)
            {
                lateWindowStarted = true;
            }

            if (lateWindowStarted && !lateWarningSent)
            {
                lateWarningSent = GameComponent_TokraOrganicOperationManager
                    .NotifyTemporaryBaseDeliveryLateWindowStarted(this);
            }
        }

        private void Expire()
        {
            if (!GameComponent_TokraOrganicOperationManager
                .NotifyTemporaryBaseDeliverySiteResolved(
                    this,
                    succeeded: false,
                    failureTextId: "failureGraceExpired",
                    deliveredLate: false))
            {
                NotifyManagerResolved(false);
            }
        }

        private bool TryTriggerInterception(int currentTick)
        {
            GateRimMissionDeliveryDef profile = ResolveDefinition()?.Delivery;
            int retryTicks = Math.Max(
                InterceptionCheckIntervalTicks,
                profile?.interceptionRetryTicks ?? 2500);
            nextInterceptionCheckTick = currentTick + retryTicks;

            Caravan caravan = FindEligibleInterceptionCaravan();

            if (caravan == null)
            {
                return false;
            }

            string incidentDefName = profile?.interceptionIncidentDefName;
            IncidentDef incidentDef = string.IsNullOrWhiteSpace(incidentDefName)
                ? null
                : DefDatabase<IncidentDef>.GetNamedSilentFail(
                    incidentDefName);

            if (incidentDef?.Worker == null)
            {
                GR_Log.Error(
                    "Cannot trigger Tok'ra delivery interception: incident "
                    + $"{incidentDefName ?? "none"} is unavailable.");
                interceptionPlanned = false;
                return false;
            }

            if (interceptionThreatPoints <= 0f)
            {
                float factor = Math.Max(
                    0.01f,
                    profile?.interceptionThreatFactor ?? 0.65f);
                interceptionThreatPoints = ClampInterceptionPoints(
                    StorytellerUtility.DefaultThreatPointsNow(caravan) * factor,
                    profile);
            }

            IncidentParms parms = StorytellerUtility.DefaultParmsNow(
                incidentDef.category,
                caravan);
            parms.forced = true;
            parms.faction = GoauldSystemLordFactionUtility.GetOrCreateFaction(
                "Tok'ra temporary-base delivery interception");
            parms.points = interceptionThreatPoints;

            bool succeeded = incidentDef.Worker.TryExecute(parms);

            if (succeeded)
            {
                interceptionTriggered = true;
                interceptionPlanned = false;
                GameComponent_TokraOrganicOperationManager
                    .NotifyTemporaryBaseDeliveryInterceptionTriggered(
                        this,
                        caravan,
                        interceptionThreatPoints);
            }

            GR_Log.Message(
                "Tok'ra temporary-base delivery interception attempt: "
                + $"success={succeeded}; site={ID}; caravan={caravan.ID}; "
                + $"points={interceptionThreatPoints:0}; "
                + $"nextRetry={nextInterceptionCheckTick}.");

            return succeeded;
        }

        private Caravan FindEligibleInterceptionCaravan()
        {
            if (Find.WorldObjects?.Caravans == null)
            {
                return null;
            }

            for (int i = 0; i < Find.WorldObjects.Caravans.Count; i++)
            {
                Caravan caravan = Find.WorldObjects.Caravans[i];

                if (!IsValidPlayerCaravan(caravan)
                    || caravan.Tile == Tile
                    || caravan.pather == null
                    || !caravan.pather.Moving
                    || caravan.pather.Destination != Tile
                    || !(caravan.pather.ArrivalAction
                        is CaravanArrivalAction_TokraTemporaryBaseDeliverySite
                            arrivalAction)
                    || !arrivalAction.Targets(this))
                {
                    continue;
                }

                int matchingCount = TokraTemporaryBaseDeliveryMissionUtility
                    .CountMatchingThings(
                        caravan,
                        RequiredThingDef,
                        requireQuality,
                        minimumQuality,
                        minimumHitPointsPercent);

                if (matchingCount >= requiredCount)
                {
                    return caravan;
                }
            }

            return null;
        }

        private bool TryTriggerDestinationCompromise(
            int currentTick,
            Caravan preferredCaravan = null)
        {
            Caravan caravan = IsEligibleDestinationCompromiseCaravan(
                    preferredCaravan)
                ? preferredCaravan
                : FindDestinationCompromiseCaravan();

            if (caravan == null)
            {
                nextDestinationCompromiseCheckTick = currentTick + 1;
                return false;
            }

            GateRimMissionDeliveryDef profile = ResolveDefinition()?.Delivery;
            int retryTicks = Math.Max(
                InterceptionCheckIntervalTicks,
                profile?.destinationCompromiseRetryTicks ?? 2500);
            nextDestinationCompromiseCheckTick = currentTick + retryTicks;

            string incidentDefName
                = profile?.destinationCompromiseIncidentDefName;
            IncidentDef incidentDef = string.IsNullOrWhiteSpace(incidentDefName)
                ? null
                : DefDatabase<IncidentDef>.GetNamedSilentFail(
                    incidentDefName);

            if (incidentDef?.Worker == null)
            {
                GR_Log.Error(
                    "Cannot trigger compromised Tok'ra delivery approach: "
                    + $"incident {incidentDefName ?? "none"} is unavailable.");
                destinationCompromisePlanned = false;
                return false;
            }

            if (destinationCompromiseThreatPoints <= 0f)
            {
                float factor = Math.Max(
                    0.01f,
                    profile?.destinationCompromiseThreatFactor ?? 0.80f);
                destinationCompromiseThreatPoints
                    = ClampDestinationCompromisePoints(
                        StorytellerUtility.DefaultThreatPointsNow(caravan)
                            * factor,
                        profile);
            }

            PlanetTile encounterTile = caravan.Tile;
            IncidentParms parms = StorytellerUtility.DefaultParmsNow(
                incidentDef.category,
                caravan);
            parms.forced = true;
            parms.faction = GoauldSystemLordFactionUtility.GetOrCreateFaction(
                "Tok'ra temporary-base compromised approach");
            parms.points = destinationCompromiseThreatPoints;

            bool succeeded = incidentDef.Worker.TryExecute(parms);

            if (succeeded)
            {
                destinationCompromiseTriggered = true;
                destinationCompromiseEncounterTile = encounterTile;
                destinationCompromiseEncounterObserved = false;
                destinationCompromiseEncounterWorldObjectId = -1;
                nextDestinationCompromiseCheckTick = currentTick + 1;

                GR_Log.Message(
                    "Tok'ra temporary-base approach compromise triggered: "
                    + $"site={ID}; caravan={caravan.ID}; "
                    + $"encounterTile={encounterTile}; "
                    + $"points={destinationCompromiseThreatPoints:0}; "
                    + "encounterWorldObject=pending.");
            }

            GR_Log.Message(
                "Tok'ra temporary-base approach compromise attempt: "
                + $"success={succeeded}; site={ID}; caravan={caravan.ID}; "
                + $"tile={encounterTile}; "
                + $"points={destinationCompromiseThreatPoints:0}; "
                + $"nextRetry={nextDestinationCompromiseCheckTick}.");

            return succeeded;
        }

        private bool TryMarkDestinationCompromiseCleared(int currentTick)
        {
            if (!destinationCompromiseTriggered
                || destinationCompromiseCleared
                || currentTick < nextDestinationCompromiseCheckTick)
            {
                return false;
            }

            if (HasActiveDestinationEncounter())
            {
                nextDestinationCompromiseCheckTick = currentTick + 60;
                return false;
            }

            destinationCompromiseCleared = true;
            destinationCompromisePlanned = false;

            Messages.Message(
                "GR_TokraTemporaryBaseDelivery_DestinationCleared"
                    .Translate(),
                this,
                MessageTypeDefOf.PositiveEvent,
                historical: false);

            GR_Log.Message(
                "Tok'ra temporary-base compromised approach cleared: "
                + $"site={ID}.");
            return true;
        }

        private Caravan FindDestinationCompromiseCaravan()
        {
            if (Find.WorldObjects?.Caravans == null)
            {
                return null;
            }

            for (int i = 0; i < Find.WorldObjects.Caravans.Count; i++)
            {
                Caravan caravan = Find.WorldObjects.Caravans[i];

                if (IsEligibleDestinationCompromiseCaravan(caravan))
                {
                    return caravan;
                }
            }

            return null;
        }

        private bool IsEligibleDestinationCompromiseCaravan(
            Caravan caravan)
        {
            if (!IsValidPlayerCaravan(caravan)
                || caravan.Tile == Tile
                || caravan.pather == null
                || !caravan.pather.Moving
                || caravan.pather.Destination != Tile
                || caravan.pather.nextTile != Tile
                || !(caravan.pather.ArrivalAction
                    is CaravanArrivalAction_TokraTemporaryBaseDeliverySite
                        arrivalAction)
                || !arrivalAction.Targets(this))
            {
                return false;
            }

            int matchingCount = TokraTemporaryBaseDeliveryMissionUtility
                .CountMatchingThings(
                    caravan,
                    RequiredThingDef,
                    requireQuality,
                    minimumQuality,
                    minimumHitPointsPercent);

            return matchingCount >= requiredCount;
        }

        private bool HasActiveDestinationEncounter()
        {
            Map encounterMap = destinationCompromiseEncounterTile.Valid
                ? Current.Game?.FindMap(destinationCompromiseEncounterTile)
                : null;

            if (encounterMap != null)
            {
                RecordDestinationEncounter(encounterMap.Parent);
                return true;
            }

            if (Find.WorldObjects?.AllWorldObjects != null)
            {
                foreach (WorldObject worldObject
                    in Find.WorldObjects.AllWorldObjects)
                {
                    if (worldObject == null
                        || worldObject.Destroyed
                        || !(worldObject is MapParent mapParent))
                    {
                        continue;
                    }

                    bool matchesId
                        = destinationCompromiseEncounterWorldObjectId >= 0
                        && worldObject.ID
                            == destinationCompromiseEncounterWorldObjectId;
                    bool matchesTile
                        = destinationCompromiseEncounterTile.Valid
                        && worldObject.Tile
                            == destinationCompromiseEncounterTile;

                    if (!matchesId && !matchesTile)
                    {
                        continue;
                    }

                    if (mapParent.HasMap)
                    {
                        RecordDestinationEncounter(mapParent);
                        return true;
                    }

                    if (matchesId
                        && destinationCompromiseEncounterObserved)
                    {
                        return false;
                    }
                }
            }

            return !destinationCompromiseEncounterObserved;
        }

        private void RecordDestinationEncounter(MapParent mapParent)
        {
            if (mapParent == null)
            {
                return;
            }

            bool firstObservation = !destinationCompromiseEncounterObserved;
            destinationCompromiseEncounterObserved = true;
            destinationCompromiseEncounterWorldObjectId = mapParent.ID;
            destinationCompromiseEncounterTile = mapParent.Tile;

            if (firstObservation)
            {
                GR_Log.Message(
                    "Tracked Tok'ra temporary-base approach encounter: "
                    + $"site={ID}; worldObject={mapParent.ID}; "
                    + $"tile={mapParent.Tile}.");
            }
        }

        private TokraOrganicOperationDefinition ResolveDefinition()
        {
            if (string.IsNullOrWhiteSpace(missionDefName))
            {
                return null;
            }

            TokraOrganicOperationDefinition definition
                = TokraOrganicOperationFramework.GetDefinition(
                    TokraOrganicOperationArchetype.TemporaryBaseDelivery);

            return definition != null
                    && string.Equals(
                        definition.MissionDefName,
                        missionDefName,
                        StringComparison.Ordinal)
                ? definition
                : null;
        }

        private static float ClampInterceptionPoints(
            float points,
            GateRimMissionDeliveryDef profile)
        {
            float minimum = Math.Max(
                35f,
                profile?.interceptionMinimumPoints ?? 120f);
            float maximum = Math.Max(
                minimum,
                profile?.interceptionMaximumPoints ?? 2500f);
            return Mathf.Clamp(points, minimum, maximum);
        }

        private static float ClampDestinationCompromisePoints(
            float points,
            GateRimMissionDeliveryDef profile)
        {
            float minimum = Math.Max(
                35f,
                profile?.destinationCompromiseMinimumPoints ?? 150f);
            float maximum = Math.Max(
                minimum,
                profile?.destinationCompromiseMaximumPoints ?? 2800f);
            return Mathf.Clamp(points, minimum, maximum);
        }

        private bool IsValidPlayerCaravan(Caravan caravan)
        {
            return caravan != null
                && !caravan.Destroyed
                && caravan.Faction == Faction.OfPlayer;
        }

        private bool IsValidPlayerCaravanAtSite(Caravan caravan)
        {
            return IsValidPlayerCaravan(caravan) && caravan.Tile == Tile;
        }

        private string GetQualityLabel()
        {
            return requireQuality
                ? minimumQuality.GetLabel().CapitalizeFirst()
                : "GR_TokraTemporaryBaseDelivery_AnyQuality".Translate()
                    .ToString();
        }

        public string GetRemainingHoursString()
        {
            int currentTick = Find.TickManager?.TicksGame ?? 0;
            int targetTick = lateWindowStarted
                ? finalExpiryTick
                : deadlineTick;

            return Math.Ceiling(
                    Math.Max(0, targetTick - currentTick) / 2500f)
                .ToString("0");
        }
    }
}
