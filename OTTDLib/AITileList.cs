namespace OpenTTD
{
    /// <summary>Creates an empty list, in which you can add tiles.</summary>
    public class AITileList : AIList<TileIndex>
    {
        /// <summary>Adds the rectangle between tile_from and tile_to to the to-be-evaluated tiles.</summary>
        /// <param name="tile_from">One corner of the tiles to add.</param>
        /// <param name="tile_to">The other corner of the tiles to add.</param>
        void AddRectangle(TileIndex tile_from, TileIndex tile_to) { throw null; }

        /// <summary>Add a tile to the to-be-evaluated tiles.</summary>
        /// <param name="tile">The tile to add.</param>
        void AddTile(TileIndex tile) { throw null; }

        /// <summary>Remove the tiles inside the rectangle between tile_from and tile_to form the list.</summary>
        /// <param name="tile_from">One corner of the tiles to remove.</param>
        /// <param name="tile_to">The other corner of the files to remove.</param>
        void RemoveRectangle(TileIndex tile_from, TileIndex tile_to) { throw null; }

        /// <summary>Remove a tile from the list.</summary>
        /// <param name="tile">The tile to remove.</param>
        void RemoveTile(TileIndex tile) { throw null; }
    }

    /**
     * Creates a list of tiles that will accept cargo for the given industry.
     * @note If a simular industry is close, it might happen that this industry receives the cargo.
     */
    public class AITileList_IndustryAccepting : AITileList
    {
        /**
         * @param industry_id The industry to create the ScriptTileList around.
         * @param radius The coverage radius of the station type you will be using.
         * @note A station part built on any of the returned tiles will give you coverage.
         */
        public AITileList_IndustryAccepting(IndustryID industry_id, int radius) { throw null; }
    }

    /**
     * Creates a list of tiles which the industry checks to see if a station is
     *  there to receive cargo produced by this industry.
     */
    public class AITileList_IndustryProducing : AITileList
    {
        /**
         * @param industry_id The industry to create the ScriptTileList around.
         * @param radius The coverage radius of the station type you will be using.
         * @note A station part built on any of the returned tiles will give you acceptance.
         */
        public AITileList_IndustryProducing(IndustryID industry_id, int radius) { throw null; }
    }

    /**
     * Creates a list of tiles which have the requested StationType of the
     *  StationID.
     */
    public class AITileList_StationType : AITileList
    {
        /// <param name="station_id">The station to create the ScriptTileList for.</summary>
        /// <param name="station_type">The StationType to create the ScriptList for.</param>
        public AITileList_StationType(StationID station_id, StationType station_type) { throw null; }
    }
}