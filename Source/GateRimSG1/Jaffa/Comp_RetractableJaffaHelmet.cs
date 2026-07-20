using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace GateRimSG1.Jaffa
{
    public enum JaffaHelmetMode
    {
        // Retained only to migrate existing saves without losing the current
        // physical helmet position.
        Automatic,
        AlwaysDeployed,
        AlwaysRetracted
    }

    public class CompProperties_RetractableJaffaHelmet : CompProperties
    {
        public string commandIconPath = "UI/Commands/SG1_JaffaHelmetMode";
        public ThingDef deployedDef;
        public ThingDef retractedDef;

        public CompProperties_RetractableJaffaHelmet()
        {
            compClass = typeof(Comp_RetractableJaffaHelmet);
        }
    }

    public class Comp_RetractableJaffaHelmet : ThingComp
    {
        private const JaffaHelmetMode LegacyDefaultMode = JaffaHelmetMode.Automatic;

        private JaffaHelmetMode mode = LegacyDefaultMode;

        public CompProperties_RetractableJaffaHelmet Props =>
            (CompProperties_RetractableJaffaHelmet)props;

        private Apparel Helmet => parent as Apparel;

        private ThingDef DeployedDef =>
            Props.deployedDef ?? GR_DefOf.SG1_JaffaDeployedHelmet;

        private ThingDef RetractedDef =>
            Props.retractedDef ?? GR_DefOf.SG1_JaffaRetractedHelmet;

        private bool IsDeployed => parent.def == DeployedDef;

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref mode, "jaffaHelmetMode", LegacyDefaultMode);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                EnsureManualMode();
                SynchronizeState();
            }
        }

        public override IEnumerable<Gizmo> CompGetWornGizmosExtra()
        {
            Pawn wearer = Helmet?.Wearer;
            if (wearer == null || !wearer.IsColonistPlayerControlled)
            {
                yield break;
            }

            EnsureManualMode();

            yield return new Command_Action
            {
                defaultLabel = IsDeployed
                    ? "SG1_JaffaHelmetModeAlwaysRetracted".Translate()
                    : "SG1_JaffaHelmetModeAlwaysDeployed".Translate(),
                defaultDesc = "SG1_JaffaHelmetModeCommandDesc".Translate(),
                icon = ContentFinder<Texture2D>.Get(Props.commandIconPath, true),
                action = TogglePosition
            };
        }

        public override string CompInspectStringExtra()
        {
            EnsureManualMode();

            return "SG1_JaffaHelmetPositionInspect".Translate() + ": " + PositionLabel;
        }

        public override void Notify_Equipped(Pawn pawn)
        {
            base.Notify_Equipped(pawn);
            EnsureManualMode();
            SynchronizeState(pawn);
        }

        public override void Notify_Unequipped(Pawn pawn)
        {
            base.Notify_Unequipped(pawn);
            EnsureManualMode();
            SynchronizeState();
        }

        public void SynchronizeState(Pawn wearerOverride = null)
        {
            EnsureManualMode();

            Pawn wearer = wearerOverride ?? Helmet?.Wearer;
            ThingDef desiredDef = mode == JaffaHelmetMode.AlwaysDeployed
                ? DeployedDef
                : RetractedDef;

            if (desiredDef == null || parent.def == desiredDef)
            {
                return;
            }

            parent.def = desiredDef;
            wearer?.Drawer?.renderer?.SetAllGraphicsDirty();
        }

        private void EnsureManualMode()
        {
            if (mode != JaffaHelmetMode.Automatic)
            {
                return;
            }

            mode = IsDeployed
                ? JaffaHelmetMode.AlwaysDeployed
                : JaffaHelmetMode.AlwaysRetracted;
        }

        private void TogglePosition()
        {
            EnsureManualMode();

            mode = IsDeployed
                ? JaffaHelmetMode.AlwaysRetracted
                : JaffaHelmetMode.AlwaysDeployed;

            SynchronizeState();

            Pawn wearer = Helmet?.Wearer;
            if (wearer != null)
            {
                Messages.Message(
                    "SG1_JaffaHelmetModeChanged".Translate() + ": " + PositionLabel,
                    wearer,
                    MessageTypeDefOf.NeutralEvent,
                    false);
            }
        }

        private string PositionLabel => IsDeployed
            ? "SG1_JaffaHelmetPositionDeployed".Translate()
            : "SG1_JaffaHelmetPositionRetracted".Translate();
    }

    // Kept as an empty compatibility component so saves made before 0.3.101-dev
    // can still resolve the historical type. Automatic synchronization is gone.
    public class GameComponent_RetractableJaffaHelmetUpdater : GameComponent
    {
        public GameComponent_RetractableJaffaHelmetUpdater(Game game)
        {
        }
    }
}
