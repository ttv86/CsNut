using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTTD;

namespace CsAi
{
    public class Point
    {
        public Point(TileIndex tile)
        {
            this.Tile = tile;
            this.X = AIMap.GetTileX(tile);
            this.Y = AIMap.GetTileY(tile);
        }

        public Point(int x, int y)
        {
            this.Tile = AIMap.GetTileIndex(x, y);
            this.X = x;
            this.Y = Y;
        }

        public int X { get; }

        public int Y { get; }

        public TileIndex Tile { get; }

        internal int Distance(Point other)
        {
            return Math.Abs(this.X - other.X) + Math.Abs(this.Y - other.Y);
        }
    }
}
