namespace OpenTTD
{
    /// <summary>Class that handles all bridge related functions.</summary>
    public static class AIBridge
    {
        /// <summary>Base for bridge related errors </summary>
        public static readonly ErrorMessage ERR_BRIDGE_BASE;

        /**
         * The bridge you want to build is not available yet,
         * or it is not available for the requested length.
         */
        public static readonly ErrorMessage ERR_BRIDGE_TYPE_UNAVAILABLE;

        /// <summary>One (or more) of the bridge head(s) ends in water. </summary>
        public static readonly ErrorMessage ERR_BRIDGE_CANNOT_END_IN_WATER;

        /// <summary>The bride heads need to be on the same height </summary>
        public static readonly ErrorMessage ERR_BRIDGE_HEADS_NOT_ON_SAME_HEIGHT;

        /**
         * Checks whether the given bridge type is valid.
         * @param bridge_id The bridge to check.
         * @returns True if and only if the bridge type is valid.
         */
        public static bool IsValidBridge(BridgeID bridge_id) { throw null; }

        /**
         * Checks whether the given tile is actually a bridge start or end tile.
         * @param tile The tile to check.
         * @returns True if and only if the tile is the beginning or end of a bridge.
         */
        public static bool IsBridgeTile(TileIndex tile) { throw null; }

        /**
         * Get the BridgeID of a bridge at a given tile.
         * @param tile The tile to get the BridgeID from.
         * @returns The BridgeID from the bridge at tile 'tile'.
         */
        public static BridgeID GetBridgeID(TileIndex tile) { throw null; }

        /**
         * Get the name of a bridge.
         * @param bridge_id The bridge to get the name of.
         * @returns The name the bridge has.
         */
        public static string GetName(BridgeID bridge_id) { throw null; }

        /**
         * Get the maximum speed of a bridge.
         * @param bridge_id The bridge to get the maximum speed of.
         * @returns The maximum speed the bridge has.
         * @note The speed is in OpenTTD's internal speed unit.
         *       This is mph / 1.6, which is roughly km/h.
         *       To get km/h multiply this number by 1.00584.
         */
        public static int GetMaxSpeed(BridgeID bridge_id) { throw null; }

        /**
         * Get the new cost of a bridge, excluding the road and/or rail.
         * @param bridge_id The bridge to get the new cost of.
         * @param length The length of the bridge.
         * @returns The new cost the bridge has.
         */
        public static long GetPrice(BridgeID bridge_id, int length) { throw null; }

        /**
         * Get the maximum length of a bridge.
         * @param bridge_id The bridge to get the maximum length of.
         * @returns The maximum length the bridge has.
         */
        public static int GetMaxLength(BridgeID bridge_id) { throw null; }

        /**
         * Get the minimum length of a bridge.
         * @param bridge_id The bridge to get the minimum length of.
         * @returns The minimum length the bridge has.
         */
        public static int GetMinLength(BridgeID bridge_id) { throw null; }

        /**
         * Build a bridge from one tile to the other.
         * As an extra for road, this functions builds two half-pieces of road on
         *  each end of the bridge, making it easier for you to connect it to your
         *  network.
         * @param vehicle_type The vehicle-type of bridge to build.
         * @param bridge_id The bridge-type to build.
         * @param start Where to start the bridge.
         * @param end Where to end the bridge.
         * @exception AIError.ERR_ALREADY_BUILT
         * @exception AIError.ERR_AREA_NOT_CLEAR
         * @exception AIError.ERR_LAND_SLOPED_WRONG
         * @exception AIError.ERR_VEHICLE_IN_THE_WAY
         * @exception AIBridge.ERR_BRIDGE_TYPE_UNAVAILABLE
         * @exception AIBridge.ERR_BRIDGE_CANNOT_END_IN_WATER
         * @exception AIBridge.ERR_BRIDGE_HEADS_NOT_ON_SAME_HEIGHT
         * @returns Whether the bridge has been/can be build or not.
         * @note No matter if the road pieces were build or not, if building the
         *  bridge succeeded, this function returns true.
         */
        public static bool BuildBridge(VehicleType vehicle_type, BridgeID bridge_id, TileIndex start, TileIndex end) { throw null; }

        /**
         * Removes a bridge, by executing it on either the start or end tile.
         * @param tile An end or start tile of the bridge.
         * @exception AIError.ERR_OWNED_BY_ANOTHER_COMPANY
         * @returns Whether the bridge has been/can be removed or not.
         */
        public static bool RemoveBridge(TileIndex tile) { throw null; }

        /**
         * Get the tile that is on the other end of a bridge starting at tile.
         * @param tile The tile that is an end of a bridge.
         * @returns The TileIndex that is the other end of the bridge.
         */
        public static TileIndex GetOtherBridgeEnd(TileIndex tile) { throw null; }
    }
}