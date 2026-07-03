using System;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public partial class CompProperties_KaraKeshShield
    {
        public float paralysisHoldRange = 6.9f;
        public float paralysisHoldEnergyCost = 2.5f;
        public int paralysisHoldCooldownTicks = 1800;
        public int paralysisHoldDurationTicks = 600;
        public int paralysisHoldAiCheckIntervalTicks = 60;
        public HediffDef paralysisHoldHediff;
    }

    public partial class Comp_KaraKeshShield
    {
        private const int NoParalysisHoldTick = -99999;

        private int lastParalysisHoldTick = NoParalysisHoldTick;
        private Pawn activeParalysisTarget;

        public int ParalysisHoldCooldownRemainingTicks
        {
            get
            {
                int currentTick = Find.TickManager?.TicksGame ?? 0;
                int cooldown = KaraKeshProps?.paralysisHoldCooldownTicks ?? 0;
                return Math.Max(
                    0,
                    cooldown - (currentTick - lastParalysisHoldTick));
            }
        }

        public HediffDef ParalysisHoldHediff
            => KaraKeshProps?.paralysisHoldHediff;

        public Pawn ActiveParalysisTarget
            => IsMaintainingParalysisHold ? activeParalysisTarget : null;

        public bool IsMaintainingParalysisHold
        {
            get
            {
                Apparel source = parent as Apparel;
                return activeParalysisTarget != null
                    && KaraKeshParalysisHoldUtility.IsHeldBy(
                        activeParalysisTarget,
                        KaraKeshProps?.paralysisHoldHediff,
                        source);
            }
        }

        public bool TryUseParalysisHold(
            Pawn target,
            bool aiControlled = false)
        {
            Pawn wearer = PawnOwner;

            if (!CanUseParalysisHold(out string disabledReason)
                || !IsValidParalysisHoldTarget(wearer, target))
            {
                if (!aiControlled && wearer != null)
                {
                    Messages.Message(
                        disabledReason.NullOrEmpty()
                            ? "GR_KaraKesh_ParalysisHoldInvalidTarget"
                                .Translate()
                                .ToString()
                            : disabledReason,
                        wearer,
                        MessageTypeDefOf.RejectInput,
                        historical: false);
                }

                return false;
            }

            Apparel source = parent as Apparel;

            if (!KaraKeshParalysisHoldUtility.TryApply(
                    target,
                    KaraKeshProps.paralysisHoldHediff,
                    KaraKeshProps.paralysisHoldDurationTicks,
                    source))
            {
                if (!aiControlled)
                {
                    Messages.Message(
                        "GR_KaraKesh_ParalysisHoldInvalidTarget".Translate(),
                        wearer,
                        MessageTypeDefOf.RejectInput,
                        historical: false);
                }

                return false;
            }

            wearer.rotationTracker?.FaceTarget(target);
            ConsumeAbilityEnergy(KaraKeshProps.paralysisHoldEnergyCost);
            lastParalysisHoldTick = Find.TickManager?.TicksGame ?? 0;
            activeParalysisTarget = target;

            if (target.Spawned && target.Map != null)
            {
                FleckMaker.ThrowLightningGlow(
                    target.DrawPos,
                    target.Map,
                    2.4f);
                FleckMaker.Static(
                    target.DrawPos,
                    target.Map,
                    FleckDefOf.ExplosionFlash,
                    1f);
                MoteMaker.ThrowText(
                    target.DrawPos,
                    target.Map,
                    "GR_KaraKesh_ParalysisHoldMote".Translate());
            }

            return true;
        }

        public bool ReleaseParalysisHold()
        {
            Pawn target = activeParalysisTarget;
            Apparel source = parent as Apparel;
            bool removed = KaraKeshParalysisHoldUtility.TryClear(
                target,
                KaraKeshProps?.paralysisHoldHediff,
                source);
            activeParalysisTarget = null;
            return removed;
        }

        internal void PrepareParalysisHoldForDebug()
        {
            ReleaseParalysisHold();
            PrepareShieldEnergyForDebug();
            lastParalysisHoldTick = NoParalysisHoldTick;
        }

        internal void ResetParalysisHoldCooldownForDebug()
        {
            lastParalysisHoldTick = NoParalysisHoldTick;
        }

        internal void NotifyParalysisHoldEnded(Pawn target)
        {
            if (activeParalysisTarget == target)
            {
                activeParalysisTarget = null;
            }
        }

        internal bool CanSustainParalysisHold(Pawn target)
        {
            Apparel source = parent as Apparel;
            Pawn wearer = source?.Wearer;

            if (target == null
                || target != activeParalysisTarget
                || target.Destroyed
                || target.Dead
                || target.health?.hediffSet == null
                || target.RaceProps?.Humanlike != true
                || !target.RaceProps.IsFlesh
                || wearer?.Spawned != true
                || wearer.Map == null
                || wearer.Dead
                || wearer.Downed
                || !WearerCanActivate
                || ShieldState != ShieldState.Active
                || target.Spawned != true
                || target.Map != wearer.Map
                || !target.HostileTo(wearer))
            {
                return false;
            }

            if (wearer.Position.DistanceTo(target.Position)
                > KaraKeshProps.paralysisHoldRange)
            {
                return false;
            }

            return GenSight.LineOfSight(
                wearer.Position,
                target.Position,
                wearer.Map);
        }

        private void ExposeParalysisHoldData()
        {
            Scribe_Values.Look(
                ref lastParalysisHoldTick,
                "karaKeshLastParalysisHoldTick",
                NoParalysisHoldTick);
            Scribe_References.Look(
                ref activeParalysisTarget,
                "karaKeshActiveParalysisTarget");
        }

        private Command_Action BuildParalysisHoldCommand()
        {
            Command_Action command = new Command_Action
            {
                defaultLabel = "GR_KaraKesh_ParalysisHoldLabel".Translate(),
                defaultDesc = "GR_KaraKesh_ParalysisHoldDescription".Translate(
                    KaraKeshProps.paralysisHoldRange.ToString("0.#"),
                    KaraKeshProps.paralysisHoldEnergyCost.ToString("0.##"),
                    KaraKeshProps.paralysisHoldDurationTicks
                        .ToStringTicksToPeriod(),
                    KaraKeshProps.paralysisHoldCooldownTicks
                        .ToStringTicksToPeriod()),
                icon = TexCommand.Attack,
                action = BeginParalysisHoldTargeting
            };

            if (!CanUseParalysisHold(out string disabledReason))
            {
                command.Disable(disabledReason);
            }

            return command;
        }

        private Command_Action BuildReleaseParalysisHoldCommand()
        {
            return new Command_Action
            {
                defaultLabel = "GR_KaraKesh_ParalysisHoldReleaseLabel"
                    .Translate(),
                defaultDesc = "GR_KaraKesh_ParalysisHoldReleaseDescription"
                    .Translate(),
                icon = TexCommand.Attack,
                action = delegate
                {
                    Pawn wearer = PawnOwner;
                    Pawn target = activeParalysisTarget;

                    if (!ReleaseParalysisHold())
                    {
                        return;
                    }

                    Messages.Message(
                        "GR_KaraKesh_ParalysisHoldReleased".Translate(
                            wearer?.LabelShortCap,
                            target?.LabelShortCap),
                        target ?? wearer,
                        MessageTypeDefOf.NeutralEvent,
                        historical: false);
                }
            };
        }

        private void BeginParalysisHoldTargeting()
        {
            Pawn wearer = PawnOwner;

            if (!CanUseParalysisHold(out string disabledReason))
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
                    return IsValidParalysisHoldTarget(
                        wearer,
                        targetInfo.Thing as Pawn);
                }
            };

            Find.Targeter.BeginTargeting(
                targetingParameters,
                delegate(LocalTargetInfo targetInfo)
                {
                    Pawn target = targetInfo.Thing as Pawn;

                    if (!TryUseParalysisHold(target))
                    {
                        return;
                    }

                    Messages.Message(
                        "GR_KaraKesh_ParalysisHoldUsed".Translate(
                            wearer.LabelShortCap,
                            target.LabelShortCap),
                        target,
                        MessageTypeDefOf.NeutralEvent,
                        historical: false);
                });
        }

        private bool CanUseParalysisHold(out string disabledReason)
        {
            Pawn wearer = PawnOwner;

            if (wearer?.Spawned != true
                || wearer.Map == null
                || KaraKeshProps?.paralysisHoldHediff == null)
            {
                disabledReason = "GR_KaraKesh_ParalysisHoldUnavailable"
                    .Translate()
                    .ToString();
                return false;
            }

            if (!WearerCanActivate)
            {
                disabledReason = "GR_KaraKesh_ParalysisHoldNeedsNaquadah"
                    .Translate()
                    .ToString();
                return false;
            }

            if (wearer.Dead || wearer.Downed)
            {
                disabledReason = "GR_KaraKesh_ParalysisHoldIncapacitated"
                    .Translate()
                    .ToString();
                return false;
            }

            if (IsMaintainingParalysisHold)
            {
                disabledReason = "GR_KaraKesh_ParalysisHoldAlreadyActive"
                    .Translate()
                    .ToString();
                return false;
            }

            if (ShieldState != ShieldState.Active)
            {
                disabledReason = "GR_KaraKesh_ParalysisHoldShieldResetting"
                    .Translate()
                    .ToString();
                return false;
            }

            if (Energy + 0.0001f < KaraKeshProps.paralysisHoldEnergyCost)
            {
                disabledReason = "GR_KaraKesh_ParalysisHoldLowEnergy".Translate(
                    KaraKeshProps.paralysisHoldEnergyCost.ToString("0.##"))
                    .ToString();
                return false;
            }

            int remainingTicks = ParalysisHoldCooldownRemainingTicks;

            if (remainingTicks > 0)
            {
                disabledReason = "GR_KaraKesh_ParalysisHoldCooldown".Translate(
                    remainingTicks.ToStringTicksToPeriod())
                    .ToString();
                return false;
            }

            disabledReason = null;
            return true;
        }

        private bool IsValidParalysisHoldTarget(Pawn wearer, Pawn target)
        {
            if (!IsCommonHostileTarget(wearer, target)
                || !KaraKeshParalysisHoldUtility.CanAffect(
                    target,
                    KaraKeshProps?.paralysisHoldHediff))
            {
                return false;
            }

            if (wearer.Position.DistanceTo(target.Position)
                > KaraKeshProps.paralysisHoldRange)
            {
                return false;
            }

            return GenSight.LineOfSight(
                wearer.Position,
                target.Position,
                wearer.Map);
        }

        private bool MaintainParalysisHold(Pawn wearer)
        {
            if (activeParalysisTarget == null)
            {
                return false;
            }

            Apparel source = parent as Apparel;

            if (!KaraKeshParalysisHoldUtility.IsHeldBy(
                    activeParalysisTarget,
                    KaraKeshProps?.paralysisHoldHediff,
                    source)
                || !CanSustainParalysisHold(activeParalysisTarget))
            {
                KaraKeshParalysisHoldUtility.TryClear(
                    activeParalysisTarget,
                    KaraKeshProps?.paralysisHoldHediff,
                    source);
                activeParalysisTarget = null;
                return false;
            }

            wearer?.rotationTracker?.FaceTarget(activeParalysisTarget);
            KeepDisplaying();
            return true;
        }

        private bool TryUseParalysisHoldForAi(Pawn wearer)
        {
            if (wearer?.Spawned != true
                || wearer.Faction == null
                || wearer.Faction == Faction.OfPlayer
                || wearer.Dead
                || wearer.Downed
                || !wearer.IsHashIntervalTick(
                    Math.Max(
                        1,
                        KaraKeshProps.paralysisHoldAiCheckIntervalTicks))
                || !CanUseParalysisHold(out _))
            {
                return false;
            }

            Pawn target = FindClosestTarget(
                wearer,
                candidate => IsValidParalysisHoldTarget(wearer, candidate));

            return target != null
                && TryUseParalysisHold(target, aiControlled: true);
        }

        private string GetParalysisHoldInspectText()
        {
            Pawn target = ActiveParalysisTarget;

            if (target != null)
            {
                return "GR_KaraKesh_ParalysisHoldActiveInspect".Translate(
                    target.LabelShortCap)
                    .ToString();
            }

            if (ParalysisHoldCooldownRemainingTicks > 0)
            {
                return "GR_KaraKesh_ParalysisHoldCooldownInspect".Translate(
                    ParalysisHoldCooldownRemainingTicks
                        .ToStringTicksToPeriod())
                    .ToString();
            }

            if (ShieldState != ShieldState.Active)
            {
                return "GR_KaraKesh_ParalysisHoldResettingInspect"
                    .Translate()
                    .ToString();
            }

            if (Energy + 0.0001f < KaraKeshProps.paralysisHoldEnergyCost)
            {
                return "GR_KaraKesh_ParalysisHoldLowEnergyInspect"
                    .Translate()
                    .ToString();
            }

            return "GR_KaraKesh_ParalysisHoldReadyInspect"
                .Translate()
                .ToString();
        }
    }
}
