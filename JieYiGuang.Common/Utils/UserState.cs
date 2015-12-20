using System;
using System.Web;

namespace JieYiGuang.Common.Utils
{
    public class UserState
    {

        #region 从Session或Cookie中获取值
        /// <summary>
        /// 写Cookie值
        /// </summary>
        /// <param name="StrName">名称</param>
        /// <param name="StrValue">值</param>
        public static object GetCookieOrSession(string StrName)
        {
            object result = UserState.GetSession(StrName);
            if (result == null)
            {
                result = UserState.GetCookie(StrName);
            }
            return result;
        }
        #endregion

        #region Cookie 操作

        /// <summary>
        /// 写Cookie值
        /// </summary>
        /// <param name="StrName">名称</param>
        /// <param name="StrValue">值</param>
        public static void SetCookie(string StrName, object StrValue)
        {
            HttpCookie Cookie = HttpContext.Current.Request.Cookies[StrName];
            if (Cookie == null)
            {
                Cookie = new HttpCookie(StrName);
            }
            Cookie.Value = HttpContext.Current.Server.UrlEncode(StrValue.ToString());
            Cookie.Expires = DateTime.Now.AddMinutes(60);
            HttpContext.Current.Response.AppendCookie(Cookie);

        }

        /// <summary>
        /// 写Cookie值
        /// </summary>
        /// <param name="StrName">名称</param>
        /// <param name="StrValue">值</param>
        /// <param name="Expires">有效时间 分钟</param>
        public static void SetCookie(string StrName, string StrValue, int Expires)
        {
            HttpCookie Cookie = HttpContext.Current.Request.Cookies[StrName];
            if (Cookie == null)
            {
                Cookie = new HttpCookie(StrName);
            }
            Cookie.Value = HttpContext.Current.Server.UrlEncode(StrValue);
            Cookie.Expires = DateTime.Now.AddMinutes(Expires);
            HttpContext.Current.Response.AppendCookie(Cookie);

        }

        /// <summary>
        /// 写Cookie值
        /// </summary>
        /// <param name="StrName">名称</param>
        /// <param name="StrValue">值</param>
        /// <param name="Expires">有效时间 分钟</param>
        /// <param name="StrPath">有效路径 域</param>
        public static void SetCookie(string StrName, string StrValue, int Expires, string StrPath)
        {
            HttpCookie Cookie = HttpContext.Current.Request.Cookies[StrName];
            if (Cookie == null)
            {
                Cookie = new HttpCookie(StrName);
                Cookie.Path = StrPath;
            }
            Cookie.Value = HttpContext.Current.Server.UrlEncode(StrValue);
            Cookie.Expires = DateTime.Now.AddMinutes(Expires);
            HttpContext.Current.Response.AppendCookie(Cookie);

        }

        /// <summary>
        /// 读Cookie值
        /// </summary>
        /// <param name="StrName">名称</param>
        /// <returns>Cookie值</returns>
        public static object GetCookie(string StrName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[StrName] != null)
            {
                return HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Cookies[StrName].Value.ToString());
            }

            return null;
        }


        /// <summary>
        /// 清除所有Cookie
        /// </summary>
        public static void RemoveCookie()
        {
            HttpCookie aCookie;
            string cookieName;
            int limit = HttpContext.Current.Request.Cookies.Count;
            for (int i = 0; i < limit; i++)
            {
                cookieName = HttpContext.Current.Request.Cookies[i].Name;
                aCookie = new HttpCookie(cookieName);
                aCookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(aCookie);
            }
            System.Web.Security.FormsAuthentication.SignOut();
        }

        /// <summary>
        /// 清除指定Cookie
        /// </summary>
        /// <param name="name">name of Cookie</param>
        public static void RemoveCookie(string StrName)
        {
            HttpCookie Cookie = HttpContext.Current.Request.Cookies[StrName];
            if (Cookie != null)
            {
                Cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Request.Cookies.Remove(StrName);
            }
        }

        #endregion

        #region Session 操作

        /// <summary>
        /// 读Session值
        /// </summary>
        /// <param name="StrName">名称</param>
        /// <returns>Session值</returns>
        public static object GetSession(string StrName)
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session[StrName] != null)
            {
                return HttpContext.Current.Session[StrName];
            }

            return null;
        }

        /// <summary>
        /// 写入Session值
        /// </summary>
        /// <param name="StrName">名称</param>
        /// <returns>Session值</returns>
        public static void SetSession(string StrName, object value)
        {
            HttpContext.Current.Session[StrName] = value;

        }

        /// <summary>
        ///  读Session值
        /// </summary>
        /// <param name="StrName">名称</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>Session值</returns>
        public static int GetSessionInt(string StrName, int defaultValue)
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session[StrName] != null)
            {
                return Convert.ToInt32(HttpContext.Current.Session[StrName]);
            }

            return defaultValue;
        }


        /// <summary>
        /// 清除所有Session
        /// </summary>
        public static void RemoveSession()
        {
            HttpContext.Current.Session.Clear();

        }
        #endregion


    }
}
