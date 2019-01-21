namespace OpenTTD
{
    /// <summary>Creates a list of cargoes that can be produced in the current game.</summary>
    public class AICargoList : AIList<CargoID>
    {
        public AICargoList() { throw null; }
    }

    /**
     * Creates a list of cargoes that the given industry accepts.
     * @note This list also includes cargoes that are temporarily not accepted
     *   by this industry, @see AIIndustry.IsCargoAccepted.
     */
    public class AICargoList_IndustryAccepting : AIList<CargoID>
    {
        /// <param name="industry_id">The industry to get the list of cargoes it accepts from.</param>
        public AICargoList_IndustryAccepting(IndustryID industry_id) { throw null; }
    }

    /// <summary>Creates a list of cargoes that the given industry can produce.</summary>
    public class AICargoList_IndustryProducing : AIList<CargoID>
    {
        /// <param name="industry_id">The industry to get the list of cargoes it produces from.</param>
        private int ds;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="industry_id"></param>
        public AICargoList_IndustryProducing(IndustryID industry_id) { throw null; }
    }

    /// <summary>Creates a list of cargoes that the given station accepts.</summary>
    public class AICargoList_StationAccepting : AIList<CargoID>
    {
        /// <param name="station_id">The station to get the list of cargoes it accepts from.</param>
        public AICargoList_StationAccepting(StationID station_id) { throw null; }
    }
}