using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region << 版本标注 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2022   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 公司名称：
 * 命名空间：CCFrame.Version
 * 文件名：LastModifiedAttribute
 * 创建者：蔡程健
 * 创建时间：2022/6/11 20:47:48
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

namespace CCFrame.Version
{
    /// <summary>
    /// 用于标记最后一次修改数据项的时间
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Constructor, AllowMultiple = true, Inherited = false)]
    public class LastModifiedAttribute : Attribute
    {
        private readonly DateTime _dateModified;
        private readonly string _changes;

        public LastModifiedAttribute(string dateModified, string changes)
        {
            _dateModified = DateTime.Parse(dateModified);
            _changes = changes;
        }

        public DateTime DateModified => _dateModified;

        public string Changes => _changes;

        public string Issues { get; set; }
    }

    /// <summary>
    /// 不带任何参数特性，它用于把程序集标记通过LastModifiedAttribute维护的文档，它读取的程序集是我们使用
    /// 自动文档过程生成的那个程序集。
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class SupportsWhatsNewAttribute : Attribute
    {
    }
}
