using CCFrame.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region << 命令集合处理基类 >>
/*----------------------------------------------------------------
// 文件名称：CommandObjectHandlerBase
// 创 建 者：蔡程健
// 创建时间：22/6/24 16:43:57
// 文件版本：
// ===============================================================
// 功能描述：子类中做具体处理
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Command
{
    public class CommandObjectHandlerBase
    {
        private static bool isRun = false;

        public static Task StartAsync(CommandObjectManager dm) =>
    Task.Run(new CommandObjectHandlerBase(dm).RunCommandTask);

        public static void Stop()
        {
            isRun = false;
        }

        protected CommandObjectHandlerBase(CommandObjectManager dm) =>
            _commandObjectManager = dm ?? throw new ArgumentNullException(nameof(dm));

        private CommandObjectManager _commandObjectManager;

        protected async Task RunCommandTask()
        {
            isRun = true;

            while (isRun)
            {
                if (_commandObjectManager.HasCommandObject)
                {
                    CommandObject cmd = _commandObjectManager.GetCommandObject();

                    while (true)
                    {
                        //for (int i = 0; i < cmd.Commands.Count; i++)
                        //{
                        Console.WriteLine("Step:" + cmd.Step);
                        if (cmd.Step == cmd.Commands.Count || cmd.Step == -1)
                        {
                            //命令执行完成
                            FinishedCommand(cmd);
                            break;
                        }
                        else
                        {
                            //运行命令
                            RunCommand(cmd);
                        }
                    }
                }
                await Task.Delay(200);
            }
        }

        /// <summary>
        /// 完成任务
        /// </summary>
        /// <param name="Obj"></param>
        public virtual void FinishedCommand(CommandObject Obj)
        {
            if (Obj.Step == -1)//执行任务异常
            {
                Obj.OverTime = DateTime.Now;
                LogSvr.Error(" 任务异常结束：" + Obj.ToString());
            }
            if (Obj.Step == Obj.Commands.Count)//执行任务成功
            {
                Obj.OverTime = DateTime.Now;
                LogSvr.Error(" 任务成功结束：" + Obj.ToString());
            }
        }

        public virtual void RunCommand(CommandObject Obj)
        {

        }
    }
}
