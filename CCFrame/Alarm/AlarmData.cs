using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：AlarmData
// 创 建 者：蔡程健
// 创建时间：22/4/27 13:46:59
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Alarm
{
    public class AlarmData
    {
        /// <summary>
        /// 故障地址 F寄存器
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 代码或者地址
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 故障级别
        /// Light,Normal,Heavy,
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 详细内容
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// 状态：1=发生, 0=故障恢复
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Date { get; set; }
    }

    //public enum AlarmStatus
    //{
    //    /// <summary>
    //    /// 发生报警
    //    /// </summary>
    //    Exist,
    //    /// <summary>
    //    /// 解除报警
    //    /// </summary>
    //    Release,
    //}
}
