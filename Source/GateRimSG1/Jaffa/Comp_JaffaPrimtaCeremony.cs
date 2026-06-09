using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace GateRimSG1.Jaffa
{
    /// <summary>
    /// First formal Jaffa Prim'ta ceremony prototype.
    ///
    /// A colony-owned Goa'uld ritual basin can start a timed ceremony for one
    /// nearby eligible Jaffa and one nearby physical Prim'ta larva. The larva
    /// is consumed on completion and SG1_JaffaPrimta is attached without using
    /// the medical-operation bill.
    ///
    /// This Core + Biotech fallback remains intentionally separate from future
    /// Ideology ritual integration.
    /// </summary>
    public class Comp_JaffaPrimtaCeremony : ThingComp
    {
        private Pawn ceremonyTarget;
        private Thing ceremonyLarva;
        private int ceremonyTicksRemaining;
        private int ceremonyTicksTotal;

        private CompProperties_JaffaPrimtaCeremony Props
            => (CompProperties_JaffaPrimtaCeremony)props;

        private bool CeremonyInProgress
            => ceremonyTarget != null
                && ceremonyLarva != null
                && ceremonyTicksRemaining > 0;

        public override void PostExposeData()
        {
            base.PostExposeData();

            Scribe_References.Look(ref ceremonyTarget, "jaffaPrimtaCeremonyTarget");
            Scribe_References.Look(ref ceremonyLarva, "jaffaPrimtaCeremonyLarva");
            Scribe_Values.Look(ref ceremonyTicksRemaining, "jaffaPrimtaCeremonyTicksRemaining", 0);
            Scribe_Values.Look(ref ceremonyTicksTotal, "jaffaPrimtaCeremonyTicksTotal", 0);

            if (Scribe.mode == LoadSaveMode.PostLoadInit
                && CeremonyInProgress)
            {
                GR_Log.Message(
                    $"Loaded formal Jaffa Prim'ta ceremony for "
                    + $"{JaffaPrimtaUtility.PawnDebugLabel(ceremonyTarget)} "
                    + $"near basin {ThingDebugLabel(parent)} "
                    + $"with {ceremonyTicksRemaining} / {ceremonyTicksTotal} ticks remaining.");
            }
        }

        public override void CompTick()
        {
            base.CompTick();

            if (!CeremonyInProgress)
            {
                return;
            }

            if (!CanContinueCeremony())
            {
                CancelCeremony(
                    "GR_JaffaPrimtaCeremony_CancelledInvalid",
                    logAsWarning: true);

                return;
            }

            ceremonyTicksRemaining--;

            if (ceremonyTicksRemaining <= 0)
            {
                CompleteCeremony();
            }
        }

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            foreach (Gizmo gizmo in base.CompGetGizmosExtra())
            {
                yield return gizmo;
            }

            if (parent == null
                || parent.Destroyed
                || !parent.Spawned
                || parent.Faction != Faction.OfPlayer)
            {
                yield break;
            }

            if (CeremonyInProgress)
            {
                yield return new Command_Action
                {
                    defaultLabel = "GR_JaffaPrimtaCeremony_CancelCommandLabel".Translate(),
                    defaultDesc = "GR_JaffaPrimtaCeremony_CancelCommandDescription".Translate(),
                    icon = ContentFinder<Texture2D>.Get("UI/Commands/SG1_RitualImplantation"),
                    action = CancelCeremonyManually
                };

                yield break;
            }

            yield return new Command_Action
            {
                defaultLabel = "GR_JaffaPrimtaCeremony_CommandLabel".Translate(),
                defaultDesc = "GR_JaffaPrimtaCeremony_CommandDescription".Translate(),
                icon = ContentFinder<Texture2D>.Get("UI/Commands/SG1_RitualImplantation"),
                action = BeginTargeting
            };
        }

        public override string CompInspectStringExtra()
        {
            if (!CeremonyInProgress)
            {
                return null;
            }

            return "GR_JaffaPrimtaCeremony_Inspect".Translate(
                ceremonyTarget.LabelShortCap,
                ceremonyLarva.LabelCap,
                ceremonyTicksRemaining,
                ceremonyTicksTotal);
        }

        private void BeginTargeting()
        {
            if (FindNearestValidLarva() == null)
            {
                Messages.Message(
                    "GR_JaffaPrimtaCeremony_NoLarvaNearby".Translate(),
                    parent,
                    MessageTypeDefOf.RejectInput,
                    historical: false);

                return;
            }

            if (FindAnyEligibleNearbyTarget() == null)
            {
                Messages.Message(
                    "GR_JaffaPrimtaCeremony_NoTargetNearby".Translate(),
                    parent,
                    MessageTypeDefOf.RejectInput,
                    historical: false);

                return;
            }

            TargetingParameters targetingParameters = new TargetingParameters
            {
                canTargetPawns = true,
                canTargetLocations = false,
                validator = delegate(TargetInfo targetInfo)
                {
                    return IsValidTarget(targetInfo.Thing as Pawn);
                }
            };

            Find.Targeter.BeginTargeting(
                targetingParameters,
                delegate(LocalTargetInfo targetInfo)
                {
                    Pawn selectedTarget = targetInfo.Thing as Pawn;

                    if (!IsValidTarget(selectedTarget))
                    {
                        Messages.Message(
                            "GR_JaffaPrimtaCeremony_InvalidTarget".Translate(),
                            parent,
                            MessageTypeDefOf.RejectInput,
                            historical: false);

                        return;
                    }

                    StartCeremony(selectedTarget);
                });
        }

        private void StartCeremony(Pawn selectedTarget)
        {
            Thing selectedLarva = FindNearestValidLarva();

            if (!IsValidTarget(selectedTarget)
                || !IsValidLarva(selectedLarva))
            {
                Messages.Message(
                    "GR_JaffaPrimtaCeremony_StartFailed".Translate(),
                    parent,
                    MessageTypeDefOf.RejectInput,
                    historical: false);

                return;
            }

            ceremonyTarget = selectedTarget;
            ceremonyLarva = selectedLarva;
            ceremonyTicksTotal = Math.Max(1, Props.ceremonyDurationTicks);
            ceremonyTicksRemaining = ceremonyTicksTotal;

            GR_Log.Message(
                $"Started formal Jaffa Prim'ta ceremony for "
                + $"{JaffaPrimtaUtility.PawnDebugLabel(ceremonyTarget)} "
                + $"near basin {ThingDebugLabel(parent)} "
                + $"using larva {ThingDebugLabel(ceremonyLarva)} "
                + $"for {ceremonyTicksTotal} ticks.");

            Messages.Message(
                "GR_JaffaPrimtaCeremony_Started".Translate(
                    ceremonyTarget.LabelShortCap,
                    ceremonyTicksTotal),
                ceremonyTarget,
                MessageTypeDefOf.NeutralEvent,
                historical: true);
        }

        private void CompleteCeremony()
        {
            if (!CanContinueCeremony())
            {
                CancelCeremony(
                    "GR_JaffaPrimtaCeremony_CancelledInvalid",
                    logAsWarning: true);

                return;
            }

            Pawn completedTarget = ceremonyTarget;
            Thing consumedLarva = ceremonyLarva;

            ClearCeremony();

            Thing splitLarva = consumedLarva.SplitOff(1);
            splitLarva.Destroy(DestroyMode.Vanish);

            Hediff primta = HediffMaker.MakeHediff(
                GR_DefOf.SG1_JaffaPrimta,
                completedTarget);

            completedTarget.health.AddHediff(primta);

            GR_Log.Message(
                $"Completed formal Jaffa Prim'ta ceremony for "
                + $"{JaffaPrimtaUtility.PawnDebugLabel(completedTarget)} "
                + $"near basin {ThingDebugLabel(parent)}.");

            Messages.Message(
                "GR_JaffaPrimtaCeremony_Success".Translate(
                    completedTarget.LabelShortCap),
                completedTarget,
                MessageTypeDefOf.PositiveEvent,
                historical: true);
        }

        private bool CanContinueCeremony()
        {
            return parent != null
                && !parent.Destroyed
                && parent.Spawned
                && parent.Faction == Faction.OfPlayer
                && IsValidTarget(ceremonyTarget)
                && IsValidLarva(ceremonyLarva);
        }

        private bool IsValidTarget(Pawn pawn)
        {
            return pawn != null
                && !pawn.Destroyed
                && pawn.Spawned
                && !pawn.Dead
                && !pawn.Downed
                && pawn.Map == parent?.Map
                && IsWithinRange(pawn)
                && JaffaPrimtaUtility.IsEligibleForPrimtaImplantation(pawn);
        }

        private bool IsValidLarva(Thing larva)
        {
            return larva != null
                && !larva.Destroyed
                && larva.Spawned
                && larva.stackCount > 0
                && larva.def == GR_DefOf.SG1_PrimtaLarva
                && larva.Map == parent?.Map
                && IsWithinRange(larva);
        }

        private Pawn FindAnyEligibleNearbyTarget()
        {
            Map map = parent?.Map;

            if (map?.mapPawns?.AllPawnsSpawned == null)
            {
                return null;
            }

            IReadOnlyList<Pawn> pawns = map.mapPawns.AllPawnsSpawned;

            for (int index = 0; index < pawns.Count; index++)
            {
                if (IsValidTarget(pawns[index]))
                {
                    return pawns[index];
                }
            }

            return null;
        }

        private Thing FindNearestValidLarva()
        {
            Map map = parent?.Map;

            if (map?.listerThings == null
                || GR_DefOf.SG1_PrimtaLarva == null)
            {
                return null;
            }

            IReadOnlyList<Thing> larvae = map.listerThings.ThingsOfDef(
                GR_DefOf.SG1_PrimtaLarva);

            Thing bestLarva = null;
            float bestDistanceSquared = float.MaxValue;

            for (int index = 0; index < larvae.Count; index++)
            {
                Thing larva = larvae[index];

                if (!IsValidLarva(larva))
                {
                    continue;
                }

                float distanceSquared = DistanceSquared(parent, larva);

                if (distanceSquared < bestDistanceSquared)
                {
                    bestLarva = larva;
                    bestDistanceSquared = distanceSquared;
                }
            }

            return bestLarva;
        }

        private bool IsWithinRange(Thing thing)
        {
            return parent?.Map != null
                && thing?.Map == parent.Map
                && DistanceSquared(parent, thing)
                    <= Props.ceremonyRange * Props.ceremonyRange;
        }

        private void CancelCeremonyManually()
        {
            CancelCeremony(
                "GR_JaffaPrimtaCeremony_CancelledManual",
                logAsWarning: false);
        }

        private void CancelCeremony(
            string translationKey,
            bool logAsWarning)
        {
            Pawn previousTarget = ceremonyTarget;
            Thing previousLarva = ceremonyLarva;
            bool hadCeremony = CeremonyInProgress
                || previousTarget != null
                || previousLarva != null;

            ClearCeremony();

            if (!hadCeremony)
            {
                return;
            }

            string message = $"Cancelled formal Jaffa Prim'ta ceremony for "
                + $"{JaffaPrimtaUtility.PawnDebugLabel(previousTarget)} "
                + $"near basin {ThingDebugLabel(parent)} "
                + $"using larva {ThingDebugLabel(previousLarva)}.";

            if (logAsWarning)
            {
                GR_Log.Warning(message);
            }
            else
            {
                GR_Log.Message(message);
            }

            Messages.Message(
                translationKey.Translate(),
                parent,
                MessageTypeDefOf.RejectInput,
                historical: false);
        }

        private void ClearCeremony()
        {
            ceremonyTarget = null;
            ceremonyLarva = null;
            ceremonyTicksRemaining = 0;
            ceremonyTicksTotal = 0;
        }

        private static float DistanceSquared(
            Thing first,
            Thing second)
        {
            int deltaX = first.Position.x - second.Position.x;
            int deltaZ = first.Position.z - second.Position.z;

            return deltaX * deltaX + deltaZ * deltaZ;
        }

        private static string ThingDebugLabel(Thing thing)
        {
            if (thing == null)
            {
                return "<null thing>";
            }

            return $"{thing.LabelCap} ({thing.ThingID})";
        }
    }
}
