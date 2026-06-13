using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Jaffa
{
    /// <summary>
    /// Low-frequency peaceful Free Jaffa visitor incident.
    ///
    /// The incident reuses RimWorld's vanilla visitor-group workflow while
    /// explicitly selecting an existing visible Free Jaffa world faction that
    /// is still non-hostile toward the player.
    ///
    /// No hidden fallback faction is created. Old saves without a Free Jaffa
    /// world presence simply refuse the incident quietly.
    /// </summary>
    public class IncidentWorker_FreeJaffaPeacefulVisitors
        : IncidentWorker_VisitorGroup
    {
        private const float PointsPerVisitor = 110f;

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            return parms?.target is Map
                && GR_DefOf.SG1_FreeJaffa != null
                && FreeJaffaFactionUtility.HasExistingNonHostileFaction();
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Faction freeJaffaFaction;

            if (!FreeJaffaFactionUtility.TryGetRandomNonHostileFaction(
                out freeJaffaFaction))
            {
                GR_Log.Message(
                    "Cannot start peaceful Free Jaffa visitors: "
                    + "no existing non-hostile Free Jaffa world faction "
                    + "is available.");

                return false;
            }

            parms.faction = freeJaffaFaction;

            bool succeeded = base.TryExecuteWorker(parms);

            if (succeeded)
            {
                GR_Log.Message(
                    $"Started peaceful Free Jaffa visitor group for "
                    + $"{freeJaffaFaction.Name} with {parms.points} points.");
            }

            return succeeded;
        }

        protected override void ResolveParmsPoints(IncidentParms parms)
        {
            if (!(parms.points >= 0f))
            {
                parms.points = PointsPerVisitor
                    * Rand.RangeInclusive(2, 4);
            }
        }

        protected override void SendLetter(
            IncidentParms parms,
            List<Pawn> pawns,
            Pawn leader,
            bool traderExists)
        {
            SendStandardLetter(
                parms,
                pawns[0]);
        }
    }
}
