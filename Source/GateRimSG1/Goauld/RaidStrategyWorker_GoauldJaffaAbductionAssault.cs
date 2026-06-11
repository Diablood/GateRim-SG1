using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI.Group;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Controlled abduction strategy.
    ///
    /// The group starts directly in a cover-and-capture lord job instead of
    /// waiting for the vanilla AssaultColony kidnapping transition.
    /// </summary>
    public class RaidStrategyWorker_GoauldJaffaAbductionAssault
        : RaidStrategyWorker
    {
        protected override LordJob MakeLordJob(
            IncidentParms parms,
            Map map,
            List<Pawn> pawns,
            int raidSeed)
        {
            return new LordJob_GoauldJaffaAbductionAssault();
        }
    }
}
