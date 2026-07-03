using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public enum GoauldJaffaRaidDoctrine
    {
        Direct,
        Abduction,
        Destruction
    }

    public struct GoauldJaffaRaidDoctrineWeights
    {
        public float direct;
        public float abduction;
        public float destruction;

        public float Total
        {
            get
            {
                return direct + abduction + destruction;
            }
        }

        public float Percentage(float weight)
        {
            return Total > 0f
                ? weight / Total * 100f
                : 0f;
        }
    }

    /// <summary>
    /// Low-frequency natural Goa'uld Jaffa raid.
    ///
    /// One storyteller incident selects a doctrine from vanilla threat points
    /// and readable colony context. Keeping one IncidentDef preserves the
    /// original frequency and refire delay instead of giving every doctrine
    /// its own independent incident roll.
    /// </summary>
    public class IncidentWorker_GoauldJaffaNaturalRaid
        : IncidentWorker_GoauldJaffaControlledRaid
    {
        public const float AbductionMinimumPoints = 800f;
        public const int AbductionMinimumColonists = 2;
        public const float DestructionMinimumPoints = 1800f;
        public const float DestructionMinimumBuildingWealth = 10000f;

        private const float DirectWeight = 2f;
        private const float AbductionWeight = 1f;
        private const float DestructionWeight = 1f;

        private GoauldJaffaRaidDoctrine? forcedDebugDoctrine;

        protected override string ControlledRaidPurpose
        {
            get
            {
                return "natural Goa'uld Jaffa raids";
            }
        }

        protected override string RaidLogContext
        {
            get
            {
                return "natural Goa'uld Jaffa raid";
            }
        }

        protected override float ResolveMissingRaidPoints(Map map)
        {
            return map == null
                ? 0f
                : StorytellerUtility.DefaultThreatPointsNow(map);
        }

        protected override void ConfigureRaidParms(IncidentParms parms)
        {
            Map map = parms.target as Map;
            GoauldJaffaRaidDoctrine doctrine = forcedDebugDoctrine
                ?? SelectDoctrine(
                    CalculateDoctrineWeights(map, parms.points));

            parms.canSteal = false;

            switch (doctrine)
            {
                case GoauldJaffaRaidDoctrine.Abduction:
                    parms.raidStrategy =
                        GR_DefOf.SG1_GoauldJaffaAbductionAssault;
                    parms.canKidnap = true;
                    parms.canTimeoutOrFlee = false;
                    break;

                case GoauldJaffaRaidDoctrine.Destruction:
                    parms.raidStrategy =
                        GR_DefOf.SG1_GoauldJaffaDestructionAssault;
                    parms.canKidnap = false;
                    parms.canTimeoutOrFlee = false;
                    break;

                default:
                    parms.raidStrategy = RaidStrategyDefOf.ImmediateAttack;
                    parms.canKidnap = false;
                    parms.canTimeoutOrFlee = true;
                    break;
            }
        }

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            return base.CanFireNowSub(parms)
                && Find.FactionManager?.FirstFactionOfDef(
                    GR_DefOf.SG1_GoauldSystemLordPrototype) != null;
        }

        public static GoauldJaffaRaidDoctrineWeights
            CalculateDoctrineWeights(Map map, float points)
        {
            GoauldJaffaRaidDoctrineWeights weights =
                new GoauldJaffaRaidDoctrineWeights
                {
                    direct = DirectWeight
                };

            if (map == null)
            {
                return weights;
            }

            if (points >= AbductionMinimumPoints
                && map.mapPawns.FreeColonistsSpawnedCount
                    >= AbductionMinimumColonists)
            {
                weights.abduction = AbductionWeight;
            }

            if (points >= DestructionMinimumPoints
                && map.wealthWatcher.WealthBuildings
                    >= DestructionMinimumBuildingWealth)
            {
                weights.destruction = DestructionWeight;
            }

            return weights;
        }

        public bool TryExecuteForcedDebugDoctrine(
            IncidentParms parms,
            GoauldJaffaRaidDoctrine doctrine)
        {
            // Debug access validates the real worker without requiring the
            // test map to satisfy normal storyteller eligibility.
            forcedDebugDoctrine = doctrine;

            try
            {
                return TryExecute(parms);
            }
            finally
            {
                forcedDebugDoctrine = null;
            }
        }

        private static GoauldJaffaRaidDoctrine SelectDoctrine(
            GoauldJaffaRaidDoctrineWeights weights)
        {
            float roll = Rand.Value * weights.Total;

            if (roll < weights.destruction)
            {
                return GoauldJaffaRaidDoctrine.Destruction;
            }

            roll -= weights.destruction;

            if (roll < weights.abduction)
            {
                return GoauldJaffaRaidDoctrine.Abduction;
            }

            return GoauldJaffaRaidDoctrine.Direct;
        }
    }
}
