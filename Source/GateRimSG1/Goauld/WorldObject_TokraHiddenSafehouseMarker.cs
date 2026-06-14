using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Temporary non-hostile world marker created from a stored Tok'ra
    /// safehouse lead.
    ///
    /// This is deliberately not a generated map, quest site, trader,
    /// recruitment point or combat encounter yet. It only proves that a stored
    /// lead can become a visible world-map footprint and expire safely.
    /// </summary>
    public class WorldObject_TokraHiddenSafehouseMarker : WorldObject
    {
        public const int DurationTicks = 300000;

        private int ticksRemaining = DurationTicks;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(
                ref ticksRemaining,
                "ticksRemaining",
                DurationTicks);
        }

        protected override void Tick()
        {
            base.Tick();

            ticksRemaining--;

            if (ticksRemaining <= 0)
            {
                Expire();
            }
        }

        public override string GetInspectString()
        {
            string baseInspectString = base.GetInspectString();
            string safehouseInspectString
                = "GR_TokraHiddenSafehouseMarker_InspectString"
                    .Translate(GetRemainingDaysString());

            if (string.IsNullOrEmpty(baseInspectString))
            {
                return safehouseInspectString;
            }

            return baseInspectString + "\n" + safehouseInspectString;
        }

        private void Expire()
        {
            GR_Log.Message(
                "Tok'ra hidden safehouse marker expired at tile "
                + $"{Tile}.");

            Messages.Message(
                "GR_TokraHiddenSafehouseMarker_Expired".Translate(),
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            Destroy();
        }

        private string GetRemainingDaysString()
        {
            float remainingDays = ticksRemaining / 60000f;

            if (remainingDays < 0f)
            {
                remainingDays = 0f;
            }

            return remainingDays.ToString("0.#");
        }
    }
}
