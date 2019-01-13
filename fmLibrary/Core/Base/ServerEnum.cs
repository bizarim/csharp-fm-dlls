using System;

namespace fmLibrary
{
    public enum CloseReason
    {
        Unknown = 0,
        NotConnected,
        SocketError,
        RemoteClose,

        TimeoutSession,
        InvalidReceiveData,
        InvalidPacketHeader,
        InvalidOutOfPacketSize,

        ThreadExclude,
        SessionSleep,
        Logout,
        Block,
        BeforeSession,
    }
}
