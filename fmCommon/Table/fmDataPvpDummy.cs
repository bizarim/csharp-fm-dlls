namespace fmCommon
{
    public class fmDataPvpDummy : fmData
    {
        public int m_nCode = 0;
        public int m_nLv = 0;

        public fmDataPvpDummy()
        {
            m_eFmDataType = eFmDataType.PvpDummy;
        }

        protected override int GetCode()
        {
            return m_nCode;
        }

        public override void EncodeDecode(eCoderType eType, BufferCoder coder)
        {
            coder.EncodeDecode(eType, ref m_nCode);
            coder.EncodeDecode(eType, ref m_nLv);
        }
    }
}
