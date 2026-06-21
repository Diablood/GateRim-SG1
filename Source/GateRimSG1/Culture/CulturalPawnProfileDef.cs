using System.Collections.Generic;
using System.Linq;
using GateRimSG1.Names;
using RimWorld;
using Verse;

namespace GateRimSG1.Culture
{
    /// <summary>
    /// Def-driven cultural profile shared by starter backstory filtering and
    /// cultural name resolution. Additional culture-dependent systems can
    /// consume the same profile without adding culture-specific branches.
    /// </summary>
    public class CulturalPawnProfileDef : Def
    {
        public int priority;
        public List<CulturalProfileMatcher> matchers
            = new List<CulturalProfileMatcher>();
        public CulturalPawnNameGroup defaultNameGroup
            = CulturalPawnNameGroup.None;
        public List<CulturalNameRule> nameRules
            = new List<CulturalNameRule>();
        public List<CulturalStarterRule> starterRules
            = new List<CulturalStarterRule>();
        public List<BackstoryDef> identityChildhoods
            = new List<BackstoryDef>();
        public List<BackstoryDef> identityAdulthoods
            = new List<BackstoryDef>();
        public List<GeneratedHostOriginDef> generatedHostOrigins
            = new List<GeneratedHostOriginDef>();

        public bool HasIdentityBackstories
        {
            get
            {
                return !identityChildhoods.NullOrEmpty()
                    || !identityAdulthoods.NullOrEmpty();
            }
        }

        public bool Matches(Pawn pawn, PawnGenerationContext context)
        {
            return pawn != null
                && !matchers.NullOrEmpty()
                && matchers.Any(matcher => matcher != null
                    && matcher.Matches(pawn, context));
        }

        public CulturalPawnNameGroup NameGroupFor(Pawn pawn)
        {
            if (!nameRules.NullOrEmpty())
            {
                for (int index = 0; index < nameRules.Count; index++)
                {
                    CulturalNameRule rule = nameRules[index];
                    if (rule != null && rule.Matches(pawn))
                    {
                        return rule.nameGroup;
                    }
                }
            }

            return defaultNameGroup;
        }

        public CulturalStarterRule StarterRuleFor(Scenario scenario)
        {
            if (starterRules.NullOrEmpty())
            {
                return null;
            }

            for (int index = 0; index < starterRules.Count; index++)
            {
                CulturalStarterRule rule = starterRules[index];
                if (rule != null && rule.MatchesScenario(scenario))
                {
                    return rule;
                }
            }

            return null;
        }

