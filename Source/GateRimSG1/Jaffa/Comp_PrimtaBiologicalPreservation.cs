using RimWorld;
using UnityEngine;
using Verse;

namespace GateRimSG1.Jaffa
{
    /// <summary>
    /// Extends vanilla rotting for fragile Prim'ta biological resources.
    ///
    /// A powered dedicated preservation basin disables ambient rot progression
    /// without repairing deterioration accumulated before storage.
    ///
    /// Outside an active basin, prolonged deep freezing is no longer perfectly
    /// safe. Exposure accumulates below the configured temperature threshold,
    /// remains temporarily tolerated, then adds a slow biological-damage rate.
    /// A powered basin or safer ambient storage progressively reduces exposure.
    /// </summary>
    public class Comp_PrimtaBiologicalPreservation : CompRottable
    {
        private const int RareTickInterval = 250;

        private float deepFreezeExposureTicks;

        private CompProperties_PrimtaBiologicalPreservation PropsPreservation
            => (CompProperties_PrimtaBiologicalPreservation)props;

        public bool HasDeepFreezeInspectStatus
        {
            get
            {
                if (IsProtectedByActiveBasin)
                {
                    return false;
                }

                return IsDeepFrozen || deepFreezeExposureTicks > 0f;
            }
        }

        private bool IsProtectedByActiveBasin
            => PrimtaPreservationUtility.TryGetActiveBasin(parent, out _);

        private bool IsDeepFrozen
            => parent.Spawned
                && parent.AmbientTemperature
                    <= PropsPreservation.deepFreezeTemperature;

        private bool IsCriticallyDeepFrozen
            => parent.Spawned
                && parent.AmbientTemperature
                    <= PropsPreservation.criticalDeepFreezeTemperature;

        public override void PostExposeData()
        {
            base.PostExposeData();

            Scribe_Values.Look(
                ref deepFreezeExposureTicks,
                "deepFreezeExposureTicks",
                0f);
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);

            RefreshDisabledState();
        }

        public override void PostDeSpawn(
            Map map,
            DestroyMode mode = DestroyMode.Vanish)
        {
            disabled = false;

            base.PostDeSpawn(map, mode);
        }

        public override void CompTickInterval(int delta)
        {
            RefreshDisabledState();
            TickDeepFreezeExposure(delta);

            base.CompTickInterval(delta);
        }

        public override void CompTickRare()
        {
            RefreshDisabledState();
            TickDeepFreezeExposure(RareTickInterval);

            base.CompTickRare();
        }

        public override void PreAbsorbStack(Thing otherStack, int count)
        {
            float weight = (float)count / (parent.stackCount + count);
            Comp_PrimtaBiologicalPreservation other =
                otherStack.TryGetComp<Comp_PrimtaBiologicalPreservation>();

            base.PreAbsorbStack(otherStack, count);

            if (other != null)
            {
                deepFreezeExposureTicks = Mathf.Lerp(
                    deepFreezeExposureTicks,
                    other.deepFreezeExposureTicks,
                    weight);
            }
        }

        public override void PostSplitOff(Thing piece)
        {
            base.PostSplitOff(piece);

            Comp_PrimtaBiologicalPreservation split =
                piece.TryGetComp<Comp_PrimtaBiologicalPreservation>();

            if (split != null)
            {
                split.deepFreezeExposureTicks = deepFreezeExposureTicks;
            }
        }

        public override string CompInspectStringExtra()
        {
            RefreshDisabledState();

            if (disabled
                && PrimtaPreservationUtility.TryGetActiveBasin(
                    parent,
                    out Comp_PrimtaPreservationBasin basin))
            {
                if (parent.TryGetComp<Comp_PrimtaLarvaTemperature>() != null)
                {
                    return null;
                }

                return "GR_PrimtaImmature_PreservationBasinStatus".Translate(
                    basin.IdealTemperature.ToStringTemperature("F0"));
            }

            if (HasDeepFreezeInspectStatus)
            {
                if (parent.TryGetComp<Comp_PrimtaLarvaTemperature>() != null)
                {
                    return null;
                }

                return GetDeepFreezeInspectString();
            }

            return base.CompInspectStringExtra();
        }

        public string GetDeepFreezeInspectString()
        {
            if (!HasDeepFreezeInspectStatus)
            {
                return null;
            }

            float temperature = parent.AmbientTemperature;

            if (IsDeepFrozen)
            {
                if (deepFreezeExposureTicks
                    <= PropsPreservation.deepFreezeGraceTicks)
                {
                    int remainingTicks = Mathf.CeilToInt(
                        PropsPreservation.deepFreezeGraceTicks
                        - deepFreezeExposureTicks);

                    return "GR_PrimtaDeepFreeze_Tolerated".Translate(
                        temperature.ToStringTemperature("F0"),
                        remainingTicks.ToStringTicksToPeriod());
                }

                float multiplier = CurrentDeepFreezeRotRateMultiplier();

                if (IsCriticallyDeepFrozen)
                {
                    return "GR_PrimtaDeepFreeze_Critical".Translate(
                        temperature.ToStringTemperature("F0"),
                        multiplier.ToString("0.##"));
                }

                return "GR_PrimtaDeepFreeze_Damaging".Translate(
                    temperature.ToStringTemperature("F0"),
                    multiplier.ToString("0.##"));
            }

            int recoveryTicks = Mathf.CeilToInt(
                deepFreezeExposureTicks
                / PropsPreservation.deepFreezeRecoveryRate);

            return "GR_PrimtaDeepFreeze_Recovery".Translate(
                recoveryTicks.ToStringTicksToPeriod());
        }

        private void TickDeepFreezeExposure(int delta)
        {
            if (!parent.Spawned || delta <= 0)
            {
                return;
            }

            if (IsProtectedByActiveBasin)
            {
                RecoverDeepFreezeExposure(delta);
                return;
            }

            if (!IsDeepFrozen)
            {
                RecoverDeepFreezeExposure(delta);
                return;
            }

            deepFreezeExposureTicks += delta;

            if (deepFreezeExposureTicks
                <= PropsPreservation.deepFreezeGraceTicks)
            {
                return;
            }

            RotProgress += CurrentDeepFreezeRotRateMultiplier() * delta;
        }

        private void RecoverDeepFreezeExposure(int delta)
        {
            if (deepFreezeExposureTicks <= 0f)
            {
                return;
            }

            deepFreezeExposureTicks = Mathf.Max(
                0f,
                deepFreezeExposureTicks
                    - PropsPreservation.deepFreezeRecoveryRate * delta);
        }

        private float CurrentDeepFreezeRotRateMultiplier()
        {
            if (IsCriticallyDeepFrozen)
            {
                return PropsPreservation
                    .criticalDeepFreezeRotRateMultiplier;
            }

            return PropsPreservation.deepFreezeRotRateMultiplier;
        }

        private void RefreshDisabledState()
        {
            disabled = IsProtectedByActiveBasin;
        }
    }
}
