using System;
using System.Collections.Generic;
using OpenTTD;
using OpenTTD.Imports;

namespace CsAi
{
    internal static class RoadBuilder
    {
        internal static bool BuildRoad(TileIndex previous, TileIndex from, TileIndex to, TileIndex next, HashSet<TileIndex> forbidden, Action<TileIndex, string> sign = null)
        {
            var path = RoadBuilder.FindPath(from, to, previous, forbidden);
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
                            AILog.Error($"Length is: {path[i].Length}. ([" + AIMap.GetTileX(p) + ", " + AIMap.GetTileY(p) + "] via [" + AIMap.GetTileX(c) + ", " + AIMap.GetTileY(c) + "] to [" + AIMap.GetTileX(n) + ", " + AIMap.GetTileY(n) + "])");
                            continue;
                            //throw new Exception("Should not be Road");
                        }

                        if (nt != BuildType.Basic)
                        {
                            continue;
                        }

                        //AILog.Info("Build a Road  from [" + AIMap.GetTileX(p) + ", " + AIMap.GetTileY(p) + "] via [" + AIMap.GetTileX(c) + ", " + AIMap.GetTileY(c) + "] to [" + AIMap.GetTileX(n) + ", " + AIMap.GetTileY(n) + "].");
                        good = AIRoad.BuildRoad(p, c);
                        good = AIRoad.BuildRoad(c, n);
                        break;
                    case BuildType.Bridge:
                        var bridgeTypes = new AIBridgeList_Length(path[i].Length);
                        //AILog.Info("Build a bridge " + bridgeTypes.Begin() + " from [" + AIMap.GetTileX(c) + ", " + AIMap.GetTileY(c) + "] to [" + AIMap.GetTileX(n) + ", " + AIMap.GetTileY(n) + "].");
                        good = AIBridge.BuildBridge(AIVehicle.VT_ROAD, bridgeTypes.Begin(), c, n);
                        if ((!good) && (sign != null))
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
                    var reason = AIError.GetLastErrorString();
                    if (reason != "ERR_ALREADY_BUILT")
                    {
                        AILog.Error("Failed to build on [" + AIMap.GetTileX(c) + ", " + AIMap.GetTileY(c) + "]. Reason: " + reason);
                    }
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
            cameFrom[start] = new PathInfo(start, 1, 0, BuildType.Basic, previous != null ? new PathInfo(previous, 1, 0, BuildType.Basic, null) : null);

            while (tilesToProcess.Count() > 0)
            {
                var current = tilesToProcess.Pop();
                //AILog.Info($"Processing: {Helper.FormatTile(current)}");
                if (current == goal)
                {
                    // We found the target.
                    return RoadBuilder.BuildFinalPath(cameFrom[current]);
                }

                var neighbors = RoadBuilder.GetNeighbors(current, cameFrom[current]);
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
            var oldDir = (cameFrom.Previous != null) ? Helper.GetDirection(cameFrom.Previous.Tile, tile) : Direction.None;

            foreach (var dir in dirs)
            {
                var x = dir[0];
                var y = dir[1];
                if (cameFrom.Tile == tile + AIMap.GetTileIndex(x, y))
                {
                    continue;
                }

                var newDir = Helper.GetDirection(tile, tile + AIMap.GetTileIndex(x, y));
                
                var isCoast = AITile.IsCoastTile(tile);
                var straight = newDir == oldDir;
                var maxLength = 20;

                bool lastWasExistingBridge = false;

                TileIndex neighbor;
                for (var length = 1; AIMap.IsValidTile(neighbor = tile + AIMap.GetTileIndex(x * length, y * length)) && length <= maxLength; length++)
                {
                    if (AIBridge.IsBridgeTile(neighbor))
                    {
                        var otherEnd = AIBridge.GetOtherBridgeEnd(neighbor);
                        var bridgeDir = Helper.GetDirection(neighbor, otherEnd);

                        if (newDir == bridgeDir)
                        {
                            length += AIMap.DistanceManhattan(neighbor, otherEnd);
                            lastWasExistingBridge = true;
                            continue;
                        }
                    }
                    else if (AIRoad.IsRoadTile(neighbor) && (lastWasExistingBridge || (length == 1)))
                    {
                        result.Add(new PathInfo(
                            neighbor,
                            length,
                            oldcost + (length * 0.5),
                            length > 1 ? BuildType.Bridge : BuildType.Basic,
                            cameFrom
                        ));
                        break;
                    }

                    lastWasExistingBridge = false;
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
                        if (isCoast)
                        {
                            result.Add(new PathInfo(
                                neighbor,
                                length,
                                oldcost + ((length * multiplier * 2)),
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
                                oldcost + ((length * multiplier)),
                                length == 1 ? BuildType.Basic : BuildType.Bridge,
                                cameFrom
                            ));

                            break;
                        }
                    }
                    else if (!(
                      (AIRoad.IsRoadTile(neighbor) || AIRoad.IsRoadTile(neighbor) || AITile.IsWaterTile(neighbor)) &&
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
    }
}