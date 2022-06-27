using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2022   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 公司名称：
 * 命名空间：CodeCollect
 * 文件名：HttpClientDemo
 * 创建者：蔡程健
 * 创建时间：2022/6/23 20:36:12
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
    internal class HttpClientDemo
    {
        //https://bandex.top/api/MacApplyYun/GetTpmGpUserInfYun
        //http://services.odata.org/Northwind/Northwind.svc/Regions
        private const string NorthwindUrl = "https://bandex.top/api/MacApplyYun/GetTpmGpUserInfYun";
        private const string IncorrectUrl = "http://services.odata.org/Northwind1/Northwind.svc/Regions";

        private HttpClient _httpClient;
        public HttpClient HttpClient => _httpClient ?? (_httpClient = new HttpClient());

        public async Task GetDataSimpleAsync()
        {
            HttpResponseMessage response = await HttpClient.GetAsync(NorthwindUrl);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");
                string responseBodyAsText = await response.Content.ReadAsStringAsync();//数据内容
                Console.WriteLine($"Received payload of {responseBodyAsText.Length} characters");
                Console.WriteLine();
                Console.WriteLine(responseBodyAsText);
            }
        }

        public async Task GetDataAdvancedAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, NorthwindUrl);

            HttpResponseMessage response = await HttpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");
                string responseBodyAsText = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Received payload of {responseBodyAsText.Length} characters");
                Console.WriteLine();
                Console.WriteLine(responseBodyAsText);
            }
        }
    }
}
