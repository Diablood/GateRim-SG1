using System.Text;
using GateRimSG1.Goauld;
using RimWorld;
using Verse;

namespace GateRimSG1.Storytelling
{
    /// <summary>
    /// Persistent lifecycle state for GateRim storyteller systems.
    ///
    /// The component exists in every game so switching storytellers is safe.
    /// Goa'uld inter-domain relation transitions are handled by their dedicated
    /// tracker and remain inactive unless the GateRim storyteller is selected.
    /// </summary>
    public sealed class GameComponent_GateRimStorytellerOrchestrator
        : GameComponent
    {
        private const int CurrentSchemaVersion = 1;
        private const int ObservationIntervalTicks = 250;

        private int schemaVersion = CurrentSchemaVersion;
        private bool wasGateRimStorytellerActive;
        private int activationCount;
        private int lastActivationTick = -1;
        private int lastDeactivationTick = -1;
        private int lastObservedTick = -1;
        private string lastObservedStorytellerDefName;

        public GameComponent_GateRimStorytellerOrchestrator(Game game)
        {
        }

        public static GameComponent_GateRimStorytellerOrchestrator Current
            => Verse.Current.Game
                ?.GetComponent<
                    GameComponent_GateRimStorytellerOrchestrator>();

        public bool IsActiveNow
            => GateRimStorytellerUtility.IsGateRimStorytellerActive;

        public int ActivationCount => activationCount;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(
                ref schemaVersion,
                "gateRimStorytellerSchemaVersion",
                CurrentSchemaVersion);
            Scribe_Values.Look(
                ref wasGateRimStorytellerActive,
                "gateRimStorytellerWasActive",
                false);
            Scribe_Values.Look(
                ref activationCount,
                "gateRimStorytellerActivationCount",
                0);
            Scribe_Values.Look(
                ref lastActivationTick,
                "gateRimStorytellerLastActivationTick",
                -1);
            Scribe_Values.Look(
                ref lastDeactivationTick,
                "gateRimStorytellerLastDeactivationTick",
                -1);
            Scribe_Values.Look(
                ref lastObservedTick,
                "gateRimStorytellerLastObservedTick",
                -1);
            Scribe_Values.Look(
                ref lastObservedStorytellerDefName,
                "gateRimStorytellerLastObservedDefName");

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                NormalizeState();
            }
        }

        public override void FinalizeInit()
        {
            base.FinalizeInit();
            RefreshNow();
        }

        public override void StartedNewGame()
        {
            base.StartedNewGame();
            RefreshNow();
        }

        public override void LoadedGame()
        {
            base.LoadedGame();
            RefreshNow();
        }

        public override void GameComponentTick()
        {
            base.GameComponentTick();

            int currentTick = Find.TickManager?.TicksGame ?? 0;

            if (lastObservedTick >= 0
                && currentTick - lastObservedTick
                    < ObservationIntervalTicks)
            {
                return;
            }

            RefreshNow();
        }

        public void RefreshNow()
        {
            GateRimStorytellerBootstrap.EnsureInitialized();

            int currentTick = Find.TickManager?.TicksGame ?? 0;
            bool active =
                GateRimStorytellerUtility.IsGateRimStorytellerActive;

            if (active && !wasGateRimStorytellerActive)
            {
                activationCount++;
                lastActivationTick = currentTick;
            }
            else if (!active && wasGateRimStorytellerActive)
            {
                lastDeactivationTick = currentTick;
            }

            wasGateRimStorytellerActive = active;
            lastObservedTick = currentTick;
            lastObservedStorytellerDefName =
                GateRimStorytellerUtility.ActiveStorytellerDefName;
        }

        public string BuildDebugReport()
        {
            RefreshNow();

            StringBuilder builder = new StringBuilder();
            builder.AppendLine(
                "GateRim SG-1 storyteller orchestration report");
            builder.AppendLine();
            builder.AppendLine(
                "active storyteller: "
                + GateRimStorytellerUtility.ActiveStorytellerLabel);
            builder.AppendLine(
                "active defName: "
                + GateRimStorytellerUtility
                    .ActiveStorytellerDefName);
            builder.AppendLine(
                "GateRim orchestration active: "
                + FormatBoolean(IsActiveNow));
            builder.AppendLine(
                "baseline definition: "
                + GateRimStorytellerUtility
                    .BaselineStorytellerDefName);
            builder.AppendLine(
                "baseline clone initialized: "
                + FormatBoolean(
                    GateRimStorytellerBootstrap.Initialized));
            builder.AppendLine(
                "baseline components: "
                + GateRimStorytellerBootstrap
                    .BaselineComponentCount);
            builder.AppendLine(
                "SG-1 components: "
                + GateRimStorytellerBootstrap
                    .GateRimComponentCount);
            builder.AppendLine(
                "persistent schema: " + schemaVersion);
            builder.AppendLine(
                "activations observed: " + activationCount);
            builder.AppendLine(
                "last activation: "
                + FormatTick(lastActivationTick));
            builder.AppendLine(
                "last deactivation: "
                + FormatTick(lastDeactivationTick));
            builder.AppendLine(
                "last observation: "
                + FormatTick(lastObservedTick));
            builder.AppendLine(
                "last observed defName: "
                + (lastObservedStorytellerDefName ?? "<none>"));
            builder.AppendLine();
            builder.AppendLine(
                "foundation incidents emitted: none");
            builder.AppendLine(
                "other storytellers modified: no");
            builder.AppendLine();

            GameComponent_GoauldInterDomainRelationTracker relationTracker =
                GameComponent_GoauldInterDomainRelationTracker.Current;

            if (relationTracker == null)
            {
                builder.AppendLine(
                    "Goa'uld inter-domain relation tracker: unavailable");
            }
            else
            {
                builder.AppendLine(
                    "Goa'uld inter-domain relation tracker: available");
                builder.AppendLine(
                    relationTracker.BuildOrchestrationSummary());
            }

            return builder.ToString();
        }

        private void NormalizeState()
        {
            schemaVersion = CurrentSchemaVersion;
            activationCount = activationCount < 0
                ? 0
                : activationCount;

            if (lastActivationTick < -1)
            {
                lastActivationTick = -1;
            }

            if (lastDeactivationTick < -1)
            {
                lastDeactivationTick = -1;
            }

            if (lastObservedTick < -1)
            {
                lastObservedTick = -1;
            }
        }

        private static string FormatBoolean(bool value)
        {
            return value ? "yes" : "no";
        }

        private static string FormatTick(int tick)
        {
            if (tick < 0)
            {
                return "<never>";
            }

            return tick
                + " ("
                + ((float)tick / GenDate.TicksPerDay)
                    .ToString("0.00")
                + " days)";
        }
    }
}
