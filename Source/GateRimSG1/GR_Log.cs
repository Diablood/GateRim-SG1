using Verse;

namespace GateRimSG1
{
    /// <summary>
    /// Centralized logging helper for GateRim SG-1.
    ///
    /// Informational lifecycle traces are written directly to Player.log only
    /// while RimWorld developer mode or the dedicated GateRim advanced-debug
    /// option is enabled. They intentionally bypass Verse.Log.Message so
    /// routine diagnostics cannot auto-open RimWorld's in-game log window.
    ///
    /// Warnings and errors continue to use Verse.Log and always remain visible.
    /// </summary>
    public static class GR_Log
    {
        private const string Prefix = "<color=#D9B44A>[GateRim SG-1]</color>";

        public static void Message(string message)
        {
            if (!GR_Debug.ShowAdvancedInformation)
            {
                return;
            }

            UnityEngine.Debug.Log(Prefix + " " + message);
        }

        public static void Warning(string message)
        {
            Log.Warning(Prefix + " " + message);
        }

        public static void Error(string message)
        {
            Log.Error(Prefix + " " + message);
        }

        public static void WarningOnce(string message, int key)
        {
            Log.WarningOnce(Prefix + " " + message, key);
        }

        public static void ErrorOnce(string message, int key)
        {
            Log.ErrorOnce(Prefix + " " + message, key);
        }
    }
}
