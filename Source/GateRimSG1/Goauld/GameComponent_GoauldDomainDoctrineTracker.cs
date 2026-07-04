using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public sealed class GoauldDomainDoctrineState : IExposable
    {
        public Faction domainFaction;
        public GoauldDomainDoctrineProfileDef profile;

        public void ExposeData()
        {
            Scribe_References.Look(ref domainFaction, "domainFaction");
            Scribe_Defs.Look(ref profile, "profile");
        }
    }

    /// <summary>
    /// Assigns and persists one strategic doctrine profile per Goa'uld domain.
    ///
    /// The key is the faction instance rather than its current leader, so a
    /// leader replacement never rerolls the domain's long-term preference.
    /// </summary>
    public sealed class GameComponent_GoauldDomainDoctrineTracker
        : GameComponent
    {
        private const int ReconcileIntervalTicks = 2500;

        private List<GoauldDomainDoctrineState> states =
            new List<GoauldDomainDoctrineState>();
        private int nextReconcileTick;

        public GameComponent_GoauldDomainDoctrineTracker(Game game)
        {
        }

        public static GameComponent_GoauldDomainDoctrineTracker Current
            => Verse.Current.Game
                ?.GetComponent<GameComponent_GoauldDomainDoctrineTracker>();

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Collections.Look(
                ref states,
                "goauldDomainDoctrineStates",
                LookMode.Deep);
            Scribe_Values.Look(
                ref nextReconcileTick,
                "goauldDomainDoctrineNextReconcileTick",
                0);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                NormalizeStates();
            }
        }

        public override void FinalizeInit()
        {
            base.FinalizeInit();
            ReconcileAllDomains();
        }

        public override void StartedNewGame()
        {
            base.StartedNewGame();
            ReconcileAllDomains();
        }

        public override void LoadedGame()
        {
            base.LoadedGame();
            ReconcileAllDomains();
        }

        public override void GameComponentTick()
        {
            base.GameComponentTick();

            int currentTick = Find.TickManager?.TicksGame ?? 0;

            if (currentTick < nextReconcileTick)
            {
                return;
            }

            nextReconcileTick =
                currentTick + ReconcileIntervalTicks;
            ReconcileAllDomains();
        }

        public void ReconcileAllDomains()
        {
            NormalizeStates();

            List<Faction> factions =
                GoauldSystemLordFactionUtility.GetAllFactions();

            for (int index = 0; index < factions.Count; index++)
            {
                GetOrAssignProfile(factions[index]);
            }
        }

        public bool TryGetProfile(
            Faction faction,
            out GoauldDomainDoctrineProfileDef profile)
        {
            profile = null;

            if (!GoauldSystemLordFactionUtility
                .IsSystemLordFaction(faction))
            {
                return false;
            }

            profile = GetOrAssignProfile(faction);
            return profile != null;
        }

        public GoauldDomainDoctrineProfileDef GetOrAssignProfile(
            Faction faction)
        {
            if (!GoauldSystemLordFactionUtility
                .IsSystemLordFaction(faction))
            {
                return null;
            }

            GoauldDomainDoctrineState state = states
                .FirstOrDefault(item => item.domainFaction == faction);

            if (state != null
                && GoauldDomainDoctrineProfileUtility
                    .IsAssignmentProfile(state.profile))
            {
                return state.profile;
            }

            GoauldDomainDoctrineProfileDef profile =
                SelectStableInitialProfile(faction);

            if (profile == null)
            {
                return null;
            }

            if (state == null)
            {
                state = new GoauldDomainDoctrineState
                {
                    domainFaction = faction
                };
                states.Add(state);
            }

            state.profile = profile;

            GR_Log.Message(
                "Assigned Goa'uld domain doctrine "
                + $"{profile.defName} to faction "
                + $"{faction.Name} ({faction.loadID}).");

            return profile;
        }

        public bool SetProfile(
            Faction faction,
            GoauldDomainDoctrineProfileDef profile)
        {
            if (!GoauldSystemLordFactionUtility
                    .IsSystemLordFaction(faction)
                || !GoauldDomainDoctrineProfileUtility
                    .IsAssignmentProfile(profile))
            {
                return false;
            }

            GoauldDomainDoctrineState state = states
                .FirstOrDefault(item => item.domainFaction == faction);

            if (state == null)
            {
                state = new GoauldDomainDoctrineState
                {
                    domainFaction = faction
                };
                states.Add(state);
            }

            state.profile = profile;
            return true;
        }

        public List<GoauldDomainDoctrineState> Snapshot()
        {
            ReconcileAllDomains();

            return states
                .Where(state =>
                    state?.domainFaction != null
                    && GoauldSystemLordFactionUtility
                        .IsSystemLordFaction(state.domainFaction)
                    && GoauldDomainDoctrineProfileUtility
                        .IsAssignmentProfile(state.profile))
                .OrderBy(state => state.domainFaction.Name)
                .ToList();
        }

        private void NormalizeStates()
        {
            states = states
                ?.Where(state =>
                    state?.domainFaction != null
                    && GoauldSystemLordFactionUtility
                        .IsSystemLordFaction(state.domainFaction))
                .GroupBy(state => state.domainFaction)
                .Select(group => group.Last())
                .ToList()
                ?? new List<GoauldDomainDoctrineState>();
        }

        private static GoauldDomainDoctrineProfileDef
            SelectStableInitialProfile(Faction faction)
        {
            List<GoauldDomainDoctrineProfileDef> profiles =
                GoauldDomainDoctrineProfileUtility
                    .GetAssignmentProfiles();

            if (profiles.Count == 0)
            {
                GR_Log.Error(
                    "No Goa'uld domain doctrine profile Def is available.");
                return null;
            }

            uint mixedKey = unchecked((uint)faction.randomKey)
                ^ unchecked((uint)faction.loadID * 2654435761u);
            int index = (int)(mixedKey % (uint)profiles.Count);

            return profiles[index];
        }
    }
}
