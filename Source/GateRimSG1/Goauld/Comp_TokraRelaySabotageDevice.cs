using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace GateRimSG1.Goauld
{
    public class CompProperties_TokraRelaySabotageDevice : CompProperties
    {
        public CompProperties_TokraRelaySabotageDevice()
        {
            compClass = typeof(Comp_TokraRelaySabotageDevice);
        }
    }

    public class Comp_TokraRelaySabotageDevice : ThingComp
    {
        private const float TotalSabotageWork = 30000f;
        private const float BaseWorkPerTick = 1f;
        private const float IntellectualWorkPerLevel = 0.05f;

        private float sabotageWorkDone;

        public bool Sabotaged
        {
            get
            {
                MapComponent_TokraRelaySabotageMission component =
                    TokraRelaySabotageMissionUtility.GetComponent(parent.Map);

                return component != null && component.SabotageCompleted;
            }
        }

        public float SabotageProgress
        {
            get
            {
                if (Sabotaged)
                {
                    return 1f;
                }

                return Math.Max(
                    0f,
                    Math.Min(1f, sabotageWorkDone / TotalSabotageWork));
            }
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(
                ref sabotageWorkDone,
                "tokraRelaySabotageWorkDone",
                0f);
        }

        public override IEnumerable<FloatMenuOption> CompFloatMenuOptions(
            Pawn selPawn)
        {
            foreach (FloatMenuOption option in base.CompFloatMenuOptions(selPawn))
            {
                yield return option;
            }

            string label = "GR_TokraRelaySabotageDevice_FloatMenuLabel"
                .Translate()
                .ToString();

            if (!CanSabotage(selPawn))
            {
                yield return new FloatMenuOption(
                    label + ": "
                    + "GR_TokraRelaySabotageDevice_PawnUnavailable"
                        .Translate(),
                    null);
                yield break;
            }

            if (!CanUseIntellectual(selPawn))
            {
                yield return new FloatMenuOption(
                    label + ": "
                    + "GR_TokraRelaySabotageDevice_IntellectualRequired"
                        .Translate(),
                    null);
                yield break;
            }

            if (Sabotaged)
            {
                yield return new FloatMenuOption(
                    label + ": "
                    + "GR_TokraRelaySabotageDevice_AlreadySabotaged"
                        .Translate(),
                    null);
                yield break;
            }

            if (!selPawn.CanReach(parent, PathEndMode.Touch, Danger.Deadly))
            {
                yield return new FloatMenuOption(
                    label + ": "
                    + "GR_TokraRelaySabotageDevice_CannotReach"
                        .Translate(),
                    null);
                yield break;
            }

            if (GR_DefOf.SG1_TokraSabotageRelayDevice == null)
            {
                yield return new FloatMenuOption(
                    label + ": "
                    + "GR_TokraRelaySabotageDevice_JobUnavailable"
                        .Translate(),
                    null);
                yield break;
            }

            yield return new FloatMenuOption(
                label,
                delegate
                {
                    Job job = JobMaker.MakeJob(
                        GR_DefOf.SG1_TokraSabotageRelayDevice,
                        parent);
                    selPawn.jobs.TryTakeOrderedJob(job);
                });
        }

        public void NotifySabotageStarted()
        {
            TokraRelaySabotageMissionUtility
                .GetComponent(parent.Map)
                ?.NotifySabotageStarted();
        }

        public bool PerformSabotageWork(Pawn worker)
        {
            if (Sabotaged)
            {
                return true;
            }

            sabotageWorkDone = Math.Min(
                TotalSabotageWork,
                sabotageWorkDone + CalculateWorkPerTick(worker));

            return sabotageWorkDone >= TotalSabotageWork;
        }

        public void CompleteSabotage(Pawn saboteur)
        {
            sabotageWorkDone = TotalSabotageWork;

            TokraRelaySabotageMissionUtility
                .GetComponent(parent.Map)
                ?.NotifySabotageCompleted(saboteur);

            parent.HitPoints = 1;
        }

        private static float CalculateWorkPerTick(Pawn worker)
        {
            int intellectualLevel = GetIntellectualLevel(worker);
            return BaseWorkPerTick
                + intellectualLevel * IntellectualWorkPerLevel;
        }

        private static int GetIntellectualLevel(Pawn worker)
        {
            if (worker?.skills == null)
            {
                return 0;
            }

            SkillRecord intellectual = worker.skills.GetSkill(
                SkillDefOf.Intellectual);

            if (intellectual == null || intellectual.TotallyDisabled)
            {
                return 0;
            }

            return intellectual.Level;
        }

        private static bool CanSabotage(Pawn pawn)
        {
            return pawn != null
                && !pawn.Dead
                && !pawn.Downed
                && pawn.Faction == Faction.OfPlayer
                && pawn.RaceProps?.Humanlike == true;
        }

        private static bool CanUseIntellectual(Pawn pawn)
        {
            if (pawn?.skills == null)
            {
                return false;
            }

            SkillRecord intellectual = pawn.skills.GetSkill(
                SkillDefOf.Intellectual);

            return intellectual != null && !intellectual.TotallyDisabled;
        }
    }
}
