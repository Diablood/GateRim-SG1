using GateRimSG1.Culture;
using GateRimSG1.Names;
using UnityEngine;
using Verse;

namespace GateRimSG1
{
    /// <summary>
    /// Persistent player-facing GateRim SG-1 settings.
    /// </summary>
    public class GateRimSG1ModSettings : ModSettings
    {
        public bool showAdvancedDebugInformation;

        public override void ExposeData()
        {
            Scribe_Values.Look(
                ref showAdvancedDebugInformation,
                "showAdvancedDebugInformation",
                false);

            base.ExposeData();
        }
    }

    /// <summary>
    /// Entry point for the GateRim SG-1 mod-settings page.
    /// </summary>
    public class GateRimSG1Mod : Mod
    {
        public static GateRimSG1ModSettings Settings { get; private set; }

        public GateRimSG1Mod(ModContentPack content) : base(content)
        {
            Settings = GetSettings<GateRimSG1ModSettings>();
        }

        public override string SettingsCategory()
        {
            return "GateRim SG-1";
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            GateRimSG1ModSettings settings = Settings;

            if (settings == null)
            {
                return;
            }

            Listing_Standard listing = new Listing_Standard();
            listing.Begin(inRect);

            listing.CheckboxLabeled(
                "GR_Settings_ShowAdvancedDebugInformation_Label".Translate(),
                ref settings.showAdvancedDebugInformation,
                "GR_Settings_ShowAdvancedDebugInformation_Desc".Translate());

            listing.Gap();
            listing.Label(
                "GR_Settings_ShowAdvancedDebugInformation_Note".Translate());

            if (GR_Debug.ShowAdvancedInformation)
            {
                listing.GapLine();
                listing.Label(
                    "GR_CulturalNames_SettingsDebugLabel".Translate());

                if (listing.ButtonText(
                    "GR_CulturalNames_OpenSamplesButton".Translate()))
                {
                    CulturalPawnNameDebugActions.OpenSampleReport();
                }

                listing.Gap();
                listing.Label(
                    "GR_CulturalIdentity_SettingsDebugLabel".Translate());

                if (listing.ButtonText(
                    "GR_CulturalIdentity_InspectSelectedButton".Translate()))
                {
                    CulturalIdentityDebugActions
                        .OpenSelectedPawnReport();
                }
            }

            listing.End();
        }
    }

    /// <summary>
    /// Shared visibility rules for technical diagnostics and developer-only
    /// state-changing actions.
    ///
    /// RimWorld developer mode always exposes diagnostics. Normal players can
    /// opt in to read-only technical information from the GateRim SG-1
    /// settings page, but that option never grants commands that force or
    /// bypass gameplay state.
    /// </summary>
    public static class GR_Debug
    {
        public static bool ShowAdvancedInformation
        {
            get
            {
                return Prefs.DevMode
                    || GateRimSG1Mod.Settings
                        ?.showAdvancedDebugInformation == true;
            }
        }

        public static bool DeveloperActionsEnabled => Prefs.DevMode;
    }
}
