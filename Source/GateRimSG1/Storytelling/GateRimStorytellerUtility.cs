
using RimWorld;
using Verse;

namespace GateRimSG1.Storytelling
{
    /// <summary>
    /// Stable identity and activation checks for the optional GateRim SG-1
    /// storyteller.
    ///
    /// Future strategic systems must use this utility instead of changing
    /// pacing or incident values for unrelated storytellers.
    /// </summary>
    public static class GateRimStorytellerUtility
    {
        public const string GateRimStorytellerDefName =
            "SG1_GateRimStoryteller";
        public const string BaselineStorytellerDefName = "Cassandra";

        public static StorytellerDef GateRimStorytellerDef
            => DefDatabase<StorytellerDef>.GetNamedSilentFail(
                GateRimStorytellerDefName);

        public static StorytellerDef BaselineStorytellerDef
            => DefDatabase<StorytellerDef>.GetNamedSilentFail(
                BaselineStorytellerDefName);

        public static StorytellerDef ActiveStorytellerDef
        {
            get
            {
                if (Verse.Current.Game == null)
                {
                    return null;
                }

                return Find.Storyteller?.def;
            }
        }

        public static bool IsGateRimStoryteller(StorytellerDef storytellerDef)
        {
            return storytellerDef != null
                && storytellerDef.defName == GateRimStorytellerDefName;
        }

        public static bool IsGateRimStorytellerActive
            => IsGateRimStoryteller(ActiveStorytellerDef);

        public static string ActiveStorytellerDefName
            => ActiveStorytellerDef?.defName ?? "<none>";

        public static string ActiveStorytellerLabel
            => ActiveStorytellerDef?.LabelCap ?? "<none>";
    }
}
