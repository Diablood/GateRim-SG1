using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Low-frequency peaceful Tok'ra visitor incident.
    ///
    /// The storyteller may select this incident rarely while developer tools
    /// remain available for controlled tests. The Tok'ra faction stays hidden
    /// and has no territorial settlements.
    ///
    /// The worker reuses the persistent hidden Tok'ra world-faction anchor,
    /// then reuses the vanilla peaceful-visitor workflow with
    /// the nested Tok'ra Peaceful pawn-group profile.
    /// </summary>
    public class IncidentWorker_TokraPeacefulVisitors : IncidentWorker_VisitorGroup
    {
        private const float PointsPerVisitor = 80f;

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            if (!TokraFactionUtility.HasPersistentFaction())
            {
                return false;
            }

            return parms?.target is Map
                && GR_DefOf.SG1_Tokra != null;
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Faction tokraFaction = TokraFactionUtility.GetOrCreatePersistentFaction(
                "peaceful Tok'ra visitors");

            if (tokraFaction == null)
            {
                GR_Log.Error(
                    "Cannot start peaceful Tok'ra visitors: "
                    + "the persistent hidden Tok'ra world faction could not be resolved.");

                return false;
            }

            parms.faction = tokraFaction;

            bool succeeded = base.TryExecuteWorker(parms);

            if (succeeded)
            {
                GR_Log.Message(
                    $"Started peaceful Tok'ra visitor group for "
                    + $"{tokraFaction.Name} with {parms.points} points.");
            }

            return succeeded;
        }

        protected override void ResolveParmsPoints(IncidentParms parms)
        {
            if (!(parms.points >= 0f))
            {
                parms.points = PointsPerVisitor
                    * Rand.RangeInclusive(1, 3);
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
