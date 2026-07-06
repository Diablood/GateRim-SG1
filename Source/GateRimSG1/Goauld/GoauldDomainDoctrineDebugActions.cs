using System.Collections.Generic;
using GateRimSG1.Storytelling;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public static class GoauldDomainDoctrineDebugActions
    {
        public static void ShowReport()
        {
            GameComponent_GoauldDomainDoctrineTracker tracker =
                GameComponent_GoauldDomainDoctrineTracker.Current;

            if (tracker == null)
            {
                Reject(
                    "The Goa'uld domain-doctrine tracker is unavailable.");
                return;
            }

            List<GoauldDomainDoctrineState> states = tracker.Snapshot();

            if (states.Count == 0)
            {
                Reject("No Goa'uld System Lord domain exists in this world.");
                return;
            }

            Map map = Find.CurrentMap;
            float points = map == null
                ? 0f
                : StorytellerUtility.DefaultThreatPointsNow(map);
            System.Text.StringBuilder builder =
                new System.Text.StringBuilder();

            builder.AppendLine("Goa'uld domain doctrine report");
            builder.AppendLine();
            builder.AppendLine(
                "SG-1 Command active: "
                + GateRimStorytellerUtility
                    .IsGateRimStorytellerActive.ToString().ToLowerInvariant());
            builder.AppendLine(
                "current-map storyteller points: "
                + (map == null ? "<no map>" : points.ToString("0")));
            builder.AppendLine(
                "relation priority: open conflict > alliance > rivalry; "
                + "neutrality and truce have no modifier");
            builder.AppendLine(
                "forced historical raid commands bypass relation doctrine "
                + "modifiers");
            builder.AppendLine();

            for (int index = 0; index < states.Count; index++)
            {
                GoauldDomainDoctrineState state = states[index];
                Faction faction = state.domainFaction;
                GoauldDomainDoctrineProfileDef profile = state.profile;
                GoauldJaffaRaidDoctrineWeights baseWeights =
                    profile.BaseWeights;

                builder.AppendLine(faction.Name);
                builder.AppendLine(
                    "  leader: "
                    + (faction.leader?.LabelShortCap ?? "<none>"));
                builder.AppendLine(
                    "  faction load ID: " + faction.loadID);
                builder.AppendLine(
                    "  profile: "
                    + GoauldDomainDoctrineProfileUtility
                        .GetDisplayLabel(profile)
                    + " ("
                    + profile.defName
                    + ")");
                builder.AppendLine(
                    "  base weights: "
                    + FormatRawWeights(baseWeights));

                if (map != null)
                {
                    GoauldJaffaRaidDoctrineWeights eligibleWeights =
                        IncidentWorker_GoauldJaffaNaturalRaid
                            .CalculateEligibleDoctrineWeights(
                                map,
                                points,
                                faction);
                    GoauldJaffaRaidDoctrineWeights effectiveWeights =
                        IncidentWorker_GoauldJaffaNaturalRaid
                            .CalculateDoctrineWeights(
                                map,
                                points,
                                faction);

                    builder.AppendLine(
                        "  eligible weights before relation: "
                        + FormatRawWeights(eligibleWeights)
                        + " ("
                        + FormatPercentages(eligibleWeights)
                        + ")");

                    if (GoauldRelationDoctrineModifierUtility
                        .TryResolveModifier(
                            faction,
                            out GoauldRelationDoctrineModifierDef modifier))
                    {
                        builder.AppendLine(
                            "  selected relation modifier: "
                            + modifier.relation
                            + " ("
                            + modifier.defName
                            + ", priority "
                            + modifier.priority
                            + ")");
                        builder.AppendLine(
                            "  multipliers: "
                            + FormatMultipliers(modifier));
                    }
                    else
                    {
                        builder.AppendLine(
                            "  selected relation modifier: none");
                    }

                    builder.AppendLine(
                        "  current-map final weights: "
                        + FormatRawWeights(effectiveWeights)
                        + " ("
                        + FormatPercentages(effectiveWeights)
                        + ")");
                }

                if (faction.defeated)
                {
                    builder.AppendLine("  state: defeated");
                }

                if (index < states.Count - 1)
                {
                    builder.AppendLine();
                }
            }

            Find.WindowStack.Add(
                new Dialog_MessageBox(builder.ToString()));
        }

        public static void SetConquest()
        {
            OpenFactionSelector(
                GoauldDomainDoctrineProfileUtility.ConquestDefName);
        }

        public static void SetEnslavement()
        {
            OpenFactionSelector(
                GoauldDomainDoctrineProfileUtility.EnslavementDefName);
        }

        public static void SetScorchedEarth()
        {
            OpenFactionSelector(
                GoauldDomainDoctrineProfileUtility.ScorchedEarthDefName);
        }

        private static void OpenFactionSelector(string profileDefName)
        {
            GameComponent_GoauldDomainDoctrineTracker tracker =
                GameComponent_GoauldDomainDoctrineTracker.Current;
            GoauldDomainDoctrineProfileDef profile =
                GoauldDomainDoctrineProfileUtility.GetByDefName(
                    profileDefName);
            List<Faction> factions =
                GoauldSystemLordFactionUtility.GetAllFactions();

            if (tracker == null || profile == null)
            {
                Reject(
                    "The Goa'uld domain-doctrine tracker or profile "
                    + "definition is unavailable.");
                return;
            }

            if (factions.Count == 0)
            {
                Reject("No Goa'uld System Lord domain exists in this world.");
                return;
            }

            List<FloatMenuOption> options =
                new List<FloatMenuOption>();

            for (int index = 0; index < factions.Count; index++)
            {
                Faction selectedFaction = factions[index];
                string leaderLabel =
                    selectedFaction.leader?.LabelShortCap ?? "<no leader>";
                string optionLabel =
                    selectedFaction.Name
                    + " — "
                    + leaderLabel;

                options.Add(
                    new FloatMenuOption(
                        optionLabel,
                        delegate
                        {
                            if (!tracker.SetProfile(
                                selectedFaction,
                                profile))
                            {
                                Reject(
                                    "Could not assign the selected "
                                    + "Goa'uld domain doctrine.");
                                return;
                            }

                            Messages.Message(
                                "GR_GoauldDomainDoctrine_AssignedMessage"
                                    .Translate(
                                        selectedFaction.Name,
                                        GoauldDomainDoctrineProfileUtility
                                            .GetDisplayLabel(profile)),
                                MessageTypeDefOf.TaskCompletion,
                                historical: false);
                        }));
            }

            Find.WindowStack.Add(new FloatMenu(options));
        }

        private static string FormatRawWeights(
            GoauldJaffaRaidDoctrineWeights weights)
        {
            return $"direct {weights.direct:0.##} / "
                + $"abduction {weights.abduction:0.##} / "
                + $"destruction {weights.destruction:0.##}";
        }


        private static string FormatMultipliers(
            GoauldRelationDoctrineModifierDef modifier)
        {
            return $"direct x{modifier.directMultiplier:0.##} / "
                + $"abduction x{modifier.abductionMultiplier:0.##} / "
                + $"destruction x{modifier.destructionMultiplier:0.##}";
        }

        private static string FormatPercentages(
            GoauldJaffaRaidDoctrineWeights weights)
        {
            return "direct "
                + $"{weights.Percentage(weights.direct):0}% / "
                + "abduction "
                + $"{weights.Percentage(weights.abduction):0}% / "
                + "destruction "
                + $"{weights.Percentage(weights.destruction):0}%";
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
