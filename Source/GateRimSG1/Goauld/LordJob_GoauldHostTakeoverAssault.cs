using RimWorld;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Legacy no-retreat takeover assault retained only so development saves
    /// created by local revision r3 can still deserialize safely.
    ///
    /// GoauldHostileTakeoverAssaultUtility replaces this Lord with
    /// LordJob_GoauldHostTakeoverRaidAssault on the next active-host check.
    /// </summary>
    public sealed class LordJob_GoauldHostTakeoverAssault
        : LordJob_AssaultColony
    {
        public LordJob_GoauldHostTakeoverAssault()
        {
        }

        public LordJob_GoauldHostTakeoverAssault(Faction faction)
            : base(
                faction,
                canKidnap: false,
                canTimeoutOrFlee: false)
        {
        }
    }
}
