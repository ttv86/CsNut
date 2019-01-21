using OpenTTD;

namespace CsAi
{
    public static class RailStationBuilder
    {
        public static RailStationBuildResult BuildStationNear(TileIndex tile, int platformLength, int platformCount = 2)
        {
            var stationTile = RailStationBuilder.FindPlaceForStation(tile, platformLength, platformCount);
            if (stationTile != null)
            {
                AILog.Info("Build " + stationTile.tile + ", " + stationTile.direction);
                var good = AIRail.BuildRailStation(stationTile.tile, stationTile.direction, platformCount, platformLength, AIStation.STATION_NEW);
                if (good)
                {
                    TileIndex entryTile;
                    int[] matrix;
                    if (stationTile.direction == AIRail.RAILTRACK_NW_SE)
                    {
                        if (stationTile.entryNearTopCorner)
                        {
                            /*   \
                                \/\
                                 \/   */
                            entryTile = stationTile.tile + AIMap.GetTileIndex(0, 0);
                            matrix = new int[] { -1, 0, 0, -1 };
                        }
                        else
                        {
                            /*  /\
                                \/\
                                 \   */
                            entryTile = stationTile.tile + AIMap.GetTileIndex(1, platformLength - 1);
                            matrix = new int[] { 1, 0, 0, 1 };
                        }
                    }
                    else if (stationTile.direction == AIRail.RAILTRACK_NE_SW)
                    {
                        if (stationTile.entryNearTopCorner)
                        {
                            /*   /
                                /\/
                                \/   */
                            entryTile = stationTile.tile + AIMap.GetTileIndex(0, 1);
                            matrix = new int[] { 0, -1, 1, 0 };
                        }
                        else
                        {
                            /*   /\
                                /\/
                                 /    */
                            entryTile = stationTile.tile + AIMap.GetTileIndex(platformLength - 1, 0);
                            matrix = new int[] { 0, 1, -1, 0 };
                        }
                    }
                    else
                    {
                        return null;
                    }

                    /* |      |
                       +00-01-+
                        02 03
                     04 05 06 07
                        08 09
                        10 11

                    */

                    var tiles = new TileIndex[] {
                        entryTile + RailStationBuilder.GetTransformedTileIndex(0, 0, matrix),
                        entryTile + RailStationBuilder.GetTransformedTileIndex(-1, 0, matrix),

                        entryTile + RailStationBuilder.GetTransformedTileIndex(0, 1, matrix),
                        entryTile + RailStationBuilder.GetTransformedTileIndex(-1, 1, matrix),

                        entryTile + RailStationBuilder.GetTransformedTileIndex(1, 2, matrix),
                        entryTile + RailStationBuilder.GetTransformedTileIndex(0, 2, matrix),
                        entryTile + RailStationBuilder.GetTransformedTileIndex(-1, 2, matrix),
                        entryTile + RailStationBuilder.GetTransformedTileIndex(-2, 2, matrix),

                        entryTile + RailStationBuilder.GetTransformedTileIndex(0, 3, matrix),
                        entryTile + RailStationBuilder.GetTransformedTileIndex(-1, 3, matrix),

                        entryTile + RailStationBuilder.GetTransformedTileIndex(0, 4, matrix),
                        entryTile + RailStationBuilder.GetTransformedTileIndex(-1, 4, matrix),
                    };

                    // Exit rail
                    AIRail.BuildRail(
                        tiles[0],
                        tiles[2],
                        tiles[10]);

                    // Entry rail
                    AIRail.BuildRail(
                        tiles[1],
                        tiles[3],
                        tiles[11]);

                    // Signals
                    AIRail.BuildSignal(tiles[2], tiles[0], AIRail.SIGNALTYPE_PBS);
                    AIRail.BuildSignal(tiles[3], tiles[1], AIRail.SIGNALTYPE_PBS);
                    AIRail.BuildSignal(tiles[9], tiles[11], AIRail.SIGNALTYPE_PBS_ONEWAY);

                    //Depot
                    TileIndex depotTile = AIMap.TILE_INVALID;
                    if (AIRail.BuildRailDepot(tiles[4], tiles[5]))
                    {
                        depotTile = tiles[4];
                        RailStationBuilder.BuildIntersection(tiles[2], tiles[4], tiles[6], tiles[8]);
                        RailStationBuilder.BuildIntersection(tiles[3], tiles[5], tiles[9]);
                    }
                    else
                    {
                        if (AIRail.BuildRailDepot(tiles[7], tiles[6]))
                        {
                            depotTile = tiles[7];
                            RailStationBuilder.BuildIntersection(tiles[3], tiles[5], tiles[7], tiles[9]);
                            RailStationBuilder.BuildIntersection(tiles[2], tiles[6], tiles[8]);
                        }
                    }

                    return new RailStationBuildResult()
                    {
                        StationID = AIStation.GetStationID(stationTile.tile),
                        ExitCloser = tiles[8],
                        ExitFarther = tiles[10],
                        EntryCloser = tiles[9],
                        EntryFarther = tiles[11],
                        DepotTile = depotTile
                    };
                }
            }

            return null;
        }

