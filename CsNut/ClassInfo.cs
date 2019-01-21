using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CsNut
{
    internal class ClassInfo
    {
        public MemberDeclarationSyntax Declaration { get; set; }

        public INamedTypeSymbol Symbol { get; set; }

        public SemanticModel SemanticModel { get; set; }
    }
}
