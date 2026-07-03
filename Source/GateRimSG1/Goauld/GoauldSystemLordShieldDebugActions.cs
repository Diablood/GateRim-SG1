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
                "System Lord kara kesh shield debug test");

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

            Messages.Message(
                hasShield
                    ? "A hostile Goa'uld System Lord was generated near the map center with its kara kesh equipped."
                    : "The hostile Goa'uld System Lord was generated, but its kara kesh could not be equipped. Check Player.log.",
                pawn,
                hasShield
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
