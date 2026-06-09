using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Developer-triggered peaceful Tok'ra visitor prototype.
    ///
    /// The incident deliberately has zero storyteller base chance. Trigger it
    /// through developer tools while the Tok'ra faction remains hidden and
    /// disconnected from normal world generation.
    ///
    /// The worker creates the hidden Tok'ra faction on first use, then reuses
    /// the vanilla peaceful-visitor workflow with the nested Tok'ra Peaceful
    /// pawn-group profile.
    /// </summary>
    public class IncidentWorker_TokraPeacefulVisitors : IncidentWorker_VisitorGroup
    {
        private const float PointsPerVisitor = 80f;

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            return parms?.target is Map
                && GR_DefOf.SG1_Tokra != null;
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Faction tokraFaction = GetOrCreateTokraFaction();

            if (tokraFaction == null)
            {
                GR_Log.Error(
                    "Cannot start peaceful Tok'ra visitors: "
                    + "the hidden Tok'ra faction could not be created.");

                return false;
            }

            parms.faction = tokraFaction;

            bool succeeded = base.TryExecuteWorker(parms);

            if (succeeded)
            {
                GR_Log.Message(
                    $"Started peaceful Tok'ra visitor prototype for "
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

        private static Faction GetOrCreateTokraFaction()
        {
            if (GR_DefOf.SG1_Tokra == null
                || Find.FactionManager == null)
            {
                return null;
            }

            Faction existingFaction = Find.FactionManager.FirstFactionOfDef(
                GR_DefOf.SG1_Tokra);

            if (existingFaction != null)
            {
                return existingFaction;
            }

            Faction createdFaction = FactionGenerator.NewGeneratedFaction(
                new FactionGeneratorParms(
                    GR_DefOf.SG1_Tokra,
                    default(IdeoGenerationParms),
                    hidden: true));

            Find.FactionManager.Add(createdFaction);

            GR_Log.Message(
                $"Created hidden Tok'ra faction instance "
                + $"{createdFaction.Name} ({createdFaction.loadID}) "
                + "for peaceful visitor testing.");

            return createdFaction;
        }
    }
}
