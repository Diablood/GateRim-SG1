using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace GateRimSG1.Goauld
{
    public class Comp_TokraMissionIntelPacket : ThingComp
    {
        private const int IntellectualExperience = 500;
        private const string AnalyzeIntelJobDefName =
            "SG1_AnalyzeTokraMissionIntelPacket";

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

            string label = "GR_TokraMissionIntelPacket_FloatMenuAnalyzeLabel"
                .Translate()
                .ToString();
            string disabledReason = GetAnalyzeDisabledReason(selPawn);
            JobDef jobDef = DefDatabase<JobDef>.GetNamedSilentFail(
                AnalyzeIntelJobDefName);

            if (jobDef == null && string.IsNullOrEmpty(disabledReason))
            {
                disabledReason = "GR_TokraMissionIntelPacket_JobUnavailable"
                    .Translate()
                    .ToString();
            }

            if (!string.IsNullOrEmpty(disabledReason))
            {
                yield return new FloatMenuOption(label + ": " + disabledReason, null);
                yield break;
            }

            yield return new FloatMenuOption(
                label,
                delegate
                {
                    Job job = JobMaker.MakeJob(jobDef, parent);
                    selPawn.jobs.TryTakeOrderedJob(job);
                });
        }

        internal string GetAnalyzeDisabledReason(Pawn pawn)
        {
            if (!CanUsePlayerOperator(pawn))
            {
                return "GR_TokraMissionIntelPacket_PlayerPawnRequired"
                    .Translate()
                    .ToString();
            }

            if (GameComponent_TokraTrustTracker
                .IsFirstTrustMissionIntelAnalyzed())
            {
                return "GR_TokraMissionIntelPacket_AlreadyAnalyzed"
                    .Translate()
                    .ToString();
            }

            if (!CanLearnIntellectual(pawn))
            {
                return "GR_TokraMissionIntelPacket_IntellectualRequired"
                    .Translate()
                    .ToString();
            }

            if (!pawn.CanReserveAndReach(parent, PathEndMode.Touch, Danger.Some))
            {
                return "GR_TokraMissionIntelPacket_CannotReach"
                    .Translate()
                    .ToString();
            }

            if (!pawn.CanReserve(parent))
            {
                return "GR_TokraMissionIntelPacket_Reserved"
                    .Translate()
                    .ToString();
            }

            return null;
        }

        internal bool TryAnalyzeIntelligence(Pawn pawn)
        {
            string disabledReason = GetAnalyzeDisabledReason(pawn);

            if (!string.IsNullOrEmpty(disabledReason))
            {
                Messages.Message(
                    disabledReason,
                    parent,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            SkillRecord intellectual = pawn.skills.GetSkill(SkillDefOf.Intellectual);
            intellectual.Learn(IntellectualExperience, true);

            if (!GameComponent_TokraTrustTracker
                    .NotifyFirstTrustMissionIntelAnalyzed(pawn))
            {
                Messages.Message(
                    "GR_TokraMissionIntelPacket_AnalysisFailed".Translate(),
                    parent,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            Messages.Message(
                "GR_TokraMissionIntelPacket_Analyzed".Translate(
                    pawn.LabelShortCap,
                    IntellectualExperience.ToString()),
                parent,
                MessageTypeDefOf.PositiveEvent,
                historical: true);

            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    "GR_TokraMissionIntelPacket_AnalysisDialogText"
                        .Translate(pawn.LabelShortCap)
                        .ToString()));

            GR_Log.Message(
                $"Tok'ra coded intelligence packet analyzed by "
                + $"{pawn.LabelShortCap}; +{IntellectualExperience} "
                + "Intellectual XP.");

            return true;
        }

        private static bool CanUsePlayerOperator(Pawn pawn)
        {
            return pawn != null
                && pawn.Spawned
                && !pawn.Dead
                && !pawn.Downed
                && pawn.Faction == Faction.OfPlayer
                && pawn.RaceProps != null
                && pawn.RaceProps.Humanlike
                && pawn.jobs != null;
        }

        private static bool CanLearnIntellectual(Pawn pawn)
        {
            if (pawn?.skills == null)
            {
                return false;
            }

            SkillRecord intellectual = pawn.skills.GetSkill(SkillDefOf.Intellectual);
            return intellectual != null && !intellectual.TotallyDisabled;
        }
    }
}
