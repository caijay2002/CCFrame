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

            //HttpClientDemo demo = new HttpClientDemo();
            //await demo.GetDataAdvancedAsync();

            DataProtection.DataProtectionSample.Demo(@"D:\Learning\ProfessionalCSharp7-master\24Security\DataProtectionSample\bin\Debug\net5.0\test1");


            Console.ReadLine();
        }
    }
}
