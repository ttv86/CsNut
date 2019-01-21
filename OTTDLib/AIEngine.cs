namespace OpenTTD
{
    /// <summary>Class that handles all engine related functions.</summary>
    public static class AIEngine
    {
        /**
         * Checks whether the given engine type is valid.
         * An engine is valid for a company if it has at least one vehicle of this engine or it's currently buildable.
         * @param engine_id The engine to check.
         * @returns True if and only if the engine type is valid.
         */
        public static bool IsValidEngine(EngineID engine_id) { throw null; }

        /**
         * Checks whether the given engine type is buildable for a company.
         * @param engine_id The engine to check.
         * @returns True if and only if the engine type is buildable.
         */
        public static bool IsBuildable(EngineID engine_id) { throw null; }

        /**
         * Get the name of an engine.
         * @param engine_id The engine to get the name of.
         * @returns The name the engine has.
         */
        public static string GetName(EngineID engine_id) { throw null; }

        /**
         * Get the cargo-type of an engine. In case it can transport multiple cargoes, it
         *  returns the first/main.
         * @param engine_id The engine to get the cargo-type of.
         * @returns The cargo-type of the engine.
         */
        public static CargoID GetCargoType(EngineID engine_id) { throw null; }

        /**
         * Check if the cargo of an engine can be refitted to your requested. If
         *  the engine already allows this cargo, the function also returns true.
         *  In case of articulated vehicles the function decides whether at least one
         *  part can carry the cargo.
         * @param engine_id The engine to check for refitting.
         * @param cargo_id The cargo to check for refitting.
         * @returns True if the engine can carry this cargo, either via refit, or
         *  by default.
         */
        public static bool CanRefitCargo(EngineID engine_id, CargoID cargo_id) { throw null; }

        /**
         * Check if the engine can pull a wagon with the given cargo.
         * @param engine_id The engine to check.
         * @param cargo_id The cargo to check.
         * @returns True if the engine can pull wagons carrying this cargo.
         * @note This function is not exhaustive; a true here does not mean
         *  that the vehicle can pull the wagons, a false does mean it can't.
         */
        public static bool CanPullCargo(EngineID engine_id, CargoID cargo_id) { throw null; }

        /**
         * Get the capacity of an engine. In case it can transport multiple cargoes, it
         *  returns the first/main.
         * @param engine_id The engine to get the capacity of.
         * @returns The capacity of the engine.
         */
        public static int GetCapacity(EngineID engine_id) { throw null; }

        /**
         * Get the reliability of an engine. The value is between 0 and 100, where
         *  100 means 100% reliability (never breaks down) and 0 means 0%
         *  reliability (you most likely don't want to buy it).
         * @param engine_id The engine to get the reliability of.
         * @returns The reliability the engine has.
         */
        public static int GetReliability(EngineID engine_id) { throw null; }

        /**
         * Get the maximum speed of an engine.
         * @param engine_id The engine to get the maximum speed of.
         * @returns The maximum speed the engine has.
         * @note The speed is in OpenTTD's internal speed unit.
         *       This is mph / 1.6, which is roughly km/h.
         *       To get km/h multiply this number by 1.00584.
         */
        public static int GetMaxSpeed(EngineID engine_id) { throw null; }

        /**
         * Get the new cost of an engine.
         * @param engine_id The engine to get the new cost of.
         * @returns The new cost the engine has.
         */
        public static long GetPrice(EngineID engine_id) { throw null; }

        /**
         * Get the maximum age of a brand new engine.
         * @param engine_id The engine to get the maximum age of.
         * @returns The maximum age of a new engine in days.
         * @note Age is in days; divide by 366 to get per year.
         */
        public static int GetMaxAge(EngineID engine_id) { throw null; }

        /**
         * Get the running cost of an engine.
         * @param engine_id The engine to get the running cost of.
         * @returns The running cost of a vehicle per year.
         * @note Cost is per year; divide by 365 to get per day.
         */
        public static long GetRunningCost(EngineID engine_id) { throw null; }

        /**
         * Get the power of an engine.
         * @param engine_id The engine to get the power of.
         * @returns The power of the engine in hp.
         */
        public static int GetPower(EngineID engine_id) { throw null; }

        /**
         * Get the weight of an engine.
         * @param engine_id The engine to get the weight of.
         * @returns The weight of the engine in metric tons.
         */
        public static int GetWeight(EngineID engine_id) { throw null; }

        /**
         * Get the maximum tractive effort of an engine.
         * @param engine_id The engine to get the maximum tractive effort of.
         * @returns The maximum tractive effort of the engine in kN.
         */
        public static int GetMaxTractiveEffort(EngineID engine_id) { throw null; }

        /**
         * Get the date this engine was designed.
         * @param engine_id The engine to get the design date of.
         * @returns The date this engine was designed.
         */
        public static Date GetDesignDate(EngineID engine_id) { throw null; }

        /**
         * Get the type of an engine.
         * @param engine_id The engine to get the type of.
         * @returns The type the engine has.
         */
        public static VehicleType GetVehicleType(EngineID engine_id) { throw null; }

        /**
         * Check if an engine is a wagon.
         * @param engine_id The engine to check.
         * @returns Whether or not the engine is a wagon.
         */
        public static bool IsWagon(EngineID engine_id) { throw null; }

        /**
         * Check if a train vehicle can run on a RailType.
         * @param engine_id The engine to check.
         * @param track_rail_type The type you want to check.
         * @returns Whether an engine of type 'engine_id' can run on 'track_rail_type'.
         * @note Even if a train can run on a RailType that doesn't mean that it'll be
         *   able to power the train. Use HasPowerOnRail for that.
         */
        public static bool CanRunOnRail(EngineID engine_id, RailType track_rail_type) { throw null; }

        /**
         * Check if a train engine has power on a RailType.
         * @param engine_id The engine to check.
         * @param track_rail_type Another RailType.
         * @returns Whether an engine of type 'engine_id' has power on 'track_rail_type'.
         */
        public static bool HasPowerOnRail(EngineID engine_id, RailType track_rail_type) { throw null; }

        /**
         * Get the RoadType of the engine.
         * @param engine_id The engine to get the RoadType of.
         * @returns The RoadType the engine has.
         */
        public static RoadType GetRoadType(EngineID engine_id) { throw null; }

        /**
         * Get the RailType of the engine.
         * @param engine_id The engine to get the RailType of.
         * @returns The RailType the engine has.
         */
        public static RailType GetRailType(EngineID engine_id) { throw null; }

        /**
         * Check if the engine is articulated.
         * @param engine_id The engine to check.
         * @returns True if the engine is articulated.
         */
        public static bool IsArticulated(EngineID engine_id) { throw null; }

        /**
         * Get the PlaneType of the engine.
         * @param engine_id The engine to get the PlaneType of.
         * @returns The PlaneType the engine has.
         */
        public static PlaneType GetPlaneType(EngineID engine_id) { throw null; }

        /**
         * Get the maximum allowed distance between two orders for an engine.
         * The distance returned is a vehicle-type specific distance independent from other
         * map distances, you may use the result of this function to compare it
         * with the result of AIOrder.GetOrderDistance.
         * @param engine_id The engine to get the max distance for.
         * @returns The maximum distance between two orders for the engine
         *         or 0 if the distance is unlimited.
         * @note   The unit of the order distances is unspecified and should
         *         not be compared with map distances
         * @see AIOrder.GetOrderDistance
         */
        public static int GetMaximumOrderDistance(EngineID engine_id) { throw null; }
    }
}