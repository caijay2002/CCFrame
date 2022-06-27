using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Configuration;

#region << OPC UA 驱动 >>
/*----------------------------------------------------------------
// 文件名称：OpcUADriver
// 创 建 者：蔡程健
// 创建时间：22/6/10 16:10:26
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Driver
{
    //public delegate void MonitorDataChanged(string key, string value);

    public class OpcUADriver
    {
        /// <summary>
        /// IP地址
        /// </summary>
        private string m_IpAddress { get; set; }
        /// <summary>
        /// 连接超时
        /// </summary>
        private int m_Port { get; set; }

        /// <summary>
        /// 应用配置
        /// </summary>
        private ApplicationConfiguration m_configuration;

        private Session m_session;

        private Action<IList, IList> m_validateResponse;

        public bool IsConnected
        {
            get
            {
                if (m_session == null) return false;
                return m_session.Connected;
            }
        }

        /// <summary>
        /// 读取数据的节点
        /// </summary>
        private ReadValueIdCollection nodesToRead = new ReadValueIdCollection();


        public string UserName { get; set; }

        public string Password { get; set; }

        public string ServerUrl { get; set; } // "opc.tcp://192.168.0.177:49321";

        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        public async Task<OperateResult> Connect()
        {
            try
            {
                var result = Ethernet.EthernetHelper.PingDevice(m_IpAddress, 3000);
                if (result.IsSuccess)
                {
                    try
                    {
                        if (IsConnected)
                        {
                            Log.LogSvr.Info($"{ServerUrl} Session already connected!");
                        }
                        else
                        {
                            EndpointDescription endpointDescription = CoreClientUtils.SelectEndpoint(ServerUrl, false);

                            EndpointConfiguration endpointConfiguration = EndpointConfiguration.Create(m_configuration);

                            ConfiguredEndpoint endpoint = new ConfiguredEndpoint(null, endpointDescription, endpointConfiguration);

                            Session session;

                            if (UserName != null)
                            {
                                session = await Session.Create(
                                m_configuration,
                                endpoint,
                                false,
                                false,
                                m_configuration.ApplicationName,
                                30 * 60 * 1000,
                                new UserIdentity(UserName, Password),
                                null);
                            }
                            else
                            {
                                session = await Session.Create(
                                   m_configuration,
                                   endpoint,
                                   false,
                                   false,
                                   m_configuration.ApplicationName,
                                   30 * 60 * 1000,
                                   null,
                                   null);

                            }

                            if (session != null && session.Connected)
                            {
                                m_session = session;
                                return OperateResult.CreateSuccessResult();
                            }                           
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.LogSvr.Error($"Create Session Error : {ex.Message}");
                        return OperateResult.CreateFailedResult(new OperateResult($"Create Session Error : {ex.Message}"));
                    }

                    return OperateResult.CreateFailedResult(new OperateResult($"Connect Session Error"));
                }
                else
                {
                    return OperateResult.CreateFailedResult(new OperateResult($"Ping {m_IpAddress} 失败 内容{result.Message}"));
                }
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailedResult(new OperateResult($"[{m_IpAddress}] QPlcMonitorDriver Connect, Exception:{ex.Message}"));
            }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void Disconnect()
        {
            try
            {
                if (m_session != null)
                {
                    m_session.Close();
                    m_session.Dispose();
                    m_session = null;
                }
                else
                {
                    Log.LogSvr.Error($"m_session is not Initialize");
                }
            }
            catch (Exception ex)
            {
                Log.LogSvr.Error($"Disconnect Error : {ex.Message}");
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize(List<DriverConfigItem> configItems)
        {
            foreach (var item in configItems)
            {
                switch (item.Key)
                {
                    case "IpAddress":
                        m_IpAddress = item.Value;
                        break;
                    case "Port":
                        m_Port = Convert.ToInt32(item.Value);
                        break;
                }
            }

            //根据IP和端口初始化连接字符串
            ServerUrl = $"opc.tcp://{m_IpAddress}:{m_Port}";

            ApplicationInstance application = InitConfig();
            application.ApplicationType = ApplicationType.Client;

            m_validateResponse = ClientBase.ValidateResponse;
            m_configuration = application.ApplicationConfiguration;
            m_configuration.CertificateValidator.CertificateValidation += CertificateValidation;
        }

        /// <summary>
        /// 初始化配置
        /// </summary>
        /// <returns></returns>
        private ApplicationInstance InitConfig()
        {
            ApplicationInstance application = new ApplicationInstance();
            application.ApplicationType = ApplicationType.Client;
            var ap = new ApplicationConfiguration()
            {
                ApplicationName = "BandexOPCClient",
                ApplicationUri = $"opc.tcp://{m_IpAddress}:{m_Port}/",
                ClientConfiguration = new ClientConfiguration
                {
                    DefaultSessionTimeout = 60000,
                    WellKnownDiscoveryUrls = new StringCollection()
                    {
                        $"opc.tcp://{m_IpAddress}:{m_Port}/"
                    }
                }
            };
            application.ApplicationConfiguration = ap;
            return application;
        }
        
        /// <summary>
        /// 注册 更新数据项
        /// </summary>
        /// <param name="itemName"></param>
        public void RegisterUpdate(string itemName)
        {
            nodesToRead.Add(new ReadValueId() { NodeId = itemName, AttributeId = Attributes.Value });
        }

        
        /// <summary>
        /// 写入节点地址，和数值
        /// </summary>
        public OperateResult WriteStringValue(string nodeID, string value)
        {
            OperateResult operateResult = new OperateResult();

            try
            {
                // Write the configured nodes
                WriteValueCollection nodesToWrite = new WriteValueCollection();

                // Int32 Node - Objects\CTT\Scalar\Scalar_Static\Int32
                WriteValue intWriteVal = new WriteValue();
                intWriteVal.NodeId = new NodeId(nodeID);
                intWriteVal.AttributeId = Attributes.Value;
                intWriteVal.Value = new DataValue();
                intWriteVal.Value.Value = value;
                nodesToWrite.Add(intWriteVal);

                // Write the node attributes
                StatusCodeCollection results = null;
                DiagnosticInfoCollection diagnosticInfos;

                // Call Write Service
                m_session.Write(null,
                                nodesToWrite,
                                out results,
                                out diagnosticInfos);

                // Validate the response
                m_validateResponse(results, nodesToWrite);

                foreach (StatusCode writeResult in results)
                {
                    if (writeResult.Code != 0)
                    {
                        Console.WriteLine("  " + writeResult.ToString());
                        operateResult.ErrorCode = (int)writeResult.Code;
                        operateResult.IsSuccess = false;
                        operateResult.Message = writeResult.ToString();
                        return operateResult;
                    }//写入失败

                }

                operateResult.IsSuccess = true;
                return operateResult;
            }
            catch (Exception ex)
            {
                Log.LogSvr.Error($"Write Data Errorr : {ex.Message}");

                //发生异常强制断开连接
                Disconnect();

                return OperateResult.CreateFailedResult(new OperateResult($" Write Data Error: {ex.Message}"));
            }
        }

        public OperateResult WriteInt16Value(string nodeID, Int16 value)
        {
            OperateResult operateResult = new OperateResult();

            if (m_session == null || m_session.Connected == false)
            {
                Connect().Wait();
            }

            if (m_session == null || m_session.Connected == false)
            {
                operateResult.IsSuccess = false;
                operateResult.Message = "nodeID:" + nodeID + "m_session == null || m_session.Connected == false";
                return operateResult;
            }

            try
            {
                // Write the configured nodes
                WriteValueCollection nodesToWrite = new WriteValueCollection();

                // Int32 Node - Objects\CTT\Scalar\Scalar_Static\Int32
                WriteValue intWriteVal = new WriteValue();
                intWriteVal.NodeId = new NodeId(nodeID);
                intWriteVal.AttributeId = Attributes.Value;
                intWriteVal.Value = new DataValue();
                intWriteVal.Value.Value = value;
                nodesToWrite.Add(intWriteVal);

                // Write the node attributes
                StatusCodeCollection results = null;
                DiagnosticInfoCollection diagnosticInfos;

                // Call Write Service
                m_session.Write(null,
                                nodesToWrite,
                                out results,
                                out diagnosticInfos);

                // Validate the response
                m_validateResponse(results, nodesToWrite);

                foreach (StatusCode writeResult in results)
                {
                    if (writeResult.Code != 0)
                    {
                        Console.WriteLine("  " + writeResult.ToString());
                        operateResult.ErrorCode = (int)writeResult.Code;
                        operateResult.IsSuccess = false;
                        operateResult.Message = writeResult.ToString();
                        return operateResult;
                    }//写入失败

                }

                operateResult.IsSuccess = true;
                return operateResult;
            }
            catch (Exception ex)
            {
                Log.LogSvr.Error($"Write Data Errorr : {ex.Message}");

                //发生异常强制断开连接
                Disconnect();

                return OperateResult.CreateFailedResult(new OperateResult($" Write Data Error: {ex.Message}"));
            }
        }

        public OperateResult WriteInt32Value(string nodeID, Int32 value)
        {
            OperateResult operateResult = new OperateResult();

            if (m_session == null || m_session.Connected == false)
            {
                Connect().Wait();
            }

            if (m_session == null || m_session.Connected == false)
            {
                operateResult.IsSuccess = false;
                operateResult.Message = "nodeID:" + nodeID + "m_session == null || m_session.Connected == false";
                return operateResult;
            }

            try
            {
                // Write the configured nodes
                WriteValueCollection nodesToWrite = new WriteValueCollection();

                // Int32 Node - Objects\CTT\Scalar\Scalar_Static\Int32
                WriteValue intWriteVal = new WriteValue();
                intWriteVal.NodeId = new NodeId(nodeID);
                intWriteVal.AttributeId = Attributes.Value;
                intWriteVal.Value = new DataValue();
                intWriteVal.Value.Value = value;
                nodesToWrite.Add(intWriteVal);

                // Write the node attributes
                StatusCodeCollection results = null;
                DiagnosticInfoCollection diagnosticInfos;

                // Call Write Service
                m_session.Write(null,
                                nodesToWrite,
                                out results,
                                out diagnosticInfos);

                // Validate the response
                m_validateResponse(results, nodesToWrite);

                foreach (StatusCode writeResult in results)
                {
                    if (writeResult.Code != 0)
                    {
                        Console.WriteLine("  " + writeResult.ToString());
                        operateResult.ErrorCode = (int)writeResult.Code;
                        operateResult.IsSuccess = false;
                        operateResult.Message = writeResult.ToString();
                        return operateResult;
                    }//写入失败
                }
                operateResult.IsSuccess = true;
                return operateResult;
            }
            catch (Exception ex)
            {
                Log.LogSvr.Error($"Write Data Errorr : {ex.Message}");

                //发生异常强制断开连接
                Disconnect();

                return OperateResult.CreateFailedResult(new OperateResult($" Write Data Error: {ex.Message}"));
            }
        }

        public OperateResult WriteValue(string nodeID, object value)
        {
            if(!IsConnected)
            {
                Connect().Wait();
            }

            if (!IsConnected) 
                return OperateResult.CreateFailedResult(new OperateResult($"nodeID:{nodeID}m_session == null || m_session.Connected == false"));
            try
            {
                WriteValueCollection nodesToWrite = new WriteValueCollection();

                WriteValue intWriteVal = new WriteValue();
                intWriteVal.NodeId = new NodeId(nodeID);
                intWriteVal.AttributeId = Attributes.Value;
                intWriteVal.Value = new DataValue();
                intWriteVal.Value.Value = value;
                nodesToWrite.Add(intWriteVal);

                StatusCodeCollection results = null;
                DiagnosticInfoCollection diagnosticInfos;

                m_session.Write(null,
                                nodesToWrite,
                                out results,
                                out diagnosticInfos);

                m_validateResponse(results, nodesToWrite);

                foreach (StatusCode writeResult in results)
                {
                    if (writeResult.Code != 0)
                    {
                        return OperateResult.CreateFailedResult(new OperateResult(
                            (int)writeResult.Code,
                            $"nodeID:{nodeID}m_session == null || m_session.Connected == false"
                            ));
                    }//写入失败
                }
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception ex)
            {
                Log.LogSvr.Error($"Write Data Errorr : {ex.Message}");

                //发生异常强制断开连接
                Disconnect();

                return OperateResult.CreateFailedResult(new OperateResult($" Write Data Error: {ex.Message}"));
            }
        }

        /// <summary>
        /// 读取数值
        /// </summary>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        public OperateResult<object> ReadData(string nodeID)
        {
            OperateResult<object> operateResult = new OperateResult<object>();

            ReadValueIdCollection readNode = new ReadValueIdCollection();

            readNode.Add(new ReadValueId() { NodeId = nodeID, AttributeId = Attributes.Value });

            if (!IsConnected)
            {
                return OperateResult.CreateFailedResult<object>(new OperateResult("nodeID:" + nodeID + "m_session == null || m_session.Connected == false"));
            }

            try
            {
                DataValue dataValue = m_session.ReadValue(nodeID);

                operateResult.IsSuccess = dataValue.StatusCode == 0;
                if (operateResult.IsSuccess) operateResult.Content = dataValue.Value;

                return operateResult;

            }
            catch (Exception ex)
            {
                Log.LogSvr.Error($"{m_IpAddress} nodeID {nodeID}  ReadData Errorr : {ex.Message}");

                //发生异常强制断开连接
                Disconnect();

                return OperateResult.CreateFailedResult<object>(new OperateResult($"{m_IpAddress} ReadData Error: {ex.Message}"));
            }
        }

        public async void UpdateData()
        {
            if (!IsConnected)
            {
                var ret = await Connect();

                if (!ret.IsSuccess) return;
            }

            try
            {
                m_session.Read(
                    null,
                    0,
                    TimestampsToReturn.Both,
                    nodesToRead,
                    out DataValueCollection resultsValues,
                    out DiagnosticInfoCollection diagnosticInfos);

                m_validateResponse(resultsValues, nodesToRead);

                for (int i = 0; i < nodesToRead.Count; i++)
                {

                    if (resultsValues[i].StatusCode.Code != 0)
                    {
                        Console.WriteLine("Error : " + resultsValues[i].ToString());
                        return;
                    }
                    else
                    {
                        Core.DataCacheSvr.UpdateCache("DataMap", nodesToRead[i].NodeId.ToString(), resultsValues[i].Value);

                    }
                }

                DataValue namespaceArray = m_session.ReadValue(Variables.Server_NamespaceArray);
                Log.LogSvr.Info($"{m_IpAddress} StatusCode {namespaceArray.StatusCode}");

            }
            catch (Exception ex)
            {
                Log.LogSvr.Error($"UpdateData Errorr : {ex.Message}");

                Disconnect();
            }
        }

        /// <summary>
        /// 证书验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CertificateValidation(CertificateValidator sender, CertificateValidationEventArgs e)
        {
            bool certificateAccepted = true;

            ServiceResult error = e.Error;
            while (error != null)
            {
                error = error.InnerResult;
            }

            if (certificateAccepted)
            {
                Log.LogSvr.Error($"Untrusted Certificate accepted. SubjectName = {e.Certificate.SubjectName.ToString()}");
            }
        }
    }
}
