using Verse;

namespace GateRimSG1.Jaffa
{
    /// <summary>
    /// Lightweight lifecycle diagnostics for the acquired Jaffa Prim'ta state.
    /// The Hediff itself is persistent through RimWorld save data.
    /// </summary>
    public class HediffComp_JaffaPrimta : HediffComp
    {
        public override void CompPostPostAdd(DamageInfo? dinfo)
        {
            base.CompPostPostAdd(dinfo);

            JaffaPrimtaUtility.RemovePrimtaDependency(
                Pawn,
                showMessage: true);

            Current.Game
                ?.GetComponent<GameComponent_JaffaPrimtaCulturalThoughts>()
                ?.TryGrantReceivedPrimtaThought(Pawn);

            GR_Log.Message(
                $"Attached Jaffa Prim'ta symbiote to "
                + $"{JaffaPrimtaUtility.PawnDebugLabel(Pawn)}.");
        }

        public override void CompExposeData()
        {
            base.CompExposeData();

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                GR_Log.Message(
                    $"Loaded Jaffa Prim'ta symbiote for "
                    + $"{JaffaPrimtaUtility.PawnDebugLabel(Pawn)}.");
            }
        }

        public override void CompPostPostRemoved()
        {
            base.CompPostPostRemoved();

            GR_Log.Message(
                $"Removed Jaffa Prim'ta symbiote from "
                + $"{JaffaPrimtaUtility.PawnDebugLabel(Pawn)}.");
        }
    }
}
