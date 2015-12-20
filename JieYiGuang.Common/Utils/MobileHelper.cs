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
    /// �ֻ��ۺϲ�ѯ������
    /// </summary>
    public class MobileHelper : System.Web.UI.Page
    {
        /// <summary>
        /// �ֻ����ݿ�ʵ�ʵ�ַ
        /// </summary>
        private string MobileDataPath = ConfigHelper.GetConfigString("MobileDBpath");


        #region ��ѯ�ֻ������ַ

        /// <summary>
        /// ��ѯIP�����ַ
        /// </summary>
        /// <param name="objString">IP��ַ</param>
        /// <returns></returns>
        public string GetMobileDate(string mobile)
        {
            try
            {
                this.MobileDataPath = Server.MapPath(MobileDataPath);
                var phoneNum = mobile.Trim();//�����ֻ�����
                var tel = phoneNum.Substring(0, 3);//ȡ�ֻ�����ǰ����λ�ж�
                phoneNum = phoneNum.Substring(0, 7);//ȡ�ֻ�����ǰ����λ���в�ѯ
                string sql = "select * from '" + tel + "' where numberrange='" + phoneNum + "' limit 0,1";
                SQLiteConnection con = null;
                SQLiteCommand cmd = null;
                SQLiteDataReader dr = null;

                con = new SQLiteConnection("Data Source=" + this.MobileDataPath);
                cmd = new SQLiteCommand(sql, con);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)//������ڴ˼�¼
                {

                    return string.Format("<a title=\"{1}\">{0}</a>", dr.GetValue(1).ToString(), dr.GetValue(1).ToString() + "-" + dr.GetValue(2).ToString() + "-" + dr.GetValue(3).ToString() + "-" + dr.GetValue(4).ToString()); ;
                    //dr.GetValue(1).ToString();//������
                    //dr.GetValue(2).ToString();//������
                    //dr.GetValue(3).ToString();//�ʱ�
                    //dr.GetValue(4).ToString();//����
                }
                else
                {
                    return string.Format("<a href=\"http://api.showji.com/Locating/query.aspx?m={0}\"  target=\"_blank\">[���߲�ѯ]</a>", mobile);
                }

            }
            catch
            {
                return string.Format("<a href=\"http://api.showji.com/Locating/query.aspx?m={0}\"  target=\"_blank\">[���߲�ѯ]</a>", mobile);
            }
        }
        #endregion


    }
}
