namespace OpenTTD
{
    /// <summary>Class that handles all subsidy related functions.</summary>
    public static class AISubsidy
    {
        /**
         * Enumeration for source and destination of a subsidy.
         * @note The list of values may grow in future.
         */
        public enum SubsidyParticipantType
        {
            SPT_INDUSTRY = 0,
            SPT_TOWN = 1,
            SPT_INVALID = 0xFF,
        }

        /**
         * Check whether this is a valid SubsidyID.
         * @param subsidy_id The SubsidyID to check.
         * @returns True if and only if this subsidy is still valid.
         */
        public static bool IsValidSubsidy(SubsidyID subsidy_id) { throw null; }

        /**
         * Checks whether this subsidy is already awarded to some company.
         * @param subsidy_id The SubsidyID to check.
         * @returns True if and only if this subsidy is already awarded.
         */
        public static bool IsAwarded(SubsidyID subsidy_id) { throw null; }

        /**
         * Get the company index of the company this subsidy is awarded to.
         * @param subsidy_id The SubsidyID to check.
         * @returns The companyindex of the company this subsidy is awarded to.
         */
        public static CompanyID GetAwardedTo(SubsidyID subsidy_id) { throw null; }

        /**
         * Get the date this subsidy expires. In case the subsidy is already
         *  awarded, return the date the subsidy expires, else, return the date the
         *  offer expires.
         * @param subsidy_id The SubsidyID to check.
         * @returns The last valid date of this subsidy.
         * @note The return value of this function will change if the subsidy is
         *  awarded.
         */
        public static Date GetExpireDate(SubsidyID subsidy_id) { throw null; }

        /**
         * Get the cargo type that has to be transported in order to be awarded this
         *  subsidy.
         * @param subsidy_id The SubsidyID to check.
         * @returns The cargo type to transport.
         */
        public static CargoID GetCargoType(SubsidyID subsidy_id) { throw null; }

        /**
         * Returns the type of source of subsidy.
         * @param subsidy_id The SubsidyID to check.
         * @returns Type of source of subsidy.
         */
        public static SubsidyParticipantType GetSourceType(SubsidyID subsidy_id) { throw null; }

        /**
         * Return the source IndustryID/TownID the subsidy is for.
         * \li GetSourceType(subsidy_id) == SPT_INDUSTRY -> return the IndustryID.
         * \li GetSourceType(subsidy_id) == SPT_TOWN -> return the TownID.
         * @param subsidy_id The SubsidyID to check.
         * @returns One of TownID/IndustryID.
         */
        public static int GetSourceIndex(SubsidyID subsidy_id) { throw null; }

        /**
         * Returns the type of destination of subsidy.
         * @param subsidy_id The SubsidyID to check.
         * @returns Type of destination of subsidy.
         */
        public static SubsidyParticipantType GetDestinationType(SubsidyID subsidy_id) { throw null; }

        /**
         * Return the destination IndustryID/TownID the subsidy is for.
         * \li GetDestinationType(subsidy_id) == SPT_INDUSTRY -> return the IndustryID.
         * \li GetDestinationType(subsidy_id) == SPT_TOWN -> return the TownID.
         * @param subsidy_id the SubsidyID to check.
         * @returns One of TownID/IndustryID.
         */
        public static int GetDestinationIndex(SubsidyID subsidy_id) { throw null; }
    }
}