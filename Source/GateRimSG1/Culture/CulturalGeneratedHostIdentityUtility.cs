using System.Collections.Generic;
using System.Linq;
using GateRimSG1.Names;
using RimWorld;
using Verse;

namespace GateRimSG1.Culture
{
    /// <summary>
    /// Generates a stable historical host identity from the cultural profile
    /// attached to a pawn generated already joined with a symbiote.
    /// </summary>
    public static class CulturalGeneratedHostIdentityUtility
    {
        public static bool TryGenerate(
            Pawn pawn,
            string identityKey,
            string excludedName,
            out GeneratedHostIdentity identity)
        {
            identity = null;

            CulturalPawnProfileDef profile = CulturalProfileResolver.ResolveProfile(
                pawn,
                PawnGenerationContext.NonPlayer);

            List<GeneratedHostOriginDef> origins = profile?.generatedHostOrigins
                ?.Where(origin => origin != null && origin.CanGenerateFor(pawn))
                .OrderBy(origin => origin.defName)
                .ToList();

            if (origins.NullOrEmpty())
            {
                return false;
            }

            GeneratedHostOriginDef selectedOrigin = SelectWeightedStable(
                origins,
                identityKey + ":origin");

            if (selectedOrigin == null)
            {
                return false;
            }

            BackstoryDef childhood = SelectStable(
                selectedOrigin.childhoods,
                identityKey + ":childhood",
                BackstorySlot.Childhood);
            BackstoryDef adulthood = SelectStable(
                selectedOrigin.adulthoods,
                identityKey + ":adulthood",
                BackstorySlot.Adulthood);
            Name name = GenerateDistinctStableName(
                selectedOrigin.nameGroup,
                pawn.gender,
                identityKey,
                excludedName);

            if (name == null || childhood == null || adulthood == null)
            {
                return false;
            }

            identity = new GeneratedHostIdentity(
                selectedOrigin,
                name,
                childhood,
                adulthood);
            return true;
        }


        private static Name GenerateDistinctStableName(
            CulturalPawnNameGroup nameGroup,
            Gender gender,
            string identityKey,
            string excludedName)
        {
            const int MaxAttempts = 16;

            for (int attempt = 0; attempt < MaxAttempts; attempt++)
            {
                Name candidate = CulturalPawnNameUtility.GenerateStableName(
                    nameGroup,
                    gender,
                    identityKey + ":name:" + attempt);

                if (candidate == null)
                {
                    return null;
                }

                if (excludedName.NullOrEmpty()
                    || candidate.ToStringFull != excludedName)
                {
                    return candidate;
                }
            }

            return null;
        }

        private static GeneratedHostOriginDef SelectWeightedStable(
            List<GeneratedHostOriginDef> origins,
            string key)
        {
            float totalWeight = origins.Sum(origin => origin.weight);
            if (totalWeight <= 0f)
            {
                return null;
            }

            float selection = StableFraction(key) * totalWeight;
            float accumulated = 0f;

            for (int index = 0; index < origins.Count; index++)
            {
                accumulated += origins[index].weight;
                if (selection < accumulated)
                {
                    return origins[index];
                }
            }

            return origins[origins.Count - 1];
        }

        private static BackstoryDef SelectStable(
            List<BackstoryDef> configuredBackstories,
            string key,
            BackstorySlot expectedSlot)
        {
            List<BackstoryDef> candidates = configuredBackstories
                ?.Where(backstory => backstory != null
                    && backstory.slot == expectedSlot)
                .OrderBy(backstory => backstory.defName)
                .ToList();

            if (candidates.NullOrEmpty())
            {
                return null;
            }

            return candidates[StableIndex(key, candidates.Count)];
        }

        private static float StableFraction(string key)
        {
            uint hash = StableHash(key);
            return hash / ((float)uint.MaxValue + 1f);
        }

        private static int StableIndex(string key, int count)
        {
            return count <= 1 ? 0 : (int)(StableHash(key) % (uint)count);
        }

        private static uint StableHash(string value)
        {
            unchecked
            {
                uint hash = 2166136261u;
                string source = value ?? string.Empty;

                for (int index = 0; index < source.Length; index++)
                {
                    hash ^= source[index];
                    hash *= 16777619u;
                }

                return hash;
            }
        }
    }

    public sealed class GeneratedHostIdentity
    {
        public GeneratedHostOriginDef Origin { get; }
        public Name Name { get; }
        public BackstoryDef Childhood { get; }
        public BackstoryDef Adulthood { get; }

        public GeneratedHostIdentity(
            GeneratedHostOriginDef origin,
            Name name,
            BackstoryDef childhood,
            BackstoryDef adulthood)
        {
            Origin = origin;
            Name = name;
            Childhood = childhood;
            Adulthood = adulthood;
        }
    }
}
