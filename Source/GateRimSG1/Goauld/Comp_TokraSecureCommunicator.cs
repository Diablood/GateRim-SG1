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
        private const int MedicalCacheCooldownTicks = 420000;
        private const int ThreatAssessmentCooldownTicks = 60000;
        private const int OperationalDebriefCooldownTicks = 120000;
        private const int OperationalDebriefSkillXp = 400;
        private const int MedicalSupportMedicineXp = 600;
        private const int MedicalCacheIndustrialMedicineCount = 4;
        private const int MedicalCacheTretoninDoseCount = 1;
        private const int DefensiveDiversionStunTicks = 180;
        private const int DefensiveDiversionMinimumVomitDelayTicks = 240;
        private const int DefensiveDiversionMaximumVomitDelayTicks = 600;
        private const int MaximumDefensiveDiversionTargets = 3;
        private const string UseCommunicatorJobDefName = "SG1_UseTokraSecureCommunicator";
        private const string CheckStatusReportJobDefName = "SG1_CheckTokraCommunicatorStatus";
        private const string RequestDiversionJobDefName = "SG1_RequestTokraDefensiveDiversion";
        private const string RequestMedicalSupportJobDefName = "SG1_RequestTokraMedicalSupport";
        private const string RequestMedicalCacheJobDefName = "SG1_RequestTokraEmergencyMedicalCache";
        private const string RequestThreatAssessmentJobDefName = "SG1_RequestTokraThreatAssessment";
        private const string RequestOperationalDebriefJobDefName = "SG1_SendTokraOperationalDebrief";
        private const string RequestFirstTrustMissionJobDefName = "SG1_RequestTokraFirstTrustMission";

        private enum TokraCommunicatorOperation
        {
            OpenChannel,
            StatusReport,
            DefensiveDiversion,
            MedicalSupport,
            MedicalCache,
            ThreatAssessment,
            OperationalDebrief,
            FirstTrustMission
        }

        private int nextDefensiveDiversionRequestTick;
        private int nextMedicalSupportRequestTick;
        private int nextMedicalCacheRequestTick;
        private int nextThreatAssessmentRequestTick;
        private int nextOperationalDebriefRequestTick;
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

            Scribe_Values.Look(
                ref nextMedicalCacheRequestTick,
                "tokraNextMedicalCacheRequestTick",
                0);

            Scribe_Values.Look(
                ref nextThreatAssessmentRequestTick,
                "tokraNextThreatAssessmentRequestTick",
                0);

            Scribe_Values.Look(
                ref nextOperationalDebriefRequestTick,
                "tokraNextOperationalDebriefRequestTick",
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

            Command_Action threatAssessmentCommand = new Command_Action
            {
                defaultLabel = "GR_TokraSecureCommunicator_ThreatCommandLabel"
                    .Translate(),
                defaultDesc = "GR_TokraSecureCommunicator_ThreatCommandDesc"
                    .Translate(),
                action = ShowPawnOperationRequiredMessage
            };

            string threatAssessmentDisabledReason =
                GetThreatAssessmentDisabledReason();

            threatAssessmentCommand.Disable(GetGizmoDisabledReason(
                threatAssessmentDisabledReason));

            yield return threatAssessmentCommand;
            Command_Action firstMissionCommand = new Command_Action
            {
                defaultLabel = "GR_TokraSecureCommunicator_FirstMissionCommandLabel"
                    .Translate(),
                defaultDesc = "GR_TokraSecureCommunicator_FirstMissionCommandDesc"
                    .Translate(),
                action = ShowPawnOperationRequiredMessage
            };

            string firstMissionDisabledReason = GetFirstTrustMissionDisabledReason();

            firstMissionCommand.Disable(GetGizmoDisabledReason(
                firstMissionDisabledReason));

            yield return firstMissionCommand;

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

            Command_Action medicalCacheCommand = new Command_Action
            {
                defaultLabel = "GR_TokraSecureCommunicator_MedicalCacheCommandLabel"
                    .Translate(),
                defaultDesc = "GR_TokraSecureCommunicator_MedicalCacheCommandDesc"
                    .Translate(),
                action = ShowPawnOperationRequiredMessage
            };

            string medicalCacheDisabledReason = GetMedicalCacheDisabledReason();

            medicalCacheCommand.Disable(GetGizmoDisabledReason(
                medicalCacheDisabledReason));

            yield return medicalCacheCommand;
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
                CheckStatusReportJobDefName,
                "GR_TokraSecureCommunicator_FloatMenuStatusLabel"
                    .Translate()
                    .ToString(),
                TokraCommunicatorOperation.StatusReport))
            {
                yield return option;
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
                RequestFirstTrustMissionJobDefName,
                "GR_TokraSecureCommunicator_FloatMenuFirstMissionLabel"
                    .Translate()
                    .ToString(),
                TokraCommunicatorOperation.FirstTrustMission))
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
                RequestThreatAssessmentJobDefName,
                "GR_TokraSecureCommunicator_FloatMenuThreatLabel"
                    .Translate()
                    .ToString(),
                TokraCommunicatorOperation.ThreatAssessment))
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

            foreach (FloatMenuOption option in GetOperateFloatMenuOptions(
                selPawn,
                RequestMedicalCacheJobDefName,
                "GR_TokraSecureCommunicator_FloatMenuMedicalCacheLabel"
                    .Translate()
                    .ToString(),
                TokraCommunicatorOperation.MedicalCache))
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
                GetFirstTrustMissionStatusLabel(),
                GetDiversionStatusLabel(),
                GetThreatAssessmentStatusLabel(),
                GetMedicalSupportStatusLabel(),
                GetMedicalCacheStatusLabel()).ToString();
        }

        internal bool TryShowStatusReport(Pawn operatorPawn)
        {
            string disabledReason = GetStatusReportDisabledReason();

            if (!string.IsNullOrEmpty(disabledReason))
            {
                Messages.Message(
                    disabledReason,
                    parent,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            int trustScore = GameComponent_TokraTrustTracker.GetCurrentTrustScore();
            TokraTrustTier currentTier = GameComponent_TokraTrustTracker
                .GetCurrentTier();
            List<Pawn> threats = GetHostileThreats();
            List<Pawn> patients = GetMedicalSupportCandidates();
            string interceptedThreatStatus = GetInterceptedThreatStatusLabel();

            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    "GR_TokraSecureCommunicator_StatusReportDialog".Translate(
                        GetTrustTierLabel(currentTier),
                        GetTrustStatusReportLabel(currentTier),
                        GetTrustProgressStatusReportLabel(trustScore, currentTier),
                        GetStatusLabel(),
                        GetFirstTrustMissionStatusLabel(),
                        GetDiversionStatusLabel(),
                        GetThreatAssessmentStatusLabel(),
                        GetMedicalSupportStatusLabel(),
                        GetMedicalCacheStatusLabel(),
                        threats.Count.ToString(),
                        patients.Count.ToString(),
                        interceptedThreatStatus)
                    .ToString()));

            Messages.Message(
                "GR_TokraSecureCommunicator_StatusReportOpened".Translate(),
                parent,
                MessageTypeDefOf.NeutralEvent,
                historical: false);

            GR_Log.Message(
                $"Checked trusted Tok'ra communicator status at "
                + $"{parent.Position} on map "
                + $"{parent.Map?.uniqueID.ToString() ?? "unknown"}; "
                + $"operator {operatorPawn?.LabelShortCap ?? "unknown"}; "
                + $"hostiles {threats.Count}; patients {patients.Count}.");

            return true;
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

        internal bool TrySendOperationalDebrief(Pawn operatorPawn)
        {
            string disabledReason = GetOperationalDebriefDisabledReason();

            if (!string.IsNullOrEmpty(disabledReason))
            {
                Messages.Message(
                    disabledReason,
                    parent,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            string skillLabel;

            if (!TryGrantOperationalDebriefExperience(
                operatorPawn,
                out skillLabel))
            {
                Messages.Message(
                    "GR_TokraSecureCommunicator_DebriefOperatorIncapable"
                        .Translate(),
                    parent,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            nextOperationalDebriefRequestTick = Find.TickManager.TicksGame
                + OperationalDebriefCooldownTicks;

            Messages.Message(
                "GR_TokraSecureCommunicator_DebriefRequested".Translate(
                    operatorPawn.LabelShortCap,
                    OperationalDebriefSkillXp.ToString(),
                    skillLabel,
                    FormatDays(OperationalDebriefCooldownTicks)),
                parent,
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    "GR_TokraSecureCommunicator_DebriefDialog".Translate(
                        operatorPawn.LabelShortCap,
                        OperationalDebriefSkillXp.ToString(),
                        skillLabel,
                        FormatDays(OperationalDebriefCooldownTicks))
                    .ToString()));

            GR_Log.Message(
                $"Sent trusted Tok'ra operational debrief from "
                + $"communicator at {parent.Position} on map "
                + $"{parent.Map?.uniqueID.ToString() ?? "unknown"}; "
                + $"operator {operatorPawn.LabelShortCap}; "
                + $"trained {skillLabel} by {OperationalDebriefSkillXp} XP.");

            return true;
        }

        internal bool TryRequestFirstTrustMissionHook(Pawn operatorPawn)
        {
            string disabledReason = GetFirstTrustMissionDisabledReason();
            string operatorLabel = operatorPawn != null
                ? operatorPawn.LabelShortCap.ToString()
                : "Unknown";

            if (!string.IsNullOrEmpty(disabledReason))
            {
                Messages.Message(
                    disabledReason,
                    parent,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            if (!GameComponent_TokraTrustTracker
                .NotifyFirstTrustMissionHookPrepared())
            {
                Messages.Message(
                    "GR_TokraSecureCommunicator_FirstMissionFailed".Translate(),
                    parent,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            Messages.Message(
                "GR_TokraSecureCommunicator_FirstMissionRequested".Translate(
                    operatorLabel),
                parent,
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    "GR_TokraSecureCommunicator_FirstMissionDialog".Translate(
                        operatorLabel)
                    .ToString()));

            GR_Log.Message(
                $"Prepared trusted Tok'ra first mission hook from "
                + $"communicator at {parent.Position} on map "
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


        internal bool TryRequestTacticalThreatAssessment(Pawn operatorPawn)
        {
            string disabledReason = GetThreatAssessmentDisabledReason();

            if (!string.IsNullOrEmpty(disabledReason))
            {
                Messages.Message(
                    disabledReason,
                    parent,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            if (GameComponent_TokraInterceptedThreatTracker
                .HasActiveThreatForMap(parent.Map))
            {
                return TryRequestInterceptedThreatAssessment(operatorPawn);
            }

            List<Pawn> threats = GetHostileThreats();

            int hostileCount = threats.Count;
            int humanlikeCount = CountHumanlikeThreats(threats);
            int mechanoidCount = CountMechanoidThreats(threats);
            int otherCount = Math.Max(
                0,
                hostileCount - humanlikeCount - mechanoidCount);
            string severityLabel = GetThreatSeverityLabel(
                hostileCount,
                mechanoidCount);

            nextThreatAssessmentRequestTick = Find.TickManager.TicksGame
                + ThreatAssessmentCooldownTicks;

            Messages.Message(
                "GR_TokraSecureCommunicator_ThreatRequested".Translate(
                    hostileCount.ToString(),
                    severityLabel,
                    FormatDays(ThreatAssessmentCooldownTicks)),
                parent,
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    "GR_TokraSecureCommunicator_ThreatDialog".Translate(
                        hostileCount.ToString(),
                        humanlikeCount.ToString(),
                        mechanoidCount.ToString(),
                        otherCount.ToString(),
                        severityLabel,
                        FormatDays(ThreatAssessmentCooldownTicks))
                    .ToString()));

            GR_Log.Message(
                $"Requested trusted Tok'ra tactical threat assessment from "
                + $"communicator at {parent.Position} on map "
                + $"{parent.Map?.uniqueID.ToString() ?? "unknown"}; "
                + $"operator {operatorPawn?.LabelShortCap ?? "unknown"}; "
                + $"hostiles {hostileCount}, humanlike {humanlikeCount}, "
                + $"mechanoid {mechanoidCount}, other {otherCount}.");

            return true;
        }


        private bool TryRequestInterceptedThreatAssessment(Pawn operatorPawn)
        {
            nextThreatAssessmentRequestTick = Find.TickManager.TicksGame
                + ThreatAssessmentCooldownTicks;

            GameComponent_TokraInterceptedThreatTracker
                .NotifyTacticalAssessmentRequested(parent.Map);

            string severityLabel = GameComponent_TokraInterceptedThreatTracker
                .GetThreatSeverityLabelForMap(parent.Map);

            Messages.Message(
                "GR_TokraSecureCommunicator_InterceptedThreatRequested"
                    .Translate(
                        GameComponent_TokraInterceptedThreatTracker
                            .GetThreatSignatureLabelForMap(parent.Map),
                        severityLabel,
                        GameComponent_TokraInterceptedThreatTracker
                            .GetRemainingThreatWindowLabelForMap(parent.Map),
                        FormatDays(ThreatAssessmentCooldownTicks)),
                parent,
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    "GR_TokraSecureCommunicator_InterceptedThreatDialog"
                        .Translate(
                            GameComponent_TokraInterceptedThreatTracker
                                .GetThreatSignatureLabelForMap(parent.Map),
                            GameComponent_TokraInterceptedThreatTracker
                                .GetThreatIntentLabelForMap(parent.Map),
                            GameComponent_TokraInterceptedThreatTracker
                                .GetRemainingThreatWindowLabelForMap(parent.Map),
                            severityLabel,
                            GameComponent_TokraInterceptedThreatTracker
                                .GetThreatPreparationAdviceLabelForMap(parent.Map),
                            FormatDays(ThreatAssessmentCooldownTicks))
                        .ToString()));

            GR_Log.Message(
                $"Requested trusted Tok'ra intercepted-threat assessment from "
                + $"communicator at {parent.Position} on map "
                + $"{parent.Map?.uniqueID.ToString() ?? "unknown"}; "
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


        internal bool TryRequestEmergencyMedicalCache(Pawn operatorPawn)
        {
            string disabledReason = GetMedicalCacheDisabledReason();

            if (!string.IsNullOrEmpty(disabledReason))
            {
                Messages.Message(
                    disabledReason,
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

            int medicineCount;
            int tretoninDoseCount;

            if (!TryPlaceEmergencyMedicalCache(
                out medicineCount,
                out tretoninDoseCount))
            {
                Messages.Message(
                    "GR_TokraSecureCommunicator_MedicalCacheFailed".Translate(),
                    parent,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return false;
            }

            nextMedicalCacheRequestTick = Find.TickManager.TicksGame
                + MedicalCacheCooldownTicks;

            Messages.Message(
                "GR_TokraSecureCommunicator_MedicalCacheRequested".Translate(
                    medicineCount.ToString(),
                    tretoninDoseCount.ToString(),
                    patientCount.ToString(),
                    FormatDays(MedicalCacheCooldownTicks)),
                parent,
                MessageTypeDefOf.PositiveEvent,
                historical: true);

            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    "GR_TokraSecureCommunicator_MedicalCacheDialog".Translate(
                        medicineCount.ToString(),
                        tretoninDoseCount.ToString(),
                        patientCount.ToString(),
                        FormatDays(MedicalCacheCooldownTicks))
                    .ToString()));

            GR_Log.Message(
                $"Requested trusted Tok'ra emergency medical cache from "
                + $"communicator at {parent.Position} on map "
                + $"{parent.Map?.uniqueID.ToString() ?? "unknown"}; "
                + $"operator {operatorPawn?.LabelShortCap ?? "unknown"}; "
                + $"placed medicine {medicineCount}, tretonin {tretoninDoseCount}; "
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

            if (operation == TokraCommunicatorOperation.StatusReport)
            {
                return GetStatusReportDisabledReason();
            }

            if (operation == TokraCommunicatorOperation.MedicalSupport
                && !CanReceiveMedicalGuidance(operatorPawn))
            {
                return "GR_TokraSecureCommunicator_MedicalOperatorIncapable"
                    .Translate()
                    .ToString();
            }

            if (operation == TokraCommunicatorOperation.OperationalDebrief
                && !CanReceiveOperationalDebriefExperience(operatorPawn))
            {
                return "GR_TokraSecureCommunicator_DebriefOperatorIncapable"
                    .Translate()
                    .ToString();
            }

            return GetFloatMenuDisabledReasonForOperation(operation);
        }

        private string GetFloatMenuDisabledReasonForOperation(
            TokraCommunicatorOperation operation)
        {
            if (operation == TokraCommunicatorOperation.StatusReport)
            {
                return GetStatusReportDisabledReason();
            }

            string channelDisabledReason = GetBasicChannelFloatMenuDisabledReason();

            if (!string.IsNullOrEmpty(channelDisabledReason))
            {
                return channelDisabledReason;
            }

            switch (operation)
            {
                case TokraCommunicatorOperation.OperationalDebrief:
                    if (IsOperationalDebriefCooldownActive())
                    {
                        return "GR_TokraSecureCommunicator_FloatMenuCooldown"
                            .Translate(FormatDays(
                                GetRemainingOperationalDebriefCooldownTicks()))
                            .ToString();
                    }

                    return null;

                case TokraCommunicatorOperation.FirstTrustMission:
                    if (GameComponent_TokraTrustTracker
                        .IsFirstTrustMissionBriefingReceived())
                    {
                        return "GR_TokraSecureCommunicator_FloatMenuFirstMissionBriefingReceived"
                            .Translate()
                            .ToString();
                    }

                    if (GameComponent_TokraTrustTracker
                        .IsFirstTrustMissionHookPrepared())
                    {
                        return "GR_TokraSecureCommunicator_FloatMenuFirstMissionPrepared"
                            .Translate()
                            .ToString();
                    }

                    return null;

                case TokraCommunicatorOperation.DefensiveDiversion:
                    if (IsDefensiveDiversionCooldownActive())
                    {
                        return "GR_TokraSecureCommunicator_FloatMenuCooldown"
                            .Translate(FormatDays(
                                GetRemainingDefensiveDiversionCooldownTicks()))
                            .ToString();
                    }

                    if (!HasHostileThreats())
                    {
                        return "GR_TokraSecureCommunicator_FloatMenuNoThreat"
                            .Translate()
                            .ToString();
                    }

                    return null;

                case TokraCommunicatorOperation.ThreatAssessment:
                    if (IsThreatAssessmentCooldownActive())
                    {
                        return "GR_TokraSecureCommunicator_FloatMenuCooldown"
                            .Translate(FormatDays(
                                GetRemainingThreatAssessmentCooldownTicks()))
                            .ToString();
                    }

                    if (!HasThreatAssessmentContext())
                    {
                        return "GR_TokraSecureCommunicator_FloatMenuNoThreat"
                            .Translate()
                            .ToString();
                    }

                    return null;

                case TokraCommunicatorOperation.MedicalSupport:
                    if (IsMedicalSupportCooldownActive())
                    {
                        return "GR_TokraSecureCommunicator_FloatMenuCooldown"
                            .Translate(FormatDays(
                                GetRemainingMedicalSupportCooldownTicks()))
                            .ToString();
                    }

                    if (!HasMedicalSupportCandidates())
                    {
                        return "GR_TokraSecureCommunicator_FloatMenuNoPatient"
                            .Translate()
                            .ToString();
                    }

                    return null;

                case TokraCommunicatorOperation.MedicalCache:
                    if (IsMedicalCacheCooldownActive())
                    {
                        return "GR_TokraSecureCommunicator_FloatMenuCooldown"
                            .Translate(FormatDays(
                                GetRemainingMedicalCacheCooldownTicks()))
                            .ToString();
                    }

                    if (!HasMedicalSupportCandidates())
                    {
                        return "GR_TokraSecureCommunicator_FloatMenuNoPatient"
                            .Translate()
                            .ToString();
                    }

                    return null;

                default:
                    return null;
            }
        }

        private string GetBasicChannelFloatMenuDisabledReason()
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
                return "GR_TokraSecureCommunicator_FloatMenuUnpowered"
                    .Translate()
                    .ToString();
            }

            if (GameComponent_TokraTrustTracker.GetCurrentTier()
                != TokraTrustTier.Trusted)
            {
                return "GR_TokraSecureCommunicator_FloatMenuTrustInsufficient"
                    .Translate()
                    .ToString();
            }

            return null;
        }

        private string GetDisabledReasonForOperation(
            TokraCommunicatorOperation operation)
        {
            switch (operation)
            {
                case TokraCommunicatorOperation.StatusReport:
                    return GetStatusReportDisabledReason();
                case TokraCommunicatorOperation.DefensiveDiversion:
                    return GetDiversionDisabledReason();
                case TokraCommunicatorOperation.MedicalSupport:
                    return GetMedicalSupportDisabledReason();
                case TokraCommunicatorOperation.MedicalCache:
                    return GetMedicalCacheDisabledReason();
                case TokraCommunicatorOperation.ThreatAssessment:
                    return GetThreatAssessmentDisabledReason();
                case TokraCommunicatorOperation.OperationalDebrief:
                    return GetOperationalDebriefDisabledReason();
                case TokraCommunicatorOperation.FirstTrustMission:
                    return GetFirstTrustMissionDisabledReason();
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

        internal string GetStatusReportDisabledReason()
        {
            if (parent.Faction != null && parent.Faction != Faction.OfPlayer)
            {
                return "GR_TokraSecureCommunicator_NotPlayerControlled"
                    .Translate()
                    .ToString();
            }

            return null;
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

        internal string GetOperationalDebriefDisabledReason()
        {
            string channelDisabledReason = GetChannelDisabledReason();

            if (!string.IsNullOrEmpty(channelDisabledReason))
            {
                return channelDisabledReason;
            }

            if (IsOperationalDebriefCooldownActive())
            {
                return "GR_TokraSecureCommunicator_DebriefCooldown".Translate(
                    FormatDays(GetRemainingOperationalDebriefCooldownTicks()))
                    .ToString();
            }

            return null;
        }

        internal string GetFirstTrustMissionDisabledReason()
        {
            string channelDisabledReason = GetChannelDisabledReason();

            if (!string.IsNullOrEmpty(channelDisabledReason))
            {
                return channelDisabledReason;
            }

            if (GameComponent_TokraTrustTracker
                .IsFirstTrustMissionBriefingReceived())
            {
                return "GR_TokraSecureCommunicator_FirstMissionBriefingAlreadyReceived"
                    .Translate()
                    .ToString();
            }

            if (GameComponent_TokraTrustTracker.IsFirstTrustMissionHookPrepared())
            {
                return "GR_TokraSecureCommunicator_FirstMissionAlreadyPreparing"
                    .Translate()
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

        internal string GetThreatAssessmentDisabledReason()
        {
            string channelDisabledReason = GetChannelDisabledReason();

            if (!string.IsNullOrEmpty(channelDisabledReason))
            {
                return channelDisabledReason;
            }

            if (IsThreatAssessmentCooldownActive())
            {
                return "GR_TokraSecureCommunicator_ThreatCooldown".Translate(
                    FormatDays(GetRemainingThreatAssessmentCooldownTicks()))
                    .ToString();
            }

            if (!HasThreatAssessmentContext())
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

        internal string GetMedicalCacheDisabledReason()
        {
            string channelDisabledReason = GetChannelDisabledReason();

            if (!string.IsNullOrEmpty(channelDisabledReason))
            {
                return channelDisabledReason;
            }

            if (IsMedicalCacheCooldownActive())
            {
                return "GR_TokraSecureCommunicator_MedicalCacheCooldown".Translate(
                    FormatDays(GetRemainingMedicalCacheCooldownTicks()))
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

        private string GetTrustStatusReportLabel(TokraTrustTier tier)
        {
            if (tier == TokraTrustTier.Trusted)
            {
                return "GR_TokraSecureCommunicator_StatusReportTrustSufficient"
                    .Translate()
                    .ToString();
            }

            return "GR_TokraSecureCommunicator_StatusReportTrustInsufficient"
                .Translate()
                .ToString();
        }

        private string GetTrustProgressStatusReportLabel(
            int trustScore,
            TokraTrustTier tier)
        {
            switch (tier)
            {
                case TokraTrustTier.Wary:
                    if (trustScore >= -5)
                    {
                        return "GR_TokraSecureCommunicator_StatusReportTrustWaryNearNeutral"
                            .Translate()
                            .ToString();
                    }

                    return "GR_TokraSecureCommunicator_StatusReportTrustWaryDistant"
                        .Translate()
                        .ToString();

                case TokraTrustTier.Neutral:
                    if (trustScore
                        >= GameComponent_TokraTrustTracker.CooperativeThreshold - 2)
                    {
                        return "GR_TokraSecureCommunicator_StatusReportTrustNeutralNearCooperative"
                            .Translate()
                            .ToString();
                    }

                    return "GR_TokraSecureCommunicator_StatusReportTrustNeutralCautious"
                        .Translate()
                        .ToString();

                case TokraTrustTier.Cooperative:
                    if (trustScore
                        >= GameComponent_TokraTrustTracker.TrustedThreshold - 3)
                    {
                        return "GR_TokraSecureCommunicator_StatusReportTrustCooperativeNearTrusted"
                            .Translate()
                            .ToString();
                    }

                    return "GR_TokraSecureCommunicator_StatusReportTrustCooperativeStable"
                        .Translate()
                        .ToString();

                case TokraTrustTier.Trusted:
                    return "GR_TokraSecureCommunicator_StatusReportTrustTrustedStable"
                        .Translate()
                        .ToString();

                default:
                    return "GR_TokraSecureCommunicator_StatusReportTrustNeutralCautious"
                        .Translate()
                        .ToString();
            }
        }

        private string GetPowerStatusReportLabel()
        {
            CompPowerTrader powerComp = parent.GetComp<CompPowerTrader>();

            if (powerComp == null)
            {
                return "GR_TokraSecureCommunicator_StatusReportPowerNotApplicable"
                    .Translate()
                    .ToString();
            }

            if (powerComp.PowerOn)
            {
                return "GR_TokraSecureCommunicator_StatusReportPowerOn"
                    .Translate()
                    .ToString();
            }

            return "GR_TokraSecureCommunicator_StatusReportPowerOff"
                .Translate()
                .ToString();
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

        private string GetOperationalDebriefStatusLabel()
        {
            string channelDisabledReason = GetChannelDisabledReason();

            if (!string.IsNullOrEmpty(channelDisabledReason))
            {
                return "GR_TokraSecureCommunicator_DebriefStatusLocked"
                    .Translate()
                    .ToString();
            }

            if (IsOperationalDebriefCooldownActive())
            {
                return "GR_TokraSecureCommunicator_DebriefStatusCooldown"
                    .Translate(FormatDays(
                        GetRemainingOperationalDebriefCooldownTicks()))
                    .ToString();
            }

            return "GR_TokraSecureCommunicator_DebriefStatusReady"
                .Translate()
                .ToString();
        }

        private string GetFirstTrustMissionStatusLabel()
        {
            if (GameComponent_TokraTrustTracker
                .IsFirstTrustMissionOutcomeDebriefReceived())
            {
                string outcomeStatusKey = GameComponent_TokraTrustTracker
                    .WasFirstTrustMissionOutcomeSuccessful()
                    ? "GR_TokraSecureCommunicator_FirstMissionStatusOutcomeDebriefSuccess"
                    : "GR_TokraSecureCommunicator_FirstMissionStatusOutcomeDebriefFailure";

                return outcomeStatusKey.Translate().ToString();
            }

            if (GameComponent_TokraTrustTracker
                .IsFirstTrustMissionOutcomeRecorded())
            {
                int remainingDebriefTicks = GameComponent_TokraTrustTracker
                    .GetRemainingFirstTrustMissionOutcomeDebriefTicks();

                return "GR_TokraSecureCommunicator_FirstMissionStatusOutcomeDebriefPending"
                    .Translate(FormatDays(remainingDebriefTicks))
                    .ToString();
            }

            string channelDisabledReason = GetChannelDisabledReason();

            if (!string.IsNullOrEmpty(channelDisabledReason))
            {
                return "GR_TokraSecureCommunicator_FirstMissionStatusLocked"
                    .Translate()
                    .ToString();
            }

            if (GameComponent_TokraTrustTracker
                .IsFirstTrustMissionRelaySabotageCompleted())
            {
                return "GR_TokraSecureCommunicator_FirstMissionStatusRelaySabotageCompleted"
                    .Translate()
                    .ToString();
            }

            if (GameComponent_TokraTrustTracker
                .IsFirstTrustMissionRelaySabotagePrepared())
            {
                return "GR_TokraSecureCommunicator_FirstMissionStatusRelaySabotagePrepared"
                    .Translate()
                    .ToString();
            }

            if (GameComponent_TokraTrustTracker
                .IsFirstTrustMissionWorldSiteReconnoitered())
            {
                return "GR_TokraSecureCommunicator_FirstMissionStatusWorldSiteReconnoitered"
                    .Translate()
                    .ToString();
            }

            if (GameComponent_TokraTrustTracker
                .IsFirstTrustMissionWorldSiteRevealed())
            {
                return "GR_TokraSecureCommunicator_FirstMissionStatusWorldSiteRevealed"
                    .Translate()
                    .ToString();
            }

            if (GameComponent_TokraTrustTracker
                .IsFirstTrustMissionLeadDecoded())
            {
                int remainingWorldSiteTicks = GameComponent_TokraTrustTracker
                    .GetRemainingFirstTrustMissionWorldSiteRevealTicks();

                if (remainingWorldSiteTicks > 0)
                {
                    return "GR_TokraSecureCommunicator_FirstMissionStatusWorldSitePending"
                        .Translate(FormatDays(remainingWorldSiteTicks))
                        .ToString();
                }

                return "GR_TokraSecureCommunicator_FirstMissionStatusLeadDecoded"
                    .Translate()
                    .ToString();
            }

            if (GameComponent_TokraTrustTracker
                .IsFirstTrustMissionIntelAnalyzed())
            {
                int remainingLeadTicks = GameComponent_TokraTrustTracker
                    .GetRemainingFirstTrustMissionLeadDecodeTicks();

                if (remainingLeadTicks > 0)
                {
                    return "GR_TokraSecureCommunicator_FirstMissionStatusLeadPending"
                        .Translate(FormatDays(remainingLeadTicks))
                        .ToString();
                }

                return "GR_TokraSecureCommunicator_FirstMissionStatusIntelAnalyzed"
                    .Translate()
                    .ToString();
            }

            if (GameComponent_TokraTrustTracker
                .IsFirstTrustMissionCacheDelivered())
            {
                return "GR_TokraSecureCommunicator_FirstMissionStatusCacheDelivered"
                    .Translate()
                    .ToString();
            }

            if (GameComponent_TokraTrustTracker
                .IsFirstTrustMissionBriefingReceived())
            {
                int remainingCacheTicks = GameComponent_TokraTrustTracker
                    .GetRemainingFirstTrustMissionCacheDeliveryTicks();

                if (remainingCacheTicks > 0)
                {
                    return "GR_TokraSecureCommunicator_FirstMissionStatusCachePending"
                        .Translate(FormatDays(remainingCacheTicks))
                        .ToString();
                }

                return "GR_TokraSecureCommunicator_FirstMissionStatusBriefingReceived"
                    .Translate()
                    .ToString();
            }

            if (GameComponent_TokraTrustTracker.IsFirstTrustMissionHookPrepared())
            {
                return "GR_TokraSecureCommunicator_FirstMissionStatusPreparing"
                    .Translate()
                    .ToString();
            }

            return "GR_TokraSecureCommunicator_FirstMissionStatusReady"
                .Translate()
                .ToString();
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

        private string GetThreatAssessmentStatusLabel()
        {
            string channelDisabledReason = GetChannelDisabledReason();

            if (!string.IsNullOrEmpty(channelDisabledReason))
            {
                return "GR_TokraSecureCommunicator_ThreatStatusLocked"
                    .Translate()
                    .ToString();
            }

            if (IsThreatAssessmentCooldownActive())
            {
                return "GR_TokraSecureCommunicator_ThreatStatusCooldown"
                    .Translate(FormatDays(
                        GetRemainingThreatAssessmentCooldownTicks()))
                    .ToString();
            }

            if (GameComponent_TokraInterceptedThreatTracker
                .HasActiveThreatForMap(parent.Map))
            {
                return "GR_TokraSecureCommunicator_ThreatStatusIntercepted"
                    .Translate()
                    .ToString();
            }

            if (!HasHostileThreats())
            {
                return "GR_TokraSecureCommunicator_ThreatStatusNoThreat"
                    .Translate()
                    .ToString();
            }

            return "GR_TokraSecureCommunicator_ThreatStatusReady"
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

        private string GetMedicalCacheStatusLabel()
        {
            string channelDisabledReason = GetChannelDisabledReason();

            if (!string.IsNullOrEmpty(channelDisabledReason))
            {
                return "GR_TokraSecureCommunicator_MedicalCacheStatusLocked"
                    .Translate()
                    .ToString();
            }

            if (IsMedicalCacheCooldownActive())
            {
                return "GR_TokraSecureCommunicator_MedicalCacheStatusCooldown"
                    .Translate(FormatDays(GetRemainingMedicalCacheCooldownTicks()))
                    .ToString();
            }

            if (!HasMedicalSupportCandidates())
            {
                return "GR_TokraSecureCommunicator_MedicalCacheStatusNoPatient"
                    .Translate()
                    .ToString();
            }

            return "GR_TokraSecureCommunicator_MedicalCacheStatusReady"
                .Translate()
                .ToString();
        }

        private bool HasThreatAssessmentContext()
        {
            return HasHostileThreats()
                || GameComponent_TokraInterceptedThreatTracker
                    .HasActiveThreatForMap(parent.Map);
        }

        private string GetInterceptedThreatStatusLabel()
        {
            if (GameComponent_TokraInterceptedThreatTracker
                .HasActiveThreatForMap(parent.Map))
            {
                return GameComponent_TokraInterceptedThreatTracker
                    .GetStatusReportLabelForMap(parent.Map);
            }

            return "GR_TokraSecureCommunicator_InterceptedThreatStatusNone"
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

        private static int CountHumanlikeThreats(List<Pawn> threats)
        {
            int count = 0;

            for (int i = 0; i < threats.Count; i++)
            {
                if (threats[i]?.RaceProps?.Humanlike == true)
                {
                    count++;
                }
            }

            return count;
        }

        private static int CountMechanoidThreats(List<Pawn> threats)
        {
            int count = 0;

            for (int i = 0; i < threats.Count; i++)
            {
                if (threats[i]?.RaceProps?.IsMechanoid == true)
                {
                    count++;
                }
            }

            return count;
        }

        private static string GetThreatSeverityLabel(
            int hostileCount,
            int mechanoidCount)
        {
            if (hostileCount >= 12 || mechanoidCount >= 4)
            {
                return "GR_TokraSecureCommunicator_ThreatSeverityHigh"
                    .Translate()
                    .ToString();
            }

            if (hostileCount >= 5 || mechanoidCount > 0)
            {
                return "GR_TokraSecureCommunicator_ThreatSeverityModerate"
                    .Translate()
                    .ToString();
            }

            return "GR_TokraSecureCommunicator_ThreatSeverityLimited"
                .Translate()
                .ToString();
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

        private static bool CanReceiveOperationalDebriefExperience(Pawn pawn)
        {
            if (!CanUsePlayerOperator(pawn) || pawn.skills == null)
            {
                return false;
            }

            return GetOperationalDebriefSkill(pawn) != null;
        }

        private static bool TryGrantOperationalDebriefExperience(
            Pawn pawn,
            out string skillLabel)
        {
            skillLabel = null;

            if (!CanReceiveOperationalDebriefExperience(pawn))
            {
                return false;
            }

            SkillRecord skill = GetOperationalDebriefSkill(pawn);

            if (skill == null)
            {
                return false;
            }

            skill.Learn(OperationalDebriefSkillXp, true);
            skillLabel = GetSkillLabel(skill);
            return true;
        }

        private static SkillRecord GetOperationalDebriefSkill(Pawn pawn)
        {
            if (pawn?.skills == null)
            {
                return null;
            }

            SkillRecord social = pawn.skills.GetSkill(SkillDefOf.Social);

            if (social != null && !social.TotallyDisabled)
            {
                return social;
            }

            SkillRecord intellectual = pawn.skills.GetSkill(SkillDefOf.Intellectual);

            if (intellectual != null && !intellectual.TotallyDisabled)
            {
                return intellectual;
            }

            return null;
        }

        private static string GetSkillLabel(SkillRecord skill)
        {
            if (skill?.def == null)
            {
                return "compétence";
            }

            return skill.def.LabelCap.ToString();
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

        private bool TryPlaceEmergencyMedicalCache(
            out int medicineCount,
            out int tretoninDoseCount)
        {
            medicineCount = TryPlaceMedicalCacheStack(
                "MedicineIndustrial",
                MedicalCacheIndustrialMedicineCount);

            tretoninDoseCount = TryPlaceMedicalCacheStack(
                "SG1_TretoninDose",
                MedicalCacheTretoninDoseCount);

            return medicineCount > 0 || tretoninDoseCount > 0;
        }

        private int TryPlaceMedicalCacheStack(string defName, int stackCount)
        {
            Map map = parent.Map;

            if (map == null || stackCount <= 0)
            {
                return 0;
            }

            ThingDef thingDef = DefDatabase<ThingDef>.GetNamedSilentFail(defName);

            if (thingDef == null)
            {
                return 0;
            }

            Thing thing = ThingMaker.MakeThing(thingDef);
            thing.stackCount = Math.Min(stackCount, thingDef.stackLimit);

            Thing placedThing;

            if (!TokraDeliveryDropUtility.TryPlaceThingNearPreferredDeliveryCell(
                    thing,
                    map,
                    parent,
                    out placedThing))
            {
                if (!thing.Destroyed)
                {
                    thing.Destroy(DestroyMode.Vanish);
                }

                return 0;
            }

            return placedThing != null ? placedThing.stackCount : thing.stackCount;
        }

        private bool IsOperationalDebriefCooldownActive()
        {
            return GetRemainingOperationalDebriefCooldownTicks() > 0;
        }

        private int GetRemainingOperationalDebriefCooldownTicks()
        {
            if (Find.TickManager == null)
            {
                return 0;
            }

            return Math.Max(
                0,
                nextOperationalDebriefRequestTick - Find.TickManager.TicksGame);
        }

        private bool IsThreatAssessmentCooldownActive()
        {
            return GetRemainingThreatAssessmentCooldownTicks() > 0;
        }

        private int GetRemainingThreatAssessmentCooldownTicks()
        {
            if (Find.TickManager == null)
            {
                return 0;
            }

            return Math.Max(
                0,
                nextThreatAssessmentRequestTick - Find.TickManager.TicksGame);
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

        private bool IsMedicalCacheCooldownActive()
        {
            return GetRemainingMedicalCacheCooldownTicks() > 0;
        }

        private int GetRemainingMedicalCacheCooldownTicks()
        {
            if (Find.TickManager == null)
            {
                return 0;
            }

            return Math.Max(
                0,
                nextMedicalCacheRequestTick - Find.TickManager.TicksGame);
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
