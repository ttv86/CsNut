using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTTD.Testing
{
    internal static class AIRoad
    {
        internal static Func<TileIndex, bool> IsRoadTile { get; set; }
    }
}
