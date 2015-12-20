using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace JieYiGuang.Common.Helper
{
    /// <summary>
    /// 计划任务 定时器
    /// </summary>
    public class TaskHelper
    {
        private static string content = "";
        /// <summary>
        /// 输出信息存储的地方.
        /// </summary>
        public static string Content
        {
            get { return TaskHelper.content; }
            set { TaskHelper.content += "<div>" + value + "</div>"; }
        }
        /// <summary>
        /// 定时器委托任务 调用的方法
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public static void SetContent(object source, ElapsedEventArgs e)
        {
            Content = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 应用池回收的时候调用的方法
        /// </summary>
        public static void SetContent()
        {
            Content = "END: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        
    }
}
