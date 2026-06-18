using RimWorld;
using Verse;
using Verse.AI.Group;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Keeps a Tok'ra medical liaison near the selected meeting point until
    /// the handoff succeeds or the operation tracker orders departure.
    /// </summary>
    public class LordJob_TokraMedicalSupplyLiaison : LordJob_VisitColony
    {
        public const string LeaveMemo = "GR_TokraMedicalSupplyLiaisonLeave";

        public LordJob_TokraMedicalSupplyLiaison()
        {
        }

        public LordJob_TokraMedicalSupplyLiaison(
            Faction faction,
            IntVec3 meetingSpot,
            int? durationTicks = null)
            : base(faction, meetingSpot, durationTicks)
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
