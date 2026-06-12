using Verse;

namespace GateRimSG1.Jaffa
{
    /// <summary>
    /// Save-persistent intrinsic forehead mark assigned to one pawn ThingID.
    /// </summary>
    public class JaffaForeheadMarkData : IExposable
    {
        public string pawnThingId;
        public JaffaForeheadMarkDef markDef;

        public JaffaForeheadMarkData()
        {
        }

        public JaffaForeheadMarkData(
            string pawnThingId,
            JaffaForeheadMarkDef markDef)
        {
            this.pawnThingId = pawnThingId;
            this.markDef = markDef;
        }

        public void ExposeData()
        {
            Scribe_Values.Look(ref pawnThingId, "pawnThingId");
            Scribe_Defs.Look(ref markDef, "markDef");
        }
    }
}
