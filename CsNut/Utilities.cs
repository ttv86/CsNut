using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;

namespace CsNut
{
    internal class Utilities
    {
        ///////////////////////////////01234567890123456789012345678901
        private static char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEF".ToCharArray();
        private static HashSet<string> reserved = new HashSet<string>("base,break,case,catch,class,clone,continue,const,default,delete,else,enum,extends,for,foreach,function,if,in,local,null,resume,return,switch,this,throw,try,typeof,while,yield,constructor,instanceof,true,false,static".Split(','));

        internal static string WriteLiteral(object value, bool prependSpaceIfNeeded = false)
        {
            string result;
            if (value == null)
            {
                result = "null";
            }
            else if (Equals(value, true))
            {
                result = "true";
            }
            else if (Equals(value, false))
            {
                result = "false";
            }
            else if (value is string str)
            {
                result = string.Format(CultureInfo.InvariantCulture, "\"{0}\"", EscapeString(str));
            }
            else if ((value is float) || (value is double))
            {
                result = string.Format(CultureInfo.InvariantCulture, "{0:0.0#########}", value);
            }
            else
            {
                result = string.Format(CultureInfo.InvariantCulture, "{0}", value);
            }

            if ((!prependSpaceIfNeeded) || string.IsNullOrEmpty(result) || (!char.IsLetterOrDigit(result[0])))
            {
                return result;
            }
            else
            {
                return " " + result;
            }
        }

        internal static string EscapeString(string value)
        {
            return Regex.Replace(value, "[\t\n\r\0\"\\\\]", m => @"\" + m.Value);
        }

        internal static IEnumerable<string> CreateNameGenerator()
        {
            ulong value = 0;
            while (value < ulong.MaxValue)
            {
                string result;
                do
                {
                    result = LongToString(value);
                    value++;
                } while (reserved.Contains(result));
                yield return result;
            }
        }

        internal static string GetValue(INamespaceSymbol namespaceValue, bool skipOpenTTD)
        {
            if (skipOpenTTD && (namespaceValue.ContainingAssembly.Name == "OTTDLib"))
            {
                return null;
            }

            if ((namespaceValue == null)||namespaceValue.IsGlobalNamespace)
            {
                return null;
            }
            
            return namespaceValue.ToString();
        }

        internal static object Increase(object previousValue)
        {
            if (previousValue is int i)
            {
                return i + 1;
            }
            else if (previousValue is long l)
            {
                return l + 1l;
            }
            else if (previousValue is uint ui)
            {
                return ui + 1u;
            }
            else if (previousValue is ulong ul)
            {
                return ul + 1ul;
            }
            else if (previousValue is byte b)
            {
                return b + 1;
            }
            else if (previousValue is short s)
            {
                return s + 1;
            }
            else if (previousValue is ushort us)
            {
                return us + 1;
            }
            else if (previousValue is sbyte sb)
            {
                return sb + 1;
            }
            else if (previousValue is double d)
            {
                return d + 1d;
            }
            else if (previousValue is float f)
            {
                return f + 1f;
            }

            throw new NotSupportedException();
        }

        private static string LongToString(ulong value)
        {
            StringBuilder sb = new StringBuilder();
            while ((value > 0) || (sb.Length == 0))
            {
                sb.Append(chars[value % 32]);
                value >>= 5;
            }

            return sb.ToString();
        }
    }
}