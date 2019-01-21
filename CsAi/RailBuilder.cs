using System;
using System.Collections.Generic;
using OpenTTD;
using OpenTTD.Imports;

namespace CsAi
{
    internal enum BuildType
    {
        Basic = 0,
        Bridge = 1,
        Tunnel = 2
    }

    internal static class RailBuilder
    {

        internal static bool BuildRail(TileIndex previous, TileIndex from, TileIndex to, TileIndex next, HashSet<TileIndex> forbidden, int railCount = 1, Action<TileIndex, string> sign = null)
        {
            var path = RailBuilder.FindPath(from, to, previous, forbidden, sign);
            if (path == null)
            {
                return false;
            }

            for (var i = 0; i < path.Count - 1; i++)
            {
                var p = i == 0 ? next : path[i - 1].Tile;
                var c = path[i].Tile;
                var n = i == path.Count - 1 ? previous : path[i + 1].Tile;
                var nt = i == 0 ? BuildType.Basic : path[i - 1].Type;
                //sign(c, "[" + AIMap.GetTileX(c) + ", " + AIMap.GetTileY(c) + "] " + path[i].type);
                bool good = false;
                switch (path[i].Type)
                {
                    case BuildType.Basic:
                        if (path[i].Length > 1)
                        {
                            throw new Exception("Should not be rail");
                        }

                        if (nt != BuildType.Basic)
                        {
                            continue;
                        }

                        //AILog.Info("Build a rail  from [" + AIMap.GetTileX(p) + ", " + AIMap.GetTileY(p) + "] via [" + AIMap.GetTileX(c) + ", " + AIMap.GetTileY(c) + "] to [" + AIMap.GetTileX(n) + ", " + AIMap.GetTileY(n) + "].");
                        good = AIRail.BuildRail(p, c, n);
                        break;
                    case BuildType.Bridge:
                        var bridgeTypes = new AIBridgeList_Length(path[i].Length);
                        //AILog.Info("Build a bridge " + bridgeTypes.Begin() + " from [" + AIMap.GetTileX(c) + ", " + AIMap.GetTileY(c) + "] to [" + AIMap.GetTileX(n) + ", " + AIMap.GetTileY(n) + "].");
                        good = AIBridge.BuildBridge(AIVehicle.VT_RAIL, bridgeTypes.Begin(), c, n);
                        if (!good)
                        {
                            sign(p, "s");
                            sign(c, "e");
                        }

                        break;
                    case BuildType.Tunnel:
                        throw new Exception("Tunnels not supported");
                }

                if (!good)
                {
                    AILog.Error("Failed to build on [" + AIMap.GetTileX(c) + ", " + AIMap.GetTileY(c) + "]. Reason: " + AIError.GetLastErrorString());
                }
            }

            return true;
        }

        /// <summary>
        /// Uses A*-algorithm to find the best route between two tiles.
        /// </summary>
        /// <param name="start">First tile to search from.</param>
        /// <param name="goal">Goal to find.</param>
        /// <param name="previous">Tile just before the first tile.</param>
        /// <param name="forbidden">List of tiles that should be never used on route.</param>
        /// <param name="sign">Optional function for building signs.</param>
        /// <returns></returns>
        internal static List<PathInfo> FindPath(TileIndex start, TileIndex goal, TileIndex previous, HashSet<TileIndex> forbidden = null, Action<TileIndex, string> sign = null)
        {
            // Nodes to evaluate
            var tilesToProcess = new FibonacciHeap<TileIndex>();
            tilesToProcess.Insert(start, 0);

            // Nodes evaluated
            var cameFrom = new Dictionary<TileIndex, PathInfo>();
            cameFrom[start] = new PathInfo(start, 1, 0, BuildType.Basic, new PathInfo(previous, 1, 0, BuildType.Basic, null));

            while (tilesToProcess.Count() > 0)
            {
                var current = tilesToProcess.Pop();
                if (current == goal)
                {
                    // We found the target.
                    return RailBuilder.BuildFinalPath(cameFrom[current]);
                }

                var neighbors = RailBuilder.GetNeighbors(current, cameFrom[current]);
                foreach (var neighborItem in neighbors)
                {
                    var neighbor = neighborItem.Tile;
                    if ((neighbor == previous) || ((forbidden != null) && forbidden.Contains(neighbor)))
                    {
                        // We can't go here.
                        continue;
                    }

                    var neighborDist = neighborItem.Length;
                    if (!cameFrom.ContainsKey(neighbor))
                    {
                        tilesToProcess.Insert(neighbor, neighborItem.Cost);
                    }
                    else
                    {
                        if (neighborItem.Cost >= cameFrom[neighbor].Cost)
                        {
                            continue;
                        }
                    }

                    sign?.Invoke(neighbor, neighborItem.Cost.ToString());
                    cameFrom[neighbor] = neighborItem;
                }
            }

            return null;
        }

