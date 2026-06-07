using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// First interactive forced-implantation prototype.
    ///
    /// A selected free Goa'uld symbiote can implant one compatible adjacent
    /// humanoid pawn through a gizmo. The same persistent symbiote identity is
    /// moved into SG1_GoauldRecentImplantation, then the free pawn is consumed.
    ///
    /// Autonomous melee AI and target-selection jobs remain future work.
    /// </summary>
    public class Comp_GoauldForcedImplantation : ThingComp
    {
        private GoauldSymbioteData symbioteData;
        private bool consumedByImplantation;

        private CompProperties_GoauldForcedImplantation Props
            => (CompProperties_GoauldForcedImplantation)props;

        private Pawn SymbiotePawn => parent as Pawn;

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);

            EnsureDataInitialized();

            if (!respawningAfterLoad)
            {
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

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                EnsureDataInitialized();

                GR_Log.Message(
                    $"Loaded free Goa'uld symbiote {symbioteData.SymbioteId} "
                    + $"as pawn {PawnDebugLabel(SymbiotePawn)}.");
            }
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
                action = TryImplantAdjacentHost
            };
        }

        public override string CompInspectStringExtra()
        {
            EnsureDataInitialized();

            return "GR_FreeGoauldSymbioteDataSummary".Translate(
                symbioteData.SymbioteId,
                symbioteData.GetOriginLabel()).ToString();
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
                    + "during forced implantation.");
            }
            else
            {
                GR_Log.Message(
                    $"Destroyed free Goa'uld symbiote {symbioteData.SymbioteId} "
                    + $"with mode {mode}.");
            }
        }

        private void TryImplantAdjacentHost()
        {
            Pawn symbiote = SymbiotePawn;
            if (symbiote == null || !symbiote.Spawned || symbiote.Destroyed)
            {
                GR_Log.Warning("Forced implantation requested from an unavailable free symbiote pawn.");
                return;
            }

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

            EnsureDataInitialized();

            Hediff implantation = HediffMaker.MakeHediff(
                GR_DefOf.SG1_GoauldRecentImplantation,
                target);

            HediffComp_GoauldSymbiote implantationComp
                = FindPersistentSymbioteComp(implantation);

            if (implantationComp == null)
            {
                GR_Log.Error(
                    "Unable to start forced implantation: "
                    + "SG1_GoauldRecentImplantation is missing "
                    + "HediffComp_GoauldSymbiote.");

                Messages.Message(
                    "GR_ForcedImplantation_InternalError".Translate(),
                    symbiote,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            string transferredId = symbioteData.SymbioteId;

            implantationComp.InitializeWithTransferredData(symbioteData);
            target.health.AddHediff(implantation);

            consumedByImplantation = true;

            GR_Log.Message(
                $"Forced implantation transferred Goa'uld symbiote {transferredId} "
                + $"from free pawn {PawnDebugLabel(symbiote)} "
                + $"into host {PawnDebugLabel(target)}.");

            Messages.Message(
                "GR_ForcedImplantation_Success".Translate(
                    target.LabelShortCap,
                    transferredId),
                target,
                MessageTypeDefOf.NegativeEvent,
                historical: true);

            symbiote.Destroy(DestroyMode.Vanish);
        }

        private Pawn FindFirstCompatibleAdjacentHost(Pawn symbiote)
        {
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

        private bool IsCompatibleHost(Pawn candidate)
        {
            if (candidate == null
                || candidate == SymbiotePawn
                || candidate.Destroyed
                || candidate.Dead
                || candidate.health == null
                || !candidate.RaceProps.Humanlike)
            {
                return false;
            }

            if (candidate.ageTracker != null
                && candidate.ageTracker.AgeBiologicalYearsFloat < Props.minimumTargetAgeYears)
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
            if (withComps?.comps == null)
            {
                return null;
            }

            for (int index = 0; index < withComps.comps.Count; index++)
            {
                HediffComp_GoauldSymbiote comp
                    = withComps.comps[index] as HediffComp_GoauldSymbiote;

                if (comp != null)
                {
                    return comp;
                }
            }

            return null;
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
}
