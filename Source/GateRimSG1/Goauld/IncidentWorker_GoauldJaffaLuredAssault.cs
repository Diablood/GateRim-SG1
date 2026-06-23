using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Dedicated hostile response for the recurring Tok'ra diversion.
    ///
    /// The incident selects a mission-only pawn group containing explicit
    /// Ma'Tok breachers, uses the vanilla immediate-breaching strategy, then
    /// registers the exact spawned Jaffa with the active operation so victory
    /// and extraction can be resolved without treating unrelated Goa'uld pawns
    /// as participants.
    /// </summary>
    public class IncidentWorker_GoauldJaffaLuredAssault
        : IncidentWorker_GoauldJaffaControlledRaid
    {
        private const string BreachingStrategyDefName
            = "SG1_GoauldJaffaLuredBreachingAssault";

        private const string AssaultGroupKindDefName
            = "SG1_TokraDiversionAssault";

        protected override string ControlledRaidPurpose
        {
            get
            {
                return "Tok'ra diversion assault operation";
            }
        }

        protected override string RaidLogContext
        {
            get
            {
                return "Tok'ra diversion Goa'uld assault";
            }
        }

        protected override RaidStrategyDef ControlledRaidStrategy
        {
            get
            {
                return DefDatabase<RaidStrategyDef>.GetNamedSilentFail(
                    BreachingStrategyDefName);
            }
        }

        protected override bool ControlledRaidCanSteal
        {
            get
            {
                return true;
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

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = parms?.target as Map;
            RaidStrategyDef strategy = ControlledRaidStrategy;
            PawnGroupKindDef assaultGroupKind
                = DefDatabase<PawnGroupKindDef>.GetNamedSilentFail(
                    AssaultGroupKindDefName);
            FactionDef factionDef
                = GR_DefOf.SG1_GoauldSystemLordPrototype;

            if (map == null
                || strategy == null
                || assaultGroupKind == null
                || !HasCompatibleBreachingGroup(
                    factionDef,
                    assaultGroupKind))
            {
                GR_Log.Warning(
                    "Cannot start the Tok'ra diversion assault: the map, "
                    + "breaching raid strategy or dedicated Jaffa breach "
                    + "group is unavailable.");
                return false;
            }

            parms.pawnGroupKind = assaultGroupKind;

            HashSet<string> existingPawnIds = new HashSet<string>(
                map.mapPawns.AllPawnsSpawned
                    .Where(pawn => pawn != null)
                    .Select(pawn => pawn.ThingID));

            bool succeeded = base.TryExecuteWorker(parms);

            if (!succeeded)
            {
                return false;
            }

            List<Pawn> spawnedRaiders = map.mapPawns.AllPawnsSpawned
                .Where(pawn => pawn != null
                    && pawn.Faction == parms.faction
                    && !existingPawnIds.Contains(pawn.ThingID))
                .ToList();
            int breacherCount = spawnedRaiders.Count(
                pawn => pawn.kindDef?.isGoodBreacher == true);

            GameComponent_TokraOrganicOperationManager
                .RegisterDiversionAssaultRaidPawns(
                    map,
                    spawnedRaiders);

            GR_Log.Message(
                "Registered " + spawnedRaiders.Count
                + " Jaffa for the Tok'ra diversion assault on map "
                + map.uniqueID + ", including " + breacherCount
                + " dedicated breacher(s).");
            return true;
        }

        private static bool HasCompatibleBreachingGroup(
            FactionDef factionDef,
            PawnGroupKindDef groupKind)
        {
            return factionDef?.pawnGroupMakers != null
                && factionDef.pawnGroupMakers.Any(
                    groupMaker => groupMaker != null
                        && groupMaker.kindDef == groupKind
                        && groupMaker.options != null
                        && groupMaker.options.Any(
                            option => option != null
                                && option.kind != null
                                && option.kind.isGoodBreacher));
        }
    }
}
