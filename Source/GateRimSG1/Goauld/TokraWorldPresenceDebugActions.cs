using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Developer-only inspection for optional Tok'ra world presence.
    /// </summary>
    public static class TokraWorldPresenceDebugActions
    {
        public static void ShowAudit()
        {
            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    TokraFactionUtility.GetWorldPresenceAuditReport()));
        }
    }
}
