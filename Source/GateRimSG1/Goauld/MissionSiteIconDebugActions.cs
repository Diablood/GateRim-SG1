using System;
using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    public static class MissionSiteIconDebugActions
    {
        private const string DecodedMissionWorldSiteDefName
            = "SG1_TokraDecodedMissionWorldSite";

        public static void CreateHiddenSafehouseMarker()
        {
            Run(
                TryCreateSafehouseIncident(
                    GR_DefOf.SG1_TokraHiddenSafehouseWorldMarker),
                "Created icon-test marker: "
                + "SG1_TokraHiddenSafehouseMarker. It replaces the other "
                + "safehouse stage by design.");
        }

        public static void CreateHiddenSafehouseSite()
        {
            Run(
                TryCreateSafehouseIncident(
                    GR_DefOf.SG1_TokraHiddenSafehouseSiteIncident),
                "Created icon-test site: SG1_TokraHiddenSafehouseSite. It "
                + "replaces the other safehouse stage by design.");
        }

        public static void CreateIntroductionArtifactSite()
        {
            Run(
                TryCreateIntroductionArtifactSite(),
                "Created icon-test site: "
                + "SG1_TokraIntroductionArtifactWorldSite.");
        }

        public static void CreateDecodedMissionWorldSite()
        {
            Run(
                TryCreateDecodedMissionWorldSite(),
                "Created or reused icon-test site: "
                + "SG1_TokraDecodedMissionWorldSite.");
        }

        public static void CreateDistressCallSite()
        {
            Run(
                TryCreateAcceptedOrganicSite(
                    map => GameComponent_TokraOrganicOperationManager
                        .DebugForceDistressCallOpportunity(
                            map,
                            TokraDistressCallVariant.GenuineRescue)),
                "Created icon-test site: "
                + "SG1_TokraDistressCallWorldSite. It replaces the active "
                + "organic operation by design.");
        }

        public static void CreateTemporaryBaseDeliverySite()
        {
            Run(
                TryCreateAcceptedOrganicSite(
                    GameComponent_TokraOrganicOperationManager
                        .DebugForceTemporaryBaseDeliveryOpportunity),
                "Created icon-test site: "
                + "SG1_TokraTemporaryBaseDeliverySite. It replaces the "
                + "active organic operation by design.");
        }

        public static void CreateJaffaOfficerCaptureSite()
        {
            Run(
                TryCreateAcceptedOrganicSite(
                    GameComponent_TokraOrganicOperationManager
                        .DebugForceJaffaOfficerCaptureOpportunity),
                "Created icon-test site: "
                + "SG1_TokraJaffaOfficerCaptureSite. It replaces the active "
                + "organic operation by design.");
        }

        private static bool TryCreateSafehouseIncident(IncidentDef incidentDef)
        {
            Map map = Find.CurrentMap;

            if (map == null
                || incidentDef?.Worker == null
                || !TokraFactionUtility.HasPersistentFaction()
                || !TryClearSafehouseWorldObjects())
            {
                return false;
            }

            if (GameComponent_TokraTrustTracker.GetCurrentTrustScore() < 0
                && !GameComponent_TokraTrustTracker.DebugSetTrustScore(0))
            {
                return false;
            }

            if (!GameComponent_TokraSafehouseLeadTracker.DebugSetLeadCount(1))
            {
                return false;
            }

            IncidentParms parms = new IncidentParms
            {
                target = map,
                forced = true
            };

            return incidentDef.Worker.TryExecute(parms);
        }

        private static bool TryCreateIntroductionArtifactSite()
        {
            Map map = Find.CurrentMap;

            if (map == null || !TokraFactionUtility.HasPersistentFaction())
            {
                return false;
            }

            GameComponent_TokraIntroductionArc.DebugReset();

            return GameComponent_TokraIntroductionArc.DebugForceOffer(map)
                && GameComponent_TokraIntroductionArc.DebugAcceptOffer(map);
        }

        private static bool TryCreateDecodedMissionWorldSite()
        {
            Map map = Find.CurrentMap;
            WorldObjectDef siteDef = DefDatabase<WorldObjectDef>
                .GetNamedSilentFail(DecodedMissionWorldSiteDefName);

            if (map == null
                || siteDef == null
                || !TokraFactionUtility.HasPersistentFaction()
                || !TryClearWorldObjects(siteDef))
            {
                return false;
            }

            if (GameComponent_TokraTrustTracker.GetCurrentTrustScore()
                    < GameComponent_TokraTrustTracker.TrustedThreshold
                && !GameComponent_TokraTrustTracker.DebugSetTrustScore(
                    GameComponent_TokraTrustTracker.TrustedThreshold))
            {
                return false;
            }

            return GameComponent_TokraTrustTracker
                .DebugRevealFirstTrustMissionWorldSite(map);
        }

        private static bool TryCreateAcceptedOrganicSite(
            Func<Map, bool> forceOffer)
        {
            Map map = Find.CurrentMap;

            if (map == null
                || forceOffer == null
                || !TokraFactionUtility.HasPersistentFaction())
            {
                return false;
            }

            return forceOffer(map)
                && GameComponent_TokraOrganicOperationManager
                    .DebugAcceptActiveOffer(map);
        }

        private static bool TryClearSafehouseWorldObjects()
        {
            return TryClearWorldObjects(
                GR_DefOf.SG1_TokraHiddenSafehouseMarker,
                GR_DefOf.SG1_TokraHiddenSafehouseSite);
        }

        private static bool TryClearWorldObjects(
            params WorldObjectDef[] worldObjectDefs)
        {
            if (Find.WorldObjects == null)
            {
                return false;
            }

            List<WorldObject> objectsToDestroy = new List<WorldObject>();

            foreach (WorldObject worldObject in Find.WorldObjects
                .AllWorldObjects)
            {
                if (worldObject == null
                    || worldObject.Destroyed
                    || !ContainsDef(worldObjectDefs, worldObject.def))
                {
                    continue;
                }

                if (worldObject is MapParent mapParent && mapParent.HasMap)
                {
                    return false;
                }

                objectsToDestroy.Add(worldObject);
            }

            foreach (WorldObject worldObject in objectsToDestroy)
            {
                worldObject.Destroy();
            }

            return true;
        }

        private static bool ContainsDef(
            WorldObjectDef[] defs,
            WorldObjectDef targetDef)
        {
            if (defs == null || targetDef == null)
            {
                return false;
            }

            for (int index = 0; index < defs.Length; index++)
            {
                if (defs[index] == targetDef)
                {
                    return true;
                }
            }

            return false;
        }

        private static void Run(bool succeeded, string successMessage)
        {
            Messages.Message(
                succeeded
                    ? successMessage
                    : "Mission-site icon test failed. Check that the "
                    + "Tok'ra faction exists, the colony map is active, no "
                    + "matching test site map is currently open, and organic "
                    + "operation tests have a powered Tok'ra secure "
                    + "communicator.",
                succeeded
                    ? MessageTypeDefOf.PositiveEvent
                    : MessageTypeDefOf.RejectInput,
                historical: false);
        }
    }
}
