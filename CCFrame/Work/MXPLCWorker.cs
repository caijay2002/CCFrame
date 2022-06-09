using CCFrame.Driver;
using CCFrame.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCFrame.Command.Data;
using CCFrame.DataConvert;

#region << 文 件 说 明 >>
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

        private string m_IpAddress { get; set; }
        private int m_stationNumber { get; set; }

        private bool IsStop { get; set; }

        public void Initialize(List<DriverConfigItem> configItems)
        {
            foreach(var item in configItems)
            {
                switch (item.Key)
                {
                    case "IpAddress":
                        m_IpAddress = item.Value;
                        break;
                    case "StationNumber":
                        m_stationNumber = Convert.ToInt32(item.Value);
                        break;
                }
            }

            MXDriver.Initialize(m_IpAddress, m_stationNumber);
        }

        public void InitData(string key, List<IData> datas)
        {
            MonitorMap.Add(key, datas);
        }

        public void Start()
        {
            Task t_Run = Task.Run(() =>
            {
                Run();
            });
        }

        public void Stop()
        {
            IsStop = true;
        }

        public OperateResult ReadData(ref IData data)
        {
            if(data is MXPlcData plcData)
            {
                //bool result = false;
                //string value;
                //result = MXDriver.ReadData(data.Address, plcData.Length, data.DataType, out value);
                //data.Value = value;
                return OperateResult.CreateSuccessResult();
            }
            else
            {
                return OperateResult.CreateFailedResult(new OperateResult(""));
            }
        }

        public OperateResult WritePlcData(IData data)
        {
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
                        if (!MXDriver.Connect().IsSuccess)
                        {
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

                    switch (data.DataType)
                    {
                        case DataType.Ascii:
                            item.Value = ShortHelper.ToAscii(result.Content);
                            break;
                        case DataType.Bit:
                            item.Value = ShortHelper.ToBool(result.Content);
                            break;
                        case DataType.Int32:
                            item.Value = ShortHelper.ToInt(result.Content);
                            break;
                        case DataType.Short:
                            item.Value = result.Content[0];
                            break;
                        case DataType.DateTime:

                            break;
                        default:

                            break;
                    }
                    Core.DataCacheSvr.UpdateCache("DataMap", item);
                }
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

                    Core.DataCacheSvr.UpdateCache("AlarmMap", item);
                }
            }
        }
    }
}
