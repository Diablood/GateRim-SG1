using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace GateRimSG1.Jaffa
{
    public enum JaffaHelmetMode
    {
        Automatic,
        AlwaysDeployed,
        AlwaysRetracted
    }

    public class CompProperties_RetractableJaffaHelmet : CompProperties
    {
        public string commandIconPath = "UI/Commands/SG1_JaffaHelmetMode";

        public CompProperties_RetractableJaffaHelmet()
        {
            compClass = typeof(Comp_RetractableJaffaHelmet);
        }
    }

    public class Comp_RetractableJaffaHelmet : ThingComp
    {
        private JaffaHelmetMode mode = JaffaHelmetMode.Automatic;

        public CompProperties_RetractableJaffaHelmet Props =>
            (CompProperties_RetractableJaffaHelmet)props;

        private Apparel Helmet => parent as Apparel;

        private bool IsDeployed => parent.def == GR_DefOf.SG1_JaffaDeployedHelmet;

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref mode, "jaffaHelmetMode", JaffaHelmetMode.Automatic);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                SynchronizeState();
            }
        }

        public override IEnumerable<Gizmo> CompGetWornGizmosExtra()
        {
            yield return new Command_Action
            {
                defaultLabel = "SG1_JaffaHelmetModeCommand".Translate() + ": " + ModeLabel,
                defaultDesc = "SG1_JaffaHelmetModeCommandDesc".Translate(),
                icon = ContentFinder<Texture2D>.Get(Props.commandIconPath, true),
                action = CycleMode
            };
        }

        public override string CompInspectStringExtra()
        {
            return "SG1_JaffaHelmetModeInspect".Translate() + ": " + ModeLabel
                + "\n"
                + "SG1_JaffaHelmetPositionInspect".Translate() + ": " + PositionLabel;
        }

        public override void Notify_Equipped(Pawn pawn)
        {
            base.Notify_Equipped(pawn);
            SynchronizeState(pawn);
        }

        public override void Notify_Unequipped(Pawn pawn)
        {
            base.Notify_Unequipped(pawn);
            SynchronizeState();
        }

        public void SynchronizeState(Pawn wearerOverride = null)
        {
            Pawn wearer = wearerOverride ?? Helmet?.Wearer;
            ThingDef desiredDef = ShouldBeDeployed(wearer)
                ? GR_DefOf.SG1_JaffaDeployedHelmet
                : GR_DefOf.SG1_JaffaRetractedHelmet;

            if (desiredDef == null || parent.def == desiredDef)
            {
                return;
            }

            parent.def = desiredDef;
            wearer?.Drawer?.renderer?.SetAllGraphicsDirty();

            GR_Log.Message(
                $"Updated Jaffa helmet {parent.ThingID} to "
                + $"{(IsDeployed ? "deployed" : "retracted")} state in {mode} mode.");
        }

        private bool ShouldBeDeployed(Pawn wearer)
        {
            switch (mode)
            {
                case JaffaHelmetMode.AlwaysDeployed:
                    return true;
                case JaffaHelmetMode.AlwaysRetracted:
                    return false;
                default:
                    return wearer != null && wearer.Drafted;
            }
        }

        private void CycleMode()
        {
            switch (mode)
            {
                case JaffaHelmetMode.Automatic:
                    mode = JaffaHelmetMode.AlwaysDeployed;
                    break;
                case JaffaHelmetMode.AlwaysDeployed:
                    mode = JaffaHelmetMode.AlwaysRetracted;
                    break;
                default:
                    mode = JaffaHelmetMode.Automatic;
                    break;
            }

            SynchronizeState();
            Pawn wearer = Helmet?.Wearer;
            if (wearer != null)
            {
                Messages.Message(
                    "SG1_JaffaHelmetModeChanged".Translate() + ": " + ModeLabel,
                    wearer,
                    MessageTypeDefOf.NeutralEvent,
                    false);
            }
        }

        private string ModeLabel
        {
            get
            {
                switch (mode)
                {
                    case JaffaHelmetMode.AlwaysDeployed:
                        return "SG1_JaffaHelmetModeAlwaysDeployed".Translate();
                    case JaffaHelmetMode.AlwaysRetracted:
                        return "SG1_JaffaHelmetModeAlwaysRetracted".Translate();
                    default:
                        return "SG1_JaffaHelmetModeAutomatic".Translate();
                }
            }
        }

        private string PositionLabel => IsDeployed
            ? "SG1_JaffaHelmetPositionDeployed".Translate()
            : "SG1_JaffaHelmetPositionRetracted".Translate();
    }

    public class GameComponent_RetractableJaffaHelmetUpdater : GameComponent
    {
        private const int ScanIntervalTicks = 15;

        public GameComponent_RetractableJaffaHelmetUpdater(Game game)
        {
        }

        public override void GameComponentTick()
        {
            if (Find.TickManager == null || Find.TickManager.TicksGame % ScanIntervalTicks != 0)
            {
                return;
            }

            for (int mapIndex = 0; mapIndex < Find.Maps.Count; mapIndex++)
            {
                SynchronizeMap(Find.Maps[mapIndex]);
            }
        }

        private static void SynchronizeMap(Map map)
        {
            IReadOnlyList<Pawn> pawns = map?.mapPawns?.AllPawnsSpawned;
            if (pawns == null)
            {
                return;
            }

            for (int pawnIndex = 0; pawnIndex < pawns.Count; pawnIndex++)
            {
                List<Apparel> wornApparel = pawns[pawnIndex]?.apparel?.WornApparel;
                if (wornApparel == null)
                {
                    continue;
                }

                for (int apparelIndex = 0; apparelIndex < wornApparel.Count; apparelIndex++)
                {
                    wornApparel[apparelIndex]
                        ?.GetComp<Comp_RetractableJaffaHelmet>()
                        ?.SynchronizeState(pawns[pawnIndex]);
                }
            }
        }
    }
}
