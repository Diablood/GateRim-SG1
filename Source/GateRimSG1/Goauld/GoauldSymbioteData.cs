using System;
using System.Collections.Generic;
using GateRimSG1.Culture;
using GateRimSG1.Names;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Persistent identity and host-tracking data for one adult Goa'uld
    /// symbiote. This object is deep-saved inside a HediffComp and transferred
    /// unchanged between free, implanted and extracted states.
    /// </summary>
    public class GoauldSymbioteData : IExposable
    {
        private string symbioteId = string.Empty;
        private string symbioteName = string.Empty;
        private string hostName = string.Empty;
        private GoauldSymbioteOrigin origin = GoauldSymbioteOrigin.Goauld;
        private Faction allegianceFaction;
        private Faction displacedHostFaction;
        private GoauldHostControlState hostControlState
            = GoauldHostControlState.None;
        private TokraHostIdentitySource hostIdentitySource
            = TokraHostIdentitySource.Unknown;
        private GeneratedHostOriginDef generatedHostOrigin;
        private BackstoryDef hostChildhood;
        private BackstoryDef hostAdulthood;
        private BackstoryDef symbioteChildhood;
        private BackstoryDef symbioteAdulthood;
        private long biologicalAgeTicks;
        private int createdAtTick = -1;
        private int implantationTick = -1;
        private int lastDetachTick = -1;
        private string currentHostThingId = string.Empty;
        private string previousHostThingId = string.Empty;

        private TokraActivePersonality activePersonality = TokraActivePersonality.Host;
        private StoredPawnNameKind hostNameKind = StoredPawnNameKind.Unknown;
        private string hostFirstName = string.Empty;
        private string hostNickName = string.Empty;
        private string hostLastName = string.Empty;
        private string hostSingleName = string.Empty;
        private bool hostSingleNameNumerical;
        private List<BackstorySkillProgressState> sharedSkillProgress
            = new List<BackstorySkillProgressState>();

        public string SymbioteId => symbioteId;
        public string SymbioteName => symbioteName;
        public string HostName => hostName;
        public GoauldSymbioteOrigin Origin => origin;
        public Faction AllegianceFaction => allegianceFaction;
        public Faction DisplacedHostFaction => displacedHostFaction;
        public GoauldHostControlState HostControlState => hostControlState;
        public bool HostileTakeoverPending
            => hostControlState == GoauldHostControlState.Pending;
        public bool HostileTakeoverActive
            => hostControlState == GoauldHostControlState.Active;
        public TokraHostIdentitySource HostIdentitySource => hostIdentitySource;
        public GeneratedHostOriginDef GeneratedHostOrigin => generatedHostOrigin;
        public BackstoryDef HostChildhood => hostChildhood;
        public BackstoryDef HostAdulthood => hostAdulthood;
        public BackstoryDef SymbioteChildhood => symbioteChildhood;
        public BackstoryDef SymbioteAdulthood => symbioteAdulthood;
        public long BiologicalAgeTicks => biologicalAgeTicks;
        public int CreatedAtTick => createdAtTick;
        public int ImplantationTick => implantationTick;
        public int LastDetachTick => lastDetachTick;
        public string CurrentHostThingId => currentHostThingId;
        public string PreviousHostThingId => previousHostThingId;
        public TokraActivePersonality ActivePersonality => activePersonality;
        public bool IsSymbiotePersonalityActive
            => activePersonality == TokraActivePersonality.Symbiote;

        public static GoauldSymbioteData CreateFree(
            int currentTick,
            GoauldSymbioteOrigin origin = GoauldSymbioteOrigin.Goauld)
        {
            var data = new GoauldSymbioteData
            {
                origin = origin
            };

            data.EnsureIdentity(currentTick);
            return data;
        }

        public static GoauldSymbioteData CreateForHost(Pawn host, int currentTick)
        {
            var data = new GoauldSymbioteData();
            data.EnsureIdentity(currentTick);
            data.AttachToHost(host, currentTick, recordImplantationTick: true);
            return data;
        }

        public static GoauldSymbioteData CreatePreJoinedTokra(
            Pawn host,
            int currentTick)
        {
            return CreatePreJoinedTokra(
                host,
                currentTick,
                allowStarterGoauldHostXenotype: false);
        }

        public static GoauldSymbioteData CreatePreJoinedTokraStarter(
            Pawn host,
            int currentTick)
        {
            return CreatePreJoinedTokra(
                host,
                currentTick,
                allowStarterGoauldHostXenotype: true);
        }

        private static GoauldSymbioteData CreatePreJoinedTokra(
            Pawn host,
            int currentTick,
            bool allowStarterGoauldHostXenotype)
        {
            var data = new GoauldSymbioteData
            {
                origin = GoauldSymbioteOrigin.Tokra
            };

            data.EnsureIdentity(currentTick);

            if (host?.Name != null)
            {
                data.symbioteName = host.Name.ToStringFull;
            }

            if (host?.story?.Adulthood != null)
            {
                data.symbioteAdulthood = host.story.Adulthood;
            }

            if (!data.TryInitializeGeneratedPreJoinedHost(
                    host,
                    currentTick,
                    allowStarterGoauldHostXenotype))
            {
                GR_Log.Error(
                    "Cannot generate a distinct historical host identity for "
                    + $"pre-joined Tok'ra {host?.ThingID ?? "<null>"}.");
            }

            return data;
        }

        public void EnsureIdentity(int currentTick)
        {
            if (string.IsNullOrEmpty(symbioteId))
            {
                symbioteId = Guid.NewGuid().ToString("N");
            }

            if (string.IsNullOrEmpty(symbioteName))
            {
                symbioteName = CulturalPawnNameUtility
                    .GenerateSymbioteNameText(origin);
            }

            EnsureCulturalIdentity();

            if (createdAtTick < 0)
            {
                createdAtTick = currentTick;
            }
        }

        public void CaptureHostName(Pawn host)
        {
            if (host?.Name == null)
            {
                return;
            }

            CaptureHostNameDetails(
                host,
                overwriteExisting: hostNameKind == StoredPawnNameKind.Unknown);
            CaptureHostBackstories(host, overwriteExisting: false);
        }

        public bool TryInitializeGeneratedSystemLordHost(
            Pawn host,
            int currentTick)
        {
            if (origin != GoauldSymbioteOrigin.Goauld
                || host?.kindDef != GR_DefOf.SG1_GoauldSystemLordHost
                || host.Name == null)
            {
                return false;
            }

            EnsureIdentity(currentTick);

            string visibleSymbioteName = host.Name.ToStringFull;
            if (visibleSymbioteName.NullOrEmpty())
            {
                return false;
            }

            symbioteName = visibleSymbioteName;

            Name generatedHostName = GenerateDistinctSystemLordHostName(
                host.gender,
                visibleSymbioteName);

            if (generatedHostName == null)
            {
                return false;
            }

            StoreHostName(generatedHostName);
            CaptureHostBackstories(host, overwriteExisting: true);
            currentHostThingId = host.ThingID ?? string.Empty;
            return true;
        }

        public void SetSymbioteName(string value)
        {
            if (!value.NullOrEmpty())
            {
                symbioteName = value;
            }
        }

        public void RecordAllegiance(Faction faction)
        {
            if (faction != null)
            {
                allegianceFaction = faction;
            }
        }

        public void PrepareHostControl(Pawn host, Faction currentSymbioteFaction)
        {
            RecordAllegiance(currentSymbioteFaction);
            displacedHostFaction = null;
            hostControlState = GoauldHostControlState.None;

            if (origin != GoauldSymbioteOrigin.Goauld
                || host?.Faction != Faction.OfPlayer
                || allegianceFaction == null
                || allegianceFaction == Faction.OfPlayer
                || !allegianceFaction.HostileTo(Faction.OfPlayer))
            {
                return;
            }

            displacedHostFaction = host.Faction;
            hostControlState = GoauldHostControlState.Pending;
        }

        public bool TryActivateHostileControl(Pawn host)
        {
            if (hostControlState != GoauldHostControlState.Pending
                || host == null
                || host.Dead
                || allegianceFaction == null
                || displacedHostFaction == null)
            {
                return false;
            }

            if (host.Faction == allegianceFaction)
            {
                hostControlState = GoauldHostControlState.Active;
                ApplyHostileControlName(host);
                return true;
            }

            if (host.Faction != displacedHostFaction)
            {
                GR_Log.Warning(
                    $"Cancelled pending Goa'uld host takeover for "
                    + $"{host.LabelShort} ({host.ThingID}) because the host's "
                    + "faction changed before neural control completed.");

                displacedHostFaction = null;
                hostControlState = GoauldHostControlState.None;
                return false;
            }

            host.SetFaction(allegianceFaction);
            hostControlState = GoauldHostControlState.Active;
            ApplyHostileControlName(host);
            return true;
        }

        public bool ReleaseHostControl(
            Pawn host,
            bool restoreDisplayedSymbioteName = false)
        {
            bool restored = false;

            if (hostControlState == GoauldHostControlState.Active
                && host != null
                && !host.Dead
                && displacedHostFaction != null
                && host.Faction != displacedHostFaction)
            {
                GoauldHostileTakeoverAssaultUtility
                    .ReleaseAssaultBehavior(host);
                host.SetFaction(displacedHostFaction);
                restored = true;
            }

            if (hostControlState == GoauldHostControlState.Active
                || (restoreDisplayedSymbioteName
                    && IsDisplayingSymbioteName(host)))
            {
                RestoreHostName(host);
            }

            displacedHostFaction = null;
            hostControlState = GoauldHostControlState.None;
            return restored;
        }

        public bool EnsureHostileControlName(Pawn host)
        {
            if (!HostileTakeoverActive
                || origin != GoauldSymbioteOrigin.Goauld
                || host == null
                || host.Dead
                || symbioteName.NullOrEmpty())
            {
                return false;
            }

            return ApplyHostileControlName(host);
        }

        public void AttachToHost(Pawn host, int currentTick, bool recordImplantationTick)
        {
            EnsureIdentity(currentTick);

            if (allegianceFaction == null && host?.Faction != null)
            {
                allegianceFaction = host.Faction;
            }

            string nextHostThingId = host?.ThingID ?? string.Empty;
            bool hostChanged = currentHostThingId != nextHostThingId;

            if (origin == GoauldSymbioteOrigin.Tokra)
            {
                if (hostIdentitySource == TokraHostIdentitySource.GeneratedPreJoined
                    && hostChanged)
                {
                    hostIdentitySource
                        = TokraHostIdentitySource.ImplantedExistingHost;
                    generatedHostOrigin = null;
                }
                else if (hostIdentitySource == TokraHostIdentitySource.Unknown
                    && (recordImplantationTick
                        || host?.kindDef != GR_DefOf.SG1_TokraVoluntaryHost
                        || !previousHostThingId.NullOrEmpty()
                        || lastDetachTick >= 0))
                {
                    hostIdentitySource
                        = TokraHostIdentitySource.ImplantedExistingHost;
                }
            }

            if (!string.IsNullOrEmpty(currentHostThingId)
                && hostChanged)
            {
                previousHostThingId = currentHostThingId;
            }

            if (hostChanged)
            {
                activePersonality = TokraActivePersonality.Host;
                BackstorySkillOffsetUtility.Reset(ref sharedSkillProgress);
                CaptureHostNameDetails(host, overwriteExisting: true);
                CaptureHostBackstories(host, overwriteExisting: true);
            }
            else if (activePersonality == TokraActivePersonality.Host)
            {
                CaptureHostNameDetails(
                    host,
                    overwriteExisting: hostNameKind == StoredPawnNameKind.Unknown);
                CaptureHostBackstories(host, overwriteExisting: false);
            }

            currentHostThingId = nextHostThingId;

            if (recordImplantationTick && implantationTick < 0)
            {
                implantationTick = currentTick;
            }
        }

        public bool TryInitializeGeneratedPreJoinedHost(
            Pawn host,
            int currentTick)
        {
            return TryInitializeGeneratedPreJoinedHost(
                host,
                currentTick,
                allowStarterGoauldHostXenotype: false);
        }

        private bool TryInitializeGeneratedPreJoinedHost(
            Pawn host,
            int currentTick,
            bool allowStarterGoauldHostXenotype)
        {
            bool supportedGeneratedHost = host?.kindDef
                == GR_DefOf.SG1_TokraVoluntaryHost;
            bool supportedStarterHost = allowStarterGoauldHostXenotype
                && host?.genes?.Xenotype == GR_DefOf.SG1_GoauldHost;

            if (origin != GoauldSymbioteOrigin.Tokra
                || host?.story == null
                || host.skills == null
                || (!supportedGeneratedHost && !supportedStarterHost)
                || hostIdentitySource == TokraHostIdentitySource.ImplantedExistingHost)
            {
                return false;
            }

            if (hostIdentitySource == TokraHostIdentitySource.GeneratedPreJoined)
            {
                return true;
            }

            if (!previousHostThingId.NullOrEmpty() || lastDetachTick >= 0)
            {
                hostIdentitySource = TokraHostIdentitySource.ImplantedExistingHost;
                generatedHostOrigin = null;
                return false;
            }

            EnsureIdentity(currentTick);

            GeneratedHostIdentity generatedIdentity;
            bool generatedHostIdentity;

            if (allowStarterGoauldHostXenotype)
            {
                generatedHostIdentity = CulturalGeneratedHostIdentityUtility
                    .TryGenerateForIdentityProfile(
                        host,
                        CulturalPawnNameGroup.Tokra,
                        symbioteId,
                        symbioteName,
                        out generatedIdentity);
            }
            else
            {
                generatedHostIdentity
                    = CulturalGeneratedHostIdentityUtility.TryGenerate(
                        host,
                        symbioteId,
                        symbioteName,
                        out generatedIdentity);
            }

            if (!generatedHostIdentity)
            {
                return false;
            }

            BackstoryDef currentChildhood = host.story.Childhood;
            BackstoryDef currentAdulthood = host.story.Adulthood;

            StoreHostName(generatedIdentity.Name);
            hostChildhood = generatedIdentity.Childhood;
            hostAdulthood = generatedIdentity.Adulthood;
            generatedHostOrigin = generatedIdentity.Origin;
            hostIdentitySource = TokraHostIdentitySource.GeneratedPreJoined;
            currentHostThingId = host.ThingID ?? string.Empty;

            if (implantationTick < 0)
            {
                implantationTick = currentTick;
            }

            BackstorySkillOffsetUtility.Reset(ref sharedSkillProgress);

            if (activePersonality == TokraActivePersonality.Host)
            {
                BackstorySkillOffsetUtility.SwitchBackstories(
                    host,
                    currentChildhood,
                    currentAdulthood,
                    hostChildhood,
                    hostAdulthood,
                    ref sharedSkillProgress);
                host.Name = CreateStoredHostName();
            }

            return true;
        }

        public bool CanSwitchPersonality(Pawn host)
        {
            return origin == GoauldSymbioteOrigin.Tokra
                && host?.story != null
                && host.skills != null
                && hostAdulthood != null
                && symbioteAdulthood != null
                && !hostName.NullOrEmpty()
                && !symbioteName.NullOrEmpty();
        }

        public bool ToggleActivePersonality(Pawn host)
        {
            if (!CanSwitchPersonality(host))
            {
                return false;
            }

            if (activePersonality == TokraActivePersonality.Host)
            {
                CaptureCurrentHostIdentity(host);
                return ApplyPersonality(host, TokraActivePersonality.Symbiote);
            }

            return ApplyPersonality(host, TokraActivePersonality.Host);
        }

        public bool RestoreHostPersonality(Pawn host)
        {
            if (activePersonality != TokraActivePersonality.Symbiote)
            {
                return false;
            }

            return ApplyPersonality(host, TokraActivePersonality.Host);
        }

        public string ActivePersonalityName()
        {
            return activePersonality == TokraActivePersonality.Symbiote
                ? symbioteName
                : hostName;
        }

        public void DetachFromHost(Pawn host, int currentTick)
        {
            string detachedHostThingId = host?.ThingID ?? currentHostThingId;

            if (!string.IsNullOrEmpty(detachedHostThingId))
            {
                previousHostThingId = detachedHostThingId;
            }

            currentHostThingId = string.Empty;
            lastDetachTick = currentTick;
            displacedHostFaction = null;
            hostControlState = GoauldHostControlState.None;
        }

        public string GetOriginLabel()
        {
            switch (origin)
            {
                case GoauldSymbioteOrigin.Tokra:
                    return "GR_GoauldSymbioteOrigin_Tokra".Translate().ToString();

                default:
                    return "GR_GoauldSymbioteOrigin_Goauld".Translate().ToString();
            }
        }

        public string ToDebugString()
        {
            return $"id={symbioteId}, symbioteName={symbioteName}, "
                + $"hostName={hostName}, origin={origin}, "
                + $"allegiance={allegianceFaction?.Name ?? "<none>"}, "
                + $"displacedFaction={displacedHostFaction?.Name ?? "<none>"}, "
                + $"hostControl={hostControlState}, "
                + $"hostIdentitySource={hostIdentitySource}, "
                + $"generatedHostOrigin={generatedHostOrigin?.defName ?? "<none>"}, "
                + $"activePersonality={activePersonality}, "
                + $"hostChildhood={hostChildhood?.defName ?? "<none>"}, "
                + $"hostAdulthood={hostAdulthood?.defName ?? "<none>"}, "
                + $"symbioteChildhood={symbioteChildhood?.defName ?? "<none>"}, "
                + $"symbioteAdulthood={symbioteAdulthood?.defName ?? "<none>"}, "
                + $"sharedSkillStates={sharedSkillProgress?.Count ?? 0}, "
                + $"ageTicks={biologicalAgeTicks}, createdAt={createdAtTick}, "
                + $"implantedAt={implantationTick}, "
                + $"currentHost={currentHostThingId}, "
                + $"previousHost={previousHostThingId}, "
                + $"lastDetach={lastDetachTick}";
        }

        public void ExposeData()
        {
            Scribe_Values.Look(ref symbioteId, "symbioteId", string.Empty);
            Scribe_Values.Look(ref symbioteName, "symbioteName", string.Empty);
            Scribe_Values.Look(ref hostName, "hostName", string.Empty);
            Scribe_Values.Look(ref origin, "origin", GoauldSymbioteOrigin.Goauld);
            Scribe_References.Look(ref allegianceFaction, "allegianceFaction");
            Scribe_References.Look(ref displacedHostFaction, "displacedHostFaction");
            Scribe_Values.Look(
                ref hostControlState,
                "hostControlState",
                GoauldHostControlState.None);
            Scribe_Values.Look(
                ref hostIdentitySource,
                "hostIdentitySource",
                TokraHostIdentitySource.Unknown);
            Scribe_Defs.Look(ref generatedHostOrigin, "generatedHostOrigin");
            Scribe_Defs.Look(ref hostChildhood, "hostChildhood");
            Scribe_Defs.Look(ref hostAdulthood, "hostAdulthood");
            Scribe_Defs.Look(ref symbioteChildhood, "symbioteChildhood");
            Scribe_Defs.Look(ref symbioteAdulthood, "symbioteAdulthood");
            Scribe_Values.Look(ref biologicalAgeTicks, "biologicalAgeTicks", 0L);
            Scribe_Values.Look(ref createdAtTick, "createdAtTick", -1);
            Scribe_Values.Look(ref implantationTick, "implantationTick", -1);
            Scribe_Values.Look(ref lastDetachTick, "lastDetachTick", -1);
            Scribe_Values.Look(ref currentHostThingId, "currentHostThingId", string.Empty);
            Scribe_Values.Look(ref previousHostThingId, "previousHostThingId", string.Empty);
            Scribe_Values.Look(
                ref activePersonality,
                "activePersonality",
                TokraActivePersonality.Host);
            Scribe_Values.Look(
                ref hostNameKind,
                "hostNameKind",
                StoredPawnNameKind.Unknown);
            Scribe_Values.Look(ref hostFirstName, "hostFirstName", string.Empty);
            Scribe_Values.Look(ref hostNickName, "hostNickName", string.Empty);
            Scribe_Values.Look(ref hostLastName, "hostLastName", string.Empty);
            Scribe_Values.Look(ref hostSingleName, "hostSingleName", string.Empty);
            Scribe_Values.Look(
                ref hostSingleNameNumerical,
                "hostSingleNameNumerical",
                false);
            Scribe_Collections.Look(
                ref sharedSkillProgress,
                "sharedSkillProgress",
                LookMode.Deep);

            if (Scribe.mode == LoadSaveMode.PostLoadInit
                && sharedSkillProgress == null)
            {
                sharedSkillProgress = new List<BackstorySkillProgressState>();
            }
        }

        private bool ApplyHostileControlName(Pawn host)
        {
            if (host == null || symbioteName.NullOrEmpty())
            {
                return false;
            }

            if (hostNameKind == StoredPawnNameKind.Unknown)
            {
                if (hostName.NullOrEmpty())
                {
                    CaptureHostNameDetails(host, overwriteExisting: true);
                }
            }

            if (host.Name is NameSingle currentName
                && currentName.Name == symbioteName)
            {
                return false;
            }

            host.Name = new NameSingle(symbioteName);
            return true;
        }

        private bool RestoreHostName(Pawn host)
        {
            if (host == null || hostName.NullOrEmpty())
            {
                return false;
            }

            Name restoredName = CreateStoredHostName();
            if (restoredName == null)
            {
                return false;
            }

            if (host.Name?.ToStringFull == restoredName.ToStringFull)
            {
                return false;
            }

            host.Name = restoredName;
            return true;
        }

        private bool ApplyPersonality(
            Pawn host,
            TokraActivePersonality targetPersonality)
        {
            if (host?.story == null || host.skills == null)
            {
                return false;
            }

            BackstoryDef targetChildhood = targetPersonality
                == TokraActivePersonality.Symbiote
                ? symbioteChildhood
                : hostChildhood;
            BackstoryDef targetAdulthood = targetPersonality
                == TokraActivePersonality.Symbiote
                ? symbioteAdulthood
                : hostAdulthood;

            BackstorySkillOffsetUtility.SwitchBackstories(
                host,
                host.story.Childhood,
                host.story.Adulthood,
                targetChildhood,
                targetAdulthood,
                ref sharedSkillProgress);

            host.Name = targetPersonality == TokraActivePersonality.Symbiote
                ? (Name)new NameSingle(symbioteName)
                : CreateStoredHostName();
            activePersonality = targetPersonality;
            return true;
        }

        private void CaptureCurrentHostIdentity(Pawn host)
        {
            bool backstoriesChanged = hostChildhood != host.story.Childhood
                || hostAdulthood != host.story.Adulthood;

            CaptureHostNameDetails(host, overwriteExisting: true);
            CaptureHostBackstories(host, overwriteExisting: true);

            if (backstoriesChanged)
            {
                BackstorySkillOffsetUtility.Reset(ref sharedSkillProgress);
            }
        }

        private Name GenerateDistinctSystemLordHostName(
            Gender gender,
            string excludedName)
        {
            const int MaxAttempts = 16;

            for (int attempt = 0; attempt < MaxAttempts; attempt++)
            {
                Name candidate = CulturalPawnNameUtility.GenerateStableName(
                    CulturalPawnNameGroup.OffworldHuman,
                    gender,
                    symbioteId + ":systemLordHost:" + attempt);

                if (candidate == null)
                {
                    return null;
                }

                if (excludedName.NullOrEmpty()
                    || candidate.ToStringFull != excludedName)
                {
                    return candidate;
                }
            }

            return null;
        }

        private bool IsDisplayingSymbioteName(Pawn host)
        {
            return origin == GoauldSymbioteOrigin.Goauld
                && host?.Name != null
                && !symbioteName.NullOrEmpty()
                && host.Name.ToStringFull == symbioteName;
        }

        private void CaptureHostNameDetails(Pawn host, bool overwriteExisting)
        {
            if (host?.Name == null
                || (!overwriteExisting
                    && hostNameKind != StoredPawnNameKind.Unknown))
            {
                return;
            }

            StoreHostName(host.Name);
        }

        private void StoreHostName(Name name)
        {
            if (name == null)
            {
                return;
            }

            hostName = name.ToStringFull;
            hostFirstName = string.Empty;
            hostNickName = string.Empty;
            hostLastName = string.Empty;
            hostSingleName = string.Empty;
            hostSingleNameNumerical = false;

            if (name is NameTriple triple)
            {
                hostNameKind = StoredPawnNameKind.Triple;
                hostFirstName = triple.First;
                hostNickName = triple.NickSet ? triple.Nick : string.Empty;
                hostLastName = triple.Last;
                return;
            }

            if (name is NameSingle single)
            {
                hostNameKind = StoredPawnNameKind.Single;
                hostSingleName = single.Name;
                hostSingleNameNumerical = single.Numerical;
                return;
            }

            hostNameKind = StoredPawnNameKind.Single;
            hostSingleName = name.ToStringFull;
        }

        private Name CreateStoredHostName()
        {
            switch (hostNameKind)
            {
                case StoredPawnNameKind.Triple:
                    return new NameTriple(
                        hostFirstName ?? string.Empty,
                        hostNickName.NullOrEmpty() ? null : hostNickName,
                        hostLastName ?? string.Empty);

                case StoredPawnNameKind.Single:
                    return new NameSingle(
                        hostSingleName.NullOrEmpty() ? hostName : hostSingleName,
                        hostSingleNameNumerical);

                default:
                    return new NameSingle(hostName);
            }
        }

        private void EnsureCulturalIdentity()
        {
            CulturalPawnNameGroup nameGroup = origin == GoauldSymbioteOrigin.Tokra
                ? CulturalPawnNameGroup.Tokra
                : CulturalPawnNameGroup.Goauld;

            if (symbioteChildhood == null)
            {
                symbioteChildhood = CulturalIdentityUtility.ResolveChildhood(
                    nameGroup,
                    symbioteId);
            }

            if (symbioteAdulthood == null)
            {
                symbioteAdulthood = CulturalIdentityUtility.ResolveAdulthood(
                    nameGroup,
                    symbioteId);
            }
        }

        private void CaptureHostBackstories(Pawn host, bool overwriteExisting)
        {
            if (host?.story == null)
            {
                return;
            }

            if (overwriteExisting || hostChildhood == null)
            {
                hostChildhood = host.story.Childhood;
            }

            if (overwriteExisting || hostAdulthood == null)
            {
                hostAdulthood = host.story.Adulthood;
            }
        }
    }

    public enum GoauldSymbioteOrigin
    {
        Goauld,
        Tokra
    }

    public enum GoauldHostControlState
    {
        None,
        Pending,
        Active
    }

    public enum TokraHostIdentitySource
    {
        Unknown,
        ImplantedExistingHost,
        GeneratedPreJoined
    }

    public enum TokraActivePersonality
    {
        Host,
        Symbiote
    }

    public enum StoredPawnNameKind
    {
        Unknown,
        Single,
        Triple
    }
}
