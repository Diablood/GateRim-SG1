using GateRimSG1.Goauld;
using Verse;

namespace GateRimSG1
{
    [StaticConstructorOnStartup]
    internal static class GateRimSG1Bootstrap
    {
        static GateRimSG1Bootstrap()
        {
            GateRimSG1HarmonyInitializer.Apply();

            GR_Log.Message(
                $"Version {typeof(GateRimSG1Bootstrap).Assembly.GetName().Version} loaded.");
        }
    }
}
