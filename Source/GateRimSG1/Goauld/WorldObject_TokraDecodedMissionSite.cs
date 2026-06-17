using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Temporary non-hostile Tok'ra mission marker created from the decoded
    /// operational lead. It represents an isolated Goa'uld relay on the world
    /// map without generating a combat map, reward or resolution yet.
    /// </summary>
    public class WorldObject_TokraDecodedMissionSite : WorldObject
    {
        public const int DurationTicks = 720000;

        private int ticksRemaining = DurationTicks;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(
                ref ticksRemaining,
                "ticksRemaining",
                DurationTicks);
        }

        protected override void Tick()
        {
            base.Tick();

            ticksRemaining--;

            if (ticksRemaining <= 0)
            {
                Expire();
            }
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            foreach (Gizmo gizmo in base.GetGizmos())
            {
                yield return gizmo;
            }

            // The normal player-facing flow is the caravan world-map
            // right-click menu. Keep direct world-site commands only as
            // diagnostic shortcuts.
            if (!GR_Debug.ShowAdvancedInformation)
            {
                yield break;
            }

            Command_Action reconCommand = new Command_Action
            {
                defaultLabel = "GR_TokraDecodedMissionWorldSite_ReconCommandLabel"
                    .Translate(),
                defaultDesc = "GR_TokraDecodedMissionWorldSite_ReconCommandDesc"
                    .Translate(GetRemainingDaysString()),
                action = TryPerformReconnaissance
            };

            if (GameComponent_TokraTrustTracker
                .IsFirstTrustMissionWorldSiteReconnoitered())
            {
                reconCommand.Disable(
                    "GR_TokraDecodedMissionWorldSite_ReconAlreadyComplete"
                        .Translate());
            }
            else if (GetPlayerCaravanAtSite() == null)
            {
                reconCommand.Disable(
                    "GR_TokraDecodedMissionWorldSite_ReconRequiresCaravan"
                        .Translate());
            }

            yield return reconCommand;

            Command_Action sabotageCommand = new Command_Action
            {
                defaultLabel = "GR_TokraDecodedMissionWorldSite_SabotageCommandLabel"
                    .Translate(),
                defaultDesc = "GR_TokraDecodedMissionWorldSite_SabotageCommandDesc"
                    .Translate(GetRemainingDaysString()),
                action = TryPrepareSabotageObjective
            };

            if (GameComponent_TokraTrustTracker
                .IsFirstTrustMissionRelaySabotagePrepared())
            {
                sabotageCommand.Disable(
                    "GR_TokraDecodedMissionWorldSite_SabotageAlreadyPrepared"
                        .Translate());
            }
            else if (!GameComponent_TokraTrustTracker
                .IsFirstTrustMissionWorldSiteReconnoitered())
            {
                sabotageCommand.Disable(
                    "GR_TokraDecodedMissionWorldSite_SabotageRequiresRecon"
                        .Translate());
            }
            else if (GetPlayerCaravanAtSite() == null)
            {
                sabotageCommand.Disable(
                    "GR_TokraDecodedMissionWorldSite_SabotageRequiresCaravan"
                        .Translate());
            }

            yield return sabotageCommand;
        }

        public override IEnumerable<FloatMenuOption> GetFloatMenuOptions(
            Caravan caravan)
        {
            IEnumerable<FloatMenuOption> baseOptions = base.GetFloatMenuOptions(
                caravan);

            if (baseOptions != null)
            {
                foreach (FloatMenuOption option in baseOptions)
                {
                    yield return option;
                }
            }

            if (!IsValidPlayerCaravan(caravan))
            {
                yield break;
            }

            if (caravan.Tile != Tile)
            {
                yield return GetTravelToSiteFloatMenuOption(caravan);
                yield break;
            }

            if (!GameComponent_TokraTrustTracker
                .IsFirstTrustMissionWorldSiteReconnoitered())
            {
                yield return GetReconnaissanceFloatMenuOption(caravan);
                yield break;
            }

            yield return GetSabotageObjectiveFloatMenuOption(caravan);
        }

        public override string GetInspectString()
        {
            string baseInspectString = base.GetInspectString();
            string missionInspectString;

            if (GameComponent_TokraTrustTracker
                .IsFirstTrustMissionRelaySabotagePrepared())
            {
                missionInspectString =
                    "GR_TokraDecodedMissionWorldSite_InspectStringSabotagePrepared"
                        .Translate(GetRemainingDaysString());
            }
            else if (GameComponent_TokraTrustTracker
                .IsFirstTrustMissionWorldSiteReconnoitered())
            {
                missionInspectString =
                    "GR_TokraDecodedMissionWorldSite_InspectStringReconnoitered"
                        .Translate(GetRemainingDaysString());
            }
            else
            {
                missionInspectString =
                    "GR_TokraDecodedMissionWorldSite_InspectString"
                        .Translate(GetRemainingDaysString());
            }

            if (string.IsNullOrEmpty(baseInspectString))
            {
                return missionInspectString;
            }

            return baseInspectString + "\n" + missionInspectString;
        }

        private void TryPerformReconnaissance()
        {
            TryPerformReconnaissance(GetPlayerCaravanAtSite());
        }

        private void TryPerformReconnaissance(Caravan caravan)
        {
            if (GameComponent_TokraTrustTracker
                .IsFirstTrustMissionWorldSiteReconnoitered())
            {
                Messages.Message(
                    "GR_TokraDecodedMissionWorldSite_ReconAlreadyComplete"
                        .Translate(),
                    this,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            if (!IsValidPlayerCaravanAtSite(caravan))
            {
                Messages.Message(
                    "GR_TokraDecodedMissionWorldSite_ReconRequiresCaravan"
                        .Translate(),
                    this,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            if (!GameComponent_TokraTrustTracker
                .NotifyFirstTrustMissionWorldSiteReconnoitered())
            {
                Messages.Message(
                    "GR_TokraDecodedMissionWorldSite_ReconAlreadyComplete"
                        .Translate(),
                    this,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            Find.LetterStack.ReceiveLetter(
                "GR_TokraDecodedMissionWorldSite_ReconLetterLabel".Translate(),
                "GR_TokraDecodedMissionWorldSite_ReconLetterText".Translate(
                    caravan.LabelCap),
                LetterDefOf.NeutralEvent,
                this);

            Messages.Message(
                "GR_TokraDecodedMissionWorldSite_ReconComplete".Translate(),
                this,
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            GR_Log.Message(
                "Tok'ra decoded mission world site reconnoitered by caravan "
                + $"{caravan.LabelCap} at tile {Tile}.");
        }

        private void TryPrepareSabotageObjective()
        {
            TryPrepareSabotageObjective(GetPlayerCaravanAtSite());
        }

        private void TryPrepareSabotageObjective(Caravan caravan)
        {
            if (GameComponent_TokraTrustTracker
                .IsFirstTrustMissionRelaySabotagePrepared())
            {
                Messages.Message(
                    "GR_TokraDecodedMissionWorldSite_SabotageAlreadyPrepared"
                        .Translate(),
                    this,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            if (!GameComponent_TokraTrustTracker
                .IsFirstTrustMissionWorldSiteReconnoitered())
            {
                Messages.Message(
                    "GR_TokraDecodedMissionWorldSite_SabotageRequiresRecon"
                        .Translate(),
                    this,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            if (!IsValidPlayerCaravanAtSite(caravan))
            {
                Messages.Message(
                    "GR_TokraDecodedMissionWorldSite_SabotageRequiresCaravan"
                        .Translate(),
                    this,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            if (!GameComponent_TokraTrustTracker
                .NotifyFirstTrustMissionRelaySabotagePrepared())
            {
                Messages.Message(
                    "GR_TokraDecodedMissionWorldSite_SabotageAlreadyPrepared"
                        .Translate(),
                    this,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            Find.LetterStack.ReceiveLetter(
                "GR_TokraDecodedMissionWorldSite_SabotageLetterLabel"
                    .Translate(),
                "GR_TokraDecodedMissionWorldSite_SabotageLetterText"
                    .Translate(caravan.LabelCap),
                LetterDefOf.NeutralEvent,
                this);

            Messages.Message(
                "GR_TokraDecodedMissionWorldSite_SabotageComplete".Translate(),
                this,
                MessageTypeDefOf.PositiveEvent,
                historical: true);

            GR_Log.Message(
                "Tok'ra relay sabotage objective prepared by caravan "
                + $"{caravan.LabelCap} at tile {Tile}.");
        }

        private FloatMenuOption GetTravelToSiteFloatMenuOption(Caravan caravan)
        {
            return new FloatMenuOption(
                "GR_TokraDecodedMissionWorldSite_TravelCommandLabel"
                    .Translate()
                    .ToString(),
                () => caravan.pather.StartPath(Tile, null, true, true));
        }

        private FloatMenuOption GetReconnaissanceFloatMenuOption(
            Caravan caravan)
        {
            string label = "GR_TokraDecodedMissionWorldSite_ReconCommandLabel"
                .Translate()
                .ToString();

            if (!IsValidPlayerCaravanAtSite(caravan))
            {
                return new FloatMenuOption(
                    label + ": "
                    + "GR_TokraDecodedMissionWorldSite_ReconRequiresCaravan"
                        .Translate()
                        .ToString(),
                    null);
            }

            return new FloatMenuOption(
                label,
                () => TryPerformReconnaissance(caravan));
        }

        private FloatMenuOption GetSabotageObjectiveFloatMenuOption(
            Caravan caravan)
        {
            string label =
                "GR_TokraDecodedMissionWorldSite_SabotageCommandLabel"
                    .Translate()
                    .ToString();

            if (GameComponent_TokraTrustTracker
                .IsFirstTrustMissionRelaySabotagePrepared())
            {
                return new FloatMenuOption(
                    label + ": "
                    + "GR_TokraDecodedMissionWorldSite_SabotageAlreadyPrepared"
                        .Translate()
                        .ToString(),
                    null);
            }

            if (!GameComponent_TokraTrustTracker
                .IsFirstTrustMissionWorldSiteReconnoitered())
            {
                return new FloatMenuOption(
                    label + ": "
                    + "GR_TokraDecodedMissionWorldSite_SabotageRequiresRecon"
                        .Translate()
                        .ToString(),
                    null);
            }

            if (!IsValidPlayerCaravanAtSite(caravan))
            {
                return new FloatMenuOption(
                    label + ": "
                    + "GR_TokraDecodedMissionWorldSite_SabotageRequiresCaravan"
                        .Translate()
                        .ToString(),
                    null);
            }

            return new FloatMenuOption(
                label,
                () => TryPrepareSabotageObjective(caravan));
        }

        private bool IsValidPlayerCaravan(Caravan caravan)
        {
            return caravan != null
                && !caravan.Destroyed
                && caravan.Faction == Faction.OfPlayer;
        }

        private Caravan GetPlayerCaravanAtSite()
        {
            if (Find.WorldObjects == null)
            {
                return null;
            }

            foreach (WorldObject worldObject in Find.WorldObjects.AllWorldObjects)
            {
                Caravan caravan = worldObject as Caravan;

                if (IsValidPlayerCaravanAtSite(caravan))
                {
                    return caravan;
                }
            }

            return null;
        }

        private bool IsValidPlayerCaravanAtSite(Caravan caravan)
        {
            return caravan != null
                && !caravan.Destroyed
                && caravan.Faction == Faction.OfPlayer
                && caravan.Tile == Tile;
        }

        private void Expire()
        {
            GR_Log.Message(
                "Tok'ra decoded mission world site expired at tile "
                + $"{Tile}.");

            Messages.Message(
                "GR_TokraDecodedMissionWorldSite_Expired".Translate(),
                MessageTypeDefOf.NeutralEvent,
                historical: true);

            Destroy();
        }

        private string GetRemainingDaysString()
        {
            float remainingDays = ticksRemaining / 60000f;

            if (remainingDays < 0f)
            {
                remainingDays = 0f;
            }

            return remainingDays.ToString("0.#");
        }
    }
}
