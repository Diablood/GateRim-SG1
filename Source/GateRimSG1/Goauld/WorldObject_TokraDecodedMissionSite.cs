using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Temporary non-hostile Tok'ra mission marker created from the decoded
    /// operational lead. It represents an isolated Goa'uld relay on the world
    /// map without generating a combat map, reward or resolution yet.
    /// </summary>
    public class WorldObject_TokraDecodedMissionSite : WorldObject
    {
        public const int DurationTicks = 720000;

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
            string missionInspectString
                = "GR_TokraDecodedMissionWorldSite_InspectString"
                    .Translate(GetRemainingDaysString());

            if (string.IsNullOrEmpty(baseInspectString))
            {
                return missionInspectString;
            }

            return baseInspectString + "\n" + missionInspectString;
        }

        private void Expire()
        {
            GR_Log.Message(
                "Tok'ra decoded mission world site expired at tile "
                + $"{Tile}.");

            Messages.Message(
                "GR_TokraDecodedMissionWorldSite_Expired".Translate(),
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
