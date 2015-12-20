using System;
using System.ComponentModel;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Linq;
using JieYiGuang.Common;
using JieYiGuang.Model;
using JieYiGuang.BLL;
using System.Data;
using System.Collections.Generic;
using JieYiGuang.Dto;
using JieYiGuang.Web.Attribute;
using JieYiGuang.DTO;
using JieYiGuangCommon.Function;


namespace JieYiGuang.Web.Areas.Admin.Module
{
    [ManageAttribute]
    public class NewsController : BaseManageController
    {
        public BLL.NewsBLL NewsBLL = new BLL.NewsBLL();
        public BLL.NewsItemBLL NewsItemBLL = new BLL.NewsItemBLL();
        public BLL.ClassNewsBLL ClassNewsBLL = new BLL.ClassNewsBLL();

        public string ViewPath = "~/Areas/Admin/Views/Module/News/";


        private T_News Model;
        private T_News ModelEn;
        private T_NewsItem ItemModel;

        //#region 主体内容处理控制器

        #region List 【信息列表】
        /// <summary>
        /// 信息列表
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
                    SearchMarkStatus = Request["SearchMarkStatus"].DefaultIsNullOrEmpty(0),
                    SearchMarkName = MvcMarkName.DefaultIsNullOrEmpty(MvcMarkName),
                    SearchClassID = Request["SearchClassID"].DefaultIsNullOrEmpty(MvcClassID),
                    SearchName = Request["SearchName"].DefaultIsNullOrEmpty("ALL"),
                    SearchRecommendID = Request["SearchRecommendID"].DefaultIsNullOrEmpty(0)
                };
                var List = NewsBLL.ManageListPage(queryParams, pageSize, pageIndex, out rowCount);
                var dic = new Dictionary<string, object>();
                dic.Add("total", rowCount);
                dic.Add("rows", List);
                return Json(dic);
            }
            return View(GetViewPath(ViewPath, MvcTemplate, MvcMarkName, MvcClassID) + "List.cshtml");
        }
        #endregion

        #region Edit 【添加或编辑】
        public ActionResult Edit(string MvcTemplate, string MvcMarkName, int MvcClassID, int? InfoID)
        {
            #region 判断是否获取的是词条列表
            var infoGUID = Request["InfoGUID"].DefaultIsNullOrEmpty(Guid.Empty);
            if (infoGUID != Guid.Empty)
            {
                var ItemList = NewsItemBLL.GetItemList(infoGUID);
                return Json(ItemList);
            }
            #endregion
            InfoID = InfoID.DefaultIsNullOrEmpty(0);
            if (InfoID == 0)
            {
                Model = new T_News();
                Model.MarkName = MvcMarkName.DefaultIsNullOrEmpty("ALL");
                Model.Orders = 6666;
                ModelEn = Model;
            }
            else
            {
                Model = NewsBLL.GetByID(InfoID.Value, false);
                ModelEn = NewsBLL.GetByRefID(InfoID.Value, false);
                if (ModelEn == null)
                {
                    ModelEn = Model;
                }
            }
            ViewData.Model = Model;
            ViewData["ModelEn"] = ModelEn;
            return View(GetViewPath(ViewPath, MvcTemplate, MvcMarkName, MvcClassID) + "Edit.cshtml");
        }
        #endregion

        #region Save 【保存信息】
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Save(string Template, string MvcMarkName, int MvcClassID, T_News Model,T_NewsEn ModelEn)
        {
            var isSuccess = false;
            var message = string.Empty;

            try
            {
                Model.TimeBegin = DateTime.Now;
                Model.TimeEnd = DateTime.Now;
                Model.Flags = Request.Form["Flags"].TrimList();
                Model.Tags = Request.Form["Tags"].TrimList();

                var FromClassID = Request.Form["FromClassID"].TrimList();
                if (string.IsNullOrWhiteSpace(FromClassID))
                {
                    FromClassID = Model.ClassID.ToString();
                }
                else
                {
                    if (!FromClassID.Contains("," + Model.ClassID + ","))
                    {
                        FromClassID = FromClassID + Model.ClassID;
                    }
                }
                Model.FromClassID = FromClassID.TrimStart(',').TrimEnd(',');

                Model.Images = Request.Form["IsFirstImage"].DefaultIsNullOrEmpty(false) ? JieYiGuang.Common.StringHelper.GetFirstImage(Model.Contents) : Model.Images;
                if (MvcMarkName == "ALL")
                {
                    var classNewModel = ClassNewsBLL.GetByID(Model.ClassID);
                    Model.MarkName = classNewModel == null ? MvcMarkName.DefaultIsNullOrEmpty("NEWS") : classNewModel.MarkName;
                }
                else
                {
                    Model.MarkName = MvcMarkName.DefaultIsNullOrEmpty("NEWS");
                }
                if (Model.ID == 0)
                {
                    //创建
                    Model.CreateTime = DateTime.Now;
                    Model.UpdateTime = DateTime.Now;
                    Model.CreateUser = JieYiGuang_AdminModel.UserName;
                    //Model.AdminGUID = JieYiGuang_AdminModel.GUID;
                    isSuccess = NewsBLL.Add(Model) > 0;
                    if(isSuccess)
                    {
                        Model.Title = ModelEn.TitleEn;
                        Model.SubTitle = ModelEn.SubTitleEn;
                        Model.SeoTitle = ModelEn.SeoTitleEn;
                        Model.SeoDescription = ModelEn.SeoDescriptionEn;
                        Model.Images = ModelEn.ImagesEn;
                        Model.Descriptions = ModelEn.DescriptionsEn;
                        Model.Contents = ModelEn.ContentsEn;
                        Model.RefId = Model.ID;
                        isSuccess = NewsBLL.Add(Model) > 0;
                    }
                    message = isSuccess ? "添加成功！" : "添加失败！";
                }
                else
                {
                    //修改
                    Model.UpdateTime = DateTime.Now;
                    Model.UpdateUser = JieYiGuang_AdminModel.UserName;
                    //Model.AdminGUID = JieYiGuang_AdminModel.GUID;
                    isSuccess = NewsBLL.Update(Model);
                    if(isSuccess)
                    {
                        T_News tmp = NewsBLL.GetByRefID(Model.ID, false);
                        if (tmp != null)
                        {
                            tmp.Title = ModelEn.TitleEn;
                            tmp.SubTitle = ModelEn.SubTitleEn;
                            tmp.SeoTitle = ModelEn.SeoTitleEn;
                            tmp.SeoDescription = ModelEn.SeoDescriptionEn;
                            tmp.Images = ModelEn.ImagesEn;
                            tmp.Descriptions = ModelEn.DescriptionsEn;
                            tmp.Contents = ModelEn.ContentsEn;
                            tmp.RefId = Model.ID;
                        }
                        if (tmp != null)
                        {
                            isSuccess = NewsBLL.Update(tmp);
                        }
                        else
                        {
                            Model.Title = ModelEn.TitleEn;
                            Model.SubTitle = ModelEn.SubTitleEn;
                            Model.SeoTitle = ModelEn.SeoTitleEn;
                            Model.SeoDescription = ModelEn.SeoDescriptionEn;
                            Model.Images = ModelEn.ImagesEn;
                            Model.Descriptions = ModelEn.DescriptionsEn;
                            Model.Contents = ModelEn.ContentsEn;
                            Model.RefId = Model.ID;
                            isSuccess = NewsBLL.Add(Model) > 0;
                        }
                    }
                    message = isSuccess ? "编辑成功！" : "编辑失败！";
                }
            }
            catch (Exception ex)
            {
                //错误
                isSuccess = false;
                ModelState.AddModelError("Message", "出现异常，错误标识:" + ex.Message);
            }
            //绑定数据
            message = message + ModelStateExtensions.ExpendErrors(this);
            return Json(new { success = isSuccess, message = message }, "application/json", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Remove 【删除信息】
        public ActionResult Remove(string Template, string MvcMarkName, int MvcClassID, int InfoID)
        {
            var success = false;
            success = NewsBLL.Remove(InfoID);
            return Json(new { success = success, message = success ? "信息保存成功！" : "信息保存失败！" }, "application/json", JsonRequestBehavior.AllowGet);
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
                    success = NewsBLL.ManageArrayAction(id, MarkType, JieYiGuang_AdminModel);
                    break;
                case "FETCHINDEX":
                    var Indexs = Request["Indexs"].DefaultIsNullOrEmpty(-99);
                    MarkType = MarkType + Indexs;
                    success = NewsBLL.ManageArrayAction(id, MarkType, JieYiGuang_AdminModel);
                    break;
                default:
                    if (MarkType.ToUpper().StartsWith("TOP"))
                    {
                        success = NewsBLL.ManageArrayAction(id, MarkType, JieYiGuang_AdminModel);
                    }
                    break;
            }
            return Json(new { success = success, message = success ? "批量操作成功！" : "批量操作失败，请与管理员联系！" }, "application/json", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 关联信息控制器
        public ActionResult EditItem(string MvcTemplate, string MvcMarkName, int MvcClassID, int? InfoID)
        {
            InfoID = InfoID.DefaultIsNullOrEmpty(0);
            if (InfoID == 0)
            {
                ItemModel = new T_NewsItem();
                ItemModel.FromGUID = Request["InfoGUID"].DefaultIsNullOrEmpty(Guid.Empty);
            }
            else
            {
                ItemModel = NewsItemBLL.GetItemByID(InfoID.Value);
            }
            ViewData.Model = ItemModel;
            return View(GetViewPath(ViewPath, MvcTemplate, MvcMarkName, MvcClassID) + "EditItem.cshtml");
        }

        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult SaveItem(string Template, string MvcMarkName, int MvcClassID, T_NewsItem Item)
        {
            var success = false;
            Item.MarkName = MvcMarkName.DefaultIsNullOrEmpty("NEWS");
            if (Item.ID == 0)
            {
                Item.CreateUser = JieYiGuang_AdminModel.UserName;
                //Item.AdminGUID = JieYiGuang_AdminModel.GUID;
                success = NewsItemBLL.AddItem(Item);
            }
            else
            {
                Item.UpdateUser = JieYiGuang_AdminModel.UserName;
                //Item.AdminGUID = JieYiGuang_AdminModel.GUID;
                success = NewsItemBLL.UpdateItem(Item);
            }
            return Json(new { success = success, message = success ? "关联信息保存成功！" : "关联信息保存失败！" }, "application/json", JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveItem(string Template, string MvcMarkName, int MvcClassID, int InfoID)
        {
            var success = false;
            success = NewsItemBLL.RemoveItem(InfoID);
            return Json(new { success = success, message = success ? "关联信息保存成功！" : "关联信息保存失败！" }, "application/json", JsonRequestBehavior.AllowGet);
        }

        #region ItemList 【信息列表】
        /// <summary>
        /// 信息列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ItemList(string MvcTemplate, string MvcMarkName, int MvcClassID)
        {
            if (Request.IsAjaxRequest())
            {
                var pageSize = Request["rows"].DefaultIsNullOrEmpty(10);
                var pageIndex = Request["page"].DefaultIsNullOrEmpty(1);
                if (pageIndex == 0)
                {
                    pageIndex = 1;
                }
                var pager = new PagerInfo() { PageSize = pageSize, CurrentPageIndex = pageIndex };

                var queryParams = new NewsParam()
                {
                    FromGUID = Request["InfoGUID"].DefaultIsNullOrEmpty(Guid.Empty),
                    MarkName = MvcMarkName.DefaultIsNullOrEmpty(MvcMarkName),
                    ClassID = Request["SearchClassID"].DefaultIsNullOrEmpty(MvcClassID)
                };

                var List = NewsItemBLL.GetListPage(queryParams, pager);
                var dic = new Dictionary<string, object>();
                dic.Add("total", pager.RowCount);
                dic.Add("rows", List);
                return Json(dic);
            }
            return View(GetViewPath(ViewPath, MvcTemplate, MvcMarkName, MvcClassID) + "List.cshtml");
        }
        #endregion
        #endregion
    }

}
