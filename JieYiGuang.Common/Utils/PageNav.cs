using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.IO.Compression;
using System.Net.Cache;
using System.Text.RegularExpressions;

namespace JieYiGuang.Common.Helper
{
    public class PageNav
    {
        private static string _unitString = "条";
        /// <summary>
        /// 设置分页单页个数
        /// </summary>
        /// <param name="str"></param>
        public static void SetPageUnit(string str)
        {
            _unitString = str;
        }
        /// <summary>
        /// 验证当前页参数格式是否正确
        /// </summary>
        /// <param name="pageStr"></param>
        /// <returns></returns>
        public static string checkPageIndex(string pageStr)
        {
            if (string.IsNullOrEmpty(pageStr))
            {
                pageStr = "1";
            }
            Regex reg = new Regex("^[0-9]+$");
            Match ma = reg.Match(pageStr);
            if (!ma.Success)
            {
                pageStr = "1";
            }
            return pageStr;
        }
        /// <param name="total">总记录数</param>
        /// <param name="per">每页记录数</param>
        /// <param name="page">当前页数</param>      
        /// <param name="page">url参数</param>      
        /// 返回一个带HTML代码的分页样式（字符串）
        public static string Pagination(int total, int per, ref string CurPage,string Param)
        {
            if (string.IsNullOrEmpty(Param))
                Param = "";
            else if ("&".Equals(Param.Substring(0, 1)))
                Param = "&" + Param;

            int page = Convert.ToInt32(CurPage);
            int allpage = 0;
            int next = 0;//上一页
            int pre = 0;//下一页
            int startcount = 0;
            int endcount = 0;
            string pagestr = "";
            pagestr += "<div>";
            string pageStyle = "cursor:pointer;color:#333333;text-decoration:underline;font-size:12px;padding-left:10px;";

            if (page < 1) { page = 1; }
            //计算总页数
            if (per != 0)
            {
                allpage = (total / per);
                allpage = ((total % per) != 0 ? allpage + 1 : allpage);
                allpage = (allpage == 0 ? 1 : allpage);
            }
            if (page > allpage)
                page = allpage;
            CurPage = page.ToString();
            next = page + 1;
            pre = page - 1;
            startcount = (page + 4) > allpage ? allpage - 7 : page - 3;//中间页起始序号
            //中间页终止序号
            endcount = page < 4 ? 8 : page + 4;
            if (startcount < 1) { startcount = 1; } //为了避免输出的时候产生负数，设置如果小于1就从序号1开始
            if (allpage < endcount) { endcount = allpage; }//页码+5的可能性就会产生最终输出序号大于总页码，那么就要将其控制在页码数之内

            pagestr += page > 1 ? "<a style='" + pageStyle + "' href='?pageindex=1" + Param + "' >第一页</a><a style='" + pageStyle + "' href='?pageindex=" + (page - 1) + Param + "' >上一页</a>" : "第一页 上一页";
            //中间页处理，这个增加时间复杂度，减小空间复杂度
            for (int i = startcount; i <= endcount; i++)
            {
                pagestr += page == i ? "<font color=\'#ff0000\'> " + i + " </font> " : "<a href='?pageindex=" + i + Param + "' >" + i + "</a>";
            }
            pagestr += page != allpage ? "<a style='" + pageStyle + "'  href='?pageindex=" + (page + 1) + Param +"' >下一页</a><a style='" + pageStyle + "' href='?pageindex=" + allpage + Param + "'>末页</a>" : " 下一页 末页";


            pagestr += "  <span>第</span><input id='curPg' name='goPageTo' onkeyup='isNum(this);' value='" + page + "'  maxlength='10' maxlength='10' type='text' style='width:25px;border:1px solid #9C9C9C;height:16px;'/> 页<a style='color:#343434;text-decoration:underline;cursor:pointer;font-size:12px;' onclick=\"javascript:window.location.href='?pageindex='+document.getElementById('curPg').value + '" + Param + "';\">GO</a>";
            pagestr += "共" + total + _unitString + " 共" + allpage + "页</div>";
            return pagestr;
        }


        /// <param name="total">总记录数</param>
        /// <param name="per">每页记录数</param>
        /// <param name="page">当前页数</param>      
        /// <param name="page">url参数</param>      
        /// 返回一个带HTML代码的分页样式（字符串）
        public static string PaginationFeed(int total, int per, ref string CurPage, string Param)
        {
            if (string.IsNullOrEmpty(Param))
                Param = "";
            else if ("&".Equals(Param.Substring(0, 1)))
                Param = "&" + Param;

            int page = Convert.ToInt32(CurPage);
            int allpage = 0;
            int next = 0;//上一页
            int pre = 0;//下一页
            int startcount = 0;
            int endcount = 0;
            string pagestr = "";
            pagestr += "<div>";
            string pageStyle = "cursor:pointer;color:#333333;text-decoration:underline;font-size:12px;padding-left:10px;";

            if (page < 1) { page = 1; }
            //计算总页数
            if (per != 0)
            {
                allpage = (total / per);
                allpage = ((total % per) != 0 ? allpage + 1 : allpage);
                allpage = (allpage == 0 ? 1 : allpage);
            }
            if (page > allpage)
                page = allpage;
            CurPage = page.ToString();
            next = page + 1;
            pre = page - 1;
            startcount = (page + 4) > allpage ? allpage - 7 : page - 3;//中间页起始序号
            //中间页终止序号
            endcount = page < 4 ? 8 : page + 4;
            if (startcount < 1) { startcount = 1; } //为了避免输出的时候产生负数，设置如果小于1就从序号1开始
            if (allpage < endcount) { endcount = allpage; }//页码+5的可能性就会产生最终输出序号大于总页码，那么就要将其控制在页码数之内

            pagestr += page > 1 ? "<a style='" + pageStyle + "' href='javascript:;' onclick='getFeed(1)' >第一页</a><a style='" + pageStyle + "' href='javascript:;' onclick='getFeed(" + (page - 1)  + ")' >上一页</a>" : "第一页 上一页";
            //中间页处理，这个增加时间复杂度，减小空间复杂度
            for (int i = startcount; i <= endcount; i++)
            {
                pagestr += page == i ? "<font color=\'#ff0000\'> " + i + " </font> " : "<a href='?pageindex=" + i + Param + "' >" + i + "</a>";
            }
            pagestr += page != allpage ? "<a style='" + pageStyle + "' href='javascript:;' onclick='getFeed(" + (page + 1) + ")' >下一页</a><a style='" + pageStyle + "' href='javascript:;' onclick='getFeed(" + allpage + ")'>末页</a>" : " 下一页 末页";


            pagestr += "  <span>第</span><input id='curPg' name='goPageTo' onkeyup='isNum(this);' value='" + page + "'  maxlength='10' maxlength='10' type='text' style='width:25px;border:1px solid #9C9C9C;height:16px;'/> 页<a style='color:#343434;text-decoration:underline;cursor:pointer;font-size:12px;' onclick=\"getFeed(document.getElementById('curPg').value)\">GO</a>";
            pagestr += "共" + total + _unitString + " 共" + allpage + "页</div>";
            return pagestr;
        }
    }
}
