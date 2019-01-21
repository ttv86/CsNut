namespace OpenTTD
{
    /// <summary>Creates a list of vehicles of which you are the owner.</summary>
    public class AIVehicleList : AIList<VehicleID>
    {
        public AIVehicleList() { throw null; }
    }

    /// <summary>Creates a list of vehicles that have orders to a given station.</summary>
    public class AIVehicleList_Station : AIList<VehicleID>
    {
        /// <param name="station_id">The station to get the list of vehicles from, which have orders to it.</param>
        public AIVehicleList_Station(StationID station_id) { throw null; }
    }

    /**
     * Creates a list of vehicles that have orders to a given depot.
     * The list is created with a tile. If the tile is part of an airport all
     * aircraft having a depot order on a hangar of that airport will be
     * returned. For all other vehicle types the tile has to be a depot or
     * an empty list will be returned.
     */
    public class AIVehicleList_Depot : AIList<VehicleID>
    {
        /// <param name="tile">The tile of the depot to get the list of vehicles from, which have orders to it.</param>
        public AIVehicleList_Depot(TileIndex tile) { throw null; }
    }

    /// <summary>Creates a list of vehicles that share orders.</summary>
    public class AIVehicleList_SharedOrders : AIList<VehicleID>
    {
        /// <param name="vehicle_id">The vehicle that the rest shared orders with.</param>
        public AIVehicleList_SharedOrders(VehicleID vehicle_id) { throw null; }
    }

    /// <summary>Creates a list of vehicles that are in a group.</summary>
    public class AIVehicleList_Group : AIList<VehicleID>
    {
        /// <param name="group_id">The ID of the group the vehicles are in.</param>
        public AIVehicleList_Group(GroupID group_id) { throw null; }
    }

    /// <summary>Creates a list of vehicles that are in the default group.</summary>
    public class AIVehicleList_DefaultGroup : AIList<VehicleID>
    {
        /// <param name="vehicle_type">The VehicleType to get the list of vehicles for.</param>
        public AIVehicleList_DefaultGroup(VehicleType vehicle_type) { throw null; }
    }
}