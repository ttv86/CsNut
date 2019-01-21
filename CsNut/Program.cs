using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CsNut
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            try
            {
                string inputProject = null;
                string outDir = Path.GetTempPath();

                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] == "--out")
                    {
                        outDir = args[i + 1];
                        i++;
                    }
                    else
                    {
                        inputProject = args[i];
                    }
                }

                if (!outDir.EndsWith(@"\"))
                {
                    outDir += @"\";
                }

                if (!File.Exists(inputProject))
                {
                    Console.WriteLine($"File {inputProject} not found.");
                    return 4;
                }

                MSBuildLocator.RegisterDefaults();
                using (var workspace = Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace.Create())
                {
                    var project = await workspace.OpenProjectAsync(inputProject);

                    NutTree nutTree = new NutTree();
                    var entryPointCandidates = new List<INamedTypeSymbol>();
                    var compilation = await project.GetCompilationAsync();
                    foreach (var tree in compilation.SyntaxTrees)
                    {
                        var root = await tree.GetRootAsync();
                        var model = compilation.GetSemanticModel(tree);
                        foreach (var enumDeclaration in root.DescendantNodes().OfType<EnumDeclarationSyntax>())
                        {
                            var typeSymbol = model.GetDeclaredSymbol(enumDeclaration);
                            nutTree.AddType(enumDeclaration, typeSymbol, model);
                        }

                        foreach (var classDeclaration in root.DescendantNodes().OfType<ClassDeclarationSyntax>())
                        {
                            var typeSymbol = model.GetDeclaredSymbol(classDeclaration);
                            nutTree.AddType(classDeclaration, typeSymbol, model);

                            var baseType = typeSymbol.BaseType;
                            if (LibraryHelper.Is(baseType, LibraryHelper.AIControllerType))
                            {
                                entryPointCandidates.Add(typeSymbol);
                            }
                        }
                    }

                    if (entryPointCandidates.Count != 1)
                    {
                        return 2;
                    }

                    nutTree.GenerateCode(entryPointCandidates[0]);
                    string infoNut = nutTree.GetInfoText();
                    string mainNut = nutTree.GetMainText();
                    if (!Directory.Exists(outDir))
                    {
                        Directory.CreateDirectory(outDir);
                    }

                    File.WriteAllText(outDir + "info.nut", infoNut);
                    File.WriteAllText(outDir + "main.nut", mainNut);
                }

                Console.WriteLine("Done");
                return 0;
            }
            catch (UnsupportedSyntaxException exp)
            {
                Console.Write("Unsupported syntax");
                return 5;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.GetType().Name);
                Console.Write(exp.Message);
                Console.Write(exp.StackTrace);
                return -999;
            }
        }
    }
}