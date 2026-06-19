using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    internal sealed class TokraOrganicOperationDefinition
    {
        public TokraOrganicOperationDefinition(
            TokraOrganicOperationArchetype archetype,
            int offerDurationTicks,
            int readyDelayTicks,
            int deadlineTicks,
            int intellectualXp,
            int medicineXp,
            int socialXp,
            int successTrustChange,
            int failureTrustChange,
            float waryWeight,
            float neutralWeight,
            float cooperativeWeight,
            float trustedWeight,
            string objectiveThingDefName,
            string acceptActionKey,
            string completeActionKey,
            string offerLetterLabelKey,
            string offerLetterTextKey,
            string offerExpiredMessageKey,
            string offeredStatusKey,
            string activeStatusKey,
            string readyStatusKey,
            string successTrustMessageKey,
            string failureTrustMessageKey,
            string debugLabel)
        {
            Archetype = archetype;
            OfferDurationTicks = offerDurationTicks;
            ReadyDelayTicks = readyDelayTicks;
            DeadlineTicks = deadlineTicks;
            IntellectualXp = intellectualXp;
            MedicineXp = medicineXp;
            SocialXp = socialXp;
            SuccessTrustChange = successTrustChange;
            FailureTrustChange = failureTrustChange;
            WaryWeight = waryWeight;
            NeutralWeight = neutralWeight;
            CooperativeWeight = cooperativeWeight;
            TrustedWeight = trustedWeight;
            ObjectiveThingDefName = objectiveThingDefName;
            AcceptActionKey = acceptActionKey;
            CompleteActionKey = completeActionKey;
            OfferLetterLabelKey = offerLetterLabelKey;
            OfferLetterTextKey = offerLetterTextKey;
            OfferExpiredMessageKey = offerExpiredMessageKey;
            OfferedStatusKey = offeredStatusKey;
            ActiveStatusKey = activeStatusKey;
            ReadyStatusKey = readyStatusKey;
            SuccessTrustMessageKey = successTrustMessageKey;
            FailureTrustMessageKey = failureTrustMessageKey;
            DebugLabel = debugLabel;
        }

        public TokraOrganicOperationArchetype Archetype { get; }

        public int OfferDurationTicks { get; }

        public int ReadyDelayTicks { get; }

        public int DeadlineTicks { get; }

        public int IntellectualXp { get; }

        public int MedicineXp { get; }

        public int SocialXp { get; }

        public int SuccessTrustChange { get; }

        public int FailureTrustChange { get; }

        public string ObjectiveThingDefName { get; }

        public string AcceptActionKey { get; }

        public string CompleteActionKey { get; }

        public string OfferLetterLabelKey { get; }

        public string OfferLetterTextKey { get; }

        public string OfferExpiredMessageKey { get; }

        public string OfferedStatusKey { get; }

        public string ActiveStatusKey { get; }

        public string ReadyStatusKey { get; }

        public string SuccessTrustMessageKey { get; }

        public string FailureTrustMessageKey { get; }

        public string DebugLabel { get; }

        public bool HasPhysicalObjective => !string.IsNullOrEmpty(
            ObjectiveThingDefName);

        public bool UsesCommunicatorForCompletion => !string.IsNullOrEmpty(
            CompleteActionKey);

        private float WaryWeight { get; }

        private float NeutralWeight { get; }

        private float CooperativeWeight { get; }

        private float TrustedWeight { get; }

        public float GetWeight(TokraTrustTier tier)
        {
            switch (tier)
            {
                case TokraTrustTier.Wary:
                    return WaryWeight;
                case TokraTrustTier.Neutral:
                    return NeutralWeight;
                case TokraTrustTier.Cooperative:
                    return CooperativeWeight;
                case TokraTrustTier.Trusted:
                    return TrustedWeight;
                default:
                    return 0f;
            }
        }
    }

    internal static class TokraOrganicOperationFramework
    {
        public const float RepeatedArchetypeWeightFactor = 0.25f;

        private static readonly IReadOnlyDictionary<
            TokraOrganicOperationArchetype,
            TokraOrganicOperationDefinition> Definitions
                = new Dictionary<
                    TokraOrganicOperationArchetype,
                    TokraOrganicOperationDefinition>
                {
                    {
                        TokraOrganicOperationArchetype.GoauldObservation,
                        new TokraOrganicOperationDefinition(
                            TokraOrganicOperationArchetype.GoauldObservation,
                            offerDurationTicks: 120000,
                            readyDelayTicks: 15000,
                            deadlineTicks: 120000,
                            intellectualXp: 250,
                            medicineXp: 0,
                            socialXp: 0,
                            successTrustChange:
                                GameComponent_TokraTrustTracker
                                    .OrganicObservationSuccessTrustChange,
                            failureTrustChange:
                                GameComponent_TokraTrustTracker
                                    .OrganicObservationFailureTrustChange,
                            waryWeight: 0.60f,
                            neutralWeight: 1.00f,
                            cooperativeWeight: 0.85f,
                            trustedWeight: 0.35f,
                            objectiveThingDefName:
                                "SG1_TokraObservationDevice",
                            acceptActionKey:
                                "GR_TokraOrganicOperation_FloatMenuAcceptObservation",
                            completeActionKey:
                                "GR_TokraOrganicOperation_FloatMenuTransmitObservation",
                            offerLetterLabelKey:
                                "GR_TokraOrganicOperation_OfferLetterLabel",
                            offerLetterTextKey:
                                "GR_TokraOrganicOperation_OfferLetterText",
                            offerExpiredMessageKey:
                                "GR_TokraOrganicOperation_OfferExpired",
                            offeredStatusKey:
                                "GR_TokraOrganicOperation_StatusOffered",
                            activeStatusKey:
                                "GR_TokraOrganicOperation_StatusObserving",
                            readyStatusKey:
                                "GR_TokraOrganicOperation_StatusReady",
                            successTrustMessageKey:
                                "GR_TokraTrust_OrganicObservationSucceeded",
                            failureTrustMessageKey:
                                "GR_TokraTrust_OrganicObservationFailed",
                            debugLabel: "Goa'uldObservation")
                    },
                    {
                        TokraOrganicOperationArchetype.DeadDropRecovery,
                        new TokraOrganicOperationDefinition(
                            TokraOrganicOperationArchetype.DeadDropRecovery,
                            offerDurationTicks: 120000,
                            readyDelayTicks: 0,
                            deadlineTicks: 90000,
                            intellectualXp: 350,
                            medicineXp: 0,
                            socialXp: 0,
                            successTrustChange:
                                GameComponent_TokraTrustTracker
                                    .OrganicDeadDropSuccessTrustChange,
                            failureTrustChange:
                                GameComponent_TokraTrustTracker
                                    .OrganicDeadDropFailureTrustChange,
                            waryWeight: 0.35f,
                            neutralWeight: 0.85f,
                            cooperativeWeight: 1.00f,
                            trustedWeight: 0.55f,
                            objectiveThingDefName:
                                "SG1_TokraOrganicDeadDrop",
                            acceptActionKey:
                                "GR_TokraOrganicOperation_FloatMenuAcceptDeadDrop",
                            completeActionKey:
                                "GR_TokraOrganicOperation_FloatMenuAnalyzeIntelligence",
                            offerLetterLabelKey:
                                "GR_TokraOrganicOperation_DeadDropOfferLetterLabel",
                            offerLetterTextKey:
                                "GR_TokraOrganicOperation_DeadDropOfferLetterText",
                            offerExpiredMessageKey:
                                "GR_TokraOrganicOperation_DeadDropOfferExpired",
                            offeredStatusKey:
                                "GR_TokraOrganicOperation_StatusDeadDropOffered",
                            activeStatusKey:
                                "GR_TokraOrganicOperation_StatusDeadDropActive",
                            readyStatusKey:
                                "GR_TokraOrganicOperation_StatusDeadDropActive",
                            successTrustMessageKey:
                                "GR_TokraTrust_OrganicDeadDropSucceeded",
                            failureTrustMessageKey:
                                "GR_TokraTrust_OrganicDeadDropFailed",
                            debugLabel: "IntelligenceRecovery")
                    },
                    {
                        TokraOrganicOperationArchetype.WoundedAgentCare,
                        new TokraOrganicOperationDefinition(
                            TokraOrganicOperationArchetype.WoundedAgentCare,
                            offerDurationTicks: 120000,
                            readyDelayTicks: 0,
                            deadlineTicks: 300000,
                            intellectualXp: 0,
                            medicineXp: 0,
                            socialXp: 0,
                            successTrustChange:
                                GameComponent_TokraTrustTracker
                                    .OrganicWoundedAgentSuccessTrustChange,
                            failureTrustChange:
                                GameComponent_TokraTrustTracker
                                    .OrganicWoundedAgentFailureTrustChange,
                            waryWeight: 0.10f,
                            neutralWeight: 0.55f,
                            cooperativeWeight: 1.00f,
                            trustedWeight: 0.85f,
                            objectiveThingDefName: null,
                            acceptActionKey:
                                "GR_TokraWoundedAgent_Accept",
                            completeActionKey: null,
                            offerLetterLabelKey:
                                "GR_TokraWoundedAgent_OfferLabel",
                            offerLetterTextKey:
                                "GR_TokraWoundedAgent_OfferText",
                            offerExpiredMessageKey:
                                "GR_TokraWoundedAgent_OfferExpired",
                            offeredStatusKey:
                                "GR_TokraWoundedAgent_StatusOffered",
                            activeStatusKey:
                                "GR_TokraWoundedAgent_StatusCare",
                            readyStatusKey:
                                "GR_TokraWoundedAgent_StatusDeparting",
                            successTrustMessageKey:
                                "GR_TokraTrust_WoundedAgentSucceeded",
                            failureTrustMessageKey:
                                "GR_TokraTrust_WoundedAgentFailed",
                            debugLabel: "WoundedAgentCare")
                    },
                    {
                        TokraOrganicOperationArchetype.MedicalSupplyHandoff,
                        new TokraOrganicOperationDefinition(
                            TokraOrganicOperationArchetype.MedicalSupplyHandoff,
                            offerDurationTicks: 120000,
                            readyDelayTicks: 0,
                            deadlineTicks: 15000,
                            intellectualXp: 0,
                            medicineXp: 0,
                            socialXp: 350,
                            successTrustChange:
                                GameComponent_TokraTrustTracker
                                    .OrganicMedicalSupplySuccessTrustChange,
                            failureTrustChange:
                                GameComponent_TokraTrustTracker
                                    .OrganicMedicalSupplyFailureTrustChange,
                            waryWeight: 0.20f,
                            neutralWeight: 0.70f,
                            cooperativeWeight: 1.00f,
                            trustedWeight: 0.65f,
                            objectiveThingDefName: null,
                            acceptActionKey:
                                "GR_TokraMedicalSupply_Accept",
                            completeActionKey: null,
                            offerLetterLabelKey:
                                "GR_TokraMedicalSupply_OfferLabel",
                            offerLetterTextKey:
                                "GR_TokraMedicalSupply_OfferText",
                            offerExpiredMessageKey:
                                "GR_TokraMedicalSupply_OfferExpired",
                            offeredStatusKey:
                                "GR_TokraMedicalSupply_StatusOffered",
                            activeStatusKey:
                                "GR_TokraMedicalSupply_StatusAwaitingArrival",
                            readyStatusKey:
                                "GR_TokraMedicalSupply_StatusReady",
                            successTrustMessageKey:
                                "GR_TokraTrust_MedicalSupplySucceeded",
                            failureTrustMessageKey:
                                "GR_TokraTrust_MedicalSupplyFailed",
                            debugLabel: "MedicalSupplyHandoff")
                    }
                };

        public static IEnumerable<TokraOrganicOperationDefinition>
            AllDefinitions => Definitions.Values;

        public static bool TryGetDefinition(
            TokraOrganicOperationArchetype archetype,
            out TokraOrganicOperationDefinition definition)
        {
            return Definitions.TryGetValue(archetype, out definition);
        }

        public static TokraOrganicOperationDefinition GetDefinition(
            TokraOrganicOperationArchetype archetype)
        {
            TokraOrganicOperationDefinition definition;

            return TryGetDefinition(archetype, out definition)
                ? definition
                : null;
        }

        public static void GetDelayRange(
            TokraTrustTier tier,
            out int minimumDelay,
            out int maximumDelay)
        {
            switch (tier)
            {
                case TokraTrustTier.Wary:
                    minimumDelay = 360000;
                    maximumDelay = 720000;
                    return;
                case TokraTrustTier.Cooperative:
                    minimumDelay = 180000;
                    maximumDelay = 420000;
                    return;
                case TokraTrustTier.Trusted:
                    minimumDelay = 360000;
                    maximumDelay = 720000;
                    return;
                default:
                    minimumDelay = 240000;
                    maximumDelay = 480000;
                    return;
            }
        }

        public static bool TryPlaceObjective(
            Map map,
            TokraOrganicOperationDefinition definition,
            out Thing objective)
        {
            objective = null;

            if (map == null
                || definition == null
                || !definition.HasPhysicalObjective)
            {
                return false;
            }

            ThingDef thingDef = DefDatabase<ThingDef>.GetNamedSilentFail(
                definition.ObjectiveThingDefName);

            if (thingDef == null)
            {
                return false;
            }

            DestroyObjectives(map, definition);

            Thing thing = ThingMaker.MakeThing(thingDef);

            if (thingDef.category == ThingCategory.Building)
            {
                thing.SetFaction(Faction.OfPlayer);
            }

            Thing placedThing;

            if (!TokraDeliveryDropUtility
                .TryPlaceThingNearPreferredDeliveryCell(
                    thing,
                    map,
                    null,
                    out placedThing))
            {
                if (!thing.Destroyed)
                {
                    thing.Destroy(DestroyMode.Vanish);
                }

                return false;
            }

            if (!IsValidPlacedObjective(map, placedThing))
            {
                if (placedThing != null && !placedThing.Destroyed)
                {
                    placedThing.Destroy(DestroyMode.Vanish);
                }

                return false;
            }

            GR_Log.Message(
                "Placed Tok'ra organic objective "
                + $"{definition.DebugLabel} at {placedThing.Position} "
                + "through the shared preferred-delivery route.");

            objective = placedThing;
            return true;
        }

        public static Thing FindExistingObjective(
            Map map,
            TokraOrganicOperationDefinition definition)
        {
            if (map?.listerThings?.AllThings == null
                || definition == null
                || !definition.HasPhysicalObjective)
            {
                return null;
            }

            return map.listerThings.AllThings.FirstOrDefault(thing =>
                thing != null
                && !thing.Destroyed
                && thing.def?.defName == definition.ObjectiveThingDefName);
        }

        public static void DestroyObjectives(
            Map map,
            TokraOrganicOperationDefinition definition)
        {
            if (map?.listerThings?.AllThings == null
                || definition == null
                || !definition.HasPhysicalObjective)
            {
                return;
            }

            List<Thing> staleObjectives = map.listerThings.AllThings
                .Where(thing =>
                    thing != null
                    && thing.def?.defName == definition.ObjectiveThingDefName)
                .ToList();

            for (int i = 0; i < staleObjectives.Count; i++)
            {
                Thing staleObjective = staleObjectives[i];

                if (!staleObjective.Destroyed)
                {
                    staleObjective.Destroy(DestroyMode.Vanish);
                }
            }
        }

        public static void DestroyAllObjectivesExcept(
            Thing retainedObjective)
        {
            if (Find.Maps == null)
            {
                return;
            }

            foreach (TokraOrganicOperationDefinition definition
                in AllDefinitions.Where(item => item.HasPhysicalObjective))
            {
                for (int mapIndex = 0; mapIndex < Find.Maps.Count; mapIndex++)
                {
                    Map map = Find.Maps[mapIndex];

                    if (map?.listerThings?.AllThings == null)
                    {
                        continue;
                    }

                    List<Thing> objectives = map.listerThings.AllThings
                        .Where(thing =>
                            thing != null
                            && thing.def?.defName
                                == definition.ObjectiveThingDefName)
                        .ToList();

                    for (int thingIndex = 0;
                        thingIndex < objectives.Count;
                        thingIndex++)
                    {
                        Thing objective = objectives[thingIndex];

                        if (objective != retainedObjective
                            && !objective.Destroyed)
                        {
                            objective.Destroy(DestroyMode.Vanish);
                        }
                    }
                }
            }
        }

        private static bool IsValidPlacedObjective(Map map, Thing objective)
        {
            return objective != null
                && objective.Spawned
                && objective.Map == map
                && !objective.Position.Fogged(map)
                && map.reachability.CanReachColony(objective.Position);
        }
    }
}
