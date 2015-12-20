using System.Web;
using System.Web.Mvc;

namespace JieYiGuang.Web
{
    public static class ThemeManager
    {
        public static void Config()
        {
            System.Web.Mvc.ViewEngines.Engines.Clear();
            System.Web.Mvc.ViewEngines.Engines.Add(new ThemeViewEngine());
        }

        public static string ThemePath
        {
            get
            {
                return "/Themes/";
            }
        }

        public static string Theme
        {
            get
            {
                string tem = "default";
                //应该从数据库中读取
                if (HttpContext.Current.Session["theme"] != null)
                {
                    tem = HttpContext.Current.Session["theme"] as string;
                }
                return tem;
            }
        }

        public static string Style
        {
            get
            {
                string tem = "default";
                //应该从数据库中读取
                if (HttpContext.Current.Session["style"] != null)
                {
                    tem = HttpContext.Current.Session["style"] as string;
                }
                return tem;
            }
        }

        public static string ThemeContent(this UrlHelper urlHelper, string url)
        {
            if (Theme == "default")
            {
                return urlHelper.Content(url);
            }
            return urlHelper.Content(url.Insert(1, ThemePath + Theme));
        }
    }
}