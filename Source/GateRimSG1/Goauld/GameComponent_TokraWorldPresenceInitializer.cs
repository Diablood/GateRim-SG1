using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Compatibility shell retained for saves created before 0.3.49-dev.
    ///
    /// Tok'ra presence is now controlled exclusively by the world-faction
    /// selection. This component deliberately performs no creation, migration
    /// or periodic reconciliation when the faction is absent.
    /// </summary>
    public class GameComponent_TokraWorldPresenceInitializer : GameComponent
    {
        public GameComponent_TokraWorldPresenceInitializer(Game game)
        {
        }
    }
}
