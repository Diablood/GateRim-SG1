using RimWorld;
using Verse;
using Verse.AI.Group;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Fast kidnapping-opportunity trigger for the dedicated Goa'uld raid.
    ///
    /// Vanilla kidnapping transitions intentionally wait longer after
    /// combat damage. Goa'uld abduction doctrine instead starts its capture
    /// phase shortly after a nearby victim becomes safely retrievable.
    /// </summary>
    public class Trigger_GoauldJaffaKidnapVictimPresent : Trigger
    {
        private const int CheckIntervalTicks = 30;
        private const float VictimSearchRadius = 8f;

        public override bool ActivateOn(
            Lord lord,
            TriggerSignal signal)
        {
            if (signal.type != TriggerSignalType.Tick
                || Find.TickManager == null
                || Find.TickManager.TicksGame % CheckIntervalTicks != 0)
            {
                return false;
            }

            for (int pawnIndex = 0;
                pawnIndex < lord.ownedPawns.Count;
                pawnIndex++)
            {
                Pawn pawn = lord.ownedPawns[pawnIndex];

                if (pawn == null
                    || !pawn.Spawned
                    || pawn.Downed
                    || pawn.MentalStateDef != null)
                {
                    continue;
                }

                if (KidnapAIUtility.TryFindGoodKidnapVictim(
                    pawn,
                    VictimSearchRadius,
                    out Pawn _))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