        public override IEnumerable<string> ConfigErrors()
        {
            foreach (string error in base.ConfigErrors())
            {
                yield return error;
            }

            if (matchers.NullOrEmpty())
            {
                yield return $"{defName} has no cultural profile matcher.";
            }
            else
            {
                for (int index = 0; index < matchers.Count; index++)
                {
                    CulturalProfileMatcher matcher = matchers[index];
                    if (matcher == null || !matcher.HasIdentityCriterion)
                    {
                        yield return $"{defName} matcher {index} has no "
                            + "race, xenotype, PawnKindDef or faction criterion.";
                    }
                }
            }

            foreach (BackstoryDef childhood in
                identityChildhoods ?? new List<BackstoryDef>())
            {
                if (childhood != null
                    && childhood.slot != BackstorySlot.Childhood)
                {
                    yield return $"{defName} uses adulthood "
                        + $"{childhood.defName} as an identity childhood.";
                }
            }

            foreach (BackstoryDef adulthood in
                identityAdulthoods ?? new List<BackstoryDef>())
            {
                if (adulthood != null
                    && adulthood.slot != BackstorySlot.Adulthood)
                {
                    yield return $"{defName} uses childhood "
                        + $"{adulthood.defName} as an identity adulthood.";
                }
            }

            if (!generatedHostOrigins.NullOrEmpty())
            {
                for (int index = 0; index < generatedHostOrigins.Count; index++)
                {
                    GeneratedHostOriginDef origin = generatedHostOrigins[index];
                    if (origin == null)
                    {
                        yield return $"{defName} generated-host origin {index} is null.";
                    }
                }
            }

            if (!starterRules.NullOrEmpty())
            {
                for (int index = 0; index < starterRules.Count; index++)
                {
                    CulturalStarterRule rule = starterRules[index];
                    if (rule == null)
                    {
                        yield return $"{defName} starter rule {index} is null.";
                        continue;
                    }

                    if (rule.childhoods.NullOrEmpty()
                        && rule.adulthoods.NullOrEmpty()
                        && rule.minimumBiologicalAge <= 0
                        && !rule.requireViolenceCapable
                        && !rule.HasStarterApparel)
                    {
                        yield return $"{defName} starter rule {index} has no "
                            + "configured behavior.";
                    }

                    if (rule.childhoodReplacementChance < 0f
                        || rule.childhoodReplacementChance > 1f)
                    {
                        yield return $"{defName} starter rule {index} has an "
                            + "invalid childhood replacement chance.";
                    }

                    if (rule.adulthoodReplacementChance < 0f
                        || rule.adulthoodReplacementChance > 1f)
                    {
                        yield return $"{defName} starter rule {index} has an "
                            + "invalid adulthood replacement chance.";
                    }

                    if (rule.minimumBiologicalAge < 0)
                    {
                        yield return $"{defName} starter rule {index} has a "
                            + "negative minimum biological age.";
                    }

                    if (rule.replaceStartingApparel
                        && !rule.HasStarterApparel)
                    {
                        yield return $"{defName} starter rule {index} replaces "
                            + "starting apparel without configured apparel.";
                    }

                    HashSet<ThingDef> configuredApparel
                        = new HashSet<ThingDef>();
                    foreach (ThingDef apparelDef in
                        rule.apparel ?? new List<ThingDef>())
                    {
                        foreach (string error in ValidateStarterApparel(
                            apparelDef,
                            null,
                            $"{defName} starter rule {index}",
                            configuredApparel))
                        {
                            yield return error;
                        }
                    }

                    for (int slotIndex = 0;
                        slotIndex < (rule.apparelSlots?.Count ?? 0);
                        slotIndex++)
                    {
                        CulturalStarterApparelSlot slot
                            = rule.apparelSlots[slotIndex];
                        string slotLabel = $"{defName} starter rule {index} "
                            + $"apparel slot {slotIndex}";

                        if (slot == null)
                        {
                            yield return $"{slotLabel} is null.";
                            continue;
                        }

                        if (slot.selectionChance < 0f
                            || slot.selectionChance > 1f)
                        {
                            yield return $"{slotLabel} has an invalid "
                                + "selection chance.";
                        }

                        if (slot.options.NullOrEmpty())
                        {
                            yield return $"{slotLabel} has no options.";
                            continue;
                        }

                        HashSet<string> slotVariantKeys
                            = new HashSet<string>();
                        for (int optionIndex = 0;
                            optionIndex < slot.options.Count;
                            optionIndex++)
                        {
                            CulturalStarterApparelOption option
                                = slot.options[optionIndex];
                            string optionLabel = $"{slotLabel} option "
                                + optionIndex;

                            if (option == null)
                            {
                                yield return $"{optionLabel} is null.";
                                continue;
                            }

                            if (option.weight <= 0f)
                            {
                                yield return $"{optionLabel} has a "
                                    + "non-positive weight.";
                            }

                            bool hasVariantGroup
                                = !string.IsNullOrEmpty(slot.variantGroup);
                            bool hasVariantKey
                                = !string.IsNullOrEmpty(option.variantKey);

                            if (hasVariantGroup && !hasVariantKey)
                            {
                                yield return $"{optionLabel} has no variant "
                                    + $"key for group {slot.variantGroup}.";
                            }
                            else if (!hasVariantGroup && hasVariantKey)
                            {
                                yield return $"{optionLabel} defines variant "
                                    + "key without a slot variant group.";
                            }
                            else if (hasVariantKey
                                && !slotVariantKeys.Add(option.variantKey))
                            {
                                yield return $"{slotLabel} has duplicate "
                                    + $"variant key {option.variantKey}.";
                            }

                            foreach (string error in ValidateStarterApparel(
                                option.apparel,
                                option.stuff,
                                optionLabel,
                                null))
                            {
                                yield return error;
                            }
                        }
                    }

                    Dictionary<string, HashSet<string>> variantGroups
                        = new Dictionary<string, HashSet<string>>();
                    for (int slotIndex = 0;
                        slotIndex < (rule.apparelSlots?.Count ?? 0);
                        slotIndex++)
                    {
                        CulturalStarterApparelSlot slot
                            = rule.apparelSlots[slotIndex];
                        if (slot == null
                            || string.IsNullOrEmpty(slot.variantGroup)
                            || slot.options.NullOrEmpty())
                        {
                            continue;
                        }

                        HashSet<string> keys = new HashSet<string>(
                            slot.options
                                .Where(option => option != null
                                    && !string.IsNullOrEmpty(
                                        option.variantKey))
                                .Select(option => option.variantKey));

                        if (!variantGroups.TryGetValue(
                            slot.variantGroup,
                            out HashSet<string> expectedKeys))
                        {
                            variantGroups.Add(slot.variantGroup, keys);
                        }
                        else if (!expectedKeys.SetEquals(keys))
                        {
                            yield return $"{defName} starter rule {index} "
                                + $"variant group {slot.variantGroup} does not "
                                + "use the same keys in every apparel slot.";
                        }
                    }

                    foreach (BackstoryDef childhood in
                        rule.childhoods ?? new List<BackstoryDef>())
                    {
                        if (childhood != null
                            && childhood.slot != BackstorySlot.Childhood)
                        {
                            yield return $"{defName} uses adulthood "
                                + $"{childhood.defName} as a childhood.";
                        }
                    }

                    foreach (BackstoryDef adulthood in
                        rule.adulthoods ?? new List<BackstoryDef>())
                    {
                        if (adulthood != null
                            && adulthood.slot != BackstorySlot.Adulthood)
                        {
                            yield return $"{defName} uses childhood "
                                + $"{adulthood.defName} as an adulthood.";
                        }
                    }
                }
            }

            if (!nameRules.NullOrEmpty())
            {
                for (int index = 0; index < nameRules.Count; index++)
                {
                    CulturalNameRule rule = nameRules[index];
                    if (rule == null)
                    {
                        yield return $"{defName} name rule {index} is null.";
                    }
                    else
                    {
                        if (rule.nameGroup == CulturalPawnNameGroup.None)
                        {
                            yield return $"{defName} name rule {index} has no "
                                + "cultural name group.";
                        }

                        if (rule.childhoods.NullOrEmpty()
                            && rule.adulthoods.NullOrEmpty())
                        {
                            yield return $"{defName} name rule {index} has no "
                                + "backstory criterion.";
                        }

                        foreach (BackstoryDef childhood in
                            rule.childhoods ?? new List<BackstoryDef>())
                        {
                            if (childhood != null
                                && childhood.slot != BackstorySlot.Childhood)
                            {
                                yield return $"{defName} name rule {index} "
                                    + $"uses adulthood {childhood.defName} "
                                    + "as a childhood criterion.";
                            }
                        }

                        foreach (BackstoryDef adulthood in
                            rule.adulthoods ?? new List<BackstoryDef>())
                        {
                            if (adulthood != null
                                && adulthood.slot != BackstorySlot.Adulthood)
                            {
                                yield return $"{defName} name rule {index} "
                                    + $"uses childhood {adulthood.defName} "
                                    + "as an adulthood criterion.";
                            }
                        }
                    }
                }
            }
        }

