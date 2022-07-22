using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2022   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 公司名称：
 * 命名空间：CCFrame.DataConvert
 * 文件名：ByteArrayHelper
 * 创建者：蔡程健
 * 创建时间：2022/6/30 21:32:24
 * 版本：V1.0.0
 * 描述：
 *
 * ----------------------------------------------------------------
 * 修改人：
 * 时间：
 * 修改说明：
 *
 * 版本：V1.0.1
 *----------------------------------------------------------------*/
#endregion << 版 本 注 释 >>

namespace CCFrame.Extensions
{
    /// <summary>
    /// 任何转换先转byte[] 然后再转换成想要转换的值
    /// </summary>
    public static class ByteArrayHelper
    {
        /// <summary>
        /// 数据库TimeStamp属性的字节数组，以可视化输出
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string StringOutput(this byte[] data)
        {
            var sb = new StringBuilder();
            foreach (byte b in data)
            {
                sb.Append($"{b}_");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 转换成short Int16
        /// </summary>
        /// <param name="data"></param>
        public static short ToShort(byte[] data)
        {
            if (data.Length == 2)
                return BitConverter.ToInt16(data, 0);
            else
                return 0;
        }

        public static ushort ToUShort(byte[] data)
        {
            if (data.Length == 2)
                return BitConverter.ToUInt16(data, 0);
            else
                return 0;
        }

        public static Int32 ToInt32(byte[] data)
        {
            if (data.Length == 4)
                return BitConverter.ToInt32(data, 0);
            else
                return 0;
        }

        /// <summary>
        /// 打印数据转换相关信息
        /// </summary>
        /// <param name="data"></param>
        public static void ConsoleDataValueInfo(byte[] data)
        {
            Console.WriteLine($"HEX:  {BitConverter.ToString(data, 0, data.Length)}");
            var sb = new StringBuilder();

            foreach (byte b in data)
            {
                sb.Append($"{b} ");
            }
            Console.WriteLine($"value10:  {sb}");
            sb.Clear();

            short[] shorts = new short[data.Length/2];
            for (int i = 0; i < shorts.Length; i++) 
            {
                shorts[i] = BitConverter.ToInt16(new byte[] { data[data.Length - (i * 2 + 2)], data[data.Length - (i * 2 + 1)] }, 0);
                sb.Append($"{shorts[i]} ");
            }
            Console.WriteLine($"Short:  {sb}");
            sb.Clear();
            


            ushort[] ushorts = new ushort[data.Length / 2];
            for (int i = 0; i < ushorts.Length; i++)
            {
                ushorts[i] = BitConverter.ToUInt16(new byte[] { data[data.Length - (i * 2 + 2)], data[data.Length - (i * 2 + 1)] }, 0);
                sb.Append($"{ushorts[i]} ");
            }
            Console.WriteLine($"UShort:  {sb}");
            sb.Clear();

            for(int i = 0; i < data.Length; i++)
            {
                sb.Append($"{Convert.ToString(data[i], 2).PadLeft(8, '0')} ");
            }
            Console.WriteLine($"Binary:  {sb}");
        }
    }
}
