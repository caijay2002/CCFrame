using System;
using System.Collections.Concurrent;

#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：SessionManager
// 创 建 者：蔡程健
// 创建时间：22/6/29 9:30:00
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Ethernet
{

    public class SessionManager
    {
        public static readonly TimeSpan SessionTimeout = TimeSpan.FromMinutes(2);

        private readonly ConcurrentDictionary<string, SocketClient> _socketClients = new ConcurrentDictionary<string, SocketClient>();

        public string AddClient(SocketClient socketClient)
        {
            //string sessionId = socketClient.c;
            //if (_socketClients.TryAdd(sessionId, socketClient))
            //{
            //    return sessionId;
            //}
            //else
            //{
            //    return string.Empty;
            //}
            return string.Empty;
        }
        public void CleanupAllSessions()
        {
            //foreach (var session in _sessions)
            //{
            //    if (session.Value.LastAccessTime + SessionTimeout >= DateTime.UtcNow)
            //    {
            //        CleanupSession(session.Key);
            //    }
            //}
        }

        public void CleanupSession(string sessionId)
        {
            //if (_sessions.TryRemove(sessionId, out Session header))
            //{
            //    Console.WriteLine($"removed {sessionId} from sessions");
            //}
        }
    }


}
