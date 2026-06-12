using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Low-frequency natural Goa'uld Jaffa direct-assault raid.
    ///
    /// This subclass reuses the validated controlled direct-assault path:
    /// explicit Goa'uld faction, ImmediateAttack, no stealing and no
    /// kidnapping. It only fires when the visible world faction already
    /// exists, keeping old saves without world presence stable.
    /// </summary>
    public class IncidentWorker_GoauldJaffaNaturalRaid
        : IncidentWorker_GoauldJaffaControlledRaid
    {
        protected override string ControlledRaidPurpose
        {
            get
            {
                return "natural Goa'uld Jaffa direct-assault raids";
            }
        }

        protected override string RaidLogContext
        {
            get
            {
                return "natural Goa'uld Jaffa raid";
            }
        }

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            return base.CanFireNowSub(parms)
                && Find.FactionManager?.FirstFactionOfDef(
                    GR_DefOf.SG1_GoauldSystemLordPrototype) != null;
        }
    }
}
