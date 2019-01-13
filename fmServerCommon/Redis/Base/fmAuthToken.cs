using System;

namespace fmServerCommon
{
    public class fmAuthToken : IDisposable
    {
        public long AccID { get; set; }
        public int GameSvr { get; set; }
        public string Token { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~fmAuthToken()
        {
            Dispose(false);
        }

        bool m_disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed) return;
            //if (disposing) { }
            m_disposed = true;
        }
    }
}
