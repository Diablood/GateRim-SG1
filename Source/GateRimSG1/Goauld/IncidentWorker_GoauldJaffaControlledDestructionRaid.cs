using RimWorld;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Developer-only controlled Goa'uld Jaffa destruction raid.
    ///
    /// This doctrine keeps the validated sustained military assault, then
    /// enters a separate opportunistic recovery phase before extraction.
    /// Natural Goa'uld raids remain disabled.
    /// </summary>
    public class IncidentWorker_GoauldJaffaControlledDestructionRaid
        : IncidentWorker_GoauldJaffaControlledRaid
    {
        protected override string ControlledRaidPurpose
        {
            get
            {
                return "controlled Goa'uld Jaffa destruction-raid tests";
            }
        }

        protected override RaidStrategyDef ControlledRaidStrategy
        {
            get
            {
                return GR_DefOf.SG1_GoauldJaffaDestructionAssault;
            }
        }

        protected override bool ControlledRaidCanTimeoutOrFlee
        {
            get
            {
                return false;
            }
        }
    }
}
