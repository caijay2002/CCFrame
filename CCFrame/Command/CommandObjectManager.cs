using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region << 命令管理类 >>
/*----------------------------------------------------------------
// 文件名称：CommandObjectHandler
// 创 建 者：蔡程健
// 创建时间：22/6/7 16:54:40
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Command
{
    public class CommandObjectManager
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ConcurrentQueue<CommandObject> _commandObjectQueue = new ConcurrentQueue<CommandObject>();

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="command"></param>
        public void AddCommandObject(CommandObject command)
        {
            _commandObjectQueue.Enqueue(command);
        }

        /// <summary>
        /// 获取任务
        /// </summary>
        /// <returns></returns>
        public CommandObject GetCommandObject()
        {
            var result = _commandObjectQueue.TryDequeue(out CommandObject command);
            return command;
        }

        /// <summary>
        /// 是否有任务
        /// </summary>
        public bool HasCommandObject => _commandObjectQueue.Count > 0;
    }
}
