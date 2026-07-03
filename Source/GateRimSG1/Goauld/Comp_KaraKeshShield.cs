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

        public CompProperties_KaraKeshShield()
        {
            compClass = typeof(Comp_KaraKeshShield);
        }
    }

    public class Comp_KaraKeshShield : CompShield
    {
        private const int NoAbsorbedHitTick = -99999;
        private const int NoKineticBlastTick = -99999;

        private int lastAbsorbedHitTick = NoAbsorbedHitTick;
        private int lastKineticBlastTick = NoKineticBlastTick;

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

                return baseText.NullOrEmpty()
                    ? inactiveText
                    : baseText + "\n" + inactiveText;
            }

            string blastText;

            if (KineticBlastCooldownRemainingTicks > 0)
            {
                blastText = "GR_KaraKesh_KineticBlastCooldownInspect".Translate(
                    KineticBlastCooldownRemainingTicks.ToStringTicksToPeriod())
                    .ToString();
            }
            else if (ShieldState != ShieldState.Active)
            {
                blastText = "GR_KaraKesh_KineticBlastResettingInspect"
                    .Translate()
                    .ToString();
            }
            else if (Energy + 0.0001f < KaraKeshProps.kineticBlastEnergyCost)
            {
                blastText = "GR_KaraKesh_KineticBlastLowEnergyInspect"
                    .Translate()
                    .ToString();
            }
            else
            {
                blastText = "GR_KaraKesh_KineticBlastReadyInspect"
                    .Translate()
                    .ToString();
            }

            return baseText.NullOrEmpty()
                ? blastText
                : baseText + "\n" + blastText;
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

            TryUseKineticBlastForAi(wearer);

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
                            ? "GR_KaraKesh_KineticBlastInvalidTarget".Translate().ToString()
                            : disabledReason,
                        wearer,
                        MessageTypeDefOf.RejectInput,
                        historical: false);
                }

                return false;
            }

            wearer.rotationTracker?.FaceTarget(target);
            ConsumeKineticBlastEnergy();
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

        public int KineticBlastCooldownRemainingTicks
        {
            get
            {
                int currentTick = Find.TickManager?.TicksGame ?? 0;
                int cooldown = KaraKeshProps?.kineticBlastCooldownTicks ?? 0;
                return Math.Max(0, cooldown - (currentTick - lastKineticBlastTick));
            }
        }

        internal void PrepareKineticBlastForDebug()
        {
            energy = parent?.GetStatValue(
                StatDefOf.EnergyShieldEnergyMax) ?? 0f;
            ticksToReset = -1;
            lastAbsorbedHitTick = NoAbsorbedHitTick;
            lastKineticBlastTick = NoKineticBlastTick;
            KeepDisplaying();
        }

        internal void ResetKineticBlastCooldownForDebug()
        {
            lastKineticBlastTick = NoKineticBlastTick;
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

        private bool CanUseKineticBlast(out string disabledReason)
        {
            Pawn wearer = PawnOwner;

            if (wearer?.Spawned != true || wearer.Map == null)
            {
                disabledReason = "GR_KaraKesh_KineticBlastUnavailable".Translate().ToString();
                return false;
            }

            if (!WearerCanActivate)
            {
                disabledReason = "GR_KaraKesh_KineticBlastNeedsNaquadah".Translate().ToString();
                return false;
            }

            if (wearer.Dead || wearer.Downed)
            {
                disabledReason = "GR_KaraKesh_KineticBlastIncapacitated".Translate().ToString();
                return false;
            }

            if (ShieldState != ShieldState.Active)
            {
                disabledReason = "GR_KaraKesh_KineticBlastShieldResetting".Translate().ToString();
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

        private bool IsValidKineticBlastTarget(Pawn wearer, Pawn target)
        {
            if (wearer?.Spawned != true
                || target?.Spawned != true
                || target.Destroyed
                || target.Dead
                || target.Downed
                || target == wearer
                || target.Map != wearer.Map
                || !target.HostileTo(wearer))
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

        private void ConsumeKineticBlastEnergy()
        {
            energy = Math.Max(
                0f,
                energy - KaraKeshProps.kineticBlastEnergyCost);
            KeepDisplaying();

            if (Find.TickManager != null)
            {
                lastAbsorbedHitTick = Find.TickManager.TicksGame;
            }
        }

        private void TryUseKineticBlastForAi(Pawn wearer)
        {
            if (wearer?.Spawned != true
                || wearer.Faction == null
                || wearer.Faction == Faction.OfPlayer
                || wearer.Dead
                || wearer.Downed
                || !wearer.IsHashIntervalTick(
                    Math.Max(1, KaraKeshProps.kineticBlastAiCheckIntervalTicks))
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

                if (!IsValidKineticBlastTarget(wearer, candidate))
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
    }
}
