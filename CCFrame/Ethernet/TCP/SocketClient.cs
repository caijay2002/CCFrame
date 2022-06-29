using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Net;
using System.IO;


#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：SocketClient
// 创 建 者：蔡程健
// 创建时间：22/6/29 9:51:31
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Ethernet
{
    public delegate void SocketEndConnectEvent(SocketClient sender, IAsyncResult status, Exception ex);
    public delegate void SocketEndSendEvent(SocketClient sender, IAsyncResult status, SocketData socketData, SocketError error);
    public delegate void SocketEndReceiveEvent(SocketClient sender, IAsyncResult status, SocketData socketData, SocketError error);

    public class SocketClient
    {
        public Socket Socket { get; set; }

        /// <summary>
        /// 缓冲区大小
        /// </summary>
        public int MaxBufferSize { get; set; }
        /// <summary>
        /// 主机
        /// </summary>
        string Host { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        int Port { get; set; }

        public string Address { get { return $"{Host}:{Port}"; } }

        public bool IsConnected
        {
            get
            {
                if (Socket == null) return false;
                return Socket.Connected;
            }
        }

        /// <summary>
        /// 同步发送超时
        /// </summary>
        int? Timeout { get; set; }

        //连接结束
        public event SocketEndConnectEvent SocketEndConnect;
        //发送结束
        public event SocketEndSendEvent SocketEndSend;
        //接收结束
        public event SocketEndReceiveEvent SocketEndReceive;

        public SocketClient()
        {
            MaxBufferSize = 4096;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        }

        public SocketClient(string host, int port)
        {
            MaxBufferSize = 4096;
            Host = host;
            Port = port;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

        }

        public SocketClient(string host, int port, int timeout)
        {
            MaxBufferSize = 4096;
            Host = host;
            Port = port;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            Timeout = timeout;
            Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, Timeout.Value);
        }

        public SocketClient(string host, int port, int timeout, string localAddress, int localPort)
        {
            MaxBufferSize = 4096;
            Host = host;
            Port = port;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            Socket.Bind(new IPEndPoint(IPAddress.Parse(localAddress), localPort));
            Timeout = timeout;
            Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, Timeout.Value);
        }

        public SocketClient(Socket socket, int maxBufferSize = 4096)
        {
            MaxBufferSize = maxBufferSize;
            Socket = socket;
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public void BeginConnect(string host, int port)
        {
            Socket.BeginConnect(host, port, new AsyncCallback(EndConnect), null);
        }

        public void BeginConnect()
        {
            Socket.BeginConnect(Host, Port, new AsyncCallback(EndConnect), null);
        }

        void EndConnect(IAsyncResult status)
        {
            try
            {
                Socket.EndConnect(status);
                if (SocketEndConnect != null)
                    SocketEndConnect(this, status, null);
            }
            catch (Exception ex)
            {
                if (SocketEndConnect != null)
                    SocketEndConnect(this, status, ex);
            }
        }

        /// <summary>
        /// 连接，连接失败抛出异常
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            Socket.Connect(Host, Port);
            return Socket.Connected;
        }

        public bool Close()
        {
            if (Socket.Connected)
            {
                Socket.Close();
            }
            return Socket.Connected;
        }

        /// <summary>
        /// 尝试连接，连接失败不抛出异常
        /// </summary>
        /// <returns></returns>
        public bool TryConnect()
        {
            try
            {
                Socket.Connect(Host, Port);
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("断开套接字连接后，只能通过异步方式再次重新连接，而且只能连接到不同的 EndPoint。") != -1)
                {
                    Socket.Close();
                    Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                    if (Timeout.HasValue)
                        Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, Timeout.Value);
                }
            }
            return Socket.Connected;
        }


        #region 异步通讯

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data"></param>
        public void BeginSend(byte[] data)
        {
            //发送数据
            SocketData sendData = new SocketData(data);
            Socket.BeginSend(sendData.Buttf, 0, sendData.Buttf.Length, SocketFlags.None, new AsyncCallback(EndSend), sendData);
        }

        void EndSend(IAsyncResult status)
        {
            //获取数据
            SocketError error;
            Socket.EndSend(status, out error);
            SocketData sData = status.AsyncState as SocketData;
            //反馈信息
            if (SocketEndSend != null)
                SocketEndSend(this, status, sData, error);
        }

        /// <summary>
        /// 开始读取数据
        /// </summary>
        public void BeginReceive()
        {
            SocketData socketData = new SocketData(MaxBufferSize);
            Socket.BeginReceive(socketData.Buttf, 0, socketData.Buttf.Length, SocketFlags.None, new AsyncCallback(EndReceive), socketData);
        }

        /// <summary>
        /// 读到数据并用事件抛送出去
        /// </summary>
        /// <param name="status"></param>
        void EndReceive(IAsyncResult status)
        {
            //获取数据
            SocketError error;
            int num = Socket.EndReceive(status, out error);
            SocketData sData = status.AsyncState as SocketData;
            sData.Length = num;

            //开始新一轮的数据接收
            if (error == SocketError.Success && num != 0)
            {//只有接收正确状态下才继续新的接收
                SocketData socketData = new SocketData(MaxBufferSize);
                Socket.BeginReceive(socketData.Buttf, 0, socketData.Buttf.Length, SocketFlags.None, new AsyncCallback(EndReceive), socketData);
            }

            //调用事件处理数据
            if (SocketEndReceive != null)
                SocketEndReceive(this, status, sData, error);//这是顺带把错误问题一起处理了
        }

        #endregion


        #region 同步通讯


        public SocketData Request(byte[] buffer)
        {
            if (Socket.Connected == false)
            {//尝试连接，如果失败就直接返回长度为0的空数据
                var connected = TryConnect();
                if (connected == false)
                {
                    return new SocketData(new byte[] { }) { Length = 0 };
                }
            }
            SocketData socketData = new SocketData(MaxBufferSize);
            try
            {
                Socket.SendTo(buffer, SocketFlags.None, Socket.RemoteEndPoint);
                //Socket.Send(buffer, buffer.Length, Socket.RemoteEndPoint);
                socketData.Length = Socket.Receive(socketData.Buttf);

                return socketData;
            }
            catch (SocketException ex)
            {

                if (ex.ErrorCode == 10060)
                {
                    socketData.Length = 0;
                    return socketData;
                }
                else
                {
                    return new SocketData(new byte[] { }) { Error = ex };
                    //throw ex;
                }
            }
        }

        #endregion
    }
}
