namespace OpenTTD
{
    /// <summary>Creates a list of industries that are currently on the map.</summary>
    public class AIIndustryList : AIList<IndustryID>
    {
        public AIIndustryList() { throw null; }
    }

    /// <summary>Creates a list of industries that accepts a given cargo.</summary>
    public class AIIndustryList_CargoAccepting : AIList<IndustryID>
    {
        /// <param name="cargo_id">The cargo this industry should accept.</param>
        public AIIndustryList_CargoAccepting(CargoID cargo_id) { throw null; }
    }

    /**
     * Creates a list of industries that can produce a given cargo.
     * @note It also contains industries that currently produces 0 units of the cargo.
     */
    public class AIIndustryList_CargoProducing : AIList<IndustryID>
    {
        /// <param name="cargo_id">The cargo this industry should produce.</param>
        public AIIndustryList_CargoProducing(CargoID cargo_id) { throw null; }
    }
}