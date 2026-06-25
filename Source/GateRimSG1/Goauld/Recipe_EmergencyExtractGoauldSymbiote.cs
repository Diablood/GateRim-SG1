using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Medical surgery used during the one-day recent-implantation window.
    /// </summary>
    public class Recipe_EmergencyExtractGoauldSymbiote
        : Recipe_ExtractGoauldSymbioteBase
    {
        protected override HediffDef TargetHediffDef
            => GR_DefOf.SG1_GoauldRecentImplantation;

        protected override string OperationDebugLabel
            => "Emergency Goa'uld extraction surgery";

        protected override string InternalErrorTranslationKey
            => "GR_EmergencyExtractionSurgery_InternalError";

        protected override string NoSpawnCellTranslationKey
            => "GR_EmergencyExtractionSurgery_NoSpawnCell";

        protected override string SuccessTranslationKey
            => "GR_EmergencyExtractionSurgery_Success";
    }
}
