namespace OpenTTD
{
    /// <summary>Class that handles all station related functions.</summary>
    public class AIStation : AIBaseStation
    {
        /// <summary>Base for station related errors </summary>
        public static readonly ErrorMessage ERR_STATION_BASE;

        /// <summary>The station is build too close to another station, airport or dock </summary>
        public static readonly ErrorMessage ERR_STATION_TOO_CLOSE_TO_ANOTHER_STATION;

        /// <summary>There are too many stations, airports and docks in the game </summary>
        public static readonly ErrorMessage ERR_STATION_TOO_MANY_STATIONS;

        /// <summary>There are too many stations, airports of docks in a town </summary>
        public static readonly ErrorMessage ERR_STATION_TOO_MANY_STATIONS_IN_TOWN;

        public static readonly StationType STATION_TRAIN;
        public static readonly StationType STATION_TRUCK_STOP;
        public static readonly StationType STATION_BUS_STOP;
        public static readonly StationType STATION_AIRPORT;
        public static readonly StationType STATION_DOCK;
        public static readonly StationType STATION_ANY;

        /**
         * Checks whether the given station is valid and owned by you.
         * @param station_id The station to check.
         * @returns True if and only if the station is valid.
         */
        public static bool IsValidStation(StationID station_id) { return Testing.AIStation.IsValidStation(station_id); }

        /**
         * Get the StationID of a tile, if there is a station.
         * @param tile The tile to find the stationID of
         * @returns StationID of the station.
         */
        public static StationID GetStationID(TileIndex tile) { throw null; }

        /**
         * See how much cargo there is waiting on a station.
         * @param station_id The station to get the cargo-waiting of.
         * @param cargo_id The cargo to get the cargo-waiting of.
         * @returns The amount of units waiting at the station.
         */
        public static int GetCargoWaiting(StationID station_id, CargoID cargo_id) { throw null; }

        /**
         * See how much cargo with a specific source station there is waiting on a station.
         * @param station_id The station to get the cargo-waiting of.
         * @param from_station_id The source station of the cargo. Pass STATION_INVALID to get cargo of which the source has been deleted.
         * @param cargo_id The cargo to get the cargo-waiting of.
         * @returns The amount of units waiting at the station originating from from_station_id.
         * @note source station means, the station where cargo was first loaded.
         */
        public static int GetCargoWaitingFrom(StationID station_id, StationID from_station_id, CargoID cargo_id) { throw null; }

        /**
         * See how much cargo with a specific via-station there is waiting on a station.
         * @param station_id The station to get the cargo-waiting of.
         * @param via_station_id The next station the cargo is going to. Pass STATION_INVALID to get waiting cargo for "via any station".
         * @param cargo_id The cargo to get the cargo-waiting of.
         * @returns The amount of units waiting at the station with via_station_id as next hop.
         * @note if ScriptCargo.GetCargoDistributionType(cargo_id) == ScriptCargo.DT_MANUAL, then all waiting cargo will have STATION_INVALID as next hop.
         */
        public static int GetCargoWaitingVia(StationID station_id, StationID via_station_id, CargoID cargo_id) { throw null; }

        /**
         * See how much cargo with a specific via-station and source station there is waiting on a station.
         * @param station_id The station to get the cargo-waiting of.
         * @param from_station_id The source station of the cargo. Pass STATION_INVALID to get cargo of which the source has been deleted.
         * @param via_station_id The next station the cargo is going to. Pass STATION_INVALID to get waiting cargo for "via any station".
         * @param cargo_id The cargo to get the cargo-waiting of.
         * @returns The amount of units waiting at the station with from_station_id as source and via_station_id as next hop.
         * @note if ScriptCargo.GetCargoDistributionType(cargo_id) == ScriptCargo.DT_MANUAL, then all waiting cargo will have STATION_INVALID as next hop.
         */
        public static int GetCargoWaitingFromVia(StationID station_id, StationID from_station_id, StationID via_station_id, CargoID cargo_id) { throw null; }

        /**
         * See how much cargo was planned to pass (including production and consumption) this station per month.
         * @param station_id The station to get the planned flow for.
         * @param cargo_id The cargo type to get the planned flow for.
         * @returns The amount of cargo units planned to pass the station per month.
         */
        public static int GetCargoPlanned(StationID station_id, CargoID cargo_id) { throw null; }

        /**
         * See how much cargo from the specified origin was planned to pass (including production and consumption) this station per month.
         * @param station_id The station to get the planned flow for.
         * @param from_station_id The station the cargo originates at.
         * @param cargo_id The cargo type to get the planned flow for.
         * @returns The amount of cargo units from the specified origin planned to pass the station per month.
         */
        public static int GetCargoPlannedFrom(StationID station_id, StationID from_station_id, CargoID cargo_id) { throw null; }

