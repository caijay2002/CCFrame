using System;
using CCFrame.Command.Data;
using CCFrame;

#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：ICommand
// 创 建 者：蔡程健
// 创建时间：22/5/28 19:49:37
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Command
{
    public class Command
    {
        /// <summary>
        /// PLC数据
        /// </summary>
        public IData Data { get; set; }
        /// <summary>
        /// 步骤名称
        /// </summary>
        public string StepName { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsOK { get; set; }
        /// <summary>
        /// 不成功时，是否需要断开
        /// </summary>
        public bool IsFinished { get; set; }
        /// <summary>
        /// 重试次数
        /// </summary>
        public int RetryTime { get; set; }
        /// <summary>
        /// 命令类型
        /// </summary>
        public CmdType CmdType { get; set; }
        /// <summary>
        /// 读取或写入
        /// </summary>
        public ReadOrWrite ReadOrWrite { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime FinishedTime { get; set; }
    }

    public enum ReadOrWrite
    {
        /// <summary>
        /// 读取
        /// </summary>
        Read = 0,
        /// <summary>
        /// 写入
        /// </summary>
        Write = 1,
    }

    /// <summary>
    /// 命令类型
    /// </summary>
    public enum CmdType
    {
        /// <summary>
        /// 三菱PLC
        /// </summary>
        MXPLC = 0,
        /// <summary>
        /// OPCUA
        /// </summary>
        OPCUA = 1,
        /// <summary>
        /// WEB API
        /// </summary>
        WEB = 6,
        /// <summary>
        /// 其他命令
        /// </summary>
        OTHER = 99,
    }
}
