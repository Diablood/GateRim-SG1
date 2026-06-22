using System.Collections.Generic;
using System.Linq;
using RimWorld;
using RimWorld.Planet;
using Verse;
using Verse.AI.Group;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Vanilla caravan-ambush flow with a fixed Goa'uld faction source.
    /// The temporary map, inventory transfer, victory state and caravan
    /// reformation remain handled by RimWorld.
    /// </summary>
    public class IncidentWorker_TokraDeliveryInterception
        : IncidentWorker_Ambush
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            return base.CanFireNowSub(parms)
                && GoauldSystemLordFactionUtility.GetOrCreateFaction(
                    "Tok'ra delivery interception validation") != null;
        }

        protected override List<Pawn> GeneratePawns(IncidentParms parms)
        {
            parms.faction = GoauldSystemLordFactionUtility.GetOrCreateFaction(
                "Tok'ra delivery interception");

            if (parms.faction == null)
            {
                return new List<Pawn>();
            }

            PawnGroupMakerParms groupParms
                = IncidentParmsUtility.GetDefaultPawnGroupMakerParms(
                    PawnGroupKindDefOf.Combat,
                    parms);
            groupParms.generateFightersOnly = true;
            groupParms.dontUseSingleUseRocketLaunchers = true;

            return PawnGroupMakerUtility.GeneratePawns(groupParms).ToList();
        }

        protected override LordJob CreateLordJob(
            List<Pawn> generatedPawns,
            IncidentParms parms)
        {
            return new LordJob_AssaultColony(
                parms.faction,
                canKidnap: true,
                canTimeoutOrFlee: false);
        }

        protected override string GetLetterText(
            Pawn anyPawn,
            IncidentParms parms)
        {
            Caravan caravan = parms.target as Caravan;
            return def.letterText.Formatted(
                    caravan != null
                        ? caravan.Name
                        : "yourCaravan".TranslateSimple(),
                    parms.faction.def.pawnsPlural,
                    parms.faction.NameColored)
                .Resolve()
                .CapitalizeFirst();
        }
    }
}
