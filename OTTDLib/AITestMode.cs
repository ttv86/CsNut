namespace OpenTTD
{
    /**
     * Class to switch current mode to Test Mode.
     * If you create an instance of this class, the mode will be switched to
     *   Testing. The original mode is stored and recovered from when ever the
     *   instance is destroyed.
     * In Test mode all the commands you execute aren't really executed. The
     *   system only checks if it would be able to execute your requests, and what
     *   the cost would be.
     */
    public class AITestMode
    {
        /**
         * Creating instance of this class switches the build mode to Testing.
         * @note When the instance is destroyed, he restores the mode that was
         *   current when the instance was created!
         */
        public AITestMode() { throw null; }
    }
}