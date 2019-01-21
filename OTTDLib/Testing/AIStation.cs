using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTTD.Testing
{
    internal static class AIStation
    {
        internal static Func<StationID, bool> IsValidStation { get; set; }
    }
}
