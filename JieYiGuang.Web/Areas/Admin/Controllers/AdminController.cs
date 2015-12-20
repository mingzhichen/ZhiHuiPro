using JieYiGuang.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using JieYiGuang.Common;
using JieYiGuang.Web.Attribute;
using JieYiGuang.Web.Areas.Admin.Models;
using JieYiGuang.BLL;
using JieYiGuang.Common.Utils;
using JieYiGuangCommon.Function;

namespace JieYiGuang.Web.Areas.Admin
{
    /// <summary>
    /// 后台数据管理中心入口控制器
    /// </summary>
    public class AdminController : BaseManageController
    {
        private const string BasePath = "~/Areas/Admin/Views/";
        private ClassNewsBLL ClassBLL = new ClassNewsBLL();

        #region Index 【管理员登陆入口】
        /// <summary>
        /// 管理员登陆入口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View(BasePath + "Login.cshtml");
        }

        //后台登入验证
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel, string returnUrl)
        {
            var isLogin = false;
            var message = string.Empty;

            if (!string.IsNullOrEmpty(loginModel.VerifyCode) && !loginModel.VerifyCode.Equals(UserState.GetSession("VerifyCode")))
            {
                ModelState.AddModelError("VerifyCode", "验证码验证失败.");
            }

            try
            {
                //验证
                if (ModelState.IsValid)
                {

                    if (loginModel.IsRememberMe)
                    {
                        UserState.SetCookie("AdminName", loginModel.AdminName, 3600 * 30 * 12);
                        UserState.SetCookie("AdminPassWord", "", 3600 * 30 * 12);
                        UserState.SetCookie("IsRememberMe", "true", 3600 * 30 * 12);
                    }
                    else
                    {
                        UserState.SetCookie("AdminName", "");
                        UserState.SetCookie("AdminPassWord", "");
                        UserState.SetCookie("IsRememberMe", "false");
                    }

                    JieYiGuang.BLL.AdminUserBLL bll = new JieYiGuang.BLL.AdminUserBLL();
                    JieYiGuang.Model.T_AdminUser model = bll.GetModel(loginModel.AdminName, loginModel.AdminPassWord, true);

                    if (model != null)
                    {
                        Session[DTKeys.SESSION_ADMIN_INFO] = model;
                        Session.Timeout = 45;
                        isLogin = true;
                    }
                    else
                    {
                        message = "用户名或密码不正确";
                        ModelState.AddModelError("Message", message);
                    }
                }
            }
            catch (Exception ex)
            {
                var errorId = string.Empty;
                isLogin = false;
                ModelState.AddModelError("Message", "账户注册出现异常，错误标识:" + errorId);
            }

            message = ModelStateExtensions.ExpendErrors(this);
            return Json(new { success = isLogin, message = message }, "application/json", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取图片验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult GetVerifyCode()
        {
            //显示验证码
            VerifyCode vCode = new VerifyCode();
            string code = vCode.CreateVerifyCode(6);
            UserState.SetSession("VerifyCode", code);
            byte[] bytes = vCode.CreateVerifyGraphicMVC(code);
            return File(bytes, @"image/jpeg");
        }
        #endregion

        #region LoginOut 【退出管理中心】
        public ActionResult LoginOut()
        {
            //AdminBLL.OutAdminStatus();
            return Redirect("/Admin/Login.html");
        }
        #endregion

        #region Default 【后台管理中心首页】
        [ManageAttribute]
        public ActionResult Default()
        {
            Model.T_AdminUser admin_info = GetAdminInfo(); //管理员信息
            //登录信息
            if (admin_info != null)
            {
                //if (model1 != null)
                //{
                //    //本次登录
                //    litIP.Text = model1.user_ip;
                //}
                //Model.manager_log model2 = bll.GetModel(admin_info.user_name, 2, DTEnums.ActionEnum.Login.ToString());
                //if (model2 != null)
                //{
                //    //上一次登录
                //    litBackIP.Text = model2.user_ip;
                //    litBackTime.Text = model2.add_time.ToString();
                //}
            }
            ViewData["Login_Info"] = admin_info;

            return View(BasePath + "Default.cshtml");
        }
        #endregion


        #region 获取树形目录的Combotree

        /// <summary>
        /// 获取combotree树形选择框的数据
        /// </summary>
        /// <param name="MarkName"></param>
        /// <returns></returns>
        [ManageAttribute]
        public ActionResult GetCombotree(string MarkName, int? ParentID, string MarkType)
        {
            var result = new List<object>();
            IList list = null;
            switch (MarkName.ToUpper())
            {
                case "NEWS":
                    list = ClassBLL.GetTreeByParent(ParentID.DefaultIsNullOrEmpty(0), false);
                    result.Add(new { id = 0, text = " 一级分类或顶级分类 ", iconCls = "icon-tree", children = ModelListLoop<T_ClassNews>(list as List<T_ClassNews>) });
                    break;
                default:
                    break;
            }
            if (list == null)
            {
                result.Add(new { id = 0, text = " 一级分类或顶级分类 ", iconCls = "icon-tree" });
            }
            return Json(result, "application/json", JsonRequestBehavior.AllowGet);
        }

        private List<object> ModelListLoop<T>(List<T> classList)
        {
            var result = new List<object>();
            if (classList != null)
            {
                foreach (var item in classList)
                {
                    var Base = item as T_ClassNews;
                    result.Add(new { id = Base.ID, text = "[" + Base.ID + "]" + Base.Title, state = Base.childcount == 0 ? "" : "closed", children = ModelListLoop<T>(Base.children as List<T>) });
                }
            }
            return result;
        }
        #endregion
    }
}