using RimWorld;
using Verse;
using Verse.AI.Group;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Keeps a wounded Tok'ra agent on the colony map until the organic
    /// operation tracker confirms that the agent is fit to travel.
    /// </summary>
    public class LordJob_TokraWoundedAgentCare : LordJob_VisitColony
    {
        public const string LeaveMemo = "GR_TokraWoundedAgentLeave";

        public LordJob_TokraWoundedAgentCare()
        {
        }

        public LordJob_TokraWoundedAgentCare(
            Faction faction,
            IntVec3 chillSpot,
            int? durationTicks = null)
            : base(faction, chillSpot, durationTicks)
        {
        }

        public override StateGraph CreateGraph()
        {
            StateGraph graph = base.CreateGraph();
            LordToil exitStartingToil = exitSubgraph.StartingToil;
            Transition leaveTransition = new Transition(
                graph.StartingToil,
                exitStartingToil);

            for (int index = 1; index < graph.lordToils.Count; index++)
            {
                LordToil sourceToil = graph.lordToils[index];

                if (sourceToil == exitStartingToil
                    || exitSubgraph.lordToils.Contains(sourceToil))
                {
                    continue;
                }

                leaveTransition.AddSource(sourceToil);
            }

            leaveTransition.AddTrigger(new Trigger_Memo(LeaveMemo));
            leaveTransition.AddPreAction(
                new TransitionAction_EnsureHaveExitDestination());
            leaveTransition.AddPostAction(new TransitionAction_WakeAll());
            leaveTransition.AddPostAction(new TransitionAction_EndAllJobs());
            graph.AddTransition(leaveTransition, highPriority: true);
            return graph;
        }
    }
}
