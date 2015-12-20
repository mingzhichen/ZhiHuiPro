using JieYiGuang.Common;
using JieYiGuang.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JieYiGuang.Web.Areas.Admin
{
    /// <summary>
    /// 后台数据管理中心控制层基础函数定义
    /// </summary>
    public class BaseManageController : Controller
    {
        public T_AdminUser JieYiGuang_AdminModel { get; set; }
        public string GetViewPath(string viewPath, string mvcTemplate, string mvcMarkName, int mvcClassID)
        {
            ViewData["MenuID"] = RouteData.Values["MenuID"].DefaultIsNullOrEmpty(0);
            ViewData["MvcTemplate"] = mvcTemplate;
            ViewData["MvcMarkName"] = mvcMarkName;
            ViewData["MvcClassID"] = mvcClassID;
            ViewData["BasePath"] = (viewPath + "/" + mvcTemplate + "/").Replace("//", "/");
            return (viewPath + "/" + mvcTemplate + "/").Replace("//", "/");
        }

        /// <summary>
        /// 判断管理员是否已经登录(解决Session超时问题)
        /// </summary>
        public bool IsAdminLogin()
        {
            //如果Session为Null
            if (Session[DTKeys.SESSION_ADMIN_INFO] != null)
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
                        Session[DTKeys.SESSION_ADMIN_INFO] = model;
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
                Model.T_AdminUser model = Session[DTKeys.SESSION_ADMIN_INFO] as Model.T_AdminUser;
                if (model != null)
                {
                    return model;
                }
            }
            return null;
        }
    }
}