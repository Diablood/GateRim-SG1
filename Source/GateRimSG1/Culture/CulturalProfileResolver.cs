using System.Collections.Generic;
using System.Linq;
using GateRimSG1.Names;
using RimWorld;
using Verse;

namespace GateRimSG1.Culture
{
    /// <summary>
    /// Resolves the highest-priority Def-driven cultural profile for a pawn.
    /// </summary>
    public static class CulturalProfileResolver
    {
        private static readonly HashSet<string> ReportedAmbiguities
            = new HashSet<string>();

        public static CulturalPawnProfileDef ResolveProfile(
            Pawn pawn,
            PawnGenerationContext context)
        {
            if (pawn == null)
            {
                return null;
            }

            List<CulturalPawnProfileDef> matches =
                DefDatabase<CulturalPawnProfileDef>
                    .AllDefsListForReading
                    .Where(profile => profile != null
                        && profile.Matches(pawn, context))
                    .OrderByDescending(profile => profile.priority)
                    .ThenBy(profile => profile.defName)
                    .ToList();

            if (matches.Count == 0)
            {
                return null;
            }

            CulturalPawnProfileDef selected = matches[0];
            List<CulturalPawnProfileDef> tied = matches
                .Where(profile => profile.priority == selected.priority)
                .ToList();

            if (tied.Count > 1)
            {
                string key = context + ":" + string.Join(
                    ",",
                    tied.Select(profile => profile.defName));

                if (ReportedAmbiguities.Add(key))
                {
                    GR_Log.Warning(
                        $"Ambiguous cultural profiles for "
                        + $"{pawn.ThingID ?? pawn.LabelShort}: {key}. "
                        + $"Using {selected.defName}.");
                }
            }

            return selected;
        }

        public static CulturalPawnProfileDef ResolveIdentityProfile(
            CulturalPawnNameGroup nameGroup)
        {
            if (nameGroup == CulturalPawnNameGroup.None)
            {
                return null;
            }

            List<CulturalPawnProfileDef> matches =
                DefDatabase<CulturalPawnProfileDef>
                    .AllDefsListForReading
                    .Where(profile => profile != null
                        && profile.defaultNameGroup == nameGroup
                        && profile.HasIdentityBackstories)
                    .OrderByDescending(profile => profile.priority)
                    .ThenBy(profile => profile.defName)
                    .ToList();

            if (matches.Count == 0)
            {
                return null;
            }

            CulturalPawnProfileDef selected = matches[0];
            List<CulturalPawnProfileDef> tied = matches
                .Where(profile => profile.priority == selected.priority)
                .ToList();

            if (tied.Count > 1)
            {
                string key = "identity:" + nameGroup + ":" + string.Join(
                    ",",
                    tied.Select(profile => profile.defName));

                if (ReportedAmbiguities.Add(key))
                {
                    GR_Log.Warning(
                        $"Ambiguous cultural identity profiles for "
                        + $"{nameGroup}: {key}. Using {selected.defName}.");
                }
            }

            return selected;
        }

        public static CulturalPawnNameGroup ResolveNameGroup(
            Pawn pawn,
            PawnGenerationContext context)
        {
            CulturalPawnProfileDef profile = ResolveProfile(pawn, context);
            return profile?.NameGroupFor(pawn)
                ?? CulturalPawnNameGroup.None;
        }

        public static bool TryResolveStarterRule(
            Pawn pawn,
            out CulturalPawnProfileDef profile,
            out CulturalStarterRule rule)
        {
            profile = ResolveProfile(
                pawn,
                PawnGenerationContext.PlayerStarter);
            rule = profile?.StarterRuleFor(Find.Scenario);
            return profile != null && rule != null;
        }
    }
}
