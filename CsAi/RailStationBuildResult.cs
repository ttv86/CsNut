using OpenTTD;

namespace CsAi
{
    public class RailStationBuildResult
    {
        public StationID StationID;

        public TileIndex ExitCloser;

        public TileIndex ExitFarther;

        public TileIndex EntryCloser;

        public TileIndex EntryFarther;

        public TileIndex DepotTile;
    }
}