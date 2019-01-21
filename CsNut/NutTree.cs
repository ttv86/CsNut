using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static CsNut.LibraryHelper;

namespace CsNut
{
    internal partial class NutTree
    {
        private readonly Dictionary<string, ClassInfo> classDictionary = new Dictionary<string, ClassInfo>();
        private readonly StringBuilder infoText = new StringBuilder();
        private readonly StringBuilder mainText = new StringBuilder();

        public NutTree()
        {
        }

        internal string GetInfoText() => this.infoText.ToString();

        internal string GetMainText() => this.mainText.ToString();

        internal void GenerateCode(INamedTypeSymbol entryPoint)
        {
            var context = new CompilationContext();
            context.MainAssembly = entryPoint.ContainingAssembly;
            context.LibraryAssembly = entryPoint.BaseType.ContainingAssembly;
            context.Uglify = false;
            foreach (var declaration in this.classDictionary.Values)
            {
                context.MakeUglified(declaration.Symbol);
                if (declaration.Symbol is INamedTypeSymbol typeSymbol)
                {
                    bool hasAnythingButFields = false;
                    foreach (var member in typeSymbol.GetMembers())
                    {
                        context.MakeUglified(member);
                        if (member is IMethodSymbol methodSymbol)
                        {
                            if (methodSymbol.DeclaringSyntaxReferences.Length > 0)
                            {
                                hasAnythingButFields = true;
                            }
                            else
                            {
                                // Auto-generated.
                            }
                        }
                        else if (!(member is IFieldSymbol))
                        {
                            hasAnythingButFields = true;
                        }
                    }

                    if ((typeSymbol.TypeKind == TypeKind.Class) && !hasAnythingButFields)
                    {
                        context.MarkAsAnonymous(declaration.Symbol);
                    }
                }

                if (declaration.Declaration is ClassDeclarationSyntax classDeclarationSyntax)
                {
                    foreach (var local in classDeclarationSyntax.DescendantNodes().OfType<VariableDeclaratorSyntax>())
                    {
                        var symbol = declaration.SemanticModel.GetDeclaredSymbol(local);
                        context.MakeUglified(symbol);
                    }
                }
            }
            
            var entry = this.classDictionary[GetFullName(entryPoint)];
            this.mainText.Append(@"import(""queue.fibonacci_heap"", ""FibonacciHeap"", 2);");
            if (!context.Uglify)
            {
                this.mainText.AppendLine();
            }

            GenerateInfo(entryPoint, context);

            foreach (var declaration in this.classDictionary.Values)
            {
                NodeWriter writer = new NodeWriter(declaration.SemanticModel, mainText, context);
                if (declaration.Declaration is EnumDeclarationSyntax enumDeclarationSyntax)
                {
                    writer.Write(enumDeclarationSyntax, declaration.Symbol);
                }
            }

            foreach (var declaration in this.classDictionary.Values)
            {
                NodeWriter writer = new NodeWriter(declaration.SemanticModel, mainText, context);
                if (declaration.Declaration is ClassDeclarationSyntax classDeclarationSyntax)
                {
                    writer.Write(classDeclarationSyntax, declaration.Symbol);
                }
            }

            context.AddToScript(mainText);
            var entryNs = Utilities.GetValue(entryPoint.ContainingNamespace, true);
            if (!string.IsNullOrEmpty(entryNs))
            {
                mainText.Append($"class {entryPoint.Name} extends {entryNs}.{entryPoint.Name}{{}}");
            }
        }

