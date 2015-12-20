using JieYiGuang.Common.Helper;
using JieYiGuang.DTO;
using JieYiGuang.Model;
using JieYiGuang.Web.Models;
using JieYiGuangCommon.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JieYiGuang.Web.Controllers
{
    public class HomeController : Controller
    {

        #region 初始化
        public BLL.NewsBLL NewsBLL = new BLL.NewsBLL();
        public BLL.ClassNewsBLL ClassBLL = new BLL.ClassNewsBLL();
        public BLL.NewsItemBLL NewsItemBLL = new BLL.NewsItemBLL();
        public BLL.MessageBLL MessageBLL = new BLL.MessageBLL();
        #endregion

        #region 首页
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        //[OutputCache(Duration = 1)]
        public ActionResult Index()
        {
            Response.Cache.SetOmitVaryStar(true);

            //MBA资讯列表
            //var mbaList1 = NewsBLL.GetList(new NewsParam() { MarkName = "Product", Start = 0, MarkIndex = 1, Max = 10, IsEn = ThemeManager.Theme != "zh-cn" });
            var mbaList1 = ClassBLL.QueryCacheData().Where(x=>x.MarkIndex == 1).ToList();
            var mbaList2 = NewsBLL.GetList(new NewsParam() { MarkName = "Product", Start = 0, MarkIndex = 1, Max = 10, IsEn = ThemeManager.Theme != "zh-cn" });
            var mbaList3 = NewsBLL.GetList(0, "Banner", 0, 5, ThemeManager.Theme != "zh-cn");

            var IndexViewModel = new IndexViewModel();
            IndexViewModel.Products = mbaList1;
            IndexViewModel.NewsProduct = mbaList2;
            IndexViewModel.Banners = mbaList3;


            var shijianlist = NewsBLL.GetList(222, 0, 1, ThemeManager.Theme != "zh-cn");
            if (shijianlist != null && shijianlist.Count > 0)
            {
                IndexViewModel.About = shijianlist[0];
            }
            else
            {
                IndexViewModel.About = new T_News();
            }

            return View("Index", IndexViewModel);
        }
        #endregion

        #region 关于我们
        /// <summary>
        /// 关于我们
        /// </summary>
        /// <returns></returns>
        //[OutputCache(Duration = 3600)]
        public ActionResult About()
        {
            Response.Cache.SetOmitVaryStar(true);

            var list = NewsBLL.GetList(0, "About", 0, 1, ThemeManager.Theme != "zh-cn");
            T_News newsModel = new T_News();
            if (list != null && list.Count > 0)
            {
                newsModel = list[0];
            }
            return View("About", newsModel);
        }
        #endregion



        #region 联系我们
        /// <summary>
        /// 联系我们
        /// </summary>
        /// <returns></returns>
        //[OutputCache(Duration = 3600)]
        public ActionResult Contact()
        {
            Response.Cache.SetOmitVaryStar(true);

            //分页
            PagerInfo pager = new PagerInfo
            {
                CurrentPageIndex = 1,
                PageSize = int.MaxValue
            };

            //初始化
            var viewIList = NewsBLL.GetListPage(45, 0, "Contact", pager, ThemeManager.Theme != "zh-cn");

            return View("Contact", viewIList);
        }
        #endregion

        #region 产品页
        /// <summary>
        /// 产品页
        /// </summary>
        /// <returns></returns>
        //[OutputCache(Duration = 1200)]
        public ActionResult Product(int? Id, int? ClassID)
        {
            Response.Cache.SetOmitVaryStar(true);
            T_News newsModel = null;
            List<T_News> productList = null;
            T_ClassNews classModel = null;
            if (ClassID.HasValue)
            {
                var list = NewsBLL.GetList(ClassID.Value, "Product", 0, 1, ThemeManager.Theme != "zh-cn");
                if (list != null && list.Count > 0)
                {
                    newsModel = list[0];
                }
                classModel = ClassBLL.GetByID(ClassID.Value);
                List<T_ClassNews> treeList = ClassBLL.GetTreeByParent(ClassID.Value, false);
                int searchId = ClassID.Value;
                if (classModel.ParentLevels < 3)
                {
                    searchId = treeList == null ? searchId : treeList[0].ID;
                }
                productList = NewsBLL.GetList(searchId, "Product", 0, int.MaxValue, ThemeManager.Theme != "zh-cn");
            }
            if (Id.HasValue)
            {
                newsModel = NewsBLL.GetByID(Id.Value);
                productList = NewsBLL.GetList(newsModel.ClassID, "Product", 0, int.MaxValue, ThemeManager.Theme != "zh-cn");
            }
            if (Id == 0 || newsModel == null)
            {
                return Content("暂无该分类产品");
            }
            else
            {
                if (classModel != null)
                {
                    var list = ClassBLL.GetByID(ClassID.HasValue ? ClassID.Value : classModel.ParentID);
                    ViewData["ClassModel"] = list;
                }
                else
                {
                    ViewData["ClassModel"] = classModel;
                }
                //newsModel.ItemList = NewsItemBLL.GetItemList(newsModel.GUID);

            }
            ViewData["ProductList"] = productList;
            return View("Product", newsModel);
        }

        public ActionResult Config(string theme = "zh-cn", string style = "default")
        {
            Session["theme"] = theme;
            Session["style"] = style;
            return this.RedirectToAction("Index");
        }
        #endregion

        #region 质量控制
        /// <summary>
        /// 质量控制
        /// </summary>
        /// <returns></returns>
        //[OutputCache(Duration = 3600)]
        public ActionResult Quality()
        {
            Response.Cache.SetOmitVaryStar(true);

            var list = NewsBLL.GetList(0, "Quality", 0, 1, ThemeManager.Theme != "zh-cn");
            T_News newsModel = new T_News();
            if (list != null && list.Count > 0)
            {
                newsModel = list[0];
            }
            return View("Quality", newsModel);
        }
        #endregion


        #region 常见问题
        /// <summary>
        /// 常见问题
        /// </summary>
        /// <returns></returns>
        //[OutputCache(Duration = 3600)]
        public ActionResult Faq(int pageindex = 1, int pagesize = 10, int type = 0)
        {
            Response.Cache.SetOmitVaryStar(true);

            //分页
            PagerInfo pager = new PagerInfo
            {
                CurrentPageIndex = pageindex,
                PageSize = pagesize
            };

            //初始化
            var viewIList = NewsBLL.GetListPage(4, 0, "Faq", pager, ThemeManager.Theme != "zh-cn");

            ViewData["Pager"] = pager;

            return View("Faq", viewIList);
        }
        #endregion

        #region 联系我们
        /// <summary>
        /// 联系我们
        /// </summary>
        /// <returns></returns>
        //[OutputCache(Duration = 3600)]
        public ActionResult Message()
        {
            return View("Message", new T_Message());
        }
        /// <summary>
        /// 联系我们
        /// </summary>
        /// <returns></returns>
        //[OutputCache(Duration = 3600)]
        public ActionResult DoMessage(T_Message model)
        {
            bool success = false;
            var message = String.Empty;
            try
            {
                #region 过滤
                //服务器端验证判断
                if (!ViewData.ModelState.IsValid)
                {
                    message = "数据验证错误!";
                    foreach (KeyValuePair<string, ModelState> keyValue in ViewData.ModelState)
                    {
                        var error = keyValue.Value.Errors.Select(e => e.ErrorMessage).LastOrDefault();
                        if (!String.IsNullOrWhiteSpace(error))
                        {
                            message = error;
                            break;
                        }
                    }
                    ViewBag.Message = message;
                    return View("Message", model);
                }
                #endregion
                #region 组织 对象
                model.UpdateTime = DateTime.Now;
                #endregion

                success = MessageBLL.Add(model) > 0;
                string content = String.Empty;
                if (ThemeManager.Theme != "zh-cn")
                {
                    content = String.Format(@"<p class='MsoNormal'>
	++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++<br />
*First&nbsp;Name:&nbsp;{0}<br />
*Last&nbsp;name:&nbsp;{1}<br />
Title:&nbsp;{2}<br />
*Company&nbsp;(organization):&nbsp;{3}<br />
Department:&nbsp;{4}<br />
*address:&nbsp;{5}<br />
*City:{6}<br />
*State&nbsp;(province):&nbsp;{7}<br />
*Post&nbsp;Code:&nbsp;{8}<br />
*Country:&nbsp;{9}<br />
*Tel:&nbsp;{10}<br />
Fax:&nbsp;{11}<br />
*Email:{12}<br />
Web&nbsp;Site:<o:p>{13}</o:p>
</p>
<p class='MsoNormal'>
	Details&nbsp;about&nbsp;your&nbsp;interest:&nbsp;&nbsp;<span style='font-family:宋体;'>{14}</span><span style='font-family:Arial;'></span><o:p></o:p>
</p>", model.Name, model.NickName, model.Title, model.Company, model.Department, model.Address, model.City, model.Province, model.PostCode, model.Country,
     model.Phone, model.Fax, model.Email, model.WebSite, model.Contents);
                }
                else
                {
                    content = String.Format(@"<p class='MsoNormal'>
	++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++<o:p></o:p>
</p>
<p class='MsoNormal'>
	*<span style='font-family:宋体;'>姓名</span>{0}<br />
尊称:&nbsp;{1}<br />
*<span style='font-family:宋体;'>企业或者机构名</span>:&nbsp;{2}<br />
部门:&nbsp;{3}<br />
*<span style='font-family:宋体;'>地址</span>:&nbsp;{4}<br />
*<span style='font-family:宋体;'>城市</span><span style='font-family:Arial;'>:</span>{5}<br />
*<span style='font-family:宋体;'>省</span>:&nbsp;{6}<br />
*<span style='font-family:宋体;'>邮编</span>:&nbsp;{7}<br />
国家:&nbsp;{8}<br />
*<span style='font-family:宋体;'>电话</span>:&nbsp;{9}<br />
传真:&nbsp;{10}<br />
*<span style='font-family:宋体;'>电子邮件</span>:{11}<br />
网址:<o:p>{12}</o:p>
</p>
<p class='MsoNormal'>
	咨询内容<span style='font-family:Arial;'>:&nbsp;&nbsp;</span><span style='font-family:宋体;'>{13}</span><span style='font-family:Arial;'></span><o:p></o:p>
</p>", model.Name, model.NickName, model.Company, model.Department, model.Address, model.City, model.Province, model.PostCode, model.Country,
     model.Phone, model.Fax, model.Email, model.WebSite, model.Contents);
                }
                #region 发送邮件
                var helper = MailHelper.CreateInstance();
                helper.To = "sales@devotop.com";
                helper.Subject = "收到来自" + model.NickName + "留言";
                helper.Body = content;
                helper.Send();

                #endregion
                message = success ? "留言成功" : "留言失败";
            }
            catch (Exception ex)
            {
                message = message + ModelStateExtensions.ExpendErrors(this);
            }
            ViewBag.Message = message;
            return View("Message", model);
        }
        #endregion


        //-------------------------------------------------------------------------

        #region 页面局部视图数据控制器

        /// <summary>
        /// 导航菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult _Partial_Navigation(int id)
        {
            var list = ClassBLL.GetTreeByParent(1, false);
            ViewBag.Id = id;

            return PartialView("_Partial_Navigation", list);
        }
        /// <summary>
        /// 头部
        /// </summary>
        /// <returns></returns>
        //[OutputCache(Duration = 3600)]
        public ActionResult _Partial_Header()
        {
            return PartialView("_Partial_Header");
        }

        #endregion
    }
}