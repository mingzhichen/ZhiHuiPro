using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using JieYiGuang.Model;
using JieYiGuang.DTO;

namespace System.Web.Mvc
{
    public static class HtmlPager
    {
        /// <summary>
        /// 分页标签
        /// </summary>
        /// <param name="PageInfo">分页数据模型</param>
        /// <param name="Url">连接地址</param>
        /// <param name="CSSClass">分页的样式</param>
        /// <returns></returns>
        public static MvcHtmlString GetPager(this HtmlHelper helper, PagerInfo PageInfo, string Url, string CSSClass = "default-page", string CurClass = "page-on", string PreClass = "pre", string NextClass = "next")
        {
            Url = Url.ToLower();
            //如果没有传入样式class类，就是用默认样式
            if (string.IsNullOrEmpty(CSSClass))
            {
                CSSClass = "default-page";
            }

            //只有一页或没有数据时不显示
            if (PageInfo.PageCount <= 1)
            {
                //return MvcHtmlString.Create("");
            }

            var pageHtml = new StringBuilder();
            pageHtml.AppendFormat("<div class=\"{0}\">", CSSClass);

            //首页
            if (PageInfo.CurrentPageIndex == 1)
            {
                pageHtml.AppendFormat("<span >首页</span> ", "javascript:void(0);");
                pageHtml.AppendFormat("<span class=\"" + PreClass + "\" >上一页</span> ", "javascript:void(0);");
            }
            else
            {
                pageHtml.AppendFormat("<a href=\"{0}\">首页</a> ", Url.Replace("%7bpage%7d", "1"));
                pageHtml.AppendFormat("<a href=\"{0}\" class=\"" + PreClass + "\">上一页</a> ", Url.Replace("%7bpage%7d", (PageInfo.CurrentPageIndex - 1).ToString()));
            }

            //中间页码
            var start = 1;
            var end = 5;

            if (PageInfo.PageCount <= 5)
            {
                start = 1;
                end = PageInfo.PageCount;
            }
            else if (PageInfo.CurrentPageIndex > PageInfo.PageCount - 2)
            {
                end = PageInfo.PageCount;
                start = PageInfo.PageCount - 4;
            }
            else
            {
                if (PageInfo.CurrentPageIndex <= 2)
                {
                    start = 1;
                    end = 5;
                }
                else
                {
                    start = PageInfo.CurrentPageIndex - 2;
                    end = PageInfo.CurrentPageIndex + 2;
                }
            }

            //数字页面
            for (int i = start; i <= end; i++)
            {
                if (i == PageInfo.CurrentPageIndex)
                {
                    //选中
                    pageHtml.AppendFormat("<span class=\"" + CurClass + "\">{0}</span>", i);
                }
                else
                {
                    //未选中
                    pageHtml.AppendFormat("<a href=\"{0}\">{1}</a> ", Url.Replace("%7bpage%7d", i.ToString()), i);
                }
            }

            if (end < PageInfo.PageCount)
            {
                pageHtml.AppendFormat("<a href=\"{0}\">{1}</a> ", Url.Replace("%7bpage%7d", (end + 1).ToString()), "...");
            }

            //末页
            if (PageInfo.CurrentPageIndex == PageInfo.PageCount)
            {
                pageHtml.AppendFormat("<span class=\"" + NextClass + "\">下一页</span> ", "javascript:void(0);");
                pageHtml.AppendFormat("<span >末页</span> ");
            }
            else
            {
                pageHtml.AppendFormat("<a href=\"{0}\" class=\"" + NextClass + "\">下一页</a> ", Url.Replace("%7bpage%7d", (PageInfo.CurrentPageIndex + 1).ToString()));
                pageHtml.AppendFormat("<a href=\"{0}\">末页</a> ", Url.Replace("%7bpage%7d", PageInfo.PageCount.ToString()));
            }
            pageHtml.Append("</div>");
            return MvcHtmlString.Create(pageHtml.ToString());
        }
    }
}
