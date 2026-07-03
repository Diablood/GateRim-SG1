using System.Linq;
using GateRimSG1.Jaffa;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    internal static class NaquadahTraceDebugActions
    {
        public static void InspectPawn(Pawn pawn)
        {
            if (pawn == null)
            {
                return;
            }

            bool hasTrace = NaquadahTraceUtility.HasPersistentTrace(pawn);
            bool adultSymbiote = NaquadahTraceUtility
                .HasActiveAdultSymbiote(pawn);
            bool primta = NaquadahTraceUtility.HasActivePrimta(pawn);
            bool karaKesh = pawn.apparel?.WornApparel.Any(
                apparel => apparel?.def == GR_DefOf.SG1_KaraKesh) == true;

            Messages.Message(
                $"{pawn.LabelShortCap}: persistentTrace={hasTrace}; "
                + $"adultSymbiote={adultSymbiote}; Primta={primta}; "
                + $"karaKeshEquipped={karaKesh}; "
                + "karaKeshEligible="
                + NaquadahTraceUtility.CanActivateNaquadahTechnology(pawn)
                + ".",
                pawn,
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        public static void EquipKaraKesh(Pawn pawn)
        {
            if (pawn?.apparel == null || GR_DefOf.SG1_KaraKesh == null)
            {
                Messages.Message(
                    pawn == null
                        ? "No pawn selected."
                        : $"{pawn.LabelShortCap} cannot wear apparel.",
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            if (pawn.apparel.WornApparel.Any(
                    apparel => apparel?.def == GR_DefOf.SG1_KaraKesh))
            {
                Messages.Message(
                    $"{pawn.LabelShortCap} already wears a kara kesh.",
                    pawn,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            Apparel karaKesh = ThingMaker.MakeThing(
                GR_DefOf.SG1_KaraKesh) as Apparel;

            if (karaKesh == null)
            {
                Messages.Message(
                    "The kara kesh definition did not create apparel.",
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            karaKesh.TryGetComp<CompQuality>()?.SetQuality(
                QualityCategory.Normal,
                ArtGenerationContext.Outsider);
            pawn.apparel.Wear(karaKesh, dropReplacedApparel: true);

            bool equipped = pawn.apparel.WornApparel.Contains(karaKesh);

            if (!equipped && !karaKesh.Destroyed)
            {
                karaKesh.Destroy(DestroyMode.Vanish);
            }

            Messages.Message(
                equipped
                    ? $"Equipped {pawn.LabelShortCap} with a normal-quality kara kesh."
                    : $"Could not equip {pawn.LabelShortCap} with a kara kesh.",
                pawn,
                equipped
                    ? MessageTypeDefOf.PositiveEvent
                    : MessageTypeDefOf.RejectInput,
                historical: false);
        }

        public static void ApplyPersistentTrace(Pawn pawn)
        {
            if (pawn == null)
            {
                return;
            }

            bool added = NaquadahTraceUtility.EnsurePersistentTrace(
                pawn,
                "developer action");

            Messages.Message(
                added
                    ? $"Added persistent naquadah traces to {pawn.LabelShortCap}."
                    : $"{pawn.LabelShortCap} already has persistent naquadah traces or is not eligible for a gene marker.",
                pawn,
                added
                    ? MessageTypeDefOf.PositiveEvent
                    : MessageTypeDefOf.RejectInput,
                historical: false);
        }

        public static void RemovePersistentTrace(Pawn pawn)
        {
            if (pawn == null)
            {
                return;
            }

            bool removed = NaquadahTraceUtility
                .RemovePersistentTraceForDebug(pawn);
            bool activeSource = NaquadahTraceUtility
                .HasActiveAdultSymbiote(pawn)
                || NaquadahTraceUtility.HasActivePrimta(pawn);

            string message = removed
                ? $"Removed persistent naquadah traces from {pawn.LabelShortCap}."
                : $"{pawn.LabelShortCap} has no persistent naquadah trace marker.";

            if (removed && activeSource)
            {
                message += " An active symbiote source will restore them on reconciliation.";
            }

            Messages.Message(
                message,
                pawn,
                removed
                    ? MessageTypeDefOf.NeutralEvent
                    : MessageTypeDefOf.RejectInput,
                historical: false);
        }

        public static void ApplyAdultSymbioteState(Pawn pawn)
        {
            if (pawn?.health == null
                || NaquadahTraceUtility.HasActiveAdultSymbiote(pawn))
            {
                Messages.Message(
                    pawn == null
                        ? "No pawn selected."
                        : $"{pawn.LabelShortCap} already has an adult symbiote state.",
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            Hediff hostState = HediffMaker.MakeHediff(
                GR_DefOf.SG1_GoauldHostSymbiote,
                pawn);
            HediffComp_GoauldSymbiote comp =
                (hostState as HediffWithComps)
                    ?.GetComp<HediffComp_GoauldSymbiote>();

            if (comp == null)
            {
                Messages.Message(
                    "The active adult-symbiote state is missing its persistent data component.",
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            GoauldSymbioteData data = GoauldSymbioteData.CreateFree(
                Find.TickManager?.TicksGame ?? 0,
                GoauldSymbioteOrigin.Goauld);
            data.RecordAllegiance(pawn.Faction);
            comp.InitializeWithTransferredData(data);
            pawn.health.AddHediff(hostState);

            Messages.Message(
                $"Applied an adult Goa'uld symbiote test state to {pawn.LabelShortCap}.",
                pawn,
                MessageTypeDefOf.PositiveEvent,
                historical: false);
        }

        public static void RemoveAdultSymbioteState(Pawn pawn)
        {
            Hediff hediff = FindHediff(
                pawn,
                GR_DefOf.SG1_GoauldRecentImplantation,
                GR_DefOf.SG1_GoauldHostSymbiote);

            if (hediff == null)
            {
                Messages.Message(
                    pawn == null
                        ? "No pawn selected."
                        : $"{pawn.LabelShortCap} has no adult symbiote state.",
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            pawn.health.RemoveHediff(hediff);
            Messages.Message(
                $"Removed the adult symbiote test state from {pawn.LabelShortCap}; persistent traces remain.",
                pawn,
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        public static void ApplyPrimtaState(Pawn pawn)
        {
            if (!JaffaPrimtaUtility.IsCompatibleJaffa(pawn))
            {
                Messages.Message(
                    pawn == null
                        ? "No pawn selected."
                        : $"{pawn.LabelShortCap} is not a compatible Jaffa.",
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            if (JaffaPrimtaUtility.HasPrimta(pawn))
            {
                Messages.Message(
                    $"{pawn.LabelShortCap} already carries a Prim'ta.",
                    pawn,
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            pawn.health.AddHediff(
                HediffMaker.MakeHediff(GR_DefOf.SG1_JaffaPrimta, pawn));
            Messages.Message(
                $"Applied a Prim'ta test state to {pawn.LabelShortCap}.",
                pawn,
                MessageTypeDefOf.PositiveEvent,
                historical: false);
        }

        public static void RemovePrimtaState(Pawn pawn)
        {
            Hediff hediff = FindHediff(pawn, GR_DefOf.SG1_JaffaPrimta);

            if (hediff == null)
            {
                Messages.Message(
                    pawn == null
                        ? "No pawn selected."
                        : $"{pawn.LabelShortCap} carries no Prim'ta.",
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            pawn.health.RemoveHediff(hediff);
            Messages.Message(
                $"Removed the Prim'ta test state from {pawn.LabelShortCap}; persistent traces remain.",
                pawn,
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        public static void ReconcileAllKnownPawns()
        {
            GameComponent_NaquadahTraceReconciler reconciler =
                GameComponent_NaquadahTraceReconciler.Current;

            if (reconciler == null)
            {
                Messages.Message(
                    "The naquadah trace reconciler is unavailable.",
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            int addedCount = reconciler.ReconcileKnownPawns(
                includeWorldPawns: true,
                writeSummaryLog: true);
            Messages.Message(
                $"Naquadah trace reconciliation added {addedCount} persistent marker(s).",
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        private static Hediff FindHediff(
            Pawn pawn,
            params HediffDef[] hediffDefs)
        {
            if (pawn?.health?.hediffSet?.hediffs == null
                || hediffDefs == null)
            {
                return null;
            }

            for (int hediffIndex = 0;
                hediffIndex < pawn.health.hediffSet.hediffs.Count;
                hediffIndex++)
            {
                Hediff hediff = pawn.health.hediffSet.hediffs[hediffIndex];

                for (int defIndex = 0;
                    defIndex < hediffDefs.Length;
                    defIndex++)
                {
                    if (hediff?.def == hediffDefs[defIndex])
                    {
                        return hediff;
                    }
                }
            }

            return null;
        }
    }
}
