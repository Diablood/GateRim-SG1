using Verse;

namespace GateRimSG1.Missions
{
    public enum GateRimMissionOutcome
    {
        Succeeded = 0,
        Failed = 1
    }

    /// <summary>
    /// Extension point for missions whose mechanics cannot be expressed only
    /// through common phase and objective data.
    /// </summary>
    public abstract class GateRimMissionWorker
    {
        public GateRimMissionDef def;

        public virtual bool CanOffer(Map map)
        {
            return map != null;
        }

        public virtual void OnOffered(GateRimMissionRuntimeData runtime, Map map)
        {
        }

        public virtual void OnAccepted(
            GateRimMissionRuntimeData runtime,
            Map map,
            Pawn operatorPawn)
        {
        }

        public virtual void Tick(GateRimMissionRuntimeData runtime, Map map)
        {
        }

        public virtual void OnResolved(
            GateRimMissionRuntimeData runtime,
            Map map,
            Pawn operatorPawn,
            GateRimMissionOutcome outcome)
        {
        }
    }
}
