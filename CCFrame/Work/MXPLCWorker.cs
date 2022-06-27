using CCFrame.Driver;
using CCFrame.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCFrame.Command.Data;
using CCFrame.DataConvert;

#region << PLC工作者 >>
/*----------------------------------------------------------------
// 文件名称：MXPLCWorker
// 创 建 者：蔡程健
// 创建时间：22/5/28 20:02:03
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Work
{
    public class MXPLCWorker:IWorker
    {
        public bool IsConnected { get; set; }

        private MXDriver MXDriver = new MXDriver();

        private static Dictionary<string, List<IData>> MonitorMap = new Dictionary<string, List<IData>>();

        private bool IsStop { get; set; }

        public void Initialize(List<DriverConfigItem> configItems)
        {
            MXDriver.Initialize(configItems);
        }

        public void InitData(string key, List<IData> datas) => MonitorMap.Add(key, datas);

        public void Start()
        {
            Task t_Run = Task.Run(() =>
            {
                Run();
            });
        }

        public void Stop() => IsStop = true;

        public OperateResult<object> ReadData(IData data)
        {
            if(data is MXPlcData plcData)
            {
                OperateResult<object> readResult = OperateResult.CreateSuccessResult<object>(null);
                var result = MXDriver.ReadData(plcData);
                if (result.IsSuccess)
                {
                    var value = GetConvertData(plcData.DataType, result.Content);

                    readResult.Content = value;

                    Core.DataCacheSvr.UpdateCache("DataMap", data.Address, value);
                }
                else
                {
                    readResult.Message = $"ReadData ErrorCode: {result.ErrorCode} Message: {result.Message}";
                    LogSvr.Error($"ReadData ErrorCode: {result.ErrorCode} Message: {result.Message}");
                }
                return readResult;
            }
            else
            {
                return OperateResult.CreateFailedResult<object>(new OperateResult($"数据格式不正确 :MXPlcData {data.Address}"));
            }
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public OperateResult WriteData(IData data) 
        { 
            if(data is MXPlcData plcData)
            {
                short[] buffer = ShortHelper.ToShorts(plcData);
                var result = MXDriver.WritePlcData(plcData.Address, buffer);
                if (!result.IsSuccess) LogSvr.Error($"ReadData ErrorCode: {result.ErrorCode} Message: {result.Message}");
                return result;
            }

            return OperateResult.CreateSuccessResult(); 
        }

        private async void Run()
        {
            while (true)
            {
                try
                {
                    if (IsStop == true)//是否退出
                    {
                        return;
                    }

                    if (IsConnected == false)//是否连接
                    {
                        var connectResult = MXDriver.Connect();
                        if (!connectResult.IsSuccess)
                        {
                            LogSvr.Error($"连接失败 {connectResult.Message}");
                            await Task.Delay(1000);
                            continue;
                        }
                        else
                        {                            
                            IsConnected = true;
                        }
                    }

                    UpdateDatas();

                    UpdateAlarms();

                    await Task.Delay(1000);
                }
                catch (Exception ex)
                {
                    LogSvr.Error(string.Format("RunThread Error :{0}", ex.Message, ex.InnerException));
                }
            }
        }

        private void UpdateDatas()
        {
            foreach (var item in MonitorMap["DataMap"])
            {
                if(item is MXPlcData data)
                {
                    short[] buffer = new short[data.Length];

                    var result = MXDriver.ReadData(data);

                    var value = GetConvertData(data.DataType, result.Content);

                    Core.DataCacheSvr.UpdateCache("DataMap", item.Address, value);
                }
            }
        }

        /// <summary>
        /// 获取转化后的数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private object GetConvertData(DataType type, short[] buffer)
        {
            switch (type)
            {
                case DataType.Ascii:
                    return ShortHelper.ToAscii(buffer);
                case DataType.Bit:
                    return ShortHelper.ToBool(buffer);
                case DataType.Int32:
                    return ShortHelper.ToInt(buffer);
                case DataType.Short:
                    return buffer[0];
                case DataType.DateTime:
                    return null;
                default:
                    return null;
            }
        }

        private void UpdateAlarms()
        {
            //AlarmMap
            foreach (var item in MonitorMap["AlarmMap"])
            {
                if (item is MXPlcData data)
                {
                    short[] buffer = new short[data.Length];

                    var result = MXDriver.ReadData(data);

                    item.Value = ShortHelper.ToInt(buffer);

                    //Core.DataCacheSvr.UpdateCache("AlarmMap", item);
                }
            }
        }
    }
}
