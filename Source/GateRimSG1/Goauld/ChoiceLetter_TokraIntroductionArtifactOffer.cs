using System.Collections.Generic;
using Verse;

namespace GateRimSG1.Goauld
{
    public sealed class ChoiceLetter_TokraIntroductionArtifactOffer
        : ChoiceLetter
    {
        private int attemptNumber;

        public int AttemptNumber => attemptNumber;

        public override bool CanDismissWithRightClick => false;

        public override IEnumerable<DiaOption> Choices
        {
            get
            {
                if (!GameComponent_TokraIntroductionArc
                    .IsCurrentOfferLetter(attemptNumber))
                {
                    yield return Option_Close;
                    yield break;
                }

                DiaOption accept = new DiaOption(
                    "GR_TokraIntroduction_AcceptAction".Translate());
                accept.action = delegate
                {
                    GameComponent_TokraIntroductionArc
                        .AcceptOfferFromLetter(attemptNumber);
                };
                accept.resolveTree = true;
                yield return accept;

                DiaOption decline = new DiaOption(
                    "GR_TokraIntroduction_DeclineAction".Translate());
                decline.action = delegate
                {
                    GameComponent_TokraIntroductionArc
                        .DeclineOfferFromLetter(attemptNumber);
                };
                decline.resolveTree = true;
                yield return decline;
            }
        }

        public void Initialize(int currentAttemptNumber)
        {
            attemptNumber = currentAttemptNumber;
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(
                ref attemptNumber,
                "tokraIntroductionAttemptNumber",
                0);
        }
    }
}
