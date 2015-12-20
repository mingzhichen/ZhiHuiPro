using System;
using log4net;

namespace JieYiGuang.Common.Utils
{
    public class LogHelper
    {
        public class LogHandler
        {
            //创建静态单实例类
            //public static LogHandler Instance = new LogHandler();

            //记录日志
            //private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            //分登记记录日志
            public static string LogError(Exception ex, out String errorId)
            {
                errorId = DateTime.Now.ToString("yyyyMMddHHmmss");
                var error = string.Empty;
                var log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                if (ex == null)
                {
                    log.Error(ex.StackTrace);
                }
                else
                {
                    error = ex.Message;
                    if (ex.InnerException != null)
                    {
                        error = ex.InnerException.Message;
                        if (ex.InnerException.InnerException != null)
                        {
                            error = ex.InnerException.InnerException.Message;
                        }
                    }
                    log.Error("异常标识：" + errorId + "\r\n异常位置：" + ex.StackTrace + "模块\r\n错误消息：" + ex.Message + "\r\n异常错误：" + error);
                }
                return error;
            }

            public static void LogError(String errModule)
            {
                var log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                log.Error(errModule);
            }

            public static void LogWarn(String errModule, Exception ex)
            {
                var log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                log.Warn("-----在:" + errModule + "模块,错误消息:" + ex.Message);
            }

            public static void LogWarn(String errModule)
            {
                var log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                log.Warn(errModule);
            }

            public static void LogInfo(String errModule, Exception ex)
            {
                var log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                if (ex == null)
                {
                    log.Info(errModule);
                }
                else
                {
                    log.Info("----在:" + errModule + "模块,错误消息:" + ex.Message);
                }
            }

            public static void LogDebug(String errModule, Exception ex)
            {
                var log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                log.Debug("-----在:" + errModule + "模块,错误消息:" + ex.Message);
            }
        }
    }
}