        private static IEnumerable<string> ValidateStarterApparel(
            ThingDef apparelDef,
            ThingDef stuffDef,
            string label,
            HashSet<ThingDef> configuredApparel)
        {
            if (apparelDef == null)
            {
                yield return $"{label} has a null apparel entry.";
                yield break;
            }

            if (apparelDef.thingClass == null
                || !typeof(Apparel).IsAssignableFrom(apparelDef.thingClass))
            {
                yield return $"{label} uses non-apparel ThingDef "
                    + $"{apparelDef.defName}.";
                yield break;
            }

            if (configuredApparel != null
                && !configuredApparel.Add(apparelDef))
            {
                yield return $"{label} duplicates apparel "
                    + $"{apparelDef.defName}.";
            }

            if (apparelDef.MadeFromStuff && stuffDef == null)
            {
                yield return $"{label} uses stuffable apparel "
                    + $"{apparelDef.defName} without configured stuff.";
            }
            else if (!apparelDef.MadeFromStuff && stuffDef != null)
            {
                yield return $"{label} configures stuff "
                    + $"{stuffDef.defName} for non-stuffable apparel "
                    + $"{apparelDef.defName}.";
            }
            else if (stuffDef != null && !stuffDef.IsStuff)
            {
                yield return $"{label} uses non-stuff ThingDef "
                    + $"{stuffDef.defName}.";
            }
        }
    }

    public class CulturalProfileMatcher
    {
        public bool playerStarterOnly;
        public List<ThingDef> races = new List<ThingDef>();
        public List<XenotypeDef> xenotypes = new List<XenotypeDef>();
        public List<PawnKindDef> pawnKinds = new List<PawnKindDef>();
        public List<FactionDef> factions = new List<FactionDef>();

        public bool HasIdentityCriterion
        {
            get
            {
                return !races.NullOrEmpty()
                    || !xenotypes.NullOrEmpty()
                    || !pawnKinds.NullOrEmpty()
                    || !factions.NullOrEmpty();
            }
        }

