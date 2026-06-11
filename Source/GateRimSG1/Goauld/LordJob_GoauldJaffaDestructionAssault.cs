using RimWorld;
using Verse.AI.Group;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Goa'uld Jaffa destruction doctrine.
    ///
    /// The group fights as a sustained military assault first. After enough
    /// colony damage or a maximum military-phase deadline, surviving Jaffa
    /// opportunistically recover victims and valuables while the rest keep
    /// fighting. A final deadline forces extraction.
    /// </summary>
    public class LordJob_GoauldJaffaDestructionAssault : LordJob
    {
        private const float VictoryDamageFraction = 0.15f;
        private const float VictoryMinimumDamage = 300f;
        private const int MaximumMilitaryPhaseTicks = 12000;
        private const int RecoveryWindowTicks = 2400;

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

            LordToil_AssaultColony militaryAssault =
                new LordToil_AssaultColony
                {
                    useAvoidGrid = true
                };

            stateGraph.AddToil(militaryAssault);

            LordToil_GoauldJaffaRecoveryCover recoveryAndCover =
                new LordToil_GoauldJaffaRecoveryCover
                {
                    useAvoidGrid = true
                };

            stateGraph.AddToil(recoveryAndCover);

            LordToil_GoauldJaffaRecoveryCover extractAndLeave =
                new LordToil_GoauldJaffaRecoveryCover
                {
                    cover = false,
                    useAvoidGrid = true
                };

            stateGraph.AddToil(extractAndLeave);

            Transition militaryVictory =
                new Transition(
                    militaryAssault,
                    recoveryAndCover);

            militaryVictory.AddTrigger(
                new Trigger_FractionColonyDamageTaken(
                    VictoryDamageFraction,
                    VictoryMinimumDamage));

            stateGraph.AddTransition(militaryVictory);

            Transition militaryDeadline =
                new Transition(
                    militaryAssault,
                    recoveryAndCover);

            militaryDeadline.AddTrigger(
                new Trigger_TicksPassed(
                    MaximumMilitaryPhaseTicks));

            stateGraph.AddTransition(militaryDeadline);

            Transition extractionDeadline =
                new Transition(
                    recoveryAndCover,
                    extractAndLeave);

            extractionDeadline.AddTrigger(
                new Trigger_TicksPassed(
                    RecoveryWindowTicks));

            stateGraph.AddTransition(extractionDeadline);

            return stateGraph;
        }
    }
}
