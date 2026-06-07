using Verse;

namespace GateRimSG1
{
    /// <summary>
    /// Centralized logging helper for GateRim SG-1.
    /// Keep all future C# diagnostics behind this wrapper so Player.log
    /// entries remain easy to filter and identify.
    /// </summary>
    public static class GR_Log
    {
        private const string Prefix = "<color=#D9B44A>[GateRim SG-1]</color>";

        public static void Message(string message)
        {
            Log.Message(Prefix + " " + message);
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
