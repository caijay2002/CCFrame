using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：IData
// 创 建 者：蔡程健
// 创建时间：22/5/27 16:51:46
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Command.Data
{
    public interface IData
    {
        /// <summary>
        /// 地址
        /// </summary>
        string Address { get; set; }

        /// <summary>
        /// 数值
        /// </summary>
        object Value { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        DateTime TimeStamp { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        DataType DataType { get; set; }
    }

    public enum DataType
    {
        Bit = 0,
        Byte = 1,
        Word = 2,
        DWord = 3,
        Long = 4,
        Float = 5,
        Double = 6,
        String = 7,
        DateTime = 8,
        Short = 9,        
        Ascii = 11,
        Int16 = 20,
        Int32 = 21,
        Int64 = 22,
        Default = 99,
    }
}
