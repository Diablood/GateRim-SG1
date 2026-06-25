using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Medical surgery that extracts a recently implanted adult Goa'uld
    /// symbiote and returns the same persistent identity to a free pawn.
    ///
    /// The vanilla Recipe_Surgery failure path remains active through
    /// CheckSurgeryFail, allowing medicine quality, doctor skill, bed quality
    /// and configured surgery factors to influence the result.
    /// </summary>
    public class Recipe_EmergencyExtractGoauldSymbiote : Recipe_Surgery
    {
        public override bool AvailableOnNow(Thing thing, BodyPartRecord part = null)
        {
            if (!base.AvailableOnNow(thing, part))
            {
                return false;
            }

            Pawn pawn = thing as Pawn;
            return pawn != null
                && FindRecentImplantation(pawn) != null;
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
                    "Emergency extraction surgery received an unavailable patient.");

                return;
            }

            Hediff recentImplantation = FindRecentImplantation(pawn);
            if (recentImplantation == null)
            {
                GR_Log.Warning(
                    $"Emergency extraction surgery skipped for {PawnDebugLabel(pawn)} "
                    + "because recent implantation is no longer present.");

                return;
            }

            if (billDoer != null)
            {
                if (CheckSurgeryFail(billDoer, pawn, ingredients, part, bill))
                {
                    GR_Log.Warning(
                        $"Emergency extraction surgery failed for "
                        + $"{PawnDebugLabel(pawn)}.");

                    return;
                }

                TaleRecorder.RecordTale(TaleDefOf.DidSurgery, billDoer, pawn);
            }

            HediffComp_GoauldSymbiote sourceComp
                = FindPersistentSymbioteComp(recentImplantation);

            if (sourceComp == null)
            {
                GR_Log.Error(
                    "Unable to complete emergency extraction surgery: "
                    + "recent implantation is missing persistent symbiote data.");

                Messages.Message(
                    "GR_EmergencyExtractionSurgery_InternalError".Translate(),
                    pawn,
                    MessageTypeDefOf.RejectInput,
                    historical: false);

                return;
            }

            Pawn freeSymbiote = PawnGenerator.GeneratePawn(
                GoauldSymbioteUtility.GetFreeSymbiotePawnKind(
                    sourceComp.SymbioteData));

            if (freeSymbiote == null)
            {
                GR_Log.Error(
                    "Unable to complete emergency extraction surgery: "
                    + "free symbiote pawn generation failed.");

                Messages.Message(
                    "GR_EmergencyExtractionSurgery_InternalError".Translate(),
                    pawn,
                    MessageTypeDefOf.RejectInput,
                    historical: false);

                return;
            }

            Comp_GoauldForcedImplantation freeComp
                = freeSymbiote.GetComp<Comp_GoauldForcedImplantation>();

            if (freeComp == null)
            {
                GR_Log.Error(
                    "Unable to complete emergency extraction surgery: "
                    + "generated free symbiote is missing "
                    + "Comp_GoauldForcedImplantation.");

                Messages.Message(
                    "GR_EmergencyExtractionSurgery_InternalError".Translate(),
                    pawn,
                    MessageTypeDefOf.RejectInput,
                    historical: false);

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
                    $"Unable to place surgically extracted Goa'uld symbiote "
                    + $"{transferredId} near patient {PawnDebugLabel(pawn)}.");

                Messages.Message(
                    "GR_EmergencyExtractionSurgery_NoSpawnCell".Translate(),
                    pawn,
                    MessageTypeDefOf.RejectInput,
                    historical: false);

                return;
            }

            sourceComp.ReleaseHostControl();
            transferredData.DetachFromHost(pawn, CurrentGameTick());
            pawn.health.RemoveHediff(recentImplantation);

            GR_Log.Message(
                $"Emergency extraction surgery returned Goa'uld symbiote "
                + $"{transferredId} from patient {PawnDebugLabel(pawn)} "
                + $"with surgeon {PawnDebugLabel(billDoer)}.");

            Messages.Message(
                "GR_EmergencyExtractionSurgery_Success".Translate(
                    pawn.LabelShortCap,
                    transferredId),
                pawn,
                MessageTypeDefOf.PositiveEvent,
                historical: true);
        }

        private static HediffComp_GoauldSymbiote FindPersistentSymbioteComp(
            Hediff hediff)
        {
            HediffWithComps withComps = hediff as HediffWithComps;
            return withComps?.GetComp<HediffComp_GoauldSymbiote>();
        }

        private static Hediff FindRecentImplantation(Pawn pawn)
        {
            if (pawn?.health?.hediffSet?.hediffs == null)
            {
                return null;
            }

            for (int index = 0; index < pawn.health.hediffSet.hediffs.Count; index++)
            {
                Hediff hediff = pawn.health.hediffSet.hediffs[index];

                if (hediff.def == GR_DefOf.SG1_GoauldRecentImplantation
                    && hediff.Visible)
                {
                    return hediff;
                }
            }

            return null;
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
