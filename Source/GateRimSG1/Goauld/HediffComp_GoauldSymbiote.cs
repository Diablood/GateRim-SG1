using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Persists one adult symbiote identity while carried by a host Hediff.
    /// The same data object is transferred between implantation, active-host
    /// and extraction states.
    /// </summary>
    public class HediffComp_GoauldSymbiote : HediffComp
    {
        private const int MissingDataWarningKey = 1160001;
        private const int HostileAssaultCheckInterval = 30;

        private GoauldSymbioteData symbioteData;
        private bool transferredOut;

        public GoauldSymbioteData SymbioteData => symbioteData;

        public Pawn HostPawn => Pawn;

        public bool CanSwitchPersonality
            => CanShowPlayerTokraIdentity()
                && symbioteData.CanSwitchPersonality(Pawn);

        public string InactivePersonalityName
            => symbioteData?.IsSymbiotePersonalityActive == true
                ? DisplayHostName()
                : DisplaySymbioteName();

        public void InitializeWithTransferredData(GoauldSymbioteData transferredData)
        {
            if (transferredData == null)
            {
                GR_Log.Error("Tried to transfer null Goa'uld symbiote data into a host state.");
                return;
            }

            symbioteData = transferredData;
            transferredOut = false;
            symbioteData.EnsureIdentity(CurrentGameTick());
        }

        public GoauldSymbioteData TakeDataForTransfer()
        {
            EnsureDataInitialized();
            symbioteData.RestoreHostPersonality(Pawn);
            transferredOut = true;

            GR_Log.Message(
                $"Prepared Goa'uld symbiote {symbioteData.SymbioteId} "
                + $"for transfer from host state on {PawnDebugLabel()}.");

            return symbioteData;
        }

        public void CancelTransferOut()
        {
            transferredOut = false;

            if (symbioteData != null)
            {
                GR_Log.Warning(
                    $"Cancelled transfer-out state for Goa'uld symbiote "
                    + $"{symbioteData.SymbioteId} on {PawnDebugLabel()}.");
            }
        }

        public bool ReleaseHostControl()
        {
            EnsureDataInitialized();
            bool hadHostControl = symbioteData.HostControlState
                != GoauldHostControlState.None;
            bool restored = symbioteData.ReleaseHostControl(Pawn);

            if (hadHostControl)
            {
                Find.ColonistBar?.MarkColonistsDirty();
                MainTabWindowUtility.NotifyAllPawnTables_PawnsChanged();
            }

            if (restored)
            {
                GR_Log.Message(
                    $"Restored the displaced faction of host {PawnDebugLabel()} "
                    + $"after releasing Goa'uld symbiote "
                    + $"{symbioteData.SymbioteId}.");
            }

            return restored;
        }

        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);

            if (symbioteData?.HostileTakeoverActive == true
                && Pawn != null
                && Pawn.IsHashIntervalTick(HostileAssaultCheckInterval))
            {
                if (symbioteData.EnsureHostileControlName(Pawn))
                {
                    Find.ColonistBar?.MarkColonistsDirty();
                    MainTabWindowUtility.NotifyAllPawnTables_PawnsChanged();
                }

                GoauldHostileTakeoverAssaultUtility
                    .EnsureAssaultBehavior(Pawn);
            }
        }

        public override string CompDescriptionExtra
        {
            get
            {
                if (symbioteData == null)
                {
                    return "GR_GoauldSymbioteDataMissing".Translate().ToString();
                }

                if (CanShowPlayerTokraIdentity())
                {
                    string identitySummary = "GR_TokraDualIdentity_Summary".Translate(
                        DisplayHostName(),
                        DisplaySymbioteName(),
                        DisplayBackstories(
                            symbioteData.HostChildhood,
                            symbioteData.HostAdulthood),
                        DisplayBackstories(
                            symbioteData.SymbioteChildhood,
                            symbioteData.SymbioteAdulthood),
                        DisplayActivePersonality()).ToString();

                    if (!GR_Debug.ShowAdvancedInformation)
                    {
                        return identitySummary;
                    }

                    return identitySummary
                        + "\n\n"
                        + TechnicalSummary();
                }

                return TechnicalSummary();
            }
        }

        public override IEnumerable<Gizmo> CompGetGizmos()
        {
            if (!CanSwitchPersonality)
            {
                yield break;
            }

            string targetName = symbioteData.IsSymbiotePersonalityActive
                ? DisplayHostName()
                : DisplaySymbioteName();
            string descriptionKey = symbioteData.IsSymbiotePersonalityActive
                ? "GR_TokraPersonalitySwitch_ToHost_Desc"
                : "GR_TokraPersonalitySwitch_ToSymbiote_Desc";

            yield return new Command_Action
            {
                defaultLabel = "GR_TokraPersonalitySwitch_Label"
                    .Translate(targetName)
                    .ToString(),
                defaultDesc = descriptionKey.Translate(
                    targetName,
                    symbioteData.IsSymbiotePersonalityActive
                        ? DisplaySymbioteName()
                        : DisplayHostName()).ToString(),
                icon = TexCommand.GatherSpotActive,
                action = () => TryTogglePersonality()
            };
        }

        public override void CompPostPostAdd(DamageInfo? dinfo)
        {
            base.CompPostPostAdd(dinfo);

            EnsureDataInitialized();
            symbioteData.AttachToHost(Pawn, CurrentGameTick(), recordImplantationTick: true);

            GR_Log.Message(
                $"Attached Goa'uld symbiote {symbioteData.SymbioteId} "
                + $"to host {PawnDebugLabel()}.");
        }

        public override void CompExposeData()
        {
            base.CompExposeData();

            Scribe_Deep.Look(ref symbioteData, "goauldSymbioteData");
            Scribe_Values.Look(ref transferredOut, "transferredOut", false);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                if (symbioteData == null)
                {
                    GR_Log.WarningOnce(
                        "Missing persistent Goa'uld symbiote data after load. "
                        + "A replacement identity will be created.",
                        MissingDataWarningKey);
                }

                EnsureDataInitialized();
                symbioteData.AttachToHost(Pawn, CurrentGameTick(), recordImplantationTick: false);

                GR_Log.Message(
                    $"Loaded Goa'uld symbiote {symbioteData.SymbioteId} "
                    + $"for host {PawnDebugLabel()}.");
            }
        }

        public override void CompPostPostRemoved()
        {
            base.CompPostPostRemoved();

            if (symbioteData == null)
            {
                return;
            }

            symbioteData.RestoreHostPersonality(Pawn);

            if (transferredOut)
            {
                GR_Log.Message(
                    $"Removed transferred Goa'uld symbiote state "
                    + $"{symbioteData.SymbioteId} from host {PawnDebugLabel()} "
                    + "without detaching the active symbiote.");

                return;
            }

            symbioteData.ReleaseHostControl(Pawn);
            symbioteData.DetachFromHost(Pawn, CurrentGameTick());

            GR_Log.Message(
                $"Detached Goa'uld symbiote {symbioteData.SymbioteId} "
                + $"from host {PawnDebugLabel()}.");
        }

        public override string CompDebugString()
        {
            return symbioteData?.ToDebugString() ?? "No Goa'uld symbiote data.";
        }

        public bool TryTogglePersonality()
        {
            if (!CanSwitchPersonality
                || !symbioteData.ToggleActivePersonality(Pawn))
            {
                return false;
            }

            Find.ColonistBar.MarkColonistsDirty();
            MainTabWindowUtility.NotifyAllPawnTables_PawnsChanged();

            string messageKey = symbioteData.IsSymbiotePersonalityActive
                ? "GR_TokraPersonalitySwitch_SymbioteActive"
                : "GR_TokraPersonalitySwitch_HostActive";
            Caravan caravan = Pawn.GetCaravan();
            LookTargets lookTargets = Pawn.Spawned || caravan == null
                ? new LookTargets(Pawn)
                : new LookTargets(caravan);
            Messages.Message(
                messageKey.Translate(
                    DisplayHostName(),
                    DisplaySymbioteName()).ToString(),
                lookTargets,
                MessageTypeDefOf.NeutralEvent,
                historical: false);

            return true;
        }

        private bool CanShowPlayerTokraIdentity()
        {
            return TokraPlayerControlUtility.IsEligibleTokra(Pawn, this);
        }

        private string TechnicalSummary()
        {
            return "GR_GoauldSymbioteDataSummary".Translate(
                symbioteData.SymbioteId,
                symbioteData.GetOriginLabel(),
                symbioteData.ImplantationTick,
                DisplayHost(symbioteData.CurrentHostThingId),
                DisplayHost(symbioteData.PreviousHostThingId)).ToString();
        }

        private string DisplayHostName()
        {
            if (!symbioteData.HostName.NullOrEmpty())
            {
                return symbioteData.HostName;
            }

            return Pawn?.Name?.ToStringFull
                ?? "GR_TokraDualIdentity_NotRecorded".Translate().ToString();
        }

        private string DisplaySymbioteName()
        {
            return symbioteData.SymbioteName.NullOrEmpty()
                ? "GR_TokraDualIdentity_NotRecorded".Translate().ToString()
                : symbioteData.SymbioteName;
        }

        private string DisplayActivePersonality()
        {
            string activeName = symbioteData.ActivePersonalityName();
            return activeName.NullOrEmpty()
                ? "GR_TokraDualIdentity_NotRecorded".Translate().ToString()
                : activeName;
        }

        private string DisplayBackstories(
            BackstoryDef childhood,
            BackstoryDef adulthood)
        {
            string childhoodTitle = childhood?.TitleFor(Pawn.gender);
            string adulthoodTitle = adulthood?.TitleFor(Pawn.gender);

            if (!childhoodTitle.NullOrEmpty()
                && !adulthoodTitle.NullOrEmpty())
            {
                return "GR_TokraDualIdentity_BackstoryPair".Translate(
                    childhoodTitle,
                    adulthoodTitle).ToString();
            }

            if (!adulthoodTitle.NullOrEmpty())
            {
                return adulthoodTitle;
            }

            if (!childhoodTitle.NullOrEmpty())
            {
                return childhoodTitle;
            }

            return "GR_TokraDualIdentity_NotRecorded".Translate().ToString();
        }

        private void EnsureDataInitialized()
        {
            if (symbioteData == null)
            {
                symbioteData = GoauldSymbioteData.CreateForHost(Pawn, CurrentGameTick());
            }
            else
            {
                symbioteData.EnsureIdentity(CurrentGameTick());
            }
        }

        private static int CurrentGameTick()
        {
            return Find.TickManager?.TicksGame ?? 0;
        }

        private string PawnDebugLabel()
        {
            if (Pawn == null)
            {
                return "<null pawn>";
            }

            return $"{Pawn.LabelShort} ({Pawn.ThingID})";
        }

        private static string DisplayHost(string hostThingId)
        {
            return string.IsNullOrEmpty(hostThingId)
                ? "GR_GoauldSymbioteHost_None".Translate().ToString()
                : hostThingId;
        }
    }
}
