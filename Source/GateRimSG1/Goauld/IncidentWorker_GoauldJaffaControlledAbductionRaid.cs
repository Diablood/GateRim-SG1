using RimWorld;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Developer-only controlled Goa'uld Jaffa abduction raid.
    ///
    /// The shared controlled-raid setup resolves the hidden faction and
    /// target-cache refresh. This subclass selects the dedicated abduction
    /// strategy while keeping natural raids disabled.
    /// </summary>
    public class IncidentWorker_GoauldJaffaControlledAbductionRaid
        : IncidentWorker_GoauldJaffaControlledRaid
    {
        protected override string ControlledRaidPurpose
        {
            get
            {
                return "controlled Goa'uld Jaffa abduction-raid tests";
            }
        }

        protected override RaidStrategyDef ControlledRaidStrategy
        {
            get
            {
                return GR_DefOf.SG1_GoauldJaffaAbductionAssault;
            }
        }

        protected override bool ControlledRaidCanKidnap
        {
            get
            {
                return true;
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
