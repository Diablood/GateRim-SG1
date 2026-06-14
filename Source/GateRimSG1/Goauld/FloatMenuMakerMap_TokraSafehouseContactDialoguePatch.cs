using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Provides the vanilla colonist-right-click interaction for the peaceful
    /// Tok'ra safehouse contact without requiring Harmony.
    ///
    /// The comp is attached to human pawns by XML patch, then filters itself
    /// down to the single generated Tok'ra safehouse contact through the
    /// internal dialogue hediff.
    /// </summary>
    public class CompProperties_TokraSafehouseContactDialogueFloatMenu
        : CompProperties
    {
        public CompProperties_TokraSafehouseContactDialogueFloatMenu()
        {
            compClass = typeof(Comp_TokraSafehouseContactDialogueFloatMenu);
        }
    }

    public class Comp_TokraSafehouseContactDialogueFloatMenu : ThingComp
    {
        public override IEnumerable<FloatMenuOption> CompFloatMenuOptions(
            Pawn selPawn)
        {
            foreach (FloatMenuOption option in base.CompFloatMenuOptions(selPawn))
            {
                yield return option;
            }

            Pawn contact = parent as Pawn;

            if (!TokraSafehouseContactDialogueUtility
                    .IsValidSafehouseContact(contact))
            {
                yield break;
            }

            string label = "GR_TokraSafehouseContactDialogue_FloatMenuLabel"
                .Translate(contact.LabelShortCap);

            if (!CanUsePlayerNegotiator(selPawn))
            {
                yield return new FloatMenuOption(
                    label + ": "
                    + "GR_TokraSafehouseContactDialogue_PlayerPawnRequired"
                        .Translate(),
                    null);
                yield break;
            }

            if (TokraSafehouseContactDialogueUtility.HasAcknowledged(contact))
            {
                yield return new FloatMenuOption(
                    label + ": "
                    + "GR_TokraSafehouseContactDialogue_AlreadyAcknowledged"
                        .Translate(),
                    null);
                yield break;
            }

            if (!selPawn.CanReach(contact, PathEndMode.Touch, Danger.Deadly))
            {
                yield return new FloatMenuOption(
                    label + ": "
                    + "GR_TokraSafehouseContactDialogue_CannotReach"
                        .Translate(),
                    null);
                yield break;
            }

            JobDef jobDef = TokraSafehouseContactDialogueUtility
                .GetDialogueJobDef();

            if (jobDef == null)
            {
                yield return new FloatMenuOption(
                    label + ": "
                    + "GR_TokraSafehouseContactDialogue_JobUnavailable"
                        .Translate(),
                    null);
                yield break;
            }

            yield return new FloatMenuOption(
                label,
                delegate
                {
                    Job job = JobMaker.MakeJob(jobDef, contact);
                    selPawn.jobs.TryTakeOrderedJob(job);
                });
        }

        private static bool CanUsePlayerNegotiator(Pawn pawn)
        {
            return pawn != null
                && !pawn.Dead
                && !pawn.Downed
                && pawn.Faction == Faction.OfPlayer
                && pawn.RaceProps?.Humanlike == true;
        }
    }
}
