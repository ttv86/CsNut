using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

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
            return value;
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