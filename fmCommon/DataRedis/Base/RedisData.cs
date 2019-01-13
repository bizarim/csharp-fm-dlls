using System;
//using System.Runtime.Serialization;
//using System.Web.Script.Serialization;

namespace fmCommon
{
    // RedisData
    public abstract class RedisData : IDisposable, IPacketable, ICloneable
    {
        //[ScriptIgnore]
        protected bool m_disposed;

        protected abstract void Release();
        public virtual void Read(ref Packet p) { }
        public virtual void Write(ref Packet p) { }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~RedisData()
        {
            Dispose(false);
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

        public virtual object Clone() { return null; }
        public virtual string ToJson() { return string.Empty; }
    }
}
