using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    internal sealed class TokraRelaySabotageSiteLayoutResult
    {
        public IntVec3 RelayCell;
        public IntVec3 RewardCell;
        public IntVec3 DefenderRootCell;
        public string LayoutLabel;
    }

    internal static class TokraRelaySabotageSiteLayoutUtility
    {
        private const int StructureMargin = 3;

        public static TokraRelaySabotageSiteLayoutResult Generate(
            Map map,
            Faction faction)
        {
            if (map == null)
            {
                return null;
            }

            IntVec3 center = map.Center;
            int layoutIndex = Rand.RangeInclusive(0, 2);
            TokraRelaySabotageSiteLayoutResult result;

            switch (layoutIndex)
            {
                case 1:
                    result = GenerateTwinBlockRelay(map, faction, center);
                    break;
                case 2:
                    result = GenerateCourtyardRelay(map, faction, center);
                    break;
                default:
                    result = GenerateBunkerRelay(map, faction, center);
                    break;
            }

            GR_Log.Message(
                "Generated Tok'ra relay sabotage site layout: "
                + (result?.LayoutLabel ?? "fallback")
                + ".");

            return result;
        }

        private static TokraRelaySabotageSiteLayoutResult GenerateBunkerRelay(
            Map map,
            Faction faction,
            IntVec3 center)
        {
            CellRect commandRoom = CenteredRect(center, 15, 11);
            CellRect storageRoom = new CellRect(
                commandRoom.maxX + 4,
                center.z - 3,
                7,
                7);
            CellRect totalRect = ExpandRect(
                BoundingRect(commandRoom, storageRoom),
                StructureMargin);

            ClearRect(map, totalRect);
            BuildRoom(
                map,
                faction,
                commandRoom,
                new HashSet<IntVec3>
                {
                    new IntVec3(commandRoom.CenterCell.x, 0, commandRoom.minZ)
                });
            BuildRoom(
                map,
                faction,
                storageRoom,
                new HashSet<IntVec3>
                {
                    new IntVec3(storageRoom.minX, 0, storageRoom.CenterCell.z)
                });
            BuildPath(
                map,
                new IntVec3(commandRoom.maxX + 1, 0, center.z),
                new IntVec3(storageRoom.minX - 1, 0, center.z));
            SpawnDefensiveLine(
                map,
                faction,
                new IntVec3(center.x - 4, 0, commandRoom.minZ - 3),
                9,
                horizontal: true);

            return new TokraRelaySabotageSiteLayoutResult
            {
                RelayCell = commandRoom.CenterCell,
                RewardCell = storageRoom.CenterCell,
                DefenderRootCell = new IntVec3(center.x, 0, commandRoom.minZ - 5),
                LayoutLabel = "command bunker"
            };
        }

        private static TokraRelaySabotageSiteLayoutResult GenerateTwinBlockRelay(
            Map map,
            Faction faction,
            IntVec3 center)
        {
            CellRect relayBlock = new CellRect(
                center.x - 13,
                center.z - 4,
                11,
                9);
            CellRect supportBlock = new CellRect(
                center.x + 4,
                center.z - 4,
                9,
                9);
            CellRect totalRect = ExpandRect(
                BoundingRect(relayBlock, supportBlock),
                StructureMargin);

            ClearRect(map, totalRect);
            BuildRoom(
                map,
                faction,
                relayBlock,
                new HashSet<IntVec3>
                {
                    new IntVec3(relayBlock.maxX, 0, relayBlock.CenterCell.z)
                });
            BuildRoom(
                map,
                faction,
                supportBlock,
                new HashSet<IntVec3>
                {
                    new IntVec3(supportBlock.minX, 0, supportBlock.CenterCell.z)
                });
            BuildPath(
                map,
                new IntVec3(relayBlock.maxX + 1, 0, center.z),
                new IntVec3(supportBlock.minX - 1, 0, center.z));
            SpawnDefensiveLine(
                map,
                faction,
                new IntVec3(center.x - 6, 0, relayBlock.minZ - 3),
                13,
                horizontal: true);

            return new TokraRelaySabotageSiteLayoutResult
            {
                RelayCell = relayBlock.CenterCell,
                RewardCell = supportBlock.CenterCell,
                DefenderRootCell = new IntVec3(center.x, 0, relayBlock.minZ - 5),
                LayoutLabel = "split relay station"
            };
        }

        private static TokraRelaySabotageSiteLayoutResult GenerateCourtyardRelay(
            Map map,
            Faction faction,
            IntVec3 center)
        {
            CellRect courtyard = CenteredRect(center, 27, 21);
            CellRect relayRoom = new CellRect(
                center.x - 5,
                center.z + 2,
                11,
                9);
            CellRect storageRoom = new CellRect(
                center.x + 5,
                center.z - 7,
                7,
                7);

            ClearRect(map, ExpandRect(courtyard, StructureMargin));
            BuildPerimeter(
                map,
                faction,
                courtyard,
                new HashSet<IntVec3>
                {
                    new IntVec3(center.x, 0, courtyard.minZ),
                    new IntVec3(center.x + 1, 0, courtyard.minZ)
                });
            BuildRoom(
                map,
                faction,
                relayRoom,
                new HashSet<IntVec3>
                {
                    new IntVec3(relayRoom.CenterCell.x, 0, relayRoom.minZ)
                });
            BuildRoom(
                map,
                faction,
                storageRoom,
                new HashSet<IntVec3>
                {
                    new IntVec3(storageRoom.minX, 0, storageRoom.CenterCell.z)
                });
            BuildPath(
                map,
                new IntVec3(center.x, 0, courtyard.minZ + 1),
                new IntVec3(center.x, 0, relayRoom.minZ - 1));
            BuildPath(
                map,
                new IntVec3(center.x + 1, 0, center.z - 3),
                new IntVec3(storageRoom.minX - 1, 0, center.z - 3));
            SpawnDefensiveLine(
                map,
                faction,
                new IntVec3(center.x - 4, 0, courtyard.minZ + 4),
                9,
                horizontal: true);

            return new TokraRelaySabotageSiteLayoutResult
            {
                RelayCell = relayRoom.CenterCell,
                RewardCell = storageRoom.CenterCell,
                DefenderRootCell = new IntVec3(center.x, 0, courtyard.minZ + 6),
                LayoutLabel = "walled relay courtyard"
            };
        }

        private static void BuildRoom(
            Map map,
            Faction faction,
            CellRect rect,
            HashSet<IntVec3> doorCells)
        {
            foreach (IntVec3 cell in rect.Cells)
            {
                if (cell.InBounds(map))
                {
                    map.terrainGrid.SetTerrain(cell, TerrainDefOf.Concrete);
                }
            }

            foreach (IntVec3 cell in rect.Cells)
            {
                bool edge = cell.x == rect.minX
                    || cell.x == rect.maxX
                    || cell.z == rect.minZ
                    || cell.z == rect.maxZ;

                if (!edge)
                {
                    continue;
                }

                SpawnEdifice(
                    map,
                    faction,
                    doorCells.Contains(cell)
                        ? GetRelayDoorDef()
                        : GetRelayWallDef(),
                    cell);
            }

            foreach (IntVec3 cell in rect.Cells)
            {
                if (cell.InBounds(map))
                {
                    map.roofGrid.SetRoof(
                        cell,
                        RoofDefOf.RoofConstructed);
                }
            }

            RefogRoomInterior(map, rect);
        }

        private static void RefogRoomInterior(Map map, CellRect rect)
        {
            if (map == null
                || map.fogGrid == null
                || rect.Width <= 2
                || rect.Height <= 2)
            {
                return;
            }

            CellRect interior = new CellRect(
                rect.minX + 1,
                rect.minZ + 1,
                rect.Width - 2,
                rect.Height - 2);

            map.fogGrid.Refog(interior);
        }

        private static void BuildPerimeter(
            Map map,
            Faction faction,
            CellRect rect,
            HashSet<IntVec3> doorCells)
        {
            foreach (IntVec3 cell in rect.Cells)
            {
                bool edge = cell.x == rect.minX
                    || cell.x == rect.maxX
                    || cell.z == rect.minZ
                    || cell.z == rect.maxZ;

                if (!edge)
                {
                    continue;
                }

                SpawnEdifice(
                    map,
                    faction,
                    doorCells.Contains(cell)
                        ? GetRelayDoorDef()
                        : GetRelayWallDef(),
                    cell);
            }
        }

        private static void BuildPath(
            Map map,
            IntVec3 from,
            IntVec3 to)
        {
            IntVec3 current = from;

            while (current.x != to.x)
            {
                SetConcreteIfValid(map, current);
                current.x += current.x < to.x ? 1 : -1;
            }

            while (current.z != to.z)
            {
                SetConcreteIfValid(map, current);
                current.z += current.z < to.z ? 1 : -1;
            }

            SetConcreteIfValid(map, current);
        }

        private static void SpawnDefensiveLine(
            Map map,
            Faction faction,
            IntVec3 start,
            int length,
            bool horizontal)
        {
            ThingDef defenseDef = GetRelayBarricadeDef();

            if (defenseDef == null)
            {
                return;
            }

            for (int index = 0; index < length; index++)
            {
                if (index == length / 2)
                {
                    continue;
                }

                IntVec3 cell = horizontal
                    ? new IntVec3(start.x + index, 0, start.z)
                    : new IntVec3(start.x, 0, start.z + index);

                if (!cell.InBounds(map) || cell.GetEdifice(map) != null)
                {
                    continue;
                }

                SpawnEdifice(map, faction, defenseDef, cell);
            }
        }

        private static ThingDef GetRelayWallDef()
        {
            return GR_DefOf.SG1_TokraRelaySiteWall ?? ThingDefOf.Wall;
        }

        private static ThingDef GetRelayDoorDef()
        {
            return GR_DefOf.SG1_TokraRelaySiteDoor ?? ThingDefOf.Door;
        }

        private static ThingDef GetRelayBarricadeDef()
        {
            return GR_DefOf.SG1_TokraRelaySiteBarricade
                ?? ThingDefOf.Barricade
                ?? ThingDefOf.Sandbags;
        }

        private static void SpawnEdifice(
            Map map,
            Faction faction,
            ThingDef def,
            IntVec3 cell)
        {
            if (map == null
                || def == null
                || !cell.InBounds(map)
                || cell.GetEdifice(map) != null)
            {
                return;
            }

            ThingDef stuff = def.MadeFromStuff ? ThingDefOf.Steel : null;
            Thing thing = ThingMaker.MakeThing(def, stuff);
            Thing spawned = GenSpawn.Spawn(thing, cell, map);
            spawned?.SetFaction(faction);
        }

        private static void SetConcreteIfValid(Map map, IntVec3 cell)
        {
            if (cell.InBounds(map))
            {
                map.terrainGrid.SetTerrain(cell, TerrainDefOf.Concrete);
            }
        }

        private static void ClearRect(Map map, CellRect rect)
        {
            foreach (IntVec3 cell in rect.Cells)
            {
                if (!cell.InBounds(map))
                {
                    continue;
                }

                List<Thing> things = cell.GetThingList(map);

                for (int index = things.Count - 1; index >= 0; index--)
                {
                    Thing thing = things[index];

                    if (thing == null || thing.Destroyed || thing is Pawn)
                    {
                        continue;
                    }

                    thing.Destroy(DestroyMode.Vanish);
                }

            }
        }

        private static CellRect CenteredRect(
            IntVec3 center,
            int width,
            int height)
        {
            return new CellRect(
                center.x - width / 2,
                center.z - height / 2,
                width,
                height);
        }


        private static CellRect ExpandRect(CellRect rect, int margin)
        {
            return new CellRect(
                rect.minX - margin,
                rect.minZ - margin,
                rect.Width + margin * 2,
                rect.Height + margin * 2);
        }

        private static CellRect BoundingRect(CellRect first, CellRect second)
        {
            int minX = first.minX < second.minX ? first.minX : second.minX;
            int minZ = first.minZ < second.minZ ? first.minZ : second.minZ;
            int maxX = first.maxX > second.maxX ? first.maxX : second.maxX;
            int maxZ = first.maxZ > second.maxZ ? first.maxZ : second.maxZ;

            return new CellRect(
                minX,
                minZ,
                maxX - minX + 1,
                maxZ - minZ + 1);
        }
    }
}
