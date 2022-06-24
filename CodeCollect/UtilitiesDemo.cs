using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2022   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 公司名称：
 * 命名空间：CodeCollect
 * 文件名：UtilitiesDemo
 * 创建者：蔡程健
 * 创建时间：2022/6/23 21:58:15
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
    internal class UtilitiesDemo
    {
        public static void IPAddressSample(string ipAddressString)
        {
            IPAddress address;
            if (!IPAddress.TryParse(ipAddressString, out address))
            {
                Console.WriteLine($"cannot parse {ipAddressString}");
                return;
            }

            byte[] bytes = address.GetAddressBytes();
            for (int i = 0; i < bytes.Length; i++)
            {
                Console.WriteLine($"byte {i}: {bytes[i]:X}");
            }

            Console.WriteLine($"family: {address.AddressFamily}, map to ipv6: {address.MapToIPv6()}, map to ipv4: {address.MapToIPv4()}");
            Console.WriteLine($"IPv4 loopback address: {IPAddress.Loopback}");
            Console.WriteLine($"IPv6 loopback address: {IPAddress.IPv6Loopback}");
            Console.WriteLine($"IPv4 broadcast address: {IPAddress.Broadcast}");
            Console.WriteLine($"IPv4 any address: {IPAddress.Any}");
            Console.WriteLine($"IPv6 any address: {IPAddress.IPv6Any}");
        }

        public static void UriSample(string url)
        {
            var page = new Uri(url);
            Console.WriteLine($"scheme: {page.Scheme}");

            Console.WriteLine($"host: {page.Host}, type:  {page.HostNameType}, idn host: {page.IdnHost}");
            Console.WriteLine($"port: {page.Port}");
            Console.WriteLine($"path: {page.AbsolutePath}");
            Console.WriteLine($"query: {page.Query}");
            foreach (var segment in page.Segments)
            {
                Console.WriteLine($"segment: {segment}");
            }

            var builder = new UriBuilder();
            builder.Host = "www.cninnovation.com";
            builder.Port = 80;
            builder.Path = "training/MVC";
            Uri uri = builder.Uri;
            Console.WriteLine(uri);
        }

        /// <summary>
        /// 解析DNS 输入主机地址  www.baidu.com
        /// </summary>
        /// <param name="hostname"></param>
        /// <returns></returns>
        public static async Task OnLookupAsync(string hostname)
        {
            try
            {
                IPHostEntry ipHost = await Dns.GetHostEntryAsync(hostname);
                Console.WriteLine($"Hostname: {ipHost.HostName}");
                foreach (IPAddress address in ipHost.AddressList)
                {
                    Console.WriteLine($"Address Family: {address.AddressFamily}");
                    Console.WriteLine($"Address: {address}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
