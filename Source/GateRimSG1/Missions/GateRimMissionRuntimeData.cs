using System.Collections.Generic;
using Verse;

namespace GateRimSG1.Missions
{
    /// <summary>
    /// Generic persistent data owned by one mission occurrence. Specialized
    /// systems may continue to store typed fields beside this state while they
    /// are migrated incrementally.
    /// </summary>
    public sealed class GateRimMissionRuntimeData : IExposable
    {
        public string missionDefName;
        public string phaseId;
        public float baseThreatPoints;
        public float scaledThreatPoints;
        public float difficultyFactor = 1f;
        public Dictionary<string, int> textVariantIndexes
            = new Dictionary<string, int>();
        public Dictionary<string, int> counters
            = new Dictionary<string, int>();
        public Dictionary<string, float> scalars
            = new Dictionary<string, float>();
        public Dictionary<string, string> strings
            = new Dictionary<string, string>();

        public void ExposeData()
        {
            Scribe_Values.Look(ref missionDefName, "missionDefName");
            Scribe_Values.Look(ref phaseId, "phaseId");
            Scribe_Values.Look(ref baseThreatPoints, "baseThreatPoints", 0f);
            Scribe_Values.Look(ref scaledThreatPoints, "scaledThreatPoints", 0f);
            Scribe_Values.Look(ref difficultyFactor, "difficultyFactor", 1f);
            Scribe_Collections.Look(
                ref textVariantIndexes,
                "textVariantIndexes",
                LookMode.Value,
                LookMode.Value);
            Scribe_Collections.Look(
                ref counters,
                "counters",
                LookMode.Value,
                LookMode.Value);
            Scribe_Collections.Look(
                ref scalars,
                "scalars",
                LookMode.Value,
                LookMode.Value);
            Scribe_Collections.Look(
                ref strings,
                "strings",
                LookMode.Value,
                LookMode.Value);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                EnsureCollections();
            }
        }

        public int GetTextVariant(string bankKey)
        {
            EnsureCollections();
            int value;
            return textVariantIndexes.TryGetValue(bankKey, out value)
                ? value
                : -1;
        }

        public void SetTextVariant(string bankKey, int index)
        {
            EnsureCollections();

            if (!string.IsNullOrEmpty(bankKey))
            {
                textVariantIndexes[bankKey] = index;
            }
        }

        public string GetString(string key, string fallback = null)
        {
            EnsureCollections();
            string value;

            return !string.IsNullOrEmpty(key)
                    && strings.TryGetValue(key, out value)
                ? value
                : fallback;
        }

        public void SetString(string key, string value)
        {
            EnsureCollections();

            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            if (value == null)
            {
                strings.Remove(key);
                return;
            }

            strings[key] = value;
        }

        public void Reset()
        {
            missionDefName = null;
            phaseId = null;
            baseThreatPoints = 0f;
            scaledThreatPoints = 0f;
            difficultyFactor = 1f;
            EnsureCollections();
            textVariantIndexes.Clear();
            counters.Clear();
            scalars.Clear();
            strings.Clear();
        }

        private void EnsureCollections()
        {
            if (textVariantIndexes == null)
            {
                textVariantIndexes = new Dictionary<string, int>();
            }

            if (counters == null)
            {
                counters = new Dictionary<string, int>();
            }

            if (scalars == null)
            {
                scalars = new Dictionary<string, float>();
            }

            if (strings == null)
            {
                strings = new Dictionary<string, string>();
            }
        }
    }
}
