using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Centralizes the player-control boundary for the Tok'ra dual-identity UI.
    /// Permanent player colonists remain eligible on a map and while travelling
    /// in a player-controlled caravan. Guests, prisoners, slaves, mental-state
    /// pawns and AI-managed Tok'ra remain excluded.
    /// </summary>
    public static class TokraPlayerControlUtility
    {
        public static bool IsDirectlyPlayerControlled(Pawn pawn)
        {
            if (pawn == null || pawn.Dead || pawn.InMentalState)
            {
                return false;
            }

            if (pawn.IsColonistPlayerControlled)
            {
                return true;
            }

            Caravan caravan = pawn.GetCaravan();
            return caravan != null
                && caravan.IsPlayerControlled
                && caravan.IsOwner(pawn);
        }

        public static bool IsEligibleTokra(
            Pawn pawn,
            HediffComp_GoauldSymbiote symbioteComp)
        {
            return symbioteComp?.SymbioteData?.Origin
                    == GoauldSymbioteOrigin.Tokra
                && IsDirectlyPlayerControlled(pawn);
        }

        public static bool TryGetEligibleTokraComp(
            Pawn pawn,
            out HediffComp_GoauldSymbiote symbioteComp)
        {
            symbioteComp = null;

            if (!IsDirectlyPlayerControlled(pawn)
                || pawn.health?.hediffSet?.hediffs == null)
            {
                return false;
            }

            for (int i = 0; i < pawn.health.hediffSet.hediffs.Count; i++)
            {
                HediffComp_GoauldSymbiote candidate = pawn.health
                    .hediffSet
                    .hediffs[i]
                    .TryGetComp<HediffComp_GoauldSymbiote>();

                if (candidate?.SymbioteData?.Origin
                    != GoauldSymbioteOrigin.Tokra)
                {
                    continue;
                }

                symbioteComp = candidate;
                return true;
            }

            return false;
        }
    }
}
