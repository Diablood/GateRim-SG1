using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public class Comp_TokraObservationPoint : ThingComp
    {
        public override IEnumerable<FloatMenuOption> CompFloatMenuOptions(
            Pawn selPawn)
        {
            foreach (FloatMenuOption option in base.CompFloatMenuOptions(selPawn))
            {
                yield return option;
            }

            if (!parent.Spawned
                || !GameComponent_TokraOrganicOperationManager
                    .IsObservationRecoveryVisible(parent))
            {
                yield break;
            }

            string label = "GR_TokraObservation_RecoverAction"
                .Translate()
                .ToString();
            string disabledReason
                = GameComponent_TokraOrganicOperationManager
                    .GetObservationRecoveryDisabledReason(parent, selPawn);

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
                    GameComponent_TokraOrganicOperationManager
                        .TryStartObservationRecovery(parent, selPawn);
                });
        }
    }
}
