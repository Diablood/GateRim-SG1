using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Persistent free-symbiote state, manual implantation tool and first
    /// autonomous pursuit prototype.
    ///
    /// A free symbiote periodically searches for the nearest reachable
    /// compatible humanoid, starts a dedicated pursuit job, then implants on
    /// contact. Manual implantation remains available for regression tests.
    /// </summary>
    public class Comp_GoauldForcedImplantation : ThingComp
    {
        private GoauldSymbioteData symbioteData;
        private bool consumedByImplantation;
        private bool autonomousHuntingEnabled = true;
        private int nextAutonomousScanTick;

        private CompProperties_GoauldForcedImplantation Props
            => (CompProperties_GoauldForcedImplantation)props;

        private Pawn SymbiotePawn => parent as Pawn;

        public void InitializeWithTransferredData(GoauldSymbioteData transferredData)
        {
            if (transferredData == null)
            {
                GR_Log.Error("Tried to initialize a free Goa'uld symbiote with null data.");
                return;
            }

            symbioteData = transferredData;
            consumedByImplantation = false;
            symbioteData.EnsureIdentity(CurrentGameTick());
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);

            EnsureDataInitialized();

            if (!respawningAfterLoad)
            {
                autonomousHuntingEnabled = Props.autonomousHuntingEnabled;
                nextAutonomousScanTick = CurrentGameTick() + Props.autonomousScanIntervalTicks;

                GR_Log.Message(
                    $"Created free Goa'uld symbiote {symbioteData.SymbioteId} "
                    + $"as pawn {PawnDebugLabel(SymbiotePawn)}.");
            }
        }

        public override void PostExposeData()
        {
            base.PostExposeData();

            Scribe_Deep.Look(ref symbioteData, "freeGoauldSymbioteData");
            Scribe_Values.Look(ref consumedByImplantation, "consumedByImplantation", false);
            Scribe_Values.Look(ref autonomousHuntingEnabled, "autonomousHuntingEnabled", true);
            Scribe_Values.Look(ref nextAutonomousScanTick, "nextAutonomousScanTick", 0);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                EnsureDataInitialized();

                GR_Log.Message(
                    $"Loaded free Goa'uld symbiote {symbioteData.SymbioteId} "
                    + $"as pawn {PawnDebugLabel(SymbiotePawn)}.");
            }
        }

        public override void CompTick()
        {
            base.CompTick();

            if (!autonomousHuntingEnabled)
            {
                return;
            }

            Pawn symbiote = SymbiotePawn;
            int currentTick = CurrentGameTick();

            if (symbiote == null
                || symbiote.Destroyed
                || !symbiote.Spawned
                || symbiote.Dead
                || symbiote.Downed
                || currentTick < nextAutonomousScanTick)
            {
                return;
            }

            nextAutonomousScanTick = currentTick
                + Math.Max(30, Props.autonomousScanIntervalTicks);

            TryRunAutonomousBehavior(symbiote, currentTick);
        }

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            foreach (Gizmo gizmo in base.CompGetGizmosExtra())
            {
                yield return gizmo;
            }

            Pawn symbiote = SymbiotePawn;
            if (symbiote == null || !symbiote.Spawned || symbiote.Destroyed)
            {
                yield break;
            }

            yield return new Command_Action
            {
                defaultLabel = "GR_ForcedImplantation_CommandLabel".Translate(),
                defaultDesc = "GR_ForcedImplantation_CommandDescription".Translate(),
                icon = ContentFinder<Texture2D>.Get("UI/Commands/SG1_ForcedImplantation"),
                action = TryImplantAdjacentHostManually
            };

            yield return new Command_Action
            {
                defaultLabel = "GR_RitualImplantation_CommandLabel".Translate(),
                defaultDesc = "GR_RitualImplantation_CommandDescription".Translate(),
                icon = ContentFinder<Texture2D>.Get("UI/Commands/SG1_RitualImplantation"),
                action = BeginRitualTargeting
            };

            yield return new Command_Toggle
            {
                defaultLabel = "GR_AutonomousHunt_CommandLabel".Translate(),
                defaultDesc = "GR_AutonomousHunt_CommandDescription".Translate(),
                icon = ContentFinder<Texture2D>.Get("UI/Commands/SG1_AutonomousHunt"),
                isActive = () => autonomousHuntingEnabled,
                toggleAction = ToggleAutonomousHunting
            };
        }

        public override string CompInspectStringExtra()
        {
            EnsureDataInitialized();

            int cooldownTicks = GetAutonomousCooldownTicksRemaining(
                CurrentGameTick());

            string autonomousLabel = autonomousHuntingEnabled
                ? "GR_AutonomousHunt_Enabled".Translate().ToString()
                : "GR_AutonomousHunt_Disabled".Translate().ToString();

            return "GR_FreeGoauldSymbioteDataSummary".Translate(
                symbioteData.SymbioteId,
                symbioteData.GetOriginLabel(),
                autonomousLabel,
                cooldownTicks).ToString();
        }

        public override void PostDestroy(DestroyMode mode, Map previousMap)
        {
            base.PostDestroy(mode, previousMap);

            if (symbioteData == null)
            {
                return;
            }

            if (consumedByImplantation)
            {
                GR_Log.Message(
                    $"Consumed free Goa'uld symbiote {symbioteData.SymbioteId} "
                    + "during implantation.");
            }
            else
            {
                GR_Log.Message(
                    $"Destroyed free Goa'uld symbiote {symbioteData.SymbioteId} "
                    + $"with mode {mode}.");
            }
        }

        public bool TryImplantHost(Pawn target, bool autonomous)
        {
            return TryImplantHost(
                target,
                autonomous
                    ? GoauldImplantationMode.AutonomousContact
                    : GoauldImplantationMode.ManualContact);
        }

        private bool TryImplantHost(
            Pawn target,
            GoauldImplantationMode mode)
        {
            Pawn symbiote = SymbiotePawn;

            if (symbiote == null || !symbiote.Spawned || symbiote.Destroyed)
            {
                GR_Log.Warning(
                    "Goa'uld implantation requested from an unavailable "
                    + "free symbiote pawn.");

                return false;
            }

            bool requiresContact = mode != GoauldImplantationMode.RitualControlled;

            if (!IsCompatibleHost(target)
                || (requiresContact && !IsAdjacentOrSameCell(symbiote, target))
                || (!requiresContact
                    && !IsValidRitualTarget(symbiote, target)))
            {
                return false;
            }

            EnsureDataInitialized();

            Hediff implantation = HediffMaker.MakeHediff(
                GR_DefOf.SG1_GoauldRecentImplantation,
                target);

            HediffComp_GoauldSymbiote implantationComp
                = FindPersistentSymbioteComp(implantation);

            if (implantationComp == null)
            {
                GR_Log.Error(
                    "Unable to start Goa'uld implantation: "
                    + "SG1_GoauldRecentImplantation is missing "
                    + "HediffComp_GoauldSymbiote.");

                Messages.Message(
                    "GR_ForcedImplantation_InternalError".Translate(),
                    symbiote,
                    MessageTypeDefOf.RejectInput,
                    historical: false);

                return false;
            }

            string transferredId = symbioteData.SymbioteId;

            implantationComp.InitializeWithTransferredData(symbioteData);
            target.health.AddHediff(implantation);

            consumedByImplantation = true;

            GR_Log.Message(
                $"{GetImplantationModeLogLabel(mode)} implantation "
                + $"transferred Goa'uld symbiote {transferredId} "
                + $"from free pawn {PawnDebugLabel(symbiote)} "
                + $"into host {PawnDebugLabel(target)}.");

            Messages.Message(
                GetImplantationSuccessTranslationKey(mode).Translate(
                    target.LabelShortCap,
                    transferredId),
                target,
                MessageTypeDefOf.NegativeEvent,
                historical: true);

            symbiote.Destroy(DestroyMode.Vanish);
            return true;
        }

        public bool IsCompatibleHost(Pawn candidate)
        {
            if (candidate == null
                || candidate == SymbiotePawn
                || candidate.Destroyed
                || !candidate.Spawned
                || candidate.Dead
                || candidate.health == null
                || !candidate.RaceProps.Humanlike)
            {
                return false;
            }

            if (candidate.ageTracker != null
                && candidate.ageTracker.AgeBiologicalYearsFloat
                    < Props.minimumTargetAgeYears)
            {
                return false;
            }

            if (HasHediff(candidate, GR_DefOf.SG1_GoauldRecentImplantation)
                || HasHediff(candidate, GR_DefOf.SG1_GoauldHostSymbiote))
            {
                return false;
            }

            return true;
        }

        private void TryImplantAdjacentHostManually()
        {
            Pawn symbiote = SymbiotePawn;
            Pawn target = FindFirstCompatibleAdjacentHost(symbiote);

            if (target == null)
            {
                Messages.Message(
                    "GR_ForcedImplantation_NoAdjacentTarget".Translate(),
                    symbiote,
                    MessageTypeDefOf.RejectInput,
                    historical: false);

                return;
            }

            TryImplantHost(target, autonomous: false);
        }

        private void BeginRitualTargeting()
        {
            Pawn symbiote = SymbiotePawn;

            if (symbiote == null
                || !symbiote.Spawned
                || symbiote.Destroyed)
            {
                GR_Log.Warning(
                    "Ritual implantation targeting requested from an "
                    + "unavailable free symbiote pawn.");

                return;
            }

            if (FindClosestCompatibleHost(
                    symbiote,
                    Props.ritualImplantationRange) == null)
            {
                Messages.Message(
                    "GR_RitualImplantation_NoNearbyTarget".Translate(),
                    symbiote,
                    MessageTypeDefOf.RejectInput,
                    historical: false);

                return;
            }

            if (symbiote.jobs?.curJob?.def
                == GR_DefOf.SG1_GoauldAutonomousImplant)
            {
                symbiote.jobs.EndCurrentJob(
                    JobCondition.InterruptForced);
            }

            nextAutonomousScanTick = CurrentGameTick()
                + Math.Max(120, Props.autonomousScanIntervalTicks);

            TargetingParameters targetingParameters = new TargetingParameters
            {
                canTargetPawns = true,
                canTargetLocations = false,
                validator = delegate(TargetInfo targetInfo)
                {
                    Pawn candidate = targetInfo.Thing as Pawn;
                    return IsValidRitualTarget(symbiote, candidate);
                }
            };

            Find.Targeter.BeginTargeting(
                targetingParameters,
                delegate(LocalTargetInfo targetInfo)
                {
                    Pawn selectedTarget = targetInfo.Thing as Pawn;

                    if (!IsValidRitualTarget(
                            symbiote,
                            selectedTarget)
                        || !TryImplantHost(
                            selectedTarget,
                            GoauldImplantationMode.RitualControlled))
                    {
                        Messages.Message(
                            "GR_RitualImplantation_InvalidTarget".Translate(),
                            symbiote,
                            MessageTypeDefOf.RejectInput,
                            historical: false);
                    }
                });
        }

        private void ToggleAutonomousHunting()
        {
            autonomousHuntingEnabled = !autonomousHuntingEnabled;

            Pawn symbiote = SymbiotePawn;
            if (!autonomousHuntingEnabled
                && symbiote?.jobs?.curJob?.def
                    == GR_DefOf.SG1_GoauldAutonomousImplant)
            {
                symbiote.jobs.EndCurrentJob(JobCondition.InterruptForced);
            }

            GR_Log.Message(
                $"Autonomous hunting for Goa'uld symbiote "
                + $"{symbioteData?.SymbioteId ?? "<uninitialized>"} "
                + $"set to {autonomousHuntingEnabled}.");
        }

        private void TryRunAutonomousBehavior(Pawn symbiote, int currentTick)
        {
            if (GetAutonomousCooldownTicksRemaining(currentTick) > 0)
            {
                return;
            }

            Pawn adjacentTarget = FindFirstCompatibleAdjacentHost(symbiote);
            if (adjacentTarget != null)
            {
                TryImplantHost(adjacentTarget, autonomous: true);
                return;
            }

            if (symbiote.jobs?.curJob?.def
                == GR_DefOf.SG1_GoauldAutonomousImplant)
            {
                return;
            }

            Pawn target = FindClosestCompatibleHost(symbiote);
            if (target == null)
            {
                return;
            }

            Job job = new Job(
                GR_DefOf.SG1_GoauldAutonomousImplant,
                target)
            {
                locomotionUrgency = LocomotionUrgency.Jog
            };

            GR_Log.Message(
                $"Free Goa'uld symbiote {symbioteData.SymbioteId} "
                + $"started autonomous pursuit of {PawnDebugLabel(target)}.");

            symbiote.jobs.StartJob(
                job,
                JobCondition.InterruptForced);
        }

        private Pawn FindClosestCompatibleHost(Pawn symbiote)
        {
            return FindClosestCompatibleHost(
                symbiote,
                Props.autonomousSearchRadius);
        }

        private Pawn FindClosestCompatibleHost(
            Pawn symbiote,
            float searchRadius)
        {
            Map map = symbiote?.Map;
            if (map?.mapPawns?.AllPawnsSpawned == null)
            {
                return null;
            }

            float radiusSquared = searchRadius * searchRadius;

            Pawn bestTarget = null;
            float bestDistanceSquared = float.MaxValue;

            IReadOnlyList<Pawn> candidates = map.mapPawns.AllPawnsSpawned;

            for (int index = 0; index < candidates.Count; index++)
            {
                Pawn candidate = candidates[index];

                if (!IsCompatibleHost(candidate)
                    || !symbiote.CanReach(
                        candidate,
                        PathEndMode.Touch,
                        Danger.Deadly))
                {
                    continue;
                }

                int deltaX = candidate.Position.x - symbiote.Position.x;
                int deltaZ = candidate.Position.z - symbiote.Position.z;
                float distanceSquared = deltaX * deltaX + deltaZ * deltaZ;

                if (distanceSquared <= radiusSquared
                    && distanceSquared < bestDistanceSquared)
                {
                    bestTarget = candidate;
                    bestDistanceSquared = distanceSquared;
                }
            }

            return bestTarget;
        }

        private Pawn FindFirstCompatibleAdjacentHost(Pawn symbiote)
        {
            if (symbiote?.Map == null)
            {
                return null;
            }

            Map map = symbiote.Map;
            IntVec3 origin = symbiote.Position;

            for (int deltaX = -1; deltaX <= 1; deltaX++)
            {
                for (int deltaZ = -1; deltaZ <= 1; deltaZ++)
                {
                    if (deltaX == 0 && deltaZ == 0)
                    {
                        continue;
                    }

                    IntVec3 cell = origin + new IntVec3(deltaX, 0, deltaZ);
                    if (!cell.InBounds(map))
                    {
                        continue;
                    }

                    List<Thing> things = cell.GetThingList(map);
                    for (int index = 0; index < things.Count; index++)
                    {
                        Pawn candidate = things[index] as Pawn;
                        if (IsCompatibleHost(candidate))
                        {
                            return candidate;
                        }
                    }
                }
            }

            return null;
        }

        private int GetAutonomousCooldownTicksRemaining(int currentTick)
        {
            if (symbioteData == null || symbioteData.LastDetachTick < 0)
            {
                return 0;
            }

            int elapsed = currentTick - symbioteData.LastDetachTick;
            return Math.Max(
                0,
                Props.autonomousCooldownAfterExtractionTicks - elapsed);
        }

        private bool IsValidRitualTarget(
            Pawn symbiote,
            Pawn candidate)
        {
            return IsCompatibleHost(candidate)
                && IsWithinRadius(
                    symbiote,
                    candidate,
                    Props.ritualImplantationRange)
                && symbiote.CanReach(
                    candidate,
                    PathEndMode.Touch,
                    Danger.Deadly);
        }

        private static bool IsWithinRadius(
            Pawn first,
            Pawn second,
            float radius)
        {
            if (first?.Map == null
                || second?.Map == null
                || first.Map != second.Map)
            {
                return false;
            }

            int deltaX = first.Position.x - second.Position.x;
            int deltaZ = first.Position.z - second.Position.z;

            return deltaX * deltaX + deltaZ * deltaZ <= radius * radius;
        }

        private static string GetImplantationModeLogLabel(
            GoauldImplantationMode mode)
        {
            switch (mode)
            {
                case GoauldImplantationMode.AutonomousContact:
                    return "Autonomous";

                case GoauldImplantationMode.RitualControlled:
                    return "Ritual";

                default:
                    return "Manual";
            }
        }

        private static string GetImplantationSuccessTranslationKey(
            GoauldImplantationMode mode)
        {
            switch (mode)
            {
                case GoauldImplantationMode.AutonomousContact:
                    return "GR_AutonomousImplantation_Success";

                case GoauldImplantationMode.RitualControlled:
                    return "GR_RitualImplantation_Success";

                default:
                    return "GR_ForcedImplantation_Success";
            }
        }

        private static bool IsAdjacentOrSameCell(Pawn first, Pawn second)
        {
            int deltaX = Math.Abs(first.Position.x - second.Position.x);
            int deltaZ = Math.Abs(first.Position.z - second.Position.z);

            return deltaX <= 1 && deltaZ <= 1;
        }

        private static bool HasHediff(Pawn pawn, HediffDef def)
        {
            List<Hediff> hediffs = pawn.health.hediffSet.hediffs;

            for (int index = 0; index < hediffs.Count; index++)
            {
                if (hediffs[index].def == def)
                {
                    return true;
                }
            }

            return false;
        }

        private static HediffComp_GoauldSymbiote FindPersistentSymbioteComp(
            Hediff hediff)
        {
            HediffWithComps withComps = hediff as HediffWithComps;
            return withComps?.GetComp<HediffComp_GoauldSymbiote>();
        }

        private void EnsureDataInitialized()
        {
            if (symbioteData == null)
            {
                symbioteData = GoauldSymbioteData.CreateFree(CurrentGameTick());
            }
            else
            {
                symbioteData.EnsureIdentity(CurrentGameTick());
            }
        }

        private static int CurrentGameTick()
        {
            return Find.TickManager?.TicksGame ?? 0;
        }

        private static string PawnDebugLabel(Pawn pawn)
        {
            if (pawn == null)
            {
                return "<null pawn>";
            }

            return $"{pawn.LabelShort} ({pawn.ThingID})";
        }
    }

    internal enum GoauldImplantationMode
    {
        ManualContact,
        AutonomousContact,
        RitualControlled
    }
}
