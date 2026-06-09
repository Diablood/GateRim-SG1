using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Manual emergency-extraction prototype for the recent-implantation phase.
    ///
    /// The same GoauldSymbioteData object is moved back into a newly generated
    /// free symbiote pawn. Medical skill checks and surgery bills remain future
    /// work after reverse identity transfer has been validated.
    /// </summary>
    public class HediffComp_GoauldEmergencyExtraction : HediffComp
    {
        public override IEnumerable<Gizmo> CompGetGizmos()
        {
            Pawn host = Pawn;

            if (host == null || !host.Spawned || host.Dead)
            {
                yield break;
            }

            yield return new Command_Action
            {
                defaultLabel = "GR_EmergencyExtraction_CommandLabel".Translate(),
                defaultDesc = "GR_EmergencyExtraction_CommandDescription".Translate(),
                icon = ContentFinder<Texture2D>.Get("UI/Commands/SG1_EmergencyExtraction"),
                action = TryExtractSymbiote
            };
        }

        private void TryExtractSymbiote()
        {
            Pawn host = Pawn;
            if (host == null || !host.Spawned || host.Dead || host.health == null)
            {
                GR_Log.Warning(
                    "Emergency Goa'uld extraction requested from an unavailable host.");

                return;
            }

            HediffComp_GoauldSymbiote sourceComp
                = parent.GetComp<HediffComp_GoauldSymbiote>();

            if (sourceComp == null)
            {
                GR_Log.Error(
                    "Unable to extract recent Goa'uld implantation: "
                    + "persistent symbiote component is missing.");

                Messages.Message(
                    "GR_EmergencyExtraction_InternalError".Translate(),
                    host,
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
                    "Unable to extract recent Goa'uld implantation: "
                    + "free symbiote pawn generation failed.");

                Messages.Message(
                    "GR_EmergencyExtraction_InternalError".Translate(),
                    host,
                    MessageTypeDefOf.RejectInput,
                    historical: false);

                return;
            }

            Comp_GoauldForcedImplantation freeComp
                = freeSymbiote.GetComp<Comp_GoauldForcedImplantation>();

            if (freeComp == null)
            {
                GR_Log.Error(
                    "Unable to extract recent Goa'uld implantation: "
                    + "generated free symbiote is missing "
                    + "Comp_GoauldForcedImplantation.");

                Messages.Message(
                    "GR_EmergencyExtraction_InternalError".Translate(),
                    host,
                    MessageTypeDefOf.RejectInput,
                    historical: false);

                return;
            }

            GoauldSymbioteData transferredData = sourceComp.TakeDataForTransfer();
            string transferredId = transferredData.SymbioteId;

            transferredData.DetachFromHost(host, CurrentGameTick());
            freeComp.InitializeWithTransferredData(transferredData);

            if (!GenPlace.TryPlaceThing(
                    freeSymbiote,
                    host.Position,
                    host.Map,
                    ThingPlaceMode.Near))
            {
                sourceComp.CancelTransferOut();

                GR_Log.Error(
                    $"Unable to place extracted Goa'uld symbiote {transferredId} "
                    + $"near host {PawnDebugLabel(host)}.");

                Messages.Message(
                    "GR_EmergencyExtraction_NoSpawnCell".Translate(),
                    host,
                    MessageTypeDefOf.RejectInput,
                    historical: false);

                return;
            }

            host.health.RemoveHediff(parent);

            GR_Log.Message(
                $"Emergency extraction returned Goa'uld symbiote {transferredId} "
                + $"from host {PawnDebugLabel(host)} to a free pawn.");

            Messages.Message(
                "GR_EmergencyExtraction_Success".Translate(
                    host.LabelShortCap,
                    transferredId),
                host,
                MessageTypeDefOf.PositiveEvent,
                historical: true);
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
