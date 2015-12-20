using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JieYiGuang.Common.Utils
{
    public class AssistanceTool
    {
        #region 订单状态代码
        /// <summary>
        /// 订单状态代码
        /// </summary>
        /// <param name="val">状态代码</param>
        /// <returns></returns>
        public String GetOrdrStatus(object val)
        {
            //5买家提出换货，-1买家有效期内不支付，交易取消，-2买家退货，等待交易取消，-3卖家确定取消订单并退款，-4买家确认收到退款，交易完全取消,-5,-6
            String status = null;
            switch ((int)val)
            {
                case 0: status = "未支付"; break;
                case 1: status = "已支付，未发货"; break;
                case 2: status = "已确认"; break;
                case 3: status = "已发货"; break;
                case 4: status = "交易成功"; break;
                case 5: status = "买家申请换货"; break;
                case 8: status = "待收款"; break;
                case 9: status = "待处理"; break;
                case 10: status = "已处理"; break;
                case -2: status = "买家申请退货"; break;
                case -5: status = "拒绝退货"; break;
                case -6: status = "同意退货"; break;
                case -7: status = "已发回退货"; break;
                case -3: status = "已退回货款"; break;
                case -4: status = "交易已取消"; break;
                default: break;
            }
            return status;
        }
        #endregion

        #region [数字转换为相对意义的字符，专用于数据库中判断字段的显示]
        /// <summary>
        /// 数字转换为相对意义的字符，专用于数据库中判断字段的显示，如：IsTop
        /// </summary>
        /// <param name="convertNum">需要转换的数字，一般为1和0</param>
        /// <returns></returns>
        public String int2Char(int convertNum)
        {
            if (convertNum == 1) return "是";
            else return "否";
        }
        public String int2Char(int convertNum, int Param)
        {
            if (convertNum == Param) return "是";
            else return "否";
        }
        public String int2Char(String convertNum)
        {
            if (convertNum == "1") return "是";
            else return "否";
        }
        #endregion

        #region [把CheckedBox的值转换成数字]
        /// <summary>
        /// 把CheckedBox的值转换成数字
        /// </summary>
        /// <param name="IsChecked">CheckedBox控件是否已经选择</param>
        public int CheckedToInt(bool IsChecked)
        {
            int resault = 0;
            if (IsChecked) resault = 1;
            return resault;
        }
        public int CheckedToInt(bool IsChecked, int Param)
        {
            int resault = 0;
            if (IsChecked) resault = Param;
            return resault;
        }
        public int CheckedToInt(String chkValue)
        {
            int resault = 0;
            if (chkValue == "True" || chkValue == "on") resault = 1;
            return resault;
        }
        #endregion

        #region [给指定的URL添加HTTP://标志]
        /// <summary>
        /// 给指定的URL添加HTTP://标志
        /// </summary>
        /// <returns></returns>
        public String GetUrl(String str)
        {
            str = str.ToLower();
            if (str.IndexOf("http://") == -1) str = "http://" + str;
            return str;
        }
        #endregion
    }
}
