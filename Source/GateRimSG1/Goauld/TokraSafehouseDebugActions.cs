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

            bool trustPrepared
                = GameComponent_TokraTrustTracker.DebugSetTrustScore(0);
            bool leadPrepared
                = GameComponent_TokraSafehouseLeadTracker.DebugSetLeadCount(1);

            if (!trustPrepared || !leadPrepared)
            {
                Messages.Message(
                    "GR_TokraSafehouseDebug_Unavailable".Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            Messages.Message(
                "GR_TokraSafehouseDebug_Prepared".Translate(),
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
