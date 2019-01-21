namespace OpenTTD
{
    /**
     * 'Abstract' class of the Scripts use to register themselves.
     *
     * @note This class is not part of the API. It is purely to document what
     *       Scripts must or can implemented to provide information to OpenTTD to
     *       base configuring/starting/loading the Script on.
     *
     * @note The required functions are also needed for Script Libraries, but in
     *       that case you extend ScriptLibrary. As such the information here can
     *       be used for libraries, but the information will not be shown in the
     *       GUI except for error/debug messages.
     *
     */
    public class AIInfo
    {
        /**
         * Gets the author name to be shown in the 'Available Scripts' window.
         *
         * @returns The author name of the Script.
         * @note This function is required.
         */
        public string GetAuthor() { throw null; }

        /**
         * Gets the Scripts name. This is shown in the 'Available Scripts' window
         * and at all other places where the Script is mentioned, like the debug
         * window or OpenTTD's help message. The name is used to uniquely
         * identify an Script within OpenTTD and this name is used in savegames
         * and the configuration file.
         *
         * @returns The name of the Script.
         * @note This function is required.
         * @note This name is not used as library name by AIController.Import,
         * instead the name returned by 
         */
        public string GetName() { throw null; }

        /**
         * Gets a 4 ASCII character short name of the Script to uniquely
         * identify it from other Scripts. The short name is primarily
         * used as unique identifier for the content system.
         * The content system uses besides the short name also the
         * MD5 checksum of all the source files to uniquely identify
         * a specific version of the Script.
         *
         * The short name must consist of precisely four ASCII
         * characters, or more precisely four non-zero bytes.
         *
         * @returns The name of the Script.
         * @note This function is required.
         */
        public string GetShortName() { throw null; }

        /**
         * Gets the description to be shown in the 'Available Scripts' window.
         *
         * @returns The description for the Script.
         * @note This function is required.
         */
        public string GetDescription() { throw null; }

        /**
         * Gets the version of the Script. This is a number to (in theory)
         * uniquely identify the versions of an Script. Generally the
         * 'instance' of an Script with the highest version is chosen to
         * be loaded.
         *
         * When OpenTTD finds, during starting, a duplicate Script with the
         * same version number one is randomly chosen. So it is
         * important that this number is regularly updated/incremented.
         *
         * @returns The version number of the Script.
         * @note This function is required.
         */
        public int GetVersion() { throw null; }

        /**
         * Gets the lowest version of the Script that OpenTTD can still load
         * the savegame of. In other words, from which version until this
         * version can the Script load the savegames.
         *
         * If this function does not exist OpenTTD assumes it can only
         * load savegames of this version. As such it will not upgrade
         * to this version upon load.
         *
         * @returns The lowest version number we load the savegame data.
         * @note This function is optional.
         */
        public int MinVersionToLoad() { throw null; }

        /**
         * Gets the development/release date of the Script.
         *
         * The intention of this is to give the user an idea how old the
         * Script is and whether there might be a newer version.
         *
         * @returns The development/release date for the Script.
         * @note This function is required.
         */
        public string GetDate() { throw null; }

        /**
         * Can this Script be used as random Script?
         *
         * The idea behind this function is to 'forbid' highly
         * competitive or other special Scripts from running in games unless
         * the user explicitly selects the Script to be loaded. This to
         * try to prevent users from complaining that the Script is too
         * aggressive or does not build profitable routes.
         *
         * If this function does not exist OpenTTD assumes the Script can
         * be used as random Script. As such it will be randomly chosen.
         *
         * @returns True if the Script can be used as random Script.
         * @note This function is optional.
         *
         */
        public bool UseAsRandomAI() { throw null; }

        /**
         * Gets the name of main class of the Script so OpenTTD knows
         * what class to instantiate. For libraries, this name is also
         * used when other scripts import it using AIController.Import.
         *
         * @returns The class name of the Script.
         * @note This function is required.
         */
        public string CreateInstance() { throw null; }

        /**
         * Gets the API version this Script is written for. If this function
         * does not exist API compatibility with version 0.7 is assumed.
         * If the function returns something OpenTTD does not understand,
         * for example a newer version or a string that is not a version,
         * the Script will not be loaded.
         *
         * Although in the future we might need to make a separate
         * compatibility 'wrapper' for a specific version of OpenTTD, for
         * example '0.7.1', we will use only the major and minor number
         * and not the bugfix number as valid return for this function.
         *
         * Valid return values are:
         * - "0.7" (for AI only)
         * - "1.0" (for AI only)
         * - "1.1" (for AI only)
         * - "1.2" (for both AI and GS)
         * - "1.3" (for both AI and GS)
         *
         * @returns The version this Script is compatible with.
         */
        public string GetAPIVersion() { throw null; }

        /**
         * Gets the URL to be shown in the 'this Script has crashed' message
         * and in the 'Available Scripts' window. If this function does not
         * exist no URL will be shown.
         *
         * This function purely exists to redirect users of the Script to the
         * right place on the internet to discuss the Script and report bugs
         * of this Script.
         *
         * @returns The URL to show.
         * @note This function is optional.
         */
        public string GetURL() { throw null; }

        /**
         * Gets the settings that OpenTTD shows in the "Script Parameters" window
         * so the user can customize the Script. This is a special function that
         * doesn't need to return anything. Instead you can call AddSetting
         * and AddLabels here.
         *
         * @note This function is optional.
         */
        public void GetSettings() { throw null; }

        /// <summary>Miscellaneous flags for Script settings. </summary>

        /**
         * Add a user configurable setting for this Script. You can call this
         * as many times as you have settings.
         * @param setting_description A table with all information about a
         *  single setting. The table should have the following name/value pairs:
         *  - name The name of the setting, this is used in openttd.cfg to
         *    store the current configuration of Scripts. Required.
         *  - description A single line describing the setting. Required.
         *  - min_value The minimum value of this setting. Required for integer
         *    settings and not allowed for boolean settings.
         *  - max_value The maximum value of this setting. Required for integer
         *    settings and not allowed for boolean settings.
         *  - easy_value The default value if the easy difficulty level
         *    is selected. Required.
         *  - medium_value The default value if the medium difficulty level
         *    is selected. Required.
         *  - hard_value The default value if the hard difficulty level
         *    is selected. Required.
         *  - custom_value The default value if the custom difficulty level
         *    is selected. Required.
         *  - random_deviation If this property has a nonzero value, then the
         *    actual value of the setting in game will be
         *    user_configured_value + random(-random_deviation, random_deviation).
         *    Not allowed if the CONFIG_RANDOM flag is set, otherwise optional.
         *  - step_size The increase/decrease of the value every time the user
         *    clicks one of the up/down arrow buttons. Optional, default is 1.
         *  - flags Bitmask of some flags, see ScriptConfigFlags. Required.
         *
         * @note This is a function provided by OpenTTD, you don't have to
         * include it in your Script but should just call it from GetSettings.
         */
        public void AddSetting(object setting_description) { throw null; }

        /**
         * Add labels for the values of a setting. Instead of a number the
         *  user will see the corresponding name.
         * @param setting_name The name of the setting.
         * @param value_names A table that maps values to names. The first
         *   character of every identifier is ignored and the rest should
         *   be an integer of the value you define a name for. The value
         *   is a short description of that value.
         * To define labels for a setting named "competition_level" you could
         * for example call it like this:
         * AddLabels("competition_level", {_0 = "no competition", _1 = "some competition",
         * _2 = "a lot of competition"})  { throw null; }
         *
         * @note This is a function provided by OpenTTD, you don't have to
         * include it in your Script but should just call it from GetSettings.
         */
        public void AddLabels(string setting_name, object value_names) { throw null; }
    }
}