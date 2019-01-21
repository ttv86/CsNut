namespace OpenTTD
{
    /// <summary>Class that handles all tunnel related functions.</summary>
    public static class AITunnel
    {
        /// <summary>All tunnel related errors.</summary>
        /// <summary>Base for bridge related errors </summary>
        public static readonly ErrorMessage ERR_TUNNEL_BASE;

        /// <summary>Can't build tunnels on water </summary>
        public static readonly ErrorMessage ERR_TUNNEL_CANNOT_BUILD_ON_WATER;

        /// <summary>The start tile must slope either North, South, West or East </summary>
        public static readonly ErrorMessage ERR_TUNNEL_START_SITE_UNSUITABLE;

        /// <summary>Another tunnel is in the way </summary>
        public static readonly ErrorMessage ERR_TUNNEL_ANOTHER_TUNNEL_IN_THE_WAY;

        /// <summary>Unable to excavate land at the end to create the tunnel's exit </summary>
        public static readonly ErrorMessage ERR_TUNNEL_END_SITE_UNSUITABLE;

        /**
         * Check whether the tile is an entrance to a tunnel.
         * @param tile The tile to check.
         * @returns True if and only if the tile is the beginning or end of a tunnel.
         */
        public static bool IsTunnelTile(TileIndex tile) { throw null; }

        /**
         * Get the tile that exits on the other end of a (would be) tunnel starting
         *  at tile. If there is no 'simple' inclined slope at the start tile,
         *  this function will return AIMap.TILE_INVALID.
         * @param tile The tile that is an entrance to a tunnel or the tile where you may want to build a tunnel.
         * @returns The TileIndex that is the other end of the (would be) tunnel, or
         *  AIMap.TILE_INVALID if no other end was found (can't build tunnel).
         * @note Even if this function returns a valid tile, that is no guarantee
         *  that building a tunnel will succeed. Use BuildTunnel in ScriptTestMode to
         *  check whether a tunnel can actually be build.
         */
        public static TileIndex GetOtherTunnelEnd(TileIndex tile) { throw null; }

        /**
         * Builds a tunnel starting at start. The direction of the tunnel depends
         *  on the slope of the start tile. Tunnels can be created for either
         *  rails or roads; use the appropriate AIVehicle.VehicleType.
         * As an extra for road, this functions builds two half-pieces of road on
         *  each end of the tunnel, making it easier for you to connect it to your
         *  network.
         * @param start Where to start the tunnel.
         * @param vehicle_type The vehicle-type of tunnel to build.
         * @exception AIError.ERR_AREA_NOT_CLEAR
         * @exception AITunnel.ERR_TUNNEL_CANNOT_BUILD_ON_WATER
         * @exception AITunnel.ERR_TUNNEL_START_SITE_UNSUITABLE
         * @exception AITunnel.ERR_TUNNEL_ANOTHER_TUNNEL_IN_THE_WAY
         * @exception AITunnel.ERR_TUNNEL_END_SITE_UNSUITABLE
         * @returns Whether the tunnel has been/can be build or not.
         * @note The slope of a tile can be determined by AITile.GetSlope(TileIndex).
         * @note No matter if the road pieces were build or not, if building the
         *  tunnel succeeded, this function returns true.
         */
        public static bool BuildTunnel(VehicleType vehicle_type, TileIndex start) { throw null; }

        /**
         * Remove the tunnel whose entrance is located at tile.
         * @param tile The tile that is an entrance to a tunnel.
         * @exception AIError.ERR_OWNED_BY_ANOTHER_COMPANY
         * @returns Whether the tunnel has been/can be removed or not.
         */
        public static bool RemoveTunnel(TileIndex tile) { throw null; }
    }
}