using System;
using Microsoft.CodeAnalysis;

namespace CsNut
{
    public class UnsupportedSyntaxException : Exception
    {
        public UnsupportedSyntaxException(string message, SyntaxNode node)
            : base(message)
        {
            this.SyntaxNode = node;
        }

        public SyntaxNode SyntaxNode { get; }
    }
}
