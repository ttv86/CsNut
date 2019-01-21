namespace OpenTTD
{
    /// <summary>Class that handles all vehicle related functions.</summary>
    public static class AIVehicle
    {
        public static readonly VehicleID VEHICLE_INVALID;
        
        /// <summary>Base for vehicle related errors </summary>
        public static readonly ErrorMessage ERR_VEHICLE_BASE;

        /// <summary>Too many vehicles in the game, can't build any more. </summary>
        public static readonly ErrorMessage ERR_VEHICLE_TOO_MANY;

        /// <summary>Vehicle is not available </summary>
        public static readonly ErrorMessage ERR_VEHICLE_NOT_AVAILABLE;

        /// <summary>Vehicle can't be build due to game settigns </summary>
        public static readonly ErrorMessage ERR_VEHICLE_BUILD_DISABLED;

        /// <summary>Vehicle can't be build in the selected depot </summary>
        public static readonly ErrorMessage ERR_VEHICLE_WRONG_DEPOT;

        /// <summary>Vehicle can't return to the depot </summary>
        public static readonly ErrorMessage ERR_VEHICLE_CANNOT_SEND_TO_DEPOT;

        /// <summary>Vehicle can't start / stop </summary>
        public static readonly ErrorMessage ERR_VEHICLE_CANNOT_START_STOP;

        /// <summary>Vehicle can't turn </summary>
        public static readonly ErrorMessage ERR_VEHICLE_CANNOT_TURN;

        /// <summary>Vehicle can't be refit </summary>
        public static readonly ErrorMessage ERR_VEHICLE_CANNOT_REFIT;

        /// <summary>Vehicle is destroyed </summary>
        public static readonly ErrorMessage ERR_VEHICLE_IS_DESTROYED;

        /// <summary>Vehicle is not in a depot </summary>
        public static readonly ErrorMessage ERR_VEHICLE_NOT_IN_DEPOT;

        /// <summary>Vehicle is flying </summary>
        public static readonly ErrorMessage ERR_VEHICLE_IN_FLIGHT;

        /// <summary>Vehicle is without power </summary>
        public static readonly ErrorMessage ERR_VEHICLE_NO_POWER;

        /// <summary>Vehicle would get too long during construction. </summary>
        public static readonly ErrorMessage ERR_VEHICLE_TOO_LONG;

        /**
         * The type of a vehicle available in the game. Trams for example are
         *  road vehicles, as maglev is a rail vehicle.
         */
        public static readonly VehicleType VT_RAIL;
        public static readonly VehicleType VT_ROAD;
        public static readonly VehicleType VT_WATER;
        public static readonly VehicleType VT_AIR;
        public static readonly VehicleType VT_INVALID;

        /// <summary>The different states a vehicle can be in.</summary>
        public static readonly VehicleState VS_RUNNING;
        public static readonly VehicleState VS_STOPPED;
        public static readonly VehicleState VS_IN_DEPOT;
        public static readonly VehicleState VS_AT_STATION;
        public static readonly VehicleState VS_BROKEN;
        public static readonly VehicleState VS_CRASHED;
        public static readonly VehicleState VS_INVALID;

        /**
         * Checks whether the given vehicle is valid and owned by you.
         * @param vehicle_id The vehicle to check.
         * @returns True if and only if the vehicle is valid.
         */
        public static bool IsValidVehicle(VehicleID vehicle_id) { throw null; }

        /**
         * Get the number of wagons a vehicle has.
         * @param vehicle_id The vehicle to get the number of wagons from.
         * @returns The number of wagons the vehicle has.
         */
        public static int GetNumWagons(VehicleID vehicle_id) { throw null; }

        /**
         * Set the name of a vehicle.
         * @param vehicle_id The vehicle to set the name for.
         * @param name The name for the vehicle (can be either a raw string, or a ScriptText object).
         * @exception AIError.ERR_NAME_IS_NOT_UNIQUE
         * @returns True if and only if the name was changed.
         */
        public static bool SetName(VehicleID vehicle_id, string name) { throw null; }

        /**
         * Get the name of a vehicle.
         * @param vehicle_id The vehicle to get the name of.
         * @returns The name the vehicle has.
         */
        public static string GetName(VehicleID vehicle_id) { throw null; }

        /**
         * Get the current location of a vehicle.
         * @param vehicle_id The vehicle to get the location of.
         * @returns The tile the vehicle is currently on.
         */
        public static TileIndex GetLocation(VehicleID vehicle_id) { throw null; }

        /**
         * Get the engine-type of a vehicle.
         * @param vehicle_id The vehicle to get the engine-type of.
         * @returns The engine type the vehicle has.
         */
        public static EngineID GetEngineType(VehicleID vehicle_id) { throw null; }

        /**
         * Get the engine-type of a wagon.
         * @param vehicle_id The vehicle to get the engine-type of.
         * @param wagon The wagon in the vehicle to get the engine-type of.
         * @returns The engine type the vehicle has.
         */
        public static EngineID GetWagonEngineType(VehicleID vehicle_id, int wagon) { throw null; }

        /**
         * Get the unitnumber of a vehicle.
         * @param vehicle_id The vehicle to get the unitnumber of.
         * @returns The unitnumber the vehicle has.
         */
        public static int GetUnitNumber(VehicleID vehicle_id) { throw null; }

        /**
         * Get the current age of a vehicle.
         * @param vehicle_id The vehicle to get the age of.
         * @returns The current age the vehicle has.
         * @note The age is in days.
         */
        public static int GetAge(VehicleID vehicle_id) { throw null; }

        /**
         * Get the current age of a second (or third, etc.) engine in a train vehicle.
         * @param vehicle_id The vehicle to get the age of.
         * @param wagon The wagon in the vehicle to get the age of.
         * @returns The current age the vehicle has.
         * @note The age is in days.
         */
        public static int GetWagonAge(VehicleID vehicle_id, int wagon) { throw null; }

        /**
         * Get the maximum age of a vehicle.
         * @param vehicle_id The vehicle to get the age of.
         * @returns The maximum age the vehicle has.
         * @note The age is in days.
         */
        public static int GetMaxAge(VehicleID vehicle_id) { throw null; }

        /**
         * Get the age a vehicle has left (maximum - current).
         * @param vehicle_id The vehicle to get the age of.
         * @returns The age the vehicle has left.
         * @note The age is in days.
         */
        public static int GetAgeLeft(VehicleID vehicle_id) { throw null; }

        /**
         * Get the current speed of a vehicle.
         * @param vehicle_id The vehicle to get the speed of.
         * @returns The current speed of the vehicle.
         * @note The speed is in OpenTTD's internal speed unit.
         *       This is mph / 1.6, which is roughly km/h.
         *       To get km/h multiply this number by 1.00584.
         */
        public static int GetCurrentSpeed(VehicleID vehicle_id) { throw null; }

        /**
         * Get the current state of a vehicle.
         * @param vehicle_id The vehicle to get the state of.
         * @returns The current state of the vehicle.
         */
        public static VehicleState GetState(VehicleID vehicle_id) { throw null; }

        /**
         * Get the running cost of this vehicle.
         * @param vehicle_id The vehicle to get the running cost of.
         * @returns The running cost of the vehicle per year.
         * @note Cost is per year; divide by 365 to get per day.
         * @note This is not equal to AIEngine.GetRunningCost for Trains, because
         *   wagons and second engines can add up in the calculation too.
         */
        public static long GetRunningCost(VehicleID vehicle_id) { throw null; }

        /**
         * Get the current profit of a vehicle.
         * @param vehicle_id The vehicle to get the profit of.
         * @returns The current profit the vehicle has.
         */
        public static long GetProfitThisYear(VehicleID vehicle_id) { throw null; }

        /**
         * Get the profit of last year of a vehicle.
         * @param vehicle_id The vehicle to get the profit of.
         * @returns The profit the vehicle had last year.
         */
        public static long GetProfitLastYear(VehicleID vehicle_id) { throw null; }

        /**
         * Get the current value of a vehicle.
         * @param vehicle_id The vehicle to get the value of.
         * @returns The value the vehicle currently has (the amount you should get
         *  when you would sell the vehicle right now).
         */
        public static long GetCurrentValue(VehicleID vehicle_id) { throw null; }

        /**
         * Get the type of vehicle.
         * @param vehicle_id The vehicle to get the type of.
         * @returns The vehicle type.
         */
        public static VehicleType GetVehicleType(VehicleID vehicle_id) { throw null; }

        /**
         * Get the RoadType of the vehicle.
         * @param vehicle_id The vehicle to get the RoadType of.
         * @returns The RoadType the vehicle has.
         */
        public static RoadType GetRoadType(VehicleID vehicle_id) { throw null; }

        /**
         * Check if a vehicle is in a depot.
         * @param vehicle_id The vehicle to check.
         * @returns True if and only if the vehicle is in a depot.
         */
        public static bool IsInDepot(VehicleID vehicle_id) { throw null; }

        /**
         * Check if a vehicle is in a depot and stopped.
         * @param vehicle_id The vehicle to check.
         * @returns True if and only if the vehicle is in a depot and stopped.
         */
        public static bool IsStoppedInDepot(VehicleID vehicle_id) { throw null; }

        /**
         * Builds a vehicle with the given engine at the given depot.
         * @param depot The depot where the vehicle will be build.
         * @param engine_id The engine to use for this vehicle.
         * @exception AIVehicle.ERR_VEHICLE_TOO_MANY
         * @exception AIVehicle.ERR_VEHICLE_BUILD_DISABLED
         * @exception AIVehicle.ERR_VEHICLE_WRONG_DEPOT
         * @returns The VehicleID of the new vehicle, or an invalid VehicleID when
         *   it failed. Check the return value using IsValidVehicle. In test-mode
         *   0 is returned if it was successful; any other value indicates failure.
         * @note In Test Mode it means you can't assign orders yet to this vehicle,
         *   as the vehicle isn't really built yet. Build it for real first before
         *   assigning orders.
         */
        public static VehicleID BuildVehicle(TileIndex depot, EngineID engine_id) { throw null; }

        /**
         * Clones a vehicle at the given depot, copying or cloning its orders.
         * @param depot The depot where the vehicle will be build.
         * @param vehicle_id The vehicle to use as example for the new vehicle.
         * @param share_orders Should the orders be copied or shared?
         * @exception AIVehicle.ERR_VEHICLE_TOO_MANY
         * @exception AIVehicle.ERR_VEHICLE_BUILD_DISABLED
         * @exception AIVehicle.ERR_VEHICLE_WRONG_DEPOT
         * @returns The VehicleID of the new vehicle, or an invalid VehicleID when
         *   it failed. Check the return value using IsValidVehicle. In test-mode
         *   0 is returned if it was successful; any other value indicates failure.
         */
        public static VehicleID CloneVehicle(TileIndex depot, VehicleID vehicle_id, bool share_orders) { throw null; }

        /**
         * Move a wagon after another wagon.
         * @param source_vehicle_id The vehicle to move a wagon away from.
         * @param source_wagon The wagon in source_vehicle to move.
         * @param dest_vehicle_id The vehicle to move the wagon to, or -1 to create a new vehicle.
         * @param dest_wagon The wagon in dest_vehicle to place source_wagon after.
         * @returns Whether or not moving the wagon succeeded.
         */
        public static bool MoveWagon(VehicleID source_vehicle_id, int source_wagon, VehicleID dest_vehicle_id, int dest_wagon) { throw null; }

        /**
         * Move a chain of wagons after another wagon.
         * @param source_vehicle_id The vehicle to move a wagon away from.
         * @param source_wagon The first wagon in source_vehicle to move.
         * @param dest_vehicle_id The vehicle to move the wagons to, or -1 to create a new vehicle.
         * @param dest_wagon The wagon in dest_vehicle to place source_wagon and following wagons after.
         * @returns Whether or not moving the wagons succeeded.
         */
        public static bool MoveWagonChain(VehicleID source_vehicle_id, int source_wagon, VehicleID dest_vehicle_id, int dest_wagon) { throw null; }

        /**
         * Gets the capacity of the given vehicle when refitted to the given cargo type.
         * @param vehicle_id The vehicle to refit.
         * @param cargo The cargo to refit to.
         * @returns The capacity the vehicle will have when refited.
         */
        public static int GetRefitCapacity(VehicleID vehicle_id, CargoID cargo) { throw null; }

        /**
         * Refits a vehicle to the given cargo type.
         * @param vehicle_id The vehicle to refit.
         * @param cargo The cargo to refit to.
         * @exception AIVehicle.ERR_VEHICLE_CANNOT_REFIT
         * @exception AIVehicle.ERR_VEHICLE_IS_DESTROYED
         * @exception AIVehicle.ERR_VEHICLE_NOT_IN_DEPOT
         * @returns True if and only if the refit succeeded.
         */
        public static bool RefitVehicle(VehicleID vehicle_id, CargoID cargo) { throw null; }

        /**
         * Sells the given vehicle.
         * @param vehicle_id The vehicle to sell.
         * @exception AIVehicle.ERR_VEHICLE_IS_DESTROYED
         * @exception AIVehicle.ERR_VEHICLE_NOT_IN_DEPOT
         * @returns True if and only if the vehicle has been sold.
         */
        public static bool SellVehicle(VehicleID vehicle_id) { throw null; }

        /**
         * Sells the given wagon from the vehicle.
         * @param vehicle_id The vehicle to sell a wagon from.
         * @param wagon The wagon to sell.
         * @exception AIVehicle.ERR_VEHICLE_IS_DESTROYED
         * @exception AIVehicle.ERR_VEHICLE_NOT_IN_DEPOT
         * @returns True if and only if the wagon has been sold.
         */
        public static bool SellWagon(VehicleID vehicle_id, int wagon) { throw null; }

        /**
         * Sells all wagons from the vehicle starting from a given position.
         * @param vehicle_id The vehicle to sell a wagon from.
         * @param wagon The wagon to sell.
         * @exception AIVehicle.ERR_VEHICLE_IS_DESTROYED
         * @exception AIVehicle.ERR_VEHICLE_NOT_IN_DEPOT
         * @returns True if and only if the wagons have been sold.
         */
        public static bool SellWagonChain(VehicleID vehicle_id, int wagon) { throw null; }

        /**
         * Sends the given vehicle to a depot. If the vehicle has already been
         * sent to a depot it continues with its normal orders instead.
         * @param vehicle_id The vehicle to send to a depot.
         * @exception AIVehicle.ERR_VEHICLE_CANNOT_SEND_TO_DEPOT
         * @returns True if the current order was changed.
         */
        public static bool SendVehicleToDepot(VehicleID vehicle_id) { throw null; }

        /**
         * Sends the given vehicle to a depot for servicing. If the vehicle has
         * already been sent to a depot it continues with its normal orders instead.
         * @param vehicle_id The vehicle to send to a depot for servicing.
         * @exception AIVehicle.ERR_VEHICLE_CANNOT_SEND_TO_DEPOT
         * @returns True if the current order was changed.
         */
        public static bool SendVehicleToDepotForServicing(VehicleID vehicle_id) { throw null; }

        /**
         * Starts or stops the given vehicle depending on the current state.
         * @param vehicle_id The vehicle to start/stop.
         * @exception AIVehicle.ERR_VEHICLE_CANNOT_START_STOP
         * @exception (For aircraft only): AIVehicle.ERR_VEHICLE_IN_FLIGHT
         * @exception (For trains only): AIVehicle.ERR_VEHICLE_NO_POWER
         * @returns True if and only if the vehicle has been started or stopped.
         */
        public static bool StartStopVehicle(VehicleID vehicle_id) { throw null; }

        /**
         * Turn the given vehicle so it'll drive the other way.
         * @param vehicle_id The vehicle to turn.
         * @returns True if and only if the vehicle has started to turn.
         * @note Vehicles cannot always be reversed. For example busses and trucks need to be running
         *  and not be inside a depot.
         */
        public static bool ReverseVehicle(VehicleID vehicle_id) { throw null; }

        /**
         * Get the maximum amount of a specific cargo the given vehicle can transport.
         * @param vehicle_id The vehicle to get the capacity of.
         * @param cargo The cargo to get the capacity for.
         * @returns The maximum amount of the given cargo the vehicle can transport.
         */
        public static int GetCapacity(VehicleID vehicle_id, CargoID cargo) { throw null; }

        /**
         * Get the length of a the total vehicle in 1/16's of a tile.
         * @param vehicle_id The vehicle to get the length of.
         * @returns The length of the engine.
         */
        public static int GetLength(VehicleID vehicle_id) { throw null; }

        /**
         * Get the amount of a specific cargo the given vehicle is transporting.
         * @param vehicle_id The vehicle to get the load amount of.
         * @param cargo The cargo to get the loaded amount for.
         * @returns The amount of the given cargo the vehicle is currently transporting.
         */
        public static int GetCargoLoad(VehicleID vehicle_id, CargoID cargo) { throw null; }

        /**
         * Get the group of a given vehicle.
         * @param vehicle_id The vehicle to get the group from.
         * @returns The group of the given vehicle.
         */
        public static GroupID GetGroupID(VehicleID vehicle_id) { throw null; }

        /**
         * Check if the vehicle is articulated.
         * @param vehicle_id The vehicle to check.
         * @returns True if the vehicle is articulated.
         */
        public static bool IsArticulated(VehicleID vehicle_id) { throw null; }

        /**
         * Check if the vehicle has shared orders.
         * @param vehicle_id The vehicle to check.
         * @returns True if the vehicle has shared orders.
         */
        public static bool HasSharedOrders(VehicleID vehicle_id) { throw null; }

        /**
         * Get the current reliability of a vehicle.
         * @param vehicle_id The vehicle to check.
         * @returns The current reliability (0-100%).
         */
        public static int GetReliability(VehicleID vehicle_id) { throw null; }

        /**
         * Get the maximum allowed distance between two orders for a vehicle.
         * The distance returned is a vehicle-type specific distance independent from other
         * map distances, you may use the result of this function to compare it
         * with the result of AIOrder.GetOrderDistance.
         * @param vehicle_id The vehicle to get the distance for.
         * @returns The maximum distance between two orders for this vehicle
         *         or 0 if the distance is unlimited.
         * @note   The unit of the order distances is unspecified and should
         *         not be compared with map distances
         * @see AIOrder.GetOrderDistance
         */
        public static int GetMaximumOrderDistance(VehicleID vehicle_id) { throw null; }
    }
}