using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;


#region << 键盘输入DEMO >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2022   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 公司名称：
 * 命名空间：CodeCollect
 * 文件名：ConsoleKeyBoardDemo
 * 创建者：蔡程健
 * 创建时间：2022/6/21 21:09:25
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

namespace CodeCollect
{
    internal class ConsoleKeyBoardDemo
    {
        //启用消息的产生者和使用者
        //static void Main()
        //{
        //    Task t1 = Task.Run(() => Producer());
        //    Task t2 = Task.Run(async () => await ConsumerAsync());
        //    Task.WaitAll(t1, t2);
        //}

        private static BufferBlock<string> s_buffer = new BufferBlock<string>();
        //Producer方法从控制台读取字符串，Post方法写到BufferBlock中
        public static void Producer()
        {
            bool exit = false;
            while (!exit)
            {
                string input = Console.ReadLine();
                if (string.Compare(input, "exit", ignoreCase: true) == 0)
                {
                    exit = true;
                }
                else
                {
                    s_buffer.Post(input);
                }
            }
        }
        //ConsumerAsync在循环中调用ReceiveAsync（）方法接收BufferBlock中的数据
        public static async Task ConsumerAsync()
        {
            while (true)
            {
                string data = await s_buffer.ReceiveAsync();
                Console.WriteLine($"user input: {data}");
            }
        }

        public static void KeyDemo()
        {
            var processInput = new ActionBlock<string>(s =>
            {
                Console.WriteLine($"user input: {s}");
            });

            bool exit = false;
            while (!exit)
            {
                string input = Console.ReadLine();
                if (string.Compare(input, "exit", ignoreCase: true) == 0)
                {
                    exit = true;
                }
                else
                {
                    processInput.Post(input);
                }
            }
        }
    }
}
