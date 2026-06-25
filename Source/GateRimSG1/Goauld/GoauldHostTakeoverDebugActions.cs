using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public static class GoauldHostTakeoverDebugActions
    {
        public static void InspectHostControl(Pawn pawn)
        {
            HediffComp_GoauldSymbiote symbioteComp
                = FindSymbioteComp(pawn, out Hediff hostState);

            if (symbioteComp == null)
            {
                Reject("The selected pawn has no Goa'uld-family host state.");
                return;
            }

            GoauldSymbioteData data = symbioteComp.SymbioteData;
            string stateLabel = hostState?.def?.defName ?? "<missing>";
            string allegiance = data?.AllegianceFaction?.Name ?? "<none>";
            string displaced = data?.DisplacedHostFaction?.Name ?? "<none>";
            string control = data == null
                ? "<missing>"
                : data.HostControlState.ToString();

            Messages.Message(
                $"Goa'uld host control: pawn={pawn.LabelShort}; "
                + $"state={stateLabel}; allegiance={allegiance}; "
                + $"displaced={displaced}; control={control}.",
                pawn,
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        public static void ForceRecentConversion(Pawn pawn)
        {
            Hediff recent = FindHostState(
                pawn,
                GR_DefOf.SG1_GoauldRecentImplantation);
            HediffComp_GoauldImplantationConversion conversion
                = (recent as HediffWithComps)
                    ?.GetComp<HediffComp_GoauldImplantationConversion>();

            if (conversion == null)
            {
                Reject("The selected pawn has no recent Goa'uld implantation to convert.");
                return;
            }

            if (!conversion.TryForceConversionNow())
            {
                Reject("The recent Goa'uld implantation could not be converted.");
            }
        }

        public static void RestoreDisplacedFaction(Pawn pawn)
        {
            HediffComp_GoauldSymbiote symbioteComp
                = FindSymbioteComp(pawn, out Hediff unusedState);

            if (symbioteComp == null)
            {
                Reject("The selected pawn has no Goa'uld-family host state.");
                return;
            }

            GoauldHostControlState previousState
                = symbioteComp.SymbioteData?.HostControlState
                    ?? GoauldHostControlState.None;
            bool restored = symbioteComp.ReleaseHostControl();

            Messages.Message(
                restored
                    ? $"Restored {pawn.LabelShort} to the displaced faction."
                    : previousState == GoauldHostControlState.Pending
                        ? $"Cleared the pending Goa'uld takeover for {pawn.LabelShort}."
                        : $"No displaced faction was restored for {pawn.LabelShort}.",
                pawn,
                restored
                    ? MessageTypeDefOf.PositiveEvent
                    : MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        private static HediffComp_GoauldSymbiote FindSymbioteComp(
            Pawn pawn,
            out Hediff hostState)
        {
            hostState = FindHostState(
                pawn,
                GR_DefOf.SG1_GoauldRecentImplantation)
                ?? FindHostState(
                    pawn,
                    GR_DefOf.SG1_GoauldHostSymbiote);

            return (hostState as HediffWithComps)
                ?.GetComp<HediffComp_GoauldSymbiote>();
        }

        private static Hediff FindHostState(Pawn pawn, HediffDef def)
        {
            if (pawn?.health?.hediffSet?.hediffs == null || def == null)
            {
                return null;
            }

            for (int index = 0;
                index < pawn.health.hediffSet.hediffs.Count;
                index++)
            {
                Hediff hediff = pawn.health.hediffSet.hediffs[index];

                if (hediff.def == def)
                {
                    return hediff;
                }
            }

            return null;
        }

        private static void Reject(string text)
        {
            Messages.Message(
                text,
                MessageTypeDefOf.RejectInput,
                historical: false);
        }
    }
}
