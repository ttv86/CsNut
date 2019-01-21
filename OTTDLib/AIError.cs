namespace OpenTTD
{
    /// <summary>Class that handles all error related functions.</summary>
    public static class AIError
    {
        /// <summary>All categories errors can be divided in.</summary>
        public static readonly ErrorCategory ERR_CAT_NONE;
        public static readonly ErrorCategory ERR_CAT_GENERAL;
        public static readonly ErrorCategory ERR_CAT_VEHICLE;
        public static readonly ErrorCategory ERR_CAT_STATION;
        public static readonly ErrorCategory ERR_CAT_BRIDGE;
        public static readonly ErrorCategory ERR_CAT_TUNNEL;
        public static readonly ErrorCategory ERR_CAT_TILE;
        public static readonly ErrorCategory ERR_CAT_SIGN;
        public static readonly ErrorCategory ERR_CAT_RAIL;
        public static readonly ErrorCategory ERR_CAT_ROAD;
        public static readonly ErrorCategory ERR_CAT_ORDER;
        public static readonly ErrorCategory ERR_CAT_MARINE;
        public static readonly ErrorCategory ERR_CAT_WAYPOINT;

        /// <summary>Initial error value </summary>
        public static readonly ErrorMessage ERR_NONE;

        /// <summary>If an error occurred and the error wasn't mapped </summary>
        public static readonly ErrorMessage ERR_UNKNOWN;

        /// <summary>If a precondition is not met </summary>
        public static readonly ErrorMessage ERR_PRECONDITION_FAILED;

        /// <summary>A string supplied was too long </summary>
        public static readonly ErrorMessage ERR_PRECONDITION_STRING_TOO_LONG;

        /// <summary>A string had too many parameters </summary>
        public static readonly ErrorMessage ERR_PRECONDITION_TOO_MANY_PARAMETERS;

        /// <summary>The company you use is invalid </summary>
        public static readonly ErrorMessage ERR_PRECONDITION_INVALID_COMPANY;

        /// <summary>An error returned by a NewGRF. No possibility to get the exact error in an script readable format </summary>
        public static readonly ErrorMessage ERR_NEWGRF_SUPPLIED_ERROR;

        /// <summary>Base for general errors </summary>
        public static readonly ErrorMessage ERR_GENERAL_BASE;

        /// <summary>Not enough cash to perform the previous action </summary>
        public static readonly ErrorMessage ERR_NOT_ENOUGH_CASH;

        /// <summary>Local authority won't allow the previous action </summary>
        public static readonly ErrorMessage ERR_LOCAL_AUTHORITY_REFUSES;

        /// <summary>The piece of infrastructure you tried to build is already in place </summary>
        public static readonly ErrorMessage ERR_ALREADY_BUILT;

        /// <summary>Area isn't clear, try to demolish the building on it </summary>
        public static readonly ErrorMessage ERR_AREA_NOT_CLEAR;

        /// <summary>Area / property is owned by another company </summary>
        public static readonly ErrorMessage ERR_OWNED_BY_ANOTHER_COMPANY;

        /// <summary>The name given is not unique for the object type </summary>
        public static readonly ErrorMessage ERR_NAME_IS_NOT_UNIQUE;

        /// <summary>The building you want to build requires flat land </summary>
        public static readonly ErrorMessage ERR_FLAT_LAND_REQUIRED;

        /// <summary>Land is sloped in the wrong direction for this build action </summary>
        public static readonly ErrorMessage ERR_LAND_SLOPED_WRONG;

        /// <summary>A vehicle is in the way </summary>
        public static readonly ErrorMessage ERR_VEHICLE_IN_THE_WAY;

        /// <summary>Site is unsuitable </summary>
        public static readonly ErrorMessage ERR_SITE_UNSUITABLE;

        /// <summary>Too close to the edge of the map </summary>
        public static readonly ErrorMessage ERR_TOO_CLOSE_TO_EDGE;

        /// <summary>Station is too spread out </summary>
        public static readonly ErrorMessage ERR_STATION_TOO_SPREAD_OUT;

        /**
         * Check the membership of the last thrown error.
         * @returns The category the error belongs to.
         * @note The last throw error can be acquired by calling GetLastError().
         */
        public static ErrorCategory GetErrorCategory() { throw null; }

        /// <summary>Get the last error.</summary><returns>An ErrorMessages enum value.</returns>
        public static AIErrorType GetLastError() { throw null; }

        /// <summary>Get the last error in string format (for human readability).</summary><returns>An ErrorMessage enum item, as string.</returns>
        public static string GetLastErrorString() { throw null; }
    }
}