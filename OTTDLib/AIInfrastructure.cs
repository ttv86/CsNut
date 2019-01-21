namespace OpenTTD
{
    /// <summary>Class that handles all company infrastructure related functions.</summary>
    public static class AIInfrastructure
    {
        /// <summary>Infrastructure categories. </summary>
        public enum Infrastructure
        {
            INFRASTRUCTURE_RAIL,
            INFRASTRUCTURE_SIGNALS,
            INFRASTRUCTURE_ROAD,
            INFRASTRUCTURE_CANAL,
            INFRASTRUCTURE_STATION,
            INFRASTRUCTURE_AIRPORT,
        }
        /**
         * Return the number of rail pieces of a specific rail type for a company.
         * @param company The company to get the count for.
         * @param railtype Rail type to get the count of.
         * @returns Count for the rail type.
         */
        public static int GetRailPieceCount(CompanyID company, RailType railtype) { throw null; }

        /**
         * Return the number of road pieces of a specific road type for a company.
         * @param company The company to get the count for.
         * @param roadtype Road type to get the count of.
         * @returns Count for the road type.
         */
        public static int GetRoadPieceCount(CompanyID company, RoadType roadtype) { throw null; }

        /**
         * Return the number of pieces of an infrastructure category for a company.
         * @param company The company to get the count for.
         * @param infra_type Infrastructure category to get the cost of.
         * @returns Count for the wanted category.
         * @note 
         */
        public static int GetInfrastructurePieceCount(CompanyID company, Infrastructure infra_type) { throw null; }

        /**
         * Return the monthly maintenance costs of a specific rail type for a company.
         * @param company The company to get the monthly cost for.
         * @param railtype Rail type to get the cost of.
         * @returns Monthly maintenance cost for the rail type.
         */
        public static long GetMonthlyRailCosts(CompanyID company, RailType railtype) { throw null; }

        /**
         * Return the monthly maintenance costs of a specific road type for a company.
         * @param company The company to get the monthly cost for.
         * @param roadtype Road type to get the cost of.
         * @returns Monthly maintenance cost for the road type.
         */
        public static long GetMonthlyRoadCosts(CompanyID company, RoadType roadtype) { throw null; }

        /**
         * Return the monthly maintenance costs of an infrastructure category for a company.
         * @param company The company to get the monthly cost for.
         * @param infra_type Infrastructure category to get the cost of.
         * @returns Monthly maintenance cost for the wanted category.
         * @note 
         */
        public static long GetMonthlyInfrastructureCosts(CompanyID company, Infrastructure infra_type) { throw null; }
    }
}