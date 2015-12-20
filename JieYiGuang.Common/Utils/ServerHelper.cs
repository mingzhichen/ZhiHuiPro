using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

namespace JieYiGuang.Common
{
    /// <summary>
    /// 服务器相关
    /// </summary>
    public class ServerHelper
    {

        #region GetServer(string strName) [返回指定的服务器变量信息]
        /// <summary>
        /// 返回指定的服务器变量信息
        /// </summary>
        /// <param name="strName">服务器变量名</param>
        /// <returns>服务器变量信息</returns>
        public static string GetServer(string strName)
        {

            if (HttpContext.Current.Request.ServerVariables[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.ServerVariables[strName].ToString();

        }
        #endregion

        #region GetNetVersion [获取.net版本]
        /// <summary>
        /// 获取.net版本
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static string GetNetVersion
        {
            get
            {
                return Environment.Version.ToString();
            }
        }
        #endregion

        #region GetIISVersion [获取iis版本]
        /// <summary>
        /// 获取iis版本
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static string GetIISVersion
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["SERVER_SOFTWARE"];
            }
        }
        #endregion

        #region GetRealDirectory [获取虚拟目录绝对路径]
        /// <summary>
        /// 获取虚拟目录绝对路径
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static string GetRealDirectory
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"];
            }
        }
        #endregion

        #region GetSessionCount [获取session总数]
        /// <summary>
        /// 获取session总数
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static string GetSessionCount
        {
            get
            {
                return HttpContext.Current.Session.Keys.Count.ToString();
            }
        }
        #endregion

        #region GetSupportHttps [获取https支持]
        /// <summary>
        /// 获取https支持
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static string GetSupportHttps
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["HTTPS"]; ;
            }
        }
        #endregion

        #region GetScriptName [获取当前页面名称]
        /// <summary>
        /// 获取当前页面名称
        /// </summary>
        public static string GetScriptName
        {

            get
            {
                return HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"].ToString();
            }

        }
        #endregion

        #region GetScriptNameExt [获取当前页面的扩展名]
        /// <summary>
        /// 获取当前页面的扩展名
        /// </summary>
        public static string GetScriptNameExt
        {

            get
            {
                return GetScriptName.Substring(GetScriptName.LastIndexOf(".") + 1);
            }

        }
        #endregion

        #region GetScriptNameQueryString [获取当前访问页面地址参数]
        /// <summary>
        /// 获取当前访问页面地址参数
        /// </summary>
        public static string GetScriptNameQueryString
        {

            get
            {
                return HttpContext.Current.Request.ServerVariables["QUERY_STRING"].ToString();
            }

        }
        #endregion

        #region GetScriptUrl [获取当前访问页面Url]
        /// <summary>
        /// 获取当前访问页面Url
        /// </summary>
        public static string GetScriptUrl
        {

            get
            {
                return GetScriptNameQueryString == "" ? GetScriptName : string.Format("{0}?{1}", GetScriptName, GetScriptNameQueryString);
            }

        }
        #endregion

        #region GetHomeBaseUrl [返回当前页面目录的url]
        /// <summary>
        /// 返回当前页面目录的url
        /// </summary>
        /// <param name="FileName">文件名</param>
        /// <returns></returns>
        public static string GetHomeBaseUrl(string FileName)
        {

            string Script_Name = GetScriptName;
            return string.Format("{0}/{1}", Script_Name.Remove(Script_Name.LastIndexOf("/")), FileName);

        }
        #endregion

        #region GetHomeUrl [返回当前网站网址]
        /// <summary>
        /// 返回当前网站网址
        /// </summary>
        /// <returns></returns>
        public static string GetHomeUrl
        {
            get
            {
                return HttpContext.Current.Request.Url.Authority;
            }
        }
        #endregion

        #region GetScriptPath [获取当前访问文件物理目录]
        /// <summary>
        /// 获取当前访问文件物理目录
        /// </summary>
        /// <returns>路径</returns>
        public static string GetScriptPath
        {

            get
            {
                string Paths = HttpContext.Current.Request.ServerVariables["PATH_TRANSLATED"].ToString();
                return Paths.Remove(Paths.LastIndexOf("\\"));
            }

        }
        #endregion

        #region GetRootURI [取得网站的根目录的URL]
        /// <summary>
        /// 取得网站的根目录的URL
        /// </summary>
        /// <returns></returns>
        public static string GetRootURI
        {
            get
            {
                string AppPath = "";
                HttpContext HttpCurrent = HttpContext.Current;
                HttpRequest Req;
                if (HttpCurrent != null)
                {
                    Req = HttpCurrent.Request;

                    string UrlAuthority = Req.Url.GetLeftPart(UriPartial.Authority);
                    if (Req.ApplicationPath == null || Req.ApplicationPath == "/")
                        //直接安装在   Web   站点   
                        AppPath = UrlAuthority;
                    else
                        //安装在虚拟子目录下   
                        AppPath = UrlAuthority + Req.ApplicationPath;
                }
                return AppPath;
            }
        }
        #endregion

        #region GetRootPath() [取得网站根目录的物理路径]
        /// <summary>
        /// 取得网站根目录的物理路径
        /// </summary>
        /// <returns></returns>
        public static string GetRootPath
        {
            get
            {
                string AppPath = "";
                HttpContext HttpCurrent = HttpContext.Current;
                if (HttpCurrent != null)
                {
                    AppPath = HttpCurrent.Server.MapPath("~");
                }
                else
                {
                    AppPath = AppDomain.CurrentDomain.BaseDirectory;
                    if (Regex.Match(AppPath, @"\\$", RegexOptions.Compiled).Success)
                        AppPath = AppPath.Substring(0, AppPath.Length - 1);
                }
                return AppPath;
            }

        }
        #endregion

        #region GetServerIP [获取服务器IP]
        /// <summary>
        /// 获取服务器IP
        /// </summary>
        public static string GetServerIP
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"].ToString();
            }

        }
        #endregion

        #region GetServerOS [获取服务器操作系统]
        /// <summary>
        /// 获取服务器操作系统
        /// </summary>
        public static string GetServerOS
        {

            get
            {
                return Environment.OSVersion.VersionString;
            }

        }
        #endregion

        #region GetServerHost [获取服务器域名]
        /// <summary>
        /// 获取服务器域名
        /// </summary>
        public static string GetServerHost
        {

            get
            {
                return HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString();
            }

        }
        #endregion

        #region GetServerNameAndPort [获取服务器名字和端口:http://localhost:1061]
        /// <summary>
        /// 获取服务器名字和端口:http://localhost:1061
        /// </summary>
        public static string GetServerNameAndPort
        {
            get
            {
                return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            }

        }
        #endregion

        #region GetCurrentUrlNoParm [获取无参数URL路径http://localhost:1061/WebSite1/Default3.aspx]
        /// <summary>
        /// 获取无参数URL路径http://localhost:1061/WebSite1/Default3.aspx
        /// </summary>
        public static string GetCurrentUrlNoParm
        {

            get
            {
                return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            }

        }
        #endregion

        #region GetScheme [获取URL的Scheme:http://]
        /// <summary>
        /// 获取URL的Scheme:http://
        /// </summary>
        public static string GetScheme
        {

            get
            {
                return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Scheme);
            }

        }
        #endregion

        #region GetBrowser [获取浏览器版本号]
        /// <summary>
        /// 获取浏览器版本号
        /// </summary>
        /// <returns></returns>
        public static string GetBrowser
        {


            get
            {
                string browsers;
                HttpBrowserCapabilities bc = HttpContext.Current.Request.Browser;
                string aa = bc.Browser.ToString();
                string bb = bc.Version.ToString();
                browsers = aa + bb;
                return browsers;
            }

        }
        #endregion

        #region SetPageNoCache() [设置页面不被缓存]
        /// <summary>
        /// 设置页面不被缓存
        /// </summary>
        public static void SetPageNoCache()
        {

            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            HttpContext.Current.Response.Expires = 0;
            HttpContext.Current.Response.CacheControl = "no-cache";
            HttpContext.Current.Response.AddHeader("Pragma", "No-Cache");
        }
        #endregion

        #region GetClientIP() [取得用户客户端IP(穿过代理服务器取远程用户真实IP地址)]
        /// <summary>
        /// 取得用户客户端IP(穿过代理服务器取远程用户真实IP地址)
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP
        {
            get
            {
                try
                {
                    if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                        return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                    else
                        return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                }
                catch { return ""; }
            }
        }
        #endregion

        #region GetUrlReferrer() [返回上一个页面的地址]
        /// <summary>
        /// 返回上一个页面的地址
        /// </summary>
        /// <returns>上一个页面的地址</returns>
        public static string GetUrlReferrer
        {
            get
            {
                string retVal = null;

                try
                {
                    retVal = HttpContext.Current.Request.UrlReferrer.ToString();
                }
                catch { }

                if (retVal == null)
                    return "";

                return retVal;
            }

        }
        #endregion

        #region GetCurrentFullHost() [得到当前完整主机头]
        /// <summary>
        /// 得到当前完整主机头
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentFullHost
        {
            get
            {
                HttpRequest request = System.Web.HttpContext.Current.Request;
                if (!request.Url.IsDefaultPort)
                {
                    return string.Format("{0}:{1}", request.Url.Host, request.Url.Port.ToString());
                }
                return request.Url.Host;
            }
        }
        #endregion

        #region GetHost() [得到主机头]
        /// <summary>
        /// 得到主机头
        /// </summary>
        /// <returns></returns>
        public static string GetHost
        {
            get
            {
                return HttpContext.Current.Request.Url.Host;
            }
        }
        #endregion

        #region GetRawUrl() [获取当前请求的原始 URL(URL 中域信息之后的部分,包括查询字符串(如果存在))]
        /// <summary>
        /// 获取当前请求的原始 URL(URL 中域信息之后的部分,包括查询字符串(如果存在))
        /// </summary>
        /// <returns>原始 URL</returns>
        public static string GetRawUrl
        {
            get
            {
                return HttpContext.Current.Request.RawUrl;
            }
        }
        #endregion

        #region IsClientBrowser() [判断当前访问是否来自浏览器软件]
        /// <summary>
        /// 判断当前访问是否来自浏览器软件
        /// </summary>
        /// <returns>当前访问是否来自浏览器软件</returns>
        public static bool IsClientBrowser()
        {
            string[] BrowserName = { "ie", "opera", "netscape", "mozilla" };
            string curBrowser = HttpContext.Current.Request.Browser.Type.ToLower();
            for (int i = 0; i < BrowserName.Length; i++)
            {
                if (curBrowser.IndexOf(BrowserName[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region IsClientSearchEngines() [判断是否来自搜索引擎链接]
        /// <summary>
        /// 判断是否来自搜索引擎链接
        /// </summary>
        /// <returns>是否来自搜索引擎链接</returns>
        public static bool IsClientSearchEngines()
        {
            string[] SearchEngine = { "google", "yahoo", "msn", "baidu", "sogou", "sohu", "sina", "163", "lycos", "tom" };
            string tmpReferrer = HttpContext.Current.Request.UrlReferrer.ToString().ToLower();
            for (int i = 0; i < SearchEngine.Length; i++)
            {
                if (tmpReferrer.IndexOf(SearchEngine[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region GetUrl() [获得当前完整Url地址]
        /// <summary>
        /// 获得当前完整Url地址
        /// </summary>
        /// <returns>当前完整Url地址</returns>
        public static string GetUrl
        {
            get
            {
                try
                {
                    return HttpContext.Current.Request.Url.ToString();
                }
                catch
                {
                    return "";
                }
            }
        }
        #endregion

        #region GetQueryString(string strName) [获得指定Url参数的值]
        /// <summary>
        /// 获得指定Url参数的值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string strName)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.QueryString[strName];
        }
        #endregion

        #region GetPageName() [获得当前页面的名称]
        /// <summary>
        /// 获得当前页面的名称
        /// </summary>
        /// <returns>当前页面的名称</returns>
        public static string GetPageName
        {
            get
            {
                string[] urlArr = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
                return urlArr[urlArr.Length - 1].ToLower();
            }
        }
        #endregion

        #region GetParamCount() [返回表单或Url参数的总个数]
        /// <summary>
        /// 返回表单或Url参数的总个数
        /// </summary>
        /// <returns></returns>
        public static int GetParamCount
        {
            get
            {
                return HttpContext.Current.Request.Form.Count + HttpContext.Current.Request.QueryString.Count;
            }
        }
        #endregion

        #region GetFormString(string strName) [获得指定表单参数的值]
        /// <summary>
        /// 获得指定表单参数的值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <returns>表单参数的值</returns>
        public static string GetFormString(string strName)
        {
            if (HttpContext.Current.Request.Form[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.Form[strName];
        }
        #endregion

        #region GetString(string strName) [获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值]
        /// <summary>
        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">参数</param>
        /// <returns>Url或表单参数的值</returns>
        public static string GetString(string strName)
        {
            if ("".Equals(GetQueryString(strName)))
            {
                return GetFormString(strName);
            }
            else
            {
                return GetQueryString(strName);
            }
        }
        #endregion

        #region RemovePageParam(String Url, String ParamName) [删除传入的参数]
        /// <summary>
        /// 删除传入的参数
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="ParamName"></param>
        /// <returns></returns>
        public String RemovePageParam(String Url, String ParamName)
        {
            String b = null;
            int start = Url.IndexOf(ParamName + "=");
            if (start != -1)
            {
                b = Url.Substring(0, start);
                int end = Url.IndexOf('&', start) + 1;
                if (end != 0)
                    b += Url.Substring(end, Url.Length - end);
                else
                    b = b.TrimEnd('&');
                return b;
            }
            return Url;
        }
        #endregion

        #region CheckScriptNameChar(string sChar) [检测当前url是否包含指定的字符]
        /// <summary>
        /// 检测当前url是否包含指定的字符
        /// </summary>
        /// <param name="sChar">要检测的字符</param>
        /// <returns></returns>
        public static bool CheckScriptNameChar(string sChar)
        {
            bool rBool = false;
            if (GetScriptName.ToLower().LastIndexOf(sChar) >= 0)
                rBool = true;
            return rBool;
        }
        #endregion

        #region IsPost() [判断当前页面是否接收到了Post请求]
        /// <summary>
        /// 判断当前页面是否接收到了Post请求
        /// </summary>
        /// <returns>是否接收到了Post请求</returns>
        public static bool IsPost()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("POST");
        }
        #endregion

        #region IsGet() [判断当前页面是否接收到了Get请求]
        /// <summary>
        /// 判断当前页面是否接收到了Get请求
        /// </summary>
        /// <returns>是否接收到了Get请求</returns>
        public static bool IsGet()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("GET");
        }
        #endregion
    }
}
