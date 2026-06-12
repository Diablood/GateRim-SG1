using Verse;

namespace GateRimSG1.Weapons
{
    /// <summary>
    /// Broad technology families used by Stargate energy weapons.
    ///
    /// Future Replicator resistance can query this classification instead of
    /// hard-coding one exception for every weapon Def.
    /// </summary>
    public enum SG1EnergyWeaponTechnology
    {
        Goauld,
        Tokra
    }

    /// <summary>
    /// Marks an equipment or projectile Def as part of a Stargate energy-
    /// weapon family.
    ///
    /// The resistance flag is declarative in 0.1.76-dev: no Replicator pawn
    /// exists yet. It prepares a shared hook for the future faction.
    /// </summary>
    public class SG1EnergyWeaponExtension : DefModExtension
    {
        public SG1EnergyWeaponTechnology technology;
        public bool affectedByFutureReplicatorResistance = true;
    }
}