        public bool Matches(Pawn pawn, PawnGenerationContext context)
        {
            if (pawn == null || !HasIdentityCriterion)
            {
                return false;
            }

            if (playerStarterOnly
                && context != PawnGenerationContext.PlayerStarter)
            {
                return false;
            }

            if (!races.NullOrEmpty()
                && !races.Contains(pawn.def))
            {
                return false;
            }

            if (!xenotypes.NullOrEmpty()
                && !xenotypes.Contains(pawn.genes?.Xenotype))
            {
                return false;
            }

            if (!pawnKinds.NullOrEmpty()
                && !pawnKinds.Contains(pawn.kindDef))
            {
                return false;
            }

            if (!factions.NullOrEmpty()
                && !factions.Contains(pawn.Faction?.def))
            {
                return false;
            }

            return true;
        }
    }

    public class CulturalStarterRule
    {
        public List<ScenPartDef> requiredScenarioParts
            = new List<ScenPartDef>();
        public float childhoodReplacementChance = 1f;
        public float adulthoodReplacementChance = 1f;
        public bool mergeWithVanillaAdulthoodPool;
        public int minimumBiologicalAge;
        public bool requireViolenceCapable;
        public bool replaceStartingApparel;
        public QualityCategory apparelQuality = QualityCategory.Normal;
        public List<ThingDef> apparel = new List<ThingDef>();
        public List<CulturalStarterApparelSlot> apparelSlots
            = new List<CulturalStarterApparelSlot>();
        public List<BackstoryDef> childhoods = new List<BackstoryDef>();
        public List<BackstoryDef> adulthoods = new List<BackstoryDef>();

        public bool HasStarterApparel
        {
            get
            {
                return !apparel.NullOrEmpty()
                    || (!apparelSlots.NullOrEmpty()
                        && apparelSlots.Any(slot => slot != null
                            && !slot.options.NullOrEmpty()));
            }
        }

        public bool AllowsPawn(Pawn pawn)
        {
            if (pawn == null)
            {
                return false;
            }

            if (minimumBiologicalAge > 0
                && (pawn.ageTracker == null
                    || pawn.ageTracker.AgeBiologicalYears
                        < minimumBiologicalAge))
            {
                return false;
            }

            if (requireViolenceCapable
                && pawn.WorkTagIsDisabled(WorkTags.Violent))
            {
                return false;
            }

            return true;
        }

        public bool MatchesScenario(Scenario scenario)
        {
            if (requiredScenarioParts.NullOrEmpty())
            {
                return true;
            }

            if (scenario == null)
            {
                return false;
            }

            for (int index = 0;
                index < requiredScenarioParts.Count;
                index++)
            {
                ScenPartDef requiredPart = requiredScenarioParts[index];
                if (requiredPart == null
                    || !scenario.AllParts.Any(part => part?.def == requiredPart))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public class CulturalStarterApparelSlot
    {
        public float selectionChance = 1f;
        public string variantGroup;
        public List<CulturalStarterApparelOption> options
            = new List<CulturalStarterApparelOption>();

        public bool TrySelect(
            IDictionary<string, string> selectedVariantKeys,
            out CulturalStarterApparelOption selectedOption)
        {
            selectedOption = null;
            if (options.NullOrEmpty()
                || !Rand.Chance(selectionChance))
            {
                return false;
            }

            List<CulturalStarterApparelOption> candidates = options;
            if (!string.IsNullOrEmpty(variantGroup)
                && selectedVariantKeys != null
                && selectedVariantKeys.TryGetValue(
                    variantGroup,
                    out string selectedVariantKey))
            {
                candidates = options
                    .Where(option => option != null
                        && option.variantKey == selectedVariantKey)
                    .ToList();
            }

            if (!candidates.TryRandomElementByWeight(
                    option => option?.weight ?? 0f,
                    out selectedOption)
                || selectedOption == null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(variantGroup)
                && !string.IsNullOrEmpty(selectedOption.variantKey)
                && selectedVariantKeys != null)
            {
                selectedVariantKeys[variantGroup]
                    = selectedOption.variantKey;
            }

            return true;
        }
    }

    public class CulturalStarterApparelOption
    {
        public ThingDef apparel;
        public ThingDef stuff;
        public float weight = 1f;
        public string variantKey;
    }

    public class CulturalNameRule
    {
        public CulturalPawnNameGroup nameGroup
            = CulturalPawnNameGroup.None;
        public List<BackstoryDef> childhoods = new List<BackstoryDef>();
        public List<BackstoryDef> adulthoods = new List<BackstoryDef>();

        public bool Matches(Pawn pawn)
        {
            if (pawn?.story == null)
            {
                return false;
            }

            bool hasCriterion = false;

            if (!childhoods.NullOrEmpty())
            {
                hasCriterion = true;
                if (!childhoods.Contains(pawn.story.Childhood))
                {
                    return false;
                }
            }

            if (!adulthoods.NullOrEmpty())
            {
                hasCriterion = true;
                if (!adulthoods.Contains(pawn.story.Adulthood))
                {
                    return false;
                }
            }

            return hasCriterion;
        }
    }
}
