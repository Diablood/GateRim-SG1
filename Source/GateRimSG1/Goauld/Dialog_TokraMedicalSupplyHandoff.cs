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
            int requiredCount
                = GameComponent_TokraOrganicOperationManager
                    .GetMedicalSupplyRequiredCount();
            string titleKey
                = GameComponent_TokraOrganicOperationManager
                    .GetMedicalSupplyRuntimeTextKey("dialogTitle");
            string bodyKey
                = GameComponent_TokraOrganicOperationManager
                    .GetMedicalSupplyRuntimeTextKey("dialogText");
            string giveKey
                = GameComponent_TokraOrganicOperationManager
                    .GetMedicalSupplyRuntimeTextKey("giveMedicine");
            string cancelKey
                = GameComponent_TokraOrganicOperationManager
                    .GetMedicalSupplyRuntimeTextKey("cancelDialogue");

            Text.Font = GameFont.Medium;
            Widgets.Label(
                new Rect(0f, 0f, inRect.width, 35f),
                titleKey.Translate(
                    liaison?.LabelShortCap ?? "?"));

            Text.Font = GameFont.Small;
            Rect bodyRect = new Rect(
                0f,
                45f,
                inRect.width,
                inRect.height - 120f);
            Widgets.Label(
                bodyRect,
                bodyKey.Translate(
                    liaison?.LabelShortCap ?? "?",
                    requiredCount.ToString()));

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
                    giveKey.Translate(requiredCount.ToString())))
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
                    cancelKey.Translate()))
            {
                Close();
            }
        }
    }
}
