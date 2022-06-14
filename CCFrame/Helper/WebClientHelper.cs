using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：WebClientHelper
// 创 建 者：蔡程健
// 创建时间：22/6/14 16:57:52
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Helper
{
    public class WebClientHelper
    {
        private string UrlAddress { get; set; } = $"http://192.168.0.40:5031/";

        public Task<string> TaskPostClient(string url)
        {
            using (var client = new WebClient())
            {
                url = UrlAddress + url;
                //await关键字会接触线程（UI）的阻塞，完成其他任务，当方法完成后处理
                //string content = await client.DownloadStringTaskAsync(url);
                return client.DownloadStringTaskAsync(url);
            }
        }
    }
}
