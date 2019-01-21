namespace OpenTTD
{
    /// <summary>Class that handles all sign related functions.</summary>
    public static class AISign
    {
        /// <summary>Base for sign building related errors </summary>
        public static readonly ErrorMessage ERR_SIGN_BASE;

        /// <summary>Too many signs have been placed </summary>
        public static readonly ErrorMessage ERR_SIGN_TOO_MANY_SIGNS;

        /**
         * Checks whether the given sign index is valid.
         * @param sign_id The index to check.
         * @returns True if and only if the sign is valid.
         */
        public static bool IsValidSign(SignID sign_id) { throw null; }

        /**
         * Set the name of a sign.
         * @param sign_id The sign to set the name for.
         * @param name The name for the sign (can be either a raw string, or a ScriptText object).
         * @exception AIError.ERR_NAME_IS_NOT_UNIQUE
         * @returns True if and only if the name was changed.
         */
        public static bool SetName(SignID sign_id, string name) { throw null; }

        /**
         * Get the name of the sign.
         * @param sign_id The sign to get the name of.
         * @returns The name of the sign.
         */
        public static string GetName(SignID sign_id) { throw null; }

        /**
         * Gets the location of the sign.
         * @param sign_id The sign to get the location of.
         * @returns The location of the sign.
         */
        public static TileIndex GetLocation(SignID sign_id) { throw null; }

        /**
         * Builds a sign on the map.
         * @param location The place to build the sign.
         * @param name The text to place on the sign (can be either a raw string, or a ScriptText object).
         * @exception AISign.ERR_SIGN_TOO_MANY_SIGNS
         * @returns The SignID of the build sign (use IsValidSign() to check for validity).
         *   In test-mode it returns 0 if successful, or any other value to indicate
         *   failure.
         */
        public static SignID BuildSign(TileIndex location, string name) { throw null; }

        /**
         * Removes a sign from the map.
         * @param sign_id The sign to remove.
         * @returns True if and only if the sign has been removed.
         */
        public static bool RemoveSign(SignID sign_id) { throw null; }

        public static bool RemoveSign(int sign_id) { throw null; }
    }
}