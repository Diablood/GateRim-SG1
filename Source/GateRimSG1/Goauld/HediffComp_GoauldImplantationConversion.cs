using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Converts recent Goa'uld implantation into an active host immediately
    /// before the temporary state expires.
    ///
    /// The same GoauldSymbioteData object is moved into the permanent host
    /// state. The recent state is then allowed to disappear normally.
    /// </summary>
    public class HediffComp_GoauldImplantationConversion
        : HediffComp_Disappears
    {
        private const int ConversionFailureErrorKey = 1180001;

        private bool conversionCompleted;

        public override void CompPostTick(ref float severityAdjustment)
        {
            if (!conversionCompleted && ticksToDisappear <= TicksLostPerTick)
            {
                if (!TryConvertToActiveHost())
                {
                    GR_Log.ErrorOnce(
                        "Unable to convert recent Goa'uld implantation into "
                        + "an active host state. The temporary state will remain "
                        + "until the conversion issue is corrected.",
                        ConversionFailureErrorKey);

                    return;
                }

                conversionCompleted = true;
            }

            base.CompPostTick(ref severityAdjustment);
        }

        public override void CompExposeData()
        {
            base.CompExposeData();
            Scribe_Values.Look(ref conversionCompleted, "conversionCompleted", false);
        }

        public override string CompDebugString()
        {
            return base.CompDebugString()
                + $", conversionCompleted: {conversionCompleted}";
        }

        private bool TryConvertToActiveHost()
        {
            Pawn host = Pawn;
            if (host == null || host.health == null || host.Dead)
            {
                GR_Log.Error("Cannot convert Goa'uld implantation: host pawn is unavailable.");
                return false;
            }

            if (HasHediff(host, GR_DefOf.SG1_GoauldHostSymbiote))
            {
                GR_Log.Warning(
                    $"Skipped duplicate Goa'uld host conversion for "
                    + $"{PawnDebugLabel(host)} because the active state already exists.");

                return true;
            }

            HediffComp_GoauldSymbiote sourceComp
                = parent.GetComp<HediffComp_GoauldSymbiote>();

            if (sourceComp == null)
            {
                GR_Log.Error(
                    "Cannot convert Goa'uld implantation: "
                    + "recent state is missing HediffComp_GoauldSymbiote.");

                return false;
            }

            Hediff activeHostState = HediffMaker.MakeHediff(
                GR_DefOf.SG1_GoauldHostSymbiote,
                host);

            HediffComp_GoauldSymbiote targetComp
                = FindPersistentSymbioteComp(activeHostState);

            if (targetComp == null)
            {
                GR_Log.Error(
                    "Cannot convert Goa'uld implantation: "
                    + "SG1_GoauldHostSymbiote is missing "
                    + "HediffComp_GoauldSymbiote.");

                return false;
            }

            GoauldSymbioteData transferredData = sourceComp.TakeDataForTransfer();
            string transferredId = transferredData.SymbioteId;

            targetComp.InitializeWithTransferredData(transferredData);
            host.health.AddHediff(activeHostState);

            GR_Log.Message(
                $"Converted recent Goa'uld implantation {transferredId} "
                + $"into active host state on {PawnDebugLabel(host)}.");

            Messages.Message(
                "GR_GoauldHostConversion_Success".Translate(
                    host.LabelShortCap,
                    transferredId),
                host,
                MessageTypeDefOf.NegativeEvent,
                historical: true);

            return true;
        }

        private static bool HasHediff(Pawn pawn, HediffDef def)
        {
            for (int index = 0; index < pawn.health.hediffSet.hediffs.Count; index++)
            {
                if (pawn.health.hediffSet.hediffs[index].def == def)
                {
                    return true;
                }
            }

            return false;
        }

        private static HediffComp_GoauldSymbiote FindPersistentSymbioteComp(
            Hediff hediff)
        {
            HediffWithComps withComps = hediff as HediffWithComps;
            return withComps?.GetComp<HediffComp_GoauldSymbiote>();
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
