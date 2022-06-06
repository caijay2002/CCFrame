using ActUtlTypeLib;
using CCFrame.Log;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using CCFrame.Command.Data;


#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：MXDriver
// 创 建 者：蔡程健
// 创建时间：22/5/26 18:33:07
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Driver
{
    public class MXDriver
    {
        private ActUtlType axActUtlType = new ActUtlType();
        private string m_strIpAddress { get; set; }
        private int m_ConnectTimeout { get; set; }
        private int m_iLogicalStationNumber { get; set; }

        public bool IsConnected = false;
        

        public void Initialize(string address, int stationNumber)
        {
            m_strIpAddress = address;
            m_ConnectTimeout = 3000;//3秒钟
            m_iLogicalStationNumber = stationNumber;
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        public OperateResult Connect()
        {
            int iReturnCode;
            try
            {
                var result = Ethernet.EthernetHelper.PingDevice(m_strIpAddress, m_ConnectTimeout);
                if (result.IsSuccess)
                {
                    axActUtlType.ActLogicalStationNumber = m_iLogicalStationNumber;
                    iReturnCode = axActUtlType.Open();
                    IsConnected = (iReturnCode == 0);

                    if (!IsConnected)
                    {
                        return OperateResult.CreateFailedResult(new OperateResult($"MX 链接失败 代码：{iReturnCode}"));
                    }

                    return OperateResult.CreateSuccessResult();
                }
                else
                {
                    return OperateResult.CreateFailedResult(new OperateResult($"Ping {m_strIpAddress} 失败 内容{result.Message}"));
                }
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailedResult(new OperateResult($"[{m_strIpAddress}] QPlcMonitorDriver Connect, Exception:{ex.Message}"));
            }
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public OperateResult<short[]> ReadData(MXPlcData data)
        {
            short[] buffer = new short[data.Length];
            return ReadShortData(data.Address, data.Length);
        }

        #region 未使用
        //public bool ReadData(string address, ref short[] buffer)
        //{
        //    var ret = ReadPlcData(address, buffer);

        //    if (!ret.IsSuccess)
        //    {
        //        LogSvr.Error(string.Format("ReadStatus Error : address - {0} result - {1}", address, ret));
        //        return false;
        //    }

        //    return true;
        //}

        //public bool ReadData(string address, int length, Command.Data.DataType dataType, out string result)
        //{
        //    result = string.Empty;

        //    short[] buffer = new short[length];
        //    var ret = ReadPlcData(address, buffer);

        //    switch (dataType)
        //    {
        //        case DataType.String:
        //            List<byte> cmdBytes = new List<byte>();
        //            for (int i = 0; i < length; i++)
        //            {
        //                cmdBytes.AddRange(BitConverter.GetBytes(buffer[i]));
        //            }

        //            result = Encoding.ASCII.GetString(cmdBytes.ToArray());
        //            break;
        //        case DataType.Float:
        //            uint val = Convert.ToUInt32(buffer[0]) | (Convert.ToUInt32(buffer[1]) << 16);

        //            var fval = BitConverter.ToSingle(BitConverter.GetBytes(val), 0);
        //            result = fval.ToString();
        //            break;
        //        case DataType.Int32:
        //            result = buffer[0].ToString();
        //            break;
        //        default:
        //            break;
        //    }

        //    if (!ret.IsSuccess)
        //    {
        //        LogSvr.Error(string.Format("ReadStatus Error : address - {0} result - {1}", address, ret));
        //        return false;
        //    }

        //    return true;
        //}

        //public int ReadStatus(string address)
        //{
        //    int result = 0;

        //    short[] buffer = new short[1];
        //    var ret = ReadPlcData(address, buffer);

        //    if (!ret.IsSuccess)
        //    {
        //        LogSvr.Error(string.Format("ReadStatus Error : address - {0} result - {1}", address, ret));
        //        return 0;
        //    }

        //    int.TryParse(buffer[0].ToString(), out result);

        //    //DataCacheSvr.UpdateCache(address, result.ToString());//刷新缓存

        //    return result;
        //}
        #endregion

        /// <summary>
        /// 读取随机地址Short
        /// </summary>
        /// <param name="strAddr"></param>
        /// <returns></returns>
        private OperateResult<short[]> ReadShortData(string[] strAddr)
        {
            try
            {
                var strAddrList = string.Join("\n", strAddr);
                short[] buffer = new short[strAddr.Length];
                int iReturnCode = axActUtlType.ReadDeviceRandom2(strAddrList, buffer.Length, out buffer[0]);
                return new OperateResult<short[]>()
                {
                    IsSuccess = iReturnCode == 0,
                    ErrorCode = iReturnCode,
                    Content = buffer
                };                
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailedResult<short[]>(new OperateResult<short[]>()
                {
                    IsSuccess = false,
                    Message = $"ReadShortData Error:{ex.Message}",
                });
            }
        }

        /// <summary>
        /// 读取随机地址int
        /// </summary>
        /// <param name="strAddr"></param>
        /// <returns></returns>
        private OperateResult<int[]> ReadIntData(string[] strAddr)
        {
            try
            {
                var strAddrList = string.Join("\n", strAddr);
                int[] buffer = new int[strAddr.Length];
                int iReturnCode = axActUtlType.ReadDeviceRandom(strAddrList, buffer.Length, out buffer[0]);
                return new OperateResult<int[]>()
                {
                    IsSuccess = iReturnCode == 0,
                    ErrorCode = iReturnCode,
                    Content = buffer
                };
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailedResult<int[]>(new OperateResult<int[]>()
                {
                    IsSuccess = false,
                    Message = $"ReadPlcData Error:{ex.Message}",
                });
            }
        }

        /// <summary>
        /// 读取连续地址short
        /// </summary>
        /// <param name="strAddrStrat"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private OperateResult<short[]> ReadShortData(string strAddrStrat, int length)
        {
            try
            {
                short[] buffer = new short[length];
                int iReturnCode = axActUtlType.ReadDeviceBlock2(strAddrStrat, length, out buffer[0]);
                if(iReturnCode == 0)
                {
                    return OperateResult.CreateSuccessResult(buffer);
                }
                else
                {
                    return OperateResult.CreateFailedResult<short[]>(new OperateResult(iReturnCode, "读取数据失败"));
                }
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailedResult<short[]>(new OperateResult($"读取数据错误ReadShortData：{ex.ToString()}"));
            }
        }

        /// <summary>
        /// 读取连续地址int
        /// </summary>
        /// <param name="strAddrStrat"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private OperateResult<int[]> ReadIntData(string strAddrStrat, int length)
        {
            try
            {
                int[] buffer = new int[length];
                int iReturnCode = axActUtlType.ReadDeviceBlock(strAddrStrat, buffer.Length, out buffer[0]);
                if (iReturnCode == 0)
                {
                    return OperateResult.CreateSuccessResult(buffer);
                }
                else
                {
                    return OperateResult.CreateFailedResult<int[]>(new OperateResult(iReturnCode, "读取数据失败"));
                }
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailedResult<int[]>(new OperateResult($"读取数据错误：{ex.ToString()}"));
            }
        }

        private OperateResult<short[]> ReadBuffer(int lStartIO, int lAddress, int length)
        {
            try
            {
                short[] buffer = new short[length];
                int iReturnCode = axActUtlType.ReadBuffer(lStartIO, lAddress, buffer.Length, out buffer[0]);
                if (iReturnCode == 0)
                {
                    return OperateResult.CreateSuccessResult(buffer);
                }
                else
                {
                    return OperateResult.CreateFailedResult<short[]>(new OperateResult(iReturnCode, "读取数据失败"));
                }
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailedResult<short[]>(new OperateResult($"读取数据错误ReadBuffer：{ex.ToString()}"));
            }
        }
        /// <summary>
        /// 写入PLC数据
        /// </summary>
        /// <param name="strAddr"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private OperateResult WritePlcData(string[] strAddr, short[] buffer)
        {
            try
            {
                var strAddrList = string.Join("\n", strAddr);
                int iReturnCode = axActUtlType.WriteDeviceRandom2(strAddrList, buffer.Length, ref buffer[0]);
                return new OperateResult()
                {
                    IsSuccess = iReturnCode == 0,
                    ErrorCode = iReturnCode
                };
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailedResult(new OperateResult()
                {
                    IsSuccess = false,
                    Message = $"WritePlcData Error:{ex.Message}",
                });
            }
        }

        /// <summary>
        /// 写入PLC数据
        /// </summary>
        /// <param name="strAddr"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private OperateResult WritePlcData(string[] strAddr, int[] buffer)
        {
            try
            {
                var strAddrList = string.Join("\n", strAddr);
                int iReturnCode = axActUtlType.WriteDeviceRandom(strAddrList, buffer.Length, ref buffer[0]);
                return new OperateResult()
                {
                    IsSuccess = iReturnCode == 0,
                    ErrorCode = iReturnCode
                };
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailedResult(new OperateResult()
                {
                    IsSuccess = false,
                    Message = $"WritePlcData Error:{ex.Message}",
                });
            }
        }

        public OperateResult WritePlcData(string strAddrStrat, short[] buffer)
        {
            try
            {
                int iReturnCode = axActUtlType.WriteDeviceBlock2(strAddrStrat, buffer.Length, ref buffer[0]);
                return new OperateResult()
                {
                    IsSuccess = iReturnCode == 0,
                    ErrorCode = iReturnCode
                };
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailedResult(new OperateResult()
                {
                    IsSuccess = false,
                    Message = $"WritePlcData Error:{ex.Message}",
                });
            }
        }

        private OperateResult WritePlcData(string strAddrStrat, int[] buffer)
        {
            try
            {
                int iReturnCode = axActUtlType.WriteDeviceBlock(strAddrStrat, buffer.Length, ref buffer[0]);
                return new OperateResult()
                {
                    IsSuccess = iReturnCode == 0,
                    ErrorCode = iReturnCode
                };
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailedResult(new OperateResult()
                {
                    IsSuccess = false,
                    Message = $"WritePlcData Error:{ex.Message}",
                });
            }
        }

        public OperateResult WritePlcData(string strAddrStart, uint dword)
        {
            var buffer = new short[] {
                (short)(dword&0xFFFF),
                (short)(dword >> 16) };

            return WritePlcData(strAddrStart, buffer);
        }

        private OperateResult WritePlcData(string strAddrStart, ulong val)
        {
            var buffer = new short[] {
                            (short)(val&0xFFFF),
                            (short)(val >> 16),
                            (short)(val >> 32),
                            (short)(val >> 48)  };
            return WritePlcData(strAddrStart, buffer);
        }

        //private OperateResult<ulong> ReadPlcData(string strAddrStart)
        //{
        //    var buffer = new int[4];
        //    var ret = ReadShortData(strAddrStart, 4);
        //    if (ret.IsSuccess)
        //    {
        //        var data = DataConvert.ShortHelper.Short2Ulong(ret.Content);
        //        return OperateResult.CreateSuccessResult<ulong>(data);
        //    }
        //    else
        //    {
        //        return ret.ConvertFailed<ulong>();
        //    }
        //}

        //private OperateResult<uint> ReadPlcData(string strAddrStart)
        //{
        //    var buffer = new int[2];
        //    var ret = ReadShortData(strAddrStart, 2);
        //    if (ret.IsSuccess)
        //    {
        //        var data = DataConvert.ShortHelper.Short2Uint(ret.Content);
        //        return OperateResult.CreateSuccessResult<uint>(data);
        //    }
        //    else
        //    {
        //        return ret.ConvertFailed<uint>();
        //    }
        //}

        //private bool ReadPlcData(string strAddrStart, ref int val)
        //{
        //    var buffer = new int[2];
        //    var isOk = false;
        //    if (ReadPlcData(strAddrStart, buffer))
        //    {
        //        val = Convert.ToInt32(buffer[0]) |
        //            (Convert.ToInt32(buffer[1]) << 16);
        //        isOk = true;
        //    }
        //    return isOk;
        //}

        //private bool ReadPlcData(string strAddrStart, ref short val)
        //{
        //    var buffer = new int[1];
        //    var isOk = false;
        //    if (ReadPlcData(strAddrStart, buffer))
        //    {
        //        val = Convert.ToInt16(buffer[0]);
        //        isOk = true;
        //    }
        //    return isOk;
        //}




        //private bool ReadUInt16BitFromIndex(short data, int idx, out bool val)
        //{
        //    bool ret = false;
        //    val = false;
        //    if (idx >= 0x0 && idx <= 0xF)
        //    {
        //        BitArray bitArr = new BitArray(BitConverter.GetBytes(data));
        //        val = Convert.ToBoolean(bitArr[idx]);
        //        ret = true;
        //    }
        //    return ret;
        //}

        //private bool ReadUInt32BitFromIndex(int data, int idx, out bool val)
        //{
        //    bool ret = false;
        //    val = false;
        //    if (idx >= 0x0 && idx <= 0x1F)
        //    {
        //        BitArray bitArr = new BitArray(BitConverter.GetBytes(data));
        //        val = Convert.ToBoolean(bitArr[idx]);
        //        ret = true;
        //    }
        //    return ret;
        //}
    }
}
