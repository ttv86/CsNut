using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTTD;

namespace CsAi
{
    public static class RoadStationBuilder
    {
        public static RoadStationInfo FindPlaceForStation(TileIndex tile)
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
                    var test = RoadStationBuilder.TestPlaceForStation(tile2);
                    if (test != null)
                    {
                        return test;
                    }

                    x = xx - Helper.Alternate(i);
                    y = yy + step;
                    tile2 = AIMap.GetTileIndex(x, y);
                    test = RoadStationBuilder.TestPlaceForStation(tile2);
                    if (test != null)
                    {
                        return test;
                    }

                    x = xx + step;
                    y = yy + Helper.Alternate(i);
                    tile2 = AIMap.GetTileIndex(x, y);
                    test = RoadStationBuilder.TestPlaceForStation(tile2);
                    if (test != null)
                    {
                        return test;
                    }

                    x = xx + Helper.Alternate(i);
                    y = yy - step;
                    tile2 = AIMap.GetTileIndex(x, y);
                    test = RoadStationBuilder.TestPlaceForStation(tile2);
                    if (test != null)
                    {
                        return test;
                    }
                }
            }

            return null;
        }

        private static RoadStationInfo TestPlaceForStation(TileIndex tile)
        {
            if (AITile.IsBuildable(tile) && (AITile.GetSlope(tile) == AITile.SLOPE_FLAT))
            {
                var other = tile + AIMap.GetTileIndex(0, 1);
                if (AIRoad.IsRoadTile(other) && (AITile.GetSlope(other) == AITile.SLOPE_FLAT)) {
                    return new RoadStationInfo() { tile = tile, entryPoint = other };
                }

                other = tile + AIMap.GetTileIndex(1, 0);
                if (AIRoad.IsRoadTile(other) && (AITile.GetSlope(other) == AITile.SLOPE_FLAT))
                {
                    return new RoadStationInfo() { tile = tile, entryPoint = other };
                }

                other = tile + AIMap.GetTileIndex(0, -1);
                if (AIRoad.IsRoadTile(other) && (AITile.GetSlope(other) == AITile.SLOPE_FLAT))
                {
                    return new RoadStationInfo() { tile = tile, entryPoint = other };
                }

                other = tile + AIMap.GetTileIndex(-1, 0);
                if (AIRoad.IsRoadTile(other) && (AITile.GetSlope(other) == AITile.SLOPE_FLAT))
                {
                    return new RoadStationInfo() { tile = tile, entryPoint = other };
                }
            }

            return null;
        }
    }
}
