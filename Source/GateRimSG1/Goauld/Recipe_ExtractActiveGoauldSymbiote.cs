using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Difficult surgery that removes an established Goa'uld symbiote from an
    /// active host after the patient has been secured by the colony.
    /// </summary>
    public class Recipe_ExtractActiveGoauldSymbiote
        : Recipe_ExtractGoauldSymbioteBase
    {
        protected override HediffDef TargetHediffDef
            => GR_DefOf.SG1_GoauldHostSymbiote;

        protected override string OperationDebugLabel
            => "Active Goa'uld host extraction surgery";

        protected override string InternalErrorTranslationKey
            => "GR_ActiveExtractionSurgery_InternalError";

        protected override string NoSpawnCellTranslationKey
            => "GR_ActiveExtractionSurgery_NoSpawnCell";

        protected override string SuccessTranslationKey
            => "GR_ActiveExtractionSurgery_Success";

        protected override bool SedateExtractedSymbiote => true;

        protected override bool CanExtractFromHost(
            Pawn pawn,
            GoauldSymbioteData symbioteData)
        {
            return pawn != null
                && symbioteData?.Origin == GoauldSymbioteOrigin.Goauld
                && (pawn.Faction == Faction.OfPlayer
                    || pawn.IsPrisonerOfColony);
        }
    }
}
