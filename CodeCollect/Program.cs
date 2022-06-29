using System;
using System.Threading.Tasks;
using CCFrame.Ethernet;

namespace CodeCollect
{
    class Program
    {
        static void Main(string[] args)
        {
            ////Console.WriteLine("Hello World!");
            //AsyncDemo demo = new AsyncDemo();
            ////VersionManage.WriteAttributeInfo()
            //VersionManage.DisplayTypeInfo(demo.GetType());

            //HttpClientDemo demo = new HttpClientDemo();
            //await demo.GetDataAdvancedAsync();
            SocketServer server = new SocketServer();
            server.Start();

            Console.ReadLine();
        }
    }
}
