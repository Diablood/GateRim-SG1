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

            listing.End();
        }
    }

    /// <summary>
    /// Shared visibility rule for technical diagnostics.
    ///
    /// RimWorld developer mode always exposes diagnostics. Normal players can
    /// opt in explicitly from the GateRim SG-1 mod-settings page.
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
    }
}
