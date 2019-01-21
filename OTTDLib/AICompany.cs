namespace OpenTTD
{
    /* All companies below MAX_COMPANIES are playable
     * companies, above, they are special, computer controlled 'companies' */
    public class Owner
    {
        /*OWNER_BEGIN,
        COMPANY_FIRST,
        MAX_COMPANIES,
        OWNER_TOWN,
        OWNER_NONE,
        OWNER_WATER,
        OWNER_DEITY,
        OWNER_END,
        INVALID_OWNER,
        INVALID_COMPANY,

        COMPANY_INACTIVE_CLIENT,
        COMPANY_NEW_COMPANY,
        COMPANY_SPECTATOR,*/
    }

    /// <summary>Class that handles all company related functions.</summary>
    public static class AICompany
    {
        /// <summary>The range of possible quarters to get company information of. </summary>
        public static readonly Quarter CURRENT_QUARTER;
        public static readonly Quarter EARLIEST_QUARTER;

        /// <summary>Different constants related to CompanyID. </summary>
        public static readonly CompanyID COMPANY_FIRST;
        public static readonly CompanyID COMPANY_LAST;

        public static readonly CompanyID COMPANY_INVALID;
        public static readonly CompanyID COMPANY_SELF;
        public static readonly CompanyID COMPANY_SPECTATOR;

        /// <summary>Possible genders for company presidents. </summary>
        public static readonly Gender GENDER_MALE;
        public static readonly Gender GENDER_FEMALE;
        public static readonly Gender GENDER_INVALID;

        /**
         * Resolved the given company index to the correct index for the company. If
         *  the company index was COMPANY_SELF it will be resolved to the index of
         *  your company. If the company with the given index does not exist it will
         *  return COMPANY_INVALID.
         * @param company The company index to resolve.
         * @returns The resolved company index.
         */
        public static CompanyID ResolveCompanyID(CompanyID company) { throw null; }

        /**
         * Check if a CompanyID is your CompanyID, to ease up checks.
         * @param company The company index to check.
         * @returns True if and only if this company is your CompanyID.
         */
        public static bool IsMine(CompanyID company) { throw null; }

        /**
         * Set the name of your company.
         * @param name The new name of the company (can be either a raw string, or a ScriptText object).
         * @exception AIError.ERR_NAME_IS_NOT_UNIQUE
         * @returns True if the name was changed.
         */
        public static bool SetName(string name) { throw null; }

        /**
         * Get the name of the given company.
         * @param company The company to get the name for.
         * @returns The name of the given company.
         */
        public static string GetName(CompanyID company) { throw null; }

        /**
         * Set the name of your president.
         * @param name The new name of the president (can be either a raw string, or a ScriptText object).
         * @exception AIError.ERR_NAME_IS_NOT_UNIQUE
         * @returns True if the name was changed.
         */
        public static bool SetPresidentName(string name) { throw null; }

        /**
         * Get the name of the president of the given company.
         * @param company The company to get the president's name for.
         * @returns The name of the president of the given company.
         */
        public static string GetPresidentName(CompanyID company) { throw null; }

        /**
         * Set the gender of the president of your company.
         * @param gender The new gender for your president.
         * @returns True if the gender was changed.
         * @note When successful a random face will be created.
         */
        public static bool SetPresidentGender(Gender gender) { throw null; }

        /**
         * Get the gender of the president of the given company.
         * @param company The company to get the presidents gender off.
         * @returns The gender of the president.
         */
        public static Gender GetPresidentGender(CompanyID company) { throw null; }

        /**
         * Sets the amount to loan.
         * @param loan The amount to loan (multiplier of GetLoanInterval()).
         * @returns True if the loan could be set to your requested amount.
         */
        public static bool SetLoanAmount(long loan) { throw null; }

        /**
         * Sets the minimum amount to loan, i.e. the given amount of loan rounded up.
         * @param loan The amount to loan (any positive number).
         * @returns True if we could allocate a minimum of 'loan' loan.
         */
        public static bool SetMinimumLoanAmount(long loan) { throw null; }

        /// <summary>Gets the amount your company have loaned.</summary><returns>The amount loaned money.</returns>
        public static long GetLoanAmount() { throw null; }

        /// <summary>Gets the maximum amount your company can loan.</summary><returns>The maximum amount your company can loan.</returns>
        public static long GetMaxLoanAmount() { throw null; }

        /// <summary>Gets the interval/loan step.</summary><returns>The loan step.</returns>
        public static long GetLoanInterval() { throw null; }

        /**
         * Gets the bank balance. In other words, the amount of money the given company can spent.
         * @param company The company to get the bank balance of.
         * @returns The actual bank balance.
         */
        public static long GetBankBalance(CompanyID company) { throw null; }

        /**
         * Get the income of the company in the given quarter.
         * Note that this function only considers recurring income from vehicles;
         * it does not include one-time income from selling stuff.
         * @param company The company to get the quarterly income of.
         * @param quarter The quarter to get the income of.
         * @returns The gross income of the company in the given quarter.
         */
        public static long GetQuarterlyIncome(CompanyID company, int quarter) { throw null; }

        /**
         * Get the expenses of the company in the given quarter.
         * Note that this function only considers recurring expenses from vehicle
         * running cost, maintenance and interests; it does not include one-time
         * expenses from construction and buying stuff.
         * @param company The company to get the quarterly expenses of.
         * @param quarter The quarter to get the expenses of.
         * @returns The expenses of the company in the given quarter.
         */
        public static long GetQuarterlyExpenses(CompanyID company, int quarter) { throw null; }

        /**
         * Get the amount of cargo delivered by the given company in the given quarter.
         * @param company The company to get the amount of delivered cargo of.
         * @param quarter The quarter to get the amount of delivered cargo of.
         * @returns The amount of cargo delivered by the given company in the given quarter.
         */
        public static int GetQuarterlyCargoDelivered(CompanyID company, int quarter) { throw null; }

        /**
         * Get the performance rating of the given company in the given quarter.
         * @param company The company to get the performance rating of.
         * @param quarter The quarter to get the performance rating of.
         * @note The performance rating is calculated after every quarter, so the value for CURRENT_QUARTER is undefined.
         * @returns The performance rating of the given company in the given quarter.
         */
        public static int GetQuarterlyPerformanceRating(CompanyID company, int quarter) { throw null; }

        /**
         * Get the value of the company in the given quarter.
         * @param company The company to get the value of.
         * @param quarter The quarter to get the value of.
         * @returns The value of the company in the given quarter.
         */
        public static long GetQuarterlyCompanyValue(CompanyID company, int quarter) { throw null; }

        /**
         * Build your company's HQ on the given tile.
         * @param tile The tile to build your HQ on, this tile is the most northern tile of your HQ.
         * @exception AIError.ERR_AREA_NOT_CLEAR
         * @exception AIError.ERR_FLAT_LAND_REQUIRED
         * @returns True if the HQ could be build.
         * @note An HQ can not be removed, only by water or rebuilding; If an HQ is
         *  build again, the old one is removed.
         */
        public static bool BuildCompanyHQ(TileIndex tile) { throw null; }

        /**
         * Return the location of a company's HQ.
         * @param company The company the get the HQ of.
         * @returns The tile of the company's HQ, this tile is the most northern tile
         *  of that HQ, or AIMap.TILE_INVALID if there is no HQ yet.
         */
        public static TileIndex GetCompanyHQ(CompanyID company) { throw null; }

        /**
         * Set whether autorenew is enabled for your company.
         * @param autorenew The new autorenew status.
         * @returns True if autorenew status has been modified.
         */
        public static bool SetAutoRenewStatus(bool autorenew) { throw null; }

        /**
         * Return whether autorenew is enabled for a company.
         * @param company The company to get the autorenew status of.
         * @returns True if autorenew is enabled.
         */
        public static bool GetAutoRenewStatus(CompanyID company) { throw null; }

        /**
         * Set the number of months before/after max age to autorenew an engine for your company.
         * @param months The new months between autorenew.
         * @returns True if autorenew months has been modified.
         */
        public static bool SetAutoRenewMonths(int months) { throw null; }

        /**
         * Return the number of months before/after max age to autorenew an engine for a company.
         * @param company The company to get the autorenew months of.
         * @returns The months before/after max age of engine.
         */
        public static int GetAutoRenewMonths(CompanyID company) { throw null; }

        /**
         * Set the minimum money needed to autorenew an engine for your company.
         * @param money The new minimum required money for autorenew to work.
         * @returns True if autorenew money has been modified.
         */
        public static bool SetAutoRenewMoney(long money) { throw null; }

        /**
         * Return the minimum money needed to autorenew an engine for a company.
         * @param company The company to get the autorenew money of.
         * @returns The minimum required money for autorenew to work.
         */
        public static long GetAutoRenewMoney(CompanyID company) { throw null; }
    }
}