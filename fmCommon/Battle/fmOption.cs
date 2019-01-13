using System;

namespace fmCommon.Battle
{
    public class fmOption : IDisposable
    {
        public eOption Kind { get; set; }
        public float Value { get; set; }

        protected bool m_disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~fmOption()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed) return;
            if (disposing)
            {
                Kind = eOption.None;
                Value = 0f;
            }
            m_disposed = true;
        }
    }

}
