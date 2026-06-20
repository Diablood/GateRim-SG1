using RimWorld;
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

        private GoauldSymbioteData symbioteData;
        private bool transferredOut;

        public GoauldSymbioteData SymbioteData => symbioteData;

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
                            symbioteData.SymbioteAdulthood)).ToString();

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

            if (transferredOut)
            {
                GR_Log.Message(
                    $"Removed transferred Goa'uld symbiote state "
                    + $"{symbioteData.SymbioteId} from host {PawnDebugLabel()} "
                    + "without detaching the active symbiote.");

                return;
            }

            symbioteData.DetachFromHost(Pawn, CurrentGameTick());

            GR_Log.Message(
                $"Detached Goa'uld symbiote {symbioteData.SymbioteId} "
                + $"from host {PawnDebugLabel()}.");
        }

        public override string CompDebugString()
        {
            return symbioteData?.ToDebugString() ?? "No Goa'uld symbiote data.";
        }

        private bool CanShowPlayerTokraIdentity()
        {
            return symbioteData?.Origin == GoauldSymbioteOrigin.Tokra
                && Pawn != null
                && Pawn.IsColonistPlayerControlled;
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
