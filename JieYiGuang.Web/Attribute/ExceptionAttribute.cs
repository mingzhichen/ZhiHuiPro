//using Fuda.Common;
//using BtxCMS.Models;
//using BtxCMS.Plugin.Manager.IBLL;
//using BtxCMS.SqlServer.Factory;
//using BtxCMS.Web.Manage.Controllers;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace BtxCMS.Web.Manage.Attribute
//{
//    /// <summary>
//    /// 异常处理控制器
//    /// </summary>
//    public class ExceptionAttribute : HandleErrorAttribute
//    {
//        /// <summary>
//        /// 异常接收
//        /// </summary>
//        /// <param name="filterContext"></param>
//        public virtual void OnException(ExceptionContext filterContext)
//        {
//            var ex = filterContext.Exception;
//            var errorID = LogHelper.LogError(ex, "WEB-MANAGE");
//            if (filterContext.HttpContext.Request.IsAjaxRequest())
//            {
//                filterContext.Result = new JsonResult()
//                {
//                    Data = new { message = "前端控制器执行出现异常，异常编号：" + errorID, success = false },
//                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
//                };
//            }
//            else
//            {
//                filterContext.Result = new ContentResult()
//                {
//                    Content = "前端控制器异常，异常编号：" + errorID
//                };
//            }
//        }
//    }
//}