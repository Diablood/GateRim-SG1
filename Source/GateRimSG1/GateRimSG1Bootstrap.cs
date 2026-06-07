using Verse;

namespace GateRimSG1
{
    [StaticConstructorOnStartup]
    internal static class GateRimSG1Bootstrap
    {
        static GateRimSG1Bootstrap()
        {
            GR_Log.Message(
                $"Version {typeof(GateRimSG1Bootstrap).Assembly.GetName().Version} loaded.");
        }
    }
}
