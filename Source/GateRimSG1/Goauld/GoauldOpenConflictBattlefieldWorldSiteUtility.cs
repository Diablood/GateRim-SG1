using System;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    public static class GoauldOpenConflictBattlefieldWorldSiteUtility
    {
        public const int MinimumTileDistance = 6;
        public const int MaximumTileDistance = 18;
        public const int SiteDurationTicks = 480000;
        public const int BattlefieldMapSize = 140;

        public static bool CanCreateFrom(Map sourceMap)
        {
            return sourceMap != null
                && sourceMap.IsPlayerHome
                && sourceMap.Tile != PlanetTile.Invalid
                && GR_DefOf.SG1_GoauldOpenConflictBattlefieldSite != null
                && Find.WorldObjects != null;
        }

        public static bool TryCreate(
            Map sourceMap,
            GoauldInterDomainRelationState pair,
            int previousLetterVariant,
            out WorldObject_GoauldOpenConflictBattlefieldSite site,
            out int letterVariant)
        {
            site = null;
            letterVariant = GoauldOpenConflictBattlefieldUtility
                .SelectLetterVariant(previousLetterVariant);

            if (!CanCreateFrom(sourceMap)
                || pair?.firstDomain == null
                || pair.secondDomain == null
                || pair.relation
                    != GoauldInterDomainRelation.OpenConflict)
            {
                return false;
            }

            PlanetTile tile;

            if (!TileFinder.TryFindNewSiteTile(
                    out tile,
                    sourceMap.Tile,
                    minDist: MinimumTileDistance,
                    maxDist: MaximumTileDistance,
                    allowCaravans: false,
                    selectLandmarkChance: 0f,
                    layer: sourceMap.Tile.Layer))
            {
                return false;
            }

            WorldObject_GoauldOpenConflictBattlefieldSite created =
                WorldObjectMaker.MakeWorldObject(
                    GR_DefOf.SG1_GoauldOpenConflictBattlefieldSite)
                as WorldObject_GoauldOpenConflictBattlefieldSite;

            if (created == null)
            {
                return false;
            }

            float vanillaPoints = StorytellerUtility.DefaultThreatPointsNow(
                sourceMap);
            float detachmentPoints = GoauldOpenConflictBattlefieldUtility
                .CalculateDetachmentPoints(vanillaPoints);
            int currentTick = Find.TickManager?.TicksGame ?? 0;

            created.Tile = tile;
            created.Initialize(
                pair.firstDomain,
                pair.secondDomain,
                vanillaPoints,
                detachmentPoints,
                currentTick + SiteDurationTicks,
                BattlefieldMapSize);
            Find.WorldObjects.Add(created);

            SendCreationLetter(
                created,
                pair.firstDomain,
                pair.secondDomain,
                letterVariant);
            site = created;
            return true;
        }

        private static void SendCreationLetter(
            WorldObject_GoauldOpenConflictBattlefieldSite site,
            Faction firstDomain,
            Faction secondDomain,
            int variant)
        {
            string textKey =
                "GR_GoauldOpenConflictWorldSite_Text" + variant;

            Find.LetterStack?.ReceiveLetter(
                "GR_GoauldOpenConflictWorldSite_Label".Translate(),
                textKey.Translate(
                    firstDomain?.Name ?? "<missing domain>",
                    secondDomain?.Name ?? "<missing domain>"),
                LetterDefOf.NeutralEvent,
                new LookTargets(site));
        }
    }
}
