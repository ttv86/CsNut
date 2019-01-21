using System;

namespace OpenTTD.Testing
{
    internal static class AITile
    {
        internal static Func<TileIndex, bool> IsBuildable { get; set; }

        internal static Func<TileIndex, bool> IsWaterTile { get; set; }

        internal static Func<TileIndex, bool> IsCoastTile { get; set; }

        internal static Func<TileIndex, bool> IsStationTile { get; set; }

        internal static Func<Slope, bool> IsSteepSlope { get; set; }

        internal static Func<Slope, bool> IsHalftileSlope { get; set; }

        internal static Func<TileIndex, bool> HasTreeOnTile { get; set; }

        internal static Func<TileIndex, bool> IsFarmTile { get; set; }

        internal static Func<TileIndex, bool> IsRockTile { get; set; }

        internal static Func<TileIndex, bool> IsRoughTile { get; set; }

        internal static Func<TileIndex, bool> IsSnowTile { get; set; }

        internal static Func<TileIndex, bool> IsDesertTile { get; set; }

        internal static Func<TileIndex, TerrainType> GetTerrainType { get; set; }

        internal static Func<TileIndex, Slope> GetSlope { get; set; }
    }
}
