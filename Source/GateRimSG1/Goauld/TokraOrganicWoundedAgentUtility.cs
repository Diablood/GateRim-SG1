using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI.Group;

namespace GateRimSG1.Goauld
{
    internal static class TokraOrganicWoundedAgentUtility
    {
        private const string SymbioteShockDefName
            = "SG1_TokraWoundedAgentSymbioteShock";
        private const string PostShockRecoveryDefName
            = "SG1_TokraWoundedAgentPostShockRecovery";
        private const float MinimumMovingCapacity = 0.50f;
        private const float MinimumConsciousnessCapacity = 0.50f;
        private const float MinimumSummaryHealth = 0.55f;
        private const float MaximumBleedRate = 0.001f;
        private const float SeriousIllnessChance = 0.35f;
        private const float SeriousIllnessSeverity = 0.35f;

        public static bool TrySpawnPatient(
            Map map,
            int careDurationTicks,
            out Pawn patient)
        {
            patient = null;

            if (map == null
                || GR_DefOf.SG1_TokraVoluntaryHost == null)
            {
                return false;
            }

            Faction tokraFaction
                = TokraFactionUtility.GetOrCreatePersistentFaction(
                    "organic wounded-agent care");

            if (tokraFaction == null)
            {
                return false;
            }

            IntVec3 entryCell;

            if (!TryFindEntryCell(map, out entryCell))
            {
                return false;
            }

            Pawn generatedPatient = PawnGenerator.GeneratePawn(
                GR_DefOf.SG1_TokraVoluntaryHost,
                tokraFaction);

            if (generatedPatient == null)
            {
                return false;
            }

            GenSpawn.Spawn(generatedPatient, entryCell, map);
            HealthUtility.DamageUntilDowned(
                generatedPatient,
                allowBleedingWounds: true);
            TryAddSeriousIllness(generatedPatient);
            EnsureSymbioteShock(generatedPatient);

            if (generatedPatient.Dead || generatedPatient.Destroyed)
            {
                if (!generatedPatient.Destroyed)
                {
                    generatedPatient.Destroy(DestroyMode.Vanish);
                }

                return false;
            }

            IntVec3 careSpot;

            if (!RCellFinder.TryFindRandomSpotJustOutsideColony(
                    generatedPatient,
                    out careSpot))
            {
                careSpot = entryCell;
            }

            LordMaker.MakeNewLord(
                tokraFaction,
                new LordJob_TokraWoundedAgentCare(
                    tokraFaction,
                    careSpot,
                    careDurationTicks),
                map,
                new List<Pawn> { generatedPatient });

            patient = generatedPatient;

            GR_Log.Message(
                "Spawned wounded Tok'ra agent "
                + $"{PawnDebugLabel(patient)} at {entryCell}; "
                + $"downed={patient.Downed}; symbioteShock="
                + $"{HasSymbioteShock(patient)}; care window "
                + $"{careDurationTicks} ticks.");

            return true;
        }

        public static bool HasReceivedInitialCare(
            Pawn patient,
            int initialTendedConditionCount)
        {
            if (patient == null
                || patient.Destroyed
                || patient.Dead
                || patient.health?.hediffSet == null)
            {
                return false;
            }

            Building_Bed bed = patient.CurrentBed();

            return bed != null
                && bed.Medical
                && bed.Faction == Faction.OfPlayer
                && IsSymbioteShockTended(patient);
        }

        private static bool IsSymbioteShockTended(Pawn patient)
        {
            HediffDef shockDef = GetSymbioteShockDef();

            if (shockDef == null
                || patient?.health?.hediffSet == null)
            {
                return false;
            }

            HediffWithComps shock = patient.health.hediffSet
                .GetFirstHediffOfDef(shockDef) as HediffWithComps;
            HediffComp_TendDuration tendComp
                = shock?.TryGetComp<HediffComp_TendDuration>();

            return tendComp != null && tendComp.IsTended;
        }

        public static int CountTendedConditions(Pawn patient)
        {
            if (patient?.health?.hediffSet == null)
            {
                return 0;
            }

            int count = 0;
            List<Hediff> hediffs = patient.health.hediffSet.hediffs;

            for (int index = 0; index < hediffs.Count; index++)
            {
                HediffWithComps hediff = hediffs[index] as HediffWithComps;
                HediffComp_TendDuration tendComp
                    = hediff?.TryGetComp<HediffComp_TendDuration>();

                if (tendComp != null && tendComp.IsTended)
                {
                    count++;
                }
            }

            return count;
        }

        public static void EnsureSymbioteShock(Pawn patient)
        {
            if (patient == null
                || patient.Destroyed
                || patient.Dead
                || patient.health?.hediffSet == null)
            {
                return;
            }

            HediffDef shockDef = GetSymbioteShockDef();

            if (shockDef == null
                || patient.health.hediffSet.HasHediff(shockDef))
            {
                return;
            }

            patient.health.AddHediff(shockDef);
        }

        public static bool HasSymbioteShock(Pawn patient)
        {
            HediffDef shockDef = GetSymbioteShockDef();

            return shockDef != null
                && patient?.health?.hediffSet != null
                && patient.health.hediffSet.HasHediff(shockDef);
        }

        public static bool HasPostShockRecovery(Pawn patient)
        {
            HediffDef recoveryDef = GetPostShockRecoveryDef();

            return recoveryDef != null
                && patient?.health?.hediffSet != null
                && patient.health.hediffSet.HasHediff(recoveryDef);
        }

