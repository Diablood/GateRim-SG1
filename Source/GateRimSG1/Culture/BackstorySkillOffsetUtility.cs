using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace GateRimSG1.Culture
{
    /// <summary>
    /// Keeps one shared skill progression while reversible backstory profiles
    /// expose different skill-level offsets on the same pawn.
    /// </summary>
    public static class BackstorySkillOffsetUtility
    {
        public static void SwitchBackstories(
            Pawn pawn,
            BackstoryDef sourceChildhood,
            BackstoryDef sourceAdulthood,
            BackstoryDef targetChildhood,
            BackstoryDef targetAdulthood,
            ref List<BackstorySkillProgressState> progressStates)
        {
            if (pawn?.skills == null || pawn.story == null)
            {
                return;
            }

            BackstoryDef resolvedTargetChildhood = targetChildhood
                ?? sourceChildhood
                ?? pawn.story.Childhood;
            BackstoryDef resolvedTargetAdulthood = targetAdulthood
                ?? sourceAdulthood
                ?? pawn.story.Adulthood;

            Dictionary<SkillDef, int> sourceOffsets = CollectSkillOffsets(
                sourceChildhood,
                sourceAdulthood);
            Dictionary<SkillDef, int> targetOffsets = CollectSkillOffsets(
                resolvedTargetChildhood,
                resolvedTargetAdulthood);

            EnsureProgressStates(pawn, sourceOffsets, ref progressStates);
            SynchronizeSharedProgress(pawn, progressStates);

            if (resolvedTargetChildhood != null
                && pawn.story.Childhood != resolvedTargetChildhood)
            {
                pawn.story.Childhood = resolvedTargetChildhood;
            }

            if (resolvedTargetAdulthood != null
                && pawn.story.Adulthood != resolvedTargetAdulthood)
            {
                pawn.story.Adulthood = resolvedTargetAdulthood;
            }

            ApplyOffsets(pawn, targetOffsets, progressStates);
            pawn.Notify_DisabledWorkTypesChanged();
        }

        public static void Reset(ref List<BackstorySkillProgressState> progressStates)
        {
            progressStates?.Clear();
        }

        private static Dictionary<SkillDef, int> CollectSkillOffsets(
            BackstoryDef childhood,
            BackstoryDef adulthood)
        {
            var result = new Dictionary<SkillDef, int>();
            AddSkillOffsets(result, childhood);
            AddSkillOffsets(result, adulthood);
            return result;
        }

        private static void AddSkillOffsets(
            Dictionary<SkillDef, int> result,
            BackstoryDef backstory)
        {
            if (backstory?.skillGains == null)
            {
                return;
            }

            foreach (SkillGain skillGain in backstory.skillGains)
            {
                if (skillGain?.skill == null)
                {
                    continue;
                }

                result.TryGetValue(skillGain.skill, out int currentAmount);
                result[skillGain.skill] = currentAmount + skillGain.amount;
            }
        }

        private static void EnsureProgressStates(
            Pawn pawn,
            Dictionary<SkillDef, int> activeOffsets,
            ref List<BackstorySkillProgressState> progressStates)
        {
            if (progressStates == null)
            {
                progressStates = new List<BackstorySkillProgressState>();
            }

            foreach (SkillRecord skillRecord in pawn.skills.skills)
            {
                BackstorySkillProgressState state = progressStates.Find(
                    candidate => candidate?.Skill == skillRecord.def);

                if (state != null)
                {
                    continue;
                }

                activeOffsets.TryGetValue(skillRecord.def, out int activeOffset);
                progressStates.Add(
                    BackstorySkillProgressState.Create(skillRecord, activeOffset));
            }
        }

        private static void SynchronizeSharedProgress(
            Pawn pawn,
            List<BackstorySkillProgressState> progressStates)
        {
            foreach (BackstorySkillProgressState state in progressStates)
            {
                if (state?.Skill == null)
                {
                    continue;
                }

                SkillRecord skillRecord = pawn.skills.GetSkill(state.Skill);
                float currentTotal = TotalExperience(
                    skillRecord.levelInt,
                    skillRecord.xpSinceLastLevel);
                float earnedSinceLastApplication = currentTotal - state.LastAppliedTotalXp;

                state.SharedTotalXp = ClampTotalExperience(
                    state.SharedTotalXp + earnedSinceLastApplication);
            }
        }

        private static void ApplyOffsets(
            Pawn pawn,
            Dictionary<SkillDef, int> targetOffsets,
            List<BackstorySkillProgressState> progressStates)
        {
            foreach (BackstorySkillProgressState state in progressStates)
            {
                if (state?.Skill == null)
                {
                    continue;
                }

                SkillRecord skillRecord = pawn.skills.GetSkill(state.Skill);
                ConvertTotalExperience(
                    state.SharedTotalXp,
                    out int sharedLevel,
                    out float sharedXp);

                targetOffsets.TryGetValue(state.Skill, out int targetOffset);
                int targetLevel = Mathf.Clamp(
                    sharedLevel + targetOffset,
                    SkillRecord.MinLevel,
                    SkillRecord.MaxLevel);
                float targetXp = ScaleProgress(sharedLevel, sharedXp, targetLevel);

                skillRecord.levelInt = targetLevel;
                skillRecord.xpSinceLastLevel = targetXp;
                state.LastAppliedTotalXp = TotalExperience(targetLevel, targetXp);
            }
        }

        private static float ScaleProgress(
            int sourceLevel,
            float sourceXp,
            int targetLevel)
        {
            if (targetLevel >= SkillRecord.MaxLevel)
            {
                return 0f;
            }

            float sourceRequired = SkillRecord.XpRequiredToLevelUpFrom(sourceLevel);
            float progress = sourceRequired > 0f
                ? Mathf.Clamp01(sourceXp / sourceRequired)
                : 0f;
            float targetRequired = SkillRecord.XpRequiredToLevelUpFrom(targetLevel);

            return Mathf.Clamp(
                progress * targetRequired,
                0f,
                Mathf.Max(0f, targetRequired - 1f));
        }

        private static float TotalExperience(int level, float xpSinceLastLevel)
        {
            int clampedLevel = Mathf.Clamp(
                level,
                SkillRecord.MinLevel,
                SkillRecord.MaxLevel);
            float total = 0f;

            for (int currentLevel = SkillRecord.MinLevel;
                currentLevel < clampedLevel;
                currentLevel++)
            {
                total += SkillRecord.XpRequiredToLevelUpFrom(currentLevel);
            }

            float currentRequired = SkillRecord.XpRequiredToLevelUpFrom(clampedLevel);
            total += Mathf.Clamp(
                xpSinceLastLevel,
                0f,
                Mathf.Max(0f, currentRequired - 1f));

            return total;
        }

        private static void ConvertTotalExperience(
            float totalExperience,
            out int level,
            out float xpSinceLastLevel)
        {
            float remaining = ClampTotalExperience(totalExperience);
            level = SkillRecord.MinLevel;

            while (level < SkillRecord.MaxLevel)
            {
                float required = SkillRecord.XpRequiredToLevelUpFrom(level);
                if (remaining < required)
                {
                    xpSinceLastLevel = remaining;
                    return;
                }

                remaining -= required;
                level++;
            }

            float maxRequired = SkillRecord.XpRequiredToLevelUpFrom(
                SkillRecord.MaxLevel);
            xpSinceLastLevel = Mathf.Clamp(
                remaining,
                0f,
                Mathf.Max(0f, maxRequired - 1f));
        }

        private static float ClampTotalExperience(float value)
        {
            return Mathf.Clamp(value, 0f, MaximumTotalExperience());
        }

        private static float MaximumTotalExperience()
        {
            float total = 0f;
            for (int level = SkillRecord.MinLevel;
                level <= SkillRecord.MaxLevel;
                level++)
            {
                total += SkillRecord.XpRequiredToLevelUpFrom(level);
            }

            return Mathf.Max(0f, total - 1f);
        }
    }

    public class BackstorySkillProgressState : IExposable
    {
        private SkillDef skill;
        private float sharedTotalXp;
        private float lastAppliedTotalXp;

        public SkillDef Skill => skill;

        public float SharedTotalXp
        {
            get => sharedTotalXp;
            set => sharedTotalXp = value;
        }

        public float LastAppliedTotalXp
        {
            get => lastAppliedTotalXp;
            set => lastAppliedTotalXp = value;
        }

        public static BackstorySkillProgressState Create(
            SkillRecord skillRecord,
            int activeOffset)
        {
            int sharedLevel = Mathf.Clamp(
                skillRecord.levelInt - activeOffset,
                SkillRecord.MinLevel,
                SkillRecord.MaxLevel);
            float activeRequired = SkillRecord.XpRequiredToLevelUpFrom(
                skillRecord.levelInt);
            float progress = activeRequired > 0f
                ? Mathf.Clamp01(skillRecord.xpSinceLastLevel / activeRequired)
                : 0f;
            float sharedRequired = SkillRecord.XpRequiredToLevelUpFrom(sharedLevel);
            float sharedXp = sharedLevel >= SkillRecord.MaxLevel
                ? 0f
                : progress * sharedRequired;

            return new BackstorySkillProgressState
            {
                skill = skillRecord.def,
                sharedTotalXp = CalculateTotalExperience(sharedLevel, sharedXp),
                lastAppliedTotalXp = CalculateTotalExperience(
                    skillRecord.levelInt,
                    skillRecord.xpSinceLastLevel)
            };
        }

        public void ExposeData()
        {
            Scribe_Defs.Look(ref skill, "skill");
            Scribe_Values.Look(ref sharedTotalXp, "sharedTotalXp", 0f);
            Scribe_Values.Look(ref lastAppliedTotalXp, "lastAppliedTotalXp", 0f);
        }

        private static float CalculateTotalExperience(int level, float xp)
        {
            int clampedLevel = Mathf.Clamp(
                level,
                SkillRecord.MinLevel,
                SkillRecord.MaxLevel);
            float total = 0f;

            for (int currentLevel = SkillRecord.MinLevel;
                currentLevel < clampedLevel;
                currentLevel++)
            {
                total += SkillRecord.XpRequiredToLevelUpFrom(currentLevel);
            }

            float required = SkillRecord.XpRequiredToLevelUpFrom(clampedLevel);
            total += Mathf.Clamp(xp, 0f, Mathf.Max(0f, required - 1f));
            return total;
        }
    }
}
