using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCFrame.Command.Data;

#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：ShortHelper
// 创 建 者：蔡程健
// 创建时间：22/5/31 13:04:09
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.DataConvert
{
    public static class ShortHelper
    {

        public static ulong ToUlong(short[] buffer)
        {
            try
            {
                if (buffer.Length != 4) return 0;
                var val = Convert.ToUInt64(buffer[0]) |
                    (Convert.ToUInt64(buffer[1]) << 16) |
                    (Convert.ToUInt64(buffer[2]) << 32) |
                    (Convert.ToUInt64(buffer[3]) << 48);
                return val;
            }
            catch (Exception)
            {
                return 0;
            }            
        }

        public static uint ToUint(short[] buffer)
        {
            try
            {
                if (buffer.Length != 2) return 0;
                var val = Convert.ToUInt32(buffer[0]) |
                (Convert.ToUInt32(buffer[1]) << 16);
                return val;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int ToInt(short[] buffer)
        {
            try
            {
                if (buffer.Length != 2) return 0;
                var val = Convert.ToInt32(buffer[0]) |
                (Convert.ToInt32(buffer[1]) << 16);
                return val;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static string ToAscii(short[] buffer)
        {
            List<byte> cmdBytes = new List<byte>();
            for (int i = 0; i < buffer.Length; i++)
            {
                cmdBytes.AddRange(BitConverter.GetBytes(buffer[i]));
            }

            return Encoding.ASCII.GetString(cmdBytes.ToArray());
        }

        public static bool ToBool(short[] buffer)
        {
            try
            {
                return buffer[0] == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static short[] ToShorts(IData data)
        {
            short[] buffer;
            switch (data.DataType)
            {
                case DataType.Bit:
                    buffer = new short[1] { ((bool)data.Value) ? (short)1 : (short)0 };
                    break;
                case DataType.Byte:
                    buffer = new short[1] { Convert.ToByte(data.Value) };
                    break;
                case DataType.Int32:
                    buffer = new short[] {(short)((int)data.Value&0xFFFF), (short)((int)data.Value >> 16) };
                    break;
                case DataType.Short:
                    buffer = new short[1] { (short)Convert.ToUInt16(data.Value) };
                    break;
                //case DataType.Double:
                //    buffer = BitConverter.GetBytes(Convert.ToDouble(data.Value));
                //    break;

                default:
                    buffer = new short[] { 0 };
                    break;
            }

            return buffer;
        }
    }
}
