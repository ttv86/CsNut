﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CsNut.UnitTests
{
    [TestClass]
    public class NodeWriterTests
    {
        [TestMethod]
        public async Task TestClassWithBasicMethodsDeclaration()
        {
            var input = @"
public class MyClass {
    public void Method1() {
    }

    public int Method2() {
        return 1;
    }
}";

            var expected = @"
class MyClass {
    function Method1() {
    }

    function Method2() {
        return 1;
    }
}";

            var compilationResult = await CompileAsync(input);
            AreEqual(RemoveExtraSpaces(expected), RemoveExtraSpaces(compilationResult));
        }

        [TestMethod]
        public async Task TestProperties()
        {
            var input = @"
public class MyClass {
    public MyClass(int arg1) {
        this.Property2 = arg1;
    }

    public int Property1 { get; set; }

    public int Property2 { get; }

    public int Property3 => Property1 + Property2;
}";

            var expected = @"
class MyClass {
    constructor(arg1) {
        this.set_Property2(arg1);
    }

    _uq_a = null;
    function get_Property1() {
        return this._uq_a;
    }

    function set_Property1(value) {
        this._uq_a = value;
    }

    _uq_b = null;
    function get_Property2() {
        return this._uq_b;
    }

    function set_Property2(value) {
        this._uq_b = value;
    }

    function get_Property3() {
        return (this.get_Property1() + this.get_Property2());
    }
}";

            var compilationResult = await CompileAsync(input);
            AreEqual(RemoveExtraSpaces(expected), RemoveExtraSpaces(compilationResult));
        }

        [TestMethod]
        public async Task TestUsing()
        {
            var input = @"
public class MyClass {
    public void Test() {
        using (var cls = new OtherClass()) {
            cls.DoWork();
        }
    }
}

public class OtherClass : System.IDisposable {
    public void DoWork() {
    }    

    public void Dispose() {
    }
}";

            var expected = @"
class MyClass {
    function Test() {
        local cls = null;
        local _uq_a = null;
        try {
            cls=/*new*/OtherClass();
            cls.DoWork();
        } catch (_uq_b) {
            _uq_a = _uq_b;
        }
        if (cls != null) {
            cls.Dispose();
        }
        if (_uq_a != null) {
            throw _uq_a;
        }
    }
}

class OtherClass {
    function DoWork() {
    }

    function Dispose() {
    }
}";

            var compilationResult = await CompileAsync(input);
            AreEqual(RemoveExtraSpaces(expected), RemoveExtraSpaces(compilationResult));
        }

        [TestMethod]
        public async Task TestGenericList()
        {
            
            var input = @"
public class MyClass {
    public void Test() {
        var list = new System.Collections.Generic.List<int>();
        list.Add(1);
        for (var i = 0; i < list.Count; i++) {
        }
        list.Clear();
    }
}";

            var expected = @"
class MyClass {
    function Test() {
        local list = [];
        list.push(1);
        for (local i = 0; (i < list.len()); i++) {
        }
        list.clear();
    }
}";

            var compilationResult = await CompileAsync(input);
            AreEqual(RemoveExtraSpaces(expected), RemoveExtraSpaces(compilationResult));
        }

        [TestMethod]
        [ExpectedException(typeof(UnsupportedSyntaxException))]
        public async Task TestLock()
        {
            var input = @"
public class MyClass {
    private lockObj = new object();

    public void Test() {
        lock (lockObj) {
        }
    }
}";

            var compilationResult = await CompileAsync(input);
            Assert.Fail();
        }

        [TestMethod]
        public async Task TestConstructorOverloading()
        {
            var input = @"
public class MyClass {
    private int value = 0;

    public MyClass() {
        value = 1;
    }

    public MyClass(int a) {
        value = 2;
    }

    public MyClass(string b) {
        value = 3;
    }

    public MyClass(int a, string b) {
        value = 4;
    }
}";

            var expected = @"
class MyClass {
    value = null;
    constructor(...) {
        this.value = 0;
        switch(vargc) {
            case 0:
                this._uq_a();
                break;
            case 1:
                if (typeof vargv[0] == ""integer"")
                {
                    this._uq_b(vargv[0]);
                }
                else
                {
                    this._uq_c(vargv[0]);
                }

                break;
            case 2:
                this._uq_d(vargv[0],vargv[1]);
                break;
        }
    }

    function _uq_a() {
        this.value = 1;
    }

    function _uq_b(a) {
        this.value = 2;
    }

    function _uq_c(b) {
        this.value = 3;
    }

    function _uq_d(a, b) {
        this.value = 4;
    }
}";

            var compilationResult = await CompileAsync(input);
            AreEqual(RemoveExtraSpaces(expected), RemoveExtraSpaces(compilationResult));
        }

        [TestMethod]
        public async Task TestConstructorInheritance()
        {
            var input = @"
public class MyClass {
    private int value = 0;

    public MyClass() {
        value = 1;
    }

    public MyClass(int a) : this() {
        value = a;
    }
}";

            var expected = @"
class MyClass {
    value = null;
    constructor(...) {
        this.value = 0;
        switch(vargc) {
            case 0:
                this._uq_a();
                break;
            case 1:
                this._uq_b(vargv[0]);
                break;
        }
    }

    function _uq_a() {
        this.value = 1;
    }

    function _uq_b(a) {
        _uq_a();
        this.value = a;
    }
}";

            var compilationResult = await CompileAsync(input);
            AreEqual(RemoveExtraSpaces(expected), RemoveExtraSpaces(compilationResult));
        }

        [TestMethod]
        public async Task TestNamespaces()
        {
            var input = @"
namespace Main {
    public class MyClass {
        public void DoWork() {
            var other = new Other.Sub.MyClass();
            var c2 = new Class2();
            other.DoWork();
        }
    }

    public class Class2 {
    }
}

namespace Other.Sub {
    public class MyClass {
        public void DoWork() {
        }
    }
}";

            var expected = @"
Main <- {};
class Main.MyClass {
    function DoWork() {
        local other = /*new*/Other.Sub.MyClass();
        local c2 = /*new*/Main.Class2();
        other.DoWork();
    }
}

class Main.Class2 {
}

Other <- {};
Other.Sub <- {};
class Other.Sub.MyClass {
    function DoWork() {
    }
}";

            var compilationResult = await CompileAsync(input);
            AreEqual(RemoveExtraSpaces(expected), RemoveExtraSpaces(compilationResult));
        }

        [TestMethod]
        public async Task TestEnums()
        {
            var input = @"
public enum EnumElement
{
    Aaa = 1,
    Bbb,
    Ccc = 6,
    Ddd,
    Eee = Class2.Value
}

public static class Class2 {    
    public const int Value = 0x80;
}";

            var expected = @"
class EnumElement {
    Aaa = 1;
    Bbb = 2;
    Ccc = 6;
    Ddd = 7;
    Eee = 128
}

class Class2 {
    Value = null;
    constructor() {
        this.Value = 128;
    }
}";

            var compilationResult = await CompileAsync(input);
            AreEqual(RemoveExtraSpaces(expected), RemoveExtraSpaces(compilationResult));
        }

        [TestMethod]
        public async Task TestTryFinally()
        {
            var input = @"
public static class MyClass {    
    public static int DoWork() {
        try {
            Log(1); // This may throw an exception
        } finally {
            Log(2); // Do this always
        }

        Log(3); // Do this only if we got here without errors.
    }

    public static void Log(int eventCode) {
    }
}";

            var expected = @"
class MyClass {
    static function DoWork() {
        local _uq_a = null;
        try {
            MyClass.Log(1);
        } catch (_uq_b) {
            _uq_a = _uq_b;
        }
        MyClass.Log(2);
        if (_uq_a != null) {
            throw _uq_a;
        };

        MyClass.Log(3);
    }

    static function Log(eventCode) {
    }
}";

            var compilationResult = await CompileAsync(input);
            AreEqual(RemoveExtraSpaces(expected), RemoveExtraSpaces(compilationResult));
        }

        [TestMethod]
        public async Task TestLambda()
        {
            var input = @"
public static class MyClass {    
    public static int DoWork() {
        DoSomething1(a => a.ToString());
        DoSomething1(a => {});
        return DoSomething2((a, b) => a + b);
    }

    public static int DoSomething1(System.Action<int> func)
    {
        func(1);
    }

    public static int DoSomething2(System.Func<int, int, int> func)
    {
        return func(1, 2);
    }
}";

            var expected = @"
class MyClass {
    static function DoWork() {
        MyClass.DoSomething1(function (a) { a.tostring(); });
        MyClass.DoSomething1(function (a) { });
        return MyClass.DoSomething2(function (a, b) { return (a + b); });
    }

    static function DoSomething1(func)
    {
        func(1);
    }

    static function DoSomething2(func)
    {
        return func(1, 2);
    }
}";


            var compilationResult = await CompileAsync(input);
            AreEqual(RemoveExtraSpaces(expected), RemoveExtraSpaces(compilationResult));
        }

        [TestMethod]
        public async Task TestNestedMethod()
        {
            string input = @"
class MyClass {
    static void DoWork() {
		var a = 5;
		var b = 5;
		var c = 5;
		double f1() { return b + 1; }
		double f2() { return c + 1; }
		double f3() { return b + c; }
		for (var i = 0; i < 10; i++) {
			Log(f1());
			Log(f2());
			Log(f3());
			a += 1;
			b += 2;
			c += 3;
		}
	}

    static double Log(double value) {
    }
}";

            string expected = @"
class MyClass {
    static function DoWork() {
		local _uq_a = {};
		local a = 5;
		_uq_a.b <- 5;
		_uq_a.c <- 5;
		local f1 = function(_uq_a) { return (_uq_a.b + 1); };
		local f2 = function(_uq_a) { return (_uq_a.c + 1); };
		local f3 = function(_uq_a) { return (_uq_a.b + _uq_a.c); };
		for (local i = 0; (i < 10); i++) {
			MyClass.Log(f1(_uq_a));
			MyClass.Log(f2(_uq_a));
			MyClass.Log(f3(_uq_a));
			a += 1;
			_uq_a.b += 2;
			_uq_a.c += 3;
		}
	}

    static function Log(value) {
    }
}";

            var compilationResult = await CompileAsync(input);
            AreEqual(RemoveExtraSpaces(expected), RemoveExtraSpaces(compilationResult));
        }

        private async Task<string> CompileAsync(string input)
        {
            var ws = new AdhocWorkspace();
            var project = ws
                .AddProject("TestProject", "C#")
                .WithMetadataReferences(new MetadataReference[]
                {
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
                });
            var document = project.AddDocument("TestClass.cs", input);
            var compilation = await document.Project.GetCompilationAsync();
            var tree = await document.GetSyntaxTreeAsync();
            var model = compilation.GetSemanticModel(tree);
            StringBuilder resultBuilder = new StringBuilder();
            var context = new CompilationContext();
            var writer = new NodeWriter(model, resultBuilder, context);
            foreach (var enumDeclarationSyntax in tree.GetRoot().DescendantNodesAndSelf().OfType<EnumDeclarationSyntax>())
            {
                var symbol = model.GetDeclaredSymbol(enumDeclarationSyntax);
                if (context.MainAssembly == null)
                {
                    context.MainAssembly = symbol.ContainingAssembly;
                }

                writer.Write(enumDeclarationSyntax, symbol);
            }

            foreach (var classDeclarationSyntax in tree.GetRoot().DescendantNodesAndSelf().OfType<ClassDeclarationSyntax>())
            {
                var symbol = model.GetDeclaredSymbol(classDeclarationSyntax);
                if (context.MainAssembly == null)
                {
                    context.MainAssembly = symbol.ContainingAssembly;
                }

                writer.Write(classDeclarationSyntax, symbol);
            }

            return resultBuilder.ToString();
        }

        private static string RemoveExtraSpaces(string str)
        {
            return Regex.Replace(str, @"(?<!\w)\s|\s(?!\w)", string.Empty, RegexOptions.Singleline);
        }

        private static void AreEqual(string expected, string actual)
        {
            if ((expected != actual) && Debugger.IsAttached)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < Math.Min(expected.Length, actual.Length); i++)
                {
                    sb.Append(expected[i] == actual[i] ? " " : "^");
                }

                Debug.WriteLine("Expec.: " + expected);
                Debug.WriteLine("Actual: " + actual);
                Debug.WriteLine("Diff:   " + sb.ToString());
            }

            Assert.AreEqual(expected, actual);
        }
    }
}
