using GateRimSG1.Goauld;
using Verse;

namespace GateRimSG1.Names
{
    public class CompProperties_CulturalPawnNameOnSpawn : CompProperties
    {
        public CompProperties_CulturalPawnNameOnSpawn()
        {
            compClass = typeof(Comp_CulturalPawnNameOnSpawn);
        }
    }

    /// <summary>
    /// Notifies the GateRim pawn initializers and shared cultural-name manager
    /// as soon as a compatible pawn is spawned. This keeps developer-spawned
    /// pawns correctly initialized and named even while the game is paused.
    /// </summary>
    public class Comp_CulturalPawnNameOnSpawn : ThingComp
    {
        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);

            if (respawningAfterLoad)
            {
                return;
            }

            Pawn pawn = parent as Pawn;

            if (pawn == null)
            {
                return;
            }

            GameComponent_GoauldHostCasteInitializer.Current
                ?.NotifyPawnSpawned(pawn);
            GameComponent_TokraHostPrototypeInitializer.Current
                ?.NotifyPawnSpawned(pawn);
            GameComponent_CulturalPawnNameManager.Current
                ?.NotifyPawnSpawned(pawn);
        }
    }
}
