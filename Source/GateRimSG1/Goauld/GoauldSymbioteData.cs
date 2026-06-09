using System;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Persistent identity and host-tracking data for one adult Goa'uld
    /// symbiote. This object is deep-saved inside a HediffComp.
    /// </summary>
    public class GoauldSymbioteData : IExposable
    {
        private string symbioteId = string.Empty;
        private string symbioteName = string.Empty;
        private GoauldSymbioteOrigin origin = GoauldSymbioteOrigin.Goauld;
        private long biologicalAgeTicks;
        private int createdAtTick = -1;
        private int implantationTick = -1;
        private int lastDetachTick = -1;
        private string currentHostThingId = string.Empty;
        private string previousHostThingId = string.Empty;

        public string SymbioteId => symbioteId;
        public string SymbioteName => symbioteName;
        public GoauldSymbioteOrigin Origin => origin;
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

            if (createdAtTick < 0)
            {
                createdAtTick = currentTick;
            }
        }

        public void AttachToHost(Pawn host, int currentTick, bool recordImplantationTick)
        {
            EnsureIdentity(currentTick);

            string nextHostThingId = host?.ThingID ?? string.Empty;

            if (!string.IsNullOrEmpty(currentHostThingId)
                && currentHostThingId != nextHostThingId)
            {
                previousHostThingId = currentHostThingId;
            }

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
            return $"id={symbioteId}, origin={origin}, ageTicks={biologicalAgeTicks}, "
                + $"createdAt={createdAtTick}, implantedAt={implantationTick}, "
                + $"currentHost={currentHostThingId}, previousHost={previousHostThingId}, "
                + $"lastDetach={lastDetachTick}";
        }

        public void ExposeData()
        {
            Scribe_Values.Look(ref symbioteId, "symbioteId", string.Empty);
            Scribe_Values.Look(ref symbioteName, "symbioteName", string.Empty);
            Scribe_Values.Look(ref origin, "origin", GoauldSymbioteOrigin.Goauld);
            Scribe_Values.Look(ref biologicalAgeTicks, "biologicalAgeTicks", 0L);
            Scribe_Values.Look(ref createdAtTick, "createdAtTick", -1);
            Scribe_Values.Look(ref implantationTick, "implantationTick", -1);
            Scribe_Values.Look(ref lastDetachTick, "lastDetachTick", -1);
            Scribe_Values.Look(ref currentHostThingId, "currentHostThingId", string.Empty);
            Scribe_Values.Look(ref previousHostThingId, "previousHostThingId", string.Empty);
        }
    }

    public enum GoauldSymbioteOrigin
    {
        Goauld,
        Tokra
    }
}