        private void GenerateInfo(INamedTypeSymbol entryPoint, CompilationContext context)
        {
            string author = entryPoint.Name;
            string name = entryPoint.Name;
            string shortName = (entryPoint.Name + "____").Substring(0, 4);
            string description = string.Empty;
            int version = 1;
            int? minVersionToLoad = null;
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            bool? useAsRandomAi = null;
            string apiVersion = "1.8";
            string url = null;

            foreach (var attribute in entryPoint.GetAttributes())
            {
                if (Is(attribute.AttributeClass, AuthorAttibuteType))
                {
                    author = (string)attribute.ConstructorArguments[0].Value;
                }
                else if (Is(attribute.AttributeClass, NameAttributeType))
                {
                    name = (string)attribute.ConstructorArguments[0].Value;
                }
                else if (Is(attribute.AttributeClass, ShortNameAttributeType))
                {
                    shortName = (string)attribute.ConstructorArguments[0].Value;
                }
                else if (Is(attribute.AttributeClass, DescriptionAttributeType))
                {
                    description = (string)attribute.ConstructorArguments[0].Value;
                }
                else if (Is(attribute.AttributeClass, VersionAttributeType))
                {
                    version = (int)attribute.ConstructorArguments[0].Value;
                }
                else if (Is(attribute.AttributeClass, MinVersionToLoadAttributeType))
                {
                    minVersionToLoad = (int)attribute.ConstructorArguments[0].Value;
                }
                else if (Is(attribute.AttributeClass, DateAttributeType))
                {
                    date = (string)attribute.ConstructorArguments[0].Value;
                }
                else if (Is(attribute.AttributeClass, UseAsRandomAIAttributeType))
                {
                    useAsRandomAi = (bool)attribute.ConstructorArguments[0].Value;
                }
                else if (Is(attribute.AttributeClass, APIVersionAttributeType))
                {
                    apiVersion = (string)attribute.ConstructorArguments[0].Value;
                }
                else if (Is(attribute.AttributeClass, URLAttributeType))
                {
                    url = (string)attribute.ConstructorArguments[0].Value;
                }
                else
                {
                    Debug.WriteLine("Unknown attribute: " + attribute.AttributeClass.Name);
                }
            }

            if ((shortName == null) || (shortName.Length != 4))
            {
                throw new InvalidOperationException("ShortName must be 4 characters.");
            }

            infoText.AppendFormat("class {0} extends AIInfo", entryPoint.Name);
            infoText.Append("{");
            string instanceName = context.GetName(entryPoint, entryPoint.Name);
            infoText.AppendFormat("function CreateInstance(){{return{0};}}", Utilities.WriteLiteral(instanceName, true));
            infoText.AppendFormat("function GetAuthor(){{return{0};}}", Utilities.WriteLiteral(author, true));
            infoText.AppendFormat("function GetName(){{return{0};}}", Utilities.WriteLiteral(name, true));
            infoText.AppendFormat("function GetShortName(){{return{0};}}", Utilities.WriteLiteral(shortName, true));
            infoText.AppendFormat("function GetDescription(){{return{0};}}", Utilities.WriteLiteral(description, true));
            infoText.AppendFormat("function GetVersion(){{return{0};}}", Utilities.WriteLiteral(version, true));

            if (minVersionToLoad.HasValue)
            {
                infoText.AppendFormat("function MinVersionToLoad(){{return{0};}}", Utilities.WriteLiteral(minVersionToLoad.Value));
            }

            if (date != null)
            {
                infoText.AppendFormat("function GetDate(){{return{0};}}", Utilities.WriteLiteral(date));
            }

            if (useAsRandomAi.HasValue)
            {
                infoText.AppendFormat("function UseAsRandomAI(){{return{0};}}", Utilities.WriteLiteral(useAsRandomAi.Value));
            }

            if (apiVersion != null)
            {
                infoText.AppendFormat("function GetAPIVersion(){{return{0};}}", Utilities.WriteLiteral(apiVersion));
            }

            if (url != null)
            {
                infoText.AppendFormat("function GetURL(){{return{0};}}", Utilities.WriteLiteral(url));
            }

            infoText.Append("}RegisterAI(");
            infoText.Append(entryPoint.Name);
            infoText.Append("());");
        }

        internal void AddType(MemberDeclarationSyntax declaration, INamedTypeSymbol typeSymbol, SemanticModel model)
        {
            this.classDictionary.Add(GetFullName(typeSymbol), new ClassInfo() { Declaration = declaration, Symbol = typeSymbol, SemanticModel = model });
        }
    }
}
