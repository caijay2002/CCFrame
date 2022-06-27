using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CCFrame.Alarm
{
    ///<summary>
    /// 版 本：v1.0.0
    /// 创建人：蔡程健
    /// 日 期：20/6/19 13:29:17
    /// 描 述：
    ///</summary>
    public class CsvToAlarmHelper
    {
        /// <summary>
        /// 将CSV文件的数据读取到DataTable中
        /// </summary>
        /// <param name="fileName">CSV文件路径</param>
        /// <returns>返回读取了CSV数据的DataTable</returns>
        public static Dictionary<int, AlarmData> ReadAlarms(string filePath)
        {
            Dictionary<int, AlarmData> alarms = new Dictionary<int, AlarmData>();
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs, Encoding.Default);
                //记录每次读取的一行记录
                string strLine = "";
                //记录每行记录中的各字段内容
                string[] aryLine = null;

                //标示是否是读取的第一行
                bool IsFirst = true;
                //逐行读取CSV中的数据
                while ((strLine = sr.ReadLine()) != null)
                {
                    if (IsFirst)
                    {
                        IsFirst = false;
                        continue;
                    }
                    aryLine = strLine.Split(',');
                    if (aryLine.Length >= 4)
                    {
                        AlarmData alarm = new AlarmData();

                        int code = 0;
                        int.TryParse(aryLine[0], out code);
                        string address = aryLine[1];
                        int level = 0;
                        int.TryParse(aryLine[2], out level);
                        string message = aryLine[3];


                        alarm.Code = code;
                        alarm.Address = address;
                        alarm.Level = level;
                        alarm.Detail = message;

                        alarms.Add(code, alarm);
                    }
                }

                sr.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                Log.LogSvr.Error($"ReadAlarms Error {ex.Message}");
                //throw;
            }
            
            return alarms;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Dictionary<string, int> ReadItems(string filePath)
        {
            Dictionary<string, int> items = new Dictionary<string, int>();
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);

            StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
            //StreamReader sr = new StreamReader(fs, encoding);
            //string fileContent = sr.ReadToEnd();
            //encoding = sr.CurrentEncoding;
            //记录每次读取的一行记录
            string strLine = "";
            //记录每行记录中的各字段内容
            string[] aryLine = null;

            //标示是否是读取的第一行
            //bool IsFirst = true;
            //逐行读取CSV中的数据
            while ((strLine = sr.ReadLine()) != null)
            {
                aryLine = strLine.Split(',');
                if (aryLine.Length == 3)
                {
                    string key = aryLine[0] + "#" + aryLine[1];
                    int value = 0;
                    int.TryParse(aryLine[2], out value);

                    if (!items.ContainsKey(key))//不包含
                    {
                        items.Add(key, value);
                    }
                    else
                    {
                        Console.WriteLine("key:{0}  value:{1}", key, value);
                    }

                }
            }

            sr.Close();
            fs.Close();
            return items;
        }

    }
}
