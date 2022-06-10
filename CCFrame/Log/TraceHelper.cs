using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2022   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 公司名称：
 * 命名空间：CCFrame.Log
 * 文件名：TraceHelper
 * 创建者：蔡程健
 * 创建时间：2022/6/9 20:58:17
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

namespace CCFrame.Log
{
    public class TraceHelper
    {
        //将线程和任务信息写入控制台
        public static void TraceThreadAndTask(string info)
        {
            string taskInfo = Task.CurrentId == null ? "no task" : "task " + Task.CurrentId;

            Console.WriteLine($"{info} in thread {Thread.CurrentThread.ManagedThreadId} and {taskInfo}");
        }
    }
}
