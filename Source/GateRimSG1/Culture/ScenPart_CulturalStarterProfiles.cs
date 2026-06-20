using System;
using System.Collections.Generic;
using System.Linq;
using GateRimSG1.Names;
using RimWorld;
using Verse;

namespace GateRimSG1.Culture
{
    /// <summary>
    /// Applies configured cultural profiles only while a new player starter is
    /// generated. Manual edits made after generation remain untouched.
    /// </summary>
    public class ScenPart_CulturalStarterProfiles : ScenPart
    {
        private const int MaxStarterNameAttempts = 100;

        private readonly HashSet<string> generatedStarterNameKeys
            = new HashSet<string>();

        public ScenPart_CulturalStarterProfiles()
        {
            visible = false;
        }

        public override string Summary(Scenario scen)
        {
            return null;
        }

        public override bool CanCoexistWith(ScenPart other)
        {
            return !(other is ScenPart_CulturalStarterProfiles);
        }

        public override void Notify_NewPawnGenerating(
            Pawn pawn,
            PawnGenerationContext context)
        {
            if (context != PawnGenerationContext.PlayerStarter
                || pawn?.story == null
                || pawn.skills == null
                || !CulturalProfileResolver.TryResolveStarterRule(
                    pawn,
                    out CulturalPawnProfileDef profile,
                    out CulturalStarterRule rule))
            {
                return;
            }

            BackstoryDef oldChildhood = pawn.story.Childhood;
            BackstoryDef oldAdulthood = pawn.story.Adulthood;

            BackstoryDef newChildhood = SelectChildhood(
                rule,
                oldChildhood);
            BackstoryDef newAdulthood = SelectAdulthood(
                rule,
                newChildhood,
                oldAdulthood);

            bool backstoryChanged = newChildhood != oldChildhood
                || newAdulthood != oldAdulthood;

            if (backstoryChanged)
            {
                pawn.story.Childhood = newChildhood;
                pawn.story.Adulthood = newAdulthood;

                BodyTypeDef bodyType = newAdulthood?.BodyTypeFor(pawn.gender);
                if (bodyType != null)
                {
                    pawn.story.bodyType = bodyType;
                }

                pawn.Notify_DisabledWorkTypesChanged();
                ApplySkillGainDelta(
                    pawn,
                    oldChildhood,
                    oldAdulthood,
                    newChildhood,
                    newAdulthood);
            }

            CulturalPawnNameGroup nameGroup = profile.NameGroupFor(pawn);
            AssignCulturalName(pawn, nameGroup);
        }

        private static BackstoryDef SelectChildhood(
            CulturalStarterRule rule,
            BackstoryDef fallback)
        {
            if (rule.childhoods.NullOrEmpty()
                || !Rand.Chance(rule.childhoodReplacementChance))
            {
                return fallback;
            }

            List<BackstoryDef> candidates = rule.childhoods
                .Where(backstory => backstory != null
                    && backstory.slot == BackstorySlot.Childhood)
                .ToList();

            return candidates.TryRandomElement(out BackstoryDef result)
                ? result
                : fallback;
        }

        private static BackstoryDef SelectAdulthood(
            CulturalStarterRule rule,
            BackstoryDef childhood,
            BackstoryDef fallback)
        {
            if (fallback == null || rule.adulthoods.NullOrEmpty())
            {
                return fallback;
            }

            List<BackstoryDef> candidates = rule.adulthoods
                .Where(backstory => backstory != null
                    && backstory.slot == BackstorySlot.Adulthood
                    && IsCompatible(childhood, backstory))
                .ToList();

            if (candidates.Count == 0)
            {
                return fallback;
            }

            if (rule.mergeWithVanillaAdulthoodPool)
            {
                return SelectFromMergedVanillaAdulthoodPool(
                    childhood,
                    fallback,
                    candidates);
            }

            if (!Rand.Chance(rule.adulthoodReplacementChance))
            {
                return fallback;
            }

            return candidates.TryRandomElement(out BackstoryDef result)
                ? result
                : fallback;
        }

        /// <summary>
        /// Adds configured careers to the same effective weighted pool as the
        /// vanilla adulthood already generated for this starter. The existing
        /// vanilla result represents the vanilla side of the pool, so solid
        /// bios and ordinary vanilla naming remain untouched when that side
        /// wins.
        /// </summary>
        private static BackstoryDef SelectFromMergedVanillaAdulthoodPool(
            BackstoryDef childhood,
            BackstoryDef fallback,
            List<BackstoryDef> configuredCandidates)
        {
            if (!fallback.shuffleable
                || fallback.spawnCategories.NullOrEmpty())
            {
                return fallback;
            }

            HashSet<string> fallbackCategories = new HashSet<string>(
                fallback.spawnCategories);
            HashSet<BackstoryDef> configuredSet = new HashSet<BackstoryDef>(
                configuredCandidates);

            List<BackstoryDef> vanillaCandidates = DefDatabase<BackstoryDef>
                .AllDefsListForReading
                .Where(backstory => backstory != null
                    && backstory.shuffleable
                    && backstory.slot == BackstorySlot.Adulthood
                    && !configuredSet.Contains(backstory)
                    && !backstory.spawnCategories.NullOrEmpty()
                    && backstory.spawnCategories.Any(
                        category => fallbackCategories.Contains(category))
                    && IsCompatible(childhood, backstory))
                .ToList();

            float vanillaWeight = vanillaCandidates.Sum(
                BackstorySelectionWeight);
            float configuredWeight = configuredCandidates.Sum(
                BackstorySelectionWeight);
            float totalWeight = vanillaWeight + configuredWeight;

            if (totalWeight <= 0f
                || Rand.Value * totalWeight < vanillaWeight)
            {
                return fallback;
            }

            return configuredCandidates.TryRandomElementByWeight(
                BackstorySelectionWeight,
                out BackstoryDef result)
                ? result
                : fallback;
        }

