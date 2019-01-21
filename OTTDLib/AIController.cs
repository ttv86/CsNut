namespace OpenTTD
{
    ///<summary>
    ///The Controller, the class each Script should extend. It creates the Script,
    /// makes sure the logic kicks in correctly, and that 
    /// value.
    ///
    ///When starting a new game, or when loading a game, OpenTTD tries to match a
    /// script that matches to the specified version as close as possible. It tries
    /// (from first to last, stopping as soon as the attempt succeeds)
    ///
    /// - load the exact same version of the same script,
    /// - load the latest version of the same script that supports loading data from
    ///   the saved version (the version of saved data must be equal or greater
    ///   than AIInfo.MinVersionToLoad),
    /// - load the latest version of the same script (ignoring version requirements),
    /// - (for AIs) load a random AI, and finally
    /// - (for AIs) load the dummy AI.
    ///
    ///After determining the script to use, starting it is done as follows
    ///
    ///- An instance is constructed of the class derived from ScriptController
    ///  (class name is retrieved from AIInfo.CreateInstance).
    ///- If there is script data available in the loaded game and if the data is
    ///  loadable according to AIInfo.MinVersionToLoad, 
    ///  data from the loaded game.
    ///- Finally, 
    ///
    ///See also http://wiki.openttd.org/AI:Save/Load for more details.
    /// </summary>
    public abstract class AIController
    {
        /// <summary>
        /// Initializer of the ScriptController.
        /// </summary>
        public AIController() { throw null; }

        /// <summary>
        /// Initializer of the ScriptController.
        /// </summary>
        /// <param name="company">The company this Script is normally serving.</param>
        public AIController(CompanyID company) { throw null; }

        /**
         * This function is called to start your script. Your script starts here. If you
         *   return from this function, your script dies, so make sure that doesn't
         *   happen.
         * @note Cannot be called from within your script.
         */
        protected abstract void Start();

        /**
         * Save the state of the script.
         *
         * By implementing this function, you can store some data inside the savegame.
         *   The function should return a table with the information you want to store.
         *   You can only store:
         *
         *   - integers,
         *   - strings,
         *   - arrays (max. 25 levels deep),
         *   - tables (max. 25 levels deep),
         *   - booleans, and
         *   - nulls.
         *
         * In particular, instances of classes can't be saved including
         *   ScriptList. Such a list should be converted to an array or table on
         *   save and converted back on load.
         *
         * The function is called as soon as the user saves the game,
         *   independently of other activities of the script. The script is not
         *   notified of the call. To avoid race-conditions between 
         *   other script code, change variables directly after a 
         *   very unlikely, to get interrupted at that point in the execution.
         * See also http://wiki.openttd.org/AI:Save/Load for more details.
         *
         * @note No other information is saved than the table returned by 
         *   For example all pending events are lost as soon as the game is loaded.
         *
         * @returns Data of the script that should be stored in the save game.
         */
        protected virtual object Save() { return null; }

        /**
         * Load saved data just before calling 
         * The function is only called when there is data to load.
         * @param version Version number of the script that created the \a data.
         * @param data Data that was saved (return value of 
         */
        protected virtual void Load(int version, object data) { }

        /// <summary>Find at which tick your script currently is.</summary><returns>returns the current tick.</returns>
        static public int GetTick() { throw null; }

        /**
         * Get the number of operations the script may still execute this tick.
         * @returns The amount of operations left to execute.
         * @note This number can go negative when certain uninteruptable
         *   operations are executed. The amount of operations that you go
         *   over the limit will be deducted from the next tick you would
         *   be allowed to run.
         */
        static public int GetOpsTillSuspend() { throw null; }

        /**
         * Get the value of one of your settings you set via info.nut.
         * @param name The name of the setting.
         * @returns the value for the setting, or -1 if the setting is not known.
         */
        static public int GetSetting(string name) { throw null; }

        /**
         * Get the OpenTTD version of this executable. The version is formatted
         * with the bits having the following meaning:
         * 28-31 major version
         * 24-27 minor version
         * 20-23 build
         *    19 1 if it is a release, 0 if it is not.
         *  0-18 revision number; 0 when the revision is unknown.
         * @returns The version in newgrf format.
         */
        static public int GetVersion() { throw null; }

        /**
         * Change the minimum amount of time the script should be put in suspend mode
         *   when you execute a command. Normally in SP this is 1, and in MP it is
         *   what ever delay the server has been programmed to delay commands
         *   (normally between 1 and 5). To give a more 'real' effect to your script,
         *   you can control that number here.
         * @param ticks The minimum amount of ticks to wait.
         * @note If the number is lower than the MP setting, the MP setting wins.
         */
        static public void SetCommandDelay(int ticks) { throw null; }

        /**
         * Sleep for X ticks. The code continues after this line when the X script ticks
         *   are passed. Mind that an script tick is different from in-game ticks and
         *   differ per script speed.
         * @param ticks the ticks to wait
         */
        static public void Sleep(int ticks) { throw null; }

        /**
         * Break execution of the script when script developer tools are active. For
         * other users, nothing will happen when you call this function. To resume
         * the script, you have to click on the continue button in the AI debug
         * window. It is not recommended to leave calls to this function in scripts
         * that you publish or upload to bananas.
         * @param message to print in the AI debug window when the break occurs.
         * @note gui.ai_developer_tools setting must be enabled or the break is
         * ignored.
         */
        static public void Break(string message) { throw null; }

        /**
         * When Squirrel triggers a print, this function is called.
         *  Squirrel calls this when 'print' is used, or when the script made an error.
         * @param error_msg If true, it is a Squirrel error message.
         * @param message The message Squirrel logged.
         * @note Use ScriptLog.Info/Warning/Error instead of 'print'.
         */
        static public void Print(bool error_msg, string message) { throw null; }

        /**
         * Import a library.
         * @param library The name of the library to import. The name should be composed as AIInfo.GetCategory() + "." +
         * AIInfo.CreateInstance().
         * @param class_name Under which name you want it to be available (or "" if you just want the returning object).
         * @param version Which version you want specifically.
         * @returns The loaded library object. If class_name is set, it is also available (under the scope of the import) under that name.
         * @note This command can be called from the global space, and does not need an instance.
         */
        static public object Import(string library, string class_name, int version) { throw null; }
    }
}