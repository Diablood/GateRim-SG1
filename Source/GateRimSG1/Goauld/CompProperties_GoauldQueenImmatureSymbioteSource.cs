using Verse;

namespace GateRimSG1.Goauld
{
    public class CompProperties_GoauldQueenImmatureSymbioteSource
        : CompProperties
    {
        public int harvestCooldownTicks = 60000;
        public int spawnCount = 1;

        public CompProperties_GoauldQueenImmatureSymbioteSource()
        {
            compClass = typeof(Comp_GoauldQueenImmatureSymbioteSource);
        }
    }
}
