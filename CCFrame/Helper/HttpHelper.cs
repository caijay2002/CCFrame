﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：HttpHelper
// 创 建 者：蔡程健
// 创建时间：22/6/14 17:00:41
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Helper
{
    public class HttpHelper
    {
        private static string UrlAddress { get; set; } = $"http://192.168.0.40:5031/";

        public static OperateResult<string> PostClient(string postUrl)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var r = client.PostAsync(postUrl, null).Result.Content.ReadAsStringAsync();

                    return OperateResult.CreateSuccessResult<string>(r.Result);
                }
            }
            catch (Exception ex)
            {
                Log.LogSvr.Error(ex.Message);
                return OperateResult.CreateFailedResult<string>(new OperateResult(ex.Message));
            }
        }

        private HttpClient _httpClient;
        public HttpClient HttpClient => _httpClient ?? (_httpClient = new HttpClient());

        /// <summary>
        /// 异步获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<OperateResult<string>> GetDataAsync(string url)
        {
            var postUrl = UrlAddress + url;

            HttpResponseMessage response = await HttpClient.GetAsync(postUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseBodyAsText = await response.Content.ReadAsStringAsync();//数据内容
                return OperateResult.CreateSuccessResult<string>(responseBodyAsText);
            }
            return OperateResult.CreateFailedResult<string>(new OperateResult($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}"));
        }

        /// <summary>
        /// 异步的自定义类型请求
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<OperateResult<string>> GetDataAdvancedAsync(HttpMethod method, string url)
        {
            var postUrl = UrlAddress + url;
            var request = new HttpRequestMessage(method, postUrl);

            HttpResponseMessage response = await HttpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");
                string responseBodyAsText = await response.Content.ReadAsStringAsync();
                return OperateResult.CreateSuccessResult<string>(responseBodyAsText);
            }
            return OperateResult.CreateFailedResult<string>(new OperateResult($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}"));
        }
    }
}
