﻿using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace JieYiGuang.Common.Config
{
    /// <summary>
    /// 邮件相关的配置
    /// </summary>
    public class EmailConfig
    {
        #region 私有变量
        static private readonly string configpath = "~/config/email.config";
        static private string _mode;
        static private string _host;
        static private int _port;
        static private string _from;
        static private string _username;
        static private string _password;
        static private bool _enablessl;
        #endregion
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static EmailConfig()
        {
            Reload();
        }
        /// <summary>
        /// 重新读取Config文件，重新取值
        /// </summary>
        static public void Reload()
        {
            string filepath = HttpContext.Current.Server.MapPath(configpath);
            XmlDocument xml = new XmlDocument();
            xml.Load(filepath);
            XmlNode rootpor = xml.SelectSingleNode("email/setting");
            _mode = rootpor.Attributes["current"].Value;
            foreach (XmlNode n in rootpor.ChildNodes)
            {
                if (n.NodeType != XmlNodeType.Comment && n.Name.ToLower() == "parameter")
                {
                    _host = n.Attributes["host"].Value;
                    _port = int.Parse(n.Attributes["port"].Value);
                    _from = n.Attributes["from"].Value;
                    _username = n.Attributes["username"].Value;
                    _password = n.Attributes["password"].Value;
                    if (n.Attributes["enablessl"].Value.ToLower() == "true")
                        _enablessl = true;
                    else
                        _enablessl = false;
                }
            }
        }
        /// <summary>
        /// 邮件组件方式
        /// </summary>
        static public string mode
        {
            get { return _mode; }
        }
        /// <summary>
        /// 服务器
        /// </summary>
        static public string host
        {
            get { return _host; }
        }
        /// <summary>
        /// 端口
        /// </summary>
        static public int port
        {
            get { return _port; }
        }
        /// <summary>
        /// 发送邮件地址
        /// </summary>
        static public string from
        {
            get { return _from; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        static public string username
        {
            get { return _username; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        static public string password
        {
            get { return _password; }
        }
        /// <summary>
        /// SSL加密
        /// </summary>
        static public bool enablessl
        {
            get { return _enablessl; }
        }

        /// <summary>
        /// 密码找回的邮件内容
        /// </summary>
        static public string retrieve
        {
            get { return BaseConfig.GetConfigValue("matter/retrievepwd", HttpContext.Current.Server.MapPath(configpath), true); }
        }
        /// <summary>
        /// 注册发送邮件内容
        /// </summary>
        static public string register
        {
            get { return BaseConfig.GetConfigValue("matter/register", HttpContext.Current.Server.MapPath(configpath), true); }
        }
        /// <summary>
        /// 邀请好友加入邮件内容
        /// </summary>
        static public string invite
        {
            get { return BaseConfig.GetConfigValue("matter/invite", HttpContext.Current.Server.MapPath(configpath), true); }
        }
        /// <summary>
        /// 取得邮箱对应的登录网页地址
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        static public string GetEmailLoginUrl(string emailAddress)
        {
            if (emailAddress == null)
                return string.Empty;
            string addr = Regex.Match(emailAddress, "@.+$", RegexOptions.Compiled).Value;
            if (addr == null || addr.Trim() == string.Empty)
                return string.Empty;
            string filepath = HttpContext.Current.Server.MapPath(configpath);
            XmlDocument xml = new XmlDocument();
            xml.Load(filepath);
            XmlNode rootpor = xml.SelectSingleNode("email/loginurl");
            foreach (XmlNode n in rootpor.ChildNodes)
            {
                if (n.NodeType != XmlNodeType.Comment && n.Name.ToLower() == "email")
                {
                    string s = n.Attributes["address"].Value;
                    if (s.ToLower() == addr.ToLower())
                        return n.InnerText;
                }
            }
            return string.Empty;
        }
    }

    public class emailsetConfig
    {
        #region 私有变量
        static private readonly string configpath = "~/config/emailset.config";
        static private string _letter;
        #endregion
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static emailsetConfig()
        {
            Reload();
        }
        /// <summary>
        /// 重新读取Config文件，重新取值
        /// </summary>
        static public void Reload()
        {
            string filepath = HttpContext.Current.Server.MapPath(configpath);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);//你的xml文件

            XmlNodeList xmlList = xmlDoc.SelectSingleNode("emailset").ChildNodes;
            foreach (XmlNode xmlNo in xmlList)
            {
                if (xmlNo.NodeType != XmlNodeType.Comment)
                {
                    XmlElement xe = (XmlElement)xmlNo;
                    {
                        if (xe.Name == "letter")
                        {
                            if (xe.InnerText != null && xe.InnerText != "")
                            {
                                _letter = xe.InnerText;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 邮件内容
        /// </summary>
        static public string letter
        {
            get { return _letter; }
        }
    }
}
