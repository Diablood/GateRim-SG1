using RimWorld;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Small direct-assault force sent to investigate an emission produced by
    /// accelerated Tok'ra intelligence decoding. The incident has no natural
    /// storyteller chance and is queued only by the organic operation.
    /// </summary>
    public class IncidentWorker_GoauldJaffaSignalPatrol
        : IncidentWorker_GoauldJaffaControlledRaid
    {
        protected override string ControlledRaidPurpose
        {
            get
            {
                return "Tok'ra intelligence interference patrol";
            }
        }

        protected override string RaidLogContext
        {
            get
            {
                return "Goa'uld Jaffa signal patrol";
            }
        }
    }
}
