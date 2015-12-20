using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.VisualBasic;
using System.Collections;

namespace JieYiGuang.Common
{
    /// <summary>
    /// 字符操作
    /// </summary>
    public partial class StringHelper
    {

        #region Filter

        /// <summary>
        /// 危险标签清理
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string FilterTagsFilter(string html)
        {
            html = Regex.Replace(html, @"<[/]?\s*i\s*f\s*r\s*a\s*m\s*e[^>]*>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<[/]?\s*f\s*r\s*a\s*m\s*e\s*s\s*e\s*t[^>]*>", "", RegexOptions.IgnoreCase);

            html = Regex.Replace(html, @"<(?![/]?script)([^>]*)>",
                delegate(Match m)
                {
                    string tag = m.Value;
                    tag = Regex.Replace(tag, @"[\s-]*s\s*c\s*r\s*i\s*p\s*t", "-script", RegexOptions.IgnoreCase);
                    tag = Regex.Replace(tag, @"[\s-]*e\s*x\s*p\s*r\s*e\s*s\s*s\s*i\s*o\s*n", "-expression", RegexOptions.IgnoreCase);
                    tag = Regex.Replace(tag, @"([/\s]+)(on[^=]+=)", " _$2", RegexOptions.IgnoreCase);
                    return tag.Replace("&", "&amp;");
                }, RegexOptions.IgnoreCase);

            html = Regex.Replace(html, @"(<script[^>]*[\s\/]+)(defer)(.*>)", "$1_defer$3", RegexOptions.IgnoreCase);
            return Regex.Replace(html, @"(<[/]?)(style[^>]*>)", "$1T:$2", RegexOptions.IgnoreCase); ;
        }

        /// <summary>
        /// 过滤提交内容中的特殊标记
        /// </summary>
        /// <param name="html">提交的内容</param>
        /// <returns></returns>
        public static String FilterWipeScript(String html)
        {
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"<form[\s\S]+</form *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            regex1.Match(html);
            html = regex1.Replace(html, ""); //过滤<script></script>标记 
            html = regex4.Replace(html, ""); //过滤iframe 
            html = regex5.Replace(html, ""); //过滤frameset 
            html = regex6.Replace(html, ""); //过滤form 

            return html;
        }

        /// <summary>
        /// 过滤非法字符
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string FilterBadChar(string html)
        {
            if (string.IsNullOrEmpty(html))
                return "";
            string strBadChar, tempChar;
            string[] arrBadChar;
            strBadChar = "@@,+,',--,%,^,&,?,(,),<,>,[,],{,},/,\\,;,:,\",\"\",";
            arrBadChar = strBadChar.Split(',');
            tempChar = html;
            for (int i = 0; i < arrBadChar.Length; i++)
            {
                if (arrBadChar[i].Length > 0)
                    tempChar = tempChar.Replace(arrBadChar[i], "");
            }
            return tempChar;
        }

