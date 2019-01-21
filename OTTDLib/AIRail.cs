namespace OpenTTD
{
    /// <summary>Class that handles all rail related functions.</summary>
    public static class AIRail
    {
        /// <summary>Base for rail building / maintaining errors </summary>
        public static readonly ErrorMessage ERR_RAIL_BASE;

        /// <summary>One-way roads cannot have crossings </summary>
        public static readonly ErrorMessage ERR_CROSSING_ON_ONEWAY_ROAD;

        /// <summary>No suitable track could be found </summary>
        public static readonly ErrorMessage ERR_UNSUITABLE_TRACK;

        /// <summary>This railtype cannot have crossings </summary>
        public static readonly ErrorMessage ERR_RAILTYPE_DISALLOWS_CROSSING;

        /// <summary>Types of rail known to the game.</summary>

        public static readonly RailType RAILTYPE_INVALID;

        /// <summary>A bitmap with all possible rail tracks on a tile.</summary>
        public static readonly RailTrack RAILTRACK_NE_SW;
        public static readonly RailTrack RAILTRACK_NW_SE;
        public static readonly RailTrack RAILTRACK_NW_NE;
        public static readonly RailTrack RAILTRACK_SW_SE;
        public static readonly RailTrack RAILTRACK_NW_SW;
        public static readonly RailTrack RAILTRACK_NE_SE;
        public static readonly RailTrack RAILTRACK_INVALID;


        /// <summary>Types of signal known to the game.</summary>
        public static readonly SignalType SIGNALTYPE_NORMAL;
        public static readonly SignalType SIGNALTYPE_ENTRY;
        public static readonly SignalType SIGNALTYPE_EXIT;
        public static readonly SignalType SIGNALTYPE_COMBO;
        public static readonly SignalType SIGNALTYPE_PBS;
        public static readonly SignalType SIGNALTYPE_PBS_ONEWAY;

        public static readonly SignalType SIGNALTYPE_TWOWAY;
        public static readonly SignalType SIGNALTYPE_NORMAL_TWOWAY;
        public static readonly SignalType SIGNALTYPE_ENTRY_TWOWAY;
        public static readonly SignalType SIGNALTYPE_EXIT_TWOWAY;
        public static readonly SignalType SIGNALTYPE_COMBO_TWOWAY;

        public static readonly SignalType SIGNALTYPE_NONE;

        /// <summary>Types of rail-related objects in the game.</summary>
        public static readonly BuildType BT_TRACK;
        public static readonly BuildType BT_SIGNAL;
        public static readonly BuildType BT_DEPOT;
        public static readonly BuildType BT_STATION;
        public static readonly BuildType BT_WAYPOINT;

        /**
         * Get the name of a rail type.
         * @param rail_type The rail type to get the name of.
         * @returns The name the rail type has.
         * @note Since there is no string with only the name of the track, the text which
         *  is shown in the dropdown where you can chose a track type is returned. This
         *  means that the name could be something like "Maglev construction" instead
         *  of just "Maglev".
         */
        public static string GetName(RailType rail_type) { throw null; }

        /**
         * Checks whether the given tile is actually a tile with rail that can be
         *  used to traverse a tile. This excludes rail depots but includes
         *  stations and waypoints.
         * @param tile The tile to check.
         * @returns True if and only if the tile has rail.
         */
        public static bool IsRailTile(TileIndex tile) { return Testing.AIRail.IsRailTile(tile); }

        /**
         * Checks whether there is a road / rail crossing on a tile.
         * @param tile The tile to check.
         * @returns True if and only if there is a road / rail crossing.
         */
        public static bool IsLevelCrossingTile(TileIndex tile) { throw null; }

        /**
         * Checks whether the given tile is actually a tile with a rail depot.
         * @param tile The tile to check.
         * @returns True if and only if the tile has a rail depot.
         */
        public static bool IsRailDepotTile(TileIndex tile) { throw null; }

        /**
         * Checks whether the given tile is actually a tile with a rail station.
         * @param tile The tile to check.
         * @returns True if and only if the tile has a rail station.
         */
        public static bool IsRailStationTile(TileIndex tile) { throw null; }

        /**
         * Checks whether the given tile is actually a tile with a rail waypoint.
         * @param tile The tile to check.
         * @returns True if and only if the tile has a rail waypoint.
         */
        public static bool IsRailWaypointTile(TileIndex tile) { throw null; }

        /**
         * Check if a given RailType is available.
         * @param rail_type The RailType to check for.
         * @returns True if this RailType can be used.
         */
        public static bool IsRailTypeAvailable(RailType rail_type) { throw null; }

        /// <summary>Get the current RailType set for all ScriptRail functions.</summary><returns>The RailType currently set.</returns>
        public static RailType GetCurrentRailType() { throw null; }

        /// <summary>Set the RailType for all further ScriptRail functions.</summary><param name="rail_type">The RailType to set.</param>
        public static void SetCurrentRailType(RailType rail_type) { throw null; }

        /**
         * Check if a train build for a rail type can run on another rail type.
         * @param engine_rail_type The rail type the train is build for.
         * @param track_rail_type The type you want to check.
         * @returns Whether a train build for 'engine_rail_type' can run on 'track_rail_type'.
         * @note Even if a train can run on a RailType that doesn't mean that it'll be
         *   able to power the train. Use TrainHasPowerOnRail for that.
         */
        public static bool TrainCanRunOnRail(RailType engine_rail_type, RailType track_rail_type) { throw null; }

        /**
         * Check if a train build for a rail type has power on another rail type.
         * @param engine_rail_type The rail type the train is build for.
         * @param track_rail_type The type you want to check.
         * @returns Whether a train build for 'engine_rail_type' has power on 'track_rail_type'.
         */
        public static bool TrainHasPowerOnRail(RailType engine_rail_type, RailType track_rail_type) { throw null; }

        /**
         * Get the RailType that is used on a tile.
         * @param tile The tile to check.
         * @returns The RailType that is used on a tile.
         */
        public static RailType GetRailType(TileIndex tile) { throw null; }

        /**
         * Convert the tracks on all tiles within a rectangle to another RailType.
         * @param start_tile One corner of the rectangle.
         * @param end_tile The opposite corner of the rectangle.
         * @param convert_to The RailType you want to convert the rails to.
         * @exception AIRail.ERR_UNSUITABLE_TRACK
         * @returns Whether at least some rail has been converted successfully.
         */
        public static bool ConvertRailType(TileIndex start_tile, TileIndex end_tile, RailType convert_to) { throw null; }

        /**
         * Gets the tile in front of a rail depot.
         * @param depot The rail depot tile.
         * @returns The tile in front of the depot.
         */
        public static TileIndex GetRailDepotFrontTile(TileIndex depot) { throw null; }

        /**
         * Gets the direction of a rail station tile.
         * @param tile The rail station tile.
         * @returns The direction of the station (either RAILTRACK_NE_SW or RAILTRACK_NW_SE).
         */
        public static RailTrack GetRailStationDirection(TileIndex tile) { throw null; }

        /**
         * Builds a rail depot.
         * @param tile Place to build the depot.
         * @param front The tile exactly in front of the depot.
         * @exception AIError.ERR_FLAT_LAND_REQUIRED
         * @exception AIError.ERR_AREA_NOT_CLEAR
         * @returns Whether the rail depot has been/can be build or not.
         */
        public static bool BuildRailDepot(TileIndex tile, TileIndex front) { throw null; }

        /**
         * Build a rail station.
         * @param tile Place to build the station.
         * @param direction The direction to build the station.
         * @param num_platforms The number of platforms to build.
         * @param platform_length The length of each platform.
         * @param station_id The station to join, AIStation.STATION_NEW or AIStation.STATION_JOIN_ADJACENT.
         * @exception AIError.ERR_OWNED_BY_ANOTHER_COMPANY
         * @exception AIError.ERR_AREA_NOT_CLEAR
         * @exception AIError.ERR_FLAT_LAND_REQUIRED
         * @exception AIStation.ERR_STATION_TOO_CLOSE_TO_ANOTHER_STATION
         * @exception AIStation.ERR_STATION_TOO_MANY_STATIONS
         * @exception AIStation.ERR_STATION_TOO_MANY_STATIONS_IN_TOWN
         * @returns Whether the station has been/can be build or not.
         */
        public static bool BuildRailStation(TileIndex tile, RailTrack direction, int num_platforms, int platform_length, StationID station_id) { throw null; }

        /**
         * Build a NewGRF rail station. This calls callback 18 to let a NewGRF
         *  provide the station class / id to build, so we don't end up with
         *  only the default stations on the map.
         * When no NewGRF provides a rail station, or an unbuildable rail station is
         *  returned by a NewGRF, this function will fall back to building a default
         *  non-NewGRF station as if AIRail.BuildRailStation was called.
         * @param tile Place to build the station.
         * @param direction The direction to build the station.
         * @param num_platforms The number of platforms to build.
         * @param platform_length The length of each platform.
         * @param station_id The station to join, AIStation.STATION_NEW or AIStation.STATION_JOIN_ADJACENT.
         * @param cargo_id The CargoID of the cargo that will be transported from / to this station.
         * @param source_industry The IndustryType of the industry you'll transport goods from, AIIndustryType.INDUSTRYTYPE_UNKNOWN or AIIndustryType.INDUSTRYTYPE_TOWN.
         * @param goal_industry The IndustryType of the industry you'll transport goods to, AIIndustryType.INDUSTRYTYPE_UNKNOWN or AIIndustryType.INDUSTRYTYPE_TOWN.
         * @param distance The manhattan distance you'll transport the cargo over.
         * @param source_station True if this is the source station, false otherwise.
         * @exception AIError.ERR_OWNED_BY_ANOTHER_COMPANY
         * @exception AIError.ERR_AREA_NOT_CLEAR
         * @exception AIError.ERR_FLAT_LAND_REQUIRED
         * @exception AIStation.ERR_STATION_TOO_CLOSE_TO_ANOTHER_STATION
         * @exception AIStation.ERR_STATION_TOO_MANY_STATIONS
         * @exception AIStation.ERR_STATION_TOO_MANY_STATIONS_IN_TOWN
         * @returns Whether the station has been/can be build or not.
         */
        public static bool BuildNewGRFRailStation(TileIndex tile, RailTrack direction, int num_platforms, int platform_length, StationID station_id, CargoID cargo_id, IndustryType source_industry, IndustryType goal_industry, int distance, bool source_station) { throw null; }

        /**
         * Build a rail waypoint.
         * @param tile Place to build the waypoint.
         * @exception AIError.ERR_FLAT_LAND_REQUIRED
         * @returns Whether the rail waypoint has been/can be build or not.
         */
        public static bool BuildRailWaypoint(TileIndex tile) { throw null; }

        /**
         * Remove all rail waypoint pieces within a rectangle on the map.
         * @param tile One corner of the rectangle to clear.
         * @param tile2 The opposite corner.
         * @param keep_rail Whether to keep the rail after removal.
         * @exception AIRail.ERR_UNSUITABLE_TRACK
         * @returns Whether at least one tile has been/can be cleared or not.
         */
        public static bool RemoveRailWaypointTileRectangle(TileIndex tile, TileIndex tile2, bool keep_rail) { throw null; }

        /**
         * Remove all rail station platform pieces within a rectangle on the map.
         * @param tile One corner of the rectangle to clear.
         * @param tile2 The opposite corner.
         * @param keep_rail Whether to keep the rail after removal.
         * @exception AIRail.ERR_UNSUITABLE_TRACK
         * @returns Whether at least one tile has been/can be cleared or not.
         */
        public static bool RemoveRailStationTileRectangle(TileIndex tile, TileIndex tile2, bool keep_rail) { throw null; }

        /**
         * Get all RailTracks on the given tile.
         * @note A depot has no railtracks.
         * @param tile The tile to check.
         * @returns A bitmask of RailTrack with all RailTracks on the tile.
         */
        public static int GetRailTracks(TileIndex tile) { throw null; }

        /**
         * Build rail on the given tile.
         * @param tile The tile to build on.
         * @param rail_track The RailTrack to build.
         * @exception AIError.ERR_AREA_NOT_CLEAR
         * @exception AIError.ERR_LAND_SLOPED_WRONG
         * @exception AIRoad.ERR_ROAD_WORKS_IN_PROGRESS
         * @exception AIRail.ERR_CROSSING_ON_ONEWAY_ROAD
         * @exception AIError.ERR_ALREADY_BUILT
         * @returns Whether the rail has been/can be build or not.
         * @note You can only build a single track with this function so do not
         *   use the values from RailTrack as bitmask.
         */
        public static bool BuildRailTrack(TileIndex tile, RailTrack rail_track) { throw null; }

        /**
         * Remove rail on the given tile.
         * @param tile The tile to remove rail from.
         * @param rail_track The RailTrack to remove.
         * @exception AIRail.ERR_UNSUITABLE_TRACK
         * @returns Whether the rail has been/can be removed or not.
         * @note You can only remove a single track with this function so do not
         *   use the values from RailTrack as bitmask.
         */
        public static bool RemoveRailTrack(TileIndex tile, RailTrack rail_track) { throw null; }

        /**
         * Check if a tile connects two adjacent tiles.
         * @param from The first tile to connect.
         * @param tile The tile that is checked.
         * @param to The second tile to connect.
         * @returns True if 'tile' connects 'from' and 'to'.
         */
        public static bool AreTilesConnected(TileIndex from, TileIndex tile, TileIndex to) { throw null; }

        /**
         * Build a rail connection between two tiles.
         * @param from The tile just before the tile to build on.
         * @param tile The first tile to build on.
         * @param to The tile just after the last tile to build on.
         * @exception AIError.ERR_AREA_NOT_CLEAR
         * @exception AIError.ERR_LAND_SLOPED_WRONG
         * @exception AIRail.ERR_CROSSING_ON_ONEWAY_ROAD
         * @exception AIRoad.ERR_ROAD_WORKS_IN_PROGRESS
         * @exception AIError.ERR_ALREADY_BUILT
         * @note Construction will fail if an obstacle is found between the start and end tiles.
         * @returns Whether the rail has been/can be build or not.
         */
        public static bool BuildRail(TileIndex from, TileIndex tile, TileIndex to) { throw null; }

        /**
         * Remove a rail connection between two tiles.
         * @param from The tile just before the tile to remove rail from.
         * @param tile The first tile to remove rail from.
         * @param to The tile just after the last tile to remove rail from.
         * @exception AIRail.ERR_UNSUITABLE_TRACK
         * @returns Whether the rail has been/can be removed or not.
         */
        public static bool RemoveRail(TileIndex from, TileIndex tile, TileIndex to) { throw null; }

        /**
         * Get the SignalType of the signal on a tile or SIGNALTYPE_NONE if there is no signal.
         * @param tile The tile that might have a signal.
         * @param front The tile in front of 'tile'.
         * @returns The SignalType of the signal on 'tile' facing to 'front'.
         */
        public static SignalType GetSignalType(TileIndex tile, TileIndex front) { throw null; }

        /**
         * Build a signal on a tile.
         * @param tile The tile to build on.
         * @param front The tile in front of the signal.
         * @param signal The SignalType to build.
         * @exception AIRail.ERR_UNSUITABLE_TRACK
         * @returns Whether the signal has been/can be build or not.
         */
        public static bool BuildSignal(TileIndex tile, TileIndex front, SignalType signal) { throw null; }

        /**
         * Remove a signal.
         * @param tile The tile to remove the signal from.
         * @param front The tile in front of the signal.
         * @exception AIRail.ERR_UNSUITABLE_TRACK
         * @returns Whether the signal has been/can be removed or not.
         */
        public static bool RemoveSignal(TileIndex tile, TileIndex front) { throw null; }

        /**
         * Get the baseprice of building a rail-related object.
         * @param railtype the railtype that is build (on)
         * @param build_type the type of object to build
         * @returns The baseprice of building the given object.
         */
        public static long GetBuildCost(RailType railtype, BuildType build_type) { throw null; }

        /**
         * Get the maximum speed of trains running on this railtype.
         * @param railtype The railtype to get the maximum speed of.
         * @returns The maximum speed trains can run on this railtype
         *   or 0 if there is no limit.
         * @note The speed is in OpenTTD's internal speed unit.
         *       This is mph / 1.6, which is roughly km/h.
         *       To get km/h multiply this number by 1.00584.
         */
        public static int GetMaxSpeed(RailType railtype) { throw null; }

        /**
         * Get the maintenance cost factor of a railtype.
         * @param railtype The railtype to get the maintenance factor of.
         * @returns Maintenance cost factor of the railtype.
         */
        public static int GetMaintenanceCostFactor(RailType railtype) { throw null; }
    }
}