using System;

namespace fmServerCommon
{
    public class fmRankerKey : IDisposable
    {
        public long AccId { get; set; }
        public string Name { get; set; }
        //public int Floor { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~fmRankerKey()
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