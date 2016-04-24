using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Shared.BbCode
{
    /// <summary>
    /// Markdown is the more popular choice. This class is for historic reasons.
    /// </summary>
    [Obsolete("Use markdown")]
    public class Render
    {
        private const string BOLD = "b";
        private const string CODE = "code", COLOR = "color";
        private const string ITALIC = "i";
        private const char LEFT_BRACKET = '[';
        private const string QUOTE = "quote";
        private const char RIGHT_BRACKET = ']';
        //\'\[color={[^\]]+}\]\'
        private static readonly List<string> colorList = new List<string>(new[]
                                                                            {
                                                                          "svart", "brun", "vinröd",
                                                                          "röd", "orange", "gul",
                                                                          "oliv", "lime", "grön",
                                                                          "turkos", "ljusblå", "blå",
                                                                          "mörkblå", "lila", "rosa",
                                                                          "grå", "silver", "vit",

                                                                          "black", "brown", "crimson",
                                                                          "red", "orange", "yellow",
                                                                          "olive", "lime", "green",
                                                                          "turkose", "lightblue", "blue",
                                                                          "darkblue", "purple", "pink",
                                                                          "grey", "silver", "white"
                                                                        }).ConvertAll(
          s => HttpUtility.HtmlEncode(s));
        private static readonly List<string> replaceList = new List<string>(new[]
                                                                              {
                                                                            "000000", "663300", "800000",
                                                                            "FF0000", "FF9900", "FFFF00",
                                                                            "808000", "00FF00", "008000",
                                                                            "008080", "00FFFF", "0000FF",
                                                                            "000080", "800080", "FF00FF",
                                                                            "808080", "C0C0C0", "FFFFFF",

                                                                            "000000", "663300", "800000",
                                                                            "FF0000", "FF9900", "FFFF00",
                                                                            "808000", "00FF00", "008000",
                                                                            "008080", "00FFFF", "0000FF",
                                                                            "000080", "800080", "FF00FF",
                                                                            "808080", "C0C0C0", "FFFFFF"
                                                                          });

        private static readonly Dictionary<string, string> TagDictionary =
          GetDictionary_KeyValueOrdinal<string, string>(
              new[] { BOLD, ITALIC, QUOTE, CODE, COLOR },
              new[] { "b", "i", "blockquote", "pre", "span" });

        public static Dictionary<TKey, TValue> GetDictionary<TKey, TValue>(params object[] args)
        {
            var dic = new Dictionary<TKey, TValue>();

            for (int j = 0; j < args.Length; j++)
                dic.Add((TKey)args[j], (TValue)args[j]);
            return dic;
        }

        public static Dictionary<TKey, TValue> GetDictionary_KeyValueOrdinal<TKey, TValue>(object[] keys,
                                                                                           object[] values)
        {
            var dic = new Dictionary<TKey, TValue>();

            for (int j = 0; j < keys.Length; j++)
                dic.Add((TKey)keys[j], (TValue)values[j]);
            return dic;
        }

        public static string ReplaceBBCode(string text)
        {
            var queue = new List<string>();

            var output = new StringBuilder(text.Length + 10);
            char c;

            int i = 0;
            None:

            #region None

            int begin_none = i;
            {
                int indexOfBracket = text.IndexOf(LEFT_BRACKET, i);
                if (indexOfBracket >= 0)
                {
                    i = indexOfBracket;
                    output.Append(text.Substring(begin_none, indexOfBracket - begin_none));
                    goto LeftTMatch;
                }
                else
                    i = text.Length;
            }
            output.Append(text.Substring(begin_none, i - begin_none));
            goto End; // done;

            #endregion

            LeftTMatch:

            #region left t match

            int begin_tmatch = i;
            {
                c = text[i];
                if (c != LEFT_BRACKET) throw new Exception("c != LEFT_BRACKET");
                int indexOfBracket = text.IndexOfAny(new[] { LEFT_BRACKET, RIGHT_BRACKET }, i + 1);
                if (indexOfBracket > 0)
                {
                    char bracket = text[indexOfBracket];
                    switch (bracket)
                    {
                        case LEFT_BRACKET:
                            output.Append(text.Substring(begin_tmatch, indexOfBracket - begin_tmatch));
                            i = indexOfBracket;
                            goto LeftTMatch;
                        case RIGHT_BRACKET:
                            int idx = -1;
                            string tag = text.Substring(begin_tmatch + 1, indexOfBracket - begin_tmatch - 1);
                            string attr = string.Empty;
                            i = indexOfBracket + 1;
                            bool endTag = tag.Length > 0 && tag[0] == '/';
                            AttributeType type = AttributeType.Empty;
                            if (endTag)
                            {
                                tag = tag.Substring(1, tag.Length - 1);
                                idx = queue.LastIndexOf(tag);
                            }
                            else // begin tag only
                            {
                                //=red =>  style="color: red;"
                                //=#FF0000 =>  style="color: #FF0000;"
                                int eqIdx = tag.IndexOf('=');
                                if (eqIdx >= 0)
                                {
                                    attr = tag.Substring(eqIdx + 1, tag.Length - eqIdx - 1);
                                    type = GetAttributeType(attr);
                                    tag = tag.Substring(0, eqIdx);
                                }
                            }
                            if (TagDictionary.ContainsKey(tag) && !(endTag && idx < 0) && type != AttributeType.InValid)
                            {
                                if (endTag)
                                {
                                    for (int j = queue.Count - 1; j >= idx; j--)
                                    {
                                        output.Append("</" + TagDictionary[queue[j]] + '>');
                                    }
                                    queue.RemoveRange(idx, queue.Count - idx);
                                }
                                else
                                {
                                    queue.Add(tag);
                                    output.Append('<' + TagDictionary[tag] + RenderAttribute(tag, attr, type) + '>');
                                }
                            }
                            else
                                output.Append(text.Substring(begin_tmatch, indexOfBracket - begin_tmatch + 1));
                            break;
                        default:
                            throw new Exception("!!");
                    }
                }
                goto None;
            }

            #endregion

            End:

            for (int j = queue.Count - 1; j >= 0; j--)
            {
                output.Append("</" + TagDictionary[queue[j]] + '>');
            }
            return output.ToString();
        }

        private static AttributeType GetAttributeType(string attr)
        {
            if (attr.Length == 0) return AttributeType.Empty;
            if (attr[0] == '#')
            {
                //[A-F0-9]{6}
                if (attr.Length != 6) return AttributeType.InValid;
                bool allLettersAndDigits = true;
                for (int i = 0; i < attr.Length; i++)
                    allLettersAndDigits &= char.IsLetterOrDigit(attr[i]);

                if (!allLettersAndDigits) return AttributeType.InValid;
                return AttributeType.ColorCode;
            }
            else
            {
                //[A-Fa-f]
                bool allLetters = true;
                for (int i = 0; i < attr.Length; i++)
                    allLetters &= char.IsLetter(attr[i]);

                if (!allLetters) return AttributeType.InValid;
                int coloridx = colorList.IndexOf(attr);
                if (coloridx < 0) return AttributeType.InValid;
                return AttributeType.ColorName;
            }
        }

        private static string RenderAttribute(string tag, string attr, AttributeType type)
        {
            if (tag != COLOR) return string.Empty;

            switch (type)
            {
                case AttributeType.InValid:
                    throw new Exception();
                case AttributeType.ColorCode:
                    return " style=\"color:" + attr + "\" ";
                case AttributeType.ColorName:
                    int coloridx = colorList.IndexOf(attr);
                    if (coloridx < 0) return string.Empty;
                    attr = "#" + replaceList[coloridx];
                    return " style=\"color:" + attr + "\" ";
                case AttributeType.Empty:
                    return string.Empty;
                default:
                    throw new Exception();
            }
            //=red =>  style="color: red;"
            //=#FF0000 =>  style="color: #FF0000;"
        }
    }
}
