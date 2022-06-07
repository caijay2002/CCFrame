using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：LogSvr
// 创 建 者：蔡程健
// 创建时间：22/5/26 18:36:38
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Log
{
    public static class LogSvr
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private static ConcurrentQueue<CCLog> cCLogs = new ConcurrentQueue<CCLog>();

        private static ConcurrentQueue<string> currentLogs = new ConcurrentQueue<string>();

        /// <summary>
        /// 过滤的重复的几条日志数量
        /// </summary>
        public static int FilterLogCount { get; set; } = 10;

        /// <summary>
        /// 设置需要过滤的日志数量
        /// </summary>
        /// <param name="count"></param>
        public static void SetFilterCount(int count)
        {
            FilterLogCount = count;
        }

        /// <summary>
        /// 过滤最近10条日志是否重复
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string PassMessage(string message)
        {
            //if (currentLogs.Count < 1) return "";
            try
            {
                //数据过滤
                if (currentLogs.Contains(message))//如果最近10条数据中有包含当前数据则不再添加数据了
                {
                    return null;
                }

                currentLogs.Enqueue(message);

                CCLog cLog;
                if (currentLogs.Count > FilterLogCount)//大于10条移除最旧的数据
                {
                    cCLogs.TryDequeue(out cLog);
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }


            return message;
        }

        public static void Error(string message)
        {
            try
            {
                message = PassMessage(message);
                if (message == null) return;

                CCLog log = new CCLog();
                log.Type = LogType.Error;
                log.Message = DateTime.Now.ToString("HH:mm:ss fff") + "  " + message;
                cCLogs.Enqueue(log);
                logger.Error(message);
            }
            catch (Exception ex)
            {

            }

        }

        public static void Debug(string message)
        {
            message = PassMessage(message);
            if (message == null) return;

            CCLog log = new CCLog();
            log.Type = LogType.Debug;
            log.Message = DateTime.Now.ToString("HH:mm:ss fff") + "  " + message;
            cCLogs.Enqueue(log);
            logger.Debug(message);
        }

        public static void Info(string message)
        {
            message = PassMessage(message);
            if (message == null) return;

            CCLog log = new CCLog();
            log.Type = LogType.Info;
            log.Message = DateTime.Now.ToString("HH:mm:ss fff") + "  " + message;
            cCLogs.Enqueue(log);
            logger.Info(message);
        }

        public static string GetMessage()
        {
            CCLog log;
            if (cCLogs.Count > 0 && cCLogs.TryDequeue(out log))
            {
                return log.Message;
            }
            return null;
        }

        /// <summary>
        /// 保存操作日志
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool WriteLogData(string filePath, string msg)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    FileInfo myfile = new FileInfo(filePath);
                    FileStream fs = myfile.Create();
                    fs.Close();
                }
                StreamWriter sw = File.AppendText(filePath);
                sw.WriteLine(msg);
                sw.Flush();
                sw.Close();

                return true;
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }

            return false;
        }

        public static void WriteLine(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

    }

    public class CCLog
    {
        public LogType Type { get; set; }

        public string Message { get; set; }
    }

    public enum LogType
    {
        Error,
        Info,
        Debug,
    }
}
