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

namespace CCFrame.Driver.OPC
{
    public delegate void MonitorDataChanged(string key, string value);

    public class OpcUADriver
    {
        /// <summary>
        /// IP地址
        /// </summary>
        private string m_strIpAddress { get; set; }
        /// <summary>
        /// 连接超时
        /// </summary>
        private int m_ConnectTimeout { get; set; }

        /// <summary>
        /// 应用配置
        /// </summary>
        private ApplicationConfiguration m_configuration;

        private Session m_session;

        private Action<IList, IList> m_validateResponse;

        private Subscription subscription;

        public event MonitorDataChanged MonitorDataChanged;

        /// <summary>
        /// 读取数据的节点
        /// </summary>
        private ReadValueIdCollection nodesToRead = new ReadValueIdCollection();

        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        public async Task<OperateResult> Connect()
        {
            try
            {
                var result = Ethernet.EthernetHelper.PingDevice(m_strIpAddress, m_ConnectTimeout);
                if (result.IsSuccess)
                {
                    try
                    {
                        if (m_session != null && m_session.Connected == true)
                        {

                        }
                        else
                        {
                            EndpointDescription endpointDescription = CoreClientUtils.SelectEndpoint(m_strIpAddress, false);

                            EndpointConfiguration endpointConfiguration = EndpointConfiguration.Create(m_configuration);

                            ConfiguredEndpoint endpoint = new ConfiguredEndpoint(null, endpointDescription, endpointConfiguration);

                            Session session = await Session.Create(
                                m_configuration,
                                endpoint,
                                false,
                                false,
                                m_configuration.ApplicationName,
                                30 * 60 * 1000,
                                new UserIdentity(),
                                null
                            );

                            if (session != null && session.Connected)
                            {
                                m_session = session;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.LogSvr.Error($"Create Session Error : {ex.Message}");
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
        public async void Initialize()
        {
            ApplicationInstance application = new ApplicationInstance();
            application.ApplicationName = "Quickstart Console Reference Client";
            application.ApplicationType = ApplicationType.Client;

            await application.LoadApplicationConfiguration("ConsoleReferenceClient.Config.xml", silent: false);
            await application.CheckApplicationInstanceCertificate(silent: false, minimumKeySize: 0);

            InitConfig("ConnentDemoConfig");

            m_validateResponse = ClientBase.ValidateResponse;
            m_configuration = application.ApplicationConfiguration;
            m_configuration.CertificateValidator.CertificateValidation += CertificateValidation;
        }

        /// <summary>
        /// 初始化配置
        /// </summary>
        /// <returns></returns>
        private static ApplicationInstance InitConfig(string fileName)
        {
            /**
            *不加证书验证配置会报错
            */
            var certificateValidator = new CertificateValidator();
            certificateValidator.CertificateValidation += (sender, eventArgs) =>
            {
                if (ServiceResult.IsGood(eventArgs.Error))
                    eventArgs.Accept = true;
                else if (eventArgs.Error.StatusCode.Code == StatusCodes.BadCertificateUntrusted)
                    eventArgs.Accept = true;
                else
                    throw new Exception(string.Format("Failed to validate certificate with error code {0}: {1}",
                        eventArgs.Error.Code, eventArgs.Error.AdditionalInfo));
            };
            return new ApplicationInstance
            {
                /*
                 * 指定应用类型
                 */
                ApplicationType = ApplicationType.Client,
                /*
                 * 配置名
                 */
                ConfigSectionName = fileName,
                /*
                 * 应用的个配置项
                 */
                ApplicationConfiguration = new ApplicationConfiguration
                {
                    ApplicationName = "ConnentDemo",
                    ApplicationType = ApplicationType.Client,
                    /*
                     * 证书验证配置
                     */
                    CertificateValidator = certificateValidator,
                    /*
                     * 服务端配置
                     */
                    ServerConfiguration = new ServerConfiguration
                    {
                        MaxSubscriptionCount = 100000,
                        MaxMessageQueueSize = 1000000,
                        MaxNotificationQueueSize = 1000000,
                        MaxPublishRequestCount = 10000000,
                    },

                    /*
                     * 安全配置
                     */
                    SecurityConfiguration = new SecurityConfiguration
                    {
                        AutoAcceptUntrustedCertificates = true,
                        //RejectSHA1SignedCertificates = false,
                        //MinimumCertificateKeySize = 1024,
                    },

                    TransportQuotas = new TransportQuotas
                    {
                        OperationTimeout = 6000000,
                        MaxStringLength = int.MaxValue,
                        MaxByteStringLength = int.MaxValue,
                        MaxArrayLength = 65535,
                        MaxMessageSize = 419430400,
                        MaxBufferSize = 65535,
                        ChannelLifetime = -1,
                        SecurityTokenLifetime = -1
                    },
                    /*
                     * 客户端配置
                     */
                    ClientConfiguration = new ClientConfiguration
                    {
                        DefaultSessionTimeout = -1,
                        MinSubscriptionLifetime = -1,
                    },

                    DisableHiResClock = true
                }
            };
        }

        /// <summary>
        /// 初始化订阅
        /// </summary>
        public void InitializeSubscribe()
        {
            if (m_session == null || m_session.Connected == false)
            {
                return;
            }

            try
            {
                subscription = new Subscription(m_session.DefaultSubscription);

                subscription.DisplayName = "Console ReferenceClient Subscription";
                subscription.PublishingEnabled = true;
                subscription.PublishingInterval = 1000;

                m_session.AddSubscription(subscription);

                subscription.Create();
            }
            catch (Exception ex)
            {
                Log.LogSvr.Error($"InitializeSubscribe Error : {ex.Message}");
            }
        }

        /// <summary>
        /// 注册 订阅数据
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="itemType"></param>
        public void RegisterSubscribe(string itemName, string itemType)
        {
            try
            {
                MonitoredItem intMonitoredItem = new MonitoredItem(subscription.DefaultItem);
                // Int32 Node - Objects\CTT\Scalar\Simulation\Int32
                intMonitoredItem.StartNodeId = new NodeId(itemName);
                intMonitoredItem.AttributeId = Attributes.Value;
                intMonitoredItem.DisplayName = itemType;
                intMonitoredItem.SamplingInterval = 1000;
                intMonitoredItem.Notification += OnMonitoredItemNotification;

                subscription.AddItem(intMonitoredItem);

                subscription.ApplyChanges();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(" RegisterSubscribe Error: " + ex.Message);
                Log.LogSvr.Error($"RegisterSubscribe Error : {ex.Message}");
            }
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
        /// 监听数据项
        /// </summary>
        private void OnMonitoredItemNotification(MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs e)
        {
            try
            {
                // Log MonitoredItem Notification event
                MonitoredItemNotification notification = e.NotificationValue as MonitoredItemNotification;

                if (MonitorDataChanged != null)
                    MonitorDataChanged(monitoredItem.StartNodeId.ToString(), notification.Value.ToString());
                //ns=3;s="MES_To_PLC_DATA"."MES_2_PLC"."Heart_Beat"
                if (monitoredItem.StartNodeId.ToString() != "ns=3;s=\"MES_To_PLC_DATA\".\"MES_2_PLC\".\"Heart_Beat\"")
                    Console.WriteLine("Notification Received for Variable \"{0}\" and Value = {1}.", monitoredItem.StartNodeId, notification.Value.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
                Connect();
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
                Connect();
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

        /// <summary>
        /// 读取数值
        /// </summary>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        public OperateResult<string> ReadValue(string nodeID)
        {
            OperateResult<string> operateResult = new OperateResult<string>();

            ReadValueIdCollection readNode = new ReadValueIdCollection();

            readNode.Add(new ReadValueId() { NodeId = nodeID, AttributeId = Attributes.Value });

            if (m_session == null || m_session.Connected == false)
            {
                operateResult.IsSuccess = false;
                operateResult.Message = "nodeID:" + nodeID + "m_session == null || m_session.Connected == false";
                return operateResult;
            }

            try
            {
                m_session.Read(
                    null,
                    0,
                    TimestampsToReturn.Both,
                    readNode,
                    out DataValueCollection resultsValues,
                    out DiagnosticInfoCollection diagnosticInfos);

                m_validateResponse(resultsValues, readNode);

                foreach (DataValue result in resultsValues)
                {
                    if (result.StatusCode.Code != 0)
                    {
                        Console.WriteLine("  " + result.ToString());
                        operateResult.ErrorCode = (int)result.StatusCode.Code;
                        operateResult.IsSuccess = false;
                        operateResult.Message = result.ToString();
                        return operateResult;
                    }
                    else
                    {
                        operateResult.Content = result.Value.ToString();
                    }
                }

                //DataValue namespaceArray = m_session.ReadValue(Variables.Server_NamespaceArray);

                operateResult.IsSuccess = true;
                return operateResult;

            }
            catch (Exception ex)
            {
                Log.LogSvr.Error($"Write Data Errorr : {ex.Message}");

                //发生异常强制断开连接
                Disconnect();

                return OperateResult.CreateFailedResult<string>(new OperateResult($" Write Data Error: {ex.Message}"));
            }
        }



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
                //MessageBox.Show("Untrusted Certificate accepted. SubjectName = {0}", e.Certificate.SubjectName.ToString());
            }
        }
    }
}
