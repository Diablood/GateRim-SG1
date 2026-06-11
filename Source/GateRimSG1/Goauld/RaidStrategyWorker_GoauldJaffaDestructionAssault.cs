using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI.Group;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Controlled Goa'uld destruction-assault strategy.
    /// </summary>
    public class RaidStrategyWorker_GoauldJaffaDestructionAssault
        : RaidStrategyWorker
    {
        protected override LordJob MakeLordJob(
            IncidentParms parms,
            Map map,
            List<Pawn> pawns,
            int raidSeed)
        {
            return new LordJob_GoauldJaffaDestructionAssault();
        }
    }
}
