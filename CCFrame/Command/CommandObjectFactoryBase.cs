using CCFrame.Command.Data;
using CCFrame.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：CommandObjectFactoryBase
// 创 建 者：蔡程健
// 创建时间：22/6/24 14:58:00
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Command
{
    public class CommandObjectFactoryBase
    {

        protected static Command CreatOPCCommand(string address, ReadOrWrite readOrWrite, DataType dataType, object value,string stepName = "")
        {
            var cmd = new Command();
            cmd.StepName = stepName;
            cmd.CmdType = CmdType.OPCUA;
            cmd.Data = new OPCData() { Address = address, DataType = dataType, Value = value};
            cmd.ReadOrWrite = readOrWrite;
            return cmd;
        }

        protected static Command CreatWEBCommand(string address, ReadOrWrite readOrWrite, object value)
        {
            var cmd = new Command();
            cmd.CmdType = CmdType.WEB;
            cmd.Data = new WebData() { Address = address, DataType = DataType.String, Value = value };
            cmd.ReadOrWrite = readOrWrite;
            return cmd;
        }

        protected static Command CreatMXCommand(string address, DataType dataType, ReadOrWrite readOrWrite, object value)
        {
            var cmd = new Command();
            cmd.Data = new MXPlcData() { Address = address, DataType = dataType, Value = value };
            cmd.CmdType = CmdType.MXPLC;
            cmd.ReadOrWrite = ReadOrWrite.Write;
            return cmd;
        }
    }
}
