using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CsNut
{
    internal class NodeWriter
    {
        private static Dictionary<Type, MethodInfo> funcs = new Dictionary<Type, MethodInfo>();
        private bool minimize = false;
        private int indent = 0;
        private SemanticModel semanticModel;
        private StringBuilder text;
        private CompilationContext context;

        static NodeWriter()
        {
            foreach (var method in typeof(NodeWriter).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (method.Name != "Write")
                {
                    continue;
                }

                var parameters = method.GetParameters();
                if (parameters.Length == 0)
                {
                    continue;
                }

                var paramType = parameters[0].ParameterType;
                if (!paramType.IsSubclassOf(typeof(Microsoft.CodeAnalysis.CSharp.CSharpSyntaxNode)))
                {
                    continue;
                }

                funcs.Add(paramType, method);
            }
        }

        internal NodeWriter(SemanticModel semanticModel, StringBuilder text, CompilationContext context)
        {
            this.semanticModel = semanticModel;
            this.text = text;
            this.context = context;
            this.minimize = this.context.Uglify;
        }

        internal void Write(ClassDeclarationSyntax classDeclarationSyntax, INamedTypeSymbol typeSymbol)
        {
            string name = context.GetName(typeSymbol, classDeclarationSyntax.Identifier.Text);
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            var typeNamespace = Utilities.GetValue(typeSymbol.ContainingNamespace, true);
            if ((typeNamespace != null) && !this.minimize)
            {
                var namespaces = this.context.GetUnintroducedNamespaces(typeNamespace);
                foreach (var ns in namespaces)
                {
                    text.Append($"{ns} <- {{}};");
                    NewLine();
                }
            }

            text.Append("class ");
            if (typeNamespace != null)
            {
                text.Append(typeNamespace);
                text.Append(".");
            }

            text.Append(name);
            if ((typeSymbol.BaseType != null) && (typeSymbol.BaseType.SpecialType == SpecialType.None))
            {
                text.Append(" extends ");
                text.Append(context.GetName(typeSymbol.BaseType, typeSymbol.BaseType.Name));
            }

            NewLine();
            text.Append("{");
            NewLine(1);
            bool isThereConstructor = false;
            var fields = classDeclarationSyntax.Members.OfType<FieldDeclarationSyntax>().ToList();
            foreach (var member in fields)
            {

                Write(member);
            }

            var constructors = classDeclarationSyntax.Members.OfType<ConstructorDeclarationSyntax>().ToList();
            this.CreateConstructor(constructors, fields);

            foreach (var member in classDeclarationSyntax.Members)
            {
                if (member is MethodDeclarationSyntax methodDeclarationSyntax)
                {
                    Write(methodDeclarationSyntax);
                }
                else if (member is FieldDeclarationSyntax fieldDeclarationSyntax)
                {
                    // Handled elsewhere.
                }
                else if (member is ConstructorDeclarationSyntax constructorDeclarationSyntax)
                {
                    // Handled elsewhere.
                }
                else if ((member is ClassDeclarationSyntax) || (member is EnumDeclarationSyntax))
                {
                    // Handled elsewhere.
                }
                else if (member is PropertyDeclarationSyntax propertyDeclarationSyntax)
                {
                    Write(propertyDeclarationSyntax);
                }
                else
                {
                    WriteComment(string.Format("Unsupported member: {0}", member.GetType().Name));
                    NewLine();
                }
            }

            /*if (!isThereConstructor)
            {
                StringBuilder postScript = new StringBuilder();
                foreach (var methodDeclarationSyntax in classDeclarationSyntax.DescendantNodes().OfType<MethodDeclarationSyntax>())
                {
                    var symbol = this.semanticModel.GetDeclaredSymbol(methodDeclarationSyntax);
                    if (!symbol.IsStatic)
                    {
                        var methodName = this.context.GetName(symbol, methodDeclarationSyntax.Identifier.Text);
                        postScript.AppendFormat("this.{0}=this._{0}.bindenv(this);", methodName);
                    }
                }

                if (postScript.Length > 0)
                {
                    text.Append("constuctor() {");
                    NewLine(1);
                    text.Append(postScript.ToString());
                    NewLine(-1);
                    text.Append("}");
                    NewLine();
                }
            }*/

            NewLine(-1);
            text.Append("}");
            NewLine();
        }

        private void CreateConstructor(List<ConstructorDeclarationSyntax> constructors, List<FieldDeclarationSyntax> fields)
        {
            if ((constructors.Count == 0) && (fields.Count == 0))
            {
                return;
            }

            text.Append("constructor");
            if (constructors.Count == 0)
            {
                text.Append("()");
            }
            else if (constructors.Count == 1)
            {
                Write(constructors[0].ParameterList);
            }
            else if (constructors.Count > 1)
            {
                text.Append("(...)");
            }

            text.Append("{");
            NewLine(1);
            foreach (var field in fields)
            {
                foreach (var variable in field.Declaration.Variables.Where(x => x.Initializer != null))
                {
                    var symbol = this.semanticModel.GetDeclaredSymbol(variable);
                    text.Append($"this.{this.context.GetName(symbol, variable.Identifier.Text)} = ");
                    Write(variable.Initializer.Value);
                    text.Append(";");
                    NewLine();
                }
            }

            StringBuilder constructorBodyBuilder = null;
            if (constructors.Count == 1)
            {
                Write(constructors[0].Body, true);
            }
            else if (constructors.Count > 1)
            {
                constructorBodyBuilder = new StringBuilder();
                var writer2 = new NodeWriter(this.semanticModel, constructorBodyBuilder, this.context);
                writer2.indent = this.indent;
                writer2.minimize = this.minimize;

                Dictionary<ConstructorDeclarationSyntax, string> newNames = new Dictionary<ConstructorDeclarationSyntax, string>();
                foreach (var item in constructors)
                {
                    var name = this.context.GenerateUniqueName();
                    newNames.Add(item, name);
                    constructorBodyBuilder.Append($"function {name}");
                    writer2.Write(item.ParameterList);
                    constructorBodyBuilder.Append("{");
                    writer2.NewLine(1);
                    if (item.Initializer != null)
                    {
                        if (item.Initializer.ThisOrBaseKeyword.Text == "this")
                        {
                            var symbol = this.semanticModel.GetSymbolInfo(item.Initializer);
                            var who = symbol.Symbol.DeclaringSyntaxReferences.Single().GetSyntax() as ConstructorDeclarationSyntax;
                            constructorBodyBuilder.Append(newNames[who]);
                        }
                        else
                        {
                            constructorBodyBuilder.Append("base");
                        }

                        writer2.Write(item.Initializer.ArgumentList);
                        constructorBodyBuilder.Append(";");
                        NewLine();
                    }

                    writer2.Write(item.Body, true);
                    writer2.NewLine(-1);
                    constructorBodyBuilder.Append("}");
                    writer2.NewLine();
                }

                text.Append("switch(vargc) {");
                NewLine(1);
                foreach (var group in constructors.GroupBy(x => x.ParameterList.Parameters.Count))
                {
                    text.Append($"case {group.Key}:");
                    NewLine(1);
                    CreateConstructorBody(group.ToList(), newNames);
                    text.Append("break;");
                    NewLine(-1);
                }

                NewLine(-1);
                text.Append("}");
                NewLine();
            }

            NewLine(-1);
            text.Append("}");
            if (constructorBodyBuilder != null)
            {
                NewLine();
                text.Append(constructorBodyBuilder.ToString());
            }
            NewLine();
        }

        private void CreateConstructorBody(IList<ConstructorDeclarationSyntax> constructors, Dictionary<ConstructorDeclarationSyntax, string> newNames)
        {
            int paramCount = constructors[0].ParameterList.Parameters.Count;
            void WriteFunc(string newName)
            {
                text.Append($"this.{newName}(");
                for (int i = 0; i < paramCount; i++)
                {
                    if (i > 0)
                    {
                        text.Append(",");
                    }

                    text.Append($"vargv[{i}]");
                }

                text.Append(");");
            }

            char GetParamType(ITypeSymbol tp)
            {
                if ((tp.SpecialType == SpecialType.System_Byte) || (tp.SpecialType == SpecialType.System_SByte) || 
                    (tp.SpecialType == SpecialType.System_Int16) || (tp.SpecialType == SpecialType.System_UInt16) ||
                    (tp.SpecialType == SpecialType.System_Int32) || (tp.SpecialType == SpecialType.System_UInt32) ||
                    (tp.SpecialType == SpecialType.System_Int64) || (tp.SpecialType == SpecialType.System_UInt64))
                {
                    return 'i';
                }

                if ((tp.SpecialType == SpecialType.System_Single) || (tp.SpecialType == SpecialType.System_Double) || (tp.SpecialType == SpecialType.System_Decimal))
                {
                    return 'f';
                }

                if (tp.SpecialType == SpecialType.System_Boolean)
                {
                    return 'b';
                }

                return 'o';
            }

            if (constructors.Count == 1)
            {
                WriteFunc(newNames[constructors[0]]);
                return;
            }
            
            Dictionary<string, string> usedSignatures = new Dictionary<string, string>();
            foreach (var constructor in constructors)
            {
                StringBuilder sb = new StringBuilder(paramCount);
                foreach (var param in constructor.ParameterList.Parameters)
                {
                    var paramSymbol = this.semanticModel.GetDeclaredSymbol(param) as IParameterSymbol;
                    sb.Append(GetParamType(paramSymbol.Type));
                }

                string signature = sb.ToString();
                if (usedSignatures.ContainsKey(signature))
                {
                    throw new UnsupportedSyntaxException("Can't have multiple too similar constructors.", constructor);
                }

                usedSignatures.Add(signature, newNames[constructor]);
            }

            var signatures = usedSignatures.OrderBy(x => x.Key).ToList();
            int ifState = 0;
            foreach (var kvp in signatures)
            {
                for (int i = 0; i < paramCount; i++)
                {
                    if (kvp.Key[i] == 'o')
                    {
                        continue;
                    }

                    switch (ifState)
                    {
                        case 0:
                            text.Append("if(");
                            break;
                        case 1:
                            text.Append(" else if(");
                            break;
                        case 2:
                            text.Append(")&&(");
                            break;
                    }

                    text.Append($"typeof vargv[{i}]==\"");
                    switch (kvp.Key[i])
                    {
                        case 'i':
                            text.Append("integer");
                            break;
                        case 'b':
                            text.Append("bool");
                            break;
                        case 'f':
                            text.Append("float");
                            break;
                    }

                    text.Append("\"");
                    ifState = 2;
                }
                
                text.Append(ifState == 2 ? "){" : "else{");
                NewLine(1);
                WriteFunc(kvp.Value);
                NewLine(-1);
                text.Append("}");
                ifState = 1;
            }
        }

        internal void Write(EnumDeclarationSyntax enumDeclarationSyntax, INamedTypeSymbol typeSymbol)
        {
            var symbol = this.semanticModel.GetDeclaredSymbol(enumDeclarationSyntax);
            var typeNamespace = Utilities.GetValue(typeSymbol.ContainingNamespace, true);
            if ((typeNamespace != null) && !this.minimize)
            {
                var namespaces = this.context.GetUnintroducedNamespaces(typeNamespace);
                foreach (var ns in namespaces)
                {
                    text.Append($"{ns} <- {{}};");
                    NewLine();
                }
            }

            text.Append("class ");
            if (typeNamespace != null)
            {
                text.Append(typeNamespace);
                text.Append(".");
            }

            text.Append(this.context.GetName(symbol, enumDeclarationSyntax.Identifier.Text));
            NewLine();
            text.Append("{");
            NewLine(1);
            object previousValue = -1;
            this.Write(enumDeclarationSyntax.Members, member =>
            {
                var memberSymbol = this.semanticModel.GetDeclaredSymbol(member);
                text.Append(this.context.GetName(memberSymbol, member.Identifier.Text));
                if (member.EqualsValue != null)
                {
                    var constant = this.semanticModel.GetConstantValue(member.EqualsValue.Value);
                    if (constant.HasValue)
                    {
                        previousValue = constant.Value;
                        text.Append($"={previousValue}");
                        return;
                    }
                }

                previousValue = Utilities.Increase(previousValue);
                text.Append($"={previousValue}");
            },";");

            NewLine(-1);
            text.Append("}");
            NewLine();
        }

        private void Write(FieldDeclarationSyntax fieldDeclarationSyntax)
        {
            foreach (var variable in fieldDeclarationSyntax.Declaration.Variables)
            {
                var symbol = this.semanticModel.GetDeclaredSymbol(variable);
                text.Append($"{this.context.GetName(symbol, variable.Identifier.Text)} = null;");
                NewLine();
            }
        }

        private void Write(MethodDeclarationSyntax methodDeclarationSyntax)
        {
            if (methodDeclarationSyntax.Modifiers.Any(s => s.Text == "static"))
            {
                text.Append("static ");
            }

            string name = methodDeclarationSyntax.Identifier.Text;
            if (name == "ToString")
            {
                name = "tostring";
            }
            else
            {
                if (!methodDeclarationSyntax.Modifiers.Any(s => s.Text == "override"))
                {
                    name = this.context.GetName(this.semanticModel.GetDeclaredSymbol(methodDeclarationSyntax), name);
                }
            }

            text.Append("function ");
            text.Append(name);
            Write(methodDeclarationSyntax.ParameterList);
            if (methodDeclarationSyntax.Body != null)
            {
                Write(methodDeclarationSyntax.Body);
            }
            else if (methodDeclarationSyntax.ExpressionBody != null)
            {
                text.Append("{ return ");
                Write(methodDeclarationSyntax.ExpressionBody.Expression);
                text.Append("}");
                NewLine();
            }
        }

        private void Write(ConstructorDeclarationSyntax constructorDeclarationSyntax)
        {
            text.Append("constructor");
            Write(constructorDeclarationSyntax.ParameterList);
            if (constructorDeclarationSyntax.Body != null)
            {
                Write(constructorDeclarationSyntax.Body);
            }
            else if (constructorDeclarationSyntax.ExpressionBody != null)
            {
                text.Append("{ return ");
                Write(constructorDeclarationSyntax.ExpressionBody.Expression);
                text.Append("}");
                NewLine();
            }
        }

        private void Write(PropertyDeclarationSyntax propertyDeclarationSyntax)
        {
            var symbol = this.semanticModel.GetDeclaredSymbol(propertyDeclarationSyntax);
            if (propertyDeclarationSyntax.ExpressionBody != null)
            {
                text.Append("function ");
                this.context.PushPropertyMode(PropertyMode.Read);
                text.Append(this.context.GetName(symbol, propertyDeclarationSyntax.Identifier.Text));
                this.context.PopPropertyMode();
                text.Append("() {");
                NewLine(1);
                text.Append("return ");
                Write(propertyDeclarationSyntax.ExpressionBody.Expression);
                text.Append(";");
                NewLine(-1);
                text.Append("}");
                this.NewLine();
            }

            if (propertyDeclarationSyntax.AccessorList != null)
            {
                string backingField = null;
                BlockSyntax getBody = null;
                BlockSyntax setBody = null;
                foreach (var accessor in propertyDeclarationSyntax.AccessorList.Accessors)
                {
                    if (accessor.Body == null && backingField == null)
                    {
                        backingField = this.context.GenerateUniqueName();
                        text.Append(backingField);
                        text.Append("=null;");
                        NewLine();
                    }

                    if (accessor.Keyword.Text == "get")
                    {
                        getBody = accessor.Body;
                    }

                    if (accessor.Keyword.Text == "set")
                    {
                        setBody = accessor.Body;
                    }
                }

                // Getter
                text.Append("function ");
                this.context.PushPropertyMode(PropertyMode.Read);
                text.Append(this.context.GetName(symbol, propertyDeclarationSyntax.Identifier.Text));
                this.context.PopPropertyMode();
                text.Append("() {");
                NewLine(1);
                if (getBody != null)
                {
                    Write(getBody);
                }
                else
                {
                    text.Append("return this.");
                    text.Append(backingField);
                    text.Append(";");
                }

                NewLine(-1);
                text.Append("}");
                this.NewLine();

                // Setter
                text.Append("function ");
                this.context.PushPropertyMode(PropertyMode.Write);
                text.Append(this.context.GetName(symbol, propertyDeclarationSyntax.Identifier.Text));
                this.context.PopPropertyMode();
                text.Append("(value) {");
                NewLine(1);
                if (setBody != null)
                {
                    Write(setBody);
                }
                else
                {
                    text.Append("this.");
                    text.Append(backingField);
                    text.Append("=value;");
                }

                NewLine(-1);
                text.Append("}");
                this.NewLine();
            }
        }

        private void Write(BlockSyntax blockSyntax, bool skipBraces = false)
        {
            if (!skipBraces)
            {
                NewLine();
                text.Append("{");
                NewLine(1);
            }

            foreach (var statement in blockSyntax.Statements)
            {
                Write(statement);
                NewLine();
            }
            
            if (!skipBraces)
            {
                NewLine(-1);
                text.Append("}");
                NewLine();
            }
        }

        private void NewLine(int indent = 0)
        {
            if (!minimize)
            {
                text.AppendLine();
                this.indent += indent;
                text.Append(new string(' ', this.indent * 4));
            }
        }

        private void Write(SyntaxNode node)
        {
            Type type = node.GetType();
            MethodInfo method = null;
            while ((type != null) && !funcs.TryGetValue(type, out method))
            {
                type = type.BaseType;
            }

            if (method != null)
            {
                object[] parameters = method.GetParameters().Select(x => x.DefaultValue).ToArray();
                parameters[0] = node;
                method.Invoke(this, parameters);
            }
            else
            {
                WriteComment(string.Format("Unsupported statement: {0}", node.GetType().Name));
                NewLine();
            }
        }

        private void WriteComment(string v)
        {
            if (!minimize)
            {
                text.Append("/*");
                text.Append(v);
                text.Append("*/");
            }
        }

        private void Write(LocalDeclarationStatementSyntax localDeclarationStatementSyntax)
        {
            Write(localDeclarationStatementSyntax.Declaration);
            text.Append(";");
        }

        private void Write(VariableDeclarationSyntax variableDeclarationSyntax, bool noLocal = false, bool initializeWithNullIfUninitialized = false)
        {
            if (!noLocal)
            {
                text.Append("local ");
            }

            Write(variableDeclarationSyntax.Variables, variable => Write(variable, initializeWithNullIfUninitialized));
        }

        private void Write(VariableDeclaratorSyntax variableDeclaratorSyntax, bool initializeWithNullIfUninitialized = false)
        {
            var symbolInfo = semanticModel.GetDeclaredSymbol(variableDeclaratorSyntax);
            text.Append(this.context.GetName(symbolInfo, variableDeclaratorSyntax.Identifier.Text));
            if (variableDeclaratorSyntax.Initializer != null)
            {
                Write(variableDeclaratorSyntax.Initializer);
            }
            else if (initializeWithNullIfUninitialized)
            {
                text.Append("=null");
            }
        }

        private void Write(EqualsValueClauseSyntax variableDeclaratorSyntax)
        {
            text.Append("=");
            Write(variableDeclaratorSyntax.Value);
        }

        private void Write(IdentifierNameSyntax identifierNameSyntax)
        {
            var symbol = this.semanticModel.GetSymbolInfo(identifierNameSyntax).Symbol;
            if ((symbol is IPropertySymbol) || (symbol is IMethodSymbol) || (symbol is IFieldSymbol))
            {
                ITypeSymbol statementSymbol = null;
                SyntaxNode node = identifierNameSyntax;
                while (node != null)
                {
                    if ((node is ClassDeclarationSyntax) || (node is StructDeclarationSyntax))
                    {
                        statementSymbol = this.semanticModel.GetDeclaredSymbol(node) as ITypeSymbol;
                        break;
                    }

                    node = node.Parent;
                }

                while (statementSymbol != null)
                {
                    if (symbol.ContainingType == statementSymbol)
                    {
                        if (symbol.ContainingType.IsStatic)
                        {
                            text.Append(this.context.GetName(symbol.ContainingType, symbol.ContainingType.Name));
                        }
                        else
                        {
                            text.Append("this");
                        }
                        text.Append(".");
                        break;
                    }

                    statementSymbol = statementSymbol.BaseType;
                }

            }
            else if (symbol is INamespaceOrTypeSymbol namespaceOrTypeSymbol)
            {
                var ns = Utilities.GetValue(namespaceOrTypeSymbol.ContainingNamespace, true);
                if (ns != null)
                {
                    text.Append(ns);
                    text.Append(".");
                }
            }

            text.Append(this.context.GetName(symbol, identifierNameSyntax.Identifier.Text));
            if ((symbol is IPropertySymbol) && (this.context.PropertyMode == PropertyMode.Read))
            {
                text.Append("()");
            }
        }

        private void Write(LiteralExpressionSyntax literalExpressionSyntax)
        {
            text.Append(Utilities.WriteLiteral(literalExpressionSyntax.Token.Value));
        }

        private void Write(ObjectCreationExpressionSyntax objectCreationExpressionSyntax)
        {
            var symbolInfo = semanticModel.GetSymbolInfo(objectCreationExpressionSyntax.Type);
            if (symbolInfo.Symbol is INamedTypeSymbol namedTypeSymbol)
            {
                var fullName = LibraryHelper.GetFullName(namedTypeSymbol);
                if (fullName == "mscorlib!System.Collections.Generic.List")
                {
                    // Consider List as Array;
                    if (objectCreationExpressionSyntax.Initializer != null)
                    {
                        text.Append("[");
                        Write(objectCreationExpressionSyntax.Initializer.Expressions);
                        text.Append("]");
                    }
                    else
                    {
                        text.Append("[]");
                    }

                    return;
                }
                else if ((fullName == "mscorlib!System.Collections.Generic.Dictionary") || (fullName == "System.Core!System.Collections.Generic.HashSet"))
                {
                    // Consider HashSet and Dictionary as Table;
                    text.Append("{}");
                    if (objectCreationExpressionSyntax.Initializer != null)
                    {
                        throw new NotImplementedException();
                        //Write(objectCreationExpressionSyntax.Initializer.Expressions);
                        //text.Append("}");
                    }

                    return;
                }

                WriteComment("new");
                string newName = this.context.GetName(namedTypeSymbol, null);
                if (newName != string.Empty)
                {
                    if (!string.IsNullOrEmpty(newName))
                    {
                        text.Append(newName);
                    }
                    else
                    {
                        Write(objectCreationExpressionSyntax.Type);
                    }

                    if (objectCreationExpressionSyntax.ArgumentList != null)
                    {
                        Write(objectCreationExpressionSyntax.ArgumentList);
                    }
                    else
                    {
                        text.Append("()");
                    }

                    if (objectCreationExpressionSyntax.Initializer != null)
                    {
                        Write(objectCreationExpressionSyntax.Initializer);
                    }
                }
                else
                {
                    if (objectCreationExpressionSyntax.Initializer != null)
                    {
                        Write(objectCreationExpressionSyntax.Initializer);
                    }
                    else
                    {
                        text.Append("{}");
                    }
                }
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        private void Write(ReturnStatementSyntax returnStatementSyntax)
        {
            if (returnStatementSyntax.Expression != null)
            {
                text.Append("return ");
                Write(returnStatementSyntax.Expression);
                text.Append(";");
            }
            else
            {
                text.Append("return;");
            }
        }

        private void Write(ArgumentListSyntax argumentListSyntax)
        {
            text.Append("(");
            Write(argumentListSyntax.Arguments);
            text.Append(")");
        }

        private void Write(ArgumentSyntax argumentSyntax)
        {
            Write(argumentSyntax.Expression);
        }

        private void Write(ParameterListSyntax parameterListSyntax)
        {
            text.Append("(");
            Write(parameterListSyntax.Parameters);
            text.Append(")");
        }

        private void Write(ParameterSyntax parameterSyntax)
        {
            text.Append(this.context.GetName(this.semanticModel.GetDeclaredSymbol(parameterSyntax), parameterSyntax.Identifier.Text));
            if (parameterSyntax.Default != null)
            {
                Write(parameterSyntax.Default);
            }
        }

        private void Write(ExpressionStatementSyntax expressionStatementSyntax)
        {
            Write(expressionStatementSyntax.Expression);
            text.Append(";");
        }

        private void Write(ForEachStatementSyntax forEachStatementSyntax)
        {
            text.Append("foreach(");
            text.Append(forEachStatementSyntax.Identifier.Text);
            text.Append(" in ");
            this.context.PushPropertyMode(PropertyMode.Read);
            Write(forEachStatementSyntax.Expression);
            this.context.PopPropertyMode();
            text.Append(")");
            Write(forEachStatementSyntax.Statement);
        }

        private void Write(ForEachVariableStatementSyntax forEachVariableStatementSyntax)
        {
            text.Append("foreach(");
            Write(forEachVariableStatementSyntax.Variable);
            text.Append(" in ");
            this.context.PushPropertyMode(PropertyMode.Read);
            Write(forEachVariableStatementSyntax.Expression);
            this.context.PopPropertyMode();
            text.Append(")");
            Write(forEachVariableStatementSyntax.Statement);
        }

        private void Write(DeclarationExpressionSyntax declarationExpressionSyntax)
        {
            Write(declarationExpressionSyntax.Designation);
        }

        private void Write(ParenthesizedVariableDesignationSyntax parenthesizedVariableDesignationSyntax)
        {
            bool noBraces = parenthesizedVariableDesignationSyntax.Parent.Parent is ForEachVariableStatementSyntax;
            if (!noBraces)
            {
                text.Append("(");
            }

            Write(parenthesizedVariableDesignationSyntax.Variables);
            if (!noBraces)
            {
                text.Append(")");
            }
        }

        private void Write(SingleVariableDesignationSyntax singleVariableDesignationSyntax)
        {
            text.Append(singleVariableDesignationSyntax.Identifier.Text);
        }

        private void Write(WhileStatementSyntax whileStatementSyntax)
        {
            text.Append("while(");
            Write(whileStatementSyntax.Condition);
            text.Append(")");
            Write(whileStatementSyntax.Statement);
        }

        private void Write(InvocationExpressionSyntax invocationExpressionSyntax)
        {
            var symbolInfo = semanticModel.GetSymbolInfo(invocationExpressionSyntax.Expression);
            if (symbolInfo.Symbol is IMethodSymbol methodSymbol)
            {
                string fullName = LibraryHelper.GetFullName(methodSymbol);
                if (HandleTables(fullName, invocationExpressionSyntax))
                {
                    return;
                }
            }

            Write(invocationExpressionSyntax.Expression);
            Write(invocationExpressionSyntax.ArgumentList);
        }

        private void Write(MemberAccessExpressionSyntax memberAccessExpressionSyntax)
        {
            var symbol = this.semanticModel.GetSymbolInfo(memberAccessExpressionSyntax);
            if (symbol.Symbol is INamedTypeSymbol)
            {
                Write(memberAccessExpressionSyntax.Name);
                return;
            }
            else if (this.context.GetReplacement(symbol.Symbol, out string replaced))
            {
                foreach (Match match in Regex.Matches(replaced, "%%EXPR%%|.+?(?=(%%EXPR%%|$))"))
                {
                    if (match.Value == "%%EXPR%%")
                    {
                        Write(memberAccessExpressionSyntax.Expression);
                    }
                    else
                    {
                        text.Append(match.Value);
                    }
                }
            }
            else
            {
                Write(memberAccessExpressionSyntax.Expression);
                text.Append(".");
                Write(memberAccessExpressionSyntax.Name);
            }

            if ((!(memberAccessExpressionSyntax.Parent is InvocationExpressionSyntax)) && (symbol.Symbol is IMethodSymbol methodSymbol) && !methodSymbol.IsStatic)
            {
                text.Append(".bindenv(");
                Write(memberAccessExpressionSyntax.Expression);
                text.Append(")");
            }
        }

        private void Write(SimpleNameSyntax simpleNameSyntax)
        {
            var symbolInfo = semanticModel.GetSymbolInfo(simpleNameSyntax);
            if (symbolInfo.Symbol is INamespaceOrTypeSymbol)
            {
                var ns = Utilities.GetValue(symbolInfo.Symbol.ContainingNamespace, true);
                if (ns != null)
                {
                    text.Append(ns);
                    text.Append(".");
                }
            }

            text.Append(this.context.GetName(symbolInfo.Symbol, simpleNameSyntax.Identifier.Text));
            if ((symbolInfo.Symbol is IPropertySymbol) && (this.context.PropertyMode == PropertyMode.Read))
            {
                text.Append("()");
            }
        }

        private void Write(BinaryExpressionSyntax binaryExpressionSyntax)
        {
            text.Append("(");
            this.context.PushPropertyMode(PropertyMode.Read);
            Write(binaryExpressionSyntax.Left);
            switch (binaryExpressionSyntax.OperatorToken.Text)
            {
                default:
                    text.Append(binaryExpressionSyntax.OperatorToken.Text);
                    break;
            }

            Write(binaryExpressionSyntax.Right);
            this.context.PopPropertyMode();
            text.Append(")");
        }

        private void Write(AnonymousObjectCreationExpressionSyntax anonymousObjectCreationExpressionSyntax)
        {
            WriteComment("new");
            text.Append("{");
            Write(anonymousObjectCreationExpressionSyntax.Initializers);
            text.Append("}");
        }

        private void Write(AnonymousObjectMemberDeclaratorSyntax anonymousObjectMemberDeclaratorSyntax)
        {
            Write(anonymousObjectMemberDeclaratorSyntax.NameEquals);
            Write(anonymousObjectMemberDeclaratorSyntax.Expression);
        }

        private void Write(ThisExpressionSyntax thisExpressionSyntax)
        {
            text.Append("this");
        }

        private void Write(IfStatementSyntax ifStatementSyntax)
        {
            text.Append("if(");
            this.context.PushPropertyMode(PropertyMode.Read);
            Write(ifStatementSyntax.Condition);
            this.context.PopPropertyMode();
            text.Append(")");
            Write(ifStatementSyntax.Statement);
            if (ifStatementSyntax.Else != null)
            {
                text.Append("else");
                if (ifStatementSyntax.Else.Statement is IfStatementSyntax)
                {
                    text.Append(" "); // Make space between else and next if.
                }

                Write(ifStatementSyntax.Else.Statement);
            }

            if (minimize)
            {
                text.Append(";");
            }
        }

        private void Write(AssignmentExpressionSyntax assignmentExpressionSyntax)
        {
            var leftSymbol = this.semanticModel.GetSymbolInfo(assignmentExpressionSyntax.Left);
            if ((leftSymbol.Symbol is IPropertySymbol propertySymbol) && !propertySymbol.IsIndexer)
            {
                this.context.PushPropertyMode(PropertyMode.Write);
                Write(assignmentExpressionSyntax.Left);
                this.context.PopPropertyMode();
                this.text.Append("(");
                this.context.PushPropertyMode(PropertyMode.Read);
                Write(assignmentExpressionSyntax.Right);
                this.context.PopPropertyMode();
                this.text.Append(")");
                return;
            }

            Write(assignmentExpressionSyntax.Left);

            if (assignmentExpressionSyntax.Left is ElementAccessExpressionSyntax)
            {
                text.Append("<-");
            }
            else
            {
                text.Append(assignmentExpressionSyntax.OperatorToken.Text);
            }

            this.context.PushPropertyMode(PropertyMode.Read);
            Write(assignmentExpressionSyntax.Right);
            this.context.PopPropertyMode();
        }

        private void Write(ParenthesizedExpressionSyntax parenthesizedExpressionSyntax)
        {
            text.Append("(");
            Write(parenthesizedExpressionSyntax.Expression);
            text.Append(")");
        }

        private void Write(TryStatementSyntax tryStatementSyntax)
        {
            string errorStoreName = null;
            if (tryStatementSyntax.Finally != null)
            {
                errorStoreName = this.context.GenerateUniqueName();
                text.Append($"local {errorStoreName}=null;");
                NewLine();
            }

            text.Append("try");
            Write(tryStatementSyntax.Block);
            if (tryStatementSyntax.Catches.Count > 1)
            {
                throw new UnsupportedSyntaxException("No more than 1 catch is supported", tryStatementSyntax);
            }

            var catchItem = tryStatementSyntax.Catches.FirstOrDefault();
            string errorVariableName;
            if ((catchItem != null) && (catchItem.Declaration != null))
            {
                errorVariableName = catchItem.Declaration.Identifier.Text;
            } else
            {
                errorVariableName = this.context.GenerateUniqueName();
            }

            text.Append($"catch({errorVariableName}){{");
            NewLine(1);
            if (errorStoreName != null)
            {
                text.Append($"{errorStoreName} = {errorVariableName};");
                NewLine();
            }

            if (catchItem != null)
            {
                Write(catchItem.Block, true);
            }

            NewLine(-1);
            text.Append("}");

            if (tryStatementSyntax.Finally != null)
            {
                Write(tryStatementSyntax.Finally.Block, true);
            }

            if (errorStoreName != null)
            {
                text.Append($"if({errorStoreName}!=null){{throw {errorStoreName};}};");
                NewLine();
            }
        }

        private void Write(DiscardDesignationSyntax discardDesignationSyntax)
        {
            text.Append("_");
        }

        private void Write(ElementAccessExpressionSyntax elementAccessExpressionSyntax)
        {
            Write(elementAccessExpressionSyntax.Expression);
            text.Append("[");
            Write(elementAccessExpressionSyntax.ArgumentList.Arguments);
            text.Append("]");
        }

        private void Write(ArrayCreationExpressionSyntax arrayCreationExpressionSyntax)
        {
            WriteComment("new");
            if (arrayCreationExpressionSyntax.Initializer != null)
            {
                text.Append("[");
                Write(arrayCreationExpressionSyntax.Initializer.Expressions);
                text.Append("]");
            }
            else
            {
                text.Append("[]");
            }
        }

        private void Write(PrefixUnaryExpressionSyntax prefixUnaryExpressionSyntax)
        {
            text.Append(prefixUnaryExpressionSyntax.OperatorToken.Text);
            Write(prefixUnaryExpressionSyntax.Operand);
        }

        private void Write(PostfixUnaryExpressionSyntax postfixUnaryExpressionSyntax)
        {
            Write(postfixUnaryExpressionSyntax.Operand);
            text.Append(postfixUnaryExpressionSyntax.OperatorToken.Text);
        }

        private void Write(ForStatementSyntax forStatementSyntax)
        {
            text.Append("for(");
            Write(forStatementSyntax.Initializers);
            Write(forStatementSyntax.Declaration);
            text.Append(";");
            Write(forStatementSyntax.Condition);
            text.Append(";");
            Write(forStatementSyntax.Incrementors);
            text.Append(")");
            Write(forStatementSyntax.Statement);
        }

        private void Write(ThrowStatementSyntax throwStatementSyntax)
        {
            text.Append("throw ");
            Write(throwStatementSyntax.Expression);
            text.Append(";");
        }

        private void Write(CastExpressionSyntax castExpressionSyntax)
        {
            var symbolInfo = semanticModel.GetSymbolInfo(castExpressionSyntax.Type);
            if ((symbolInfo.Symbol as ITypeSymbol).SpecialType == SpecialType.System_Int32)
            {
                text.Append("(");
                Write(castExpressionSyntax.Expression);
                text.Append(").tointeger()");
            }
            else if ((symbolInfo.Symbol as ITypeSymbol).SpecialType == SpecialType.System_Single)
            {
                text.Append("(");
                Write(castExpressionSyntax.Expression);
                text.Append(").tofloat()");
            }
            else if ((symbolInfo.Symbol as ITypeSymbol).SpecialType == SpecialType.System_String)
            {
                text.Append("(");
                Write(castExpressionSyntax.Expression);
                text.Append(").tostring()");
            }
            else
            {
                Write(castExpressionSyntax.Expression);
            }
        }

        private void Write(BreakStatementSyntax breakStatementSyntax)
        {
            text.Append("break;");
        }

        private void Write(ContinueStatementSyntax continueStatementSyntax)
        {
            text.Append("continue;");
        }

        private void Write(InitializerExpressionSyntax initializerExpressionSyntax)
        {
            text.Append("{");
            Write(initializerExpressionSyntax.Expressions);
            text.Append("}");
        }

        private void Write(ConditionalExpressionSyntax conditionalExpressionSyntax)
        {
            Write(conditionalExpressionSyntax.Condition);
            text.Append("?");
            Write(conditionalExpressionSyntax.WhenTrue);
            text.Append(":");
            Write(conditionalExpressionSyntax.WhenFalse);
        }

        private void Write(SwitchStatementSyntax switchStatementSyntax)
        {
            text.Append("switch(");
            Write(switchStatementSyntax.Expression);
            text.Append("){");
            NewLine(1);
            foreach (var section in switchStatementSyntax.Sections)
            {
                Write(section);
            }

            NewLine(-1);
            text.Append("}");
            NewLine();
        }

        private void Write(SwitchSectionSyntax switchSectionSyntax)
        {
            foreach (var label in switchSectionSyntax.Labels)
            {
                if (label is CaseSwitchLabelSyntax caseSwitchLabelSyntax)
                {
                    text.Append("case ");
                    Write(caseSwitchLabelSyntax.Value);
                }
                else if (label is DefaultSwitchLabelSyntax)
                {
                    text.Append("default");
                }

                text.Append(":");
                NewLine();
            }

            NewLine(1);
            foreach (var statement in switchSectionSyntax.Statements)
            {
                Write(statement);
            }

            NewLine(-1);
        }

        private void Write(ConditionalAccessExpressionSyntax conditionalAccessExpressionSyntax)
        {
            string uniqueName = this.context.GenerateUniqueName();
            text.Append("local ");
            text.Append(uniqueName);
            text.Append(" = ");
            Write(conditionalAccessExpressionSyntax.Expression);
            text.Append(";");
            NewLine();
            text.Append("if (");
            text.Append(uniqueName);
            text.Append("!=null){");
            NewLine(1);
            text.Append(uniqueName);
            Write(conditionalAccessExpressionSyntax.WhenNotNull);
            text.Append(";");
            NewLine(-1);
            text.Append("}");
        }

        private void Write(MemberBindingExpressionSyntax memberBindingExpressionSyntax)
        {
            // Left intentionally empty;
        }

        private void Write(QualifiedNameSyntax qualifiedNameSyntax)
        {
            Write(qualifiedNameSyntax.Right);
        }

        private void Write(InterpolatedStringExpressionSyntax interpolatedStringExpressionSyntax)
        {
            text.Append("(\"");
            for (int i = 0; i < interpolatedStringExpressionSyntax.Contents.Count; i++)
            {
                if (interpolatedStringExpressionSyntax.Contents[i] is InterpolatedStringTextSyntax interpolatedStringTextSyntax)
                {
                    text.Append(Utilities.EscapeString(interpolatedStringTextSyntax.TextToken.Text));
                }
                else if (interpolatedStringExpressionSyntax.Contents[i] is InterpolationSyntax interpolationSyntax)
                {
                    text.Append("\" + ");
                    Write(interpolationSyntax.Expression);
                    text.Append(" + \"");
                }
                else
                {
                    text.Append(interpolatedStringExpressionSyntax.Contents[i].GetType().FullName);
                }
            }

            text.Append("\")");
        }

        private void Write<T>(SeparatedSyntaxList<T> list, Action<T> callback = null, string separator = ",") where T : SyntaxNode
        {
            bool flag = false;
            foreach (T item in list)
            {
                if (flag)
                {
                    text.Append(separator);
                }
                else
                {
                    flag = true;
                }

                if (callback != null)
                {
                    callback(item);
                }
                else
                {
                    Write(item);
                }
            }
        }

        private bool HandleTables(string fullName, InvocationExpressionSyntax invocationExpressionSyntax)
        {
            if ((fullName == "mscorlib!System.Collections.Generic.Dictionary.Add") && (invocationExpressionSyntax.ArgumentList.Arguments.Count == 2))
            {
                if (invocationExpressionSyntax.Expression is MemberAccessExpressionSyntax memberAccessExpressionSyntax)
                {
                    Write(memberAccessExpressionSyntax.Expression);
                    text.Append("[");
                    Write(invocationExpressionSyntax.ArgumentList.Arguments[0]);
                    text.Append("]<-");
                    Write(invocationExpressionSyntax.ArgumentList.Arguments[1]);
                    return true;
                }
            }
            if ((fullName == "System.Core!System.Collections.Generic.HashSet.Add") && (invocationExpressionSyntax.ArgumentList.Arguments.Count == 1))
            {
                if (invocationExpressionSyntax.Expression is MemberAccessExpressionSyntax memberAccessExpressionSyntax)
                {
                    Write(memberAccessExpressionSyntax.Expression);
                    text.Append("[");
                    Write(invocationExpressionSyntax.ArgumentList.Arguments[0]);
                    text.Append("]<-true");
                    return true;
                }
            }
            else if (
                ((fullName == "mscorlib!System.Collections.Generic.Dictionary.ContainsKey") && (invocationExpressionSyntax.ArgumentList.Arguments.Count == 1)) ||
                ((fullName == "System.Core!System.Collections.Generic.HashSet.Contains") && (invocationExpressionSyntax.ArgumentList.Arguments.Count == 1)))
            {
                if (invocationExpressionSyntax.Expression is MemberAccessExpressionSyntax memberAccessExpressionSyntax)
                {
                    text.Append("(");
                    Write(invocationExpressionSyntax.ArgumentList.Arguments[0]);
                    text.Append(" in ");
                    Write(memberAccessExpressionSyntax.Expression);
                    text.Append(")");
                    return true;
                }
            }

            return false;
        }

        #region [ Generated ]
        private void Write(AliasQualifiedNameSyntax aliasQualifiedNameSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(PredefinedTypeSyntax predefinedTypeSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(ArrayTypeSyntax arrayTypeSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(PointerTypeSyntax pointerTypeSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(NullableTypeSyntax nullableTypeSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(TupleTypeSyntax tupleTypeSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(OmittedTypeArgumentSyntax omittedTypeArgumentSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(RefTypeSyntax refTypeSyntax)
        {
            throw new NotImplementedException();
        }
        
        private void Write(TupleExpressionSyntax tupleExpressionSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(AwaitExpressionSyntax awaitExpressionSyntax)
        {
            throw new UnsupportedSyntaxException("Await syntax is not supported", awaitExpressionSyntax);
        }

        private void Write(ElementBindingExpressionSyntax elementBindingExpressionSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(ImplicitElementAccessSyntax implicitElementAccessSyntax)
        {
            throw new NotImplementedException();
        }
        
        private void Write(BaseExpressionSyntax baseExpressionSyntax)
        {
            text.Append("base");
        }
        
        private void Write(MakeRefExpressionSyntax makeRefExpressionSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(RefTypeExpressionSyntax refTypeExpressionSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(RefValueExpressionSyntax refValueExpressionSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(CheckedExpressionSyntax checkedExpressionSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(DefaultExpressionSyntax defaultExpressionSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(TypeOfExpressionSyntax typeOfExpressionSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(SizeOfExpressionSyntax sizeOfExpressionSyntax)
        {
            throw new NotImplementedException();
        }
        
        private void Write(AnonymousMethodExpressionSyntax anonymousMethodExpressionSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(SimpleLambdaExpressionSyntax simpleLambdaExpressionSyntax)
        {
            text.Append("function(");
            if (simpleLambdaExpressionSyntax.Parameter != null)
            {
                Write(simpleLambdaExpressionSyntax.Parameter);
            }

            text.Append(")");
            if (simpleLambdaExpressionSyntax.Body is BlockSyntax blockSyntax)
            {
                Write(blockSyntax);
            }
            else
            {
                var symbol = this.semanticModel.GetSymbolInfo(simpleLambdaExpressionSyntax).Symbol as IMethodSymbol;
                text.Append("{");
                if (!symbol.ReturnsVoid)
                {
                    text.Append("return ");
                }

                Write(simpleLambdaExpressionSyntax.Body);
                text.Append(";}");
            }
        }

        private void Write(ParenthesizedLambdaExpressionSyntax parenthesizedLambdaExpressionSyntax)
        {
            text.Append("function");
            Write(parenthesizedLambdaExpressionSyntax.ParameterList);
            if (parenthesizedLambdaExpressionSyntax.Body is BlockSyntax blockSyntax)
            {
                Write(blockSyntax);
            }
            else
            {
                var symbol = this.semanticModel.GetSymbolInfo(parenthesizedLambdaExpressionSyntax).Symbol as IMethodSymbol;
                text.Append("{");
                if (!symbol.ReturnsVoid)
                {
                    text.Append("return ");
                }

                Write(parenthesizedLambdaExpressionSyntax.Body);
                text.Append(";}");
            }
        }

        private void Write(RefExpressionSyntax refExpressionSyntax)
        {
            throw new NotImplementedException();
        }
        
        private void Write(ImplicitArrayCreationExpressionSyntax implicitArrayCreationExpressionSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(StackAllocArrayCreationExpressionSyntax stackAllocArrayCreationExpressionSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(ImplicitStackAllocArrayCreationExpressionSyntax implicitStackAllocArrayCreationExpressionSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(QueryExpressionSyntax queryExpressionSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(OmittedArraySizeExpressionSyntax omittedArraySizeExpressionSyntax)
        {
            throw new NotImplementedException();
        }
        
        private void Write(IsPatternExpressionSyntax isPatternExpressionSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(ThrowExpressionSyntax throwExpressionSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(TypeArgumentListSyntax typeArgumentListSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(ArrayRankSpecifierSyntax arrayRankSpecifierSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(TupleElementSyntax tupleElementSyntax)
        {
            throw new NotImplementedException();
        }
        
        private void Write(BracketedArgumentListSyntax bracketedArgumentListSyntax)
        {
            throw new NotImplementedException();
        }
        
        private void Write(NameColonSyntax nameColonSyntax)
        {
            throw new NotImplementedException();
        }
        
        private void Write(FromClauseSyntax fromClauseSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(LetClauseSyntax letClauseSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(JoinClauseSyntax joinClauseSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(WhereClauseSyntax whereClauseSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(OrderByClauseSyntax orderByClauseSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(SelectClauseSyntax selectClauseSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(GroupClauseSyntax groupClauseSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(QueryBodySyntax queryBodySyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(JoinIntoClauseSyntax joinIntoClauseSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(OrderingSyntax orderingSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(QueryContinuationSyntax queryContinuationSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(WhenClauseSyntax whenClauseSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(DeclarationPatternSyntax declarationPatternSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(ConstantPatternSyntax constantPatternSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(InterpolatedStringTextSyntax interpolatedStringTextSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(InterpolationSyntax interpolationSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(InterpolationAlignmentClauseSyntax interpolationAlignmentClauseSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(InterpolationFormatClauseSyntax interpolationFormatClauseSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(GlobalStatementSyntax globalStatementSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(NamespaceDeclarationSyntax namespaceDeclarationSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(StructDeclarationSyntax structDeclarationSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(InterfaceDeclarationSyntax interfaceDeclarationSyntax)
        {
            throw new NotImplementedException();
        }
        
        private void Write(DelegateDeclarationSyntax delegateDeclarationSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(EnumMemberDeclarationSyntax enumMemberDeclarationSyntax)
        {
            throw new NotImplementedException();
        }
        
        private void Write(EventFieldDeclarationSyntax eventFieldDeclarationSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(OperatorDeclarationSyntax operatorDeclarationSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(ConversionOperatorDeclarationSyntax conversionOperatorDeclarationSyntax)
        {
            throw new NotImplementedException();
        }
        
        private void Write(DestructorDeclarationSyntax destructorDeclarationSyntax)
        {
            throw new UnsupportedSyntaxException("Destructors are not supported", destructorDeclarationSyntax);
        }

        private void Write(EventDeclarationSyntax eventDeclarationSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(IndexerDeclarationSyntax indexerDeclarationSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(IncompleteMemberSyntax incompleteMemberSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(LocalFunctionStatementSyntax localFunctionStatementSyntax)
        {
            throw new NotImplementedException();
        }
        
        private void Write(EmptyStatementSyntax emptyStatementSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(LabeledStatementSyntax labeledStatementSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(GotoStatementSyntax gotoStatementSyntax)
        {
            throw new NotImplementedException();
        }
        
        private void Write(YieldStatementSyntax yieldStatementSyntax)
        {
            throw new NotImplementedException();
        }
        
        private void Write(DoStatementSyntax doStatementSyntax)
        {
            throw new NotImplementedException();
        }
        
        private void Write(UsingStatementSyntax usingStatementSyntax)
        {
            string variableName;
            VariableDeclaratorSyntax variableDeclarator = null;
            if (usingStatementSyntax.Declaration != null)
            {
                variableDeclarator = usingStatementSyntax.Declaration.Variables.SingleOrDefault();
                var symbol = this.semanticModel.GetDeclaredSymbol(variableDeclarator);
                this.context.MakeUglified(symbol);
                variableName = this.context.GetName(symbol, variableDeclarator.Identifier.Text);
            }
            else if (usingStatementSyntax.Expression != null)
            {
                variableName = this.context.GenerateUniqueName();
            }
            else
            {
                throw new NotImplementedException();
            }

            string errorHolderName = this.context.GenerateUniqueName();
            string errorTempName = this.context.GenerateUniqueName();
            text.Append($"local {variableName}=null;local {errorHolderName}=null;");
            NewLine();
            text.Append($"try{{");
            NewLine(1);
            if (variableDeclarator != null)
            {
                Write(variableDeclarator);
            }
            else
            {
                text.Append($"{variableName}=");
                Write(usingStatementSyntax.Expression);
            }

            text.Append(";");
            NewLine();

            if (usingStatementSyntax.Statement is BlockSyntax blockSyntax)
            {
                Write(blockSyntax, true);
            }
            else
            {
                Write(usingStatementSyntax.Statement);
            }

            NewLine(-1);
            text.Append($"}}catch({errorTempName}){{{errorHolderName}={errorTempName};}}");
            NewLine();
            text.Append($"if({variableName}!=null){{{variableName}.Dispose();}}");
            NewLine();
            text.Append($"if({errorHolderName}!=null){{throw {errorHolderName};}}");
        }

        private void Write(FixedStatementSyntax fixedStatementSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(CheckedStatementSyntax checkedStatementSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(UnsafeStatementSyntax unsafeStatementSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(LockStatementSyntax lockStatementSyntax)
        {
            throw new UnsupportedSyntaxException("Lock syntax is not supported", lockStatementSyntax);
        }

        private void Write(ExternAliasDirectiveSyntax externAliasDirectiveSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(UsingDirectiveSyntax usingDirectiveSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(AttributeListSyntax attributeListSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(AttributeTargetSpecifierSyntax attributeTargetSpecifierSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(AttributeSyntax attributeSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(AttributeArgumentListSyntax attributeArgumentListSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(AttributeArgumentSyntax attributeArgumentSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(NameEqualsSyntax nameEqualsSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(TypeParameterListSyntax typeParameterListSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(TypeParameterSyntax typeParameterSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(BaseListSyntax baseListSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(SimpleBaseTypeSyntax simpleBaseTypeSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(TypeParameterConstraintClauseSyntax typeParameterConstraintClauseSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(ConstructorConstraintSyntax constructorConstraintSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(ClassOrStructConstraintSyntax classOrStructConstraintSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(TypeConstraintSyntax typeConstraintSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifierSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(ConstructorInitializerSyntax constructorInitializerSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(ArrowExpressionClauseSyntax arrowExpressionClauseSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(AccessorListSyntax accessorListSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(AccessorDeclarationSyntax accessorDeclarationSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(BracketedParameterListSyntax bracketedParameterListSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(TypeCrefSyntax typeCrefSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(QualifiedCrefSyntax qualifiedCrefSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(NameMemberCrefSyntax nameMemberCrefSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(IndexerMemberCrefSyntax indexerMemberCrefSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(OperatorMemberCrefSyntax operatorMemberCrefSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(ConversionOperatorMemberCrefSyntax conversionOperatorMemberCrefSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(CrefParameterListSyntax crefParameterListSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(CrefBracketedParameterListSyntax crefBracketedParameterListSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(CrefParameterSyntax crefParameterSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(XmlElementSyntax xmlElementSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(XmlEmptyElementSyntax xmlEmptyElementSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(XmlTextSyntax xmlTextSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(XmlCDataSectionSyntax xmlCDataSectionSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(XmlProcessingInstructionSyntax xmlProcessingInstructionSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(XmlCommentSyntax xmlCommentSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(XmlElementStartTagSyntax xmlElementStartTagSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(XmlElementEndTagSyntax xmlElementEndTagSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(XmlNameSyntax xmlNameSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(XmlPrefixSyntax xmlPrefixSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(XmlTextAttributeSyntax xmlTextAttributeSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(XmlCrefAttributeSyntax xmlCrefAttributeSyntax)
        {
            throw new NotImplementedException();
        }

        private void Write(XmlNameAttributeSyntax xmlNameAttributeSyntax)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}