namespace OpenTTD
{
    /// <summary>Class that handles all town related functions.</summary>
    public static class AITown
    {
        /// <summary>Actions that one can perform on a town.</summary>

        /**
        * The cargo ratings temporary gains 25% of rating (in
        * absolute percentage, so 10% becomes 35%, with a max of 99%)
        * for all stations within 10 tiles.
        */
        public static readonly TownAction TOWN_ACTION_ADVERTISE_SMALL;

        /**
        * The cargo ratings temporary gains 44% of rating (in
        * absolute percentage; so 10% becomes 54%; with a max of 99%)
        * for all stations within 15 tiles.
        */
        public static readonly TownAction TOWN_ACTION_ADVERTISE_MEDIUM;

        /**
        * The cargo ratings temporary gains 63% of rating (in
        * absolute percentage; so 10% becomes 73%; with a max of 99%)
        * for all stations within 20 tiles.
        */
        public static readonly TownAction TOWN_ACTION_ADVERTISE_LARGE;

        /// <summary>Rebuild the roads of this town for 6 months.</summary>
        public static readonly TownAction TOWN_ACTION_ROAD_REBUILD;

        /// <summary>Build a statue in this town.</summary>
        public static readonly TownAction TOWN_ACTION_BUILD_STATUE;

        /// <summary>Fund the creation of extra buildings for 3 months.</summary>
        public static readonly TownAction TOWN_ACTION_FUND_BUILDINGS;

        /// <summary>Buy exclusive rights for this town for 12 months.</summary>
        public static readonly TownAction TOWN_ACTION_BUY_RIGHTS;

        /// <summary>Bribe the town in order to get a higher rating.</summary>
        public static readonly TownAction TOWN_ACTION_BRIBE;

        /// <summary>Different ratings one could have in a town.</summary>
        public static readonly TownRating TOWN_RATING_NONE;
        public static readonly TownRating TOWN_RATING_APPALLING;
        public static readonly TownRating TOWN_RATING_VERY_POOR;
        public static readonly TownRating TOWN_RATING_POOR;
        public static readonly TownRating TOWN_RATING_MEDIOCRE;
        public static readonly TownRating TOWN_RATING_GOOD;
        public static readonly TownRating TOWN_RATING_VERY_GOOD;
        public static readonly TownRating TOWN_RATING_EXCELLENT;
        public static readonly TownRating TOWN_RATING_OUTSTANDING;
        public static readonly TownRating TOWN_RATING_INVALID;

        /// <summary>Possible layouts for the roads in a town.</summary>
        public static readonly RoadLayout ROAD_LAYOUT_ORIGINAL;
        public static readonly RoadLayout ROAD_LAYOUT_BETTER_ROADS;
        public static readonly RoadLayout ROAD_LAYOUT_2x2;
        public static readonly RoadLayout ROAD_LAYOUT_3x3;
        public static readonly RoadLayout ROAD_LAYOUT_INVALID;

        /// <summary>Possible town construction sizes.</summary>
        public static readonly TownSize TOWN_SIZE_SMALL;
        public static readonly TownSize TOWN_SIZE_MEDIUM;
        public static readonly TownSize TOWN_SIZE_LARGE;
        public static readonly TownSize TOWN_SIZE_INVALID;

        /// <summary>Special values for SetGrowthRate.</summary>
        public static readonly TownGrowth TOWN_GROWTH_NONE;
        public static readonly TownGrowth TOWN_GROWTH_NORMAL;

        /// <summary>Gets the number of towns.</summary><returns>The number of towns.</returns>
        public static int GetTownCount() { throw null; }

        /**
         * Checks whether the given town index is valid.
         * @param town_id The index to check.
         * @returns True if and only if the town is valid.
         */
        public static bool IsValidTown(TownID town_id) { throw null; }

        /**
         * Get the name of the town.
         * @param town_id The town to get the name of.
         * @returns The name of the town.
         */
        public static string GetName(TownID town_id) { throw null; }

        /**
         * Gets the number of inhabitants in the town.
         * @param town_id The town to get the population of.
         * @returns The number of inhabitants.
         */
        public static int GetPopulation(TownID town_id) { throw null; }

        /**
         * Gets the number of houses in the town.
         * @param town_id The town to get the number of houses of.
         * @returns The number of houses.
         */
        public static int GetHouseCount(TownID town_id) { throw null; }

        /**
         * Gets the location of the town.
         * @param town_id The town to get the location of.
         * @returns The location of the town.
         */
        public static TileIndex GetLocation(TownID town_id) { throw null; }

        /**
         * Get the total last month's production of the given cargo at a town.
         * @param town_id The index of the town.
         * @param cargo_id The index of the cargo.
         * @returns The last month's production of the given cargo for this town.
         */
        public static int GetLastMonthProduction(TownID town_id, CargoID cargo_id) { throw null; }

        /**
         * Get the total amount of cargo supplied from a town last month.
         * @param town_id The index of the town.
         * @param cargo_id The index of the cargo.
         * @returns The amount of cargo supplied for transport from this town last month.
         */
        public static int GetLastMonthSupplied(TownID town_id, CargoID cargo_id) { throw null; }

        /**
         * Get the percentage of transported production of the given cargo at a town.
         * @param town_id The index of the town.
         * @param cargo_id The index of the cargo.
         * @returns The percentage of given cargo transported from this town last month.
         */
        public static int GetLastMonthTransportedPercentage(TownID town_id, CargoID cargo_id) { throw null; }

