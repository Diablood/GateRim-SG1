using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public static class GoauldFreeSymbioteIncursionDebugActions
    {
        private const float WeakColonyTestPoints = 300f;
        private const float AdvancedColonyTestPoints = 2600f;

        public static void ShowCurrentScaling()
        {
            Map map = Find.CurrentMap;

            if (map == null)
            {
                Messages.Message(
                    "No current map is available.",
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            float points = StorytellerUtility.DefaultThreatPointsNow(map);
            int count = IncidentWorker_GoauldFreeSymbioteIncursion
                .CalculateSymbioteCount(points);

            Messages.Message(
                $"Goa'uld free-symbiote incursion: points={points:0}; "
                + $"symbiotes={count}.",
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        public static void ForceCurrentScaling()
        {
            ForceIncident(0f, "current storyteller points");
        }

        public static void ForceWeakColonyScaling()
        {
            ForceIncident(WeakColonyTestPoints, "weak-colony test points");
        }

        public static void ForceAdvancedColonyScaling()
        {
            ForceIncident(
                AdvancedColonyTestPoints,
                "advanced-colony test points");
        }

        private static void ForceIncident(float explicitPoints, string context)
        {
            Map map = Find.CurrentMap;
            IncidentDef incidentDef = GR_DefOf.SG1_GoauldFreeSymbioteIncursion;

            if (map == null || incidentDef?.category == null)
            {
                Messages.Message(
                    "The current map or incident definition is unavailable.",
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            IncidentParms parms = StorytellerUtility.DefaultParmsNow(
                incidentDef.category,
                map);
            parms.forced = true;

            if (explicitPoints > 0f)
            {
                parms.points = explicitPoints;
            }

            if (!incidentDef.Worker.TryExecute(parms))
            {
                Messages.Message(
                    "The Goa'uld free-symbiote incursion could not start "
                    + $"with {context}.",
                    MessageTypeDefOf.RejectInput,
                    historical: false);
            }
        }
    }
}