        /**
         * See how much cargo was planned to pass (including production and consumption) this station per month, heading for the specified next hop.
         * @param station_id The station to get the planned flow for.
         * @param via_station_id The next station the cargo will go on to.
         * @param cargo_id The cargo type to get the planned flow for.
         * @returns The amount of cargo units planned to pass the station per month, going via the specified next hop.
         * @note Cargo planned to go "via" the same station that's being queried is actually planned to be consumed there.
         */
        public static int GetCargoPlannedVia(StationID station_id, StationID via_station_id, CargoID cargo_id) { throw null; }

        /**
         * See how much cargo from the specified origin was planned to pass this station per month,
         * heading for the specified next hop.
         * @param station_id The station to get the planned flow for.
         * @param from_station_id The station the cargo originates at.
         * @param via_station_id The next station the cargo will go on to.
         * @param cargo_id The cargo type to get the planned flow for.
         * @returns The amount of cargo units from the specified origin planned to pass the station per month, going via the specified next hop.
         * @note Cargo planned to go "via" the same station that's being queried is actually planned to be consumed there.
         * @note Cargo planned to pass "from" the same station that's being queried is actually produced there.
         */
        public static int GetCargoPlannedFromVia(StationID station_id, StationID from_station_id, StationID via_station_id, CargoID cargo_id) { throw null; }

        /**
         * Check whether the given cargo at the given station a rating.
         * @param station_id The station to get the cargo-rating state of.
         * @param cargo_id The cargo to get the cargo-rating state of.
         * @returns True if the cargo has a rating, otherwise false.
         */
        public static bool HasCargoRating(StationID station_id, CargoID cargo_id) { throw null; }

        /**
         * See how high the rating is of a cargo on a station.
         * @param station_id The station to get the cargo-rating of.
         * @param cargo_id The cargo to get the cargo-rating of.
         * @returns The rating in percent of the cargo on the station.
         */
        public static int GetCargoRating(StationID station_id, CargoID cargo_id) { throw null; }

        /**
         * Get the coverage radius of this type of station.
         * @param station_type The type of station.
         * @returns The radius in tiles.
         * @note Coverage radius of airports needs to be requested via AIAirport.GetAirportCoverageRadius(), as it requires AirportType.
         */
        public static int GetCoverageRadius(StationType station_type) { throw null; }

        /**
         * Get the coverage radius of this station.
         * @param station_id The station to get the coverage radius of.
         * @returns The radius in tiles.
         */
        public static int GetStationCoverageRadius(StationID station_id) { throw null; }

        /**
         * Get the manhattan distance from the tile to the AIStation.GetLocation()
         *  of the station.
         * @param station_id The station to get the distance to.
         * @param tile The tile to get the distance to.
         * @returns The distance between station and tile.
         */
        public static int GetDistanceManhattanToTile(StationID station_id, TileIndex tile) { throw null; }

        /**
         * Get the square distance from the tile to the AIStation.GetLocation()
         *  of the station.
         * @param station_id The station to get the distance to.
         * @param tile The tile to get the distance to.
         * @returns The distance between station and tile.
         */
        public static int GetDistanceSquareToTile(StationID station_id, TileIndex tile) { throw null; }

        /**
         * Find out if this station is within the rating influence of a town.
         *  The service quality of stations with signs within this radius
         *  influences the rating of the town.
         * @param station_id The station to check.
         * @param town_id The town to check.
         * @returns True if the tile is within the rating influence of the town.
         */
        public static bool IsWithinTownInfluence(StationID station_id, TownID town_id) { throw null; }

        /**
         * Check if any part of the station contains a station of the type
         *  StationType
         * @param station_id The station to look at.
         * @param station_type The StationType to look for.
         * @returns True if the station has a station part of the type StationType.
         */
        public static bool HasStationType(StationID station_id, StationType station_type) { throw null; }

        /**
         * Check if any part of the station contains a station of the type
         *  RoadType.
         * @param station_id The station to look at.
         * @param road_type The RoadType to look for.
         * @returns True if the station has a station part of the type RoadType.
         */
        public static bool HasRoadType(StationID station_id, RoadType road_type) { throw null; }

        /**
         * Get the town that was nearest to the given station when the station was built.
         * @param station_id The station to look at.
         * @returns The TownID of the town whose center tile was closest to the station
         *  at the time the station was built.
         * @note There is no guarantee that the station is even near the returned town
         *  nor that the returns town is closest to the station now. A station that was
         *  'walked' to the other end of the map will still return the same town. Also,
         *  towns grow, towns change. So don't depend on this value too much.
         */
        public static TownID GetNearestTown(StationID station_id) { throw null; }

        /**
         * Get the open/closed state of an airport.
         * @param station_id The airport to look at.
         * @returns True if the airport is currently closed to incoming traffic.
         */
        public static bool IsAirportClosed(StationID station_id) { throw null; }

        /**
         * Toggle the open/closed state of an airport.
         * @param station_id The airport to modify.
         * @returns True if the state was toggled successfully.
         */
        public static bool OpenCloseAirport(StationID station_id) { throw null; }
    }
}