using GateRimSG1.Goauld;
using GateRimSG1.Jaffa;
using GateRimSG1.Names;
using RimWorld;
using Verse;

namespace GateRimSG1.Culture
{
    /// <summary>
    /// Adds the biological state implied by a newly generated player starter.
    ///
    /// This runs only from the PlayerStarter generation callback, before the
    /// pawn is shown on the configuration page. The scenario part guards only
    /// a completed generation cycle; no game-level tracker later restores a
    /// Prim'ta or adult symbiote removed by the player.
    /// </summary>
    public static class StarterSymbioteInitializer
    {
        /// <summary>
        /// Attempts to resolve the biological state for the current generation
        /// cycle. Returns true only when the affected starter has reached a
        /// terminal result that can safely be guarded against duplicate
        /// callbacks. Every Goa'uld-host xenotype resolves to either a Tok'ra
        /// or Goa'uld-origin adult symbiote from its final cultural career.
        /// </summary>
        public static bool TryInitialize(
            Pawn pawn,
            CulturalPawnProfileDef profile)
        {
            if (pawn?.health == null || profile == null)
            {
                return false;
            }

            XenotypeDef xenotype = pawn.genes?.Xenotype;

            if (xenotype == GR_DefOf.SG1_Jaffa)
            {
                TryInitializeJaffa(pawn);
                return true;
            }

            if (xenotype == GR_DefOf.SG1_GoauldHost)
            {
                return TryInitializeGoauldHost(pawn, profile);
            }

            return false;
        }

        private static void TryInitializeJaffa(Pawn pawn)
        {
            if (!JaffaPrimtaUtility.IsEligibleForPrimtaImplantation(pawn))
            {
                return;
            }

            Hediff primta = HediffMaker.MakeHediff(
                GR_DefOf.SG1_JaffaPrimta,
                pawn);
            pawn.health.AddHediff(primta);

            GR_Log.Message(
                $"Initialized player-starter Jaffa "
                + $"{JaffaPrimtaUtility.PawnDebugLabel(pawn)} "
                + "with a visible Prim'ta before starter confirmation.");
        }

        private static bool TryInitializeGoauldHost(
            Pawn pawn,
            CulturalPawnProfileDef profile)
        {
            if (FindExistingPersistentSymbioteComp(pawn) != null)
            {
                return true;
            }

            CulturalPawnNameGroup nameGroup = profile.NameGroupFor(pawn);

            return nameGroup == CulturalPawnNameGroup.Tokra
                ? TryInitializeTokra(pawn)
                : TryInitializeGoauld(pawn);
        }

        private static bool TryInitializeTokra(Pawn pawn)
        {
            Hediff activeHostState = HediffMaker.MakeHediff(
                GR_DefOf.SG1_GoauldHostSymbiote,
                pawn);
            HediffComp_GoauldSymbiote targetComp
                = FindPersistentSymbioteComp(activeHostState);

            if (targetComp == null)
            {
                GR_Log.Error(
                    "Cannot initialize player-starter Tok'ra: "
                    + "SG1_GoauldHostSymbiote is missing "
                    + "HediffComp_GoauldSymbiote.");
                return false;
            }

            GoauldSymbioteData data
                = GoauldSymbioteData.CreatePreJoinedTokraStarter(
                    pawn,
                    Find.TickManager?.TicksGame ?? 0);

            if (data.HostIdentitySource
                != TokraHostIdentitySource.GeneratedPreJoined)
            {
                GR_Log.Error(
                    "Cannot initialize player-starter Tok'ra because a "
                    + "distinct historical host identity was not generated "
                    + $"for {PawnDebugLabel(pawn)}.");
                return false;
            }

            targetComp.InitializeWithTransferredData(data);
            pawn.health.AddHediff(activeHostState);

            GR_Log.Message(
                $"Initialized player-starter Tok'ra {PawnDebugLabel(pawn)} "
                + $"with symbiote {data.SymbioteId} and a distinct host "
                + "identity before starter confirmation.");

            return true;
        }

        private static bool TryInitializeGoauld(Pawn pawn)
        {
            Hediff activeHostState = HediffMaker.MakeHediff(
                GR_DefOf.SG1_GoauldHostSymbiote,
                pawn);
            HediffComp_GoauldSymbiote targetComp
                = FindPersistentSymbioteComp(activeHostState);

            if (targetComp == null)
            {
                GR_Log.Error(
                    "Cannot initialize player-starter Goa'uld host: "
                    + "SG1_GoauldHostSymbiote is missing "
                    + "HediffComp_GoauldSymbiote.");
                return false;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            GoauldSymbioteData data = GoauldSymbioteData.CreateFree(
                currentTick,
                GoauldSymbioteOrigin.Goauld);

            if (pawn.Name != null)
            {
                data.SetSymbioteName(pawn.Name.ToStringFull);
            }

            data.RecordAllegiance(pawn.Faction);
            targetComp.InitializeWithTransferredData(data);
            pawn.health.AddHediff(activeHostState);

            GR_Log.Message(
                $"Initialized player-starter Goa'uld host "
                + $"{PawnDebugLabel(pawn)} with symbiote "
                + $"{data.SymbioteId} before starter confirmation.");

            return true;
        }

        private static HediffComp_GoauldSymbiote
            FindExistingPersistentSymbioteComp(Pawn pawn)
        {
            if (pawn?.health?.hediffSet?.hediffs == null)
            {
                return null;
            }

            for (int index = 0;
                index < pawn.health.hediffSet.hediffs.Count;
                index++)
            {
                HediffComp_GoauldSymbiote comp = FindPersistentSymbioteComp(
                    pawn.health.hediffSet.hediffs[index]);

                if (comp != null)
                {
                    return comp;
                }
            }

            return null;
        }

        private static HediffComp_GoauldSymbiote FindPersistentSymbioteComp(
            Hediff hediff)
        {
            HediffWithComps withComps = hediff as HediffWithComps;
            return withComps?.GetComp<HediffComp_GoauldSymbiote>();
        }

        private static string PawnDebugLabel(Pawn pawn)
        {
            if (pawn == null)
            {
                return "<null pawn>";
            }

            return $"{pawn.LabelShort} ({pawn.ThingID})";
        }
    }
}
