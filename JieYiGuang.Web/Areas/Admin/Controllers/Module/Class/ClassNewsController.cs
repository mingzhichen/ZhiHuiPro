using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using JieYiGuang.Model;
using JieYiGuang.Common;
using JieYiGuang.BLL;
using JieYiGuang.Common;
using System.Collections;
using JieYiGuang.Web.Attribute;
using JieYiGuangCommon.Function;

namespace JieYiGuang.Web.Areas.Admin
{
    /// <summary>
    /// 类别管理控制器
    /// </summary>
    [ManageAttribute]
    public class ClassNewsController : BaseManageController
    {
        public string BasePath = "~/Areas/Admin/Views/Module/Class/";
        public string CacheKeyPrefix = "ClassNews_";

        private ClassNewsBLL ClassBLL = new ClassNewsBLL();

        private T_ClassNews Model;

        #region List 【树形目录列表】
        /// <summary>
        /// 树形目录列表
        /// 根据MvcMarkName字段进行判断数据来源表
        /// </summary>
        /// <param name="MvcClassID"></param>
        /// <param name="MvcMarkName"></param>
        /// <param name="MvcMvcTemplate"></param>
        /// <returns></returns>
        public ActionResult List(string MvcTemplate, string MvcMarkName, int MvcClassID)
        {
            if (Request.IsAjaxRequest())
            {
                var id = Request["id"].DefaultIsNullOrEmpty(MvcClassID);
                var classList = ClassBLL.GetTreeByParent(id, false);
                #region 输出优化

                var viewIList = (from t in classList
                                 select new
                                 {
                                     t.ID,
                                     t.ParentID,
                                     t.Title,
                                     t.ParentLevels,
                                     t.Initial,
                                     t.Flags,
                                     t.Tags,
                                     t.Letter,
                                     t.Descriptions,
                                     t.Url,
                                     t.MarkName,
                                     t.Hits,
                                     t.OrderNum,
                                     t.state,
                                     t.MarkHot,
                                     t.MarkStatus,
                                     t.childcount,
                                     t.E_MarkStatusTitle,
                                     t.E_MarkHotTitle,
                                     t.CreateUser,
                                     t.CreateTime,
                                     t.UpdateTime
                                 }).ToList();
                #endregion
                return new ContentResult
                {
                    Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(viewIList),
                    ContentType = "application/json"
                };
            }
            return View(GetViewPath(BasePath, MvcTemplate, MvcMarkName, MvcClassID) + "List.cshtml");
        }
        #endregion

        #region Edit 【添加修改目录信息】
        public ActionResult Edit(string MvcTemplate, string MvcMarkName, int MvcClassID)
        {
            var parentID = Request["ParentID"].DefaultIsNullOrEmpty(0);
            var ClassID = Request["ClassID"].DefaultIsNullOrEmpty(0);
            if (ClassID == 0)
            {
                //添加
                Model = new T_ClassNews();
                Model.ParentID = parentID;
                Model.OrderNum = 6666;
            }
            else
            {
                //编辑
                Model = ClassBLL.GetByID(ClassID);
                if (Model == null)
                {
                    return Content("数据不存在！");
                }
            }
            ViewData.Model = Model;
            return View(GetViewPath(BasePath, MvcTemplate, MvcMarkName, MvcClassID)+"/Edit.cshtml");
        }
        #endregion

