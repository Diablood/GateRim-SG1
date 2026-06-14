using System.Collections.Generic;
using LudeonTK;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    public static class TokraSafehouseDebugActions
    {
        [DebugAction(
            "GateRim SG-1",
            "Prepare Tok'ra safehouse site test",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void PrepareSafehouseSiteTest()
        {
            List<WorldObject> safehouseObjects
                = GetActiveSafehouseWorldObjects();

            foreach (WorldObject worldObject in safehouseObjects)
            {
                if (worldObject is MapParent mapParent && mapParent.HasMap)
                {
                    Messages.Message(
                        "GR_TokraSafehouseDebug_ActiveMap".Translate(),
                        MessageTypeDefOf.RejectInput,
                        historical: false);
                    return;
                }
            }

            foreach (WorldObject worldObject in safehouseObjects)
            {
                worldObject.Destroy();
            }

            bool leadPrepared
                = GameComponent_TokraSafehouseLeadTracker.DebugSetLeadCount(1);

            if (!leadPrepared)
            {
                Messages.Message(
                    "GR_TokraSafehouseDebug_Unavailable".Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            int trustScore = GameComponent_TokraTrustTracker
                .GetCurrentTrustScore();
            TokraTrustTier tier = GameComponent_TokraTrustTracker
                .GetCurrentTier();

            Messages.Message(
                "GR_TokraSafehouseDebug_Prepared"
                    .Translate(
                        trustScore.ToString(),
                        GetDebugTrustTierLabel(tier)),
                MessageTypeDefOf.PositiveEvent,
                historical: false);
        }

        [DebugAction(
            "GateRim SG-1",
            "Create Tok'ra safehouse test site",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void CreateSafehouseTestSite()
        {
            Map map = Find.CurrentMap;

            if (map == null
                || GR_DefOf.SG1_TokraHiddenSafehouseSiteIncident == null)
            {
                Messages.Message(
                    "GR_TokraSafehouseDebug_Unavailable".Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            IncidentParms parms = new IncidentParms
            {
                target = map,
                forced = true
            };

            if (!GR_DefOf.SG1_TokraHiddenSafehouseSiteIncident
                    .Worker
                    .TryExecute(parms))
            {
                Messages.Message(
                    "GR_TokraSafehouseDebug_CreateFailed".Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
            }
        }


        [DebugAction(
            "GateRim SG-1",
            "Increase Tok'ra trust test step",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void IncreaseTokraTrustTestStep()
        {
            AdjustTokraTrustForDebug(5);
        }

        [DebugAction(
            "GateRim SG-1",
            "Decrease Tok'ra trust test step",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void DecreaseTokraTrustTestStep()
        {
            AdjustTokraTrustForDebug(-5);
        }

        [DebugAction(
            "GateRim SG-1",
            "Verify Tok'ra safehouse contact test",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void VerifySafehouseContactTest()
        {
            Map map = Find.CurrentMap;
            List<Pawn> contacts = new List<Pawn>();

            if (map?.mapPawns == null
                || map.Parent?.def != GR_DefOf.SG1_TokraHiddenSafehouseSite)
            {
                Messages.Message(
                    "GR_TokraSafehouseDebug_ContactUnavailable".Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            foreach (Pawn pawn in map.mapPawns.AllPawnsSpawned)
            {
                if (pawn?.kindDef == GR_DefOf.SG1_TokraVoluntaryHost
                    && pawn.Faction?.def == GR_DefOf.SG1_Tokra)
                {
                    contacts.Add(pawn);
                }
            }

            Pawn contact = contacts.Count == 1 ? contacts[0] : null;
            int biologicalAge = contact?.ageTracker?.AgeBiologicalYears ?? -1;
            bool hasAdultBackstory = contact?.story?.Adulthood != null;
            bool isHostile = contact?.Faction == null
                || contact.Faction.HostileTo(Faction.OfPlayer)
                || Faction.OfPlayer.HostileTo(contact.Faction);
            bool hasTraderRole = contact?.TraderKind != null;
            bool isRecruitable = contact?.guest?.Recruitable ?? true;
            HediffDef dialogueHediffDef = DefDatabase<HediffDef>
                .GetNamedSilentFail("SG1_TokraSafehouseContactDialogue");
            bool hasDialogueOutcome = dialogueHediffDef != null
                && contact?.health?.hediffSet
                    ?.GetFirstHediffOfDef(dialogueHediffDef) != null;
            bool passed = contacts.Count == 1
                && biologicalAge >= 20
                && hasAdultBackstory
                && !isHostile
                && !hasTraderRole
                && !isRecruitable
                && hasDialogueOutcome;

            string messageKey = passed
                ? "GR_TokraSafehouseDebug_ContactPassed"
                : "GR_TokraSafehouseDebug_ContactFailed";

            Messages.Message(
                messageKey.Translate(
                    contacts.Count.ToString().Named("CONTACTCOUNT"),
                    biologicalAge.ToString().Named("AGE"),
                    hasAdultBackstory.ToString().Named("ADULTBACKSTORY"),
                    isHostile.ToString().Named("HOSTILE"),
                    hasTraderRole.ToString().Named("TRADER"),
                    isRecruitable.ToString().Named("RECRUITABLE"),
                    hasDialogueOutcome.ToString().Named("DIALOGUE")),
                passed
                    ? MessageTypeDefOf.PositiveEvent
                    : MessageTypeDefOf.RejectInput,
                historical: false);
        }


        private static void AdjustTokraTrustForDebug(int requestedChange)
        {
            int previousScore = GameComponent_TokraTrustTracker
                .GetCurrentTrustScore();
            int targetScore = previousScore + requestedChange;

            if (targetScore > GameComponent_TokraTrustTracker.MaximumTrust)
            {
                targetScore = GameComponent_TokraTrustTracker.MaximumTrust;
            }
            else if (targetScore < GameComponent_TokraTrustTracker.MinimumTrust)
            {
                targetScore = GameComponent_TokraTrustTracker.MinimumTrust;
            }

            if (!GameComponent_TokraTrustTracker.DebugSetTrustScore(targetScore))
            {
                Messages.Message(
                    "GR_TokraSafehouseDebug_Unavailable".Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            int appliedChange = targetScore - previousScore;
            TokraTrustTier tier = GameComponent_TokraTrustTracker
                .GetCurrentTier();

            Messages.Message(
                "GR_TokraSafehouseDebug_TrustAdjusted"
                    .Translate(
                        targetScore.ToString(),
                        GetDebugTrustTierLabel(tier),
                        FormatSignedChange(appliedChange)),
                GetTrustDebugMessageType(appliedChange),
                historical: false);
        }

        private static string GetDebugTrustTierLabel(TokraTrustTier tier)
        {
            switch (tier)
            {
                case TokraTrustTier.Wary:
                    return "GR_TokraTrust_Tier_Wary".Translate().ToString();
                case TokraTrustTier.Cooperative:
                    return "GR_TokraTrust_Tier_Cooperative"
                        .Translate()
                        .ToString();
                case TokraTrustTier.Trusted:
                    return "GR_TokraTrust_Tier_Trusted".Translate().ToString();
                default:
                    return "GR_TokraTrust_Tier_Neutral".Translate().ToString();
            }
        }

        private static MessageTypeDef GetTrustDebugMessageType(int change)
        {
            if (change > 0)
            {
                return MessageTypeDefOf.PositiveEvent;
            }

            if (change < 0)
            {
                return MessageTypeDefOf.NegativeEvent;
            }

            return MessageTypeDefOf.NeutralEvent;
        }

        private static string FormatSignedChange(int change)
        {
            return change > 0 ? $"+{change}" : change.ToString();
        }

        private static List<WorldObject> GetActiveSafehouseWorldObjects()
        {
            List<WorldObject> safehouseObjects = new List<WorldObject>();

            if (Find.WorldObjects == null)
            {
                return safehouseObjects;
            }

            foreach (WorldObject worldObject in Find.WorldObjects.AllWorldObjects)
            {
                if (worldObject?.def == GR_DefOf.SG1_TokraHiddenSafehouseMarker
                    || worldObject?.def == GR_DefOf.SG1_TokraHiddenSafehouseSite)
                {
                    safehouseObjects.Add(worldObject);
                }
            }

            return safehouseObjects;
        }
    }
}
