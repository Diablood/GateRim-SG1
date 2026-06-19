using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace GateRimSG1.Goauld
{
    public class Comp_TokraOrganicDeadDrop : ThingComp
    {
        private const string SecureDeadDropJobDefName =
            "SG1_SecureTokraOrganicDeadDrop";

        public override IEnumerable<FloatMenuOption> CompFloatMenuOptions(
            Pawn selPawn)
        {
            foreach (FloatMenuOption option in base.CompFloatMenuOptions(selPawn))
            {
                yield return option;
            }

            if (!parent.Spawned)
            {
                yield break;
            }

            string label = "GR_TokraOrganicOperation_FloatMenuSecureDeadDrop"
                .Translate()
                .ToString();
            string disabledReason = GetDisabledReason(selPawn);

            if (!string.IsNullOrEmpty(disabledReason))
            {
                yield return new FloatMenuOption(
                    label + ": " + disabledReason,
                    null);
                yield break;
            }

            yield return new FloatMenuOption(
                label,
                delegate
                {
                    JobDef jobDef = DefDatabase<JobDef>.GetNamedSilentFail(
                        SecureDeadDropJobDefName);

                    if (jobDef == null)
                    {
                        Messages.Message(
                            "GR_TokraOrganicOperation_DeadDropJobUnavailable"
                                .Translate(),
                            parent,
                            MessageTypeDefOf.RejectInput,
                            historical: false);
                        return;
                    }

                    Job job = JobMaker.MakeJob(jobDef, parent);
                    selPawn.jobs.TryTakeOrderedJob(job);
                });
        }

        internal bool TrySecure(Pawn operatorPawn)
        {
            if (!CanUsePlayerOperator(operatorPawn))
            {
                return false;
            }

            SkillRecord intellectual = operatorPawn.skills?.GetSkill(
                SkillDefOf.Intellectual);

            if (intellectual == null || intellectual.TotallyDisabled)
            {
                return false;
            }

            return GameComponent_TokraOrganicOperationManager.TrySecureDeadDrop(
                parent,
                operatorPawn);
        }

        internal bool IsOperationActive()
        {
            return GameComponent_TokraOrganicOperationManager
                .IsActiveDeadDrop(parent);
        }

        private string GetDisabledReason(Pawn pawn)
        {
            string trackerReason
                = GameComponent_TokraOrganicOperationManager
                    .GetDeadDropDisabledReason(parent);

            if (!string.IsNullOrEmpty(trackerReason))
            {
                return trackerReason;
            }

            if (!CanUsePlayerOperator(pawn))
            {
                return "GR_TokraSecureCommunicator_PlayerPawnRequired"
                    .Translate()
                    .ToString();
            }

            SkillRecord intellectual = pawn.skills?.GetSkill(
                SkillDefOf.Intellectual);

            if (intellectual == null || intellectual.TotallyDisabled)
            {
                return "GR_TokraOrganicOperation_OperatorIncapable"
                    .Translate()
                    .ToString();
            }

            if (!pawn.CanReach(parent, PathEndMode.Touch, Danger.Some))
            {
                return "GR_TokraOrganicOperation_DeadDropCannotReach"
                    .Translate()
                    .ToString();
            }

            if (!pawn.CanReserve(parent))
            {
                return "GR_TokraOrganicOperation_DeadDropReserved"
                    .Translate()
                    .ToString();
            }

            if (DefDatabase<JobDef>.GetNamedSilentFail(
                SecureDeadDropJobDefName) == null)
            {
                return "GR_TokraOrganicOperation_DeadDropJobUnavailable"
                    .Translate()
                    .ToString();
            }

            return null;
        }

        private static bool CanUsePlayerOperator(Pawn pawn)
        {
            return pawn != null
                && pawn.Faction == Faction.OfPlayer
                && pawn.RaceProps != null
                && pawn.RaceProps.Humanlike
                && !pawn.Dead
                && !pawn.Downed
                && pawn.jobs != null;
        }
    }
}
