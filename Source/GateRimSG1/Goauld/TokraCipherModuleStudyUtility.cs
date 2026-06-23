using System.Text;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public static class TokraCipherModuleStudyUtility
    {
        public const int AnalysisId = 73203101;
        public const int ReplacementMinimumDelayTicks = 120000;
        public const int ReplacementMaximumDelayTicks = 480000;

        public static bool TryGetProgress(
            out int completedSessions,
            out int requiredSessions)
        {
            completedSessions = 0;
            requiredSessions = GetRequiredSessionCount();

            if (Find.AnalysisManager == null
                || !Find.AnalysisManager.TryGetAnalysisProgress(
                    AnalysisId,
                    out AnalysisDetails details)
                || details == null)
            {
                return false;
            }

            completedSessions = details.timesDone;
            requiredSessions = details.required;
            return true;
        }

        public static bool IsAnalysisComplete
        {
            get
            {
                return TryGetProgress(
                        out int completedSessions,
                        out int requiredSessions)
                    && requiredSessions > 0
                    && completedSessions >= requiredSessions;
            }
        }

        public static bool EnsureAnalysisTask()
        {
            if (Find.AnalysisManager == null)
            {
                return false;
            }

            if (!Find.AnalysisManager.HasAnalysisWithID(AnalysisId))
            {
                Find.AnalysisManager.AddAnalysisTask(
                    AnalysisId,
                    GetRequiredSessionCount());
            }

            return true;
        }

        public static bool DebugFinishAnalysis()
        {
            if (!GameComponent_TokraIntroductionArc
                    .DebugPrepareForCompletedCipherAnalysis()
                || !EnsureAnalysisTask())
            {
                return false;
            }

            Find.AnalysisManager.ForceCompleteAnalysisProgress(AnalysisId);
            GameComponent_TokraIntroductionArc
                .NotifyCipherModuleAnalysisCompleted();
            return true;
        }

        public static bool ForceCompleteForFinishedResearch()
        {
            if (!EnsureAnalysisTask())
            {
                return false;
            }

            Find.AnalysisManager.ForceCompleteAnalysisProgress(AnalysisId);
            return true;
        }

        public static bool DebugResetAnalysis()
        {
            if (Find.AnalysisManager == null
                || GR_DefOf.SG1_TokraSecureCommunications?.IsFinished == true)
            {
                return false;
            }

            Find.AnalysisManager.RemoveAnalysisDetails(AnalysisId);
            Find.AnalysisManager.AddAnalysisTask(
                AnalysisId,
                GetRequiredSessionCount());
            return true;
        }

        public static string GetDebugStateReport()
        {
            StringBuilder builder = new StringBuilder();
            Thing trackedArtifact
                = GameComponent_TokraIntroductionArc
                    .FindTrackedRecoveredArtifact();

            builder.AppendLine("Tok'ra cipher-module study");
            builder.AppendLine(
                "Introduction completed: "
                + GameComponent_TokraIntroductionArc
                    .IsCompletedPermanently);
            builder.AppendLine(
                "Tracked artifact ID: "
                + (GameComponent_TokraIntroductionArc
                    .TrackedArtifactThingId ?? "none"));
            builder.AppendLine(
                "Tracked artifact available: "
                + (trackedArtifact != null));
            builder.AppendLine(
                "Tracked artifact location: "
                + DescribeLocation(trackedArtifact));
            builder.AppendLine(
                "Replacement due: "
                + GameComponent_TokraIntroductionArc
                    .GetCipherModuleReplacementDebugText());
            builder.AppendLine(
                "Replacement modules issued: "
                + GameComponent_TokraIntroductionArc
                    .CipherModuleReplacementCount);

            if (TryGetProgress(
                out int completedSessions,
                out int requiredSessions))
            {
                builder.AppendLine(
                    "Analysis progress: "
                    + completedSessions
                    + "/"
                    + requiredSessions);
            }
            else
            {
                builder.AppendLine("Analysis progress: not initialized");
            }

            ResearchProjectDef project
                = GR_DefOf.SG1_TokraSecureCommunications;
            builder.AppendLine(
                "Electricity completed: "
                + (DefDatabase<ResearchProjectDef>
                    .GetNamedSilentFail("Electricity")?.IsFinished == true));
            builder.AppendLine(
                "Secure communications available: "
                + (project?.CanStartNow == true));
            builder.AppendLine(
                "Secure communications completed: "
                + (project?.IsFinished == true));
            return builder.ToString().TrimEndNewlines();
        }

        private static int GetRequiredSessionCount()
        {
            CompProperties_TokraCipherModuleAnalysis properties
                = GR_DefOf.SG1_TokraIntroductionArtifact
                    ?.GetCompProperties<
                        CompProperties_TokraCipherModuleAnalysis>();

            if (properties == null)
            {
                return 3;
            }

            return System.Math.Max(
                1,
                properties.analysisRequiredRange.min);
        }

        private static string DescribeLocation(Thing artifact)
        {
            if (artifact == null)
            {
                return "missing";
            }

            if (artifact.Spawned)
            {
                return artifact.MapHeld?.IsPlayerHome == true
                    ? "player home map"
                    : "non-home map";
            }

            if (artifact.ParentHolder is Pawn pawn)
            {
                return "pawn inventory: " + pawn.LabelShortCap;
            }

            return artifact.ParentHolder?.GetType().Name ?? "unspawned";
        }
    }
}