        private static RailStationInfo FindPlaceForStation(TileIndex tile, int platformLength, int platformCount)
        {
            var xx = AIMap.GetTileX(tile);
            var yy = AIMap.GetTileY(tile);
            for (var step = 1; step < 15; step++)
            {
                for (var i = 0; i < step * 2; i++)
                {
                    var x = xx - step;
                    var y = yy + Helper.Alternate(i);
                    var tile2 = AIMap.GetTileIndex(x, y);
                    var test = RailStationBuilder.TestPlaceForStation(tile2, platformLength, platformCount);
                    if (test != null)
                    {
                        return test;
                    }

                    x = xx - Helper.Alternate(i);
                    y = yy + step;
                    tile2 = AIMap.GetTileIndex(x, y);
                    test = RailStationBuilder.TestPlaceForStation(tile2, platformLength, platformCount);
                    if (test != null)
                    {
                        return test;
                    }

                    x = xx + step;
                    y = yy + Helper.Alternate(i);
                    tile2 = AIMap.GetTileIndex(x, y);
                    test = RailStationBuilder.TestPlaceForStation(tile2, platformLength, platformCount);
                    if (test != null)
                    {
                        return test;
                    }

                    x = xx + Helper.Alternate(i);
                    y = yy - step;
                    tile2 = AIMap.GetTileIndex(x, y);
                    test = RailStationBuilder.TestPlaceForStation(tile2, platformLength, platformCount);
                    if (test != null)
                    {
                        return test;
                    }
                }
            }

            return null;
        }

        private static RailStationInfo TestPlaceForStation(TileIndex tile, int platformLength, int platformCount)
        {
            var buffer = 4;
            var clearLength = platformLength;
            var clearWidth = platformCount;

            var tile2 = tile - AIMap.GetTileIndex(clearWidth / 2, clearLength / 2);
            if (RailStationBuilder.IsClear(tile2, clearWidth, clearLength + buffer))
            {
                return new RailStationInfo() { tile = tile - AIMap.GetTileIndex(clearWidth / 2, clearLength / 2), direction = AIRail.RAILTRACK_NW_SE, entryNearTopCorner = false };
            }

            tile2 = tile - AIMap.GetTileIndex(clearLength / 2, clearWidth / 2);
            if (RailStationBuilder.IsClear(tile2, clearLength + buffer, clearWidth))
            {
                return new RailStationInfo() { tile = tile - AIMap.GetTileIndex(clearLength / 2, clearWidth / 2), direction = AIRail.RAILTRACK_NE_SW, entryNearTopCorner = false };
            }

            tile2 = tile - AIMap.GetTileIndex(clearWidth / 2, (clearLength / 2) + buffer);
            if (RailStationBuilder.IsClear(tile2, clearWidth, clearLength + buffer))
            {
                return new RailStationInfo() { tile = tile - AIMap.GetTileIndex(clearWidth / 2, clearLength / 2), direction = AIRail.RAILTRACK_NW_SE, entryNearTopCorner = true };
            }

            tile2 = tile - AIMap.GetTileIndex((clearLength / 2) + buffer, clearWidth / 2);
            if (RailStationBuilder.IsClear(tile2, clearLength + buffer, clearWidth))
            {
                return new RailStationInfo() { tile = tile - AIMap.GetTileIndex(clearLength / 2, clearWidth / 2), direction = AIRail.RAILTRACK_NE_SW, entryNearTopCorner = true };
            }

            return null;
        }