        #region Save 【保存类别信息】
        public ActionResult Save(string MvcTemplate, string MvcMarkName, int MvcClassID, T_ClassNews FormModel)
        {
            var success = false;
            var message = string.Empty;
            var isNew = Request["isNew"].DefaultIsNullOrEmpty(false);
            var parentid = 0;

            try
            {

                //服务器端验证
                if (MvcMarkName != "ALL" && FormModel.ParentID == 0)
                {
                    ViewData.ModelState.AddModelError("ParentID", "所属分类不能为空");
                }
                if (FormModel.ID != 0 && FormModel.ParentID == FormModel.ID)
                {
                    ViewData.ModelState.AddModelError("ParentID", "所属分类选取错误，不能选择自身为父分类！");
                }

                //服务器端验证判断
                if (ViewData.ModelState.IsValid)
                {
                    if (FormModel.ID == 0)
                    {
                        FormModel.CreateUser = JieYiGuang_AdminModel.UserName;
                        success = ClassBLL.AddClass(FormModel);
                        message = success ? "添加成功！" : "添加失败！";
                    }
                    else
                    {
                        FormModel.UpdateUser = JieYiGuang_AdminModel.UserName;
                        success = ClassBLL.UpdateClass(FormModel);
                        message = success ? "编辑成功！" : "编辑失败！";
                    }
                }
                parentid = ClassBLL.GetRefreshID(FormModel);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                ModelState.AddModelError("ERROR", "发生异常:" + message);
            }
            //绑定数据
            message = message + ModelStateExtensions.ExpendErrors(this);

            return Json(new { success = success, isNew = isNew, message = message, parentid = parentid }, "text/html", JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region Remove 【删除类别信息】
        public ActionResult Remove(string MvcTemplate, string MvcMarkName, int MvcClassID)
        {
            var success = false;
            var message = string.Empty;
            var ClassID = Request["ClassID"].DefaultIsNullOrEmpty(0);
            if (ClassID != 0)
            {
                success = ClassBLL.RemoveClass(ClassID);
            }
            return Json(new { success = success, message = success ? "删除成功！" : "操作失败，请与管理员联系！" }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        // ----------------------- 扩展

        #region ReBuild 【重建缓存】
        public ActionResult ReBuild(string MvcTemplate, string MvcMarkName, int MvcClassID)
        {
            var success = true;
            var message = string.Empty;
            ClassBLL.RemoveCache();
            return Json(new { success = success, message = success ? "重建分类缓存成功！" : "重建分类缓存失败，请与管理员联系！" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region UpdateAllParentPath 【重建分类结构】
        public ActionResult UpdateAllParentPath(string MvcTemplate, string MvcMarkName, int MvcClassID)
        {
            var success = true;
            var message = string.Empty;
            success = ClassBLL.UpdateAllParentPath(0);
            return Json(new { success = success, message = success ? "重建分类结构成功！" : "重建分类结构失败，请与管理员联系！" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ManageArrayAction 【批量状态操作】
        public ActionResult ManageArrayAction(string Template, string MvcMarkName, int MvcClassID, string MarkType)
        {
            var success = false;
            var message = string.Empty;
            var id = Request["InfoID"].DefaultIsNullOrEmpty("0");
            if (id == "0")
            {
                return Json(new { success = false, message = "操作错误，请选择要进行批量操作的记录！" });
            }
            switch (MarkType.ToUpper())
            {
                //批量更改状态
                case "FETCHSTATUS":
                    var MarkStatus = Request["MarkStatus"].DefaultIsNullOrEmpty(-99);
                    if (MarkStatus == -99)
                    {
                        return Json(new { success = false, message = "操作错误，请选择要修改的状态！" });
                    }
                    MarkType = MarkType + MarkStatus;
                    success = ClassBLL.ManageArrayAction(id, MarkType, 0,JieYiGuang_AdminModel);
                    break;
                case "FETCHINDEX":
                    var Indexs = Request["Indexs"].DefaultIsNullOrEmpty(-99);
                    MarkType = MarkType + Indexs;
                    success = ClassBLL.ManageArrayAction(id, MarkType, 0, JieYiGuang_AdminModel);
                    break;
                default:
                    if (MarkType.ToUpper().StartsWith("TOP"))
                    {
                        success = ClassBLL.ManageArrayAction(id, MarkType, 0, JieYiGuang_AdminModel);
                    }
                    break;
            }
            return Json(new { success = success, message = success ? "批量操作成功！" : "批量操作失败，请与管理员联系！" }, "application/json", JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}