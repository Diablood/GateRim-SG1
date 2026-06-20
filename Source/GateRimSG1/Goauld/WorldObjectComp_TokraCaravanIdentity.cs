using System;
using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GateRimSG1.Goauld
{
    public sealed class WorldObjectCompProperties_TokraCaravanIdentity
        : WorldObjectCompProperties
    {
        public WorldObjectCompProperties_TokraCaravanIdentity()
        {
            compClass = typeof(WorldObjectComp_TokraCaravanIdentity);
        }
    }

    /// <summary>
    /// Exposes one compact world-map command for every eligible Tok'ra in a
    /// player caravan. Individual Hediff gizmos are map-only in vanilla, so the
    /// caravan menu delegates to the same validated personality-switch method.
    /// </summary>
    public sealed class WorldObjectComp_TokraCaravanIdentity : WorldObjectComp
    {
        public override IEnumerable<Gizmo> GetGizmos()
        {
            Caravan caravan = parent as Caravan;
            if (caravan == null
                || !caravan.IsPlayerControlled
                || Find.WorldSelector.SingleSelectedObject != caravan)
            {
                yield break;
            }

            List<HediffComp_GoauldSymbiote> eligible = EligibleTokra(caravan);
            if (eligible.Count == 0)
            {
                yield break;
            }

            yield return new Command_Action
            {
                defaultLabel = "GR_TokraCaravanIdentity_Label".Translate().ToString(),
                defaultDesc = "GR_TokraCaravanIdentity_Desc".Translate().ToString(),
                icon = TexCommand.GatherSpotActive,
                action = () => OpenIdentityMenu(eligible)
            };
        }

        private static List<HediffComp_GoauldSymbiote> EligibleTokra(
            Caravan caravan)
        {
            List<HediffComp_GoauldSymbiote> result
                = new List<HediffComp_GoauldSymbiote>();

            List<Pawn> pawns = caravan.PawnsListForReading;
            for (int i = 0; i < pawns.Count; i++)
            {
                if (TokraPlayerControlUtility.TryGetEligibleTokraComp(
                    pawns[i],
                    out HediffComp_GoauldSymbiote symbioteComp)
                    && symbioteComp.CanSwitchPersonality)
                {
                    result.Add(symbioteComp);
                }
            }

            return result;
        }

        private static void OpenIdentityMenu(
            List<HediffComp_GoauldSymbiote> eligible)
        {
            List<FloatMenuOption> options = new List<FloatMenuOption>();

            for (int i = 0; i < eligible.Count; i++)
            {
                HediffComp_GoauldSymbiote symbioteComp = eligible[i];
                Pawn pawn = symbioteComp.HostPawn;
                string pawnName = pawn?.LabelShortCap
                    ?? "GR_TokraDualIdentity_NotRecorded".Translate().ToString();
                string targetName = symbioteComp.InactivePersonalityName;

                options.Add(new FloatMenuOption(
                    "GR_TokraCaravanIdentity_Option".Translate(
                        pawnName,
                        targetName).ToString(),
                    () => symbioteComp.TryTogglePersonality()));
            }

            Find.WindowStack.Add(new FloatMenu(options));
        }
    }
}
