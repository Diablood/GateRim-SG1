using UnityEngine;
using Verse;

namespace GateRimSG1.Goauld
{
    internal class Dialog_TokraMedicalSupplyHandoff : Window
    {
        private readonly Pawn liaison;
        private readonly Pawn negotiator;

        public Dialog_TokraMedicalSupplyHandoff(
            Pawn liaison,
            Pawn negotiator)
        {
            this.liaison = liaison;
            this.negotiator = negotiator;
            forcePause = true;
            absorbInputAroundWindow = true;
            closeOnClickedOutside = false;
            doCloseX = false;
        }

        public override Vector2 InitialSize => new Vector2(640f, 360f);

        public override void DoWindowContents(Rect inRect)
        {
            Text.Font = GameFont.Medium;
            Widgets.Label(
                new Rect(0f, 0f, inRect.width, 35f),
                "GR_TokraMedicalSupply_DialogTitle".Translate(
                    liaison?.LabelShortCap ?? "?"));

            Text.Font = GameFont.Small;
            Rect bodyRect = new Rect(
                0f,
                45f,
                inRect.width,
                inRect.height - 120f);
            Widgets.Label(
                bodyRect,
                "GR_TokraMedicalSupply_DialogText".Translate(
                    liaison?.LabelShortCap ?? "?",
                    TokraOrganicMedicalSupplyUtility
                        .RequiredMedicineCount.ToString()));

            float buttonWidth = (inRect.width - 12f) / 2f;
            Rect giveButtonRect = new Rect(
                0f,
                inRect.height - 55f,
                buttonWidth,
                45f);
            Rect cancelButtonRect = new Rect(
                buttonWidth + 12f,
                inRect.height - 55f,
                buttonWidth,
                45f);

            if (Widgets.ButtonText(
                    giveButtonRect,
                    "GR_TokraMedicalSupply_GiveMedicine".Translate()))
            {
                if (GameComponent_TokraOrganicOperationManager
                    .TryCompleteMedicalSupplyHandoff(
                        liaison,
                        negotiator))
                {
                    Close();
                }
            }

            if (Widgets.ButtonText(
                    cancelButtonRect,
                    "GR_TokraMedicalSupply_CancelDialogue".Translate()))
            {
                Close();
            }
        }
    }
}
