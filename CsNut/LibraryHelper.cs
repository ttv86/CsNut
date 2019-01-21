using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

namespace CsNut
{
    internal static class LibraryHelper
    {
        internal static readonly string AIControllerType = GetFullName(typeof(OpenTTD.AIController));

        internal static readonly string AuthorAttibuteType = GetFullName(typeof(OpenTTD.AuthorAttribute));
        internal static readonly string NameAttributeType = GetFullName(typeof(OpenTTD.NameAttribute));
        internal static readonly string ShortNameAttributeType = GetFullName(typeof(OpenTTD.ShortNameAttribute));
        internal static readonly string DescriptionAttributeType = GetFullName(typeof(OpenTTD.DescriptionAttribute));
        internal static readonly string VersionAttributeType = GetFullName(typeof(OpenTTD.VersionAttribute));
        internal static readonly string MinVersionToLoadAttributeType = GetFullName(typeof(OpenTTD.MinVersionToLoadAttribute));
        internal static readonly string DateAttributeType = GetFullName(typeof(OpenTTD.DateAttribute));
        internal static readonly string UseAsRandomAIAttributeType = GetFullName(typeof(OpenTTD.UseAsRandomAIAttribute));
        internal static readonly string APIVersionAttributeType = GetFullName(typeof(OpenTTD.APIVersionAttribute));
        internal static readonly string URLAttributeType = GetFullName(typeof(OpenTTD.URLAttribute));

        internal static bool IsBaseClass(INamedTypeSymbol symbol)
        {
            return symbol.ContainingAssembly.Name == "OTTDLib";
        }

        internal static bool Is(INamedTypeSymbol symbol, string name)
        {
            return IsBaseClass(symbol) && GetFullName(symbol) == name;
        }

        internal static string GetFullName(INamedTypeSymbol typeSymbol)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(typeSymbol.ContainingAssembly.Name);
            sb.Append("!");
            List<INamespaceSymbol> namespaceList = new List<INamespaceSymbol>();
            INamespaceSymbol namespaceSymbol = typeSymbol.ContainingNamespace;
            while (namespaceSymbol != null)
            {
                namespaceList.Add(namespaceSymbol);
                namespaceSymbol = namespaceSymbol.ContainingNamespace;
            }

            for (int i = namespaceList.Count - 1; i >= 0; i--)
            {
                if (!string.IsNullOrEmpty(namespaceList[i].Name))
                {
                    sb.Append(namespaceList[i].Name);
                    sb.Append(".");
                }
            }

            sb.Append(typeSymbol.Name);
            return sb.ToString();
        }

        internal static string GetFullName(IMethodSymbol methodSymbol)
        {
            StringBuilder sb = new StringBuilder();
            if (methodSymbol.ReceiverType is INamedTypeSymbol namedTypeSymbol)
            {
                sb.Append(GetFullName(namedTypeSymbol));
                sb.Append(".");
            }

            sb.Append(methodSymbol.Name);
            return sb.ToString();
        }

        internal static string GetFullName(IFieldSymbol fieldSymbol)
        {
            StringBuilder sb = new StringBuilder();
            if (fieldSymbol.ContainingType is INamedTypeSymbol namedTypeSymbol)
            {
                sb.Append(GetFullName(namedTypeSymbol));
                sb.Append(".");
            }

            sb.Append(fieldSymbol.Name);
            return sb.ToString();
        }

        internal static string GetFullName(ILocalSymbol localSymbol)
        {
            StringBuilder sb = new StringBuilder();
            if (localSymbol.ContainingSymbol is IMethodSymbol methodSymbol)
            {
                sb.Append(GetFullName(methodSymbol));
                sb.Append("#");
            }

            sb.Append(localSymbol.Name);
            return sb.ToString();
        }

        internal static string GetFullName(IParameterSymbol parameterSymbol)
        {
            StringBuilder sb = new StringBuilder();
            if (parameterSymbol.ContainingSymbol is IMethodSymbol methodSymbol)
            {
                sb.Append(GetFullName(methodSymbol));
                sb.Append("#");
            }

            sb.Append(parameterSymbol.Name);
            return sb.ToString();
        }

        internal static string GetFullName(IPropertySymbol propertySymbol, string prepfix)
        {
            StringBuilder sb = new StringBuilder();
            if (propertySymbol.ContainingType is INamedTypeSymbol namedTypeSymbol)
            {
                sb.Append(GetFullName(namedTypeSymbol));
                sb.Append(".");
            }

            sb.Append(prepfix);
            sb.Append(propertySymbol.Name);
            return sb.ToString();
        }

        internal static string GetFullName(Type type)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(type.Assembly.GetName().Name);
            sb.Append("!");
            sb.Append(type.Namespace);
            sb.Append(".");
            sb.Append(type.Name);
            return sb.ToString();
        }
    }
}
