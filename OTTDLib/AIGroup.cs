namespace OpenTTD
{
    /// <summary>Class that handles all group related functions.</summary>
    public static class AIGroup
    {
        /// <summary>The group IDs of some special groups.</summary>

        public static readonly GroupID GROUP_ALL;
        public static readonly GroupID GROUP_DEFAULT;
        public static readonly GroupID GROUP_INVALID;

        /**
         * Checks whether the given group is valid.
         * @param group_id The group to check.
         * @returns True if and only if the group is valid.
         */
        public static bool IsValidGroup(GroupID group_id) { throw null; }

        /**
         * Create a new group.
         * @param vehicle_type The type of vehicle to create a group for.
         * @returns The GroupID of the new group, or an invalid GroupID when
         *  it failed. Check the return value using IsValidGroup(). In test-mode
         *  0 is returned if it was successful; any other value indicates failure.
         */
        public static GroupID CreateGroup(VehicleType vehicle_type) { throw null; }

        /**
         * Delete the given group. When the deletion succeeds all vehicles in the
         *  given group will move to the GROUP_DEFAULT.
         * @param group_id The group to delete.
         * @returns True if and only if the group was successfully deleted.
         */
        public static bool DeleteGroup(GroupID group_id) { throw null; }

        /**
         * Get the vehicle type of a group.
         * @param group_id The group to get the type from.
         * @returns The vehicletype of the given group.
         */
        public static VehicleType GetVehicleType(GroupID group_id) { throw null; }

        /**
         * Set the name of a group.
         * @param group_id The group to set the name for.
         * @param name The name for the group (can be either a raw string, or a ScriptText object).
         * @exception AIError.ERR_NAME_IS_NOT_UNIQUE
         * @returns True if and only if the name was changed.
         */
        public static bool SetName(GroupID group_id, string name) { throw null; }

        /**
         * Get the name of a group.
         * @param group_id The group to get the name of.
         * @returns The name the group has.
         */
        public static string GetName(GroupID group_id) { throw null; }

        /**
         * Enable or disable autoreplace protected. If the protection is
         *  enabled, global autoreplace won't affect vehicles in this group.
         * @param group_id The group to change the protection for.
         * @param enable True if protection should be enabled.
         * @returns True if and only if the protection was successfully changed.
         */
        public static bool EnableAutoReplaceProtection(GroupID group_id, bool enable) { throw null; }

        /**
         * Get the autoreplace protection status.
         * @param group_id The group to get the protection status for.
         * @returns The autoreplace protection status for the given group.
         */
        public static bool GetAutoReplaceProtection(GroupID group_id) { throw null; }

        /**
         * Get the number of engines in a given group.
         * @param group_id The group to get the number of engines in.
         * @param engine_id The engine id to count.
         * @returns The number of engines with id engine_id in the group with id group_id.
         */
        public static int GetNumEngines(GroupID group_id, EngineID engine_id) { throw null; }

        /**
         * Move a vehicle to a group.
         * @param group_id The group to move the vehicle to.
         * @param vehicle_id The vehicle to move to the group.
         * @returns True if and only if the vehicle was successfully moved to the group.
         * @note A vehicle can be in only one group at the same time. To remove it from
         *  a group, move it to another or to GROUP_DEFAULT. Moving the vehicle to the
         *  given group means removing it from another group.
         */
        public static bool MoveVehicle(GroupID group_id, VehicleID vehicle_id) { throw null; }

        /**
         * Enable or disable the removal of wagons when a (part of a) vehicle is
         *  (auto)replaced with a longer variant (longer wagons or longer engines)
         *  If enabled, wagons are removed from the end of the vehicle until it
         *  fits in the same number of tiles as it did before.
         * @param keep_length If true, wagons will be removed if the new engine is longer.
         * @returns True if and only if the value was successfully changed.
         */
        public static bool EnableWagonRemoval(bool keep_length) { throw null; }

        /// <summary>Get the current status of wagon removal.</summary><returns>Whether or not wagon removal is enabled.</returns>
        public static bool HasWagonRemoval() { throw null; }

        /**
         * Start replacing all vehicles with a specified engine with another engine.
         * @param group_id The group to replace vehicles from. Use ALL_GROUP to replace
         *  vehicles from all groups that haven't set autoreplace protection.
         * @param engine_id_old The engine id to start replacing.
         * @param engine_id_new The engine id to replace with.
         * @returns True if and if the replacing was successfully started.
         * @note To stop autoreplacing engine_id_old, call StopAutoReplace(group_id, engine_id_old).
         */
        public static bool SetAutoReplace(GroupID group_id, EngineID engine_id_old, EngineID engine_id_new) { throw null; }

        /**
         * Get the EngineID the given EngineID is replaced with.
         * @param group_id The group to get the replacement from.
         * @param engine_id The engine that is being replaced.
         * @returns The EngineID that is replacing engine_id or an invalid EngineID
         *   in case engine_id is not begin replaced.
         */
        public static EngineID GetEngineReplacement(GroupID group_id, EngineID engine_id) { throw null; }

        /**
         * Stop replacing a certain engine in the specified group.
         * @param group_id The group to stop replacing the engine in.
         * @param engine_id The engine id to stop replacing with another engine.
         * @returns True if and if the replacing was successfully stopped.
         */
        public static bool StopAutoReplace(GroupID group_id, EngineID engine_id) { throw null; }
    }
}