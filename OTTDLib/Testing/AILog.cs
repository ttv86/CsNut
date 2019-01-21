using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTTD.Testing
{
    internal static class AILog
    {
        internal static Action<string> Info { get; set; }

        internal static Action<string> Warning { get; set; }

        internal static Action<string> Error { get; set; }
    }
}
