using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Collections.Specialized;
using System.Net;
using System.IO;

namespace JieYiGuang.Common.Helper
{
    public class XpathHelper
    {
        private string proxyURI = "w23s-isaarray02.china-channel.com:8080";
        private string proxyUserName = "chenbin";
        private string proxyPassword = "123456";
        private string proxyDomain = "CHINA-CHANNEL.COM";
        private WebClient webClient = new WebClient();
        private HtmlDocument document = new HtmlDocument();
        private List<HtmlNode> listNode = new List<HtmlNode>();
        private HtmlNode itemNode = null;


        #region 初始化
        private void webProxy()
        {
            #region 使用代理
#if DEBUG
            System.Net.WebProxy myProxy = new System.Net.WebProxy(proxyURI, true);
            myProxy.Credentials = new System.Net.NetworkCredential(proxyUserName, proxyPassword, proxyDomain);
            webClient.Proxy = myProxy;
#endif

            #endregion
        }

        public XpathHelper(string html)
        {
            try
            {
                document.LoadHtml(html);
                itemNode = document.DocumentNode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public XpathHelper(string charSet, string url)
        {
            var readHtml = string.Empty;
            try
            {
                this.webProxy();
                Encoding PageEncoding = Encoding.GetEncoding(charSet);
                Stream stream = webClient.OpenRead(url);
                StreamReader sr = new StreamReader(stream, PageEncoding);
                readHtml = sr.ReadToEnd();
                document.LoadHtml(readHtml);
                itemNode = document.DocumentNode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public XpathHelper(string charSet, string url, string EVENTTARGET, string VIEWSTATE, int page)
        {
            var readHtml = string.Empty;
            try
            {
                this.webProxy();
                NameValueCollection na = new NameValueCollection();
                na.Add("__EVENTTARGET", EVENTTARGET);
                na.Add("__VIEWSTATE", VIEWSTATE);
                na.Add("__EVENTARGUMENT", page.ToString());//页码
                byte[] webPost = webClient.UploadValues(url, "POST", na);
                Encoding encode = Encoding.GetEncoding(charSet);
                readHtml = encode.GetString(webPost);
                document.LoadHtml(readHtml);
                itemNode = document.DocumentNode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// 初始化读取
        /// </summary>
        /// <param name="charSet"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static XpathHelper Instance(string html)
        {
            return new XpathHelper(html);
        }

        /// <summary>
        /// 初始化Http读取
        /// </summary>
        /// <param name="charSet"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static XpathHelper Instance(string charSet, string url)
        {
            return new XpathHelper(charSet, url);
        }

        /// <summary>
        /// 初始化HttpPost读取 [Aspx服务器控件]
        /// </summary>
        /// <param name="charSet"></param>
        /// <param name="url"></param>
        /// <param name="EVENTTARGET"></param>
        /// <param name="VIEWSTATE"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static XpathHelper Instance(string charSet, string url, string EVENTTARGET, string VIEWSTATE, int page)
        {
            return new XpathHelper(charSet, url, EVENTTARGET, VIEWSTATE, page);
        }


        /// <summary>
        /// 获得跟节点
        /// </summary>
        /// <param name="html"></param>
        /// <param name="listXpath"></param>
        /// <returns></returns>
        public XpathHelper GetLoadHtml(string html)
        {
            try
            {
                document.LoadHtml(html);
                itemNode = document.DocumentNode;
            }
            catch (Exception ex)
            {
                itemNode = null;
            }
            return this;
        }

        /// <summary>
        /// 获得绝对路径的节点列表
        /// </summary>
        /// <param name="html"></param>
        /// <param name="listXpath"></param>
        /// <returns></returns>
        public List<HtmlNode> GetListByXpath(string listXpath)
        {
            try
            {
                HtmlNodeCollection nodeLoopList = itemNode.SelectNodes(listXpath);
                if (nodeLoopList != null && nodeLoopList.Count > 0)
                {
                    //循环节点
                    foreach (HtmlNode node in nodeLoopList)
                    {
                        listNode.Add(node);
                    }
                }
            }
            catch (Exception ex)
            {
                listNode = null;
            }
            return listNode;
        }

        /// <summary>
        /// 设置节点 
        /// </summary>
        /// <param name="htmlNode">Html信息</param>
        /// <returns></returns>
        public XpathHelper SetHtmlNodeByCreate(string htmlNode)
        {
            try
            {
                itemNode = HtmlNode.CreateNode(htmlNode);
            }
            catch (Exception ex)
            {
                itemNode = null;
            }
            return this;
        }

        /// <summary>
        /// 设置节点
        /// </summary>
        /// <param name="htmlNode"></param>
        /// <returns></returns>
        public XpathHelper SetHtmlNode(HtmlNode htmlNode)
        {
            try
            {
                itemNode = htmlNode;
            }
            catch (Exception ex)
            {
                itemNode = null;
            }
            return this;
        }

        /// <summary>
        /// 获得节点 [唯一ID]
        /// </summary>
        /// <param name="id">唯一ID</param>
        public XpathHelper GetHtmlNodeElementbyId(string id)
        {
            try
            {
                itemNode = document.GetElementbyId(id);
            }
            catch (Exception ex)
            {
                itemNode = null;
            }
            return this;
        }

        /// <summary>
        /// 获得节点 [Xpath绝对路径]
        /// </summary>
        /// <param name="xPath">Xpath绝对路径</param>
        /// <returns></returns>
        public XpathHelper GetHtmlNodeSelectNodes(string xPath)
        {
            try
            {
                itemNode = itemNode.SelectNodes(xPath)[0];
            }
            catch (Exception ex)
            {
                itemNode = null;
            }
            return this;
        }

        /// <summary>
        /// 获得节点 [Xpath相对路径]
        /// </summary>
        /// <param name="xPath">Xpath相对路径</param>
        /// <returns></returns>
        public XpathHelper GetHtmlNodeSelectSingleNode(string xPath)
        {
            try
            {
                itemNode = itemNode.SelectSingleNode(xPath);
            }
            catch (Exception ex)
            {
                itemNode = null;
            }
            return this;
        }

        /// <summary>
        /// 输出节点Html 
        /// </summary>
        /// <returns></returns>
        public string ToOuterHtml(bool isNull)
        {
            string vlaue = string.Empty;
            try
            {
                vlaue = itemNode.OuterHtml;
            }
            catch (Exception ex)
            {
                if (isNull)
                {
                    vlaue = null;
                }
                else
                {
                    throw ex;
                }
            }
            return vlaue;
        }

        /// <summary>
        /// 输出节点内部Html
        /// </summary>
        /// <returns></returns>
        public string ToInnerHtml(bool isNull)
        {
            string vlaue = string.Empty;
            try
            {
                vlaue = itemNode.InnerHtml;
            }
            catch (Exception ex)
            {
                if (isNull)
                {
                    vlaue = null;
                }
                else
                {
                    throw ex;
                }
            }
            return vlaue;
        }

        /// <summary>
        /// 输出节点内部Text
        /// </summary>
        /// <returns></returns>
        public string ToInnerText(bool isNull)
        {
            string vlaue = string.Empty;
            try
            {
                vlaue = itemNode.InnerText;
            }
            catch (Exception ex)
            {
                if (isNull)
                {
                    vlaue = null;
                }
                else
                {
                    throw ex;
                }
            }
            return vlaue;
        }

        /// <summary>
        /// 输出节点属性值
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns></returns>
        public string ToValueAttribute(string attribute, bool isNull)
        {
            string vlaue = string.Empty;
            try
            {
                vlaue = itemNode.Attributes[attribute].Value;
            }
            catch (Exception ex)
            {
                if (isNull)
                {
                    vlaue = null;
                }
                else
                {
                    throw ex;
                }
            }
            return vlaue;
        }

    }
}
