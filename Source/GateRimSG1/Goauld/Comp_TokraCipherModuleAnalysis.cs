using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public class CompProperties_TokraCipherModuleAnalysis
        : CompProperties_CompAnalyzableUnlockResearch
    {
        public CompProperties_TokraCipherModuleAnalysis()
        {
            compClass = typeof(Comp_TokraCipherModuleAnalysis);
            analysisID = TokraCipherModuleStudyUtility.AnalysisId;
        }
    }

    public class Comp_TokraCipherModuleAnalysis
        : CompAnalyzableUnlockResearch
    {
        public new CompProperties_TokraCipherModuleAnalysis Props
            => (CompProperties_TokraCipherModuleAnalysis)props;

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            ApplyLocalizedStrings();
            base.PostSpawnSetup(respawningAfterLoad);
        }

        public override AcceptanceReport CanInteract(
            Pawn activateBy = null,
            bool checkOptionalItems = true)
        {
            ApplyLocalizedStrings();

            if (!GameComponent_TokraIntroductionArc
                .IsTrackedRecoveredArtifact(parent))
            {
                return "GR_TokraCipherStudy_NotRecoveredModule"
                    .Translate();
            }

            if (parent.MapHeld == null || !parent.MapHeld.IsPlayerHome)
            {
                return "GR_TokraCipherStudy_HomeMapRequired"
                    .Translate();
            }

            return base.CanInteract(activateBy, checkOptionalItems);
        }

        public override void OnAnalyzed(Pawn pawn)
        {
            if (!GameComponent_TokraIntroductionArc
                    .IsTrackedRecoveredArtifact(parent)
                || parent.MapHeld == null
                || !parent.MapHeld.IsPlayerHome
                || !TokraCipherModuleStudyUtility.EnsureAnalysisTask()
                || !Find.AnalysisManager.TryIncrementAnalysisProgress(
                    AnalysisID,
                    out AnalysisDetails details)
                || details == null)
            {
                Messages.Message(
                    "GR_TokraCipherStudy_InvalidModule".Translate(),
                    parent,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            if (details.Satisfied)
            {
                Find.LetterStack.ReceiveLetter(
                    "GR_TokraCipherStudy_CompletedLabel".Translate(),
                    "GR_TokraCipherStudy_CompletedText".Translate(
                        pawn.LabelShortCap,
                        GR_DefOf.SG1_TokraSecureCommunications.label),
                    LetterDefOf.PositiveEvent,
                    pawn);

                GameComponent_TokraIntroductionArc
                    .NotifyCipherModuleAnalysisCompleted(parent);

                GR_Log.Message(
                    "Completed analysis of the tracked Tok'ra cipher module "
                    + "and dismantled the physical device.");
                return;
            }

            Find.LetterStack.ReceiveLetter(
                "GR_TokraCipherStudy_ProgressLabel".Translate(
                    details.timesDone,
                    details.required),
                "GR_TokraCipherStudy_ProgressText".Translate(
                    pawn.LabelShortCap,
                    details.timesDone,
                    details.required),
                LetterDefOf.NeutralEvent,
                parent);
        }

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            ApplyLocalizedStrings();

            foreach (Gizmo gizmo in base.CompGetGizmosExtra())
            {
                yield return gizmo;
            }
        }

        public override IEnumerable<FloatMenuOption> CompFloatMenuOptions(
            Pawn selPawn)
        {
            ApplyLocalizedStrings();

            foreach (FloatMenuOption option
                in base.CompFloatMenuOptions(selPawn))
            {
                yield return option;
            }
        }

        public override string CompInspectStringExtra()
        {
            if (!GameComponent_TokraIntroductionArc
                .IsTrackedRecoveredArtifact(parent))
            {
                return "GR_TokraCipherStudy_CopyInspect".Translate();
            }

            if (parent.MapHeld == null || !parent.MapHeld.IsPlayerHome)
            {
                return "GR_TokraCipherStudy_BringHomeInspect".Translate();
            }

            if (TokraCipherModuleStudyUtility.TryGetProgress(
                out int completedSessions,
                out int requiredSessions))
            {
                if (requiredSessions > 0
                    && completedSessions >= requiredSessions)
                {
                    return "GR_TokraCipherStudy_CompletedInspect"
                        .Translate();
                }

                return "GR_TokraCipherStudy_ProgressInspect".Translate(
                    completedSessions,
                    requiredSessions);
            }

            return "GR_TokraCipherStudy_ReadyInspect".Translate();
        }

        private void ApplyLocalizedStrings()
        {
            Props.activateLabelString
                = "GR_TokraCipherStudy_CommandLabel".Translate();
            Props.activateDescString
                = "GR_TokraCipherStudy_CommandDescription".Translate();
            Props.guiLabelString
                = "GR_TokraCipherStudy_TargetingLabel".Translate();
            Props.jobString
                = "GR_TokraCipherStudy_JobString".Translate();
            Props.activatingString
                = "GR_TokraCipherStudy_JobString".Translate();
            Props.activatingStringPending
                = "GR_TokraCipherStudy_JobString".Translate();
        }
    }
}
