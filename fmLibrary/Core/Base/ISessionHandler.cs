using System;
using System.Net.Sockets;

namespace fmLibrary
{
    // SessionHandler를 만든이유는 
    // SessionBase를 상속 받아 appServer 별로 Session을 다르게 구성 할 수 있게 하기 위해서.
    // 특히 ServerSession인지 ClinetSession인지 구분해서 기능들을 추가 할 수 있게 하기 위해서.
    // ClinetSession을 쿠키 처럼 구현 할 수 있지 않을까 해서.

    public interface ISessionHandler
    {
        SessionBase CreateSession(Socket socket);
        void DestroySession(SessionBase session);
    }
}
