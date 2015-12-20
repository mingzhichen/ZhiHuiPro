using JieYiGuang.Common.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JieYiGuang.Common.Utils
{
    public class LogInfoHelper
    {
        public static void LogInfo(string info)
        {
            try
            {
                FileInfo fileinfo = new FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/Logs/LogWeiXin/") + "Logfile.txt");
                using (FileStream fs = fileinfo.OpenWrite())
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine("=====================================");
                    sw.Write("添加日期为:" + DateTime.Now.ToString() + "\r\n");
                    sw.Write("日志内容为:" + info + "\r\n");
                    sw.WriteLine("=====================================");
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                var errorId = string.Empty;
                LogHelper.LogHandler.LogError(ex, out errorId);
            }
        }

        public static void TextLogInfo(string info)
        {
            try
            {
                FileInfo fileinfo = new FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/Logs/LogText/") + "Logfile.txt");
                using (FileStream fs = fileinfo.OpenWrite())
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine("=====================================");
                    sw.Write("添加日期为:" + DateTime.Now.ToString() + "\r\n");
                    sw.Write("日志内容为:" + info + "\r\n");
                    sw.WriteLine("=====================================");
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                var errorId = string.Empty;
                LogHelper.LogHandler.LogError(ex, out errorId);
            }
        }
    }
}
