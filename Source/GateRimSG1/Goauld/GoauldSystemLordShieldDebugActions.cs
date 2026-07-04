using System.Linq;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    internal static class GoauldSystemLordShieldDebugActions
    {
        private const float TestDamage = 10f;

        public static void SpawnPersonalShield()
        {
            Map map = Find.CurrentMap;
            ThingDef shieldDef = GR_DefOf.SG1_KaraKesh;

            if (map == null || shieldDef == null)
            {
                Messages.Message(
                    "No current map or kara kesh definition.",
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            Thing shield = ThingMaker.MakeThing(shieldDef);
            shield.TryGetComp<CompQuality>()?.SetQuality(
                QualityCategory.Normal,
                ArtGenerationContext.Outsider);

            Thing placed;

            if (!GenPlace.TryPlaceThing(
                    shield,
                    map.Center,
                    map,
                    ThingPlaceMode.Near,
                    out placed))
            {
                if (!shield.Destroyed)
                {
                    shield.Destroy(DestroyMode.Vanish);
                }

                Messages.Message(
                    "Could not place the kara kesh.",
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            Messages.Message(
                "A normal-quality kara kesh was placed near the map center.",
                placed,
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        public static void SpawnHostileSystemLord()
        {
            Map map = Find.CurrentMap;
            Faction faction = GoauldSystemLordFactionUtility.GetOrCreateFaction(
                "System Lord kara kesh debug test");

            if (map == null
                || faction == null
                || GR_DefOf.SG1_GoauldSystemLordHost == null)
            {
                Messages.Message(
                    "No current map, Goa'uld faction or System Lord pawn kind.",
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            PawnGenerationRequest request = new PawnGenerationRequest(
                kind: GR_DefOf.SG1_GoauldSystemLordHost,
                faction: faction,
                context: PawnGenerationContext.NonPlayer,
                tile: map.Tile,
                forceGenerateNewPawn: true,
                canGeneratePawnRelations: false,
                colonistRelationChanceFactor: 0f,
                relationWithExtraPawnChanceFactor: 0f);
            Pawn pawn = PawnGenerator.GeneratePawn(request);
            IntVec3 spawnCell = CellFinder.RandomClosewalkCellNear(
                map.Center,
                map,
                10);
            GenSpawn.Spawn(pawn, spawnCell, map);
            GameComponent_GoauldHostCasteInitializer.Current
                ?.NotifyPawnSpawned(pawn);

            bool hasShield = pawn.apparel?.WornApparel.Any(
                apparel => apparel?.def
                    == GR_DefOf.SG1_KaraKesh) == true;
            bool hasHealingBracelet = pawn.apparel?.WornApparel.Any(
                apparel => apparel?.def
                    == GR_DefOf.SG1_GoauldHealingBracelet) == true;
            bool hasRankEquipment = hasShield && hasHealingBracelet;

            Messages.Message(
                hasRankEquipment
                    ? "A hostile Goa'uld System Lord was generated near the map center with its kara kesh and healing bracelet equipped."
                    : "The hostile Goa'uld System Lord was generated, but some rank equipment could not be equipped. Check Player.log.",
                pawn,
                hasRankEquipment
                    ? MessageTypeDefOf.ThreatSmall
                    : MessageTypeDefOf.RejectInput,
                historical: false);
        }

        public static void ApplyRangedTestHit(Pawn pawn)
        {
            ApplyTestDamage(pawn, DamageDefOf.Bullet, "ranged projectile");
        }

        public static void ApplyMeleeTestHit(Pawn pawn)
        {
            ApplyTestDamage(pawn, DamageDefOf.Blunt, "melee blunt");
        }

        public static void ApplyEmpTestHit(Pawn pawn)
        {
            ApplyTestDamage(pawn, DamageDefOf.EMP, "EMP");
        }

        public static void InspectKineticBlastState(Pawn pawn)
        {
            Comp_KaraKeshShield comp = GetKaraKeshComp(pawn);

            if (comp == null)
            {
                ReportMissingKaraKesh(pawn);
                return;
            }

            Messages.Message(
                "Kara kesh kinetic blast state for " + pawn.LabelShortCap
                    + ": trace="
                    + NaquadahTraceUtility.HasPersistentTrace(pawn)
                    + ", shieldState=" + comp.ShieldState
                    + ", energy=" + comp.Energy.ToString("0.00")
                    + ", cooldownTicks="
                    + comp.KineticBlastCooldownRemainingTicks + ".",
                pawn,
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        public static void UseSelectedWearerKineticBlast(Pawn target)
        {
            Pawn wearer = Find.Selector?.SingleSelectedThing as Pawn;
            Comp_KaraKeshShield comp = GetKaraKeshComp(wearer);

            if (wearer == null || comp == null)
            {
                Messages.Message(
                    "Select a pawn wearing a kara kesh before choosing the blast target.",
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            if (!comp.TryUseKineticBlast(target))
            {
                return;
            }

            Messages.Message(
                wearer.LabelShortCap + " used the kara kesh kinetic blast on "
                    + target.LabelShortCap + ".",
                target,
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        public static void PrepareKineticBlastTestState(Pawn pawn)
        {
            Comp_KaraKeshShield comp = GetKaraKeshComp(pawn);

            if (comp == null)
            {
                ReportMissingKaraKesh(pawn);
                return;
            }

            comp.PrepareKineticBlastForDebug();
            Messages.Message(
                "Prepared a fully charged kara kesh kinetic blast for "
                    + pawn.LabelShortCap + ".",
                pawn,
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        public static void ResetKineticBlastCooldown(Pawn pawn)
        {
            Comp_KaraKeshShield comp = GetKaraKeshComp(pawn);

            if (comp == null)
            {
                ReportMissingKaraKesh(pawn);
                return;
            }

            comp.ResetKineticBlastCooldownForDebug();
            Messages.Message(
                "Reset the kara kesh kinetic blast cooldown for "
                    + pawn.LabelShortCap + ".",
                pawn,
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        public static void InspectNeuralAttackState(Pawn pawn)
        {
            Comp_KaraKeshShield comp = GetKaraKeshComp(pawn);

            if (comp == null)
            {
                ReportMissingKaraKesh(pawn);
                return;
            }

            Messages.Message(
                "Kara kesh neural attack state for " + pawn.LabelShortCap
                    + ": trace="
                    + NaquadahTraceUtility.HasPersistentTrace(pawn)
                    + ", shieldState=" + comp.ShieldState
                    + ", energy=" + comp.Energy.ToString("0.00")
                    + ", cooldownTicks="
                    + comp.NeuralAttackCooldownRemainingTicks
                    + ", neuralAgony="
                    + KaraKeshNeuralAttackUtility.HasEffect(
                        pawn,
                        comp.NeuralAttackHediff)
                    + ".",
                pawn,
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        public static void UseSelectedWearerNeuralAttack(Pawn target)
        {
            Pawn wearer = Find.Selector?.SingleSelectedThing as Pawn;
            Comp_KaraKeshShield comp = GetKaraKeshComp(wearer);

            if (wearer == null || comp == null)
            {
                Messages.Message(
                    "Select a pawn wearing a kara kesh before choosing the neural-attack target.",
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            if (!comp.TryUseNeuralAttack(target))
            {
                return;
            }

            Messages.Message(
                wearer.LabelShortCap + " used the kara kesh neural attack on "
                    + target.LabelShortCap + ".",
                target,
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        public static void PrepareNeuralAttackTestState(Pawn pawn)
        {
            Comp_KaraKeshShield comp = GetKaraKeshComp(pawn);

            if (comp == null)
            {
                ReportMissingKaraKesh(pawn);
                return;
            }

            comp.PrepareNeuralAttackForDebug();
            Messages.Message(
                "Prepared a fully charged kara kesh neural attack for "
                    + pawn.LabelShortCap + ".",
                pawn,
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        public static void ResetNeuralAttackCooldown(Pawn pawn)
        {
            Comp_KaraKeshShield comp = GetKaraKeshComp(pawn);

            if (comp == null)
            {
                ReportMissingKaraKesh(pawn);
                return;
            }

            comp.ResetNeuralAttackCooldownForDebug();
            Messages.Message(
                "Reset the kara kesh neural attack cooldown for "
                    + pawn.LabelShortCap + ".",
                pawn,
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        public static void ClearNeuralAgony(Pawn pawn)
        {
            bool removed = KaraKeshNeuralAttackUtility.TryClear(
                pawn,
                GR_DefOf.SG1_KaraKeshNeuralAgony);

            Messages.Message(
                removed
                    ? "Cleared kara kesh neural agony from "
                        + pawn.LabelShortCap + "."
                    : pawn?.LabelShortCap
                        + " does not have kara kesh neural agony.",
                pawn,
                removed
                    ? MessageTypeDefOf.NeutralEvent
                    : MessageTypeDefOf.RejectInput,
                historical: false);
        }


        public static void InspectParalysisHoldState(Pawn pawn)
        {
            Comp_KaraKeshShield comp = GetKaraKeshComp(pawn);

            if (comp == null)
            {
                ReportMissingKaraKesh(pawn);
                return;
            }

            Pawn activeTarget = comp.ActiveParalysisTarget;
            Messages.Message(
                "Kara kesh paralysis hold state for " + pawn.LabelShortCap
                    + ": trace="
                    + NaquadahTraceUtility.HasPersistentTrace(pawn)
                    + ", shieldState=" + comp.ShieldState
                    + ", energy=" + comp.Energy.ToString("0.00")
                    + ", cooldownTicks="
                    + comp.ParalysisHoldCooldownRemainingTicks
                    + ", activeTarget="
                    + (activeTarget?.LabelShortCap ?? "<none>")
                    + ", targetHeld="
                    + KaraKeshParalysisHoldUtility.HasEffect(
                        activeTarget,
                        comp.ParalysisHoldHediff)
                    + ".",
                pawn,
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        public static void UseSelectedWearerParalysisHold(Pawn target)
        {
            Pawn wearer = Find.Selector?.SingleSelectedThing as Pawn;
            Comp_KaraKeshShield comp = GetKaraKeshComp(wearer);

            if (wearer == null || comp == null)
            {
                Messages.Message(
                    "Select a pawn wearing a kara kesh before choosing the paralysis-hold target.",
                    MessageTypeDefOf.RejectInput,
                    historical: false);
                return;
            }

            if (!comp.TryUseParalysisHold(target))
            {
                return;
            }

            Messages.Message(
                wearer.LabelShortCap
                    + " used the kara kesh paralysis hold on "
                    + target.LabelShortCap + ".",
                target,
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        public static void PrepareParalysisHoldTestState(Pawn pawn)
        {
            Comp_KaraKeshShield comp = GetKaraKeshComp(pawn);

            if (comp == null)
            {
                ReportMissingKaraKesh(pawn);
                return;
            }

            comp.PrepareParalysisHoldForDebug();
            Messages.Message(
                "Prepared a fully charged kara kesh paralysis hold for "
                    + pawn.LabelShortCap + ".",
                pawn,
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        public static void ResetParalysisHoldCooldown(Pawn pawn)
        {
            Comp_KaraKeshShield comp = GetKaraKeshComp(pawn);

            if (comp == null)
            {
                ReportMissingKaraKesh(pawn);
                return;
            }

            comp.ResetParalysisHoldCooldownForDebug();
            Messages.Message(
                "Reset the kara kesh paralysis hold cooldown for "
                    + pawn.LabelShortCap + ".",
                pawn,
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }

        public static void ReleaseParalysisHold(Pawn pawn)
        {
            Comp_KaraKeshShield comp = GetKaraKeshComp(pawn);

            if (comp == null)
            {
                ReportMissingKaraKesh(pawn);
                return;
            }

            bool released = comp.ReleaseParalysisHold();
            Messages.Message(
                released
                    ? "Released the kara kesh paralysis hold maintained by "
                        + pawn.LabelShortCap + "."
                    : pawn.LabelShortCap
                        + " is not maintaining a kara kesh paralysis hold.",
                pawn,
                released
                    ? MessageTypeDefOf.NeutralEvent
                    : MessageTypeDefOf.RejectInput,
                historical: false);
        }

        public static void ClearParalysisHold(Pawn pawn)
        {
            bool removed = KaraKeshParalysisHoldUtility.TryClear(
                pawn,
                GR_DefOf.SG1_KaraKeshParalysisHold);

            Messages.Message(
                removed
                    ? "Cleared the kara kesh paralysis hold from "
                        + pawn.LabelShortCap + "."
                    : pawn?.LabelShortCap
                        + " does not have a kara kesh paralysis hold.",
                pawn,
                removed
                    ? MessageTypeDefOf.NeutralEvent
                    : MessageTypeDefOf.RejectInput,
                historical: false);
        }

        private static Comp_KaraKeshShield GetKaraKeshComp(Pawn pawn)
        {
            Apparel karaKesh = pawn?.apparel?.WornApparel?.FirstOrDefault(
                apparel => apparel?.def == GR_DefOf.SG1_KaraKesh);
            return karaKesh?.TryGetComp<Comp_KaraKeshShield>();
        }

        private static void ReportMissingKaraKesh(Pawn pawn)
        {
            Messages.Message(
                pawn?.LabelShortCap + " is not wearing a kara kesh.",
                pawn,
                MessageTypeDefOf.RejectInput,
                historical: false);
        }

        private static void ApplyTestDamage(
            Pawn pawn,
            DamageDef damageDef,
            string label)
        {
            if (pawn == null || pawn.Destroyed || damageDef == null)
            {
                return;
            }

            pawn.TakeDamage(new DamageInfo(damageDef, TestDamage));
            Messages.Message(
                "Applied " + TestDamage.ToString("0") + " " + label
                    + " test damage to " + pawn.LabelShortCap + ".",
                pawn,
                MessageTypeDefOf.NeutralEvent,
                historical: false);
        }
    }
}
