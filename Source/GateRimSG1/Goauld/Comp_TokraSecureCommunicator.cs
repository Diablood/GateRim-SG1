using System;
using System.Collections.Generic;
using System.Reflection;
using RimWorld;
using Verse;
using Verse.AI;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Trusted-tier Tok'ra communicator foundation.
    ///
    /// The communicator exposes discreet trusted-channel requests one small
    /// mechanic at a time. It remains non-trading and non-recruiting: support
    /// is defensive, rare and limited to the current map context.
    /// </summary>
    public class Comp_TokraSecureCommunicator : ThingComp
    {
        private const int DefensiveDiversionCooldownTicks = 300000;
        private const int DefensiveDiversionStunTicks = 180;
        private const int DefensiveDiversionMinimumVomitDelayTicks = 240;
        private const int DefensiveDiversionMaximumVomitDelayTicks = 600;
        private const int MaximumDefensiveDiversionTargets = 3;

        private int nextDefensiveDiversionRequestTick;
        private List<Pawn> pendingDiversionVomitPawns = new List<Pawn>();
        private List<int> pendingDiversionVomitTicks = new List<int>();

        public override void PostExposeData()
        {
            base.PostExposeData();

            Scribe_Values.Look(
                ref nextDefensiveDiversionRequestTick,
                "tokraNextDefensiveDiversionRequestTick",
                0);

            Scribe_Collections.Look(
                ref pendingDiversionVomitPawns,
                "tokraPendingDiversionVomitPawns",
                LookMode.Reference);

            Scribe_Collections.Look(
                ref pendingDiversionVomitTicks,
                "tokraPendingDiversionVomitTicks",
                LookMode.Value);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                EnsurePendingDiversionVomitLists();
            }
        }

        public override void CompTick()
        {
            base.CompTick();

            TickPendingDiversionVomitEffects();
        }

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            foreach (Gizmo gizmo in base.CompGetGizmosExtra())
            {
                yield return gizmo;
            }

            if (!parent.Spawned
                || (parent.Faction != null && parent.Faction != Faction.OfPlayer))
            {
                yield break;
            }

            Command_Action contactCommand = new Command_Action
            {
                defaultLabel = "GR_TokraSecureCommunicator_CommandLabel".Translate(),
                defaultDesc = "GR_TokraSecureCommunicator_CommandDesc".Translate(),
                action = TryOpenSecureChannel
            };

            string channelDisabledReason = GetChannelDisabledReason();

            if (!string.IsNullOrEmpty(channelDisabledReason))
            {
                contactCommand.Disable(channelDisabledReason);
            }

            yield return contactCommand;

            Command_Action diversionCommand = new Command_Action
            {
                defaultLabel = "GR_TokraSecureCommunicator_DiversionCommandLabel"
                    .Translate(),
                defaultDesc = "GR_TokraSecureCommunicator_DiversionCommandDesc"
                    .Translate(),
                action = TryRequestDefensiveDiversion
            };

            string diversionDisabledReason = GetDiversionDisabledReason();

            if (!string.IsNullOrEmpty(diversionDisabledReason))
            {
                diversionCommand.Disable(diversionDisabledReason);
            }

            yield return diversionCommand;
        }

        public override string CompInspectStringExtra()
        {
            if (!parent.Spawned)
            {
                return null;
            }

            return "GR_TokraSecureCommunicator_Inspect".Translate(
                GetTrustTierLabel(GameComponent_TokraTrustTracker.GetCurrentTier()),
                GetStatusLabel(),
                GetDiversionStatusLabel()).ToString();
        }

        private void TryOpenSecureChannel()
        {
            string disabledReason = GetChannelDisabledReason();

            if (!string.IsNullOrEmpty(disabledReason))
            {
                Messages.Message(
                    disabledReason,
                    parent,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    "GR_TokraSecureCommunicator_DialogTrusted".Translate()
                        .ToString()));

            Messages.Message(
                "GR_TokraSecureCommunicator_ChannelOpened".Translate(),
                parent,
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            GR_Log.Message(
                $"Opened trusted Tok'ra secure communicator channel at "
                + $"{parent.Position} on map "
                + $"{parent.Map?.uniqueID.ToString() ?? "unknown"}.");
        }

        private void TryRequestDefensiveDiversion()
        {
            string disabledReason = GetDiversionDisabledReason();

            if (!string.IsNullOrEmpty(disabledReason))
            {
                Messages.Message(
                    disabledReason,
                    parent,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            List<Pawn> targets = GetHostileThreats();
            ShuffleTargets(targets);

            int affectedCount = 0;
            int maximumTargets = Math.Min(
                MaximumDefensiveDiversionTargets,
                targets.Count);

            for (int i = 0; i < maximumTargets; i++)
            {
                if (TryApplyDiversionEffects(targets[i]))
                {
                    affectedCount++;
                }
            }

            if (affectedCount <= 0)
            {
                Messages.Message(
                    "GR_TokraSecureCommunicator_DiversionFailed".Translate(),
                    parent,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            nextDefensiveDiversionRequestTick = Find.TickManager.TicksGame
                + DefensiveDiversionCooldownTicks;

            Messages.Message(
                "GR_TokraSecureCommunicator_DiversionRequested".Translate(
                    affectedCount.ToString(),
                    FormatDays(DefensiveDiversionCooldownTicks)),
                parent,
                MessageTypeDefOf.PositiveEvent,
                historical: true);

            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    "GR_TokraSecureCommunicator_DiversionDialog".Translate(
                        affectedCount.ToString(),
                        FormatDays(DefensiveDiversionCooldownTicks))
                    .ToString()));

            GR_Log.Message(
                $"Requested trusted Tok'ra defensive diversion from "
                + $"communicator at {parent.Position} on map "
                + $"{parent.Map?.uniqueID.ToString() ?? "unknown"}; "
                + $"affected {affectedCount} hostile pawn(s).");
        }

        private string GetChannelDisabledReason()
        {
            if (parent.Faction != null && parent.Faction != Faction.OfPlayer)
            {
                return "GR_TokraSecureCommunicator_NotPlayerControlled"
                    .Translate()
                    .ToString();
            }

            CompPowerTrader powerComp = parent.GetComp<CompPowerTrader>();

            if (powerComp != null && !powerComp.PowerOn)
            {
                return "GR_TokraSecureCommunicator_Unpowered".Translate().ToString();
            }

            if (GameComponent_TokraTrustTracker.GetCurrentTier()
                != TokraTrustTier.Trusted)
            {
                return "GR_TokraSecureCommunicator_RequiresTrusted".Translate(
                    GetTrustTierLabel(
                        GameComponent_TokraTrustTracker.GetCurrentTier()))
                    .ToString();
            }

            return null;
        }

        private string GetDiversionDisabledReason()
        {
            string channelDisabledReason = GetChannelDisabledReason();

            if (!string.IsNullOrEmpty(channelDisabledReason))
            {
                return channelDisabledReason;
            }

            if (IsDefensiveDiversionCooldownActive())
            {
                return "GR_TokraSecureCommunicator_DiversionCooldown".Translate(
                    FormatDays(GetRemainingDefensiveDiversionCooldownTicks()))
                    .ToString();
            }

            if (!HasHostileThreats())
            {
                return "GR_TokraSecureCommunicator_DiversionNoThreat"
                    .Translate()
                    .ToString();
            }

            return null;
        }

        private string GetStatusLabel()
        {
            string disabledReason = GetChannelDisabledReason();

            if (string.IsNullOrEmpty(disabledReason))
            {
                return "GR_TokraSecureCommunicator_StatusReady".Translate().ToString();
            }

            return "GR_TokraSecureCommunicator_StatusLocked".Translate().ToString();
        }

        private string GetDiversionStatusLabel()
        {
            string channelDisabledReason = GetChannelDisabledReason();

            if (!string.IsNullOrEmpty(channelDisabledReason))
            {
                return "GR_TokraSecureCommunicator_DiversionStatusLocked"
                    .Translate()
                    .ToString();
            }

            if (IsDefensiveDiversionCooldownActive())
            {
                return "GR_TokraSecureCommunicator_DiversionStatusCooldown"
                    .Translate(FormatDays(
                        GetRemainingDefensiveDiversionCooldownTicks()))
                    .ToString();
            }

            if (!HasHostileThreats())
            {
                return "GR_TokraSecureCommunicator_DiversionStatusNoThreat"
                    .Translate()
                    .ToString();
            }

            return "GR_TokraSecureCommunicator_DiversionStatusReady"
                .Translate()
                .ToString();
        }

        private bool HasHostileThreats()
        {
            return GetHostileThreats().Count > 0;
        }

        private List<Pawn> GetHostileThreats()
        {
            List<Pawn> threats = new List<Pawn>();
            Map map = parent.Map;

            if (map == null)
            {
                return threats;
            }

            foreach (Pawn pawn in map.mapPawns.AllPawnsSpawned)
            {
                if (IsValidHostileThreat(pawn))
                {
                    threats.Add(pawn);
                }
            }

            return threats;
        }

        private static bool IsValidHostileThreat(Pawn pawn)
        {
            return pawn != null
                && pawn.Spawned
                && !pawn.Dead
                && !pawn.Downed
                && pawn.HostileTo(Faction.OfPlayer);
        }

        private bool IsDefensiveDiversionCooldownActive()
        {
            return GetRemainingDefensiveDiversionCooldownTicks() > 0;
        }

        private int GetRemainingDefensiveDiversionCooldownTicks()
        {
            if (Find.TickManager == null)
            {
                return 0;
            }

            return Math.Max(
                0,
                nextDefensiveDiversionRequestTick - Find.TickManager.TicksGame);
        }

        private bool TryApplyDiversionEffects(Pawn pawn)
        {
            if (!TryStunPawn(
                pawn,
                DefensiveDiversionStunTicks,
                parent))
            {
                return false;
            }

            ScheduleDelayedDiversionVomit(pawn);

            return true;
        }

        private void ScheduleDelayedDiversionVomit(Pawn pawn)
        {
            if (!CanReceiveDelayedVomit(pawn))
            {
                return;
            }

            EnsurePendingDiversionVomitLists();

            int existingIndex = pendingDiversionVomitPawns.IndexOf(pawn);
            int triggerTick = Find.TickManager.TicksGame
                + Rand.RangeInclusive(
                    DefensiveDiversionMinimumVomitDelayTicks,
                    DefensiveDiversionMaximumVomitDelayTicks);

            if (existingIndex >= 0)
            {
                pendingDiversionVomitTicks[existingIndex] = Math.Min(
                    pendingDiversionVomitTicks[existingIndex],
                    triggerTick);
                return;
            }

            pendingDiversionVomitPawns.Add(pawn);
            pendingDiversionVomitTicks.Add(triggerTick);
        }

        private void TickPendingDiversionVomitEffects()
        {
            if (!parent.Spawned
                || Find.TickManager == null)
            {
                return;
            }

            EnsurePendingDiversionVomitLists();

            int currentTick = Find.TickManager.TicksGame;

            for (int i = pendingDiversionVomitPawns.Count - 1; i >= 0; i--)
            {
                if (currentTick < pendingDiversionVomitTicks[i])
                {
                    continue;
                }

                Pawn pawn = pendingDiversionVomitPawns[i];
                pendingDiversionVomitPawns.RemoveAt(i);
                pendingDiversionVomitTicks.RemoveAt(i);

                TryStartDelayedVomit(pawn);
            }
        }

        private void EnsurePendingDiversionVomitLists()
        {
            if (pendingDiversionVomitPawns == null)
            {
                pendingDiversionVomitPawns = new List<Pawn>();
            }

            if (pendingDiversionVomitTicks == null)
            {
                pendingDiversionVomitTicks = new List<int>();
            }

            while (pendingDiversionVomitTicks.Count < pendingDiversionVomitPawns.Count)
            {
                pendingDiversionVomitTicks.Add(Find.TickManager?.TicksGame ?? 0);
            }

            while (pendingDiversionVomitPawns.Count < pendingDiversionVomitTicks.Count)
            {
                pendingDiversionVomitTicks.RemoveAt(
                    pendingDiversionVomitTicks.Count - 1);
            }
        }

        private static bool CanReceiveDelayedVomit(Pawn pawn)
        {
            return pawn != null
                && pawn.RaceProps != null
                && !pawn.RaceProps.IsMechanoid;
        }

        private static bool TryStartDelayedVomit(Pawn pawn)
        {
            if (!IsValidHostileThreat(pawn)
                || !CanReceiveDelayedVomit(pawn)
                || pawn.jobs == null)
            {
                return false;
            }

            JobDef vomitDef = DefDatabase<JobDef>.GetNamedSilentFail("Vomit");

            if (vomitDef == null)
            {
                return false;
            }

            try
            {
                pawn.jobs.StartJob(
                    JobMaker.MakeJob(vomitDef),
                    JobCondition.InterruptForced,
                    null,
                    true,
                    true);
                return true;
            }
            catch (Exception exception)
            {
                GR_Log.Warning(
                    $"Tok'ra defensive diversion could not start delayed vomit "
                    + $"for {pawn.LabelShortCap}: {exception.GetType().Name}.");
                return false;
            }
        }

        private static bool TryStunPawn(
            Pawn pawn,
            int ticks,
            Thing instigator)
        {
            object stunner = pawn?.stances?.stunner;

            if (stunner == null)
            {
                return false;
            }

            MethodInfo[] methods = stunner.GetType().GetMethods(
                BindingFlags.Instance
                | BindingFlags.Public
                | BindingFlags.NonPublic);

            for (int i = 0; i < methods.Length; i++)
            {
                MethodInfo method = methods[i];

                if (method.Name != "StunFor")
                {
                    continue;
                }

                ParameterInfo[] parameters = method.GetParameters();

                if (parameters.Length < 1
                    || parameters[0].ParameterType != typeof(int))
                {
                    continue;
                }

                object[] arguments = BuildStunForArguments(
                    parameters,
                    ticks,
                    instigator);

                if (arguments == null)
                {
                    continue;
                }

                try
                {
                    method.Invoke(stunner, arguments);
                    return true;
                }
                catch (Exception exception)
                {
                    GR_Log.Warning(
                        $"Tok'ra defensive diversion could not stun "
                        + $"{pawn.LabelShortCap}: {exception.GetType().Name}.");
                }
            }

            return false;
        }

        private static object[] BuildStunForArguments(
            ParameterInfo[] parameters,
            int ticks,
            Thing instigator)
        {
            object[] arguments = new object[parameters.Length];
            arguments[0] = ticks;

            for (int i = 1; i < parameters.Length; i++)
            {
                Type parameterType = parameters[i].ParameterType;

                if (parameterType == typeof(Pawn))
                {
                    arguments[i] = instigator as Pawn;
                }
                else if (parameterType == typeof(Thing))
                {
                    arguments[i] = instigator;
                }
                else if (parameterType == typeof(bool))
                {
                    arguments[i] = GetStunForBoolArgument(parameters[i]);
                }
                else if (parameters[i].HasDefaultValue)
                {
                    arguments[i] = parameters[i].DefaultValue;
                }
                else if (!parameterType.IsValueType)
                {
                    arguments[i] = null;
                }
                else
                {
                    return null;
                }
            }

            return arguments;
        }

        private static bool GetStunForBoolArgument(ParameterInfo parameter)
        {
            string name = parameter.Name ?? string.Empty;

            if (name.IndexOf("mote", StringComparison.OrdinalIgnoreCase) >= 0
                || name.IndexOf("show", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }

            return false;
        }

        private static void ShuffleTargets(List<Pawn> targets)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                int randomIndex = Rand.Range(i, targets.Count);
                Pawn current = targets[i];
                targets[i] = targets[randomIndex];
                targets[randomIndex] = current;
            }
        }

        private static string GetTrustTierLabel(TokraTrustTier tier)
        {
            switch (tier)
            {
                case TokraTrustTier.Wary:
                    return "GR_TokraTrust_Tier_Wary".Translate().ToString();
                case TokraTrustTier.Cooperative:
                    return "GR_TokraTrust_Tier_Cooperative".Translate().ToString();
                case TokraTrustTier.Trusted:
                    return "GR_TokraTrust_Tier_Trusted".Translate().ToString();
                default:
                    return "GR_TokraTrust_Tier_Neutral".Translate().ToString();
            }
        }

        private static string FormatDays(int ticks)
        {
            return (ticks / 60000f).ToString("0.#");
        }
    }
}
