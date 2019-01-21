namespace OpenTTD
{
    /**
     * Class that handles some basic functions.
     *
     * @note The random functions are not called Random and RandomRange, because
     *        RANDOM_DEBUG does some tricky stuff, which messes with those names.
     * @note In MP we cannot use Random because that will cause desyncs (scripts are
     *        ran on the server only, not on all clients). This means that
     *        we use InteractiveRandom in MP. Rand() takes care of this for you.
     */
    public static class AIBase
    {
        /// <summary>Get a random value.</summary><returns>A random value between 0 and MAX(uint32).</returns>
        public static int Rand() { throw null; }

        /**
         * Get a random value.
         * @param unused_param This parameter is not used, but is needed to work with lists.
         * @returns A random value between 0 and MAX(uint32).
         */
        public static int RandItem(int unused_param) { throw null; }

        /**
         * Get a random value in a range.
         * @param max The first number this function will never return (the maximum it returns is max - 1).
         * @returns A random value between 0 .. max - 1.
         */
        public static int RandRange(int max) { throw null; }

        /**
         * Get a random value in a range.
         * @param unused_param This parameter is not used, but is needed to work with lists.
         * @param max The first number this function will never return (the maximum it returns is max - 1).
         * @returns A random value between 0 .. max - 1.
         */
        public static int RandRangeItem(int unused_param, int max) { throw null; }

        /**
         * Returns approximately 'out' times true when called 'max' times.
         *   After all, it is a random function.
         * @param out How many times it should return true.
         * @param max Out of this many times.
         * @returns True if the chance worked out.
         */
        public static bool Chance(int @out, int max) { throw null; }

        /**
         * Returns approximately 'out' times true when called 'max' times.
         *   After all, it is a random function.
         * @param unused_param This parameter is not used, but is needed to work with lists.
         * @param out How many times it should return true.
         * @param max Out of this many times.
         * @returns True if the chance worked out.
         */
        public static bool ChanceItem(int unused_param, int @out, int max) { throw null; }
    }
}