using fmLibrary;
using System.Net.Sockets;


namespace fmServerCommon
{
    // 서버용 세션
    public class ServerSession : Session
    {
        public descOtherServer m_descServer;

        public ServerSession(Socket socket, SocketAsyncEventArgs recvSAEA, SocketAsyncEventArgs sendSAEA, PooledBufferManager pooledBufferManager)
            : base(socket, recvSAEA, sendSAEA, pooledBufferManager)
        {
        }

        public override void Dispose()
        {
            m_descServer.Dispose();
            base.Dispose();
        }
    }
}
