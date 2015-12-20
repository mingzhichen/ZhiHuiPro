using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.IO;

namespace System.Web.Mvc
{
    //编辑器调用
    public static class HtmlImages
    {



        public static MvcHtmlString JieYiGuang_GetImage(this HtmlHelper htmlHelper, string imgpath)
        {
            if (!string.IsNullOrWhiteSpace(imgpath))
            {
                imgpath = "http://img.shejiquan.me" + imgpath;
            }
            return MvcHtmlString.Create(imgpath);
        }

        #region 获取按宽高截取后的图片路径
        /// <summary>
        /// 获取按宽高截取后的图片路径
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="imgName">图片路径</param>
        /// <param name="width">截取的宽度</param>
        /// <param name="height">截取的高度</param>
        /// <returns>返回截取后的图片路径</returns>
        public static MvcHtmlString JieYiGuang_GetImageCut(this HtmlHelper htmlHelper, string imgpath, int width, int height)
        {
            if (string.IsNullOrEmpty(imgpath) || imgpath.IndexOf(".") < 1)
            {
                return MvcHtmlString.Create("http://img.shejiquan.me/Content/images/untitled.png");
            }

            if (!imgpath.ToLower().StartsWith("http"))
            {
                var ext = Path.GetExtension(imgpath);
                imgpath = imgpath.Substring(0, imgpath.LastIndexOf(".")) + "_" + width + "_" + height + ext;
                imgpath = "http://img.shejiquan.me" + imgpath;
            }
            return MvcHtmlString.Create(imgpath);
        }
        #endregion
    }
}