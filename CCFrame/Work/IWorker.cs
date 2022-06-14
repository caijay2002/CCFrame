using CCFrame.Command.Data;
using CCFrame.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCFrame.Work
{
    public interface IWorker
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="configItems"></param>
        void Initialize(List<DriverConfigItem> configItems);
        /// <summary>
        /// 启动
        /// </summary>
        void Start();
        /// <summary>
        /// 停止
        /// </summary>
        void Stop();
        /// <summary>
        /// 读取寄存器数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        OperateResult ReadData(IData data);
        /// <summary>
        /// 写入寄存器数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        OperateResult WriteData(IData data);
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="datas"></param>
        void InitData(string key, List<IData> datas);
    }
}
