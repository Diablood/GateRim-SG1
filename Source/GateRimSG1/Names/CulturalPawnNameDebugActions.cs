using Verse;

namespace GateRimSG1.Names
{
    /// <summary>
    /// Single grouped developer entry point for inspecting all cultural name
    /// generators without spawning dozens of test pawns.
    /// </summary>
    public static class CulturalPawnNameDebugActions
    {
        public static void ShowSamples()
        {
            OpenSampleReport();
        }

        public static void OpenSampleReport()
        {
            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    CulturalPawnNameUtility.BuildSampleReport(12)));
        }
    }
}
