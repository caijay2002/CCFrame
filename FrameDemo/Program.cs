using System;
using CCFrame;
using CCFrame.Extensions;

namespace FrameDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            float f = 123.12f;
            var bytes = BitConverter.GetBytes(f);
            ByteArrayHelper.ConsoleDataValueInfo(bytes);
        }

        public static int BitToInt16Converter(byte[] bytes)
        {
            string str = "";
            foreach (byte b in bytes)
            {
                str += b.ToString("X2");
            }
            int temp = Convert.ToUInt16(str, 16);
            return temp;
        }
    }
}
