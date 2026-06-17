using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public class Alert_TokraRelayReinforcementsInbound : Alert
    {
        public Alert_TokraRelayReinforcementsInbound()
        {
            defaultLabel = "GR_TokraRelaySabotageMission_ReinforcementAlertLabel"
                .Translate("?");
            defaultExplanation = "GR_TokraRelaySabotageMission_ReinforcementAlertDesc"
                .Translate();
        }

        public override AlertReport GetReport()
        {
            MapComponent_TokraRelaySabotageMission component =
                GetActiveMissionComponent();

            if (component == null)
            {
                return false;
            }

            defaultLabel = "GR_TokraRelaySabotageMission_ReinforcementAlertLabel"
                .Translate(component.FormatRemainingReinforcementTime());
            defaultExplanation = "GR_TokraRelaySabotageMission_ReinforcementAlertDesc"
                .Translate();

            return true;
        }

        private static MapComponent_TokraRelaySabotageMission
            GetActiveMissionComponent()
        {
            if (Find.Maps == null)
            {
                return null;
            }

            List<Map> maps = Find.Maps;

            for (int index = 0; index < maps.Count; index++)
            {
                MapComponent_TokraRelaySabotageMission component =
                    TokraRelaySabotageMissionUtility.GetComponent(maps[index]);

                if (component != null
                    && component.ReinforcementTimerStarted
                    && !component.ReinforcementsArrived
                    && component.RemainingReinforcementTicks > 0)
                {
                    return component;
                }
            }

            return null;
        }
    }
}
