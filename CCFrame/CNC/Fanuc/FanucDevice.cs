using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region << 发那科CNC设备 >>
/*----------------------------------------------------------------
// 文件名称：FanucDevice
// 创 建 者：蔡程健
// 创建时间：22/6/30 11:51:45
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.CNC
{
    public class FanucDevice : NetDeviceBase
    {
        private FanucAPI? fanucAPI;

        private short toolNo = 1;

        public FanucDevice()
        {

        }

        public override void Initialize(string deviceName, string ip)
        {
            base.Initialize(deviceName, ip);
            fanucAPI = new FanucAPI(m_strIpAddress, (ushort)m_iTcpPort, m_ConnectTimeout);
        }

        public override OperateResult Connect()
        {
            if (fanucAPI == null) return OperateResult.CreateFailedResult(new OperateResult("设备未实例化"));
            var result = fanucAPI.Connect();
            if (!result.IsSuccess) CCFrame.Log.LogSvr.Error(DeviceName + "ErrorCode:" + result.ErrorCode + "Message:" + result.Message);
            return result;
        }

        public override OperateResult Disconnect()
        {
            if (fanucAPI == null) return OperateResult.CreateFailedResult(new OperateResult("设备未实例化"));
            var result = fanucAPI.Disconnect();
            if (!result.IsSuccess) CCFrame.Log.LogSvr.Error(DeviceName + "ErrorCode:" + result.ErrorCode + "Message:" + result.Message);
            return result;
        }

        public override OperateResult ReadData()
        {
            if (fanucAPI == null) return OperateResult.CreateFailedResult(new OperateResult("设备未实例化"));
            if (!Connect().IsSuccess) return OperateResult.CreateFailedResult(new OperateResult("连接失败"));

            #region 读取机床相关数据 并更新数据缓存
            var FeedOverride = fanucAPI.GetDeviceFeedOverride().Then((result) =>
            {
                UpdateValue("FeedOverride", result);
                return OperateResult.CreateSuccessResult(result);
            });
            var SpindleOverride = fanucAPI.GetDeviceSpindleOverride().Then((result) =>
            {
                UpdateValue("SpindleOverride", result);
                return OperateResult.CreateSuccessResult(result);
            });
            var G54X = fanucAPI.GetDeviceAxisPosition(1, 1).Then((result) =>
            {
                UpdateValue("G54X", result);
                return OperateResult.CreateSuccessResult(result);
            });
            var G54Y = fanucAPI.GetDeviceAxisPosition(2, 1).Then((result) =>
            {
                UpdateValue("G54Y", result);
                return OperateResult.CreateSuccessResult(result);
            });
            var G54Z = fanucAPI.GetDeviceAxisPosition(3, 1).Then((result) =>
            {
                UpdateValue("G54Z", result);
                return OperateResult.CreateSuccessResult(result);
            });
            //var G54A = fanucAPI.GetDeviceAxisPosition(4, 1);
            var ToolOffsetR = fanucAPI.GetToolOffset(toolNo, "R").Then((result) =>
            {
                UpdateValue("ToolOffsetR", result);
                return OperateResult.CreateSuccessResult(result);
            });
            //var ToolOffsetL = fanucAPI.GetToolOffset(5, "L");
            var ToolOffsetL = fanucAPI.GetToolOffset(toolNo, "L").Then((result) =>
            {
                UpdateValue("ToolOffsetL", result);
                return OperateResult.CreateSuccessResult(result);
            });
            //var ToolOffsetR = fanucAPI.GetToolOffset(5, "R");
            var MainToolNo = fanucAPI.GetToolNo().Then((result) =>
            {
                UpdateValue("MainToolNo", result);
                return OperateResult.CreateSuccessResult(result);
            });
            var ToolLife = fanucAPI.GetToolLife(toolNo).Then((result1, result2) =>
            {
                UpdateValue("ToolLife", result1);
                UpdateValue("ToolCount", result2);
                return OperateResult.CreateSuccessResult(result1, result2);
            });
            #endregion
            //读取数据不成功打印日志
            if (!FeedOverride.IsSuccess) CCFrame.Log.LogSvr.Error(" Read FeedOverride: " + FeedOverride.ToErrorMessage());
            //UpdateValue("FeedOverride", FeedOverride.Content);
            //UpdateValue("SpindleOverride", SpindleOverride.Content);
            //UpdateValue("G54X", G54X.Content);
            //UpdateValue("G54Y", G54Y.Content);
            //UpdateValue("G54Z", G54Z.Content);
            ////UpdateValue("G54A", G54A.Content);
            //UpdateValue("ToolOffsetR", ToolOffsetR.Content);
            //UpdateValue("ToolOffsetL", ToolOffsetL.Content);
            //UpdateValue("MainToolNo", MainToolNo.Content);
            //UpdateValue("ToolLife", ToolLife.Content1);
            //UpdateValue("ToolCount", ToolLife.Content2);

            Disconnect();

            return OperateResult.CreateSuccessResult(new OperateResult());
        }

        public void SetToolNo(short no)
        {
            toolNo = no;
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override OperateResult WriteData(string itemName, string[] args, object value)
        {
            if (fanucAPI == null) return OperateResult.CreateFailedResult(new OperateResult("设备未实例化"));
            if (!Connect().IsSuccess) return OperateResult.CreateFailedResult(new OperateResult("连接失败"));

            int data = 0;
            short toolNo = 1;
            short axis = 1;
            int.TryParse(value.ToString(), out data);

            switch (itemName)
            {
                case "G54":
                    if (args.Length < 1) return OperateResult.CreateFailedResult(new OperateResult("读取G54 缺少参数"));
                    if (args[0] == "X") axis = 1;
                    else if (args[0] == "Y") axis = 2;
                    else if (args[0] == "Z") axis = 3;
                    else if (args[0] == "A") axis = 4;
                    return fanucAPI.WriteAxisPosition(axis, 1, data);//G54  X
                case "ToolOffsetR":
                    short.TryParse(args[0].ToString(), out toolNo);
                    return fanucAPI.WriteToolOffset(toolNo, "R", data);
                case "ToolOffsetL":
                    short.TryParse(args[0].ToString(), out toolNo);
                    return fanucAPI.WriteToolOffset(toolNo, "L", data);
                default:

                    break;
            }

            Disconnect();

            return OperateResult.CreateFailedResult(new OperateResult());
        }
    }
}
