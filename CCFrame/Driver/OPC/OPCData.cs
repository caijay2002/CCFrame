using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCFrame.Command.Data;

#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：OPCData
// 创 建 者：蔡程健
// 创建时间：22/6/13 15:46:19
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Driver
{
    public class OPCData : IData
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 标题（站号）OP20A.S1
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 信号类型  例如：PC_Flag
        /// </summary>
        public string SignalType { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// 数值
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime TimeStamp { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public DataType DataType { get; set; }

        public override string ToString()
        {
            return $"地址：{Address},数值：{Value},描述：{Description}";
        }
    }
}
