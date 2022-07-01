using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2022   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 公司名称：
 * 命名空间：CCFrame.DataConvert
 * 文件名：ByteArrayHelper
 * 创建者：蔡程健
 * 创建时间：2022/6/30 21:32:24
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

namespace CCFrame.Extensions
{
    public static class ByteArrayHelper
    {
        /// <summary>
        /// 数据库TimeStamp属性的字节数组，以可视化输出
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string StringOutput(this byte[] data)
        {
            var sb = new StringBuilder();
            foreach (byte b in data)
            {
                sb.Append($"{b}.");
            }
            return sb.ToString();
        }
    }
}
