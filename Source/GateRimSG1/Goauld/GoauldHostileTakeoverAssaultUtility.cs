using RimWorld;
using Verse;
using Verse.AI.Group;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Keeps an actively controlled Goa'uld host in a real hostile assault
    /// group instead of allowing the default unaffiliated-hostile pawn logic
    /// to send the former colonist toward a map edge.
    /// </summary>
    public static class GoauldHostileTakeoverAssaultUtility
    {
        public static bool EnsureAssaultBehavior(Pawn host)
        {
            if (host == null
                || host.Dead
                || !host.Spawned
                || host.Map == null)
            {
                return false;
            }

            if (host.IsPrisonerOfColony)
            {
                ReleaseAssaultBehavior(host);
                return false;
            }

            if (host.Faction == null
                || host.Faction == Faction.OfPlayer
                || !host.Faction.HostileTo(Faction.OfPlayer))
            {
                return false;
            }

            Lord currentLord = host.GetLord();

            if (currentLord?.LordJob
                is LordJob_GoauldHostTakeoverRaidAssault)
            {
                return true;
            }

            bool migratedLegacyAssault = currentLord?.LordJob
                is LordJob_GoauldHostTakeoverAssault;

            currentLord?.RemovePawn(host);
            host.jobs?.StopAll();

            Lord assaultLord = LordMaker.MakeNewLord(
                host.Faction,
                new LordJob_GoauldHostTakeoverRaidAssault(host.Faction),
                host.Map);
            assaultLord.AddPawn(host);

            GR_Log.Message(
                $"Assigned hostile Goa'uld host {host.LabelShort} "
                + $"({host.ThingID}) to a raid-like colony assault "
                + $"with vanilla retreat enabled; migratedLegacyAssault="
                + $"{migratedLegacyAssault}; faction={host.Faction.Name} "
                + $"({host.Faction.loadID}).");

            return true;
        }

        public static void ReleaseAssaultBehavior(Pawn host)
        {
            Lord currentLord = host?.GetLord();

            if (!(currentLord?.LordJob
                    is LordJob_GoauldHostTakeoverRaidAssault)
                && !(currentLord?.LordJob
                    is LordJob_GoauldHostTakeoverAssault))
            {
                return;
            }

            currentLord.RemovePawn(host);
            host.jobs?.StopAll();

            GR_Log.Message(
                $"Released Goa'uld-controlled host {host.LabelShort} "
                + $"({host.ThingID}) from the takeover assault.");
        }
    }
}
