using System;
using System.Net.Sockets;

namespace fmLibrary
{
    public class Connector : IDisposable
    {
        public delegate void ConnectHandler(Connector connector, Socket socket, bool isSuccess);
        public delegate void ErrorHandler(Connector connector, Exception ex);

        public ConnectHandler m_handlerAttachResult;
        public ErrorHandler m_handlerError;

        protected SessionBase m_session;

        public void SetSession(SessionBase session)
        {
            m_session = session;
        }

        protected void Connect(string ip, int port)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.BeginConnect(ip, port, CompletedConnectResult, socket);
        }

        void CompletedConnectResult(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            try
            {
                socket.EndConnect(ar);
                OnConnectResult(socket, true);
            }
            catch (Exception ex)
            {
                OnError(ex);
                OnConnectResult(socket, false);
            }
        }

        void OnConnectResult(Socket socket, bool isSuccess)
        {
            if (m_handlerAttachResult != null)
                m_handlerAttachResult(this, socket, isSuccess);

            OnAttachResult(socket, isSuccess);
        }

        void OnError(Exception ex)
        {
            if (m_handlerError != null)
                m_handlerError(this, ex);
        }

        public void Dispose()
        {

        }

        public virtual void OnAttachResult(Socket socket, bool isSuccess)
        {
        }
    }
}
