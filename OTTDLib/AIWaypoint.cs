namespace OpenTTD
{
    /// <summary>Class that handles all waypoint related functions.</summary>
    public class AIWaypoint : AIBaseStation
    {
        /// <summary>Base for waypoint related errors </summary>
        public static readonly ErrorMessage ERR_WAYPOINT_BASE;

        /// <summary>The waypoint is build too close to another waypoint</summary>
        public static readonly ErrorMessage ERR_WAYPOINT_TOO_CLOSE_TO_ANOTHER_WAYPOINT;

        /// <summary>The waypoint would join more than one existing waypoint together.</summary>
        public static readonly ErrorMessage ERR_WAYPOINT_ADJOINS_MULTIPLE_WAYPOINTS;
        
        public static readonly WaypointType WAYPOINT_RAIL;
        public static readonly WaypointType WAYPOINT_BUOY;
        public static readonly WaypointType WAYPOINT_ANY;

        /**
         * Checks whether the given waypoint is valid and owned by you.
         * @param waypoint_id The waypoint to check.
         * @returns True if and only if the waypoint is valid.
         */
        public static bool IsValidWaypoint(StationID waypoint_id) { throw null; }

        /**
         * Get the StationID of a tile.
         * @param tile The tile to find the StationID of.
         * @returns StationID of the waypoint.
         */
        public static StationID GetWaypointID(TileIndex tile) { throw null; }

        /**
         * Check if any part of the waypoint contains a waypoint of the type waypoint_type
         * @param waypoint_id The waypoint to look at.
         * @param waypoint_type The WaypointType to look for.
         * @returns True if the waypoint has a waypoint part of the type waypoint_type.
         */
        public static bool HasWaypointType(StationID waypoint_id, WaypointType waypoint_type) { throw null; }
    }
}