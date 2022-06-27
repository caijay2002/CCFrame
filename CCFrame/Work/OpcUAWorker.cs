using CCFrame.Command.Data;
using CCFrame.Driver;
using CCFrame.Log;
using CCFrame.Version;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region << OpcUa工作者 >>
/*----------------------------------------------------------------
// 文件名称：OpcUAWorker
// 创 建 者：蔡程健
// 创建时间：22/6/10 16:49:38
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Work
{
    [LastModified("2022-06-11", "OPCUA驱动工作者")]
    public class OpcUAWorker : IWorker
    {
        private OpcUADriver OpcUADriver = new OpcUADriver();
        private bool IsStop { get; set; }
        public bool IsConnected { get; set; }

        /// <summary>
        /// 监控的数据表
        /// </summary>
        private Dictionary<string, List<IData>> MonitorMap = new Dictionary<string, List<IData>>();

        /// <summary>
        /// 初始化配置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="datas"></param>
        public void InitData(string key, List<IData> datas)// => MonitorMap.Add(key, datas);
        {
            foreach(var item in datas)
            {   //注册需要刷新的数据
                OpcUADriver.RegisterUpdate(item.Address);
            }

            MonitorMap.Add(key, datas);
        }

        /// <summary>
        /// 初始化配置
        /// </summary>
        /// <param name="configItems"></param>
        public void Initialize(List<DriverConfigItem> configItems)
        {
            OpcUADriver.Initialize(configItems);
        }

        public OperateResult<object> ReadData(IData data)
        {
            if (data is OPCData opcdata)
            {
                var result = OpcUADriver.ReadData(data.Address);
                if (result.IsSuccess)
                {
                    //var value = GetConvertData(plcData.DataType, result.Content);

                    Core.DataCacheSvr.UpdateCache("DataMap", data.Address, data.Value);
                }
                else
                {
                    LogSvr.Error($"ReadData ErrorCode: {result.ErrorCode} Message: {result.Message}");
                }
                //return result;
                return result;
            }
            else
            {
                return OperateResult.CreateFailedResult<object>(new OperateResult($"数据格式不正确 :MXPlcData {data.Address}"));
            }
        }



        public OperateResult WriteData(IData data)
        {
            if (data is OPCData opcdata)
            {
                //var result = OpcUADriver.WriteValue(opcdata.Address,opcdata.Value);
                OperateResult result = OperateResult.CreateSuccessResult();

                switch (opcdata.DataType)
                {
                    case DataType.Short:
                        Int16.TryParse(data.Value.ToString(), out Int16 writeVlaue);
                        result = OpcUADriver.WriteValue(data.Address, writeVlaue);
                        break;
                    case DataType.Int32:
                        result = OpcUADriver.WriteValue(data.Address, data.Value);
                        break;
                    default:
                        result = OpcUADriver.WriteValue(data.Address, data.Value.ToString());
                        break;
    
                }

                if (result.IsSuccess)
                {
                    Core.DataCacheSvr.UpdateCache("DataMap", opcdata.Address, opcdata.Value);
                }
                else
                {
                    Log.LogSvr.Error($"WriteData ErrorCode: {result.ErrorCode} Message: {result.Message}");
                }
                //return result;
                return OperateResult.CreateSuccessResult();
            }
            else
            {
                return OperateResult.CreateFailedResult(new OperateResult($"数据格式不正确 :MXPlcData {data.Address}"));
            }
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
                        var connectResult = await OpcUADriver.Connect();
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

                    OpcUADriver.UpdateData();

                    //UpdateDatas();

                    //UpdateAlarms();

                    await Task.Delay(1000);
                }
                catch (Exception ex)
                {
                    LogSvr.Error(string.Format("RunThread Error :{0} InnerException {1}", ex.Message, ex.InnerException));
                }
            }
        }

        private void UpdateDatas()
        {
            foreach (var item in MonitorMap["DataMap"])
            {
                if (item is OPCData data)
                {
                    var result = OpcUADriver.ReadData(item.Address);

                    var value = result.Content;

                    Core.DataCacheSvr.UpdateCache("DataMap", item.Address, value);
                }
            }
        }
    }
}
