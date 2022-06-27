using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region << 命令 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2022   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 公司名称：
 * 命名空间：CodeCollect
 * 文件名：Command
 * 创建者：蔡程健
 * 创建时间：2022/6/23 21:49:43
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
    internal class Command<T>
    {
        public Command(string option, string text, Action<T> action)
        {
            Option = option;
            Text = text;
            Action = action;
        }

        public string Option { get; }
        public string Text { get; }
        public Action<T> Action { get; }
    }
}
