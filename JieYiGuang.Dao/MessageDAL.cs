using JieYiGuang.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace JieYiGuang.Dao
{
    public class MessageDAL
    {
        public MessageDAL()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from DT_Message");
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
        public int Add(JieYiGuang.Model.T_Message model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into DT_Message(");
            strSql.Append("GUID,CreateTime,CreateUser,UpdateTime,UpdateUser,Name,Phone,Email,Address,Contents,FromBrower,FromIP,MarkStatus,Department,Company,Province,PostCode,Country,City,WebSite,Fax)");
            strSql.Append(" values (");
            strSql.Append("@GUID,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser,@Name,@Phone,@Email,@Address,@Contents,@FromBrower,@FromIP,@MarkStatus,@Department,@Company,@Province,@PostCode,@Country,@City,@WebSite,@Fax)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@GUID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Phone", SqlDbType.NVarChar,50),
					new SqlParameter("@Email", SqlDbType.NVarChar,80),
					new SqlParameter("@Address", SqlDbType.NVarChar,200),
					new SqlParameter("@Contents", SqlDbType.NVarChar,-1),
					new SqlParameter("@FromBrower", SqlDbType.NVarChar,200),
					new SqlParameter("@FromIP", SqlDbType.NVarChar,50),
					new SqlParameter("@MarkStatus", SqlDbType.Int,4),
					new SqlParameter("@Department", SqlDbType.NVarChar,50),
					new SqlParameter("@Company", SqlDbType.NVarChar,50),
					new SqlParameter("@Province", SqlDbType.NVarChar,50),
					new SqlParameter("@PostCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Country", SqlDbType.NVarChar,50),
					new SqlParameter("@City", SqlDbType.NVarChar,50),
					new SqlParameter("@WebSite", SqlDbType.NVarChar,100),
					new SqlParameter("@Fax", SqlDbType.NVarChar,50)};
            parameters[0].Value = Guid.NewGuid();
            parameters[1].Value = model.CreateTime;
            parameters[2].Value = model.CreateUser;
            parameters[3].Value = model.UpdateTime;
            parameters[4].Value = model.UpdateUser;
            parameters[5].Value = model.Name;
            parameters[6].Value = model.Phone;
            parameters[7].Value = model.Email;
            parameters[8].Value = model.Address;
            parameters[9].Value = model.Contents;
            parameters[10].Value = model.FromBrower;
            parameters[11].Value = model.FromIP;
            parameters[12].Value = model.MarkStatus;
            parameters[13].Value = model.Department;
            parameters[14].Value = model.Company;
            parameters[15].Value = model.Province;
            parameters[16].Value = model.PostCode;
            parameters[17].Value = model.Country;
            parameters[18].Value = model.City;
            parameters[19].Value = model.WebSite;
            parameters[20].Value = model.Fax;

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
        public bool Update(JieYiGuang.Model.T_Message model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DT_Message set ");
            strSql.Append("GUID=@GUID,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("CreateUser=@CreateUser,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("UpdateUser=@UpdateUser,");
            strSql.Append("Name=@Name,");
            strSql.Append("Phone=@Phone,");
            strSql.Append("Email=@Email,");
            strSql.Append("Address=@Address,");
            strSql.Append("Contents=@Contents,");
            strSql.Append("FromBrower=@FromBrower,");
            strSql.Append("FromIP=@FromIP,");
            strSql.Append("MarkStatus=@MarkStatus");
            strSql.Append("Department=@Department,");
            strSql.Append("Company=@Company,");
            strSql.Append("Province=@Province,");
            strSql.Append("PostCode=@PostCode,");
            strSql.Append("Country=@Country,");
            strSql.Append("City=@City,");
            strSql.Append("WebSite=@WebSite,");
            strSql.Append("Fax=@Fax");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@GUID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Phone", SqlDbType.NVarChar,50),
					new SqlParameter("@Email", SqlDbType.NVarChar,80),
					new SqlParameter("@Address", SqlDbType.NVarChar,200),
					new SqlParameter("@Contents", SqlDbType.NVarChar,-1),
					new SqlParameter("@FromBrower", SqlDbType.NVarChar,200),
					new SqlParameter("@FromIP", SqlDbType.NVarChar,50),
					new SqlParameter("@MarkStatus", SqlDbType.Int,4),
					new SqlParameter("@Department", SqlDbType.NVarChar,50),
					new SqlParameter("@Company", SqlDbType.NVarChar,50),
					new SqlParameter("@Province", SqlDbType.NVarChar,50),
					new SqlParameter("@PostCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Country", SqlDbType.NVarChar,50),
					new SqlParameter("@City", SqlDbType.NVarChar,50),
					new SqlParameter("@WebSite", SqlDbType.NVarChar,100),
					new SqlParameter("@Fax", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.GUID;
            parameters[1].Value = model.CreateTime;
            parameters[2].Value = model.CreateUser;
            parameters[3].Value = model.UpdateTime;
            parameters[4].Value = model.UpdateUser;
            parameters[5].Value = model.Name;
            parameters[6].Value = model.Phone;
            parameters[7].Value = model.Email;
            parameters[8].Value = model.Address;
            parameters[9].Value = model.Contents;
            parameters[10].Value = model.FromBrower;
            parameters[11].Value = model.FromIP;
            parameters[12].Value = model.MarkStatus;
            parameters[13].Value = model.Department;
            parameters[14].Value = model.Company;
            parameters[15].Value = model.Province;
            parameters[16].Value = model.PostCode;
            parameters[17].Value = model.Country;
            parameters[18].Value = model.City;
            parameters[19].Value = model.WebSite;
            parameters[20].Value = model.Fax;
            parameters[21].Value = model.ID;

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
            strSql.Append("delete from DT_Message ");
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
            strSql.Append("delete from DT_Message ");
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
        public JieYiGuang.Model.T_Message GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,GUID,CreateTime,CreateUser,UpdateTime,UpdateUser,Name,Phone,Email,Address,Contents,FromBrower,FromIP,Department,Company,Province,PostCode,Country,City,WebSite,Fax from DT_Message ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            JieYiGuang.Model.T_Message model = new JieYiGuang.Model.T_Message();
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
        public JieYiGuang.Model.T_Message DataRowToModel(DataRow row)
        {
            JieYiGuang.Model.T_Message model = new JieYiGuang.Model.T_Message();
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
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["Phone"] != null)
                {
                    model.Phone = row["Phone"].ToString();
                }
                if (row["Email"] != null)
                {
                    model.Email = row["Email"].ToString();
                }
                if (row["Address"] != null)
                {
                    model.Address = row["Address"].ToString();
                }
                if (row["Contents"] != null)
                {
                    model.Contents = row["Contents"].ToString();
                }
                if (row["FromBrower"] != null)
                {
                    model.FromBrower = row["FromBrower"].ToString();
                }
                if (row["FromIP"] != null)
                {
                    model.FromIP = row["FromIP"].ToString();
                }
                if (row["Department"] != null)
                {
                    model.Department = row["Department"].ToString();
                }
                if (row["Company"] != null)
                {
                    model.Company = row["Company"].ToString();
                }
                if (row["Province"] != null)
                {
                    model.Province = row["Province"].ToString();
                }
                if (row["PostCode"] != null)
                {
                    model.PostCode = row["PostCode"].ToString();
                }
                if (row["Country"] != null)
                {
                    model.Country = row["Country"].ToString();
                }
                if (row["City"] != null)
                {
                    model.City = row["City"].ToString();
                }
                if (row["WebSite"] != null)
                {
                    model.WebSite = row["WebSite"].ToString();
                }
                if (row["Fax"] != null)
                {
                    model.Fax = row["Fax"].ToString();
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
            strSql.Append(" FROM DT_Message ");
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
            strSql.Append(" FROM DT_Message ");
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
            strSql.Append("select count(1) FROM DT_Message ");
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
            strSql.Append(")AS Row, T.*  from DT_Message T ");
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
            parameters[0].Value = "DT_Message";
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

        #endregion  ExtensionMethod
    }
}