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

        protected override void NotifySuccessfulExtraction(
            Pawn formerHost,
            GoauldSymbioteData symbioteData,
            Pawn freeSymbiote)
        {
            ReleaseGeneratedFormerHostFromDomain(
                formerHost,
                symbioteData);

            GameComponent_GoauldDomainReprisalTracker
                .NotifyActiveHostExtracted(
                    formerHost,
                    symbioteData,
                    freeSymbiote);
        }

        protected override void NotifyFailedExtraction(
            Pawn formerHost,
            GoauldSymbioteData symbioteData,
            Map operationMap,
            string formerHostLabel)
        {
            if (formerHost?.Dead != true)
            {
                return;
            }

            GameComponent_GoauldDomainReprisalTracker
                .NotifyFatalActiveHostExtraction(
                    operationMap,
                    formerHostLabel,
                    symbioteData);
        }

        private static void ReleaseGeneratedFormerHostFromDomain(
            Pawn formerHost,
            GoauldSymbioteData symbioteData)
        {
            if (formerHost == null
                || formerHost.Dead
                || !formerHost.IsPrisonerOfColony
                || formerHost.Faction != symbioteData?.AllegianceFaction
                || !GoauldSystemLordFactionUtility.IsSystemLordFaction(
                    formerHost.Faction)
                || (formerHost.kindDef != GR_DefOf.SG1_GoauldHostCaste
                    && formerHost.kindDef
                        != GR_DefOf.SG1_GoauldSystemLordHost))
            {
                return;
            }

            string formerHostLabel = formerHost.LabelShortCap;
            string domainName = formerHost.Faction.Name;

            formerHost.SetFaction(null);

            if (!formerHost.IsPrisonerOfColony)
            {
                formerHost.guest?.SetGuestStatus(
                    Faction.OfPlayer,
                    GuestStatus.Prisoner);
            }

            if (formerHost.guest != null)
            {
                formerHost.guest.Recruitable = true;
            }

            Messages.Message(
                "GR_ActiveExtractionSurgery_FormerHostReleased".Translate(
                    formerHostLabel,
                    domainName),
                formerHost,
                MessageTypeDefOf.PositiveEvent,
                historical: true);

            GR_Log.Message(
                $"Released generated former host {formerHostLabel} "
                + $"({formerHost.ThingID}) from Goa'uld domain "
                + $"{domainName}; factionless prisoner="
                + $"{formerHost.IsPrisonerOfColony}, recruitable="
                + $"{formerHost.guest?.Recruitable ?? false}.");
        }
    }
}
