using System;
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

        public string SymbioteId => symbioteId;
        public string SymbioteName => symbioteName;
        public string HostName => hostName;
        public GoauldSymbioteOrigin Origin => origin;
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
            if (!string.IsNullOrEmpty(hostName)
                || host?.Name == null)
            {
                return;
            }

            hostName = host.Name.ToStringFull;
            CaptureHostBackstories(host, overwriteExisting: false);
        }

        public void SetSymbioteName(string value)
        {
            if (!value.NullOrEmpty())
            {
                symbioteName = value;
            }
        }

        public void AttachToHost(Pawn host, int currentTick, bool recordImplantationTick)
        {
            EnsureIdentity(currentTick);

            string nextHostThingId = host?.ThingID ?? string.Empty;
            bool hostChanged = currentHostThingId != nextHostThingId;

            if (!string.IsNullOrEmpty(currentHostThingId)
                && hostChanged)
            {
                previousHostThingId = currentHostThingId;
            }

            if (host?.Name != null
                && (string.IsNullOrEmpty(hostName) || hostChanged))
            {
                hostName = host.Name.ToStringFull;
            }

            CaptureHostBackstories(host, overwriteExisting: hostChanged);
            currentHostThingId = nextHostThingId;

            if (recordImplantationTick && implantationTick < 0)
            {
                implantationTick = currentTick;
            }
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
                + $"hostChildhood={hostChildhood?.defName ?? "<none>"}, "
                + $"hostAdulthood={hostAdulthood?.defName ?? "<none>"}, "
                + $"symbioteChildhood={symbioteChildhood?.defName ?? "<none>"}, "
                + $"symbioteAdulthood={symbioteAdulthood?.defName ?? "<none>"}, "
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
}
