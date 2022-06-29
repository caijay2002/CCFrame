using System;
using CCFrame;
using CCFrame.Ethernet;

namespace FrameDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //OperateResultDemo.CreatResult();
            SocketClient client = new SocketClient();
            client.OnConnect();
            client.OnSendCommand(new SocketData("HELLO"));

            Console.WriteLine("Hello World!");
        }
    }
}
