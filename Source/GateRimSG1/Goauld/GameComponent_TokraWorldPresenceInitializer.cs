using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Ensures one persistent hidden Tok'ra world-faction instance exists.
    ///
    /// New worlds normally receive the faction through
    /// requiredCountAtGameStart = 1. The component also covers older saves
    /// created before the Tok'ra world-presence milestone.
    /// </summary>
    public class GameComponent_TokraWorldPresenceInitializer : GameComponent
    {
        private const int RetryIntervalTicks = 600;

        public GameComponent_TokraWorldPresenceInitializer(Game game)
        {
        }

        public override void StartedNewGame()
        {
            base.StartedNewGame();
            EnsurePersistentFaction("new-game Tok'ra world-presence initialization");
        }

        public override void LoadedGame()
        {
            base.LoadedGame();
            EnsurePersistentFaction("loaded-save Tok'ra world-presence migration");
        }

        public override void GameComponentTick()
        {
            if (Find.TickManager.TicksGame % RetryIntervalTicks != 0)
            {
                return;
            }

            EnsurePersistentFaction("Tok'ra world-presence retry");
        }

        private static void EnsurePersistentFaction(string purpose)
        {
            TokraFactionUtility.GetOrCreatePersistentFaction(purpose);
        }
    }
}
