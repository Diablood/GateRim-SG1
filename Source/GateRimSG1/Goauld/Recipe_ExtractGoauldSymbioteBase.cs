using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Shared transaction-safe surgery flow for transferring one persistent
    /// adult symbiote from a host Hediff back into a free symbiote pawn.
    /// </summary>
    public abstract class Recipe_ExtractGoauldSymbioteBase : Recipe_Surgery
    {
        protected abstract HediffDef TargetHediffDef { get; }

        protected abstract string OperationDebugLabel { get; }

        protected abstract string InternalErrorTranslationKey { get; }

        protected abstract string NoSpawnCellTranslationKey { get; }

        protected abstract string SuccessTranslationKey { get; }

        protected virtual bool SedateExtractedSymbiote => false;

        public override bool AvailableOnNow(
            Thing thing,
            BodyPartRecord part = null)
        {
            if (!base.AvailableOnNow(thing, part))
            {
                return false;
            }

            Pawn pawn = thing as Pawn;
            Hediff hostState = FindTargetHostState(pawn);
            HediffComp_GoauldSymbiote sourceComp
                = FindPersistentSymbioteComp(hostState);

            return pawn != null
                && hostState != null
                && sourceComp != null
                && CanExtractFromHost(pawn, sourceComp.SymbioteData);
        }

        public override void ApplyOnPawn(
            Pawn pawn,
            BodyPartRecord part,
            Pawn billDoer,
            List<Thing> ingredients,
            Bill bill)
        {
            if (pawn == null || pawn.health == null)
            {
                GR_Log.Error(
                    $"{OperationDebugLabel} received an unavailable patient.");

                return;
            }

            Hediff hostState = FindTargetHostState(pawn);
            HediffComp_GoauldSymbiote sourceComp
                = FindPersistentSymbioteComp(hostState);

            if (hostState == null
                || sourceComp == null
                || !CanExtractFromHost(pawn, sourceComp.SymbioteData))
            {
                GR_Log.Warning(
                    $"{OperationDebugLabel} skipped for {PawnDebugLabel(pawn)} "
                    + "because the required host state is no longer eligible.");

                return;
            }

            if (billDoer != null)
            {
                if (CheckSurgeryFail(billDoer, pawn, ingredients, part, bill))
                {
                    GR_Log.Warning(
                        $"{OperationDebugLabel} failed for "
                        + $"{PawnDebugLabel(pawn)}.");

                    return;
                }

                TaleRecorder.RecordTale(TaleDefOf.DidSurgery, billDoer, pawn);
            }

            Pawn freeSymbiote = PawnGenerator.GeneratePawn(
                GoauldSymbioteUtility.GetFreeSymbiotePawnKind(
                    sourceComp.SymbioteData));

            if (freeSymbiote == null)
            {
                RejectInternalError(
                    pawn,
                    "free symbiote pawn generation failed");

                return;
            }

            Comp_GoauldForcedImplantation freeComp
                = freeSymbiote.GetComp<Comp_GoauldForcedImplantation>();

            if (freeComp == null)
            {
                RejectInternalError(
                    pawn,
                    "generated free symbiote is missing "
                    + "Comp_GoauldForcedImplantation");

                return;
            }

            GoauldSymbioteData transferredData = sourceComp.TakeDataForTransfer();
            string transferredId = transferredData.SymbioteId;

            freeComp.InitializeWithTransferredData(transferredData);

            if (!GenPlace.TryPlaceThing(
                    freeSymbiote,
                    pawn.Position,
                    pawn.Map,
                    ThingPlaceMode.Near))
            {
                sourceComp.CancelTransferOut();

                GR_Log.Error(
                    $"Unable to place symbiote {transferredId} extracted by "
                    + $"{OperationDebugLabel} near patient "
                    + $"{PawnDebugLabel(pawn)}.");

                Messages.Message(
                    NoSpawnCellTranslationKey.Translate(),
                    pawn,
                    MessageTypeDefOf.RejectInput,
                    historical: false);

                return;
            }

            if (SedateExtractedSymbiote)
            {
                ApplyPostExtractionSedation(freeSymbiote);
            }

            sourceComp.ReleaseHostControl(restoreDisplayedSymbioteName: true);
            transferredData.DetachFromHost(pawn, CurrentGameTick());
            pawn.health.RemoveHediff(hostState);

            Find.ColonistBar?.MarkColonistsDirty();
            MainTabWindowUtility.NotifyAllPawnTables_PawnsChanged();

            GR_Log.Message(
                $"{OperationDebugLabel} returned Goa'uld symbiote "
                + $"{transferredId} from patient {PawnDebugLabel(pawn)} "
                + $"with surgeon {PawnDebugLabel(billDoer)}; "
                + $"sedated={SedateExtractedSymbiote}.");

            Messages.Message(
                SuccessTranslationKey.Translate(
                    pawn.LabelShortCap,
                    transferredId),
                pawn,
                MessageTypeDefOf.PositiveEvent,
                historical: true);

            NotifySuccessfulExtraction(pawn, transferredData);
        }

        protected virtual bool CanExtractFromHost(
            Pawn pawn,
            GoauldSymbioteData symbioteData)
        {
            return symbioteData != null;
        }

        protected virtual void NotifySuccessfulExtraction(
            Pawn formerHost,
            GoauldSymbioteData symbioteData)
        {
        }

        private Hediff FindTargetHostState(Pawn pawn)
        {
            if (pawn?.health?.hediffSet?.hediffs == null
                || TargetHediffDef == null)
            {
                return null;
            }

            for (int index = 0;
                index < pawn.health.hediffSet.hediffs.Count;
                index++)
            {
                Hediff hediff = pawn.health.hediffSet.hediffs[index];

                if (hediff.def == TargetHediffDef && hediff.Visible)
                {
                    return hediff;
                }
            }

            return null;
        }

        private static HediffComp_GoauldSymbiote FindPersistentSymbioteComp(
            Hediff hediff)
        {
            HediffWithComps withComps = hediff as HediffWithComps;
            return withComps?.GetComp<HediffComp_GoauldSymbiote>();
        }

        private void RejectInternalError(Pawn pawn, string technicalReason)
        {
            GR_Log.Error(
                $"Unable to complete {OperationDebugLabel}: "
                + $"{technicalReason}.");

            Messages.Message(
                InternalErrorTranslationKey.Translate(),
                pawn,
                MessageTypeDefOf.RejectInput,
                historical: false);
        }

        private static void ApplyPostExtractionSedation(Pawn freeSymbiote)
        {
            if (freeSymbiote?.health == null)
            {
                return;
            }

            Hediff anesthetic = HediffMaker.MakeHediff(
                HediffDefOf.Anesthetic,
                freeSymbiote);
            anesthetic.Severity = 1f;
            freeSymbiote.health.AddHediff(anesthetic);
            freeSymbiote.jobs?.StopAll();
        }

        private static int CurrentGameTick()
        {
            return Find.TickManager?.TicksGame ?? 0;
        }

        private static string PawnDebugLabel(Pawn pawn)
        {
            if (pawn == null)
            {
                return "<null pawn>";
            }

            return $"{pawn.LabelShort} ({pawn.ThingID})";
        }
    }
}
