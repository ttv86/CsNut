using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTTD.Testing
{
    public sealed class TestWorld : IDisposable
    {
        public TestWorld(int sizeX, int sizeY)
        {
            Action<object> debug = m => { Debug.WriteLine(m); };
            Action<object> noop1 = _ => { };
            Func<object, bool> alwaysTrue = _ => true;
            Func<object, bool> alwaysFalse = _ => false;

            AILog.Info = debug;
            AILog.Warning = debug;
            AILog.Error = debug;

            AIMap.IsValidTile = (tile) => (tile.Index >= 0) && (tile.Index < (sizeX * sizeY));
            AIMap.GetTileIndex = (x, y) => new TileIndex(y * sizeY + x);
            AIMap.GetTileX = (tile) => tile.Index % sizeX;
            AIMap.GetTileY = (tile) => tile.Index / sizeX;
            AITile.GetSlope = _ => OpenTTD.AITile.SLOPE_FLAT;
            AITile.IsCoastTile = alwaysFalse;
            AITile.IsWaterTile = alwaysFalse;
            AITile.IsFarmTile = alwaysFalse;
            AITile.IsRockTile = alwaysFalse;
            AITile.IsRoughTile = alwaysFalse;
            AITile.IsBuildable = alwaysTrue;
            AIRail.IsRailTile = alwaysFalse;
            AIRoad.IsRoadTile = alwaysFalse;
            AIStation.IsValidStation = alwaysFalse;
        }

        public void Dispose()
        {
        }
    }
}
