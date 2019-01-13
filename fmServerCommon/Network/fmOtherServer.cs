using System;

namespace fmServerCommon
{
    public class fmOtherServer : IDisposable
    {
        public ServerSession m_session;
        public descOtherServer m_desc;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~fmOtherServer()
        {
            Dispose(false);
        }

        bool m_disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed) return;
            if (disposing)
            {
                if (null != m_desc)
                {
                    m_desc.Dispose();
                    m_desc = null;
                }

                m_session = null;
            }
            m_disposed = true;
        }
    }
}
