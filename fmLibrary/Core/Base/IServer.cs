using System;

namespace fmLibrary
{
    public interface IServer
    {
        bool LoadConfig(string[] args);
        bool LoadData();

        bool Initialize();
        void Uninitialize();

        void Ready();   // 대기 Start는 Server내부에서. 실행과 동시에 Start하기 싫어서.
        bool Start();
        bool Stop();
    }
}
