using UnityEngine;
using Verse;

namespace GateRimSG1.Goauld
{
    internal class Dialog_TokraIntelligenceAnalysis : Window
    {
        private readonly Thing communicator;
        private readonly Pawn operatorPawn;

        public Dialog_TokraIntelligenceAnalysis(
            Thing communicator,
            Pawn operatorPawn)
        {
            this.communicator = communicator;
            this.operatorPawn = operatorPawn;
            forcePause = true;
            absorbInputAroundWindow = true;
            closeOnClickedOutside = false;
            doCloseX = false;
        }

        public override Vector2 InitialSize => new Vector2(720f, 430f);

        public override void DoWindowContents(Rect inRect)
        {
            Text.Font = GameFont.Medium;
            Widgets.Label(
                new Rect(0f, 0f, inRect.width, 35f),
                "GR_TokraOrganicOperation_IntelligenceDialogTitle"
                    .Translate());

            Text.Font = GameFont.Small;
            Widgets.Label(
                new Rect(0f, 45f, inRect.width, inRect.height - 145f),
                "GR_TokraOrganicOperation_IntelligenceDialogText"
                    .Translate(operatorPawn?.LabelShortCap ?? "?"));

            float spacing = 10f;
            float buttonWidth = (inRect.width - (spacing * 2f)) / 3f;
            float buttonY = inRect.height - 60f;
            Rect cautiousRect = new Rect(0f, buttonY, buttonWidth, 50f);
            Rect acceleratedRect = new Rect(
                buttonWidth + spacing,
                buttonY,
                buttonWidth,
                50f);
            Rect cancelRect = new Rect(
                (buttonWidth + spacing) * 2f,
                buttonY,
                buttonWidth,
                50f);

            if (Widgets.ButtonText(
                    cautiousRect,
                    "GR_TokraOrganicOperation_IntelligenceCautiousButton"
                        .Translate()))
            {
                if (GameComponent_TokraOrganicOperationManager
                    .TryStartIntelligenceAnalysis(
                        communicator,
                        operatorPawn,
                        TokraIntelligenceAnalysisMethod.Cautious))
                {
                    Close();
                }
            }

            if (Widgets.ButtonText(
                    acceleratedRect,
                    "GR_TokraOrganicOperation_IntelligenceAcceleratedButton"
                        .Translate()))
            {
                if (GameComponent_TokraOrganicOperationManager
                    .TryStartIntelligenceAnalysis(
                        communicator,
                        operatorPawn,
                        TokraIntelligenceAnalysisMethod.Accelerated))
                {
                    Close();
                }
            }

            if (Widgets.ButtonText(
                    cancelRect,
                    "GR_TokraOrganicOperation_IntelligenceCancelButton"
                        .Translate()))
            {
                Close();
            }
        }
    }
}
