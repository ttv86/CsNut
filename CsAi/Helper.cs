using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTTD;

namespace CsAi
{
    public enum Direction
    {
        None,
        NorthEast,
        NorthWest,
        SouthWest,
        SouthEast,
    }

    public static class Helper
    {
        public static string FormatTile(TileIndex tile)
        {
            return "[" + AIMap.GetTileX(tile) + ", " + AIMap.GetTileY(tile) + "]";
        }

        public static int Alternate(int value)
        {
            if (value % 2 == 0)
            {
                return (value / 2) - value;
            }
            else
            {
                return (value + 2) / 2;
            }
        }

        /// <summary>
        /// Gets a direction of tile2 related to tile1.
        /// </summary>
        /// <param name="tile1"></param>
        /// <param name="tile2"></param>
        /// <returns></returns>
        public static Direction GetDirection(TileIndex tile1, TileIndex tile2)
        {
            if ((tile1 == tile2) || !(AIMap.IsValidTile(tile1) && AIMap.IsValidTile(tile2)))
            {
                return Direction.None;
            }

            int x1 = AIMap.GetTileX(tile1);
            int x2 = AIMap.GetTileX(tile2);
            int y1 = AIMap.GetTileY(tile1);
            int y2 = AIMap.GetTileY(tile2);

            if (y1 == y2)
            {
                // /
                return x1 < x2 ? Direction.SouthWest : Direction.NorthEast;
            }
            else if (x1 == x2)
            {
                // \
                return y1 < y2 ? Direction.SouthEast : Direction.NorthWest;
            }

            return Direction.None;
        }

        public static string FormatDirection(Direction dir)
        {
            switch (dir)
            {
                case Direction.NorthEast:
                    return "NorthEast";
                case Direction.NorthWest:
                    return "NorthWest";
                case Direction.SouthWest:
                    return "SouthWest";
                case Direction.SouthEast:
                    return "SouthEast";
                default:
                    return "None";
            }
        }

        public static string Serialize(object obj)
        {
            var result = "";
            switch (Squirrel.TypeOf(obj)) {
                case "null":
                    result += "null";
                    break;
                case "integer":
                case "float":
                case "bool":
                    result += obj;
                    break;
                case "string":
                    result += "\"" + obj + "\"";
                    break;
                case "table":
                    result += "{";
                    foreach (var (v1,v2) in (IEnumerable<(string, object)>)obj) {
                        result += v1 + ":" + Serialize(v2) + ",";
                    }
                    result += "}";
                    break;
                case "array":
                    result += "[";
                    foreach (var v1 in (IEnumerable<object>)obj)
                    {
                        result += Serialize(v1) + ",";
                    }
                    result += "]";
                    break;
                case "function":
                    result += "function()";
                    break;
                case "instance":
                    result += "instance";
                    break;
                default:
                    result += "unknown type: " + Squirrel.TypeOf(obj);
                    break;
            }
            return result;
        }
    }
}
