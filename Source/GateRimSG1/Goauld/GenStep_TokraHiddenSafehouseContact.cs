using RimWorld;
using Verse;
using Verse.AI.Group;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Adds one peaceful, non-recruitable Tok'ra contact to the temporary
    /// safehouse map after the medical stash has been generated.
    /// </summary>
    public class GenStep_TokraHiddenSafehouseContact : GenStep
    {
        private const int ContactVisitDurationTicks = 180000;

        public override int SeedPart => 1732149017;

        public override void Generate(Map map, GenStepParams parms)
        {
            Faction tokraFaction = map?.ParentFaction;

            if (map == null
                || tokraFaction == null
                || tokraFaction.def != GR_DefOf.SG1_Tokra
                || GR_DefOf.SG1_TokraVoluntaryHost == null)
            {
                GR_Log.Warning(
                    "Cannot generate the Tok'ra safehouse contact: the map, "
                    + "persistent Tok'ra faction or voluntary-host pawn kind "
                    + "is unavailable.");
                return;
            }

            Pawn contact = PawnGenerator.GeneratePawn(
                GR_DefOf.SG1_TokraVoluntaryHost,
                tokraFaction,
                map.Tile);

            if (contact == null)
            {
                GR_Log.Warning(
                    "Cannot generate the Tok'ra safehouse contact: pawn "
                    + "generation returned no pawn.");
                return;
            }

            contact.guest.Recruitable = false;

            IntVec3 contactCell = CellFinder.RandomClosewalkCellNear(
                map.Center,
                map,
                12);

            GenSpawn.Spawn(contact, contactCell, map);

            LordMaker.MakeNewLord(
                tokraFaction,
                new LordJob_VisitColony(
                    tokraFaction,
                    contactCell,
                    ContactVisitDurationTicks),
                map,
                new[] { contact });

            GR_Log.Message(
                $"Generated non-trading, non-recruitable Tok'ra safehouse "
                + $"contact {contact.LabelShortCap} at {contactCell} with a "
                + "three-day peaceful visit duty.");
        }
    }
}
