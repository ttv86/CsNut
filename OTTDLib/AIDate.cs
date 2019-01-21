namespace OpenTTD
{
    /**
     * Class that handles all date related (calculation) functions.
     *
     * @note Months and days of month are 1-based; the first month of the
     *       year is 1 and the first day of the month is also 1.
     * @note Years are zero based; they start with the year 0.
     * @note Dates can be used to determine the number of days between
     *       two different moments in time because they count the number
     *       of days since the year 0.
     */
    public static class AIDate
    {
        /**
         * Date data type is an integer value. Use AIDate.GetDate to
         * compose valid date values for a known year, month and day.
         */
        public static readonly Date DATE_INVALID;

        /**
         * Validates if a date value represent a valid date.
         * @param date The date to validate.
         * @returns True if the date is valid, otherwise false
         */
        public static bool IsValidDate(Date date) { throw null; }

        /**
         * Get the current date.
         * This is the number of days since epoch under the assumption that
         *  there is a leap year every 4 years, except when dividable by
         *  100 but not by 400.
         * @returns The current date.
         */
        public static Date GetCurrentDate() { throw null; }

        /**
         * Get the year of the given date.
         * @param date The date to get the year of.
         * @returns The year.
         */
        public static int GetYear(Date date) { throw null; }

        /**
         * Get the month of the given date.
         * @param date The date to get the month of.
         * @returns The month.
         */
        public static int GetMonth(Date date) { throw null; }

        /**
         * Get the day (of the month) of the given date.
         * @param date The date to get the day of.
         * @returns The day.
         */
        public static int GetDayOfMonth(Date date) { throw null; }

        /**
         * Get the date given a year, month and day of month.
         * @param year The year of the to-be determined date.
         * @param month The month of the to-be determined date.
         * @param day_of_month The day of month of the to-be determined date.
         * @returns The date.
         */
        public static Date GetDate(int year, int month, int day_of_month) { throw null; }
    }
}