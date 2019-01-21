namespace OpenTTD
{
    /// <summary>Create a list of bridges types.</summary>
    public class AIBridgeList : AIList<BridgeID>
    {
        public AIBridgeList() { throw null; }
    }

    /// <summary>Create a list of bridges types that can be built on a specific length.</summary>
    public class AIBridgeList_Length : AIList<BridgeID>
    {
        /// <param name="length">The length of the bridge you want to build.</param>
        public AIBridgeList_Length(int length) { throw null; }
    }
}