        /**
         * Get the total amount of cargo effects received by a town last month.
         * @param town_id The index of the town.
         * @param towneffect_id The index of the cargo.
         * @returns The amount of cargo received by this town last month for this cargo effect.
         */
        public static int GetLastMonthReceived(TownID town_id, TownEffect towneffect_id) { throw null; }

        /**
         * Get the amount of cargo that needs to be delivered (per TownEffect) for a
         *  town to grow. All goals need to be reached before a town will grow.
         * @param town_id The index of the town.
         * @param towneffect_id The index of the towneffect.
         * @returns The goal of the cargo.
         * @note Goals can change over time. For example with a changing snowline, or
         *  with a growing town.
         */
        public static int GetCargoGoal(TownID town_id, TownEffect towneffect_id) { throw null; }

        /**
         * Get the amount of days between town growth.
         * @param town_id The index of the town.
         * @returns Amount of days between town growth, or TOWN_GROWTH_NONE.
         * @note This function does not indicate when it will grow next. It only tells you the time between growths.
         */
        public static int GetGrowthRate(TownID town_id) { throw null; }

        /**
         * Get the manhattan distance from the tile to the AITown.GetLocation()
         *  of the town.
         * @param town_id The town to get the distance to.
         * @param tile The tile to get the distance to.
         * @returns The distance between town and tile.
         */
        public static int GetDistanceManhattanToTile(TownID town_id, TileIndex tile) { throw null; }

        /**
         * Get the square distance from the tile to the AITown.GetLocation()
         *  of the town.
         * @param town_id The town to get the distance to.
         * @param tile The tile to get the distance to.
         * @returns The distance between town and tile.
         */
        public static int GetDistanceSquareToTile(TownID town_id, TileIndex tile) { throw null; }

        /**
         * Find out if this tile is within the rating influence of a town.
         *  If a station sign would be on this tile, the servicing quality of the station would
         *  influence the rating of the town.
         * @param town_id The town to check.
         * @param tile The tile to check.
         * @returns True if the tile is within the rating influence of the town.
         */
        public static bool IsWithinTownInfluence(TownID town_id, TileIndex tile) { throw null; }

        /**
         * Find out if this town has a statue for the current company.
         * @param town_id The town to check.
         * @returns True if the town has a statue.
         */
        public static bool HasStatue(TownID town_id) { throw null; }

        /**
         * Find out if the town is a city.
         * @param town_id The town to check.
         * @returns True if the town is a city.
         */
        public static bool IsCity(TownID town_id) { throw null; }

        /**
         * Find out how long the town is undergoing road reconstructions.
         * @param town_id The town to check.
         * @returns The number of months the road reworks are still going to take.
         *         The value 0 means that there are currently no road reworks.
         */
        public static int GetRoadReworkDuration(TownID town_id) { throw null; }

        /**
         * Find out how long new buildings are still being funded in a town.
         * @param town_id The town to check.
         * @returns The number of months building construction is still funded.
         *         The value 0 means that there is currently no funding.
         */
        public static int GetFundBuildingsDuration(TownID town_id) { throw null; }

        /**
         * Find out which company currently has the exclusive rights of this town.
         * @param town_id The town to check.
         * @returns The company that has the exclusive rights. The value
         *         AICompany.COMPANY_INVALID means that there are currently no
         *         exclusive rights given out to anyone.
         */
        public static CompanyID GetExclusiveRightsCompany(TownID town_id) { throw null; }

        /**
         * Find out how long the town is under influence of the exclusive rights.
         * @param town_id The town to check.
         * @returns The number of months the exclusive rights hold.
         *         The value 0 means that there are currently no exclusive rights
         *         given out to anyone.
         */
        public static int GetExclusiveRightsDuration(TownID town_id) { throw null; }

        /**
         * Find out if an action can currently be performed on the town.
         * @param town_id The town to perform the action on.
         * @param town_action The action to perform on the town.
         * @returns True if and only if the action can performed.
         */
        public static bool IsActionAvailable(TownID town_id, TownAction town_action) { throw null; }

        /**
         * Perform a town action on this town.
         * @param town_id The town to perform the action on.
         * @param town_action The action to perform on the town.
         * @returns True if the action succeeded.
         */
        public static bool PerformTownAction(TownID town_id, TownAction town_action) { throw null; }

        /**
         * Found a new town.
         * @param tile The location of the new town.
         * @param size The town size of the new town.
         * @param city True if the new town should be a city.
         * @param layout The town layout of the new town.
         * @param name The name of the new town. Pass NULL to use a random town name.
         * @ai @pre ScriptSettings.GetValue("economy.found_town") != 0.
         * @ai @pre size != TOWN_SIZE_LARGE.
         * @returns True if the action succeeded.
         * @ai @note AIs are restricted by the advanced setting that controls if funding towns is allowed or not. If custom road layout is forbidden, the layout parameter will be ignored.
         */
        public static bool FoundTown(TileIndex tile, TownSize size, bool city, RoadLayout layout, string name) { throw null; }

        /**
         * Get the rating of a company within a town.
         * @param town_id The town to get the rating for.
         * @param company_id The company to get the rating for.
         * @returns The rating as shown to humans.
         */
        public static TownRating GetRating(TownID town_id, CompanyID company_id) { throw null; }

        /**
         * Get the maximum level of noise that still can be added by airports
         *  before the town start to refuse building a new airport.
         * @param town_id The town to get the allowed noise from.
         * @returns The noise that still can be added.
         */
        public static int GetAllowedNoise(TownID town_id) { throw null; }

        /**
         * Get the road layout for a town.
         * @param town_id The town to get the road layout from.
         * @returns The RoadLayout for the town.
         */
        public static RoadLayout GetRoadLayout(TownID town_id) { throw null; }
    }
}