        public static void RemoveSymbioteShock(Pawn patient)
        {
            if (patient == null
                || patient.Destroyed
                || patient.health?.hediffSet == null)
            {
                return;
            }

            HediffDef shockDef = GetSymbioteShockDef();

            if (shockDef == null)
            {
                return;
            }

            Hediff shock = patient.health.hediffSet
                .GetFirstHediffOfDef(shockDef);

            if (shock != null)
            {
                patient.health.RemoveHediff(shock);
            }
        }

        public static void BeginPostShockRecovery(Pawn patient)
        {
            RemoveSymbioteShock(patient);
            EnsurePostShockRecovery(patient);
        }

        public static void EnsurePostShockRecovery(Pawn patient)
        {
            if (patient == null
                || patient.Destroyed
                || patient.Dead
                || patient.health?.hediffSet == null)
            {
                return;
            }

            HediffDef recoveryDef = GetPostShockRecoveryDef();

            if (recoveryDef == null
                || patient.health.hediffSet.HasHediff(recoveryDef))
            {
                return;
            }

            patient.health.AddHediff(recoveryDef);
        }

        public static void ClearOperationHealthConditions(Pawn patient)
        {
            if (patient == null
                || patient.Destroyed
                || patient.health?.hediffSet == null)
            {
                return;
            }

            RemoveSymbioteShock(patient);

            HediffDef recoveryDef = GetPostShockRecoveryDef();

            if (recoveryDef == null)
            {
                return;
            }

            Hediff recovery = patient.health.hediffSet
                .GetFirstHediffOfDef(recoveryDef);

            if (recovery != null)
            {
                patient.health.RemoveHediff(recovery);
            }
        }

        public static bool IsFitForDeparture(Pawn patient)
        {
            if (patient == null
                || patient.Destroyed
                || patient.Dead
                || !patient.Spawned
                || patient.Map == null
                || patient.Downed
                || patient.IsPrisonerOfColony
                || HasSymbioteShock(patient)
                || patient.health?.capacities == null
                || patient.health.hediffSet == null
                || patient.health.summaryHealth == null)
            {
                return false;
            }

            if (patient.health.capacities.GetLevel(
                    PawnCapacityDefOf.Moving) < MinimumMovingCapacity
                || patient.health.capacities.GetLevel(
                    PawnCapacityDefOf.Consciousness)
                    < MinimumConsciousnessCapacity
                || patient.health.hediffSet.BleedRateTotal > MaximumBleedRate
                || patient.health.summaryHealth.SummaryHealthPercent
                    < MinimumSummaryHealth
                || HealthAIUtility.ShouldSeekMedicalRestUrgent(patient))
            {
                return false;
            }

            return !HasCriticalHealthCondition(patient);
        }

        public static bool TryOrderDeparture(Pawn patient)
        {
            if (patient == null
                || patient.Destroyed
                || patient.Dead
                || !patient.Spawned
                || patient.Map == null)
            {
                return false;
            }

            Lord lord = patient.GetLord();

            if (lord?.LordJob is LordJob_TokraWoundedAgentCare)
            {
                lord.ReceiveMemo(LordJob_TokraWoundedAgentCare.LeaveMemo);
                return true;
            }

            return lord?.LordJob is LordJob_TravelAndExit;
        }

        public static void RemoveLivingPatient(Pawn patient)
        {
            if (patient == null
                || patient.Destroyed
                || patient.Dead)
            {
                return;
            }

            patient.Destroy(DestroyMode.Vanish);
        }

        private static bool HasCriticalHealthCondition(Pawn patient)
        {
            List<Hediff> hediffs = patient.health.hediffSet.hediffs;

            for (int index = 0; index < hediffs.Count; index++)
            {
                Hediff hediff = hediffs[index];

                if (hediff == null || hediff.def == null)
                {
                    continue;
                }

                float lethalSeverity = hediff.def.lethalSeverity;

                if (lethalSeverity > 0f
                    && hediff.Severity >= lethalSeverity * 0.70f)
                {
                    return true;
                }
            }

            return false;
        }

        private static void TryAddSeriousIllness(Pawn patient)
        {
            if (patient?.health == null
                || !Rand.Chance(SeriousIllnessChance))
            {
                return;
            }

            HediffDef fluDef = DefDatabase<HediffDef>.GetNamedSilentFail(
                "Flu");

            if (fluDef == null)
            {
                return;
            }

            Hediff flu = HediffMaker.MakeHediff(fluDef, patient);
            flu.Severity = SeriousIllnessSeverity;
            patient.health.AddHediff(flu);
        }

        private static HediffDef GetSymbioteShockDef()
        {
            return DefDatabase<HediffDef>.GetNamedSilentFail(
                SymbioteShockDefName);
        }

        private static HediffDef GetPostShockRecoveryDef()
        {
            return DefDatabase<HediffDef>.GetNamedSilentFail(
                PostShockRecoveryDefName);
        }

        private static bool TryFindEntryCell(Map map, out IntVec3 entryCell)
        {
            return CellFinder.TryFindRandomEdgeCellWith(
                cell => map.reachability.CanReachColony(cell)
                    && !cell.Fogged(map),
                map,
                CellFinder.EdgeRoadChance_Neutral,
                out entryCell);
        }

        private static string PawnDebugLabel(Pawn pawn)
        {
            return pawn == null
                ? "<null>"
                : $"{pawn.LabelShort} ({pawn.ThingID})";
        }
    }
}
