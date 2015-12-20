using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JieYiGuang.Model;
using JieYiGuang.Common;

namespace JieYiGuang.BLL
{
    /// <summary>
    /// ClassNewsBLL
    /// </summary>
    public partial class ClassNewsBLL
    {
        private readonly JieYiGuang.DAL.ClassNewsDAL dal = new JieYiGuang.DAL.ClassNewsDAL();
        public ClassNewsBLL()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JieYiGuang.Model.T_ClassNews model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JieYiGuang.Model.T_ClassNews model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            return dal.Delete(ID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            return dal.DeleteList(IDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JieYiGuang.Model.T_ClassNews GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JieYiGuang.Model.T_ClassNews> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JieYiGuang.Model.T_ClassNews> DataTableToList(DataTable dt)
        {
            List<JieYiGuang.Model.T_ClassNews> modelList = new List<JieYiGuang.Model.T_ClassNews>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JieYiGuang.Model.T_ClassNews model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod

        //--------------------基本操作------------------------------------------------

        #region AddClass 【增加类别信息】
        public bool AddClass(T_ClassNews model)
        {
            var List = dal.QueryCacheData();

            model.Title = model.Title.Trim();
            model.Letter = model.Letter.DefaultIsNullOrEmpty(JieYiGuang.Common.Utils.ChinaPinYinHelper.GetPinyin(model.Title.DefaultIsNullOrEmpty("").Replace(" ", "").Replace("　", "").Replace("/", ""))).ToLower();
            model.Initial = model.Letter[0].ToString().ToUpper();

            if (model.ParentID == 0)
            {
                model.ParentLevels = 1;
                model.ParentGUID = null;
                model.ParentPath = ",0,";
                model.MarkName = model.MarkName.DefaultIsNullOrEmpty("ALL");
            }
            else
            {
                var Parent = List.Find(t => t.ID == model.ParentID) as T_ClassNews;
                model.ParentLevels = Parent.ParentLevels + 1;
                model.ParentGUID = Parent.GUID;
                model.ParentPath = Parent.ParentPath + (Parent as T_ClassNews).ID + ",";
                model.MarkName = model.MarkName.DefaultIsNullOrEmpty(Parent.MarkName);
            }

            model.ID = dal.Add(model);
            List.Add(model);
            //清除缓存
            CacheHelper.Remove(typeof(T_ClassNews).FullName + "_ALL");
            //重新建立缓存
            CacheHelper.Insert(typeof(T_ClassNews).FullName + "_ALL", List, 60 * 24 * 365);
            return true;
        }
        #endregion

        #region UpdateClass 【修改类别信息】
        public bool UpdateClass(T_ClassNews model)
        {
            var List = dal.QueryCacheData();

            model.Title = model.Title.Trim();
            //model.Letter = model.Letter.DefaultIsNullOrEmpty(JieYiGuang.Common.Utils.ChinaPinYinHelper.GetPinyin(model.Title.DefaultIsNullOrEmpty("").Replace(" ", "").Replace("　", "").Replace("/", ""))).ToLower();
            model.Initial = model.Letter[0].ToString().ToUpper();

            //判断简码是不是唯一
            var LetterCount = List.Where(t => !string.IsNullOrEmpty((t as T_ClassNews).Letter) && (t as T_ClassNews).Letter.ToLower() == model.Letter
                && (t as T_ClassNews).ID != model.ID && (t as T_ClassNews).MarkStatus >= 0).Count();
            if (LetterCount > 0)
            {
                return false;
            }

            //基本数据填充
            //父类为第一级
            if (model.ParentID == 0)
            {
                model.ParentLevels = 1;
                model.ParentGUID = null;
                model.ParentPath = ",0,";
                model.MarkName = model.MarkName.DefaultIsNullOrEmpty("ALL");
            }
            else
            {
                var Parent = List.Find(t => (t as T_ClassNews).ID == model.ParentID) as T_ClassNews;
                //子类不能作为父类的父级
                if (Parent.ParentPath.Contains("," + model.ID + ","))
                {
                    return false;
                }
                model.ParentLevels = Parent.ParentLevels + 1;
                model.ParentGUID = Parent.GUID;
                model.ParentPath = Parent.ParentPath + (Parent as T_ClassNews).ID + ",";
                model.MarkName = model.MarkName.DefaultIsNullOrEmpty(Parent.MarkName);
            }

            if (dal.Update(model))
            {
                //清除旧数据
                var OLD = List.Find(t => (t as T_ClassNews).ID == model.ID);
                List.Remove(OLD);
                //将修改的数据进行缓存
                List.Add(model);
                //更新子类的父类路径字段
                List.FindAll(t => (t as T_ClassNews).ParentPath.Contains("," + model.ID + ",")).ForEach(c =>
                {
                    (c as T_ClassNews).ParentPath.Replace((OLD as T_ClassNews).ParentPath, model.ParentPath);
                });
                //清除缓存
                CacheHelper.Remove(typeof(T_ClassNews).FullName + "_ALL");
                //重新建立缓存
                CacheHelper.Insert(typeof(T_ClassNews).FullName + "_ALL", List, 60 * 24 * 365);
                return true;
            }
            return false;
        }
        #endregion

        #region RemoveClass 【删除类别信息】
        public bool RemoveClass(int ID)
        {
            var List = dal.QueryCacheData();
            List.RemoveAll(t => (t as T_ClassNews).ParentPath.Contains("," + ID + ",") || (t as T_ClassNews).ID == ID);
            //清除缓存
            CacheHelper.Remove(typeof(T_ClassNews).FullName + "_ALL");
            //重新建立缓存
            CacheHelper.Insert(typeof(T_ClassNews).FullName + "_ALL", List, 60 * 24 * 365);
            //执行数据库逻辑删除操作
            dal.Delete(ID);
            return true;
        }
        #endregion

        #region RemoveCache 【清除缓存数据】
        public void RemoveCache()
        {
            CacheHelper.Remove(typeof(T_ClassNews).FullName + "_ALL");
        }
        #endregion

        //--------------------前端查询------------------------------------------------

        #region QueryCacheData 【查询数据并进行缓存处理】

        public List<T_ClassNews> QueryCacheData()
        {
            return dal.QueryCacheData();
        }
        #endregion

        #region GetByParent 【根据父级编号获取自己列表】
        public List<T_ClassNews> GetByParent(int ParentID, bool HashParent)
        {
            var List = QueryCacheData();
            return List.Where(t => (t as T_ClassNews).ParentPath.Contains("," + ParentID + ",") && (t as T_ClassNews).MarkStatus == 0).ToList();
        }
        #endregion

        #region GetTreeByParent 【按上级编号获取树形结构数据】
        public List<T_ClassNews> GetTreeByParent(int ParentID, bool HasParent)
        {
            //var CacheKey = typeof(ClassNews).FullName + "_Tree_" + ParentID + "_" + HasParent;
            //var List = CacheUtil.GetCacheList<T>(CacheKey);
            //if (List == null)
            //{
            var List = QueryCacheData().Where(t =>
            ((t as T_ClassNews).ParentPath.Contains(string.Format(",{0},", ParentID)) ||
            (HasParent ? (t as T_ClassNews).ID == ParentID : true)) && (t as T_ClassNews).MarkStatus == 0)
            .ToList();
            //树形
            List = ToTree(ParentID, HasParent, List);
            //CacheHelper.Insert(CacheKey, List, 30);
            //}
            return List;
        }

        #region ToTree 【生成树形目录，内部函数】
        private List<T_ClassNews> ToTree(int ParentID, bool HasParent, List<T_ClassNews> List)
        {
            //定义字典类型，将List转换成字典类型，集合中的元素个数是相同的
            var dic = new Dictionary<int, T_ClassNews>(List.Count);

            foreach (var department in List)
            {
                if (department.children!=null)
                {
                    department.children = null;
                }
                dic.Add(department.ID, department);
            }

            //通过一次遍历集合，转换成具有层次结构的类型
            foreach (var department in dic.Values)
            {
                if (dic.ContainsKey((department as T_ClassNews).ParentID))
                {
                    if ((dic[(department as T_ClassNews).ParentID] as T_ClassNews).children == null)
                    {
                        (dic[(department as T_ClassNews).ParentID] as T_ClassNews).children = new List<T_ClassNews>();
                    }

                    (dic[(department as T_ClassNews).ParentID] as T_ClassNews).children.Add(department);
                }
            }
            return dic.Values.Where(t => (t as T_ClassNews).ParentID == ParentID).ToList();
        }
        #endregion

        #endregion

        #region GetByID 【根据主键ID获取对象】
        public T_ClassNews GetByID(int ID)
        {
            return dal.GetByID(ID);
        }
        #endregion
        #region GetClassTitle 【获取类别的标题名称，类似导航的结果】
        public string GetClassTitle(int ID) 
        {
            var title = string.Empty;
            while (ID != 0)
            {
                var info = GetByID(ID);
                if (info != null)
                {
                    title = info.Title + " - " + title;
                    ID = info.ParentID;
                }
                break;
            }
            return title != string.Empty ? title.Substring(0, title.Length - 3) : "";
        }
        #endregion

        //--------------------后端操作------------------------------------------------

        #region GetRefreshID 【获取分类的刷新ID】
        /// <summary>
        /// 获取分类的刷新ID
        /// </summary>
        /// <returns></returns>
        public int GetRefreshID(T_ClassNews model)
        {
            var list = dal.QueryCacheData();
            var parentModel = list.Find(t => (t as T_ClassNews).ID == model.ParentID);
            if (parentModel != null)
            {
                var num = list.Where(t => (t as T_ClassNews).ParentPath.Contains("," + model.ParentID + ",")).Count();
                if (num == 1)
                {
                    return (parentModel as T_ClassNews).ParentID;
                }
            }
            return model.ParentID;
        }
        #endregion

        #region ManageArrayAction 【后端批量操作更新】
        public int ManageArrayAction(string idArray, string doAction, int doClassID, string AdminName)
        {
            return dal.ManageArrayAction(idArray, doAction, doClassID, AdminName);
        }
        #endregion

        #region UpdateOrders 【更新排序】
        public int UpdateOrders(string idArray, string idOrderArray, string AdminName)
        {
            var count = dal.UpdateOrders(idArray, idOrderArray, AdminName);
            if (count > 0)
            {
                RemoveCache();
            }
            return count;
        }
        #endregion

        #region UpdateAllParentPath 【重建分类关系】
        public bool UpdateAllParentPath(int parentID)
        {
            var success = dal.UpdateAllParentPath(parentID);
            return success;
        }
        #endregion

        #region GetCombotree 【获取Combotree控件的数据类型】
        public List<object> GetCombotree<T>()
        {
            var List = QueryCacheData();
            var result = new List<object>();
            result.Add(new { id = 0, text = " 一级栏目或顶级栏目 ", iconCls = "icon-tree", children = ModelListLoop(List as List<T_ClassNews>) });
            return result;
        }

        private List<object> ModelListLoop(List<T_ClassNews> classList)
        {
            var result = new List<object>();
            if (classList != null)
            {
                foreach (var item in classList)
                {
                    result.Add(new { id = item.ID, text = "[" + item.ID + "]" + item.Title, state = item.childcount == 0 ? "" : "closed", children = ModelListLoop(item.children as List<T_ClassNews>) });
                }
            }
            return result;
        }
        #endregion
        #endregion  ExtensionMethod


        #region ManageArrayAction 【批量修改资讯{MarkType}相对应的字段信息】
        public bool ManageArrayAction(string IdArray, string MarkType, int doClassID, JieYiGuang.Model.T_AdminUser Admin)
        {
            return dal.ManageArrayAction(IdArray, MarkType, doClassID, Admin.Name)>0;
        }
        #endregion
    }
}