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


        [DebugAction(
            "GateRim SG-1",
            "Deliver Tok'ra first mission cache test",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void DeliverTokraFirstMissionCacheTest()
        {
            Map map = Find.CurrentMap;

            if (map == null)
            {
                Messages.Message(
                    "GR_TokraSafehouseDebug_Unavailable".Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            GameComponent_TokraTrustTracker.DebugSetTrustScore(
                GameComponent_TokraTrustTracker.TrustedThreshold);

            if (!GameComponent_TokraTrustTracker
                .DebugMarkFirstTrustMissionBriefingReceived())
            {
                Messages.Message(
                    "GR_TokraFirstMissionCache_DebugDeliveryFailed".Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            if (!GameComponent_TokraTrustTracker
                .DebugDeliverFirstTrustMissionCache(map))
            {
                Messages.Message(
                    "GR_TokraFirstMissionCache_DebugDeliveryFailed".Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            Messages.Message(
                "GR_TokraFirstMissionCache_DebugDelivered".Translate(),
                MessageTypeDefOf.PositiveEvent,
                historical: false);
        }


        [DebugAction(
            "GateRim SG-1",
            "Reset Tok'ra first mission cache test",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ResetTokraFirstMissionCacheTest()
        {
            if (!GameComponent_TokraTrustTracker
                    .DebugResetFirstTrustMissionCacheDelivery())
            {
                Messages.Message(
                    "GR_TokraFirstMissionCache_DebugDeliveryFailed".Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            Messages.Message(
                "Tok'ra first mission cache test state reset.",
                MessageTypeDefOf.PositiveEvent,
                historical: false);
        }



        [DebugAction(
            "GateRim SG-1",
            "Decode Tok'ra mission lead test",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void DecodeTokraMissionLeadTest()
        {
            GameComponent_TokraTrustTracker.DebugSetTrustScore(
                GameComponent_TokraTrustTracker.TrustedThreshold);

            if (!GameComponent_TokraTrustTracker
                    .DebugMarkFirstTrustMissionLeadDecoded())
            {
                Messages.Message(
                    "GR_TokraDecodedMissionLead_DebugFailed".Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            Messages.Message(
                "GR_TokraDecodedMissionLead_DebugDecoded".Translate(),
                MessageTypeDefOf.PositiveEvent,
                historical: false);
        }


        [DebugAction(
            "GateRim SG-1",
            "Reveal Tok'ra decoded mission world site test",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void RevealTokraDecodedMissionWorldSiteTest()
        {
            Map map = Find.CurrentMap;

            if (map == null)
            {
                Messages.Message(
                    "GR_TokraSafehouseDebug_Unavailable".Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            GameComponent_TokraTrustTracker.DebugSetTrustScore(
                GameComponent_TokraTrustTracker.TrustedThreshold);

            if (!GameComponent_TokraTrustTracker
                    .DebugRevealFirstTrustMissionWorldSite(map))
            {
                Messages.Message(
                    "GR_TokraDecodedMissionWorldSite_DebugFailed".Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            Messages.Message(
                "GR_TokraDecodedMissionWorldSite_DebugRevealed".Translate(),
                MessageTypeDefOf.PositiveEvent,
                historical: false);
        }


        [DebugAction(
            "GateRim SG-1",
            "Recon Tok'ra decoded mission world site test",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ReconTokraDecodedMissionWorldSiteTest()
        {
            GameComponent_TokraTrustTracker.DebugSetTrustScore(
                GameComponent_TokraTrustTracker.TrustedThreshold);

            if (!GameComponent_TokraTrustTracker
                    .DebugReconnoiterFirstTrustMissionWorldSite())
            {
                Messages.Message(
                    "GR_TokraDecodedMissionWorldSite_ReconDebugFailed"
                        .Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            Messages.Message(
                "GR_TokraDecodedMissionWorldSite_ReconDebugComplete".Translate(),
                MessageTypeDefOf.PositiveEvent,
                historical: false);
        }


        [DebugAction(
            "GateRim SG-1",
            "Prepare Tok'ra relay sabotage objective test",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void PrepareTokraRelaySabotageObjectiveTest()
        {
            GameComponent_TokraTrustTracker.DebugSetTrustScore(
                GameComponent_TokraTrustTracker.TrustedThreshold);

            if (!GameComponent_TokraTrustTracker
                    .DebugPrepareFirstTrustMissionRelaySabotageObjective())
            {
                Messages.Message(
                    "GR_TokraDecodedMissionWorldSite_SabotageDebugFailed"
                        .Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            Messages.Message(
                "GR_TokraDecodedMissionWorldSite_SabotageDebugComplete"
                    .Translate(),
                MessageTypeDefOf.PositiveEvent,
                historical: false);
        }




        [DebugAction(
            "GateRim SG-1",
            "Complete Tok'ra relay sabotage test",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void CompleteTokraRelaySabotageTest()
        {
            GameComponent_TokraTrustTracker.DebugSetTrustScore(
                GameComponent_TokraTrustTracker.TrustedThreshold);

            if (!GameComponent_TokraTrustTracker
                    .DebugCompleteFirstTrustMissionRelaySabotage())
            {
                Messages.Message(
                    "GR_TokraRelaySabotageMission_DebugCompleteFailed"
                        .Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            Messages.Message(
                "GR_TokraRelaySabotageMission_DebugCompleted".Translate(),
                MessageTypeDefOf.PositiveEvent,
                historical: false);
        }


        [DebugAction(
            "GateRim SG-1",
            "Create Tok'ra intercepted threat test",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void CreateTokraInterceptedThreatTest()
        {
            Map map = Find.CurrentMap;

            if (map == null)
            {
                Messages.Message(
                    "GR_TokraSafehouseDebug_Unavailable".Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            GameComponent_TokraTrustTracker.DebugSetTrustScore(
                GameComponent_TokraTrustTracker.TrustedThreshold);

            if (!GameComponent_TokraInterceptedThreatTracker
                .TryStartInterceptedThreat(map, debugShortDelay: true))
            {
                Messages.Message(
                    "GR_TokraInterceptedThreat_DebugCreateFailed".Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            SendTokraInterceptedThreatDebugLetter(map);

            Messages.Message(
                "GR_TokraInterceptedThreat_DebugCreated".Translate(),
                MessageTypeDefOf.PositiveEvent,
                historical: false);
        }

        [DebugAction(
            "GateRim SG-1",
            "Clear Tok'ra intercepted threat test",
            actionType = DebugActionType.Action,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ClearTokraInterceptedThreatTest()
        {
            if (!GameComponent_TokraInterceptedThreatTracker
                .DebugClearInterceptedThreat())
            {
                Messages.Message(
                    "GR_TokraInterceptedThreat_DebugClearFailed".Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            Messages.Message(
                "GR_TokraInterceptedThreat_DebugCleared".Translate(),
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        private static void SendTokraInterceptedThreatDebugLetter(Map map)
        {
            if (map == null || Find.LetterStack == null)
            {
                return;
            }

            Find.LetterStack.ReceiveLetter(
                "GR_TokraInterceptedThreat_LetterLabel".Translate(),
                "GR_TokraInterceptedThreat_LetterText".Translate(
                    GameComponent_TokraInterceptedThreatTracker
                        .GetRemainingThreatWindowLabelForMap(map),
                    GameComponent_TokraInterceptedThreatTracker
                        .GetThreatSignatureLabelForMap(map)),
                LetterDefOf.ThreatSmall);
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
