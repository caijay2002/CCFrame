using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;


#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2022   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 公司名称：
 * 命名空间：CodeCollect.DataProtection
 * 文件名：DataProtectionSample
 * 创建者：蔡程健
 * 创建时间：2022/6/24 21:40:55
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

namespace CodeCollect.DataProtection
{
    public class DataProtectionSample
    {

        public static void Demo(string fileName)
        {
            MySafe safe = SetupDataProtection();

            Write(safe, fileName);//写入数据

            Read(safe, fileName);//读取数据
        }

        //SetupDataProtection.AddDataProtection() 通过哦依赖注入添加数据保护
        public static MySafe SetupDataProtection()
        {
            var services = new ServiceCollection();
            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo("."))
                .SetDefaultKeyLifetime(TimeSpan.FromDays(20))
                .ProtectKeysWithDpapi();
            services.AddTransient<MySafe>();

            IServiceProvider provider = services.BuildServiceProvider();
            return provider.GetService<MySafe>();
        }
        /// <summary>
        /// 加密读取
        /// </summary>
        /// <param name="safe"></param>
        /// <param name="fileName"></param>
        public static void Read(MySafe safe, string fileName)
        {
            string encrypted = File.ReadAllText(fileName);
            string decrypted = safe.Decrypt(encrypted);
            Console.WriteLine(decrypted);
        }
        /// <summary>
        /// 加密写入
        /// </summary>
        /// <param name="safe"></param>
        /// <param name="fileName"></param>
        public static void Write(MySafe safe, string fileName)
        {
            Console.WriteLine("enter content to write:");
            string content = Console.ReadLine();
            string encrypted = safe.Encrypt(content);
            File.WriteAllText(fileName, encrypted);
            Console.WriteLine($"content written to {fileName}");
        }
    }
}
