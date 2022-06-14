using System;
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
        public static OperateResult<string> PostClient(string postUrl)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var r = client.PostAsync(postUrl, null).Result.Content.ReadAsStringAsync();

                    //result = r.Result;
                    return OperateResult.CreateSuccessResult<string>(r.Result);
                }
            }
            catch (Exception ex)
            {
                Log.LogSvr.Error(ex.Message);
                return OperateResult.CreateFailedResult<string>(new OperateResult(ex.Message));
            }
        }
    }
}
