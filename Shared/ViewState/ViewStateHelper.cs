using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ViewStateHelper
    {
        public static string GetRawBase64Data(string value)
        {
            byte[] bytes = Convert.FromBase64String(value);
            char[] chars = new UTF8Encoding(false).GetChars(bytes);
            var sb = new StringBuilder();
            foreach (char ch in chars)
            {
                if ((char.IsLetterOrDigit(ch) || char.IsPunctuation(ch)) || char.IsSeparator(ch))
                {
                    sb.Append(ch);
                }
                else
                {
                    sb.Append("&#");
                    sb.Append(((int)ch).ToString());
                    sb.Append(';');
                }
            }
            return sb.ToString();
        }
    }
}
