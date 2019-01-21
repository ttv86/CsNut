namespace OpenTTD
{
    /// <summary>
    /// Creates a list of waypoints of which you are the owner.
    /// </summary>
    public class AIWaypointList : AIList<StationID>
    {
        /// <param name="waypoint_type">The type of waypoint to make a list of waypoints for.</param>
        public AIWaypointList(WaypointType waypoint_type) { throw null; }
    }

    /// <summary>
    /// Creates a list of waypoints which the vehicle has in its orders.
    /// </summary>
    public class AIWaypointList_Vehicle : AIList<StationID>
    {
        /// <param name="vehicle_id">The vehicle to get the list of waypoints he has in its orders from.</param>
        public AIWaypointList_Vehicle(VehicleID vehicle_id) { throw null; }
    }
}