        /// <summary>
        /// 过滤A标签
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string FilterA(string html)
        {
            string returnStr = html;
            return Regex.Replace(returnStr, "<.?a(.|\n)*?>", "", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 过滤DIV标签
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string FilterDiv(string html)
        {
            string returnStr = html;
            return Regex.Replace(html, "<.?div(.|\n)*?>", "", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 过滤FONT标签
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string FilterFont(string html)
        {
            string returnStr = html;
            return Regex.Replace(returnStr, "<.?font(.|\n)*?>", "", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 过滤IMG标签
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string FilterImg(string html)
        {
            string returnStr = html;
            return Regex.Replace(returnStr, "<img(.|\n)*?>", "", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 过滤OBJECT标签
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string FilterObject(string html)
        {
            string pattern = @"<object((?:.|\n)*?)</object>";
            string objStr = string.Empty;
            Match m = new Regex(pattern, RegexOptions.IgnoreCase).Match(html);
            if (m.Success)
            {
                objStr = m.Value;
                html = html.Replace(objStr, "");
            }
            return html;
        }

        /// <summary>
        /// 过滤JavaScript标签
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string FilterScript(string html)
        {
            string pattern = @"<script((?:.|\n)*?)</script>";

            MatchCollection matches = Regex.Matches(html, pattern, RegexOptions.IgnoreCase);
            foreach (Match m in matches)
            {
                html = html.Replace(m.Value, "");
            }
            return html;
        }

        /// <summary>
        /// 过滤IFRAME标签
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string FilterIFrame(string html)
        {
            string pattern = @"<iframe((?:.|\n)*?)</iframe>";

            MatchCollection matches = Regex.Matches(html, pattern, RegexOptions.IgnoreCase);
            foreach (Match m in matches)
            {
                html = html.Replace(m.Value, "");
            }
            return html;
        }

        /// <summary>
        /// 过滤SPAN标签
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string FilterSpan(string html)
        {
            string returnStr = html;
            return Regex.Replace(html, "<.?span(.|\n)*?>", "", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 过滤STYLE样式标签
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string FilterStyle(string html)
        {
            string pattern = @"<style((?:.|\n)*?)</style>";
            string styleStr = string.Empty;
            Match m = new Regex(pattern, RegexOptions.IgnoreCase).Match(html);
            if (m.Success)
            {
                styleStr = m.Value;
                html = html.Replace(styleStr, "");
            }
            return html;
        }

        /// <summary>
        /// 过滤TABLE、TR、TD
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string FilterTable(string html)
        {
            string returnStr = html;
            return Regex.Replace(Regex.Replace(Regex.Replace(returnStr, "<.?table(.|\n)*?>", "", RegexOptions.IgnoreCase), "<.?tr(.|\n)*?>", "", RegexOptions.IgnoreCase), "<.?td(.|\n)*?>", "", RegexOptions.IgnoreCase);
        }



        /// <summary>
        /// 过滤HTML标签
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string FilterHtml(string html)
        {
            if (!string.IsNullOrWhiteSpace(html))
            {

                foreach (Match m in Regex.Matches(html, "<(.|\n)+?>"))
                {
                    html = html.Replace(m.Value, "");
                }
                html = html.Replace("&nbsp;", "");
                html = html.Replace("　", "");
                html = html.Replace("\t", "");
                html = html.Replace("\n", "");
                html = html.Replace("&quot;", "");
            }
            return html;
        }

        /// <summary>
        /// 根据传入的正则表达式进行过滤
        /// </summary>
        /// <param name="html"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static string FilterRegex(string html, string regex)
        {
            return Regex.Replace(html, regex, "", RegexOptions.IgnoreCase);
        }

        #endregion

        #region 中文转换



        /// <summary> 
        /// 汉字转拼音缩写 
        /// </summary> 
        /// <param name="Input">要转换的汉字字符串</param> 
        /// <returns>拼音缩写</returns> 
        public static string ToPYString(string Input)
        {
            string ret = "";
            foreach (char c in Input)
            {
                if ((int)c >= 33 && (int)c <= 126)
                {//字母和符号原样保留 
                    ret += c.ToString();
                }
                else
                {//累加拼音声母 
                    ret += ToPYChar(c.ToString());
                }
            }
            return ret;
        }

        /// <summary> 
        /// 取单个字符的拼音声母 
        /// 2004-11-30 
        /// </summary> 
        /// <param name="c">要转换的单个汉字</param> 
        /// <returns>拼音声母</returns> 
        private static string ToPYChar(string c)
        {
            byte[] array = new byte[2];
            array = System.Text.Encoding.Default.GetBytes(c);
            int i = (short)(array[0] - '\0') * 256 + ((short)(array[1] - '\0'));
            if (i < 0xB0A1) return "*";
            if (i < 0xB0C5) return "A";
            if (i < 0xB2C1) return "B";
            if (i < 0xB4EE) return "C";
            if (i < 0xB6EA) return "D";
            if (i < 0xB7A2) return "E";
            if (i < 0xB8C1) return "F";
            if (i < 0xB9FE) return "G";
            if (i < 0xBBF7) return "H";
            if (i < 0xBFA6) return "J";
            if (i < 0xC0AC) return "K";
            if (i < 0xC2E8) return "L";
            if (i < 0xC4C3) return "M";
            if (i < 0xC5B6) return "N";
            if (i < 0xC5BE) return "O";
            if (i < 0xC6DA) return "P";
            if (i < 0xC8BB) return "Q";
            if (i < 0xC8F6) return "R";
            if (i < 0xCBFA) return "S";
            if (i < 0xCDDA) return "T";
            if (i < 0xCEF4) return "W";
            if (i < 0xD1B9) return "X";
            if (i < 0xD4D1) return "Y";
            if (i < 0xD7FA) return "Z";
            return "*";
        }

        #endregion

        #region Encode

        /// <summary>
        ///  将字符转化为HTML编码
        /// </summary>
        /// <param name="str">待处理的字符串</param>
        /// <returns></returns>
        public static string HtmlEncode(object Input)
        {
            if (Input == null)
                return string.Empty;
            else
                return HttpContext.Current.Server.HtmlEncode(Input.ToString());
        }


        /// <summary>
        /// 返回 HTML 字符串的解码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string HtmlDecode(string Input)
        {
            if (Input == null || Input.Trim() == string.Empty)
                return string.Empty;
            else
                return HttpContext.Current.Server.HtmlDecode(Input);
        }



        /// <summary>
        /// 过滤特殊字符/前台会员
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string Htmls(string Input)
        {
            if (Input != string.Empty && Input != null)
            {
                string ihtml = Input.ToLower();
                ihtml = ihtml.Replace("<script", "&lt;script");
                ihtml = ihtml.Replace("script>", "script&gt;");
                ihtml = ihtml.Replace("</script", "&lt;/script");
                ihtml = ihtml.Replace("<%", "&lt;%");
                ihtml = ihtml.Replace("%>", "%&gt;");
                ihtml = ihtml.Replace("<$", "&lt;$");
                ihtml = ihtml.Replace("$>", "$&gt;");
                return ihtml;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 字符串字符处理
        /// </summary>
        /// <param name="chr">等待处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        /// //把HTML代码转换成TXT格式
        public static String ToTxt(String Input)
        {
            StringBuilder sb = new StringBuilder(Input);
            sb.Replace("&nbsp;", " ");
            sb.Replace("<br>", "\r\n");
            sb.Replace("<br>", "\n");
            sb.Replace("<br />", "\n");
            sb.Replace("<br />", "\r\n");
            sb.Replace("&lt;", "<");
            sb.Replace("&gt;", ">");
            sb.Replace("&amp;", "&");
            return sb.ToString();
        }

        /// <summary>
        /// 字符串字符处理
        /// </summary>
        /// <param name="chr">等待处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        /// //把HTML代码转换成TXT格式
        public static String ToshowTxt(String Input)
        {
            StringBuilder sb = new StringBuilder(Input);
            sb.Replace("&lt;", "<");
            sb.Replace("&gt;", ">");
            return sb.ToString();
        }

        /// <summary>
        /// 把字符转化为文本格式
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string ForTXT(string Input)
        {
            StringBuilder sb = new StringBuilder(Input);
            sb.Replace("<font", " ");
            sb.Replace("<span", " ");
            sb.Replace("<style", " ");
            sb.Replace("<div", " ");
            sb.Replace("<p", "");
            sb.Replace("</p>", "");
            sb.Replace("<label", " ");
            sb.Replace("&nbsp;", " ");
            sb.Replace("<br>", "");
            sb.Replace("<br />", "");
            sb.Replace("<br />", "");
            sb.Replace("&lt;", "");
            sb.Replace("&gt;", "");
            sb.Replace("&amp;", "");
            sb.Replace("<", "");
            sb.Replace(">", "");
            return sb.ToString();
        }

        /// <summary>
        /// 字符串字符处理
        /// </summary>
        /// <param name="chr">等待处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        /// //把TXT代码转换成HTML格式
        public static String ToHtml(string Input)
        {
            StringBuilder sb = new StringBuilder(Input);
            sb.Replace("&", "&amp;");
            sb.Replace("<", "&lt;");
            sb.Replace(">", "&gt;");
            sb.Replace("\r\n", "<br />");
            sb.Replace("\n", "<br />");
            sb.Replace("\t", " ");
            //sb.Replace(" ", "&nbsp;");
            return sb.ToString();
        }

        /// <summary>
        /// 替换XML的特殊字符
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string EncodeXml(string html)
        {
            if (!string.IsNullOrEmpty(html))
            {
                html = html.Replace("&", "&amp;");
                html = html.Replace("<", "&lt;");
                html = html.Replace(">", "&gt;");
                html = html.Replace("'", "&apos;");
                html = html.Replace("\"", "&quot;");
            }
            return html;
        }

        /// <summary>
        /// 替换JS的特殊字符
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string EncodeJs(string html)
        {
            html = html.Replace(@"\", @"\\");
            html = html.Replace("\n", @"\n");
            html = html.Replace("\r", @"\r");
            html = html.Replace("\"", "\\\"");
            return html;
        }

        /// <summary>
        /// 替换html字符
        /// </summary>
        public static string EncodeHtml(string strHtml)
        {
            if (strHtml != "")
            {
                strHtml = strHtml.Replace(",", "&def");
                strHtml = strHtml.Replace("'", "&dot");
                strHtml = strHtml.Replace(";", "&dec");
                return strHtml;
            }
            return "";
        }

        /// <summary>
        /// 替换回车换行符为html换行符
        /// </summary>
        public static string StrFormat(string str)
        {
            string str2;

            if (str == null)
            {
                str2 = "";
            }
            else
            {
                str = str.Replace("\r\n", "<br />");
                str = str.Replace("\n", "<br />");
                str2 = str;
            }
            return str2;
        }

        #endregion

        #region 内容 获得第一张图片路径
        public string GetFirstIMG(string content)
        {
            //取出图片路径
            Regex r = new Regex(@"<img.*?src=(?:""|')?(.*?\.(?:jpg|gif)).*?");
            var mc = r.Matches("这里填写你要找的HTML代码 赋值给一个字符串");
            for (int i = 0; i < mc.Count; i++)
            {
                return mc[0].Value;
            }
            return "";
        }
        #endregion

        #region 标题分解

        /// <summary>
        /// 根据新闻标题的属性设置返回设置后的标题
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="TitleColor">标题颜色</param>
        /// <param name="IsB">是否粗体</param>
        /// <param name="IsI">是否斜体</param>
        /// <param name="TitleNum">返回标题字数</param>
        /// <returns>返回设置后的标题</returns>
        public static string GetTitle(string Title, string TitleColor, bool IsB, bool IsI, int TitleNum, string LastStr)
        {
            string Return_title = "";
            string FormatTitle = "";
            if (Title != null && Title != string.Empty)
            {
                FormatTitle = FilterHtml(Title);
                FormatTitle = GetSubString(FormatTitle, TitleNum, LastStr);
                if (IsB)
                {
                    FormatTitle = "<b>" + FormatTitle + "</b>";
                }
                if (IsI)
                {
                    FormatTitle = "<i>" + FormatTitle + "</i>";
                }
                if (TitleColor != null && TitleColor != string.Empty)
                {
                    FormatTitle = "<font style=\"color:" + TitleColor + ";\">" + FormatTitle + "</font>";
                }
                Return_title = FormatTitle;
            }
            return Return_title;
        }


        /// <summary>
        /// 截取字符串函数
        /// </summary>
        /// <param name="Str">所要截取的字符串</param>
        /// <param name="length">截取字符串的长度</param>
        /// <param name="LastStr">截取字符串后省略部分的字符串</param>
        /// <returns></returns>
        public static string GetSubString(string str, int length, string LastStr)
        {
            int i = 0, j = 0;
            if (!string.IsNullOrEmpty(str))
            {
                foreach (char chr in str)
                {
                    if ((int)chr > 127)
                    {
                        i += 2;
                    }
                    else
                    {
                        i++;
                    }
                    if (i > length)
                    {
                        str = str.Substring(0, j);
                        str += LastStr;
                        break;
                    }
                    j++;
                }
            }
            return str;
        }

        /// <summary>
        /// 验证字符串是否是图片路径
        /// </summary>
        /// <param name="Input">待检测的字符串</param>
        /// <returns>返回true 或 false</returns>
        public static bool IsImgString(string Input)
        {
            return IsImgString(Input, "/{@dirfile}/");
        }

        public static bool IsImgString(string Input, string checkStr)
        {
            bool re_Val = false;
            if (Input != string.Empty)
            {
                string s_input = Input.ToLower();
                if (s_input.IndexOf(checkStr.ToLower()) != -1 && s_input.IndexOf(".") != -1)
                {
                    string Ex_Name = s_input.Substring(s_input.LastIndexOf(".") + 1).ToString().ToLower();
                    if (Ex_Name == "jpg" || Ex_Name == "gif" || Ex_Name == "bmp" || Ex_Name == "png")
                    {
                        re_Val = true;
                    }
                }
            }
            return re_Val;
        }

        /// <summary>
        /// 获得第一个图片
        /// </summary>
        /// <param name="html">输入HTML</param>
        /// <param name="img">图</param>
        /// <param name="text">文</param>
        public static string GetFirstImage(string html)
        {
            string imgFirst = string.Empty;
            if (!string.IsNullOrEmpty(html))
            {
                // 定义正则表达式用来匹配 img 标签
                Regex regex = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
                // 搜索匹配的字符串
                MatchCollection matches = regex.Matches(html);
                if (matches != null && matches.Count > 0)
                {
                    imgFirst = matches[0].Groups["imgUrl"].Value;
                }
            }
            return imgFirst;
        }

        /// <summary>
        /// 把图文分解成图，文
        /// </summary>
        /// <param name="html">输入HTML</param>
        /// <param name="img">图</param>
        /// <param name="text">文</param>
        public static void splitImgAndText(string html, out string img, out string text)
        {
            if (html == null)
            {
                img = "";
                text = "";
                return;
            }
            Regex regex = new System.Text.RegularExpressions.Regex(@"<img[\s\S]+</img *>|<img[^>]+/? *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Match m = regex.Match(html);
            string imgstr = string.Empty;
            while (m.Success)
            {
                imgstr += m.Value;
                m = m.NextMatch();
            }
            string textStr = FilterHtml(html);
            img = imgstr;
            text = textStr;
        }

        #endregion

        #region GetTitle [标题截取]
        /// <summary>
        /// 标题截取
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public string GetTitle(object o, int strLen)
        {
            string str = o.ToString();
            string i = "";
            int count = 0;

            int temp = 0;
            int charCount = 0;
            //如果字符串中包含非中文字符，那么补上相应长度
            for (int j = 0; j < str.Length; j++)
            {
                temp = System.Text.Encoding.Default.GetByteCount(str[j].ToString());
                if (charCount < 2 * strLen)
                {
                    charCount += temp;
                }
                else
                {
                    count = j;
                    break;
                }
            }

            if (str.Length > count && count > 0)
            {
                i = str.Substring(0, count);
            }
            else
            {
                i = str;
            }

            return i;
        }
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="str_value"></param>
        /// <param name="str_len"></param>
        /// <returns></returns>
        public static string leftx(string str_value, int str_len)
        {
            int p_num = 0;
            int i;
            string New_Str_value = "";

            if (str_value == "")
            {
                New_Str_value = "";
            }
            else
            {
                int Len_Num = str_value.Length;
                for (i = 0; i <= Len_Num - 1; i++)
                {
                    if (i > Len_Num) break;
                    char c = Convert.ToChar(str_value.Substring(i, 1));
                    if (((int)c > 255) || ((int)c < 0))
                        p_num = p_num + 2;
                    else
                        p_num = p_num + 1;



                    if (p_num >= str_len)
                    {

                        New_Str_value = str_value.Substring(0, i + 1);
                        break;
                    }
                    else
                    {
                        New_Str_value = str_value;
                    }

                }

            }
            return New_Str_value;
        }

        /// <summary>
        /// 返回字符串真实长度, 1个汉字长度为2
        /// </summary>
        /// <returns></returns>
        public static int GetChineseLength(string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }

        #endregion

        #region GetAreaCityName [根据邮编取得地区城市名称]
        /// <summary>
        /// 根据邮编取得地区城市名称
        /// </summary>
        /// <param name="zipcode">邮编</param>
        public static string GetAreaCityName(string zipcode)
        {
            Hashtable htPro = new Hashtable();
            htPro.Add("110000", "北京市");
            htPro.Add("120000", "天津市");
            htPro.Add("130000", "河北省");
            htPro.Add("140000", "山西省");
            htPro.Add("150000", "内蒙古自治区");
            htPro.Add("210000", "辽宁省");
            htPro.Add("220000", "吉林省");
            htPro.Add("230000", "黑龙江省");
            htPro.Add("310000", "上海市");
            htPro.Add("320000", "江苏省");
            htPro.Add("330000", "浙江省");
            htPro.Add("340000", "安徽省");
            htPro.Add("350000", "福建省");
            htPro.Add("360000", "江西省");
            htPro.Add("370000", "山东省");
            htPro.Add("410000", "河南省");
            htPro.Add("420000", "湖北省");
            htPro.Add("430000", "湖南省");
            htPro.Add("440000", "广东省");
            htPro.Add("450000", "广西壮族自治区");
            htPro.Add("460000", "海南省");
            htPro.Add("500000", "重庆市");
            htPro.Add("510000", "四川省");
            htPro.Add("520000", "贵州省");
            htPro.Add("530000", "云南省");
            htPro.Add("540000", "西藏自治区");
            htPro.Add("610000", "陕西省");
            htPro.Add("620000", "甘肃省");
            htPro.Add("630000", "青海省");
            htPro.Add("640000", "宁夏回族自治区");
            htPro.Add("650000", "新疆维吾尔自治区");
            htPro.Add("710000", "台湾省");



            htPro.Add("110100", "北京");
            htPro.Add("120100", "天津");
            htPro.Add("130101", "石家庄");
            htPro.Add("130201", "唐山");
            htPro.Add("130301", "秦皇岛");
            htPro.Add("130701", "张家口");
            htPro.Add("130801", "承德");
            htPro.Add("131001", "廊坊");
            htPro.Add("130401", "邯郸");
            htPro.Add("130501", "邢台");
            htPro.Add("130601", "保定");
            htPro.Add("130901", "沧州");
            htPro.Add("133001", "衡水");
            htPro.Add("140101", "太原");
            htPro.Add("140201", "大同");
            htPro.Add("140301", "阳泉");
            htPro.Add("140501", "晋城");
            htPro.Add("140601", "朔州");
            htPro.Add("142201", "忻州");
            htPro.Add("142331", "离石");
            htPro.Add("142401", "榆次");
            htPro.Add("142601", "临汾");
            htPro.Add("142701", "运城");
            htPro.Add("140401", "长治");
            htPro.Add("150101", "呼和浩特");
            htPro.Add("150201", "包头");
            htPro.Add("150301", "乌海");
            htPro.Add("152601", "集宁");
            htPro.Add("152701", "东胜");
            htPro.Add("152801", "临河");
            htPro.Add("152921", "阿拉善左旗");
            htPro.Add("150401", "赤峰");
            htPro.Add("152301", "通辽");
            htPro.Add("152502", "锡林浩特");
            htPro.Add("152101", "海拉尔");
            htPro.Add("152201", "乌兰浩特");
            htPro.Add("210101", "沈阳");
            htPro.Add("210201", "大连");
            htPro.Add("210301", "鞍山");
            htPro.Add("210401", "抚顺");
            htPro.Add("210501", "本溪");
            htPro.Add("210701", "锦州");
            htPro.Add("210801", "营口");
            htPro.Add("210901", "阜新");
            htPro.Add("211101", "盘锦");
            htPro.Add("211201", "铁岭");
            htPro.Add("211301", "朝阳");
            htPro.Add("211401", "锦西");
            htPro.Add("210601", "丹东");
            htPro.Add("220101", "长春");
            htPro.Add("220201", "吉林");
            htPro.Add("220301", "四平");
            htPro.Add("220401", "辽源");
            htPro.Add("220601", "浑江");
            htPro.Add("222301", "白城");
            htPro.Add("222401", "延吉");
            htPro.Add("220501", "通化");
            htPro.Add("230101", "哈尔滨");
            htPro.Add("230301", "鸡西");
            htPro.Add("230401", "鹤岗");
            htPro.Add("230501", "双鸭山");
            htPro.Add("230701", "伊春");
            htPro.Add("230801", "佳木斯");
            htPro.Add("230901", "七台河");
            htPro.Add("231001", "牡丹江");
            htPro.Add("232301", "绥化");
            htPro.Add("230201", "齐齐哈尔");
            htPro.Add("230601", "大庆");
            htPro.Add("232601", "黑河");
            htPro.Add("232700", "加格达奇");
            htPro.Add("310100", "上海");
            htPro.Add("320101", "南京");
            htPro.Add("320201", "无锡");
            htPro.Add("320301", "徐州");
            htPro.Add("320401", "常州");
            htPro.Add("320501", "苏州");
            htPro.Add("320600", "南通");
            htPro.Add("320701", "连云港");
            htPro.Add("320801", "淮阴");
            htPro.Add("320901", "盐城");
            htPro.Add("321001", "扬州");
            htPro.Add("321101", "镇江");
            htPro.Add("330101", "杭州");
            htPro.Add("330201", "宁波");
            htPro.Add("330301", "温州");
            htPro.Add("330401", "嘉兴");
            htPro.Add("330501", "湖州");
            htPro.Add("330601", "绍兴");
            htPro.Add("330701", "金华");
            htPro.Add("330801", "衢州");
            htPro.Add("330901", "舟山");
            htPro.Add("332501", "丽水");
            htPro.Add("332602", "临海");
            htPro.Add("340101", "合肥");
            htPro.Add("340201", "芜湖");
            htPro.Add("340301", "蚌埠");
            htPro.Add("340401", "淮南");
            htPro.Add("340501", "马鞍山");
            htPro.Add("340601", "淮北");
            htPro.Add("340701", "铜陵");
            htPro.Add("340801", "安庆");
            htPro.Add("341001", "黄山");
            htPro.Add("342101", "阜阳");
            htPro.Add("342201", "宿州");
            htPro.Add("342301", "滁州");
            htPro.Add("342401", "六安");
            htPro.Add("342501", "宣州");
            htPro.Add("342601", "巢湖");
            htPro.Add("342901", "贵池");
            htPro.Add("350101", "福州");
            htPro.Add("350201", "厦门");
            htPro.Add("350301", "莆田");
            htPro.Add("350401", "三明");
            htPro.Add("350501", "泉州");
            htPro.Add("350601", "漳州");
            htPro.Add("352101", "南平");
            htPro.Add("352201", "宁德");
            htPro.Add("352601", "龙岩");
            htPro.Add("360101", "南昌");
            htPro.Add("360201", "景德镇");
            htPro.Add("362101", "赣州");
            htPro.Add("360301", "萍乡");
            htPro.Add("360401", "九江");
            htPro.Add("360501", "新余");
            htPro.Add("360601", "鹰潭");
            htPro.Add("362201", "宜春");
            htPro.Add("362301", "上饶");
            htPro.Add("362401", "吉安");
            htPro.Add("362502", "临川");
            htPro.Add("370101", "济南");
            htPro.Add("370201", "青岛");
            htPro.Add("370301", "淄博");
            htPro.Add("370401", "枣庄");
            htPro.Add("370501", "东营");
            htPro.Add("370601", "烟台");
            htPro.Add("370701", "潍坊");
            htPro.Add("370801", "济宁");
            htPro.Add("370901", "泰安");
            htPro.Add("371001", "威海");
            htPro.Add("371100", "日照");
            htPro.Add("372301", "滨州");
            htPro.Add("372401", "德州");
            htPro.Add("372501", "聊城");
            htPro.Add("372801", "临沂");
            htPro.Add("372901", "菏泽");
            htPro.Add("410101", "郑州");
            htPro.Add("410201", "开封");
            htPro.Add("410301", "洛阳");
            htPro.Add("410401", "平顶山");
            htPro.Add("410501", "安阳");
            htPro.Add("410601", "鹤壁");
            htPro.Add("410701", "新乡");
            htPro.Add("410801", "焦作");
            htPro.Add("410901", "濮阳");
            htPro.Add("411001", "许昌");
            htPro.Add("411101", "漯河");
            htPro.Add("411201", "三门峡");
            htPro.Add("412301", "商丘");
            htPro.Add("412701", "周口");
            htPro.Add("412801", "驻马店");
            htPro.Add("412901", "南阳");
            htPro.Add("413001", "信阳");
            htPro.Add("420101", "武汉");
            htPro.Add("420201", "黄石");
            htPro.Add("420301", "十堰");
            htPro.Add("420400", "沙市");
            htPro.Add("420501", "宜昌");
            htPro.Add("420601", "襄樊");
            htPro.Add("420701", "鄂州");
            htPro.Add("420801", "荆门");
            htPro.Add("422103", "黄州");
            htPro.Add("422201", "孝感");
            htPro.Add("422301", "咸宁");
            htPro.Add("422421", "江陵");
            htPro.Add("422801", "恩施");
            htPro.Add("430101", "长沙");
            htPro.Add("430401", "衡阳");
            htPro.Add("430501", "邵阳");
            htPro.Add("432801", "郴州");
            htPro.Add("432901", "永州");
            htPro.Add("430801", "大庸");
            htPro.Add("433001", "怀化");
            htPro.Add("433101", "吉首");
            htPro.Add("430201", "株洲");
            htPro.Add("430301", "湘潭");
            htPro.Add("430601", "岳阳");
            htPro.Add("430701", "常德");
            htPro.Add("432301", "益阳");
            htPro.Add("432501", "娄底");
            htPro.Add("440101", "广州");
            htPro.Add("440301", "深圳");
            htPro.Add("441501", "汕尾");
            htPro.Add("441301", "惠州");
            htPro.Add("441601", "河源");
            htPro.Add("440601", "佛山");
            htPro.Add("441801", "清远");
            htPro.Add("441901", "东莞");
            htPro.Add("440401", "珠海");
            htPro.Add("440701", "江门");
            htPro.Add("441201", "肇庆");
            htPro.Add("442001", "中山");
            htPro.Add("440801", "湛江");
            htPro.Add("440901", "茂名");
            htPro.Add("440201", "韶关");
            htPro.Add("440501", "汕头");
            htPro.Add("441401", "梅州");
            htPro.Add("441701", "阳江");
            htPro.Add("450101", "南宁");
            htPro.Add("450401", "梧州");
            htPro.Add("452501", "玉林");
            htPro.Add("450301", "桂林");
            htPro.Add("452601", "百色");
            htPro.Add("452701", "河池");
            htPro.Add("452802", "钦州");
            htPro.Add("450201", "柳州");
            htPro.Add("450501", "北海");
            htPro.Add("460100", "海口");
            htPro.Add("460200", "三亚");
            htPro.Add("510101", "成都");
            htPro.Add("513321", "康定");
            htPro.Add("513101", "雅安");
            htPro.Add("513229", "马尔康");
            htPro.Add("510301", "自贡");
            htPro.Add("500100", "重庆");
            htPro.Add("512901", "南充");
            htPro.Add("510501", "泸州");
            htPro.Add("510601", "德阳");
            htPro.Add("510701", "绵阳");
            htPro.Add("510901", "遂宁");
            htPro.Add("511001", "内江");
            htPro.Add("511101", "乐山");
            htPro.Add("512501", "宜宾");
            htPro.Add("510801", "广元");
            htPro.Add("513021", "达县");
            htPro.Add("513401", "西昌");
            htPro.Add("510401", "攀枝花");
            htPro.Add("500239", "黔江土家族苗族自治县");
            htPro.Add("520101", "贵阳");
            htPro.Add("520200", "六盘水");
            htPro.Add("522201", "铜仁");
            htPro.Add("522501", "安顺");
            htPro.Add("522601", "凯里");
            htPro.Add("522701", "都匀");
            htPro.Add("522301", "兴义");
            htPro.Add("522421", "毕节");
            htPro.Add("522101", "遵义");
            htPro.Add("530101", "昆明");
            htPro.Add("530201", "东川");
            htPro.Add("532201", "曲靖");
            htPro.Add("532301", "楚雄");
            htPro.Add("532401", "玉溪");
            htPro.Add("532501", "个旧");
            htPro.Add("532621", "文山");
            htPro.Add("532721", "思茅");
            htPro.Add("532101", "昭通");
            htPro.Add("532821", "景洪");
            htPro.Add("532901", "大理");
            htPro.Add("533001", "保山");
            htPro.Add("533121", "潞西");
            htPro.Add("533221", "丽江纳西族自治县");
            htPro.Add("533321", "泸水");
            htPro.Add("533421", "中甸");
            htPro.Add("533521", "临沧");
            htPro.Add("540101", "拉萨");
            htPro.Add("542121", "昌都");
            htPro.Add("542221", "乃东");
            htPro.Add("542301", "日喀则");
            htPro.Add("542421", "那曲");
            htPro.Add("542523", "噶尔");
            htPro.Add("542621", "林芝");
            htPro.Add("610101", "西安");
            htPro.Add("610201", "铜川");
            htPro.Add("610301", "宝鸡");
            htPro.Add("610401", "咸阳");
            htPro.Add("612101", "渭南");
            htPro.Add("612301", "汉中");
            htPro.Add("612401", "安康");
            htPro.Add("612501", "商州");
            htPro.Add("612601", "延安");
            htPro.Add("612701", "榆林");
            htPro.Add("620101", "兰州");
            htPro.Add("620401", "白银");
            htPro.Add("620301", "金昌");
            htPro.Add("620501", "天水");
            htPro.Add("622201", "张掖");
            htPro.Add("622301", "武威");
            htPro.Add("622421", "定西");
            htPro.Add("622624", "成县");
            htPro.Add("622701", "平凉");
            htPro.Add("622801", "西峰");
            htPro.Add("622901", "临夏");
            htPro.Add("623027", "夏河");
            htPro.Add("620201", "嘉峪关");
            htPro.Add("622102", "酒泉");
            htPro.Add("630100", "西宁");
            htPro.Add("632121", "平安");
            htPro.Add("632221", "门源回族自治县");
            htPro.Add("632321", "同仁");
            htPro.Add("632521", "共和");
            htPro.Add("632621", "玛沁");
            htPro.Add("632721", "玉树");
            htPro.Add("632802", "德令哈");
            htPro.Add("640101", "银川");
            htPro.Add("640201", "石嘴山");
            htPro.Add("642101", "吴忠");
            htPro.Add("642221", "固原");
            htPro.Add("650101", "乌鲁木齐");
            htPro.Add("650201", "克拉玛依");
            htPro.Add("652101", "吐鲁番");
            htPro.Add("652201", "哈密");
            htPro.Add("652301", "昌吉");
            htPro.Add("652701", "博乐");
            htPro.Add("652801", "库尔勒");
            htPro.Add("652901", "阿克苏");
            htPro.Add("653001", "阿图什");
            htPro.Add("653101", "喀什");
            htPro.Add("654101", "伊宁");
            htPro.Add("710001", "台北");
            htPro.Add("710002", "基隆");
            htPro.Add("710020", "台南");
            htPro.Add("710019", "高雄");
            htPro.Add("710008", "台中");
            htPro.Add("211001", "辽阳");
            htPro.Add("653201", "和田");
            htPro.Add("542200", "泽当镇");
            htPro.Add("542600", "八一镇");
            htPro.Add("820000", "澳门");
            htPro.Add("810000", "香港");

            if (htPro[zipcode] != null)
            {
                return htPro[zipcode].ToString();
            }
            return "";
        }

        #endregion

        ///<summary>
        ///生成随机字符串
        ///</summary>
        ///<returns>指定长度的随机字符串</returns>
        public static string GetRnd()
        {
            int length = 16;
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string str = "";
            Random rad = new Random();
            for (int i = 0; i < length; i++)
            {
                str += chars.Substring(rad.Next(0, chars.Length - 1), 1);
            }
            return str; 
        }
        public static string StuffMobile(string mobile)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(mobile) && mobile.Length == 11)
                {
                    return mobile.Remove(3, 8) + "****" + mobile.Substring(7);
                }
                else
                {
                    return mobile.Remove(7, 6) + "****" + mobile.Substring(11);
                }
            }
            catch (Exception ex)
            {
                return mobile;
            }
        }
    }
}
