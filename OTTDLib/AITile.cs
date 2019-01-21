namespace OpenTTD
{
    /// <summary>Class that handles all tile related functions.</summary>
    public static class AITile
    {
        /// <summary>Error messages related to modifying tiles.</summary>
        /// <summary>Base for tile related errors </summary>
        public static readonly ErrorMessage ERR_TILE_BASE;

        /// <summary>Tile can't be raised any higher </summary>
        public static readonly ErrorMessage ERR_TILE_TOO_HIGH;

        /// <summary>Tile can't be lowered any lower </summary>
        public static readonly ErrorMessage ERR_TILE_TOO_LOW;

        /// <summary>The area was already flat </summary>
        public static readonly ErrorMessage ERR_AREA_ALREADY_FLAT;

        /// <summary>There is a tunnel underneed </summary>
        public static readonly ErrorMessage ERR_EXCAVATION_WOULD_DAMAGE;

        /// <summary>Enumeration for corners of tiles.</summary>
        public static readonly Corner CORNER_W;
        public static readonly Corner CORNER_S;
        public static readonly Corner CORNER_E;
        public static readonly Corner CORNER_N;
        public static readonly Corner CORNER_INVALID;

        /**
         * Enumeration for the slope-type.
         *
         * This enumeration use the chars N, E, S, W corresponding the
         *  direction North, East, South and West. The top corner of a tile
         *  is the north-part of the tile.
         */
        public static readonly Slope SLOPE_FLAT;
        public static readonly Slope SLOPE_W;
        public static readonly Slope SLOPE_S;
        public static readonly Slope SLOPE_E;
        public static readonly Slope SLOPE_N;
        public static readonly Slope SLOPE_STEEP;
        public static readonly Slope SLOPE_NW;
        public static readonly Slope SLOPE_SW;
        public static readonly Slope SLOPE_SE;
        public static readonly Slope SLOPE_NE;
        public static readonly Slope SLOPE_EW;
        public static readonly Slope SLOPE_NS;
        public static readonly Slope SLOPE_ELEVATED;
        public static readonly Slope SLOPE_NWS;
        public static readonly Slope SLOPE_WSE;
        public static readonly Slope SLOPE_SEN;
        public static readonly Slope SLOPE_ENW;
        public static readonly Slope SLOPE_STEEP_W;
        public static readonly Slope SLOPE_STEEP_S;
        public static readonly Slope SLOPE_STEEP_E;
        public static readonly Slope SLOPE_STEEP_N;
        public static readonly Slope SLOPE_INVALID;

        /// <summary>The different transport types a tile can have.</summary>
        public static readonly TransportType TRANSPORT_RAIL;
        public static readonly TransportType TRANSPORT_ROAD;
        public static readonly TransportType TRANSPORT_WATER;
        public static readonly TransportType TRANSPORT_AIR;
        public static readonly TransportType TRANSPORT_INVALID;

        /// <summary>Get the base cost for building/clearing several things.</summary>
        public static readonly BuildType BT_FOUNDATION;
        public static readonly BuildType BT_TERRAFORM;
        public static readonly BuildType BT_BUILD_TREES;
        public static readonly BuildType BT_CLEAR_GRASS;
        public static readonly BuildType BT_CLEAR_ROUGH;
        public static readonly BuildType BT_CLEAR_ROCKY;
        public static readonly BuildType BT_CLEAR_FIELDS;
        public static readonly BuildType BT_CLEAR_HOUSE;

        /**
         * The types of terrain a tile can have.
         *
         * @note When a desert or rainforest tile are changed, their terrain type will remain the same. In other words, a sea tile can be of the desert terrain type.
         * @note The snow terrain type can change to the normal terrain type and vice versa based on landscaping or variable snow lines from NewGRFs.
         */
        public static readonly TerrainType TERRAIN_NORMAL;
        public static readonly TerrainType TERRAIN_DESERT;
        public static readonly TerrainType TERRAIN_RAINFOREST;
        public static readonly TerrainType TERRAIN_SNOW;

        /**
         * Check if this tile is buildable, i.e. no things on it that needs
         *  demolishing.
         * @param tile The tile to check on.
         * @returns True if it is buildable, false if not.
         * @note For trams you also might want to check for AIRoad.IsRoad(),
         *   as you can build tram-rails on road-tiles.
         * @note For rail you also might want to check for AIRoad.IsRoad(),
         *   as in some cases you can build rails on road-tiles.
         */
        public static bool IsBuildable(TileIndex tile) { return Testing.AITile.IsBuildable(tile); }

        /**
         * Check if this tile is buildable in a rectangle around a tile, with the
         *  entry in the list as top-left.
         * @param tile The tile to check on.
         * @param width The width of the rectangle.
         * @param height The height of the rectangle.
         * @returns True if it is buildable, false if not.
         */
        public static bool IsBuildableRectangle(TileIndex tile, int width, int height) { throw null; }

        /**
         * Checks whether the given tile is actually a water tile.
         * @param tile The tile to check on.
         * @returns True if and only if the tile is a water tile.
         */
        public static bool IsWaterTile(TileIndex tile) { return Testing.AITile.IsWaterTile(tile); }

        /**
         * Checks whether the given tile is actually a coast tile.
         * @param tile The tile to check.
         * @returns True if and only if the tile is a coast tile.
         * @note Building on coast tiles in general is more expensive. This is not
         *  true if there are also trees on the tile, see 
         */
        public static bool IsCoastTile(TileIndex tile) { return Testing.AITile.IsCoastTile(tile); }

        /**
         * Checks whether the given tile is a station tile of any station.
         * @param tile The tile to check.
         * @returns True if and only if the tile is a station tile.
         */
        public static bool IsStationTile(TileIndex tile) { return Testing.AITile.IsStationTile(tile); }

        /**
         * Check if a tile has a steep slope.
         * Steep slopes are slopes with a height difference of 2 across one diagonal of the tile.
         * @param slope The slope to check on.
         * @returns True if the slope is a steep slope.
         */
        public static bool IsSteepSlope(Slope slope) { return Testing.AITile.IsSteepSlope(slope); }

        /**
         * Check if a tile has a halftile slope.
         * Halftile slopes appear on top of halftile foundations. E.g. the slope you get when building a horizontal railtrack on the top of a SLOPE_N or SLOPE_STEEP_N.
         * @param slope The slope to check on.
         * @returns True if the slope is a halftile slope.
         * @note Currently there is no API function that would return or accept a halftile slope.
         */
        public static bool IsHalftileSlope(Slope slope) { return Testing.AITile.IsHalftileSlope(slope); }

        /**
         * Check if the tile has any tree on it.
         * @param tile The tile to check on.
         * @returns True if and only if there is a tree on the tile.
         */
        public static bool HasTreeOnTile(TileIndex tile) { return Testing.AITile.HasTreeOnTile(tile); }

        /**
         * Check if the tile is a farmland tile.
         * @param tile The tile to check on.
         * @returns True if and only if the tile is farmland.
         */
        public static bool IsFarmTile(TileIndex tile) { return Testing.AITile.IsFarmTile(tile); }

        /**
         * Check if the tile is a rock tile.
         * @param tile The tile to check on.
         * @returns True if and only if the tile is rock tile.
         */
        public static bool IsRockTile(TileIndex tile) { return Testing.AITile.IsRockTile(tile); }

        /**
         * Check if the tile is a rough tile.
         * @param tile The tile to check on.
         * @returns True if and only if the tile is rough tile.
         */
        public static bool IsRoughTile(TileIndex tile) { return Testing.AITile.IsRoughTile(tile); }

        /**
         * Check if the tile without buildings or infrastructure is a snow tile.
         * @note If you want to know if a tile (with or without buildings and infrastructure) is on or above the snowline, use AITile.GetTerrainType(tile).
         * @param tile The tile to check on.
         * @returns True if and only if the tile is snow tile.
         */
        public static bool IsSnowTile(TileIndex tile) { return Testing.AITile.IsSnowTile(tile); }

        /**
         * Check if the tile without buildings or infrastructure is a desert tile.
         * @note If you want to know if a tile (with or without buildings and infrastructure) is in a desert, use AITile.GetTerrainType(tile).
         * @param tile The tile to check on.
         * @returns True if and only if the tile is desert tile.
         */
        public static bool IsDesertTile(TileIndex tile) { return Testing.AITile.IsDesertTile(tile); }

        /**
         * Get the type of terrain regardless of buildings or infrastructure.
         * @note When a desert or rainforest tile are changed, their terrain type will remain the same. In other words, a sea tile can be of the desert terrain type.
         * @note The snow terrain type can change to the normal terrain type and vice versa based on landscaping or variable snow lines from NewGRFs.
         * @param tile The tile to check on.
         * @returns The 
         */
        public static TerrainType GetTerrainType(TileIndex tile) { return Testing.AITile.GetTerrainType(tile); }

        /**
         * Get the slope of a tile.
         * This is the slope of the bare tile. A possible foundation on the tile does not influence this slope.
         * @param tile The tile to check on.
         * @returns Bit mask encoding the slope. See 
         */
        public static Slope GetSlope(TileIndex tile) { return Testing.AITile.GetSlope(tile); }

        /**
         * Get the complement of the slope.
         * @param slope The slope to get the complement of.
         * @returns The complement of a slope. This means that all corners that
         *  weren't raised, are raised, and visa versa.
         */
        public static Slope GetComplementSlope(Slope slope) { throw null; }

        /**
         * Get the minimal height on a tile.
         * The returned height is the height of the bare tile. A possible foundation on the tile does not influence this height.
         * @param tile The tile to check on.
         * @returns The height of the lowest corner of the tile, ranging from 0 to 15.
         */
        public static int GetMinHeight(TileIndex tile) { throw null; }

        /**
         * Get the maximal height on a tile.
         * The returned height is the height of the bare tile. A possible foundation on the tile does not influence this height.
         * @param tile The tile to check on.
         * @returns The height of the highest corner of the tile, ranging from 0 to 15.
         */
        public static int GetMaxHeight(TileIndex tile) { throw null; }

        /**
         * Get the height of a certain corner of a tile.
         * The returned height is the height of the bare tile. A possible foundation on the tile does not influence this height.
         * @param tile The tile to check on.
         * @param corner The corner to query.
         * @returns The height of the lowest corner of the tile, ranging from 0 to 15.
         */
        public static int GetCornerHeight(TileIndex tile, Corner corner) { throw null; }

        /**
         * Get the owner of the tile.
         * @param tile The tile to get the owner from.
         * @returns The CompanyID of the owner of the tile, or COMPANY_INVALID if
         *  there is no owner (grass/industry/water tiles, etc.).
         */
        public static CompanyID GetOwner(TileIndex tile) { throw null; }

        /**
         * Checks whether the given tile contains parts suitable for the given
         *  TransportType.
         * @param tile The tile to check.
         * @param transport_type The TransportType to check against.
         * @note Returns false on tiles with roadworks and on road tiles with only
         *       a single piece of road as these tiles cannot be used to transport
         *       anything on. It furthermore returns true on some coast tile for
         *       TRANSPORT_WATER because ships can navigate over them.
         * @note Use ScriptAirport.IsAirportTile to check for airport tiles. Aircraft
         *       can fly over every tile on the map so using HasTransportType
         *       doesn't make sense for TRANSPORT_AIR.
         * @returns True if and only if the tile has the given TransportType.
         */
        public static bool HasTransportType(TileIndex tile, TransportType transport_type) { throw null; }

        /**
         * Check how much cargo this tile accepts.
         *  It creates a radius around the tile, and adds up all acceptance of this
         *   cargo.
         * @param tile The tile to check on.
         * @param cargo_type The cargo to check the acceptance of.
         * @param width The width of the station.
         * @param height The height of the station.
         * @param radius The radius of the station.
         * @returns Value below 8 means no acceptance; the more the better.
         */
        public static int GetCargoAcceptance(TileIndex tile, CargoID cargo_type, int width, int height, int radius) { throw null; }

        /**
         * Checks how many producers in the radius produces this cargo.
         *  It creates a radius around the tile, and counts all producer of this cargo.
         * @param tile The tile to check on.
         * @param cargo_type The cargo to check the production of.
         * @param width The width of the station.
         * @param height The height of the station.
         * @param radius The radius of the station.
         * @returns The number of producers that produce this cargo within radius of the tile.
         */
        public static int GetCargoProduction(TileIndex tile, CargoID cargo_type, int width, int height, int radius) { throw null; }

        /**
         * Get the manhattan distance from the tile to the tile.
         * @param tile_from The tile to get the distance to.
         * @param tile_to The tile to get the distance to.
         * @returns The distance between the two tiles.
         */
        public static int GetDistanceManhattanToTile(TileIndex tile_from, TileIndex tile_to) { throw null; }

        /**
         * Get the square distance from the tile to the tile.
         * @param tile_from The tile to get the distance to.
         * @param tile_to The tile to get the distance to.
         * @returns The distance between the two tiles.
         */
        public static int GetDistanceSquareToTile(TileIndex tile_from, TileIndex tile_to) { throw null; }

        /**
         * Raise the given corners of the tile. The corners can be combined,
         *  for SLOPE_N example | SLOPE_W (= SLOPE_NW) will raise the west and the north corner.
         * @note The corners will be modified in the order west (first), south, east, north (last).
         *       Changing one corner might cause another corner to be changed too. So modifiing
         *       multiple corners may result in changing some corners by multiple steps.
         * @param tile The tile to raise.
         * @param slope Corners to raise (SLOPE_xxx).
         * @exception AIError.ERR_AREA_NOT_CLEAR
         * @exception AIError.ERR_TOO_CLOSE_TO_EDGE
         * @exception AITile.ERR_TILE_TOO_HIGH
         * @returns 0 means failed, 1 means success.
         */
        public static bool RaiseTile(TileIndex tile, int slope) { throw null; }

        /**
         * Lower the given corners of the tile. The corners can be combined,
         *  for SLOPE_N example | SLOPE_W (= SLOPE_NW) will lower the west and the north corner.
         * @note The corners will be modified in the order west (first), south, east, north (last).
         *       Changing one corner might cause another corner to be changed too. So modifiing
         *       multiple corners may result in changing some corners by multiple steps.
         * @param tile The tile to lower.
         * @param slope Corners to lower (SLOPE_xxx).
         * @exception AIError.ERR_AREA_NOT_CLEAR
         * @exception AIError.ERR_TOO_CLOSE_TO_EDGE
         * @exception AITile.ERR_TILE_TOO_LOW
         * @returns 0 means failed, 1 means success.
         */
        public static bool LowerTile(TileIndex tile, int slope) { throw null; }

        /**
         * Level all tiles in the rectangle between start_tile and end_tile so they
         *  are at the same height. All tiles will be raised or lowered until
         *  they are at height AITile.GetCornerHeight(start_tile, AITile.CORNER_N).
         * @param start_tile One corner of the rectangle to level.
         * @param end_tile The opposite corner of the rectangle.
         * @exception AIError.ERR_AREA_NOT_CLEAR
         * @exception AIError.ERR_TOO_CLOSE_TO_EDGE
         * @returns True if one or more tiles were leveled.
         * @note Even if leveling some part fails, some other part may have been
         *  successfully leveled already.
         * @note This function may return true in ScriptTestMode, although it fails in
         *  ScriptExecMode.
         */
        public static bool LevelTiles(TileIndex start_tile, TileIndex end_tile) { throw null; }

        /**
         * Destroy everything on the given tile.
         * @param tile The tile to demolish.
         * @exception AIError.ERR_AREA_NOT_CLEAR
         * @returns True if and only if the tile was demolished.
         */
        public static bool DemolishTile(TileIndex tile) { throw null; }

        /**
         * Create a random tree on a tile.
         * @param tile The tile to build a tree on.
         * @returns True if and only if a tree was added on the tile.
         */
        public static bool PlantTree(TileIndex tile) { throw null; }

        /**
         * Create a random tree on a rectangle of tiles.
         * @param tile The top left tile of the rectangle.
         * @param width The width of the rectangle.
         * @param height The height of the rectangle.
         * @returns True if and only if a tree was added on any of the tiles in the rectangle.
         */
        public static bool PlantTreeRectangle(TileIndex tile, int width, int height) { throw null; }

        /**
         * Find out if this tile is within the rating influence of a town.
         *  If a station sign would be on this tile, the servicing quality of the station would
         *  influence the rating of the town.
         * @param tile The tile to check.
         * @param town_id The town to check.
         * @returns True if the tile is within the rating influence of the town.
         */
        public static bool IsWithinTownInfluence(TileIndex tile, TownID town_id) { throw null; }

        /**
         * Find the town which has authority for the tile.
         *  The rating of your company in this town will be checked and affected when
         *  building stations, trees etc.
         * @param tile The tile to check.
         * @returns The TownID of the town which has authority on this tile.
         */
        public static TownID GetTownAuthority(TileIndex tile) { throw null; }

        /**
         * Find the town that is closest to a tile. Stations you build at this tile
         *  will belong to this town.
         * @param tile The tile to check.
         * @returns The TownID of the town closest to the tile.
         */
        public static TownID GetClosestTown(TileIndex tile) { throw null; }

        /**
         * Get the baseprice of building/clearing various tile-related things.
         * @param build_type the type to build
         * @returns The baseprice of building or removing the given object.
         */
        public static long GetBuildCost(BuildType build_type) { throw null; }
    }
}