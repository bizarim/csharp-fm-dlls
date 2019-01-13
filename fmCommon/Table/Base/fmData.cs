using System;

namespace fmCommon
{
    public abstract partial class fmData
    {
        protected eFmDataType m_eFmDataType = eFmDataType.None;
        public eFmDataType GetFmDataType() { return m_eFmDataType; }
        public int Code { get { return GetCode(); } }
        protected abstract int GetCode();
        public virtual void EncodeDecode(eCoderType eType, BufferCoder coder) { }
    }
}
