using RimWorld;
using Verse.AI.Group;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Goa'uld Jaffa abduction doctrine.
    ///
    /// The group begins with a sustained assault. Once a nearby downed
    /// colonist can be safely recovered, the group enters a cover-and-
    /// capture window: available Jaffa kidnap victims while the rest keep
    /// fighting. At the deadline, all surviving Jaffa extract with or
    /// without captives.
    /// </summary>
    public class LordJob_GoauldJaffaAbductionAssault : LordJob
    {
        private const int MaximumAssaultWithoutVictimTicks = 12000;
        private const int CaptureWindowTicks = 2400;

        public override bool GuiltyOnDowned
        {
            get
            {
                return true;
            }
        }

        public override StateGraph CreateGraph()
        {
            StateGraph stateGraph = new StateGraph();

            LordToil_AssaultColony initialAssault =
                new LordToil_AssaultColony
                {
                    useAvoidGrid = true
                };

            stateGraph.AddToil(initialAssault);

            LordToil_KidnapCover captureAndCover =
                new LordToil_KidnapCover
                {
                    useAvoidGrid = true
                };

            stateGraph.AddToil(captureAndCover);

            LordToil_KidnapCover extractAndLeave =
                new LordToil_KidnapCover
                {
                    cover = false,
                    useAvoidGrid = true
                };

            stateGraph.AddToil(extractAndLeave);

            Transition startCaptureWindow =
                new Transition(
                    initialAssault,
                    captureAndCover);

            startCaptureWindow.AddTrigger(
                new Trigger_GoauldJaffaKidnapVictimPresent());

            stateGraph.AddTransition(startCaptureWindow);

            Transition captureDeadline =
                new Transition(
                    captureAndCover,
                    extractAndLeave);

            captureDeadline.AddTrigger(
                new Trigger_TicksPassed(CaptureWindowTicks));

            stateGraph.AddTransition(captureDeadline);

            Transition noVictimDeadline =
                new Transition(
                    initialAssault,
                    extractAndLeave);

            noVictimDeadline.AddTrigger(
                new Trigger_TicksPassed(
                    MaximumAssaultWithoutVictimTicks));

            stateGraph.AddTransition(noVictimDeadline);

            return stateGraph;
        }
    }
}
