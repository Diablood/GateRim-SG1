using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    internal static class TokraSafehouseContactDialogueUtility
    {
        private const string DialogueHediffDefName
            = "SG1_TokraSafehouseContactDialogue";
        internal const string DialogueJobDefName
            = "SG1_TokraSafehouseContactDialogue";

        internal static bool IsValidSafehouseContact(Pawn pawn)
        {
            return pawn != null
                && !pawn.Dead
                && pawn.Spawned
                && pawn.kindDef == GR_DefOf.SG1_TokraVoluntaryHost
                && pawn.Faction?.def == GR_DefOf.SG1_Tokra
                && pawn.Map?.Parent?.def == GR_DefOf.SG1_TokraHiddenSafehouseSite
                && GetDialogueComp(pawn) != null;
        }

        internal static bool HasAcknowledged(Pawn pawn)
        {
            return GetDialogueComp(pawn)?.ContactAcknowledged ?? false;
        }

        internal static bool TryAcknowledge(Pawn contact, Pawn negotiator)
        {
            return GetDialogueComp(contact)?.TryAcknowledgeContact(negotiator)
                ?? false;
        }

        internal static JobDef GetDialogueJobDef()
        {
            return DefDatabase<JobDef>.GetNamedSilentFail(DialogueJobDefName);
        }

        private static HediffComp_TokraSafehouseContactDialogue GetDialogueComp(
            Pawn pawn)
        {
            HediffDef dialogueHediffDef = DefDatabase<HediffDef>
                .GetNamedSilentFail(DialogueHediffDefName);

            if (pawn?.health?.hediffSet == null || dialogueHediffDef == null)
            {
                return null;
            }

            Hediff hediff = pawn.health.hediffSet
                .GetFirstHediffOfDef(dialogueHediffDef);

            return hediff?.TryGetComp<HediffComp_TokraSafehouseContactDialogue>();
        }
    }
}
