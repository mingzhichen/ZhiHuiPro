using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using JieYiGuang.Model;
using JieYiGuang.Common;
using System.Collections.Generic;//Please add references
using System.Linq;
using JieYiGuang.Common.Utils;

namespace JieYiGuang.DAL
{

    /// <summary>
    /// 数据访问类:T_T_ClassNewsDAL
    /// </summary>
    public partial class ClassNewsDAL
    {
        public ClassNewsDAL()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Dt_Class_News");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JieYiGuang.Model.T_ClassNews model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Dt_Class_News(");
            strSql.Append("GUID,CreateTime,CreateUser,UpdateTime,UpdateUser,MarkStatus,MarkName,SeoTitle,SeoKeywords,SeoDescription,UserGUID,AdminGUID,MarkHtml,ClassName,ParentID,ParentGUID,ParentPath,OrderNum,ParentLevels,Title,SubTitle,Letter,Initial,Flags,Tags,Style,Images,Descriptions,Contents,Url,Hits,NumCount,Value,ValueType,Lang,MarkHot)");
            strSql.Append(" values (");
            strSql.Append("@GUID,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser,@MarkStatus,@MarkName,@SeoTitle,@SeoKeywords,@SeoDescription,@UserGUID,@AdminGUID,@MarkHtml,@ClassName,@ParentID,@ParentGUID,@ParentPath,@OrderNum,@ParentLevels,@Title,@SubTitle,@Letter,@Initial,@Flags,@Tags,@Style,@Images,@Descriptions,@Contents,@Url,@Hits,@NumCount,@Value,@ValueType,@Lang,@MarkHot)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@GUID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@MarkStatus", SqlDbType.Int,4),
					new SqlParameter("@MarkName", SqlDbType.NVarChar,50),
					new SqlParameter("@SeoTitle", SqlDbType.NVarChar,50),
					new SqlParameter("@SeoKeywords", SqlDbType.NVarChar,50),
					new SqlParameter("@SeoDescription", SqlDbType.NVarChar,50),
					new SqlParameter("@UserGUID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@AdminGUID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@MarkHtml", SqlDbType.Int,4),
					new SqlParameter("@ClassName", SqlDbType.NVarChar,50),
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@ParentGUID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ParentPath", SqlDbType.NVarChar,50),
					new SqlParameter("@OrderNum", SqlDbType.Int,4),
					new SqlParameter("@ParentLevels", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.NVarChar,250),
					new SqlParameter("@SubTitle", SqlDbType.NVarChar,50),
					new SqlParameter("@Letter", SqlDbType.NVarChar,50),
					new SqlParameter("@Initial", SqlDbType.NVarChar,10),
					new SqlParameter("@Flags", SqlDbType.NVarChar,250),
					new SqlParameter("@Tags", SqlDbType.NVarChar,250),
					new SqlParameter("@Style", SqlDbType.NVarChar,50),
					new SqlParameter("@Images", SqlDbType.NVarChar,250),
					new SqlParameter("@Descriptions", SqlDbType.NVarChar,2000),
					new SqlParameter("@Contents", SqlDbType.NVarChar,255),
					new SqlParameter("@Url", SqlDbType.NVarChar,250),
					new SqlParameter("@Hits", SqlDbType.Int,4),
					new SqlParameter("@NumCount", SqlDbType.Int,4),
					new SqlParameter("@Value", SqlDbType.NVarChar,50),
					new SqlParameter("@ValueType", SqlDbType.NVarChar,50),
					new SqlParameter("@Lang", SqlDbType.NVarChar,10),
					new SqlParameter("@MarkHot", SqlDbType.Int,4)};
            parameters[0].Value = Guid.NewGuid();
            parameters[1].Value = model.CreateTime;
            parameters[2].Value = model.CreateUser;
            parameters[3].Value = model.UpdateTime;
            parameters[4].Value = model.UpdateUser;
            parameters[5].Value = model.MarkStatus;
            parameters[6].Value = model.MarkName;
            parameters[7].Value = model.SeoTitle;
            parameters[8].Value = model.SeoKeywords;
            parameters[9].Value = model.SeoDescription;
            parameters[10].Value = Guid.NewGuid();
            parameters[11].Value = Guid.NewGuid();
            parameters[12].Value = model.MarkHtml;
            parameters[13].Value = model.ClassName;
            parameters[14].Value = model.ParentID;
            parameters[15].Value = Guid.NewGuid();
            parameters[16].Value = model.ParentPath;
            parameters[17].Value = model.OrderNum;
            parameters[18].Value = model.ParentLevels;
            parameters[19].Value = model.Title;
            parameters[20].Value = model.SubTitle;
            parameters[21].Value = model.Letter;
            parameters[22].Value = model.Initial;
            parameters[23].Value = model.Flags;
            parameters[24].Value = model.Tags;
            parameters[25].Value = model.Style;
            parameters[26].Value = model.Images;
            parameters[27].Value = model.Descriptions;
            parameters[28].Value = model.Contents;
            parameters[29].Value = model.Url;
            parameters[30].Value = model.Hits;
            parameters[31].Value = model.NumCount;
            parameters[32].Value = model.Value;
            parameters[33].Value = model.ValueType;
            parameters[34].Value = model.Lang;
            parameters[35].Value = model.MarkHot;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JieYiGuang.Model.T_ClassNews model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Dt_Class_News set ");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("CreateUser=@CreateUser,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("UpdateUser=@UpdateUser,");
            strSql.Append("MarkStatus=@MarkStatus,");
            strSql.Append("MarkName=@MarkName,");
            strSql.Append("SeoTitle=@SeoTitle,");
            strSql.Append("SeoKeywords=@SeoKeywords,");
            strSql.Append("SeoDescription=@SeoDescription,");
            strSql.Append("UserGUID=@UserGUID,");
            strSql.Append("AdminGUID=@AdminGUID,");
            strSql.Append("MarkHtml=@MarkHtml,");
            strSql.Append("ClassName=@ClassName,");
            strSql.Append("ParentID=@ParentID,");
            strSql.Append("ParentGUID=@ParentGUID,");
            strSql.Append("ParentPath=@ParentPath,");
            strSql.Append("OrderNum=@OrderNum,");
            strSql.Append("ParentLevels=@ParentLevels,");
            strSql.Append("Title=@Title,");
            strSql.Append("SubTitle=@SubTitle,");
            strSql.Append("Initial=@Initial,");
            strSql.Append("Flags=@Flags,");
            strSql.Append("Tags=@Tags,");
            strSql.Append("Style=@Style,");
            strSql.Append("Images=@Images,");
            strSql.Append("Descriptions=@Descriptions,");
            strSql.Append("Contents=@Contents,");
            strSql.Append("Url=@Url,");
            strSql.Append("Hits=@Hits,");
            strSql.Append("NumCount=@NumCount,");
            strSql.Append("Value=@Value,");
            strSql.Append("ValueType=@ValueType,");
            strSql.Append("Lang=@Lang,");
            strSql.Append("MarkHot=@MarkHot");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@MarkStatus", SqlDbType.Int,4),
					new SqlParameter("@MarkName", SqlDbType.NVarChar,50),
					new SqlParameter("@SeoTitle", SqlDbType.NVarChar,50),
					new SqlParameter("@SeoKeywords", SqlDbType.NVarChar,50),
					new SqlParameter("@SeoDescription", SqlDbType.NVarChar,50),
					new SqlParameter("@UserGUID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@AdminGUID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@MarkHtml", SqlDbType.Int,4),
					new SqlParameter("@ClassName", SqlDbType.NVarChar,50),
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@ParentGUID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ParentPath", SqlDbType.NVarChar,50),
					new SqlParameter("@OrderNum", SqlDbType.Int,4),
					new SqlParameter("@ParentLevels", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.NVarChar,250),
					new SqlParameter("@SubTitle", SqlDbType.NVarChar,50),
					new SqlParameter("@Initial", SqlDbType.NVarChar,10),
					new SqlParameter("@Flags", SqlDbType.NVarChar,250),
					new SqlParameter("@Tags", SqlDbType.NVarChar,250),
					new SqlParameter("@Style", SqlDbType.NVarChar,50),
					new SqlParameter("@Images", SqlDbType.NVarChar,250),
					new SqlParameter("@Descriptions", SqlDbType.NVarChar,2000),
					new SqlParameter("@Contents", SqlDbType.NVarChar,255),
					new SqlParameter("@Url", SqlDbType.NVarChar,250),
					new SqlParameter("@Hits", SqlDbType.Int,4),
					new SqlParameter("@NumCount", SqlDbType.Int,4),
					new SqlParameter("@Value", SqlDbType.NVarChar,50),
					new SqlParameter("@ValueType", SqlDbType.NVarChar,50),
					new SqlParameter("@Lang", SqlDbType.NVarChar,10),
					new SqlParameter("@MarkHot", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@GUID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@Letter", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.CreateTime;
            parameters[1].Value = model.CreateUser;
            parameters[2].Value = model.UpdateTime;
            parameters[3].Value = model.UpdateUser;
            parameters[4].Value = model.MarkStatus;
            parameters[5].Value = model.MarkName;
            parameters[6].Value = model.SeoTitle;
            parameters[7].Value = model.SeoKeywords;
            parameters[8].Value = model.SeoDescription;
            parameters[9].Value = model.UserGUID;
            parameters[10].Value = model.AdminGUID;
            parameters[11].Value = model.MarkHtml;
            parameters[12].Value = model.ClassName;
            parameters[13].Value = model.ParentID;
            parameters[14].Value = model.ParentGUID;
            parameters[15].Value = model.ParentPath;
            parameters[16].Value = model.OrderNum;
            parameters[17].Value = model.ParentLevels;
            parameters[18].Value = model.Title;
            parameters[19].Value = model.SubTitle;
            parameters[20].Value = model.Initial;
            parameters[21].Value = model.Flags;
            parameters[22].Value = model.Tags;
            parameters[23].Value = model.Style;
            parameters[24].Value = model.Images;
            parameters[25].Value = model.Descriptions;
            parameters[26].Value = model.Contents;
            parameters[27].Value = model.Url;
            parameters[28].Value = model.Hits;
            parameters[29].Value = model.NumCount;
            parameters[30].Value = model.Value;
            parameters[31].Value = model.ValueType;
            parameters[32].Value = model.Lang;
            parameters[33].Value = model.MarkHot;
            parameters[34].Value = model.ID;
            parameters[35].Value = model.GUID;
            parameters[36].Value = model.Letter;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Dt_Class_News ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Dt_Class_News ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JieYiGuang.Model.T_ClassNews GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from Dt_Class_News ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            JieYiGuang.Model.T_ClassNews model = new JieYiGuang.Model.T_ClassNews();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JieYiGuang.Model.T_ClassNews DataRowToModel(DataRow row)
        {
            JieYiGuang.Model.T_ClassNews model = new JieYiGuang.Model.T_ClassNews();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["GUID"] != null && row["GUID"].ToString() != "")
                {
                    model.GUID = new Guid(row["GUID"].ToString());
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                }
                if (row["CreateUser"] != null)
                {
                    model.CreateUser = row["CreateUser"].ToString();
                }
                if (row["UpdateTime"] != null && row["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(row["UpdateTime"].ToString());
                }
                if (row["UpdateUser"] != null)
                {
                    model.UpdateUser = row["UpdateUser"].ToString();
                }
                if (row["MarkStatus"] != null && row["MarkStatus"].ToString() != "")
                {
                    model.MarkStatus = int.Parse(row["MarkStatus"].ToString());
                }
                if (row["MarkName"] != null)
                {
                    model.MarkName = row["MarkName"].ToString();
                }
                if (row["SeoTitle"] != null)
                {
                    model.SeoTitle = row["SeoTitle"].ToString();
                }
                if (row["SeoKeywords"] != null)
                {
                    model.SeoKeywords = row["SeoKeywords"].ToString();
                }
                if (row["SeoDescription"] != null)
                {
                    model.SeoDescription = row["SeoDescription"].ToString();
                }
                if (row["UserGUID"] != null && row["UserGUID"].ToString() != "")
                {
                    model.UserGUID = new Guid(row["UserGUID"].ToString());
                }
                if (row["AdminGUID"] != null && row["AdminGUID"].ToString() != "")
                {
                    model.AdminGUID = new Guid(row["AdminGUID"].ToString());
                }
                if (row["MarkHtml"] != null && row["MarkHtml"].ToString() != "")
                {
                    model.MarkHtml = int.Parse(row["MarkHtml"].ToString());
                }
                if (row["ClassName"] != null)
                {
                    model.ClassName = row["ClassName"].ToString();
                }
                if (row["ParentID"] != null && row["ParentID"].ToString() != "")
                {
                    model.ParentID = int.Parse(row["ParentID"].ToString());
                }
                if (row["ParentGUID"] != null && row["ParentGUID"].ToString() != "")
                {
                    model.ParentGUID = new Guid(row["ParentGUID"].ToString());
                }
                if (row["ParentPath"] != null)
                {
                    model.ParentPath = row["ParentPath"].ToString();
                }
                if (row["OrderNum"] != null && row["OrderNum"].ToString() != "")
                {
                    model.OrderNum = int.Parse(row["OrderNum"].ToString());
                }
                if (row["ParentLevels"] != null && row["ParentLevels"].ToString() != "")
                {
                    model.ParentLevels = int.Parse(row["ParentLevels"].ToString());
                }
                if (row["Title"] != null)
                {
                    model.Title = row["Title"].ToString();
                }
                if (row["SubTitle"] != null)
                {
                    model.SubTitle = row["SubTitle"].ToString();
                }
                if (row["Letter"] != null)
                {
                    model.Letter = row["Letter"].ToString();
                }
                if (row["Initial"] != null)
                {
                    model.Initial = row["Initial"].ToString();
                }
                if (row["Flags"] != null)
                {
                    model.Flags = row["Flags"].ToString();
                }
                if (row["Tags"] != null)
                {
                    model.Tags = row["Tags"].ToString();
                }
                if (row["Style"] != null)
                {
                    model.Style = row["Style"].ToString();
                }
                if (row["Images"] != null)
                {
                    model.Images = row["Images"].ToString();
                }
                if (row["Descriptions"] != null)
                {
                    model.Descriptions = row["Descriptions"].ToString();
                }
                if (row["Contents"] != null)
                {
                    model.Contents = row["Contents"].ToString();
                }
                if (row["Url"] != null)
                {
                    model.Url = row["Url"].ToString();
                }
                if (row["Hits"] != null && row["Hits"].ToString() != "")
                {
                    model.Hits = int.Parse(row["Hits"].ToString());
                }
                if (row["NumCount"] != null && row["NumCount"].ToString() != "")
                {
                    model.NumCount = int.Parse(row["NumCount"].ToString());
                }
                if (row["Value"] != null)
                {
                    model.Value = row["Value"].ToString();
                }
                if (row["ValueType"] != null)
                {
                    model.ValueType = row["ValueType"].ToString();
                }
                if (row["Lang"] != null)
                {
                    model.Lang = row["Lang"].ToString();
                }
                if (row["MarkHot"] != null && row["MarkHot"].ToString() != "")
                {
                    model.MarkHot = int.Parse(row["MarkHot"].ToString());
                }
                if (row["MarkIndex"] != null && row["MarkIndex"].ToString() != "")
                {
                    model.MarkIndex = byte.Parse(row["MarkIndex"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM Dt_Class_News ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM Dt_Class_News ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Dt_Class_News ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from Dt_Class_News T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "Dt_Class_News";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        //--------------------后端操作------------------------------------------------

        #region QueryCacheData 【查询缓存数据】
        public List<T_ClassNews> QueryCacheData()
        {
            var CacheKey = typeof(T_ClassNews).FullName + "_ALL";
            var List = CacheHelper.Get(CacheKey) as List<T_ClassNews>;
            if (List == null)
            {
                //读取数据库
                DataSet ds = this.GetList("");
                if (ds != null && ds.Tables.Count > 0)
                {
                    List = new List<T_ClassNews>();
                    foreach (DataRow data in ds.Tables[0].Rows)
                    {
                        List.Add(this.DataRowToModel(data));
                    }
                }

                CacheHelper.Insert(CacheKey, List, 60 * 24 * 365);
            }
            //排序
            List = List.OrderByDescending(t => (t as T_ClassNews).MarkHot)
                   .ThenBy(t => (t as T_ClassNews).OrderNum)
                   .ThenBy(t => (t as T_ClassNews).CreateTime).ToList();
            return List;
        }
        #endregion

        #region GetByID
        public T_ClassNews GetByID(int ID)
        {
            var all = QueryCacheData();
            var model = all.Find(t => (t as T_ClassNews).ID == ID);
            if (model == null)
            {
                return null;
            }
            return model;
        }

        public List<T_ClassNews> GetByID(int[] ID)
        {
            var all = QueryCacheData();
            var list = all.FindAll(t => ID.Contains((t as T_ClassNews).ID));
            if (list == null)
            {
                return null;
            }
            return list;
        }
        #endregion

        #region GetByLetter 【获取对象 根据英文】
        public T_ClassNews GetByLetter(string letter)
        {
            if (!string.IsNullOrWhiteSpace(letter))
            {
                var ClassList = QueryCacheData();
                letter = letter.ToLower();
                return ClassList.Find(t => (t as T_ClassNews).Letter == letter);
            }
            return null;
        }
        #endregion

        //--------------------后端操作------------------------------------------------

        #region ManageArrayAction
        public int ManageArrayAction(string idArray, string doAction, int doClassID, string AdminName)
        {
            if (!string.IsNullOrEmpty(idArray) && !string.IsNullOrEmpty(doAction))
            {
                var partnoArr = idArray.Trim().Split(',');
                var keyArray = Array.ConvertAll<string, int>(partnoArr, delegate(string s) { return int.Parse(s); });
                var List = QueryCacheData().Where(t => keyArray.Contains((t as T_ClassNews).ID)).ToList();
                var Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "@UpdateTime", Value = DateTime.Now, DbType = DbType.DateTime });
                Params.Add(new SqlParameter() { ParameterName = "@UpdateUser", Value = AdminName, DbType = DbType.String });
                var SQL = "Update Dt_Class_News Set UpdateTime=@UpdateTime,UpdateUser=@UpdateUser,";
                switch (doAction)
                {
                    //刷新发布时间
                    case "UpdateTimeTo":
                        SQL = SQL + "CreateTime=@CreateTime ";
                        Params.Add(new SqlParameter() { ParameterName = "@CreateTime", Value = DateTime.Now, DbType = DbType.DateTime });
                        break;
                    //移动到
                    case "MoreTo":
                        SQL = SQL + "ParentID=@ParentID,ParentGUID=@ParentGUID ";
                        var ParentClass = QueryCacheData().Find(p => (p as T_ClassNews).ID == doClassID);
                        Params.Add(new SqlParameter() { ParameterName = "@ParentID", Value = doClassID, DbType = DbType.Int32 });
                        Params.Add(new SqlParameter() { ParameterName = "@ParentGUID", Value = (ParentClass as T_ClassNews).GUID, DbType = DbType.Guid });
                        break;
                    //置顶6
                    case "Hot6":
                        SQL = SQL + "MarkHot=6 ";
                        break;
                    //置顶5
                    case "Hot5":
                        SQL = SQL + "MarkHot=5 ";
                        break;
                    //置顶4
                    case "Hot4":
                        SQL = SQL + "MarkHot=4 ";
                        break;
                    //置顶3
                    case "Hot3":
                        SQL = SQL + "MarkHot=3 ";
                        break;
                }
                switch (doAction)
                {
                    //置顶2
                    case "Hot2":
                        SQL = SQL + "MarkHot=2 ";
                        break;
                    //置顶1
                    case "Hot1":
                        SQL = SQL + "MarkHot=1 ";
                        break;
                    //撤销置顶
                    case "HotFalse":
                        SQL = SQL + "MarkHot=0 ";
                        break;
                }
                switch (doAction)
                {
                    //已发布
                    case "Started":
                        SQL = SQL + "MarkStatus=0 ";
                        break;
                    //锁定中
                    case "Locking":
                        SQL = SQL + "MarkStatus=10 ";
                        break;
                    //审核中
                    case "Checking":
                        SQL = SQL + "MarkStatus=20 ";
                        break;
                    //已过期
                    case "Overdued":
                        SQL = SQL + "MarkStatus=30 ";
                        break;
                    //已停用
                    case "Stopped":
                        SQL = SQL + "MarkStatus=40 ";
                        break;
                    //删除
                    case "Delete":
                        SQL = SQL + "MarkStatus=-1 ";
                        break;
                }
                if (doAction.ToUpper().StartsWith("FETCHINDEX"))
                {
                    SQL = SQL + string.Format("markindex={0} ", doAction.ToUpper().Substring("FETCHINDEX".Length));
                }
                SQL = SQL + " Where ID IN (" + idArray + ")";
                return DbHelperSQL.ExecuteNonQuery(SQL, Params.ToArray());
            }
            return 0;
        }
        #endregion

        #region UpdateOrders 【更新分类排序】
        public int UpdateOrders(string idArray, string idOrderArray, string AdminName)
        {
            var SQL = "Update Dt_Class_News Set OrderNum=@Order Where ID=@ID";
            var idArrayList = idArray.Trim().Split(',');
            var idOrderArrayList = idOrderArray.Trim().Split(',');
            if (idArrayList.Length == idOrderArrayList.Length)
            {
                SqlParameter[] Params;
                for (var i = 0; i < idArrayList.Length; i++)
                {
                    Params = new SqlParameter[2];
                    Params[0] = new SqlParameter() { ParameterName = "@ID", Value = idArrayList[i], DbType = DbType.Int32 };
                    Params[1] = new SqlParameter() { ParameterName = "@Order", Value = idOrderArrayList[i], DbType = DbType.Int32 };
                    DbHelperSQL.ExecuteNonQuery(SQL, Params);
                }
                return idArrayList.Length;
            }
            return 0;
        }
        #endregion

        #region UpdateAllParentPath 【重建分类关系】
        public bool UpdateAllParentPath(int parentID)
        {
            var parm = new IDataParameter[1];
            parm[0] = (new SqlParameter()
            {
                ParameterName = "@TableName",
                Value = "Dt_Class_News",
                DbType = System.Data.DbType.String
            });
            DbHelperSQL.RunProcedure("P_UpdateClassInfo", parm);
            return true;
        }
        #endregion
        #endregion  ExtensionMethod
    }
}
