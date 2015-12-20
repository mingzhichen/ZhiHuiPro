using JieYiGuang.Common;
using JieYiGuang.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JieYiGuang.Common;
using JieYiGuang.Web.Areas.Admin;

namespace JieYiGuang.Web.Attribute
{
    /// <summary>
    /// 会员中心过滤器
    /// </summary>
    public class ManageAttribute : ActionFilterAttribute
    {
        protected internal Model.siteconfig siteConfig;
        private T_AdminUser Admin;

        /// <summary>
        /// 控制器函数执行后
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            #region 设置基本参数及数据
            //管理员信息
            filterContext.Controller.ViewData["BtxAdmin_Model"] = Admin;
            //操作按键GUID
            filterContext.Controller.ViewData["ActionGUID"] = filterContext.RequestContext.HttpContext.Request["ActionGUID"];
            //访问路径的基本参数
            filterContext.Controller.ViewData["Template"] = filterContext.RouteData.Values["Template"];
            filterContext.Controller.ViewData["MvcMarkName"] = filterContext.RouteData.Values["MvcMarkName"];
            filterContext.Controller.ViewData["MvcClassID"] = filterContext.RouteData.Values["MvcClassID"];
            #endregion

            #region 获取当前线程中是否存在错误
            var ErrorID = System.Runtime.Remoting.Messaging.CallContext.GetData("ThreadExceptionID") as string;
            if (!string.IsNullOrEmpty(ErrorID))
            {
                filterContext.Controller.ViewData["ErrorID"] = ErrorID;
            }
            #endregion
        }
        /// <summary>
        /// 控制器函数执行前
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ActionGUID = filterContext.RequestContext.HttpContext.Request["ActionGUID"].DefaultIsNullOrEmpty(Guid.Empty);
            var Action = filterContext.RouteData.Values["action"].DefaultIsNullOrEmpty("").ToUpper();
            var Controller = filterContext.RouteData.Values["controller"].DefaultIsNullOrEmpty("").ToUpper();
            if (Action == "ADMIN_LOGIN" || Action == "GETVERIFYCODE")
            {
                return;
            }

            Admin = GetAdminInfo();
            //未登录或授权失败
            if (!IsAdminLogin())
            {
                filterContext.Result = new RedirectResult("/Admin/Login");
                return;
            }

            //设置管理员信息
            (filterContext.Controller as BaseManageController).JieYiGuang_AdminModel = Admin;

        }

        #region 管理员============================================
        /// <summary>
        /// 判断管理员是否已经登录(解决Session超时问题)
        /// </summary>
        public bool IsAdminLogin()
        {
            //如果Session为Null
            if (HttpContext.Current.Session[DTKeys.SESSION_ADMIN_INFO] != null)
            {
                return true;
            }
            else
            {
                //检查Cookies
                string adminname = UtilHelper.GetCookie("AdminName", "JieYiGuang");
                string adminpwd = UtilHelper.GetCookie("AdminPwd", "JieYiGuang");
                if (adminname != "" && adminpwd != "")
                {
                    BLL.AdminUserBLL bll = new BLL.AdminUserBLL();
                    Model.T_AdminUser model = bll.GetModel(adminname, adminpwd);
                    if (model != null)
                    {
                        HttpContext.Current.Session[DTKeys.SESSION_ADMIN_INFO] = model;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 取得管理员信息
        /// </summary>
        public Model.T_AdminUser GetAdminInfo()
        {
            if (IsAdminLogin())
            {
                Model.T_AdminUser model = HttpContext.Current.Session[DTKeys.SESSION_ADMIN_INFO] as Model.T_AdminUser;
                if (model != null)
                {
                    return model;
                }
            }
            return null;
        }

        #endregion

        /// <summary>
        /// 数据返回后
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {

        }
        /// <summary>
        /// 数据返回前
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {

        }
    }
}