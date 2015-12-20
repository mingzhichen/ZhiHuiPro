using JieYiGuang.BLL;
using JieYiGuang.Common;
using JieYiGuang.Dto;
using JieYiGuang.Web.Areas.Admin;
using JieYiGuang.Web.Attribute;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace JieYiGuang.Web.Areas.Admin.Module
{
    /// <summary>
    /// 管理员信息管理控制器
    /// </summary>
    [ManageAttribute]
    public class ManageController : BaseManageController
    {
        public string ViewPath = "~/Areas/Admin/Views/Module/Manage/";
        public JieYiGuang.BLL.AdminUserBLL ManagerBLL = new JieYiGuang.BLL.AdminUserBLL();
        private JieYiGuang.Model.T_AdminUser Admin;

        #region List 【管理员信息列表】
        /// <summary>
        /// 管理员信息列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List(string MvcTemplate, string MvcMarkName, int MvcClassID)
        {
            if (Request.IsAjaxRequest())
            {
                var pageSize = Request["rows"].DefaultIsNullOrEmpty(10);
                var pageIndex = Request["page"].DefaultIsNullOrEmpty(1);
                if (pageIndex == 0)
                {
                    pageIndex = 1;
                }
                int rowCount = 0;
                var queryParams = new QueryParams()
                {
                    SearchKey = Request["SearchKey"].DefaultIsNullOrEmpty(""),
                    SearchMarkStatus = Request["SearchMarkStatus"].DefaultIsNullOrEmpty(99),
                    SearchClassID = Request["SearchClassID"].DefaultIsNullOrEmpty(0)
                };
                var List = ManagerBLL.ManageListPage(queryParams, pageSize, pageIndex, out rowCount);
                var dic = new Dictionary<string, object>();
                dic["rows"] = List;
                dic["total"] = rowCount;
                return Json(dic);
            }
            return View(GetViewPath(ViewPath, MvcTemplate, MvcMarkName, MvcClassID) + "List.cshtml");
        }
        #endregion

        #region Edit 【添加或编辑管理员信息】
        public ActionResult Edit(string MvcTemplate, string MvcMarkName, int MvcClassID, int? AdminID)
        {
            AdminID = AdminID.DefaultIsNullOrEmpty(0);
            if (!AdminID.HasValue || AdminID == 0)
            {
                Admin = new JieYiGuang.Model.T_AdminUser();
            }
            else
            {
                Admin = ManagerBLL.GetModel(AdminID.Value);
            }
            ViewData.Model = Admin;
            return View(GetViewPath(ViewPath, MvcTemplate, MvcMarkName, MvcClassID) + "Edit.cshtml");
        }
        #endregion

        #region Save 【保存管理员信息】
        public ActionResult Save(string MvcTemplate, string MvcMarkName, int MvcClassID, JieYiGuang.Model.T_AdminUser Model)
        {
            var success = false;
            if (Model.ID == 0)
            {
                Model.AdminPassword = DESEncrypt.Encrypt(Model.AdminPassword, "");
                success = ManagerBLL.Add(Model)>0;
            }
            else
            {
                var Old = ManagerBLL.GetModel(Model.ID);
                if (Old != null)
                {
                    bool isCry = false;
                    if (Model.AdminPassword != Old.AdminPassword)
                    {

                    }
                    UpdateModel(Old);
                    Old.AdminPassword = DESEncrypt.Encrypt(Model.AdminPassword, "");
                    success = ManagerBLL.Update(Old);
                }
            }
            return Json(new { success = success, message = success ? "管理员信息保存成功！" : "管理员信息保存失败，不允许存在重复的管理员名称！" }, "application/json", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Remove 【删除管理员信息】
        public ActionResult Remove(string MvcTemplate, string MvcMarkName, int MvcClassID, int AdminID)
        {
            var success = false;
            success = ManagerBLL.Delete(AdminID);
            return Json(new { success = success, message = success ? "管理员信息保存成功！" : "管理员信息保存失败！" }, "application/json", JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}