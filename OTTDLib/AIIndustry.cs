namespace OpenTTD
{
    /// <summary>Class that handles all industry related functions.</summary>
    public static class AIIndustry
    {
        /// <summary>Ways for an industry to accept a cargo. </summary>
        public enum CargoAcceptState
        {
            CAS_NOT_ACCEPTED,
            CAS_ACCEPTED,
            CAS_TEMP_REFUSED,
        }

        /**
         * Gets the number of industries.
         * @returns The number of industries.
         * @note The maximum valid IndustryID can be higher than the value returned.
         */
        public static int GetIndustryCount() { throw null; }

        /**
         * Checks whether the given industry index is valid.
         * @param industry_id The index to check.
         * @returns True if and only if the industry is valid.
         */
        public static bool IsValidIndustry(IndustryID industry_id) { throw null; }

        /**
         * Get the IndustryID of a tile, if there is an industry.
         * @param tile The tile to find the IndustryID of.
         * @returns IndustryID of the industry.
         * @note GetIndustryID will return an invalid IndustryID for the
         *   station tile of industries with a dock/heliport.
         */
        public static IndustryID GetIndustryID(TileIndex tile) { throw null; }

        /**
         * Get the name of the industry.
         * @param industry_id The industry to get the name of.
         * @returns The name of the industry.
         */
        public static string GetName(IndustryID industry_id) { throw null; }

        /**
         * See whether an industry currently accepts a certain cargo.
         * @param industry_id The index of the industry.
         * @param cargo_id The index of the cargo.
         * @returns Whether the industry accepts, temporarily refuses or never accepts this cargo.
         */
        public static CargoAcceptState IsCargoAccepted(IndustryID industry_id, CargoID cargo_id) { throw null; }

        /**
         * Get the amount of cargo stockpiled for processing.
         * @param industry_id The index of the industry.
         * @param cargo_id The index of the cargo.
         * @returns The amount of cargo that is waiting for processing.
         */
        public static int GetStockpiledCargo(IndustryID industry_id, CargoID cargo_id) { throw null; }

        /**
         * Get the total last month's production of the given cargo at an industry.
         * @param industry_id The index of the industry.
         * @param cargo_id The index of the cargo.
         * @returns The last month's production of the given cargo for this industry.
         */
        public static int GetLastMonthProduction(IndustryID industry_id, CargoID cargo_id) { throw null; }

        /**
         * Get the total amount of cargo transported from an industry last month.
         * @param industry_id The index of the industry.
         * @param cargo_id The index of the cargo.
         * @returns The amount of given cargo transported from this industry last month.
         */
        public static int GetLastMonthTransported(IndustryID industry_id, CargoID cargo_id) { throw null; }

        /**
         * Get the percentage of cargo transported from an industry last month.
         * @param industry_id The index of the industry.
         * @param cargo_id The index of the cargo.
         * @returns The percentage of given cargo transported from this industry last month.
         */
        public static int GetLastMonthTransportedPercentage(IndustryID industry_id, CargoID cargo_id) { throw null; }

        /**
         * Gets the location of the industry.
         * @param industry_id The index of the industry.
         * @returns The location of the industry.
         */
        public static TileIndex GetLocation(IndustryID industry_id) { throw null; }

        /**
         * Get the number of stations around an industry. All stations that can
         * service the industry are counted, your own stations but also your
         * opponents stations.
         * @param industry_id The index of the industry.
         * @returns The number of stations around an industry.
         */
        public static int GetAmountOfStationsAround(IndustryID industry_id) { throw null; }

        /**
         * Get the manhattan distance from the tile to the AIIndustry.GetLocation()
         *  of the industry.
         * @param industry_id The industry to get the distance to.
         * @param tile The tile to get the distance to.
         * @returns The distance between industry and tile.
         */
        public static int GetDistanceManhattanToTile(IndustryID industry_id, TileIndex tile) { throw null; }

        /**
         * Get the square distance from the tile to the AIIndustry.GetLocation()
         *  of the industry.
         * @param industry_id The industry to get the distance to.
         * @param tile The tile to get the distance to.
         * @returns The distance between industry and tile.
         */
        public static int GetDistanceSquareToTile(IndustryID industry_id, TileIndex tile) { throw null; }

        /**
         * Is this industry built on water.
         * @param industry_id The index of the industry.
         * @returns True when the industry is built on water.
         */
        public static bool IsBuiltOnWater(IndustryID industry_id) { throw null; }

        /**
         * Does this industry have a heliport?
         * @param industry_id The index of the industry.
         * @returns True when the industry has a heliport.
         */
        public static bool HasHeliport(IndustryID industry_id) { throw null; }

        /**
         * Gets the location of the industry's heliport.
         * @param industry_id The index of the industry.
         * @returns The location of the industry's heliport.
         */
        public static TileIndex GetHeliportLocation(IndustryID industry_id) { throw null; }

        /**
         * Does this industry have a dock?
         * @param industry_id The index of the industry.
         * @returns True when the industry has a dock.
         */
        public static bool HasDock(IndustryID industry_id) { throw null; }

        /**
         * Gets the location of the industry's dock.
         * @param industry_id The index of the industry.
         * @returns The location of the industry's dock.
         */
        public static TileIndex GetDockLocation(IndustryID industry_id) { throw null; }

        /**
         * Get the IndustryType of the industry.
         * @param industry_id The index of the industry.
         * @returns The IndustryType of the industry.
         */
        public static IndustryType GetIndustryType(IndustryID industry_id) { throw null; }
    }
}