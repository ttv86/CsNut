namespace OpenTTD
{
    /// <summary>Base class for stations and waypoints.</summary>
    public class AIBaseStation
    {
        /**
         * Special station IDs for building adjacent/new stations when
         * the adjacent/distant join features are enabled.
         */
        public static readonly StationID STATION_NEW;
        public static readonly StationID STATION_JOIN_ADJACENT;
        public static readonly StationID STATION_INVALID;

        /**
         * Checks whether the given basestation is valid and owned by you.
         * @param station_id The station to check.
         * @returns True if and only if the basestation is valid.
         * @note IsValidBaseStation == (IsValidStation || IsValidWaypoint).
         */
        public static bool IsValidBaseStation(StationID station_id) { throw null; }

        /**
         * Get the name of a basestation.
         * @param station_id The basestation to get the name of.
         * @returns The name of the station.
         */
        public static string GetName(StationID station_id) { throw null; }

        /**
         * Set the name this basestation.
         * @param station_id The basestation to set the name of.
         * @param name The new name of the station (can be either a raw string, or a ScriptText object).
         * @exception AIError.ERR_NAME_IS_NOT_UNIQUE
         * @returns True if the name was changed.
         */
        public static bool SetName(StationID station_id, string name) { throw null; }

        /**
         * Get the current location of a basestation.
         * @param station_id The basestation to get the location of.
         * @returns The tile the basestation sign above it.
         * @note The tile is not necessarily a station tile (and if it is, it could also belong to another station).
         * @see ScriptTileList_StationType.
         */
        public static TileIndex GetLocation(StationID station_id) { throw null; }

        /**
         * Get the last date a station part was added to this station.
         * @param station_id The station to look at.
         * @returns The last date some part of this station was build.
         */
        public static Date GetConstructionDate(StationID station_id) { throw null; }
    }
}