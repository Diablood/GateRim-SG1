using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Shared Goa'uld-aligned Jaffa direct-assault raid path.
    ///
    /// The original controlled IncidentDef keeps a zero storyteller chance
    /// for developer regression tests. A separate low-frequency natural
    /// incident reuses this validated path after the visible world faction
    /// baseline is enabled.
    /// </summary>
    public class IncidentWorker_GoauldJaffaControlledRaid
        : IncidentWorker_RaidEnemy
    {
        private const float DefaultControlledRaidPoints = 500f;

        protected virtual string ControlledRaidPurpose
        {
            get
            {
                return "controlled Goa'uld Jaffa direct-assault tests";
            }
        }

        protected virtual string RaidLogContext
        {
            get
            {
                return "controlled Goa'uld Jaffa raid";
            }
        }

        protected virtual RaidStrategyDef ControlledRaidStrategy
        {
            get
            {
                return RaidStrategyDefOf.ImmediateAttack;
            }
        }

        protected virtual bool ControlledRaidCanSteal
        {
            get
            {
                return false;
            }
        }

        protected virtual bool ControlledRaidCanKidnap
        {
            get
            {
                return false;
            }
        }

        protected virtual bool ControlledRaidCanTimeoutOrFlee
        {
            get
            {
                return true;
            }
        }

        protected virtual float ResolveMissingRaidPoints(Map map)
        {
            return DefaultControlledRaidPoints;
        }

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
                    $"Cannot start {RaidLogContext}: "
                    + "the incident target is not a map.");

                return false;
            }

            Faction goauldFaction =
                GoauldSystemLordFactionUtility.GetOrCreateFaction(
                    ControlledRaidPurpose);

            if (goauldFaction == null)
            {
                GR_Log.Error(
                    $"Cannot start {RaidLogContext}: "
                    + "the System Lord faction could not be resolved.");

                return false;
            }

            parms.faction = goauldFaction;
            parms.forced = true;
            parms.raidStrategy = ControlledRaidStrategy;
            parms.raidArrivalMode = PawnsArrivalModeDefOf.EdgeWalkIn;
            parms.canSteal = ControlledRaidCanSteal;
            parms.canKidnap = ControlledRaidCanKidnap;
            parms.canTimeoutOrFlee = ControlledRaidCanTimeoutOrFlee;

            if (!(parms.points > 0f))
            {
                parms.points = ResolveMissingRaidPoints(parms.target as Map);
            }

            bool succeeded = base.TryExecuteWorker(parms);

            if (!succeeded)
            {
                GR_Log.Warning(
                    $"Unable to start {RaidLogContext} after "
                    + "resolving the System Lord faction.");
            }

            return succeeded;
        }
    }
}