        private static bool IsClear(TileIndex topCorner, int width, int height)
        {
            if (!AITile.IsBuildableRectangle(topCorner, width, height))
            {
                return false;
            }

            var xx = AIMap.GetTileX(topCorner);
            var yy = AIMap.GetTileY(topCorner);
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var testTile = topCorner + AIMap.GetTileIndex(x, y);
                    if (!AIMap.IsValidTile(testTile))
                    {
                        return false;
                    }

                    var slope = AITile.GetSlope(testTile);
                    if (slope != AITile.SLOPE_FLAT)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static void BuildIntersection(TileIndex tile1, TileIndex tile2, TileIndex tile3, TileIndex tile4 = null)
        {
            var tile1x = AIMap.GetTileX(tile1);
            var tile1y = AIMap.GetTileY(tile1);
            var tile2x = AIMap.GetTileX(tile2);
            var tile2y = AIMap.GetTileY(tile2);
            var tile3x = AIMap.GetTileX(tile3);
            var tile3y = AIMap.GetTileY(tile3);

            var x = (int)((tile1x + tile2x + tile3x) / 3.0 + 0.5);
            var y = (int)((tile1y + tile2y + tile3y) / 3.0 + 0.5);

            var center = AIMap.GetTileIndex(x, y);

            var nwTile = AIMap.GetTileIndex(x, y - 1);
            var neTile = AIMap.GetTileIndex(x - 1, y);
            var swTile = AIMap.GetTileIndex(x + 1, y);
            var seTile = AIMap.GetTileIndex(x, y + 1);

            if (tile4 != null)
            {
                AIRail.BuildRailTrack(center, AIRail.RAILTRACK_NE_SW); // /
                AIRail.BuildRailTrack(center, AIRail.RAILTRACK_NW_SE); // \
                AIRail.BuildRailTrack(center, AIRail.RAILTRACK_NW_NE); // - (upper)
                AIRail.BuildRailTrack(center, AIRail.RAILTRACK_SW_SE); // - (lower)
                AIRail.BuildRailTrack(center, AIRail.RAILTRACK_NW_SW); // | left
                AIRail.BuildRailTrack(center, AIRail.RAILTRACK_NE_SE); // | right
                return;
            }

            if ((tile1 != nwTile) && (tile2 != nwTile) && (tile3 != nwTile))
            {
                AIRail.BuildRailTrack(center, AIRail.RAILTRACK_NE_SW); // /
                AIRail.BuildRailTrack(center, AIRail.RAILTRACK_SW_SE); // - (lower)
                AIRail.BuildRailTrack(center, AIRail.RAILTRACK_NE_SE); // | right
                return;
            }

            if ((tile1 != neTile) && (tile2 != neTile) && (tile3 != neTile))
            {
                AIRail.BuildRailTrack(center, AIRail.RAILTRACK_NW_SE); // \
                AIRail.BuildRailTrack(center, AIRail.RAILTRACK_SW_SE); // - (lower)
                AIRail.BuildRailTrack(center, AIRail.RAILTRACK_NW_SW); // | left
                return;
            }

            if ((tile1 != swTile) && (tile2 != swTile) && (tile3 != swTile))
            {
                AIRail.BuildRailTrack(center, AIRail.RAILTRACK_NW_SE); // \
                AIRail.BuildRailTrack(center, AIRail.RAILTRACK_NW_NE); // - (upper)
                AIRail.BuildRailTrack(center, AIRail.RAILTRACK_NE_SE); // | right
                return;
            }

            if ((tile1 != seTile) && (tile2 != seTile) && (tile3 != seTile))
            {
                AIRail.BuildRailTrack(center, AIRail.RAILTRACK_NE_SW); // /
                AIRail.BuildRailTrack(center, AIRail.RAILTRACK_NW_NE); // - (upper)
                AIRail.BuildRailTrack(center, AIRail.RAILTRACK_NW_SW); // | left
                return;
            }
        }

        private static TileIndex GetTransformedTileIndex(int x, int y, int[] matrix)
        {
            return AIMap.GetTileIndex(x * matrix[0] + y * matrix[1], x * matrix[2] + y * matrix[3]);
        }
    }
}