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
                        && rule.adulthoods.NullOrEmpty())
                    {
                        yield return $"{defName} starter rule {index} has no "
                            + "configured backstory.";
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
        public List<BackstoryDef> childhoods = new List<BackstoryDef>();
        public List<BackstoryDef> adulthoods = new List<BackstoryDef>();

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
