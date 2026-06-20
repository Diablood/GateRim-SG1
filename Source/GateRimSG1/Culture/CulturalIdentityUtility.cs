using System.Collections.Generic;
using System.Linq;
using GateRimSG1.Names;
using RimWorld;

namespace GateRimSG1.Culture
{
    /// <summary>
    /// Selects persistent identity backstories from the shared cultural
    /// profile framework. Selection is deterministic for a saved identity key.
    /// </summary>
    public static class CulturalIdentityUtility
    {
        public static BackstoryDef ResolveChildhood(
            CulturalPawnNameGroup nameGroup,
            string identityKey)
        {
            CulturalPawnProfileDef profile =
                CulturalProfileResolver.ResolveIdentityProfile(nameGroup);

            return SelectStable(
                profile?.identityChildhoods,
                identityKey,
                BackstorySlot.Childhood);
        }

        public static BackstoryDef ResolveAdulthood(
            CulturalPawnNameGroup nameGroup,
            string identityKey)
        {
            CulturalPawnProfileDef profile =
                CulturalProfileResolver.ResolveIdentityProfile(nameGroup);

            return SelectStable(
                profile?.identityAdulthoods,
                identityKey,
                BackstorySlot.Adulthood);
        }

        private static BackstoryDef SelectStable(
            List<BackstoryDef> configuredBackstories,
            string identityKey,
            BackstorySlot expectedSlot)
        {
            if (configuredBackstories == null)
            {
                return null;
            }

            List<BackstoryDef> candidates = configuredBackstories
                .Where(backstory => backstory != null
                    && backstory.slot == expectedSlot)
                .OrderBy(backstory => backstory.defName)
                .ToList();

            if (candidates.Count == 0)
            {
                return null;
            }

            int index = StableIndex(identityKey, candidates.Count);
            return candidates[index];
        }

        private static int StableIndex(string identityKey, int count)
        {
            if (count <= 1)
            {
                return 0;
            }

            unchecked
            {
                uint hash = 2166136261u;
                string value = identityKey ?? string.Empty;

                for (int index = 0; index < value.Length; index++)
                {
                    hash ^= value[index];
                    hash *= 16777619u;
                }

                return (int)(hash % (uint)count);
            }
        }
    }
}
