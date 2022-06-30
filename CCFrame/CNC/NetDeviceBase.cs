using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：NetDeviceBase
// 创 建 者：蔡程健
// 创建时间：22/6/30 11:56:19
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.CNC
{
    /// <summary>
    /// 网络设备基类
    /// </summary>
    public abstract class NetDeviceBase
    {
        public string DeviceName;
        /// <summary>
        /// 默认ip地址
        /// </summary>
        protected string m_strIpAddress = "192.168.0.190";
        /// <summary>
        /// 设备通信端口
        /// </summary>
        protected int m_iTcpPort = 8193;
        /// <summary>
        /// 连接超时(S)
        /// </summary>
        protected int m_ConnectTimeout = 10;
        /// <summary>
        /// 数据缓存
        /// </summary>
        public Core.DataCache DeviceCache = new Core.DataCache();

        public virtual void Initialize()
        {
        }

        public virtual void Initialize(string deviceName)
        {
            DeviceName = deviceName;
        }

        public virtual void Initialize(string deviceName, string ip)
        {
            DeviceName = deviceName;
            m_strIpAddress = ip;
        }

        public virtual void Initialize(string deviceName, string ip, int port)
        {
            DeviceName = deviceName;
            m_strIpAddress = ip;
            m_iTcpPort = port;
        }

        public virtual void Initialize(string deviceName, string ip, int port, int timeOut)
        {
            DeviceName = deviceName;
            m_strIpAddress = ip;
            m_iTcpPort = port;
            m_ConnectTimeout = timeOut;
        }

        public abstract OperateResult Connect();

        public abstract OperateResult Disconnect();

        public abstract OperateResult ReadData();

        public abstract OperateResult WriteData(string key, string[] args, object value);

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public virtual object GetValue(string key)
        {
            return DeviceCache.GetValue(key);
        }

        /// <summary>
        /// 增加或更新数据项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        protected void UpdateValue(string key, object value)
        {
            DeviceCache.UpdateValue(key, value);
        }

        //protected void SaveErrorLog(OperateResult result)
        //{
        //    Log.LogSvr.Error(DeviceName + "ErrorCode:" + result.ErrorCode + "Message:" + result.Message);
        //}

        public override string ToString()
        {
            return string.Format("Device Name {0} , IP Address {1} , Port {2}", DeviceName, m_strIpAddress, m_iTcpPort);
        }
    }
}
