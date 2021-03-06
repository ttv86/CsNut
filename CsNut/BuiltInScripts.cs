﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsNut
{
    internal class BuiltInScripts
    {
        internal static readonly Dictionary<string, string> NewFuncs = new Dictionary<string, string>()
        {
            { "mscorlib!System.Math.Min", "function %%FUNCNAME%%(a,b){return a<b?a:b}"},
            { "mscorlib!System.Math.Max", "function %%FUNCNAME%%(a,b){return a>b?a:b}"},
            { "mscorlib!System.Math.Abs", "function %%FUNCNAME%%(a){return a<0?-a:a}"},
            { "mscorlib!System.Math.Sign", "function %%FUNCNAME%%(a){return a<0?-1:1}"},
            { "mscorlib!System.Math.Round", "function %%FUNCNAME%%(a){a=a-0.5;local i=a.tointeger();if(i==a){return i}else if(a>0){i+=1};;return i}"},
            { "mscorlib!System.Math.Ceiling", "function %%FUNCNAME%%(a){local i=a.tointeger();if(i==a){return i}else if(a>0){i+=1};;return i}"},
            { "mscorlib!System.Math.Floor", "function %%FUNCNAME%%(a){local i=a.tointeger();if(i==a){return i}else if(a<0){i-=1};;return i}"},
            { "mscorlib!System.String.IsNullOrEmpty", "function %%FUNCNAME%%(a){return a==null||a==\"\"}"},
            { "mscorlib!System.String.Join", "function %%FUNCNAME%%(a,b){local r=\"\";for(local c=0;c<b.len();c++){if(c>0){r+=a};r+=b[c].tostring()}return r}"},
        };

        internal static readonly Dictionary<string, string> InlineChanges = new Dictionary<string, string>()
        {
            {"mscorlib!System.Object.ToString", "%%EXPR%%.tostring"},
            {"mscorlib!System.Int32.ToString", "%%EXPR%%.tostring"},
            {"mscorlib!System.Single.ToString", "%%EXPR%%.tostring"},
            {"mscorlib!System.Double.ToString", "%%EXPR%%.tostring"},
            {"mscorlib!System.String.ToLower", "%%EXPR%%.tolower"},
            {"mscorlib!System.String.ToLowerInvariant", "%%EXPR%%.tolower"},
            {"mscorlib!System.String.ToUpper", "%%EXPR%%.toupper"},
            {"mscorlib!System.String.ToUpperInvariant", "%%EXPR%%.toupper"},
            {"mscorlib!System.String.get_Length", "%%EXPR%%.len()"},
            {"mscorlib!System.Array.get_Length", "%%EXPR%%.len()"},

            { "mscorlib!System.Math.PI", "(3.1415926535897931)"},
            { "mscorlib!System.Math.E", "(2.7182818284590451)"},

            {"mscorlib!System.Collections.Generic.List.Add", "%%EXPR%%.push"},
            {"mscorlib!System.Collections.Generic.List.AddRange", "%%EXPR%%.extend"},
            {"mscorlib!System.Collections.Generic.List.Clear", "%%EXPR%%.clear"},
            {"mscorlib!System.Collections.Generic.List.RemoveAt", "%%EXPR%%.remove"},
            {"mscorlib!System.Collections.Generic.List.get_Count", "%%EXPR%%.len()"},
            {"mscorlib!System.Collections.Generic.Dictionary.get_Count", "%%EXPR%%.len()"},
            {"mscorlib!System.Collections.Generic.Dictionary.Clear", "%%EXPR%%.clear"},

            {"mscorlib!System.Collections.Generic.IList.Add", "%%EXPR%%.push"},
            {"mscorlib!System.Collections.Generic.IList.Clear", "%%EXPR%%.clear"},
            {"mscorlib!System.Collections.Generic.IList.RemoveAt", "%%EXPR%%.remove"},
            {"mscorlib!System.Collections.Generic.IList.get_Count", "%%EXPR%%.len()"},
            {"mscorlib!System.Collections.Generic.IDictionary.get_Count", "%%EXPR%%.len()"},
            {"mscorlib!System.Collections.Generic.IDictionary.Clear", "%%EXPR%%.clear"},

            {"System.Core!System.Collections.Generic.HashSet.Clear", "%%EXPR%%.clear"},
        };
    }
}