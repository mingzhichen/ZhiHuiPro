using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using JieYiGuang.Model;
using JieYiGuang.Common;
using System.Collections.Generic;//Please add references
using System.Linq;
using JieYiGuang.Dto;
using JieYiGuang.DTO;

namespace JieYiGuang.DAL
{

    /// <summary>
    /// 数据访问类:T_T_ClassNewsDAL
    /// </summary>
    public partial class NewsItemDAL
    {
        public NewsItemDAL()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Dt_News_Item");
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
        public int Add(JieYiGuang.Model.T_NewsItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Dt_News_Item(");
            strSql.Append("GUID,CreateTime,CreateUser,UpdateTime,UpdateUser,MarkStatus,MarkName,SeoTitle,SeoKeywords,SeoDescription,UserGUID,AdminGUID,MarkHtml,FromGUID,Title,SubTitle,Style,Flags,Tags,Type,Width,Height,Size,Path,Url,Exp,Descriptions,Orders,Hits,MarkHot,Contents)");
            strSql.Append(" values (");
            strSql.Append("@GUID,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser,@MarkStatus,@MarkName,@SeoTitle,@SeoKeywords,@SeoDescription,@UserGUID,@AdminGUID,@MarkHtml,@FromGUID,@Title,@SubTitle,@Style,@Flags,@Tags,@Type,@Width,@Height,@Size,@Path,@Url,@Exp,@Descriptions,@Orders,@Hits,@MarkHot,@Contents)");
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
					new SqlParameter("@FromGUID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@Title", SqlDbType.NVarChar,250),
					new SqlParameter("@SubTitle", SqlDbType.NVarChar,200),
					new SqlParameter("@Style", SqlDbType.NVarChar,200),
					new SqlParameter("@Flags", SqlDbType.NVarChar,250),
					new SqlParameter("@Tags", SqlDbType.NVarChar,200),
					new SqlParameter("@Type", SqlDbType.NVarChar,50),
					new SqlParameter("@Width", SqlDbType.Int,4),
					new SqlParameter("@Height", SqlDbType.Int,4),
					new SqlParameter("@Size", SqlDbType.Int,4),
					new SqlParameter("@Path", SqlDbType.NVarChar,250),
					new SqlParameter("@Url", SqlDbType.NVarChar,250),
					new SqlParameter("@Exp", SqlDbType.NVarChar,10),
					new SqlParameter("@Descriptions", SqlDbType.NVarChar,2000),
					new SqlParameter("@Orders", SqlDbType.Int,4),
					new SqlParameter("@Hits", SqlDbType.Int,4),
					new SqlParameter("@MarkHot", SqlDbType.Int,4),
					new SqlParameter("@Contents", SqlDbType.NText)};
            parameters[0].Value = model.GUID;
            parameters[1].Value = model.CreateTime;
            parameters[2].Value = model.CreateUser;
            parameters[3].Value = model.UpdateTime;
            parameters[4].Value = model.UpdateUser;
            parameters[5].Value = model.MarkStatus;
            parameters[6].Value = model.MarkName;
            parameters[7].Value = model.SeoTitle;
            parameters[8].Value = model.SeoKeywords;
            parameters[9].Value = model.SeoDescription;
            parameters[10].Value = model.UserGUID;
            parameters[11].Value = model.AdminGUID;
            parameters[12].Value = model.MarkHtml;
            parameters[13].Value = model.FromGUID;
            parameters[14].Value = model.Title;
            parameters[15].Value = model.SubTitle;
            parameters[16].Value = model.Style;
            parameters[17].Value = model.Flags;
            parameters[18].Value = model.Tags;
            parameters[19].Value = model.Type;
            parameters[20].Value = model.Width;
            parameters[21].Value = model.Height;
            parameters[22].Value = model.Size;
            parameters[23].Value = model.Path;
            parameters[24].Value = model.Url;
            parameters[25].Value = model.Exp;
            parameters[26].Value = model.Descriptions;
            parameters[27].Value = model.Orders;
            parameters[28].Value = model.Hits;
            parameters[29].Value = model.MarkHot;
            parameters[30].Value = model.Contents;

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
        public bool Update(JieYiGuang.Model.T_NewsItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Dt_News_Item set ");
            strSql.Append("GUID=@GUID,");
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
            strSql.Append("FromGUID=@FromGUID,");
            strSql.Append("Title=@Title,");
            strSql.Append("SubTitle=@SubTitle,");
            strSql.Append("Style=@Style,");
            strSql.Append("Flags=@Flags,");
            strSql.Append("Tags=@Tags,");
            strSql.Append("Type=@Type,");
            strSql.Append("Width=@Width,");
            strSql.Append("Height=@Height,");
            strSql.Append("Size=@Size,");
            strSql.Append("Path=@Path,");
            strSql.Append("Url=@Url,");
            strSql.Append("Exp=@Exp,");
            strSql.Append("Descriptions=@Descriptions,");
            strSql.Append("Orders=@Orders,");
            strSql.Append("Hits=@Hits,");
            strSql.Append("MarkHot=@MarkHot,");
            strSql.Append("Contents=@Contents");
            strSql.Append(" where ID=@ID");
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
					new SqlParameter("@FromGUID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@Title", SqlDbType.NVarChar,250),
					new SqlParameter("@SubTitle", SqlDbType.NVarChar,200),
					new SqlParameter("@Style", SqlDbType.NVarChar,200),
					new SqlParameter("@Flags", SqlDbType.NVarChar,250),
					new SqlParameter("@Tags", SqlDbType.NVarChar,200),
					new SqlParameter("@Type", SqlDbType.NVarChar,50),
					new SqlParameter("@Width", SqlDbType.Int,4),
					new SqlParameter("@Height", SqlDbType.Int,4),
					new SqlParameter("@Size", SqlDbType.Int,4),
					new SqlParameter("@Path", SqlDbType.NVarChar,250),
					new SqlParameter("@Url", SqlDbType.NVarChar,250),
					new SqlParameter("@Exp", SqlDbType.NVarChar,10),
					new SqlParameter("@Descriptions", SqlDbType.NVarChar,2000),
					new SqlParameter("@Orders", SqlDbType.Int,4),
					new SqlParameter("@Hits", SqlDbType.Int,4),
					new SqlParameter("@MarkHot", SqlDbType.Int,4),
					new SqlParameter("@Contents", SqlDbType.NText),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.GUID;
            parameters[1].Value = model.CreateTime;
            parameters[2].Value = model.CreateUser;
            parameters[3].Value = model.UpdateTime;
            parameters[4].Value = model.UpdateUser;
            parameters[5].Value = model.MarkStatus;
            parameters[6].Value = model.MarkName;
            parameters[7].Value = model.SeoTitle;
            parameters[8].Value = model.SeoKeywords;
            parameters[9].Value = model.SeoDescription;
            parameters[10].Value = model.UserGUID;
            parameters[11].Value = model.AdminGUID;
            parameters[12].Value = model.MarkHtml;
            parameters[13].Value = model.FromGUID;
            parameters[14].Value = model.Title;
            parameters[15].Value = model.SubTitle;
            parameters[16].Value = model.Style;
            parameters[17].Value = model.Flags;
            parameters[18].Value = model.Tags;
            parameters[19].Value = model.Type;
            parameters[20].Value = model.Width;
            parameters[21].Value = model.Height;
            parameters[22].Value = model.Size;
            parameters[23].Value = model.Path;
            parameters[24].Value = model.Url;
            parameters[25].Value = model.Exp;
            parameters[26].Value = model.Descriptions;
            parameters[27].Value = model.Orders;
            parameters[28].Value = model.Hits;
            parameters[29].Value = model.MarkHot;
            parameters[30].Value = model.Contents;
            parameters[31].Value = model.ID;

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
            strSql.Append("delete from Dt_News_Item ");
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
            strSql.Append("delete from Dt_News_Item ");
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
        public JieYiGuang.Model.T_NewsItem GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 convert(int,id) as id,GUID,CreateTime,CreateUser,UpdateTime,UpdateUser,MarkStatus,MarkName,SeoTitle,SeoKeywords,SeoDescription,UserGUID,AdminGUID,MarkHtml,FromGUID,Title,SubTitle,Style,Flags,Tags,Type,Width,Height,Size,Path,Url,Exp,Descriptions,Orders,Hits,MarkHot,Contents from Dt_News_Item ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            JieYiGuang.Model.T_NewsItem model = new JieYiGuang.Model.T_NewsItem();
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
        public JieYiGuang.Model.T_NewsItem DataRowToModel(DataRow row)
        {
            JieYiGuang.Model.T_NewsItem model = new JieYiGuang.Model.T_NewsItem();
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
                if (row["FromGUID"] != null && row["FromGUID"].ToString() != "")
                {
                    model.FromGUID = new Guid(row["FromGUID"].ToString());
                }
                if (row["Title"] != null)
                {
                    model.Title = row["Title"].ToString();
                }
                if (row["SubTitle"] != null)
                {
                    model.SubTitle = row["SubTitle"].ToString();
                }
                if (row["Style"] != null)
                {
                    model.Style = row["Style"].ToString();
                }
                if (row["Flags"] != null)
                {
                    model.Flags = row["Flags"].ToString();
                }
                if (row["Tags"] != null)
                {
                    model.Tags = row["Tags"].ToString();
                }
                if (row["Type"] != null)
                {
                    model.Type = row["Type"].ToString();
                }
                if (row["Width"] != null && row["Width"].ToString() != "")
                {
                    model.Width = int.Parse(row["Width"].ToString());
                }
                if (row["Height"] != null && row["Height"].ToString() != "")
                {
                    model.Height = int.Parse(row["Height"].ToString());
                }
                if (row["Size"] != null && row["Size"].ToString() != "")
                {
                    model.Size = int.Parse(row["Size"].ToString());
                }
                if (row["Path"] != null)
                {
                    model.Path = row["Path"].ToString();
                }
                if (row["Url"] != null)
                {
                    model.Url = row["Url"].ToString();
                }
                if (row["Exp"] != null)
                {
                    model.Exp = row["Exp"].ToString();
                }
                if (row["Descriptions"] != null)
                {
                    model.Descriptions = row["Descriptions"].ToString();
                }
                if (row["Orders"] != null && row["Orders"].ToString() != "")
                {
                    model.Orders = int.Parse(row["Orders"].ToString());
                }
                if (row["Hits"] != null && row["Hits"].ToString() != "")
                {
                    model.Hits = int.Parse(row["Hits"].ToString());
                }
                if (row["MarkHot"] != null && row["MarkHot"].ToString() != "")
                {
                    model.MarkHot = int.Parse(row["MarkHot"].ToString());
                }
                if (row["Contents"] != null)
                {
                    model.Contents = row["Contents"].ToString();
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
            strSql.Append("select convert(int,id) as id,GUID,CreateTime,CreateUser,UpdateTime,UpdateUser,MarkStatus,MarkName,SeoTitle,SeoKeywords,SeoDescription,UserGUID,AdminGUID,MarkHtml,FromGUID,Title,SubTitle,Style,Flags,Tags,Type,Width,Height,Size,Path,Url,Exp,Descriptions,Orders,Hits,MarkHot,Contents ");
            strSql.Append(" FROM Dt_News_Item ");
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
            strSql.Append(" convert(int,id) as id,GUID,CreateTime,CreateUser,UpdateTime,UpdateUser,MarkStatus,MarkName,SeoTitle,SeoKeywords,SeoDescription,UserGUID,AdminGUID,MarkHtml,FromGUID,Title,SubTitle,Style,Flags,Tags,Type,Width,Height,Size,Path,Url,Exp,Descriptions,Orders,Hits,MarkHot,Contents ");
            strSql.Append(" FROM Dt_News_Item ");
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
            strSql.Append("select count(1) FROM Dt_News_Item ");
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
            strSql.Append(")AS Row, T.*  from Dt_News_Item T ");
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
            parameters[0].Value = "Dt_News_Item";
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

        // ------------------- 基本操作 ------------------- 

        #region GetItemList 【根据资讯的GUID获取词条列表】
        public List<T_NewsItem> GetItemList(Guid GUID)
        {
            var sql = "select * from dt_News_item where MarkStatus!=-1 and FromGUID=@GUID order by orders";
            var param = new SqlParameter()
            {
                ParameterName = "@GUID",
                Value = GUID,
                DbType = System.Data.DbType.Guid
            };

            var dt = DbHelperSQL.ExecuteModelList(sql, param);
            var List = new List<T_NewsItem>();
            if (dt != null)
            {
                foreach (DataRow data in dt.Rows)
                {
                    List.Add(this.DataRowToModel(data));
                }
            }
            return List;
        }
        #endregion

        // ------------------- 前端查询 ------------------- 

        #region GetList 【前端查询获取资讯列表】

        public List<T_NewsItem> GetList(NewsParam Param, PagerInfo PageInfo)
        {
            #region 查询语句及参数设定
            var sql = new StringBuilder();
            sql.Append("SELECT n.MarkHot NewsMarkHot,n.Hits NewsHits,n.NumFavorite,n.NumGood,n.NumComment,n.ClassID,convert(int,i.id) as id,i.[GUID],i.[CreateTime],i.[CreateUser],i.[UpdateTime],i.[UpdateUser],i.[MarkStatus],i.[MarkName],i.[UserGUID],i.[AdminGUID],i.[FromGUID],i.[Title],i.[SubTitle],i.[Style],i.[Flags],i.[Tags],i.[Type],i.[Width],i.[Height],i.[Size],i.[Path],i.[Url],i.[Exp] ,i.[Descriptions],i.[Orders],i.[Hits],i.[Contents] ");
            sql.Append("FROM DT_News_Item i  ");
            sql.Append("LEFT JOIN dt_News n ON i.FromGUID=n.GUID ");
            sql.Append("WHERE n.MarkStatus=0 AND i.MarkStatus=0  ");
            var paramList = new List<SqlParameter>();

            #region 关键字搜索 KeyWord
            if (!string.IsNullOrEmpty(Param.KeyWord))
            {
                sql.Append("And (n.Title Like @KeyWord Or u.NickName=@KeyWord)");
                paramList.Add(new SqlParameter()
                {
                    ParameterName = "@KeyWord",
                    Value = "%" + Param.KeyWord + "%",
                    DbType = System.Data.DbType.String
                });
            }
            #endregion

            #region 类别搜索 ClassIDStr
            if (Param.ClassID > 0)
            {
                sql.AppendFormat("And n.ClassID IN ({0}) ", Param.ClassIDStr);
            }
            #endregion

            #region 标签搜索 Tags
            if (!string.IsNullOrEmpty(Param.Tags))
            {
                sql.Append("And (");
                foreach (var item in Param.Tags.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                {
                    sql.AppendFormat("n.Tags LIKE '%,{0},%' OR ", item);
                }
                sql.Length = sql.Length - 3;
                sql.Append(") ");
            }
            #endregion

            #region 标志搜索 Flags
            if (!string.IsNullOrEmpty(Param.Flags))
            {
                sql.Append("And (");
                foreach (var item in Param.Flags.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                {
                    sql.AppendFormat("n.Flags LIKE '%{0}%' OR ", item);
                }
                sql.Length = sql.Length - 3;
                sql.Append(") ");
            }
            #endregion

            #region 咨询GUID GUID
            if (!string.IsNullOrEmpty(Param.MarkName) && Param.MarkName.ToUpper() != "ALL")
            {
                sql.AppendFormat("And n.FromGUID=@FromGUID ");
                paramList.Add(new SqlParameter()
                {
                    ParameterName = "@FromGUID",
                    Value = Param.FromGUID,
                    DbType = System.Data.DbType.String
                });
            }
            #endregion

            #region 标记搜索 MarkName
            if (!string.IsNullOrEmpty(Param.MarkName) && Param.MarkName.ToUpper() != "ALL")
            {
                sql.AppendFormat("And n.MarkName=@MarkName ");
                paramList.Add(new SqlParameter()
                {
                    ParameterName = "@MarkName",
                    Value = Param.MarkName,
                    DbType = System.Data.DbType.String
                });
            }
            #endregion

            #endregion

            var OrderStr = string.Empty;
            if (Param.OrderEnum != null && Param.OrderEnum.Length > 0)
            {
                Param.OrderEnum.ToList().ForEach(t =>
                {
                    OrderStr = string.Format("{0},{1} {2}", OrderStr, t.ToString(), (int)t == 1 ? "DESC" : "ASC");
                });
            }
            else
            {
                OrderStr = "NewsMarkHot Desc,NewsHits Desc,NumFavorite Desc,NumGood Desc,NumComment Desc,Orders,CreateTime Desc";
            }
            //分页查询
            if (PageInfo != null)
            {
                int RowCount = 0;
                var ds = DbHelperSQL.ExecutePageModel(sql.ToString(), OrderStr, PageInfo.PageSize, PageInfo.CurrentPageIndex, out RowCount, paramList.ToArray());
                PageInfo.RowCount = RowCount;

                var List = new List<T_NewsItem>();
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow data in ds.Tables[0].Rows)
                    {
                        List.Add(this.DataRowToModel(data));
                    }
                }
                PageInfo.RowCount = RowCount;
                return List;
            }
            else
            {
                var dt = DbHelperSQL.ExecuteRangeModel(sql.ToString(), OrderStr, Param.Start, Param.Max, paramList.ToArray());

                var List = new List<T_NewsItem>();
                if (dt != null)
                {
                    foreach (DataRow data in dt.Rows)
                    {
                        List.Add(this.DataRowToModel(data));
                    }
                }
                return List;
            }
        }
        #endregion

        #region GetList 【按资讯的GUID获取图集列表】
        public List<T_NewsItem> GetList(Guid[] NewsGUIDList)
        {
            var guids = NewsGUIDList.Select(t => t.ToString()).ToArray().ToJoinString(",");
            guids = "'" + guids.Replace(",", "','") + "'";
            var sql = string.Format("select * from dt_news_item where fromguid in ({0}) order by fromguid", guids);
            var dt = DbHelperSQL.ExecuteModelList(sql);
            var List = new List<T_NewsItem>();
            if (dt != null)
            {
                foreach (DataRow data in dt.Rows)
                {
                    List.Add(this.DataRowToModel(data));
                }
            }
            return List;
        }
        #endregion
        #endregion  ExtensionMethod
    }
}
