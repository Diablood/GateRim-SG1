using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Scenarios
{
    /// <summary>
    /// Dresses every generated player starter with the validated generic
    /// four-piece SG-team field set.
    ///
    /// Weapons and bivouac supplies remain normal scenario starting things so
    /// the player can distribute and deploy them after arriving on the map.
    /// </summary>
    public class ScenPart_SGTeamStartingGear : ScenPart
    {
        private const int MinimumStartingAge = 20;

        public ThingDef uniform;
        public ThingDef boots;
        public ThingDef gloves;
        public ThingDef vest;

        public override string Summary(Scenario scen)
        {
            return "GR_StrandedSGTeam_StartingGearSummary".Translate();
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Defs.Look(ref uniform, "uniform");
            Scribe_Defs.Look(ref boots, "boots");
            Scribe_Defs.Look(ref gloves, "gloves");
            Scribe_Defs.Look(ref vest, "vest");
        }

        public override bool AllowPlayerStartingPawn(
            Pawn pawn,
            bool tryingToRedress,
            PawnGenerationRequest request)
        {
            return pawn?.ageTracker != null
                && pawn.ageTracker.AgeBiologicalYears >= MinimumStartingAge
                && !pawn.WorkTagIsDisabled(WorkTags.Violent);
        }

        public override void Notify_PawnGenerated(
            Pawn pawn,
            PawnGenerationContext context,
            bool redressed)
        {
            if (context != PawnGenerationContext.PlayerStarter
                || pawn?.apparel == null)
            {
                return;
            }

            pawn.apparel.DestroyAll();

            Wear(pawn, uniform);
            Wear(pawn, boots);
            Wear(pawn, gloves);
            Wear(pawn, vest);
        }

        public override IEnumerable<string> ConfigErrors()
        {
            foreach (string error in base.ConfigErrors())
            {
                yield return error;
            }

            if (uniform == null)
            {
                yield return "SG-team starting uniform is null.";
            }

            if (boots == null)
            {
                yield return "SG-team starting boots are null.";
            }

            if (gloves == null)
            {
                yield return "SG-team starting gloves are null.";
            }

            if (vest == null)
            {
                yield return "SG-team starting vest is null.";
            }
        }

        public override bool CanCoexistWith(ScenPart other)
        {
            return !(other is ScenPart_SGTeamStartingGear);
        }

        private static void Wear(Pawn pawn, ThingDef apparelDef)
        {
            if (apparelDef == null)
            {
                return;
            }

            Apparel apparel = ThingMaker.MakeThing(apparelDef) as Apparel;
            if (apparel == null)
            {
                GR_Log.Warning(
                    $"Cannot equip SG-team starter apparel: "
                    + $"{apparelDef.defName} is not an Apparel ThingDef.");
                return;
            }

            CompQuality quality = apparel.TryGetComp<CompQuality>();
            quality?.SetQuality(
                QualityCategory.Normal,
                ArtGenerationContext.Outsider);

            pawn.apparel.Wear(
                apparel,
                dropReplacedApparel: false);
        }
    }
}
