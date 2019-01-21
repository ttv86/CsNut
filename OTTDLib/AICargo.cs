namespace OpenTTD
{
    /// <summary>Class that handles all cargo related functions.</summary>
    public static class AICargo
    {
        /// <summary>The classes of cargo.</summary>

        public static readonly CargoClass CC_PASSENGERS;
        public static readonly CargoClass CC_MAIL;
        public static readonly CargoClass CC_EXPRESS;
        public static readonly CargoClass CC_ARMOURED;
        public static readonly CargoClass CC_BULK;
        public static readonly CargoClass CC_PIECE_GOODS;
        public static readonly CargoClass CC_LIQUID;
        public static readonly CargoClass CC_REFRIGERATED;
        public static readonly CargoClass CC_HAZARDOUS;
        public static readonly CargoClass CC_COVERED;

        /// <summary>The effects a cargo can have on a town.</summary>
        public static readonly TownEffect TE_NONE;
        public static readonly TownEffect TE_PASSENGERS;
        public static readonly TownEffect TE_MAIL;
        public static readonly TownEffect TE_GOODS;
        public static readonly TownEffect TE_WATER;
        public static readonly TownEffect TE_FOOD;

        /// <summary>Special cargo types.</summary>
        public static readonly SpecialCargoID CT_AUTO_REFIT;
        public static readonly SpecialCargoID CT_NO_REFIT;

        /// <summary>Type of cargo distribution.</summary>
        public static readonly DistributionType DT_MANUAL;
        public static readonly DistributionType DT_ASYMMETRIC;
        public static readonly DistributionType DT_SYMMETRIC;
        public static readonly DistributionType INVALID_DISTRIBUTION_TYPE;

        /**
         * Checks whether the given cargo type is valid.
         * @param cargo_type The cargo to check.
         * @returns True if and only if the cargo type is valid.
         */
        public static bool IsValidCargo(CargoID cargo_type) { throw null; }

        /**
         * Checks whether the given town effect type is valid.
         * @param towneffect_type The town effect to check.
         * @returns True if and only if the town effect type is valid.
         */
        public static bool IsValidTownEffect(TownEffect towneffect_type) { throw null; }

        /**
         * Gets the string representation of the cargo label.
         * @param cargo_type The cargo to get the string representation of.
         * @returns The cargo label.
         * @note
         *  - The label uniquely identifies a specific cargo. Use this if you want to
         *    detect special cargos from specific industry set (like production booster cargos, supplies, ...).
         *  - For more generic cargo support, rather check cargo properties though. For example:
         *     - Use AICargo.HasCargoClass(..., CC_PASSENGER) to decide bus vs. truck requirements.
         *     - Use AICargo.GetTownEffect(...) paired with AITown.GetCargoGoal(...) to determine
         *       town growth requirements.
         *  - In other Only words use the cargo label, if you know more about the behaviour
         *    of a specific cargo from a specific industry set, than the API methods can tell you.
         */
        public static string GetCargoLabel(CargoID cargo_type) { throw null; }

        /**
         * Checks whether the give cargo is a freight or not.
         * This defines whether the "freight train weight multiplier" will apply to
         * trains transporting this cargo.
         * @param cargo_type The cargo to check on.
         * @returns True if and only if the cargo is freight.
         */
        public static bool IsFreight(CargoID cargo_type) { throw null; }

        /**
         * Check if this cargo is in the requested cargo class.
         * @param cargo_type The cargo to check on.
         * @param cargo_class The class to check for.
         * @returns True if and only if the cargo is in the cargo class.
         */
        public static bool HasCargoClass(CargoID cargo_type, CargoClass cargo_class) { throw null; }

        /**
         * Get the effect this cargo has on a town.
         * @param cargo_type The cargo to check on.
         * @returns The effect this cargo has on a town, or TE_NONE if it has no effect.
         */
        public static TownEffect GetTownEffect(CargoID cargo_type) { throw null; }

        /**
         * Get the income for transporting a piece of cargo over the
         *   given distance within the specified time.
         * @param cargo_type The cargo to transport.
         * @param distance The distance the cargo travels from begin to end.
         * @param days_in_transit Amount of (game) days the cargo is in transit. The max value of this variable is 637. Any value higher returns the same as 637 would.
         * @returns The amount of money that would be earned by this trip.
         */
        public static long GetCargoIncome(CargoID cargo_type, int distance, int days_in_transit) { throw null; }

        /**
         * Get the cargo distribution type for a cargo.
         * @param cargo_type The cargo to check on.
         * @returns The cargo distribution type for the given cargo.
         */
        public static DistributionType GetDistributionType(CargoID cargo_type) { throw null; }
    }
}