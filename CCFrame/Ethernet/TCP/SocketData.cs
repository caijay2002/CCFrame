using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using CCFrame.Command.Data;

#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：SocketData
// 创 建 者：蔡程健
// 创建时间：22/6/29 10:43:23
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Ethernet
{
    ///<summary>
    /// 版 本：v1.0.0
    /// 创建人：蔡程健
    /// 日 期：20/6/22 11:39:12
    /// 描 述：数据项
    ///</summary>
    public class SocketData: IData
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

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

        public byte[] Buttf { get; set; }

        //有效数据的长度
        public int Length { get; set; }

        /// <summary>
        /// 错误
        /// </summary>
        public SocketException Error { get; set; } = null;

        public SocketData(int size)
        {
            Buttf = new byte[size];
            Length = Buttf.Length;
        }

        public SocketData(byte[] buttf)
        {
            Buttf = buttf;
            Length = buttf.Length;
        }

        public SocketData(string data)
        {
            Buttf = Encoding.ASCII.GetBytes(data);
            Length = Buttf.Length;
        }
        //TODO:后期追加
        public SocketData(object data)
        {

        }

        /// <summary>
        /// 依据Length获得有效数据
        /// </summary>
        /// <returns></returns>
        public byte[] GetValidButtf()
        {
            return Buttf.Take(Length).ToArray();
        }

        /// <summary>
        /// 用文本显示内容
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Encoding.ASCII.GetString(Buttf, 0, Buttf.Length);
            //return Length.ToString() + ":" + BitConverter.ToString(Buttf, 0, Buttf.Length);
        }
    }
}
