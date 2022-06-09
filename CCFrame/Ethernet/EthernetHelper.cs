using CCFrame.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;


#region << 以太网通信帮助类 >>
/*----------------------------------------------------------------
// 文件名称：EthernetHelper
// 创 建 者：蔡程健
// 创建时间：22/5/29 10:01:43
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Ethernet
{
    public class EthernetHelper
    {
        public static OperateResult PingDevice(string ip, int timeout)
        {
            OperateResult result = new OperateResult();
            try
            {
                Ping pingSender = new Ping();
                //Ping 选项设置
                PingOptions options = new PingOptions();
                options.DontFragment = true;
                //测试数据
                string data = "";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                //调用同步 send 方法发送数据,将返回结果保存至PingReply实例
                PingReply reply = pingSender.Send(ip, timeout, buffer, options);
                //isOk = reply.Status == IPStatus.Success;
                if(reply.Status == IPStatus.Success)
                {
                    return OperateResult.CreateSuccessResult();
                }
                else
                {
                    return OperateResult.CreateFailedResult(new OperateResult() 
                    { 
                        Message = reply.Status.ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                LogSvr.Error(string.Format("PingDevice {0} 失败，故障内容 {1}", ip, ex.Message));
                return OperateResult.CreateFailedResult(new OperateResult()
                {
                    Message =$"PingDevice Error:{ex.Message}"
                }) ;
            }
        }
    }
}
