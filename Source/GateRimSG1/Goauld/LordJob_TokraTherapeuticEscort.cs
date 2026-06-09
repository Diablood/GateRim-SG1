using RimWorld;
using Verse;
using Verse.AI.Group;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Peaceful Tok'ra medical escort visit with a safe early-departure memo.
    ///
    /// The vanilla visitor graph already knows how to prepare an exit
    /// destination and move pawns off the map. This subclass adds one memo
    /// transition so the lifecycle tracker can request that departure without
    /// replacing a LordJob while the lord is ticking.
    /// </summary>
    public class LordJob_TokraTherapeuticEscort : LordJob_VisitColony
    {
        public const string LeaveMemo = "GR_TokraTherapeuticEscortLeave";

        public LordJob_TokraTherapeuticEscort()
        {
        }

        public LordJob_TokraTherapeuticEscort(
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
