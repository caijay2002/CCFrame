using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：FanucAPI
// 创 建 者：蔡程健
// 创建时间：22/6/30 11:41:25
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.CNC
{
    public class FanucAPI
    {
        private ushort m_ConnectionHandle;

        private string m_strIpAddress = "192.168.0.190";
        private ushort m_iTcpPort = 8193;
        private int m_ConnectTimeout = 10;

        public bool IsConnected
        {
            get
            {
                return m_ConnectionHandle != 0;
            }
        }

        public FanucAPI()
        {

        }

        public FanucAPI(string ip, ushort port, int timeOut)
        {
            m_strIpAddress = ip;
            m_iTcpPort = port;
            m_ConnectTimeout = timeOut;
        }

        /// <summary>
        /// 设备连接
        /// </summary>
        /// <returns></returns>
        public OperateResult Connect()
        {
            OperateResult result = new OperateResult();

            if (IsConnected) return OperateResult.CreateSuccessResult("已连接");

            short ret;
            try
            {
                //发那科驱动的连接函数
                ret = Focas1.cnc_allclibhndl3(m_strIpAddress, m_iTcpPort, m_ConnectTimeout, out m_ConnectionHandle);

                if (ret == Focas1.EW_OK)
                {
                    result = OperateResult.CreateSuccessResult();
                }
                else
                {
                    result.ErrorCode = ret;
                    //ERROR CODE
                    //EW_SOCKET (-16)   Socket communication error Check the power supply of CNC, Ethernet I / F board, Ethernet connection cable.
                    //EW_NODLL  (-15)   There is no DLL file for each CNC series 
                    //EW_HANDLE (-8)    Allocation of handle number is failed. 
                    return OperateResult.CreateFailedResult(result);
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.ErrorCode = ex.GetHashCode();
                return OperateResult.CreateFailedResult(result);
            }

            return result;
        }

        public OperateResult Disconnect()
        {

            //cnc_freelibhndl
            OperateResult result = new OperateResult();

            if (!IsConnected) return OperateResult.CreateSuccessResult("未连接");
            short ret;
            try
            {
                if (!IsConnected) return OperateResult.CreateSuccessResult();
                //发那科驱动的断开函数
                ret = Focas1.cnc_freelibhndl(m_ConnectionHandle);

                if (ret == Focas1.EW_OK)
                {
                    result = OperateResult.CreateSuccessResult();
                    m_ConnectionHandle = 0;
                }
                else
                {
                    result.ErrorCode = ret;
                    //ERROR CODE
                    //EW_SOCKET (-16)   Socket communication error Check the power supply of CNC, Ethernet I / F board, Ethernet connection cable.
                    //EW_NODLL  (-15)   There is no DLL file for each CNC series 
                    //EW_HANDLE (-8)    Allocation of handle number is failed. 
                    return OperateResult.CreateFailedResult<bool>(result);
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.ErrorCode = ex.GetHashCode();
                return OperateResult.CreateFailedResult(result);
            }

            return result;
        }

        public OperateResult<uint> GetDeviceFeedOverride()
        {
            if (!IsConnected) return OperateResult.CreateFailedResult<uint>(new OperateResult("设备未连接"));
            OperateResult<uint> result = new OperateResult<uint>();

            Focas1.IODBPMC0 iODBPMC0 = new Focas1.IODBPMC0();

            try
            {
                var code = Focas1.pmc_rdpmcrng(m_ConnectionHandle, 0, 0, 12, 12, 8 + 1, iODBPMC0);

                byte b = iODBPMC0.cdata[0];

                uint val = DToSpnSpeed(b);

                if (code == Focas1.EW_OK)
                {
                    result = OperateResult.CreateSuccessResult(val);
                }
                else
                {
                    result.ErrorCode = code;
                    return OperateResult.CreateFailedResult<uint>(result);
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.ErrorCode = ex.GetHashCode();
                return OperateResult.CreateFailedResult<uint>(result);
            }

            return result;
        }

        public OperateResult<uint> GetDeviceSpindleOverride()
        {
            if (!IsConnected) return OperateResult.CreateFailedResult<uint>(new OperateResult("设备未连接"));
            OperateResult<uint> result = new OperateResult<uint>();

            Focas1.IODBPMC0 iODBPMC0 = new Focas1.IODBPMC0();

            try
            {

                var code = Focas1.pmc_rdpmcrng(m_ConnectionHandle, 0, 0, 30, 30, 8 + 1, iODBPMC0);

                uint val = iODBPMC0.cdata[0];

                if (code == Focas1.EW_OK)
                {

                    result = OperateResult.CreateSuccessResult(val);
                }
                else
                {
                    result.ErrorCode = code;
                    return OperateResult.CreateFailedResult<uint>(result);
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.ErrorCode = ex.GetHashCode();
                return OperateResult.CreateFailedResult<uint>(result);
            }

            return result;
        }

        public OperateResult<int> GetToolOffset(short toolNo, string type)
        {
            if (!IsConnected) return OperateResult.CreateFailedResult<int>(new OperateResult("设备未连接"));
            OperateResult<int> result = new OperateResult<int>();
            Focas1.ODBTOFS oDBTOFS = new Focas1.ODBTOFS();

            short axis = 0;
            short size = 8;

            switch (type)
            {
                case "R"://形状（H）
                    axis = 3;
                    break;

                case "H"://磨损（H）
                    axis = 2;
                    break;

                case "L"://形状（L）
                    axis = 1;
                    break;

                case "D"://磨损（D）
                    axis = 0;
                    break;

                default:
                    break;
            }

            try
            {

                var code = Focas1.cnc_rdtofs(m_ConnectionHandle, toolNo, axis, size, oDBTOFS);

                if (code == Focas1.EW_OK)
                {

                    result = OperateResult.CreateSuccessResult(oDBTOFS.data);
                }
                else
                {
                    result.ErrorCode = code;
                    return OperateResult.CreateFailedResult<int>(result);
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.ErrorCode = ex.GetHashCode();
                return OperateResult.CreateFailedResult<int>(result);
            }

            return result;
        }

        public OperateResult<int> GetDeviceAxisPosition(short axis, short type)
        {
            if (!IsConnected) return OperateResult.CreateFailedResult<int>(new OperateResult("设备未连接"));
            OperateResult<int> result = new OperateResult<int>();
            Focas1.IODBZOR iODBZOR = new Focas1.IODBZOR();
            short startID = type;
            short endID = type;
            short len = 12;
            //iODBZOR.type = type;
            //iODBZOR.datano_s = axis;
            //iODBZOR.datano_e = axis;

            try
            {
                var code = Focas1.cnc_rdzofsr(m_ConnectionHandle, startID, axis, endID, len, iODBZOR);

                int val = iODBZOR.data[0];

                if (code == Focas1.EW_OK)
                {

                    result = OperateResult.CreateSuccessResult(val);
                }
                else
                {
                    result.ErrorCode = code;
                    return OperateResult.CreateFailedResult<int>(result);
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.ErrorCode = ex.GetHashCode();
                return OperateResult.CreateFailedResult<int>(result);
            }

            return result;

        }

        /// <summary>
        /// 主轴刀具号
        /// </summary>
        /// <returns></returns>
        public OperateResult<int> GetToolNo()
        {
            if (!IsConnected) return OperateResult.CreateFailedResult<int>(new OperateResult("设备未连接"));
            OperateResult<int> result = new OperateResult<int>();

            Focas1.IODBPMC0 iODBPMC0 = new Focas1.IODBPMC0();
            //pmc_rdpmcrng
            //adr_type F = 1
            //data_type = 0     0 : Byte type  1 : Word type  2 : Long type 
            //s_number 26
            //e_number 26
            //data_type is 0(byte type) : length = 8 + N 
            //data_type is 1(word type) : length = 8 + N × 2 
            //data_type is 2(long type) : length = 8 + N × 4 
            try
            {
                var code = Focas1.pmc_rdpmcrng(m_ConnectionHandle, 1, 0, 26, 26, 9, iODBPMC0);

                if (code == Focas1.EW_OK)
                {
                    int input = Convert.ToInt32(iODBPMC0.cdata[0]);
                    //var result = ConvertToData(input, 16);
                    result = OperateResult.CreateSuccessResult(ConvertToData(input, 16));
                }
                else
                {
                    result.ErrorCode = code;
                    return OperateResult.CreateFailedResult<int>(result);
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.ErrorCode = ex.GetHashCode();
                return OperateResult.CreateFailedResult<int>(result);
            }

            return result;
        }

        public OperateResult<int, int> GetToolLife(short toolNo)
        {
            if (!IsConnected) return OperateResult.CreateFailedResult<int, int>(new OperateResult("设备未连接"));
            OperateResult<int, int> result = new OperateResult<int, int>();

            Focas1.IODBTR iODBTR = new Focas1.IODBTR();

            var code = Focas1.cnc_rdtoolrng(m_ConnectionHandle, toolNo, toolNo, 20, iODBTR);


            try
            {
                if (code == Focas1.EW_OK)
                {
                    var life = iODBTR.data.data1.life;
                    var count = iODBTR.data.data1.count;
                    //int input = Convert.ToInt32(iODBTR.data[0].);
                    ////var result = ConvertToData(input, 16);
                    result = OperateResult.CreateSuccessResult(life, count);
                }
                else
                {
                    result.ErrorCode = code;
                    return OperateResult.CreateFailedResult<int, int>(result);
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.ErrorCode = ex.GetHashCode();
                return OperateResult.CreateFailedResult<int, int>(result);
            }

            return result;
        }

        public OperateResult WriteToolOffset(short toolNo, string type, int value)
        {
            if (!IsConnected) return OperateResult.CreateFailedResult(new OperateResult("设备未连接"));
            OperateResult result = new OperateResult();

            short axis = 0;
            short size = 8;

            switch (type)
            {
                case "R"://形状（H）
                    axis = 3;
                    break;

                case "H"://磨损（H）
                    axis = 2;
                    break;

                case "L"://形状（L）
                    axis = 1;
                    break;

                case "D"://磨损（D）
                    axis = 0;
                    break;

                default:
                    break;
            }

            try
            {
                var code = Focas1.cnc_wrtofs(m_ConnectionHandle, toolNo, axis, size, value);

                if (code == Focas1.EW_OK)
                {

                    result = OperateResult.CreateSuccessResult(true);
                }
                else
                {
                    result.ErrorCode = code;
                    return OperateResult.CreateFailedResult(result);
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.ErrorCode = ex.GetHashCode();
                return OperateResult.CreateFailedResult(result);
            }

            return result;
        }

        /// <summary>
        /// 写入坐标
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public OperateResult WriteAxisPosition(short axis, short type, int value)
        {
            if (!IsConnected) return OperateResult.CreateFailedResult(new OperateResult("设备未连接"));
            OperateResult result = new OperateResult();

            Focas1.IODBZOR iODBZOR = new Focas1.IODBZOR();
            //axis: X = 1, Y = 2, Z = 3, A = 4,
            //type: 0 G53, 1 G54, 2 G55, 3 G56 

            iODBZOR.type = axis;
            iODBZOR.datano_s = type;
            iODBZOR.datano_e = type;
            iODBZOR.data[0] = value;
            //iODBZOR.data[1] = value;

            try
            {
                var code = Focas1.cnc_wrzofsr(m_ConnectionHandle, 12, iODBZOR);

                if (code == Focas1.EW_OK)
                {

                    result = OperateResult.CreateSuccessResult(true);
                }
                else
                {
                    result.ErrorCode = code;
                    return OperateResult.CreateFailedResult(result);
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.ErrorCode = ex.GetHashCode();
                return OperateResult.CreateFailedResult(result);
            }

            return result;
        }

        private uint DToSpnSpeed(byte inValue)
        {
            uint val = 0;

            switch (inValue)
            {
                case 0x00: return 0;
                case 0xF5: return 10;
                case 0xEB: return 20;
                case 0xE1: return 30;
                case 0xD7: return 40;
                case 0xCD: return 50;
                case 0xC3: return 60;
                case 0xB9: return 70;
                case 0xAF: return 80;
                case 0xA5: return 90;
                case 0x9B: return 100;
                case 0x91: return 110;
                case 0x87: return 120;
                case 0x7D: return 130;
                case 0x73: return 140;
                case 0x69: return 150;
            }
            return val;
        }

        private int ConvertToData(int input, int baseNum)
        {
            int result = 0;
            int.TryParse(Convert.ToString(input, baseNum), out result);
            return result;
        }
    }
}
