using System;
using System.Threading.Tasks;

namespace CodeCollect
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ////Console.WriteLine("Hello World!");
            //AsyncDemo demo = new AsyncDemo();
            ////VersionManage.WriteAttributeInfo()
            //VersionManage.DisplayTypeInfo(demo.GetType());

            HttpClientDemo demo = new HttpClientDemo();
            await demo.GetDataAdvancedAsync();

            Console.ReadLine();
        }
    }
}
