using System;

namespace OpenTTD.Testing
{
    internal static class AIMap
    {
        internal static Func<TileIndex, bool> IsValidTile { get; set; }
        
        internal static Func<int, int, TileIndex> GetTileIndex { get; set; }

        internal static Func<TileIndex, int> GetTileX { get; set; }

        internal static Func<TileIndex, int> GetTileY { get; set; }
    }
}