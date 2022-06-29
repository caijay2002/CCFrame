using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：TcpServer
// 创 建 者：蔡程健
// 创建时间：22/6/28 17:15:44
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Ethernet
{
    public class SocketServer
    {
        private int _portNumber = 8800;
        public List<SocketClient> _clientList = new List<SocketClient>();//连接清单
        private bool _isRun = true;
        /// <summary>
        /// 默认8800端口
        /// </summary>
        public SocketServer() { }

        public SocketServer(int port) { _portNumber = port; }

        public void Initialize()
        {

        }

        public async void Start()
        {
            //每隔2分钟检测一次
            using (var timer = new Timer(TimerSessionCleanup, null, TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(2)))
            {
                await RunServerAsync();
            }
        }

        public void Stop()
        {
            _isRun = false;
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <returns></returns>
        public async Task RunServerAsync()
        {
            try
            {
                var listener = new TcpListener(IPAddress.Any, _portNumber);
                Console.WriteLine($"{listener.LocalEndpoint} started at port {_portNumber}");
                listener.Start();

                while (true)
                {
                    Console.WriteLine("waiting for client...");
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    //方法1
                    Task t = Accept(client);
                    //方法2
                    //Task t = RunClientRequestAsync(client);
                }
            }
            catch (Exception ex)
            {
                Log.LogSvr.Error($"Exception of type {ex.GetType().Name}, Message: {ex.Message}");
                Console.WriteLine($"Exception of type {ex.GetType().Name}, Message: {ex.Message}");
            }
        }

        /// <summary>
        /// 客户端接入
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        private Task Accept(TcpClient client)
        {
            return Task.Run(() =>
            {
                try
                {
                    Socket sc = client.Client;
                    if (sc != null)
                    {
                        SocketClient socketClient = new SocketClient(sc);
                        Console.WriteLine($"{socketClient.Address} : Connected");
                        socketClient.SocketEndReceive += client_SocketDataEndReceive;    //收到数据    
                        _clientList.Add(socketClient);
                        socketClient.BeginReceive();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception of type {ex.GetType().Name}, Message: {ex.Message}");
                }
            });
        }

        //处理接收到的内容，一般就是处理出现的异常，完成从客户端列表剔除功能
        void client_SocketDataEndReceive(SocketClient sender, IAsyncResult status, SocketData socketData, SocketError error)
        {
            //这里只负责对断开的连接剔除列表，至于断开的连接资源释放放到客户端Socket的逻辑处理中，避免反复释放出现问题
            if (error != SocketError.Success || socketData.Length == 0)
                _clientList.Remove(sender);

            Console.WriteLine(socketData.ToString());

            Send(sender, new SocketData("test data"));
        }

        /// <summary>
        /// 同步发送
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        private Task RunClientRequestAsync(TcpClient client)
        {
            return Task.Run(async () =>
            {
                try
                {
                    using (client)
                    {
                        Log.LogSvr.Info($"{client.Client.LocalEndPoint} : Connected : {client.Connected} ");
                        Console.WriteLine($"{client.Client.LocalEndPoint} : Connected : {client.Connected} ");

                        //Socket sc = client.Client;
                        //SocketClient socketClient = new SocketClient(sc);
                        //_clientList.Add(socketClient);

                        using (NetworkStream stream = client.GetStream())
                        {
                            do
                            {
                                byte[] readBuffer = new byte[1024];
                                int read = await stream.ReadAsync(readBuffer, 0, readBuffer.Length);
                                string request = Encoding.ASCII.GetString(readBuffer, 0, read);
                                Console.WriteLine($"received {request}");

                                byte[] writeBuffer = null;
                                string response = string.Empty;
                                
                                writeBuffer = Encoding.ASCII.GetBytes("aaa");
                                await stream.WriteAsync(writeBuffer, 0, writeBuffer.Length);
                                await stream.FlushAsync();
                                Console.WriteLine($"returned {Encoding.ASCII.GetString(writeBuffer, 0, writeBuffer.Length)}");
                            } while (_isRun);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception in client request handling of type {ex.GetType().Name}, Message: {ex.Message}");
                }
                Console.WriteLine("client disconnected");
            });
        }

        //对所有连接的客户端广播信息
        public void Send(SocketData data)
        {
            foreach (var item in _clientList)
            {
                if (!item.Socket.Connected) continue;
                item.BeginSend(data.Buttf);
            }
        }

        //指定远端发送数据
        public void Send(SocketClient client, SocketData data)
        {
            foreach (var item in _clientList)
            {
                if (!item.Socket.Connected) continue;
                if (item.Socket.RemoteEndPoint == client.Socket.RemoteEndPoint) item.BeginSend(data.Buttf);
            }
        }

        private void TimerSessionCleanup(object o) { }
    }
}
