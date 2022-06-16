using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region << 命令集合 >>
/*----------------------------------------------------------------
// 文件名称：CommandObject
// 创 建 者：蔡程健
// 创建时间：22/5/31 19:34:53
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Command
{
    public class CommandObject
    {
        /// <summary>
        /// plc地址
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string CommandName { get; set; }
        /// <summary>
        /// 当前步骤
        /// </summary>
        public int Step { get; set; }
        /// <summary>
        /// 命令创建时间
        /// </summary>
        public DateTime CreatTime { get; set; }
        /// <summary>
        /// 命令结束时间
        /// </summary>
        public DateTime OverTime { get; set; }
        /// <summary>
        /// 命令队列
        /// </summary>
        public Dictionary<int, Command> Commands { get; set; }

        public override string ToString()
        {
            return $"任务名称:{CommandName} 地址：{Key} 当前步骤：{Step} 创建时间：{CreatTime} 结束时间：{OverTime}";
        }

        public CommandObject()
        {
            Commands = new Dictionary<int, Command>();
            CreatTime = DateTime.Now;
        }
    }
}
