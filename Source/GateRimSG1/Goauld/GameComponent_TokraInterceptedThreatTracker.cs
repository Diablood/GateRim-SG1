using System;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Persistent Tok'ra intercepted-threat state.
    ///
    /// The interception creates advance warning before a delayed Goa'uld/Jaffa
    /// attack. It intentionally stores only a small amount of strategic
    /// information: enough to make tactical assessment meaningful without
    /// revealing full raid composition or creating direct Tok'ra aid.
    /// </summary>
    public class GameComponent_TokraInterceptedThreatTracker : GameComponent
    {
        private const int MinimumAttackDelayTicks = 60000;
        private const int MaximumAttackDelayTicks = 180000;
        private const int DebugAttackDelayTicks = 5000;
        private const int TriggerCheckIntervalTicks = 2500;
        private const float DefaultRaidPoints = 500f;
        private const string DelayedRaidIncidentDefName = "SG1_GoauldJaffaNaturalRaid";

        private bool interceptedThreatActive;
        private bool tacticalAssessmentRequested;
        private int interceptedThreatMapUniqueId = -1;
        private int interceptedThreatStartTick;
        private int interceptedThreatAttackTick;
        private int nextTriggerCheckTick;
        private int interceptedThreatKind;
        private float interceptedThreatPoints;

        public GameComponent_TokraInterceptedThreatTracker(Game game)
        {
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(
                ref interceptedThreatActive,
                "tokraInterceptedThreatActive",
                false);
            Scribe_Values.Look(
                ref tacticalAssessmentRequested,
                "tokraInterceptedThreatAssessmentRequested",
                false);
            Scribe_Values.Look(
                ref interceptedThreatMapUniqueId,
                "tokraInterceptedThreatMapUniqueId",
                -1);
            Scribe_Values.Look(
                ref interceptedThreatStartTick,
                "tokraInterceptedThreatStartTick",
                0);
            Scribe_Values.Look(
                ref interceptedThreatAttackTick,
                "tokraInterceptedThreatAttackTick",
                0);
            Scribe_Values.Look(
                ref nextTriggerCheckTick,
                "tokraInterceptedThreatNextTriggerCheckTick",
                0);
            Scribe_Values.Look(
                ref interceptedThreatKind,
                "tokraInterceptedThreatKind",
                0);
            Scribe_Values.Look(
                ref interceptedThreatPoints,
                "tokraInterceptedThreatPoints",
                DefaultRaidPoints);
        }

        public override void GameComponentTick()
        {
            base.GameComponentTick();

            TryTriggerDueInterceptedThreat();
        }

        public static bool HasActiveThreat()
        {
            GameComponent_TokraInterceptedThreatTracker tracker
                = GetCurrentTracker();

            return tracker != null && tracker.interceptedThreatActive;
        }

        public static bool HasActiveThreatForMap(Map map)
        {
            GameComponent_TokraInterceptedThreatTracker tracker
                = GetCurrentTracker();

            return tracker != null
                && tracker.interceptedThreatActive
                && map != null
                && tracker.interceptedThreatMapUniqueId == map.uniqueID;
        }

        public static bool TryStartInterceptedThreat(
            Map map,
            bool debugShortDelay = false)
        {
            GameComponent_TokraInterceptedThreatTracker tracker
                = GetCurrentTracker();

            if (tracker == null || map == null || Find.TickManager == null)
            {
                return false;
            }

            if (tracker.interceptedThreatActive)
            {
                return false;
            }

            int currentTick = Find.TickManager.TicksGame;
            int delayTicks = debugShortDelay
                ? DebugAttackDelayTicks
                : Rand.RangeInclusive(
                    MinimumAttackDelayTicks,
                    MaximumAttackDelayTicks);

            tracker.interceptedThreatActive = true;
            tracker.tacticalAssessmentRequested = false;
            tracker.interceptedThreatMapUniqueId = map.uniqueID;
            tracker.interceptedThreatStartTick = currentTick;
            tracker.interceptedThreatAttackTick = currentTick + delayTicks;
            tracker.nextTriggerCheckTick = currentTick + TriggerCheckIntervalTicks;
            tracker.interceptedThreatKind = Rand.RangeInclusive(0, 1);
            tracker.interceptedThreatPoints = DefaultRaidPoints;

            GR_Log.Message(
                "Tok'ra intercepted threat scheduled: "
                + $"map {map.uniqueID}, delay {delayTicks} tick(s), "
                + $"kind {tracker.interceptedThreatKind}, "
                + $"points {tracker.interceptedThreatPoints:0}.");

            return true;
        }

        public static bool DebugClearInterceptedThreat()
        {
            GameComponent_TokraInterceptedThreatTracker tracker
                = GetCurrentTracker();

            if (tracker == null || !tracker.interceptedThreatActive)
            {
                return false;
            }

            tracker.ClearInterceptedThreat();
            return true;
        }

        public static void NotifyTacticalAssessmentRequested(Map map)
        {
            GameComponent_TokraInterceptedThreatTracker tracker
                = GetCurrentTracker();

            if (tracker == null
                || !tracker.interceptedThreatActive
                || map == null
                || tracker.interceptedThreatMapUniqueId != map.uniqueID)
            {
                return;
            }

            tracker.tacticalAssessmentRequested = true;
        }

        public static string GetStatusReportLabelForMap(Map map)
        {
            if (!HasActiveThreatForMap(map))
            {
                return "GR_TokraInterceptedThreat_StatusNone"
                    .Translate()
                    .ToString();
            }

            if (GetCurrentTracker()?.tacticalAssessmentRequested == true)
            {
                return "GR_TokraInterceptedThreat_StatusAssessed"
                    .Translate(GetRemainingThreatWindowLabelForMap(map))
                    .ToString();
            }

            return "GR_TokraInterceptedThreat_StatusActive"
                .Translate(GetRemainingThreatWindowLabelForMap(map))
                .ToString();
        }

        public static string GetAlertLabel()
        {
            return "GR_TokraInterceptedThreat_AlertLabel".Translate().ToString();
        }

        public static string GetAlertExplanation()
        {
            GameComponent_TokraInterceptedThreatTracker tracker
                = GetCurrentTracker();

            if (tracker == null || !tracker.interceptedThreatActive)
            {
                return string.Empty;
            }

            return "GR_TokraInterceptedThreat_AlertExplanation".Translate(
                tracker.GetThreatSignatureLabel(),
                tracker.GetThreatIntentLabel(),
                tracker.GetRemainingThreatWindowLabel(),
                tracker.GetThreatPreparationAdviceLabel()).ToString();
        }

        public static string GetThreatSignatureLabelForMap(Map map)
        {
            GameComponent_TokraInterceptedThreatTracker tracker
                = GetCurrentTracker();

            if (tracker == null || !tracker.IsActiveForMap(map))
            {
                return "GR_TokraInterceptedThreat_SignatureUnknown"
                    .Translate()
                    .ToString();
            }

            return tracker.GetThreatSignatureLabel();
        }

        public static string GetThreatIntentLabelForMap(Map map)
        {
            GameComponent_TokraInterceptedThreatTracker tracker
                = GetCurrentTracker();

            if (tracker == null || !tracker.IsActiveForMap(map))
            {
                return "GR_TokraInterceptedThreat_IntentUnknown"
                    .Translate()
                    .ToString();
            }

            return tracker.GetThreatIntentLabel();
        }

        public static string GetThreatSeverityLabelForMap(Map map)
        {
            GameComponent_TokraInterceptedThreatTracker tracker
                = GetCurrentTracker();

            if (tracker == null || !tracker.IsActiveForMap(map))
            {
                return "GR_TokraSecureCommunicator_ThreatSeverityLimited"
                    .Translate()
                    .ToString();
            }

            return tracker.GetThreatSeverityLabel();
        }

        public static string GetRemainingThreatWindowLabelForMap(Map map)
        {
            GameComponent_TokraInterceptedThreatTracker tracker
                = GetCurrentTracker();

            if (tracker == null || !tracker.IsActiveForMap(map))
            {
                return "GR_TokraInterceptedThreat_WindowUnknown"
                    .Translate()
                    .ToString();
            }

            return tracker.GetRemainingThreatWindowLabel();
        }

        public static string GetThreatPreparationAdviceLabelForMap(Map map)
        {
            GameComponent_TokraInterceptedThreatTracker tracker
                = GetCurrentTracker();

            if (tracker == null || !tracker.IsActiveForMap(map))
            {
                return "GR_TokraInterceptedThreat_AdviceGeneral"
                    .Translate()
                    .ToString();
            }

            return tracker.GetThreatPreparationAdviceLabel();
        }

        private void TryTriggerDueInterceptedThreat()
        {
            if (!interceptedThreatActive || Find.TickManager == null)
            {
                return;
            }

            int currentTick = Find.TickManager.TicksGame;

            if (currentTick < nextTriggerCheckTick)
            {
                return;
            }

            nextTriggerCheckTick = currentTick + TriggerCheckIntervalTicks;

            if (currentTick < interceptedThreatAttackTick)
            {
                return;
            }

            Map targetMap = GetTargetMap();

            if (targetMap == null)
            {
                GR_Log.Warning(
                    "Clearing Tok'ra intercepted threat: target map is no "
                    + "longer available.");
                ClearInterceptedThreat();
                return;
            }

            if (TryStartDelayedRaid(targetMap))
            {
                Messages.Message(
                    "GR_TokraInterceptedThreat_AttackStarted".Translate(),
                    MessageTypeDefOf.NegativeEvent,
                    historical: true);
            }
            else
            {
                Messages.Message(
                    "GR_TokraInterceptedThreat_AttackFailed".Translate(),
                    MessageTypeDefOf.NeutralEvent,
                    historical: true);
            }

            ClearInterceptedThreat();
        }

        private bool TryStartDelayedRaid(Map targetMap)
        {
            IncidentDef raidIncident = DefDatabase<IncidentDef>
                .GetNamedSilentFail(DelayedRaidIncidentDefName);

            if (raidIncident?.Worker == null)
            {
                GR_Log.Error(
                    "Cannot start Tok'ra-intercepted Goa'uld raid: incident "
                    + $"{DelayedRaidIncidentDefName} is unavailable.");
                return false;
            }

            IncidentParms parms = new IncidentParms
            {
                target = targetMap,
                forced = true,
                points = interceptedThreatPoints
            };

            bool succeeded = raidIncident.Worker.TryExecute(parms);

            GR_Log.Message(
                "Tok'ra intercepted threat triggered delayed raid: "
                + $"success={succeeded}, map={targetMap.uniqueID}, "
                + $"points={interceptedThreatPoints:0}.");

            return succeeded;
        }

        private Map GetTargetMap()
        {
            if (Find.Maps == null)
            {
                return null;
            }

            for (int i = 0; i < Find.Maps.Count; i++)
            {
                Map map = Find.Maps[i];

                if (map != null && map.uniqueID == interceptedThreatMapUniqueId)
                {
                    return map;
                }
            }

            return null;
        }

        private void ClearInterceptedThreat()
        {
            interceptedThreatActive = false;
            tacticalAssessmentRequested = false;
            interceptedThreatMapUniqueId = -1;
            interceptedThreatStartTick = 0;
            interceptedThreatAttackTick = 0;
            nextTriggerCheckTick = 0;
            interceptedThreatKind = 0;
            interceptedThreatPoints = DefaultRaidPoints;
        }

        private bool IsActiveForMap(Map map)
        {
            return interceptedThreatActive
                && map != null
                && interceptedThreatMapUniqueId == map.uniqueID;
        }

        private string GetThreatSignatureLabel()
        {
            if (interceptedThreatKind == 1)
            {
                return "GR_TokraInterceptedThreat_SignatureJaffaAssault"
                    .Translate()
                    .ToString();
            }

            return "GR_TokraInterceptedThreat_SignatureJaffaPatrol"
                .Translate()
                .ToString();
        }

        private string GetThreatIntentLabel()
        {
            if (interceptedThreatKind == 1)
            {
                return "GR_TokraInterceptedThreat_IntentDirectAssault"
                    .Translate()
                    .ToString();
            }

            return "GR_TokraInterceptedThreat_IntentPunitiveProbe"
                .Translate()
                .ToString();
        }

        private string GetThreatSeverityLabel()
        {
            if (interceptedThreatKind == 1)
            {
                return "GR_TokraSecureCommunicator_ThreatSeverityModerate"
                    .Translate()
                    .ToString();
            }

            return "GR_TokraSecureCommunicator_ThreatSeverityLimited"
                .Translate()
                .ToString();
        }

        private string GetThreatPreparationAdviceLabel()
        {
            if (interceptedThreatKind == 1)
            {
                return "GR_TokraInterceptedThreat_AdviceAssault"
                    .Translate()
                    .ToString();
            }

            return "GR_TokraInterceptedThreat_AdvicePatrol"
                .Translate()
                .ToString();
        }

        private string GetRemainingThreatWindowLabel()
        {
            int currentTick = Find.TickManager?.TicksGame ?? 0;
            int remainingTicks = Math.Max(
                0,
                interceptedThreatAttackTick - currentTick);
            float remainingDays = remainingTicks / 60000f;

            if (remainingDays >= 1f)
            {
                return "GR_TokraInterceptedThreat_WindowDays"
                    .Translate(remainingDays.ToString("0.#"))
                    .ToString();
            }

            return "GR_TokraInterceptedThreat_WindowSoon".Translate().ToString();
        }

        private static GameComponent_TokraInterceptedThreatTracker GetCurrentTracker()
        {
            return Current.Game?.GetComponent<GameComponent_TokraInterceptedThreatTracker>();
        }
    }
}
