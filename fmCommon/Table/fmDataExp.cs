namespace fmCommon
{
    public class fmDataExp : fmData
    {
        public int m_nCode          = 0;
        public long m_biTotalExp    = 0;
        public long m_biNeedExp     = 0;

        public fmDataExp() { m_eFmDataType = eFmDataType.Exp; }
        protected override int GetCode() { return m_nCode; }
        public override void EncodeDecode(eCoderType eType, BufferCoder coder)
        {
            coder.EncodeDecode(eType, ref m_nCode);
            coder.EncodeDecode(eType, ref m_biTotalExp);
            coder.EncodeDecode(eType, ref m_biNeedExp);
        }
    }
}
