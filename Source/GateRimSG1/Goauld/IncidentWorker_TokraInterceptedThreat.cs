using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Tok'ra-initiated warning that schedules a delayed Goa'uld/Jaffa raid.
    ///
    /// The incident itself is not the attack. It creates persistent advance
    /// intelligence, shows a letter, activates an alert and lets the tactical
    /// assessment command reveal useful preparation details before the raid
    /// starts later.
    /// </summary>
    public class IncidentWorker_TokraInterceptedThreat : IncidentWorker
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            if (!base.CanFireNowSub(parms))
            {
                return false;
            }

            Map map = parms?.target as Map;

            return map != null
                && GameComponent_TokraTrustTracker.GetCurrentTier()
                    == TokraTrustTier.Trusted
                && !GameComponent_TokraInterceptedThreatTracker
                    .HasActiveThreat();
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = parms?.target as Map;

            if (map == null)
            {
                GR_Log.Warning(
                    "Cannot start Tok'ra intercepted threat: the incident "
                    + "target is not a map.");
                return false;
            }

            if (GameComponent_TokraTrustTracker.GetCurrentTier()
                != TokraTrustTier.Trusted)
            {
                GR_Log.Message(
                    "Cannot start Tok'ra intercepted threat: trusted Tok'ra "
                    + "contact is not available yet.");
                return false;
            }

            if (!GameComponent_TokraInterceptedThreatTracker
                .TryStartInterceptedThreat(map))
            {
                GR_Log.Message(
                    "Cannot start Tok'ra intercepted threat: another "
                    + "intercepted threat is already active or the tracker is "
                    + "unavailable.");
                return false;
            }

            SendStandardLetter(
                parms,
                null,
                GameComponent_TokraInterceptedThreatTracker
                    .GetRemainingThreatWindowLabelForMap(map)
                    .Named("ARRIVALWINDOW"),
                GameComponent_TokraInterceptedThreatTracker
                    .GetThreatSignatureLabelForMap(map)
                    .Named("SIGNATURE"));

            Messages.Message(
                "GR_TokraInterceptedThreat_Intercepted".Translate(),
                MessageTypeDefOf.NegativeEvent,
                historical: true);

            GR_Log.Message(
                "Started Tok'ra intercepted threat contact for map "
                + $"{map.uniqueID}.");

            return true;
        }
    }
}
