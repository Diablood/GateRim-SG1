using System;
using System.Collections.Generic;
using GateRimSG1.Missions;
using RimWorld;
using Verse;
using Verse.AI.Group;

namespace GateRimSG1.Goauld
{
    internal static class TokraOrganicWoundedAgentUtility
    {
        public static bool TrySpawnPatient(
            Map map,
            TokraOrganicOperationDefinition definition,
            float scaledThreatPoints,
            int careDurationTicks,
            out Pawn patient)
        {
            patient = null;
            GateRimMissionPawnCareDef profile = definition?.PawnCare;
            PawnKindDef pawnKind = GetPawnKindDef(profile);

            if (map == null || profile == null || pawnKind == null)
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
                pawnKind,
                tokraFaction);

            if (generatedPatient == null)
            {
                return false;
            }

            GenSpawn.Spawn(generatedPatient, entryCell, map);
            HealthUtility.DamageUntilDowned(
                generatedPatient,
                allowBleedingWounds: true);
            float illnessChance;
            float illnessSeverity;
            bool illnessApplied = TryAddSeriousIllness(
                generatedPatient,
                definition,
                scaledThreatPoints,
                out illnessChance,
                out illnessSeverity);
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
                + $"{careDurationTicks} ticks; scaled threat "
                + $"{scaledThreatPoints:0}; optional illness chance "
                + $"{illnessChance:0.000}; severity "
                + $"{illnessSeverity:0.000}; applied={illnessApplied}.");

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
            GateRimMissionPawnCareDef profile = GetPawnCareProfile();

            if (profile == null
                || patient == null
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
                    PawnCapacityDefOf.Moving)
                    < profile.minimumMovingCapacity
                || patient.health.capacities.GetLevel(
                    PawnCapacityDefOf.Consciousness)
                    < profile.minimumConsciousnessCapacity
                || patient.health.hediffSet.BleedRateTotal
                    > profile.maximumBleedRate
                || patient.health.summaryHealth.SummaryHealthPercent
                    < profile.minimumSummaryHealth
                || HealthAIUtility.ShouldSeekMedicalRestUrgent(patient))
            {
                return false;
            }

            return !HasCriticalHealthCondition(patient, profile);
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

        private static bool HasCriticalHealthCondition(
            Pawn patient,
            GateRimMissionPawnCareDef profile)
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
                    && hediff.Severity >= lethalSeverity
                        * profile.criticalHediffSeverityFraction)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool TryAddSeriousIllness(
            Pawn patient,
            TokraOrganicOperationDefinition definition,
            float scaledThreatPoints,
            out float chance,
            out float severity)
        {
            chance = 0f;
            severity = 0f;
            GateRimMissionPawnCareDef profile = definition?.PawnCare;

            if (patient?.health == null || profile == null)
            {
                return false;
            }

            float difficultyFactor = GetDifficultyFactor(
                definition.MissionDef?.difficulty,
                scaledThreatPoints);
            chance = Lerp(
                profile.optionalIllnessChanceMinimum,
                profile.optionalIllnessChanceMaximum,
                difficultyFactor);
            severity = Lerp(
                profile.optionalIllnessSeverityMinimum,
                profile.optionalIllnessSeverityMaximum,
                difficultyFactor);

            if (!Rand.Chance(chance))
            {
                return false;
            }

            HediffDef illnessDef = DefDatabase<HediffDef>.GetNamedSilentFail(
                profile.optionalIllnessHediffDefName);

            if (illnessDef == null)
            {
                return false;
            }

            Hediff illness = HediffMaker.MakeHediff(illnessDef, patient);
            illness.Severity = severity;
            patient.health.AddHediff(illness);
            return true;
        }

        private static float GetDifficultyFactor(
            GateRimMissionDifficultyDef difficulty,
            float scaledThreatPoints)
        {
            if (difficulty == null
                || difficulty.maximumPoints <= difficulty.minimumPoints)
            {
                return 0.5f;
            }

            float normalized = (scaledThreatPoints - difficulty.minimumPoints)
                / (difficulty.maximumPoints - difficulty.minimumPoints);
            return Math.Max(0f, Math.Min(1f, normalized));
        }

        private static float Lerp(float minimum, float maximum, float factor)
        {
            return minimum + ((maximum - minimum) * factor);
        }

        private static GateRimMissionPawnCareDef GetPawnCareProfile()
        {
            return TokraOrganicOperationFramework.GetDefinition(
                TokraOrganicOperationArchetype.WoundedAgentCare)?.PawnCare;
        }

        private static PawnKindDef GetPawnKindDef(
            GateRimMissionPawnCareDef profile)
        {
            return string.IsNullOrWhiteSpace(profile?.pawnKindDefName)
                ? null
                : DefDatabase<PawnKindDef>.GetNamedSilentFail(
                    profile.pawnKindDefName);
        }

        private static HediffDef GetSymbioteShockDef()
        {
            GateRimMissionPawnCareDef profile = GetPawnCareProfile();
            return string.IsNullOrWhiteSpace(profile?.initialHediffDefName)
                ? null
                : DefDatabase<HediffDef>.GetNamedSilentFail(
                    profile.initialHediffDefName);
        }

        private static HediffDef GetPostShockRecoveryDef()
        {
            GateRimMissionPawnCareDef profile = GetPawnCareProfile();
            return string.IsNullOrWhiteSpace(profile?.recoveryHediffDefName)
                ? null
                : DefDatabase<HediffDef>.GetNamedSilentFail(
                    profile.recoveryHediffDefName);
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
