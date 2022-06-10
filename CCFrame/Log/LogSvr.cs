using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


#region << 日志统一处理 >>
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
        private static bool PassMessage(string message)
        {
            try
            {
                //if (currentLogs.Count < 1) return "";
                //数据过滤 //如果最近10条数据中有包含当前数据则不再添加数据了
                if (currentLogs.Contains(message)) return true;

                currentLogs.Enqueue(message);

                if (currentLogs.Count > FilterLogCount)//大于10条移除最旧的数据
                {
                    currentLogs.TryDequeue(out string cLog);
                }
            }
            catch (Exception ex)
            {

                return false;
            }


            return false;
        }

        public static void Error(string message)
        {
            try
            {
                logger.Error(message);
                if (PassMessage(message)) return;//需要数据过滤
                CCLog log = new CCLog();
                log.Type = LogType.Error;
                log.Message = DateTime.Now.ToString("HH:mm:ss fff") + "  " + message;
                cCLogs.Enqueue(log);                
            }
            catch (Exception ex)
            {

            }

        }

        public static void Debug(string message)
        {
            logger.Debug(message);
            if (PassMessage(message)) return;//需要数据过滤
            CCLog log = new CCLog();
            log.Type = LogType.Debug;
            log.Message = DateTime.Now.ToString("HH:mm:ss fff") + "  " + message;
            cCLogs.Enqueue(log);            
        }

        public static void Info(string message)
        {
            logger.Info(message);
            if (PassMessage(message)) return;//需要数据过滤

            CCLog log = new CCLog();
            log.Type = LogType.Info;
            log.Message = DateTime.Now.ToString("HH:mm:ss fff") + "  " + message;
            cCLogs.Enqueue(log);            
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

        ///// <summary>
        ///// 显示行号，路径，方法名
        ///// </summary>
        ///// <param name="line">行号</param>
        ///// <param name="path">路径</param>
        ///// <param name="name">方法名</param>
        //public static void Log([CallerLineNumber] int line = -1,
        //[CallerFilePath] string path = null,
        //[CallerMemberName] string name = null)
        //{
        //    Console.WriteLine((line < 0) ? "No line" : "Line " + line);
        //    Console.WriteLine((path == null) ? "No file path" : path);
        //    Console.WriteLine((name == null) ? "No member name" : name);
        //    Console.WriteLine();
        //}

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
