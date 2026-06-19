using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Compatibility bridge for a 0.3.0 save made while the former direct
    /// module job was active. New jobs are never created with this Def.
    /// </summary>
    public class JobDriver_TokraSecureOrganicDeadDrop : JobDriver
    {
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return true;
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            Toil redirect = ToilMaker.MakeToil(
                "RedirectLegacyTokraIntelligenceJob");
            redirect.initAction = delegate
            {
                Messages.Message(
                    "GR_TokraOrganicOperation_IntelligenceLegacyJobRedirected"
                        .Translate(),
                    pawn,
                    MessageTypeDefOf.NeutralEvent,
                    historical: false);
            };
            redirect.defaultCompleteMode = ToilCompleteMode.Instant;
            yield return redirect;
        }
    }
}
