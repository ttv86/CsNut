using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CsNut
{
    public enum PropertyMode
    {
        Undefined,
        Read,
        Write,
        Both
    }

    public class CompilationContext
    {
        private readonly IEnumerator<string> nameGenerator = Utilities.CreateNameGenerator().GetEnumerator();
        private readonly Dictionary<string, string> createdNames = new Dictionary<string, string>();
        private readonly Dictionary<string, string> builtInNames = new Dictionary<string, string>();
        private readonly Dictionary<string, string> generatedBuiltins = new Dictionary<string, string>();

        public bool Uglify { get; internal set; }

        private Stack<PropertyMode> propertyModes = new Stack<PropertyMode>();

        public CompilationContext()
        {
        }

        public void MarkAsAnonymous(INamedTypeSymbol symbol)
        {
            createdNames[LibraryHelper.GetFullName(symbol)] = string.Empty;
        }

        public void PushPropertyMode(PropertyMode mode)
        {
            this.propertyModes.Push(mode);
        }

        public void PopPropertyMode()
        {
            this.propertyModes.Pop();
        }

        public PropertyMode PropertyMode => this.propertyModes.Count > 0 ? this.propertyModes.Peek() : PropertyMode.Read;

        public IAssemblySymbol MainAssembly { get; internal set; }

        public IAssemblySymbol LibraryAssembly { get; internal set; }

        internal string GetName(ISymbol symbol, string defaultValue)
        {
            string fullName = this.GetFullName(symbol, out string prefix);

            if (fullName != null)
            {
                if (fullName.EndsWith(".ToString"))
                {
                    return "tostring";
                }

                if (createdNames.TryGetValue(fullName, out string result))
                {
                    return result;
                }
            }

            return prefix + defaultValue;
        }

        private string GetFullName(ISymbol symbol, out string prefix)
        {
            prefix = string.Empty;
            if (symbol is IMethodSymbol methodSymbol)
            {
                return LibraryHelper.GetFullName(methodSymbol);
            }
            else if (symbol is IFieldSymbol fieldSymbol)
            {
                return LibraryHelper.GetFullName(fieldSymbol);
            }
            else if (symbol is INamedTypeSymbol namedTypeSymbol)
            {
                return LibraryHelper.GetFullName(namedTypeSymbol);
            }
            else if (symbol is ILocalSymbol localSymbol)
            {
                return LibraryHelper.GetFullName(localSymbol);
            }
            else if (symbol is IParameterSymbol parameterSymbol)
            {
                return LibraryHelper.GetFullName(parameterSymbol);
            }
            else if (symbol is IPropertySymbol propertySymbol)
            {
                var mode = this.PropertyMode;
                prefix = mode == PropertyMode.Write ? "set_" : "get_";
                return LibraryHelper.GetFullName(propertySymbol, prefix);
            }
            else if (symbol != null)
            {
                string interfaces = string.Join(",", symbol.GetType().GetInterfaces().Where(x => x.IsPublic).Select(x => x.Name));
                throw new NotImplementedException("Unknwon symbol: " + interfaces);
            }

            return null;
        }

        internal void AddToScript(StringBuilder text)
        {
            foreach (string item in this.generatedBuiltins.Values)
            {
                text.Append(item);
                if (!this.Uglify)
                {
                    text.AppendLine();
                }
            }
        }

        private string GenerateBuiltInScripts(string fullName)
        {
            if (this.builtInNames.TryGetValue(fullName, out string result))
            {
                return result;
            }
            else if (!generatedBuiltins.ContainsKey(fullName))
            {
                string value;
                if (BuiltInScripts.StaticFuncs.TryGetValue(fullName, out value))
                {
                    string uniqueName = this.GenerateUniqueName();
                    value = value.Replace("%%FUNCNAME%%", uniqueName);
                    this.builtInNames.Add(fullName, uniqueName);
                    generatedBuiltins.Add(fullName, value);
                    return uniqueName;
                }
                else if (BuiltInScripts.InstanceFuncs.TryGetValue(fullName, out value))
                {
                    return value;
                }
            }

            throw new InvalidOperationException();
        }

        public void MakeUglified(ISymbol symbol)
        {
            if (!this.Uglify)
            {
                return;
            }

            string fullName = GetFullName(symbol, out _);

            if (!this.createdNames.ContainsKey(fullName))
            {
                this.nameGenerator.MoveNext();
                this.createdNames.Add(fullName, this.nameGenerator.Current);
            }
        }

        internal string GenerateUniqueName()
        {
            this.nameGenerator.MoveNext();
            return "_uq_" + this.nameGenerator.Current;
        }

        internal bool GetReplacement(ISymbol symbol, out string result)
        {
            if ((symbol.ContainingAssembly != this.MainAssembly) && (symbol.ContainingAssembly != this.LibraryAssembly))
            {
                string fullName = this.GetFullName(symbol, out string prefix);
                if (!string.IsNullOrEmpty(fullName))
                {
                    result = this.GenerateBuiltInScripts(fullName);
                    return true;
                }

                throw new InvalidOperationException();
            }

            result = null;
            return false;
        }
    }
}
