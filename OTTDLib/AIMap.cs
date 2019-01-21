namespace OpenTTD
{
    /// <summary>Class that handles all map related functions.</summary>
    public static class AIMap
    {
        public static readonly TileIndex TILE_INVALID;

        /**
         * Checks whether the given tile is valid.
         * @param tile The tile to check.
         * @returns True is the tile it within the boundaries of the map.
         */
        public static bool IsValidTile(TileIndex tile) { return Testing.AIMap.IsValidTile(tile); }

        /// <summary>Gets the number of tiles in the map.</summary><returns>The size of the map in tiles.</returns>
        public static int GetMapSize() { throw null; }

        /// <summary>Gets the amount of tiles along the SW and NE border.</summary><returns>The length along the SW and NE borders.</returns>
        public static int GetMapSizeX() { throw null; }

        /// <summary>Gets the amount of tiles along the SE and NW border.</summary><returns>The length along the SE and NW borders.</returns>
        public static int GetMapSizeY() { throw null; }

        /**
         * Gets the place along the SW/NE border (X-value).
         * @param tile The tile to get the X-value of.
         * @returns The X-value.
         */
        public static int GetTileX(TileIndex tile) { return Testing.AIMap.GetTileX(tile); }

        /**
         * Gets the place along the SE/NW border (Y-value).
         * @param tile The tile to get the Y-value of.
         * @returns The Y-value.
         */
        public static int GetTileY(TileIndex tile) { return Testing.AIMap.GetTileY(tile); }

        /**
         * Gets the TileIndex given a x,y-coordinate.
         * @param x The X coordinate.
         * @param y The Y coordinate.
         * @returns The TileIndex for the given (x,y) coordinate.
         */
        public static TileIndex GetTileIndex(int x, int y) { return Testing.AIMap.GetTileIndex(x, y); }

        /**
         * Calculates the Manhattan distance; the difference of
         *  the X and Y added together.
         * @param tile_from The start tile.
         * @param tile_to The destination tile.
         * @returns The Manhattan distance between the tiles.
         */
        public static int DistanceManhattan(TileIndex tile_from, TileIndex tile_to) { throw null; }

        /**
         * Calculates the distance between two tiles via 1D calculation.
         *  This means the distance between X or the distance between Y, depending
         *  on which one is bigger.
         * @param tile_from The start tile.
         * @param tile_to The destination tile.
         * @returns The maximum distance between the tiles.
         */
        public static int DistanceMax(TileIndex tile_from, TileIndex tile_to) { throw null; }

        /**
         * The squared distance between the two tiles.
         *  This is the distance is the length of the shortest straight line
         *  between both points.
         * @param tile_from The start tile.
         * @param tile_to The destination tile.
         * @returns The squared distance between the tiles.
         */
        public static int DistanceSquare(TileIndex tile_from, TileIndex tile_to) { throw null; }

        /**
         * Calculates the shortest distance to the edge.
         * @param tile From where the distance has to be calculated.
         * @returns The distances to the closest edge.
         */
        public static int DistanceFromEdge(TileIndex tile) { throw null; }
    }
}