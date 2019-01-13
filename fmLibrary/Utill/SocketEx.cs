using System;
using System.Net.Sockets;

namespace fmLibrary
{
    // http://msdn.microsoft.com/ko-kr/library/vstudio/system.net.sockets.socketerror(v=vs.100).aspx

    public static class SocketEx
    {
        public static void CloseEx(this Socket socket)
        {
            if (null == socket)
                return;

            if (!socket.Connected)
                return;

            try
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch
            {
            }
        }

        public static bool IsIgnorableError(SocketError error)
        {
            if (error == SocketError.Interrupted
                || error == SocketError.ConnectionAborted
                || error == SocketError.ConnectionReset
                || error == SocketError.OperationAborted)
            {
                return true;
            }
            return false;
        }
    }
}
