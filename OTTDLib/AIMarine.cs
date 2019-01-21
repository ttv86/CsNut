namespace OpenTTD
{
    /// <summary>Class that handles all marine related functions.</summary>
    public static class AIMarine
    {
        /// <summary>Base for marine related errors </summary>
        public static readonly ErrorMessage ERR_MARINE_BASE;

        /// <summary>Infrastructure must be built on water </summary>
        public static readonly ErrorMessage ERR_MARINE_MUST_BE_BUILT_ON_WATER;

        /// <summary>Types of water-related objects in the game.</summary>
        public enum BuildType
        {
            BT_DOCK,
            BT_DEPOT,
            BT_BUOY,
        }

        /**
         * Checks whether the given tile is actually a tile with a water depot.
         * @param tile The tile to check.
         * @returns True if and only if the tile has a water depot.
         */
        public static bool IsWaterDepotTile(TileIndex tile) { throw null; }

        /**
         * Checks whether the given tile is actually a tile with a dock.
         * @param tile The tile to check.
         * @returns True if and only if the tile has a dock.
         */
        public static bool IsDockTile(TileIndex tile) { throw null; }

        /**
         * Checks whether the given tile is actually a tile with a buoy.
         * @param tile The tile to check.
         * @returns True if and only if the tile has a buoy.
         */
        public static bool IsBuoyTile(TileIndex tile) { throw null; }

        /**
         * Checks whether the given tile is actually a tile with a lock.
         * @param tile The tile to check.
         * @returns True if and only if the tile has a lock.
         */
        public static bool IsLockTile(TileIndex tile) { throw null; }

        /**
         * Checks whether the given tile is actually a tile with a canal.
         * @param tile The tile to check.
         * @returns True if and only if the tile has a canal.
         */
        public static bool IsCanalTile(TileIndex tile) { throw null; }

        /**
         * Checks whether the given tiles are directly connected, i.e. whether
         *  a ship vehicle can travel from the center of the first tile to the
         *  center of the second tile.
         * @param tile_from The source tile.
         * @param tile_to The destination tile.
         * @returns True if and only if a ship can go from tile_from to tile_to.
         */
        public static bool AreWaterTilesConnected(TileIndex tile_from, TileIndex tile_to) { throw null; }

        /**
         * Builds a water depot on tile.
         * @param tile The tile where the water depot will be build.
         * @param front A tile on the same axis with 'tile' as the depot shall be oriented.
         * @exception AIError.ERR_AREA_NOT_CLEAR
         * @exception AIError.ERR_SITE_UNSUITABLE
         * @exception AIMarine.ERR_MARINE_MUST_BE_BUILT_ON_WATER
         * @returns Whether the water depot has been/can be build or not.
         * @note A WaterDepot is 1 tile in width, and 2 tiles in length.
         * @note The depot will be built towards the south from 'tile', not necessarily towards 'front'.
         */
        public static bool BuildWaterDepot(TileIndex tile, TileIndex front) { throw null; }

        /**
         * Builds a dock where tile is the tile still on land.
         * @param tile The tile still on land of the dock.
         * @param station_id The station to join, AIStation.STATION_NEW or AIStation.STATION_JOIN_ADJACENT.
         * @exception AIError.ERR_AREA_NOT_CLEAR
         * @exception AIError.ERR_SITE_UNSUITABLE
         * @exception AIStation.ERR_STATION_TOO_CLOSE_TO_ANOTHER_STATION
         * @exception AIStation.ERR_STATION_TOO_MANY_STATIONS
         * @returns Whether the dock has been/can be build or not.
         */
        public static bool BuildDock(TileIndex tile, StationID station_id) { throw null; }

        /**
         * Builds a buoy on tile.
         * @param tile The tile where the buoy will be build.
         * @exception AIError.ERR_AREA_NOT_CLEAR
         * @exception AIError.ERR_SITE_UNSUITABLE
         * @exception AIStation.ERR_STATION_TOO_MANY_STATIONS
         * @returns Whether the buoy has been/can be build or not.
         */
        public static bool BuildBuoy(TileIndex tile) { throw null; }

        /**
         * Builds a lock on tile.
         * @param tile The tile where the lock will be build.
         * @exception AIError.ERR_LAND_SLOPED_WRONG
         * @exception AIError.ERR_SITE_UNSUITABLE
         * @returns Whether the lock has been/can be build or not.
         */
        public static bool BuildLock(TileIndex tile) { throw null; }

        /**
         * Builds a canal on tile.
         * @param tile The tile where the canal will be build.
         * @exception AIError.ERR_AREA_NOT_CLEAR
         * @exception AIError.ERR_LAND_SLOPED_WRONG
         * @exception AIError.ERR_OWNED_BY_ANOTHER_COMPANY
         * @exception AIError.ERR_ALREADY_BUILT
         * @returns Whether the canal has been/can be build or not.
         */
        public static bool BuildCanal(TileIndex tile) { throw null; }

        /**
         * Removes a water depot.
         * @param tile Any tile of the water depot.
         * @exception AIError.ERR_OWNED_BY_ANOTHER_COMPANY
         * @returns Whether the water depot has been/can be removed or not.
         */
        public static bool RemoveWaterDepot(TileIndex tile) { throw null; }

        /**
         * Removes a dock.
         * @param tile Any tile of the dock.
         * @exception AIError.ERR_OWNED_BY_ANOTHER_COMPANY
         * @returns Whether the dock has been/can be removed or not.
         */
        public static bool RemoveDock(TileIndex tile) { throw null; }

        /**
         * Removes a buoy.
         * @param tile Any tile of the buoy.
         * @exception AIError.ERR_OWNED_BY_ANOTHER_COMPANY
         * @returns Whether the buoy has been/can be removed or not.
         */
        public static bool RemoveBuoy(TileIndex tile) { throw null; }

        /**
         * Removes a lock.
         * @param tile Any tile of the lock.
         * @exception AIError.ERR_OWNED_BY_ANOTHER_COMPANY
         * @returns Whether the lock has been/can be removed or not.
         */
        public static bool RemoveLock(TileIndex tile) { throw null; }

        /**
         * Removes a canal.
         * @param tile Any tile of the canal.
         * @exception AIError.ERR_OWNED_BY_ANOTHER_COMPANY
         * @returns Whether the canal has been/can be removed or not.
         */
        public static bool RemoveCanal(TileIndex tile) { throw null; }

        /**
         * Get the baseprice of building a water-related object.
         * @param build_type the type of object to build
         * @returns The baseprice of building the given object.
         */
        public static long GetBuildCost(BuildType build_type) { throw null; }
    }
}