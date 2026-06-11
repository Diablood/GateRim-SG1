using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Developer-only controlled Goa'uld-aligned Jaffa raid.
    ///
    /// The IncidentDef has a zero storyteller base chance. It exists only
    /// so developer tools can create a real hidden hostile faction instance
    /// and exercise the vanilla Combat pawn-group and raid workflows.
    /// Natural Goa'uld raids remain disabled.
    /// </summary>
    public class IncidentWorker_GoauldJaffaControlledRaid
        : IncidentWorker_RaidEnemy
    {
        private const float DefaultControlledRaidPoints = 500f;

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            return parms?.target is Map
                && GR_DefOf.SG1_GoauldSystemLordPrototype != null;
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            if (!(parms?.target is Map))
            {
                GR_Log.Warning(
                    "Cannot start controlled Goa'uld Jaffa raid: "
                    + "the incident target is not a map.");

                return false;
            }

            Faction goauldFaction =
                GoauldSystemLordFactionUtility.GetOrCreateHiddenFaction(
                    "controlled Goa'uld Jaffa raid tests");

            if (goauldFaction == null)
            {
                GR_Log.Error(
                    "Cannot start controlled Goa'uld Jaffa raid: "
                    + "the hidden System Lord faction could not be created.");

                return false;
            }

            parms.faction = goauldFaction;
            parms.forced = true;

            if (!(parms.points > 0f))
            {
                parms.points = DefaultControlledRaidPoints;
            }

            bool succeeded = base.TryExecuteWorker(parms);

            if (!succeeded)
            {
                GR_Log.Warning(
                    "Unable to start controlled Goa'uld Jaffa raid after "
                    + "creating or resolving the hidden System Lord faction.");
            }

            return succeeded;
        }
    }
}
