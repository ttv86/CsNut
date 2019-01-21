namespace OpenTTD
{
    /// <summary>
    /// Class that keeps track of the costs, so you can request how much a block of
    /// commands did cost in total. Works in both Execute as in Test mode.
    /// </summary>
    /// <example>
    /// <code>
    ///   {
    ///     local costs = ScriptAccounting();
    ///     BuildRoad(from_here, to_here);
    ///     BuildRoad(from_there, to_there);
    ///     print("Costs for route is: " + costs.GetCosts());
    ///   }
    ///   </code>
    /// </example>
    public class AIAccounting
    {
        /// <summary>
        /// Creating instance of this class starts counting the costs of commands
        /// from zero. Saves the current value of GetCosts so we can return to
        /// the old value when the instance gets deleted.
        /// </summary>
        public AIAccounting() { throw null; }

        /// <summary>
        /// Get the current value of the costs.
        /// </summary>
        /// <returns>The current costs.</returns>
        /// <remarks>When nesting ScriptAccounting instances all instances' GetCosts will always return the value of the 'top' instance.</remarks>
        public long GetCosts() { throw null; }

        /// <summary>
        /// Reset the costs to zero.
        /// </summary>
        /// <remarks>When nesting ScriptAccounting instances all instances' ResetCosts will always effect on the 'top' instance.</remarks>
        public void ResetCosts() { throw null; }
    }
}