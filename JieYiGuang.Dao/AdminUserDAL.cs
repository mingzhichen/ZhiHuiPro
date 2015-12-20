using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using JieYiGuang.Common;
using System.Collections.Generic;
using JieYiGuang.Dto;

namespace JieYiGuang.DAL
{
    /// <summary>
    /// 数据访问类:管理员
    /// </summary>
    public partial class AdminUserDAL
    {
        private string databaseprefix; //数据库表名前缀
        public AdminUserDAL(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region 基本方法=============================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "Admin_User");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 查询用户名是否存在
        /// </summary>
        public bool Exists(string user_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "Admin_User");
            strSql.Append(" where user_name=@user_name ");
            SqlParameter[] parameters = {
					new SqlParameter("@user_name", SqlDbType.NVarChar,100)};
            parameters[0].Value = user_name;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JieYiGuang.Model.T_AdminUser model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Dt_Admin_User(");
            strSql.Append("CreateTime,UpdateTime,MarkStatus,MarkName,UserName,AdminPassword,Name,Mobile,Email,Note,IsLock)");
            strSql.Append(" values (");
            strSql.Append("@CreateTime,@UpdateTime,@MarkStatus,@MarkName,@UserName,@AdminPassword,@Name,@Mobile,@Email,@Note,@IsLock)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@MarkStatus", SqlDbType.Int,4),
					new SqlParameter("@MarkName", SqlDbType.NVarChar,50),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@AdminPassword", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,20),
					new SqlParameter("@Mobile", SqlDbType.NVarChar,20),
					new SqlParameter("@Email", SqlDbType.NVarChar,30),
					new SqlParameter("@Note", SqlDbType.NVarChar,255),
					new SqlParameter("@IsLock", SqlDbType.Int,4)};
            parameters[0].Value = model.CreateTime;
            parameters[1].Value = model.UpdateTime;
            parameters[2].Value = model.MarkStatus;
            parameters[3].Value = model.MarkName;
            parameters[4].Value = model.UserName;
            parameters[5].Value = model.AdminPassword;
            parameters[6].Value = model.Name;
            parameters[7].Value = model.Mobile;
            parameters[8].Value = model.Email;
            parameters[9].Value = model.Note;
            parameters[10].Value = model.IsLock;

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
        public bool Update(JieYiGuang.Model.T_AdminUser model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Dt_Admin_User set ");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("MarkStatus=@MarkStatus,");
            strSql.Append("MarkName=@MarkName,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("AdminPassword=@AdminPassword,");
            strSql.Append("Name=@Name,");
            strSql.Append("Mobile=@Mobile,");
            strSql.Append("Email=@Email,");
            strSql.Append("Note=@Note,");
            strSql.Append("IsLock=@IsLock");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@MarkStatus", SqlDbType.Int,4),
					new SqlParameter("@MarkName", SqlDbType.NVarChar,50),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@AdminPassword", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,20),
					new SqlParameter("@Mobile", SqlDbType.NVarChar,20),
					new SqlParameter("@Email", SqlDbType.NVarChar,30),
					new SqlParameter("@Note", SqlDbType.NVarChar,255),
					new SqlParameter("@IsLock", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.CreateTime;
            parameters[1].Value = model.UpdateTime;
            parameters[2].Value = model.MarkStatus;
            parameters[3].Value = model.MarkName;
            parameters[4].Value = model.UserName;
            parameters[5].Value = model.AdminPassword;
            parameters[6].Value = model.Name;
            parameters[7].Value = model.Mobile;
            parameters[8].Value = model.Email;
            parameters[9].Value = model.Note;
            parameters[10].Value = model.IsLock;
            parameters[11].Value = model.ID;

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
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "Admin_User ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

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
        /// 得到一个对象实体
        /// </summary>
        public JieYiGuang.Model.T_AdminUser GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from Dt_Admin_User ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            JieYiGuang.Model.T_AdminUser model = new JieYiGuang.Model.T_AdminUser();
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
        /// 根据用户名密码返回一个实体
        /// </summary>
        public JieYiGuang.Model.T_AdminUser GetModel(string user_name, string password)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id from " + databaseprefix + "Admin_User");
            strSql.Append(" where username=@user_name and AdminPassword=@password and islock=0");
            SqlParameter[] parameters = {
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
                    new SqlParameter("@password", SqlDbType.NVarChar,100)};
            parameters[0].Value = user_name;
            parameters[1].Value = password;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return GetModel(Convert.ToInt32(obj));
            }
            return null;
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
            strSql.Append(" FROM " + databaseprefix + "Admin_User ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "Admin_User");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        #endregion



        // ------------------- 后台管理操作 ------------------- 

        #region ManageListPage 【后台数据管理列表查询】
        public List<JieYiGuang.Model.T_AdminUser> ManageListPage(QueryParams QueryParams, int PageSize, int PageIndex, out int rowCount)
        {
            var sql = new StringBuilder(@"SELECT * FROM [dt_Admin_User] b (NOLOCK) where 1=1 ");
            List<SqlParameter> paramList = null;
            if (QueryParams != null)
            {
                paramList = new List<SqlParameter>();

                #region 搜索关键字 SearchKey
                if (!string.IsNullOrWhiteSpace(QueryParams.SearchKey))
                {
                    int ID = 0;
                    if (int.TryParse(QueryParams.SearchKey, out ID))
                    {
                        QueryParams.SearchName = "ID";
                    }
                    //判断关键字类别
                    switch (QueryParams.SearchName.ToUpper())
                    {
                        //ID搜索
                        case "ID":
                            sql.Append("and b.ID=@SearchKey ");
                            paramList.Add(new SqlParameter()
                            {
                                ParameterName = "@SearchKey",
                                Value = ID,
                                DbType = System.Data.DbType.Int32
                            });
                            break;
                        //标题
                        case "TITLE":
                            sql.Append("and b.UserName like @SearchKey ");
                            paramList.Add(new SqlParameter()
                            {
                                ParameterName = "@SearchKey",
                                Value = "%" + QueryParams.SearchKey + "%",
                                DbType = System.Data.DbType.String
                            });
                            break;
                        //全局多条件搜索
                        default:
                            sql.Append("and (b.name like @SearchKey ");
                            paramList.Add(new SqlParameter()
                            {
                                ParameterName = "@SearchKey",
                                Value = "%" + QueryParams.SearchKey + "%",
                                DbType = System.Data.DbType.String
                            });
                            break;
                    }
                }
                #endregion

            }
            var ds = DbHelperSQL.ExecutePageModel(sql.ToString(), "CreateTime Desc", PageSize, PageIndex, out rowCount, paramList.ToArray());

            var List = new List<JieYiGuang.Model.T_AdminUser>();
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow data in ds.Tables[0].Rows)
                {
                    List.Add(this.DataRowToModel(data));
                }
            }
            return List;
        }
        #endregion


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JieYiGuang.Model.T_AdminUser DataRowToModel(DataRow row)
        {
            JieYiGuang.Model.T_AdminUser model = new JieYiGuang.Model.T_AdminUser();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                }
                if (row["UpdateTime"] != null && row["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(row["UpdateTime"].ToString());
                }
                if (row["MarkStatus"] != null && row["MarkStatus"].ToString() != "")
                {
                    model.MarkStatus = int.Parse(row["MarkStatus"].ToString());
                }
                if (row["MarkName"] != null)
                {
                    model.MarkName = row["MarkName"].ToString();
                }
                if (row["UserName"] != null)
                {
                    model.UserName = row["UserName"].ToString();
                }
                if (row["AdminPassword"] != null)
                {
                    model.AdminPassword = row["AdminPassword"].ToString();
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["Mobile"] != null)
                {
                    model.Mobile = row["Mobile"].ToString();
                }
                if (row["Email"] != null)
                {
                    model.Email = row["Email"].ToString();
                }
                if (row["Note"] != null)
                {
                    model.Note = row["Note"].ToString();
                }
                if (row["IsLock"] != null && row["IsLock"].ToString() != "")
                {
                    model.IsLock = int.Parse(row["IsLock"].ToString());
                }
            }
            return model;
        }
    }
}