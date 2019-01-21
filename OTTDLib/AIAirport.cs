namespace OpenTTD
{
    /// <summary>
    /// Class that handles all airport related functions.
    /// </summary>
    public static class AIAirport
    {
        public static readonly AirportType AT_SMALL;
        public static readonly AirportType AT_LARGE;
        public static readonly AirportType AT_METROPOLITAN;
        public static readonly AirportType AT_INTERNATIONAL;
        public static readonly AirportType AT_COMMUTER;
        public static readonly AirportType AT_INTERCON;
        public static readonly AirportType AT_HELIPORT;
        public static readonly AirportType AT_HELISTATION;
        public static readonly AirportType AT_HELIDEPOT;
        public static readonly AirportType AT_INVALID;
        
        public static readonly PlaneType PT_HELICOPTER;
        public static readonly PlaneType PT_SMALL_PLANE;
        public static readonly PlaneType PT_BIG_PLANE;
        public static readonly PlaneType PT_INVALID;

        /**
         * Checks whether the given AirportType is valid and available.
         * @param type The AirportType to check.
         * @returns True if and only if the AirportType is valid and available.
         */
        public static bool IsValidAirportType(AirportType type) { throw null; }

        /**
         * Can you get information on this airport type? As opposed to
         * IsValidAirportType this will return also return true when
         * an airport type is no longer buildable.
         * @param type The AirportType to check.
         * @returns True if and only if the AirportType is valid.
         */
        public static bool IsAirportInformationAvailable(AirportType type) { throw null; }

        /**
         * Get the cost to build this AirportType.
         * @param type The AirportType to check.
         * @returns The cost of building this AirportType.
         */
        public static long GetPrice(AirportType type) { throw null; }

        /**
         * Checks whether the given tile is actually a tile with a hangar.
         * @param tile The tile to check.
         * @returns True if and only if the tile has a hangar.
         */
        public static bool IsHangarTile(TileIndex tile) { throw null; }

        /**
         * Checks whether the given tile is actually a tile with an airport.
         * @param tile The tile to check.
         * @returns True if and only if the tile has an airport.
         */
        public static bool IsAirportTile(TileIndex tile) { throw null; }

        /**
         * Get the width of this type of airport.
         * @param type The type of airport.
         * @returns The width in tiles.
         */
        public static int GetAirportWidth(AirportType type) { throw null; }

        /**
         * Get the height of this type of airport.
         * @param type The type of airport.
         * @returns The height in tiles.
         */
        public static int GetAirportHeight(AirportType type) { throw null; }

        /**
         * Get the coverage radius of this type of airport.
         * @param type The type of airport.
         * @returns The radius in tiles.
         */
        public static int GetAirportCoverageRadius(AirportType type) { throw null; }

        /**
         * Get the number of hangars of the airport.
         * @param tile Any tile of the airport.
         * @returns The number of hangars of the airport.
         */
        public static int GetNumHangars(TileIndex tile) { throw null; }

        /**
         * Get the first hangar tile of the airport.
         * @param tile Any tile of the airport.
         * @returns The first hangar tile of the airport.
         * @note Possible there are more hangars, but you won't be able to find them
         *  without walking over all the tiles of the airport and using
         *  IsHangarTile() on them.
         */
        public static TileIndex GetHangarOfAirport(TileIndex tile) { throw null; }

        /**
         * Builds a airport with tile at the topleft corner.
         * @param tile The topleft corner of the airport.
         * @param type The type of airport to build.
         * @param station_id The station to join, AIStation.STATION_NEW or AIStation.STATION_JOIN_ADJACENT.
         * @exception AIError.ERR_AREA_NOT_CLEAR
         * @exception AIError.ERR_FLAT_LAND_REQUIRED
         * @exception AIError.ERR_LOCAL_AUTHORITY_REFUSES
         * @exception AIStation.ERR_STATION_TOO_LARGE
         * @exception AIStation.ERR_STATION_TOO_CLOSE_TO_ANOTHER_STATION
         * @returns Whether the airport has been/can be build or not.
         */
        public static bool BuildAirport(TileIndex tile, AirportType type, StationID station_id) { throw null; }

        /**
         * Removes an airport.
         * @param tile Any tile of the airport.
         * @exception AIError.ERR_OWNED_BY_ANOTHER_COMPANY
         * @returns Whether the airport has been/can be removed or not.
         */
        public static bool RemoveAirport(TileIndex tile) { throw null; }

        /**
         * Get the AirportType of an existing airport.
         * @param tile Any tile of the airport.
         * @returns The AirportType of the airport.
         */
        public static AirportType GetAirportType(TileIndex tile) { throw null; }

        /**
         * Get the noise that will be added to the nearest town if an airport was
         *  built at this tile.
         * @param tile The tile to check.
         * @param type The AirportType to check.
         * @returns The amount of noise added to the nearest town.
         * @note The noise will be added to the town with TownID GetNearestTown(tile, type).
         */
        public static int GetNoiseLevelIncrease(TileIndex tile, AirportType type) { throw null; }

        /**
         * Get the TownID of the town whose local authority will influence
         *  an airport at some tile.
         * @param tile The tile to check.
         * @param type The AirportType to check.
         * @returns The TownID of the town closest to the tile.
         */
        public static TownID GetNearestTown(TileIndex tile, AirportType type) { throw null; }

        /**
         * Get the maintenance cost factor of an airport type.
         * @param type The airport type to get the maintenance factor of.
         * @returns Maintenance cost factor of the airport type.
         */
        public static int GetMaintenanceCostFactor(AirportType type) { throw null; }

        /**
         * Get the monthly maintenance cost of an airport type.
         * @param type The airport type to get the monthly maintenance cost of.
         * @returns Monthly maintenance cost of the airport type.
         */
        public static long GetMonthlyMaintenanceCost(AirportType type) { throw null; }
    }
}