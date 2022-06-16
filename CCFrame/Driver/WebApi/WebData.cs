using CCFrame.Command.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：WebData
// 创 建 者：蔡程健
// 创建时间：22/6/15 11:28:34
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Driver
{
    public class WebData : IData
    {
        /// <summary>
        /// 地址 URL地址
        /// </summary>
        public string Address { get; set; }
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
