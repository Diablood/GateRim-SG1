using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public class CompProperties_KaraKeshShield : CompProperties_Shield
    {
        public int rechargeDelayAfterAbsorbedHitTicks = 300;
        public float kineticBlastRange = 10.9f;
        public float kineticBlastEnergyCost = 1.25f;
        public int kineticBlastCooldownTicks = 900;
        public float kineticBlastDamage = 12f;
        public float kineticBlastArmorPenetration = 0.25f;
        public int kineticBlastStunTicks = 120;
        public int kineticBlastKnockbackCells = 2;
        public int kineticBlastAiCheckIntervalTicks = 60;
        public float neuralAttackRange = 8.9f;
        public float neuralAttackEnergyCost = 1.75f;
        public int neuralAttackCooldownTicks = 1200;
        public int neuralAttackDurationTicks = 600;
        public int neuralAttackAiCheckIntervalTicks = 60;
        public HediffDef neuralAttackHediff;

        public CompProperties_KaraKeshShield()
        {
            compClass = typeof(Comp_KaraKeshShield);
        }
    }

    public class Comp_KaraKeshShield : CompShield
    {
        private const int NoAbsorbedHitTick = -99999;
        private const int NoKineticBlastTick = -99999;
        private const int NoNeuralAttackTick = -99999;

        private int lastAbsorbedHitTick = NoAbsorbedHitTick;
        private int lastKineticBlastTick = NoKineticBlastTick;
        private int lastNeuralAttackTick = NoNeuralAttackTick;

        private CompProperties_KaraKeshShield KaraKeshProps
            => props as CompProperties_KaraKeshShield;

        private bool WearerCanActivate
            => NaquadahTraceUtility.CanActivateNaquadahTechnology(PawnOwner);

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(
                ref lastAbsorbedHitTick,
                "karaKeshLastAbsorbedHitTick",
                NoAbsorbedHitTick);
            Scribe_Values.Look(
                ref lastKineticBlastTick,
                "karaKeshLastKineticBlastTick",
                NoKineticBlastTick);
            Scribe_Values.Look(
                ref lastNeuralAttackTick,
                "karaKeshLastNeuralAttackTick",
                NoNeuralAttackTick);
        }

        public override IEnumerable<Gizmo> CompGetWornGizmosExtra()
        {
            if (!WearerCanActivate)
            {
                yield break;
            }

            foreach (Gizmo gizmo in base.CompGetWornGizmosExtra())
            {
                yield return gizmo;
            }

            if (PawnOwner?.Faction == Faction.OfPlayer)
            {
                yield return BuildKineticBlastCommand();
                yield return BuildNeuralAttackCommand();
            }
        }

        public override string CompInspectStringExtra()
        {
            string baseText = base.CompInspectStringExtra();

            if (PawnOwner == null)
            {
                return baseText;
            }

            if (!WearerCanActivate)
            {
                string inactiveText = "GR_KaraKesh_InactiveWithoutNaquadah"
                    .Translate(PawnOwner.LabelShortCap)
                    .ToString();

                return AppendInspectLine(baseText, inactiveText);
            }

            string result = AppendInspectLine(
                baseText,
                GetKineticBlastInspectText());
            return AppendInspectLine(
                result,
                GetNeuralAttackInspectText());
        }

        public override void PostPreApplyDamage(
            ref DamageInfo dinfo,
            out bool absorbed)
        {
            if (!WearerCanActivate)
            {
                absorbed = false;
                return;
            }

            base.PostPreApplyDamage(ref dinfo, out absorbed);

            if (absorbed && Find.TickManager != null)
            {
                lastAbsorbedHitTick = Find.TickManager.TicksGame;
            }
        }

        public override void CompTick()
        {
            Pawn wearer = PawnOwner;

            if (wearer != null && !WearerCanActivate)
            {
                return;
            }

            bool neuralAttackUsed = TryUseNeuralAttackForAi(wearer);

            if (!neuralAttackUsed)
            {
                TryUseKineticBlastForAi(wearer);
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            int rechargeDelay = KaraKeshProps
                ?.rechargeDelayAfterAbsorbedHitTicks ?? 0;

            if (ShieldState == ShieldState.Active
                && currentTick - lastAbsorbedHitTick < rechargeDelay)
            {
                return;
            }

            base.CompTick();
        }

        public override void CompDrawWornExtras()
        {
            if (!WearerCanActivate)
            {
                return;
            }

            base.CompDrawWornExtras();
        }

        public override bool CompAllowVerbCast(Verb verb)
        {
            return !WearerCanActivate || base.CompAllowVerbCast(verb);
        }

        public bool TryUseKineticBlast(Pawn target, bool aiControlled = false)
        {
            Pawn wearer = PawnOwner;

            if (!CanUseKineticBlast(out string disabledReason)
                || !IsValidKineticBlastTarget(wearer, target))
            {
                if (!aiControlled && wearer != null)
                {
                    Messages.Message(
                        disabledReason.NullOrEmpty()
                            ? "GR_KaraKesh_KineticBlastInvalidTarget"
                                .Translate()
                                .ToString()
                            : disabledReason,
                        wearer,
                        MessageTypeDefOf.RejectInput,
                        historical: false);
                }

                return false;
            }

            wearer.rotationTracker?.FaceTarget(target);
            ConsumeAbilityEnergy(KaraKeshProps.kineticBlastEnergyCost);
            lastKineticBlastTick = Find.TickManager?.TicksGame ?? 0;

            DamageInfo damage = new DamageInfo(
                DamageDefOf.Blunt,
                KaraKeshProps.kineticBlastDamage,
                KaraKeshProps.kineticBlastArmorPenetration,
                0f,
                wearer,
                null,
                parent.def,
                DamageInfo.SourceCategory.ThingOrUnknown,
                target,
                instigatorGuilty: !wearer.Drafted);

            target.TakeDamage(damage);

            if (!target.Destroyed && !target.Dead)
            {
                TryKnockTargetBack(wearer, target);
                target.stances?.stunner?.StunFor(
                    KaraKeshProps.kineticBlastStunTicks,
                    wearer);
            }

            if (target.Spawned && target.Map != null)
            {
                FleckMaker.Static(
                    target.DrawPos,
                    target.Map,
                    FleckDefOf.ExplosionFlash,
                    1.35f);
                MoteMaker.ThrowText(
                    target.DrawPos,
                    target.Map,
                    "GR_KaraKesh_KineticBlastMote".Translate());
            }

            return true;
        }

        public bool TryUseNeuralAttack(Pawn target, bool aiControlled = false)
        {
            Pawn wearer = PawnOwner;

            if (!CanUseNeuralAttack(out string disabledReason)
                || !IsValidNeuralAttackTarget(wearer, target))
            {
                if (!aiControlled && wearer != null)
                {
                    Messages.Message(
                        disabledReason.NullOrEmpty()
                            ? "GR_KaraKesh_NeuralAttackInvalidTarget"
                                .Translate()
                                .ToString()
                            : disabledReason,
                        wearer,
                        MessageTypeDefOf.RejectInput,
                        historical: false);
                }

                return false;
            }

            if (!KaraKeshNeuralAttackUtility.TryApply(
                    target,
                    KaraKeshProps.neuralAttackHediff,
                    KaraKeshProps.neuralAttackDurationTicks))
            {
                if (!aiControlled)
                {
                    Messages.Message(
                        "GR_KaraKesh_NeuralAttackInvalidTarget".Translate(),
                        wearer,
                        MessageTypeDefOf.RejectInput,
                        historical: false);
                }

                return false;
            }

            wearer.rotationTracker?.FaceTarget(target);
            ConsumeAbilityEnergy(KaraKeshProps.neuralAttackEnergyCost);
            lastNeuralAttackTick = Find.TickManager?.TicksGame ?? 0;

            if (target.Spawned && target.Map != null)
            {
                FleckMaker.ThrowLightningGlow(
                    target.DrawPos,
                    target.Map,
                    2f);
                FleckMaker.Static(
                    target.DrawPos,
                    target.Map,
                    FleckDefOf.ExplosionFlash,
                    0.8f);
                MoteMaker.ThrowText(
                    target.DrawPos,
                    target.Map,
                    "GR_KaraKesh_NeuralAttackMote".Translate());
            }

            return true;
        }

        public int KineticBlastCooldownRemainingTicks
        {
            get
            {
                int currentTick = Find.TickManager?.TicksGame ?? 0;
                int cooldown = KaraKeshProps?.kineticBlastCooldownTicks ?? 0;
                return Math.Max(
                    0,
                    cooldown - (currentTick - lastKineticBlastTick));
            }
        }

        public int NeuralAttackCooldownRemainingTicks
        {
            get
            {
                int currentTick = Find.TickManager?.TicksGame ?? 0;
                int cooldown = KaraKeshProps?.neuralAttackCooldownTicks ?? 0;
                return Math.Max(
                    0,
                    cooldown - (currentTick - lastNeuralAttackTick));
            }
        }

        public HediffDef NeuralAttackHediff
            => KaraKeshProps?.neuralAttackHediff;

        internal void PrepareKineticBlastForDebug()
        {
            PrepareShieldEnergyForDebug();
            lastKineticBlastTick = NoKineticBlastTick;
        }

        internal void ResetKineticBlastCooldownForDebug()
        {
            lastKineticBlastTick = NoKineticBlastTick;
        }

        internal void PrepareNeuralAttackForDebug()
        {
            PrepareShieldEnergyForDebug();
            lastNeuralAttackTick = NoNeuralAttackTick;
        }

        internal void ResetNeuralAttackCooldownForDebug()
        {
            lastNeuralAttackTick = NoNeuralAttackTick;
        }

        private void PrepareShieldEnergyForDebug()
        {
            energy = parent?.GetStatValue(
                StatDefOf.EnergyShieldEnergyMax) ?? 0f;
            ticksToReset = -1;
            lastAbsorbedHitTick = NoAbsorbedHitTick;
            KeepDisplaying();
        }

        private Command_Action BuildKineticBlastCommand()
        {
            Command_Action command = new Command_Action
            {
                defaultLabel = "GR_KaraKesh_KineticBlastLabel".Translate(),
                defaultDesc = "GR_KaraKesh_KineticBlastDescription".Translate(
                    KaraKeshProps.kineticBlastRange.ToString("0.#"),
                    KaraKeshProps.kineticBlastEnergyCost.ToString("0.##"),
                    KaraKeshProps.kineticBlastCooldownTicks
                        .ToStringTicksToPeriod()),
                icon = TexCommand.Attack,
                action = BeginKineticBlastTargeting
            };

            if (!CanUseKineticBlast(out string disabledReason))
            {
                command.Disable(disabledReason);
            }

            return command;
        }

        private Command_Action BuildNeuralAttackCommand()
        {
            Command_Action command = new Command_Action
            {
                defaultLabel = "GR_KaraKesh_NeuralAttackLabel".Translate(),
                defaultDesc = "GR_KaraKesh_NeuralAttackDescription".Translate(
                    KaraKeshProps.neuralAttackRange.ToString("0.#"),
                    KaraKeshProps.neuralAttackEnergyCost.ToString("0.##"),
                    KaraKeshProps.neuralAttackDurationTicks
                        .ToStringTicksToPeriod(),
                    KaraKeshProps.neuralAttackCooldownTicks
                        .ToStringTicksToPeriod()),
                icon = TexCommand.Attack,
                action = BeginNeuralAttackTargeting
            };

            if (!CanUseNeuralAttack(out string disabledReason))
            {
                command.Disable(disabledReason);
            }

            return command;
        }

        private void BeginKineticBlastTargeting()
        {
            Pawn wearer = PawnOwner;

            if (!CanUseKineticBlast(out string disabledReason))
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
                    return IsValidKineticBlastTarget(
                        wearer,
                        targetInfo.Thing as Pawn);
                }
            };

            Find.Targeter.BeginTargeting(
                targetingParameters,
                delegate(LocalTargetInfo targetInfo)
                {
                    Pawn target = targetInfo.Thing as Pawn;

                    if (!TryUseKineticBlast(target))
                    {
                        return;
                    }

                    Messages.Message(
                        "GR_KaraKesh_KineticBlastUsed".Translate(
                            wearer.LabelShortCap,
                            target.LabelShortCap),
                        target,
                        MessageTypeDefOf.NeutralEvent,
                        historical: false);
                });
        }

        private void BeginNeuralAttackTargeting()
        {
            Pawn wearer = PawnOwner;

            if (!CanUseNeuralAttack(out string disabledReason))
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
                    return IsValidNeuralAttackTarget(
                        wearer,
                        targetInfo.Thing as Pawn);
                }
            };

            Find.Targeter.BeginTargeting(
                targetingParameters,
                delegate(LocalTargetInfo targetInfo)
                {
                    Pawn target = targetInfo.Thing as Pawn;

                    if (!TryUseNeuralAttack(target))
                    {
                        return;
                    }

                    Messages.Message(
                        "GR_KaraKesh_NeuralAttackUsed".Translate(
                            wearer.LabelShortCap,
                            target.LabelShortCap),
                        target,
                        MessageTypeDefOf.NeutralEvent,
                        historical: false);
                });
        }

        private bool CanUseKineticBlast(out string disabledReason)
        {
            Pawn wearer = PawnOwner;

            if (wearer?.Spawned != true || wearer.Map == null)
            {
                disabledReason = "GR_KaraKesh_KineticBlastUnavailable"
                    .Translate()
                    .ToString();
                return false;
            }

            if (!WearerCanActivate)
            {
                disabledReason = "GR_KaraKesh_KineticBlastNeedsNaquadah"
                    .Translate()
                    .ToString();
                return false;
            }

            if (wearer.Dead || wearer.Downed)
            {
                disabledReason = "GR_KaraKesh_KineticBlastIncapacitated"
                    .Translate()
                    .ToString();
                return false;
            }

            if (ShieldState != ShieldState.Active)
            {
                disabledReason = "GR_KaraKesh_KineticBlastShieldResetting"
                    .Translate()
                    .ToString();
                return false;
            }

            if (Energy + 0.0001f < KaraKeshProps.kineticBlastEnergyCost)
            {
                disabledReason = "GR_KaraKesh_KineticBlastLowEnergy".Translate(
                    KaraKeshProps.kineticBlastEnergyCost.ToString("0.##"))
                    .ToString();
                return false;
            }

            int remainingTicks = KineticBlastCooldownRemainingTicks;

            if (remainingTicks > 0)
            {
                disabledReason = "GR_KaraKesh_KineticBlastCooldown".Translate(
                    remainingTicks.ToStringTicksToPeriod())
                    .ToString();
                return false;
            }

            disabledReason = null;
            return true;
        }

        private bool CanUseNeuralAttack(out string disabledReason)
        {
            Pawn wearer = PawnOwner;

            if (wearer?.Spawned != true
                || wearer.Map == null
                || KaraKeshProps?.neuralAttackHediff == null)
            {
                disabledReason = "GR_KaraKesh_NeuralAttackUnavailable"
                    .Translate()
                    .ToString();
                return false;
            }

            if (!WearerCanActivate)
            {
                disabledReason = "GR_KaraKesh_NeuralAttackNeedsNaquadah"
                    .Translate()
                    .ToString();
                return false;
            }

            if (wearer.Dead || wearer.Downed)
            {
                disabledReason = "GR_KaraKesh_NeuralAttackIncapacitated"
                    .Translate()
                    .ToString();
                return false;
            }

            if (ShieldState != ShieldState.Active)
            {
                disabledReason = "GR_KaraKesh_NeuralAttackShieldResetting"
                    .Translate()
                    .ToString();
                return false;
            }

            if (Energy + 0.0001f < KaraKeshProps.neuralAttackEnergyCost)
            {
                disabledReason = "GR_KaraKesh_NeuralAttackLowEnergy".Translate(
                    KaraKeshProps.neuralAttackEnergyCost.ToString("0.##"))
                    .ToString();
                return false;
            }

            int remainingTicks = NeuralAttackCooldownRemainingTicks;

            if (remainingTicks > 0)
            {
                disabledReason = "GR_KaraKesh_NeuralAttackCooldown".Translate(
                    remainingTicks.ToStringTicksToPeriod())
                    .ToString();
                return false;
            }

            disabledReason = null;
            return true;
        }

        private bool IsValidKineticBlastTarget(Pawn wearer, Pawn target)
        {
            if (!IsCommonHostileTarget(wearer, target))
            {
                return false;
            }

            if (wearer.Position.DistanceTo(target.Position)
                > KaraKeshProps.kineticBlastRange)
            {
                return false;
            }

            return GenSight.LineOfSight(
                wearer.Position,
                target.Position,
                wearer.Map);
        }

        private bool IsValidNeuralAttackTarget(Pawn wearer, Pawn target)
        {
            if (!IsCommonHostileTarget(wearer, target)
                || !KaraKeshNeuralAttackUtility.CanAffect(target))
            {
                return false;
            }

            if (wearer.Position.DistanceTo(target.Position)
                > KaraKeshProps.neuralAttackRange)
            {
                return false;
            }

            return GenSight.LineOfSight(
                wearer.Position,
                target.Position,
                wearer.Map);
        }

        private static bool IsCommonHostileTarget(Pawn wearer, Pawn target)
        {
            return wearer?.Spawned == true
                && target?.Spawned == true
                && !target.Destroyed
                && !target.Dead
                && !target.Downed
                && target != wearer
                && target.Map == wearer.Map
                && target.HostileTo(wearer);
        }

        private void ConsumeAbilityEnergy(float cost)
        {
            energy = Math.Max(0f, energy - cost);
            KeepDisplaying();

            if (Find.TickManager != null)
            {
                lastAbsorbedHitTick = Find.TickManager.TicksGame;
            }
        }

        private bool TryUseNeuralAttackForAi(Pawn wearer)
        {
            if (wearer?.Spawned != true
                || wearer.Faction == null
                || wearer.Faction == Faction.OfPlayer
                || wearer.Dead
                || wearer.Downed
                || !wearer.IsHashIntervalTick(
                    Math.Max(
                        1,
                        KaraKeshProps.neuralAttackAiCheckIntervalTicks))
                || !CanUseNeuralAttack(out _))
            {
                return false;
            }

            Pawn target = FindClosestNeuralAttackTarget(wearer);

            return target != null
                && TryUseNeuralAttack(target, aiControlled: true);
        }

        private void TryUseKineticBlastForAi(Pawn wearer)
        {
            if (wearer?.Spawned != true
                || wearer.Faction == null
                || wearer.Faction == Faction.OfPlayer
                || wearer.Dead
                || wearer.Downed
                || !wearer.IsHashIntervalTick(
                    Math.Max(
                        1,
                        KaraKeshProps.kineticBlastAiCheckIntervalTicks))
                || !CanUseKineticBlast(out _))
            {
                return;
            }

            Pawn target = FindClosestKineticBlastTarget(wearer);

            if (target != null)
            {
                TryUseKineticBlast(target, aiControlled: true);
            }
        }

        private Pawn FindClosestKineticBlastTarget(Pawn wearer)
        {
            return FindClosestTarget(
                wearer,
                candidate => IsValidKineticBlastTarget(wearer, candidate));
        }

        private Pawn FindClosestNeuralAttackTarget(Pawn wearer)
        {
            return FindClosestTarget(
                wearer,
                candidate => IsValidNeuralAttackTarget(wearer, candidate));
        }

        private static Pawn FindClosestTarget(
            Pawn wearer,
            Predicate<Pawn> validator)
        {
            IReadOnlyList<Pawn> pawns = wearer.Map?.mapPawns?.AllPawnsSpawned;
            Pawn closest = null;
            float closestDistanceSquared = float.MaxValue;

            if (pawns == null)
            {
                return null;
            }

            for (int index = 0; index < pawns.Count; index++)
            {
                Pawn candidate = pawns[index];

                if (!validator(candidate))
                {
                    continue;
                }

                float distanceSquared = wearer.Position
                    .DistanceToSquared(candidate.Position);

                if (distanceSquared < closestDistanceSquared)
                {
                    closest = candidate;
                    closestDistanceSquared = distanceSquared;
                }
            }

            return closest;
        }

        private void TryKnockTargetBack(Pawn wearer, Pawn target)
        {
            Map map = target.Map;
            IntVec3 delta = target.Position - wearer.Position;
            IntVec3 step = new IntVec3(
                Math.Sign(delta.x),
                0,
                Math.Sign(delta.z));

            if (map == null || step == IntVec3.Zero)
            {
                return;
            }

            IntVec3 destination = target.Position;

            for (int index = 0;
                index < KaraKeshProps.kineticBlastKnockbackCells;
                index++)
            {
                IntVec3 candidate = destination + step;

                if (!candidate.InBounds(map)
                    || !candidate.Walkable(map)
                    || candidate.GetFirstPawn(map) != null)
                {
                    break;
                }

                destination = candidate;
            }

            if (destination == target.Position)
            {
                return;
            }

            target.Position = destination;
            target.Notify_Teleported();
        }

        private string GetKineticBlastInspectText()
        {
            if (KineticBlastCooldownRemainingTicks > 0)
            {
                return "GR_KaraKesh_KineticBlastCooldownInspect".Translate(
                    KineticBlastCooldownRemainingTicks.ToStringTicksToPeriod())
                    .ToString();
            }

            if (ShieldState != ShieldState.Active)
            {
                return "GR_KaraKesh_KineticBlastResettingInspect"
                    .Translate()
                    .ToString();
            }

            if (Energy + 0.0001f < KaraKeshProps.kineticBlastEnergyCost)
            {
                return "GR_KaraKesh_KineticBlastLowEnergyInspect"
                    .Translate()
                    .ToString();
            }

            return "GR_KaraKesh_KineticBlastReadyInspect"
                .Translate()
                .ToString();
        }

        private string GetNeuralAttackInspectText()
        {
            if (NeuralAttackCooldownRemainingTicks > 0)
            {
                return "GR_KaraKesh_NeuralAttackCooldownInspect".Translate(
                    NeuralAttackCooldownRemainingTicks.ToStringTicksToPeriod())
                    .ToString();
            }

            if (ShieldState != ShieldState.Active)
            {
                return "GR_KaraKesh_NeuralAttackResettingInspect"
                    .Translate()
                    .ToString();
            }

            if (Energy + 0.0001f < KaraKeshProps.neuralAttackEnergyCost)
            {
                return "GR_KaraKesh_NeuralAttackLowEnergyInspect"
                    .Translate()
                    .ToString();
            }

            return "GR_KaraKesh_NeuralAttackReadyInspect"
                .Translate()
                .ToString();
        }

        private static string AppendInspectLine(
            string current,
            string line)
        {
            if (line.NullOrEmpty())
            {
                return current;
            }

            return current.NullOrEmpty()
                ? line
                : current + "\n" + line;
        }
    }
}
