namespace OpenTTD
{
    /**
     * Class to switch current mode to Execute Mode.
     * If you create an instance of this class, the mode will be switched to
     *   Execute. The original mode is stored and recovered from when ever the
     *   instance is destroyed.
     * In Execute mode all commands you do are executed for real.
     */
    public class AIExecMode
    {
        /**
         * Creating instance of this class switches the build mode to Execute.
         * @note When the instance is destroyed, he restores the mode that was
         *   current when the instance was created!
         */
        public AIExecMode() { throw null; }
    }
}