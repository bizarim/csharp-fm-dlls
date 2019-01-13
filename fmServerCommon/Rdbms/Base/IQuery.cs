using System;

namespace fmServerCommon
{
    // 상속받는 곳에서 목적에 맞게 구현해라.
    public abstract class IQuery : IDisposable
    {
        public string m_strCommand { get; set; }
        public abstract bool Execute();
        protected abstract void Free();
        public abstract void OnResult(object reader, eDBError eError, string strError = "");

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~IQuery()
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
