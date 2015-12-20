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
    public partial class NewsBLL
    {
        private readonly JieYiGuang.DAL.NewsDAL dal = new JieYiGuang.DAL.NewsDAL();
        private readonly JieYiGuang.DAL.ClassNewsDAL ClassDAL = new JieYiGuang.DAL.ClassNewsDAL();
        private readonly JieYiGuang.DAL.NewsItemDAL NewsItemDAL = new JieYiGuang.DAL.NewsItemDAL();
        private List<T_ClassNews> ClassList = null;

        public NewsBLL()
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
		public int  Add(JieYiGuang.Model.T_News model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(JieYiGuang.Model.T_News model)
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
		public JieYiGuang.Model.T_News GetModel(int ID)
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
		public List<JieYiGuang.Model.T_News> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<JieYiGuang.Model.T_News> DataTableToList(DataTable dt)
		{
			List<JieYiGuang.Model.T_News> modelList = new List<JieYiGuang.Model.T_News>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				JieYiGuang.Model.T_News model;
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


        //--------------------资讯前端查询------------------------------------------------

        #region Remove 【删除资讯内容】
        public bool Remove(int ID)
        {
            return dal.Remove(ID);
        }

        public bool Remove(Guid GUID)
        {
            return dal.Remove(GUID);
        }
        #endregion
        #region GetByID 【根据资讯主键获取资讯内容】
        public T_News GetByID(int ID, bool HasItem = false, Guid UserGUID = new Guid())
        {
            var model = dal.GetModel(ID);
            if (model != null)
            {
                model.ClassNews = GetClassModel(model.ClassID);
                if (HasItem && model.NumItem > 0)
                {
                    model.ItemList = NewsItemDAL.GetItemList(model.GUID);
                }
                //更新点击数
                dal.UpdateAttribute(ID, NewsUpdateEnum.Hits);
            }
            return model;
        }
        #endregion

        #region GetByRefID 【根据资讯主键获取资讯内容】
        public T_News GetByRefID(int ID, bool HasItem = false, Guid UserGUID = new Guid())
        {
            var model = dal.GetModelByRefID(ID);
            if (model != null)
            {
                model.ClassNews = GetClassModel(model.ClassID);
                if (HasItem && model.NumItem > 0)
                {
                    model.ItemList = NewsItemDAL.GetItemList(model.GUID);
                }
                //更新点击数
                dal.UpdateAttribute(ID, NewsUpdateEnum.Hits);
            }
            return model;
        }
        #endregion
        #region GetList 【前端查询获取资讯列表】

        public List<T_News> GetList(int ClassID, int Start, int Max, bool isEn = false)
        {
            return GetList(ClassID, 0, null, Start, Max,isEn, null);
        }

        public List<T_News> GetList(int ClassID, string MarkName, int Start, int Max,bool isEn = false)
        {
            return GetList(ClassID, 0, MarkName, Start, Max, isEn, null);
        }

        public List<T_News> GetList(int ClassID, int ItemID, int Start, int Max, bool isEn = false)
        {
            return GetList(ClassID, ItemID, null, Start, Max, isEn, null);
        }

        public List<T_News> GetList(int ClassID, int ItemID, string MarkName, int Start, int Max, bool isEn = false)
        {
            return GetList(ClassID, ItemID, MarkName, Start, Max, isEn, null);
        }

        public List<T_News> GetList(int ClassID, int ItemID, string MarkName, int Start, int Max, bool isEn = false, params NewsOrderEnum[] OrderEnum)
        {
            return GetList(new NewsParam() { ClassID = ClassID, ItemID = ItemID, MarkName = MarkName, Start = Start, Max = Max, OrderEnum = OrderEnum,IsEn = isEn });
        }

        public List<T_News> GetList(NewsParam Param)
        {
            return GetListPage(Param, null);
        }

        #endregion

        #region GetListPage 【分页获取资讯列表】

        public List<T_News> GetListPage(int ClassID, int ItemID, string MarkName, PagerInfo PageInfo,bool isEn = false)
        {
            return GetListPage(new NewsParam() { ClassID = ClassID, ItemID = ItemID, MarkName = MarkName, IsEn = isEn }, PageInfo);
        }

        public List<T_News> GetListPage(NewsParam Param, PagerInfo PageInfo)
        {
            List<T_News> List = null;

            //类别的所有子类，拼装成字符串
            if (Param.ClassID > 0)
            {
                var ClassList = ClassDAL.QueryCacheData();
                Param.ClassIDStr = ClassList.FindAll(t => t.ParentPath.Contains("," + Param.ClassID + ",") || t.ID == Param.ClassID).Select(t => t.ID).ToArray().ToJoinString(",");
            }
            //获取列表
            List = dal.GetList(Param, PageInfo);
            //资讯列表设置对应的类别模型数据绑定
            if (List != null && List.Count > 0)
            {
                List.ForEach(t =>
                {
                    t.ClassNews = GetClassModel(t.ClassID);
                });
            }
            return List;
        }

        #endregion

        //--------------------后台管理操作------------------------------------------------

        #region ManageListPage 【资讯后台管理查询】
        public List<T_News> ManageListPage(QueryParams QueryParams, int PageSize, int PageIndex, out int rowCount)
        {
            int count = 0;
            List<T_News> List = null;

            if (QueryParams.SearchClassID > 0)
            {
                var ClassList = ClassDAL.QueryCacheData();
                QueryParams.SearchClassIDStr =
                    ClassList.FindAll(t => t.ParentPath.Contains("," + QueryParams.SearchClassID + ",") || t.ID == QueryParams.SearchClassID).Select(t => t.ID).ToArray().ToJoinString(",");
            }
            List = dal.ManageListPage(QueryParams, PageSize, PageIndex, out count);
            if (List != null)
            {
                List.ForEach(t =>
                {
                    t.ClassNews = GetClassModel(t.ClassID);
                });
            }
            rowCount = count;

            return List;
        }
        #endregion

        #region ManageArrayAction 【批量修改资讯{MarkType}相对应的字段信息】
        public bool ManageArrayAction(string IdArray, string MarkType, JieYiGuang.Model.T_AdminUser Admin)
        {
            return dal.ManageArrayAction(IdArray, MarkType, Admin);
        }
        #endregion

        #region UpdateAttribute 【资讯其他更新操作】
        /// <summary>
        /// 更新资讯属性字段
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <param name="UpdateEnum">更新字段的类型</param>
        /// <returns></returns>
        public bool UpdateAttribute(int ID, NewsUpdateEnum UpdateEnum)
        {
            return dal.UpdateAttribute(ID, UpdateEnum);
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