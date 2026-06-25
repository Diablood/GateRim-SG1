using RimWorld;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Raid-like assault assigned to a colonist whose active Goa'uld
    /// symbiote has completed a hostile takeover.
    ///
    /// The host attacks the colony as a normal assailant, but vanilla raid
    /// timeout and retreat logic remains enabled so it can eventually
    /// preserve itself and leave an exhausted or abandoned map.
    /// </summary>
    public sealed class LordJob_GoauldHostTakeoverRaidAssault
        : LordJob_AssaultColony
    {
        public LordJob_GoauldHostTakeoverRaidAssault()
        {
        }

        public LordJob_GoauldHostTakeoverRaidAssault(Faction faction)
            : base(
                faction,
                canKidnap: false,
                canTimeoutOrFlee: true)
        {
        }
    }
}
