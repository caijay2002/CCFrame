using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region << 驱动配置项 >>
/*----------------------------------------------------------------
// 文件名称：DriverConfigItem
// 创 建 者：蔡程健
// 创建时间：22/5/29 9:56:42
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Driver
{
    /// <summary>
    /// 驱动配置项
    /// </summary>
    public class DriverConfigItem
    {
        public DriverConfigItem() { }
        public DriverConfigItem(string key, string value, string description)
        {
            Key = key;
            Value = value;
            Description = description;
        }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }
}