        private static List<PathInfo> BuildFinalPath(PathInfo cameFrom)
        {
            var total_path = new List<PathInfo>();
            var c = cameFrom;
            while (c != null)
            {
                //AILog.Info($"- [{AIMap.GetTileX(c.Tile)}, {AIMap.GetTileY(c.Tile)}]: {c.Type} {c.Length}");
                total_path.Add(c);
                c = c.Previous;
            }

            return total_path;
        }

        internal static List<PathInfo> GetNeighbors(TileIndex tile, PathInfo cameFrom, Action<TileIndex, string> sign = null)
        {
            var oldcost = cameFrom.Cost;
            var result = new List<PathInfo>();
            var isFlat = AITile.GetSlope(tile) == AITile.SLOPE_FLAT;
            var dirs = new int[][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };
            var oldDir = Helper.GetDirection(tile, cameFrom.Previous.Tile);

            foreach (var dir in dirs)
            {
                var x = dir[0];
                var y = dir[1];
                if (cameFrom.Tile == tile + AIMap.GetTileIndex(x, y))
                {
                    continue;
                }

                var newDir = Helper.GetDirection(tile + AIMap.GetTileIndex(x, y), tile);

                var isCoast = AITile.IsCoastTile(tile);
                var straight = newDir == oldDir;
                var maxLength = isCoast ? 20 : (straight ? 5 : 1);

                TileIndex neighbor;
                for (var length = 1; AIMap.IsValidTile(neighbor = tile + AIMap.GetTileIndex(x * length, y * length)) && length <= maxLength; length++)
                {
                    if (isCoast)
                    {
                        if (!straight)
                        {
                            break;
                        }

                        if (AITile.IsWaterTile(neighbor))
                        {
                            continue;
                        }
                    }

                    double multiplier = 1;
                    if (AITile.IsFarmTile(neighbor) || AITile.IsRockTile(neighbor) || AITile.IsRoughTile(tile))
                    {
                        // Make farms, rocks, etc more expensive.
                        multiplier *= 1.1;
                    }

                    if (AITile.IsBuildable(neighbor))
                    {
                        double angleFactor = RailBuilder.CalculateAngle(neighbor, tile, cameFrom.Previous);
                        if (isCoast)
                        {
                            result.Add(new PathInfo(
                                neighbor,
                                length,
                                oldcost + ((length * multiplier * 2) + angleFactor),
                                BuildType.Bridge,
                                cameFrom
                            ));

                            break;
                        }
                        else if ((isFlat && cameFrom.Length == 1) || ((length == 1) && straight))
                        {
                            result.Add(new PathInfo(
                                neighbor,
                                length,
                                oldcost + ((length * multiplier) + angleFactor),
                                length == 1 ? BuildType.Basic : BuildType.Bridge,
                                cameFrom
                            ));

                            break;
                        }
                    }
                    else if (!(
                      (AIRail.IsRailTile(neighbor) || AIRoad.IsRoadTile(neighbor) || AITile.IsWaterTile(neighbor)) &&
                      (AITile.GetSlope(neighbor) == AITile.SLOPE_FLAT) &&
                      !AIStation.IsValidStation(AIStation.GetStationID(neighbor))))
                    {
                        // Can't built over
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Get multiplier for the angle. Lower is better.
        /// </summary>
        /// <param name="tile1"></param>
        /// <param name="cameFrom"></param>
        /// <returns></returns>
        internal static double CalculateAngle(TileIndex tile1, TileIndex tile2, PathInfo cameFrom)
        {
            if ((cameFrom == null) || (cameFrom.Previous == null))
            {
                // Not enough data available. Allow everything.
                return 0;
            }

            Direction a = Helper.GetDirection(tile1, tile2);
            Direction b = Direction.None;
            int counter = 0;
            TileIndex current = tile2;
            PathInfo path = cameFrom;
            while (path != null)
            {
                var dir = Helper.GetDirection(current, path.Tile);
                if (counter == 0)
                {
                    b = dir;
                }
                else
                {
                    if (dir != (counter % 2 == 0 ? b : a))
                    {
                        if ((counter == 1) && (a != b) && (dir != a) && (dir != b))
                        {
                            return 100;
                        }

                        return 1d / counter;
                    }
                }

                current = path.Tile;
                path = path.Previous;
                counter++;
            }

            return 0;
        }
    }

    internal class PathInfo
    {
        internal PathInfo(TileIndex tile, int length, double cost, BuildType type, PathInfo previous)
        {
            this.Tile = tile;
            this.Length = length;
            this.Cost = cost;
            this.Type = type;
            this.Previous = previous;
        }

        internal TileIndex Tile { get; }

        internal int Length { get; }

        internal double Cost { get; }

        internal BuildType Type { get; }

        internal PathInfo Previous { get; }
    }
}