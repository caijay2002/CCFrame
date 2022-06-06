using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：AlarmSvr
// 创 建 者：蔡程健
// 创建时间：22/4/27 14:48:29
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Alarm
{
    public class AlarmSvr
    {
        /// <summary>
        /// 报警数据
        /// </summary>
        private static Dictionary<int, AlarmData> AlarmMap = new Dictionary<int, AlarmData>();
        /// <summary>
        /// 当前的报警
        /// </summary>
        public static Dictionary<int, AlarmData> CurrentAlarm = new Dictionary<int, AlarmData>();
        /// <summary>
        /// 报警列表是否改变
        /// </summary>
        public static bool IsAlarmChange = true;

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Initialize(string filePath)
        {
            AlarmMap = CsvToAlarmHelper.ReadAlarms(filePath);
        }

        /// <summary>
        /// 添加报警
        /// </summary>
        /// <param name="address"></param>
        public static void AddAlarm(int code)
        {
            var alarm = AlarmMap[code];
            if (alarm != null && !CurrentAlarm.Keys.Contains(code))
            {
                CurrentAlarm.Add(code, alarm);
                AlarmMap[code].Status = "1";

                IsAlarmChange = true;
            }
        }

        /// <summary>
        /// 移除报警
        /// </summary>
        /// <param name="address"></param>
        public static void RemoveAlarm(int code)
        {
            if (CurrentAlarm.Keys.Contains(code))
            {
                CurrentAlarm.Remove(code);
                AlarmMap[code].Status = "0";

                IsAlarmChange = true;
            }
        }

        public static AlarmData GetAlarm(int code)
        {
            if (AlarmMap.Keys.Contains(code))
            {
                return AlarmMap[code];
            }
            else return null;
        }

        public static List<AlarmData> GetAlarmDatas()
        {
            return AlarmMap.Values.ToList();
        }

        /// <summary>
        /// 保存报警
        /// </summary>
        /// <param name="address"></param>
        public static void SaveAlarmToCsv(AlarmData alarm)
        {

        }
    }
}
