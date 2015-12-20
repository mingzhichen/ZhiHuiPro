using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Data;
namespace JieYiGuang.Common.Helper
{
    public class JsonHelper
    {
        /// <summary>
        /// 生成Json格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetJson<T>(T obj)
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                json.WriteObject(stream, obj);
                string szJson = Encoding.UTF8.GetString(stream.ToArray()); return szJson;
            }
        }
        /// <summary>
        /// 获取Json的Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="szJson"></param>
        /// <returns></returns>
        public static T ParseFromJson<T>(string szJson)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(szJson)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }

        ///<summary>
        /// 返回easyui/extjs中datagrid使用的json格式
        ///</summary>
        ///<param name="dt">datatable数据</param>
        ///<param name="count">总的条数</param>
        ///<returns></returns>
        public static string DataToJson(DataTable dt, int count)
        {
            StringBuilder sbjson = new StringBuilder();
            sbjson.Append("{");
            sbjson.Append("\"total\":" + count + ",\"rows\":[");
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i > 0)
                    {
                        sbjson.Append(",");
                        sbjson.Append("{");
                        foreach (DataColumn dc in dt.Columns)
                        {
                            if (dt.Columns.IndexOf(dc) > 0)
                            {
                                sbjson.Append(",");
                                sbjson.Append("\"" + dc.ColumnName + "\":\"" + dt.Rows[i][dc.ColumnName].ToString().Trim() + "\"");
                            }
                            else
                            {
                                sbjson.Append("\"" + dc.ColumnName + "\":\"" + dt.Rows[i][dc.ColumnName].ToString().Trim() + "\"");
                            }
                        }
                        sbjson.Append("}");
                    }
                    else
                    {
                        sbjson.Append("{");
                        foreach (DataColumn dc in dt.Columns)
                        {
                            if (dt.Columns.IndexOf(dc) > 0)
                            {
                                sbjson.Append(",");
                                sbjson.Append("\"" + dc.ColumnName + "\":\"" + dt.Rows[i][dc.ColumnName].ToString().Trim() + "\"");
                            }
                            else
                            {
                                sbjson.Append("\"" + dc.ColumnName + "\":\"" + dt.Rows[i][dc.ColumnName].ToString().Trim() + "\"");
                            }
                        }
                        sbjson.Append("}");
                    }
                }
            }
            sbjson.Append("]}");
            return sbjson.ToString();
        }
    }
}

//public class topMenu
//{
//    public string id { get; set; }
//    public string title { get; set; }
//    public string defaulturl { get; set; }
//}
//            topMenu t_menu = new topMenu()
//            {
//                id = "1",
//                title = "全局",
//                defaulturl = "123456"
//            };
//            List<topMenu> l_topmenu = new List<topMenu>();
//            for (int i = 0; i < 3; i++)
//            {
//                l_topmenu.Add(t_menu);
//            }
//            Response.Write(JsonHelper.GetJson<List<topMenu>>(l_topmenu));

