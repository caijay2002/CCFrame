using CCFrame.Command.Data;
using CCFrame.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCFrame.CNC;


#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2022   保留所有权利。
 * 命名空间：CodeOptimization
 * 文件名：Class1
 * 创建者：蔡程健
 * 创建时间：2022/5/22 12:14:38
 * 版本：V1.0.0
 * 描述：
 *
 * ----------------------------------------------------------------
 * 修改人：
 * 时间：
 * 修改说明：
 *----------------------------------------------------------------*/
#endregion << 版 本 注 释 >>

namespace CCFrame.Work
{
    public class FanucMachineWorker : IWorker
    {
        FanucDevice m_device;

        public void InitData(string key, List<IData> datas)
        {
            //throw new NotImplementedException();

        }

        public void Initialize(List<DriverConfigItem> configItems)
        {
            m_device = new FanucDevice();
            m_device.Initialize("TestDevice", "10.90.254.153");
        }

        public OperateResult<object> ReadData(IData data)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            var con = m_device.Connect();

            var read = m_device.ReadData();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public OperateResult WriteData(IData data)
        {
            throw new NotImplementedException();
        }
    }
}
