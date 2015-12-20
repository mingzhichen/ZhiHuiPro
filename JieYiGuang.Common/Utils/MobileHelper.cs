using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Xml;
using JieYiGuang.Common.Config;
using System.Data.SQLite;

namespace JieYiGuang.Common.Helper
{
    /// <summary>
    /// 手机综合查询公共类
    /// </summary>
    public class MobileHelper : System.Web.UI.Page
    {
        /// <summary>
        /// 手机数据库实际地址
        /// </summary>
        private string MobileDataPath = ConfigHelper.GetConfigString("MobileDBpath");


        #region 查询手机物理地址

        /// <summary>
        /// 查询IP物理地址
        /// </summary>
        /// <param name="objString">IP地址</param>
        /// <returns></returns>
        public string GetMobileDate(string mobile)
        {
            try
            {
                this.MobileDataPath = Server.MapPath(MobileDataPath);
                var phoneNum = mobile.Trim();//完整手机号码
                var tel = phoneNum.Substring(0, 3);//取手机号码前面三位判断
                phoneNum = phoneNum.Substring(0, 7);//取手机号码前面七位进行查询
                string sql = "select * from '" + tel + "' where numberrange='" + phoneNum + "' limit 0,1";
                SQLiteConnection con = null;
                SQLiteCommand cmd = null;
                SQLiteDataReader dr = null;

                con = new SQLiteConnection("Data Source=" + this.MobileDataPath);
                cmd = new SQLiteCommand(sql, con);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)//如果存在此记录
                {

                    return string.Format("<a title=\"{1}\">{0}</a>", dr.GetValue(1).ToString(), dr.GetValue(1).ToString() + "-" + dr.GetValue(2).ToString() + "-" + dr.GetValue(3).ToString() + "-" + dr.GetValue(4).ToString()); ;
                    //dr.GetValue(1).ToString();//归属地
                    //dr.GetValue(2).ToString();//卡类型
                    //dr.GetValue(3).ToString();//邮编
                    //dr.GetValue(4).ToString();//区号
                }
                else
                {
                    return string.Format("<a href=\"http://api.showji.com/Locating/query.aspx?m={0}\"  target=\"_blank\">[在线查询]</a>", mobile);
                }

            }
            catch
            {
                return string.Format("<a href=\"http://api.showji.com/Locating/query.aspx?m={0}\"  target=\"_blank\">[在线查询]</a>", mobile);
            }
        }
        #endregion


    }
}
