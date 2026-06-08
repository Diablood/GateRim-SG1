using Verse;

namespace GateRimSG1.Jaffa
{
    /// <summary>
    /// Lifecycle diagnostics and immediate dependency relief for temporary
    /// tretonin substitution.
    /// </summary>
    public class HediffComp_TretoninSubstitution : HediffComp
    {
        public override void CompPostPostAdd(DamageInfo? dinfo)
        {
            base.CompPostPostAdd(dinfo);

            JaffaPrimtaUtility.RemovePrimtaDependency(
                Pawn,
                showMessage: true);

            GR_Log.Message(
                $"Started tretonin substitution for "
                + $"{JaffaPrimtaUtility.PawnDebugLabel(Pawn)}.");
        }

        public override void CompExposeData()
        {
            base.CompExposeData();

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                GR_Log.Message(
                    $"Loaded tretonin substitution for "
                    + $"{JaffaPrimtaUtility.PawnDebugLabel(Pawn)}.");
            }
        }

        public override void CompPostPostRemoved()
        {
            base.CompPostPostRemoved();

            GR_Log.Message(
                $"Ended tretonin substitution for "
                + $"{JaffaPrimtaUtility.PawnDebugLabel(Pawn)}.");
        }
    }
}
