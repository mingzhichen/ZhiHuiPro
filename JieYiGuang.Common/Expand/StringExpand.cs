using JieYiGuang.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace System
{
    //String 扩展方法
    public static class StringExpand
    {

        #region[标题截取]
        /// <summary>
        /// 截取并省略字符串
        /// </summary>
        /// <param name="length">截取长度</param>
        /// <param name="lastStr">超过截取长度显示的字符</param>
        /// <returns></returns>
        public static string SubstringEllipsis(this string str, int length, string lastStr)
        {
            return GetTitleAttribute(str, null, false, false, length, lastStr);
        }

        /// <summary>
        /// 截取并显示属性
        /// </summary>
        /// <param name="length">截取长度</param>
        /// <param name="lastStr">超过截取长度显示的字符</param>
        /// <param name="Color">颜色值 #FFFFFF</param>
        /// <param name="isB">是否粗体</param>
        /// <param name="isI">是否斜体</param>
        /// <returns></returns>
        public static string SubstringEllipsisAttribute(this String str, int length, string lastStr, string Color, bool isB, bool isI)
        {

            return GetTitleAttribute(str, Color, isB, isI, length, lastStr);
        }

        public static string GetTitleAttribute(string title, string color, bool isB, bool isI, int length, string lastStr)
        {
            var FormatTitle = string.Empty;
            if (string.IsNullOrEmpty(title))
            {
                return null;
            }

            title = title.Trim();
            FormatTitle = GetSubString(title, length);

            if (title.ToString().Length > FormatTitle.Length)
            {
                FormatTitle += lastStr;
            }
            if (isB)
            {
                FormatTitle = "<b>" + FormatTitle + "</b>";
            }
            if (isI)
            {
                FormatTitle = "<i>" + FormatTitle + "</i>";
            }
            if (!string.IsNullOrEmpty(color))
            {
                FormatTitle = string.Format("<font style=\"color:{0}\">{1}</font>", color, FormatTitle);
            }
            return FormatTitle;
        }

        /// <summary>
        /// 获取指定字符串从 0 开始指定长度的字符。单个字符若是双字节长度算 1 若为单字节长度算 0.5。
        /// </summary>
        /// <param name="input">需要获取的字符串</param>
        /// <param name="count">需要获取的长度</param>
        /// <returns></returns>
        private static string GetSubString(string input, int count)
        {
            int outputCount = 0;
            bool appendCount = false; // 是否递增长度
            string output = "";

            foreach (Char c in input.ToCharArray())
            {
                if (outputCount >= count)
                {
                    break;
                }
                if (Regex.IsMatch(c.ToString(), @"[^\x00-\xff]"))
                {
                    outputCount++; // 双字节算长度 1 
                }
                else
                {
                    // 单字节算长度 0.5
                    if (!appendCount)
                    {
                        appendCount = true;
                    }
                    else
                    {
                        appendCount = false;
                        outputCount++;
                    }
                }
                output += c.ToString();
            }
            return output;
        }
        #endregion

        #region[标题截取]
        /// <summary>
        /// 截取并省略字符串
        /// </summary>
        /// <param name="length">截取长度</param>
        /// <param name="lastStr">超过截取长度显示的字符</param>
        /// <returns></returns>
        public static string Color(this string str, string color)
        {
            if (!string.IsNullOrEmpty(color))
            {
                str = string.Format("<span style=\"color:{0};\"></span>", color, str);
            }
            return str;
        }
        #endregion

        #region 移除HTML代码
        /// <summary>
        /// 移除HTML代码
        /// </summary>
        /// <param name="Htmlstring">要移除的字符串</param>
        /// <returns></returns>
        public static string RemoveHTML(this string Htmlstring)
        {
            if (!string.IsNullOrEmpty(Htmlstring))
            {
                //删除脚本
                Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
                //删除HTML
                Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
                //Htmlstring = Regex.Replace(Htmlstring,@"<A>.*</A>","");
                //Htmlstring = Regex.Replace(Htmlstring,@"<[a-zA-Z]*=\.[a-zA-Z]*\?[a-zA-Z]+=\d&\w=%[a-zA-Z]*|[A-Z0-9]","");
                Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
                Htmlstring.Replace("<", "");
                Htmlstring.Replace(">", "");
                Htmlstring.Replace("\r\n", "");
                Htmlstring.Replace(" ", "");
                Htmlstring.Replace("　", "");
                //Htmlstring=HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            }
            return Htmlstring;
        }
        #endregion

        #region ValueFilter 值安全验证
        /// <summary>
        /// 值安全验证
        /// </summary>
        /// <param name="defaultStr">默认值</param>
        /// <returns></returns>
        public static string ValueFilter(this object str)
        {
            var value = string.Empty;
            value = str.ToString().Trim();
            if (str == null || string.IsNullOrEmpty(str.ToString()))
            {
                return value;
            }

            value = JieYiGuang.Common.StringHelper.FilterBadChar(value);
            value = JieYiGuang.Common.StringHelper.FilterWipeScript(value);
            value = JieYiGuang.Common.StringHelper.FilterTagsFilter(value);
            return value;

        }
        #endregion

        #region ToJoinString 将字符串数组用一个符号连接起来

        /// <summary>
        /// 将字符串数组用一个符号连接起来
        /// </summary>
        /// <param name="items">数组</param>
        /// <param name="joinFlag">符合，比如中文的逗号“，”</param>
        /// <returns></returns>
        public static string ToJoinString(this string[] items, string joinFlag)
        {
            if (items == null || items.Length == 0)
            {
                return string.Empty;
            }
            System.Text.StringBuilder result = new System.Text.StringBuilder();
            int i = 1;
            int itemLength = items.Length;
            foreach (var item in items)
            {
                result.Append(item);
                if (!string.IsNullOrEmpty(joinFlag) && i < itemLength)
                {
                    result.Append(joinFlag);
                }
                i++;
            }
            return result.ToString();
        }

        /// <summary>
        /// 将字符串数组用一个符号连接起来
        /// </summary>
        /// <param name="items">数组</param>
        /// <param name="joinFlag">符合，比如中文的逗号“，”</param>
        /// <returns></returns>
        public static string ToJoinString(this int[] items, string joinFlag)
        {
            if (items == null || items.Length == 0)
            {
                return string.Empty;
            }
            System.Text.StringBuilder result = new System.Text.StringBuilder();
            int i = 1;
            int itemLength = items.Length;
            foreach (var item in items)
            {
                result.Append(item);
                if (!string.IsNullOrEmpty(joinFlag) && i < itemLength)
                {
                    result.Append(joinFlag);
                }
                i++;
            }
            return result.ToString();
        }
        #endregion

        #region TrimList 组装属性列    ,10,12,

        public static string TrimList(this string str)
        {
            return TrimList(str, ',');
        }

        public static string TrimList(this string str, char joinFlag)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                return joinFlag + str.Trim().TrimStart(joinFlag).TrimEnd(joinFlag) + joinFlag;
            }
            else
            {
                return "";
            }

        }
        #endregion

        #region String型类型转换

        /// <summary>
        /// string型转换为int型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(this string strValue, int defValue)
        {
            try
            {
                if (!string.IsNullOrEmpty(strValue))
                {
                    return defValue;
                }
                else
                {
                    return Convert.ToInt32(strValue);
                }
            }
            catch
            {
                return defValue;
            }
        }

        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static Decimal StrToDecimal(this string strValue, decimal defValue)
        {
            try
            {
                if (!string.IsNullOrEmpty(strValue))
                {
                    return defValue;
                }
                else
                {
                    return Convert.ToDecimal(strValue);
                }
            }
            catch
            {
                return defValue;
            }
        }


        /// <summary>
        /// string型转换为时间型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的时间类型结果</returns>
        public static DateTime StrToDateTime(this string strValue, DateTime defValue)
        {
            try
            {
                if (!string.IsNullOrEmpty(strValue))
                {
                    return defValue;
                }
                else
                {
                    return Convert.ToDateTime(strValue);
                }
            }
            catch
            {
                return defValue;
            }
        }
        #endregion

        #region String型为空默认值 string
        /// <summary>
        /// 判断字符为空(默认值)
        /// </summary>
        /// <param name="defaultStr">默认值</param>
        /// <returns></returns>
        public static string DefaultIsNullOrEmpty(this object str, string defaultStr)
        {
            try
            {
                if (str == null || string.IsNullOrEmpty(str.ToString()))
                {
                    return defaultStr;
                }
                return StringHelper.FilterTagsFilter(str.ToString().Trim());
            }
            catch (Exception ex)
            {
                return defaultStr;
            }

        }
        #endregion

        #region String型为空默认值 int
        /// <summary>
        /// 判断字符为空(默认值)
        /// </summary>
        /// <param name="defaultStr">默认值</param>
        /// <returns></returns>
        public static int DefaultIsNullOrEmpty(this object str, int defaultStr)
        {
            try
            {
                if (str == null || string.IsNullOrEmpty(str.ToString()))
                {
                    return defaultStr;
                }
                return Convert.ToInt32(str.ToString());
            }
            catch (Exception ex)
            {
                return defaultStr;
            }

        }
        #endregion

        #region String型为空默认值 decimal
        /// <summary>
        /// 判断字符为空(默认值)
        /// </summary>
        /// <param name="defaultStr">默认值</param>
        /// <returns></returns>
        public static decimal DefaultIsNullOrEmpty(this object str, decimal defaultStr)
        {
            try
            {
                if (str == null || string.IsNullOrEmpty(str.ToString()))
                {
                    return defaultStr;
                }
                return Convert.ToDecimal(str.ToString());
            }
            catch (Exception ex)
            {
                return defaultStr;
            }

        }
        #endregion

        #region String型为空默认值 float
        /// <summary>
        /// 判断字符为空(默认值)
        /// </summary>
        /// <param name="defaultStr">默认值</param>
        /// <returns></returns>
        public static float DefaultIsNullOrEmpty(this object str, float defaultStr)
        {
            try
            {
                if (str == null || string.IsNullOrEmpty(str.ToString()))
                {
                    return defaultStr;
                }
                return float.Parse(str.ToString());
            }
            catch (Exception ex)
            {
                return defaultStr;
            }

        }
        #endregion

        #region String型为空默认值 bool
        /// <summary>
        /// 判断字符为空(默认值)
        /// </summary>
        /// <param name="defaultStr">默认值</param>
        /// <returns></returns>
        public static bool DefaultIsNullOrEmpty(this object str, bool defaultStr)
        {
            try
            {
                if (str == null || string.IsNullOrEmpty(str.ToString()))
                {
                    return defaultStr;
                }
                return Convert.ToBoolean(str);
            }
            catch (Exception ex)
            {
                return defaultStr;
            }
        }
        #endregion

        #region String型为空默认值 DateTime
        /// <summary>
        /// 判断字符为空(默认值)
        /// </summary>
        /// <param name="defaultStr">默认值</param>
        /// <returns></returns>
        public static DateTime DefaultIsNullOrEmpty(this object str, DateTime defaultStr)
        {
            try
            {
                if (str == null || string.IsNullOrEmpty(str.ToString()))
                {
                    return defaultStr;
                }
                return Convert.ToDateTime(str);
            }
            catch (Exception ex)
            {
                return defaultStr;
            }

        }
        #endregion

        #region String型为空默认值 Guid
        /// <summary>
        /// 判断字符为空(默认值)
        /// </summary>
        /// <param name="defaultStr">默认值</param>
        /// <returns></returns>
        public static Guid DefaultIsNullOrEmpty(this object str, Guid defaultStr)
        {
            try
            {
                if (str == null || string.IsNullOrEmpty(str.ToString()))
                {
                    return defaultStr;
                }
                return Guid.Parse(str.ToString().Trim());
            }
            catch (Exception ex)
            {
                return defaultStr;
            }

        }
        #endregion
    }
}
