using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;
using Verse.AI.Group;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Opportunistic post-assault recovery phase for Goa'uld Jaffa.
    ///
    /// During cover mode, available pawns prioritize downed colonists, then
    /// valuable nearby items. Other Jaffa continue AssaultColony duties.
    /// During extraction mode, all pawns leave the map with whatever they
    /// are already carrying.
    /// </summary>
    public class LordToil_GoauldJaffaRecoveryCover : LordToil
    {
        private const int ReassignmentIntervalTicks = 181;
        private const float VictimSearchRadius = 8f;
        private const float ItemSearchRadius = 7f;

        public bool cover = true;

        public override bool AllowSatisfyLongNeeds
        {
            get
            {
                return false;
            }
        }

        public override bool ForceHighStoryDanger
        {
            get
            {
                return cover;
            }
        }

        public override bool AllowSelfTend
        {
            get
            {
                return false;
            }
        }

        public override void UpdateAllDuties()
        {
            AssignDuties();
        }

        public override void LordToilTick()
        {
            if (!cover
                || Find.TickManager == null
                || Find.TickManager.TicksGame
                    % ReassignmentIntervalTicks != 0)
            {
                return;
            }

            AssignDuties();
        }

        private void AssignDuties()
        {
            List<Thing> reservedVictims = new List<Thing>();
            List<Thing> reservedItems = new List<Thing>();

            for (int pawnIndex = 0;
                pawnIndex < lord.ownedPawns.Count;
                pawnIndex++)
            {
                Pawn pawn = lord.ownedPawns[pawnIndex];

                if (pawn == null || pawn.Downed)
                {
                    continue;
                }

                Thing carriedThing = pawn.carryTracker?.CarriedThing;

                if (carriedThing is Pawn carriedPawn)
                {
                    reservedVictims.Add(carriedPawn);
                    SetDuty(pawn, DutyDefOf.Kidnap);
                    continue;
                }

                if (carriedThing != null)
                {
                    reservedItems.Add(carriedThing);
                    SetDuty(pawn, DutyDefOf.Steal);
                    continue;
                }

                if (!cover)
                {
                    SetDuty(pawn, DutyDefOf.ExitMapBest);
                    continue;
                }

                if (!GenAI.InDangerousCombat(pawn)
                    && KidnapAIUtility.TryFindGoodKidnapVictim(
                        pawn,
                        VictimSearchRadius,
                        out Pawn victim,
                        reservedVictims))
                {
                    reservedVictims.Add(victim);
                    SetDuty(pawn, DutyDefOf.Kidnap);
                    continue;
                }

                if (!GenAI.InDangerousCombat(pawn)
                    && StealAIUtility.TryFindBestItemToSteal(
                        pawn.Position,
                        pawn.Map,
                        ItemSearchRadius,
                        out Thing item,
                        pawn,
                        reservedItems))
                {
                    reservedItems.Add(item);
                    SetDuty(pawn, DutyDefOf.Steal);
                    continue;
                }

                SetDuty(pawn, DutyDefOf.AssaultColony);
            }
        }

        private static void SetDuty(
            Pawn pawn,
            DutyDef dutyDef)
        {
            if (pawn.mindState?.duty?.def == dutyDef)
            {
                return;
            }

            pawn.mindState.duty = new PawnDuty(dutyDef);
            pawn.jobs.EndCurrentJob(JobCondition.InterruptForced);
        }
    }
}
