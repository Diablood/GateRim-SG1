using System.Collections.Generic;
using GateRimSG1.Storytelling;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public static class GoauldThreatProgressionDebugActions
    {
        private const float WeakRaidPoints = 300f;
        private const float OfficerEligibleRaidPoints = 900f;
        private const float AlliedReinforcementRaidPoints = 1200f;
        private const float AdvancedRaidPoints = 4000f;

        public static void ShowCurrentProgression()
        {
            Map map = Find.CurrentMap;

            if (map == null)
            {
                Reject("No current map is available.");
                return;
            }

            float points = StorytellerUtility.DefaultThreatPointsNow(map);
            float relayDefenderPoints = TokraRelaySabotageMissionUtility
                .CalculateDefenderPoints(points);
            float relayReinforcementPoints = TokraRelaySabotageMissionUtility
                .CalculateReinforcementPoints(points);
            int relayDefenders = TokraRelaySabotageMissionUtility
                .CalculateJaffaCount(relayDefenderPoints, 2);
            int relayReinforcements = TokraRelaySabotageMissionUtility
                .CalculateJaffaCount(relayReinforcementPoints, 1);
            int symbiotes = IncidentWorker_GoauldFreeSymbioteIncursion
                .CalculateSymbioteCount(points);

            List<Faction> domains =
                GoauldSystemLordFactionUtility.GetAllFactions();
            Faction diagnosticDomain = domains.Count > 0
                ? domains[0]
                : null;
            GoauldDomainDoctrineProfileDef profile = null;

            GameComponent_GoauldDomainDoctrineTracker tracker =
                GameComponent_GoauldDomainDoctrineTracker.Current;

            if (tracker != null)
            {
                tracker.TryGetProfile(diagnosticDomain, out profile);
            }

            GoauldJaffaRaidDoctrineWeights doctrineWeights =
                IncidentWorker_GoauldJaffaNaturalRaid
                    .CalculateDoctrineWeights(
                        map,
                        points,
                        diagnosticDomain);
            float naturalRaidPressureFactor =
                IncidentWorker_GoauldJaffaNaturalRaid
                    .ResolveNaturalRaidPressureFactor(
                        diagnosticDomain);
            float effectiveNaturalRaidPoints =
                IncidentWorker_GoauldJaffaNaturalRaid
                    .CalculateEffectiveNaturalRaidPoints(
                        diagnosticDomain,
                        points);

            Dialog_MessageBox dialog = new Dialog_MessageBox(
                "Goa'uld threat progression audit\n\n"
                + $"Vanilla storyteller points: {points:0}\n"
                + "Natural raid pressure factor: "
                + $"{naturalRaidPressureFactor * 100f:0}%\n"
                + "Natural raid effective points: "
                + $"{effectiveNaturalRaidPoints:0}\n"
                + "Officer threshold: "
                + GoauldJaffaOfficerForceUtility.MinimumEligibleJaffaCount
                + " eligible Jaffa with one budget-equivalent guard\n"
                + $"Intercepted raid points: {points:0}\n"
                + "Diagnostic domain: "
                + (diagnosticDomain?.Name ?? "<none>")
                + "\n"
                + "Domain doctrine: "
                + (profile == null
                    ? "<fallback 2/1/1>"
                    : GoauldDomainDoctrineProfileUtility
                        .GetDisplayLabel(profile))
                + "\n"
                + "Natural raid doctrines: "
                + FormatDoctrineWeights(doctrineWeights)
                + "\n"
                + "Doctrine context: "
                + $"{map.mapPawns.FreeColonistsSpawnedCount} free colonists "
                + $"(abduction needs "
                + $"{IncidentWorker_GoauldJaffaNaturalRaid.AbductionMinimumColonists}), "
                + $"{map.wealthWatcher.WealthBuildings:0} building wealth "
                + $"(destruction needs "
                + $"{IncidentWorker_GoauldJaffaNaturalRaid.DestructionMinimumBuildingWealth:0})\n"
                + $"Free symbiotes: {symbiotes}\n"
                + "Relay defenders: "
                + $"{relayDefenderPoints:0} points / "
                + $"about {relayDefenders} Jaffa\n"
                + "Relay reinforcements: "
                + $"{relayReinforcementPoints:0} points / "
                + $"about {relayReinforcements} Jaffa\n"
                + "Relay layout: "
                + TokraRelaySabotageSiteLayoutUtility.GetLayoutLabel(
                    relayDefenderPoints));

            Find.WindowStack.Add(dialog);
        }

        private static string FormatDoctrineWeights(
            GoauldJaffaRaidDoctrineWeights weights)
        {
            return "direct "
                + $"{weights.Percentage(weights.direct):0}% / abduction "
                + $"{weights.Percentage(weights.abduction):0}% / destruction "
                + $"{weights.Percentage(weights.destruction):0}%";
        }

        public static void ForceCurrentNaturalRaidWithPressure()
        {
            Map map = Find.CurrentMap;
            IncidentDef incidentDef =
                GR_DefOf.SG1_GoauldJaffaNaturalRaid;
            IncidentWorker_GoauldJaffaNaturalRaid worker =
                incidentDef?.Worker
                    as IncidentWorker_GoauldJaffaNaturalRaid;

            if (map == null
                || incidentDef?.category == null
                || worker == null)
            {
                Reject(
                    "The current map or natural Goa'uld raid definition "
                    + "is unavailable.");
                return;
            }

            IncidentParms parms = StorytellerUtility.DefaultParmsNow(
                incidentDef.category,
                map);
            parms.forced = true;

            if (!worker.TryExecuteForcedWithRelationPressure(parms))
            {
                Reject(
                    "Could not start the current natural Goa'uld raid "
                    + "with inter-domain relation pressure enabled.");
            }
        }

        public static void ForceEligibleNaturalRaidWithOfficer()
        {
            Map map = Find.CurrentMap;
            IncidentDef incidentDef = GR_DefOf.SG1_GoauldJaffaNaturalRaid;
            IncidentWorker_GoauldJaffaNaturalRaid worker =
                incidentDef?.Worker
                    as IncidentWorker_GoauldJaffaNaturalRaid;

            if (map == null || incidentDef?.category == null || worker == null)
            {
                Reject("The natural Goa'uld raid definition is unavailable.");
                return;
            }

            IncidentParms parms = StorytellerUtility.DefaultParmsNow(
                incidentDef.category,
                map);
            parms.forced = true;
            parms.points = OfficerEligibleRaidPoints;

            if (!worker.TryExecuteForcedDebugDoctrineWithOfficer(
                    parms,
                    GoauldJaffaRaidDoctrine.Direct))
            {
                Reject(
                    "Could not start the eligible natural Goa'uld raid "
                    + "with a Jaffa officer.");
            }
        }

        public static void ForceAlliedNaturalRaid()
        {
            Map map = Find.CurrentMap;
            IncidentDef incidentDef = GR_DefOf.SG1_GoauldJaffaNaturalRaid;
            IncidentWorker_GoauldJaffaNaturalRaid worker =
                incidentDef?.Worker
                    as IncidentWorker_GoauldJaffaNaturalRaid;

            if (map == null
                || incidentDef?.category == null
                || worker == null)
            {
                Reject("The natural Goa'uld raid definition is unavailable.");
                return;
            }

            if (!GateRimStorytellerUtility.IsGateRimStorytellerActive)
            {
                Reject(
                    "The Commandement SG-1 storyteller must be active for "
                    + "alliance raid consequences.");
                return;
            }

            if (GameComponent_GoauldAlliedReinforcementTracker
                .HasPendingOrActive(map))
            {
                Reject(
                    "An allied Goa'uld reinforcement is already pending or "
                    + "active on this map.");
                return;
            }

            if (!GameComponent_GoauldAlliedReinforcementTracker
                .TryGetFirstAlliancePair(
                    out Faction primaryDomain,
                    out Faction alliedDomain))
            {
                Reject(
                    "Set at least one eligible Goa'uld relation pair to "
                    + "Alliance first. Open conflict takes precedence.");
                return;
            }

            IncidentParms parms = StorytellerUtility.DefaultParmsNow(
                incidentDef.category,
                map);
            parms.forced = true;
            parms.points = AlliedReinforcementRaidPoints;
            parms.faction = primaryDomain;

            if (!worker.TryExecuteForcedWithAlliedReinforcement(parms))
            {
                Reject(
                    "Could not start the natural Goa'uld raid with delayed "
                    + $"reinforcements from {alliedDomain.Name}.");
            }
        }

        public static void ShowAlliedReinforcementReport()
        {
            GameComponent_GoauldAlliedReinforcementTracker tracker =
                GameComponent_GoauldAlliedReinforcementTracker.Current;

            if (tracker == null)
            {
                Reject("The allied Goa'uld reinforcement tracker is unavailable.");
                return;
            }

            Find.WindowStack.Add(
                new Dialog_MessageBox(tracker.BuildDebugReport()));
        }

        public static void ForceCurrentDirectRaid()
        {
            ForceRaid(
                GR_DefOf.SG1_GoauldJaffaControlledRaid,
                0f,
                "current direct raid");
        }

        public static void ForceCurrentAbductionRaid()
        {
            ForceRaid(
                GR_DefOf.SG1_GoauldJaffaControlledAbductionRaid,
                0f,
                "current abduction raid");
        }

        public static void ForceCurrentDestructionRaid()
        {
            ForceRaid(
                GR_DefOf.SG1_GoauldJaffaControlledDestructionRaid,
                0f,
                "current destruction raid");
        }

        public static void ForceWeakDirectRaid()
        {
            ForceRaid(
                GR_DefOf.SG1_GoauldJaffaControlledRaid,
                WeakRaidPoints,
                "300-point direct raid");
        }

        public static void ForceAdvancedDirectRaid()
        {
            ForceRaid(
                GR_DefOf.SG1_GoauldJaffaControlledRaid,
                AdvancedRaidPoints,
                "4000-point direct raid");
        }

        public static void ForceNaturalDirectRaid()
        {
            ForceNaturalDoctrine(
                GoauldJaffaRaidDoctrine.Direct,
                WeakRaidPoints,
                "natural direct raid");
        }

        public static void ForceNaturalAbductionRaid()
        {
            ForceNaturalDoctrine(
                GoauldJaffaRaidDoctrine.Abduction,
                IncidentWorker_GoauldJaffaNaturalRaid
                    .AbductionMinimumPoints,
                "natural abduction raid");
        }

        public static void ForceNaturalDestructionRaid()
        {
            ForceNaturalDoctrine(
                GoauldJaffaRaidDoctrine.Destruction,
                IncidentWorker_GoauldJaffaNaturalRaid
                    .DestructionMinimumPoints,
                "natural destruction raid");
        }

        private static void ForceNaturalDoctrine(
            GoauldJaffaRaidDoctrine doctrine,
            float points,
            string context)
        {
            Map map = Find.CurrentMap;
            IncidentDef incidentDef = GR_DefOf.SG1_GoauldJaffaNaturalRaid;
            IncidentWorker_GoauldJaffaNaturalRaid worker =
                incidentDef?.Worker
                    as IncidentWorker_GoauldJaffaNaturalRaid;

            if (map == null || incidentDef?.category == null || worker == null)
            {
                Reject("The natural Goa'uld raid definition is unavailable.");
                return;
            }

            IncidentParms parms = StorytellerUtility.DefaultParmsNow(
                incidentDef.category,
                map);
            parms.forced = true;
            parms.points = points;

            if (!worker.TryExecuteForcedDebugDoctrine(parms, doctrine))
            {
                Reject(
                    $"Could not start the Goa'uld {context}.");
            }
        }

        private static void ForceRaid(
            IncidentDef incidentDef,
            float explicitPoints,
            string context)
        {
            Map map = Find.CurrentMap;

            if (map == null || incidentDef?.category == null)
            {
                Reject("The current map or raid definition is unavailable.");
                return;
            }

            IncidentParms parms = StorytellerUtility.DefaultParmsNow(
                incidentDef.category,
                map);
            parms.forced = true;

            if (explicitPoints > 0f)
            {
                parms.points = explicitPoints;
            }

            if (!incidentDef.Worker.TryExecute(parms))
            {
                Reject($"Could not start the Goa'uld {context}.");
            }
        }

        private static void Reject(string text)
        {
            Messages.Message(
                text,
                MessageTypeDefOf.RejectInput,
                historical: false);
        }
    }
}
