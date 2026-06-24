using RimWorld;
using Verse;

namespace GateRimSG1.Weapons
{
    /// <summary>
    /// Developer-only helpers for focused validation before a recurrent
    /// capture mission depends on these tools.
    /// </summary>
    public static class NonLethalCaptureDebugActions
    {
        public static void GiveBolas(Pawn pawn)
        {
            GiveTool(pawn, GR_DefOf.SG1_Bolas);
        }

        public static void GiveTokraHypodermicRifle(Pawn pawn)
        {
            GiveTool(pawn, GR_DefOf.SG1_TokraHypodermicRifle);
        }

        public static void ShowNeutralizationChances(Pawn pawn)
        {
            float bolasChance = ChanceFor(
                pawn,
                GR_DefOf.SG1_BolasProjectile);
            float rifleChance = ChanceFor(
                pawn,
                GR_DefOf.SG1_TokraHypodermicDart);

            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    "GR_NonLethalCapture_DebugChanceReport".Translate(
                        pawn.LabelShortCap,
                        bolasChance.ToStringPercent("F0"),
                        rifleChance.ToStringPercent("F0"))));
        }

        public static void ClearNeutralization(Pawn pawn)
        {
            if (!NonLethalCaptureUtility.HasTemporaryNeutralization(pawn))
            {
                Messages.Message(
                    "GR_NonLethalCapture_DebugNothingToClear".Translate(
                        pawn?.LabelShortCap ?? "pawn"),
                    pawn,
                    MessageTypeDefOf.NeutralEvent,
                    historical: false);
                return;
            }

            NonLethalCaptureUtility.ClearTemporaryNeutralization(pawn);

            Messages.Message(
                "GR_NonLethalCapture_DebugCleared".Translate(
                    pawn.LabelShortCap),
                pawn,
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        private static float ChanceFor(Pawn pawn, ThingDef projectileDef)
        {
            NonLethalCaptureProjectileExtension profile = projectileDef
                ?.GetModExtension<NonLethalCaptureProjectileExtension>();

            return NonLethalCaptureUtility
                .CalculateNeutralizationChance(pawn, profile);
        }

        private static void GiveTool(Pawn pawn, ThingDef thingDef)
        {
            if (pawn == null || thingDef == null)
            {
                return;
            }

            ThingWithComps tool = ThingMaker.MakeThing(thingDef)
                as ThingWithComps;

            if (tool == null)
            {
                return;
            }

            tool.TryGetComp<CompQuality>()?.SetQuality(
                QualityCategory.Normal,
                ArtGenerationContext.Outsider);

            bool stored = pawn.inventory?.innerContainer.TryAdd(tool) == true;

            if (!stored && pawn.Spawned)
            {
                Thing placed;
                stored = GenPlace.TryPlaceThing(
                    tool,
                    pawn.Position,
                    pawn.Map,
                    ThingPlaceMode.Near,
                    out placed);
            }

            if (!stored && !tool.Destroyed)
            {
                tool.Destroy(DestroyMode.Vanish);
            }

            Messages.Message(
                stored
                    ? "GR_NonLethalCapture_DebugToolGiven".Translate(
                        thingDef.LabelCap,
                        pawn.LabelShortCap)
                    : "GR_NonLethalCapture_DebugToolGiveFailed".Translate(
                        thingDef.LabelCap,
                        pawn.LabelShortCap),
                pawn,
                stored
                    ? MessageTypeDefOf.NeutralEvent
                    : MessageTypeDefOf.RejectInput,
                historical: false);
        }
    }
}
