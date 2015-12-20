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
    public partial class NewsDAL
    {        
		public NewsDAL()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Dt_News");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(JieYiGuang.Model.T_News model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Dt_News(");
			strSql.Append("GUID,CreateTime,CreateUser,UpdateTime,UpdateUser,MarkStatus,MarkName,SeoTitle,SeoKeywords,SeoDescription,UserGUID,AdminGUID,MarkHtml,ClassID,ItemID,TypeID,AreaID,Number,Title,SubTitle,Style,Source,Author,Tags,Flags,Type,Images,ArrayImages,ArrayFiles,LinkUrl,Descriptions,Contents,TimeBegin,TimeEnd,HtmlTemplate,Orders,Hits,MarkHot,IsComment,NumComment,NumFavorite,NumGood,NumItem,Lang,FromClassID,ItemTitle,RefID)");
			strSql.Append(" values (");
            strSql.Append("@GUID,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser,@MarkStatus,@MarkName,@SeoTitle,@SeoKeywords,@SeoDescription,@UserGUID,@AdminGUID,@MarkHtml,@ClassID,@ItemID,@TypeID,@AreaID,@Number,@Title,@SubTitle,@Style,@Source,@Author,@Tags,@Flags,@Type,@Images,@ArrayImages,@ArrayFiles,@LinkUrl,@Descriptions,@Contents,@TimeBegin,@TimeEnd,@HtmlTemplate,@Orders,@Hits,@MarkHot,@IsComment,@NumComment,@NumFavorite,@NumGood,@NumItem,@Lang,@FromClassID,@ItemTitle,@RefID)");
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
					new SqlParameter("@ClassID", SqlDbType.Int,4),
					new SqlParameter("@ItemID", SqlDbType.Int,4),
					new SqlParameter("@TypeID", SqlDbType.Int,4),
					new SqlParameter("@AreaID", SqlDbType.Int,4),
					new SqlParameter("@Number", SqlDbType.NVarChar,20),
					new SqlParameter("@Title", SqlDbType.NVarChar,250),
					new SqlParameter("@SubTitle", SqlDbType.NVarChar,250),
					new SqlParameter("@Style", SqlDbType.NVarChar,50),
					new SqlParameter("@Source", SqlDbType.NVarChar,250),
					new SqlParameter("@Author", SqlDbType.NVarChar,250),
					new SqlParameter("@Tags", SqlDbType.NVarChar,1000),
					new SqlParameter("@Flags", SqlDbType.NVarChar,250),
					new SqlParameter("@Type", SqlDbType.NVarChar,250),
					new SqlParameter("@Images", SqlDbType.NVarChar,250),
					new SqlParameter("@ArrayImages", SqlDbType.NVarChar,2000),
					new SqlParameter("@ArrayFiles", SqlDbType.NVarChar,2000),
					new SqlParameter("@LinkUrl", SqlDbType.NVarChar,250),
					new SqlParameter("@Descriptions", SqlDbType.NVarChar,2000),
					new SqlParameter("@Contents", SqlDbType.NText),
					new SqlParameter("@TimeBegin", SqlDbType.DateTime),
					new SqlParameter("@TimeEnd", SqlDbType.DateTime),
					new SqlParameter("@HtmlTemplate", SqlDbType.NVarChar,250),
					new SqlParameter("@Orders", SqlDbType.Int,4),
					new SqlParameter("@Hits", SqlDbType.Int,4),
					new SqlParameter("@MarkHot", SqlDbType.Int,4),
					new SqlParameter("@IsComment", SqlDbType.Bit,1),
					new SqlParameter("@NumComment", SqlDbType.Int,4),
					new SqlParameter("@NumFavorite", SqlDbType.Int,4),
					new SqlParameter("@NumGood", SqlDbType.Int,4),
					new SqlParameter("@NumItem", SqlDbType.Int,4),
					new SqlParameter("@Lang", SqlDbType.NVarChar,50),
					new SqlParameter("@FromClassID", SqlDbType.NVarChar,2000),
					new SqlParameter("@ItemTitle", SqlDbType.NVarChar,250),
					new SqlParameter("@RefID", SqlDbType.Int,4)};
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
			parameters[13].Value = model.ClassID;
			parameters[14].Value = model.ItemID;
			parameters[15].Value = model.TypeID;
			parameters[16].Value = model.AreaID;
			parameters[17].Value = model.Number;
			parameters[18].Value = model.Title;
			parameters[19].Value = model.SubTitle;
			parameters[20].Value = model.Style;
			parameters[21].Value = model.Source;
			parameters[22].Value = model.Author;
			parameters[23].Value = model.Tags;
			parameters[24].Value = model.Flags;
			parameters[25].Value = model.Type;
			parameters[26].Value = model.Images;
			parameters[27].Value = model.ArrayImages;
			parameters[28].Value = model.ArrayFiles;
			parameters[29].Value = model.LinkUrl;
			parameters[30].Value = model.Descriptions;
			parameters[31].Value = model.Contents;
			parameters[32].Value = model.TimeBegin;
			parameters[33].Value = model.TimeEnd;
			parameters[34].Value = model.HtmlTemplate;
			parameters[35].Value = model.Orders;
			parameters[36].Value = model.Hits;
			parameters[37].Value = model.MarkHot;
			parameters[38].Value = model.IsComment;
			parameters[39].Value = model.NumComment;
			parameters[40].Value = model.NumFavorite;
			parameters[41].Value = model.NumGood;
			parameters[42].Value = model.NumItem;
			parameters[43].Value = model.Lang;
            parameters[44].Value = model.FromClassID;
            parameters[45].Value = model.ItemTitle;
            parameters[46].Value = model.RefId;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
                model.ID = Convert.ToInt32(obj);
                return model.ID;
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(JieYiGuang.Model.T_News model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Dt_News set ");
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
			strSql.Append("ClassID=@ClassID,");
			strSql.Append("ItemID=@ItemID,");
			strSql.Append("TypeID=@TypeID,");
			strSql.Append("AreaID=@AreaID,");
			strSql.Append("Number=@Number,");
			strSql.Append("Title=@Title,");
			strSql.Append("SubTitle=@SubTitle,");
			strSql.Append("Style=@Style,");
			strSql.Append("Source=@Source,");
			strSql.Append("Author=@Author,");
			strSql.Append("Tags=@Tags,");
			strSql.Append("Flags=@Flags,");
			strSql.Append("Type=@Type,");
			strSql.Append("Images=@Images,");
			strSql.Append("ArrayImages=@ArrayImages,");
			strSql.Append("ArrayFiles=@ArrayFiles,");
			strSql.Append("LinkUrl=@LinkUrl,");
			strSql.Append("Descriptions=@Descriptions,");
			strSql.Append("Contents=@Contents,");
			strSql.Append("TimeBegin=@TimeBegin,");
			strSql.Append("TimeEnd=@TimeEnd,");
			strSql.Append("HtmlTemplate=@HtmlTemplate,");
			strSql.Append("Orders=@Orders,");
			strSql.Append("Hits=@Hits,");
			strSql.Append("MarkHot=@MarkHot,");
			strSql.Append("IsComment=@IsComment,");
			strSql.Append("NumComment=@NumComment,");
			strSql.Append("NumFavorite=@NumFavorite,");
			strSql.Append("NumGood=@NumGood,");
			strSql.Append("NumItem=@NumItem,");
			strSql.Append("Lang=@Lang,");
            strSql.Append("FromClassID=@FromClassID,");
            strSql.Append("ItemTitle=@ItemTitle,");
            strSql.Append("RefID=@RefID");
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
					new SqlParameter("@ClassID", SqlDbType.Int,4),
					new SqlParameter("@ItemID", SqlDbType.Int,4),
					new SqlParameter("@TypeID", SqlDbType.Int,4),
					new SqlParameter("@AreaID", SqlDbType.Int,4),
					new SqlParameter("@Number", SqlDbType.NVarChar,20),
					new SqlParameter("@Title", SqlDbType.NVarChar,250),
					new SqlParameter("@SubTitle", SqlDbType.NVarChar,250),
					new SqlParameter("@Style", SqlDbType.NVarChar,50),
					new SqlParameter("@Source", SqlDbType.NVarChar,250),
					new SqlParameter("@Author", SqlDbType.NVarChar,250),
					new SqlParameter("@Tags", SqlDbType.NVarChar,1000),
					new SqlParameter("@Flags", SqlDbType.NVarChar,250),
					new SqlParameter("@Type", SqlDbType.NVarChar,250),
					new SqlParameter("@Images", SqlDbType.NVarChar,250),
					new SqlParameter("@ArrayImages", SqlDbType.NVarChar,2000),
					new SqlParameter("@ArrayFiles", SqlDbType.NVarChar,2000),
					new SqlParameter("@LinkUrl", SqlDbType.NVarChar,250),
					new SqlParameter("@Descriptions", SqlDbType.NVarChar,2000),
					new SqlParameter("@Contents", SqlDbType.NText),
					new SqlParameter("@TimeBegin", SqlDbType.DateTime),
					new SqlParameter("@TimeEnd", SqlDbType.DateTime),
					new SqlParameter("@HtmlTemplate", SqlDbType.NVarChar,250),
					new SqlParameter("@Orders", SqlDbType.Int,4),
					new SqlParameter("@Hits", SqlDbType.Int,4),
					new SqlParameter("@MarkHot", SqlDbType.Int,4),
					new SqlParameter("@IsComment", SqlDbType.Bit,1),
					new SqlParameter("@NumComment", SqlDbType.Int,4),
					new SqlParameter("@NumFavorite", SqlDbType.Int,4),
					new SqlParameter("@NumGood", SqlDbType.Int,4),
					new SqlParameter("@NumItem", SqlDbType.Int,4),
					new SqlParameter("@Lang", SqlDbType.NVarChar,50),
					new SqlParameter("@FromClassID", SqlDbType.NVarChar,2000),
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@ItemTitle", SqlDbType.NVarChar,250),
					new SqlParameter("@RefID", SqlDbType.Int,4)};
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
			parameters[13].Value = model.ClassID;
			parameters[14].Value = model.ItemID;
			parameters[15].Value = model.TypeID;
			parameters[16].Value = model.AreaID;
			parameters[17].Value = model.Number;
			parameters[18].Value = model.Title;
			parameters[19].Value = model.SubTitle;
			parameters[20].Value = model.Style;
			parameters[21].Value = model.Source;
			parameters[22].Value = model.Author;
			parameters[23].Value = model.Tags;
			parameters[24].Value = model.Flags;
			parameters[25].Value = model.Type;
			parameters[26].Value = model.Images;
			parameters[27].Value = model.ArrayImages;
			parameters[28].Value = model.ArrayFiles;
			parameters[29].Value = model.LinkUrl;
			parameters[30].Value = model.Descriptions;
			parameters[31].Value = model.Contents;
			parameters[32].Value = model.TimeBegin;
			parameters[33].Value = model.TimeEnd;
			parameters[34].Value = model.HtmlTemplate;
			parameters[35].Value = model.Orders;
			parameters[36].Value = model.Hits;
			parameters[37].Value = model.MarkHot;
			parameters[38].Value = model.IsComment;
			parameters[39].Value = model.NumComment;
			parameters[40].Value = model.NumFavorite;
			parameters[41].Value = model.NumGood;
			parameters[42].Value = model.NumItem;
			parameters[43].Value = model.Lang;
			parameters[44].Value = model.FromClassID;
            parameters[45].Value = model.ID;
            parameters[46].Value = model.ItemTitle;
            parameters[47].Value = model.RefId;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Dt_News ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Dt_News ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public JieYiGuang.Model.T_News GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 ID,GUID,CreateTime,CreateUser,UpdateTime,UpdateUser,MarkStatus,MarkName,SeoTitle,SeoKeywords,SeoDescription,UserGUID,AdminGUID,MarkHtml,ClassID,ItemID,TypeID,AreaID,Number,Title,SubTitle,Style,Source,Author,Tags,Flags,Type,Images,ArrayImages,ArrayFiles,LinkUrl,Descriptions,Contents,TimeBegin,TimeEnd,HtmlTemplate,Orders,Hits,MarkHot,IsComment,NumComment,NumFavorite,NumGood,NumItem,Lang,FromClassID,ItemTitle from Dt_News ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			JieYiGuang.Model.T_News model=new JieYiGuang.Model.T_News();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
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
        public JieYiGuang.Model.T_News GetModelByRefID(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,GUID,CreateTime,CreateUser,UpdateTime,UpdateUser,MarkStatus,MarkName,SeoTitle,SeoKeywords,SeoDescription,UserGUID,AdminGUID,MarkHtml,ClassID,ItemID,TypeID,AreaID,Number,Title,SubTitle,Style,Source,Author,Tags,Flags,Type,Images,ArrayImages,ArrayFiles,LinkUrl,Descriptions,Contents,TimeBegin,TimeEnd,HtmlTemplate,Orders,Hits,MarkHot,IsComment,NumComment,NumFavorite,NumGood,NumItem,Lang,FromClassID,ItemTitle from Dt_News ");
            strSql.Append(" where RefID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            JieYiGuang.Model.T_News model = new JieYiGuang.Model.T_News();
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
		public JieYiGuang.Model.T_News DataRowToModel(DataRow row)
		{
			JieYiGuang.Model.T_News model=new JieYiGuang.Model.T_News();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["GUID"]!=null && row["GUID"].ToString()!="")
				{
					model.GUID= new Guid(row["GUID"].ToString());
				}
				if(row["CreateTime"]!=null && row["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(row["CreateTime"].ToString());
				}
				if(row["CreateUser"]!=null)
				{
					model.CreateUser=row["CreateUser"].ToString();
				}
				if(row["UpdateTime"]!=null && row["UpdateTime"].ToString()!="")
				{
					model.UpdateTime=DateTime.Parse(row["UpdateTime"].ToString());
				}
				if(row["UpdateUser"]!=null)
				{
					model.UpdateUser=row["UpdateUser"].ToString();
				}
				if(row["MarkStatus"]!=null && row["MarkStatus"].ToString()!="")
				{
					model.MarkStatus=int.Parse(row["MarkStatus"].ToString());
				}
				if(row["MarkName"]!=null)
				{
					model.MarkName=row["MarkName"].ToString();
				}
				if(row["SeoTitle"]!=null)
				{
					model.SeoTitle=row["SeoTitle"].ToString();
				}
				if(row["SeoKeywords"]!=null)
				{
					model.SeoKeywords=row["SeoKeywords"].ToString();
				}
				if(row["SeoDescription"]!=null)
				{
					model.SeoDescription=row["SeoDescription"].ToString();
				}
				if(row["UserGUID"]!=null && row["UserGUID"].ToString()!="")
				{
					model.UserGUID= new Guid(row["UserGUID"].ToString());
				}
				if(row["AdminGUID"]!=null && row["AdminGUID"].ToString()!="")
				{
					model.AdminGUID= new Guid(row["AdminGUID"].ToString());
				}
				if(row["MarkHtml"]!=null && row["MarkHtml"].ToString()!="")
				{
					model.MarkHtml=int.Parse(row["MarkHtml"].ToString());
				}
				if(row["ClassID"]!=null && row["ClassID"].ToString()!="")
				{
					model.ClassID=int.Parse(row["ClassID"].ToString());
				}
				if(row["ItemID"]!=null && row["ItemID"].ToString()!="")
				{
					model.ItemID=int.Parse(row["ItemID"].ToString());
				}
				if(row["TypeID"]!=null && row["TypeID"].ToString()!="")
				{
					model.TypeID=int.Parse(row["TypeID"].ToString());
				}
				if(row["AreaID"]!=null && row["AreaID"].ToString()!="")
				{
					model.AreaID=int.Parse(row["AreaID"].ToString());
				}
				if(row["Number"]!=null)
				{
					model.Number=row["Number"].ToString();
				}
				if(row["Title"]!=null)
				{
					model.Title=row["Title"].ToString();
				}
				if(row["SubTitle"]!=null)
				{
					model.SubTitle=row["SubTitle"].ToString();
				}
				if(row["Style"]!=null)
				{
					model.Style=row["Style"].ToString();
				}
				if(row["Source"]!=null)
				{
					model.Source=row["Source"].ToString();
				}
				if(row["Author"]!=null)
				{
					model.Author=row["Author"].ToString();
				}
				if(row["Tags"]!=null)
				{
					model.Tags=row["Tags"].ToString();
				}
				if(row["Flags"]!=null)
				{
					model.Flags=row["Flags"].ToString();
				}
				if(row["Type"]!=null)
				{
					model.Type=row["Type"].ToString();
				}
				if(row["Images"]!=null)
				{
					model.Images=row["Images"].ToString();
				}
				if(row["ArrayImages"]!=null)
				{
					model.ArrayImages=row["ArrayImages"].ToString();
				}
				if(row["ArrayFiles"]!=null)
				{
					model.ArrayFiles=row["ArrayFiles"].ToString();
				}
				if(row["LinkUrl"]!=null)
				{
					model.LinkUrl=row["LinkUrl"].ToString();
				}
				if(row["Descriptions"]!=null)
				{
					model.Descriptions=row["Descriptions"].ToString();
				}
				if(row["Contents"]!=null)
				{
					model.Contents=row["Contents"].ToString();
				}
				if(row["TimeBegin"]!=null && row["TimeBegin"].ToString()!="")
				{
					model.TimeBegin=DateTime.Parse(row["TimeBegin"].ToString());
				}
				if(row["TimeEnd"]!=null && row["TimeEnd"].ToString()!="")
				{
					model.TimeEnd=DateTime.Parse(row["TimeEnd"].ToString());
				}
				if(row["HtmlTemplate"]!=null)
				{
					model.HtmlTemplate=row["HtmlTemplate"].ToString();
				}
				if(row["Orders"]!=null && row["Orders"].ToString()!="")
				{
					model.Orders=int.Parse(row["Orders"].ToString());
				}
				if(row["Hits"]!=null && row["Hits"].ToString()!="")
				{
					model.Hits=int.Parse(row["Hits"].ToString());
				}
				if(row["MarkHot"]!=null && row["MarkHot"].ToString()!="")
				{
					model.MarkHot=int.Parse(row["MarkHot"].ToString());
				}
				if(row["IsComment"]!=null && row["IsComment"].ToString()!="")
				{
					if((row["IsComment"].ToString()=="1")||(row["IsComment"].ToString().ToLower()=="true"))
					{
						model.IsComment=true;
					}
					else
					{
						model.IsComment=false;
					}
				}
				if(row["NumComment"]!=null && row["NumComment"].ToString()!="")
				{
					model.NumComment=int.Parse(row["NumComment"].ToString());
				}
				if(row["NumFavorite"]!=null && row["NumFavorite"].ToString()!="")
				{
					model.NumFavorite=int.Parse(row["NumFavorite"].ToString());
				}
				if(row["NumGood"]!=null && row["NumGood"].ToString()!="")
				{
					model.NumGood=int.Parse(row["NumGood"].ToString());
				}
				if(row["NumItem"]!=null && row["NumItem"].ToString()!="")
				{
					model.NumItem=int.Parse(row["NumItem"].ToString());
				}
				if(row["Lang"]!=null)
				{
					model.Lang=row["Lang"].ToString();
				}
				if(row["FromClassID"]!=null)
				{
					model.FromClassID=row["FromClassID"].ToString();
                }
                if (row["ItemTitle"] != null)
                {
                    model.ItemTitle = row["ItemTitle"].ToString();
                }
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,GUID,CreateTime,CreateUser,UpdateTime,UpdateUser,MarkStatus,MarkName,SeoTitle,SeoKeywords,SeoDescription,UserGUID,AdminGUID,MarkHtml,ClassID,ItemID,TypeID,AreaID,Number,Title,SubTitle,Style,Source,Author,Tags,Flags,Type,Images,ArrayImages,ArrayFiles,LinkUrl,Descriptions,Contents,TimeBegin,TimeEnd,HtmlTemplate,Orders,Hits,MarkHot,IsComment,NumComment,NumFavorite,NumGood,NumItem,Lang,FromClassID,ItemTitle ");
			strSql.Append(" FROM Dt_News ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
            strSql.Append(" ID,GUID,CreateTime,CreateUser,UpdateTime,UpdateUser,MarkStatus,MarkName,SeoTitle,SeoKeywords,SeoDescription,UserGUID,AdminGUID,MarkHtml,ClassID,ItemID,TypeID,AreaID,Number,Title,SubTitle,Style,Source,Author,Tags,Flags,Type,Images,ArrayImages,ArrayFiles,LinkUrl,Descriptions,Contents,TimeBegin,TimeEnd,HtmlTemplate,Orders,Hits,MarkHot,IsComment,NumComment,NumFavorite,NumGood,NumItem,Lang,FromClassID,ItemTitle ");
			strSql.Append(" FROM Dt_News ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM Dt_News ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from Dt_News T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion


        #region Remove 【删除资讯的内容及词条】
        public bool Remove(object Key)
        {
            var sql = "update DT_News set markstatus=-1 where {0};update DT_News_item set markstatus=-1 where FromGUID={1}";
            Guid GUID = Guid.Empty;
            SqlParameter IDParam;
            if (Guid.TryParse(Key.ToString(), out GUID))
            {
                IDParam = new SqlParameter()
                {
                    ParameterName = "@ID",
                    Value = GUID,
                    DbType = System.Data.DbType.Guid
                };
                sql = string.Format(sql, "guid=@ID", "@ID");
            }
            else
            {
                IDParam = new SqlParameter()
                {
                    ParameterName = "@ID",
                    Value = Key.DefaultIsNullOrEmpty(0),
                    DbType = System.Data.DbType.Int32
                };
                sql = string.Format(sql, "ID=@ID", "(select guid from DT_News where id=@ID)");
            }
            return DbHelperSQL.ExecuteNonQuery(sql, IDParam) > 0;
        }
        #endregion

        // ------------------- 前端查询 ------------------- 
        
        #region GetList 【前端查询获取资讯列表】

        public List<T_News> GetList(NewsParam Param, PagerInfo PageInfo)
        {
            #region 查询语句及参数设定
            var sql = new StringBuilder();
            sql.Append(@"Select ID
      ,[GUID]
      ,[CreateTime]
      ,[CreateUser]
      ,[UpdateTime]
      ,[UpdateUser]
      ,[MarkStatus]
      ,[MarkName]
      ,[SeoTitle]
      ,[SeoKeywords]
      ,[SeoDescription]
      ,[UserGUID]
      ,[AdminGUID]
      ,[MarkHtml]
      ,[ClassID]
      ,[ItemID]
      ,[TypeID]
      ,[AreaID]
      ,[Number]
      ,[Title]
      ,[SubTitle]
      ,[Style]
      ,[Source]
      ,[Author]
      ,[Tags]
      ,[Flags]
      ,[Type]
      ,[Images]
      ,[ArrayImages]
      ,[ArrayFiles]
      ,[LinkUrl]
      ,[Descriptions]
      ,[Contents]
      ,[TimeBegin]
      ,[TimeEnd]
      ,[HtmlTemplate]
      ,[Orders]
      ,[Hits]
      ,[MarkHot]
      ,[IsComment]
      ,[NumComment]
      ,[NumFavorite]
      ,[NumGood]
      ,[NumItem]
      ,[Lang]
      ,[FromClassID],ItemTitle,RefID ");
            sql.Append("FROM dt_news n (NOLOCK) ");
            sql.Append("Where n.MarkStatus=0 And  n.CreateTime<=GETDATE() ");
            var paramList = new List<SqlParameter>();
            if(Param.IsEn)
            {
                sql.Append(" And n.RefID>0 ");
            }
            else
            {
                sql.Append(" And n.RefID=0 ");
            }
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
            if (Param.ClassID > 0 && !String.IsNullOrWhiteSpace(Param.ClassIDStr))
            {
                //sql.AppendFormat("And n.ClassID IN ({0}) ", Param.ClassIDStr);
                sql.AppendFormat("And ClassID in ({0}) ", Param.ClassIDStr);
            }
            #endregion

            #region 标签搜索 Tags
            if (!string.IsNullOrEmpty(Param.Tags))
            {
                sql.Append("And (");
                foreach (var item in Param.Tags.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                {
                    sql.AppendFormat("n.Tags LIKE '%,{0},%' OR ", item.DefaultIsNullOrEmpty(0));
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
                    sql.AppendFormat("n.Flags LIKE '%{0}%' OR ", item.DefaultIsNullOrEmpty(0));
                }
                sql.Length = sql.Length - 3;
                sql.Append(") ");
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

            #region 栏目ID ItemID
            if (Param.ItemID > 0)
            {
                sql.Append("And n.ItemID=@ItemID ");
                paramList.Add(new SqlParameter()
                {
                    ParameterName = "@ItemID",
                    Value = Param.ItemID,
                    DbType = System.Data.DbType.Int32
                });
            }
            #endregion

            #region 栏目ID ItemID
            if (Param.MarkIndex > 0)
            {
                sql.Append("And n.markindex=@markindex ");
                paramList.Add(new SqlParameter()
                {
                    ParameterName = "@markindex",
                    Value = Param.MarkIndex,
                    DbType = System.Data.DbType.Int32
                });
            }
            #endregion

            //#region 推荐 RecommendID
            //if (Param.RecommendID > 0)
            //{
            //    sql.Append("And Exists(select 1 from btxcms_info_recommend r where r.InfoID=n.ID and r.RecommendID=@RecommendID and r.MarkName='NEWS') ");
            //    paramList.Add(new SqlParameter()
            //    {
            //        ParameterName = "@RecommendID",
            //        Value = Param.RecommendID,
            //        DbType = System.Data.DbType.Int32
            //    });
            //}
            //#endregion

            #endregion

            var OrderStr = string.Empty;
            if (Param.OrderEnum != null && Param.OrderEnum.Length > 0)
            {
                Param.OrderEnum.ToList().ForEach(t =>
                {
                    OrderStr = string.Format("{0},{1} {2}", OrderStr, t.ToString(), (int)t < 20 ? "DESC" : "ASC");
                });
                OrderStr = OrderStr.Substring(1);
            }
            else
            {
                OrderStr = "MarkHot Desc,Orders,CreateTime Desc";
            }
            //分页查询
            if (PageInfo != null)
            {
                int RowCount = 0;
                var ds = DbHelperSQL.ExecutePageModel(sql.ToString(), OrderStr, PageInfo.PageSize, PageInfo.CurrentPageIndex, out RowCount, paramList.ToArray());

                var List = new List<T_News>();
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

                var List = new List<T_News>();
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

        // ------------------- 后台管理操作 ------------------- 

        #region ManageListPage 【后台数据管理列表查询】
        public List<T_News> ManageListPage(QueryParams QueryParams, int PageSize, int PageIndex, out int rowCount)
        {
            var sql = new StringBuilder(@"SELECT id
      ,[GUID]
      ,[CreateTime]
      ,[CreateUser]
      ,[UpdateTime]
      ,[UpdateUser]
      ,[MarkStatus]
      ,[MarkName]
      ,[SeoTitle]
      ,[SeoKeywords]
      ,[SeoDescription]
      ,[UserGUID]
      ,[AdminGUID]
      ,[MarkHtml]
      ,[ClassID]
      ,[ItemID]
      ,[TypeID]
      ,[AreaID]
      ,[Number]
      ,[Title]
      ,[SubTitle]
      ,[Style]
      ,[Source]
      ,[Author]
      ,[Tags]
      ,[Flags]
      ,[Type]
      ,[Images]
      ,[ArrayImages]
      ,[ArrayFiles]
      ,[LinkUrl]
      ,[Descriptions]
      ,[Contents]
      ,[TimeBegin]
      ,[TimeEnd]
      ,[HtmlTemplate]
      ,[Orders]
      ,[Hits]
      ,[MarkHot]
      ,[IsComment]
      ,[NumComment]
      ,[NumFavorite]
      ,[NumGood]
      ,[NumItem]
      ,[Lang]
      ,[FromClassID],ItemTitle,RefID FROM DT_News b (NOLOCK) where 1=1 ");
            List<SqlParameter> paramList = null;
            if (QueryParams != null)
            {
                paramList = new List<SqlParameter>();
                if (QueryParams.IsEn.HasValue)
                {
                    if (QueryParams.IsEn.Value)
                    {
                        sql.Append(" And b.RefID>0 ");
                    }
                    else
                    {
                        sql.Append(" And b.RefID=0 ");
                    }
                }

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
                            sql.Append("and b.title like @SearchKey ");
                            paramList.Add(new SqlParameter()
                            {
                                ParameterName = "@SearchKey",
                                Value = "%" + QueryParams.SearchKey + "%",
                                DbType = System.Data.DbType.String
                            });
                            break;
                        //操作管理员
                        case "ADMINOPERATOR":
                            sql.Append("and (b.CreateUser=@SearchKey or b.UpdateUser=@SearchKey) ");
                            paramList.Add(new SqlParameter()
                            {
                                ParameterName = "@SearchKey",
                                Value = QueryParams.SearchKey.Trim(),
                                DbType = System.Data.DbType.String
                            });
                            break;
                        //全局多条件搜索
                        default:
                            sql.Append("and (b.title like @SearchKey or b.CreateUser like @SearchKey or b.UpdateUser like @SearchKey) ");
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

                #region 搜索类别 SearchClassID
                if (QueryParams.SearchClassID != 0)
                {
                    //sql.AppendFormat("And b.ClassID in ({0})", QueryParams.SearchClassIDStr);
                    sql.AppendFormat("And ClassID in ({0}) ", QueryParams.SearchClassIDStr);
                }
                #endregion

                #region 搜索归类 SearchMarkName
                if (!string.IsNullOrWhiteSpace(QueryParams.SearchMarkName) && QueryParams.SearchMarkName!="ALL")
                {
                    sql = sql.Append("and b.MarkName=@SearchMarkName ");
                    paramList.Add(new SqlParameter()
                    {
                        ParameterName = "@SearchMarkName",
                        Value = QueryParams.SearchMarkName,
                        DbType = System.Data.DbType.String
                    });
                }
                #endregion

                #region 搜索时间区间 SearchTimeBegin  SearchTimeEnd
                if (!string.IsNullOrWhiteSpace(QueryParams.SearchTimeBegin))
                {
                    sql = sql.Append("and b.CreateTime>=@SearchTimeBegin ");
                    paramList.Add(new SqlParameter()
                    {
                        ParameterName = "@SearchTimeBegin",
                        Value = DateTime.Parse(QueryParams.SearchTimeBegin),
                        DbType = System.Data.DbType.DateTime2
                    });
                }
                if (!string.IsNullOrWhiteSpace(QueryParams.SearchTimeEnd))
                {
                    sql = sql.Append("and b.CreateTime<=@SearchTimeEnd ");
                    paramList.Add(new SqlParameter()
                    {
                        ParameterName = "@SearchTimeEnd",
                        Value = DateTime.Parse(QueryParams.SearchTimeEnd).AddDays(86399F / 86400),
                        DbType = System.Data.DbType.DateTime2
                    });
                }
                #endregion

                #region 搜索状态 SearchMarkStatus
                switch (QueryParams.SearchMarkStatus)
                {
                    case 9999:
                        sql.Append("and b.MarkStatus!=-1 ");
                        break;
                    case -1:
                        sql.Append("and b.MarkStatus=-1 ");
                        break;
                    default:
                        sql.Append("and b.MarkStatus=@SearchMarkStatus ");
                        paramList.Add(new SqlParameter()
                        {
                            ParameterName = "@SearchMarkStatus",
                            Value = QueryParams.SearchMarkStatus,
                            DbType = System.Data.DbType.Int32
                        });
                        break;
                }
                #endregion

                #region 搜索地区 SearchAreaID
                if (QueryParams.SearchAreaID != 0)
                {
                    sql.Append("and b.AreaID=@SearchAreaID ");
                    paramList.Add(new SqlParameter()
                    {
                        ParameterName = "@SearchAreaID",
                        Value = QueryParams.SearchAreaID,
                        DbType = System.Data.DbType.Int32
                    });
                }
                #endregion

                #region 推荐 SearchRecommendID
                if (QueryParams.SearchRecommendID > 0)
                {
                    sql.Append("And Exists(select 1 from btxcms_info_recommend r where r.InfoID=b.ID and r.RecommendID=@RecommendID and r.MarkName='NEWS') ");
                    paramList.Add(new SqlParameter()
                    {
                        ParameterName = "@RecommendID",
                        Value = QueryParams.SearchRecommendID,
                        DbType = System.Data.DbType.Int32
                    });
                }
                #endregion

                #region 管理员过滤 SearchAdminGUID
                //if (QueryParams.SearchAdminGUID != Guid.Empty)
                //{
                //    sql.Append("and b.AdminGUID=@SearchAdminGUID ");
                //    paramList.Add(new SqlParameter()
                //    {
                //        ParameterName = "@SearchAdminGUID",
                //        Value = QueryParams.SearchAdminGUID,
                //        DbType = System.Data.DbType.Guid
                //    });
                //}
                #endregion
            }
            var ds = DbHelperSQL.ExecutePageModel(sql.ToString(), "MarkHot Desc,Orders,CreateTime Desc", PageSize, PageIndex, out rowCount, paramList.ToArray());

            var List = new List<T_News>();
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

        #region ManageArrayAction 【批量修改资讯{MarkType}相对应的字段信息】
        public bool ManageArrayAction(string IdArray, string MarkType, JieYiGuang.Model.T_AdminUser manageInfo)
        {
            var sql = new StringBuilder();
            sql.Append("update DT_News set adminguid=@guid,updateuser=@user,");
            switch (MarkType.ToUpper())
            {
                case "TOP1":
                    sql.Append("markhot=1 ");
                    break;
                case "TOP2":
                    sql.Append("markhot=2 ");
                    break;
                case "TOP3":
                    sql.Append("markhot=3 ");
                    break;
                case "TOP4":
                    sql.Append("markhot=4 ");
                    break;
                case "TOP5":
                    sql.Append("markhot=5 ");
                    break;
                case "TOP6":
                    sql.Append("markhot=6 ");
                    break;
                case "TOP0":
                    sql.Append("markhot=0 ");
                    break;
            }
            if (MarkType.ToUpper().StartsWith("FETCHSTATUS"))
            {
                sql.AppendFormat("markstatus={0} ", MarkType.ToUpper().Substring("FETCHSTATUS".Length));
            }
            if (MarkType.ToUpper().StartsWith("FETCHINDEX"))
            {
                sql.AppendFormat("markindex={0} ", MarkType.ToUpper().Substring("FETCHINDEX".Length));
            }
            sql.Append("where id in (" + IdArray + ") or RefId in (" + IdArray + ")");
            var guidParam = new SqlParameter()
            {
                ParameterName = "@guid",
                Value = Guid.NewGuid(),
                DbType = System.Data.DbType.Guid
            };
            var userParam = new SqlParameter()
            {
                ParameterName = "@user",
                Value = manageInfo.UserName,
                DbType = System.Data.DbType.String
            };
            return DbHelperSQL.ExecuteNonQuery(sql.ToString(), guidParam, userParam) > 0;
        }
        #endregion

        #region UpdateAttribute 【更新资讯属性字段】
        /// <summary>
        /// 更新资讯属性字段
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="UpdateEnum"></param>
        /// <returns></returns>
        public bool UpdateAttribute(object ID, NewsUpdateEnum UpdateEnum)
        {
            var sql = "Update dt_news Set {0}={0}+1 Where ";
            if (ID.DefaultIsNullOrEmpty(0) == 0)
            {
                sql = sql + "GUID='{1}'";
            }
            else
            {
                sql = sql + "ID={1}";
            }
            switch (UpdateEnum)
            {
                case NewsUpdateEnum.Hits:
                    sql = string.Format(sql, "Hits", ID);
                    break;
                case NewsUpdateEnum.Favorite:
                    sql = string.Format(sql, "NumFavorite", ID);
                    break;
                case NewsUpdateEnum.Comment:
                    sql = string.Format(sql, "NumComment", ID);
                    break;
                case NewsUpdateEnum.Good:
                    sql = string.Format(sql, "NumGood", ID);
                    break;
                case NewsUpdateEnum.ItemNum:
                    if (ID.DefaultIsNullOrEmpty(0) == 0)
                    {
                        sql = string.Format("Update dt_news Set NumItem=(select count(*) from dt_news_item where fromguid='{0}') where guid='{0}'", ID);
                    }
                    else
                    {
                        sql = string.Format("Update dt_news Set NumItem=(select count(*) from dt_news_item where fromguid=dt_news.guid) where id={0}", ID);
                    }
                    break;
                default:
                    return false;
            }
            return DbHelperSQL.ExecuteNonQuery(sql) > 0;
        }
        #endregion

        #region AddNewsClassArray 【资讯类别关联记录】
        public bool AddNewsClassArray(int NewsID, string ClassArray)
        {
            //删除资讯的所有类别
            var sql = string.Format("DELETE FROM Dt_News_Class WHERE NewsID={0}", NewsID);
            DbHelperSQL.ExecuteNonQuery(sql);
            //重新关联资讯类别
            sql = string.Format("INSERT INTO Dt_News_Class(NewsID,ClassID) SELECT {0},Item FROM Split('{1}',',')", NewsID, ClassArray);
            return DbHelperSQL.ExecuteNonQuery(sql) > 0;
        }
        #endregion
    }
}
