using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region << 字符串帮助类 >>
/*----------------------------------------------------------------
// 文件名称：StringHelper
// 创 建 者：蔡程健
// 创建时间：22/5/27 18:38:44
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Extensions
{
    public static class StringHelper
    {
        /// <summary>
        /// 获取地址的数值部分
        /// </summary>
        /// <returns></returns>
        public static int GetAddressNumber(string address)
        {
            var data = System.Text.RegularExpressions.Regex.Replace(address, @"[^0-9]+", "");
            if( int.TryParse(data, out int val))
            {
                return val;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// yyMMddHHmmss
        /// </summary>
        /// <param name="str"></param>
        /// <param name="stringFormat"></param>
        /// <returns></returns>
        public static DateTime StringToDate(string str,string stringFormat = "yyMMddHHmmss")
        {
            return DateTime.ParseExact(str, stringFormat, System.Globalization.CultureInfo.CurrentCulture);
        }
    }
}
