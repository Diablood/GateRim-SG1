using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public class CompProperties_GoauldHealingBracelet : CompProperties
    {
        public float range = 1.9f;
        public float totalHealing = 20f;
        public int maximumInjuries = 4;
        public float bloodLossReduction = 0.15f;
        public int cooldownTicks = 30000;
        public int fatigueDurationTicks = 12000;
        public int aiCheckIntervalTicks = 60;
        public float aiMinimumBleedRate = 0.2f;
        public float aiMaximumHealthPercent = 0.55f;
        public HediffDef fatigueHediff;

        public CompProperties_GoauldHealingBracelet()
        {
            compClass = typeof(Comp_GoauldHealingBracelet);
        }
    }

    public class Comp_GoauldHealingBracelet : ThingComp
    {
        private const int NoUseTick = -99999;

        private int lastUseTick = NoUseTick;

        public CompProperties_GoauldHealingBracelet BraceletProps
            => props as CompProperties_GoauldHealingBracelet;

        public Pawn Wearer => (parent as Apparel)?.Wearer;

        public int CooldownRemainingTicks
        {
            get
            {
                int currentTick = Find.TickManager?.TicksGame ?? 0;
                int elapsed = currentTick - lastUseTick;
                return Math.Max(0, (BraceletProps?.cooldownTicks ?? 0) - elapsed);
            }
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(
                ref lastUseTick,
                "goauldHealingBraceletLastUseTick",
                NoUseTick);
        }

        public override IEnumerable<Gizmo> CompGetWornGizmosExtra()
        {
            Pawn wearer = Wearer;

            if (wearer?.Faction != Faction.OfPlayer)
            {
                yield break;
            }

            Command_Action command = new Command_Action
            {
                defaultLabel = "GR_GoauldHealingBracelet_UseLabel".Translate(),
                defaultDesc = "GR_GoauldHealingBracelet_UseDescription".Translate(
                    BraceletProps.range.ToString("0.#"),
                    BraceletProps.totalHealing.ToString("0.#"),
                    BraceletProps.maximumInjuries,
                    BraceletProps.cooldownTicks.ToStringTicksToPeriod()),
                icon = parent.def.uiIcon,
                action = BeginTargeting
            };

            if (!CanActivate(out string disabledReason))
            {
                command.Disable(disabledReason);
            }

            yield return command;
        }

        public override string CompInspectStringExtra()
        {
            Pawn wearer = Wearer;

            if (wearer == null)
            {
                return null;
            }

            if (!NaquadahTraceUtility.CanActivateNaquadahTechnology(wearer))
            {
                return "GR_GoauldHealingBracelet_InactiveInspect".Translate(
                    wearer.LabelShortCap);
            }

            if (CooldownRemainingTicks > 0)
            {
                return "GR_GoauldHealingBracelet_CooldownInspect".Translate(
                    CooldownRemainingTicks.ToStringTicksToPeriod());
            }

            return "GR_GoauldHealingBracelet_ReadyInspect".Translate();
        }

        public override void CompTick()
        {
            base.CompTick();

            Pawn wearer = Wearer;

            if (wearer == null
                || wearer.Faction == Faction.OfPlayer
                || Find.TickManager == null
                || Find.TickManager.TicksGame % BraceletProps.aiCheckIntervalTicks
                    != 0
                || !ShouldUseForAi(wearer))
            {
                return;
            }

            TryHeal(wearer, aiControlled: true);
        }

        public bool TryHeal(Pawn patient, bool aiControlled = false)
        {
            Pawn wearer = Wearer;

            if (!CanActivate(out string disabledReason)
                || !IsValidPatient(wearer, patient))
            {
                if (!aiControlled && wearer != null)
                {
                    Messages.Message(
                        disabledReason.NullOrEmpty()
                            ? "GR_GoauldHealingBracelet_InvalidTarget"
                                .Translate()
                                .ToString()
                            : disabledReason,
                        wearer,
                        MessageTypeDefOf.RejectInput,
                        historical: false);
                }

                return false;
            }

            List<Hediff_Injury> injuries = TreatableInjuries(patient)
                .OrderByDescending(injury => injury.BleedRate)
                .ThenByDescending(injury => injury.Severity)
                .ToList();
            int stabilizedInjuries = StabilizeBleedingInjuries(injuries);
            float remainingHealing = BraceletProps.totalHealing;
            int treatedInjuries = HealRecentInjuries(
                injuries,
                BraceletProps.maximumInjuries,
                ref remainingHealing);

            float reducedBloodLoss = ReduceBloodLoss(patient);

            if (stabilizedInjuries == 0
                && treatedInjuries == 0
                && reducedBloodLoss <= 0f)
            {
                return false;
            }

            lastUseTick = Find.TickManager?.TicksGame ?? 0;
            ApplyFatigue(wearer);
            wearer.rotationTracker?.FaceTarget(patient);

            GR_Log.Message(
                $"Goa'uld healing bracelet used by {PawnDebugLabel(wearer)} "
                + $"on {PawnDebugLabel(patient)}; "
                + $"stabilized={stabilizedInjuries}, "
                + $"injuries={treatedInjuries}, "
                + $"healing={(BraceletProps.totalHealing - remainingHealing):0.##}, "
                + $"bloodLossReduction={reducedBloodLoss:0.##}.");

            if (!aiControlled || wearer.Faction == Faction.OfPlayer)
            {
                Messages.Message(
                    "GR_GoauldHealingBracelet_Used".Translate(
                        wearer.LabelShortCap,
                        patient.LabelShortCap),
                    patient,
                    MessageTypeDefOf.PositiveEvent,
                    historical: false);
            }

            return true;
        }

        internal void ResetForDebug()
        {
            lastUseTick = NoUseTick;
            Pawn wearer = Wearer;

            if (wearer?.health == null || BraceletProps?.fatigueHediff == null)
            {
                return;
            }

            Hediff fatigue = wearer.health.hediffSet.GetFirstHediffOfDef(
                BraceletProps.fatigueHediff);

            if (fatigue != null)
            {
                wearer.health.RemoveHediff(fatigue);
            }
        }

        private void BeginTargeting()
        {
            Pawn wearer = Wearer;

            if (!CanActivate(out string disabledReason))
            {
                Messages.Message(
                    disabledReason,
                    wearer,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            TargetingParameters targetingParameters = new TargetingParameters
            {
                canTargetPawns = true,
                canTargetLocations = false,
                validator = delegate(TargetInfo targetInfo)
                {
                    return IsValidPatient(wearer, targetInfo.Thing as Pawn);
                }
            };

            Find.Targeter.BeginTargeting(
                targetingParameters,
                delegate(LocalTargetInfo targetInfo)
                {
                    TryHeal(targetInfo.Thing as Pawn);
                });
        }

        private bool CanActivate(out string disabledReason)
        {
            Pawn wearer = Wearer;

            if (wearer?.Spawned != true || wearer.Map == null)
            {
                disabledReason = "GR_GoauldHealingBracelet_Unavailable"
                    .Translate()
                    .ToString();
                return false;
            }

            if (!NaquadahTraceUtility.CanActivateNaquadahTechnology(wearer))
            {
                disabledReason = "GR_GoauldHealingBracelet_NeedsNaquadah"
                    .Translate()
                    .ToString();
                return false;
            }

            if (wearer.Dead || wearer.Downed || wearer.health == null)
            {
                disabledReason = "GR_GoauldHealingBracelet_Incapacitated"
                    .Translate()
                    .ToString();
                return false;
            }

            if (CooldownRemainingTicks > 0)
            {
                disabledReason = "GR_GoauldHealingBracelet_Cooldown".Translate(
                    CooldownRemainingTicks.ToStringTicksToPeriod())
                    .ToString();
                return false;
            }

            disabledReason = null;
            return true;
        }

        private bool IsValidPatient(Pawn wearer, Pawn patient)
        {
            if (wearer == null
                || patient?.Spawned != true
                || patient.Dead
                || patient.health == null
                || patient.RaceProps?.Humanlike != true
                || patient.RaceProps.IsMechanoid
                || patient.Map != wearer.Map
                || !HasTreatableCondition(patient))
            {
                return false;
            }

            if (patient != wearer
                && (wearer.Position.DistanceTo(patient.Position)
                        > BraceletProps.range
                    || !GenSight.LineOfSight(
                        wearer.Position,
                        patient.Position,
                        wearer.Map)))
            {
                return false;
            }

            return true;
        }

        private bool ShouldUseForAi(Pawn wearer)
        {
            if (!CanActivate(out _)
                || !IsValidPatient(wearer, wearer))
            {
                return false;
            }

            float bleedRate = wearer.health.hediffSet.BleedRateTotal;
            float healthPercent = wearer.health.summaryHealth.SummaryHealthPercent;
            return bleedRate >= BraceletProps.aiMinimumBleedRate
                || healthPercent <= BraceletProps.aiMaximumHealthPercent;
        }

        private static bool HasTreatableCondition(Pawn patient)
        {
            return TreatableInjuries(patient).Any()
                || GetBloodLoss(patient)?.Severity > 0f;
        }

        private static IEnumerable<Hediff_Injury> TreatableInjuries(Pawn patient)
        {
            List<Hediff> hediffs = patient?.health?.hediffSet?.hediffs;

            if (hediffs == null)
            {
                yield break;
            }

            for (int index = 0; index < hediffs.Count; index++)
            {
                Hediff_Injury injury = hediffs[index] as Hediff_Injury;

                if (injury == null || injury.Severity <= 0f)
                {
                    continue;
                }

                HediffComp_GetsPermanent permanentComp
                    = injury.TryGetComp<HediffComp_GetsPermanent>();

                if (permanentComp?.IsPermanent != true)
                {
                    yield return injury;
                }
            }
        }

        private float ReduceBloodLoss(Pawn patient)
        {
            Hediff bloodLoss = GetBloodLoss(patient);

            if (bloodLoss == null || bloodLoss.Severity <= 0f)
            {
                return 0f;
            }

            float reduction = Math.Min(
                bloodLoss.Severity,
                BraceletProps.bloodLossReduction);
            bloodLoss.Severity -= reduction;
            return reduction;
        }

        private static int StabilizeBleedingInjuries(
            IEnumerable<Hediff_Injury> injuries)
        {
            int stabilized = 0;

            foreach (Hediff_Injury injury in injuries)
            {
                if (injury.BleedRate <= 0f || !injury.TendableNow(false))
                {
                    continue;
                }

                injury.Tended(0.8f, 0.8f, stabilized + 1);
                stabilized++;
            }

            return stabilized;
        }

        private static int HealRecentInjuries(
            IEnumerable<Hediff_Injury> injuries,
            int maximumInjuries,
            ref float remainingHealing)
        {
            List<Hediff_Injury> active = injuries
                .Take(maximumInjuries)
                .Where(injury => injury.Severity > 0f)
                .ToList();
            HashSet<Hediff_Injury> treated = new HashSet<Hediff_Injury>();

            while (remainingHealing > 0.0001f && active.Count > 0)
            {
                float share = remainingHealing / active.Count;

                for (int index = active.Count - 1; index >= 0; index--)
                {
                    Hediff_Injury injury = active[index];
                    float appliedHealing = Math.Min(injury.Severity, share);

                    if (appliedHealing > 0f)
                    {
                        injury.Heal(appliedHealing);
                        remainingHealing -= appliedHealing;
                        treated.Add(injury);
                    }

                    if (injury.Severity <= 0.0001f)
                    {
                        active.RemoveAt(index);
                    }
                }
            }

            return treated.Count;
        }

        private static Hediff GetBloodLoss(Pawn patient)
        {
            return patient?.health?.hediffSet?.GetFirstHediffOfDef(
                HediffDefOf.BloodLoss);
        }

        private void ApplyFatigue(Pawn wearer)
        {
            HediffDef fatigueDef = BraceletProps?.fatigueHediff;

            if (wearer?.health == null || fatigueDef == null)
            {
                return;
            }

            Hediff existing = wearer.health.hediffSet.GetFirstHediffOfDef(
                fatigueDef);

            if (existing != null)
            {
                wearer.health.RemoveHediff(existing);
            }

            Hediff fatigue = HediffMaker.MakeHediff(fatigueDef, wearer);
            HediffComp_Disappears disappears
                = (fatigue as HediffWithComps)
                    ?.GetComp<HediffComp_Disappears>();

            if (disappears != null)
            {
                disappears.ticksToDisappear = BraceletProps.fatigueDurationTicks;
            }

            wearer.health.AddHediff(fatigue);
        }

        private static string PawnDebugLabel(Pawn pawn)
        {
            return pawn == null
                ? "<null pawn>"
                : $"{pawn.LabelShort} ({pawn.ThingID})";
        }
    }
}
