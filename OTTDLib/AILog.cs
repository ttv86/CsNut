namespace OpenTTD
{
    /// <summary>Class that handles all log related functions.</summary>
    public static class AILog
    {
        /**
         * Print an Info message to the logs.
         * @param message The message to log.
         * @note Special characters such as U+0000-U+0019 and U+E000-U+E1FF are not supported and removed or replaced by a question mark. This includes newlines and tabs.
        */
        public static void Info(string message) { Testing.AILog.Info(message); }

        /**
         * Print a Warning message to the logs.
         * @param message The message to log.
         * @note Special characters such as U+0000-U+0019 and U+E000-U+E1FF are not supported and removed or replaced by a question mark. This includes newlines and tabs.
         */
        public static void Warning(string message) { Testing.AILog.Warning(message); }

        /**
         * Print an Error message to the logs.
         * @param message The message to log.
         * @note Special characters such as U+0000-U+0019 and U+E000-U+E1FF are not supported and removed or replaced by a question mark. This includes newlines and tabs.
         */
        public static void Error(string message) { Testing.AILog.Error(message); }
    }
}