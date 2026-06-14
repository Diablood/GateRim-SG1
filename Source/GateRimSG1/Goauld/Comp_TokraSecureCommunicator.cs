using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// First trusted-tier Tok'ra communicator foundation.
    ///
    /// The current prototype deliberately does not grant items, treatments,
    /// quests or military aid. It gives the player a safe, testable interaction
    /// point that future medical, safehouse and defensive support requests can
    /// attach to once those systems are implemented.
    /// </summary>
    public class Comp_TokraSecureCommunicator : ThingComp
    {
        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            foreach (Gizmo gizmo in base.CompGetGizmosExtra())
            {
                yield return gizmo;
            }

            if (!parent.Spawned
                || (parent.Faction != null && parent.Faction != Faction.OfPlayer))
            {
                yield break;
            }

            Command_Action command = new Command_Action
            {
                defaultLabel = "GR_TokraSecureCommunicator_CommandLabel".Translate(),
                defaultDesc = "GR_TokraSecureCommunicator_CommandDesc".Translate(),
                action = TryOpenSecureChannel
            };

            string disabledReason = GetDisabledReason();

            if (!string.IsNullOrEmpty(disabledReason))
            {
                command.Disable(disabledReason);
            }

            yield return command;
        }

        public override string CompInspectStringExtra()
        {
            if (!parent.Spawned)
            {
                return null;
            }

            return "GR_TokraSecureCommunicator_Inspect".Translate(
                GetTrustTierLabel(GameComponent_TokraTrustTracker.GetCurrentTier()),
                GetStatusLabel()).ToString();
        }

        private void TryOpenSecureChannel()
        {
            string disabledReason = GetDisabledReason();

            if (!string.IsNullOrEmpty(disabledReason))
            {
                Messages.Message(
                    disabledReason,
                    parent,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            Find.WindowStack.Add(
                new Dialog_MessageBox(
                    "GR_TokraSecureCommunicator_DialogTrusted".Translate()
                        .ToString()));

            Messages.Message(
                "GR_TokraSecureCommunicator_ChannelOpened".Translate(),
                parent,
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            GR_Log.Message(
                $"Opened trusted Tok'ra secure communicator channel at "
                + $"{parent.Position} on map "
                + $"{parent.Map?.uniqueID.ToString() ?? "unknown"}.");
        }

        private string GetDisabledReason()
        {
            if (parent.Faction != null && parent.Faction != Faction.OfPlayer)
            {
                return "GR_TokraSecureCommunicator_NotPlayerControlled"
                    .Translate()
                    .ToString();
            }

            CompPowerTrader powerComp = parent.GetComp<CompPowerTrader>();

            if (powerComp != null && !powerComp.PowerOn)
            {
                return "GR_TokraSecureCommunicator_Unpowered".Translate().ToString();
            }

            if (GameComponent_TokraTrustTracker.GetCurrentTier()
                != TokraTrustTier.Trusted)
            {
                return "GR_TokraSecureCommunicator_RequiresTrusted".Translate(
                    GetTrustTierLabel(
                        GameComponent_TokraTrustTracker.GetCurrentTier()))
                    .ToString();
            }

            return null;
        }

        private string GetStatusLabel()
        {
            string disabledReason = GetDisabledReason();

            if (string.IsNullOrEmpty(disabledReason))
            {
                return "GR_TokraSecureCommunicator_StatusReady".Translate().ToString();
            }

            return "GR_TokraSecureCommunicator_StatusLocked".Translate().ToString();
        }

        private static string GetTrustTierLabel(TokraTrustTier tier)
        {
            switch (tier)
            {
                case TokraTrustTier.Wary:
                    return "GR_TokraTrust_Tier_Wary".Translate().ToString();
                case TokraTrustTier.Cooperative:
                    return "GR_TokraTrust_Tier_Cooperative".Translate().ToString();
                case TokraTrustTier.Trusted:
                    return "GR_TokraTrust_Tier_Trusted".Translate().ToString();
                default:
                    return "GR_TokraTrust_Tier_Neutral".Translate().ToString();
            }
        }
    }
}
