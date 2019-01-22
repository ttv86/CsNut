using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace CsNut
{
    public class ClosureInfo
    {
        public ClosureInfo(string closureName)
        {
            this.Name = closureName;
        }

        public string Name { get; }

        public HashSet<IMethodSymbol> Methods { get; } = new HashSet<IMethodSymbol>();

        public HashSet<ILocalSymbol> Locals { get; } = new HashSet<ILocalSymbol>();
    }
}