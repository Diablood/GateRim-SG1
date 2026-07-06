using System;
using System.Collections.Generic;
using System.Linq;
using GateRimSG1.Storytelling;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Data-driven, bounded influence applied by one strategic relation to the
    /// already eligible natural-raid doctrine weights of a Goa'uld domain.
    ///
    /// The relation layer never unlocks a doctrine, changes storyteller
    /// frequency or replaces the persistent domain profile. It only multiplies
    /// weights that survived the existing point and colony-context checks.
    /// </summary>
    public sealed class GoauldRelationDoctrineModifierDef : Def
    {
        public GoauldInterDomainRelation relation;
        public int priority;
        public float directMultiplier = 1f;
        public float abductionMultiplier = 1f;
        public float destructionMultiplier = 1f;

        public bool HasWeightModifier
            => Math.Abs(directMultiplier - 1f) >= 0.001f
                || Math.Abs(abductionMultiplier - 1f) >= 0.001f
                || Math.Abs(destructionMultiplier - 1f) >= 0.001f;

        public GoauldJaffaRaidDoctrineWeights Apply(
            GoauldJaffaRaidDoctrineWeights weights)
        {
            return new GoauldJaffaRaidDoctrineWeights
            {
                direct = ApplyMultiplier(
                    weights.direct,
                    directMultiplier),
                abduction = ApplyMultiplier(
                    weights.abduction,
                    abductionMultiplier),
                destruction = ApplyMultiplier(
                    weights.destruction,
                    destructionMultiplier)
            };
        }

        public override IEnumerable<string> ConfigErrors()
        {
            foreach (string error in base.ConfigErrors())
            {
                yield return error;
            }

            if (priority < 0)
            {
                yield return $"{defName}: priority cannot be negative.";
            }

            if (!(directMultiplier > 0f))
            {
                yield return $"{defName}: directMultiplier must be above zero.";
            }

            if (!(abductionMultiplier > 0f))
            {
                yield return $"{defName}: abductionMultiplier must be above zero.";
            }

            if (!(destructionMultiplier > 0f))
            {
                yield return $"{defName}: destructionMultiplier must be above zero.";
            }
        }

        private static float ApplyMultiplier(
            float weight,
            float multiplier)
        {
            return weight > 0f
                ? weight * multiplier
                : 0f;
        }
    }

    public static class GoauldRelationDoctrineModifierUtility
    {
        public static bool TryResolveModifier(
            Faction faction,
            out GoauldRelationDoctrineModifierDef modifier)
        {
            modifier = null;

            if (!GateRimStorytellerUtility.IsGateRimStorytellerActive
                || !GoauldSystemLordFactionUtility
                    .IsSystemLordFaction(faction))
            {
                return false;
            }

            GameComponent_GoauldInterDomainRelationTracker tracker =
                GameComponent_GoauldInterDomainRelationTracker.Current;

            if (tracker == null)
            {
                return false;
            }

            List<GoauldInterDomainRelation> activeRelations = tracker
                .Snapshot(includeInactive: false)
                .Where(state =>
                    state != null
                    && (state.firstDomain == faction
                        || state.secondDomain == faction))
                .Select(state => state.relation)
                .Distinct()
                .ToList();

            foreach (GoauldInterDomainRelation relation in activeRelations)
            {
                GoauldRelationDoctrineModifierDef candidate =
                    GetModifierForRelation(relation);

                if (candidate == null || !candidate.HasWeightModifier)
                {
                    continue;
                }

                if (modifier == null
                    || candidate.priority > modifier.priority
                    || (candidate.priority == modifier.priority
                        && string.CompareOrdinal(
                            candidate.defName,
                            modifier.defName) < 0))
                {
                    modifier = candidate;
                }
            }

            return modifier != null;
        }

        public static GoauldJaffaRaidDoctrineWeights ApplyModifier(
            GoauldJaffaRaidDoctrineWeights weights,
            Faction faction,
            out GoauldRelationDoctrineModifierDef modifier)
        {
            if (!TryResolveModifier(faction, out modifier))
            {
                return weights;
            }

            return modifier.Apply(weights);
        }

        public static GoauldRelationDoctrineModifierDef
            GetModifierForRelation(GoauldInterDomainRelation relation)
        {
            return DefDatabase<GoauldRelationDoctrineModifierDef>
                .AllDefsListForReading
                .Where(definition => definition.relation == relation)
                .OrderByDescending(definition => definition.priority)
                .ThenBy(definition => definition.defName)
                .FirstOrDefault();
        }
    }
}
