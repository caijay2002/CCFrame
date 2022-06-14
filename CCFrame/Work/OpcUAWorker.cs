using CCFrame.Command.Data;
using CCFrame.Driver;
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
        //private string m_IpAddress { get; set; }

        private OpcUADriver OpcUADriver = new OpcUADriver();

        /// <summary>
        /// 监控的数据表
        /// </summary>
        private static Dictionary<string, List<IData>> MonitorMap = new Dictionary<string, List<IData>>();

        /// <summary>
        /// 初始化配置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="datas"></param>
        public void InitData(string key, List<IData> datas) => MonitorMap.Add(key, datas);

        /// <summary>
        /// 初始化配置
        /// </summary>
        /// <param name="configItems"></param>
        public void Initialize(List<DriverConfigItem> configItems)
        {
            OpcUADriver.Initialize(configItems);
        }

        public OperateResult ReadData(ref IData data)
        {
            return OperateResult.CreateSuccessResult();
        }

        public void Start()
        {

        }

        public void Stop()
        {

        }

        public OperateResult WritePlcData(IData data)
        {
            return OperateResult.CreateSuccessResult();
        }
    }
}
