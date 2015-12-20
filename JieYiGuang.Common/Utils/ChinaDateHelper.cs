using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace JieYiGuang.Common.Utils
{
    public class ChinaDateHelper
    {
        ///<summary>
        /// 显示今天农历类
        ///</summary>
        public class CNDate
        {
            private DateTime m_Date; //今天的日期

            private readonly int cny; //农历的年月日

            private int cnm; //农历的年月日

            private readonly int cnd; //农历的年月日

            private readonly int icnm; //农历闰月

            ///<summary>
            /// 显示日期构造函数
            ///</summary>

            public CNDate()
            {
                m_Date = DateTime.Today;
                ChineseLunisolarCalendar cnCalendar = new ChineseLunisolarCalendar();
                cny = cnCalendar.GetSexagenaryYear(m_Date);
                cnm = cnCalendar.GetMonth(m_Date);
                cnd = cnCalendar.GetDayOfMonth(m_Date);
                icnm = cnCalendar.GetLeapMonth(cnCalendar.GetYear(m_Date));
            }
            ///<summary>
            /// 返回格式化的公历显示
            ///</summary>
            ///<returns>格式如:2008年05月14日</returns>

            public
            string GetDate()
            {
                int y = m_Date.Year;
                int m = m_Date.Month;
                int d = m_Date.Day;
                return String.Format("{0}年{1:00}月{2:00}日", y, m, d);
            }
            ///<summary>
            /// 返回格式化的星期显示
            ///</summary>
            ///<returns>格式如:星期日</returns>

            public string GetWeek()
            {
                string ws = "星期";
                int w = Convert.ToInt32(m_Date.DayOfWeek);
                ws = ws + "日一二三四五六".Substring(w, 1);
                return ws;
            }
            ///<summary>
            /// 返回格式化的农历显示
            ///</summary>
            ///<returns>格式如:戊子(鼠)年润四月廿三</returns>

            public
            string GetCNDate()
            {
                string txcns = "";
                const string szText1 = "癸甲乙丙丁戊己庚辛壬";
                const string szText2 = "亥子丑寅卯辰巳午未申酉戌";
                const string szText3 = "猪鼠牛虎免龙蛇马羊猴鸡狗";
                int tn = cny % 10; //天干

                int dn = cny % 12;  //地支
                txcns += szText1.Substring(tn, 1);
                txcns += szText2.Substring(dn, 1);
                txcns += "(" + szText3.Substring(dn, 1) + ")年";
                //格式化月份显示

                string[] cnMonth ={ "", "正月", "二月", "三月", "四月", "五月", "六月"
                , "七月", "八月", "九月", "十月", "十一月", "十二月", "十二月" };
                if (icnm > 0)
                {
                    for (int i = icnm + 1; i < 13; i++) //cnMonth = cnMonth[i - 1];
                        cnMonth[icnm] = "闰" + cnMonth[icnm];
                }
                txcns += cnMonth[cnm];
                string[] cnDay ={ "", "初一", "初二", "初三", "初四", "初五", "初六", "初七"
                , "初八", "初九", "初十", "十一", "十二", "十三", "十四", "十五", "十六"
                , "十七", "十八", "十九", "二十", "廿一", "廿二", "廿三", "廿四", "廿五"
                , "廿六", "廿七", "廿八", "廿九", "三十" };
                txcns += cnDay[cnd];
                return txcns;
            }

        }
    }
}
