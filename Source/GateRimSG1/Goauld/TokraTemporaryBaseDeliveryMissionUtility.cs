using System;
using System.Collections.Generic;
using System.Linq;
using GateRimSG1.Missions;
using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace GateRimSG1.Goauld
{
    internal static class TokraTemporaryBaseDeliveryMissionUtility
    {
        public const string WorldObjectIdCounterKey
            = "deliveryWorldObjectId";
        public const string ThingDefNameStringKey
            = "deliveryThingDefName";
        public const string RequiredCountCounterKey
            = "deliveryRequiredCount";
        public const string RequireQualityCounterKey
            = "deliveryRequireQuality";
        public const string MinimumQualityCounterKey
            = "deliveryMinimumQuality";
        public const string MinimumHitPointsScalarKey
            = "deliveryMinimumHitPoints";
        public const string LateWindowStartedCounterKey
            = "deliveryLateWindowStarted";
        public const string DeliveredLateCounterKey
            = "deliveryDeliveredLate";

        public static bool CanCreateContract(
            Map map,
            GateRimMissionDeliveryDef profile)
        {
            return map != null
                && profile != null
                && map.Tile != PlanetTile.Invalid
                && Find.WorldObjects != null
                && GetEligibleCandidates(map, profile).Count > 0
                && TokraFactionUtility.GetOrCreatePersistentFaction(
                    "temporary-base delivery offerability") != null;
        }

        public static bool TrySelectAndStoreContract(
            Map map,
            GateRimMissionDeliveryDef profile,
            GateRimMissionRuntimeData runtime)
        {
            if (runtime == null)
            {
                return false;
            }

            List<EligibleCandidate> eligible = GetEligibleCandidates(
                map,
                profile);

            if (eligible.Count == 0)
            {
                return false;
            }

            EligibleCandidate selected = SelectWeighted(eligible);
            int requiredCount = CalculateRequiredCount(
                map,
                profile,
                selected);

            runtime.SetString(
                ThingDefNameStringKey,
                selected.ThingDef.defName);
            SetCounter(
                runtime,
                RequiredCountCounterKey,
                requiredCount);
            SetCounter(
                runtime,
                RequireQualityCounterKey,
                selected.Def.requireQuality ? 1 : 0);
            SetCounter(
                runtime,
                MinimumQualityCounterKey,
                (int)selected.Def.minimumQuality);
            SetScalar(
                runtime,
                MinimumHitPointsScalarKey,
                Mathf.Clamp01(selected.Def.minimumHitPointsPercent));

            GR_Log.Message(
                "Selected Tok'ra temporary-base delivery contract: "
                + $"{requiredCount}x {selected.ThingDef.defName}; "
                + $"minimum quality {selected.Def.minimumQuality}; "
                + $"quality required={selected.Def.requireQuality}; "
                + "minimum hit points "
                + $"{selected.Def.minimumHitPointsPercent:P0}.");

            return true;
        }

        public static bool TryCreateWorldSite(
            Map map,
            TokraOrganicOperationDefinition definition,
            GateRimMissionRuntimeData runtime,
            out WorldObject_TokraTemporaryBaseDeliverySite site)
        {
            site = null;
            GateRimMissionDeliveryDef profile = definition?.Delivery;

            if (!HasStoredContract(runtime)
                || map == null
                || profile == null
                || Find.WorldObjects == null)
            {
                return false;
            }

            PlanetTile tile;

            if (!TileFinder.TryFindNewSiteTile(
                    out tile,
                    map.Tile,
                    minDist: Math.Max(1, profile.minimumTileDistance),
                    maxDist: Math.Max(
                        profile.minimumTileDistance,
                        profile.maximumTileDistance),
                    allowCaravans: false,
                    selectLandmarkChance: 0f,
                    layer: map.Tile.Layer))
            {
                return false;
            }

            WorldObjectDef worldObjectDef = ResolveWorldObjectDef(profile);
            Faction tokraFaction
                = TokraFactionUtility.GetOrCreatePersistentFaction(
                    "Tok'ra temporary-base delivery site");
            WorldObject_TokraTemporaryBaseDeliverySite createdSite
                = worldObjectDef == null
                    ? null
                    : WorldObjectMaker.MakeWorldObject(worldObjectDef)
                        as WorldObject_TokraTemporaryBaseDeliverySite;

            if (createdSite == null || tokraFaction == null)
            {
                return false;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            int deadlineTick = currentTick
                + Math.Max(1, definition.DeadlineTicks);
            int finalExpiryTick = deadlineTick
                + Math.Max(1, profile.lateGraceTicks);
            bool planInterception = profile.interceptionChance > 0f
                && Rand.Chance(Mathf.Clamp01(profile.interceptionChance));
            int minimumInterceptionDelay = Math.Max(
                1,
                profile.interceptionMinimumDelayTicks);
            int maximumInterceptionDelay = Math.Max(
                minimumInterceptionDelay,
                profile.interceptionMaximumDelayTicks);
            int interceptionTriggerTick = planInterception
                ? currentTick + Rand.RangeInclusive(
                    minimumInterceptionDelay,
                    maximumInterceptionDelay)
                : 0;
            float interceptionMinimumPoints = Math.Max(
                35f,
                profile.interceptionMinimumPoints);
            float interceptionMaximumPoints = Math.Max(
                interceptionMinimumPoints,
                profile.interceptionMaximumPoints);
            float interceptionThreatPoints = Mathf.Clamp(
                runtime.scaledThreatPoints
                    * Math.Max(0.01f, profile.interceptionThreatFactor),
                interceptionMinimumPoints,
                interceptionMaximumPoints);
            bool planDestinationCompromise = !planInterception
                && profile.destinationCompromiseChance > 0f
                && Rand.Chance(Mathf.Clamp01(
                    profile.destinationCompromiseChance));
            float destinationCompromiseMinimumPoints = Math.Max(
                35f,
                profile.destinationCompromiseMinimumPoints);
            float destinationCompromiseMaximumPoints = Math.Max(
                destinationCompromiseMinimumPoints,
                profile.destinationCompromiseMaximumPoints);
            float destinationCompromiseThreatPoints = Mathf.Clamp(
                runtime.scaledThreatPoints
                    * Math.Max(
                        0.01f,
                        profile.destinationCompromiseThreatFactor),
                destinationCompromiseMinimumPoints,
                destinationCompromiseMaximumPoints);

            createdSite.Tile = tile;
            createdSite.SetFaction(tokraFaction);
            createdSite.Initialize(
                map.uniqueID,
                definition.MissionDefName,
                GetContractThingDef(runtime)?.defName,
                GetRequiredCount(runtime),
                RequiresQuality(runtime),
                GetMinimumQuality(runtime),
                GetMinimumHitPointsPercent(runtime),
                deadlineTick,
                finalExpiryTick,
                planInterception,
                interceptionTriggerTick,
                interceptionThreatPoints,
                planDestinationCompromise,
                destinationCompromiseThreatPoints);

            Find.WorldObjects.Add(createdSite);
            SetCounter(runtime, WorldObjectIdCounterKey, createdSite.ID);
            site = createdSite;

            return true;
        }

        public static WorldObject_TokraTemporaryBaseDeliverySite FindWorldSite(
            GateRimMissionRuntimeData runtime)
        {
            int worldObjectId = GetCounter(
                runtime,
                WorldObjectIdCounterKey,
                -1);

            if (worldObjectId < 0 || Find.WorldObjects == null)
            {
                return null;
            }

            return Find.WorldObjects.AllWorldObjects
                .OfType<WorldObject_TokraTemporaryBaseDeliverySite>()
                .FirstOrDefault(worldObject => worldObject != null
                    && !worldObject.Destroyed
                    && worldObject.ID == worldObjectId);
        }

        public static bool HasStoredContract(GateRimMissionRuntimeData runtime)
        {
            return GetContractThingDef(runtime) != null
                && GetRequiredCount(runtime) > 0;
        }

        public static ThingDef GetContractThingDef(
            GateRimMissionRuntimeData runtime)
        {
            string defName = runtime?.GetString(ThingDefNameStringKey);
            return string.IsNullOrWhiteSpace(defName)
                ? null
                : DefDatabase<ThingDef>.GetNamedSilentFail(defName);
        }

        public static int GetRequiredCount(GateRimMissionRuntimeData runtime)
        {
            return Math.Max(
                0,
                GetCounter(runtime, RequiredCountCounterKey, 0));
        }

        public static bool RequiresQuality(GateRimMissionRuntimeData runtime)
        {
            return GetCounter(runtime, RequireQualityCounterKey, 0) > 0;
        }

        public static QualityCategory GetMinimumQuality(
            GateRimMissionRuntimeData runtime)
        {
            return (QualityCategory)GetCounter(
                runtime,
                MinimumQualityCounterKey,
                (int)QualityCategory.Normal);
        }

        public static float GetMinimumHitPointsPercent(
            GateRimMissionRuntimeData runtime)
        {
            return Mathf.Clamp01(GetScalar(
                runtime,
                MinimumHitPointsScalarKey,
                0f));
        }

        public static string GetContractLabel(
            GateRimMissionRuntimeData runtime)
        {
            ThingDef thingDef = GetContractThingDef(runtime);
            return thingDef?.label?.CapitalizeFirst() ?? "?";
        }

        public static string GetQualityLabel(
            GateRimMissionRuntimeData runtime)
        {
            return RequiresQuality(runtime)
                ? GetMinimumQuality(runtime).GetLabel().CapitalizeFirst()
                : "GR_TokraTemporaryBaseDelivery_AnyQuality".Translate()
                    .ToString();
        }

        public static void SetLateWindowStarted(
            GateRimMissionRuntimeData runtime,
            bool value)
        {
            SetCounter(
                runtime,
                LateWindowStartedCounterKey,
                value ? 1 : 0);
        }

        public static bool IsLateWindowStarted(
            GateRimMissionRuntimeData runtime)
        {
            return GetCounter(
                runtime,
                LateWindowStartedCounterKey,
                0) > 0;
        }

        public static void SetDeliveredLate(
            GateRimMissionRuntimeData runtime,
            bool value)
        {
            SetCounter(runtime, DeliveredLateCounterKey, value ? 1 : 0);
        }

        public static bool WasDeliveredLate(
            GateRimMissionRuntimeData runtime)
        {
            return GetCounter(runtime, DeliveredLateCounterKey, 0) > 0;
        }

        public static bool IsMatchingThing(
            Thing thing,
            ThingDef requiredDef,
            bool requireQuality,
            QualityCategory minimumQuality,
            float minimumHitPointsPercent)
        {
            if (thing == null
                || thing.Destroyed
                || thing.def != requiredDef)
            {
                return false;
            }

            if (thing.def.useHitPoints
                && thing.MaxHitPoints > 0
                && thing.HitPoints / (float)thing.MaxHitPoints
                    < minimumHitPointsPercent)
            {
                return false;
            }

            if (!requireQuality)
            {
                return true;
            }

            CompQuality quality = thing.TryGetComp<CompQuality>();
            return quality != null && quality.Quality >= minimumQuality;
        }

        public static int CountMatchingThings(
            Caravan caravan,
            ThingDef requiredDef,
            bool requireQuality,
            QualityCategory minimumQuality,
            float minimumHitPointsPercent)
        {
            if (caravan == null || requiredDef == null)
            {
                return 0;
            }

            return CaravanInventoryUtility.AllInventoryItems(caravan)
                .Where(thing => IsMatchingThing(
                    thing,
                    requiredDef,
                    requireQuality,
                    minimumQuality,
                    minimumHitPointsPercent))
                .Sum(thing => thing.stackCount);
        }

        public static bool TryConsumeMatchingThings(
            Caravan caravan,
            ThingDef requiredDef,
            int requiredCount,
            bool requireQuality,
            QualityCategory minimumQuality,
            float minimumHitPointsPercent)
        {
            if (CountMatchingThings(
                    caravan,
                    requiredDef,
                    requireQuality,
                    minimumQuality,
                    minimumHitPointsPercent) < requiredCount)
            {
                return false;
            }

            int remaining = requiredCount;
            List<Thing> matches = CaravanInventoryUtility
                .AllInventoryItems(caravan)
                .Where(thing => IsMatchingThing(
                    thing,
                    requiredDef,
                    requireQuality,
                    minimumQuality,
                    minimumHitPointsPercent))
                .OrderBy(GetThingQualityRank)
                .ThenBy(thing => thing.MaxHitPoints > 0
                    ? thing.HitPoints / (float)thing.MaxHitPoints
                    : 1f)
                .ToList();

            Dictionary<Thing, int> quantitiesByThing
                = new Dictionary<Thing, int>();

            foreach (Thing thing in matches)
            {
                if (remaining <= 0)
                {
                    break;
                }

                int takeCount = Math.Min(remaining, thing.stackCount);
                quantitiesByThing[thing] = takeCount;
                remaining -= takeCount;
            }

            if (remaining > 0)
            {
                return false;
            }

            List<Thing> takenThings = CaravanInventoryUtility.TakeThings(
                caravan,
                thing => quantitiesByThing.TryGetValue(
                    thing,
                    out int takeCount)
                        ? takeCount
                        : 0);
            int removedCount = takenThings.Sum(thing => thing.stackCount);

            foreach (Thing takenThing in takenThings)
            {
                takenThing.Destroy(DestroyMode.Vanish);
            }

            return removedCount >= requiredCount;
        }

        private static int GetThingQualityRank(Thing thing)
        {
            CompQuality quality = thing?.TryGetComp<CompQuality>();
            return quality == null
                ? (int)QualityCategory.Awful
                : (int)quality.Quality;
        }

        private static List<EligibleCandidate> GetEligibleCandidates(
            Map map,
            GateRimMissionDeliveryDef profile)
        {
            List<EligibleCandidate> result = new List<EligibleCandidate>();

            if (map == null || profile?.candidates == null)
            {
                return result;
            }

            foreach (GateRimMissionDeliveryCandidateDef candidate
                in profile.candidates)
            {
                if (candidate == null
                    || string.IsNullOrWhiteSpace(candidate.thingDefName)
                    || candidate.minimumCount <= 0
                    || candidate.maximumCount < candidate.minimumCount
                    || candidate.weight <= 0f)
                {
                    continue;
                }

                ThingDef thingDef = DefDatabase<ThingDef>.GetNamedSilentFail(
                    candidate.thingDefName);

                if (thingDef == null
                    || thingDef.category != ThingCategory.Item
                    || thingDef.techLevel > profile.maximumTechLevel
                    || (candidate.requireQuality
                        && !thingDef.HasComp(typeof(CompQuality))))
                {
                    continue;
                }

                RecipeDef recipe = FindUsableRecipe(
                    map,
                    profile,
                    thingDef,
                    out int capableCrafterCount,
                    out int bestSkillLevel);

                if (recipe == null)
                {
                    continue;
                }

                result.Add(new EligibleCandidate(
                    candidate,
                    thingDef,
                    recipe,
                    capableCrafterCount,
                    bestSkillLevel));
            }

            return result;
        }

        private static RecipeDef FindUsableRecipe(
            Map map,
            GateRimMissionDeliveryDef profile,
            ThingDef product,
            out int capableCrafterCount,
            out int bestSkillLevel)
        {
            capableCrafterCount = 0;
            bestSkillLevel = 0;

            foreach (RecipeDef recipe
                in DefDatabase<RecipeDef>.AllDefsListForReading)
            {
                if (recipe == null
                    || recipe.ProducedThingDef != product
                    || !recipe.AvailableNow)
                {
                    continue;
                }

                if (profile.requireExistingWorkTable
                    && !HasExistingRecipeUser(map, recipe))
                {
                    continue;
                }

                int recipeCapableCount = 0;
                int recipeBestSkill = 0;

                foreach (Pawn pawn in map.mapPawns.FreeColonists)
                {
                    if (pawn == null
                        || pawn.Dead
                        || pawn.Downed
                        || !recipe.PawnSatisfiesSkillRequirements(pawn)
                        || (recipe.requiredGiverWorkType != null
                            && pawn.WorkTypeIsDisabled(
                                recipe.requiredGiverWorkType)))
                    {
                        continue;
                    }

                    int skillLevel = recipe.workSkill == null
                        ? profile.minimumCrafterSkill
                        : pawn.skills?.GetSkill(recipe.workSkill)?.Level ?? 0;

                    if (skillLevel < profile.minimumCrafterSkill)
                    {
                        continue;
                    }

                    recipeCapableCount++;
                    recipeBestSkill = Math.Max(
                        recipeBestSkill,
                        skillLevel);
                }

                if (recipeCapableCount <= 0)
                {
                    continue;
                }

                capableCrafterCount = recipeCapableCount;
                bestSkillLevel = recipeBestSkill;
                return recipe;
            }

            return null;
        }

        private static bool HasExistingRecipeUser(Map map, RecipeDef recipe)
        {
            if (map?.listerThings?.AllThings == null)
            {
                return false;
            }

            foreach (Thing thing in map.listerThings.AllThings)
            {
                Building_WorkTable workTable = thing as Building_WorkTable;

                if (workTable == null
                    || workTable.Destroyed
                    || workTable.Faction != Faction.OfPlayer)
                {
                    continue;
                }

                IEnumerable<RecipeDef> recipes = workTable.def.AllRecipes;

                if (recipes != null && recipes.Contains(recipe))
                {
                    return true;
                }
            }

            return false;
        }

        private static int CalculateRequiredCount(
            Map map,
            GateRimMissionDeliveryDef profile,
            EligibleCandidate candidate)
        {
            int minimum = Math.Max(1, candidate.Def.minimumCount);
            int maximum = Math.Max(minimum, candidate.Def.maximumCount);
            float colonistFactor = Mathf.InverseLerp(
                1f,
                8f,
                candidate.CapableCrafterCount);
            float skillFactor = Mathf.InverseLerp(
                Math.Max(1, profile.minimumCrafterSkill),
                16f,
                candidate.BestSkillLevel);
            float wealthFactor = Mathf.InverseLerp(
                30000f,
                250000f,
                map.wealthWatcher?.WealthTotal ?? 0f);
            float productionFactor = Mathf.Clamp01(
                colonistFactor * 0.45f
                + skillFactor * 0.35f
                + wealthFactor * 0.20f);

            return Mathf.Clamp(
                Mathf.RoundToInt(Mathf.Lerp(minimum, maximum, productionFactor)),
                minimum,
                maximum);
        }

        private static EligibleCandidate SelectWeighted(
            List<EligibleCandidate> candidates)
        {
            float totalWeight = candidates.Sum(candidate => Math.Max(
                0f,
                candidate.Def.weight));
            float roll = Rand.Value * totalWeight;

            foreach (EligibleCandidate candidate in candidates)
            {
                roll -= Math.Max(0f, candidate.Def.weight);

                if (roll <= 0f)
                {
                    return candidate;
                }
            }

            return candidates[candidates.Count - 1];
        }

        private static WorldObjectDef ResolveWorldObjectDef(
            GateRimMissionDeliveryDef profile)
        {
            if (GR_DefOf.SG1_TokraTemporaryBaseDeliverySite != null)
            {
                return GR_DefOf.SG1_TokraTemporaryBaseDeliverySite;
            }

            return string.IsNullOrWhiteSpace(profile?.worldObjectDefName)
                ? null
                : DefDatabase<WorldObjectDef>.GetNamedSilentFail(
                    profile.worldObjectDefName);
        }

        private static void SetCounter(
            GateRimMissionRuntimeData runtime,
            string key,
            int value)
        {
            if (runtime?.counters != null && !string.IsNullOrEmpty(key))
            {
                runtime.counters[key] = value;
            }
        }

        private static int GetCounter(
            GateRimMissionRuntimeData runtime,
            string key,
            int fallback)
        {
            int value;
            return runtime?.counters != null
                    && runtime.counters.TryGetValue(key, out value)
                ? value
                : fallback;
        }

        private static void SetScalar(
            GateRimMissionRuntimeData runtime,
            string key,
            float value)
        {
            if (runtime?.scalars != null && !string.IsNullOrEmpty(key))
            {
                runtime.scalars[key] = value;
            }
        }

        private static float GetScalar(
            GateRimMissionRuntimeData runtime,
            string key,
            float fallback)
        {
            float value;
            return runtime?.scalars != null
                    && runtime.scalars.TryGetValue(key, out value)
                ? value
                : fallback;
        }

        private sealed class EligibleCandidate
        {
            public EligibleCandidate(
                GateRimMissionDeliveryCandidateDef def,
                ThingDef thingDef,
                RecipeDef recipe,
                int capableCrafterCount,
                int bestSkillLevel)
            {
                Def = def;
                ThingDef = thingDef;
                Recipe = recipe;
                CapableCrafterCount = capableCrafterCount;
                BestSkillLevel = bestSkillLevel;
            }

            public GateRimMissionDeliveryCandidateDef Def { get; }
            public ThingDef ThingDef { get; }
            public RecipeDef Recipe { get; }
            public int CapableCrafterCount { get; }
            public int BestSkillLevel { get; }
        }
    }
}
