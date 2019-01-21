namespace OpenTTD
{
    /// <summary>Creates a list of stations of which you are the owner.</summary>
    public class AIStationList : AIList<StationID>
    {
        /// <param name="station_type">The type of station to make a list of stations for.</param>
        public AIStationList(StationType station_type) { throw null; }
    }

    /**
     * Creates a list of stations associated with cargo at a station. This is very generic. Use the
     * subclasses for all practical purposes.
     */
    public class AIStationList_Cargo : AIList<StationID>
    {
        /// <summary>Criteria of selecting and grouping cargo at a station.</summary>
        public enum CargoSelector
        {
            CS_BY_FROM,
            CS_VIA_BY_FROM,
            CS_BY_VIA,
            CS_FROM_BY_VIA
        }

        /// <summary>Ways of associating cargo to stations.</summary>
        public enum CargoMode
        {
            CM_WAITING,
            CM_PLANNED
        }

        /**
         * Creates a list of stations associated with cargo in the specified way, selected and grouped
         * by the chosen criteria.
         * @param mode Mode of association, either waiting cargo or planned cargo.
         * @param selector Mode of grouping and selecting to be applied.
         * @param station_id Station to be queried.
         * @param cargo Cargo type to query for.
         * @param other_station Other station to restrict the query with.
         */
        public AIStationList_Cargo(AIStationList_Cargo.CargoMode mode, AIStationList_Cargo.CargoSelector selector, StationID station_id, CargoID cargo, StationID other_station) { throw null; }
    }

    /**
     * Creates a list of stations associated with cargo waiting at a station. This is very generic. Use
     * the subclasses for all practical purposes.
     */
    public class AIStationList_CargoWaiting : AIStationList_Cargo
    {
        /**
         * Creates a list of stations associated with waiting cargo, selected and grouped by the chosen
         * criteria.
         * @param selector Mode of grouping and selecting to be applied.
         * @param station_id Station to be queried.
         * @param cargo Cargo type to query for.
         * @param other_station Other station to restrict the query with.
         */
        public AIStationList_CargoWaiting(AIStationList_Cargo.CargoSelector selector, StationID station_id, CargoID cargo, StationID other_station)
            : base(CargoMode.CM_WAITING, selector, station_id, cargo, other_station)
        { throw null; }
    }

    /**
     * Creates a list of stations associated with cargo planned to pass a station. This is very
     * generic. Use the subclasses for all practical purposes.
     */
    public class AIStationList_CargoPlanned : AIStationList_Cargo
    {
        /**
         * Creates a list of stations associated with cargo planned to pass the station, selected and
         * grouped by the chosen criteria.
         * @param selector Mode of grouping and selecting to be applied.
         * @param station_id Station to be queried.
         * @param cargo Cargo type to query for.
         * @param other_station Other station to restrict the query with.
         */
        public AIStationList_CargoPlanned(AIStationList_Cargo.CargoSelector selector, StationID station_id, CargoID cargo, StationID other_station)
            : base(CargoMode.CM_PLANNED, selector, station_id, cargo, other_station)
        { throw null; }
    }

    /**
     * Creates a list of origin stations of waiting cargo at a station, with the amounts of cargo
     * waiting from each of those origin stations as values.
     */
    public class AIStationList_CargoWaitingByFrom : AIStationList_CargoWaiting
    {
        /// <param name="station_id">Station to query for waiting cargo.</param><param name="cargo">Cargo type to query for.</param>
        public AIStationList_CargoWaitingByFrom(StationID station_id, CargoID cargo) : base(CargoSelector.CS_BY_FROM, station_id, cargo, null) { throw null; }
    }

    /**
     * Creates a list of origin stations of cargo waiting at a station for a transfer via another
     * station, with the amounts of cargo waiting from each of those origin stations as values.
     */
    public class AIStationList_CargoWaitingViaByFrom : AIStationList_CargoWaiting
    {
        /// <param name="station_id">Station to query for waiting cargo.</param><param name="cargo">Cargo type to query for.</param><param name="via">Next hop to restrict the query with.</param>
        public AIStationList_CargoWaitingViaByFrom(StationID station_id, CargoID cargo, StationID via) : base(CargoSelector.CS_VIA_BY_FROM, station_id, cargo, null) { throw null; }
    }

    /**
     * Creates a list of next hops of waiting cargo at a station, with the amounts of cargo waiting for
     * each of those next hops as values.
     */
    public class AIStationList_CargoWaitingByVia : AIStationList_CargoWaiting
    {
        /// <param name="station_id">Station to query for waiting cargo.</param><param name="cargo">Cargo type to query for.</param>
        public AIStationList_CargoWaitingByVia(StationID station_id, CargoID cargo) : base(CargoSelector.CS_BY_VIA, station_id, cargo, null) { throw null; }
    }

    /**
     * Creates a list of next hops of waiting cargo from a specific station at another station, with
     * the amounts of cargo waiting for each of those next hops as values.
     */
    public class AIStationList_CargoWaitingFromByVia : AIStationList_CargoWaiting
    {
        /// <param name="station_id">Station to query for waiting cargo.</param><param name="cargo">Cargo type to query for.</param><param name="from">Origin station to restrict the query with.</param>
        public AIStationList_CargoWaitingFromByVia(StationID station_id, CargoID cargo, StationID from) : base(CargoSelector.CS_FROM_BY_VIA, station_id, cargo, null) { throw null; }
    }

    /**
     * Creates a list of origin stations of cargo planned to pass a station, with the monthly amounts
     * of cargo planned for each of those origin stations as values.
     */
    public class AIStationList_CargoPlannedByFrom : AIStationList_CargoPlanned
    {
        /// <param name="station_id">Station to query for planned flows.</param><param name="cargo">Cargo type to query for.</param>
        public AIStationList_CargoPlannedByFrom(StationID station_id, CargoID cargo) : base(CargoSelector.CS_BY_FROM, station_id, cargo, null) { throw null; }
    }

    /**
     * Creates a list of origin stations of cargo planned to pass a station going via another station,
     * with the monthly amounts of cargo planned for each of those origin stations as values.
     */
    public class AIStationList_CargoPlannedViaByFrom : AIStationList_CargoPlanned
    {
        /// <param name="station_id">Station to query for planned flows.</param><param name="cargo">Cargo type to query for.</param><param name="via">Next hop to restrict the query with.</param>
        public AIStationList_CargoPlannedViaByFrom(StationID station_id, CargoID cargo, StationID via) : base(CargoSelector.CS_VIA_BY_FROM, station_id, cargo, via) { throw null; }
    }

    /**
     * Creates a list of next hops of cargo planned to pass a station, with the monthly amounts of
     * cargo planned for each of those next hops as values.
     * Cargo planned to go "via" the station being queried will actually be delivered there.
     */
    public class AIStationList_CargoPlannedByVia : AIStationList_CargoPlanned
    {
        /// <param name="station_id">Station to query for planned flows.</param><param name="cargo">Cargo type to query for.</param>
        public AIStationList_CargoPlannedByVia(StationID station_id, CargoID cargo) : base(CargoSelector.CS_BY_VIA, station_id, cargo, null) { throw null; }
    }

    /**
     * Creates a list of next hops of cargo planned to pass a station and originating from another
     * station, with the monthly amounts of cargo planned for each of those next hops as values.
     * Cargo planned to go "via" the station being queried will actually be delivered there.
     */
    public class AIStationList_CargoPlannedFromByVia : AIStationList_CargoPlanned
    {
        /// <param name="station_id">Station to query for planned flows.</param><param name="cargo">Cargo type to query for.</param><param name="from">Origin station to restrict the query with.</param>
        public AIStationList_CargoPlannedFromByVia(StationID station_id, CargoID cargo, StationID from) : base(CargoSelector.CS_FROM_BY_VIA, station_id, cargo, from) { throw null; }
    }

    /// <summary>Creates a list of stations which the vehicle has in its orders.</summary>
    public class AIStationList_Vehicle : AIList<StationID>
    {
        /// <param name="vehicle_id">The vehicle to get the list of stations he has in its orders from.</param>
        public AIStationList_Vehicle(VehicleID vehicle_id) { throw null; }
    }
}