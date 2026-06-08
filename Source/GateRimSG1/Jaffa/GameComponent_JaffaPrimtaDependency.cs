using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Jaffa
{
    /// <summary>
    /// First puberty-dependency prototype for Jaffa without an implanted Prim'ta.
    ///
    /// Every in-game hour, spawned pawns on active maps are checked. Compatible
    /// Jaffa aged 12 biological years or older receive a progressive immune
    /// deficiency while they remain without SG1_JaffaPrimta.
    ///
    /// A later milestone can extend the same rule to caravans and introduce
    /// tretonin substitution.
    /// </summary>
    public class GameComponent_JaffaPrimtaDependency : GameComponent
    {
        private const int ScanIntervalTicks = 2500;
        private const float InitialDependencySeverity = 0.01f;

        public GameComponent_JaffaPrimtaDependency(Game game)
        {
        }

        public override void GameComponentTick()
        {
            if (Find.TickManager.TicksGame % ScanIntervalTicks != 0)
            {
                return;
            }

            for (int mapIndex = 0; mapIndex < Find.Maps.Count; mapIndex++)
            {
                UpdateMap(Find.Maps[mapIndex]);
            }
        }

        private static void UpdateMap(Map map)
        {
            IReadOnlyList<Pawn> pawns = map?.mapPawns?.AllPawnsSpawned;

            if (pawns == null)
            {
                return;
            }

            for (int pawnIndex = 0; pawnIndex < pawns.Count; pawnIndex++)
            {
                UpdatePawn(pawns[pawnIndex]);
            }
        }

        private static void UpdatePawn(Pawn pawn)
        {
            if (pawn == null || pawn.Dead || pawn.health == null)
            {
                return;
            }

            Hediff dependency = JaffaPrimtaUtility.GetPrimtaDependency(pawn);

            if (!JaffaPrimtaUtility.ShouldHavePrimtaDependency(pawn))
            {
                if (dependency != null)
                {
                    JaffaPrimtaUtility.RemovePrimtaDependency(
                        pawn,
                        showMessage: true);
                }

                return;
            }

            if (dependency == null)
            {
                dependency = HediffMaker.MakeHediff(
                    GR_DefOf.SG1_JaffaPrimtaDependency,
                    pawn);

                dependency.Severity = InitialDependencySeverity;
                pawn.health.AddHediff(dependency);

                GR_Log.Warning(
                    $"Started Jaffa Prim'ta dependency for "
                    + $"{JaffaPrimtaUtility.PawnDebugLabel(pawn)} "
                    + $"at biological age {pawn.ageTracker.AgeBiologicalYears}.");

                Messages.Message(
                    "GR_JaffaPrimtaDependency_Started".Translate(
                        pawn.LabelShortCap,
                        JaffaPrimtaUtility.MinimumPrimtaDependencyBiologicalAge),
                    pawn,
                    MessageTypeDefOf.NegativeEvent,
                    historical: true);

                return;
            }

            float addedSeverity = JaffaPrimtaUtility.PrimtaDependencySeverityPerDay
                * ScanIntervalTicks
                / GenDate.TicksPerDay;

            dependency.Severity = Math.Min(
                JaffaPrimtaUtility.MaximumPrimtaDependencySeverity,
                dependency.Severity + addedSeverity);
        }
    }
}
