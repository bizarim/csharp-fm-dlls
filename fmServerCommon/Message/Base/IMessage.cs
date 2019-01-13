using fmLibrary;
using System;

namespace fmServerCommon
{
    public abstract class IMessage : IDisposable
    {
        protected appServer m_server;
        public abstract void Process();        
        public abstract void Exclude();

        public void SetServer(appServer server) { m_server = server; }

        protected bool m_disposed;

        ~IMessage()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed) return;
            if (disposing)
            {
                Release();
            }
            m_disposed = true;
        }

        protected abstract void Release();
    }
}
