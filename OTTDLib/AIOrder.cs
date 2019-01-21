namespace OpenTTD
{
    /// <summary>Class that handles all order related functions.</summary>
    public static class AIOrder
    {
        /// <summary>Base for all order related errors </summary>
        public static readonly ErrorMessage ERR_ORDER_BASE;

        /// <summary>No more space for orders </summary>
        public static readonly ErrorMessage ERR_ORDER_TOO_MANY;

        /// <summary>Destination of new order is to far away from the previous order </summary>
        public static readonly ErrorMessage ERR_ORDER_TOO_FAR_AWAY_FROM_PREVIOUS_DESTINATION;

        /// <summary>Aircraft has not enough range to copy/share orders. </summary>
        public static readonly ErrorMessage ERR_ORDER_AIRCRAFT_NOT_ENOUGH_RANGE;

        /// <summary>Flags that can be used to modify the behaviour of orders.</summary>
        /// <summary>Just go to the station/depot, stop unload if possible and load if needed. </summary>
        public static readonly OrderFlags OF_NONE;

        /// <summary>Do not stop at the stations that are passed when going to the destination. Only for trains and road vehicles. </summary>
        public static readonly OrderFlags OF_NON_STOP_INTERMEDIATE;
        /// <summary>Do not stop at the destination station. Only for trains and road vehicles. </summary>
        public static readonly OrderFlags OF_NON_STOP_DESTINATION;

        /// <summary>Always unload the vehicle, only for stations. Cannot be set when OF_TRANSFER or OF_NO_UNLOAD is set. </summary>
        public static readonly OrderFlags OF_UNLOAD;
        /// <summary>Transfer instead of deliver the goods, only for stations. Cannot be set when OF_UNLOAD or OF_NO_UNLOAD is set. </summary>
        public static readonly OrderFlags OF_TRANSFER;
        /// <summary>Never unload the vehicle, only for stations. Cannot be set when OF_UNLOAD, OF_TRANSFER or OF_NO_LOAD is set. </summary>
        public static readonly OrderFlags OF_NO_UNLOAD;

        /// <summary>Wt till the vehicle is fully loaded, only for stations. Cannot be set when OF_NO_LOAD is set. </summary>
        public static readonly OrderFlags OF_FULL_LOAD;
        /// <summary>Wt till at least one cargo of the vehicle is fully loaded, only for stations. Cannot be set when OF_NO_LOAD is set. </summary>
        public static readonly OrderFlags OF_FULL_LOAD_ANY;
        /// <summary>Do not load any cargo, only for stations. Cannot be set when OF_NO_UNLOAD, OF_FULL_LOAD or OF_FULL_LOAD_ANY is set. </summary>
        public static readonly OrderFlags OF_NO_LOAD;

        /// <summary>Service the vehicle when needed, otherwise skip this order, only for depots. </summary>
        public static readonly OrderFlags OF_SERVICE_IF_NEEDED;
        /// <summary>Stop in the depot instead of only go there for servicing, only for depots. </summary>
        public static readonly OrderFlags OF_STOP_IN_DEPOT;
        /// <summary>Go to nearest depot. </summary>
        public static readonly OrderFlags OF_GOTO_NEAREST_DEPOT;

        /// <summary>All flags related to non-stop settings. </summary>
        public static readonly OrderFlags OF_NON_STOP_FLAGS;
        /// <summary>All flags related to unloading. </summary>
        public static readonly OrderFlags OF_UNLOAD_FLAGS;
        /// <summary>All flags related to loading. </summary>
        public static readonly OrderFlags OF_LOAD_FLAGS;
        /// <summary>All flags related to depots. </summary>
        public static readonly OrderFlags OF_DEPOT_FLAGS;

        /// <summary>For marking invalid order flags </summary>
        public static readonly OrderFlags OF_INVALID;

        /// <summary>All conditions a conditional order can depend on.</summary>
        public enum OrderCondition
        {
            OC_LOAD_PERCENTAGE,
            OC_RELIABILITY,
            OC_MAX_RELIABILITY,
            OC_MAX_SPEED,
            OC_AGE,
            OC_REQUIRES_SERVICE,
            OC_UNCONDITIONALLY,
            OC_REMAINING_LIFETIME,

            OC_INVALID = -1,
        }

        /// <summary>Comparators for conditional orders.</summary>
        public enum CompareFunction
        {
            CF_EQUALS,
            CF_NOT_EQUALS,
            CF_LESS_THAN,
            CF_LESS_EQUALS,
            CF_MORE_THAN,
            CF_MORE_EQUALS,
            CF_IS_TRUE,
            CF_IS_FALSE,

            CF_INVALID = -1,
        }

        /**
        * Index in the list of orders for a vehicle. The first order has index 0, the second
        * order index 1, etc. The current order can be queried by using ORDER_CURRENT. Do not
        * use ORDER_INVALID yourself, it's used as return value by for example ResolveOrderPosition.
        * @note Automatic orders are hidden from scripts, so OrderPosition 0 will always be the first
        * manual order.
*/
        public enum OrderPosition
        {
            ORDER_CURRENT = 0xFF,
            ORDER_INVALID = -1,
        }

        /// <summary>Where to stop trains in a station that's longer than the train </summary>
        public enum StopLocation
        {
            STOPLOCATION_NEAR,
            STOPLOCATION_MIDDLE,
            STOPLOCATION_FAR,
            STOPLOCATION_INVALID = -1,
        }

        /**
         * Checks whether the given order id is valid for the given vehicle.
         * @param vehicle_id The vehicle to check the order index for.
         * @param order_position The order index to check.
         * @returns True if and only if the order_position is valid for the given vehicle.
         */
        public static bool IsValidVehicleOrder(VehicleID vehicle_id, OrderPosition order_position) { throw null; }

        /**
         * Checks whether the given order is a goto-station order.
         * @param vehicle_id The vehicle to check.
         * @param order_position The order index to check.
         * @returns True if and only if the order is a goto-station order.
         */
        public static bool IsGotoStationOrder(VehicleID vehicle_id, OrderPosition order_position) { throw null; }

        /**
         * Checks whether the given order is a goto-depot order.
         * @param vehicle_id The vehicle to check.
         * @param order_position The order index to check.
         * @returns True if and only if the order is a goto-depot order.
         */
        public static bool IsGotoDepotOrder(VehicleID vehicle_id, OrderPosition order_position) { throw null; }

        /**
         * Checks whether the given order is a goto-waypoint order.
         * @param vehicle_id The vehicle to check.
         * @param order_position The order index to check.
         * @returns True if and only if the order is a goto-waypoint order.
         */
        public static bool IsGotoWaypointOrder(VehicleID vehicle_id, OrderPosition order_position) { throw null; }

        /**
         * Checks whether the given order is a conditional order.
         * @param vehicle_id The vehicle to check.
         * @param order_position The order index to check.
         * @returns True if and only if the order is a conditional order.
         */
        public static bool IsConditionalOrder(VehicleID vehicle_id, OrderPosition order_position) { throw null; }

        /**
         * Checks whether the given order is a void order.
         * A void order is an order that used to be a goto station, depot or waypoint order but
         * its destination got removed. In OpenTTD these orders as shown as "(Invalid Order)"
         * in the order list of a vehicle.
         * @param vehicle_id The vehicle to check.
         * @param order_position The order index to check.
         * @returns True if and only if the order is a void order.
         */
        public static bool IsVoidOrder(VehicleID vehicle_id, OrderPosition order_position) { throw null; }

        /**
         * Checks whether the given order has a valid refit cargo.
         * @param vehicle_id The vehicle to check.
         * @param order_position The order index to check.
         * @returns True if and only if the order is has a valid refit cargo.
         */
        public static bool IsRefitOrder(VehicleID vehicle_id, OrderPosition order_position) { throw null; }

        /**
         * Checks whether the current order is part of the orderlist.
         * @param vehicle_id The vehicle to check.
         * @returns True if and only if the current order is part of the order list.
         * @note If the order is a non-'non-stop' order, and the vehicle is currently
         * (un)loading at a station that is not the final destination, this function
         * will still return true.
         */
        public static bool IsCurrentOrderPartOfOrderList(VehicleID vehicle_id) { throw null; }

        /**
         * Resolves the given order index to the correct index for the given vehicle.
         *  If the order index was ORDER_CURRENT it will be resolved to the index of
         *  the current order (as shown in the order list). If the order with the
         *  given index does not exist it will return ORDER_INVALID.
         * @param vehicle_id The vehicle to check the order index for.
         * @param order_position The order index to resolve.
         * @returns The resolved order index.
         */
        public static OrderPosition ResolveOrderPosition(VehicleID vehicle_id, OrderPosition order_position) { throw null; }

        /**
         * Checks whether the given order flags are valid for the given destination.
         * @param destination The destination of the order.
         * @param order_flags The flags given to the order.
         * @returns True if and only if the order_flags are valid for the given location.
         */
        public static bool AreOrderFlagsValid(TileIndex destination, OrderFlags order_flags) { throw null; }

        /**
         * Checks whether the given combination of condition and compare function is valid.
         * @param condition The condition to check.
         * @param compare The compare function to check.
         * @returns True if and only if the combination of condition and compare function is valid.
         */
        public static bool IsValidConditionalOrder(OrderCondition condition, CompareFunction compare) { throw null; }

        /**
         * Returns the number of orders for the given vehicle.
         * @param vehicle_id The vehicle to get the order count of.
         * @returns The number of orders for the given vehicle or a negative
         *   value when the vehicle does not exist.
         */
        public static int GetOrderCount(VehicleID vehicle_id) { throw null; }

        /**
         * Gets the destination of the given order for the given vehicle.
         * @param vehicle_id The vehicle to get the destination for.
         * @param order_position The order to get the destination for.
         * @note Giving ORDER_CURRENT as order_position will give the order that is
         *  currently being executed by the vehicle. This is not necessarily the
         *  current order as given by ResolveOrderPosition (the current index in the
         *  order list) as manual or autoservicing depot orders do not show up
         *  in the orderlist, but they can be the current order of a vehicle.
         * @returns The destination tile of the order.
         */
        public static TileIndex GetOrderDestination(VehicleID vehicle_id, OrderPosition order_position) { throw null; }

        /**
         * Gets the ScriptOrderFlags of the given order for the given vehicle.
         * @param vehicle_id The vehicle to get the destination for.
         * @param order_position The order to get the destination for.
         * @note Giving ORDER_CURRENT as order_position will give the order that is
         *  currently being executed by the vehicle. This is not necessarily the
         *  current order as given by ResolveOrderPosition (the current index in the
         *  order list) as manual or autoservicing depot orders do not show up
         *  in the orderlist, but they can be the current order of a vehicle.
         * @returns The ScriptOrderFlags of the order.
         */
        public static OrderFlags GetOrderFlags(VehicleID vehicle_id, OrderPosition order_position) { throw null; }

        /**
         * Gets the OrderPosition to jump to if the check succeeds of the given order for the given vehicle.
         * @param vehicle_id The vehicle to get the OrderPosition for.
         * @param order_position The order to get the OrderPosition for.
         * @returns The target of the conditional jump.
         */
        public static OrderPosition GetOrderJumpTo(VehicleID vehicle_id, OrderPosition order_position) { throw null; }

        /**
         * Gets the OrderCondition of the given order for the given vehicle.
         * @param vehicle_id The vehicle to get the condition type for.
         * @param order_position The order to get the condition type for.
         * @returns The OrderCondition of the order.
         */
        public static OrderCondition GetOrderCondition(VehicleID vehicle_id, OrderPosition order_position) { throw null; }

        /**
         * Gets the CompareFunction of the given order for the given vehicle.
         * @param vehicle_id The vehicle to get the compare function for.
         * @param order_position The order to get the compare function for.
         * @returns The CompareFunction of the order.
         */
        public static CompareFunction GetOrderCompareFunction(VehicleID vehicle_id, OrderPosition order_position) { throw null; }

        /**
         * Gets the value to compare against of the given order for the given vehicle.
         * @param vehicle_id The vehicle to get the value for.
         * @param order_position The order to get the value for.
         * @returns The value to compare against of the order.
         */
        public static int GetOrderCompareValue(VehicleID vehicle_id, OrderPosition order_position) { throw null; }

        /**
         * Gets the stoplocation of the given order for the given train.
         * @param vehicle_id The vehicle to get the value for.
         * @param order_position The order to get the value for.
         * @returns The relative position where the train will stop inside a station.
         */
        public static StopLocation GetStopLocation(VehicleID vehicle_id, OrderPosition order_position) { throw null; }

        /**
         * Gets the refit cargo type of the given order for the given vehicle.
         * @param vehicle_id The vehicle to get the refit cargo for.
         * @param order_position The order to get the refit cargo for.
         * @note Giving ORDER_CURRENT as order_position will give the order that is
         *  currently being executed by the vehicle. This is not necessarily the
         *  current order as given by ResolveOrderPosition (the current index in the
         *  order list) as manual or autoservicing depot orders do not show up
         *  in the orderlist, but they can be the current order of a vehicle.
         * @returns The refit cargo of the order or CT_NO_REFIT if no refit is set.
         */
        public static CargoID GetOrderRefit(VehicleID vehicle_id, OrderPosition order_position) { throw null; }

        /**
         * Sets the OrderPosition to jump to if the check succeeds of the given order for the given vehicle.
         * @param vehicle_id The vehicle to set the OrderPosition for.
         * @param order_position The order to set the OrderPosition for.
         * @param jump_to The order to jump to if the check succeeds.
         * @returns Whether the order has been/can be changed.
         */
        public static bool SetOrderJumpTo(VehicleID vehicle_id, OrderPosition order_position, OrderPosition jump_to) { throw null; }

        /**
         * Sets the OrderCondition of the given order for the given vehicle.
         * @param vehicle_id The vehicle to set the condition type for.
         * @param order_position The order to set the condition type for.
         * @param condition The condition to compare on.
         * @returns Whether the order has been/can be changed.
         */
        public static bool SetOrderCondition(VehicleID vehicle_id, OrderPosition order_position, OrderCondition condition) { throw null; }

        /**
         * Sets the CompareFunction of the given order for the given vehicle.
         * @param vehicle_id The vehicle to set the compare function for.
         * @param order_position The order to set the compare function for.
         * @param compare The new compare function of the order.
         * @returns Whether the order has been/can be changed.
         */
        public static bool SetOrderCompareFunction(VehicleID vehicle_id, OrderPosition order_position, CompareFunction compare) { throw null; }

        /**
         * Sets the value to compare against of the given order for the given vehicle.
         * @param vehicle_id The vehicle to set the value for.
         * @param order_position The order to set the value for.
         * @param value The value to compare against.
         * @returns Whether the order has been/can be changed.
         */
        public static bool SetOrderCompareValue(VehicleID vehicle_id, OrderPosition order_position, int value) { throw null; }

        /**
         * Sets the stoplocation of the given order for the given train.
         * @param vehicle_id The vehicle to get the value for.
         * @param order_position The order to get the value for.
         * @param stop_location The relative position where a train will stop inside a station.
         * @returns Whether the order has been/can be changed.
         */
        public static bool SetStopLocation(VehicleID vehicle_id, OrderPosition order_position, StopLocation stop_location) { throw null; }

        /**
         * Sets the refit cargo type of the given order for the given vehicle.
         * @param vehicle_id The vehicle to set the refit cargo for.
         * @param order_position The order to set the refit cargo for.
         * @param refit_cargo The cargo to refit to. The refit can be cleared by passing CT_NO_REFIT.
         * @returns Whether the order has been/can be changed.
         */
        public static bool SetOrderRefit(VehicleID vehicle_id, OrderPosition order_position, CargoID refit_cargo) { throw null; }

        /**
         * Appends an order to the end of the vehicle's order list.
         * @param vehicle_id The vehicle to append the order to.
         * @param destination The destination of the order.
         * @param order_flags The flags given to the order.
         * @exception AIError.ERR_OWNED_BY_ANOTHER_COMPANY
         * @exception AIOrder.ERR_ORDER_TOO_MANY
         * @exception AIOrder.ERR_ORDER_TOO_FAR_AWAY_FROM_PREVIOUS_DESTINATION
         * @returns True if and only if the order was appended.
         */
        public static bool AppendOrder(VehicleID vehicle_id, TileIndex destination, OrderFlags order_flags) { throw null; }

        /**
         * Appends a conditional order to the end of the vehicle's order list.
         * @param vehicle_id The vehicle to append the order to.
         * @param jump_to The OrderPosition to jump to if the condition is true.
         * @exception AIError.ERR_OWNED_BY_ANOTHER_COMPANY
         * @exception AIOrder.ERR_ORDER_TOO_MANY
         * @returns True if and only if the order was appended.
         */
        public static bool AppendConditionalOrder(VehicleID vehicle_id, OrderPosition jump_to) { throw null; }

        /**
         * Inserts an order before the given order_position into the vehicle's order list.
         * @param vehicle_id The vehicle to add the order to.
         * @param order_position The order to place the new order before.
         * @param destination The destination of the order.
         * @param order_flags The flags given to the order.
         * @exception AIError.ERR_OWNED_BY_ANOTHER_COMPANY
         * @exception AIOrder.ERR_ORDER_TOO_MANY
         * @exception AIOrder.ERR_ORDER_TOO_FAR_AWAY_FROM_PREVIOUS_DESTINATION
         * @returns True if and only if the order was inserted.
         */
        public static bool InsertOrder(VehicleID vehicle_id, OrderPosition order_position, TileIndex destination, OrderFlags order_flags) { throw null; }

        /**
         * Appends a conditional order before the given order_position into the vehicle's order list.
         * @param vehicle_id The vehicle to add the order to.
         * @param order_position The order to place the new order before.
         * @param jump_to The OrderPosition to jump to if the condition is true.
         * @exception AIError.ERR_OWNED_BY_ANOTHER_COMPANY
         * @exception AIOrder.ERR_ORDER_TOO_MANY
         * @returns True if and only if the order was inserted.
         */
        public static bool InsertConditionalOrder(VehicleID vehicle_id, OrderPosition order_position, OrderPosition jump_to) { throw null; }

        /**
         * Removes an order from the vehicle's order list.
         * @param vehicle_id The vehicle to remove the order from.
         * @param order_position The order to remove from the order list.
         * @exception AIError.ERR_OWNED_BY_ANOTHER_COMPANY
         * @returns True if and only if the order was removed.
         */
        public static bool RemoveOrder(VehicleID vehicle_id, OrderPosition order_position) { throw null; }

        /**
         * Changes the order flags of the given order.
         * @param vehicle_id The vehicle to change the order of.
         * @param order_position The order to change.
         * @param order_flags The new flags given to the order.
         * @exception AIError.ERR_OWNED_BY_ANOTHER_COMPANY
         * @returns True if and only if the order was changed.
         */
        public static bool SetOrderFlags(VehicleID vehicle_id, OrderPosition order_position, OrderFlags order_flags) { throw null; }

        /**
         * Move an order inside the orderlist
         * @param vehicle_id The vehicle to move the orders.
         * @param order_position_move The order to move.
         * @param order_position_target The target order
         * @exception AIError.ERR_OWNED_BY_ANOTHER_COMPANY
         * @returns True if and only if the order was moved.
         * @note If the order is moved to a lower place (e.g. from 7 to 2)
         *  the target order is moved upwards (e.g. 3). If the order is moved
         *  to a higher place (e.g. from 7 to 9) the target will be moved
         *  downwards (e.g. 8).
         */
        public static bool MoveOrder(VehicleID vehicle_id, OrderPosition order_position_move, OrderPosition order_position_target) { throw null; }

        /**
         * Make a vehicle execute next_order instead of its current order.
         * @param vehicle_id The vehicle that should skip some orders.
         * @param next_order The order the vehicle should skip to.
         * @exception AIError.ERR_OWNED_BY_ANOTHER_COMPANY
         * @returns True if and only the current order was changed.
         */
        public static bool SkipToOrder(VehicleID vehicle_id, OrderPosition next_order) { throw null; }

        /**
         * Copies the orders from another vehicle. The orders of the main vehicle
         *  are going to be the orders of the changed vehicle.
         * @param vehicle_id The vehicle to copy the orders to.
         * @param main_vehicle_id The vehicle to copy the orders from.
         * @exception AIError.ERR_OWNED_BY_ANOTHER_COMPANY
         * @exception AIOrder.ERR_ORDER_TOO_MANY
         * @exception AIOrder.ERR_ORDER_AIRCRAFT_NOT_ENOUGH_RANGE
         * @returns True if and only if the copying succeeded.
         */
        public static bool CopyOrders(VehicleID vehicle_id, VehicleID main_vehicle_id) { throw null; }

        /**
         * Shares the orders between two vehicles. The orders of the main
         * vehicle are going to be the orders of the changed vehicle.
         * @param vehicle_id The vehicle to add to the shared order list.
         * @param main_vehicle_id The vehicle to share the orders with.
         * @exception AIError.ERR_OWNED_BY_ANOTHER_COMPANY
         * @exception AIOrder.ERR_ORDER_AIRCRAFT_NOT_ENOUGH_RANGE
         * @returns True if and only if the sharing succeeded.
         */
        public static bool ShareOrders(VehicleID vehicle_id, VehicleID main_vehicle_id) { throw null; }

        /**
         * Removes the given vehicle from a shared orders list.
         * @param vehicle_id The vehicle to remove from the shared order list.
         * @returns True if and only if the unsharing succeeded.
         */
        public static bool UnshareOrders(VehicleID vehicle_id) { throw null; }

        /**
         * Get the distance between two points for a vehicle type.
         * Use this function to compute the distance between two tiles wrt. a vehicle type.
         * These vehicle-type specific distances are independent from other map distances, you may
         * use the result of this function to compare it with the result of
         * AIEngine.GetMaximumOrderDistance or AIVehicle.GetMaximumOrderDistance.
         * @param vehicle_type The vehicle type to get the distance for.
         * @param origin_tile Origin, can be any tile or a tile of a specific station.
         * @param dest_tile Destination, can be any tile or a tile of a specific station.
         * @returns The distance between the origin and the destination for a
         *         vehicle of the given vehicle type.
         * @note   The unit of the order distances is unspecified and should
         *         not be compared with map distances
         * @see AIEngine.GetMaximumOrderDistance and AIVehicle.GetMaximumOrderDistance
         */
        public static int GetOrderDistance(VehicleType vehicle_type, TileIndex origin_tile, TileIndex dest_tile) { throw null; }
    }
}