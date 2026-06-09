using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Low-frequency peaceful Tok'ra visitor incident.
    ///
    /// The storyteller may select this incident rarely while developer tools
    /// remain available for controlled tests. The Tok'ra faction stays hidden
    /// and disconnected from normal world generation.
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
            EnsureRelatedPawnsAreWorldManaged(pawns);

            SendStandardLetter(
                parms,
                pawns[0]);
        }

        private static void EnsureRelatedPawnsAreWorldManaged(List<Pawn> pawns)
        {
            if (pawns == null
                || pawns.Count == 0
                || Find.WorldPawns == null)
            {
                return;
            }

            Queue<Pawn> pendingPawns = new Queue<Pawn>();
            HashSet<Pawn> visitedPawns = new HashSet<Pawn>();

            for (int index = 0; index < pawns.Count; index++)
            {
                Pawn pawn = pawns[index];

                if (pawn != null)
                {
                    pendingPawns.Enqueue(pawn);
                }
            }

            while (pendingPawns.Count > 0)
            {
                Pawn pawn = pendingPawns.Dequeue();

                if (pawn?.relations == null
                    || !visitedPawns.Add(pawn))
                {
                    continue;
                }

                List<DirectPawnRelation> directRelations =
                    pawn.relations.DirectRelations;

                for (int index = 0; index < directRelations.Count; index++)
                {
                    Pawn relatedPawn = directRelations[index]?.otherPawn;

                    if (relatedPawn == null
                        || visitedPawns.Contains(relatedPawn))
                    {
                        continue;
                    }

                    pendingPawns.Enqueue(relatedPawn);

                    if (relatedPawn.Spawned
                        || relatedPawn.Destroyed
                        || relatedPawn.Discarded
                        || Find.WorldPawns.Contains(relatedPawn))
                    {
                        continue;
                    }

                    Find.WorldPawns.PassToWorld(
                        relatedPawn,
                        PawnDiscardDecideMode.KeepForever);

                    GR_Log.Message(
                        $"Registered unmanaged related pawn "
                        + $"{relatedPawn.LabelShort} ({relatedPawn.ThingID}) "
                        + "as a world pawn for Tok'ra visitor save safety.");
                }
            }
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
                + "for peaceful Tok'ra visitors.");

            return createdFaction;
        }
    }
}
