using GateRimSG1.Goauld;
using RimWorld;
using Verse;

namespace GateRimSG1.Jaffa
{
    /// <summary>
    /// Public access point for intrinsic Jaffa forehead-mark data.
    /// </summary>
    public static class JaffaForeheadMarkUtility
    {
        public static GameComponent_JaffaForeheadMarks Component =>
            Current.Game?.GetComponent<GameComponent_JaffaForeheadMarks>();

        public static JaffaForeheadMarkDef MarkFor(Pawn pawn)
        {
            return Component?.MarkFor(pawn);
        }

        public static bool SetMark(
            Pawn pawn,
            JaffaForeheadMarkDef markDef)
        {
            return Component?.SetMark(pawn, markDef) ?? false;
        }

        public static bool SetMarkForRank(
            Pawn pawn,
            Faction faction,
            GoauldJaffaMarkRank rank)
        {
            return SetMark(
                pawn,
                GoauldSystemLordDomainUtility.MarkFor(faction, rank));
        }

        public static bool RemoveMark(Pawn pawn)
        {
            return Component?.RemoveMark(pawn) ?? false;
        }

        public static void NotifyGraphicsDirty(Pawn pawn)
        {
            pawn?.Drawer?.renderer?.SetAllGraphicsDirty();
        }

        public static string PawnDebugLabel(Pawn pawn)
        {
            if (pawn == null)
            {
                return "<null pawn>";
            }

            return $"{pawn.LabelShort} ({pawn.ThingID})";
        }
    }
}
