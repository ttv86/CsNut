namespace OpenTTD
{
    /// <summary>Class that handles all industry-type related functions.</summary>
    public static class AIIndustryType
    {
        /// <summary>Special IndustryTypes.</summary>
        public enum SpecialIndustryType
        {
            INDUSTRYTYPE_UNKNOWN = 0xFE,
            INDUSTRYTYPE_TOWN = 0xFF,
        }

        /**
         * Checks whether the given industry-type is valid.
         * @param industry_type The type check.
         * @returns True if and only if the industry-type is valid.
         */
        public static bool IsValidIndustryType(IndustryType industry_type) { throw null; }

        /**
         * Get the name of an industry-type.
         * @param industry_type The type to get the name for.
         * @returns The name of an industry.
         */
        public static string GetName(IndustryType industry_type) { throw null; }

        /**
         * Get a list of CargoID possible produced by this industry-type.
         * @warning This function only returns the default cargoes of the industry type.
         *          Industries can specify new cargotypes on construction.
         * @param industry_type The type to get the CargoIDs for.
         * @returns The CargoIDs of all cargotypes this industry could produce.
         */
        public static AIList<CargoID> GetProducedCargo(IndustryType industry_type) { throw null; }

        /**
         * Get a list of CargoID accepted by this industry-type.
         * @warning This function only returns the default cargoes of the industry type.
         *          Industries can specify new cargotypes on construction.
         * @param industry_type The type to get the CargoIDs for.
         * @returns The CargoIDs of all cargotypes this industry accepts.
         */
        public static AIList<CargoID> GetAcceptedCargo(IndustryType industry_type) { throw null; }

        /**
         * Is this industry type a raw industry?
         * Raw industries usually produce cargo without any prerequisites.
         * ("Usually" means that advanced NewGRF industry concepts might not fit the "raw"/"processing"
         * classification, so it's up to the interpretation of the NewGRF author.)
         * @param industry_type The type of the industry.
         * @returns True if it should be handled as a raw industry.
         * @note Industries might be neither raw nor processing.
         *       This is usually the case for industries which produce nothing (e.g. power plants),
         *       but also for weird industries like temperate banks and tropic lumber mills.
         */
        public static bool IsRawIndustry(IndustryType industry_type) { throw null; }

        /**
         * Is this industry type a processing industry?
         * Processing industries usually produce cargo when delivered with input cargo.
         * ("Usually" means that advanced NewGRF industry concepts might not fit the "raw"/"processing"
         * classification, so it's up to the interpretation of the NewGRF author.)
         * @param industry_type The type of the industry.
         * @returns True if it is a processing industry.
         * @note Industries might be neither raw nor processing.
         *       This is usually the case for industries which produce nothing (e.g. power plants),
         *       but also for weird industries like temperate banks and tropic lumber mills.
         */
        public static bool IsProcessingIndustry(IndustryType industry_type) { throw null; }

        /**
         * Can the production of this industry increase?
         * @param industry_type The type of the industry.
         * @returns True if the production of this industry can increase.
         */
        public static bool ProductionCanIncrease(IndustryType industry_type) { throw null; }

        /**
         * Get the cost for building this industry-type.
         * @param industry_type The type of the industry.
         * @returns The cost for building this industry-type.
         */
        public static long GetConstructionCost(IndustryType industry_type) { throw null; }

        /**
         * Can you build this type of industry?
         * @param industry_type The type of the industry.
         * @returns True if you can build this type of industry at locations of your choice.
         * @ai @note Returns false if you can only prospect this type of industry, or not build it at all.
         */
        public static bool CanBuildIndustry(IndustryType industry_type) { throw null; }

        /**
         * Can you prospect this type of industry?
         * @param industry_type The type of the industry.
         * @returns True if you can prospect this type of industry.
         * @ai @note If the setting "Manual primary industry construction method" is set
         * @ai to either "None" or "as other industries" this function always returns false.
         */
        public static bool CanProspectIndustry(IndustryType industry_type) { throw null; }

        /**
         * Build an industry of the specified type.
         * @param industry_type The type of the industry to build.
         * @param tile The tile to build the industry on.
         * @returns True if the industry was successfully build.
         */
        public static bool BuildIndustry(IndustryType industry_type, TileIndex tile) { throw null; }

        /**
         * Prospect an industry of this type. Prospecting an industries let the game try to create
         * an industry on a random place on the map.
         * @param industry_type The type of the industry.
         * @returns True if no error occurred while trying to prospect.
         * @note Even if true is returned there is no guarantee a new industry is build.
         * @note If true is returned the money is paid, whether a new industry was build or not.
         */
        public static bool ProspectIndustry(IndustryType industry_type) { throw null; }

        /**
         * Is this type of industry built on water.
         * @param industry_type The type of the industry.
         * @returns True when this type is built on water.
         */
        public static bool IsBuiltOnWater(IndustryType industry_type) { throw null; }

        /**
         * Does this type of industry have a heliport?
         * @param industry_type The type of the industry.
         * @returns True when this type has a heliport.
         */
        public static bool HasHeliport(IndustryType industry_type) { throw null; }

        /**
         * Does this type of industry have a dock?
         * @param industry_type The type of the industry.
         * @returns True when this type has a dock.
         */
        public static bool HasDock(IndustryType industry_type) { throw null; }
    }
}