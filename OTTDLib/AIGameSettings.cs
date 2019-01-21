namespace OpenTTD
{
    /**
     * Class that handles all game settings related functions.
     *
     * @note AIGameSettings.IsValid and AIGameSettings.GetValue are functions
     *       that rely on the settings as OpenTTD stores them in savegame and
     *       openttd.cfg. No guarantees can be given on the long term validity,
     *       consistency and stability of the names, values and value ranges.
     *       Using these settings can be dangerous and could cause issues in
     *       future versions. To make sure that a setting still exists in the
     *       current version you have to run AIGameSettings.IsValid before
     *       accessing it.
     *
     * @note The names of the setting for AIGameSettings.IsValid and
     *       AIGameSettings.GetValue are the same ones as those that are shown by
     *       the list_settings command in the in-game console. Settings that are
     *       string based are NOT supported and AIGameSettings.IsValid will return
     *       false for them. These settings will not be supported either because
     *       they have no relevance for the script (default client names, server IPs,
     *       etc.).
     */
    public static class AIGameSettings
    {
        /**
         * Is the given game setting a valid setting for this instance of OpenTTD?
         * @param setting The setting to check for existence.
         * @warning Results of this function are not governed by the API. This means
         *          that a setting that previously existed can be gone or has
         *          changed its name.
         * @note Results achieved in the past offer no guarantee for the future.
         * @returns True if and only if the setting is valid.
         */
        public static bool IsValid(string setting) { throw null; }

        /**
         * Gets the value of the game setting.
         * @param setting The setting to get the value of.
         * @warning Results of this function are not governed by the API. This means
         *          that the value of settings may be out of the expected range. It
         *          also means that a setting that previously existed can be gone or
         *          has changed its name/characteristics.
         * @note Results achieved in the past offer no guarantee for the future.
         * @returns The value for the setting.
         */
        public static int GetValue(string setting) { throw null; }

        /**
         * Checks whether the given vehicle-type is disabled for companies.
         * @param vehicle_type The vehicle-type to check.
         * @returns True if the vehicle-type is disabled.
         */
        public static bool IsDisabledVehicleType(VehicleType vehicle_type) { throw null; }
    }
}