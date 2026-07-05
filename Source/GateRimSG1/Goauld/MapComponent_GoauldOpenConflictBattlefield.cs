using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;
using Verse.AI.Group;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Maintains the arrival, rally, combat and withdrawal phases of one
    /// local battle between two exact Goa'uld domain detachments.
    /// </summary>
    public sealed class MapComponent_GoauldOpenConflictBattlefield
        : MapComponent
    {
        private const int WithdrawalRefreshIntervalTicks = 600;
        private const float RallyReadyFraction = 0.70f;

        private bool initialized;
        private bool resolved;
        private bool assaultStarted;
        private bool assaultMessageSent;
        private bool withdrawalOrdered;
        private Faction firstDomain;
        private Faction secondDomain;
        private List<Pawn> firstDetachment = new List<Pawn>();
        private List<Pawn> secondDetachment = new List<Pawn>();
        private List<Pawn> firstPlayerProvokers = new List<Pawn>();
        private List<Pawn> secondPlayerProvokers = new List<Pawn>();
        private int initialFirstCombatantCount;
        private int initialSecondCombatantCount;
        private int firstLastProvocationTick = -1;
        private int secondLastProvocationTick = -1;
        private IntVec3 firstProvocationOrigin = IntVec3.Invalid;
        private IntVec3 secondProvocationOrigin = IntVec3.Invalid;
        private int firstWithdrawalRetaliationStartTick = -1;
        private int secondWithdrawalRetaliationStartTick = -1;
        private Dictionary<Pawn, float> firstInjurySnapshots
            = new Dictionary<Pawn, float>();
        private Dictionary<Pawn, float> secondInjurySnapshots
            = new Dictionary<Pawn, float>();
        private IntVec3 firstEntry = IntVec3.Invalid;
        private IntVec3 secondEntry = IntVec3.Invalid;
        private IntVec3 firstAnchor = IntVec3.Invalid;
        private IntVec3 secondAnchor = IntVec3.Invalid;
        private float vanillaThreatPoints;
        private float detachmentPoints;
        private int startTick;
        private int rallyReadyTick;
        private int rallyDeadlineTick;
        private int assaultStartTick;
        private int withdrawalTick;
        private int forcedExitTick;
        private int nextCombatCheckTick;
        private int nextWithdrawalRefreshTick;

        public MapComponent_GoauldOpenConflictBattlefield(Map map)
            : base(map)
        {
        }

        public bool Active => initialized && !resolved;

        public string MapLabel
            => map?.Parent?.LabelCap ?? "map " + (map?.uniqueID ?? -1);

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(
                ref initialized,
                "goauldOpenConflictBattlefieldInitialized",
                false);
            Scribe_Values.Look(
                ref resolved,
                "goauldOpenConflictBattlefieldResolved",
                false);
            Scribe_Values.Look(
                ref assaultStarted,
                "goauldOpenConflictBattlefieldAssaultStarted",
                false);
            Scribe_Values.Look(
                ref assaultMessageSent,
                "goauldOpenConflictBattlefieldAssaultMessageSent",
                false);
            Scribe_Values.Look(
                ref withdrawalOrdered,
                "goauldOpenConflictBattlefieldWithdrawalOrdered",
                false);
            Scribe_References.Look(
                ref firstDomain,
                "goauldOpenConflictBattlefieldFirstDomain");
            Scribe_References.Look(
                ref secondDomain,
                "goauldOpenConflictBattlefieldSecondDomain");
            Scribe_Collections.Look(
                ref firstDetachment,
                "goauldOpenConflictBattlefieldFirstDetachment",
                LookMode.Reference);
            Scribe_Collections.Look(
                ref secondDetachment,
                "goauldOpenConflictBattlefieldSecondDetachment",
                LookMode.Reference);
            Scribe_Collections.Look(
                ref firstPlayerProvokers,
                "goauldOpenConflictBattlefieldFirstPlayerProvokers",
                LookMode.Reference);
            Scribe_Collections.Look(
                ref secondPlayerProvokers,
                "goauldOpenConflictBattlefieldSecondPlayerProvokers",
                LookMode.Reference);
            Scribe_Values.Look(
                ref initialFirstCombatantCount,
                "goauldOpenConflictBattlefieldInitialFirstCombatantCount",
                0);
            Scribe_Values.Look(
                ref initialSecondCombatantCount,
                "goauldOpenConflictBattlefieldInitialSecondCombatantCount",
                0);
            Scribe_Values.Look(
                ref firstLastProvocationTick,
                "goauldOpenConflictBattlefieldFirstLastProvocationTick",
                -1);
            Scribe_Values.Look(
                ref secondLastProvocationTick,
                "goauldOpenConflictBattlefieldSecondLastProvocationTick",
                -1);
            Scribe_Values.Look(
                ref firstProvocationOrigin,
                "goauldOpenConflictBattlefieldFirstProvocationOrigin",
                IntVec3.Invalid);
            Scribe_Values.Look(
                ref secondProvocationOrigin,
                "goauldOpenConflictBattlefieldSecondProvocationOrigin",
                IntVec3.Invalid);
            Scribe_Values.Look(
                ref firstWithdrawalRetaliationStartTick,
                "goauldOpenConflictBattlefieldFirstWithdrawalRetaliationStartTick",
                -1);
            Scribe_Values.Look(
                ref secondWithdrawalRetaliationStartTick,
                "goauldOpenConflictBattlefieldSecondWithdrawalRetaliationStartTick",
                -1);
            Scribe_Values.Look(
                ref firstEntry,
                "goauldOpenConflictBattlefieldFirstEntry",
                IntVec3.Invalid);
            Scribe_Values.Look(
                ref secondEntry,
                "goauldOpenConflictBattlefieldSecondEntry",
                IntVec3.Invalid);
            Scribe_Values.Look(
                ref firstAnchor,
                "goauldOpenConflictBattlefieldFirstAnchor",
                IntVec3.Invalid);
            Scribe_Values.Look(
                ref secondAnchor,
                "goauldOpenConflictBattlefieldSecondAnchor",
                IntVec3.Invalid);
            Scribe_Values.Look(
                ref vanillaThreatPoints,
                "goauldOpenConflictBattlefieldVanillaThreatPoints",
                0f);
            Scribe_Values.Look(
                ref detachmentPoints,
                "goauldOpenConflictBattlefieldDetachmentPoints",
                0f);
            Scribe_Values.Look(
                ref startTick,
                "goauldOpenConflictBattlefieldStartTick",
                0);
            Scribe_Values.Look(
                ref rallyReadyTick,
                "goauldOpenConflictBattlefieldRallyReadyTick",
                0);
            Scribe_Values.Look(
                ref rallyDeadlineTick,
                "goauldOpenConflictBattlefieldRallyDeadlineTick",
                0);
            Scribe_Values.Look(
                ref assaultStartTick,
                "goauldOpenConflictBattlefieldAssaultStartTick",
                0);
            Scribe_Values.Look(
                ref withdrawalTick,
                "goauldOpenConflictBattlefieldWithdrawalTick",
                0);
            Scribe_Values.Look(
                ref forcedExitTick,
                "goauldOpenConflictBattlefieldForcedExitTick",
                0);
            Scribe_Values.Look(
                ref nextCombatCheckTick,
                "goauldOpenConflictBattlefieldNextCombatCheckTick",
                0);
            Scribe_Values.Look(
                ref nextWithdrawalRefreshTick,
                "goauldOpenConflictBattlefieldNextWithdrawalRefreshTick",
                0);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                firstDetachment = firstDetachment ?? new List<Pawn>();
                secondDetachment = secondDetachment ?? new List<Pawn>();
                firstPlayerProvokers = firstPlayerProvokers
                    ?? new List<Pawn>();
                secondPlayerProvokers = secondPlayerProvokers
                    ?? new List<Pawn>();
                initialFirstCombatantCount = initialFirstCombatantCount > 0
                    ? initialFirstCombatantCount
                    : firstDetachment.Count(pawn => pawn != null);
                initialSecondCombatantCount = initialSecondCombatantCount > 0
                    ? initialSecondCombatantCount
                    : secondDetachment.Count(pawn => pawn != null);
                firstLastProvocationTick = Math.Max(
                    -1,
                    firstLastProvocationTick);
                secondLastProvocationTick = Math.Max(
                    -1,
                    secondLastProvocationTick);
                firstWithdrawalRetaliationStartTick = Math.Max(
                    -1,
                    firstWithdrawalRetaliationStartTick);
                secondWithdrawalRetaliationStartTick = Math.Max(
                    -1,
                    secondWithdrawalRetaliationStartTick);

                int currentTick = Find.TickManager?.TicksGame ?? 0;
                MigrateLegacyProvocationState(
                    firstDetachment,
                    firstPlayerProvokers,
                    firstAnchor,
                    ref firstLastProvocationTick,
                    ref firstProvocationOrigin,
                    ref firstWithdrawalRetaliationStartTick,
                    currentTick);
                MigrateLegacyProvocationState(
                    secondDetachment,
                    secondPlayerProvokers,
                    secondAnchor,
                    ref secondLastProvocationTick,
                    ref secondProvocationOrigin,
                    ref secondWithdrawalRetaliationStartTick,
                    currentTick);
                RebuildInjurySnapshots();

                // Development saves from r1/r2 had no rally-phase fields.
                // Preserve their already-running battle instead of restaging it.
                if (initialized
                    && !resolved
                    && rallyDeadlineTick <= 0)
                {
                    assaultStarted = true;
                    assaultMessageSent = true;
                    assaultStartTick = Math.Max(startTick, assaultStartTick);
                    rallyDeadlineTick = startTick;
                }
            }
        }

        public override void MapComponentTick()
        {
            base.MapComponentTick();

            if (!Active)
            {
                return;
            }

            int currentTick = Find.TickManager?.TicksGame ?? 0;

            if (currentTick < nextCombatCheckTick)
            {
                return;
            }

            nextCombatCheckTick = currentTick
                + GoauldOpenConflictBattlefieldUtility
                    .CombatCheckIntervalTicks;

            DetectPlayerProvocation(currentTick);
            DetectDamageProvocation(currentTick);

            if (withdrawalOrdered)
            {
                TickWithdrawal(currentTick);
                return;
            }

            if (!assaultStarted)
            {
                TickRally(currentTick);
                return;
            }

            TickAssault(currentTick);
        }

        public void Initialize(
            Faction firstFaction,
            Faction secondFaction,
            List<Pawn> firstPawns,
            List<Pawn> secondPawns,
            IntVec3 firstEntryCell,
            IntVec3 secondEntryCell,
            IntVec3 firstRallyCell,
            IntVec3 secondRallyCell,
            float originalThreatPoints,
            float pointsPerDetachment)
        {
            firstDomain = firstFaction;
            secondDomain = secondFaction;
            firstDetachment = firstPawns ?? new List<Pawn>();
            secondDetachment = secondPawns ?? new List<Pawn>();
            firstPlayerProvokers = new List<Pawn>();
            secondPlayerProvokers = new List<Pawn>();
            initialFirstCombatantCount = firstDetachment.Count;
            initialSecondCombatantCount = secondDetachment.Count;
            firstLastProvocationTick = -1;
            secondLastProvocationTick = -1;
            firstProvocationOrigin = IntVec3.Invalid;
            secondProvocationOrigin = IntVec3.Invalid;
            firstWithdrawalRetaliationStartTick = -1;
            secondWithdrawalRetaliationStartTick = -1;
            firstInjurySnapshots = new Dictionary<Pawn, float>();
            secondInjurySnapshots = new Dictionary<Pawn, float>();
            firstEntry = firstEntryCell;
            secondEntry = secondEntryCell;
            firstAnchor = firstRallyCell;
            secondAnchor = secondRallyCell;
            vanillaThreatPoints = originalThreatPoints;
            detachmentPoints = pointsPerDetachment;
            startTick = Find.TickManager?.TicksGame ?? 0;
            rallyReadyTick = 0;
            rallyDeadlineTick = startTick
                + GoauldOpenConflictBattlefieldUtility.RallyTimeoutTicks;
            assaultStartTick = 0;
            withdrawalTick = 0;
            forcedExitTick = 0;
            nextCombatCheckTick = startTick;
            nextWithdrawalRefreshTick = 0;
            assaultStarted = false;
            assaultMessageSent = false;
            withdrawalOrdered = false;
            resolved = false;
            initialized = true;
            RebuildInjurySnapshots();
        }

        public void OrderWithdrawalDebug()
        {
            if (Active && !withdrawalOrdered)
            {
                BeginWithdrawal(Find.TickManager?.TicksGame ?? 0);
            }
        }

        public string BuildDebugSummary()
        {
            int currentTick = Find.TickManager?.TicksGame ?? 0;
            int firstActive = GetActiveCombatants(firstDetachment).Count;
            int secondActive = GetActiveCombatants(secondDetachment).Count;

            return "active pair: "
                + (firstDomain?.Name ?? "<missing>")
                + " <-> "
                + (secondDomain?.Name ?? "<missing>")
                + "\nmap: " + MapLabel
                + "\nphase: " + CurrentPhaseLabel()
                + "\nvanilla threat points: "
                + vanillaThreatPoints.ToString("0")
                + "\npoints per detachment: "
                + detachmentPoints.ToString("0")
                + "\nfirst entry / rally: "
                + firstEntry + " / " + firstAnchor
                + "\nsecond entry / rally: "
                + secondEntry + " / " + secondAnchor
                + "\nfirst active / initial: "
                + firstActive + "/" + initialFirstCombatantCount
                + " (break at "
                + ResolveBreakThreshold(initialFirstCombatantCount)
                + ")"
                + "\nsecond active / initial: "
                + secondActive + "/" + initialSecondCombatantCount
                + " (break at "
                + ResolveBreakThreshold(initialSecondCombatantCount)
                + ")"
                + "\nfirst player provokers: "
                + GetActiveFirstPlayerProvokers(currentTick).Count
                + "; last=" + firstLastProvocationTick
                + "; origin=" + firstProvocationOrigin
                + "\nsecond player provokers: "
                + GetActiveSecondPlayerProvokers(currentTick).Count
                + "; last=" + secondLastProvocationTick
                + "; origin=" + secondProvocationOrigin
                + "\nrally ready tick: "
                + (rallyReadyTick > 0
                    ? rallyReadyTick.ToString()
                    : "<not ready>")
                + "\nrally deadline tick: " + rallyDeadlineTick
                + "\nassault start tick: "
                + (assaultStartTick > 0
                    ? assaultStartTick.ToString()
                    : "<not started>")
                + "\nwithdrawal ordered: "
                + (withdrawalOrdered ? "yes" : "no")
                + "\nfirst withdrawal retaliation start: "
                + firstWithdrawalRetaliationStartTick
                + "\nsecond withdrawal retaliation start: "
                + secondWithdrawalRetaliationStartTick
                + "\nstart tick: " + startTick
                + "\nforced exit tick: "
                + (forcedExitTick > 0
                    ? forcedExitTick.ToString()
                    : "<not scheduled>");
        }

        private void TickRally(int currentTick)
        {
            List<Pawn> firstActive = GetActiveCombatants(
                firstDetachment);
            List<Pawn> secondActive = GetActiveCombatants(
                secondDetachment);

            if (firstActive.Count == 0 || secondActive.Count == 0)
            {
                BeginWithdrawal(currentTick);
                return;
            }

            List<Pawn> firstProvokers =
                GetActiveFirstPlayerProvokers(currentTick);
            List<Pawn> secondProvokers =
                GetActiveSecondPlayerProvokers(currentTick);

            if (firstProvokers.Count > 0)
            {
                AssignCombatJobs(firstActive, firstProvokers);
            }
            else
            {
                AssignRallyJobs(firstActive, firstAnchor);
            }

            if (secondProvokers.Count > 0)
            {
                AssignCombatJobs(secondActive, secondProvokers);
            }
            else
            {
                AssignRallyJobs(secondActive, secondAnchor);
            }

            bool firstReady = IsDetachmentRallied(
                firstActive,
                firstAnchor);
            bool secondReady = IsDetachmentRallied(
                secondActive,
                secondAnchor);

            if (firstReady && secondReady)
            {
                if (rallyReadyTick <= 0)
                {
                    rallyReadyTick = currentTick;
                }
            }
            else
            {
                rallyReadyTick = 0;
            }

            bool heldLongEnough = rallyReadyTick > 0
                && currentTick - rallyReadyTick
                    >= GoauldOpenConflictBattlefieldUtility
                        .RallyHoldTicks;
            bool timedOut = currentTick >= rallyDeadlineTick;

            if (heldLongEnough || timedOut)
            {
                BeginAssault(currentTick, timedOut);
            }
        }

        private void BeginAssault(int currentTick, bool rallyTimedOut)
        {
            if (assaultStarted || withdrawalOrdered)
            {
                return;
            }

            assaultStarted = true;
            assaultStartTick = currentTick;
            ReleaseFromRallyLords(firstDetachment);
            ReleaseFromRallyLords(secondDetachment);

            List<Pawn> firstActive = GetActiveCombatants(firstDetachment);
            List<Pawn> secondActive = GetActiveCombatants(secondDetachment);

            if (!assaultMessageSent)
            {
                Pawn focusPawn = firstActive.FirstOrDefault()
                    ?? secondActive.FirstOrDefault();
                Messages.Message(
                    "GR_GoauldOpenConflictBattlefield_AssaultBegins"
                        .Translate(
                            firstDomain?.Name ?? "<missing domain>",
                            secondDomain?.Name ?? "<missing domain>"),
                    focusPawn,
                    MessageTypeDefOf.ThreatSmall,
                    historical: true);
                assaultMessageSent = true;
            }

            GR_Log.Message(
                "Started the assault phase of Goa'uld open-conflict "
                + $"battlefield on map {map?.uniqueID ?? -1}; "
                + $"rallyTimedOut={rallyTimedOut}; first="
                + $"{firstActive.Count}; second={secondActive.Count}.");

            TickAssault(currentTick);
        }

        private void TickAssault(int currentTick)
        {
            List<Pawn> firstActive = GetActiveCombatants(
                firstDetachment);
            List<Pawn> secondActive = GetActiveCombatants(
                secondDetachment);

            if (firstActive.Count == 0 || secondActive.Count == 0)
            {
                BeginWithdrawal(currentTick);
                return;
            }

            if (currentTick - startTick
                >= GoauldOpenConflictBattlefieldUtility
                    .MaximumBattlefieldDurationTicks)
            {
                BeginWithdrawal(
                    currentTick,
                    "GR_GoauldOpenConflictBattlefield_TimeLimit",
                    firstDomain,
                    secondDomain);
                return;
            }

            bool firstBreaking = ShouldBreakContact(
                firstActive.Count,
                initialFirstCombatantCount);
            bool secondBreaking = ShouldBreakContact(
                secondActive.Count,
                initialSecondCombatantCount);

            if (firstBreaking != secondBreaking)
            {
                Faction breakingDomain = firstBreaking
                    ? firstDomain
                    : secondDomain;
                Faction holdingDomain = firstBreaking
                    ? secondDomain
                    : firstDomain;

                BeginWithdrawal(
                    currentTick,
                    "GR_GoauldOpenConflictBattlefield_BreaksOff",
                    breakingDomain,
                    holdingDomain);
                return;
            }

            List<Pawn> firstProvokers =
                GetActiveFirstPlayerProvokers(currentTick);
            List<Pawn> secondProvokers =
                GetActiveSecondPlayerProvokers(currentTick);

            AssignCombatJobs(
                firstActive,
                firstProvokers.Count > 0
                    ? firstProvokers
                    : secondActive);
            AssignCombatJobs(
                secondActive,
                secondProvokers.Count > 0
                    ? secondProvokers
                    : firstActive);
        }

        private void DetectPlayerProvocation(int currentTick)
        {
            if (map?.mapPawns == null)
            {
                return;
            }

            List<Pawn> playerPawns = map.mapPawns.FreeColonistsSpawned;

            for (int index = 0; index < playerPawns.Count; index++)
            {
                Pawn playerPawn = playerPawns[index];
                Pawn target = ResolvePlayerAttackTarget(playerPawn);

                if (target == null)
                {
                    continue;
                }

                if (firstDetachment.Contains(target))
                {
                    RegisterFirstPlayerProvoker(
                        playerPawn,
                        target,
                        currentTick);
                }

                if (secondDetachment.Contains(target))
                {
                    RegisterSecondPlayerProvoker(
                        playerPawn,
                        target,
                        currentTick);
                }
            }
        }

        private static Pawn ResolvePlayerAttackTarget(Pawn playerPawn)
        {
            Job job = playerPawn?.jobs?.curJob;

            if (job != null
                && (job.def == JobDefOf.AttackMelee
                    || job.def == JobDefOf.AttackStatic))
            {
                Pawn jobTarget = job.targetA.Thing as Pawn
                    ?? job.targetB.Thing as Pawn;

                if (jobTarget != null)
                {
                    return jobTarget;
                }
            }

            Stance_Warmup warmup = playerPawn?.stances?.curStance
                as Stance_Warmup;
            return warmup?.focusTarg.Thing as Pawn;
        }

        private void DetectDamageProvocation(int currentTick)
        {
            bool firstOpponentDefeated = GetActiveCombatants(
                secondDetachment).Count == 0;
            bool secondOpponentDefeated = GetActiveCombatants(
                firstDetachment).Count == 0;

            DetectDamageProvocationForDetachment(
                firstDetachment,
                firstInjurySnapshots,
                firstOpponentDefeated,
                firstCamp: true,
                currentTick: currentTick);
            DetectDamageProvocationForDetachment(
                secondDetachment,
                secondInjurySnapshots,
                secondOpponentDefeated,
                firstCamp: false,
                currentTick: currentTick);
        }

        private void DetectDamageProvocationForDetachment(
            List<Pawn> detachment,
            Dictionary<Pawn, float> snapshots,
            bool opponentDefeated,
            bool firstCamp,
            int currentTick)
        {
            if (detachment == null || snapshots == null)
            {
                return;
            }

            Pawn damagedPawn = null;
            float greatestIncrease = 0f;

            foreach (Pawn pawn in detachment.Where(item => item != null))
            {
                float currentSeverity = TotalInjurySeverity(pawn);
                float previousSeverity = snapshots.TryGetValue(
                        pawn,
                        out float storedSeverity)
                    ? storedSeverity
                    : currentSeverity;
                float increase = currentSeverity - previousSeverity;

                if (increase > greatestIncrease + 0.001f)
                {
                    damagedPawn = pawn;
                    greatestIncrease = increase;
                }

                snapshots[pawn] = currentSeverity;
            }

            bool alreadyProvoked = firstCamp
                ? GetActiveFirstPlayerProvokers(currentTick).Count > 0
                : GetActiveSecondPlayerProvokers(currentTick).Count > 0;

            if (damagedPawn == null
                || !opponentDefeated
                || alreadyProvoked)
            {
                return;
            }

            Pawn inferredProvoker = FindNearestActivePlayerPawn(
                detachment);

            if (inferredProvoker == null)
            {
                return;
            }

            if (firstCamp)
            {
                RegisterFirstPlayerProvoker(
                    inferredProvoker,
                    damagedPawn,
                    currentTick);
            }
            else
            {
                RegisterSecondPlayerProvoker(
                    inferredProvoker,
                    damagedPawn,
                    currentTick);
            }
        }

        private Pawn FindNearestActivePlayerPawn(List<Pawn> detachment)
        {
            if (map?.mapPawns == null || detachment == null)
            {
                return null;
            }

            List<Pawn> livingTargets = detachment
                .Where(IsValidCombatTarget)
                .ToList();

            if (livingTargets.Count == 0)
            {
                return null;
            }

            List<Pawn> activePlayerPawns = map.mapPawns
                .FreeColonistsSpawned
                .Where(pawn =>
                    pawn != null
                    && !pawn.Dead
                    && !pawn.Downed
                    && pawn.Spawned
                    && pawn.Map == map)
                .ToList();
            List<Pawn> draftedPlayerPawns = activePlayerPawns
                .Where(pawn => pawn.Drafted)
                .ToList();
            List<Pawn> candidates = draftedPlayerPawns.Count > 0
                ? draftedPlayerPawns
                : activePlayerPawns;

            return candidates
                .OrderBy(pawn => livingTargets.Min(target =>
                    HorizontalDistanceSquared(
                        pawn.Position,
                        target.Position)))
                .FirstOrDefault();
        }

        private void MigrateLegacyProvocationState(
            List<Pawn> detachment,
            List<Pawn> provokers,
            IntVec3 fallbackOrigin,
            ref int lastProvocationTick,
            ref IntVec3 provocationOrigin,
            ref int withdrawalRetaliationStartTick,
            int currentTick)
        {
            if (provokers == null
                || provokers.Count == 0
                || lastProvocationTick >= 0
                || provocationOrigin.IsValid)
            {
                return;
            }

            Pawn originPawn = detachment?.FirstOrDefault(pawn =>
                pawn != null
                && !pawn.Destroyed
                && pawn.Spawned
                && pawn.Map == map);

            provocationOrigin = originPawn?.Position
                ?? (fallbackOrigin.IsValid
                    ? fallbackOrigin
                    : map?.Center ?? IntVec3.Invalid);
            lastProvocationTick = currentTick;

            if (withdrawalOrdered
                && withdrawalRetaliationStartTick < 0)
            {
                withdrawalRetaliationStartTick = currentTick;
            }
        }

        private void RebuildInjurySnapshots()
        {
            firstInjurySnapshots = BuildInjurySnapshot(firstDetachment);
            secondInjurySnapshots = BuildInjurySnapshot(secondDetachment);
        }

        private static Dictionary<Pawn, float> BuildInjurySnapshot(
            List<Pawn> pawns)
        {
            Dictionary<Pawn, float> result
                = new Dictionary<Pawn, float>();

            if (pawns == null)
            {
                return result;
            }

            foreach (Pawn pawn in pawns.Where(item => item != null))
            {
                result[pawn] = TotalInjurySeverity(pawn);
            }

            return result;
        }

        private static float TotalInjurySeverity(Pawn pawn)
        {
            return pawn?.health?.hediffSet?.hediffs
                ?.OfType<Hediff_Injury>()
                .Sum(hediff => hediff.Severity)
                ?? 0f;
        }

        private void RegisterFirstPlayerProvoker(
            Pawn playerPawn,
            Pawn attackedPawn,
            int currentTick)
        {
            RegisterPlayerProvoker(
                playerPawn,
                attackedPawn,
                firstPlayerProvokers,
                firstDomain,
                ref firstLastProvocationTick,
                ref firstProvocationOrigin,
                ref firstWithdrawalRetaliationStartTick,
                currentTick);
        }

        private void RegisterSecondPlayerProvoker(
            Pawn playerPawn,
            Pawn attackedPawn,
            int currentTick)
        {
            RegisterPlayerProvoker(
                playerPawn,
                attackedPawn,
                secondPlayerProvokers,
                secondDomain,
                ref secondLastProvocationTick,
                ref secondProvocationOrigin,
                ref secondWithdrawalRetaliationStartTick,
                currentTick);
        }

        private void RegisterPlayerProvoker(
            Pawn playerPawn,
            Pawn attackedPawn,
            List<Pawn> provokers,
            Faction provokedDomain,
            ref int lastProvocationTick,
            ref IntVec3 provocationOrigin,
            ref int withdrawalRetaliationStartTick,
            int currentTick)
        {
            if (playerPawn == null
                || attackedPawn == null
                || provokers == null)
            {
                return;
            }

            if (withdrawalOrdered
                && withdrawalRetaliationStartTick >= 0
                && currentTick - withdrawalRetaliationStartTick
                    >= GoauldOpenConflictBattlefieldUtility
                        .WithdrawalRetaliationMaximumTicks)
            {
                return;
            }

            bool previousWindowExpired = lastProvocationTick < 0
                || currentTick - lastProvocationTick
                    > GoauldOpenConflictBattlefieldUtility
                        .PlayerRetaliationTimeoutTicks
                || !provocationOrigin.IsValid;

            if (previousWindowExpired)
            {
                provokers.Clear();
                provocationOrigin = attackedPawn.Position;
            }
            else if (!IsWithinProvocationBoundary(
                         playerPawn,
                         provocationOrigin))
            {
                // Ongoing fire outside the pursuit boundary does not drag the
                // detachment farther across the map. Keep the episode alive
                // until the player stops attacking, but do not pursue it.
                lastProvocationTick = currentTick;
                return;
            }

            bool newlyRecorded = !provokers.Contains(playerPawn);

            if (newlyRecorded)
            {
                provokers.Add(playerPawn);
            }

            lastProvocationTick = currentTick;

            if (withdrawalOrdered
                && withdrawalRetaliationStartTick < 0)
            {
                withdrawalRetaliationStartTick = currentTick;
            }

            if (!newlyRecorded)
            {
                return;
            }

            GR_Log.Message(
                $"Recorded {playerPawn.LabelShort} ({playerPawn.ThingID}) "
                + "as a player provocateur against Goa'uld battlefield "
                + $"domain {provokedDomain?.Name ?? "<missing>"} "
                + $"({provokedDomain?.loadID ?? -1}) on map "
                + $"{map?.uniqueID ?? -1}; origin={provocationOrigin}; "
                + $"withdrawal={withdrawalOrdered}.");
        }

        private List<Pawn> GetActiveFirstPlayerProvokers(int currentTick)
        {
            return GetActivePlayerProvokers(
                firstPlayerProvokers,
                ref firstLastProvocationTick,
                ref firstProvocationOrigin,
                firstWithdrawalRetaliationStartTick,
                currentTick);
        }

        private List<Pawn> GetActiveSecondPlayerProvokers(int currentTick)
        {
            return GetActivePlayerProvokers(
                secondPlayerProvokers,
                ref secondLastProvocationTick,
                ref secondProvocationOrigin,
                secondWithdrawalRetaliationStartTick,
                currentTick);
        }

        private List<Pawn> GetActivePlayerProvokers(
            List<Pawn> provokers,
            ref int lastProvocationTick,
            ref IntVec3 provocationOrigin,
            int withdrawalRetaliationStartTick,
            int currentTick)
        {
            if (provokers == null)
            {
                return new List<Pawn>();
            }

            bool quietTimeoutReached = lastProvocationTick < 0
                || currentTick - lastProvocationTick
                    > GoauldOpenConflictBattlefieldUtility
                        .PlayerRetaliationTimeoutTicks;
            bool withdrawalLimitReached = withdrawalOrdered
                && withdrawalRetaliationStartTick >= 0
                && currentTick - withdrawalRetaliationStartTick
                    >= GoauldOpenConflictBattlefieldUtility
                        .WithdrawalRetaliationMaximumTicks;

            if (quietTimeoutReached || withdrawalLimitReached)
            {
                provokers.Clear();
                lastProvocationTick = -1;
                provocationOrigin = IntVec3.Invalid;
                return new List<Pawn>();
            }

            IntVec3 boundaryOrigin = provocationOrigin;

            provokers.RemoveAll(pawn =>
                pawn == null
                || pawn.Dead
                || pawn.Destroyed
                || !pawn.Spawned
                || pawn.Map != map
                || pawn.Downed
                || pawn.Faction != Faction.OfPlayer
                || !IsWithinProvocationBoundary(
                    pawn,
                    boundaryOrigin));

            return provokers.ToList();
        }

        private static bool IsWithinProvocationBoundary(
            Pawn playerPawn,
            IntVec3 provocationOrigin)
        {
            if (playerPawn == null || !provocationOrigin.IsValid)
            {
                return false;
            }

            int maximumDistance =
                GoauldOpenConflictBattlefieldUtility
                    .PlayerPursuitMaximumDistance;
            return HorizontalDistanceSquared(
                    playerPawn.Position,
                    provocationOrigin)
                <= maximumDistance * maximumDistance;
        }

        private void AssignRallyJobs(
            List<Pawn> pawns,
            IntVec3 rallyCell)
        {
            if (pawns == null || !rallyCell.IsValid)
            {
                return;
            }

            int radiusSquared =
                GoauldOpenConflictBattlefieldUtility.RallyRadius
                * GoauldOpenConflictBattlefieldUtility.RallyRadius;

            foreach (Pawn pawn in pawns)
            {
                if (HorizontalDistanceSquared(
                        pawn.Position,
                        rallyCell)
                    <= radiusSquared)
                {
                    continue;
                }

                if (HasValidRallyJob(pawn, rallyCell))
                {
                    continue;
                }

                IntVec3 destination = CellFinder.RandomClosewalkCellNear(
                    rallyCell,
                    map,
                    4);
                Job job = JobMaker.MakeJob(
                    JobDefOf.Goto,
                    destination.IsValid ? destination : rallyCell);
                job.locomotionUrgency = LocomotionUrgency.Jog;
                pawn.jobs?.StartJob(
                    job,
                    JobCondition.InterruptForced);
            }
        }

        private bool HasValidRallyJob(Pawn pawn, IntVec3 rallyCell)
        {
            Job job = pawn?.jobs?.curJob;

            if (job?.def != JobDefOf.Goto
                || !job.targetA.Cell.IsValid)
            {
                return false;
            }

            int radius =
                GoauldOpenConflictBattlefieldUtility.RallyRadius + 3;
            return HorizontalDistanceSquared(
                    job.targetA.Cell,
                    rallyCell)
                <= radius * radius;
        }

        private bool IsDetachmentRallied(
            List<Pawn> pawns,
            IntVec3 rallyCell)
        {
            if (pawns == null
                || pawns.Count == 0
                || !rallyCell.IsValid)
            {
                return false;
            }

            int radiusSquared =
                GoauldOpenConflictBattlefieldUtility.RallyRadius
                * GoauldOpenConflictBattlefieldUtility.RallyRadius;
            int required = Math.Max(
                1,
                (int)Math.Ceiling(pawns.Count * RallyReadyFraction));
            int ready = pawns.Count(pawn =>
                HorizontalDistanceSquared(
                    pawn.Position,
                    rallyCell)
                <= radiusSquared);
            return ready >= required;
        }

        private void AssignCombatJobs(
            List<Pawn> attackers,
            List<Pawn> targets)
        {
            if (attackers == null || targets == null || targets.Count == 0)
            {
                return;
            }

            foreach (Pawn attacker in attackers)
            {
                Pawn target = targets
                    .Where(candidate => IsValidCombatTarget(candidate))
                    .OrderBy(candidate => HorizontalDistanceSquared(
                        attacker.Position,
                        candidate.Position))
                    .FirstOrDefault();

                if (target == null)
                {
                    continue;
                }

                Verb verb = attacker.equipment?.PrimaryEq?.PrimaryVerb
                    ?? attacker.TryGetAttackVerb(target);
                bool ranged = verb != null
                    && !verb.verbProps.IsMeleeAttack;

                if (!ranged)
                {
                    if (!HasValidAttackJob(
                            attacker,
                            target,
                            JobDefOf.AttackMelee))
                    {
                        StartCombatJob(
                            attacker,
                            JobMaker.MakeJob(
                                JobDefOf.AttackMelee,
                                target));
                    }

                    continue;
                }

                float range = Math.Max(4f, verb.verbProps.range);

                if (CanFireAtTarget(attacker, target, range))
                {
                    if (!HasValidAttackJob(
                            attacker,
                            target,
                            JobDefOf.AttackStatic))
                    {
                        StartCombatJob(
                            attacker,
                            JobMaker.MakeJob(
                                JobDefOf.AttackStatic,
                                target));
                    }

                    continue;
                }

                if (HasValidPursuitJob(attacker, target, range))
                {
                    continue;
                }

                if (!TryFindApproachCell(
                        attacker,
                        target,
                        range,
                        out IntVec3 destination))
                {
                    destination = CellFinder.RandomClosewalkCellNear(
                        target.Position,
                        map,
                        3);
                }

                Job approachJob = JobMaker.MakeJob(
                    JobDefOf.Goto,
                    destination);
                approachJob.locomotionUrgency = LocomotionUrgency.Jog;
                StartCombatJob(attacker, approachJob);
            }
        }

        private void StartCombatJob(Pawn pawn, Job job)
        {
            if (pawn?.jobs == null || job == null)
            {
                return;
            }

            Lord lord = pawn.GetLord();

            if (lord?.LordJob is LordJob_ExitMapBest
                || lord?.LordJob is LordJob_DefendBase)
            {
                lord.RemovePawn(pawn);
            }

            pawn.jobs.StartJob(
                job,
                JobCondition.InterruptForced);
        }

        private bool HasValidAttackJob(
            Pawn pawn,
            Pawn target,
            JobDef expectedJobDef)
        {
            Job job = pawn?.jobs?.curJob;
            Pawn currentTarget = job?.targetA.Thing as Pawn;

            return job?.def == expectedJobDef
                && currentTarget == target
                && IsValidCombatTarget(currentTarget);
        }

        private bool HasValidPursuitJob(
            Pawn pawn,
            Pawn target,
            float range)
        {
            Job job = pawn?.jobs?.curJob;

            if (job?.def != JobDefOf.Goto
                || !job.targetA.Cell.IsValid
                || pawn.pather == null
                || !pawn.pather.Moving)
            {
                return false;
            }

            float safeRange = Math.Max(3f, range - 1f);
            return HorizontalDistanceSquared(
                    job.targetA.Cell,
                    target.Position)
                <= safeRange * safeRange;
        }

        private bool CanFireAtTarget(
            Pawn attacker,
            Pawn target,
            float range)
        {
            return attacker != null
                && IsValidCombatTarget(target)
                && HorizontalDistanceSquared(
                    attacker.Position,
                    target.Position)
                    <= range * range
                && GenSight.LineOfSight(
                    attacker.Position,
                    target.Position,
                    map);
        }

        private bool TryFindApproachCell(
            Pawn attacker,
            Pawn target,
            float range,
            out IntVec3 destination)
        {
            destination = IntVec3.Invalid;

            if (attacker == null
                || !IsValidCombatTarget(target)
                || map == null)
            {
                return false;
            }

            int deltaX = attacker.Position.x - target.Position.x;
            int deltaZ = attacker.Position.z - target.Position.z;
            double distance = Math.Sqrt(
                deltaX * deltaX + deltaZ * deltaZ);
            int desiredDistance = Math.Max(
                3,
                Math.Min(16, (int)Math.Floor(range * 0.70f)));
            IntVec3 candidate;

            if (distance > 0.001)
            {
                candidate = new IntVec3(
                    target.Position.x
                        + (int)Math.Round(
                            deltaX / distance * desiredDistance),
                    0,
                    target.Position.z
                        + (int)Math.Round(
                            deltaZ / distance * desiredDistance));
            }
            else
            {
                candidate = target.Position;
            }

            int searchRadius = Math.Max(4, desiredDistance / 2);
            return CellFinder.TryFindRandomCellNear(
                candidate,
                map,
                searchRadius,
                cell => IsUsableApproachCell(
                    attacker,
                    target,
                    cell,
                    range),
                out destination);
        }

        private bool IsUsableApproachCell(
            Pawn attacker,
            Pawn target,
            IntVec3 cell,
            float range)
        {
            if (!cell.InBounds(map)
                || !cell.Standable(map)
                || cell.GetFirstBuilding(map) != null
                || (cell.GetFirstPawn(map) != null
                    && cell.GetFirstPawn(map) != attacker))
            {
                return false;
            }

            float safeRange = Math.Max(3f, range - 1f);
            return HorizontalDistanceSquared(cell, target.Position)
                    <= safeRange * safeRange
                && GenSight.LineOfSight(cell, target.Position, map)
                && attacker.CanReach(
                    cell,
                    PathEndMode.OnCell,
                    Danger.Deadly);
        }

        private bool IsValidCombatTarget(Pawn pawn)
        {
            return pawn != null
                && !pawn.Dead
                && !pawn.Destroyed
                && pawn.Spawned
                && pawn.Map == map
                && !pawn.Downed;
        }

        private void BeginWithdrawal(
            int currentTick,
            string messageKey = null,
            Faction primaryDomain = null,
            Faction secondaryDomain = null)
        {
            if (withdrawalOrdered)
            {
                return;
            }

            withdrawalOrdered = true;
            withdrawalTick = currentTick;
            forcedExitTick = currentTick
                + GoauldOpenConflictBattlefieldUtility
                    .WithdrawalGraceTicks;
            nextWithdrawalRefreshTick = currentTick
                + WithdrawalRefreshIntervalTicks;

            if (!string.IsNullOrEmpty(messageKey))
            {
                Pawn focusPawn = GetMobileSurvivors(firstDetachment)
                    .FirstOrDefault()
                    ?? GetMobileSurvivors(secondDetachment)
                        .FirstOrDefault();
                Messages.Message(
                    messageKey.Translate(
                        primaryDomain?.Name ?? "<missing domain>",
                        secondaryDomain?.Name ?? "<missing domain>"),
                    focusPawn,
                    MessageTypeDefOf.NeutralEvent,
                    historical: true);
            }

            OrderFactionWithdrawal(firstDomain, firstDetachment);
            OrderFactionWithdrawal(secondDomain, secondDetachment);

            GR_Log.Message(
                "Ordered withdrawal from Goa'uld open-conflict "
                + $"battlefield on map {map?.uniqueID ?? -1}; first="
                + $"{GetMobileSurvivors(firstDetachment).Count}; second="
                + $"{GetMobileSurvivors(secondDetachment).Count}; "
                + $"forcedExitTick={forcedExitTick}; reason="
                + $"{messageKey ?? "combat resolved"}.");
        }

        private void TickWithdrawal(int currentTick)
        {
            List<Pawn> firstRemaining = GetMobileSurvivors(
                firstDetachment);
            List<Pawn> secondRemaining = GetMobileSurvivors(
                secondDetachment);
            List<Pawn> remaining = firstRemaining
                .Concat(secondRemaining)
                .ToList();

            if (remaining.Count == 0)
            {
                ResolveBattlefield();
                return;
            }

            if (currentTick >= forcedExitTick)
            {
                ForceRemainingMobilePawnsOffMap(remaining);
                ResolveBattlefield();
                return;
            }

            List<Pawn> firstProvokers =
                GetActiveFirstPlayerProvokers(currentTick);
            List<Pawn> secondProvokers =
                GetActiveSecondPlayerProvokers(currentTick);

            if (firstProvokers.Count > 0
                && firstWithdrawalRetaliationStartTick < 0)
            {
                firstWithdrawalRetaliationStartTick = currentTick;
            }

            if (secondProvokers.Count > 0
                && secondWithdrawalRetaliationStartTick < 0)
            {
                secondWithdrawalRetaliationStartTick = currentTick;
            }

            bool firstEngaged = firstRemaining.Count > 0
                && firstProvokers.Count > 0;
            bool secondEngaged = secondRemaining.Count > 0
                && secondProvokers.Count > 0;

            if (firstEngaged)
            {
                CancelWithdrawalForCombat(firstRemaining);
                AssignCombatJobs(firstRemaining, firstProvokers);
            }

            if (secondEngaged)
            {
                CancelWithdrawalForCombat(secondRemaining);
                AssignCombatJobs(secondRemaining, secondProvokers);
            }

            if (firstEngaged || secondEngaged)
            {
                return;
            }

            if (currentTick >= nextWithdrawalRefreshTick)
            {
                RefreshStalledWithdrawal(firstDomain, firstDetachment);
                RefreshStalledWithdrawal(secondDomain, secondDetachment);
                nextWithdrawalRefreshTick = currentTick
                    + WithdrawalRefreshIntervalTicks;
            }

        }

        private void ForceRemainingMobilePawnsOffMap(List<Pawn> remaining)
        {
            foreach (Pawn pawn in remaining)
            {
                if (pawn == null
                    || pawn.Destroyed
                    || pawn.Dead
                    || !pawn.Spawned
                    || pawn.Map != map
                    || pawn.IsPrisonerOfColony)
                {
                    continue;
                }

                pawn.ExitMap(false, Rot4.Invalid);
            }
        }

        private void CancelWithdrawalForCombat(List<Pawn> pawns)
        {
            foreach (Pawn pawn in pawns)
            {
                Lord lord = pawn.GetLord();

                if (lord?.LordJob is LordJob_ExitMapBest)
                {
                    lord.RemovePawn(pawn);
                }

                if (pawn.jobs?.curJob?.def == JobDefOf.Goto)
                {
                    pawn.jobs.EndCurrentJob(
                        JobCondition.InterruptForced);
                }
            }
        }

        private void RefreshStalledWithdrawal(
            Faction faction,
            List<Pawn> pawns)
        {
            List<Pawn> stalled = GetMobileSurvivors(pawns)
                .Where(pawn =>
                    !(pawn.GetLord()?.LordJob
                        is LordJob_ExitMapBest)
                    || pawn.jobs?.curJob == null
                    || pawn.pather == null
                    || !pawn.pather.Moving)
                .ToList();

            if (stalled.Count > 0)
            {
                OrderFactionWithdrawal(faction, stalled);
            }
        }

        private void OrderFactionWithdrawal(
            Faction faction,
            List<Pawn> pawns)
        {
            List<Pawn> remaining = GetMobileSurvivors(pawns);

            if (faction == null || remaining.Count == 0)
            {
                return;
            }

            foreach (Pawn pawn in remaining)
            {
                pawn.GetLord()?.RemovePawn(pawn);
                pawn.jobs?.EndCurrentJob(JobCondition.InterruptForced);
            }

            LordMaker.MakeNewLord(
                faction,
                new LordJob_ExitMapBest(
                    LocomotionUrgency.Jog,
                    canDig: false,
                    canDefendSelf: true),
                map,
                remaining);
        }

        private void ReleaseFromRallyLords(List<Pawn> pawns)
        {
            foreach (Pawn pawn in GetActiveCombatants(pawns))
            {
                pawn.GetLord()?.RemovePawn(pawn);
                pawn.jobs?.EndCurrentJob(JobCondition.InterruptForced);
            }
        }

        private List<Pawn> GetActiveCombatants(List<Pawn> pawns)
        {
            return pawns
                ?.Where(pawn =>
                    pawn != null
                    && !pawn.Dead
                    && !pawn.Destroyed
                    && pawn.Spawned
                    && pawn.Map == map
                    && !pawn.Downed
                    && !pawn.IsPrisonerOfColony)
                .ToList()
                ?? new List<Pawn>();
        }

        private List<Pawn> GetMobileSurvivors(List<Pawn> pawns)
        {
            return GetActiveCombatants(pawns);
        }

        private static bool ShouldBreakContact(
            int activeCombatants,
            int initialCombatants)
        {
            int threshold = ResolveBreakThreshold(initialCombatants);
            return threshold > 0 && activeCombatants <= threshold;
        }

        private static int ResolveBreakThreshold(int initialCombatants)
        {
            if (initialCombatants <= 0)
            {
                return 0;
            }

            return (int)Math.Floor(
                initialCombatants
                * GoauldOpenConflictBattlefieldUtility
                    .EarlyWithdrawalFraction);
        }

        private void ResolveBattlefield()
        {
            if (resolved)
            {
                return;
            }

            resolved = true;
            GameComponent_GoauldOpenConflictBattlefieldTracker.Current
                ?.NotifyBattlefieldResolved(map?.uniqueID ?? -1);

            GR_Log.Message(
                "Resolved Goa'uld open-conflict battlefield on map "
                + $"{map?.uniqueID ?? -1}; first survivors on map="
                + $"{GetActiveCombatants(firstDetachment).Count}; second="
                + $"{GetActiveCombatants(secondDetachment).Count}.");
        }

        private string CurrentPhaseLabel()
        {
            if (resolved)
            {
                return "resolved";
            }

            if (withdrawalOrdered)
            {
                return "withdrawal";
            }

            return assaultStarted ? "assault" : "rally";
        }

        private static int HorizontalDistanceSquared(
            IntVec3 first,
            IntVec3 second)
        {
            int x = first.x - second.x;
            int z = first.z - second.z;
            return x * x + z * z;
        }
    }
}
