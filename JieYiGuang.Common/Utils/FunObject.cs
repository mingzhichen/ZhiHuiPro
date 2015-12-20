/**********************************************************************************
 * 
 * 功能说明:常用函数基类
 * 版本:V0.1(C#2.0);时间:2006-8-13
 * 
 * *******************************************************************************/
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace JieYiGuang.Common.Helper
{
    /// <summary>
    /// 常用函数基类
    /// </summary>
    public class FunObject
    {
        #region 替换字符串
        /// <summary>
        /// 功能:替换字符
        /// </summary>
        /// <param name="strVAlue">字符串</param>
        /// <returns>替换掉'的字符串</returns>
        public static string FilterSQL(string strVAlue)
        {
            string str = "";
            str = strVAlue.Replace("''", "");
            return str;
        }

        /// <summary>
        /// 功能:对表 表单内容进行转换HTML操作,
        /// </summary>
        /// <param name="fString">html字符串</param>
        /// <returns></returns>
        public static string HtmlDiscode(string theString)
        {
            if (theString != "")
            {
                theString = theString.Replace("&gt;", ">");
                theString = theString.Replace("&lt;", "<");
                theString = theString.Replace("&nbsp;", " ");
                theString = theString.Replace(" &nbsp;", "  ");
                theString = theString.Replace("&quot;", "\"");
                theString = theString.Replace("&#39;", "\'");
                theString = theString.Replace("<br/> ", "\n");
                return theString;
            }
            else
                return string.Empty;
        }

        public static string HtmlEncode(string theString)
        {
            if (theString != "")
            {
                theString = theString.Replace("\r", " ");
                theString = theString.Replace(">", "&gt;");
                theString = theString.Replace("<", "&lt;");
                theString = theString.Replace("  ", " &nbsp;");
                theString = theString.Replace("  ", " &nbsp;");
                theString = theString.Replace("\"", "&quot;");
                theString = theString.Replace("\'", "&#39;");
                theString = theString.Replace("\n", "<br/> ");
                return theString;
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// 判断是否:返回值：√ or ×
        /// </summary>
        /// <param name="b">true 或false</param>
        /// <returns>√ or ×</returns>
        public static string Judgement(bool b)
        {
            string s = "";
            if (b == true)
                s = "<b><font color=#009900>√</font></b>";
            else
                s = "<b><font color=#FF0000>×</font></b>";
            return s;
        }
        #endregion

        #region 截取字符串
        /// <summary>
        /// 功能:截取字符串长度
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="length">字符串长度</param>
        /// <param name="flg">true:加...,flase:不加</param>
        /// <returns></returns>
        public static string GetString(string str, int length, bool flg)
        {
            int i = 0, j = 0;
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
                    if (flg)
                        str += "...";
                    break;
                }
                j++;
            }
            return str;
        }
        #endregion

        #region 字符串分解
        /// <summary>
        /// 分解字符串为数组1
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="index"></param>
        /// <param name="Separ"></param>
        /// <returns></returns>
        public static string StringSplit(string strings, int index, string Separ)
        {
            string[] s = strings.Split(char.Parse(Separ));
            return s[index];
        }

        /// <summary>
        /// 字符串分函数2
        /// </summary>
        /// <param name="str">要分解的字符串</param>
        /// <param name="splitstr">分割符,可以为string类型</param>
        /// <returns>字符数组</returns>
        public static string[] StringSplit(string str, string splitstr)
        {
            if (splitstr != "" && splitstr != null && str != "" && str!=null)
            {
                System.Collections.ArrayList c = new System.Collections.ArrayList();
                while (true)
                {
                    int thissplitindex = str.IndexOf(splitstr);
                    if (thissplitindex >= 0)
                    {
                        c.Add(str.Substring(0, thissplitindex));
                        str = str.Substring(thissplitindex + splitstr.Length);
                    }
                    else
                    {
                        c.Add(str);
                        break;
                    }
                }
                string[] d = new string[c.Count];
                for (int i = 0; i < c.Count; i++)
                {
                    d[i] = c[i].ToString();
                }
                return d;
            }
            else
            {
                return new string[] { str };
            }
        }

        /// <summary>
        /// 采用递归将字符串分割成数组
        /// </summary>
        /// <param name="strSource"></param>
        /// <param name="strSplit"></param>
        /// <param name="attachArray"></param>
        /// <returns></returns>
        private string[] StringSplit(string strSource, string strSplit, string[] attachArray)
        {
            string[] strtmp = new string[attachArray.Length + 1];
            attachArray.CopyTo(strtmp, 0);

            int index = strSource.IndexOf(strSplit, 0);
            if (index < 0)
            {
                strtmp[attachArray.Length] = strSource;
                return strtmp;
            }
            else
            {
                strtmp[attachArray.Length] = strSource.Substring(0, index);
                return StringSplit(strSource.Substring(index + strSplit.Length), strSplit, strtmp);
            }
        }
        #endregion

        #region URL编码
        /// <summary>
        /// URL编码
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string UrlEncoding(string str)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            return System.Text.Encoding.UTF8.GetString(bytes).ToString();
        }
        #endregion

        /// <summary>
        /// 清除html
        /// </summary>
        /// <returns></returns>
        public static string DealHtml(string str)
        {
            str = Regex.Replace(str, @"\<(img)[^>]*>|<\/(img)>", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"\<(table|tbody|tr|td|th|)[^>]*>|<\/(table|tbody|tr|td|th|)>", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"\<(div|blockquote|fieldset|legend)[^>]*>|<\/(div|blockquote|fieldset|legend)>", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"\<(font|i|u|h[1-9]|s)[^>]*>|<\/(font|i|u|h[1-9]|s)>", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"\<(style|strong)[^>]*>|<\/(style|strong)>", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"\<a[^>]*>|<\/a>", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"\<(meta|iframe|frame|span|tbody|layer)[^>]*>|<\/(iframe|frame|meta|span|tbody|layer)>", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"\<a[^>]*", "", RegexOptions.IgnoreCase);
            return str;
        }

        /// <summary>
        /// 判断一个字符串是否是时间格式
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public bool IsDatetime(string strValue)
        {
            string strReg = @"([1-2][0-9][0-9][0-9])-(0*[1-9]|1[0-2])-(0*[1-9]|[12][0-9]|3[01])\ (0*[0-9]|1[0-9]|2[0-3]):(0*[0-9]|[1-5][0-9]):(0[0-9]|[1-5][0-9])";
            if (strValue == "")
            {
                return false;
            }
            else
            {
                Regex re = new Regex(strReg);
                MatchCollection mc = re.Matches(strValue);
                if (mc.Count == 1)
                    foreach (Match m in mc)
                    {
                        if (m.Value == strValue)
                            return true;
                    }
            }
            return false;
        }

        /// <summary>
        /// 检查一个字符串是否可以转化为日期，一般用于验证用户输入日期的合法性。
        /// </summary>
        /// <param name="_value">需验证的字符串。</param>
        /// <returns>是否可以转化为日期的bool值。</returns>
        public static bool IsStringDate(string _value)
        {
            DateTime dt;
            try
            {
                dt = DateTime.Parse(_value);
            }
            catch (FormatException e)
            {
                //日期格式不正确时
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检查一个字符串是否是纯数字构成的，一般用于查询字符串参数的有效性验证。
        /// </summary>
        /// <param name="_value">需验证的字符串。。</param>
        /// <returns>是否合法的bool值。</returns>
        public static bool IsNumberId(string _value)
        {
            return FunObject.QuickValidate("^[1-9]*[0-9]*$", _value);
        }

        /// <summary>
        /// 检查一个字符串是否是纯字母和数字构成的，一般用于查询字符串参数的有效性验证。
        /// </summary>
        /// <param name="_value">需验证的字符串。</param>
        /// <returns>是否合法的bool值。</returns>
        public static bool IsLetterOrNumber(string _value)
        {
            return FunObject.QuickValidate("^[a-zA-Z0-9_]*$", _value);
        }

        /// <summary>
        /// 判断是否是数字，包括小数和整数。
        /// </summary>
        /// <param name="_value">需验证的字符串。</param>
        /// <returns>是否合法的bool值。</returns>
        public static bool IsNumber(string _value)
        {
            return FunObject.QuickValidate("^(0|([1-9]+[0-9]*))(.[0-9]+)?$", _value);
        }

        /// <summary>
        /// 快速验证一个字符串是否符合指定的正则表达式。
        /// </summary>
        /// <param name="_express">正则表达式的内容。</param>
        /// <param name="_value">需验证的字符串。</param>
        /// <returns>是否合法的bool值。</returns>
        public static bool QuickValidate(string _express, string _value)
        {
            System.Text.RegularExpressions.Regex myRegex = new System.Text.RegularExpressions.Regex(_express);
            if (_value.Length == 0)
            {
                return false;
            }
            return myRegex.IsMatch(_value);
        }

        /// <summary>
        /// 判断是否是电子邮件
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsEmail(string email)
        {
            string strExp = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            // Create a new Regex object.
            Regex r = new Regex(strExp);
            // Find a single match in the string.
            Match m = r.Match(email);
            if (m.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否手机号码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsMobileNum(string str)
        {
            return FunObject.QuickValidate("^0{0,1}1[3|4|5|6|7|8|9][0-9]{9}$", str);
        }

        /// <summary>
        /// 对时间进行格式化，如：2007-1-15,2007/5/2
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="geshi">如：y-m-d；y/m/d；y-m-d h:mm:ss；m-d-y；m/d/y</param>
        /// <param name="spstr">分隔符号，如：-，/</param>
        /// <returns></returns>
        public static string DateString(DateTime dt, string geshi, string spstr)
        {
            string str = "";
            string y, m, d, h, mm, ss;
            y = dt.Year.ToString();
            m = dt.Month.ToString();
            if (m.Length < 2) m = "0" + m;
            d = dt.Day.ToString();
            if (d.Length < 2) d = "0" + d;
            h = dt.Hour.ToString();
            if (h.Length < 2) h = "0" + h;
            mm = dt.Minute.ToString();
            if (mm.Length < 2) mm = "0" + mm;
            ss = dt.Second.ToString();
            if (ss.Length < 2) ss = "0" + ss;

            if (geshi == "y-m-d")
            {
                str = y + spstr + m + spstr + d;
            }
            else if (geshi == "y-m-d h:mm:ss")
            {
                str = y + spstr + m + spstr + d + " " + h + ":" + mm + ":" + ss;
            }
            else if (geshi == "m-d-y")
            {
                str = m + spstr + d + spstr + y;
            }
            else if (geshi == "d-m-y")
            {
                str = d + spstr + m + spstr + y;
            }
            else if (geshi == "yy-mm-dd")
            {
                str = y.Substring(2) + spstr + m + spstr + d + " " + h + ":" + mm + ":" + ss;
            }
            else
            {
                str = DateTime.Now.ToString();
            }
            return str;
        }

        /// <summary>
        /// 对指定字符串进行md5散列加密
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetMd5(string s)
        {
            //下面两种加密方式选其1
            //md5加密
            s = FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5");
            //sha1加密
            //s=FormsAuthentication.HashPasswordForStoringInConfigFile(s, "sha1");
            return s.Substring(0, 16);
        }

        /// <summary>
        /// 替换sql语句中的有问题符号
        /// </summary>
        public static string ChkSQL(string str)
        {
            string str2;

            if (str == null)
            {
                str2 = "";
            }
            else
            {
                str = str.Replace("'", "''");
                str = str.Replace("&", "&#x26;");
                str = str.Replace("<", "&#x3C;");
                str = str.Replace(">", "&#x3E;");
                str = str.Replace(" ", ""); //过滤TAB键
                str = str.Replace(" ", ""); //过滤空格键

                Regex myConditonReg;
                myConditonReg = new Regex("javascript:", RegexOptions.IgnoreCase);
                myConditonReg.Replace(str, "javascript：");
                myConditonReg = new Regex("jscript:", RegexOptions.IgnoreCase);
                myConditonReg.Replace(str, "jscript：");
                myConditonReg = new Regex("vbscript:", RegexOptions.IgnoreCase);
                myConditonReg.Replace(str, "vbscript：");

                str2 = str;
            }
            return str2;
        }

        /// <summary>
        /// 返回Request对象返回值
        /// </summary>
        public static string AffPara(string sort, string Debar)
        {
            string returnstr = "";
            if (sort == "11")
            {
                foreach (string key in System.Web.HttpContext.Current.Request.QueryString.Keys)
                {
                    if (Debar.ToLower().IndexOf(key.Trim().ToLower()) == -1 && key.Trim().ToLower() != "1")
                    {
                        returnstr += "<input type=hidden name='AFF_" + key + "' value='" + System.Web.HttpContext.Current.Request.QueryString[key] + "'>\r\n";
                    }
                }
            }
            else if (sort == "12")
            {
                foreach (string key in System.Web.HttpContext.Current.Request.QueryString.Keys)
                {
                    if (Debar.ToLower().IndexOf(key.Trim().ToLower()) == -1 && key.Trim().ToLower() != "1")
                    {
                        returnstr += "<input type=hidden name='" + key + "' value='" + System.Web.HttpContext.Current.Request.QueryString[key] + "'>\r\n";
                    }
                }
            }
            else if (sort == "01")
            {
                foreach (string key in System.Web.HttpContext.Current.Request.QueryString.Keys)
                {
                    string key2 = key.Replace("AFF_", "");

                    if (Debar.ToLower().IndexOf(key2.Trim().ToLower()) == -1 && key2.Trim().ToLower() != "1")
                    {
                        if (key.Length > 4 && key.Substring(0, 4) == "AFF_")
                        {
                            returnstr += "&" + key2 + "=" + System.Web.HttpContext.Current.Server.UrlEncode(System.Web.HttpContext.Current.Request.QueryString[key2]) + "";
                        }
                    }
                }

                foreach (string key in System.Web.HttpContext.Current.Request.Form.Keys)
                {
                    string key2 = key.Replace("AFF_", "");

                    if (Debar.ToLower().IndexOf(key2.Trim().ToLower()) == -1 && key2.Trim().ToLower() != "1")
                    {
                        if (key.Length > 4 && key.Substring(0, 4) == "AFF_")
                        {
                            returnstr += "&" + key2 + "=" + System.Web.HttpContext.Current.Server.UrlEncode(System.Web.HttpContext.Current.Request.Form[key2]) + "";
                        }
                    }
                }
            }
            else if (sort == "02")
            {
                foreach (string key in System.Web.HttpContext.Current.Request.QueryString.Keys)
                {
                    if (Debar.ToLower().IndexOf(key.Trim().ToLower()) == -1 && key.Trim().ToLower() != "1")
                    {
                        returnstr += "&" + key + "=" + System.Web.HttpContext.Current.Server.UrlEncode(System.Web.HttpContext.Current.Request.QueryString[key]) + "";
                    }
                }
            }
            return returnstr;
        }

        /// <summary>
        /// PHP的时间转换成.net中的DateTime
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime PhpDatetimeToNet(long time)
        {
            DateTime timeStamp = new DateTime(1970, 1, 1);  //得到1970年的时间戳
            long t = (time + 8 * 60 * 60) * 10000000 + timeStamp.Ticks;
            DateTime dt = new DateTime(t);
            return dt;
        }

        /// <summary>
        /// .Net的时间转换成PHP
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long NetDatetimeToPhp(DateTime time)
        {
            DateTime timeStamp = new DateTime(1970, 1, 1);  //得到1970年的时间戳
            long a = (time.Ticks - timeStamp.Ticks) / 10000000 - 8 * 60 * 60;  //用now就要减掉8个小时
            return a;
        }


        /// <summary>
        /// 初始化CheckBoxList中哪些是选中了的
        /// </summary>
        /// <param name="checkList">CheckBoxList</param>
        /// <param name="selval">选中了的值串例如："0,1,1,2,1"</param>
        /// <param name="separator">值串中使用的分割符例如"0,1,1,2,1"中的逗号</param>
        public static void SetCBLChecked(CheckBoxList checkList, string selval, string separator)
        {
            //例如："0,1,1,2,1"->",0,1,1,2,1,"
            if (!selval.StartsWith(separator))
            {
                selval = separator + selval;
            }
            if (!selval.EndsWith(separator))
            {
                selval = selval + separator;
            }

            for (int i = 0; i < checkList.Items.Count; i++)
            {
                checkList.Items[i].Selected = false;
                string val = separator + checkList.Items[i].Value + separator;
                if (selval.IndexOf(val) != -1)
                {
                    checkList.Items[i].Selected = true;
                    selval = selval.Replace(val, separator);        //然后从原来的值串中删除已经选中了的
                    if (selval == separator)        //selval的最后一项也被选中的话，此时经过Replace后，只会剩下一个分隔符
                    {
                        selval += separator;        //添加一个分隔符
                    }
                }
            }
            //selval = selval.Substring(1, selval.Length - 2);//除去前后加的分割符号
            //return selval;
        }

        /// <summary>
        /// 得到CheckBoxList中选中了的值
        /// </summary>
        /// <param name="checkList">CheckBoxList</param>
        /// <param name="separator">分割符号</param>
        /// <returns></returns>
        public static string GetCBLChecked(CheckBoxList checkList, string separator)
        {
            string selval = "";
            for (int i = 0; i < checkList.Items.Count; i++)
            {
                if (checkList.Items[i].Selected)
                {
                    selval += checkList.Items[i].Value + separator;
                }
            }
            if (selval != "")
            {
                selval = selval.Substring(0, selval.Length - separator.Length);
            }
            return selval;
        }

        /// <summary>
        /// 获取当前网站的域名，如：http://www.域名.com/
        /// </summary>
        /// <returns>形如：http://www.域名.com/</returns>
        public string GetNowUrl()
        {
            string input = string.Empty;
            string pattern = @"http://(?<domain>[^\/]*)";
            input = HttpContext.Current.Request.Url.ToString();
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return "http://" + regex.Match(input).Groups["domain"].Value + "/";
        }

    }

}