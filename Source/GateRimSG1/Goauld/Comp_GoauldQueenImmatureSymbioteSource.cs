using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public class Comp_GoauldQueenImmatureSymbioteSource : ThingComp
    {
        private const string ImmatureSymbioteDefName =
            "SG1_ImmaturePrimtaSymbiote";

        private int nextHarvestTick;

        private CompProperties_GoauldQueenImmatureSymbioteSource Props
            => (CompProperties_GoauldQueenImmatureSymbioteSource)props;

        private int RemainingCooldownTicks
            => Math.Max(0, nextHarvestTick - Find.TickManager.TicksGame);

        public override void PostExposeData()
        {
            base.PostExposeData();

            Scribe_Values.Look(
                ref nextHarvestTick,
                "nextImmatureSymbioteHarvestTick",
                0);
        }

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            foreach (Gizmo gizmo in base.CompGetGizmosExtra())
            {
                yield return gizmo;
            }

            if (!CanHarvestFromQueen())
            {
                yield break;
            }

            Command_Action command = new Command_Action
            {
                defaultLabel =
                    "GR_GoauldQueenHarvestImmatureSymbioteLabel".Translate(),
                defaultDesc =
                    "GR_GoauldQueenHarvestImmatureSymbioteDesc".Translate(),
                action = TryHarvest
            };

            if (RemainingCooldownTicks > 0)
            {
                float remainingDays = RemainingCooldownTicks / 60000f;

                command.Disable(
                    "GR_GoauldQueenHarvestCooldown".Translate(
                        remainingDays.ToString("0.0")));
            }

            yield return command;
        }

        public override string CompInspectStringExtra()
        {
            if (!GR_Debug.ShowAdvancedInformation)
            {
                return null;
            }

            return "GR_GoauldQueenHarvestDebugCooldown".Translate(
                RemainingCooldownTicks);
        }

        private void TryHarvest()
        {
            if (!CanHarvestFromQueen() || RemainingCooldownTicks > 0)
            {
                return;
            }

            ThingDef immatureDef =
                DefDatabase<ThingDef>.GetNamedSilentFail(
                    ImmatureSymbioteDefName);

            if (immatureDef == null)
            {
                GR_Log.Error(
                    "Cannot harvest an immature Prim'ta symbiote from the "
                    + "Goa'uld queen: SG1_ImmaturePrimtaSymbiote could not "
                    + "be resolved.");
                return;
            }

            Thing immature = ThingMaker.MakeThing(immatureDef);
            immature.stackCount = Props.spawnCount;

            Thing placedThing;

            if (!GenPlace.TryPlaceThing(
                    immature,
                    parent.Position,
                    parent.Map,
                    ThingPlaceMode.Near,
                    out placedThing))
            {
                if (!immature.Destroyed)
                {
                    immature.Destroy(DestroyMode.Vanish);
                }

                Messages.Message(
                    "GR_GoauldQueenHarvestPlacementFailed".Translate(),
                    MessageTypeDefOf.RejectInput,
                    historical: false);

                GR_Log.Warning(
                    "Cannot harvest an immature Prim'ta symbiote from the "
                    + $"Goa'uld queen at {parent.Position}: placement "
                    + "failed.");
                return;
            }

            nextHarvestTick =
                Find.TickManager.TicksGame + Props.harvestCooldownTicks;

            Messages.Message(
                "GR_GoauldQueenHarvestedImmatureSymbiote".Translate(
                    Props.spawnCount),
                placedThing,
                MessageTypeDefOf.PositiveEvent,
                historical: true);

            GR_Log.Message(
                $"Harvested {Props.spawnCount} immature Prim'ta "
                + $"symbiote(s) from Goa'uld queen {parent.LabelCap} "
                + $"({parent.ThingID}) at {parent.Position}; next "
                + $"harvest in {Props.harvestCooldownTicks} "
                + "tick(s).");
        }

        private bool CanHarvestFromQueen()
        {
            return parent.Spawned
                && (Prefs.DevMode || parent.Faction == Faction.OfPlayer);
        }
    }
}
