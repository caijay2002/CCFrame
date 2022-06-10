using CCFrame.Command.Data;
using CCFrame.Driver;
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
    public class OpcUAWorker : IWorker
    {
        public void InitData(string key, List<IData> datas)
        {

        }

        public void Initialize(List<DriverConfigItem> configItems)
        {

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
