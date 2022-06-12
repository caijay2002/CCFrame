using System;

namespace CodeCollect
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            AsyncDemo demo = new AsyncDemo();
            //VersionManage.WriteAttributeInfo()
            VersionManage.DisplayTypeInfo(demo.GetType());

            Console.ReadLine();
        }
    }
}