        private static float BackstorySelectionWeight(BackstoryDef backstory)
        {
            WorkTags disabled = backstory?.workDisables ?? WorkTags.None;
            float weight = 1f;

            if ((disabled & WorkTags.ManualDumb) != WorkTags.None)
            {
                weight *= 0.5f;
            }

            if ((disabled & WorkTags.Violent) != WorkTags.None)
            {
                weight *= 0.6f;
            }

            if ((disabled & WorkTags.Social) != WorkTags.None)
            {
                weight *= 0.7f;
            }

            if ((disabled & WorkTags.Intellectual) != WorkTags.None)
            {
                weight *= 0.4f;
            }

            if ((disabled & WorkTags.Firefighting) != WorkTags.None)
            {
                weight *= 0.8f;
            }

            return weight;
        }

        private static bool IsCompatible(
            BackstoryDef childhood,
            BackstoryDef adulthood)
        {
            return childhood == null
                || adulthood == null
                || ((adulthood.requiredWorkTags
                    & childhood.workDisables) == WorkTags.None
                    && (childhood.requiredWorkTags
                        & adulthood.workDisables) == WorkTags.None);
        }

        /// <summary>
        /// Preserves the pawn's randomized skill baseline, passions and gene
        /// aptitudes, then applies only the difference between the old and new
        /// backstory skill bonuses.
        /// </summary>
        private static void ApplySkillGainDelta(
            Pawn pawn,
            BackstoryDef oldChildhood,
            BackstoryDef oldAdulthood,
            BackstoryDef newChildhood,
            BackstoryDef newAdulthood)
        {
            Dictionary<SkillDef, int> oldGains = CollectSkillGains(
                oldChildhood,
                oldAdulthood);
            Dictionary<SkillDef, int> newGains = CollectSkillGains(
                newChildhood,
                newAdulthood);

            HashSet<SkillDef> skills = new HashSet<SkillDef>(oldGains.Keys);
            skills.UnionWith(newGains.Keys);

            foreach (SkillDef skillDef in skills)
            {
                int oldGain = oldGains.TryGetValue(skillDef, out int oldValue)
                    ? oldValue
                    : 0;
                int newGain = newGains.TryGetValue(skillDef, out int newValue)
                    ? newValue
                    : 0;
                SkillRecord record = pawn.skills.GetSkill(skillDef);
                int baseLevel = record.levelInt;

                record.Level = Math.Max(
                    0,
                    Math.Min(20, baseLevel + newGain - oldGain));
            }
        }

        private static Dictionary<SkillDef, int> CollectSkillGains(
            params BackstoryDef[] backstories)
        {
            Dictionary<SkillDef, int> result
                = new Dictionary<SkillDef, int>();

            for (int backstoryIndex = 0;
                backstoryIndex < backstories.Length;
                backstoryIndex++)
            {
                BackstoryDef backstory = backstories[backstoryIndex];
                if (backstory?.skillGains == null)
                {
                    continue;
                }

                for (int gainIndex = 0;
                    gainIndex < backstory.skillGains.Count;
                    gainIndex++)
                {
                    SkillGain gain = backstory.skillGains[gainIndex];
                    if (gain?.skill == null)
                    {
                        continue;
                    }

                    result[gain.skill] = result.TryGetValue(
                        gain.skill,
                        out int current)
                        ? current + gain.amount
                        : gain.amount;
                }
            }

            return result;
        }

        private void AssignCulturalName(
            Pawn pawn,
            CulturalPawnNameGroup group)
        {
            if (group == CulturalPawnNameGroup.None)
            {
                return;
            }

            Name generatedName = GameComponent_CulturalPawnNameManager.Current
                ?.GenerateUniqueName(group, pawn.gender);

            if (generatedName == null)
            {
                for (int attempt = 0;
                    attempt < MaxStarterNameAttempts;
                    attempt++)
                {
                    Name candidate = CulturalPawnNameUtility.GenerateName(
                        group,
                        pawn.gender);
                    string candidateKey = candidate?.ToStringFull;

                    if (candidateKey.NullOrEmpty()
                        || !generatedStarterNameKeys.Add(candidateKey))
                    {
                        continue;
                    }

                    generatedName = candidate;
                    break;
                }
            }

            if (generatedName != null)
            {
                pawn.Name = generatedName;
            }
        }
    }
}
