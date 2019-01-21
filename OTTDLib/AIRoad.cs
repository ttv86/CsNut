namespace OpenTTD
{
    /// <summary>Class that handles all road related functions.</summary>
    public static class AIRoad
    {
        /// <summary>Base for road building / maintaining errors </summary>
        public static readonly ErrorMessage ERR_ROAD_BASE;

        /// <summary>Road works are in progress </summary>
        public static readonly ErrorMessage ERR_ROAD_WORKS_IN_PROGRESS;

        /// <summary>Drive through is in the wrong direction </summary>
        public static readonly ErrorMessage ERR_ROAD_DRIVE_THROUGH_WRONG_DIRECTION;

        /// <summary>Drive through roads can't be build on town owned roads </summary>
        public static readonly ErrorMessage ERR_ROAD_CANNOT_BUILD_ON_TOWN_ROAD;

        /// <summary>One way roads can't have junctions </summary>
        public static readonly ErrorMessage ERR_ROAD_ONE_WAY_ROADS_CANNOT_HAVE_JUNCTIONS;
        
        public static readonly RoadType ROADTYPE_ROAD;
        public static readonly RoadType ROADTYPE_TRAM;
        public static readonly RoadType ROADTYPE_INVALID;
        
        public static readonly BuildType BT_ROAD;
        public static readonly BuildType BT_DEPOT;
        public static readonly BuildType BT_BUS_STOP;
        public static readonly BuildType BT_TRUCK_STOP;
        
        public static readonly RoadVehicleType ROADVEHTYPE_BUS;
        public static readonly RoadVehicleType ROADVEHTYPE_TRUCK;

        /**
         * Determines whether a busstop or a truckstop is needed to transport a certain cargo.
         * @param cargo_type The cargo to test.
         * @returns The road vehicle type needed to transport the cargo.
         */
        public static RoadVehicleType GetRoadVehicleTypeForCargo(CargoID cargo_type) { throw null; }

        /**
         * Checks whether the given tile is actually a tile with road that can be
         *  used to traverse a tile. This excludes road depots and 'normal' road
         *  stations, but includes drive through stations.
         * @param tile The tile to check.
         * @returns True if and only if the tile has road.
         */
        public static bool IsRoadTile(TileIndex tile) { return Testing.AIRoad.IsRoadTile(tile); }

        /**
         * Checks whether the given tile is actually a tile with a road depot.
         * @param tile The tile to check.
         * @returns True if and only if the tile has a road depot.
         */
        public static bool IsRoadDepotTile(TileIndex tile) { throw null; }

        /**
         * Checks whether the given tile is actually a tile with a road station.
         * @param tile The tile to check.
         * @returns True if and only if the tile has a road station.
         */
        public static bool IsRoadStationTile(TileIndex tile) { throw null; }

        /**
         * Checks whether the given tile is actually a tile with a drive through
         *  road station.
         * @param tile The tile to check.
         * @returns True if and only if the tile has a drive through road station.
         */
        public static bool IsDriveThroughRoadStationTile(TileIndex tile) { throw null; }

        /**
         * Check if a given RoadType is available.
         * @param road_type The RoadType to check for.
         * @returns True if this RoadType can be used.
         */
        public static bool IsRoadTypeAvailable(RoadType road_type) { throw null; }

        /// <summary>Get the current RoadType set for all ScriptRoad functions.</summary><returns>The RoadType currently set.</returns>
        public static RoadType GetCurrentRoadType() { throw null; }

        /// <summary>Set the RoadType for all further ScriptRoad functions.</summary><param name="road_type">The RoadType to set.</param>
        public static void SetCurrentRoadType(RoadType road_type) { throw null; }

        /**
         * Check if a given tile has RoadType.
         * @param tile The tile to check.
         * @param road_type The RoadType to check for.
         * @returns True if the tile contains a RoadType object.
         */
        public static bool HasRoadType(TileIndex tile, RoadType road_type) { throw null; }

        /**
         * Checks whether the given tiles are directly connected, i.e. whether
         *  a road vehicle can travel from the center of the first tile to the
         * center of the second tile.
         * @param tile_from The source tile.
         * @param tile_to The destination tile.
         * @returns True if and only if a road vehicle can go from tile_from to tile_to.
         */
        public static bool AreRoadTilesConnected(TileIndex tile_from, TileIndex tile_to) { throw null; }

        /**
         * Lookup function for building road parts independend on whether the
         *  "building on slopes" setting is enabled or not.
         *  This implementation can be used for abstract reasoning about a tile as
         *  it needs the slope and existing road parts of the tile as information.
         * @param slope The slope of the tile to examine.
         * @param existing An array with the existing neighbours in the same format
         *                 as "start" and "end", e.g. ScriptMap.GetTileIndex(0, 1).
         *                 As a result of this all values of the existing array
         *                 must be of type integer.
         * @param start The tile from where the 'tile to be considered' will be
         *              entered. This is a relative tile, so valid parameters are:
         *              ScriptMap.GetTileIndex(0, 1), ScriptMap.GetTileIndex(0, -1),
         *              ScriptMap.GetTileIndex(1, 0) and ScriptMap.GetTileIndex(-1, 0).
         * @param end The tile from where the 'tile to be considered' will be
         *            exited. This is a relative tile, sovalid parameters are:
         *              ScriptMap.GetTileIndex(0, 1), ScriptMap.GetTileIndex(0, -1),
         *              ScriptMap.GetTileIndex(1, 0) and ScriptMap.GetTileIndex(-1, 0).
         * @note Passing data that would be invalid in-game, e.g. existing containing
         *       road parts that can not be build on a tile with the given slope,
         *       does not necessarily means that -1 is returned, i.e. not all
         *       preconditions written here or assumed by the game are extensively
         *       checked to make sure the data entered is valid.
         * @returns 0 when the build parts do not connect, 1 when they do connect once
         *         they are build or 2 when building the first part automatically
         *         builds the second part. -1 means the preconditions are not met.
         */
        public static int CanBuildConnectedRoadParts(Slope slope, int[] existing, TileIndex start, TileIndex end) { throw null; }

        /**
         * Lookup function for building road parts independend on whether the
         *  "building on slopes" setting is enabled or not.
         *  This implementation can be used for reasoning about an existing tile.
         * @param tile The the tile to examine.
         * @param start The tile from where "tile" will be entered.
         * @param end The tile from where "tile" will be exited.
         * @returns 0 when the build parts do not connect, 1 when they do connect once
         *         they are build or 2 when building the first part automatically
         *         builds the second part. -1 means the preconditions are not met.
         */
        public static int CanBuildConnectedRoadPartsHere(TileIndex tile, TileIndex start, TileIndex end) { throw null; }

        /**
         * Count how many neighbours are road.
         * @param tile The tile to check on.
         * @returns 0 means no neighbour road; max value is 4.
         */
        public static int GetNeighbourRoadCount(TileIndex tile) { throw null; }

        /**
         * Gets the tile in front of a road depot.
         * @param depot The road depot tile.
         * @returns The tile in front of the depot.
         */
        public static TileIndex GetRoadDepotFrontTile(TileIndex depot) { throw null; }

        /**
         * Gets the tile in front of a road station.
         * @param station The road station tile.
         * @returns The tile in front of the road station.
         */
        public static TileIndex GetRoadStationFrontTile(TileIndex station) { throw null; }

        /**
         * Gets the tile at the back of a drive through road station.
         *  So, one side of the drive through station is retrieved with
         *  GetTileInFrontOfStation, the other with this function.
         * @param station The road station tile.
         * @returns The tile at the back of the drive through road station.
         */
        public static TileIndex GetDriveThroughBackTile(TileIndex station) { throw null; }

        /**
         * Builds a road from the center of tile start to the center of tile end.
         * @param start The start tile of the road.
         * @param end The end tile of the road.
         * @exception AIError.ERR_ALREADY_BUILT
         * @exception AIError.ERR_LAND_SLOPED_WRONG
         * @exception AIError.ERR_AREA_NOT_CLEAR
         * @exception AIRoad.ERR_ROAD_ONE_WAY_ROADS_CANNOT_HAVE_JUNCTIONS
         * @exception AIRoad.ERR_ROAD_WORKS_IN_PROGRESS
         * @exception AIError.ERR_VEHICLE_IN_THE_WAY
         * @note Construction will fail if an obstacle is found between the start and end tiles.
         * @returns Whether the road has been/can be build or not.
         */
        public static bool BuildRoad(TileIndex start, TileIndex end) { throw null; }

        /**
         * Builds a one-way road from the center of tile start to the center
         *  of tile end. If the road already exists, it is made one-way road.
         *  If the road already exists and is already one-way in this direction,
         *  the road is made two-way again. If the road already exists but is
         *  one-way in the other direction, it's made a 'no'-way road (it's
         *  forbidden to enter the tile from any direction).
         * @param start The start tile of the road.
         * @param end The end tile of the road.
         * @exception AIError.ERR_ALREADY_BUILT
         * @exception AIError.ERR_LAND_SLOPED_WRONG
         * @exception AIError.ERR_AREA_NOT_CLEAR
         * @exception AIRoad.ERR_ROAD_ONE_WAY_ROADS_CANNOT_HAVE_JUNCTIONS
         * @exception AIRoad.ERR_ROAD_WORKS_IN_PROGRESS
         * @exception AIError.ERR_VEHICLE_IN_THE_WAY
         * @note Construction will fail if an obstacle is found between the start and end tiles.
         * @returns Whether the road has been/can be build or not.
         */
        public static bool BuildOneWayRoad(TileIndex start, TileIndex end) { throw null; }

        /**
         * Builds a road from the edge of tile start to the edge of tile end (both
         *  included).
         * @param start The start tile of the road.
         * @param end The end tile of the road.
         * @exception AIError.ERR_ALREADY_BUILT
         * @exception AIError.ERR_LAND_SLOPED_WRONG
         * @exception AIError.ERR_AREA_NOT_CLEAR
         * @exception AIRoad.ERR_ROAD_ONE_WAY_ROADS_CANNOT_HAVE_JUNCTIONS
         * @exception AIRoad.ERR_ROAD_WORKS_IN_PROGRESS
         * @exception AIError.ERR_VEHICLE_IN_THE_WAY
         * @note Construction will fail if an obstacle is found between the start and end tiles.
         * @returns Whether the road has been/can be build or not.
         */
        public static bool BuildRoadFull(TileIndex start, TileIndex end) { throw null; }

        /**
         * Builds a one-way road from the edge of tile start to the edge of tile end
         *  (both included). If the road already exists, it is made one-way road.
         *  If the road already exists and is already one-way in this direction,
         *  the road is made two-way again. If the road already exists but is
         *  one-way in the other direction, it's made a 'no'-way road (it's
         *  forbidden to enter the tile from any direction).
         * @param start The start tile of the road.
         * @param start The start tile of the road.
         * @param end The end tile of the road.
         * @exception AIError.ERR_ALREADY_BUILT
         * @exception AIError.ERR_LAND_SLOPED_WRONG
         * @exception AIError.ERR_AREA_NOT_CLEAR
         * @exception AIRoad.ERR_ROAD_ONE_WAY_ROADS_CANNOT_HAVE_JUNCTIONS
         * @exception AIRoad.ERR_ROAD_WORKS_IN_PROGRESS
         * @exception AIError.ERR_VEHICLE_IN_THE_WAY
         * @note Construction will fail if an obstacle is found between the start and end tiles.
         * @returns Whether the road has been/can be build or not.
         */
        public static bool BuildOneWayRoadFull(TileIndex start, TileIndex end) { throw null; }

        /**
         * Builds a road depot.
         * @param tile Place to build the depot.
         * @param front The tile exactly in front of the depot.
         * @exception AIError.ERR_FLAT_LAND_REQUIRED
         * @exception AIError.ERR_AREA_NOT_CLEAR
         * @returns Whether the road depot has been/can be build or not.
         */
        public static bool BuildRoadDepot(TileIndex tile, TileIndex front) { throw null; }

        /**
         * Builds a road bus or truck station.
         * @param tile Place to build the station.
         * @param front The tile exactly in front of the station.
         * @param road_veh_type Whether to build a truck or bus station.
         * @param station_id The station to join, AIStation.STATION_NEW or AIStation.STATION_JOIN_ADJACENT.
         * @exception AIError.ERR_OWNED_BY_ANOTHER_COMPANY
         * @exception AIError.ERR_AREA_NOT_CLEAR
         * @exception AIError.ERR_FLAT_LAND_REQUIRED
         * @exception AIRoad.ERR_ROAD_DRIVE_THROUGH_WRONG_DIRECTION
         * @exception AIRoad.ERR_ROAD_CANNOT_BUILD_ON_TOWN_ROAD
         * @exception AIError.ERR_VEHICLE_IN_THE_WAY
         * @exception AIStation.ERR_STATION_TOO_CLOSE_TO_ANOTHER_STATION
         * @exception AIStation.ERR_STATION_TOO_MANY_STATIONS
         * @exception AIStation.ERR_STATION_TOO_MANY_STATIONS_IN_TOWN
         * @returns Whether the station has been/can be build or not.
         */
        public static bool BuildRoadStation(TileIndex tile, TileIndex front, RoadVehicleType road_veh_type, StationID station_id) { throw null; }

        /**
         * Builds a drive-through road bus or truck station.
         * @param tile Place to build the station.
         * @param front A tile on the same axis with 'tile' as the station shall be oriented.
         * @param road_veh_type Whether to build a truck or bus station.
         * @param station_id The station to join, AIStation.STATION_NEW or AIStation.STATION_JOIN_ADJACENT.
         * @exception AIError.ERR_OWNED_BY_ANOTHER_COMPANY
         * @exception AIError.ERR_AREA_NOT_CLEAR
         * @exception AIError.ERR_FLAT_LAND_REQUIRED
         * @exception AIRoad.ERR_ROAD_DRIVE_THROUGH_WRONG_DIRECTION
         * @exception AIRoad.ERR_ROAD_CANNOT_BUILD_ON_TOWN_ROAD
         * @exception AIError.ERR_VEHICLE_IN_THE_WAY
         * @exception AIStation.ERR_STATION_TOO_CLOSE_TO_ANOTHER_STATION
         * @exception AIStation.ERR_STATION_TOO_MANY_STATIONS
         * @exception AIStation.ERR_STATION_TOO_MANY_STATIONS_IN_TOWN
         * @returns Whether the station has been/can be build or not.
         */
        public static bool BuildDriveThroughRoadStation(TileIndex tile, TileIndex front, RoadVehicleType road_veh_type, StationID station_id) { throw null; }

        /**
         * Removes a road from the center of tile start to the center of tile end.
         * @param start The start tile of the road.
         * @param end The end tile of the road.
         * @exception AIError.ERR_OWNED_BY_ANOTHER_COMPANY
         * @exception AIError.ERR_VEHICLE_IN_THE_WAY
         * @exception AIRoad.ERR_ROAD_WORKS_IN_PROGRESS
         * @returns Whether the road has been/can be removed or not.
         */
        public static bool RemoveRoad(TileIndex start, TileIndex end) { throw null; }

        /**
         * Removes a road from the edge of tile start to the edge of tile end (both
         *  included).
         * @param start The start tile of the road.
         * @param end The end tile of the road.
         * @exception AIError.ERR_OWNED_BY_ANOTHER_COMPANY
         * @exception AIError.ERR_VEHICLE_IN_THE_WAY
         * @exception AIRoad.ERR_ROAD_WORKS_IN_PROGRESS
         * @returns Whether the road has been/can be removed or not.
         */
        public static bool RemoveRoadFull(TileIndex start, TileIndex end) { throw null; }

        /**
         * Removes a road depot.
         * @param tile Place to remove the depot from.
         * @exception AIError.ERR_OWNED_BY_ANOTHER_COMPANY
         * @exception AIError.ERR_VEHICLE_IN_THE_WAY
         * @returns Whether the road depot has been/can be removed or not.
         */
        public static bool RemoveRoadDepot(TileIndex tile) { throw null; }

        /**
         * Removes a road bus or truck station.
         * @param tile Place to remove the station from.
         * @exception AIError.ERR_OWNED_BY_ANOTHER_COMPANY
         * @exception AIError.ERR_VEHICLE_IN_THE_WAY
         * @returns Whether the station has been/can be removed or not.
         */
        public static bool RemoveRoadStation(TileIndex tile) { throw null; }

        /**
         * Get the baseprice of building a road-related object.
         * @param roadtype the roadtype that is build (on)
         * @param build_type the type of object to build
         * @returns The baseprice of building the given object.
         */
        public static long GetBuildCost(RoadType roadtype, BuildType build_type) { throw null; }

        /**
         * Get the maintenance cost factor of a roadtype.
         * @param roadtype The roadtype to get the maintenance factor of.
         * @returns Maintenance cost factor of the roadtype.
         */
        public static int GetMaintenanceCostFactor(RoadType roadtype) { throw null; }
    }
}