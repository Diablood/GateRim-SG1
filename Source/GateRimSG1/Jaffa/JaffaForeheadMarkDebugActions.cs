using LudeonTK;
using RimWorld;
using Verse;

namespace GateRimSG1.Jaffa
{
    /// <summary>
    /// Developer-only map tools for assigning marks to any pawn, including a
    /// non-Jaffa infiltrator used for scenario and lore tests.
    /// </summary>
    public static class JaffaForeheadMarkDebugActions
    {
        [DebugAction(
            "GateRim SG-1",
            "Jaffa mark: black",
            actionType = DebugActionType.ToolMapForPawns,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void SetOrdinaryBlack(Pawn pawn)
        {
            Apply(pawn, GR_DefOf.SG1_JaffaForeheadMark_GenericIntrinsic);
        }

        [DebugAction(
            "GateRim SG-1",
            "Jaffa mark: silver",
            actionType = DebugActionType.ToolMapForPawns,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void SetEliteSilver(Pawn pawn)
        {
            Apply(pawn, GR_DefOf.SG1_JaffaForeheadMark_GenericSilverIntrinsic);
        }

        [DebugAction(
            "GateRim SG-1",
            "Jaffa mark: gold",
            actionType = DebugActionType.ToolMapForPawns,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void SetFirstPrimeGold(Pawn pawn)
        {
            Apply(pawn, GR_DefOf.SG1_JaffaForeheadMark_GenericGoldIntrinsic);
        }

        [DebugAction(
            "GateRim SG-1",
            "Clear Jaffa mark",
            actionType = DebugActionType.ToolMapForPawns,
            allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void Remove(Pawn pawn)
        {
            bool removed = JaffaForeheadMarkUtility.RemoveMark(pawn);

            Messages.Message(
                removed
                    ? "GR_JaffaForeheadMark_Removed".Translate(pawn.LabelShortCap)
                    : "GR_JaffaForeheadMark_AlreadyAbsent".Translate(
                        pawn.LabelShortCap),
                pawn,
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        private static void Apply(
            Pawn pawn,
            JaffaForeheadMarkDef markDef)
        {
            bool changed = JaffaForeheadMarkUtility.SetMark(pawn, markDef);

            Messages.Message(
                changed
                    ? "GR_JaffaForeheadMark_Applied".Translate(
                        markDef.LabelCap,
                        pawn.LabelShortCap)
                    : "GR_JaffaForeheadMark_AlreadyApplied".Translate(
                        markDef.LabelCap,
                        pawn.LabelShortCap),
                pawn,
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }
    }
}
