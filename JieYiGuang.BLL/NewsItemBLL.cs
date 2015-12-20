using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JieYiGuang.Model;
using JieYiGuang.Common;
using JieYiGuang.DTO;
using JieYiGuang.Dto;

namespace JieYiGuang.BLL
{
    /// <summary>
    /// NewsBLL
    /// </summary>
    public partial class NewsItemBLL
    {
        private readonly JieYiGuang.DAL.NewsItemDAL dal = new JieYiGuang.DAL.NewsItemDAL();
        private readonly JieYiGuang.DAL.NewsDAL NewsDAL = new JieYiGuang.DAL.NewsDAL();
        private readonly JieYiGuang.DAL.ClassNewsDAL ClassDAL = new JieYiGuang.DAL.ClassNewsDAL();
        private List<T_ClassNews> ClassList = null;
        public NewsItemBLL()
		{}
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
		public int  Add(JieYiGuang.Model.T_NewsItem model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(JieYiGuang.Model.T_NewsItem model)
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
		public bool DeleteList(string IDlist )
		{
			return dal.DeleteList(IDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public JieYiGuang.Model.T_NewsItem GetModel(int ID)
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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<JieYiGuang.Model.T_NewsItem> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<JieYiGuang.Model.T_NewsItem> DataTableToList(DataTable dt)
		{
			List<JieYiGuang.Model.T_NewsItem> modelList = new List<JieYiGuang.Model.T_NewsItem>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				JieYiGuang.Model.T_NewsItem model;
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
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
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

        //--------------------图集基本操作------------------------------------------------

        #region AddItem 【添加资讯词条】
        public bool AddItem(Model.T_NewsItem Model)
        {
            if (dal.Add(Model)>0)
            {
                NewsDAL.UpdateAttribute(Model.FromGUID, NewsUpdateEnum.ItemNum);
                return true;
            }
            return false;
        }
        #endregion

        #region UpdateItem 【修改资讯词条】
        public bool UpdateItem(Model.T_NewsItem Model)
        {
            if (dal.Update(Model))
            {
                NewsDAL.UpdateAttribute(Model.FromGUID, NewsUpdateEnum.ItemNum);
                return true;
            }
            return false;
        }
        #endregion

        #region RemoveItem 【删除资讯词条】
        public bool RemoveItem(int ID)
        {
            var Item = dal.GetModel(ID);
            if (dal.Delete(ID))
            {
                NewsDAL.UpdateAttribute(Item.FromGUID, NewsUpdateEnum.ItemNum);
                return true;
            }
            return false;
        }
        #endregion

        #region GetItemByID 【获取资讯词条】
        public Model.T_NewsItem GetItemByID(int ID)
        {
            return dal.GetModel(ID);
        }
        #endregion

        //--------------------图集前端查询------------------------------------------------

        #region GetItemList 【根据资讯的GUID获取词条列表】
        public List<Model.T_NewsItem> GetItemList(Guid GUID)
        {
            return dal.GetItemList(GUID);
        }
        #endregion


        #region GetList 【获取图集列表】

        public List<T_NewsItem> GetList(int ClassID, int Start, int Max)
        {
            return GetList(ClassID, 0, null, Start, Max, null);
        }

        public List<T_NewsItem> GetList(int ClassID, string MarkName, int Start, int Max)
        {
            return GetList(ClassID, 0, MarkName, Start, Max, null);
        }

        public List<T_NewsItem> GetList(int ClassID, int ItemID, int Start, int Max)
        {
            return GetList(ClassID, ItemID, null, Start, Max, null);
        }

        public List<T_NewsItem> GetList(int ClassID, int ItemID, string MarkName, int Start, int Max)
        {
            return GetList(ClassID, ItemID, MarkName, Start, Max, null);
        }

        public List<T_NewsItem> GetList(int ClassID, int ItemID, string MarkName, int Start, int Max, params NewsOrderEnum[] OrderEnum)
        {
            return GetList(new NewsParam() { ClassID = ClassID, ItemID = ItemID, MarkName = MarkName, Start = Start, Max = Max, OrderEnum = OrderEnum });
        }

        public List<T_NewsItem> GetList(NewsParam Param)
        {
            return GetListPage(Param, null);
        }

        #endregion

        #region GetList 【按资讯的GUID获取图集列表】
        public List<T_NewsItem> GetList(Guid[] NewsGUIDList)
        {
            return dal.GetList(NewsGUIDList);
        }
        #endregion

        #region GetListPage 【分页获取图集列表】

        public List<T_NewsItem> GetListPage(int ClassID, int ItemID, string MarkName, PagerInfo PageInfo)
        {
            return GetListPage(new NewsParam() { ClassID = ClassID, ItemID = ItemID, MarkName = MarkName }, PageInfo);
        }

        public List<T_NewsItem> GetListPage(NewsParam Param, PagerInfo PageInfo)
        {
            List<T_NewsItem> List = null;

            //类别的所有子类，拼装成字符串
            if (Param.ClassID > 0)
            {
                if (ClassList == null)
                {
                    ClassList = ClassDAL.QueryCacheData();
                }
                Param.ClassIDStr = ClassList.FindAll(t => t.ParentPath.Contains("," + Param.ClassID + ",") || t.ID == Param.ClassID).Select(t => t.ID).ToArray().ToJoinString(",");
            }
            //获取列表
            List = dal.GetList(Param, PageInfo);
            //资讯列表设置对应的类别模型数据绑定
            if (List != null && List.Count > 0)
            {
                List.ForEach(t =>
                {
                    t.ClassNews = GetClassModel(t.E_ClassID);
                });
            }
            return List;
        }

        #endregion

        //--------------------内部操作函数------------------------------------------------

        #region GetClassModel 【获取所属类型的数据模型】
        private T_ClassNews GetClassModel(int ClassID)
        {
            if (ClassID > 0)
            {
                if (ClassList == null)
                {
                    ClassList = ClassDAL.QueryCacheData();
                }
                return ClassList.Find(t => t.ID == ClassID);
            }
            return null;
        }
        #endregion
		#endregion  ExtensionMethod
    }
}