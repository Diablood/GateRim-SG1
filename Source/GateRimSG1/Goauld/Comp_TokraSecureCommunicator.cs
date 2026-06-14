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
        private const int MedicalSupportCooldownTicks = 180000;
        private const int MedicalSupportMedicineXp = 600;
        private const int DefensiveDiversionStunTicks = 180;
        private const int DefensiveDiversionMinimumVomitDelayTicks = 240;
        private const int DefensiveDiversionMaximumVomitDelayTicks = 600;
        private const int MaximumDefensiveDiversionTargets = 3;
        private const string UseCommunicatorJobDefName = "SG1_UseTokraSecureCommunicator";
        private const string RequestDiversionJobDefName = "SG1_RequestTokraDefensiveDiversion";
        private const string RequestMedicalSupportJobDefName = "SG1_RequestTokraMedicalSupport";

        private enum TokraCommunicatorOperation
        {
            OpenChannel,
            DefensiveDiversion,
            MedicalSupport
        }

        private int nextDefensiveDiversionRequestTick;
        private int nextMedicalSupportRequestTick;
        private List<Pawn> pendingDiversionVomitPawns = new List<Pawn>();
        private List<int> pendingDiversionVomitTicks = new List<int>();

        public override void PostExposeData()
        {
            base.PostExposeData();

            Scribe_Values.Look(
                ref nextDefensiveDiversionRequestTick,
                "tokraNextDefensiveDiversionRequestTick",
                0);

            Scribe_Values.Look(
                ref nextMedicalSupportRequestTick,
                "tokraNextMedicalSupportRequestTick",
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

            // Player-facing use goes through pawn right-click work. Direct
            // building gizmos are kept only for debug diagnostics.
            if (!GR_Debug.ShowAdvancedInformation)
            {
                yield break;
            }

            Command_Action contactCommand = new Command_Action
            {
                defaultLabel = "GR_TokraSecureCommunicator_CommandLabel".Translate(),
                defaultDesc = "GR_TokraSecureCommunicator_CommandDesc".Translate(),
                action = ShowPawnOperationRequiredMessage
            };

            string channelDisabledReason = GetChannelDisabledReason();

            contactCommand.Disable(GetGizmoDisabledReason(channelDisabledReason));

            yield return contactCommand;

            Command_Action diversionCommand = new Command_Action
            {
                defaultLabel = "GR_TokraSecureCommunicator_DiversionCommandLabel"
                    .Translate(),
                defaultDesc = "GR_TokraSecureCommunicator_DiversionCommandDesc"
                    .Translate(),
                action = ShowPawnOperationRequiredMessage
            };

            string diversionDisabledReason = GetDiversionDisabledReason();

            diversionCommand.Disable(GetGizmoDisabledReason(diversionDisabledReason));

            yield return diversionCommand;

            Command_Action medicalCommand = new Command_Action
            {
                defaultLabel = "GR_TokraSecureCommunicator_MedicalCommandLabel"
                    .Translate(),
                defaultDesc = "GR_TokraSecureCommunicator_MedicalCommandDesc"
                    .Translate(),
                action = ShowPawnOperationRequiredMessage
            };

            string medicalDisabledReason = GetMedicalSupportDisabledReason();

            medicalCommand.Disable(GetGizmoDisabledReason(medicalDisabledReason));

            yield return medicalCommand;
        }


        public override IEnumerable<FloatMenuOption> CompFloatMenuOptions(
            Pawn selPawn)
        {
            foreach (FloatMenuOption option in base.CompFloatMenuOptions(selPawn))
            {
                yield return option;
            }

            if (!parent.Spawned
                || (parent.Faction != null && parent.Faction != Faction.OfPlayer))
            {
                yield break;
            }

            foreach (FloatMenuOption option in GetOperateFloatMenuOptions(
                selPawn,
                UseCommunicatorJobDefName,
                "GR_TokraSecureCommunicator_FloatMenuUseLabel".Translate().ToString(),
                TokraCommunicatorOperation.OpenChannel))
            {
                yield return option;
            }

            foreach (FloatMenuOption option in GetOperateFloatMenuOptions(
                selPawn,
                RequestDiversionJobDefName,
                "GR_TokraSecureCommunicator_FloatMenuDiversionLabel".Translate().ToString(),
                TokraCommunicatorOperation.DefensiveDiversion))
            {
                yield return option;
            }

            foreach (FloatMenuOption option in GetOperateFloatMenuOptions(
                selPawn,
                RequestMedicalSupportJobDefName,
                "GR_TokraSecureCommunicator_FloatMenuMedicalLabel".Translate().ToString(),
                TokraCommunicatorOperation.MedicalSupport))
            {
                yield return option;
            }
        }

        private IEnumerable<FloatMenuOption> GetOperateFloatMenuOptions(
            Pawn selPawn,
            string jobDefName,
            string label,
            TokraCommunicatorOperation operation)
        {
            string disabledReason = GetPawnOperationDisabledReason(
                selPawn,
                operation);

            JobDef jobDef = GetOperationJobDef(jobDefName);

            if (jobDef == null && string.IsNullOrEmpty(disabledReason))
            {
                disabledReason = "GR_TokraSecureCommunicator_JobUnavailable"
                    .Translate()
                    .ToString();
            }

            if (!string.IsNullOrEmpty(disabledReason))
            {
                yield return new FloatMenuOption(
                    label + ": " + disabledReason,
                    null);
                yield break;
            }

            yield return new FloatMenuOption(
                label,
                delegate
                {
                    Job job = JobMaker.MakeJob(jobDef, parent);
                    selPawn.jobs.TryTakeOrderedJob(job);
                });
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
                GetDiversionStatusLabel(),
                GetMedicalSupportStatusLabel()).ToString();
        }

        internal bool TryOpenSecureChannel(Pawn operatorPawn)
        {
            string disabledReason = GetChannelDisabledReason();

            if (!string.IsNullOrEmpty(disabledReason))
            {
                Messages.Message(
                    disabledReason,
                    parent,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
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
                + $"{parent.Map?.uniqueID.ToString() ?? "unknown"}; "
                + $"operator {operatorPawn?.LabelShortCap ?? "unknown"}.");

            return true;
        }

        internal bool TryRequestDefensiveDiversion(Pawn operatorPawn)
        {
            string disabledReason = GetDiversionDisabledReason();

            if (!string.IsNullOrEmpty(disabledReason))
            {
                Messages.Message(
                    disabledReason,
                    parent,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
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
                return false;
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
                + $"affected {affectedCount} hostile pawn(s); "
                + $"operator {operatorPawn?.LabelShortCap ?? "unknown"}.");

            return true;
        }


        internal bool TryRequestMedicalSupport(Pawn operatorPawn)
        {
            string disabledReason = GetMedicalSupportDisabledReason();

            if (!string.IsNullOrEmpty(disabledReason))
            {
                Messages.Message(
                    disabledReason,
                    parent,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            if (!CanReceiveMedicalGuidance(operatorPawn))
            {
                Messages.Message(
                    "GR_TokraSecureCommunicator_MedicalOperatorIncapable".Translate(),
                    parent,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            List<Pawn> patients = GetMedicalSupportCandidates();
            int patientCount = patients.Count;

            if (patientCount <= 0)
            {
                Messages.Message(
                    "GR_TokraSecureCommunicator_MedicalNoPatient".Translate(),
                    parent,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            if (!TryGrantMedicalGuidanceExperience(operatorPawn))
            {
                Messages.Message(
                    "GR_TokraSecureCommunicator_MedicalOperatorIncapable".Translate(),
                    parent,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            nextMedicalSupportRequestTick = Find.TickManager.TicksGame
                + MedicalSupportCooldownTicks;

            Messages.Message(
                "GR_TokraSecureCommunicator_MedicalRequested".Translate(
                    operatorPawn.LabelShortCap,
                    MedicalSupportMedicineXp.ToString(),
                    patientCount.ToString(),
                    FormatDays(MedicalSupportCooldownTicks)),
                parent,
                MessageTypeDefOf.PositiveEvent,
                historical: true);

            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    "GR_TokraSecureCommunicator_MedicalDialog".Translate(
                        operatorPawn.LabelShortCap,
                        MedicalSupportMedicineXp.ToString(),
                        patientCount.ToString(),
                        FormatDays(MedicalSupportCooldownTicks))
                    .ToString()));

            GR_Log.Message(
                $"Requested trusted Tok'ra medical guidance from "
                + $"communicator at {parent.Position} on map "
                + $"{parent.Map?.uniqueID.ToString() ?? "unknown"}; "
                + $"operator {operatorPawn.LabelShortCap}; "
                + $"patient candidates {patientCount}.");

            return true;
        }


        private void ShowPawnOperationRequiredMessage()
        {
            Messages.Message(
                "GR_TokraSecureCommunicator_SelectPawnToUse".Translate(),
                parent,
                MessageTypeDefOf.RejectInput,
                historical: false);
        }

        private string GetGizmoDisabledReason(string operationDisabledReason)
        {
            if (!string.IsNullOrEmpty(operationDisabledReason))
            {
                return operationDisabledReason;
            }

            return "GR_TokraSecureCommunicator_SelectPawnToUse"
                .Translate()
                .ToString();
        }

        private string GetPawnOperationDisabledReason(
            Pawn operatorPawn,
            TokraCommunicatorOperation operation)
        {
            if (!CanUsePlayerOperator(operatorPawn))
            {
                return "GR_TokraSecureCommunicator_PlayerPawnRequired"
                    .Translate()
                    .ToString();
            }

            if (!operatorPawn.CanReach(parent, PathEndMode.Touch, Danger.Deadly))
            {
                return "GR_TokraSecureCommunicator_CannotReach"
                    .Translate()
                    .ToString();
            }

            if (!operatorPawn.CanReserve(parent))
            {
                return "GR_TokraSecureCommunicator_Reserved"
                    .Translate()
                    .ToString();
            }

            if (operation == TokraCommunicatorOperation.MedicalSupport
                && !CanReceiveMedicalGuidance(operatorPawn))
            {
                return "GR_TokraSecureCommunicator_MedicalOperatorIncapable"
                    .Translate()
                    .ToString();
            }

            return GetDisabledReasonForOperation(operation);
        }

        private string GetDisabledReasonForOperation(
            TokraCommunicatorOperation operation)
        {
            switch (operation)
            {
                case TokraCommunicatorOperation.DefensiveDiversion:
                    return GetDiversionDisabledReason();
                case TokraCommunicatorOperation.MedicalSupport:
                    return GetMedicalSupportDisabledReason();
                default:
                    return GetChannelDisabledReason();
            }
        }

        private static bool CanUsePlayerOperator(Pawn pawn)
        {
            return pawn != null
                && !pawn.Dead
                && !pawn.Downed
                && pawn.Faction == Faction.OfPlayer
                && pawn.RaceProps?.Humanlike == true;
        }

        private static JobDef GetOperationJobDef(string defName)
        {
            return DefDatabase<JobDef>.GetNamedSilentFail(defName);
        }

        internal string GetChannelDisabledReason()
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

        internal string GetDiversionDisabledReason()
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

        internal string GetMedicalSupportDisabledReason()
        {
            string channelDisabledReason = GetChannelDisabledReason();

            if (!string.IsNullOrEmpty(channelDisabledReason))
            {
                return channelDisabledReason;
            }

            if (IsMedicalSupportCooldownActive())
            {
                return "GR_TokraSecureCommunicator_MedicalCooldown".Translate(
                    FormatDays(GetRemainingMedicalSupportCooldownTicks()))
                    .ToString();
            }

            if (!HasMedicalSupportCandidates())
            {
                return "GR_TokraSecureCommunicator_MedicalNoPatient"
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

        private string GetMedicalSupportStatusLabel()
        {
            string channelDisabledReason = GetChannelDisabledReason();

            if (!string.IsNullOrEmpty(channelDisabledReason))
            {
                return "GR_TokraSecureCommunicator_MedicalStatusLocked"
                    .Translate()
                    .ToString();
            }

            if (IsMedicalSupportCooldownActive())
            {
                return "GR_TokraSecureCommunicator_MedicalStatusCooldown"
                    .Translate(FormatDays(GetRemainingMedicalSupportCooldownTicks()))
                    .ToString();
            }

            if (!HasMedicalSupportCandidates())
            {
                return "GR_TokraSecureCommunicator_MedicalStatusNoPatient"
                    .Translate()
                    .ToString();
            }

            return "GR_TokraSecureCommunicator_MedicalStatusReady"
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

        private bool HasMedicalSupportCandidates()
        {
            return GetMedicalSupportCandidates().Count > 0;
        }

        private List<Pawn> GetMedicalSupportCandidates()
        {
            List<Pawn> candidates = new List<Pawn>();
            Map map = parent.Map;

            if (map == null)
            {
                return candidates;
            }

            foreach (Pawn pawn in map.mapPawns.AllPawnsSpawned)
            {
                if (IsValidMedicalSupportCandidate(pawn))
                {
                    candidates.Add(pawn);
                }
            }

            return candidates;
        }

        private static bool IsValidMedicalSupportCandidate(Pawn pawn)
        {
            return pawn != null
                && pawn.Spawned
                && !pawn.Dead
                && pawn.Faction == Faction.OfPlayer
                && pawn.RaceProps?.Humanlike == true
                && HasCurrentMedicalConcern(pawn);
        }

        private static bool HasCurrentMedicalConcern(Pawn pawn)
        {
            if (pawn?.health?.hediffSet?.hediffs == null)
            {
                return false;
            }

            List<Hediff> hediffs = pawn.health.hediffSet.hediffs;

            for (int i = 0; i < hediffs.Count; i++)
            {
                Hediff hediff = hediffs[i];

                if (hediff == null)
                {
                    continue;
                }

                if (hediff.Bleeding || hediff.TendableNow())
                {
                    return true;
                }

                if (hediff is Hediff_Injury && hediff.Severity > 0.05f)
                {
                    return true;
                }

                if (hediff.def != null
                    && hediff.def.isBad
                    && hediff.Visible
                    && hediff.Severity > 0.05f)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool CanReceiveMedicalGuidance(Pawn pawn)
        {
            if (!CanUsePlayerOperator(pawn) || pawn.skills == null)
            {
                return false;
            }

            SkillRecord medicine = pawn.skills.GetSkill(SkillDefOf.Medicine);

            return medicine != null && !medicine.TotallyDisabled;
        }

        private static bool TryGrantMedicalGuidanceExperience(Pawn pawn)
        {
            if (!CanReceiveMedicalGuidance(pawn))
            {
                return false;
            }

            SkillRecord medicine = pawn.skills.GetSkill(SkillDefOf.Medicine);
            medicine.Learn(MedicalSupportMedicineXp, true);
            return true;
        }

        private bool IsMedicalSupportCooldownActive()
        {
            return GetRemainingMedicalSupportCooldownTicks() > 0;
        }

        private int GetRemainingMedicalSupportCooldownTicks()
        {
            if (Find.TickManager == null)
            {
                return 0;
            }

            return Math.Max(
                0,
                nextMedicalSupportRequestTick - Find.TickManager.TicksGame);
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
