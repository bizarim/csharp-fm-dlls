using System;

namespace fmCommon
{
    public interface IfmObject : IDisposable, ICloneable
    {
        // fmObject 소속 관계와
        // IDisposable 구현과
        // ICloneable 구현 하도록 인터페이스
    }

    public abstract class fmObject : IDisposable
    {
        public delegate void fnFREE(fmObject _fmObject);
        protected fnFREE m_fnFree = null;
        public void SetFnFree(fnFREE fnFree) { m_fnFree = fnFree; }

        protected fmObject() { }

        public void Dispose()
        {
            Dispose(true);
            //GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Free();
            }
        }

        ~fmObject()
        {
            Dispose(false);
        }

        private void Free()
        {
            if (m_fnFree != null)
                m_fnFree(this);
            else
            {
                throw new Exception(string.Format("Caution!! Use fmObjectPool: {0}", this.GetType().ToString()));
            }
        }

        protected abstract void Release();
    }
}
