using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace JieYiGuang.Common.Helper
{

    /// <summary>
    /// 验证类
    /// </summary>
    public class ValiDateUtils
    {
        private static Regex RegNumber = new Regex("^[0-9]+$");
        private static Regex RegNumberSign = new Regex("^[+-]?[0-9]+$");
        private static Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
        private static Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$"); //等价于^[+-]?\d+[.]?\d+$
        private static Regex RegUser = new Regex(@"^[a-zA-Z0-9_]{4,20}$");//只能是字符数字下划线的组合
        private static Regex RegEmail = new Regex(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]+$");//w 英文字母或数字的字符串，和 [a-zA-Z0-9] 语法一样 
        private static Regex RegCHZN = new Regex("[a-zA-Z\u4e00-\u9fa5]");
        private static Regex RegMoney = new Regex(@"^([0-9]\d+|[0-9])(\.\d{0,4}?)*$");    //等价于：0或0.0000
        private static Regex RegMoney_2 = new Regex(@"^([0-9]\d+|[0-9])(\.\d{0,2}?)*$");    //等价于：0或0.00
        private static Regex RegMoney_4 = new Regex(@"^([0-9]\d+|[0-9])(\.\d{0,1}?)*$");    //等价于：0或0.0
        private static Regex RegUrl = new Regex(@"^http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
        private static Regex RegMobile = new Regex(@"^(13|14|15|16|17|18|19)\d{9}$"); //手机号码
        private static Regex RegImg = new Regex(@"(?i)<img[^>]*?src=(['""]?)([^'""\s>]+)\1[^>]*>"); //提取图片
        private static Regex RegTel = new Regex(@"^(([0\\+]\\d{2,3}-)?(0\\d{2,3})-)?(\\d{7,8})?$");//固定电话
        private static Regex RegTel2 = new Regex(@"^d{7,8}?$");//固定电话
        private static Regex RegTel3 = new Regex(@"^(\d{2,5}-)?(\d{7,8})");
        private static Regex RegMobileTel = new Regex(@"^0?1\\d{10}$|^[2-9]\\d{6,7}[ \\-]?\\d{0,6}$|^0[1-9]\\d{2}[ \\-]?[2-9]\\d{6,7}[\\-]?\\d{0,6}$|^0[1-9]\\d[ \\-]?[2-9]\\d{7}[\\-]?\\d{0,6}$|^0[1-9]\\d[ \\-]?[2-9]\\d{6,7}[ \\-]?\\d{0,6}$");
        private static Regex RegIDCard = new Regex(@"^[1-9]([0-9]{14}|[0-9]{17})$");//身份证
        private static Regex RegMoney_3 = new Regex(@"^((\d{1,3}(,\d{3})*)|(\d+))(\.\d{0,2})?$");
        private static Regex RegDateTime = new Regex("^[1-9]d{4}-(0[0-9]|1[0-2])-([0-2][0-9]|3[0-1])$");
        private static Regex RegDateTime2 = new Regex("^((\\d{2}(([02468][048])|([13579][26]))[\\-\\/\\s]?((((0?[13578])|(1[02]))[\\-\\/\\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\\-\\/\\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\\-\\/\\s]?((0?[1-9])|([1-2][0-9])))))|(\\d{2}(([02468][1235679])|([13579][01345789]))[\\-\\/\\s]?((((0?[13578])|(1[02]))[\\-\\/\\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\\-\\/\\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\\-\\/\\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))(\\s(((0?[1-9])|(1[0-2]))\\:([0-5][0-9])((\\s)|(\\:([0-5][0-9])\\s))([AM|PM|am|pm]{2,2})))?$"); //以下正确的输入格式： [2004-2-29], [2004-02-29 10:29:39 pm], [2004/12/31] 
        private static Regex RegIP = new Regex(@"\d+\.\d+\.\d+\.\d+");


        #region 数字字符串检查

        /// <summary>
        /// 检查Request查询字符串的键值，是否是数字，最大长度限制
        /// </summary>
        /// <param name="req">Request</param>
        /// <param name="inputKey">Request的键值</param>
        /// <param name="maxLen">最大长度</param>
        /// <returns>返回Request查询字符串</returns>
        public static string FetchInputDigit(HttpRequest req, string inputKey, int maxLen)
        {
            string retVal = string.Empty;
            if (inputKey != null && inputKey != string.Empty)
            {
                retVal = req.QueryString[inputKey];
                if (null == retVal)
                    retVal = req.Form[inputKey];
                if (null != retVal)
                {
                    retVal = SqlText(retVal, maxLen);
                    if (!IsNumber(retVal))
                        retVal = string.Empty;
                }
            }
            if (retVal == null)
                retVal = string.Empty;
            return retVal;
        }

        /// <summary>
        /// 是否数字字符串
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumber(string inputData)
        {
            if (string.IsNullOrEmpty(inputData)) return false;
            Match m = RegNumber.Match(inputData);
            return m.Success;
        }

        ///// <summary>
        ///// 判断空值 (是 true 否 false)
        ///// </summary>
        ///// <param name="chrStr"></param>
        ///// <returns></returns>
        public static Boolean IsNumber(object chrStr)
        {
            Boolean IsNumber = false;
            try
            {
                if (chrStr != null)
                {
                    Convert.ToInt32(chrStr);
                    IsNumber = true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
            }
            return IsNumber;

        }
        /// <summary>
        ///  检查是否含有中文
        /// </summary>
        /// <param name="InputText">需要检查的字符串</param>
        /// <returns></returns>
        public static bool IsHasChinese(string str)
        {
            byte[] btCN = System.Text.Encoding.Default.GetBytes(str);
            string strUTF8 = System.Text.Encoding.UTF8.GetString(btCN);

            int strLen = strUTF8.Length;
            //字符串的长度，一个字母和汉字都算一个 
            int bytLeng = System.Text.Encoding.UTF8.GetBytes(strUTF8).Length;
            //字符串的字节数，字母占1位，汉字占2位,注意，一定要UTF8 
            bool chkResult = false;
            if (strLen < bytLeng)  //如果字符串的长度比字符串的字节数小，当然就是其中有汉字啦^-^ 
            {
                chkResult = true;
            }
            return chkResult;

        }

        /// <summary>
        /// 是否数字字符串 可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumberSign(string inputData)
        {
            Match m = RegNumberSign.Match(inputData);
            return m.Success;
        }
        /// <summary>
        /// 是否是浮点数
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimal(string inputData)
        {
            Match m = RegDecimal.Match(inputData);
            return m.Success;
        }
        /// <summary>
        /// 是否是日期
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDateTime(string inputData)
        {
            Match m = RegDateTime.Match(inputData);
            return m.Success;
        }
        /// <summary>
        /// 是否是浮点数 可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimalSign(string inputData)
        {
            Match m = RegDecimalSign.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否是带小数四位的值
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsMoney(string inputData)
        {
            Match m = RegMoney.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否是带小数二位的值
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsMoney_2(string inputData)
        {
            Match m = RegMoney_2.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否是带小数一位的值（0或0.0）
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsMoney_4(string inputData)
        {
            Match m = RegMoney_4.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否是带小数二位的值
        /// XXXX 
        /// XXXX.XX 
        /// XX,XXX,XXX 
        /// XX,XXX,XXX.XX 
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsMoney_3(string inputData)
        {
            Match m = RegMoney_3.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否是手机号或固定电话号码（可带分机号）
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsMobileTel(string inputData)
        {
            Match m = RegMobileTel.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否是手机号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsMobile(string inputData)
        {
            Match m = RegMobile.Match(inputData);
            return m.Success;
        }
        /// <summary>
        /// 是否是固定电话
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsTel(string inputData)
        {
            Match m = RegTel.Match(inputData);
            return m.Success;
        }
        /// <summary>
        /// 是否是身份证
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsIDCard(string inputData)
        {
            Match m = RegIDCard.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否是邮箱
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsEmail(string inputData)
        {
            Match m = RegEmail.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否是用户名
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsUser(string inputData)
        {
            Match m = RegUser.Match(inputData);
            return m.Success;
        }

        #endregion

        #region 其他
        /// <summary>
        /// 判断字符串是否符合正则表达式
        /// </summary>
        /// <param name="str">验证字符串</param>
        /// <param name="regexpattern">正则规则字符</param>
        /// <returns></returns>
        public static bool ByRegex(string str, string regexpattern)
        {
            if (string.IsNullOrEmpty(str)) return false;
            return new Regex(regexpattern).IsMatch(str);
        }

        /// <summary>
        /// 检查字符串最大长度，返回指定长度的串
        /// </summary>
        /// <param name="sqlInput">输入字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns></returns>			
        public static string SqlText(string sqlInput, int maxLength)
        {
            if (sqlInput != null && sqlInput != string.Empty)
            {
                sqlInput = sqlInput.Trim();
                if (sqlInput.Length > maxLength)//按最大长度截取字符串
                    sqlInput = sqlInput.Substring(0, maxLength);
            }
            return sqlInput;
        }
        /// <summary>
        /// 字符串编码
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static string HtmlEncode(string inputData)
        {
            return HttpUtility.HtmlEncode(inputData);
        }
        /// <summary>
        /// 设置Label显示Encode的字符串
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="txtInput"></param>
        public static void SetLabel(Label lbl, string txtInput)
        {
            lbl.Text = HtmlEncode(txtInput);
        }
        public static void SetLabel(Label lbl, object inputObj)
        {
            SetLabel(lbl, inputObj.ToString());
        }
        //字符串清理
        public static string InputText(string inputString, int maxLength)
        {
            StringBuilder retVal = new StringBuilder();

            // 检查是否为空
            if ((inputString != null) && (inputString != String.Empty))
            {
                inputString = inputString.Trim();

                //检查长度
                if (inputString.Length > maxLength)
                    inputString = inputString.Substring(0, maxLength);

                //替换危险字符
                for (int i = 0; i < inputString.Length; i++)
                {
                    switch (inputString[i])
                    {
                        case '"':
                            retVal.Append("&quot;");
                            break;
                        case '<':
                            retVal.Append("&lt;");
                            break;
                        case '>':
                            retVal.Append("&gt;");
                            break;
                        default:
                            retVal.Append(inputString[i]);
                            break;
                    }
                }
                retVal.Replace("'", " ");// 替换单引号
            }
            return retVal.ToString();

        }
        /// <summary>
        /// 转换成 HTML code
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>string</returns>
        public static string Encode(string str)
        {
            str = str.Replace("&", "&amp;");
            str = str.Replace("'", "''");
            str = str.Replace("\"", "&quot;");
            str = str.Replace(" ", "&nbsp;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("\n", "<br>");
            return str;
        }
        /// <summary>
        ///解析html成 普通文本
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>string</returns>
        public static string Decode(string str)
        {
            str = str.Replace("<br>", "\n");
            str = str.Replace("&gt;", ">");
            str = str.Replace("&lt;", "<");
            str = str.Replace("&nbsp;", " ");
            str = str.Replace("&quot;", "\"");
            return str;
        }

        #endregion


        /// <summary>
        /// 检测是否整数型数据
        /// </summary>
        /// <param name="Num">待检查数据</param>
        /// <returns></returns>
        public static bool IsInteger(string Input)
        {
            if (Input == null)
            {
                return false;
            }
            else
            {
                return IsInteger(Input, true);
            }
        }

        /// <summary>
        /// 是否全是正整数
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static bool IsInteger(string Input, bool Plus)
        {
            if (Input == null)
            {
                return false;
            }
            else
            {
                string pattern = "^-?[0-9]+$";
                if (Plus)
                    pattern = "^[0-9]+$";
                if (Regex.Match(Input, pattern, RegexOptions.Compiled).Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 判断输入是否为日期类型
        /// </summary>
        /// <param name="s">待检查数据</param>
        /// <returns></returns>
        public static bool IsDate(string s)
        {
            try
            {
                DateTime d = DateTime.Parse(s);
                return true;
            }
            catch
            {
                return false;
            }
        }



        /// <summary>
        /// string型转换为bool型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StringToBool(object expression, bool defValue)
        {
            if (expression != null)
            {
                return StringToBool(expression, defValue);
            }
            return defValue;
        }

        /// <summary>
        /// string型转换为bool型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StringToBool(string expression, bool defValue)
        {
            if (expression != null)
            {
                if (string.Compare(expression, "true", true) == 0)
                {
                    return true;
                }
                else if (string.Compare(expression, "false", true) == 0)
                {
                    return false;
                }
            }
            return defValue;
        }

        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StringToInt32(object expression, int defValue)
        {
            if (expression != null)
            {
                return StringToInt32(expression.ToString(), defValue);
            }
            return defValue;
        }


        /// <summary>
        /// 将对象转换为Int16类型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime StringToDateTime(string str, string defValue)
        {
            DateTime rv;
            if (DateTime.TryParse(str, out rv))
                return rv;
            return DateTime.Parse(defValue);
        }

        /// <summary>
        /// 将对象转换为Int16类型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static short StringToInt16(string str, short defValue)
        {

            short rv;
            if (short.TryParse(str, out rv))
                return rv;
            return defValue;
        }

        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StringToInt32(string str, int defValue)
        {
            //if (str == null)
            //    return defValue;
            //if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*$"))
            //{
            //    if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') || (str.Length == 11 && str[0] == '-' && str[1] == '1'))
            //    {
            //        return Convert.ToInt32(str);
            //    }
            //}
            //return defValue;          
            int rv;
            if (Int32.TryParse(str, out rv))
                return rv;
            return defValue;
        }

        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float StringToFloat(object strValue, float defValue)
        {
            if ((strValue == null))
            {
                return defValue;
            }

            return StringToFloat(strValue.ToString(), defValue);
        }

        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float StringToFloat(string strValue, float defValue)
        {
            if ((strValue == null) || (strValue.Length > 10))
            {
                return defValue;
            }

            float intValue = defValue;
            if (strValue != null)
            {
                bool IsFloat = Regex.IsMatch(strValue, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (IsFloat)
                {
                    float.TryParse(strValue, out intValue);
                }
            }
            return intValue;
        }


        /// <summary>
        /// string型转换为decimal型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static decimal StringToDecimal(string strValue, decimal defValue)
        {
            if ((strValue == null) || (strValue.Length > 10))
            {
                return defValue;
            }

            decimal intValue = defValue;
            if (strValue != null)
            {
                bool IsFloat = Regex.IsMatch(strValue, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (IsFloat)
                {
                    decimal.TryParse(strValue, out intValue);
                }
            }
            return intValue;
        }

        /// <summary>
        /// 判断空值 (是 true 否 false)
        /// </summary>
        /// <param name="chrStr"></param>
        /// <returns></returns>
        public static Boolean IsNothing(object chrStr)
        {
            Boolean IsNothing = false;
            if (chrStr == null) { IsNothing = true; }
            else if (chrStr.ToString() == "")
            { IsNothing = true; }
            return IsNothing;

        }


        /// <summary>
        /// 验证是否为IP地址
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsIP(string inputData)
        {
            return Regex.IsMatch(inputData, @"\d+\.\d+\.\d+\.\d+");
        }


        /// <summary>
        /// 检测是否符合email格式
        /// </summary>
        /// <param name="strEmail">要判断的email字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsValidEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }


        /// <summary>
        /// 检测是否符合url格式,前面必需含有http://
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsURL(string url)
        {
            return Regex.IsMatch(url, @"^http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$");
        }

        /// <summary>
        /// 检测是否符合电话格式
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static bool IsPhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^(\(\d{3}\)|\d{3}-)?\d{7,8}$");
        }



        /// <summary>
        /// 检测是否符合时间格式
        /// </summary>
        /// <returns></returns>
        public static bool IsTime(string timeval)
        {
            return Regex.IsMatch(timeval, @"20\d{2}\-[0-1]{1,2}\-[0-3]?[0-9]?(\s*((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?))?");
        }



        /// <summary>
        /// 检测是否符合身份证号码格式
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool IsIdentityNumber(string num)
        {
            return Regex.IsMatch(num, @"^\d{17}[\d|X]|\d{15}$");
        }

        #region 验证身份证号码15位或18位
        /// <summary>
        /// 验证身份证号码15位或18位
        /// </summary>
        /// <param name="Id">身份证号码</param>
        /// <returns>验证成功为True，否则为False</returns>

        public static bool CheckIDCard(string Id)
        {
            #region 身份证号码验证
            if (Id.Length == 18)
            {
                bool check = CheckIDCard18(Id);
                return check;
            }
            else if (Id.Length == 15)
            {
                bool check = CheckIDCard15(Id);
                return check;
            }
            else
            {
                return false;
            }
            #endregion
        }

        /// <summary>
        /// 验证15位身份证号
        /// </summary>
        /// <param name="Id">身份证号</param>
        /// <returns>验证成功为True，否则为False</returns>
        private static bool CheckIDCard18(string Id)
        {
            #region 身份证号码验证
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return false;//校验码验证
            }
            return true;//符合GB11643-1999标准
            #endregion
        }

        /// <summary>
        /// 验证18位身份证号
        /// </summary>
        /// <param name="Id">身份证号</param>
        /// <returns>验证成功为True，否则为False</returns>
        private static bool CheckIDCard15(string Id)
        {
            #region 身份证号码验证
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            return true;//符合15位身份证标准
            #endregion
        }
        #endregion

        /// <summary>
        /// 检测是否符合邮编格式
        /// </summary>
        /// <param name="postCode"></param>
        /// <returns></returns>
        public static bool IsPostCode(string postCode)
        {
            return Regex.IsMatch(postCode, @"^\d{6}$");
        }

        /// <summary>
        /// 判断字符串是否全为中文
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public static bool WordsIScn(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                Regex rx = new Regex("^[\u4e00-\u9fa5]$");
                if (!rx.IsMatch(s[i].ToString()))
                {
                    return false;
                }
            }
            return true;
        }



    }
}
