using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;

namespace JieYiGuang.Common.Config
{
   public class BaseConfig
    {
        /// <summary>
        /// 得到配置文件
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        public static string GetConfigParamvalue(string Item)
        {
            return string.Empty;
        }
        /// <summary>
        /// 读netcms.config取配置文件
        /// </summary>
        /// <param name="Target"></param>
        /// <returns></returns>
        static internal string GetConfigValue(string Target)
        {
            string path = HttpContext.Current.Server.MapPath("~/netsns.config");
            return GetConfigValue(Target, path);
        }
        /// <summary>
        /// 读netcms.config取配置文件
        /// </summary>
        /// <param name="Target"></param>
        /// <param name="ConfigPathName"></param>
        /// <returns></returns>
        static internal string GetConfigValue(string Target, string XmlPath, params bool[] cdata)
        {
            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(XmlPath);
            XmlElement root = xdoc.DocumentElement;
            XmlNode node = root.SelectSingleNode(Target);
            if (node != null)
            {
                if (cdata != null && cdata[0])
                    return node.InnerText;
                else
                    return node.InnerXml;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
